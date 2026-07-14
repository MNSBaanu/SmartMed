namespace SmartMed.UI
{
    partial class ManageOrdersForm
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
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelFilterOuter = new System.Windows.Forms.Panel();
            this.flowFilterMain = new System.Windows.Forms.FlowLayoutPanel();
            this.flowFilterActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnApplyFilters = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.panelFieldDate = new System.Windows.Forms.Panel();
            this.flowDateRange = new System.Windows.Forms.FlowLayoutPanel();
            this.chkDateRange = new System.Windows.Forms.CheckBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblDateTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblServiceDate = new System.Windows.Forms.Label();
            this.panelFieldStatus = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatusCategory = new System.Windows.Forms.Label();
            this.panelFieldSearch = new System.Windows.Forms.Panel();
            this.panelSearchWrap = new System.Windows.Forms.Panel();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchOrders = new System.Windows.Forms.Label();
            this.lblFilterBadge = new System.Windows.Forms.Label();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.flowPager = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPagePrev = new System.Windows.Forms.Button();
            this.lblPageInfo = new System.Windows.Forms.Label();
            this.btnPageNext = new System.Windows.Forms.Button();
            this.tableStatsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatVolume = new System.Windows.Forms.Panel();
            this.lblVolume = new System.Windows.Forms.Label();
            this.lblStatVolumeTitle = new System.Windows.Forms.Label();
            this.panelStatAvg = new System.Windows.Forms.Panel();
            this.lblAvgTime = new System.Windows.Forms.Label();
            this.lblStatAvgTitle = new System.Windows.Forms.Label();
            this.panelStatFlags = new System.Windows.Forms.Panel();
            this.lblFlags = new System.Windows.Forms.Label();
            this.lblStatFlagsTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFilterOuter.SuspendLayout();
            this.flowFilterMain.SuspendLayout();
            this.flowFilterActions.SuspendLayout();
            this.panelFieldDate.SuspendLayout();
            this.flowDateRange.SuspendLayout();
            this.panelFieldStatus.SuspendLayout();
            this.panelFieldSearch.SuspendLayout();
            this.panelSearchWrap.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
            this.panelFooter.SuspendLayout();
            this.flowPager.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatVolume.SuspendLayout();
            this.panelStatAvg.SuspendLayout();
            this.panelStatFlags.SuspendLayout();
            this.SuspendLayout();

            this.panelScrollHost.AutoScroll = true;
            this.panelScrollHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelScrollHost.Controls.Add(this.tableLayoutRoot);
            this.panelScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScrollHost.Location = new System.Drawing.Point(0, 0);
            this.panelScrollHost.Name = "panelScrollHost";
            this.panelScrollHost.Padding = new System.Windows.Forms.Padding(24);
            this.panelScrollHost.Size = new System.Drawing.Size(1060, 720);
            this.panelScrollHost.TabIndex = 0;

            this.tableLayoutRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.tableStatsRow, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelFilterOuter, 0, 2);
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
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 700);
            this.tableLayoutRoot.TabIndex = 0;

            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;

            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(520, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Monitor and process pharmaceutical orders across all clinical departments.";

            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(188, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Manage Orders";

            this.panelFilterOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelFilterOuter.Controls.Add(this.flowFilterMain);
            this.panelFilterOuter.Controls.Add(this.lblFilterBadge);
            this.panelFilterOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFilterOuter.Location = new System.Drawing.Point(0, 200);
            this.panelFilterOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelFilterOuter.Name = "panelFilterOuter";
            this.panelFilterOuter.Padding = new System.Windows.Forms.Padding(16, 20, 16, 16);
            this.panelFilterOuter.Size = new System.Drawing.Size(1012, 140);
            this.panelFilterOuter.TabIndex = 1;

            this.flowFilterMain.AutoSize = true;
            this.flowFilterMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.flowFilterMain.Controls.Add(this.panelFieldSearch);
            this.flowFilterMain.Controls.Add(this.panelFieldStatus);
            this.flowFilterMain.Controls.Add(this.panelFieldDate);
            this.flowFilterMain.Controls.Add(this.flowFilterActions);
            this.flowFilterMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowFilterMain.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowFilterMain.Location = new System.Drawing.Point(16, 20);
            this.flowFilterMain.Name = "flowFilterMain";
            this.flowFilterMain.Padding = new System.Windows.Forms.Padding(0, 12, 0, 0);
            this.flowFilterMain.Size = new System.Drawing.Size(980, 104);
            this.flowFilterMain.TabIndex = 1;
            this.flowFilterMain.WrapContents = true;

            this.flowFilterActions.AutoSize = true;
            this.flowFilterActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.flowFilterActions.Controls.Add(this.btnApplyFilters);
            this.flowFilterActions.Controls.Add(this.btnExport);
            this.flowFilterActions.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowFilterActions.Location = new System.Drawing.Point(0, 66);
            this.flowFilterActions.Margin = new System.Windows.Forms.Padding(0, 22, 0, 0);
            this.flowFilterActions.Name = "flowFilterActions";
            this.flowFilterActions.Size = new System.Drawing.Size(242, 30);
            this.flowFilterActions.TabIndex = 3;

            this.btnApplyFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.btnApplyFilters.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnApplyFilters.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnApplyFilters.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApplyFilters.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnApplyFilters.ForeColor = System.Drawing.Color.White;
            this.btnApplyFilters.Location = new System.Drawing.Point(0, 0);
            this.btnApplyFilters.Margin = new System.Windows.Forms.Padding(0, 0, 8, 0);
            this.btnApplyFilters.Name = "btnApplyFilters";
            this.btnApplyFilters.Size = new System.Drawing.Size(120, 30);
            this.btnApplyFilters.TabIndex = 0;
            this.btnApplyFilters.Text = "Apply Filters";
            this.btnApplyFilters.UseVisualStyleBackColor = false;
            this.btnApplyFilters.Click += new System.EventHandler(this.BtnApplyFilters_Click);

            this.btnExport.BackColor = System.Drawing.Color.White;
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExport.Location = new System.Drawing.Point(128, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 30);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export Data";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            this.panelFieldDate.AutoSize = true;
            this.panelFieldDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelFieldDate.Controls.Add(this.flowDateRange);
            this.panelFieldDate.Controls.Add(this.lblServiceDate);
            this.panelFieldDate.Location = new System.Drawing.Point(500, 0);
            this.panelFieldDate.Margin = new System.Windows.Forms.Padding(0, 0, 20, 8);
            this.panelFieldDate.Name = "panelFieldDate";
            this.panelFieldDate.Size = new System.Drawing.Size(380, 52);
            this.panelFieldDate.TabIndex = 2;

            this.flowDateRange.AutoSize = true;
            this.flowDateRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.flowDateRange.Controls.Add(this.chkDateRange);
            this.flowDateRange.Controls.Add(this.dtpFrom);
            this.flowDateRange.Controls.Add(this.lblDateTo);
            this.flowDateRange.Controls.Add(this.dtpTo);
            this.flowDateRange.Location = new System.Drawing.Point(0, 20);
            this.flowDateRange.Name = "flowDateRange";
            this.flowDateRange.Size = new System.Drawing.Size(380, 30);
            this.flowDateRange.TabIndex = 1;

            this.chkDateRange.AutoSize = true;
            this.chkDateRange.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.chkDateRange.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.chkDateRange.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.chkDateRange.Location = new System.Drawing.Point(0, 6);
            this.chkDateRange.Margin = new System.Windows.Forms.Padding(0, 6, 8, 0);
            this.chkDateRange.Name = "chkDateRange";
            this.chkDateRange.Size = new System.Drawing.Size(112, 22);
            this.chkDateRange.TabIndex = 0;
            this.chkDateRange.Text = "Use date range";
            this.chkDateRange.UseVisualStyleBackColor = false;

            this.dtpFrom.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(120, 3);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(120, 25);
            this.dtpFrom.TabIndex = 1;

            this.lblDateTo.AutoSize = true;
            this.lblDateTo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblDateTo.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblDateTo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblDateTo.Location = new System.Drawing.Point(246, 8);
            this.lblDateTo.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(18, 18);
            this.lblDateTo.TabIndex = 2;
            this.lblDateTo.Text = "to";

            this.dtpTo.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(273, 3);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(120, 25);
            this.dtpTo.TabIndex = 3;

            this.lblServiceDate.AutoSize = true;
            this.lblServiceDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblServiceDate.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblServiceDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblServiceDate.Location = new System.Drawing.Point(0, 0);
            this.lblServiceDate.Name = "lblServiceDate";
            this.lblServiceDate.Size = new System.Drawing.Size(130, 16);
            this.lblServiceDate.TabIndex = 0;
            this.lblServiceDate.Text = "Service Date Range:";

            this.panelFieldStatus.AutoSize = true;
            this.panelFieldStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelFieldStatus.Controls.Add(this.cmbStatus);
            this.panelFieldStatus.Controls.Add(this.lblStatusCategory);
            this.panelFieldStatus.Location = new System.Drawing.Point(320, 0);
            this.panelFieldStatus.Margin = new System.Windows.Forms.Padding(0, 0, 20, 8);
            this.panelFieldStatus.Name = "panelFieldStatus";
            this.panelFieldStatus.Size = new System.Drawing.Size(160, 52);
            this.panelFieldStatus.TabIndex = 1;

            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "All Statuses",
            "Pending",
            "Ready for Pickup",
            "Delivered",
            "Cancelled",
            "Flagged"});
            this.cmbStatus.Location = new System.Drawing.Point(0, 20);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(160, 26);
            this.cmbStatus.TabIndex = 1;

            this.lblStatusCategory.AutoSize = true;
            this.lblStatusCategory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblStatusCategory.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatusCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblStatusCategory.Location = new System.Drawing.Point(0, 0);
            this.lblStatusCategory.Name = "lblStatusCategory";
            this.lblStatusCategory.Size = new System.Drawing.Size(108, 16);
            this.lblStatusCategory.TabIndex = 0;
            this.lblStatusCategory.Text = "Status Category:";

            this.panelFieldSearch.AutoSize = true;
            this.panelFieldSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelFieldSearch.Controls.Add(this.panelSearchWrap);
            this.panelFieldSearch.Controls.Add(this.lblSearchOrders);
            this.panelFieldSearch.Location = new System.Drawing.Point(0, 0);
            this.panelFieldSearch.Margin = new System.Windows.Forms.Padding(0, 0, 20, 8);
            this.panelFieldSearch.Name = "panelFieldSearch";
            this.panelFieldSearch.Size = new System.Drawing.Size(300, 52);
            this.panelFieldSearch.TabIndex = 0;

            this.panelSearchWrap.BackColor = System.Drawing.Color.White;
            this.panelSearchWrap.Controls.Add(this.txtSearch);
            this.panelSearchWrap.Location = new System.Drawing.Point(0, 20);
            this.panelSearchWrap.Name = "panelSearchWrap";
            this.panelSearchWrap.Size = new System.Drawing.Size(280, 30);
            this.panelSearchWrap.TabIndex = 1;

            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtSearch.Location = new System.Drawing.Point(0, 0);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(280, 19);
            this.txtSearch.TabIndex = 0;

            this.lblSearchOrders.AutoSize = true;
            this.lblSearchOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblSearchOrders.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSearchOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblSearchOrders.Location = new System.Drawing.Point(0, 0);
            this.lblSearchOrders.Name = "lblSearchOrders";
            this.lblSearchOrders.Size = new System.Drawing.Size(92, 16);
            this.lblSearchOrders.TabIndex = 0;
            this.lblSearchOrders.Text = "Search Orders:";

            this.lblFilterBadge.AutoSize = true;
            this.lblFilterBadge.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.lblFilterBadge.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFilterBadge.ForeColor = System.Drawing.Color.White;
            this.lblFilterBadge.Location = new System.Drawing.Point(28, 0);
            this.lblFilterBadge.Name = "lblFilterBadge";
            this.lblFilterBadge.Size = new System.Drawing.Size(100, 16);
            this.lblFilterBadge.TabIndex = 0;
            this.lblFilterBadge.Text = "  Filter Options  ";

            this.panelGridOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.panelGridOuter.Controls.Add(this.gridOrders);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 356);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 364);
            this.panelGridOuter.TabIndex = 2;

            this.gridOrders.AllowUserToAddRows = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrders.BackgroundColor = System.Drawing.Color.White;
            this.gridOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridOrders.ColumnHeadersHeight = 36;
            this.gridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridOrders.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrders.EnableHeadersVisualStyles = false;
            this.gridOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridOrders.Location = new System.Drawing.Point(1, 1);
            this.gridOrders.MinimumSize = new System.Drawing.Size(0, 280);
            this.gridOrders.MultiSelect = false;
            this.gridOrders.Name = "gridOrders";
            this.gridOrders.ReadOnly = true;
            this.gridOrders.RowHeadersVisible = false;
            this.gridOrders.RowHeadersWidth = 51;
            this.gridOrders.RowTemplate.Height = 36;
            this.gridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.Size = new System.Drawing.Size(1010, 362);
            this.gridOrders.TabIndex = 0;
            this.gridOrders.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.GridOrders_CellBeginEdit);
            this.gridOrders.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridOrders_CellClick);
            this.gridOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridOrders_CellContentClick);
            this.gridOrders.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridOrders_CellFormatting);
            this.gridOrders.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridOrders_CellValueChanged);
            this.gridOrders.CurrentCellDirtyStateChanged += new System.EventHandler(this.GridOrders_CurrentCellDirtyStateChanged);
            this.gridOrders.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.GridOrders_DataError);
            this.gridOrders.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.GridOrders_EditingControlShowing);
            this.gridOrders.SelectionChanged += new System.EventHandler(this.GridOrders_SelectionChanged);

            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelFooter.Controls.Add(this.flowPager);
            this.panelFooter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFooter.Location = new System.Drawing.Point(0, 672);
            this.panelFooter.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(1012, 48);
            this.panelFooter.TabIndex = 4;

            this.flowPager.AutoSize = true;
            this.flowPager.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowPager.Controls.Add(this.btnPagePrev);
            this.flowPager.Controls.Add(this.lblPageInfo);
            this.flowPager.Controls.Add(this.btnPageNext);
            this.flowPager.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowPager.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowPager.Location = new System.Drawing.Point(829, 0);
            this.flowPager.Name = "flowPager";
            this.flowPager.Padding = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.flowPager.Size = new System.Drawing.Size(183, 48);
            this.flowPager.TabIndex = 1;

            this.btnPagePrev.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPagePrev.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPagePrev.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPagePrev.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPagePrev.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnPagePrev.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPagePrev.Location = new System.Drawing.Point(8, 8);
            this.btnPagePrev.Name = "btnPagePrev";
            this.btnPagePrev.Size = new System.Drawing.Size(36, 32);
            this.btnPagePrev.TabIndex = 0;
            this.btnPagePrev.Text = "<";
            this.btnPagePrev.UseVisualStyleBackColor = false;
            this.btnPagePrev.Click += new System.EventHandler(this.BtnPagePrev_Click);

            this.lblPageInfo.AutoSize = true;
            this.lblPageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageInfo.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageInfo.Location = new System.Drawing.Point(52, 12);
            this.lblPageInfo.Margin = new System.Windows.Forms.Padding(8, 8, 8, 0);
            this.lblPageInfo.Name = "lblPageInfo";
            this.lblPageInfo.Size = new System.Drawing.Size(67, 18);
            this.lblPageInfo.TabIndex = 1;
            this.lblPageInfo.Text = "Page 1 / 1";

            this.btnPageNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPageNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPageNext.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPageNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPageNext.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnPageNext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPageNext.Location = new System.Drawing.Point(135, 8);
            this.btnPageNext.Name = "btnPageNext";
            this.btnPageNext.Size = new System.Drawing.Size(36, 32);
            this.btnPageNext.TabIndex = 2;
            this.btnPageNext.Text = ">";
            this.btnPageNext.UseVisualStyleBackColor = false;
            this.btnPageNext.Click += new System.EventHandler(this.BtnPageNext_Click);

            this.tableStatsRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableStatsRow.ColumnCount = 3;
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableStatsRow.Controls.Add(this.panelStatVolume, 0, 0);
            this.tableStatsRow.Controls.Add(this.panelStatAvg, 1, 0);
            this.tableStatsRow.Controls.Add(this.panelStatFlags, 2, 0);
            this.tableStatsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStatsRow.Location = new System.Drawing.Point(0, 92);
            this.tableStatsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.tableStatsRow.Name = "tableStatsRow";
            this.tableStatsRow.RowCount = 1;
            this.tableStatsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableStatsRow.Size = new System.Drawing.Size(1012, 108);
            this.tableStatsRow.TabIndex = 1;

            this.panelStatVolume.BackColor = System.Drawing.Color.White;
            this.panelStatVolume.Controls.Add(this.lblVolume);
            this.panelStatVolume.Controls.Add(this.lblStatVolumeTitle);
            this.panelStatVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatVolume.Location = new System.Drawing.Point(0, 0);
            this.panelStatVolume.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatVolume.Name = "panelStatVolume";
            this.panelStatVolume.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatVolume.Size = new System.Drawing.Size(323, 108);
            this.panelStatVolume.TabIndex = 0;

            this.lblVolume.BackColor = System.Drawing.Color.White;
            this.lblVolume.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblVolume.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblVolume.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblVolume.Location = new System.Drawing.Point(16, 30);
            this.lblVolume.Name = "lblVolume";
            this.lblVolume.Size = new System.Drawing.Size(293, 48);
            this.lblVolume.TabIndex = 1;
            this.lblVolume.Text = "-";
            this.lblVolume.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatVolumeTitle.BackColor = System.Drawing.Color.White;
            this.lblStatVolumeTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatVolumeTitle.Font = new System.Drawing.Font("Hanken Grotesk", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatVolumeTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatVolumeTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatVolumeTitle.Name = "lblStatVolumeTitle";
            this.lblStatVolumeTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatVolumeTitle.TabIndex = 0;
            this.lblStatVolumeTitle.Text = "VOLUME";

            this.panelStatAvg.BackColor = System.Drawing.Color.White;
            this.panelStatAvg.Controls.Add(this.lblAvgTime);
            this.panelStatAvg.Controls.Add(this.lblStatAvgTitle);
            this.panelStatAvg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatAvg.Location = new System.Drawing.Point(337, 0);
            this.panelStatAvg.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatAvg.Name = "panelStatAvg";
            this.panelStatAvg.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatAvg.Size = new System.Drawing.Size(323, 108);
            this.panelStatAvg.TabIndex = 1;

            this.lblAvgTime.BackColor = System.Drawing.Color.White;
            this.lblAvgTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAvgTime.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblAvgTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblAvgTime.Location = new System.Drawing.Point(16, 30);
            this.lblAvgTime.Name = "lblAvgTime";
            this.lblAvgTime.Size = new System.Drawing.Size(293, 48);
            this.lblAvgTime.TabIndex = 1;
            this.lblAvgTime.Text = "-";
            this.lblAvgTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatAvgTitle.BackColor = System.Drawing.Color.White;
            this.lblStatAvgTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatAvgTitle.Font = new System.Drawing.Font("Hanken Grotesk", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatAvgTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatAvgTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatAvgTitle.Name = "lblStatAvgTitle";
            this.lblStatAvgTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatAvgTitle.TabIndex = 0;
            this.lblStatAvgTitle.Text = "AVG TIME";

            this.panelStatFlags.BackColor = System.Drawing.Color.White;
            this.panelStatFlags.Controls.Add(this.lblFlags);
            this.panelStatFlags.Controls.Add(this.lblStatFlagsTitle);
            this.panelStatFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatFlags.Location = new System.Drawing.Point(674, 0);
            this.panelStatFlags.Name = "panelStatFlags";
            this.panelStatFlags.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatFlags.Size = new System.Drawing.Size(338, 108);
            this.panelStatFlags.TabIndex = 2;

            this.lblFlags.BackColor = System.Drawing.Color.White;
            this.lblFlags.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblFlags.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblFlags.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblFlags.Location = new System.Drawing.Point(16, 30);
            this.lblFlags.Name = "lblFlags";
            this.lblFlags.Size = new System.Drawing.Size(308, 48);
            this.lblFlags.TabIndex = 1;
            this.lblFlags.Text = "-";
            this.lblFlags.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatFlagsTitle.BackColor = System.Drawing.Color.White;
            this.lblStatFlagsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatFlagsTitle.Font = new System.Drawing.Font("Hanken Grotesk", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblStatFlagsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatFlagsTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatFlagsTitle.Name = "lblStatFlagsTitle";
            this.lblStatFlagsTitle.Size = new System.Drawing.Size(308, 16);
            this.lblStatFlagsTitle.TabIndex = 0;
            this.lblStatFlagsTitle.Text = "FLAGS";

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
            this.Name = "ManageOrdersForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Manage Orders";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFilterOuter.ResumeLayout(false);
            this.panelFilterOuter.PerformLayout();
            this.flowFilterMain.ResumeLayout(false);
            this.flowFilterMain.PerformLayout();
            this.flowFilterActions.ResumeLayout(false);
            this.panelFieldDate.ResumeLayout(false);
            this.panelFieldDate.PerformLayout();
            this.flowDateRange.ResumeLayout(false);
            this.flowDateRange.PerformLayout();
            this.panelFieldStatus.ResumeLayout(false);
            this.panelFieldStatus.PerformLayout();
            this.panelFieldSearch.ResumeLayout(false);
            this.panelFieldSearch.PerformLayout();
            this.panelSearchWrap.ResumeLayout(false);
            this.panelSearchWrap.PerformLayout();
            this.panelGridOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.panelFooter.ResumeLayout(false);
            this.panelFooter.PerformLayout();
            this.flowPager.ResumeLayout(false);
            this.flowPager.PerformLayout();
            this.tableStatsRow.ResumeLayout(false);
            this.panelStatVolume.ResumeLayout(false);
            this.panelStatAvg.ResumeLayout(false);
            this.panelStatFlags.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelFilterOuter;
        private System.Windows.Forms.FlowLayoutPanel flowFilterMain;
        private System.Windows.Forms.FlowLayoutPanel flowFilterActions;
        private System.Windows.Forms.Button btnApplyFilters;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Panel panelFieldDate;
        private System.Windows.Forms.FlowLayoutPanel flowDateRange;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label lblDateTo;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblServiceDate;
        private System.Windows.Forms.Panel panelFieldStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Label lblStatusCategory;
        private System.Windows.Forms.Panel panelFieldSearch;
        private System.Windows.Forms.Panel panelSearchWrap;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearchOrders;
        private System.Windows.Forms.Label lblFilterBadge;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.FlowLayoutPanel flowPager;
        private System.Windows.Forms.Button btnPagePrev;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnPageNext;
        private System.Windows.Forms.TableLayoutPanel tableStatsRow;
        private System.Windows.Forms.Panel panelStatVolume;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblStatVolumeTitle;
        private System.Windows.Forms.Panel panelStatAvg;
        private System.Windows.Forms.Label lblAvgTime;
        private System.Windows.Forms.Label lblStatAvgTitle;
        private System.Windows.Forms.Panel panelStatFlags;
        private System.Windows.Forms.Label lblFlags;
        private System.Windows.Forms.Label lblStatFlagsTitle;
    }
}
