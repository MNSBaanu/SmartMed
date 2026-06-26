using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            : base(AdminNavItem.Overview, "Admin Dashboard")
        {
            InitializeComponent();
        }

        internal AdminDashboardForm(bool embedded)
            : base(AdminNavItem.Overview, "Admin Dashboard", embedded)
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

        public void RefreshData() => LoadDashboardData();

        private readonly ReportService _reports = new ReportService();
        private readonly OrderService _orders = new OrderService();
        private readonly MedicineService _medicines = new MedicineService();

        private Label lblWelcome;
        private Label lblStatus;
        private Label lblStockValue;
        private Label lblOrdersValue;
        private Label lblSalesValue;
        private Label lblCustomersValue;
        private DataGridView gridRecent;
        private FlowLayoutPanel panelLowStockAlerts;
        private FlowLayoutPanel panelExpiryAlerts;
        private TableLayoutPanel _scrollRoot;

        private void BuildContent()
        {
            lblWelcome = new Label();
            lblStatus = new Label();
            lblStockValue = new Label();
            lblOrdersValue = new Label();
            lblSalesValue = new Label();
            lblCustomersValue = new Label();
            gridRecent = CreateGrid();
            panelLowStockAlerts = CreateAlertListPanel();
            panelExpiryAlerts = CreateAlertListPanel();

            PagePanel.Controls.Clear();

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 5,
                MinimumSize = new Size(0, 980),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 180f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 280f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateAlertsRow(), 0, 2);
            _scrollRoot.Controls.Add(CreateMiddleRow(), 0, 3);
            _scrollRoot.Controls.Add(CreateTrendsSection(), 0, 4);
            WireScrollRoot(_scrollRoot);
        }

        private Panel CreatePageHeader()
        {
            lblWelcome.Text = $"Welcome, {Session.CurrentAdmin?.Username ?? "admin"}";
            lblStatus.Text = "System Status: Operational  •  Last sync: 2 minutes ago";

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0, 0, 0, 16)
            };
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Height = 28;
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Height = 22;

            header.Controls.Add(lblStatus);
            header.Controls.Add(lblWelcome);

            var lnkPassword = new LinkLabel
            {
                Text = "Change Password",
                AutoSize = true,
                Dock = DockStyle.Right,
                LinkColor = UiTheme.GridHeaderText,
                Padding = new Padding(0, 8, 0, 0)
            };
            lnkPassword.Click += (s, e) =>
            {
                using (var dlg = new ChangePasswordForm(isAdmin: true))
                    dlg.ShowDialog(this);
            };
            header.Controls.Add(lnkPassword);

            return header;
        }

        private Panel CreateStatsRow()
        {
            var wrap = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 130,
                Margin = new Padding(0, 0, 0, 16)
            };

            var statsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 4,
                RowCount = 1
            };
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25f));

            statsRow.Controls.Add(CreateStatCard("Medicines in Stock", lblStockValue,
                "SKUs tracked across inventory", "+12%"), 0, 0);
            statsRow.Controls.Add(CreateStatCard("Active Orders", lblOrdersValue,
                "Orders requiring verification", "Priority"), 1, 0);
            statsRow.Controls.Add(CreateStatCard("Total Sales", lblSalesValue,
                "Lifetime pharmacy revenue (LKR)", "Daily"), 2, 0);
            statsRow.Controls.Add(CreateStatCard("Registered Customers", lblCustomersValue,
                "Customers signed up in the system", "Users"), 3, 0);

            wrap.Controls.Add(statsRow);
            return wrap;
        }

        private static FlowLayoutPanel CreateAlertListPanel() =>
            new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

        private Panel CreateAlertsRow()
        {
            var alertsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16)
            };
            alertsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            alertsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            var (lowStockOuter, lowStockBody) = CreateSectionPanel(
                "Low Stock Alert",
                showViewAll: true,
                onViewAll: () => GoToAdminSection(AdminNavItem.Medicines),
                viewAllText: "Manage Medicines");
            panelLowStockAlerts.Dock = DockStyle.Fill;
            lowStockBody.Controls.Add(panelLowStockAlerts);
            alertsRow.Controls.Add(lowStockOuter, 0, 0);

            var (expiryOuter, expiryBody) = CreateSectionPanel(
                "Medicine Expiry Warning",
                showViewAll: true,
                onViewAll: () => GoToAdminSection(AdminNavItem.Medicines),
                viewAllText: "Manage Medicines");
            panelExpiryAlerts.Dock = DockStyle.Fill;
            expiryBody.Controls.Add(panelExpiryAlerts);
            expiryOuter.Margin = new Padding(8, 0, 0, 0);
            alertsRow.Controls.Add(expiryOuter, 1, 0);

            var wrap = new Panel { Dock = DockStyle.Fill, Height = 180 };
            wrap.Controls.Add(alertsRow);
            return wrap;
        }

        private Panel CreateMiddleRow()
        {
            var (gridOuter, gridBody) = CreateSectionPanel("Recent Fulfillment Activity", showViewAll: true);
            gridRecent.Dock = DockStyle.Fill;
            gridBody.Controls.Add(gridRecent);

            var wrap = new Panel { Dock = DockStyle.Fill, Height = 280 };
            gridOuter.Dock = DockStyle.Fill;
            wrap.Controls.Add(gridOuter);
            return wrap;
        }

        private Panel CreateTrendsSection()
        {
            var (trendsOuter, trendsBody) = CreateSectionPanel("Sales & Demand Trends", showPeriodTabs: true);
            trendsOuter.Dock = DockStyle.Top;
            trendsOuter.MinimumSize = new Size(0, 220);

            var chartArea = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Control,
                Padding = new Padding(16)
            };
            chartArea.Paint += (s, e) =>
            {
                var rect = chartArea.ClientRectangle;
                rect.Inflate(-16, -16);
                using (var pen = new Pen(Color.FromArgb(128, SystemColors.ControlDark)))
                {
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    e.Graphics.DrawRectangle(pen, rect);
                }

                var barHeights = new[] { 0.30f, 0.45f, 0.60f, 0.55f, 0.80f, 0.95f, 0.40f };
                var barWidth = Math.Max(20, (rect.Width - 80) / barHeights.Length);
                var x = rect.Left + 24;
                for (var i = 0; i < barHeights.Length; i++)
                {
                    var h = (int)((rect.Height - 20) * barHeights[i]);
                    var alpha = (int)(50 + barHeights[i] * 180);
                    using (var brush = new SolidBrush(Color.FromArgb(alpha, UiTheme.Primary)))
                    {
                        e.Graphics.FillRectangle(brush, x, rect.Bottom - h, barWidth, h);
                    }
                    x += barWidth + 8;
                }
            };
            chartArea.Controls.Add(new Label
            {
                Text = "Trend visualization data loaded",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = SystemColors.GrayText,
                Font = UiTheme.UiFont
            });

            trendsBody.Controls.Add(chartArea);
            return trendsOuter;
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
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.Vertical
            };
            UiTheme.ApplyGrid(grid);
            return grid;
        }

        private Panel CreateStatCard(string title, Label valueLabel, string subtitle, string badge)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(16),
                BackColor = SystemColors.Window
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var badgeLabel = new Label
            {
                Text = badge,
                Font = UiTheme.UiFont,
                ForeColor = badge == "Priority" ? Color.Red : SystemColors.ControlText,
                BackColor = badge == "Priority" ? Color.FromArgb(255, 218, 214) : SystemColors.ControlLight,
                AutoSize = true,
                Location = new Point(card.Width - 72, 12),
                Padding = new Padding(4, 2, 4, 2)
            };
            card.Controls.Add(badgeLabel);
            card.Controls.Add(new Label
            {
                Text = title,
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                AutoSize = true,
                Location = new Point(16, 12)
            });

            valueLabel.Text = "0";
            valueLabel.Font = UiTheme.UiFont;
            valueLabel.ForeColor = UiTheme.GridHeaderText;
            valueLabel.AutoSize = true;
            valueLabel.Location = new Point(16, 32);
            card.Controls.Add(valueLabel);

            card.Controls.Add(new Label
            {
                Text = subtitle,
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                AutoSize = true,
                Location = new Point(16, 64)
            });

            return card;
        }

        private (Panel outer, Panel body) CreateSectionPanel(
            string title,
            bool showViewAll = false,
            bool showPeriodTabs = false,
            Action onViewAll = null,
            string viewAllText = "View All")
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(1)
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
                Height = 44,
                BackColor = SystemColors.Control,
                Padding = new Padding(16, 12, 16, 8)
            };
            header.Controls.Add(new Label
            {
                Text = title,
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.GridHeaderText,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            if (showViewAll)
            {
                var btnViewAll = new LinkLabel
                {
                    Text = viewAllText,
                    Dock = DockStyle.Right,
                    AutoSize = true,
                    LinkColor = UiTheme.GridHeaderText
                };
                btnViewAll.Click += (s, e) =>
                {
                    if (onViewAll != null)
                        onViewAll();
                    else
                        GoToAdminSection(AdminNavItem.Orders);
                };
                header.Controls.Add(btnViewAll);
            }

            if (showPeriodTabs)
            {
                var tabs = new FlowLayoutPanel
                {
                    Dock = DockStyle.Right,
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false
                };
                tabs.Controls.Add(CreatePeriodTab("Week", active: false));
                tabs.Controls.Add(CreatePeriodTab("Month", active: true));
                tabs.Controls.Add(CreatePeriodTab("Year", active: false));
                header.Controls.Add(tabs);
            }

            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            outer.Controls.Add(body);
            outer.Controls.Add(header);
            return (outer, body);
        }

        private static Button CreatePeriodTab(string text, bool active)
        {
            var btn = new Button
            {
                Text = text,
                Height = 28,
                Width = 56,
                Margin = new Padding(2, 0, 0, 0),
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            if (active)
            {
                btn.BackColor = UiTheme.Primary;
                btn.ForeColor = Color.White;
            }
            else
            {
                btn.BackColor = SystemColors.Control;
                btn.ForeColor = SystemColors.ControlText;
            }
            btn.Font = UiTheme.UiFont;
            return btn;
        }

        private void LoadDesignTimePreview()
        {
            lblWelcome.Text = "Welcome, admin";
            lblStockValue.Text = "4,281";
            lblOrdersValue.Text = "127";
            lblSalesValue.Text = "LKR 12,402.50";
            lblCustomersValue.Text = "248";

            gridRecent.DataSource = new[]
            {
                new { OrderId = "#SM-9821", Patient = "Robert Fox", Medication = "Amoxicillin 500mg", Status = "Shipped", Time = "09:12 AM" },
                new { OrderId = "#SM-9820", Patient = "Jane Cooper", Medication = "Lisinopril 10mg", Status = "Processing", Time = "08:45 AM" },
                new { OrderId = "#SM-9819", Patient = "Wade Warren", Medication = "Metformin 850mg", Status = "On Hold", Time = "08:30 AM" },
                new { OrderId = "#SM-9818", Patient = "Esther Howard", Medication = "Atorvastatin 20mg", Status = "Shipped", Time = "07:55 AM" }
            };

            BindLowStockAlerts(new[]
            {
                ("Insulin Glargine", "CRITICAL: 2 units left", true),
                ("Gabapentin 300mg", "LOW: 15 units left", false)
            });
            BindExpiryAlerts(new[]
            {
                ("Amoxicillin 500mg", "EXPIRED: exp. 2024-08-15", true),
                ("Lisinopril 10mg", "EXPIRING SOON: exp. 2025-07-12", false)
            });
        }

        private void LoadDashboardData()
        {
            lblWelcome.Text = $"Welcome, {Session.CurrentAdmin?.Username ?? "admin"}";
            lblStockValue.Text = _reports.MedicinesInStock.ToString("N0");
            lblOrdersValue.Text = _reports.ActiveOrders.ToString("N0");
            lblSalesValue.Text = $"LKR {_reports.TotalSales:N2}";
            lblCustomersValue.Text = _reports.RegisteredCustomers.ToString("N0");

            gridRecent.DataSource = _orders.GetRecentSummaries(8);
            LoadLowStockAlerts();
            LoadExpiryAlerts();
        }

        private void LoadLowStockAlerts()
        {
            var alerts = _medicines.GetLowStock(maxCount: 6)
                .Select(m =>
                {
                    var critical = m.StockQuantity <= 5;
                    return (m.MedicineName,
                        critical ? $"CRITICAL: {m.StockQuantity} units left" : $"LOW: {m.StockQuantity} units left",
                        critical);
                })
                .ToList();
            BindLowStockAlerts(alerts);
        }

        private void LoadExpiryAlerts()
        {
            var alerts = new List<(string Name, string Message, bool Critical)>();

            foreach (var med in _medicines.GetExpiredMedicines().Take(4))
                alerts.Add((med.MedicineName, $"EXPIRED: exp. {med.ExpiryDate:yyyy-MM-dd}", true));

            foreach (var med in _medicines.GetExpiringSoonMedicines().Take(4))
            {
                if (alerts.Count >= 6) break;
                alerts.Add((med.MedicineName, $"EXPIRING SOON: exp. {med.ExpiryDate:yyyy-MM-dd}", false));
            }

            BindExpiryAlerts(alerts);
        }

        private void BindLowStockAlerts(IEnumerable<(string Name, string Message, bool Critical)> alerts) =>
            BindAlerts(panelLowStockAlerts, alerts, "No low stock alerts.");

        private void BindExpiryAlerts(IEnumerable<(string Name, string Message, bool Critical)> alerts) =>
            BindAlerts(panelExpiryAlerts, alerts, "No expiry warnings.");

        private static void BindAlerts(
            FlowLayoutPanel panel,
            IEnumerable<(string Name, string Message, bool Critical)> alerts,
            string emptyText)
        {
            panel.Controls.Clear();
            var list = alerts.ToList();
            if (list.Count == 0)
            {
                panel.Controls.Add(new Label
                {
                    Text = emptyText,
                    AutoSize = true,
                    ForeColor = SystemColors.GrayText,
                    Padding = new Padding(4)
                });
                return;
            }

            foreach (var alert in list)
                panel.Controls.Add(CreateAlertRow(panel, alert.Name, alert.Message, alert.Critical));
        }

        private static Panel CreateAlertRow(FlowLayoutPanel host, string name, string message, bool critical)
        {
            var row = new Panel
            {
                Width = Math.Max(200, host.ClientSize.Width - 24),
                Height = 52,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = critical ? Color.FromArgb(255, 245, 245) : Color.FromArgb(248, 248, 248),
                Padding = new Padding(12, 8, 8, 8)
            };
            row.Paint += (s, e) =>
            {
                using (var pen = new Pen(critical ? Color.Red : SystemColors.ControlDark, 3))
                    e.Graphics.DrawLine(pen, 0, 0, 0, row.Height);
            };
            row.Controls.Add(new Label
            {
                Text = critical ? "!" : "i",
                Font = UiTheme.UiFontBold,
                ForeColor = critical ? Color.Red : SystemColors.ControlDark,
                Location = new Point(4, 12),
                AutoSize = true
            });
            row.Controls.Add(new Label
            {
                Text = name,
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.ControlText,
                Location = new Point(20, 4),
                AutoSize = true
            });
            row.Controls.Add(new Label
            {
                Text = message,
                Font = UiTheme.UiFont,
                ForeColor = critical ? Color.Red : SystemColors.ControlText,
                Location = new Point(28, 24),
                AutoSize = true
            });
            return row;
        }
    }
}
