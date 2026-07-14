namespace SmartMed.UI
{
    partial class ManageMedicinesForm
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
            this.panelScrollHost = new System.Windows.Forms.Panel();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.flowHeaderActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnViewExpiryAlerts = new System.Windows.Forms.Button();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.gridMedicines = new System.Windows.Forms.DataGridView();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.tableGridHeader = new System.Windows.Forms.TableLayoutPanel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.flowGridFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategoryLabel = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPriceLabel = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.tableStatsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatTotal = new System.Windows.Forms.Panel();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.lblStatTotalTitle = new System.Windows.Forms.Label();
            this.panelStatLow = new System.Windows.Forms.Panel();
            this.lblLowStock = new System.Windows.Forms.Label();
            this.lblStatLowTitle = new System.Windows.Forms.Label();
            this.panelStatCompliance = new System.Windows.Forms.Panel();
            this.lblExpiringSoon = new System.Windows.Forms.Label();
            this.lblStatComplianceTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowHeaderActions.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedicines)).BeginInit();
            this.panelGridHeader.SuspendLayout();
            this.tableGridHeader.SuspendLayout();
            this.flowGridFilters.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatTotal.SuspendLayout();
            this.panelStatLow.SuspendLayout();
            this.panelStatCompliance.SuspendLayout();
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
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 2);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 3;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 720);
            this.tableLayoutRoot.TabIndex = 0;

            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.flowHeaderActions);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;

            this.flowHeaderActions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowHeaderActions.AutoSize = true;
            this.flowHeaderActions.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowHeaderActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowHeaderActions.Controls.Add(this.btnExport);
            this.flowHeaderActions.Controls.Add(this.btnAdd);
            this.flowHeaderActions.Controls.Add(this.btnViewExpiryAlerts);
            this.flowHeaderActions.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowHeaderActions.Location = new System.Drawing.Point(560, 8);
            this.flowHeaderActions.Name = "flowHeaderActions";
            this.flowHeaderActions.Size = new System.Drawing.Size(452, 36);
            this.flowHeaderActions.TabIndex = 2;
            this.flowHeaderActions.WrapContents = false;

            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExport.Location = new System.Drawing.Point(0, 3);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(110, 30);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export PDF";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);

            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(106, 3);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 30);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "+ Add Medicine";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);

            this.btnViewExpiryAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnViewExpiryAlerts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewExpiryAlerts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnViewExpiryAlerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewExpiryAlerts.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnViewExpiryAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnViewExpiryAlerts.Location = new System.Drawing.Point(246, 3);
            this.btnViewExpiryAlerts.Margin = new System.Windows.Forms.Padding(0);
            this.btnViewExpiryAlerts.Name = "btnViewExpiryAlerts";
            this.btnViewExpiryAlerts.Size = new System.Drawing.Size(116, 30);
            this.btnViewExpiryAlerts.TabIndex = 2;
            this.btnViewExpiryAlerts.Text = "Alerts (0)";
            this.btnViewExpiryAlerts.UseVisualStyleBackColor = false;
            this.btnViewExpiryAlerts.Click += new System.EventHandler(this.BtnViewExpiryAlerts_Click);

            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(360, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Update and monitor pharmaceutical inventory levels.";

            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(230, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Manage Medicines";

            this.panelGridOuter.BackColor = System.Drawing.Color.White;
            this.panelGridOuter.Controls.Add(this.panelGridBody);
            this.panelGridOuter.Controls.Add(this.panelGridHeader);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 220);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 448);
            this.panelGridOuter.TabIndex = 3;

            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.gridMedicines);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(1, 45);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 406);
            this.panelGridBody.TabIndex = 1;

            this.gridMedicines.AllowUserToAddRows = false;
            this.gridMedicines.AllowUserToDeleteRows = false;
            this.gridMedicines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridMedicines.BackgroundColor = System.Drawing.Color.White;
            this.gridMedicines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridMedicines.ColumnHeadersHeight = 36;
            this.gridMedicines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMedicines.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridMedicines.Location = new System.Drawing.Point(0, 4);
            this.gridMedicines.MultiSelect = false;
            this.gridMedicines.Name = "gridMedicines";
            this.gridMedicines.ReadOnly = true;
            this.gridMedicines.RowHeadersVisible = false;
            this.gridMedicines.RowTemplate.Height = 36;
            this.gridMedicines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMedicines.Size = new System.Drawing.Size(1010, 406);
            this.gridMedicines.TabIndex = 0;
            this.gridMedicines.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridMedicines_CellContentClick);
            this.gridMedicines.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridMedicines_CellFormatting);
            this.gridMedicines.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.GridMedicines_RowPrePaint);
            this.gridMedicines.SelectionChanged += new System.EventHandler(this.GridMedicines_SelectionChanged);

            this.panelGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelGridHeader.Controls.Add(this.tableGridHeader);
            this.panelGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGridHeader.Location = new System.Drawing.Point(1, 1);
            this.panelGridHeader.MinimumSize = new System.Drawing.Size(0, 40);
            this.panelGridHeader.Name = "panelGridHeader";
            this.panelGridHeader.Padding = new System.Windows.Forms.Padding(12, 6, 10, 4);
            this.panelGridHeader.Size = new System.Drawing.Size(1010, 40);
            this.panelGridHeader.TabIndex = 0;

            this.tableGridHeader.AutoSize = true;
            this.tableGridHeader.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.tableGridHeader.ColumnCount = 2;
            this.tableGridHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableGridHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableGridHeader.Controls.Add(this.lblGridTitle, 0, 0);
            this.tableGridHeader.Controls.Add(this.flowGridFilters, 1, 0);
            this.tableGridHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableGridHeader.Location = new System.Drawing.Point(12, 6);
            this.tableGridHeader.Name = "tableGridHeader";
            this.tableGridHeader.RowCount = 1;
            this.tableGridHeader.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableGridHeader.Size = new System.Drawing.Size(988, 30);
            this.tableGridHeader.TabIndex = 0;

            this.lblGridTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Top)));
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblGridTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblGridTitle.Location = new System.Drawing.Point(0, 8);
            this.lblGridTitle.Margin = new System.Windows.Forms.Padding(0, 6, 12, 0);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(128, 16);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Medicine Inventory";
            this.lblGridTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.flowGridFilters.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowGridFilters.AutoSize = true;
            this.flowGridFilters.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowGridFilters.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.flowGridFilters.Controls.Add(this.lblCategoryLabel);
            this.flowGridFilters.Controls.Add(this.cmbCategory);
            this.flowGridFilters.Controls.Add(this.txtSearch);
            this.flowGridFilters.Controls.Add(this.btnSearch);
            this.flowGridFilters.Controls.Add(this.lblPriceLabel);
            this.flowGridFilters.Controls.Add(this.txtMinPrice);
            this.flowGridFilters.Controls.Add(this.txtMaxPrice);
            this.flowGridFilters.Controls.Add(this.btnClear);
            this.flowGridFilters.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowGridFilters.Location = new System.Drawing.Point(116, 0);
            this.flowGridFilters.Margin = new System.Windows.Forms.Padding(0);
            this.flowGridFilters.MinimumSize = new System.Drawing.Size(720, 30);
            this.flowGridFilters.Name = "flowGridFilters";
            this.flowGridFilters.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.flowGridFilters.Size = new System.Drawing.Size(872, 30);
            this.flowGridFilters.TabIndex = 1;
            this.flowGridFilters.WrapContents = false;

            this.lblCategoryLabel.AutoSize = true;
            this.lblCategoryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblCategoryLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblCategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblCategoryLabel.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblCategoryLabel.Name = "lblCategoryLabel";
            this.lblCategoryLabel.Size = new System.Drawing.Size(68, 18);
            this.lblCategoryLabel.TabIndex = 0;
            this.lblCategoryLabel.Text = "Category:";

            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "All categories"});
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(130, 26);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.CmbCategory_SelectedIndexChanged);

            this.txtSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(140, 25);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);

            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 26);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);

            this.lblPriceLabel.AutoSize = true;
            this.lblPriceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblPriceLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPriceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPriceLabel.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblPriceLabel.Name = "lblPriceLabel";
            this.lblPriceLabel.Size = new System.Drawing.Size(40, 18);
            this.lblPriceLabel.TabIndex = 5;
            this.lblPriceLabel.Text = "Price:";

            this.txtMinPrice.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtMinPrice.Margin = new System.Windows.Forms.Padding(0, 2, 4, 0);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.Size = new System.Drawing.Size(56, 25);
            this.txtMinPrice.TabIndex = 6;
            this.txtMinPrice.TextChanged += new System.EventHandler(this.TxtMinPrice_TextChanged);

            this.txtMaxPrice.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtMaxPrice.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.Size = new System.Drawing.Size(56, 25);
            this.txtMaxPrice.TabIndex = 7;
            this.txtMaxPrice.TextChanged += new System.EventHandler(this.TxtMaxPrice_TextChanged);

            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnClear.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 26);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);

            this.tableStatsRow.ColumnCount = 3;
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableStatsRow.Controls.Add(this.panelStatTotal, 0, 0);
            this.tableStatsRow.Controls.Add(this.panelStatLow, 1, 0);
            this.tableStatsRow.Controls.Add(this.panelStatCompliance, 2, 0);
            this.tableStatsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStatsRow.Location = new System.Drawing.Point(0, 96);
            this.tableStatsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.tableStatsRow.Name = "tableStatsRow";
            this.tableStatsRow.RowCount = 1;
            this.tableStatsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableStatsRow.Size = new System.Drawing.Size(1012, 108);
            this.tableStatsRow.TabIndex = 4;

            this.panelStatTotal.BackColor = System.Drawing.Color.White;
            this.panelStatTotal.Controls.Add(this.lblTotalItems);
            this.panelStatTotal.Controls.Add(this.lblStatTotalTitle);
            this.panelStatTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatTotal.Location = new System.Drawing.Point(0, 0);
            this.panelStatTotal.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatTotal.Name = "panelStatTotal";
            this.panelStatTotal.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatTotal.Size = new System.Drawing.Size(323, 108);
            this.panelStatTotal.TabIndex = 0;

            this.lblTotalItems.BackColor = System.Drawing.Color.White;
            this.lblTotalItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalItems.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblTotalItems.Location = new System.Drawing.Point(16, 30);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(293, 64);
            this.lblTotalItems.TabIndex = 1;
            this.lblTotalItems.Text = "-";
            this.lblTotalItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatTotalTitle.BackColor = System.Drawing.Color.White;
            this.lblStatTotalTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTotalTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTotalTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatTotalTitle.Name = "lblStatTotalTitle";
            this.lblStatTotalTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatTotalTitle.TabIndex = 0;
            this.lblStatTotalTitle.Text = "TOTAL ITEMS";

            this.panelStatLow.BackColor = System.Drawing.Color.White;
            this.panelStatLow.Controls.Add(this.lblLowStock);
            this.panelStatLow.Controls.Add(this.lblStatLowTitle);
            this.panelStatLow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatLow.Location = new System.Drawing.Point(337, 0);
            this.panelStatLow.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatLow.Name = "panelStatLow";
            this.panelStatLow.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatLow.Size = new System.Drawing.Size(323, 108);
            this.panelStatLow.TabIndex = 1;

            this.lblLowStock.BackColor = System.Drawing.Color.White;
            this.lblLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowStock.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblLowStock.Location = new System.Drawing.Point(16, 30);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(293, 64);
            this.lblLowStock.TabIndex = 1;
            this.lblLowStock.Text = "-";
            this.lblLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatLowTitle.BackColor = System.Drawing.Color.White;
            this.lblStatLowTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatLowTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatLowTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatLowTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatLowTitle.Name = "lblStatLowTitle";
            this.lblStatLowTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatLowTitle.TabIndex = 0;
            this.lblStatLowTitle.Text = "LOW STOCK";

            this.panelStatCompliance.BackColor = System.Drawing.Color.White;
            this.panelStatCompliance.Controls.Add(this.lblExpiringSoon);
            this.panelStatCompliance.Controls.Add(this.lblStatComplianceTitle);
            this.panelStatCompliance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatCompliance.Location = new System.Drawing.Point(674, 0);
            this.panelStatCompliance.Name = "panelStatCompliance";
            this.panelStatCompliance.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatCompliance.Size = new System.Drawing.Size(338, 108);
            this.panelStatCompliance.TabIndex = 2;

            this.lblExpiringSoon.BackColor = System.Drawing.Color.White;
            this.lblExpiringSoon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpiringSoon.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblExpiringSoon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblExpiringSoon.Location = new System.Drawing.Point(16, 30);
            this.lblExpiringSoon.Name = "lblExpiringSoon";
            this.lblExpiringSoon.Size = new System.Drawing.Size(308, 64);
            this.lblExpiringSoon.TabIndex = 1;
            this.lblExpiringSoon.Text = "-";
            this.lblExpiringSoon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.lblStatComplianceTitle.BackColor = System.Drawing.Color.White;
            this.lblStatComplianceTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatComplianceTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatComplianceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatComplianceTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatComplianceTitle.Name = "lblStatComplianceTitle";
            this.lblStatComplianceTitle.Size = new System.Drawing.Size(308, 16);
            this.lblStatComplianceTitle.TabIndex = 0;
            this.lblStatComplianceTitle.Text = "COMPLIANCE";

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
            this.Name = "ManageMedicinesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Manage Medicines";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowHeaderActions.ResumeLayout(false);
            this.flowHeaderActions.PerformLayout();
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMedicines)).EndInit();
            this.panelGridHeader.ResumeLayout(false);
            this.panelGridHeader.PerformLayout();
            this.tableGridHeader.ResumeLayout(false);
            this.tableGridHeader.PerformLayout();
            this.flowGridFilters.ResumeLayout(false);
            this.flowGridFilters.PerformLayout();
            this.tableStatsRow.ResumeLayout(false);
            this.panelStatTotal.ResumeLayout(false);
            this.panelStatLow.ResumeLayout(false);
            this.panelStatCompliance.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.FlowLayoutPanel flowHeaderActions;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnViewExpiryAlerts;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label lblPriceLabel;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView gridMedicines;
        private System.Windows.Forms.Panel panelGridHeader;
        private System.Windows.Forms.TableLayoutPanel tableGridHeader;
        private System.Windows.Forms.FlowLayoutPanel flowGridFilters;
        private System.Windows.Forms.Label lblCategoryLabel;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.TableLayoutPanel tableStatsRow;
        private System.Windows.Forms.Panel panelStatTotal;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label lblStatTotalTitle;
        private System.Windows.Forms.Panel panelStatLow;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label lblStatLowTitle;
        private System.Windows.Forms.Panel panelStatCompliance;
        private System.Windows.Forms.Label lblExpiringSoon;
        private System.Windows.Forms.Label lblStatComplianceTitle;
    }
}
