namespace SmartMed.UI
{
    partial class ManageCustomersForm
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
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.tableStatsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatTotal = new System.Windows.Forms.Panel();
            this.lblTotalCustomers = new System.Windows.Forms.Label();
            this.lblStatTotalTitle = new System.Windows.Forms.Label();
            this.panelStatActive = new System.Windows.Forms.Panel();
            this.lblActiveCustomers = new System.Windows.Forms.Label();
            this.lblStatActiveTitle = new System.Windows.Forms.Label();
            this.panelStatInactive = new System.Windows.Forms.Panel();
            this.lblInactiveCustomers = new System.Windows.Forms.Label();
            this.lblStatInactiveTitle = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.flowToolbarRight = new System.Windows.Forms.FlowLayoutPanel();
            this.lblSearchLabel = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.flowToolbarLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridInner = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.gridCustomers = new System.Windows.Forms.DataGridView();
            this.colCustomerID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCustomerRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colContactInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colOrders = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.lblGridHeaderTitle = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.flowPager = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPagePrev = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnPageNext = new System.Windows.Forms.Button();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowHeaderActions.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatTotal.SuspendLayout();
            this.panelStatActive.SuspendLayout();
            this.panelStatInactive.SuspendLayout();
            this.panelToolbar.SuspendLayout();
            this.flowToolbarRight.SuspendLayout();
            this.flowToolbarLeft.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridInner.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).BeginInit();
            this.panelGridHeader.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.flowPager.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelScrollHost
            // 
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
            this.tableLayoutRoot.Controls.Add(this.panelToolbar, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 3);
            this.tableLayoutRoot.Controls.Add(this.panelFooter, 0, 4);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 5;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 672);
            this.tableLayoutRoot.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.flowHeaderActions);
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;
            // 
            // flowHeaderActions
            // 
            this.flowHeaderActions.AutoSize = true;
            this.flowHeaderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowHeaderActions.Controls.Add(this.btnExport);
            this.flowHeaderActions.Controls.Add(this.btnPrint);
            this.flowHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowHeaderActions.Location = new System.Drawing.Point(810, 0);
            this.flowHeaderActions.Name = "flowHeaderActions";
            this.flowHeaderActions.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flowHeaderActions.Size = new System.Drawing.Size(202, 76);
            this.flowHeaderActions.TabIndex = 2;
            this.flowHeaderActions.WrapContents = false;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExport.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExport.Location = new System.Drawing.Point(0, 16);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(96, 30);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPrint.Location = new System.Drawing.Point(106, 16);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(96, 30);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(668, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "View customer records. Order Activity shows recent ordering; use Account to enabl" +
    "e or disable login.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(329, 39);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Manage Customers";
            // 
            // tableStatsRow
            // 
            this.tableStatsRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableStatsRow.ColumnCount = 3;
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableStatsRow.Controls.Add(this.panelStatTotal, 0, 0);
            this.tableStatsRow.Controls.Add(this.panelStatActive, 1, 0);
            this.tableStatsRow.Controls.Add(this.panelStatInactive, 2, 0);
            this.tableStatsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStatsRow.Location = new System.Drawing.Point(0, 100);
            this.tableStatsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.tableStatsRow.Name = "tableStatsRow";
            this.tableStatsRow.RowCount = 1;
            this.tableStatsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableStatsRow.Size = new System.Drawing.Size(1012, 108);
            this.tableStatsRow.TabIndex = 1;
            // 
            // panelStatTotal
            // 
            this.panelStatTotal.BackColor = System.Drawing.Color.White;
            this.panelStatTotal.Controls.Add(this.lblTotalCustomers);
            this.panelStatTotal.Controls.Add(this.lblStatTotalTitle);
            this.panelStatTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatTotal.Location = new System.Drawing.Point(0, 0);
            this.panelStatTotal.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatTotal.Name = "panelStatTotal";
            this.panelStatTotal.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatTotal.Size = new System.Drawing.Size(323, 108);
            this.panelStatTotal.TabIndex = 0;
            // 
            // lblTotalCustomers
            // 
            this.lblTotalCustomers.BackColor = System.Drawing.Color.White;
            this.lblTotalCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblTotalCustomers.Location = new System.Drawing.Point(16, 30);
            this.lblTotalCustomers.Name = "lblTotalCustomers";
            this.lblTotalCustomers.Size = new System.Drawing.Size(293, 64);
            this.lblTotalCustomers.TabIndex = 1;
            this.lblTotalCustomers.Text = "-";
            this.lblTotalCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTotalTitle
            // 
            this.lblStatTotalTitle.BackColor = System.Drawing.Color.White;
            this.lblStatTotalTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTotalTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTotalTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatTotalTitle.Name = "lblStatTotalTitle";
            this.lblStatTotalTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatTotalTitle.TabIndex = 0;
            this.lblStatTotalTitle.Text = "TOTAL CUSTOMERS";
            // 
            // panelStatActive
            // 
            this.panelStatActive.BackColor = System.Drawing.Color.White;
            this.panelStatActive.Controls.Add(this.lblActiveCustomers);
            this.panelStatActive.Controls.Add(this.lblStatActiveTitle);
            this.panelStatActive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatActive.Location = new System.Drawing.Point(337, 0);
            this.panelStatActive.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatActive.Name = "panelStatActive";
            this.panelStatActive.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatActive.Size = new System.Drawing.Size(323, 108);
            this.panelStatActive.TabIndex = 1;
            // 
            // lblActiveCustomers
            // 
            this.lblActiveCustomers.BackColor = System.Drawing.Color.White;
            this.lblActiveCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblActiveCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblActiveCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblActiveCustomers.Location = new System.Drawing.Point(16, 30);
            this.lblActiveCustomers.Name = "lblActiveCustomers";
            this.lblActiveCustomers.Size = new System.Drawing.Size(293, 64);
            this.lblActiveCustomers.TabIndex = 1;
            this.lblActiveCustomers.Text = "-";
            this.lblActiveCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatActiveTitle
            // 
            this.lblStatActiveTitle.BackColor = System.Drawing.Color.White;
            this.lblStatActiveTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatActiveTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatActiveTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatActiveTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatActiveTitle.Name = "lblStatActiveTitle";
            this.lblStatActiveTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatActiveTitle.TabIndex = 0;
            this.lblStatActiveTitle.Text = "ENABLED";
            // 
            // panelStatInactive
            // 
            this.panelStatInactive.BackColor = System.Drawing.Color.White;
            this.panelStatInactive.Controls.Add(this.lblInactiveCustomers);
            this.panelStatInactive.Controls.Add(this.lblStatInactiveTitle);
            this.panelStatInactive.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatInactive.Location = new System.Drawing.Point(674, 0);
            this.panelStatInactive.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatInactive.Name = "panelStatInactive";
            this.panelStatInactive.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatInactive.Size = new System.Drawing.Size(338, 108);
            this.panelStatInactive.TabIndex = 2;
            // 
            // lblInactiveCustomers
            // 
            this.lblInactiveCustomers.BackColor = System.Drawing.Color.White;
            this.lblInactiveCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInactiveCustomers.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblInactiveCustomers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblInactiveCustomers.Location = new System.Drawing.Point(16, 30);
            this.lblInactiveCustomers.Name = "lblInactiveCustomers";
            this.lblInactiveCustomers.Size = new System.Drawing.Size(308, 64);
            this.lblInactiveCustomers.TabIndex = 1;
            this.lblInactiveCustomers.Text = "-";
            this.lblInactiveCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatInactiveTitle
            // 
            this.lblStatInactiveTitle.BackColor = System.Drawing.Color.White;
            this.lblStatInactiveTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatInactiveTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatInactiveTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatInactiveTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatInactiveTitle.Name = "lblStatInactiveTitle";
            this.lblStatInactiveTitle.Size = new System.Drawing.Size(308, 16);
            this.lblStatInactiveTitle.TabIndex = 0;
            this.lblStatInactiveTitle.Text = "DISABLED";
            // 
            // panelToolbar
            // 
            this.panelToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelToolbar.Controls.Add(this.flowToolbarRight);
            this.panelToolbar.Controls.Add(this.flowToolbarLeft);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelToolbar.Location = new System.Drawing.Point(0, 224);
            this.panelToolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1012, 44);
            this.panelToolbar.TabIndex = 1;
            // 
            // flowToolbarRight
            // 
            this.flowToolbarRight.AutoSize = true;
            this.flowToolbarRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowToolbarRight.Controls.Add(this.lblSearchLabel);
            this.flowToolbarRight.Controls.Add(this.txtSearch);
            this.flowToolbarRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowToolbarRight.Location = new System.Drawing.Point(747, 0);
            this.flowToolbarRight.Name = "flowToolbarRight";
            this.flowToolbarRight.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowToolbarRight.Size = new System.Drawing.Size(265, 44);
            this.flowToolbarRight.TabIndex = 1;
            this.flowToolbarRight.WrapContents = false;
            // 
            // lblSearchLabel
            // 
            this.lblSearchLabel.AutoSize = true;
            this.lblSearchLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblSearchLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblSearchLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblSearchLabel.Location = new System.Drawing.Point(0, 10);
            this.lblSearchLabel.Margin = new System.Windows.Forms.Padding(0, 6, 6, 0);
            this.lblSearchLabel.Name = "lblSearchLabel";
            this.lblSearchLabel.Size = new System.Drawing.Size(59, 18);
            this.lblSearchLabel.TabIndex = 0;
            this.lblSearchLabel.Text = "Search:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtSearch.Location = new System.Drawing.Point(65, 4);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(200, 24);
            this.txtSearch.TabIndex = 1;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // flowToolbarLeft
            // 
            this.flowToolbarLeft.AutoSize = true;
            this.flowToolbarLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowToolbarLeft.Controls.Add(this.btnAdd);
            this.flowToolbarLeft.Controls.Add(this.btnReload);
            this.flowToolbarLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowToolbarLeft.Location = new System.Drawing.Point(0, 0);
            this.flowToolbarLeft.Name = "flowToolbarLeft";
            this.flowToolbarLeft.Size = new System.Drawing.Size(224, 44);
            this.flowToolbarLeft.TabIndex = 0;
            this.flowToolbarLeft.WrapContents = false;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(109)))), ((int)(((byte)(100)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add Customer";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnReload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnReload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnReload.Location = new System.Drawing.Point(140, 0);
            this.btnReload.Margin = new System.Windows.Forms.Padding(0);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(84, 30);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            this.btnReload.Click += new System.EventHandler(this.BtnReload_Click);
            // 
            // panelGridOuter
            // 
            this.panelGridOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.panelGridOuter.Controls.Add(this.panelGridInner);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 280);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 312);
            this.panelGridOuter.TabIndex = 2;
            // 
            // panelGridInner
            // 
            this.panelGridInner.BackColor = System.Drawing.Color.White;
            this.panelGridInner.Controls.Add(this.panelGridBody);
            this.panelGridInner.Controls.Add(this.panelGridHeader);
            this.panelGridInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridInner.Location = new System.Drawing.Point(1, 1);
            this.panelGridInner.Name = "panelGridInner";
            this.panelGridInner.Size = new System.Drawing.Size(1010, 310);
            this.panelGridInner.TabIndex = 0;
            // 
            // panelGridBody
            // 
            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.gridCustomers);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(0, 36);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 274);
            this.panelGridBody.TabIndex = 1;
            // 
            // gridCustomers
            // 
            this.gridCustomers.AllowUserToAddRows = false;
            this.gridCustomers.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridCustomers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridCustomers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCustomers.BackgroundColor = System.Drawing.Color.White;
            this.gridCustomers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridCustomers.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridCustomers.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridCustomers.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridCustomers.ColumnHeadersHeight = 36;
            this.gridCustomers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridCustomers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCustomerID,
            this.colCustomerRef,
            this.colName,
            this.colContactInfo,
            this.colLastOrder,
            this.colOrders,
            this.colStatus});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridCustomers.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridCustomers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCustomers.EnableHeadersVisualStyles = false;
            this.gridCustomers.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridCustomers.Location = new System.Drawing.Point(0, 4);
            this.gridCustomers.MultiSelect = false;
            this.gridCustomers.Name = "gridCustomers";
            this.gridCustomers.ReadOnly = true;
            this.gridCustomers.RowHeadersVisible = false;
            this.gridCustomers.RowHeadersWidth = 51;
            this.gridCustomers.RowTemplate.Height = 36;
            this.gridCustomers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCustomers.Size = new System.Drawing.Size(1010, 270);
            this.gridCustomers.TabIndex = 0;
            this.gridCustomers.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCustomers_CellContentClick);
            this.gridCustomers.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridCustomers_CellFormatting);
            this.gridCustomers.SelectionChanged += new System.EventHandler(this.GridCustomers_SelectionChanged);
            // 
            // colCustomerID
            // 
            this.colCustomerID.DataPropertyName = "CustomerID";
            this.colCustomerID.HeaderText = "CustomerID";
            this.colCustomerID.MinimumWidth = 6;
            this.colCustomerID.Name = "colCustomerID";
            this.colCustomerID.ReadOnly = true;
            this.colCustomerID.Visible = false;
            // 
            // colCustomerRef
            // 
            this.colCustomerRef.DataPropertyName = "ID";
            this.colCustomerRef.HeaderText = "Customer ID";
            this.colCustomerRef.MinimumWidth = 6;
            this.colCustomerRef.Name = "colCustomerRef";
            this.colCustomerRef.ReadOnly = true;
            // 
            // colName
            // 
            this.colName.DataPropertyName = "Name";
            this.colName.HeaderText = "Name";
            this.colName.MinimumWidth = 6;
            this.colName.Name = "colName";
            this.colName.ReadOnly = true;
            // 
            // colContactInfo
            // 
            this.colContactInfo.DataPropertyName = "ContactInfo";
            this.colContactInfo.HeaderText = "Contact";
            this.colContactInfo.MinimumWidth = 6;
            this.colContactInfo.Name = "colContactInfo";
            this.colContactInfo.ReadOnly = true;
            // 
            // colLastOrder
            // 
            this.colLastOrder.DataPropertyName = "LastOrder";
            this.colLastOrder.HeaderText = "Last Order";
            this.colLastOrder.MinimumWidth = 6;
            this.colLastOrder.Name = "colLastOrder";
            this.colLastOrder.ReadOnly = true;
            // 
            // colOrders
            // 
            this.colOrders.DataPropertyName = "Orders";
            this.colOrders.HeaderText = "Orders";
            this.colOrders.MinimumWidth = 6;
            this.colOrders.Name = "colOrders";
            this.colOrders.ReadOnly = true;
            // 
            // colStatus
            // 
            this.colStatus.DataPropertyName = "Activity";
            this.colStatus.HeaderText = "Order Activity";
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
            this.lblGridHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblGridHeaderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGridHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblGridHeaderTitle.Location = new System.Drawing.Point(12, 8);
            this.lblGridHeaderTitle.Name = "lblGridHeaderTitle";
            this.lblGridHeaderTitle.Size = new System.Drawing.Size(174, 17);
            this.lblGridHeaderTitle.TabIndex = 0;
            this.lblGridHeaderTitle.Text = "CUSTOMER RECORDS";
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelFooter.Controls.Add(this.flowPager);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFooter.Location = new System.Drawing.Point(0, 624);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1012, 48);
            this.panelFooter.TabIndex = 4;
            // 
            // flowPager
            // 
            this.flowPager.AutoSize = true;
            this.flowPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowPager.Controls.Add(this.btnPagePrev);
            this.flowPager.Controls.Add(this.lblPageInfo);
            this.flowPager.Controls.Add(this.btnPageNext);
            this.flowPager.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowPager.Location = new System.Drawing.Point(833, 0);
            this.flowPager.Name = "flowPager";
            this.flowPager.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.flowPager.Size = new System.Drawing.Size(179, 48);
            this.flowPager.TabIndex = 1;
            this.flowPager.WrapContents = false;
            // 
            // btnPagePrev
            // 
            this.btnPagePrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPagePrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagePrev.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPagePrev.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnPagePrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagePrev.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPagePrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPagePrev.Location = new System.Drawing.Point(8, 8);
            this.btnPagePrev.Margin = new System.Windows.Forms.Padding(0);
            this.btnPagePrev.Name = "btnPagePrev";
            this.btnPagePrev.Size = new System.Drawing.Size(36, 32);
            this.btnPagePrev.TabIndex = 0;
            this.btnPagePrev.Text = "<";
            this.btnPagePrev.UseVisualStyleBackColor = false;
            // 
            // lblPageInfo
            // 
            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageInfo.Location = new System.Drawing.Point(52, 16);
            this.lblPageInfo.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(83, 18);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1 of 1";
            // 
            // btnPageNext
            // 
            this.btnPageNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPageNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPageNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPageNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnPageNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPageNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPageNext.Location = new System.Drawing.Point(143, 8);
            this.btnPageNext.Margin = new System.Windows.Forms.Padding(0);
            this.btnPageNext.Name = "btnPageNext";
            this.btnPageNext.Size = new System.Drawing.Size(36, 32);
            this.btnPageNext.TabIndex = 2;
            this.btnPageNext.Text = ">";
            this.btnPageNext.UseVisualStyleBackColor = false;
            // 
            // ManageCustomersForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.Controls.Add(this.panelScrollHost);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Name = "ManageCustomersForm";
            this.Text = "Manage Customers";
            this.panelScrollHost.ResumeLayout(false);
            this.tableLayoutRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowHeaderActions.ResumeLayout(false);
            this.tableStatsRow.ResumeLayout(false);
            this.panelStatTotal.ResumeLayout(false);
            this.panelStatActive.ResumeLayout(false);
            this.panelStatInactive.ResumeLayout(false);
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.flowToolbarRight.ResumeLayout(false);
            this.flowToolbarRight.PerformLayout();
            this.flowToolbarLeft.ResumeLayout(false);
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridInner.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCustomers)).EndInit();
            this.panelGridHeader.ResumeLayout(false);
            this.panelGridHeader.PerformLayout();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.flowPager.ResumeLayout(false);
            this.flowPager.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flowHeaderActions;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.FlowLayoutPanel flowToolbarLeft;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.FlowLayoutPanel flowToolbarRight;
        private System.Windows.Forms.Label lblSearchLabel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridInner;
        private System.Windows.Forms.Panel panelGridHeader;
        private System.Windows.Forms.Label lblGridHeaderTitle;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView gridCustomers;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCustomerRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn colName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colContactInfo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastOrder;
        private System.Windows.Forms.DataGridViewTextBoxColumn colOrders;
        private System.Windows.Forms.DataGridViewTextBoxColumn colStatus;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.TableLayoutPanel tableStatsRow;
        private System.Windows.Forms.Panel panelStatTotal;
        private System.Windows.Forms.Label lblStatTotalTitle;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Panel panelStatActive;
        private System.Windows.Forms.Label lblStatActiveTitle;
        private System.Windows.Forms.Label lblActiveCustomers;
        private System.Windows.Forms.Panel panelStatInactive;
        private System.Windows.Forms.Label lblStatInactiveTitle;
        private System.Windows.Forms.Label lblInactiveCustomers;
        private System.Windows.Forms.FlowLayoutPanel flowPager;
        private System.Windows.Forms.Button btnPagePrev;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnPageNext;
    }
}
