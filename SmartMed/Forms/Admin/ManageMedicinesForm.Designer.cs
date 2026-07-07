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
            this.btnPrint = new System.Windows.Forms.Button();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelToolbar = new System.Windows.Forms.Panel();
            this.flowToolbarRight = new System.Windows.Forms.FlowLayoutPanel();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.lblPriceLabel = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategoryLabel = new System.Windows.Forms.Label();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblNameLabel = new System.Windows.Forms.Label();
            this.flowToolbarLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnReload = new System.Windows.Forms.Button();
            this.panelExpiryAlerts = new System.Windows.Forms.Panel();
            this.lblExpirySummary = new System.Windows.Forms.Label();
            this.btnViewExpiryAlerts = new System.Windows.Forms.Button();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.gridMedicines = new System.Windows.Forms.DataGridView();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
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
            this.panelToolbar.SuspendLayout();
            this.flowToolbarRight.SuspendLayout();
            this.flowToolbarLeft.SuspendLayout();
            this.panelExpiryAlerts.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMedicines)).BeginInit();
            this.panelGridHeader.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatTotal.SuspendLayout();
            this.panelStatLow.SuspendLayout();
            this.panelStatCompliance.SuspendLayout();
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
            this.tableLayoutRoot.AutoSize = true;
            this.tableLayoutRoot.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.tableStatsRow, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelToolbar, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.panelExpiryAlerts, 0, 3);
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 4);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.MinimumSize = new System.Drawing.Size(0, 720);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 5;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 720);
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
            this.flowHeaderActions.Controls.Add(this.btnExport);
            this.flowHeaderActions.Controls.Add(this.btnPrint);
            this.flowHeaderActions.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowHeaderActions.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowHeaderActions.Location = new System.Drawing.Point(820, 0);
            this.flowHeaderActions.Name = "flowHeaderActions";
            this.flowHeaderActions.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flowHeaderActions.Size = new System.Drawing.Size(192, 46);
            this.flowHeaderActions.TabIndex = 2;
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExport.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExport.Location = new System.Drawing.Point(0, 16);
            this.btnExport.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(96, 30);
            this.btnExport.TabIndex = 0;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = false;
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnPrint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnPrint.Location = new System.Drawing.Point(106, 16);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(96, 30);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = false;
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(360, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Update and monitor pharmaceutical inventory levels.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(230, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Manage Medicines";
            // 
            // panelToolbar
            // 
            this.panelToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelToolbar.Controls.Add(this.flowToolbarRight);
            this.panelToolbar.Controls.Add(this.flowToolbarLeft);
            this.panelToolbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelToolbar.Location = new System.Drawing.Point(0, 96);
            this.panelToolbar.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelToolbar.Name = "panelToolbar";
            this.panelToolbar.Size = new System.Drawing.Size(1012, 44);
            this.panelToolbar.TabIndex = 1;
            // 
            // flowToolbarRight
            // 
            this.flowToolbarRight.AutoSize = true;
            this.flowToolbarRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowToolbarRight.Controls.Add(this.lblNameLabel);
            this.flowToolbarRight.Controls.Add(this.txtSearch);
            this.flowToolbarRight.Controls.Add(this.lblCategoryLabel);
            this.flowToolbarRight.Controls.Add(this.cmbCategory);
            this.flowToolbarRight.Controls.Add(this.lblPriceLabel);
            this.flowToolbarRight.Controls.Add(this.txtMinPrice);
            this.flowToolbarRight.Controls.Add(this.txtMaxPrice);
            this.flowToolbarRight.Controls.Add(this.btnClear);
            this.flowToolbarRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.flowToolbarRight.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowToolbarRight.Location = new System.Drawing.Point(404, 0);
            this.flowToolbarRight.Name = "flowToolbarRight";
            this.flowToolbarRight.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.flowToolbarRight.Size = new System.Drawing.Size(608, 30);
            this.flowToolbarRight.TabIndex = 1;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnClear.Location = new System.Drawing.Point(538, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 30);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // txtMaxPrice
            // 
            this.txtMaxPrice.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtMaxPrice.Location = new System.Drawing.Point(466, 4);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.Size = new System.Drawing.Size(72, 25);
            this.txtMaxPrice.TabIndex = 6;
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtMinPrice.Location = new System.Drawing.Point(394, 4);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.Size = new System.Drawing.Size(72, 25);
            this.txtMinPrice.TabIndex = 5;
            // 
            // lblPriceLabel
            // 
            this.lblPriceLabel.AutoSize = true;
            this.lblPriceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPriceLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPriceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPriceLabel.Location = new System.Drawing.Point(354, 10);
            this.lblPriceLabel.Margin = new System.Windows.Forms.Padding(8, 6, 4, 0);
            this.lblPriceLabel.Name = "lblPriceLabel";
            this.lblPriceLabel.Size = new System.Drawing.Size(40, 18);
            this.lblPriceLabel.TabIndex = 4;
            this.lblPriceLabel.Text = "Price:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "All categories"});
            this.cmbCategory.Location = new System.Drawing.Point(224, 4);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(130, 26);
            this.cmbCategory.TabIndex = 3;
            // 
            // lblCategoryLabel
            // 
            this.lblCategoryLabel.AutoSize = true;
            this.lblCategoryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblCategoryLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblCategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblCategoryLabel.Location = new System.Drawing.Point(156, 10);
            this.lblCategoryLabel.Margin = new System.Windows.Forms.Padding(8, 6, 4, 0);
            this.lblCategoryLabel.Name = "lblCategoryLabel";
            this.lblCategoryLabel.Size = new System.Drawing.Size(68, 18);
            this.lblCategoryLabel.TabIndex = 2;
            this.lblCategoryLabel.Text = "Category:";
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtSearch.Location = new System.Drawing.Point(52, 4);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(160, 25);
            this.txtSearch.TabIndex = 1;
            // 
            // lblNameLabel
            // 
            this.lblNameLabel.AutoSize = true;
            this.lblNameLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblNameLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblNameLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblNameLabel.Location = new System.Drawing.Point(0, 10);
            this.lblNameLabel.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblNameLabel.Name = "lblNameLabel";
            this.lblNameLabel.Size = new System.Drawing.Size(48, 18);
            this.lblNameLabel.TabIndex = 0;
            this.lblNameLabel.Text = "Name:";
            // 
            // flowToolbarLeft
            // 
            this.flowToolbarLeft.AutoSize = true;
            this.flowToolbarLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowToolbarLeft.Controls.Add(this.btnAdd);
            this.flowToolbarLeft.Controls.Add(this.btnEdit);
            this.flowToolbarLeft.Controls.Add(this.btnRemove);
            this.flowToolbarLeft.Controls.Add(this.btnReload);
            this.flowToolbarLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowToolbarLeft.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowToolbarLeft.Location = new System.Drawing.Point(0, 0);
            this.flowToolbarLeft.Name = "flowToolbarLeft";
            this.flowToolbarLeft.Size = new System.Drawing.Size(404, 30);
            this.flowToolbarLeft.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(130, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "+ Add Medicine";
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEdit.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnEdit.Location = new System.Drawing.Point(140, 0);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(72, 30);
            this.btnEdit.TabIndex = 1;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = false;
            // 
            // btnRemove
            // 
            this.btnRemove.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemove.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnRemove.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnRemove.Location = new System.Drawing.Point(222, 0);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(84, 30);
            this.btnRemove.TabIndex = 2;
            this.btnRemove.Text = "Remove";
            this.btnRemove.UseVisualStyleBackColor = false;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnReload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnReload.Location = new System.Drawing.Point(316, 0);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(84, 30);
            this.btnReload.TabIndex = 3;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            // 
            // panelExpiryAlerts
            // 
            this.panelExpiryAlerts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.panelExpiryAlerts.Controls.Add(this.lblExpirySummary);
            this.panelExpiryAlerts.Controls.Add(this.btnViewExpiryAlerts);
            this.panelExpiryAlerts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelExpiryAlerts.Location = new System.Drawing.Point(0, 152);
            this.panelExpiryAlerts.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelExpiryAlerts.Name = "panelExpiryAlerts";
            this.panelExpiryAlerts.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelExpiryAlerts.Size = new System.Drawing.Size(1012, 40);
            this.panelExpiryAlerts.TabIndex = 2;
            // 
            // lblExpirySummary
            // 
            this.lblExpirySummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.lblExpirySummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpirySummary.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblExpirySummary.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(0)))));
            this.lblExpirySummary.Location = new System.Drawing.Point(12, 8);
            this.lblExpirySummary.Name = "lblExpirySummary";
            this.lblExpirySummary.Size = new System.Drawing.Size(858, 24);
            this.lblExpirySummary.TabIndex = 0;
            this.lblExpirySummary.Text = "No expiry alerts. All medicines are within safe expiry dates.";
            this.lblExpirySummary.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnViewExpiryAlerts
            // 
            this.btnViewExpiryAlerts.BackColor = System.Drawing.Color.White;
            this.btnViewExpiryAlerts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewExpiryAlerts.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnViewExpiryAlerts.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnViewExpiryAlerts.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewExpiryAlerts.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnViewExpiryAlerts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnViewExpiryAlerts.Location = new System.Drawing.Point(870, 8);
            this.btnViewExpiryAlerts.Name = "btnViewExpiryAlerts";
            this.btnViewExpiryAlerts.Size = new System.Drawing.Size(130, 26);
            this.btnViewExpiryAlerts.TabIndex = 1;
            this.btnViewExpiryAlerts.Text = "View All Alerts";
            this.btnViewExpiryAlerts.UseVisualStyleBackColor = false;
            this.btnViewExpiryAlerts.Visible = false;
            // 
            // panelGridOuter
            // 
            this.panelGridOuter.BackColor = System.Drawing.Color.White;
            this.panelGridOuter.Controls.Add(this.panelGridBody);
            this.panelGridOuter.Controls.Add(this.panelGridHeader);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 204);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 392);
            this.panelGridOuter.TabIndex = 3;
            // 
            // panelGridBody
            // 
            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.gridMedicines);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(1, 37);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 354);
            this.panelGridBody.TabIndex = 1;
            // 
            // gridMedicines
            // 
            this.gridMedicines.AllowUserToAddRows = false;
            this.gridMedicines.AllowUserToDeleteRows = false;
            this.gridMedicines.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridMedicines.BackgroundColor = System.Drawing.Color.White;
            this.gridMedicines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridMedicines.ColumnHeadersHeight = 36;
            this.gridMedicines.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMedicines.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridMedicines.Location = new System.Drawing.Point(0, 4);
            this.gridMedicines.MinimumSize = new System.Drawing.Size(0, 360);
            this.gridMedicines.MultiSelect = false;
            this.gridMedicines.Name = "gridMedicines";
            this.gridMedicines.ReadOnly = true;
            this.gridMedicines.RowHeadersVisible = false;
            this.gridMedicines.RowTemplate.Height = 36;
            this.gridMedicines.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridMedicines.Size = new System.Drawing.Size(1010, 350);
            this.gridMedicines.TabIndex = 0;
            // 
            // panelGridHeader
            // 
            this.panelGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelGridHeader.Controls.Add(this.lblGridTitle);
            this.panelGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGridHeader.Location = new System.Drawing.Point(1, 1);
            this.panelGridHeader.Name = "panelGridHeader";
            this.panelGridHeader.Padding = new System.Windows.Forms.Padding(12, 8, 10, 4);
            this.panelGridHeader.Size = new System.Drawing.Size(1010, 36);
            this.panelGridHeader.TabIndex = 0;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblGridTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblGridTitle.Location = new System.Drawing.Point(12, 10);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(128, 16);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Medicine Inventory";
            // 
            // tableStatsRow
            // 
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
            // 
            // panelStatTotal
            // 
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
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.BackColor = System.Drawing.Color.White;
            this.lblTotalItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotalItems.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblTotalItems.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblTotalItems.Location = new System.Drawing.Point(16, 30);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(293, 64);
            this.lblTotalItems.TabIndex = 1;
            this.lblTotalItems.Text = "12";
            this.lblTotalItems.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatTotalTitle
            // 
            this.lblStatTotalTitle.BackColor = System.Drawing.Color.White;
            this.lblStatTotalTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatTotalTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatTotalTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatTotalTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatTotalTitle.Name = "lblStatTotalTitle";
            this.lblStatTotalTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatTotalTitle.TabIndex = 0;
            this.lblStatTotalTitle.Text = "TOTAL ITEMS";
            // 
            // panelStatLow
            // 
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
            // 
            // lblLowStock
            // 
            this.lblLowStock.BackColor = System.Drawing.Color.White;
            this.lblLowStock.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblLowStock.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblLowStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblLowStock.Location = new System.Drawing.Point(16, 30);
            this.lblLowStock.Name = "lblLowStock";
            this.lblLowStock.Size = new System.Drawing.Size(293, 64);
            this.lblLowStock.TabIndex = 1;
            this.lblLowStock.Text = "2";
            this.lblLowStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatLowTitle
            // 
            this.lblStatLowTitle.BackColor = System.Drawing.Color.White;
            this.lblStatLowTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatLowTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatLowTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatLowTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatLowTitle.Name = "lblStatLowTitle";
            this.lblStatLowTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatLowTitle.TabIndex = 0;
            this.lblStatLowTitle.Text = "LOW STOCK";
            // 
            // panelStatCompliance
            // 
            this.panelStatCompliance.BackColor = System.Drawing.Color.White;
            this.panelStatCompliance.Controls.Add(this.lblExpiringSoon);
            this.panelStatCompliance.Controls.Add(this.lblStatComplianceTitle);
            this.panelStatCompliance.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatCompliance.Location = new System.Drawing.Point(674, 0);
            this.panelStatCompliance.Name = "panelStatCompliance";
            this.panelStatCompliance.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatCompliance.Size = new System.Drawing.Size(338, 108);
            this.panelStatCompliance.TabIndex = 2;
            // 
            // lblExpiringSoon
            // 
            this.lblExpiringSoon.BackColor = System.Drawing.Color.White;
            this.lblExpiringSoon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblExpiringSoon.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblExpiringSoon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblExpiringSoon.Location = new System.Drawing.Point(16, 30);
            this.lblExpiringSoon.Name = "lblExpiringSoon";
            this.lblExpiringSoon.Size = new System.Drawing.Size(308, 64);
            this.lblExpiringSoon.TabIndex = 1;
            this.lblExpiringSoon.Text = "91.7%";
            this.lblExpiringSoon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatComplianceTitle
            // 
            this.lblStatComplianceTitle.BackColor = System.Drawing.Color.White;
            this.lblStatComplianceTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatComplianceTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatComplianceTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatComplianceTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatComplianceTitle.Name = "lblStatComplianceTitle";
            this.lblStatComplianceTitle.Size = new System.Drawing.Size(308, 16);
            this.lblStatComplianceTitle.TabIndex = 0;
            this.lblStatComplianceTitle.Text = "COMPLIANCE";
            // 
            // ManageMedicinesForm
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
            this.panelToolbar.ResumeLayout(false);
            this.panelToolbar.PerformLayout();
            this.flowToolbarRight.ResumeLayout(false);
            this.flowToolbarRight.PerformLayout();
            this.flowToolbarLeft.ResumeLayout(false);
            this.panelExpiryAlerts.ResumeLayout(false);
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMedicines)).EndInit();
            this.panelGridHeader.ResumeLayout(false);
            this.panelGridHeader.PerformLayout();
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
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelToolbar;
        private System.Windows.Forms.FlowLayoutPanel flowToolbarRight;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label lblPriceLabel;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.Label lblCategoryLabel;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblNameLabel;
        private System.Windows.Forms.FlowLayoutPanel flowToolbarLeft;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel panelExpiryAlerts;
        private System.Windows.Forms.Label lblExpirySummary;
        private System.Windows.Forms.Button btnViewExpiryAlerts;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView gridMedicines;
        private System.Windows.Forms.Panel panelGridHeader;
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
