using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class ManageOrdersForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ManageOrdersForm()
            : base(AdminNavItem.Orders, "Manage Orders")
        {
            InitializeComponent();
            CompleteDesignInitialization();
        }

        internal ManageOrdersForm(bool embedded)
            : base(AdminNavItem.Orders, "Manage Orders", embedded)
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                LoadOrders();
        }

        private OrderService _orders;
        private CustomerService _customers;
        private int? _selectedOrderId;
        private string _statusFilter = "All";
        private ComboBox cmbStatusFilter;
        private TextBox txtSearch;

        private DataGridView gridOrders;
        private ComboBox cmbStatus;
        private Label lblStatusCaption;
        private Label lblOrderStatus;
        private Label lblLastUpdated;
        private Label lblTotalOrders;
        private Label lblPendingOrders;
        private Label lblDeliveredOrders;
        private Label lblRegisteredCustomers;
        private Button btnUpdateStatus;
        private Label lblRxStatus;
        private Button btnViewPrescription;
        private Button btnVerifyPrescription;
        private Button btnRejectPrescription;
        private TableLayoutPanel _scrollRoot;

        private OrderService Orders => GetRuntimeService(ref _orders);
        private CustomerService Customers => GetRuntimeService(ref _customers);

        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            ClinicalUi.StylePagePanel(PagePanel);

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 5,
                MinimumSize = new Size(0, 820),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 380f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateOrdersGridPanel(), 0, 2);
            _scrollRoot.Controls.Add(CreatePrescriptionPanel(), 0, 3);
            _scrollRoot.Controls.Add(CreateStatusPanel(), 0, 4);
            WireScrollRoot(_scrollRoot);
        }

        private Panel CreatePageHeader()
        {
            return ClinicalUi.CreatePageHeader(
                "Manage Orders",
                "Review fulfillment queue, verify prescriptions, and update order status.",
                actions =>
                {
                    actions.Controls.Add(new Label
                    {
                        Text = "Status:",
                        AutoSize = true,
                        ForeColor = UiTheme.AdminMuted,
                        Font = UiTheme.UiFont,
                        Margin = new Padding(0, 8, 8, 0)
                    });
                    cmbStatusFilter = new ComboBox
                    {
                        DropDownStyle = ComboBoxStyle.DropDownList,
                        Width = 150,
                        Margin = new Padding(0, 0, 10, 0)
                    };
                    UiTheme.StyleComboBox(cmbStatusFilter);
                    cmbStatusFilter.Items.AddRange(new object[]
                    {
                        "All",
                        OrderService.StatusPending,
                        OrderService.StatusReadyForPickup,
                        OrderService.StatusDelivered
                    });
                    cmbStatusFilter.SelectedIndex = 0;
                    cmbStatusFilter.SelectedIndexChanged += (s, e) =>
                    {
                        _statusFilter = cmbStatusFilter.SelectedItem?.ToString() ?? "All";
                        if (!IsDesignHost()) ApplySearchFilter();
                    };
                    actions.Controls.Add(cmbStatusFilter);
                });
        }

        private Panel CreateSearchPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(8, 8, 8, 4),
                Margin = new Padding(0),
                BackColor = UiTheme.AdminSidebar
            };

            txtSearch = new TextBox { Width = 280 };
            var btnClearSearch = new Button { Text = "Clear", Width = 70, Height = 28, Margin = new Padding(8, 0, 0, 0) };
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            flow.Controls.Add(new Label
            {
                Text = "Search:",
                AutoSize = true,
                Padding = new Padding(0, 6, 4, 0)
            });
            flow.Controls.Add(txtSearch);
            flow.Controls.Add(btnClearSearch);
            txtSearch.TextChanged += (s, e) => ApplySearchFilter();
            btnClearSearch.Click += (s, e) =>
            {
                txtSearch.Clear();
                ApplySearchFilter();
            };
            panel.Controls.Add(flow);
            return panel;
        }

        private static DataGridView CreateGrid()
        {
            var grid = new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MultiSelect = false,
                ScrollBars = ScrollBars.Vertical
            };
            UiTheme.ApplyClinicalGrid(grid);
            return grid;
        }

        private Panel CreateStatsRow()
        {
            var statsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 108,
                ColumnCount = 4,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 24)
            };
            for (var i = 0; i < 4; i++)
                statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            lblTotalOrders = new Label();
            lblPendingOrders = new Label();
            lblDeliveredOrders = new Label();
            lblRegisteredCustomers = new Label();

            statsRow.Controls.Add(ClinicalUi.CreateStatCard("Total Orders", lblTotalOrders, UiTheme.AdminTeal), 0, 0);
            statsRow.Controls.Add(ClinicalUi.CreateStatCard("Pending", lblPendingOrders, Color.FromArgb(180, 83, 9)), 1, 0);
            statsRow.Controls.Add(ClinicalUi.CreateStatCard("Delivered", lblDeliveredOrders, Color.FromArgb(16, 185, 129)), 2, 0);
            statsRow.Controls.Add(ClinicalUi.CreateStatCard("Registered Customers", lblRegisteredCustomers, Color.FromArgb(59, 130, 246)), 3, 0);
            return statsRow;
        }

        private Panel CreateOrdersGridPanel()
        {
            gridOrders = CreateGrid();
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;
            gridOrders.CellDoubleClick += GridOrders_CellDoubleClick;

            var outer = ClinicalUi.CreateSectionPanel("Orders & Line Items", gridOrders);
            outer.Margin = new Padding(0, 0, 0, 16);
            outer.Controls.Add(CreateSearchPanel());
            return outer;
        }

        private Panel CreatePrescriptionPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = Color.White,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 0, 0, 16)
            };
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            lblRxStatus = new Label
            {
                Text = "Prescription verification: select an order.",
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminOnSurface,
                AutoSize = true,
                Location = new Point(16, 20)
            };

            btnViewPrescription = ClinicalUi.CreateWinButton("View Prescription", primary: false, width: 140);
            btnViewPrescription.Enabled = false;
            btnViewPrescription.Click += BtnViewPrescription_Click;

            btnVerifyPrescription = ClinicalUi.CreateWinButton("Verify", primary: true, width: 88);
            btnVerifyPrescription.Enabled = false;
            btnVerifyPrescription.Click += BtnVerifyPrescription_Click;

            btnRejectPrescription = ClinicalUi.CreateWinButton("Reject", primary: false, width: 88);
            btnRejectPrescription.Enabled = false;
            btnRejectPrescription.Click += BtnRejectPrescription_Click;

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false
            };
            actions.Controls.Add(btnViewPrescription);
            actions.Controls.Add(btnVerifyPrescription);
            actions.Controls.Add(btnRejectPrescription);

            panel.Controls.Add(actions);
            panel.Controls.Add(lblRxStatus);
            return panel;
        }

        private Panel CreateStatusPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = UiTheme.AdminTeal,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 0, 0, 16)
            };

            cmbStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 200,
                Location = new Point(88, 16),
                Enabled = false,
                Visible = false
            };

            lblLastUpdated = new Label
            {
                Text = "Last Updated: —",
                ForeColor = SystemColors.ControlLightLight,
                Font = UiTheme.UiFont,
                AutoSize = true,
                Location = new Point(320, 20)
            };

            btnUpdateStatus = new Button
            {
                Text = "Update Status",
                Width = 150,
                Height = 40,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Enabled = false,
                Visible = false
            };
            btnUpdateStatus.Click += BtnUpdateStatus_Click;

            lblStatusCaption = new Label
            {
                Text = "STATUS:",
                ForeColor = Color.White,
                Font = UiTheme.UiFont,
                AutoSize = true,
                Location = new Point(16, 20)
            };

            lblOrderStatus = new Label
            {
                Text = OrderService.StatusDelivered,
                ForeColor = Color.White,
                Font = UiTheme.UiFont,
                AutoSize = true,
                Location = new Point(88, 20),
                Visible = false
            };

            panel.Controls.Add(lblStatusCaption);
            panel.Controls.Add(lblOrderStatus);
            panel.Controls.Add(cmbStatus);
            panel.Controls.Add(lblLastUpdated);
            panel.Controls.Add(btnUpdateStatus);

            panel.Resize += (s, e) =>
            {
                btnUpdateStatus.Location = new Point(panel.Width - btnUpdateStatus.Width - 16, 14);
            };

            return panel;
        }

        private void LoadDesignTimePreview()
        {
            gridOrders.SelectionChanged -= GridOrders_SelectionChanged;
            gridOrders.DataSource = new[]
            {
                new { OrderID = 9421, OrderRef = "#ORD-9421", CustomerName = "Margaret Sullivan", OrderDate = "Oct 24, 2023", Status = OrderService.StatusPending, Total = "LKR 124.50", Prescription = "9421_20231024120000_rx.pdf", RxStatus = PrescriptionService.StatusPending, MedicineName = "Amoxicillin 500mg (30 Caps)", Rx = "Yes", Quantity = 1, UnitPrice = "LKR 15.00", Subtotal = "LKR 15.00" },
                new { OrderID = 9421, OrderRef = "#ORD-9421", CustomerName = "Margaret Sullivan", OrderDate = "Oct 24, 2023", Status = OrderService.StatusPending, Total = "LKR 124.50", Prescription = "9421_20231024120000_rx.pdf", RxStatus = PrescriptionService.StatusPending, MedicineName = "Lisinopril 10mg (90 Tabs)", Rx = "No", Quantity = 1, UnitPrice = "LKR 30.00", Subtotal = "LKR 30.00" },
                new { OrderID = 9420, OrderRef = "#ORD-9420", CustomerName = "Jonathan Wick", OrderDate = "Oct 24, 2023", Status = OrderService.StatusReadyForPickup, Total = "LKR 45.00", Prescription = "—", RxStatus = "—", MedicineName = "Paracetamol 500mg", Rx = "No", Quantity = 2, UnitPrice = "LKR 22.50", Subtotal = "LKR 45.00" },
                new { OrderID = 9419, OrderRef = "#ORD-9419", CustomerName = "Sarah Connor", OrderDate = "Oct 23, 2023", Status = OrderService.StatusDelivered, Total = "LKR 312.20", Prescription = "—", RxStatus = "—", MedicineName = "Atorvastatin 20mg", Rx = "No", Quantity = 1, UnitPrice = "LKR 312.20", Subtotal = "LKR 312.20" }
            };
            HideOrderIdColumn();
            UiTheme.BeautifyGridHeaders(gridOrders);
            gridOrders.ClearSelection();
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            UpdatePrescriptionControls(9421, PrescriptionService.StatusPending, "9421_20231024120000_rx.pdf");
            cmbStatus.SelectedItem = OrderService.StatusReadyForPickup;
            lblLastUpdated.Text = "Last Updated: Today, 10:42 AM";
            lblTotalOrders.Text = "4";
            lblPendingOrders.Text = "1";
            lblDeliveredOrders.Text = "2";
            lblRegisteredCustomers.Text = "248";
        }

        public void LoadOrders()
        {
            if (IsDesignHost() || Orders == null) return;
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            if (IsDesignHost() || Orders == null) return;

            var all = Orders.GetAll();
            if (_statusFilter != "All")
                all = all.Where(o => o.Status == _statusFilter).ToList();

            all = SearchService.SearchOrders(all, txtSearch?.Text);

            UiTheme.SetGridDataSource(gridOrders, BuildOrderGridRows(all));

            HideOrderIdColumn();
            UiTheme.BeautifyGridHeaders(gridOrders);
            UpdateStats(all);
            ClearSelection();
        }

        private List<object> BuildOrderGridRows(List<Order> orders)
        {
            var rows = new List<object>();
            foreach (var order in orders)
            {
                var items = Orders.GetItems(order.OrderID);
                var orderRef = $"#SM-{order.OrderID:D4}";
                var orderDate = order.OrderDate.ToString("MMM dd, yyyy");
                var total = $"LKR {order.TotalAmount:N2}";
                var prescription = Orders.GetPrescriptionDisplay(order.OrderID);
                var rxStatus = Orders.GetPrescriptionStatusDisplay(order.OrderID);

                if (items.Count == 0)
                {
                    rows.Add(new
                    {
                        order.OrderID,
                        OrderRef = orderRef,
                        order.CustomerName,
                        OrderDate = orderDate,
                        order.Status,
                        Total = total,
                        Prescription = prescription,
                        RxStatus = rxStatus,
                        MedicineName = "—",
                        Rx = "—",
                        Quantity = (int?)null,
                        UnitPrice = "—",
                        Subtotal = "—"
                    });
                    continue;
                }

                foreach (var item in items)
                {
                    rows.Add(new
                    {
                        order.OrderID,
                        OrderRef = orderRef,
                        order.CustomerName,
                        OrderDate = orderDate,
                        order.Status,
                        Total = total,
                        Prescription = prescription,
                        RxStatus = rxStatus,
                        item.MedicineName,
                        Rx = item.RequiresPrescription ? "Yes" : "No",
                        item.Quantity,
                        UnitPrice = $"LKR {item.UnitPrice:N2}",
                        Subtotal = $"LKR {item.Subtotal:N2}"
                    });
                }
            }

            return rows;
        }

        private void HideOrderIdColumn()
        {
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
        }

        private void UpdateStats(List<Order> all)
        {
            Orders.GetStatusStats(all, out var pending, out var delivered);
            lblTotalOrders.Text = all.Count.ToString("N0");
            lblPendingOrders.Text = pending.ToString("N0");
            lblDeliveredOrders.Text = delivered.ToString("N0");
            if (Customers != null)
                lblRegisteredCustomers.Text = Customers.GetRegisteredCount().ToString("N0");
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (IsDesignHost())
                return;

            if (gridOrders.CurrentRow == null) return;
            var idCell = gridOrders.CurrentRow.Cells["OrderID"];
            if (idCell?.Value == null) return;

            _selectedOrderId = Convert.ToInt32(idCell.Value);
            var status = gridOrders.CurrentRow.Cells["Status"].Value?.ToString() ?? OrderService.StatusPending;

            PopulateStatusOptions(status);
            UpdateStatusControls(status);

            lblLastUpdated.Text = $"Last Updated: {DateTime.Now:MMM dd, yyyy hh:mm tt}";

            var prescriptionName = gridOrders.CurrentRow.Cells["Prescription"].Value?.ToString() ?? "—";
            var rxStatus = gridOrders.CurrentRow.Cells["RxStatus"].Value?.ToString() ?? "—";
            UpdatePrescriptionControls(_selectedOrderId.Value, rxStatus, prescriptionName);
        }

        private void GridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsDesignHost() || Orders == null || e.RowIndex < 0) return;
            if (!gridOrders.Columns.Contains("Prescription")) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Prescription") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            OpenPrescriptionFile(orderId);
        }

        private void UpdatePrescriptionControls(int orderId, string rxStatus, string prescriptionName)
        {
            if (lblRxStatus == null) return;

            if (orderId <= 0)
            {
                lblRxStatus.Text = "Prescription verification: select an order.";
                lblRxStatus.ForeColor = UiTheme.GridHeaderText;
                btnViewPrescription.Enabled = false;
                btnVerifyPrescription.Enabled = false;
                btnRejectPrescription.Enabled = false;
                return;
            }

            var hasPrescription = !string.IsNullOrWhiteSpace(prescriptionName) && prescriptionName != "—";
            if (!hasPrescription)
            {
                lblRxStatus.Text = "Prescription verification: not required for this order.";
                lblRxStatus.ForeColor = SystemColors.GrayText;
                btnViewPrescription.Enabled = false;
                btnVerifyPrescription.Enabled = false;
                btnRejectPrescription.Enabled = false;
                return;
            }

            lblRxStatus.ForeColor = GetRxStatusColor(rxStatus);
            lblRxStatus.Text = $"Prescription: {prescriptionName}  |  Status: {rxStatus}";

            btnViewPrescription.Enabled = true;
            var pending = string.Equals(rxStatus, PrescriptionService.StatusPending, StringComparison.OrdinalIgnoreCase);
            btnVerifyPrescription.Enabled = pending;
            btnRejectPrescription.Enabled = pending;
        }

        private static Color GetRxStatusColor(string rxStatus)
        {
            if (string.Equals(rxStatus, PrescriptionService.StatusVerified, StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(0, 100, 0);
            if (string.Equals(rxStatus, PrescriptionService.StatusRejected, StringComparison.OrdinalIgnoreCase))
                return Color.DarkRed;
            if (string.Equals(rxStatus, PrescriptionService.StatusPending, StringComparison.OrdinalIgnoreCase))
                return Color.FromArgb(140, 70, 0);
            return UiTheme.GridHeaderText;
        }

        private void BtnViewPrescription_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue) return;
            OpenPrescriptionFile(_selectedOrderId.Value);
        }

        private void OpenPrescriptionFile(int orderId)
        {
            if (Orders == null) return;

            var filePath = Orders.GetPrescriptionFilePath(orderId);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("This order has no prescription file.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("Prescription file is no longer available on this device.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open prescription file.\n{ex.Message}", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnVerifyPrescription_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue || Orders == null) return;

            if (MessageBox.Show("Verify this prescription for the selected order?", "Verify Prescription",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                Orders.VerifyPrescription(_selectedOrderId.Value);
                LoadOrders();
                MessageBox.Show("Prescription verified.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Verify Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRejectPrescription_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue || Orders == null) return;

            if (MessageBox.Show("Reject this prescription? The order cannot move forward until a valid prescription is provided.",
                    "Reject Prescription", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                Orders.RejectPrescription(_selectedOrderId.Value);
                LoadOrders();
                MessageBox.Show("Prescription rejected.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Reject Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ClearSelection()
        {
            _selectedOrderId = null;
            gridOrders.ClearSelection();
            cmbStatus.Items.Clear();
            cmbStatus.SelectedIndex = -1;
            UpdateStatusControls(null);
            UpdatePrescriptionControls(0, "—", "—");
            lblLastUpdated.Text = "Last Updated: —";
        }

        private void UpdateStatusControls(string currentStatus)
        {
            var canUpdate = !string.IsNullOrWhiteSpace(currentStatus)
                && OrderService.GetAllowedNextStatuses(currentStatus).Count > 0;
            var isDelivered = string.Equals(currentStatus, OrderService.StatusDelivered, StringComparison.OrdinalIgnoreCase);

            cmbStatus.Visible = canUpdate;
            btnUpdateStatus.Visible = canUpdate;
            lblOrderStatus.Visible = isDelivered;
            if (isDelivered)
                lblOrderStatus.Text = OrderService.StatusDelivered;

            if (canUpdate)
            {
                cmbStatus.Enabled = true;
                btnUpdateStatus.Enabled = true;
            }
        }

        private void PopulateStatusOptions(string currentStatus)
        {
            cmbStatus.Items.Clear();
            foreach (var status in OrderService.GetAllowedNextStatuses(currentStatus))
                cmbStatus.Items.Add(status);

            if (cmbStatus.Items.Count > 0)
                cmbStatus.SelectedIndex = 0;
            else
                cmbStatus.SelectedIndex = -1;
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue)
            {
                MessageBox.Show("Select an order from the grid to update status.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Select a valid status.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var status = cmbStatus.SelectedItem.ToString();
                if (Orders == null) return;
                Orders.UpdateStatus(_selectedOrderId.Value, status);
                LoadOrders();
                MessageBox.Show("Order status updated successfully.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
