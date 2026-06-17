using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Controls
{
    public partial class AdminDashboardPanel : UserControl
    {
        private readonly DashboardService _dashboard = new DashboardService();
        private readonly OrderService _orders = new OrderService();
        private readonly MedicineService _medicines = new MedicineService();
        private bool _dataLoaded;

        private Label lblWelcome;
        private Label lblStatus;
        private Label lblStockValue;
        private Label lblOrdersValue;
        private Label lblSalesValue;
        private DataGridView gridRecent;
        private FlowLayoutPanel panelAlerts;
        private TableLayoutPanel _scrollRoot;

        public AdminDashboardPanel()
        {
            InitializeComponent();
            FontManager.Initialize();
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BuildContent();
            ApplyDashboardTheme();
            if (IsDesignHost())
                LoadDesignTimePreview();
            Load += AdminDashboardPanel_Load;
        }

        private static bool IsDesignHost() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        private void AdminDashboardPanel_Load(object sender, EventArgs e)
        {
            if (_dataLoaded) return;
            _dataLoaded = true;
            if (!IsDesignHost())
                LoadDashboardData();
        }

        public void RefreshData() => LoadDashboardData();

        private int GetScrollContentWidth()
        {
            var w = ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            if (w < 200)
                w = 850;
            return w;
        }

        private void BuildContent()
        {
            lblWelcome = new Label();
            lblStatus = new Label();
            lblStockValue = new Label();
            lblOrdersValue = new Label();
            lblSalesValue = new Label();
            gridRecent = CreateGrid();
            panelAlerts = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                Dock = DockStyle.Fill
            };

            Controls.Clear();
            AutoScroll = true;

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 4,
                MinimumSize = new Size(0, 850),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 280f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateMiddleRow(), 0, 2);
            _scrollRoot.Controls.Add(CreateTrendsSection(), 0, 3);

            Controls.Add(_scrollRoot);
            Resize += (s, e) =>
            {
                if (_scrollRoot != null)
                    _scrollRoot.Width = GetScrollContentWidth();
            };
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
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Height = 28;
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Height = 22;

            header.Controls.Add(lblStatus);
            header.Controls.Add(lblWelcome);
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
                ColumnCount = 3,
                RowCount = 1
            };
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));

            statsRow.Controls.Add(CreateStatCard("\uE7C3", "Medicines in Stock", lblStockValue,
                "SKUs tracked across inventory", "+12%"), 0, 0);
            statsRow.Controls.Add(CreateStatCard("\uE7BF", "Active Orders", lblOrdersValue,
                "Orders requiring verification", "Priority"), 1, 0);
            statsRow.Controls.Add(CreateStatCard("\uE8CB", "Total Sales", lblSalesValue,
                "Lifetime pharmacy revenue (LKR)", "Daily"), 2, 0);

            wrap.Controls.Add(statsRow);
            return wrap;
        }

        private Panel CreateMiddleRow()
        {
            var middleRow = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16)
            };
            middleRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66f));
            middleRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34f));

            var (gridOuter, gridBody) = CreateSectionPanel("Recent Fulfillment Activity", showViewAll: true);
            gridRecent.Dock = DockStyle.Fill;
            gridBody.Controls.Add(gridRecent);
            middleRow.Controls.Add(gridOuter, 0, 0);

            var rightCol = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8, 0, 0, 0) };
            var quickPanel = CreateQuickFulfillmentPanel();
            quickPanel.Dock = DockStyle.Top;
            quickPanel.Height = 130;
            var (alertsOuter, alertsBody) = CreateSectionPanel("Stock Alerts");
            alertsOuter.Dock = DockStyle.Fill;
            panelAlerts.Dock = DockStyle.Fill;
            alertsBody.Controls.Add(panelAlerts);
            rightCol.Controls.Add(alertsOuter);
            rightCol.Controls.Add(quickPanel);
            middleRow.Controls.Add(rightCol, 1, 0);

            var wrap = new Panel { Dock = DockStyle.Fill, Height = 280 };
            wrap.Controls.Add(middleRow);
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
                BackColor = AppTheme.SurfaceContainer,
                Padding = new Padding(16)
            };
            chartArea.Paint += (s, e) =>
            {
                var rect = chartArea.ClientRectangle;
                rect.Inflate(-16, -16);
                using (var pen = new Pen(Color.FromArgb(128, AppTheme.OutlineVariant)))
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
                    using (var brush = new SolidBrush(Color.FromArgb(alpha, AppTheme.Primary)))
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
                ForeColor = AppTheme.OnSurfaceVariant,
                Font = AppTheme.LabelFont
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
                BackgroundColor = AppTheme.SurfaceContainerLowest,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                ScrollBars = ScrollBars.Vertical
            };
            ThemeApplier.ApplyDataGrid(grid);
            return grid;
        }

        private Panel CreateStatCard(string iconGlyph, string title, Label valueLabel, string subtitle, string badge)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 8, 0),
                Padding = new Padding(16),
                BackColor = AppTheme.SurfaceContainerLowest
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var badgeLabel = new Label
            {
                Text = badge,
                Font = AppTheme.LabelFont,
                ForeColor = badge == "Priority" ? AppTheme.Error : AppTheme.OnSecondaryContainer,
                BackColor = badge == "Priority" ? Color.FromArgb(255, 218, 214) : AppTheme.SecondaryContainer,
                AutoSize = true,
                Location = new Point(card.Width - 72, 12),
                Padding = new Padding(4, 2, 4, 2)
            };
            card.Controls.Add(badgeLabel);
            card.Controls.Add(new Label
            {
                Text = iconGlyph,
                Font = AppTheme.IconFont,
                ForeColor = AppTheme.Primary,
                AutoSize = true,
                Location = new Point(16, 12)
            });
            card.Controls.Add(new Label
            {
                Text = title,
                Font = AppTheme.LabelFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                AutoSize = true,
                Location = new Point(16, 44)
            });

            valueLabel.Text = "0";
            valueLabel.Font = AppTheme.StatValueFont;
            valueLabel.ForeColor = AppTheme.Primary;
            valueLabel.AutoSize = true;
            valueLabel.Location = new Point(16, 64);
            card.Controls.Add(valueLabel);

            card.Controls.Add(new Label
            {
                Text = subtitle,
                Font = AppTheme.VersionFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                AutoSize = true,
                Location = new Point(16, 96)
            });

            return card;
        }

        private (Panel outer, Panel body) CreateSectionPanel(string title, bool showViewAll = false, bool showPeriodTabs = false)
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.SurfaceContainerLowest,
                Padding = new Padding(1)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = AppTheme.SurfaceContainer,
                Padding = new Padding(16, 12, 16, 8)
            };
            header.Controls.Add(new Label
            {
                Text = title,
                Font = AppTheme.SectionHeaderFont,
                ForeColor = AppTheme.Primary,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            if (showViewAll)
            {
                var btnViewAll = new LinkLabel
                {
                    Text = "View All",
                    Dock = DockStyle.Right,
                    AutoSize = true,
                    LinkColor = AppTheme.Primary
                };
                btnViewAll.Click += (s, e) => ShowComingSoon("View All Orders");
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
                btn.BackColor = AppTheme.Primary;
                btn.ForeColor = AppTheme.OnPrimary;
            }
            else
            {
                btn.BackColor = AppTheme.SurfaceContainer;
                btn.ForeColor = AppTheme.OnSurface;
            }
            btn.Font = AppTheme.LabelFont;
            return btn;
        }

        private Panel CreateQuickFulfillmentPanel()
        {
            var panel = new Panel
            {
                BackColor = AppTheme.Primary,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 0, 8)
            };
            panel.Controls.Add(new Label
            {
                Text = "Quick Fulfillment",
                Font = AppTheme.SectionHeaderFont,
                ForeColor = AppTheme.OnPrimary,
                Dock = DockStyle.Top,
                Height = 24
            });
            panel.Controls.Add(new Label
            {
                Text = "Scan RX barcode or enter order ID to start processing.",
                ForeColor = AppTheme.OnPrimaryMuted,
                Dock = DockStyle.Top,
                Height = 32
            });

            var inputRow = new Panel { Dock = DockStyle.Top, Height = 36 };
            var txtScan = new TextBox
            {
                Text = "",
                Width = 140,
                Location = new Point(0, 4)
            };
            ThemeApplier.ApplyTextBox(txtScan);
            txtScan.BackColor = Color.FromArgb(230, 245, 250, 255);
            var btnStart = new Button
            {
                Text = "START",
                Location = new Point(148, 2),
                Width = 72,
                Height = 32
            };
            ThemeApplier.ApplyPrimaryButton(btnStart);
            btnStart.Click += (s, e) => ShowComingSoon("Order Fulfillment");
            inputRow.Controls.Add(btnStart);
            inputRow.Controls.Add(txtScan);
            panel.Controls.Add(inputRow);

            return panel;
        }

        private static void ShowComingSoon(string feature)
        {
            MessageBox.Show($"{feature} will be available in the next update.", "SmartMed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ApplyDashboardTheme()
        {
            lblWelcome.Font = AppTheme.SectionHeaderFont;
            lblWelcome.ForeColor = AppTheme.Primary;
            lblStatus.Font = AppTheme.BodyFont;
            lblStatus.ForeColor = AppTheme.OnSurfaceVariant;
        }

        private void LoadDesignTimePreview()
        {
            lblWelcome.Text = "Welcome, admin";
            lblStockValue.Text = "4,281";
            lblOrdersValue.Text = "127";
            lblSalesValue.Text = "LKR 12,402.50";

            gridRecent.DataSource = new[]
            {
                new { OrderId = "#SM-9821", Patient = "Robert Fox", Medication = "Amoxicillin 500mg", Status = "Shipped", Time = "09:12 AM" },
                new { OrderId = "#SM-9820", Patient = "Jane Cooper", Medication = "Lisinopril 10mg", Status = "Processing", Time = "08:45 AM" },
                new { OrderId = "#SM-9819", Patient = "Wade Warren", Medication = "Metformin 850mg", Status = "On Hold", Time = "08:30 AM" },
                new { OrderId = "#SM-9818", Patient = "Esther Howard", Medication = "Atorvastatin 20mg", Status = "Shipped", Time = "07:55 AM" }
            };

            panelAlerts.Controls.Clear();
            panelAlerts.Controls.Add(CreateAlertRow("Insulin Glargine", "CRITICAL: 2 units left", critical: true));
            panelAlerts.Controls.Add(CreateAlertRow("Gabapentin 300mg", "LOW: 15 units left", critical: false));
        }

        private void LoadDashboardData()
        {
            lblWelcome.Text = $"Welcome, {Session.CurrentAdmin?.Username ?? "admin"}";
            lblStockValue.Text = _dashboard.MedicinesInStock.ToString("N0");
            lblOrdersValue.Text = _dashboard.ActiveOrders.ToString("N0");
            lblSalesValue.Text = $"LKR {_dashboard.TotalSales:N2}";

            var rows = _orders.GetAllOrders().Take(8).Select(o =>
            {
                var items = _orders.GetOrderItems(o.OrderID);
                var med = items.Count > 0 ? items[0].MedicineName : "-";
                if (items.Count > 1) med += $" (+{items.Count - 1})";
                return new
                {
                    OrderId = $"#SM-{o.OrderID:D4}",
                    Patient = o.CustomerName,
                    Medication = med,
                    Status = o.Status,
                    Time = o.OrderDate.ToString("hh:mm tt")
                };
            }).ToList();

            gridRecent.DataSource = rows;

            panelAlerts.Controls.Clear();
            var lowStock = _medicines.GetAll()
                .Where(m => m.StockQuantity <= 20)
                .OrderBy(m => m.StockQuantity)
                .Take(4)
                .ToList();

            if (lowStock.Count == 0)
            {
                panelAlerts.Controls.Add(new Label
                {
                    Text = "No critical stock alerts.",
                    AutoSize = true,
                    ForeColor = AppTheme.OnSurfaceVariant,
                    Padding = new Padding(4)
                });
                return;
            }

            foreach (var med in lowStock)
            {
                var critical = med.StockQuantity <= 5;
                panelAlerts.Controls.Add(CreateAlertRow(med.MedicineName,
                    critical ? $"CRITICAL: {med.StockQuantity} units left" : $"LOW: {med.StockQuantity} units left",
                    critical));
            }
        }

        private Panel CreateAlertRow(string name, string message, bool critical)
        {
            var row = new Panel
            {
                Width = Math.Max(200, panelAlerts.ClientSize.Width - 24),
                Height = 52,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = critical ? Color.FromArgb(255, 245, 245) : Color.FromArgb(248, 250, 255),
                Padding = new Padding(12, 8, 8, 8)
            };
            row.Paint += (s, e) =>
            {
                using (var pen = new Pen(critical ? AppTheme.Error : AppTheme.Secondary, 3))
                    e.Graphics.DrawLine(pen, 0, 0, 0, row.Height);
            };
            row.Controls.Add(new Label
            {
                Text = critical ? "\uE7BA" : "\uE946",
                Font = AppTheme.IconFont,
                ForeColor = critical ? AppTheme.Error : AppTheme.Secondary,
                Location = new Point(4, 12),
                AutoSize = true
            });
            row.Controls.Add(new Label
            {
                Text = name,
                Font = AppTheme.LabelFont,
                ForeColor = AppTheme.OnSurface,
                Location = new Point(28, 4),
                AutoSize = true
            });
            row.Controls.Add(new Label
            {
                Text = message,
                Font = AppTheme.LinkFont,
                ForeColor = critical ? AppTheme.Error : AppTheme.OnSecondaryContainer,
                Location = new Point(28, 24),
                AutoSize = true
            });
            return row;
        }
    }
}
