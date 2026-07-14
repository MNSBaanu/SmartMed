using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ReportsForm : EmbeddedPageForm
    {
        private readonly ReportService _reports;
        private readonly CustomerService _customers;
        private readonly bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;
        private Label _lblPreviewPlaceholder;
        private Label _lblGridHeaderSubtitle;

        private const string TabSales = "SalesPerformance";
        private const string TabInventory = "MedicineInventory";
        private const string TabHistory = "CustomerOrderHistory";

        private string _activeTab = TabSales;
        private ReportPeriod _activePeriod = ReportPeriod.Week;
        private bool _reportViewed;
        private bool _suppressRangeEvents;
        private DataTable _currentReportTable;
        private DataTable _sourceReportTable;

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
            EnsureRuntimeReady();
            base.OnLoad(e);
        }

        protected override void SetVisibleCore(bool value)
        {
            if (value && _servicesReady && !IsDesignHost())
                EnsureRuntimeReady();
            base.SetVisibleCore(value);
        }

        internal void EnsureRuntimeReady()
        {
            ApplyViewChrome();
            if (!_servicesReady || _runtimeWired)
                return;

            WireRuntimeBehavior();
        }

        protected override void DoRefreshPage()
        {
            if (!_servicesReady || !_runtimeWired)
                return;

            try
            {
                EnsureCustomerFilterLoaded();
                if (_reportViewed)
                    LoadActiveReport();
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Reports", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            UpdateTabStyles();
            UpdatePeriodStyles();
            SyncPeriodDatesToPickers();
            UpdateDateRangeEnabled();
            UpdateStatTitlesForTab();

            _currentReportTable = null;
            _sourceReportTable = null;
            BindReportGrid(null);
            lblTotalRevenue.Text = "-";
            lblTotalOrders.Text = "-";
            lblLowStock.Text = "-";
            lblOutstanding.Text = "-";
            lblFooterStatus.Text = string.Empty;
            _reportViewed = false;
            UpdatePreviewDisplay();
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

            btnSalesTab.Tag = TabSales;
            btnInventoryTab.Tag = TabInventory;
            btnHistoryTab.Tag = TabHistory;
            btnWeekPeriod.Tag = ReportPeriod.Week;
            btnMonthPeriod.Tag = ReportPeriod.Month;
            btnYearPeriod.Tag = ReportPeriod.Year;

            UpdatePeriodFilterVisibility();
            UpdateTabStyles();
            UpdatePeriodStyles();
            SyncPeriodDatesToPickers();
            UpdateDateRangeEnabled();
            UpdateStatTitlesForTab();
            EnsurePreviewChrome();
            UpdateExportButtons();
        }

        private void EnsurePreviewChrome()
        {
            if (_lblPreviewPlaceholder == null && panelGridBody != null)
            {
                _lblPreviewPlaceholder = new Label
                {
                    Text = "Select report type and filters, then click View Report to preview results here.",
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = UiTheme.AdminMuted,
                    Font = UiTheme.UiFont,
                    BackColor = Color.White
                };
                panelGridBody.Controls.Add(_lblPreviewPlaceholder);
            }

            if (_lblGridHeaderSubtitle == null && panelGridHeader != null)
            {
                _lblGridHeaderSubtitle = new Label
                {
                    AutoSize = true,
                    ForeColor = UiTheme.AdminMuted,
                    Font = UiTheme.UiFont,
                    BackColor = panelGridHeader.BackColor,
                    Location = new Point(12, 28),
                    MaximumSize = new Size(960, 36)
                };
                panelGridHeader.Controls.Add(_lblGridHeaderSubtitle);
                panelGridHeader.Padding = new Padding(12, 8, 10, 8);
                panelGridHeader.Height = 52;
            }
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel is null || panel.Tag is "dash-border") return;
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
            if (card is null || card.Tag is "dash-stat") return;
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

            SyncPeriodDatesToPickers();
            UpdateDateRangeEnabled();
            TryLoadActiveReport();
        }

        private void BtnSalesTab_Click(object sender, EventArgs e) =>
            SwitchTab(TabSales);

        private void BtnInventoryTab_Click(object sender, EventArgs e) =>
            SwitchTab(TabInventory);

        private void BtnHistoryTab_Click(object sender, EventArgs e) =>
            SwitchTab(TabHistory);

        private void BtnWeekPeriod_Click(object sender, EventArgs e) =>
            SwitchPeriod(ReportPeriod.Week);

        private void BtnMonthPeriod_Click(object sender, EventArgs e) =>
            SwitchPeriod(ReportPeriod.Month);

        private void BtnYearPeriod_Click(object sender, EventArgs e) =>
            SwitchPeriod(ReportPeriod.Year);

        private void ChkDateRange_CheckedChanged(object sender, EventArgs e)
        {
            if (_suppressRangeEvents) return;
            UpdateDateRangeEnabled();
            UpdatePeriodStyles();
            if (_servicesReady && !IsDesignHost())
                LoadActiveReport();
        }

        private void DtpRange_ValueChanged(object sender, EventArgs e)
        {
            if (_suppressRangeEvents) return;
            if (chkDateRange == null || !chkDateRange.Checked) return;
            if (_servicesReady && !IsDesignHost())
                LoadActiveReport();
        }

        private void CmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_reportViewed)
                LoadActiveReport();
        }

        private void EnsureCustomerFilterLoaded()
        {
            if (!_servicesReady || cmbCustomer == null) return;
            if (cmbCustomer.DataSource != null) return;

            var allCustomers = _customers.GetAll();
            cmbCustomer.DataSource = null;
            cmbCustomer.Items.Clear();
            cmbCustomer.DisplayMember = "Name";
            cmbCustomer.ValueMember = "CustomerID";
            var list = new System.Collections.Generic.List<Customer>
            {
                new Customer { CustomerID = 0, Name = "All Customers" }
            };
            list.AddRange(allCustomers);
            cmbCustomer.DataSource = list;
            cmbCustomer.SelectedIndex = 0;
        }

        private int? GetSelectedCustomerId()
        {
            if (cmbCustomer?.SelectedItem is Customer customer)
                return customer.CustomerID;

            if (cmbCustomer?.SelectedValue != null
                && int.TryParse(cmbCustomer.SelectedValue.ToString(), out var id))
                return id;

            return null;
        }

        private void SwitchTab(string tab)
        {
            if (_activeTab == tab)
                return;

            _activeTab = tab;
            panelCustomerFilter.Visible = tab == TabHistory;
            UpdatePeriodFilterVisibility();
            UpdateTabStyles();
            UpdateStatTitlesForTab();

            if (_servicesReady && !IsDesignHost())
                LoadActiveReport();
            else
                ResetReportPreview();
        }

        private void SwitchPeriod(ReportPeriod period)
        {
            _activePeriod = period;
            _suppressRangeEvents = true;
            try
            {
                if (chkDateRange != null)
                    chkDateRange.Checked = false;
                SyncPeriodDatesToPickers();
                UpdateDateRangeEnabled();
            }
            finally
            {
                _suppressRangeEvents = false;
            }

            UpdatePeriodStyles();
            if (_servicesReady && !IsDesignHost())
                LoadActiveReport();
        }

        private bool UsesCustomDateRange =>
            chkDateRange != null && chkDateRange.Checked;

        private void SyncPeriodDatesToPickers()
        {
            if (dtpFrom == null || dtpTo == null) return;

            var (from, toExclusive) = ReportPeriodHelper.GetRange(_activePeriod);
            var toInclusive = toExclusive.AddDays(-1);
            if (toInclusive < from)
                toInclusive = from;

            var prior = _suppressRangeEvents;
            _suppressRangeEvents = true;
            try
            {
                dtpFrom.Value = from;
                dtpTo.Value = toInclusive;
            }
            finally
            {
                _suppressRangeEvents = prior;
            }
        }

        private void UpdateDateRangeEnabled()
        {
            var enabled = UsesCustomDateRange;
            if (dtpFrom != null) dtpFrom.Enabled = enabled;
            if (dtpTo != null) dtpTo.Enabled = enabled;
        }

        private bool TryGetCustomInclusiveRange(out DateTime fromInclusive, out DateTime toInclusive)
        {
            fromInclusive = dtpFrom?.Value.Date ?? DateTime.Today;
            toInclusive = dtpTo?.Value.Date ?? DateTime.Today;
            if (toInclusive < fromInclusive)
            {
                SmartMedMessageBox.Show(
                    "End date must be on or after the start date.",
                    "Reports",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void UpdateStatTitlesForTab()
        {
            if (lblStatTitleRevenue == null) return;

            if (_activeTab == TabInventory)
            {
                lblStatTitleRevenue.Text = "TOTAL ITEMS";
                lblStatTitleOrders.Text = "LOW STOCK";
                lblStatTitleLowStock.Text = "EXPIRED";
                lblStatTitleOutstanding.Text = "NEAR EXPIRY";
                return;
            }

            if (_activeTab == TabHistory)
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
            // Period chrome stays visible on all tabs (including inventory snapshot) for consistency.
            if (panelPeriodFilter != null)
                panelPeriodFilter.Visible = true;
        }

        private void UpdatePeriodStyles()
        {
            var highlightPreset = !UsesCustomDateRange;
            StylePeriod(btnWeekPeriod, highlightPreset && _activePeriod == ReportPeriod.Week);
            StylePeriod(btnMonthPeriod, highlightPreset && _activePeriod == ReportPeriod.Month);
            StylePeriod(btnYearPeriod, highlightPreset && _activePeriod == ReportPeriod.Year);
        }

        private static void StylePeriod(Button btn, bool active)
        {
            if (btn == null) return;
            if (active)
            {
                btn.BackColor = UiTheme.AdminTeal;
                btn.ForeColor = Color.White;
                btn.Font = UiTheme.UiFontBold;
                btn.FlatAppearance.BorderSize = 0;
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

        private string GetPeriodStatusText()
        {
            if (UsesCustomDateRange && dtpFrom != null && dtpTo != null)
                return $"{dtpFrom.Value:MMM dd, yyyy} – {dtpTo.Value:MMM dd, yyyy}";
            return ReportPeriodHelper.GetLabel(_activePeriod);
        }

        private string GetPeriodFileSuffix()
        {
            if (UsesCustomDateRange && dtpFrom != null && dtpTo != null)
                return $"{dtpFrom.Value:yyyyMMdd}-{dtpTo.Value:yyyyMMdd}";
            return ReportPeriodHelper.GetFileSuffix(_activePeriod);
        }

        private void UpdateTabStyles()
        {
            StyleTab(btnSalesTab, _activeTab == TabSales);
            StyleTab(btnInventoryTab, _activeTab == TabInventory);
            StyleTab(btnHistoryTab, _activeTab == TabHistory);
        }

        private static void StyleTab(Button btn, bool active) => UiTheme.StyleTabButton(btn, active);

        private void ResetReportPreview()
        {
            _reportViewed = false;
            _currentReportTable = null;
            _sourceReportTable = null;
            if (gridReport != null)
            {
                gridReport.DataSource = null;
                gridReport.Rows.Clear();
                gridReport.Columns.Clear();
            }
            if (lblFooterStatus != null)
                lblFooterStatus.Text = "Select report type and filters, then click View Report.";
            ClearSummaryStats();
            UpdatePreviewDisplay();
            UpdateExportButtons();
        }

        private void UpdatePreviewDisplay()
        {
            EnsurePreviewChrome();

            if (lblGridHeaderTitle != null)
                lblGridHeaderTitle.Text = _reportViewed ? GetReportTitle() : "Report Preview";

            if (_lblGridHeaderSubtitle != null)
            {
                _lblGridHeaderSubtitle.Text = _reportViewed ? GetReportSubtitle() : string.Empty;
                _lblGridHeaderSubtitle.Visible = _reportViewed;
            }

            if (_lblPreviewPlaceholder != null)
            {
                _lblPreviewPlaceholder.Visible = !_reportViewed;
                if (_reportViewed)
                    _lblPreviewPlaceholder.SendToBack();
                else
                    _lblPreviewPlaceholder.BringToFront();
            }

            if (gridReport != null)
            {
                gridReport.Visible = true;
                if (_reportViewed)
                    gridReport.BringToFront();
            }

            panelGridBody?.Refresh();
            panelGridOuter?.Refresh();
        }

        private void ClearSummaryStats()
        {
            if (lblTotalRevenue == null) return;
            lblTotalRevenue.Text = "-";
            lblTotalOrders.Text = "-";
            lblLowStock.Text = "-";
            lblOutstanding.Text = "-";
        }

        private void UpdateExportButtons()
        {
            if (btnExportCsv == null || btnExportPdf == null) return;
            var canExport = _reportViewed && _currentReportTable != null && _currentReportTable.Rows.Count > 0;
            btnExportCsv.Enabled = canExport;
            btnExportPdf.Enabled = canExport;
        }

        private void BtnViewReport_Click(object sender, EventArgs e)
        {
            EnsureRuntimeReady();
            if (!TryLoadActiveReport())
                return;

            ReportPreviewDialog.Show(
                FindForm(),
                _currentReportTable,
                GetReportTitle(),
                GetReportSubtitle());
        }

        private void LoadActiveReport() => TryLoadActiveReport();

        private bool TryLoadActiveReport()
        {
            if (!_servicesReady)
                return false;

            try
            {
                if (UsesCustomDateRange && !TryGetCustomInclusiveRange(out _, out _))
                    return false;

                if (_activeTab == TabSales)
                    LoadSalesReport();
                else if (_activeTab == TabInventory)
                    LoadInventoryReport();
                else
                    LoadHistoryReport();

                UpdateSummaryStats();
                _reportViewed = true;
                UpdatePreviewDisplay();
                UpdateExportButtons();
                return true;
            }
            catch (Exception ex)
            {
                ResetReportPreview();
                SmartMedMessageBox.Show(ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        private void LoadSalesReport()
        {
            _sourceReportTable = UsesCustomDateRange
                ? _reports.GetSalesReport(dtpFrom.Value.Date, dtpTo.Value.Date)
                : _reports.GetSalesReport(_activePeriod);
            _currentReportTable = ReportTableFormatter.FormatSalesReport(_sourceReportTable);
            BindReportGrid(_currentReportTable);
            lblFooterStatus.Text =
                $"Items: {_sourceReportTable.Rows.Count} | Completed sales only | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void BindReportGrid(DataTable table)
        {
            gridReport.DataSource = null;
            gridReport.Rows.Clear();
            gridReport.Columns.Clear();
            gridReport.AutoGenerateColumns = true;
            UiTheme.SetGridDataSource(gridReport, table ?? new DataTable());
            UiTheme.BeautifyGridHeaders(gridReport);
            gridReport.ClearSelection();
            gridReport.Refresh();
        }

        private void LoadInventoryReport()
        {
            _sourceReportTable = FetchStockReport();
            _currentReportTable = ReportTableFormatter.FormatStockReport(_sourceReportTable);
            BindReportGrid(_currentReportTable);

            var current = ReportTableFormatter.CountColumnValue(_sourceReportTable, "InventoryStatus", "Current");
            var lowStock = ReportTableFormatter.CountColumnValue(_sourceReportTable, "StockStatus", "Low Stock");
            var expired = ReportTableFormatter.CountColumnValue(_sourceReportTable, "ExpiryStatus", "Expired");
            var nearExpiry = ReportTableFormatter.CountColumnValue(_sourceReportTable, "ExpiryStatus", "Near Expiry");

            lblFooterStatus.Text =
                $"Items: {_sourceReportTable.Rows.Count} | Current: {current} | Low stock: {lowStock} | Expired: {expired} | Near expiry: {nearExpiry} | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void LoadHistoryReport()
        {
            EnsureCustomerFilterLoaded();

            // CustomerID 0 ("All Customers") loads history for every customer in the period.
            var customerId = GetSelectedCustomerId() ?? 0;

            _sourceReportTable = UsesCustomDateRange
                ? _reports.GetCustomerOrderHistory(customerId, dtpFrom.Value.Date, dtpTo.Value.Date)
                : _reports.GetCustomerOrderHistory(customerId, _activePeriod);
            _currentReportTable = ReportTableFormatter.FormatCustomerOrderHistory(_sourceReportTable);
            BindReportGrid(_currentReportTable);
            lblFooterStatus.Text =
                $"Items: {_sourceReportTable.Rows.Count} | Customer order history | {cmbCustomer.Text} | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt}";
        }

        private void UpdateSummaryStats()
        {
            if (!_servicesReady) return;

            if (_activeTab == TabInventory)
            {
                var stock = _sourceReportTable ?? FetchStockReport();
                lblTotalRevenue.Text = stock.Rows.Count.ToString("N0");
                lblTotalOrders.Text = ReportTableFormatter.CountColumnValue(stock, "StockStatus", "Low Stock").ToString("N0");
                lblLowStock.Text = ReportTableFormatter.CountColumnValue(stock, "ExpiryStatus", "Expired").ToString("N0");
                lblOutstanding.Text = ReportTableFormatter.CountColumnValue(stock, "ExpiryStatus", "Near Expiry").ToString("N0");
                return;
            }

            if (_activeTab == TabHistory)
            {
                lblTotalRevenue.Text = $"LKR {ReportTableFormatter.SumAmountColumn(_sourceReportTable):N2}";
                lblTotalOrders.Text = (_sourceReportTable?.Rows.Count ?? 0).ToString("N0");

                var stock = FetchStockReport();
                lblLowStock.Text = ReportTableFormatter.CountColumnValue(stock, "StockStatus", "Low Stock").ToString("N0");
                lblOutstanding.Text = $"LKR {GetOutstandingAmount():N2}";
                return;
            }

            var sales = _sourceReportTable ?? FetchSalesReport();
            lblTotalRevenue.Text = $"LKR {ReportTableFormatter.SumAmountColumn(sales):N2}";
            lblTotalOrders.Text = sales.Rows.Count.ToString("N0");

            var inventory = FetchStockReport();
            lblLowStock.Text = ReportTableFormatter.CountColumnValue(inventory, "StockStatus", "Low Stock").ToString("N0");
            lblOutstanding.Text = $"LKR {GetOutstandingAmount():N2}";
        }

        private DataTable FetchSalesReport() =>
            UsesCustomDateRange
                ? _reports.GetSalesReport(dtpFrom.Value.Date, dtpTo.Value.Date)
                : _reports.GetSalesReport(_activePeriod);

        private DataTable FetchStockReport() =>
            UsesCustomDateRange
                ? _reports.GetStockReport(dtpFrom.Value.Date, dtpTo.Value.Date)
                : _reports.GetStockReport(_activePeriod);

        private decimal GetOutstandingAmount() =>
            UsesCustomDateRange
                ? _reports.GetOutstandingAmount(dtpFrom.Value.Date, dtpTo.Value.Date)
                : _reports.GetOutstandingAmount(_activePeriod);

        private void GridReport_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (_activeTab != TabInventory || e.RowIndex < 0) return;
            if (UiTheme.IsSelectedRow(gridReport, e.RowIndex))
            {
                UiTheme.ApplySelectedRowCellStyle(e.CellStyle);
                return;
            }
            if (_currentReportTable == null || !_currentReportTable.Columns.Contains("Inventory Status")) return;
            if (e.RowIndex >= _currentReportTable.Rows.Count) return;

            var status = _currentReportTable.Rows[e.RowIndex]["Inventory Status"]?.ToString();
            if (string.IsNullOrEmpty(status)) return;

            if (status == "Expired")
            {
                e.CellStyle.BackColor = Color.FromArgb(254, 232, 232);
                e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
            }
            else if (status == "Near Expiry")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 248, 232);
                e.CellStyle.ForeColor = Color.FromArgb(140, 70, 0);
            }
            else if (status == "Low Stock")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 246, 238);
                e.CellStyle.ForeColor = Color.FromArgb(180, 90, 20);
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e) => ExportReport(isPdf: false);

        private void BtnExportPdf_Click(object sender, EventArgs e) => ExportReport(isPdf: true);

        private void ExportReport(bool isPdf)
        {
            if (!_reportViewed || _currentReportTable == null)
            {
                SmartMedMessageBox.Show("View the report before exporting.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (_currentReportTable.Rows.Count == 0)
            {
                SmartMedMessageBox.Show("Nothing to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var baseName = $"{GetExportBaseName()}_{GetPeriodFileSuffix()}_{DateTime.Now:yyyyMMdd}";

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
                        SmartMedMessageBox.Show("Report exported as PDF.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        SmartMedMessageBox.Show("Report exported as CSV.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetReportTitle()
        {
            if (_activeTab == TabSales) return "Sales Performance Report";
            if (_activeTab == TabInventory) return "Medicine Inventory Report";
            return "Customer Order History Report";
        }

        private string GetReportSubtitle()
        {
            if (_activeTab == TabInventory)
                return $"Period: {GetPeriodStatusText()} | Current inventory snapshot | Low stock, expired, and near-expiry items | Generated {DateTime.Now:MMM dd, yyyy hh:mm tt}";

            if (_activeTab == TabHistory)
                return $"Customer: {cmbCustomer?.Text} | Period: {GetPeriodStatusText()} | Generated {DateTime.Now:MMM dd, yyyy hh:mm tt}";

            return $"Period: {GetPeriodStatusText()} | Completed orders only | Generated {DateTime.Now:MMM dd, yyyy hh:mm tt}";
        }

        private string GetExportBaseName()
        {
            if (_activeTab == TabSales) return "sales-performance";
            if (_activeTab == TabInventory) return "medicine-inventory";
            return "customer-order-history";
        }
    }
}
