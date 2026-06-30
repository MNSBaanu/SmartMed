using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ReportsForm : AdminPageControl
    {
        private ReportService _reports;
        private CustomerService _customers;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        private ReportTab _activeTab = ReportTab.SalesPerformance;
        private ReportPeriod _activePeriod = ReportPeriod.Month;
        private bool _reportViewed;
        private DataTable _currentReportTable;

        public ReportsForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _reports = new ReportService();
                _customers = new CustomerService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
            else
            {
                UpdateTabStyles();
                UpdatePeriodStyles();
                UpdateStatTitlesForTab();
            }
        }

        protected override void BuildPageLayout()
        {
            // Layout lives in ReportsForm.Designer.cs.
        }

        protected override void DoRefreshPage()
        {
            if (_reportViewed)
                LoadActiveReport();
            else
                ResetReportPreview();
        }

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            UpdateTabStyles();
            UpdatePeriodStyles();
            UpdateStatTitlesForTab();

            _currentReportTable = DesignTimePreviewData.SalesReportTable();
            UiTheme.SetGridDataSource(gridReport, _currentReportTable);
            UiTheme.BeautifyGridHeaders(gridReport);
            lblTotalRevenue.Text = "LKR 3,340.00";
            lblTotalOrders.Text = "2";
            lblLowStock.Text = "1";
            lblOutstanding.Text = "LKR 890.00";
            lblFooterStatus.Text = "Design preview — sample sales report.";
            _reportViewed = true;
            UpdateExportButtons();
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridReport);
            UiTheme.StyleComboBox(cmbCustomer);

            WirePanelBorder(panelGridOuter);
            WireStatCard(panelStatRevenue, UiTheme.AdminTeal);
            WireStatCard(panelStatOrders, Color.FromArgb(59, 130, 246));
            WireStatCard(panelStatLowStock, UiTheme.Danger);
            WireStatCard(panelStatOutstanding, Color.FromArgb(16, 185, 129));

            btnSalesTab.Tag = ReportTab.SalesPerformance;
            btnInventoryTab.Tag = ReportTab.MedicineInventory;
            btnHistoryTab.Tag = ReportTab.CustomerOrderHistory;
            btnWeekPeriod.Tag = ReportPeriod.Week;
            btnMonthPeriod.Tag = ReportPeriod.Month;
            btnYearPeriod.Tag = ReportPeriod.Year;

            UpdatePeriodFilterVisibility();
            UpdateTabStyles();
            UpdatePeriodStyles();
            UpdateStatTitlesForTab();
            UpdateExportButtons();
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

        private static void WireStatCard(Panel card, Color accent)
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
                using (var brush = new SolidBrush(accent))
                    e.Graphics.FillRectangle(brush, 0, 0, 4, rect.Height);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnViewReport.Click += BtnViewReport_Click;
            btnExportCsv.Click += BtnExportCsv_Click;
            btnExportPdf.Click += BtnExportPdf_Click;
            btnSalesTab.Click += (s, e) => SwitchTab(ReportTab.SalesPerformance);
            btnInventoryTab.Click += (s, e) => SwitchTab(ReportTab.MedicineInventory);
            btnHistoryTab.Click += (s, e) => SwitchTab(ReportTab.CustomerOrderHistory);
            btnWeekPeriod.Click += (s, e) => SwitchPeriod(ReportPeriod.Week);
            btnMonthPeriod.Click += (s, e) => SwitchPeriod(ReportPeriod.Month);
            btnYearPeriod.Click += (s, e) => SwitchPeriod(ReportPeriod.Year);
            cmbCustomer.SelectedIndexChanged += (s, e) => ResetReportPreview();
            gridReport.CellFormatting += GridReport_CellFormatting;
        }

        private void SwitchTab(ReportTab tab)
        {
            _activeTab = tab;
            panelCustomerFilter.Visible = tab == ReportTab.CustomerOrderHistory;
            UpdatePeriodFilterVisibility();
            UpdateTabStyles();
            UpdateStatTitlesForTab();
            ResetReportPreview();
        }

        private void SwitchPeriod(ReportPeriod period)
        {
            _activePeriod = period;
            UpdatePeriodStyles();
            ResetReportPreview();
        }

        private void UpdateStatTitlesForTab()
        {
            if (lblStatTitleRevenue == null) return;

            if (_activeTab == ReportTab.MedicineInventory)
            {
                lblStatTitleRevenue.Text = "TOTAL ITEMS";
                lblStatTitleOrders.Text = "LOW STOCK";
                lblStatTitleLowStock.Text = "EXPIRED";
                lblStatTitleOutstanding.Text = "NEAR EXPIRY";
                return;
            }

            if (_activeTab == ReportTab.CustomerOrderHistory)
            {
                lblStatTitleRevenue.Text = "PERIOD SPEND";
                lblStatTitleOrders.Text = "ORDERS";
                lblStatTitleLowStock.Text = "LOW STOCK";
                lblStatTitleOutstanding.Text = "OUTSTANDING";
                return;
            }

            lblStatTitleRevenue.Text = "COMPLETED REVENUE";
            lblStatTitleOrders.Text = "COMPLETED ORDERS";
            lblStatTitleLowStock.Text = "LOW STOCK ITEMS";
            lblStatTitleOutstanding.Text = "OUTSTANDING";
        }

        private void UpdatePeriodFilterVisibility()
        {
            if (panelPeriodFilter != null)
                panelPeriodFilter.Visible = _activeTab != ReportTab.MedicineInventory;
        }

        private void UpdatePeriodStyles()
        {
            StylePeriod(btnWeekPeriod, _activePeriod == ReportPeriod.Week);
            StylePeriod(btnMonthPeriod, _activePeriod == ReportPeriod.Month);
            StylePeriod(btnYearPeriod, _activePeriod == ReportPeriod.Year);
        }

        private static void StylePeriod(Button btn, bool active)
        {
            if (btn == null) return;
            if (active)
            {
                btn.BackColor = UiTheme.AdminTeal;
                btn.ForeColor = Color.White;
                btn.Font = UiTheme.UiFontBold;
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = UiTheme.AdminOnSurface;
                btn.Font = UiTheme.UiFont;
                btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                btn.FlatAppearance.BorderSize = 1;
            }
        }

        private string GetPeriodStatusText() => ReportPeriodHelper.GetLabel(_activePeriod);

        private void UpdateTabStyles()
        {
            StyleTab(btnSalesTab, _activeTab == ReportTab.SalesPerformance);
            StyleTab(btnInventoryTab, _activeTab == ReportTab.MedicineInventory);
            StyleTab(btnHistoryTab, _activeTab == ReportTab.CustomerOrderHistory);
        }

        private static void StyleTab(Button btn, bool active) => UiTheme.StyleTabButton(btn, active);

        private void ResetReportPreview()
        {
            _reportViewed = false;
            _currentReportTable = null;
            if (gridReport != null)
                gridReport.DataSource = null;
            if (lblFooterStatus != null)
                lblFooterStatus.Text = "Select report type and filters, then click View Report.";
            ClearSummaryStats();
            UpdateExportButtons();
        }

        private void ClearSummaryStats()
        {
            if (lblTotalRevenue == null) return;
            lblTotalRevenue.Text = "—";
            lblTotalOrders.Text = "—";
            lblLowStock.Text = "—";
            lblOutstanding.Text = "—";
        }

        private void UpdateExportButtons()
        {
            if (btnExportCsv == null || btnExportPdf == null) return;
            var canExport = _reportViewed && _currentReportTable != null && _currentReportTable.Rows.Count > 0;
            btnExportCsv.Enabled = canExport;
            btnExportPdf.Enabled = canExport;
        }

        private void BtnViewReport_Click(object sender, EventArgs e) => LoadActiveReport();

        private void LoadActiveReport()
        {
            if (!_servicesReady) return;

            try
            {
                if (_activeTab == ReportTab.SalesPerformance)
                    LoadSalesReport();
                else if (_activeTab == ReportTab.MedicineInventory)
                    LoadInventoryReport();
                else
                    LoadHistoryReport();

                UpdateSummaryStats();
                _reportViewed = _currentReportTable != null;
                UpdateExportButtons();

                if (_reportViewed && _currentReportTable.Rows.Count == 0)
                    MessageBox.Show("No records found for the selected filters.", "View Report",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                ResetReportPreview();
                MessageBox.Show(ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSalesReport()
        {
            var table = _reports.GetSalesReport(_activePeriod);
            _currentReportTable = table;
            UiTheme.SetGridDataSource(gridReport, table);
            UiTheme.BeautifyGridHeaders(gridReport);
            lblFooterStatus.Text =
                $"Items: {table.Rows.Count} | Completed sales only | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void LoadInventoryReport()
        {
            var table = _reports.GetStockReport();
            _currentReportTable = table;
            UiTheme.SetGridDataSource(gridReport, table);
            UiTheme.BeautifyGridHeaders(gridReport);

            var current = CountColumnValue(table, "InventoryStatus", "Current");
            var lowStock = CountColumnValue(table, "StockStatus", "Low Stock");
            var expired = CountColumnValue(table, "ExpiryStatus", "Expired");
            var nearExpiry = CountColumnValue(table, "ExpiryStatus", "Near Expiry");

            lblFooterStatus.Text =
                $"Items: {table.Rows.Count} | Current: {current} | Low stock: {lowStock} | Expired: {expired} | Near expiry: {nearExpiry} | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void LoadHistoryReport()
        {
            var allCustomers = _customers.GetAll();
            if (cmbCustomer.Items.Count == 0)
            {
                cmbCustomer.DisplayMember = "Name";
                cmbCustomer.ValueMember = "CustomerID";
                cmbCustomer.DataSource = allCustomers;
            }

            if (cmbCustomer.SelectedValue == null && allCustomers.Count > 0)
                cmbCustomer.SelectedIndex = 0;

            if (cmbCustomer.SelectedValue == null)
            {
                _currentReportTable = null;
                gridReport.DataSource = null;
                lblFooterStatus.Text = "No customers available | Server Connected";
                return;
            }

            var customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            var table = _reports.GetCustomerOrderHistory(customerId, _activePeriod);
            _currentReportTable = table;
            UiTheme.SetGridDataSource(gridReport, table);
            UiTheme.BeautifyGridHeaders(gridReport);
            lblFooterStatus.Text =
                $"Items: {table.Rows.Count} | Customer order history | {cmbCustomer.Text} | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt}";
        }

        private void UpdateSummaryStats()
        {
            if (!_servicesReady) return;

            if (_activeTab == ReportTab.MedicineInventory)
            {
                var stock = _currentReportTable ?? _reports.GetStockReport();
                lblTotalRevenue.Text = stock.Rows.Count.ToString("N0");
                lblTotalOrders.Text = CountColumnValue(stock, "StockStatus", "Low Stock").ToString("N0");
                lblLowStock.Text = CountColumnValue(stock, "ExpiryStatus", "Expired").ToString("N0");
                lblOutstanding.Text = CountColumnValue(stock, "ExpiryStatus", "Near Expiry").ToString("N0");
                return;
            }

            if (_activeTab == ReportTab.CustomerOrderHistory && _currentReportTable != null)
            {
                decimal spend = 0;
                foreach (DataRow row in _currentReportTable.Rows)
                    spend += Convert.ToDecimal(row["TotalAmount"]);

                lblTotalRevenue.Text = $"LKR {spend:N2}";
                lblTotalOrders.Text = _currentReportTable.Rows.Count.ToString("N0");

                var stock = _reports.GetStockReport();
                lblLowStock.Text = CountColumnValue(stock, "StockStatus", "Low Stock").ToString("N0");
                lblOutstanding.Text = $"LKR {_reports.GetOutstandingAmount(_activePeriod):N2}";
                return;
            }

            var sales = _reports.GetSalesReport(_activePeriod);
            decimal totalRevenue = 0;
            foreach (DataRow row in sales.Rows)
                totalRevenue += Convert.ToDecimal(row["TotalAmount"]);

            lblTotalRevenue.Text = $"LKR {totalRevenue:N2}";
            lblTotalOrders.Text = sales.Rows.Count.ToString("N0");

            var inventory = _reports.GetStockReport();
            lblLowStock.Text = CountColumnValue(inventory, "StockStatus", "Low Stock").ToString("N0");
            lblOutstanding.Text = $"LKR {_reports.GetOutstandingAmount(_activePeriod):N2}";
        }

        private static int CountColumnValue(DataTable table, string column, string value)
        {
            if (table == null || !table.Columns.Contains(column)) return 0;
            var count = 0;
            foreach (DataRow row in table.Rows)
            {
                if (string.Equals(row[column]?.ToString(), value, StringComparison.OrdinalIgnoreCase))
                    count++;
            }
            return count;
        }

        private void GridReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_activeTab != ReportTab.MedicineInventory || e.RowIndex < 0 || gridReport == null) return;

            var status = gridReport.Rows[e.RowIndex].Cells["InventoryStatus"]?.Value?.ToString();
            if (string.IsNullOrEmpty(status)) return;

            if (status == "Expired")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 235, 235);
                e.CellStyle.ForeColor = Color.DarkRed;
            }
            else if (status == "Near Expiry")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 248, 220);
                e.CellStyle.ForeColor = Color.FromArgb(140, 70, 0);
            }
            else if (status == "Low Stock")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 242, 230);
                e.CellStyle.ForeColor = Color.DarkOrange;
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e) => ExportReport(isPdf: false);

        private void BtnExportPdf_Click(object sender, EventArgs e) => ExportReport(isPdf: true);

        private void ExportReport(bool isPdf)
        {
            if (!_reportViewed || _currentReportTable == null)
            {
                MessageBox.Show("View the report before exporting.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentReportTable.Rows.Count == 0)
            {
                MessageBox.Show("Nothing to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var periodSuffix = _activeTab == ReportTab.MedicineInventory
                    ? string.Empty
                    : $"_{ReportPeriodHelper.GetFileSuffix(_activePeriod)}";
                var baseName = $"{GetExportBaseName()}{periodSuffix}_{DateTime.Now:yyyyMMdd}";

                if (isPdf)
                {
                    using (var dialog = new SaveFileDialog
                    {
                        Filter = "PDF files (*.pdf)|*.pdf",
                        FileName = $"{baseName}.pdf"
                    })
                    {
                        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                        _reports.ExportActiveReportToPdf(_currentReportTable, dialog.FileName, GetReportTitle(), GetReportSubtitle());
                        MessageBox.Show("Report exported as PDF.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    using (var dialog = new SaveFileDialog
                    {
                        Filter = "CSV files (*.csv)|*.csv",
                        FileName = $"{baseName}.csv"
                    })
                    {
                        if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                        _reports.ExportActiveReportToCsv(_currentReportTable, dialog.FileName);
                        MessageBox.Show("Report exported as CSV.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetReportTitle()
        {
            if (_activeTab == ReportTab.SalesPerformance) return "Sales Performance Report";
            if (_activeTab == ReportTab.MedicineInventory) return "Medicine Inventory Report";
            return "Customer Order History Report";
        }

        private string GetReportSubtitle()
        {
            if (_activeTab == ReportTab.MedicineInventory)
                return $"Generated {DateTime.Now:MMM dd, yyyy hh:mm tt} | Current, low stock, expired, and near-expiry items";

            if (_activeTab == ReportTab.CustomerOrderHistory)
                return $"Customer: {cmbCustomer?.Text} | Period: {GetPeriodStatusText()} | Generated {DateTime.Now:MMM dd, yyyy hh:mm tt}";

            return $"Period: {GetPeriodStatusText()} | Completed orders only | Generated {DateTime.Now:MMM dd, yyyy hh:mm tt}";
        }

        private string GetExportBaseName()
        {
            if (_activeTab == ReportTab.SalesPerformance) return "sales-performance";
            if (_activeTab == ReportTab.MedicineInventory) return "medicine-inventory";
            return "customer-order-history";
        }
    }
}
