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

        public AdminDashboardPanel()
        {
            FontManager.Initialize();
            DoubleBuffered = true;
            BackColor = AppTheme.Surface;
            Dock = DockStyle.Fill;
            BuildDashboardContent();
            ApplyDashboardTheme();
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                LoadDesignTimePreview();
            Load += AdminDashboardPanel_Load;
        }

        private void AdminDashboardPanel_Load(object sender, EventArgs e)
        {
            if (_dataLoaded) return;
            _dataLoaded = true;
            if (LicenseManager.UsageMode != LicenseUsageMode.Designtime)
                LoadDashboardData();
        }

        public void RefreshData() => LoadDashboardData();

        private void BuildDashboardContent()
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

            var header = new Panel { Dock = DockStyle.Top, Height = 72, Padding = new Padding(0, 0, 0, 8) };
            lblWelcome.Dock = DockStyle.Top;
            lblWelcome.Height = 28;
            lblWelcome.Text = $"Welcome, {Session.CurrentAdmin?.Username ?? "admin"}";
            lblStatus.Dock = DockStyle.Top;
            lblStatus.Height = 22;
            lblStatus.Text = "System Status: Operational";
            header.Controls.Add(lblStatus);
            header.Controls.Add(lblWelcome);

            var statsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 130,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16)
            };
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));
            statsRow.Controls.Add(CreateStatCard("\uE7C3", "Medicines in Stock", lblStockValue, "Units available in inventory"), 0, 0);
            statsRow.Controls.Add(CreateStatCard("\uE7BF", "Active Orders", lblOrdersValue, "Orders pending fulfillment"), 1, 0);
            statsRow.Controls.Add(CreateStatCard("\uE8CB", "Total Sales", lblSalesValue, "Lifetime pharmacy revenue (LKR)"), 2, 0);

            var middleRow = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 240,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 16)
            };
            middleRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66f));
            middleRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34f));

            var (gridOuter, gridBody) = CreateSectionPanel("Recent Fulfillment Activity");
            gridRecent.Dock = DockStyle.Fill;
            gridBody.Controls.Add(gridRecent);
            middleRow.Controls.Add(gridOuter, 0, 0);

            var rightCol = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8, 0, 0, 0) };
            var quickPanel = CreateQuickFulfillmentPanel();
            quickPanel.Dock = DockStyle.Top;
            quickPanel.Height = 110;
            var (alertsOuter, alertsBody) = CreateSectionPanel("Stock Alerts");
            alertsOuter.Dock = DockStyle.Fill;
            panelAlerts.Dock = DockStyle.Fill;
            alertsBody.Controls.Add(panelAlerts);
            rightCol.Controls.Add(alertsOuter);
            rightCol.Controls.Add(quickPanel);
            middleRow.Controls.Add(rightCol, 1, 0);

            var (trendsOuter, trendsBody) = CreateSectionPanel("Sales & Demand Trends");
            trendsOuter.Dock = DockStyle.Top;
            trendsOuter.Height = 200;
            trendsBody.Controls.Add(new Label
            {
                Text = "Trend visualization will be available in a future update.",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = AppTheme.OnSurfaceVariant
            });

            Controls.Add(trendsOuter);
            Controls.Add(middleRow);
            Controls.Add(statsRow);
            Controls.Add(header);
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
                EnableHeadersVisualStyles = false
            };
            ThemeApplier.ApplyDataGrid(grid);
            return grid;
        }

        private Panel CreateStatCard(string iconGlyph, string title, Label valueLabel, string subtitle)
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

            valueLabel.Text = "0";
            valueLabel.Font = AppTheme.StatValueFont;
            valueLabel.ForeColor = AppTheme.Primary;
            valueLabel.AutoSize = true;
            valueLabel.Location = new Point(16, 64);

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

        private (Panel outer, Panel body) CreateSectionPanel(string title)
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
                Dock = DockStyle.Fill
            });

            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(8) };
            outer.Controls.Add(body);
            outer.Controls.Add(header);
            return (outer, body);
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
                Text = "Enter order ID to jump to order management.",
                ForeColor = AppTheme.OnPrimaryMuted,
                Dock = DockStyle.Top,
                Height = 36
            });
            var btn = new Button { Text = "Manage Orders", Dock = DockStyle.Bottom, Height = 32 };
            ThemeApplier.ApplyPrimaryButton(btn);
            panel.Controls.Add(btn);
            return panel;
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
            lblStockValue.Text = "1,248";
            lblOrdersValue.Text = "12";
            lblSalesValue.Text = "LKR 45,230.00";

            gridRecent.DataSource = new[]
            {
                new { OrderId = "#SM-0001", Patient = "John Smith", Medication = "Amoxicillin 500mg", Status = "Pending", Time = "10:30 AM" },
                new { OrderId = "#SM-0002", Patient = "Jane Doe", Medication = "Ibuprofen 200mg", Status = "Shipped", Time = "09:15 AM" }
            };

            panelAlerts.Controls.Clear();
            panelAlerts.Controls.Add(CreateAlertRow("Ibuprofen 200mg", "LOW: 12 units left", critical: true));
        }

        private void LoadDashboardData()
        {
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
                using (var pen = new Pen(critical ? AppTheme.Error : AppTheme.SecondaryContainer, 3))
                    e.Graphics.DrawLine(pen, 0, 0, 0, row.Height);
            };
            row.Controls.Add(new Label
            {
                Text = name,
                Font = AppTheme.LabelFont,
                ForeColor = AppTheme.OnSurface,
                Location = new Point(12, 4),
                AutoSize = true
            });
            row.Controls.Add(new Label
            {
                Text = message,
                Font = AppTheme.LinkFont,
                ForeColor = critical ? AppTheme.Error : AppTheme.OnSecondaryContainer,
                Location = new Point(12, 24),
                AutoSize = true
            });
            return row;
        }
    }
}
