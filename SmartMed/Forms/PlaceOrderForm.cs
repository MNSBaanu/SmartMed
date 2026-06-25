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
            PagePanel.Controls.Clear();
            var root = new Panel { Dock = DockStyle.Top, AutoSize = true, Width = GetScrollContentWidth() };

            var actions = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 8, 0, 0) };
            var btnRemove = new Button { Text = "Remove Selected", Width = 130, Height = 32 };
            btnRemove.Click += BtnRemove_Click;
            var btnClear = new Button { Text = "Clear Cart", Width = 100, Height = 32 };
            btnClear.Click += (s, e) => { CartService.Clear(); RefreshCart(); };
            var btnPlace = new Button { Text = "Place Order", Width = 120, Height = 32 };
            btnPlace.Click += BtnPlace_Click;
            actions.Controls.Add(btnRemove);
            actions.Controls.Add(btnClear);
            actions.Controls.Add(btnPlace);

            var rxRow = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 8, 0, 8) };
            txtPrescriptionPath = new TextBox { Width = 360, ReadOnly = true };
            var btnBrowse = new Button { Text = "Upload Prescription", Width = 150, Height = 28 };
            btnBrowse.Click += BtnBrowse_Click;
            rxRow.Controls.Add(txtPrescriptionPath);
            rxRow.Controls.Add(btnBrowse);

            lblRxNote = new Label
            {
                Dock = DockStyle.Top,
                Height = 24,
                ForeColor = Color.DarkRed,
                Text = "Rx medicines require a prescription upload."
            };

            lblTotal = new Label { Dock = DockStyle.Top, Height = 28, Font = UiTheme.UiFontBold, Margin = new Padding(0, 8, 0, 0) };

            gridCart = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 260,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 0, 0, 8)
            };

            // Dock.Top: last added appears at the top — add bottom sections first.
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
            gridCart.DataSource = CartService.Items.Select(l => new
            {
                l.MedicineID,
                l.MedicineName,
                l.Quantity,
                UnitPrice = $"LKR {l.UnitPrice:N2}",
                Subtotal = $"LKR {l.Subtotal:N2}",
                Rx = l.RequiresPrescription ? "Yes" : "No"
            }).ToList();
            if (gridCart.Columns.Contains("MedicineID"))
                gridCart.Columns["MedicineID"].Visible = false;
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
            gridCart.DataSource = new[]
            {
                new { MedicineID = 1, MedicineName = "Paracetamol", Quantity = 2, UnitPrice = "LKR 5.50", Subtotal = "LKR 11.00", Rx = "No" }
            };
            lblTotal.Text = "Total: LKR 11.00 (2 items)";
        }
    }
}
