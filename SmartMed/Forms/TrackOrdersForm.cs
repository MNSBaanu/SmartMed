using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class TrackOrdersForm : CustomerShellForm
    {
        private bool _pageBuilt;
        private OrderService _orders;
        private DataGridView gridOrders;
        private DataGridView gridItems;
        private int? _selectedOrderId;

        public TrackOrdersForm()
            : base(CustomerNavItem.Orders, "My Orders")
        {
            InitializeComponent();
        }

        internal TrackOrdersForm(bool embedded)
            : base(CustomerNavItem.Orders, "My Orders", embedded)
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
                RefreshOrders();
        }

        public void RefreshOrders()
        {
            if (IsDesignHost() || Orders == null || gridOrders == null) return;
            var customerId = Session.CurrentCustomer?.CustomerID ?? 0;
            var orders = Orders.GetByCustomer(customerId);
            gridOrders.DataSource = orders.Select(o => new
            {
                o.OrderID,
                OrderRef = $"#SM-{o.OrderID:D4}",
                OrderDate = o.OrderDate.ToString("MMM dd, yyyy hh:mm tt"),
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}"
            }).ToList();
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
            gridItems.DataSource = null;
            _selectedOrderId = null;
        }

        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            var root = new Panel { Dock = DockStyle.Top, AutoSize = true, Width = GetScrollContentWidth() };

            var header = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = UiTheme.CustomerSectionMargin };
            var btnCancel = new Button { Text = "Cancel Pending Order", Width = 160, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnCancel.Click += BtnCancel_Click;
            var btnExport = new Button { Text = "Export CSV", Width = 100, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnExport.Click += BtnExport_Click;
            header.Controls.Add(btnCancel);
            header.Controls.Add(btnExport);

            var lblItems = UiTheme.CreateSectionHeading("Order Items");
            gridItems = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 180,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };

            var lblOrders = UiTheme.CreateSectionHeading("Your Orders");
            gridOrders = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 220,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = UiTheme.CustomerSectionMargin
            };
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            // Dock.Top: last added appears at the top — add bottom sections first.
            root.Controls.Add(gridItems);
            root.Controls.Add(lblItems);
            root.Controls.Add(gridOrders);
            root.Controls.Add(lblOrders);
            root.Controls.Add(header);

            WireScrollRoot(root, minHeight: 460);
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (IsDesignHost() || Orders == null || gridOrders.CurrentRow == null) return;
            _selectedOrderId = Convert.ToInt32(gridOrders.CurrentRow.Cells["OrderID"].Value);
            var items = Orders.GetItems(_selectedOrderId.Value);
            gridItems.DataSource = items.Select(i => new
            {
                i.MedicineName,
                i.Quantity,
                UnitPrice = $"LKR {i.UnitPrice:N2}",
                Subtotal = $"LKR {i.Subtotal:N2}"
            }).ToList();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue)
            {
                MessageBox.Show("Select a pending order to cancel.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Cancel this order and restore stock?", "Confirm Cancel",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                Orders.CancelOrder(_selectedOrderId.Value, Session.CurrentCustomer.CustomerID);
                RefreshOrders();
                MessageBox.Show("Order cancelled.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Orders == null) return;
            using (var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "my_orders.csv"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                var orders = Orders.GetByCustomer(Session.CurrentCustomer.CustomerID);
                Orders.ExportOrdersToCsv(orders, dialog.FileName);
                MessageBox.Show("Order history exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LoadDesignTimePreview()
        {
            gridOrders.DataSource = new[]
            {
                new { OrderID = 2, OrderRef = "#SM-0002", OrderDate = "Jun 22, 2026 10:00 AM", Status = "Pending", Total = "LKR 15.00" }
            };
            gridItems.DataSource = new[]
            {
                new { MedicineName = "Vitamin C", Quantity = 1, UnitPrice = "LKR 15.00", Subtotal = "LKR 15.00" }
            };
        }
    }
}
