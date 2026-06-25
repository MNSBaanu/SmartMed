using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class ReportsForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ReportsForm()
            : base(AdminNavItem.Reports, "Generate Reports")
        {
            InitializeComponent();
        }

        internal ReportsForm(bool embedded)
            : base(AdminNavItem.Reports, "Generate Reports", embedded)
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            UpdateTabStyles();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                LoadActiveReport();
        }

        public void RefreshReports() => LoadActiveReport();

        private ReportService _reports;
        private CustomerService _customers;
        private ReportTab _activeTab = ReportTab.SalesPerformance;
        private ReportPeriod _activePeriod = ReportPeriod.Month;

        private DataGridView gridReport;
        private ComboBox cmbCustomer;
        private Panel panelCustomerFilter;
        private Panel panelPeriodFilter;
        private Button btnWeekPeriod;
        private Button btnMonthPeriod;
        private Button btnYearPeriod;
        private Button btnSalesTab;
        private Button btnInventoryTab;
        private Button btnHistoryTab;
        private Label lblTotalRevenue;
        private Label lblTotalOrders;
        private Label lblLowStock;
        private Label lblOutstanding;
        private Label lblFooterStatus;
        private TableLayoutPanel _scrollRoot;

        private ReportService Reports => GetRuntimeService(ref _reports);

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
                RowCount = 7,
                MinimumSize = new Size(0, 900),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 320f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateTabBar(), 0, 2);
            _scrollRoot.Controls.Add(CreatePeriodFilter(), 0, 3);
            _scrollRoot.Controls.Add(CreateCustomerFilter(), 0, 4);
            _scrollRoot.Controls.Add(CreateReportGridPanel(), 0, 5);
            _scrollRoot.Controls.Add(CreateFooterBar(), 0, 6);
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
                Text = "Sales performance, medicine inventory, and customer order history reports.",
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
            var btnExport = CreateToolbarButton("Export CSV");
            btnExport.Click += BtnExport_Click;
            var btnPrint = CreateToolbarButton("Print");
            btnPrint.Click += BtnPrint_Click;
            actions.Controls.Add(btnExport);
            actions.Controls.Add(btnPrint);

            header.Controls.Add(actions);
            header.Controls.Add(titleBlock);
            return header;
        }

        private static Button CreateToolbarButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Height = 32,
                Width = 120,
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

            lblTotalRevenue = new Label();
            lblTotalOrders = new Label();
            lblLowStock = new Label();
            lblOutstanding = new Label();

            row.Controls.Add(CreateStatTile("Total Revenue", lblTotalRevenue, UiTheme.GridHeaderText), 0, 0);
            row.Controls.Add(CreateStatTile("Total Orders", lblTotalOrders, SystemColors.ControlText), 1, 0);
            row.Controls.Add(CreateStatTile("Low Stock Items", lblLowStock, Color.Red), 2, 0);
            row.Controls.Add(CreateStatTile("Outstanding", lblOutstanding, SystemColors.ControlText), 3, 0);

            wrap.Controls.Add(row);
            return wrap;
        }

        private Panel CreateStatTile(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(12),
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
            valueLabel.Location = new Point(12, 34);
            valueLabel.AutoSize = true;

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                Location = new Point(12, 14),
                AutoSize = true
            });
            card.Controls.Add(valueLabel);
            return card;
        }

        private Panel CreateTabBar()
        {
            var bar = new Panel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = SystemColors.Control,
                Padding = new Padding(8, 8, 8, 0),
                Margin = new Padding(0, 0, 0, 8)
            };

            var tabs = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                AutoSize = true
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
                Margin = new Padding(0, 0, 8, 0)
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
                Margin = new Padding(0, 0, 0, 8)
            };

            var lbl = new Label
            {
                Text = "Period:",
                AutoSize = true,
                Location = new Point(0, 12),
                Font = UiTheme.UiFont
            };

            var tabs = new FlowLayoutPanel
            {
                Location = new Point(56, 6),
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
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
                Margin = new Padding(0, 0, 0, 8)
            };

            var lbl = new Label
            {
                Text = "Customer:",
                AutoSize = true,
                Location = new Point(0, 12)
            };

            cmbCustomer = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 320,
                Location = new Point(72, 8)
            };
            cmbCustomer.SelectedIndexChanged += (s, e) => LoadActiveReport();

            panelCustomerFilter.Controls.Add(cmbCustomer);
            panelCustomerFilter.Controls.Add(lbl);
            return panelCustomerFilter;
        }

        private Panel CreateReportGridPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 8)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            gridReport = new DataGridView
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
                ScrollBars = ScrollBars.Both
            };

            UiTheme.ApplyGrid(gridReport);
            outer.Controls.Add(gridReport);
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
                BackColor = SystemColors.ControlLight,
                ForeColor = SystemColors.GrayText,
                Font = UiTheme.UiFont,
                Text = "Items: 0 | Server Connected"
            };
            lblFooterStatus.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawLine(pen, 0, 0, lblFooterStatus.Width, 0);
            };

            var wrap = new Panel { Dock = DockStyle.Top, Height = 28 };
            wrap.Controls.Add(lblFooterStatus);
            return wrap;
        }

        private void SwitchTab(ReportTab tab)
        {
            _activeTab = tab;
            panelCustomerFilter.Visible = tab == ReportTab.CustomerOrderHistory;
            UpdatePeriodFilterVisibility();
            UpdateTabStyles();
            if (!IsDesignHost())
                LoadActiveReport();
        }

        private void SwitchPeriod(ReportPeriod period)
        {
            _activePeriod = period;
            UpdatePeriodStyles();
            if (!IsDesignHost())
                LoadActiveReport();
        }

        private void UpdatePeriodFilterVisibility()
        {
            if (panelPeriodFilter == null) return;
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
                btn.BackColor = UiTheme.Primary;
                btn.ForeColor = Color.White;
                btn.Font = UiTheme.UiFontBold;
            }
            else
            {
                btn.BackColor = SystemColors.Control;
                btn.ForeColor = SystemColors.ControlText;
                btn.Font = UiTheme.UiFont;
            }
        }

        private string GetPeriodStatusText() =>
            ReportPeriodHelper.GetLabel(_activePeriod);

        private void UpdateTabStyles()
        {
            StyleTab(btnSalesTab, _activeTab == ReportTab.SalesPerformance);
            StyleTab(btnInventoryTab, _activeTab == ReportTab.MedicineInventory);
            StyleTab(btnHistoryTab, _activeTab == ReportTab.CustomerOrderHistory);
        }

        private static void StyleTab(Button btn, bool active) => UiTheme.StyleTabButton(btn, active);

        private void LoadDesignTimePreview()
        {
            lblTotalRevenue.Text = "LKR 1,245,300";
            lblTotalOrders.Text = "142";
            lblLowStock.Text = "3";
            lblOutstanding.Text = "LKR 45,200";

            gridReport.DataSource = new[]
            {
                new { OrderID = 1, Customer = "Margaret Sullivan", OrderDate = "Oct 24, 2023", Status = "Delivered", TotalAmount = "LKR 124.50" },
                new { OrderID = 2, Customer = "Jonathan Wick", OrderDate = "Oct 24, 2023", Status = "Pending", TotalAmount = "LKR 45.00" }
            };
            lblFooterStatus.Text = "Items: 2 | Server Connected | " + DateTime.Now.ToString("hh:mm tt | MMM dd, yyyy");
        }

        private void LoadActiveReport()
        {
            if (IsDesignHost() || Reports == null) return;

            try
            {
                if (_activeTab == ReportTab.SalesPerformance)
                    LoadSalesReport();
                else if (_activeTab == ReportTab.MedicineInventory)
                    LoadInventoryReport();
                else
                    LoadHistoryReport();

                UpdateSummaryStats();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Report Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadSalesReport()
        {
            var table = Reports.GetSalesReport(_activePeriod);
            gridReport.DataSource = table;
            lblFooterStatus.Text =
                $"Items: {table.Rows.Count} | Sales performance | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void LoadInventoryReport()
        {
            var table = Reports.GetStockReport();
            gridReport.DataSource = table;
            lblFooterStatus.Text =
                $"Items: {table.Rows.Count} | Medicine inventory (current stock) | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void LoadHistoryReport()
        {
            if (Customers == null) return;

            var allCustomers = Customers.GetAll();
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
                gridReport.DataSource = null;
                lblFooterStatus.Text = "No customers available | Server Connected";
                return;
            }

            var customerId = Convert.ToInt32(cmbCustomer.SelectedValue);
            var table = Reports.GetCustomerOrderHistory(customerId, _activePeriod);
            gridReport.DataSource = table;
            lblFooterStatus.Text =
                $"Items: {table.Rows.Count} | Customer order history | {cmbCustomer.Text} | {GetPeriodStatusText()} | {DateTime.Now:hh:mm tt}";
        }

        private void UpdateSummaryStats()
        {
            var sales = Reports.GetSalesReport(_activePeriod);
            decimal totalRevenue = 0;
            decimal outstanding = 0;
            foreach (DataRow row in sales.Rows)
            {
                var amount = Convert.ToDecimal(row["TotalAmount"]);
                totalRevenue += amount;
                var status = row["Status"]?.ToString() ?? string.Empty;
                if (status != "Delivered")
                    outstanding += amount;
            }

            lblTotalRevenue.Text = $"LKR {totalRevenue:N2}";
            lblTotalOrders.Text = sales.Rows.Count.ToString("N0");

            var stock = Reports.GetStockReport();
            var lowStock = 0;
            foreach (DataRow row in stock.Rows)
            {
                if (Convert.ToInt32(row["StockQuantity"]) <= 20)
                    lowStock++;
            }
            lblLowStock.Text = lowStock.ToString("N0");
            lblOutstanding.Text = $"LKR {outstanding:N2}";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Reports == null || gridReport.DataSource == null) return;
            try
            {
                var table = gridReport.DataSource as DataTable;
                if (table == null)
                {
                    MessageBox.Show("Nothing to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                var periodSuffix = _activeTab == ReportTab.MedicineInventory
                    ? string.Empty
                    : $"_{ReportPeriodHelper.GetFileSuffix(_activePeriod)}";
                using (var dialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"{GetExportBaseName()}{periodSuffix}_{DateTime.Now:yyyyMMdd}.csv"
                })
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    Reports.ExportActiveReportToCsv(table, dialog.FileName);
                    MessageBox.Show("Report exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (gridReport.Rows.Count == 0)
            {
                MessageBox.Show("No report data to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                ExportHelper.PrintGrid(gridReport, "SmartMed Report");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private string GetExportBaseName()
        {
            if (_activeTab == ReportTab.SalesPerformance) return "sales-performance";
            if (_activeTab == ReportTab.MedicineInventory) return "medicine-inventory";
            return "customer-order-history";
        }
    }
}
