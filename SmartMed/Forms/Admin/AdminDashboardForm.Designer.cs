namespace SmartMed.UI
{
    partial class AdminDashboardForm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelScrollHost = new System.Windows.Forms.Panel();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.flowHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.tableStatsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatStock = new System.Windows.Forms.Panel();
            this.lblStockValue = new System.Windows.Forms.Label();
            this.lblStatStockTitle = new System.Windows.Forms.Label();
            this.panelStatOrders = new System.Windows.Forms.Panel();
            this.lblOrdersValue = new System.Windows.Forms.Label();
            this.lblStatOrdersTitle = new System.Windows.Forms.Label();
            this.panelStatSales = new System.Windows.Forms.Panel();
            this.lblSalesValue = new System.Windows.Forms.Label();
            this.lblStatSalesTitle = new System.Windows.Forms.Label();
            this.panelStatUsers = new System.Windows.Forms.Panel();
            this.lblCustomersValue = new System.Windows.Forms.Label();
            this.lblStatUsersTitle = new System.Windows.Forms.Label();
            this.tableAlertsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelLowStockOuter = new System.Windows.Forms.Panel();
            this.panelLowStockInner = new System.Windows.Forms.Panel();
            this.panelLowStockBody = new System.Windows.Forms.Panel();
            this.gridLowStock = new System.Windows.Forms.DataGridView();
            this.MedicineName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelLowStockHeader = new System.Windows.Forms.Panel();
            this.lblLowStockHeaderTitle = new System.Windows.Forms.Label();
            this.panelExpiryOuter = new System.Windows.Forms.Panel();
            this.panelExpiryInner = new System.Windows.Forms.Panel();
            this.panelExpiryBody = new System.Windows.Forms.Panel();
            this.gridExpiry = new System.Windows.Forms.DataGridView();
            this.BatchId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Medicine = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DueDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelExpiryHeader = new System.Windows.Forms.Panel();
            this.lblExpiryHeaderTitle = new System.Windows.Forms.Label();
            this.panelRecentOuter = new System.Windows.Forms.Panel();
            this.panelRecentInner = new System.Windows.Forms.Panel();
            this.panelRecentBody = new System.Windows.Forms.Panel();
            this.gridRecent = new System.Windows.Forms.DataGridView();
            this.OrderRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CustomerName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FulfillmentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Timestamp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelRecentHeader = new System.Windows.Forms.Panel();
            this.flowRecentHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblRecentHeaderTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowHeaderActions.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatStock.SuspendLayout();
            this.panelStatOrders.SuspendLayout();
            this.panelStatSales.SuspendLayout();
            this.panelStatUsers.SuspendLayout();
            this.tableAlertsRow.SuspendLayout();
            this.panelLowStockOuter.SuspendLayout();
            this.panelLowStockInner.SuspendLayout();
            this.panelLowStockBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStock)).BeginInit();
            this.panelLowStockHeader.SuspendLayout();
            this.panelExpiryOuter.SuspendLayout();
            this.panelExpiryInner.SuspendLayout();
            this.panelExpiryBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridExpiry)).BeginInit();
            this.panelExpiryHeader.SuspendLayout();
            this.panelRecentOuter.SuspendLayout();
            this.panelRecentInner.SuspendLayout();
            this.panelRecentBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecent)).BeginInit();
            this.panelRecentHeader.SuspendLayout();
            this.flowRecentHeaderActions.SuspendLayout();
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
            this.tableLayoutRoot.Controls.Add(this.tableAlertsRow, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.panelRecentOuter, 0, 3);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 4;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 210F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 672);
            this.tableLayoutRoot.TabIndex = 0;

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

            this.flowHeaderActions.AutoSize = true;
            this.flowHeaderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowHeaderActions.Controls.Add(this.btnRefresh);
            this.flowHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowHeaderActions.Location = new System.Drawing.Point(916, 0);
            this.flowHeaderActions.Name = "flowHeaderActions";
            this.flowHeaderActions.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flowHeaderActions.Size = new System.Drawing.Size(96, 76);
            this.flowHeaderActions.TabIndex = 2;
            this.flowHeaderActions.WrapContents = false;

            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefresh.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnRefresh.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnRefresh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnRefresh.Location = new System.Drawing.Point(0, 16);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(0);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);

            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(371, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Overview of pharmaceutical stock and fulfillment health.";

            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(390, 39);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Operational Dashboard";

            this.tableStatsRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableStatsRow.ColumnCount = 4;
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableStatsRow.Controls.Add(this.panelStatStock, 0, 0);
            this.tableStatsRow.Controls.Add(this.panelStatOrders, 1, 0);
            this.tableStatsRow.Controls.Add(this.panelStatSales, 2, 0);
            this.tableStatsRow.Controls.Add(this.panelStatUsers, 3, 0);
            this.tableStatsRow.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableStatsRow.Location = new System.Drawing.Point(0, 100);
            this.tableStatsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24);
            this.tableStatsRow.Name = "tableStatsRow";
            this.tableStatsRow.RowCount = 1;
            this.tableStatsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableStatsRow.Size = new System.Drawing.Size(1012, 108);
            this.tableStatsRow.TabIndex = 1;

            this.panelStatStock.BackColor = System.Drawing.Color.White;
            this.panelStatStock.Controls.Add(this.lblStockValue);
            this.panelStatStock.Controls.Add(this.lblStatStockTitle);
            this.panelStatStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatStock.Location = new System.Drawing.Point(0, 0);
            this.panelStatStock.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatStock.Name = "panelStatStock";
            this.panelStatStock.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatStock.Size = new System.Drawing.Size(239, 108);
            this.panelStatStock.TabIndex = 0;

            this.lblStockValue.BackColor = System.Drawing.Color.White;
            this.lblStockValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStockValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblStockValue.Location = new System.Drawing.Point(16, 30);
            this.lblStockValue.Name = "lblStockValue";
            this.lblStockValue.Size = new System.Drawing.Size(209, 64);
            this.lblStockValue.TabIndex = 1;
            this.lblStockValue.Text = "-";
            this.lblStockValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatStockTitle.BackColor = System.Drawing.Color.White;
            this.lblStatStockTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatStockTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatStockTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatStockTitle.Name = "lblStatStockTitle";
            this.lblStatStockTitle.Size = new System.Drawing.Size(209, 16);
            this.lblStatStockTitle.TabIndex = 0;
            this.lblStatStockTitle.Text = "STOCK ITEMS";

            this.panelStatOrders.BackColor = System.Drawing.Color.White;
            this.panelStatOrders.Controls.Add(this.lblOrdersValue);
            this.panelStatOrders.Controls.Add(this.lblStatOrdersTitle);
            this.panelStatOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatOrders.Location = new System.Drawing.Point(253, 0);
            this.panelStatOrders.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatOrders.Name = "panelStatOrders";
            this.panelStatOrders.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatOrders.Size = new System.Drawing.Size(239, 108);
            this.panelStatOrders.TabIndex = 1;

            this.lblOrdersValue.BackColor = System.Drawing.Color.White;
            this.lblOrdersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrdersValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblOrdersValue.Location = new System.Drawing.Point(16, 30);
            this.lblOrdersValue.Name = "lblOrdersValue";
            this.lblOrdersValue.Size = new System.Drawing.Size(209, 64);
            this.lblOrdersValue.TabIndex = 1;
            this.lblOrdersValue.Text = "-";
            this.lblOrdersValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatOrdersTitle.BackColor = System.Drawing.Color.White;
            this.lblStatOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatOrdersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatOrdersTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatOrdersTitle.Name = "lblStatOrdersTitle";
            this.lblStatOrdersTitle.Size = new System.Drawing.Size(209, 16);
            this.lblStatOrdersTitle.TabIndex = 0;
            this.lblStatOrdersTitle.Text = "PENDING ORDERS";

            this.panelStatSales.BackColor = System.Drawing.Color.White;
            this.panelStatSales.Controls.Add(this.lblSalesValue);
            this.panelStatSales.Controls.Add(this.lblStatSalesTitle);
            this.panelStatSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatSales.Location = new System.Drawing.Point(506, 0);
            this.panelStatSales.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatSales.Name = "panelStatSales";
            this.panelStatSales.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatSales.Size = new System.Drawing.Size(239, 108);
            this.panelStatSales.TabIndex = 2;

            this.lblSalesValue.BackColor = System.Drawing.Color.White;
            this.lblSalesValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSalesValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblSalesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblSalesValue.Location = new System.Drawing.Point(16, 30);
            this.lblSalesValue.Name = "lblSalesValue";
            this.lblSalesValue.Size = new System.Drawing.Size(209, 64);
            this.lblSalesValue.TabIndex = 1;
            this.lblSalesValue.Text = "-";
            this.lblSalesValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatSalesTitle.BackColor = System.Drawing.Color.White;
            this.lblStatSalesTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatSalesTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatSalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatSalesTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatSalesTitle.Name = "lblStatSalesTitle";
            this.lblStatSalesTitle.Size = new System.Drawing.Size(209, 16);
            this.lblStatSalesTitle.TabIndex = 0;
            this.lblStatSalesTitle.Text = "REVENUE (LKR)";

            this.panelStatUsers.BackColor = System.Drawing.Color.White;
            this.panelStatUsers.Controls.Add(this.lblCustomersValue);
            this.panelStatUsers.Controls.Add(this.lblStatUsersTitle);
            this.panelStatUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatUsers.Location = new System.Drawing.Point(759, 0);
            this.panelStatUsers.Margin = new System.Windows.Forms.Padding(0);
            this.panelStatUsers.Name = "panelStatUsers";
            this.panelStatUsers.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatUsers.Size = new System.Drawing.Size(253, 108);
            this.panelStatUsers.TabIndex = 3;

            this.lblCustomersValue.BackColor = System.Drawing.Color.White;
            this.lblCustomersValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCustomersValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 22F, System.Drawing.FontStyle.Bold);
            this.lblCustomersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblCustomersValue.Location = new System.Drawing.Point(16, 30);
            this.lblCustomersValue.Name = "lblCustomersValue";
            this.lblCustomersValue.Size = new System.Drawing.Size(223, 64);
            this.lblCustomersValue.TabIndex = 1;
            this.lblCustomersValue.Text = "-";
            this.lblCustomersValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatUsersTitle.BackColor = System.Drawing.Color.White;
            this.lblStatUsersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatUsersTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatUsersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatUsersTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatUsersTitle.Name = "lblStatUsersTitle";
            this.lblStatUsersTitle.Size = new System.Drawing.Size(223, 16);
            this.lblStatUsersTitle.TabIndex = 0;
            this.lblStatUsersTitle.Text = "ACTIVE USERS";

            this.tableAlertsRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableAlertsRow.ColumnCount = 2;
            this.tableAlertsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableAlertsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableAlertsRow.Controls.Add(this.panelLowStockOuter, 0, 0);
            this.tableAlertsRow.Controls.Add(this.panelExpiryOuter, 1, 0);
            this.tableAlertsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableAlertsRow.Location = new System.Drawing.Point(0, 232);
            this.tableAlertsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 24);
            this.tableAlertsRow.Name = "tableAlertsRow";
            this.tableAlertsRow.RowCount = 1;
            this.tableAlertsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableAlertsRow.Size = new System.Drawing.Size(1012, 186);
            this.tableAlertsRow.TabIndex = 2;

            this.panelLowStockOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.panelLowStockOuter.Controls.Add(this.panelLowStockInner);
            this.panelLowStockOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLowStockOuter.Location = new System.Drawing.Point(0, 0);
            this.panelLowStockOuter.Margin = new System.Windows.Forms.Padding(0);
            this.panelLowStockOuter.Name = "panelLowStockOuter";
            this.panelLowStockOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelLowStockOuter.Size = new System.Drawing.Size(506, 186);
            this.panelLowStockOuter.TabIndex = 0;

            this.panelLowStockInner.BackColor = System.Drawing.Color.White;
            this.panelLowStockInner.Controls.Add(this.panelLowStockBody);
            this.panelLowStockInner.Controls.Add(this.panelLowStockHeader);
            this.panelLowStockInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLowStockInner.Location = new System.Drawing.Point(1, 1);
            this.panelLowStockInner.Name = "panelLowStockInner";
            this.panelLowStockInner.Size = new System.Drawing.Size(504, 184);
            this.panelLowStockInner.TabIndex = 0;

            this.panelLowStockBody.BackColor = System.Drawing.Color.White;
            this.panelLowStockBody.Controls.Add(this.gridLowStock);
            this.panelLowStockBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLowStockBody.Location = new System.Drawing.Point(0, 32);
            this.panelLowStockBody.Name = "panelLowStockBody";
            this.panelLowStockBody.Size = new System.Drawing.Size(504, 152);
            this.panelLowStockBody.TabIndex = 1;

            this.gridLowStock.AllowUserToAddRows = false;
            this.gridLowStock.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridLowStock.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridLowStock.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridLowStock.BackgroundColor = System.Drawing.Color.White;
            this.gridLowStock.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridLowStock.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridLowStock.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridLowStock.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridLowStock.ColumnHeadersHeight = 36;
            this.gridLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridLowStock.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MedicineName,
            this.Level,
            this.Status});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridLowStock.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLowStock.EnableHeadersVisualStyles = false;
            this.gridLowStock.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridLowStock.Location = new System.Drawing.Point(0, 0);
            this.gridLowStock.Name = "gridLowStock";
            this.gridLowStock.ReadOnly = true;
            this.gridLowStock.RowHeadersVisible = false;
            this.gridLowStock.RowHeadersWidth = 51;
            this.gridLowStock.RowTemplate.Height = 36;
            this.gridLowStock.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridLowStock.Size = new System.Drawing.Size(504, 152);
            this.gridLowStock.TabIndex = 0;
            this.gridLowStock.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridLowStock_CellFormatting);

            this.MedicineName.DataPropertyName = "MedicineName";
            this.MedicineName.HeaderText = "Medicine";
            this.MedicineName.MinimumWidth = 6;
            this.MedicineName.Name = "MedicineName";
            this.MedicineName.ReadOnly = true;

            this.Level.DataPropertyName = "Level";
            this.Level.HeaderText = "Level";
            this.Level.MinimumWidth = 6;
            this.Level.Name = "Level";
            this.Level.ReadOnly = true;

            this.Status.DataPropertyName = "Status";
            this.Status.HeaderText = "Status";
            this.Status.MinimumWidth = 6;
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;

            this.panelLowStockHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(243)))));
            this.panelLowStockHeader.Controls.Add(this.lblLowStockHeaderTitle);
            this.panelLowStockHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLowStockHeader.Location = new System.Drawing.Point(0, 0);
            this.panelLowStockHeader.Name = "panelLowStockHeader";
            this.panelLowStockHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 4);
            this.panelLowStockHeader.Size = new System.Drawing.Size(504, 32);
            this.panelLowStockHeader.TabIndex = 0;

            this.lblLowStockHeaderTitle.AutoSize = true;
            this.lblLowStockHeaderTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(243)))));
            this.lblLowStockHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblLowStockHeaderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblLowStockHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblLowStockHeaderTitle.Location = new System.Drawing.Point(12, 8);
            this.lblLowStockHeaderTitle.Name = "lblLowStockHeaderTitle";
            this.lblLowStockHeaderTitle.Size = new System.Drawing.Size(141, 17);
            this.lblLowStockHeaderTitle.TabIndex = 0;
            this.lblLowStockHeaderTitle.Text = "CRITICAL ALERTS";

            this.panelExpiryOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.panelExpiryOuter.Controls.Add(this.panelExpiryInner);
            this.panelExpiryOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpiryOuter.Location = new System.Drawing.Point(520, 0);
            this.panelExpiryOuter.Margin = new System.Windows.Forms.Padding(14, 0, 0, 0);
            this.panelExpiryOuter.Name = "panelExpiryOuter";
            this.panelExpiryOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelExpiryOuter.Size = new System.Drawing.Size(492, 186);
            this.panelExpiryOuter.TabIndex = 1;

            this.panelExpiryInner.BackColor = System.Drawing.Color.White;
            this.panelExpiryInner.Controls.Add(this.panelExpiryBody);
            this.panelExpiryInner.Controls.Add(this.panelExpiryHeader);
            this.panelExpiryInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpiryInner.Location = new System.Drawing.Point(1, 1);
            this.panelExpiryInner.Name = "panelExpiryInner";
            this.panelExpiryInner.Size = new System.Drawing.Size(490, 184);
            this.panelExpiryInner.TabIndex = 0;

            this.panelExpiryBody.BackColor = System.Drawing.Color.White;
            this.panelExpiryBody.Controls.Add(this.gridExpiry);
            this.panelExpiryBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpiryBody.Location = new System.Drawing.Point(0, 32);
            this.panelExpiryBody.Name = "panelExpiryBody";
            this.panelExpiryBody.Size = new System.Drawing.Size(490, 152);
            this.panelExpiryBody.TabIndex = 1;

            this.gridExpiry.AllowUserToAddRows = false;
            this.gridExpiry.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridExpiry.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridExpiry.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridExpiry.BackgroundColor = System.Drawing.Color.White;
            this.gridExpiry.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridExpiry.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridExpiry.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridExpiry.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridExpiry.ColumnHeadersHeight = 36;
            this.gridExpiry.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridExpiry.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BatchId,
            this.Medicine,
            this.DueDate});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridExpiry.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridExpiry.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridExpiry.EnableHeadersVisualStyles = false;
            this.gridExpiry.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridExpiry.Location = new System.Drawing.Point(0, 0);
            this.gridExpiry.Name = "gridExpiry";
            this.gridExpiry.ReadOnly = true;
            this.gridExpiry.RowHeadersVisible = false;
            this.gridExpiry.RowHeadersWidth = 51;
            this.gridExpiry.RowTemplate.Height = 36;
            this.gridExpiry.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridExpiry.Size = new System.Drawing.Size(490, 152);
            this.gridExpiry.TabIndex = 0;
            this.gridExpiry.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridExpiry_CellFormatting);

            this.BatchId.DataPropertyName = "BatchId";
            this.BatchId.HeaderText = "Batch";
            this.BatchId.MinimumWidth = 6;
            this.BatchId.Name = "BatchId";
            this.BatchId.ReadOnly = true;

            this.Medicine.DataPropertyName = "Medicine";
            this.Medicine.HeaderText = "Medicine";
            this.Medicine.MinimumWidth = 6;
            this.Medicine.Name = "Medicine";
            this.Medicine.ReadOnly = true;

            this.DueDate.DataPropertyName = "DueDate";
            this.DueDate.HeaderText = "Due Date";
            this.DueDate.MinimumWidth = 6;
            this.DueDate.Name = "DueDate";
            this.DueDate.ReadOnly = true;

            this.panelExpiryHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(240)))));
            this.panelExpiryHeader.Controls.Add(this.lblExpiryHeaderTitle);
            this.panelExpiryHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelExpiryHeader.Location = new System.Drawing.Point(0, 0);
            this.panelExpiryHeader.Name = "panelExpiryHeader";
            this.panelExpiryHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 4);
            this.panelExpiryHeader.Size = new System.Drawing.Size(490, 32);
            this.panelExpiryHeader.TabIndex = 0;

            this.lblExpiryHeaderTitle.AutoSize = true;
            this.lblExpiryHeaderTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(240)))));
            this.lblExpiryHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblExpiryHeaderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblExpiryHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(57)))), ((int)(((byte)(61)))));
            this.lblExpiryHeaderTitle.Location = new System.Drawing.Point(12, 8);
            this.lblExpiryHeaderTitle.Name = "lblExpiryHeaderTitle";
            this.lblExpiryHeaderTitle.Size = new System.Drawing.Size(151, 17);
            this.lblExpiryHeaderTitle.TabIndex = 0;
            this.lblExpiryHeaderTitle.Text = "EXPIRY WARNINGS";

            this.panelRecentOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.panelRecentOuter.Controls.Add(this.panelRecentInner);
            this.panelRecentOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecentOuter.Location = new System.Drawing.Point(0, 442);
            this.panelRecentOuter.Margin = new System.Windows.Forms.Padding(0);
            this.panelRecentOuter.Name = "panelRecentOuter";
            this.panelRecentOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelRecentOuter.Size = new System.Drawing.Size(1012, 230);
            this.panelRecentOuter.TabIndex = 3;

            this.panelRecentInner.BackColor = System.Drawing.Color.White;
            this.panelRecentInner.Controls.Add(this.panelRecentBody);
            this.panelRecentInner.Controls.Add(this.panelRecentHeader);
            this.panelRecentInner.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecentInner.Location = new System.Drawing.Point(1, 1);
            this.panelRecentInner.Name = "panelRecentInner";
            this.panelRecentInner.Size = new System.Drawing.Size(1010, 228);
            this.panelRecentInner.TabIndex = 0;

            this.panelRecentBody.BackColor = System.Drawing.Color.White;
            this.panelRecentBody.Controls.Add(this.gridRecent);
            this.panelRecentBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecentBody.Location = new System.Drawing.Point(0, 36);
            this.panelRecentBody.Name = "panelRecentBody";
            this.panelRecentBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelRecentBody.Size = new System.Drawing.Size(1010, 192);
            this.panelRecentBody.TabIndex = 1;

            this.gridRecent.AllowUserToAddRows = false;
            this.gridRecent.AllowUserToDeleteRows = false;
            this.gridRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRecent.BackgroundColor = System.Drawing.Color.White;
            this.gridRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRecent.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridRecent.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gridRecent.ColumnHeadersHeight = 36;
            this.gridRecent.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridRecent.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.OrderRef,
            this.CustomerName,
            this.FulfillmentStatus,
            this.Timestamp});
            this.gridRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRecent.EnableHeadersVisualStyles = false;
            this.gridRecent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridRecent.Location = new System.Drawing.Point(0, 4);
            this.gridRecent.Name = "gridRecent";
            this.gridRecent.ReadOnly = true;
            this.gridRecent.RowHeadersVisible = false;
            this.gridRecent.RowHeadersWidth = 51;
            this.gridRecent.RowTemplate.Height = 36;
            this.gridRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRecent.Size = new System.Drawing.Size(1010, 188);
            this.gridRecent.TabIndex = 0;
            this.gridRecent.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridRecent_CellContentClick);
            this.gridRecent.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridRecent_CellFormatting);

            this.OrderRef.DataPropertyName = "OrderRef";
            this.OrderRef.HeaderText = "Order Ref";
            this.OrderRef.MinimumWidth = 6;
            this.OrderRef.Name = "OrderRef";
            this.OrderRef.ReadOnly = true;

            this.CustomerName.DataPropertyName = "CustomerName";
            this.CustomerName.HeaderText = "Customer";
            this.CustomerName.MinimumWidth = 6;
            this.CustomerName.Name = "CustomerName";
            this.CustomerName.ReadOnly = true;

            this.FulfillmentStatus.DataPropertyName = "FulfillmentStatus";
            this.FulfillmentStatus.HeaderText = "Status";
            this.FulfillmentStatus.MinimumWidth = 6;
            this.FulfillmentStatus.Name = "FulfillmentStatus";
            this.FulfillmentStatus.ReadOnly = true;

            this.Timestamp.DataPropertyName = "Timestamp";
            this.Timestamp.HeaderText = "Timestamp";
            this.Timestamp.MinimumWidth = 6;
            this.Timestamp.Name = "Timestamp";
            this.Timestamp.ReadOnly = true;

            this.panelRecentHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelRecentHeader.Controls.Add(this.flowRecentHeaderActions);
            this.panelRecentHeader.Controls.Add(this.lblRecentHeaderTitle);
            this.panelRecentHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecentHeader.Location = new System.Drawing.Point(0, 0);
            this.panelRecentHeader.Name = "panelRecentHeader";
            this.panelRecentHeader.Padding = new System.Windows.Forms.Padding(12, 8, 10, 4);
            this.panelRecentHeader.Size = new System.Drawing.Size(1010, 36);
            this.panelRecentHeader.TabIndex = 0;

            this.flowRecentHeaderActions.AutoSize = true;
            this.flowRecentHeaderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.flowRecentHeaderActions.Controls.Add(this.btnPrint);
            this.flowRecentHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowRecentHeaderActions.Location = new System.Drawing.Point(928, 8);
            this.flowRecentHeaderActions.Name = "flowRecentHeaderActions";
            this.flowRecentHeaderActions.Size = new System.Drawing.Size(72, 24);
            this.flowRecentHeaderActions.TabIndex = 1;
            this.flowRecentHeaderActions.WrapContents = false;

            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPrint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(234)))), ((int)(((byte)(233)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPrint.Location = new System.Drawing.Point(0, 0);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(0);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(72, 28);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);

            this.lblRecentHeaderTitle.AutoSize = true;
            this.lblRecentHeaderTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblRecentHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblRecentHeaderTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecentHeaderTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblRecentHeaderTitle.Location = new System.Drawing.Point(12, 8);
            this.lblRecentHeaderTitle.Name = "lblRecentHeaderTitle";
            this.lblRecentHeaderTitle.Size = new System.Drawing.Size(251, 17);
            this.lblRecentHeaderTitle.TabIndex = 0;
            this.lblRecentHeaderTitle.Text = "RECENT FULFILLMENT ACTIVITY";

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.Controls.Add(this.panelScrollHost);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.Name = "AdminDashboardForm";
            this.Text = "Operational Dashboard";
            this.panelScrollHost.ResumeLayout(false);
            this.tableLayoutRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowHeaderActions.ResumeLayout(false);
            this.tableStatsRow.ResumeLayout(false);
            this.panelStatStock.ResumeLayout(false);
            this.panelStatOrders.ResumeLayout(false);
            this.panelStatSales.ResumeLayout(false);
            this.panelStatUsers.ResumeLayout(false);
            this.tableAlertsRow.ResumeLayout(false);
            this.panelLowStockOuter.ResumeLayout(false);
            this.panelLowStockInner.ResumeLayout(false);
            this.panelLowStockBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLowStock)).EndInit();
            this.panelLowStockHeader.ResumeLayout(false);
            this.panelLowStockHeader.PerformLayout();
            this.panelExpiryOuter.ResumeLayout(false);
            this.panelExpiryInner.ResumeLayout(false);
            this.panelExpiryBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridExpiry)).EndInit();
            this.panelExpiryHeader.ResumeLayout(false);
            this.panelExpiryHeader.PerformLayout();
            this.panelRecentOuter.ResumeLayout(false);
            this.panelRecentInner.ResumeLayout(false);
            this.panelRecentBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRecent)).EndInit();
            this.panelRecentHeader.ResumeLayout(false);
            this.panelRecentHeader.PerformLayout();
            this.flowRecentHeaderActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flowHeaderActions;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.TableLayoutPanel tableStatsRow;
        private System.Windows.Forms.Panel panelStatStock;
        private System.Windows.Forms.Label lblStatStockTitle;
        private System.Windows.Forms.Label lblStockValue;
        private System.Windows.Forms.Panel panelStatOrders;
        private System.Windows.Forms.Label lblStatOrdersTitle;
        private System.Windows.Forms.Label lblOrdersValue;
        private System.Windows.Forms.Panel panelStatSales;
        private System.Windows.Forms.Label lblStatSalesTitle;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.Panel panelStatUsers;
        private System.Windows.Forms.Label lblStatUsersTitle;
        private System.Windows.Forms.Label lblCustomersValue;
        private System.Windows.Forms.TableLayoutPanel tableAlertsRow;
        private System.Windows.Forms.Panel panelLowStockOuter;
        private System.Windows.Forms.Panel panelLowStockInner;
        private System.Windows.Forms.Panel panelLowStockHeader;
        private System.Windows.Forms.Label lblLowStockHeaderTitle;
        private System.Windows.Forms.Panel panelLowStockBody;
        private System.Windows.Forms.DataGridView gridLowStock;
        private System.Windows.Forms.Panel panelExpiryOuter;
        private System.Windows.Forms.Panel panelExpiryInner;
        private System.Windows.Forms.Panel panelExpiryHeader;
        private System.Windows.Forms.Label lblExpiryHeaderTitle;
        private System.Windows.Forms.Panel panelExpiryBody;
        private System.Windows.Forms.DataGridView gridExpiry;
        private System.Windows.Forms.Panel panelRecentOuter;
        private System.Windows.Forms.Panel panelRecentInner;
        private System.Windows.Forms.Panel panelRecentHeader;
        private System.Windows.Forms.Label lblRecentHeaderTitle;
        private System.Windows.Forms.FlowLayoutPanel flowRecentHeaderActions;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panelRecentBody;
        private System.Windows.Forms.DataGridView gridRecent;
        private System.Windows.Forms.DataGridViewTextBoxColumn MedicineName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Level;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn BatchId;
        private System.Windows.Forms.DataGridViewTextBoxColumn Medicine;
        private System.Windows.Forms.DataGridViewTextBoxColumn DueDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn OrderRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn CustomerName;
        private System.Windows.Forms.DataGridViewTextBoxColumn FulfillmentStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Timestamp;
    }
}
