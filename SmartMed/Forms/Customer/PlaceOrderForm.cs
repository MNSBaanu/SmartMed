using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class PlaceOrderForm : EmbeddedPageForm
    {
        private OrderService _orders;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;
        private string _prescriptionPath;

        public PlaceOrderForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _orders = new OrderService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
        }

        protected override void DoRefreshPage() => RefreshCart();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            UiTheme.SetGridDataSource(gridCart, DesignTimePreviewData.CartRows());
            BeautifyCartGrid();
            lblTotal.Text = "Total: LKR 975.00 (2 items)";
            lblRxNote.Visible = true;
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridCart);
            UiTheme.StyleTextBox(txtPrescriptionPath);

            WirePanelBorder(panelGridOuter);
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "dash-border") return;
            panel.Tag = "dash-border";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnRemoveSelected.Click += BtnRemove_Click;
            btnClearCart.Click += (s, e) =>
            {
                CartService.Clear();
                RefreshCart();
            };
            btnPlaceOrder.Click += BtnPlace_Click;
            btnUploadPrescription.Click += BtnBrowse_Click;
        }

        private void RefreshCart()
        {
            if (gridCart == null) return;

            if (PreferDesignTimePreview())
                return;

            UiTheme.SetGridDataSource(gridCart, CartService.Items.Select(l => new
            {
                l.MedicineID,
                l.MedicineName,
                l.Quantity,
                ListPrice = $"LKR {l.ListPrice:N2}",
                UnitPrice = $"LKR {l.UnitPrice:N2}",
                l.DiscountDisplay,
                l.PromoDisplay,
                Applied = l.OfferDisplay,
                Subtotal = $"LKR {l.Subtotal:N2}",
                Rx = l.RequiresPrescription ? "Yes" : "No"
            }).ToList());
            BeautifyCartGrid();
            lblTotal.Text = $"Total: LKR {CartService.Total:N2} ({CartService.ItemCount} items)";
            lblRxNote.Visible = CartService.RequiresPrescription;
        }

        private void BeautifyCartGrid()
        {
            if (gridCart.Columns.Contains("MedicineID"))
                gridCart.Columns["MedicineID"].Visible = false;
            if (gridCart.Columns.Contains("DiscountDisplay"))
                gridCart.Columns["DiscountDisplay"].HeaderText = "Discount";
            if (gridCart.Columns.Contains("PromoDisplay"))
                gridCart.Columns["PromoDisplay"].HeaderText = "Promo";
            UiTheme.BeautifyGridHeaders(gridCart);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Filter = "Prescription files|*.pdf;*.jpg;*.jpeg;*.png;*.bmp|All files|*.*",
                Title = "Select Prescription"
            })
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                _prescriptionPath = dialog.FileName;
                txtPrescriptionPath.Text = _prescriptionPath;
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (gridCart.CurrentRow == null) return;
            var id = Convert.ToInt32(gridCart.CurrentRow.Cells["MedicineID"].Value);
            CartService.Remove(id);
            RefreshCart();
        }

        private void BtnPlace_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var orderId = _orders.PlaceOrder(customer.CustomerID, CartService.Items, _prescriptionPath);
                CartService.Clear();
                _prescriptionPath = null;
                txtPrescriptionPath.Clear();
                RefreshCart();
                SmartMedMessageBox.Show($"Order placed successfully. Reference #SM-{orderId:D4}", "Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
