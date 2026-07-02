using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class AdminDashboardForm : EmbeddedPageForm
    {
        private ReportService _reports;
        private OrderService _orders;
        private MedicineService _medicines;
        private bool _servicesReady;

        private List<object> _recentRows = new List<object>();
        private bool _runtimeWired;
        private bool _chromeApplied;

        public AdminDashboardForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _reports = new ReportService();
                _orders = new OrderService();
                _medicines = new MedicineService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        public void RefreshData() => RefreshPage();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
        }

        protected override void DoRefreshPage() => LoadDashboardData();

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridLowStock);
            UiTheme.ApplyClinicalGrid(gridExpiry);
            UiTheme.ApplyClinicalGrid(gridRecent);

            WireStatCard(panelStatStock, UiTheme.AdminTeal, skipAccent: true);
            WireStatCard(panelStatOrders, Color.FromArgb(184, 237, 226), skipAccent: true);
            WireStatCard(panelStatSales, Color.FromArgb(199, 234, 228), skipAccent: true);
            WireStatCard(panelStatUsers, Color.FromArgb(171, 205, 200), skipAccent: true);
            WirePanelBorder(panelLowStockOuter);
            WirePanelBorder(panelExpiryOuter);
            WirePanelBorder(panelRecentOuter);

            gridLowStock.CellFormatting += GridAlertStatus_CellFormatting;
            gridExpiry.CellFormatting += GridAlertStatus_CellFormatting;
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "dash-border") return;
            panel.Tag = "dash-border";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private static void WireStatCard(Panel card, Color accent, bool skipAccent = false)
        {
            if (card == null || card.Tag as string == "dash-stat") return;
            card.Tag = "dash-stat";
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
                if (!skipAccent)
                {
                    using (var brush = new SolidBrush(accent))
                        e.Graphics.FillRectangle(brush, 0, 0, 4, rect.Height);
                }
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnRefresh.Click += (s, e) => LoadDashboardData();
            btnNewOrder.Click += (s, e) => GoToAdminSection(AdminNavItem.Orders);
            btnPrint.Click += BtnPrintRecent_Click;
            gridRecent.CellFormatting += GridRecent_CellFormatting;
            gridRecent.CellContentClick += GridRecent_CellContentClick;
        }

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            lblStockValue.Text = "128";
            lblOrdersValue.Text = "4";
            lblSalesValue.Text = "245,600";
            lblCustomersValue.Text = "86";

            gridLowStock.DataSource = new[]
            {
                new { MedicineName = "Amoxicillin 500mg", Level = "8 units", Status = "LOW" },
                new { MedicineName = "Metformin 850mg", Level = "3 units", Status = "CRITICAL" }
            };
            UiTheme.BeautifyGridHeaders(gridLowStock);

            gridExpiry.DataSource = new[]
            {
                new { BatchId = "#M-003", Medicine = "Metformin 850mg", DueDate = "EXPIRED" },
                new { BatchId = "#M-001", Medicine = "Amoxicillin 500mg", DueDate = "18 Days" }
            };
            UiTheme.BeautifyGridHeaders(gridExpiry);

            _recentRows = new List<object>
            {
                new { OrderRef = "#SM-0001", CustomerName = "Jane Perera", FulfillmentStatus = OrderService.StatusPending, Timestamp = "2 hours ago" },
                new { OrderRef = "#SM-0002", CustomerName = "Kamal Silva", FulfillmentStatus = OrderService.StatusDelivered, Timestamp = "1 day ago" }
            };
            BindRecentGrid(_recentRows);
        }

        private void LoadDashboardData()
        {
            if (!_servicesReady) return;

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
            BindRecentGrid(_recentRows);
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
            GoToAdminSection(AdminNavItem.Orders);
        }

        private void GoToAdminSection(AdminNavItem nav)
        {
            var host = FindForm() as AdminHostForm;
            if (host == null)
            {
                for (Control parent = Parent; parent != null; parent = parent.Parent)
                {
                    if (parent is AdminHostForm adminHost)
                    {
                        host = adminHost;
                        break;
                    }
                }
            }
            host?.NavigateTo(nav);
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
