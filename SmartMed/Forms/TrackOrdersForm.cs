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
                Total = $"LKR {o.TotalAmount:N2}",
                Prescription = OrderService.GetPrescriptionDisplay(o)
            }).ToList();
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
            gridItems.DataSource = null;
            _selectedOrderId = null;
        }

        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                Width = GetScrollContentWidth()
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            var lblOrders = UiTheme.CreateSectionHeading("Your Orders");
            root.Controls.Add(lblOrders, 0, 0);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            gridOrders = new DataGridView
            {
                Dock = DockStyle.Fill,
                Height = 220,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, UiTheme.CustomerControlGap, 0, UiTheme.CustomerSectionGap)
            };
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;
            gridOrders.CellDoubleClick += GridOrders_CellDoubleClick;
            root.Controls.Add(gridOrders, 0, 1);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));

            var lblItems = UiTheme.CreateSectionHeading("Order Items");
            lblItems.Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, UiTheme.CustomerControlGap);
            root.Controls.Add(lblItems, 0, 2);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            gridItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                Height = 180,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, UiTheme.CustomerControlGap, 0, 0)
            };
            root.Controls.Add(gridItems, 0, 3);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 180f));

            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0)
            };
            var btnCancel = new Button { Text = "Cancel Pending Order", Width = 160, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnCancel.Click += BtnCancel_Click;
            var btnExport = new Button { Text = "Export CSV", Width = 100, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnExport.Click += BtnExport_Click;
            actions.Controls.Add(btnCancel);
            actions.Controls.Add(btnExport);
            root.Controls.Add(actions, 0, 4);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            WireScrollRoot(root, minHeight: 500);
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

        private void GridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsDesignHost() || Orders == null || e.RowIndex < 0) return;
            if (!gridOrders.Columns.Contains("Prescription")) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Prescription") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            var order = Orders.GetById(orderId);
            if (order == null || string.IsNullOrWhiteSpace(order.PrescriptionFile))
                return;

            if (!System.IO.File.Exists(order.PrescriptionFile))
            {
                MessageBox.Show("Prescription file is no longer available on this device.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(order.PrescriptionFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open prescription file.\n{ex.Message}", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                new { OrderID = 2, OrderRef = "#SM-0002", OrderDate = "Jun 22, 2026 10:00 AM", Status = "Pending", Total = "LKR 15.00", Prescription = "2_20260622100000_rx.pdf" }
            };
            gridItems.DataSource = new[]
            {
                new { MedicineName = "Vitamin C", Quantity = 1, UnitPrice = "LKR 15.00", Subtotal = "LKR 15.00" }
            };
        }
    }
}
