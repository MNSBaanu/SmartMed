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
        private DataGridView gridItems;
        private ComboBox cmbStatus;
        private Label lblStatusCaption;
        private Label lblOrderStatus;
        private Label lblOrderDetails;
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

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 6,
                MinimumSize = new Size(0, 1000),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 200f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateOrdersGridPanel(), 0, 2);
            _scrollRoot.Controls.Add(CreateItemsGridPanel(), 0, 3);
            _scrollRoot.Controls.Add(CreatePrescriptionPanel(), 0, 4);
            _scrollRoot.Controls.Add(CreateStatusPanel(), 0, 5);
            WireScrollRoot(_scrollRoot);
        }

        private Panel CreatePageHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0, 0, 0, 16)
            };
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            var titleBlock = new Panel { Dock = DockStyle.Left, Width = 520 };
            titleBlock.Controls.Add(new Label
            {
                Text = "Review fulfillment queue, verify prescriptions, and update order status.",
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                Dock = DockStyle.Fill
            });

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };
            actions.Controls.Add(new Label { Text = "Status:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            cmbStatusFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 150
            };
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

            header.Controls.Add(actions);
            header.Controls.Add(titleBlock);
            return header;
        }

        private Panel CreateSearchPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(8, 8, 8, 4),
                Margin = new Padding(0),
                BackColor = Color.FromArgb(248, 248, 248)
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

        private static Button CreateToolbarButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Height = 32,
                Width = 110,
                Margin = new Padding(4, 0, 0, 0)
            };
            return btn;
        }

        private Panel CreateStatsRow()
        {
            var wrap = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 88,
                Margin = new Padding(0, 0, 0, 16)
            };

            var row = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 4, RowCount = 1 };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            lblTotalOrders = new Label();
            lblPendingOrders = new Label();
            lblDeliveredOrders = new Label();
            lblRegisteredCustomers = new Label();

            row.Controls.Add(CreateStatTile("Total Orders", lblTotalOrders, UiTheme.GridHeaderText), 0, 0);
            row.Controls.Add(CreateStatTile("Pending", lblPendingOrders, SystemColors.ControlText), 1, 0);
            row.Controls.Add(CreateStatTile("Delivered", lblDeliveredOrders, Color.Green), 2, 0);
            row.Controls.Add(CreateStatTile("Registered Customers", lblRegisteredCustomers, UiTheme.GridHeaderText), 3, 0);

            wrap.Controls.Add(row);
            return wrap;
        }

        private Panel CreateStatTile(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 8, 0)
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            valueLabel.Text = "0";
            valueLabel.Font = UiTheme.UiFont;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(16, 36);
            valueLabel.AutoSize = true;

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                Location = new Point(16, 16),
                AutoSize = true
            });
            card.Controls.Add(valueLabel);
            return card;
        }

        private Panel CreateOrdersGridPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = SystemColors.Control,
                Padding = new Padding(12, 10, 12, 8)
            };
            header.Controls.Add(new Label
            {
                Text = "Recent Orders",
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.GridHeaderText,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            gridOrders = CreateGrid();
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;
            gridOrders.CellDoubleClick += GridOrders_CellDoubleClick;

            outer.Controls.Add(gridOrders);
            outer.Controls.Add(CreateSearchPanel());
            outer.Controls.Add(header);
            return outer;
        }

        private Panel CreateItemsGridPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            lblOrderDetails = new Label
            {
                Text = "Order Details",
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(12, 8, 0, 0),
                BackColor = SystemColors.Control
            };

            gridItems = CreateGrid();
            outer.Controls.Add(gridItems);
            outer.Controls.Add(lblOrderDetails);
            return outer;
        }

        private Panel CreatePrescriptionPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                BackColor = SystemColors.Window,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 0, 0, 16)
            };
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            lblRxStatus = new Label
            {
                Text = "Prescription verification: select an order.",
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.GridHeaderText,
                AutoSize = true,
                Location = new Point(16, 20)
            };

            btnViewPrescription = CreateToolbarButton("View Prescription");
            btnViewPrescription.Width = 140;
            btnViewPrescription.Enabled = false;
            btnViewPrescription.Click += BtnViewPrescription_Click;

            btnVerifyPrescription = CreateToolbarButton("Verify");
            btnVerifyPrescription.Enabled = false;
            btnVerifyPrescription.Click += BtnVerifyPrescription_Click;

            btnRejectPrescription = CreateToolbarButton("Reject");
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
                BackColor = UiTheme.Primary,
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

        private static DataGridView CreateGrid()
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                MultiSelect = false,
                ScrollBars = ScrollBars.Vertical
            };
            UiTheme.ApplyGrid(grid);
            return grid;
        }

        private void LoadDesignTimePreview()
        {
            gridOrders.SelectionChanged -= GridOrders_SelectionChanged;
            gridOrders.DataSource = new[]
            {
                new { OrderID = 9421, OrderRef = "#ORD-9421", CustomerName = "Margaret Sullivan", OrderDate = "Oct 24, 2023", Status = OrderService.StatusPending, Total = "LKR 124.50", Prescription = "9421_20231024120000_rx.pdf", RxStatus = PrescriptionService.StatusPending },
                new { OrderID = 9420, OrderRef = "#ORD-9420", CustomerName = "Jonathan Wick", OrderDate = "Oct 24, 2023", Status = OrderService.StatusReadyForPickup, Total = "LKR 45.00", Prescription = "—", RxStatus = "—" },
                new { OrderID = 9419, OrderRef = "#ORD-9419", CustomerName = "Sarah Connor", OrderDate = "Oct 23, 2023", Status = OrderService.StatusDelivered, Total = "LKR 312.20", Prescription = "—", RxStatus = "—" }
            };
            HideOrderIdColumn();
            gridOrders.ClearSelection();
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            gridItems.DataSource = new[]
            {
                new { MedicineName = "Amoxicillin 500mg (30 Caps)", Rx = "Yes", Quantity = 1, UnitPrice = "LKR 15.00", Subtotal = "LKR 15.00" },
                new { MedicineName = "Lisinopril 10mg (90 Tabs)", Rx = "No", Quantity = 1, UnitPrice = "LKR 30.00", Subtotal = "LKR 30.00" }
            };

            lblOrderDetails.Text = "Order Details: #ORD-9421";
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

            UiTheme.SetGridDataSource(gridOrders, all.Select(o => new
            {
                o.OrderID,
                OrderRef = $"#SM-{o.OrderID:D4}",
                o.CustomerName,
                OrderDate = o.OrderDate.ToString("MMM dd, yyyy"),
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}",
                Prescription = Orders.GetPrescriptionDisplay(o.OrderID),
                RxStatus = Orders.GetPrescriptionStatusDisplay(o.OrderID)
            }).ToList());

            HideOrderIdColumn();
            UpdateStats(all);
            ClearSelection();
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
            var orderRef = gridOrders.CurrentRow.Cells["OrderRef"].Value?.ToString() ?? string.Empty;

            lblOrderDetails.Text = $"Order Details: {orderRef}";
            PopulateStatusOptions(status);
            UpdateStatusControls(status);

            lblLastUpdated.Text = $"Last Updated: {DateTime.Now:MMM dd, yyyy hh:mm tt}";

            if (Orders == null)
                return;

            var items = Orders.GetItems(_selectedOrderId.Value);
            gridItems.DataSource = items.Select(i => new
            {
                i.MedicineName,
                Rx = i.RequiresPrescription ? "Yes" : "No",
                i.Quantity,
                UnitPrice = $"LKR {i.UnitPrice:N2}",
                Subtotal = $"LKR {i.Subtotal:N2}"
            }).ToList();

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
            gridItems.DataSource = null;
            lblOrderDetails.Text = "Order Details";
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
