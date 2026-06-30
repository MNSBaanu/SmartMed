using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class PlaceOrderForm : CustomerPageControl
    {
        private readonly OrderService _orders = new OrderService();
        private string _prescriptionPath;

        public PlaceOrderForm()
        {
            InitializeComponent();
        }

        protected override void BuildPageLayout() => BuildContent();

        protected override void DoRefreshPage()
        {
            SyncScrollRootWidth();
            RefreshCart();
        }

        protected override void LoadDesignTimePreview()
        {
            UiTheme.SetGridDataSource(gridCart, DesignTimePreviewData.CartRows());
            if (gridCart.Columns.Contains("MedicineID"))
                gridCart.Columns["MedicineID"].Visible = false;
            if (gridCart.Columns.Contains("DiscountDisplay"))
                gridCart.Columns["DiscountDisplay"].HeaderText = "Discount";
            if (gridCart.Columns.Contains("PromoDisplay"))
                gridCart.Columns["PromoDisplay"].HeaderText = "Promo";
            lblTotal.Text = "Total: LKR 975.00 (2 items)";
            lblRxNote.Visible = true;
        }

        private void BuildContent()
        {
            txtPrescriptionPath = new TextBox { Width = 360, ReadOnly = true, Margin = new Padding(0, 0, 8, 0) };
            UiTheme.StyleTextBox(txtPrescriptionPath);

            lblRxNote = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = UiTheme.Danger,
                BackColor = UiTheme.AdminSurface,
                Text = "Rx medicines require a prescription upload.",
                Margin = new Padding(0, 0, 0, 12)
            };

            lblTotal = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                BackColor = UiTheme.AdminSurface,
                Margin = new Padding(0, 0, 0, 12)
            };

            gridCart = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 260,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 0, 0, 12)
            };
            UiTheme.ApplyClinicalGrid(gridCart);

            var root = new Panel
            {
                AutoSize = true,
                MinimumSize = new Size(0, 400),
                BackColor = UiTheme.AdminSurface
            };

            root.Controls.Add(CreateRxRow());
            root.Controls.Add(lblRxNote);
            root.Controls.Add(lblTotal);
            root.Controls.Add(CreateActionsPanel());
            root.Controls.Add(gridCart);
            root.Controls.Add(AdminUiHelpers.CreatePageHeader("My Cart & Checkout",
                "Review items, upload prescriptions, and place your order."));

            WireScrollRoot(root);
        }

        private Panel CreateActionsPanel()
        {
            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = UiTheme.AdminSurface
            };

            var btnRemove = AdminUiHelpers.CreateWinButton("Remove Selected", false, 130);
            btnRemove.Click += BtnRemove_Click;
            var btnClear = AdminUiHelpers.CreateWinButton("Clear Cart", false, 100);
            btnClear.Click += (s, e) => { CartService.Clear(); RefreshCart(); };
            var btnPlace = AdminUiHelpers.CreateWinButton("Place Order", true, 120);
            btnPlace.Click += BtnPlace_Click;

            actions.Controls.Add(btnRemove);
            actions.Controls.Add(btnClear);
            actions.Controls.Add(btnPlace);
            return actions;
        }

        private Panel CreateRxRow()
        {
            var rxRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 12, 0, 8),
                BackColor = UiTheme.AdminSurface
            };

            var btnBrowse = AdminUiHelpers.CreateWinButton("Upload Prescription", false, 150);
            btnBrowse.Click += BtnBrowse_Click;
            rxRow.Controls.Add(txtPrescriptionPath);
            rxRow.Controls.Add(btnBrowse);
            return rxRow;
        }

        private void RefreshCart()
        {
            if (gridCart == null) return;
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
            if (gridCart.Columns.Contains("MedicineID"))
                gridCart.Columns["MedicineID"].Visible = false;
            if (gridCart.Columns.Contains("DiscountDisplay"))
                gridCart.Columns["DiscountDisplay"].HeaderText = "Discount";
            if (gridCart.Columns.Contains("PromoDisplay"))
                gridCart.Columns["PromoDisplay"].HeaderText = "Promo";
            lblTotal.Text = $"Total: LKR {CartService.Total:N2} ({CartService.ItemCount} items)";
            lblRxNote.Visible = CartService.RequiresPrescription;
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
                MessageBox.Show($"Order placed successfully. Reference #SM-{orderId:D4}", "Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
