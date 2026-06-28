using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMedNew.Services;
using SmartMedNew.UI;

namespace SmartMedNew.UI
{
    public sealed partial class AdminDashboardForm : UserControl
    {
        private readonly ReportService _reports = new ReportService();
        private readonly OrderService _orders = new OrderService();
        private readonly MedicineService _medicines = new MedicineService();

        private List<object> _recentRows = new List<object>();

        public AdminDashboardForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            BackColor = UiTheme.AdminSurface;
            BuildContent();
            UiTheme.EnableFontPropagation(this);
            LoadDashboardData();
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
            txtSearch = new TextBox();
            gridLowStock = CreateGrid();
            gridExpiry = CreateGrid();
            gridRecent = CreateGrid();
            gridRecent.CellFormatting += GridRecent_CellFormatting;
            gridRecent.CellContentClick += GridRecent_CellContentClick;

            Controls.Clear();
            BackColor = UiTheme.AdminSurface;

            _scrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiTheme.AdminSurface,
                Padding = new Padding(24, 24, 24, 24)
            };
            Controls.Add(_scrollHost);

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
        }

        private int GetScrollContentWidth(int fallback = 800)
        {
            var w = _scrollHost?.ClientSize.Width ?? fallback;
            return w < 200 ? fallback : w - 48;
        }

        private void SyncScrollRootWidth(int fallback = 800)
        {
            if (_scrollHost == null || _contentPanel == null || _scrollHost.IsDisposed)
                return;

            var width = Math.Max(600, GetScrollContentWidth(fallback));
            if (_contentPanel.Width != width)
                _contentPanel.Width = width;
            _contentPanel.PerformLayout();
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
                AutoSize = true,
                BackColor = UiTheme.AdminSurface
            });
            header.Controls.Add(new Label
            {
                Text = "Operational Dashboard",
                Font = UiTheme.FontAt(20f, bold: true),
                ForeColor = UiTheme.PrimaryDark,
                Location = new Point(0, 8),
                AutoSize = true,
                BackColor = UiTheme.AdminSurface
            });

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 16, 0, 0),
                BackColor = UiTheme.AdminSurface
            };

            txtSearch.Margin = new Padding(0);
            UiTheme.StyleTextBox(txtSearch);
            txtSearch.TextChanged += (s, e) => ApplyRecentSearch();

            var searchWrap = CreateSearchBox();
            var btnRefresh = CreateWinButton("Refresh", primary: false, width: 96);
            btnRefresh.Click += (s, e) => LoadDashboardData();
            var btnNewOrder = CreateWinButton("+ New Order", primary: true, width: 118);
            btnNewOrder.Click += (s, e) => ShowComingSoon();

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
                Width = 224,
                Height = 30,
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
            txtSearch.Margin = new Padding(28, 0, 8, 0);
            wrap.Controls.Add(txtSearch);

            var hint = new Label
            {
                Text = "Search ID...",
                ForeColor = UiTheme.PlaceholderText,
                BackColor = Color.White,
                Bounds = new Rectangle(10, 0, 170, 30),
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
                Margin = new Padding(0, 0, 0, 24),
                BackColor = UiTheme.AdminSurface
            };
            for (var i = 0; i < 4; i++)
                statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            statsRow.Controls.Add(CreateStatCard("Stock Items", lblStockValue, UiTheme.AdminTeal), 0, 0);
            statsRow.Controls.Add(CreateStatCard("Pending Orders", lblOrdersValue, Color.FromArgb(184, 237, 226)), 1, 0);
            statsRow.Controls.Add(CreateStatCard("Revenue (LKR)", lblSalesValue, Color.FromArgb(199, 234, 228)), 2, 0);
            statsRow.Controls.Add(CreateStatCard("Active Users", lblCustomersValue, Color.FromArgb(171, 205, 200)), 3, 0);
            return statsRow;
        }

        private static Panel CreateStatCard(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 14, 0),
                Padding = new Padding(16, 14, 14, 14),
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
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                Dock = DockStyle.Top,
                Height = 16,
                BackColor = Color.White
            });

            valueLabel.Text = "0";
            valueLabel.Font = UiTheme.FontAt(22f, bold: true);
            valueLabel.ForeColor = UiTheme.PrimaryDark;
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            valueLabel.BackColor = Color.White;
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
                Margin = new Padding(0, 0, 0, 24),
                BackColor = UiTheme.AdminSurface
            };
            alertsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            alertsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            alertsRow.Controls.Add(CreateAlertTablePanel(
                "Critical Alerts", Color.FromArgb(255, 245, 243), UiTheme.Danger, gridLowStock), 0, 0);

            var expiryPanel = CreateAlertTablePanel(
                "Expiry Warnings", Color.FromArgb(255, 243, 240), Color.FromArgb(104, 57, 61), gridExpiry);
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
                Height = 32,
                BackColor = headerBg,
                Padding = new Padding(12, 8, 12, 4)
            };
            header.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = headerText,
                Dock = DockStyle.Left,
                AutoSize = true,
                BackColor = headerBg
            });

            grid.Dock = DockStyle.Fill;
            grid.CellFormatting += GridAlertStatus_CellFormatting;

            var body = new Panel { Dock = DockStyle.Fill, BackColor = Color.White };
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
                Height = 36,
                BackColor = UiTheme.AdminSidebar,
                Padding = new Padding(12, 8, 10, 4)
            };
            header.Controls.Add(new Label
            {
                Text = "RECENT FULFILLMENT ACTIVITY",
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                Dock = DockStyle.Left,
                AutoSize = true,
                BackColor = UiTheme.AdminSidebar
            });

            var headerActions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = UiTheme.AdminSidebar
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
            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 4, 0, 0), BackColor = Color.White };
            body.Controls.Add(gridRecent);
            outer.Controls.Add(body);
            outer.Controls.Add(header);
            return outer;
        }

        private static Button CreateWinButton(string text, bool primary, int width)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = 30,
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
                btn.BackColor = Color.FromArgb(238, 245, 244);
                btn.ForeColor = UiTheme.AdminOnSurface;
                btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(227, 234, 233);
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
                    e.CellStyle.BackColor = Color.FromArgb(255, 218, 214);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(value, "LOW", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.ForeColor = Color.FromArgb(104, 57, 61);
                    e.CellStyle.BackColor = Color.FromArgb(255, 218, 219);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
            }
            else if (string.Equals(value, "EXPIRED", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.ForeColor = UiTheme.Danger;
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
        }

        private void GridRecent_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridRecent.Columns[e.ColumnIndex].Name != "FulfillmentStatus") return;

            var status = e.Value?.ToString() ?? string.Empty;
            if (string.Equals(status, OrderService.StatusDelivered, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(184, 237, 226);
                e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                e.CellStyle.Font = UiTheme.UiFontBold;
                e.Value = "FULFILLED";
            }
            else if (string.Equals(status, OrderService.StatusReadyForPickup, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(199, 234, 228);
                e.CellStyle.ForeColor = UiTheme.AdminTeal;
                e.CellStyle.Font = UiTheme.UiFontBold;
                e.Value = "READY FOR PICKUP";
            }
            else if (string.Equals(status, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 218, 219);
                e.CellStyle.ForeColor = Color.FromArgb(104, 57, 61);
                e.CellStyle.Font = UiTheme.UiFontBold;
                e.Value = "PENDING";
            }
        }

        private void GridRecent_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || gridRecent.Columns[e.ColumnIndex].Name != "Actions") return;
            ShowComingSoon();
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

        private static void ShowComingSoon()
        {
            MessageBox.Show(
                "This section is coming soon.",
                "SmartMed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void LoadDashboardData()
        {
            lblStockValue.Text = _medicines.GetAll().Count.ToString("N0");
            lblOrdersValue.Text = _orders.GetAll().Count(o => o.Status == OrderService.StatusPending).ToString("N0");
            lblSalesValue.Text = _reports.TotalSales.ToString("N0");
            lblCustomersValue.Text = _reports.RegisteredCustomers.ToString("N0");

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
