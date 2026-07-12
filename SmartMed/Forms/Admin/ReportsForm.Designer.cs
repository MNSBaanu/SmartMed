namespace SmartMed.UI
{
    partial class ReportsForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelScrollHost = new System.Windows.Forms.Panel();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.flowHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnViewReport = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.tableStatsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatRevenue = new System.Windows.Forms.Panel();
            this.lblTotalRevenue = new System.Windows.Forms.Label();
            this.lblStatTitleRevenue = new System.Windows.Forms.Label();
            this.panelStatOrders = new System.Windows.Forms.Panel();
            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblStatTitleOrders = new System.Windows.Forms.Label();
            this.panelStatLowStock = new System.Windows.Forms.Panel();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.lblStatTitleLowStock = new System.Windows.Forms.Label();
            this.panelStatOutstanding = new System.Windows.Forms.Panel();
            this.lblOutstanding = new System.Windows.Forms.Label();
            this.lblStatTitleOutstanding = new System.Windows.Forms.Label();
            this.panelTabBar = new System.Windows.Forms.Panel();
            this.flowTabs = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSalesTab = new System.Windows.Forms.Button();
            this.btnInventoryTab = new System.Windows.Forms.Button();
            this.btnHistoryTab = new System.Windows.Forms.Button();
            this.panelPeriodFilter = new System.Windows.Forms.Panel();
            this.flowPeriodTabs = new System.Windows.Forms.FlowLayoutPanel();
            this.btnWeekPeriod = new System.Windows.Forms.Button();
            this.btnMonthPeriod = new System.Windows.Forms.Button();
            this.btnYearPeriod = new System.Windows.Forms.Button();
            this.lblPeriodLabel = new System.Windows.Forms.Label();
            this.panelCustomerFilter = new System.Windows.Forms.Panel();
            this.cmbCustomer = new System.Windows.Forms.ComboBox();
            this.lblCustomerLabel = new System.Windows.Forms.Label();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridInner = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.gridReport = new System.Windows.Forms.DataGridView();
            this.colOrderRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrderDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalAmount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.lblGridHeaderTitle = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblFooterStatus = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowHeaderActions.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatRevenue.SuspendLayout();
            this.panelStatOrders.SuspendLayout();
            this.panelStatLowStock.SuspendLayout();
            this.panelStatOutstanding.SuspendLayout();
            this.panelTabBar.SuspendLayout();
            this.flowTabs.SuspendLayout();
            this.panelPeriodFilter.SuspendLayout();
            this.flowPeriodTabs.SuspendLayout();
            this.panelCustomerFilter.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridInner.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).BeginInit();
            this.panelGridHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelScrollHost
            // 
            this.panelScrollHost.AutoScroll = true;
            this.panelScrollHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelScrollHost.Controls.Add(this.tableLayoutRoot);
            this.panelScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScrollHost.Location = new System.Drawing.Point(0, 0);
            this.panelScrollHost.Name = "panelScrollHost";
            this.panelScrollHost.Padding = new System.Windows.Forms.Padding(24);
            this.panelScrollHost.Size = new System.Drawing.Size(1060, 720);
            this.panelScrollHost.TabIndex = 0;
            // 
            // tableLayoutRoot
            // 
            this.tableLayoutRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.tableStatsRow, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelTabBar, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.panelPeriodFilter, 0, 3);
            this.tableLayoutRoot.Controls.Add(this.panelCustomerFilter, 0, 4);
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 5);
            this.tableLayoutRoot.Controls.Add(this.panelFooter, 0, 6);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 7;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 900);
            this.tableLayoutRoot.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.flowHeaderActions);
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;
            // 
            // flowHeaderActions
            // 
            this.flowHeaderActions.AutoSize = true;
            this.flowHeaderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowHeaderActions.Controls.Add(this.btnViewReport);
            this.flowHeaderActions.Controls.Add(this.btnExportCsv);
            this.flowHeaderActions.Controls.Add(this.btnExportPdf);
            this.flowHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowHeaderActions.Location = new System.Drawing.Point(680, 0);
            this.flowHeaderActions.Name = "flowHeaderActions";
            this.flowHeaderActions.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flowHeaderActions.Size = new System.Drawing.Size(332, 76);
            this.flowHeaderActions.TabIndex = 2;
            this.flowHeaderActions.WrapContents = false;
            // 
            // btnViewReport
            // 
            this.btnViewReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnViewReport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewReport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnViewReport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(109)))), ((int)(((byte)(100)))));
            this.btnViewReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewReport.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnViewReport.ForeColor = System.Drawing.Color.White;
            this.btnViewReport.Location = new System.Drawing.Point(0, 16);
            this.btnViewReport.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnViewReport.Name = "btnViewReport";
            this.btnViewReport.Size = new System.Drawing.Size(110, 30);
            this.btnViewReport.TabIndex = 0;
            this.btnViewReport.Text = "View Report";
            this.btnViewReport.UseVisualStyleBackColor = false;
            this.btnViewReport.Click += new System.EventHandler(this.btnViewReport_Click);
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCsv.Enabled = false;
            this.btnExportCsv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExportCsv.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExportCsv.Location = new System.Drawing.Point(120, 16);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(100, 30);
            this.btnExportCsv.TabIndex = 1;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            this.btnExportCsv.Click += new System.EventHandler(this.btnExportCsv_Click);
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExportPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportPdf.Enabled = false;
            this.btnExportPdf.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExportPdf.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPdf.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExportPdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExportPdf.Location = new System.Drawing.Point(230, 16);
            this.btnExportPdf.Margin = new System.Windows.Forms.Padding(0);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(100, 30);
            this.btnExportPdf.TabIndex = 2;
            this.btnExportPdf.Text = "Export PDF";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            this.btnExportPdf.Click += new System.EventHandler(this.btnExportPdf_Click);
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(520, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Completed sales, stock alerts, and customer order history for quick decisions.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(96, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Reports";
            // 
            // tableStatsRow
            // 
            this.tableStatsRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableStatsRow.ColumnCount = 4;
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.Controls.Add(this.panelStatRevenue, 0, 0);
            this.tableStatsRow.Controls.Add(this.panelStatOrders, 1, 0);
            this.tableStatsRow.Controls.Add(this.panelStatLowStock, 2, 0);
            this.tableStatsRow.Controls.Add(this.panelStatOutstanding, 3, 0);
            this.tableStatsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStatsRow.Location = new System.Drawing.Point(0, 96);
            this.tableStatsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24);
            this.tableStatsRow.Name = "tableStatsRow";
            this.tableStatsRow.RowCount = 1;
            this.tableStatsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableStatsRow.Size = new System.Drawing.Size(1012, 108);
            this.tableStatsRow.TabIndex = 1;
            // 
            // panelStatRevenue
            // 
            this.panelStatRevenue.BackColor = System.Drawing.Color.White;
            this.panelStatRevenue.Controls.Add(this.lblTotalRevenue);
            this.panelStatRevenue.Controls.Add(this.lblStatTitleRevenue);
            this.panelStatRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatRevenue.Location = new System.Drawing.Point(0, 0);
            this.panelStatRevenue.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatRevenue.Name = "panelStatRevenue";
            this.panelStatRevenue.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatRevenue.Size = new System.Drawing.Size(239, 108);
            this.panelStatRevenue.TabIndex = 0;
            // 
            // lblTotalRevenue
            // 
            this.lblTotalRevenue.BackColor = System.Drawing.Color.White;
            this.lblTotalRevenue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalRevenue.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblTotalRevenue.Location = new System.Drawing.Point(16, 30);
            this.lblTotalRevenue.Name = "lblTotalRevenue";
            this.lblTotalRevenue.Size = new System.Drawing.Size(209, 64);
            this.lblTotalRevenue.TabIndex = 1;
            this.lblTotalRevenue.Text = "-";
            this.lblTotalRevenue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTitleRevenue
            // 
            this.lblStatTitleRevenue.BackColor = System.Drawing.Color.White;
            this.lblStatTitleRevenue.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTitleRevenue.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTitleRevenue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTitleRevenue.Location = new System.Drawing.Point(16, 14);
            this.lblStatTitleRevenue.Name = "lblStatTitleRevenue";
            this.lblStatTitleRevenue.Size = new System.Drawing.Size(209, 16);
            this.lblStatTitleRevenue.TabIndex = 0;
            this.lblStatTitleRevenue.Text = "COMPLETED REVENUE";
            // 
            // panelStatOrders
            // 
            this.panelStatOrders.BackColor = System.Drawing.Color.White;
            this.panelStatOrders.Controls.Add(this.lblTotalOrders);
            this.panelStatOrders.Controls.Add(this.lblStatTitleOrders);
            this.panelStatOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatOrders.Location = new System.Drawing.Point(253, 0);
            this.panelStatOrders.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatOrders.Name = "panelStatOrders";
            this.panelStatOrders.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatOrders.Size = new System.Drawing.Size(239, 108);
            this.panelStatOrders.TabIndex = 1;
            // 
            // lblTotalOrders
            // 
            this.lblTotalOrders.BackColor = System.Drawing.Color.White;
            this.lblTotalOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalOrders.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblTotalOrders.Location = new System.Drawing.Point(16, 30);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Size = new System.Drawing.Size(209, 64);
            this.lblTotalOrders.TabIndex = 1;
            this.lblTotalOrders.Text = "-";
            this.lblTotalOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTitleOrders
            // 
            this.lblStatTitleOrders.BackColor = System.Drawing.Color.White;
            this.lblStatTitleOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTitleOrders.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTitleOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTitleOrders.Location = new System.Drawing.Point(16, 14);
            this.lblStatTitleOrders.Name = "lblStatTitleOrders";
            this.lblStatTitleOrders.Size = new System.Drawing.Size(209, 16);
            this.lblStatTitleOrders.TabIndex = 0;
            this.lblStatTitleOrders.Text = "COMPLETED ORDERS";
            // 
            // panelStatLowStock
            // 
            this.panelStatLowStock.BackColor = System.Drawing.Color.White;
            this.panelStatLowStock.Controls.Add(this.lblLowStock);
            this.panelStatLowStock.Controls.Add(this.lblStatTitleLowStock);
            this.panelStatLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatLowStock.Location = new System.Drawing.Point(506, 0);
            this.panelStatLowStock.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatLowStock.Name = "panelStatLowStock";
            this.panelStatLowStock.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatLowStock.Size = new System.Drawing.Size(239, 108);
            this.panelStatLowStock.TabIndex = 2;
            // 
            // lblLowStock
            // 
            this.lblLowStock.BackColor = System.Drawing.Color.White;
            this.lblLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowStock.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblLowStock.Location = new System.Drawing.Point(16, 30);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(209, 64);
            this.lblLowStock.TabIndex = 1;
            this.lblLowStock.Text = "-";
            this.lblLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTitleLowStock
            // 
            this.lblStatTitleLowStock.BackColor = System.Drawing.Color.White;
            this.lblStatTitleLowStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTitleLowStock.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTitleLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTitleLowStock.Location = new System.Drawing.Point(16, 14);
            this.lblStatTitleLowStock.Name = "lblStatTitleLowStock";
            this.lblStatTitleLowStock.Size = new System.Drawing.Size(209, 16);
            this.lblStatTitleLowStock.TabIndex = 0;
            this.lblStatTitleLowStock.Text = "LOW STOCK ITEMS";
            // 
            // panelStatOutstanding
            // 
            this.panelStatOutstanding.BackColor = System.Drawing.Color.White;
            this.panelStatOutstanding.Controls.Add(this.lblOutstanding);
            this.panelStatOutstanding.Controls.Add(this.lblStatTitleOutstanding);
            this.panelStatOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatOutstanding.Location = new System.Drawing.Point(759, 0);
            this.panelStatOutstanding.Name = "panelStatOutstanding";
            this.panelStatOutstanding.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatOutstanding.Size = new System.Drawing.Size(253, 108);
            this.panelStatOutstanding.TabIndex = 3;
            // 
            // lblOutstanding
            // 
            this.lblOutstanding.BackColor = System.Drawing.Color.White;
            this.lblOutstanding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOutstanding.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblOutstanding.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblOutstanding.Location = new System.Drawing.Point(16, 30);
            this.lblOutstanding.Name = "lblOutstanding";
            this.lblOutstanding.Size = new System.Drawing.Size(223, 64);
            this.lblOutstanding.TabIndex = 1;
            this.lblOutstanding.Text = "-";
            this.lblOutstanding.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTitleOutstanding
            // 
            this.lblStatTitleOutstanding.BackColor = System.Drawing.Color.White;
            this.lblStatTitleOutstanding.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTitleOutstanding.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTitleOutstanding.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTitleOutstanding.Location = new System.Drawing.Point(16, 14);
            this.lblStatTitleOutstanding.Name = "lblStatTitleOutstanding";
            this.lblStatTitleOutstanding.Size = new System.Drawing.Size(223, 16);
            this.lblStatTitleOutstanding.TabIndex = 0;
            this.lblStatTitleOutstanding.Text = "OUTSTANDING";
            // 
            // panelTabBar
            // 
            this.panelTabBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelTabBar.Controls.Add(this.flowTabs);
            this.panelTabBar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTabBar.Location = new System.Drawing.Point(0, 228);
            this.panelTabBar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelTabBar.Name = "panelTabBar";
            this.panelTabBar.Padding = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.panelTabBar.Size = new System.Drawing.Size(1012, 48);
            this.panelTabBar.TabIndex = 2;
            // 
            // flowTabs
            // 
            this.flowTabs.AutoSize = true;
            this.flowTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.flowTabs.Controls.Add(this.btnSalesTab);
            this.flowTabs.Controls.Add(this.btnInventoryTab);
            this.flowTabs.Controls.Add(this.btnHistoryTab);
            this.flowTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowTabs.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowTabs.Location = new System.Drawing.Point(8, 8);
            this.flowTabs.Name = "flowTabs";
            this.flowTabs.Size = new System.Drawing.Size(996, 32);
            this.flowTabs.TabIndex = 0;
            this.flowTabs.WrapContents = false;
            // 
            // btnSalesTab
            // 
            this.btnSalesTab.AutoSize = true;
            this.btnSalesTab.BackColor = System.Drawing.Color.White;
            this.btnSalesTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSalesTab.FlatAppearance.BorderSize = 0;
            this.btnSalesTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalesTab.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalesTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnSalesTab.Location = new System.Drawing.Point(0, 0);
            this.btnSalesTab.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnSalesTab.MinimumSize = new System.Drawing.Size(120, 32);
            this.btnSalesTab.Name = "btnSalesTab";
            this.btnSalesTab.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btnSalesTab.Size = new System.Drawing.Size(152, 32);
            this.btnSalesTab.TabIndex = 0;
            this.btnSalesTab.Text = "Sales Performance";
            this.btnSalesTab.UseVisualStyleBackColor = false;
            this.btnSalesTab.Click += new System.EventHandler(this.btnSalesTab_Click);
            // 
            // btnInventoryTab
            // 
            this.btnInventoryTab.AutoSize = true;
            this.btnInventoryTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnInventoryTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInventoryTab.FlatAppearance.BorderSize = 0;
            this.btnInventoryTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventoryTab.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnInventoryTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.btnInventoryTab.Location = new System.Drawing.Point(160, 0);
            this.btnInventoryTab.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnInventoryTab.MinimumSize = new System.Drawing.Size(120, 32);
            this.btnInventoryTab.Name = "btnInventoryTab";
            this.btnInventoryTab.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btnInventoryTab.Size = new System.Drawing.Size(156, 32);
            this.btnInventoryTab.TabIndex = 1;
            this.btnInventoryTab.Text = "Medicine Inventory";
            this.btnInventoryTab.UseVisualStyleBackColor = false;
            this.btnInventoryTab.Click += new System.EventHandler(this.btnInventoryTab_Click);
            // 
            // btnHistoryTab
            // 
            this.btnHistoryTab.AutoSize = true;
            this.btnHistoryTab.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnHistoryTab.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHistoryTab.FlatAppearance.BorderSize = 0;
            this.btnHistoryTab.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistoryTab.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnHistoryTab.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.btnHistoryTab.Location = new System.Drawing.Point(324, 0);
            this.btnHistoryTab.MinimumSize = new System.Drawing.Size(120, 32);
            this.btnHistoryTab.Name = "btnHistoryTab";
            this.btnHistoryTab.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.btnHistoryTab.Size = new System.Drawing.Size(176, 32);
            this.btnHistoryTab.TabIndex = 2;
            this.btnHistoryTab.Text = "Customer Order History";
            this.btnHistoryTab.UseVisualStyleBackColor = false;
            this.btnHistoryTab.Click += new System.EventHandler(this.btnHistoryTab_Click);
            // 
            // panelPeriodFilter
            // 
            this.panelPeriodFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelPeriodFilter.Controls.Add(this.flowPeriodTabs);
            this.panelPeriodFilter.Controls.Add(this.lblPeriodLabel);
            this.panelPeriodFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelPeriodFilter.Location = new System.Drawing.Point(0, 284);
            this.panelPeriodFilter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelPeriodFilter.Name = "panelPeriodFilter";
            this.panelPeriodFilter.Size = new System.Drawing.Size(1012, 44);
            this.panelPeriodFilter.TabIndex = 3;
            // 
            // flowPeriodTabs
            // 
            this.flowPeriodTabs.AutoSize = true;
            this.flowPeriodTabs.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowPeriodTabs.Controls.Add(this.btnWeekPeriod);
            this.flowPeriodTabs.Controls.Add(this.btnMonthPeriod);
            this.flowPeriodTabs.Controls.Add(this.btnYearPeriod);
            this.flowPeriodTabs.Location = new System.Drawing.Point(56, 6);
            this.flowPeriodTabs.Name = "flowPeriodTabs";
            this.flowPeriodTabs.Size = new System.Drawing.Size(210, 28);
            this.flowPeriodTabs.TabIndex = 1;
            // 
            // btnWeekPeriod
            // 
            this.btnWeekPeriod.BackColor = System.Drawing.Color.White;
            this.btnWeekPeriod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWeekPeriod.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnWeekPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWeekPeriod.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnWeekPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnWeekPeriod.Location = new System.Drawing.Point(0, 0);
            this.btnWeekPeriod.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnWeekPeriod.Name = "btnWeekPeriod";
            this.btnWeekPeriod.Size = new System.Drawing.Size(64, 28);
            this.btnWeekPeriod.TabIndex = 0;
            this.btnWeekPeriod.Text = "Week";
            this.btnWeekPeriod.UseVisualStyleBackColor = false;
            this.btnWeekPeriod.Click += new System.EventHandler(this.btnWeekPeriod_Click);
            // 
            // btnMonthPeriod
            // 
            this.btnMonthPeriod.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnMonthPeriod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMonthPeriod.FlatAppearance.BorderSize = 0;
            this.btnMonthPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMonthPeriod.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Bold);
            this.btnMonthPeriod.ForeColor = System.Drawing.Color.White;
            this.btnMonthPeriod.Location = new System.Drawing.Point(70, 0);
            this.btnMonthPeriod.Margin = new System.Windows.Forms.Padding(0, 0, 6, 0);
            this.btnMonthPeriod.Name = "btnMonthPeriod";
            this.btnMonthPeriod.Size = new System.Drawing.Size(64, 28);
            this.btnMonthPeriod.TabIndex = 1;
            this.btnMonthPeriod.Text = "Month";
            this.btnMonthPeriod.UseVisualStyleBackColor = false;
            this.btnMonthPeriod.Click += new System.EventHandler(this.btnMonthPeriod_Click);
            // 
            // btnYearPeriod
            // 
            this.btnYearPeriod.BackColor = System.Drawing.Color.White;
            this.btnYearPeriod.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYearPeriod.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnYearPeriod.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYearPeriod.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnYearPeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnYearPeriod.Location = new System.Drawing.Point(140, 0);
            this.btnYearPeriod.Name = "btnYearPeriod";
            this.btnYearPeriod.Size = new System.Drawing.Size(64, 28);
            this.btnYearPeriod.TabIndex = 2;
            this.btnYearPeriod.Text = "Year";
            this.btnYearPeriod.UseVisualStyleBackColor = false;
            this.btnYearPeriod.Click += new System.EventHandler(this.btnYearPeriod_Click);
            // 
            // lblPeriodLabel
            // 
            this.lblPeriodLabel.AutoSize = true;
            this.lblPeriodLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPeriodLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPeriodLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblPeriodLabel.Location = new System.Drawing.Point(0, 12);
            this.lblPeriodLabel.Name = "lblPeriodLabel";
            this.lblPeriodLabel.Size = new System.Drawing.Size(46, 18);
            this.lblPeriodLabel.TabIndex = 0;
            this.lblPeriodLabel.Text = "Period:";
            // 
            // panelCustomerFilter
            // 
            this.panelCustomerFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelCustomerFilter.Controls.Add(this.cmbCustomer);
            this.panelCustomerFilter.Controls.Add(this.lblCustomerLabel);
            this.panelCustomerFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCustomerFilter.Location = new System.Drawing.Point(0, 336);
            this.panelCustomerFilter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelCustomerFilter.Name = "panelCustomerFilter";
            this.panelCustomerFilter.Size = new System.Drawing.Size(1012, 44);
            this.panelCustomerFilter.TabIndex = 4;
            this.panelCustomerFilter.Visible = false;
            // 
            // cmbCustomer
            // 
            this.cmbCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomer.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.cmbCustomer.FormattingEnabled = true;
            this.cmbCustomer.Location = new System.Drawing.Point(72, 8);
            this.cmbCustomer.Name = "cmbCustomer";
            this.cmbCustomer.Size = new System.Drawing.Size(320, 26);
            this.cmbCustomer.TabIndex = 1;
            this.cmbCustomer.SelectedIndexChanged += new System.EventHandler(this.cmbCustomer_SelectedIndexChanged);
            // 
            // lblCustomerLabel
            // 
            this.lblCustomerLabel.AutoSize = true;
            this.lblCustomerLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblCustomerLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblCustomerLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblCustomerLabel.Location = new System.Drawing.Point(0, 12);
            this.lblCustomerLabel.Name = "lblCustomerLabel";
            this.lblCustomerLabel.Size = new System.Drawing.Size(66, 18);
            this.lblCustomerLabel.TabIndex = 0;
            this.lblCustomerLabel.Text = "Customer:";
            // 
            // panelGridOuter
            // 
            this.panelGridOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.panelGridOuter.Controls.Add(this.panelGridInner);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 388);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 320);
            this.panelGridOuter.TabIndex = 5;
            // 
            // panelGridInner
            // 
            this.panelGridInner.BackColor = System.Drawing.Color.White;
            this.panelGridInner.Controls.Add(this.panelGridBody);
            this.panelGridInner.Controls.Add(this.panelGridHeader);
            this.panelGridInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridInner.Location = new System.Drawing.Point(1, 1);
            this.panelGridInner.Name = "panelGridInner";
            this.panelGridInner.Size = new System.Drawing.Size(1010, 318);
            this.panelGridInner.TabIndex = 0;
            // 
            // panelGridBody
            // 
            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.gridReport);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(0, 36);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 282);
            this.panelGridBody.TabIndex = 1;
            // 
            // gridReport
            // 
            this.gridReport.AllowUserToAddRows = false;
            this.gridReport.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridReport.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridReport.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridReport.BackgroundColor = System.Drawing.Color.White;
            this.gridReport.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridReport.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridReport.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridReport.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridReport.ColumnHeadersHeight = 36;
            this.gridReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridReport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colOrderRef,
            this.colCustomer,
            this.colOrderDate,
            this.colTotalAmount,
            this.colStatus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridReport.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridReport.EnableHeadersVisualStyles = false;
            this.gridReport.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridReport.Location = new System.Drawing.Point(0, 4);
            this.gridReport.MinimumSize = new System.Drawing.Size(0, 280);
            this.gridReport.MultiSelect = false;
            this.gridReport.Name = "gridReport";
            this.gridReport.ReadOnly = true;
            this.gridReport.RowHeadersVisible = false;
            this.gridReport.RowHeadersWidth = 51;
            this.gridReport.RowTemplate.Height = 36;
            this.gridReport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridReport.Size = new System.Drawing.Size(1010, 278);
            this.gridReport.TabIndex = 0;
            this.gridReport.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gridReport_CellFormatting);
            // 
            // colOrderRef
            // 
            this.colOrderRef.HeaderText = "Order Ref";
            this.colOrderRef.MinimumWidth = 6;
            this.colOrderRef.Name = "colOrderRef";
            this.colOrderRef.ReadOnly = true;
            // 
            // colCustomer
            // 
            this.colCustomer.HeaderText = "Customer";
            this.colCustomer.MinimumWidth = 6;
            this.colCustomer.Name = "colCustomer";
            this.colCustomer.ReadOnly = true;
            // 
            // colOrderDate
            // 
            this.colOrderDate.HeaderText = "Order Date";
            this.colOrderDate.MinimumWidth = 6;
            this.colOrderDate.Name = "colOrderDate";
            this.colOrderDate.ReadOnly = true;
            // 
            // colTotalAmount
            // 
            this.colTotalAmount.HeaderText = "Total Amount";
            this.colTotalAmount.MinimumWidth = 6;
            this.colTotalAmount.Name = "colTotalAmount";
            this.colTotalAmount.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.HeaderText = "Status";
            this.colStatus.MinimumWidth = 6;
            this.colStatus.Name = "colStatus";
            this.colStatus.ReadOnly = true;
            // 
            // panelGridHeader
            // 
            this.panelGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelGridHeader.Controls.Add(this.lblGridHeaderTitle);
            this.panelGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGridHeader.Location = new System.Drawing.Point(0, 0);
            this.panelGridHeader.Name = "panelGridHeader";
            this.panelGridHeader.Padding = new System.Windows.Forms.Padding(12, 8, 10, 4);
            this.panelGridHeader.Size = new System.Drawing.Size(1010, 36);
            this.panelGridHeader.TabIndex = 0;
            // 
            // lblGridHeaderTitle
            // 
            this.lblGridHeaderTitle.AutoSize = true;
            this.lblGridHeaderTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblGridHeaderTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGridHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblGridHeaderTitle.Location = new System.Drawing.Point(12, 10);
            this.lblGridHeaderTitle.Name = "lblGridHeaderTitle";
            this.lblGridHeaderTitle.Size = new System.Drawing.Size(98, 16);
            this.lblGridHeaderTitle.TabIndex = 0;
            this.lblGridHeaderTitle.Text = "Report Preview";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelFooter.Controls.Add(this.lblFooterStatus);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFooter.Location = new System.Drawing.Point(0, 716);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1012, 28);
            this.panelFooter.TabIndex = 6;
            // 
            // lblFooterStatus
            // 
            this.lblFooterStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblFooterStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFooterStatus.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblFooterStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblFooterStatus.Location = new System.Drawing.Point(0, 0);
            this.lblFooterStatus.Name = "lblFooterStatus";
            this.lblFooterStatus.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblFooterStatus.Size = new System.Drawing.Size(1012, 28);
            this.lblFooterStatus.TabIndex = 0;
            this.lblFooterStatus.Text = "";
            this.lblFooterStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.ControlBox = false;
            this.Controls.Add(this.panelScrollHost);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ReportsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Reports";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowHeaderActions.ResumeLayout(false);
            this.tableStatsRow.ResumeLayout(false);
            this.panelStatRevenue.ResumeLayout(false);
            this.panelStatOrders.ResumeLayout(false);
            this.panelStatLowStock.ResumeLayout(false);
            this.panelStatOutstanding.ResumeLayout(false);
            this.panelTabBar.ResumeLayout(false);
            this.panelTabBar.PerformLayout();
            this.flowTabs.ResumeLayout(false);
            this.flowTabs.PerformLayout();
            this.panelPeriodFilter.ResumeLayout(false);
            this.panelPeriodFilter.PerformLayout();
            this.flowPeriodTabs.ResumeLayout(false);
            this.panelCustomerFilter.ResumeLayout(false);
            this.panelCustomerFilter.PerformLayout();
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridInner.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridReport)).EndInit();
            this.panelGridHeader.ResumeLayout(false);
            this.panelGridHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowHeaderActions;
        private System.Windows.Forms.Button btnViewReport;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.TableLayoutPanel tableStatsRow;
        private System.Windows.Forms.Panel panelStatRevenue;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblStatTitleRevenue;
        private System.Windows.Forms.Panel panelStatOrders;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblStatTitleOrders;
        private System.Windows.Forms.Panel panelStatLowStock;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label lblStatTitleLowStock;
        private System.Windows.Forms.Panel panelStatOutstanding;
        private System.Windows.Forms.Label lblOutstanding;
        private System.Windows.Forms.Label lblStatTitleOutstanding;
        private System.Windows.Forms.Panel panelTabBar;
        private System.Windows.Forms.FlowLayoutPanel flowTabs;
        private System.Windows.Forms.Button btnSalesTab;
        private System.Windows.Forms.Button btnInventoryTab;
        private System.Windows.Forms.Button btnHistoryTab;
        private System.Windows.Forms.Panel panelPeriodFilter;
        private System.Windows.Forms.FlowLayoutPanel flowPeriodTabs;
        private System.Windows.Forms.Button btnWeekPeriod;
        private System.Windows.Forms.Button btnMonthPeriod;
        private System.Windows.Forms.Button btnYearPeriod;
        private System.Windows.Forms.Label lblPeriodLabel;
        private System.Windows.Forms.Panel panelCustomerFilter;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Label lblCustomerLabel;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridInner;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView gridReport;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrderDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalAmount;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Panel panelGridHeader;
        private System.Windows.Forms.Label lblGridHeaderTitle;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Label lblFooterStatus;
    }
}
