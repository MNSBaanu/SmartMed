using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class AdminDashboardForm : AdminShellForm
    {
        private bool _pageBuilt;

        public AdminDashboardForm()
            : base(AdminNavItem.Overview, "Operational Dashboard")
        {
            InitializeComponent();
            CompleteDesignInitialization();
        }

        internal AdminDashboardForm(bool embedded)
            : base(AdminNavItem.Overview, "Operational Dashboard", embedded)
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
                LoadDashboardData();
        }

        private readonly ReportService _reports = new ReportService();
        private readonly OrderService _orders = new OrderService();
        private readonly MedicineService _medicines = new MedicineService();

        private Label lblStockValue;
        private Label lblOrdersValue;
        private Label lblSalesValue;
        private Label lblCustomersValue;
        private Label lblStatusTime;
        private TextBox txtSearch;
        private DataGridView gridLowStock;
        private DataGridView gridExpiry;
        private DataGridView gridRecent;
        private Panel _pageRoot;
        private Panel _scrollHost;
        private TableLayoutPanel _contentPanel;
        private List<object> _recentRows = new List<object>();

        internal override void SyncScrollRootWidth(int fallback = 850)
        {
            base.SyncScrollRootWidth(fallback);
            if (_scrollHost == null || _contentPanel == null || _scrollHost.IsDisposed)
                return;

            var width = Math.Max(600, _scrollHost.ClientSize.Width - 4);
            if (_contentPanel.Width != width)
                _contentPanel.Width = width;
            _pageRoot?.PerformLayout();
        }

        public void RefreshData()
        {
            SyncScrollRootWidth();
            LoadDashboardData();
        }

        private void BuildContent()
        {
            lblStockValue = new Label();
            lblOrdersValue = new Label();
            lblSalesValue = new Label();
            lblCustomersValue = new Label();
            lblStatusTime = new Label();
            txtSearch = new TextBox();
            gridLowStock = CreateGrid();
            gridExpiry = CreateGrid();
            gridRecent = CreateGrid();
            gridRecent.CellFormatting += GridRecent_CellFormatting;
            gridRecent.CellContentClick += GridRecent_CellContentClick;

            PagePanel.Controls.Clear();
            PagePanel.BackColor = UiTheme.AdminSurface;

            _pageRoot = new Panel { Dock = DockStyle.Fill, BackColor = UiTheme.AdminSurface };

            var footer = CreateStatusBar();
            footer.Dock = DockStyle.Bottom;
            _pageRoot.Controls.Add(footer);

            _scrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiTheme.AdminSurface,
                Padding = new Padding(0, 0, 0, 8)
            };
            _pageRoot.Controls.Add(_scrollHost);

            _contentPanel = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 4,
                Width = Math.Max(600, GetScrollContentWidth()),
                BackColor = UiTheme.AdminSurface
            };
            _contentPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _contentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _contentPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _contentPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 210f));
            _contentPanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 320f));

            _contentPanel.Controls.Add(CreatePageHeader(), 0, 0);
            _contentPanel.Controls.Add(CreateStatsRow(), 0, 1);
            _contentPanel.Controls.Add(CreateAlertsRow(), 0, 2);
            _contentPanel.Controls.Add(CreateRecentActivityPanel(), 0, 3);

            _scrollHost.Controls.Add(_contentPanel);
            UiTheme.EnableDoubleBuffer(_scrollHost);
            _scrollHost.Resize += (s, e) => SyncScrollRootWidth();

            RegisterPageRoot(_pageRoot);
        }

        private Panel CreatePageHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 76,
                Margin = new Padding(0, 0, 0, 24),
                BackColor = UiTheme.AdminSurface
            };

            header.Controls.Add(new Label
            {
                Text = "Overview of pharmaceutical stock and fulfillment health.",
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Location = new Point(0, 44),
                AutoSize = true
            });
            header.Controls.Add(new Label
            {
                Text = "Operational Dashboard",
                Font = new Font(UiTheme.UiFont.FontFamily, 20f, FontStyle.Bold),
                ForeColor = UiTheme.AdminOnSurface,
                Location = new Point(0, 8),
                AutoSize = true
            });

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 16, 0, 0)
            };

            txtSearch.Margin = new Padding(0);
            UiTheme.StyleTextBox(txtSearch);
            txtSearch.TextChanged += (s, e) => ApplyRecentSearch();

            var searchWrap = CreateSearchBox();
            var btnRefresh = CreateWinButton("Refresh", primary: false, width: 96);
            btnRefresh.Click += (s, e) => LoadDashboardData();
            var btnNewOrder = CreateWinButton("+ New Order", primary: true, width: 118);
            btnNewOrder.Click += (s, e) => GoToAdminSection(AdminNavItem.Orders);

            actions.Controls.Add(searchWrap);
            actions.Controls.Add(btnRefresh);
            actions.Controls.Add(btnNewOrder);
            header.Controls.Add(actions);
            return header;
        }

        private Panel CreateSearchBox()
        {
            var wrap = new Panel
            {
                Width = 200,
                Height = 34,
                Margin = new Padding(0, 0, 10, 0),
                BackColor = Color.White
            };
            wrap.Paint += (s, e) =>
            {
                var rect = wrap.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            txtSearch.Dock = DockStyle.Fill;
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Margin = new Padding(8, 0, 8, 0);
            wrap.Controls.Add(txtSearch);

            var hint = new Label
            {
                Text = "Search ID...",
                ForeColor = UiTheme.AdminMuted,
                BackColor = Color.White,
                Bounds = new Rectangle(10, 0, 170, 34),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.IBeam
            };
            wrap.Controls.Add(hint);
            hint.BringToFront();
            txtSearch.GotFocus += (s, e) => hint.Visible = false;
            txtSearch.LostFocus += (s, e) => hint.Visible = string.IsNullOrEmpty(txtSearch.Text);
            wrap.Click += (s, e) => txtSearch.Focus();
            hint.Click += (s, e) => txtSearch.Focus();
            return wrap;
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

            statsRow.Controls.Add(CreateStatCard("Stock Items", lblStockValue, UiTheme.AdminTeal), 0, 0);
            statsRow.Controls.Add(CreateStatCard("Pending Orders", lblOrdersValue, Color.FromArgb(59, 130, 246)), 1, 0);
            statsRow.Controls.Add(CreateStatCard("Revenue (LKR)", lblSalesValue, Color.FromArgb(168, 85, 247)), 2, 0);
            statsRow.Controls.Add(CreateStatCard("Registered Users", lblCustomersValue, Color.FromArgb(16, 185, 129)), 3, 0);
            return statsRow;
        }

        private static Panel CreateStatCard(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 14, 0),
                Padding = new Padding(18, 14, 14, 14),
                BackColor = Color.White
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
                using (var brush = new SolidBrush(accent))
                    e.Graphics.FillRectangle(brush, 0, 0, 4, rect.Height);
            };

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = new Font(UiTheme.UiFont.FontFamily, 8.5f, FontStyle.Bold),
                ForeColor = UiTheme.AdminMuted,
                Dock = DockStyle.Top,
                Height = 16
            });

            valueLabel.Text = "0";
            valueLabel.Font = new Font(UiTheme.UiFont.FontFamily, 22f, FontStyle.Bold);
            valueLabel.ForeColor = UiTheme.AdminOnSurface;
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            card.Controls.Add(valueLabel);
            return card;
        }

        private Panel CreateAlertsRow()
        {
            var alertsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 24)
            };
            alertsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            alertsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            alertsRow.Controls.Add(CreateAlertTablePanel(
                "Critical Alerts", Color.FromArgb(255, 235, 235), UiTheme.Danger, gridLowStock), 0, 0);

            var expiryPanel = CreateAlertTablePanel(
                "Expiry Warnings", Color.FromArgb(255, 243, 224), Color.FromArgb(180, 83, 9), gridExpiry);
            expiryPanel.Margin = new Padding(14, 0, 0, 0);
            alertsRow.Controls.Add(expiryPanel, 1, 0);
            return alertsRow;
        }

        private static Panel CreateAlertTablePanel(string title, Color headerBg, Color headerText, DataGridView grid)
        {
            var outer = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(1) };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 36,
                BackColor = headerBg,
                Padding = new Padding(14, 10, 14, 6)
            };
            header.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = new Font(UiTheme.UiFont, FontStyle.Bold),
                ForeColor = headerText,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            grid.Dock = DockStyle.Fill;
            grid.CellFormatting += GridAlertStatus_CellFormatting;

            var body = new Panel { Dock = DockStyle.Fill };
            body.Controls.Add(grid);
            outer.Controls.Add(body);
            outer.Controls.Add(header);
            return outer;
        }

        private Panel CreateRecentActivityPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(1)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = UiTheme.AdminSidebar,
                Padding = new Padding(14, 10, 10, 6)
            };
            header.Controls.Add(new Label
            {
                Text = "Recent Fulfillment Activity",
                Font = new Font(UiTheme.UiFont, FontStyle.Bold),
                ForeColor = UiTheme.AdminOnSurface,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            var headerActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight
            };
            var btnFilter = CreateWinButton("Filter", false, 72);
            btnFilter.Height = 28;
            btnFilter.Click += (s, e) => txtSearch.Focus();
            var btnPrint = CreateWinButton("Print", false, 72);
            btnPrint.Height = 28;
            btnPrint.Click += BtnPrintRecent_Click;
            headerActions.Controls.Add(btnFilter);
            headerActions.Controls.Add(btnPrint);
            header.Controls.Add(headerActions);

            gridRecent.Dock = DockStyle.Fill;
            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 4, 0, 0) };
            body.Controls.Add(gridRecent);
            outer.Controls.Add(body);
            outer.Controls.Add(header);
            return outer;
        }

        private Panel CreateStatusBar()
        {
            var bar = new Panel
            {
                Height = 32,
                BackColor = UiTheme.AdminTeal,
                Padding = new Padding(16, 0, 16, 0)
            };

            bar.Controls.Add(new Label
            {
                Text = "●  System Status: Healthy     ☁  Cloud Sync Active",
                ForeColor = Color.White,
                Font = UiTheme.UiFont,
                Dock = DockStyle.Left,
                AutoSize = true,
                Padding = new Padding(0, 8, 0, 0)
            });

            lblStatusTime.ForeColor = Color.White;
            lblStatusTime.Font = UiTheme.UiFont;
            lblStatusTime.Dock = DockStyle.Right;
            lblStatusTime.AutoSize = true;
            lblStatusTime.Padding = new Padding(0, 8, 0, 0);
            bar.Controls.Add(lblStatusTime);
            return bar;
        }

        private static Button CreateWinButton(string text, bool primary, int width)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = 34,
                FlatStyle = FlatStyle.Flat,
                Font = UiTheme.UiFont,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 10, 0)
            };
            btn.FlatAppearance.BorderSize = 1;
            if (primary)
            {
                btn.BackColor = UiTheme.AdminTeal;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = UiTheme.AdminTealDark;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 109, 100);
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = UiTheme.AdminOnSurface;
                btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 250, 249);
            }
            btn.UseVisualStyleBackColor = false;
            return btn;
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
                ScrollBars = ScrollBars.Vertical
            };
            UiTheme.ApplyClinicalGrid(grid);
            return grid;
        }

        private static void GridAlertStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var grid = (DataGridView)sender;
            if (grid.Columns[e.ColumnIndex].Name != "Status" && grid.Columns[e.ColumnIndex].Name != "DueDate")
                return;

            var value = e.Value?.ToString() ?? string.Empty;
            if (grid.Columns[e.ColumnIndex].Name == "Status")
            {
                if (string.Equals(value, "CRITICAL", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = UiTheme.Danger;
                    e.CellStyle.BackColor = Color.FromArgb(255, 235, 235);
                    e.CellStyle.Font = new Font(UiTheme.UiFont, FontStyle.Bold);
                }
                else if (string.Equals(value, "LOW", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(180, 83, 9);
                    e.CellStyle.BackColor = Color.FromArgb(255, 248, 235);
                    e.CellStyle.Font = new Font(UiTheme.UiFont, FontStyle.Bold);
                }
            }
            else if (string.Equals(value, "EXPIRED", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.ForeColor = UiTheme.Danger;
                e.CellStyle.Font = new Font(UiTheme.UiFont, FontStyle.Bold);
            }
        }

        private void GridRecent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridRecent.Columns[e.ColumnIndex].Name != "FulfillmentStatus") return;

            var status = e.Value?.ToString() ?? string.Empty;
            if (string.Equals(status, OrderService.StatusDelivered, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(220, 252, 231);
                e.CellStyle.ForeColor = Color.FromArgb(22, 101, 52);
                e.CellStyle.Font = new Font(UiTheme.UiFont, FontStyle.Bold);
                e.Value = "FULFILLED";
            }
            else if (string.Equals(status, OrderService.StatusReadyForPickup, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(204, 251, 241);
                e.CellStyle.ForeColor = UiTheme.AdminTeal;
                e.CellStyle.Font = new Font(UiTheme.UiFont, FontStyle.Bold);
                e.Value = "READY FOR PICKUP";
            }
            else if (string.Equals(status, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                e.CellStyle.ForeColor = UiTheme.Danger;
                e.CellStyle.Font = new Font(UiTheme.UiFont, FontStyle.Bold);
                e.Value = "PENDING";
            }
        }

        private void GridRecent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridRecent.Columns[e.ColumnIndex].Name != "Actions") return;
            GoToAdminSection(AdminNavItem.Orders);
        }

        private void BtnPrintRecent_Click(object sender, EventArgs e)
        {
            if (gridRecent.Rows.Count == 0)
            {
                MessageBox.Show("No recent orders to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                ExportHelper.PrintGrid(gridRecent, "Recent Fulfillment Activity");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDesignTimePreview()
        {
            lblStockValue.Text = "12";
            lblOrdersValue.Text = "3";
            lblSalesValue.Text = "56,240";
            lblCustomersValue.Text = "4";
            lblStatusTime.Text = $"Local Time: {DateTime.Now:HH:mm}";

            gridLowStock.DataSource = new[]
            {
                new { MedicineName = "Insulin Glargine", Level = "2 units", Status = "CRITICAL" },
                new { MedicineName = "Metformin 850mg", Level = "4 units", Status = "LOW" }
            };
            UiTheme.BeautifyGridHeaders(gridLowStock);

            gridExpiry.DataSource = new[]
            {
                new { BatchId = "#M-003", Medicine = "Amoxicillin 500mg", DueDate = "EXPIRED" },
                new { BatchId = "#M-007", Medicine = "Lisinopril 10mg", DueDate = "12 Days" }
            };
            UiTheme.BeautifyGridHeaders(gridExpiry);

            _recentRows = new List<object>
            {
                new { OrderRef = "#SM-0003", CustomerName = "Jonathan Miller", FulfillmentStatus = OrderService.StatusReadyForPickup, Timestamp = "10 mins ago" },
                new { OrderRef = "#SM-0002", CustomerName = "Sarah Chen", FulfillmentStatus = OrderService.StatusDelivered, Timestamp = "1 hour ago" },
                new { OrderRef = "#SM-0001", CustomerName = "Robert Fox", FulfillmentStatus = OrderService.StatusPending, Timestamp = "2 hours ago" }
            };
            BindRecentGrid(_recentRows);
        }

        private void LoadDashboardData()
        {
            if (IsDesignHost()) return;

            lblStockValue.Text = _medicines.GetAll().Count.ToString("N0");
            lblOrdersValue.Text = _orders.GetAll().Count(o => o.Status == OrderService.StatusPending).ToString("N0");
            lblSalesValue.Text = _reports.TotalSales.ToString("N0");
            lblCustomersValue.Text = _reports.RegisteredCustomers.ToString("N0");
            lblStatusTime.Text = $"Local Time: {DateTime.Now:HH:mm}";

            gridLowStock.DataSource = _medicines.GetLowStock(maxCount: 6)
                .Select(m => new
                {
                    m.MedicineName,
                    Level = $"{m.StockQuantity} units",
                    Status = m.StockQuantity <= 5 ? "CRITICAL" : "LOW"
                }).ToList();
            UiTheme.BeautifyGridHeaders(gridLowStock);

            var expiryRows = new List<object>();
            foreach (var med in _medicines.GetExpiredMedicines().Take(4))
                expiryRows.Add(new { BatchId = $"#M-{med.MedicineID:D3}", Medicine = med.MedicineName, DueDate = "EXPIRED" });
            foreach (var med in _medicines.GetExpiringSoonMedicines().Take(4))
            {
                if (expiryRows.Count >= 6) break;
                var days = (med.ExpiryDate.Date - DateTime.Today).Days;
                expiryRows.Add(new { BatchId = $"#M-{med.MedicineID:D3}", Medicine = med.MedicineName, DueDate = days <= 0 ? "EXPIRED" : $"{days} Days" });
            }
            gridExpiry.DataSource = expiryRows;
            UiTheme.BeautifyGridHeaders(gridExpiry);

            _recentRows = _orders.GetAll().Take(8).Select(o => (object)new
            {
                OrderRef = $"#SM-{o.OrderID:D4}",
                CustomerName = o.CustomerName,
                FulfillmentStatus = o.Status,
                Timestamp = FormatRelativeTime(o.OrderDate)
            }).ToList();
            ApplyRecentSearch();
        }

        private void ApplyRecentSearch()
        {
            if (_recentRows == null || _recentRows.Count == 0)
            {
                BindRecentGrid(_recentRows ?? new List<object>());
                return;
            }

            var term = txtSearch?.Text?.Trim();
            BindRecentGrid(string.IsNullOrWhiteSpace(term)
                ? _recentRows
                : _recentRows.Where(row =>
                {
                    var value = row.GetType().GetProperty("OrderRef")?.GetValue(row)?.ToString() ?? "";
                    return value.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0;
                }).ToList());
        }

        private void BindRecentGrid(IEnumerable<object> rows)
        {
            gridRecent.DataSource = rows?.ToList() ?? new List<object>();
            UiTheme.BeautifyGridHeaders(gridRecent);
            EnsureRecentActionsColumn();
        }

        private void EnsureRecentActionsColumn()
        {
            if (gridRecent.Columns.Contains("Actions")) return;
            gridRecent.Columns.Add(new DataGridViewButtonColumn
            {
                Name = "Actions",
                HeaderText = "Actions",
                Text = "Open",
                UseColumnTextForButtonValue = true,
                Width = 72,
                FlatStyle = FlatStyle.Flat
            });
            gridRecent.Columns["Actions"].DisplayIndex = gridRecent.Columns.Count - 1;
        }

        private static string FormatRelativeTime(DateTime orderDate)
        {
            var span = DateTime.Now - orderDate;
            if (span.TotalMinutes < 1) return "Just now";
            if (span.TotalMinutes < 60) return $"{Math.Max(1, (int)span.TotalMinutes)} mins ago";
            if (span.TotalHours < 24) return $"{Math.Max(1, (int)span.TotalHours)} hour{(span.TotalHours >= 2 ? "s" : "")} ago";
            return orderDate.ToString("MMM dd, yyyy hh:mm tt");
        }
    }
}
