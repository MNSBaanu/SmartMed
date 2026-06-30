using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ReportsForm : AdminPageControl
    {
        private readonly ReportService _reports = new ReportService();
        private readonly CustomerService _customers = new CustomerService();

        private ReportTab _activeTab = ReportTab.SalesPerformance;
        private ReportPeriod _activePeriod = ReportPeriod.Month;
        private bool _reportViewed;
        private DataTable _currentReportTable;

        public ReportsForm()
        {
            InitializeComponent();
        }

        protected override void BuildPageLayout() => BuildContent();

        protected override void DoRefreshPage()
        {
            SyncScrollRootWidth();
            if (_reportViewed)
                LoadActiveReport();
            else
                ResetReportPreview();
        }

        protected override void LoadDesignTimePreview()
        {
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

        private void BuildContent()
        {
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 7,
                MinimumSize = new Size(0, 900)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 320f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            root.Controls.Add(CreatePageHeader(), 0, 0);
            root.Controls.Add(CreateStatsRow(), 0, 1);
            root.Controls.Add(CreateTabBar(), 0, 2);
            root.Controls.Add(CreatePeriodFilter(), 0, 3);
            root.Controls.Add(CreateCustomerFilter(), 0, 4);
            root.Controls.Add(CreateReportGridPanel(), 0, 5);
            root.Controls.Add(CreateFooterBar(), 0, 6);
            WireScrollRoot(root);
        }

        private Panel CreatePageHeader() =>
            AdminUiHelpers.CreatePageHeader(
                "Generate Reports",
                "Completed sales, stock alerts, and customer order history for quick decisions.",
                actions =>
                {
                    btnViewReport = AdminUiHelpers.CreateWinButton("View Report", true, 110);
                    btnViewReport.Click += BtnViewReport_Click;
                    btnExportCsv = AdminUiHelpers.CreateWinButton("Export CSV", false, 100);
                    btnExportCsv.Click += BtnExportCsv_Click;
                    btnExportPdf = AdminUiHelpers.CreateWinButton("Export PDF", false, 100);
                    btnExportPdf.Click += BtnExportPdf_Click;
                    actions.Controls.Add(btnViewReport);
                    actions.Controls.Add(btnExportCsv);
                    actions.Controls.Add(btnExportPdf);
                    UpdateExportButtons();
                });

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

            lblTotalRevenue = new Label();
            lblTotalOrders = new Label();
            lblLowStock = new Label();
            lblOutstanding = new Label();

            var card0 = AdminUiHelpers.CreateStatCard("Completed Revenue", lblTotalRevenue, UiTheme.AdminTeal);
            lblStatTitleRevenue = GetStatTitleLabel(card0, lblTotalRevenue);
            statsRow.Controls.Add(card0, 0, 0);

            var card1 = AdminUiHelpers.CreateStatCard("Completed Orders", lblTotalOrders, Color.FromArgb(59, 130, 246));
            lblStatTitleOrders = GetStatTitleLabel(card1, lblTotalOrders);
            statsRow.Controls.Add(card1, 1, 0);

            var card2 = AdminUiHelpers.CreateStatCard("Low Stock Items", lblLowStock, UiTheme.Danger);
            lblStatTitleLowStock = GetStatTitleLabel(card2, lblLowStock);
            statsRow.Controls.Add(card2, 2, 0);

            var card3 = AdminUiHelpers.CreateStatCard("Outstanding", lblOutstanding, Color.FromArgb(16, 185, 129));
            lblStatTitleOutstanding = GetStatTitleLabel(card3, lblOutstanding);
            statsRow.Controls.Add(card3, 3, 0);

            return statsRow;
        }

        private static Label GetStatTitleLabel(Panel card, Label valueLabel)
        {
            foreach (Control control in card.Controls)
            {
                if (control is Label lbl && !ReferenceEquals(lbl, valueLabel))
                    return lbl;
            }
            return null;
        }

        private Panel CreateTabBar()
        {
            var bar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = UiTheme.AdminSidebar,
                Padding = new Padding(8, 8, 8, 0),
                Margin = new Padding(0, 0, 0, 8)
            };

            var tabs = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true,
                BackColor = UiTheme.AdminSidebar
            };

            btnSalesTab = CreateTabButton("Sales Performance", ReportTab.SalesPerformance);
            btnInventoryTab = CreateTabButton("Medicine Inventory", ReportTab.MedicineInventory);
            btnHistoryTab = CreateTabButton("Customer Order History", ReportTab.CustomerOrderHistory);

            tabs.Controls.Add(btnSalesTab);
            tabs.Controls.Add(btnInventoryTab);
            tabs.Controls.Add(btnHistoryTab);
            bar.Controls.Add(tabs);
            UpdateTabStyles();
            return bar;
        }

        private Button CreateTabButton(string text, ReportTab tab)
        {
            var btn = new Button
            {
                Text = text,
                AutoSize = true,
                Height = 32,
                MinimumSize = new Size(120, 32),
                Padding = new Padding(12, 0, 12, 0),
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 0, 8, 0),
                BackColor = UiTheme.AdminSidebar
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Tag = tab;
            btn.Click += (s, e) => SwitchTab(tab);
            return btn;
        }

        private Panel CreatePeriodFilter()
        {
            panelPeriodFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = UiTheme.AdminSurface
            };

            var lbl = new Label
            {
                Text = "Period:",
                AutoSize = true,
                Location = new Point(0, 12),
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            };

            var tabs = new FlowLayoutPanel
            {
                Location = new Point(56, 6),
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = UiTheme.AdminSurface
            };

            btnWeekPeriod = CreatePeriodButton("Week", ReportPeriod.Week);
            btnMonthPeriod = CreatePeriodButton("Month", ReportPeriod.Month);
            btnYearPeriod = CreatePeriodButton("Year", ReportPeriod.Year);
            tabs.Controls.Add(btnWeekPeriod);
            tabs.Controls.Add(btnMonthPeriod);
            tabs.Controls.Add(btnYearPeriod);

            panelPeriodFilter.Controls.Add(tabs);
            panelPeriodFilter.Controls.Add(lbl);
            UpdatePeriodFilterVisibility();
            UpdatePeriodStyles();
            return panelPeriodFilter;
        }

        private Button CreatePeriodButton(string text, ReportPeriod period)
        {
            var btn = new Button
            {
                Text = text,
                Width = 64,
                Height = 28,
                FlatStyle = FlatStyle.Flat,
                Margin = new Padding(0, 0, 6, 0),
                Font = UiTheme.UiFont
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Tag = period;
            btn.Click += (s, e) => SwitchPeriod(period);
            return btn;
        }

        private Panel CreateCustomerFilter()
        {
            panelCustomerFilter = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Visible = false,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = UiTheme.AdminSurface
            };

            var lbl = new Label
            {
                Text = "Customer:",
                AutoSize = true,
                Location = new Point(0, 12),
                BackColor = UiTheme.AdminSurface
            };

            cmbCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 320,
                Location = new Point(72, 8)
            };
            UiTheme.StyleComboBox(cmbCustomer);
            cmbCustomer.SelectedIndexChanged += (s, e) => ResetReportPreview();

            panelCustomerFilter.Controls.Add(cmbCustomer);
            panelCustomerFilter.Controls.Add(lbl);
            return panelCustomerFilter;
        }

        private Panel CreateReportGridPanel()
        {
            gridReport = new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ScrollBars = ScrollBars.Both,
                MinimumSize = new Size(0, 280)
            };
            UiTheme.ApplyClinicalGrid(gridReport);
            gridReport.CellFormatting += GridReport_CellFormatting;

            var outer = AdminUiHelpers.CreateSectionPanel("Report Preview", gridReport);
            outer.Margin = new Padding(0, 0, 0, 8);
            return outer;
        }

        private Panel CreateFooterBar()
        {
            lblFooterStatus = new Label
            {
                Dock = DockStyle.Fill,
                Height = 28,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(12, 0, 0, 0),
                BackColor = UiTheme.AdminSidebar,
                ForeColor = UiTheme.AdminMuted,
                Font = UiTheme.UiFont,
                Text = "Select report type and filters, then click View Report."
            };
            lblFooterStatus.Paint += (s, e) =>
            {
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawLine(pen, 0, 0, lblFooterStatus.Width, 0);
            };

            var wrap = new Panel { Dock = DockStyle.Top, Height = 28, BackColor = UiTheme.AdminSurface };
            wrap.Controls.Add(lblFooterStatus);
            return wrap;
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
