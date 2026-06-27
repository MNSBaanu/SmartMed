using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class PlaceOrderForm : CustomerShellForm
    {
        private bool _pageBuilt;
        private OrderService _orders;
        private DataGridView gridCart;
        private Label lblTotal;
        private Label lblRxNote;
        private TextBox txtPrescriptionPath;
        private string _prescriptionPath;

        public PlaceOrderForm()
            : base(CustomerNavItem.Cart, "My Cart & Checkout")
        {
            InitializeComponent();
            CompleteDesignInitialization();
        }

        internal PlaceOrderForm(bool embedded)
            : base(CustomerNavItem.Cart, "My Cart & Checkout", embedded)
        {
        }

        private OrderService Orders => GetRuntimeService(ref _orders);

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                RefreshCart();
        }

        private void BuildContent()
        {
            ClinicalUi.PreparePagePanel(PagePanel);
            PagePanel.Controls.Clear();

            var root = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Width = GetScrollContentWidth(),
                BackColor = UiTheme.AdminSurface
            };

            root.Controls.Add(ClinicalUi.CreatePageHeader("My Cart & Checkout",
                "Review items, upload prescriptions, and place your order."));

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0),
                BackColor = UiTheme.AdminSurface
            };
            var btnRemove = ClinicalUi.CreateButton("Remove Selected", width: 130, height: 32);
            btnRemove.Click += BtnRemove_Click;
            var btnClear = ClinicalUi.CreateButton("Clear Cart", width: 100, height: 32);
            btnClear.Click += (s, e) => { CartService.Clear(); RefreshCart(); };
            var btnPlace = ClinicalUi.CreateButton("Place Order", primary: true, width: 120, height: 32);
            btnPlace.Click += BtnPlace_Click;
            actions.Controls.Add(btnRemove);
            actions.Controls.Add(btnClear);
            actions.Controls.Add(btnPlace);

            var rxRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = UiTheme.CustomerSectionMargin,
                BackColor = UiTheme.AdminSurface
            };
            txtPrescriptionPath = new TextBox { Width = 360, ReadOnly = true, Margin = UiTheme.CustomerControlMargin };
            UiTheme.StyleTextBox(txtPrescriptionPath);
            var btnBrowse = ClinicalUi.CreateButton("Upload Prescription", width: 150, height: 32);
            btnBrowse.Click += BtnBrowse_Click;
            rxRow.Controls.Add(txtPrescriptionPath);
            rxRow.Controls.Add(btnBrowse);

            lblRxNote = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = UiTheme.Danger,
                BackColor = UiTheme.AdminSurface,
                Text = "Rx medicines require a prescription upload.",
                Margin = UiTheme.CustomerSectionMargin
            };

            lblTotal = new Label
            {
                Dock = DockStyle.Top,
                Height = 28,
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                BackColor = UiTheme.AdminSurface,
                Margin = UiTheme.CustomerSectionMargin
            };

            gridCart = ClinicalUi.CreateGrid();
            gridCart.Dock = DockStyle.Top;
            gridCart.Height = 260;
            gridCart.Margin = UiTheme.CustomerSectionMargin;

            root.Controls.Add(rxRow);
            root.Controls.Add(lblRxNote);
            root.Controls.Add(lblTotal);
            root.Controls.Add(actions);
            root.Controls.Add(gridCart);

            WireScrollRoot(root, minHeight: 400);
        }

        public void RefreshCart()
        {
            if (IsDesignHost() || gridCart == null) return;
            ClinicalUi.BindGrid(gridCart, CartService.Items.Select(l => new
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
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
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
            if (IsDesignHost() || Orders == null) return;
            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var orderId = Orders.PlaceOrder(customer.CustomerID, CartService.Items, _prescriptionPath);
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

        private void LoadDesignTimePreview()
        {
            ClinicalUi.BindGrid(gridCart, new[]
            {
                new
                {
                    MedicineID = 1,
                    MedicineName = "Paracetamol",
                    Quantity = 2,
                    ListPrice = "LKR 5.50",
                    UnitPrice = "LKR 5.50",
                    DiscountDisplay = "—",
                    PromoDisplay = "—",
                    Applied = "—",
                    Subtotal = "LKR 11.00",
                    Rx = "No"
                }
            });
            lblTotal.Text = "Total: LKR 11.00 (2 items)";
        }
    }
}
