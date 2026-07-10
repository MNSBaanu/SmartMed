namespace SmartMed.UI
{
    partial class ManageHealthServicesForm
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
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelServicesOuter = new System.Windows.Forms.Panel();
            this.panelServicesBody = new System.Windows.Forms.Panel();
            this.gridServices = new System.Windows.Forms.DataGridView();
            this.colServiceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServicePrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colServiceDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelServicesHeader = new System.Windows.Forms.Panel();
            this.btnAddService = new System.Windows.Forms.Button();
            this.lblServicesTitle = new System.Windows.Forms.Label();
            this.panelRecordsOuter = new System.Windows.Forms.Panel();
            this.panelRecordsBody = new System.Windows.Forms.Panel();
            this.gridRecords = new System.Windows.Forms.DataGridView();
            this.colRecordDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordCustomer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordService = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordResult = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordPharmacist = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRecordNotes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowRecordsToolbar = new System.Windows.Forms.FlowLayoutPanel();
            this.lblRecordSearch = new System.Windows.Forms.Label();
            this.txtRecordSearch = new System.Windows.Forms.TextBox();
            this.btnReload = new System.Windows.Forms.Button();
            this.panelRecordsHeader = new System.Windows.Forms.Panel();
            this.btnAddRecord = new System.Windows.Forms.Button();
            this.lblRecordsTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelServicesOuter.SuspendLayout();
            this.panelServicesBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridServices)).BeginInit();
            this.panelServicesHeader.SuspendLayout();
            this.panelRecordsOuter.SuspendLayout();
            this.panelRecordsBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecords)).BeginInit();
            this.flowRecordsToolbar.SuspendLayout();
            this.panelRecordsHeader.SuspendLayout();
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
            this.tableLayoutRoot.Controls.Add(this.panelServicesOuter, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelRecordsOuter, 0, 2);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 3;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 45F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 55F));
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 672);
            this.tableLayoutRoot.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.MaximumSize = new System.Drawing.Size(900, 0);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(620, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Register available services and record customer service delivery with results and pharmacist notes.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(178, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Health Services";
            // 
            // panelServicesOuter
            // 
            this.panelServicesOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelServicesOuter.Controls.Add(this.panelServicesBody);
            this.panelServicesOuter.Controls.Add(this.panelServicesHeader);
            this.panelServicesOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelServicesOuter.Location = new System.Drawing.Point(0, 88);
            this.panelServicesOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.panelServicesOuter.Name = "panelServicesOuter";
            this.panelServicesOuter.Size = new System.Drawing.Size(1012, 260);
            this.panelServicesOuter.TabIndex = 1;
            // 
            // panelServicesBody
            // 
            this.panelServicesBody.BackColor = System.Drawing.Color.White;
            this.panelServicesBody.Controls.Add(this.gridServices);
            this.panelServicesBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelServicesBody.Location = new System.Drawing.Point(0, 40);
            this.panelServicesBody.Name = "panelServicesBody";
            this.panelServicesBody.Padding = new System.Windows.Forms.Padding(1);
            this.panelServicesBody.Size = new System.Drawing.Size(1012, 220);
            this.panelServicesBody.TabIndex = 1;
            // 
            // gridServices
            // 
            this.gridServices.AllowUserToAddRows = false;
            this.gridServices.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridServices.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gridServices.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridServices.BackgroundColor = System.Drawing.Color.White;
            this.gridServices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridServices.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridServices.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridServices.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.gridServices.ColumnHeadersHeight = 36;
            this.gridServices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridServices.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colServiceName,
            this.colServicePrice,
            this.colServiceStatus,
            this.colServiceDescription});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridServices.DefaultCellStyle = dataGridViewCellStyle3;
            this.gridServices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridServices.EnableHeadersVisualStyles = false;
            this.gridServices.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridServices.Location = new System.Drawing.Point(1, 1);
            this.gridServices.MultiSelect = false;
            this.gridServices.Name = "gridServices";
            this.gridServices.ReadOnly = true;
            this.gridServices.RowHeadersVisible = false;
            this.gridServices.RowTemplate.Height = 36;
            this.gridServices.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridServices.Size = new System.Drawing.Size(1010, 218);
            this.gridServices.TabIndex = 0;
            this.gridServices.Rows.Add(new object[] {
            "Blood Pressure Check",
            "LKR 500.00",
            "Active",
            "In-pharmacy screening"});
            this.gridServices.Rows.Add(new object[] {
            "Flu Vaccination",
            "LKR 1,200.00",
            "Active",
            "Seasonal vaccination"});
            // 
            // colServiceName
            // 
            this.colServiceName.HeaderText = "Service Name";
            this.colServiceName.MinimumWidth = 6;
            this.colServiceName.Name = "colServiceName";
            this.colServiceName.ReadOnly = true;
            // 
            // colServicePrice
            // 
            this.colServicePrice.HeaderText = "Price";
            this.colServicePrice.MinimumWidth = 6;
            this.colServicePrice.Name = "colServicePrice";
            this.colServicePrice.ReadOnly = true;
            // 
            // colServiceStatus
            // 
            this.colServiceStatus.HeaderText = "Status";
            this.colServiceStatus.MinimumWidth = 6;
            this.colServiceStatus.Name = "colServiceStatus";
            this.colServiceStatus.ReadOnly = true;
            // 
            // colServiceDescription
            // 
            this.colServiceDescription.HeaderText = "Description";
            this.colServiceDescription.MinimumWidth = 6;
            this.colServiceDescription.Name = "colServiceDescription";
            this.colServiceDescription.ReadOnly = true;
            // 
            // panelServicesHeader
            // 
            this.panelServicesHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelServicesHeader.Controls.Add(this.btnAddService);
            this.panelServicesHeader.Controls.Add(this.lblServicesTitle);
            this.panelServicesHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelServicesHeader.Location = new System.Drawing.Point(0, 0);
            this.panelServicesHeader.Name = "panelServicesHeader";
            this.panelServicesHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelServicesHeader.Size = new System.Drawing.Size(1012, 40);
            this.panelServicesHeader.TabIndex = 0;
            // 
            // btnAddService
            // 
            this.btnAddService.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddService.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.btnAddService.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddService.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnAddService.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddService.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnAddService.ForeColor = System.Drawing.Color.White;
            this.btnAddService.Location = new System.Drawing.Point(872, 4);
            this.btnAddService.Name = "btnAddService";
            this.btnAddService.Size = new System.Drawing.Size(120, 30);
            this.btnAddService.TabIndex = 1;
            this.btnAddService.Text = "Add Service";
            this.btnAddService.UseVisualStyleBackColor = false;
            // 
            // lblServicesTitle
            // 
            this.lblServicesTitle.AutoSize = true;
            this.lblServicesTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblServicesTitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Bold);
            this.lblServicesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblServicesTitle.Location = new System.Drawing.Point(12, 10);
            this.lblServicesTitle.Name = "lblServicesTitle";
            this.lblServicesTitle.Size = new System.Drawing.Size(138, 18);
            this.lblServicesTitle.TabIndex = 0;
            this.lblServicesTitle.Text = "AVAILABLE SERVICES";
            // 
            // panelRecordsOuter
            // 
            this.panelRecordsOuter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelRecordsOuter.Controls.Add(this.panelRecordsBody);
            this.panelRecordsOuter.Controls.Add(this.flowRecordsToolbar);
            this.panelRecordsOuter.Controls.Add(this.panelRecordsHeader);
            this.panelRecordsOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecordsOuter.Location = new System.Drawing.Point(0, 356);
            this.panelRecordsOuter.Margin = new System.Windows.Forms.Padding(0);
            this.panelRecordsOuter.Name = "panelRecordsOuter";
            this.panelRecordsOuter.Size = new System.Drawing.Size(1012, 316);
            this.panelRecordsOuter.TabIndex = 2;
            // 
            // panelRecordsBody
            // 
            this.panelRecordsBody.BackColor = System.Drawing.Color.White;
            this.panelRecordsBody.Controls.Add(this.gridRecords);
            this.panelRecordsBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRecordsBody.Location = new System.Drawing.Point(0, 80);
            this.panelRecordsBody.Name = "panelRecordsBody";
            this.panelRecordsBody.Padding = new System.Windows.Forms.Padding(1);
            this.panelRecordsBody.Size = new System.Drawing.Size(1012, 236);
            this.panelRecordsBody.TabIndex = 2;
            // 
            // gridRecords
            // 
            this.gridRecords.AllowUserToAddRows = false;
            this.gridRecords.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
            this.gridRecords.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gridRecords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRecords.BackgroundColor = System.Drawing.Color.White;
            this.gridRecords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRecords.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.gridRecords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridRecords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gridRecords.ColumnHeadersHeight = 36;
            this.gridRecords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gridRecords.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colRecordDate,
            this.colRecordCustomer,
            this.colRecordService,
            this.colRecordResult,
            this.colRecordPharmacist,
            this.colRecordNotes});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(240)))), ((int)(((byte)(236)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridRecords.DefaultCellStyle = dataGridViewCellStyle6;
            this.gridRecords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRecords.EnableHeadersVisualStyles = false;
            this.gridRecords.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridRecords.Location = new System.Drawing.Point(1, 1);
            this.gridRecords.MultiSelect = false;
            this.gridRecords.Name = "gridRecords";
            this.gridRecords.ReadOnly = true;
            this.gridRecords.RowHeadersVisible = false;
            this.gridRecords.RowTemplate.Height = 36;
            this.gridRecords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRecords.Size = new System.Drawing.Size(1010, 234);
            this.gridRecords.TabIndex = 0;
            this.gridRecords.Rows.Add(new object[] {
            "2026-07-05",
            "Jane Customer",
            "Blood Pressure Check",
            "120/80 mmHg",
            "admin",
            "Within normal range."});
            // 
            // colRecordDate
            // 
            this.colRecordDate.HeaderText = "Service Date";
            this.colRecordDate.MinimumWidth = 6;
            this.colRecordDate.Name = "colRecordDate";
            this.colRecordDate.ReadOnly = true;
            // 
            // colRecordCustomer
            // 
            this.colRecordCustomer.HeaderText = "Customer";
            this.colRecordCustomer.MinimumWidth = 6;
            this.colRecordCustomer.Name = "colRecordCustomer";
            this.colRecordCustomer.ReadOnly = true;
            // 
            // colRecordService
            // 
            this.colRecordService.HeaderText = "Service";
            this.colRecordService.MinimumWidth = 6;
            this.colRecordService.Name = "colRecordService";
            this.colRecordService.ReadOnly = true;
            // 
            // colRecordResult
            // 
            this.colRecordResult.HeaderText = "Result";
            this.colRecordResult.MinimumWidth = 6;
            this.colRecordResult.Name = "colRecordResult";
            this.colRecordResult.ReadOnly = true;
            // 
            // colRecordPharmacist
            // 
            this.colRecordPharmacist.HeaderText = "Pharmacist";
            this.colRecordPharmacist.MinimumWidth = 6;
            this.colRecordPharmacist.Name = "colRecordPharmacist";
            this.colRecordPharmacist.ReadOnly = true;
            // 
            // colRecordNotes
            // 
            this.colRecordNotes.HeaderText = "Notes";
            this.colRecordNotes.MinimumWidth = 6;
            this.colRecordNotes.Name = "colRecordNotes";
            this.colRecordNotes.ReadOnly = true;
            // 
            // flowRecordsToolbar
            // 
            this.flowRecordsToolbar.AutoSize = true;
            this.flowRecordsToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowRecordsToolbar.Controls.Add(this.lblRecordSearch);
            this.flowRecordsToolbar.Controls.Add(this.txtRecordSearch);
            this.flowRecordsToolbar.Controls.Add(this.btnReload);
            this.flowRecordsToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowRecordsToolbar.Location = new System.Drawing.Point(0, 40);
            this.flowRecordsToolbar.Name = "flowRecordsToolbar";
            this.flowRecordsToolbar.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.flowRecordsToolbar.Size = new System.Drawing.Size(1012, 40);
            this.flowRecordsToolbar.TabIndex = 1;
            this.flowRecordsToolbar.WrapContents = false;
            // 
            // lblRecordSearch
            // 
            this.lblRecordSearch.AutoSize = true;
            this.lblRecordSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblRecordSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblRecordSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblRecordSearch.Location = new System.Drawing.Point(8, 10);
            this.lblRecordSearch.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblRecordSearch.Name = "lblRecordSearch";
            this.lblRecordSearch.Size = new System.Drawing.Size(47, 18);
            this.lblRecordSearch.TabIndex = 0;
            this.lblRecordSearch.Text = "Search:";
            // 
            // txtRecordSearch
            // 
            this.txtRecordSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtRecordSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRecordSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtRecordSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtRecordSearch.Location = new System.Drawing.Point(59, 7);
            this.txtRecordSearch.Margin = new System.Windows.Forms.Padding(0, 3, 8, 0);
            this.txtRecordSearch.Name = "txtRecordSearch";
            this.txtRecordSearch.Size = new System.Drawing.Size(180, 25);
            this.txtRecordSearch.TabIndex = 1;
            // 
            // btnReload
            // 
            this.btnReload.BackColor = System.Drawing.Color.White;
            this.btnReload.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReload.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnReload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnReload.Location = new System.Drawing.Point(247, 5);
            this.btnReload.Margin = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(88, 30);
            this.btnReload.TabIndex = 2;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = false;
            // 
            // panelRecordsHeader
            // 
            this.panelRecordsHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelRecordsHeader.Controls.Add(this.btnAddRecord);
            this.panelRecordsHeader.Controls.Add(this.lblRecordsTitle);
            this.panelRecordsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelRecordsHeader.Location = new System.Drawing.Point(0, 0);
            this.panelRecordsHeader.Name = "panelRecordsHeader";
            this.panelRecordsHeader.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelRecordsHeader.Size = new System.Drawing.Size(1012, 40);
            this.panelRecordsHeader.TabIndex = 0;
            // 
            // btnAddRecord
            // 
            this.btnAddRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddRecord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.btnAddRecord.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddRecord.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnAddRecord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRecord.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnAddRecord.ForeColor = System.Drawing.Color.White;
            this.btnAddRecord.Location = new System.Drawing.Point(860, 4);
            this.btnAddRecord.Name = "btnAddRecord";
            this.btnAddRecord.Size = new System.Drawing.Size(132, 30);
            this.btnAddRecord.TabIndex = 1;
            this.btnAddRecord.Text = "Record Service";
            this.btnAddRecord.UseVisualStyleBackColor = false;
            // 
            // lblRecordsTitle
            // 
            this.lblRecordsTitle.AutoSize = true;
            this.lblRecordsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblRecordsTitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Bold);
            this.lblRecordsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblRecordsTitle.Location = new System.Drawing.Point(12, 10);
            this.lblRecordsTitle.Name = "lblRecordsTitle";
            this.lblRecordsTitle.Size = new System.Drawing.Size(123, 18);
            this.lblRecordsTitle.TabIndex = 0;
            this.lblRecordsTitle.Text = "SERVICE HISTORY";
            // 
            // ManageHealthServicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.Controls.Add(this.panelScrollHost);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.Name = "ManageHealthServicesForm";
            this.Text = "Health Services";
            this.panelScrollHost.ResumeLayout(false);
            this.tableLayoutRoot.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelServicesOuter.ResumeLayout(false);
            this.panelServicesBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridServices)).EndInit();
            this.panelServicesHeader.ResumeLayout(false);
            this.panelServicesHeader.PerformLayout();
            this.panelRecordsOuter.ResumeLayout(false);
            this.panelRecordsOuter.PerformLayout();
            this.panelRecordsBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRecords)).EndInit();
            this.flowRecordsToolbar.ResumeLayout(false);
            this.flowRecordsToolbar.PerformLayout();
            this.panelRecordsHeader.ResumeLayout(false);
            this.panelRecordsHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelServicesOuter;
        private System.Windows.Forms.Panel panelServicesBody;
        private System.Windows.Forms.DataGridView gridServices;
        private System.Windows.Forms.Panel panelServicesHeader;
        private System.Windows.Forms.Button btnAddService;
        private System.Windows.Forms.Label lblServicesTitle;
        private System.Windows.Forms.Panel panelRecordsOuter;
        private System.Windows.Forms.Panel panelRecordsBody;
        private System.Windows.Forms.DataGridView gridRecords;
        private System.Windows.Forms.FlowLayoutPanel flowRecordsToolbar;
        private System.Windows.Forms.Label lblRecordSearch;
        private System.Windows.Forms.TextBox txtRecordSearch;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.Panel panelRecordsHeader;
        private System.Windows.Forms.Button btnAddRecord;
        private System.Windows.Forms.Label lblRecordsTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServicePrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn colServiceDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordCustomer;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordService;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordPharmacist;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRecordNotes;
    }
}
