using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Data;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class ReportsForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ReportsForm()
            : base(AdminNavItem.Reports, "Generate Reports")
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignTime)
                LoadActiveReport();
        }

        public void RefreshReports() => LoadActiveReport();

        private enum ReportTab
        {
            Sales,
            Stock,
            History
        }

        private ReportService _reports;
        private CustomerRepository _customers;
        private ReportTab _activeTab = ReportTab.Sales;
        private bool _dataLoaded;

        private DataGridView gridReport;
        private ComboBox cmbCustomer;
        private Panel panelCustomerFilter;
        private Button btnSalesTab;
        private Button btnStockTab;
        private Button btnHistoryTab;
        private Label lblTotalRevenue;
        private Label lblTotalOrders;
        private Label lblLowStock;
        private Label lblOutstanding;
        private Label lblFooterStatus;
        private TableLayoutPanel _scrollRoot;

        private void BuildPageContent() {
            BuildContent();
            UpdateTabStyles();
            if (IsDesignHost())
                LoadDesignTimePreview();
            
        }

        private ReportService Reports
        {
            get
            {
                if (IsDesignHost()) return null;
                return _reports ?? (_reports = new ReportService());
            }
        }

        private CustomerRepository Customers
        {
            get
            {
                if (IsDesignHost()) return null;
                return _customers ?? (_customers = new CustomerRepository());
            }
        }

        private static bool IsDesignHost() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        private void LoadPageData(object sender, EventArgs e)
        {
            if (_dataLoaded) return;
            _dataLoaded = true;
            if (!IsDesignHost())
                LoadActiveReport();
        }

        private int GetScrollContentWidth()
        {
            var w = panelContent.ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            if (w < 200)
                w = 850;
            return w;
        }

        private void BuildContent()
        {
            panelContent.Controls.Clear();

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 6,
                MinimumSize = new Size(0, 900),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 320f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateTabBar(), 0, 2);
            _scrollRoot.Controls.Add(CreateCustomerFilter(), 0, 3);
            _scrollRoot.Controls.Add(CreateReportGridPanel(), 0, 4);
            _scrollRoot.Controls.Add(CreateFooterBar(), 0, 5);

            panelContent.Controls.Add(_scrollRoot);
            panelContent.Resize += (s, e) =>
            {
                if (_scrollRoot != null)
                    _scrollRoot.Width = GetScrollContentWidth();
            };
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
                Text = "Generate sales, stock, and customer order history reports.",
                Font = SystemFonts.DefaultFont,
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
            var btnExport = CreateToolbarButton("Export PDF");
            btnExport.Click += (s, e) => ShowComingSoon("Export PDF");
            var btnPrint = CreateToolbarButton("Print");
            btnPrint.Click += (s, e) => ShowComingSoon("Print");
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

            row.Controls.Add(CreateStatTile("Total Revenue", lblTotalRevenue, SystemColors.Highlight), 0, 0);
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
            valueLabel.Font = SystemFonts.DefaultFont;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(12, 34);
            valueLabel.AutoSize = true;

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = SystemFonts.DefaultFont,
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

            btnSalesTab = CreateTabButton("Sales Report", ReportTab.Sales);
            btnStockTab = CreateTabButton("Stock Report", ReportTab.Stock);
            btnHistoryTab = CreateTabButton("Customer Order History", ReportTab.History);

            btnSalesTab.Location = new Point(8, 8);
            btnStockTab.Location = new Point(132, 8);
            btnHistoryTab.Location = new Point(256, 8);

            bar.Controls.Add(btnHistoryTab);
            bar.Controls.Add(btnStockTab);
            bar.Controls.Add(btnSalesTab);
            UpdateTabStyles();
            return bar;
        }

        private Button CreateTabButton(string text, ReportTab tab)
        {
            var btn = new Button
            {
                Text = text,
                Width = tab == ReportTab.History ? 180 : 120,
                Height = 32,
                FlatStyle = FlatStyle.Flat
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.Tag = tab;
            btn.Click += (s, e) => SwitchTab(tab);
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
                Font = SystemFonts.DefaultFont,
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
            panelCustomerFilter.Visible = tab == ReportTab.History;
            UpdateTabStyles();
            if (!IsDesignHost())
                LoadActiveReport();
        }

        private void UpdateTabStyles()
        {
            StyleTab(btnSalesTab, _activeTab == ReportTab.Sales);
            StyleTab(btnStockTab, _activeTab == ReportTab.Stock);
            StyleTab(btnHistoryTab, _activeTab == ReportTab.History);
        }

        private static void StyleTab(Button btn, bool active)
        {
            if (active)
            {
                btn.ForeColor = SystemColors.Highlight;
                btn.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold);
            }
            else
            {
                btn.ForeColor = SystemColors.GrayText;
                btn.Font = SystemFonts.DefaultFont;
            }
        }

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
                if (_activeTab == ReportTab.Sales)
                    LoadSalesReport();
                else if (_activeTab == ReportTab.Stock)
                    LoadStockReport();
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
            var table = Reports.GetSalesReport();
            gridReport.DataSource = table;
            lblFooterStatus.Text = $"Items: {table.Rows.Count} | Server Connected | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
        }

        private void LoadStockReport()
        {
            var table = Reports.GetStockReport();
            gridReport.DataSource = table;
            lblFooterStatus.Text = $"Items: {table.Rows.Count} | Server Connected | {DateTime.Now:hh:mm tt | MMM dd, yyyy}";
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
            var table = Reports.GetCustomerOrderHistory(customerId);
            gridReport.DataSource = table;
            lblFooterStatus.Text = $"Items: {table.Rows.Count} | Customer: {cmbCustomer.Text} | {DateTime.Now:hh:mm tt}";
        }

        private void UpdateSummaryStats()
        {
            var sales = Reports.GetSalesReport();
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
    }
}
