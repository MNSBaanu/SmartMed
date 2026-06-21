namespace SmartMed.UI
{
    partial class AdminShellForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblTopSubtitle = new System.Windows.Forms.Label();
            this.lblTopTitle = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnNavLogout = new System.Windows.Forms.Button();
            this.btnNavReports = new System.Windows.Forms.Button();
            this.btnNavOrders = new System.Windows.Forms.Button();
            this.btnNavCustomers = new System.Windows.Forms.Button();
            this.btnNavMedicines = new System.Windows.Forms.Button();
            this.btnNavOverview = new System.Windows.Forms.Button();
            this.lblNavBrand = new System.Windows.Forms.Label();
            this.lblNavTagline = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.lblTopSubtitle);
            this.panelTop.Controls.Add(this.lblTopTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1184, 48);
            this.panelTop.TabIndex = 0;
            // 
            // lblTopTitle
            // 
            this.lblTopTitle.AutoSize = true;
            this.lblTopTitle.Location = new System.Drawing.Point(16, 14);
            this.lblTopTitle.Name = "lblTopTitle";
            this.lblTopTitle.Size = new System.Drawing.Size(118, 20);
            this.lblTopTitle.TabIndex = 0;
            this.lblTopTitle.Text = "SmartMed Pharmacy";
            // 
            // lblTopSubtitle
            // 
            this.lblTopSubtitle.AutoSize = true;
            this.lblTopSubtitle.Location = new System.Drawing.Point(200, 16);
            this.lblTopSubtitle.Name = "lblTopSubtitle";
            this.lblTopSubtitle.Size = new System.Drawing.Size(104, 15);
            this.lblTopSubtitle.TabIndex = 1;
            this.lblTopSubtitle.Text = "Admin Dashboard";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1140, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.btnNavLogout);
            this.panelSidebar.Controls.Add(this.btnNavReports);
            this.panelSidebar.Controls.Add(this.btnNavOrders);
            this.panelSidebar.Controls.Add(this.btnNavCustomers);
            this.panelSidebar.Controls.Add(this.btnNavMedicines);
            this.panelSidebar.Controls.Add(this.btnNavOverview);
            this.panelSidebar.Controls.Add(this.lblNavTagline);
            this.panelSidebar.Controls.Add(this.lblNavBrand);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 48);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 16);
            this.panelSidebar.Size = new System.Drawing.Size(280, 712);
            this.panelSidebar.TabIndex = 1;
            // 
            // lblNavBrand
            // 
            this.lblNavBrand.AutoSize = true;
            this.lblNavBrand.Location = new System.Drawing.Point(16, 20);
            this.lblNavBrand.Name = "lblNavBrand";
            this.lblNavBrand.Size = new System.Drawing.Size(67, 20);
            this.lblNavBrand.TabIndex = 0;
            this.lblNavBrand.Text = "SmartMed";
            // 
            // lblNavTagline
            // 
            this.lblNavTagline.AutoSize = true;
            this.lblNavTagline.Location = new System.Drawing.Point(16, 44);
            this.lblNavTagline.Name = "lblNavTagline";
            this.lblNavTagline.Size = new System.Drawing.Size(174, 15);
            this.lblNavTagline.TabIndex = 1;
            this.lblNavTagline.Text = "Pharmacy Management System";
            // 
            // btnNavOverview
            // 
            this.btnNavOverview.Location = new System.Drawing.Point(0, 80);
            this.btnNavOverview.Name = "btnNavOverview";
            this.btnNavOverview.Size = new System.Drawing.Size(280, 40);
            this.btnNavOverview.TabIndex = 2;
            this.btnNavOverview.Text = "Dashboard Overview";
            this.btnNavOverview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOverview.UseVisualStyleBackColor = false;
            this.btnNavOverview.Click += new System.EventHandler(this.BtnNavOverview_Click);
            // 
            // btnNavMedicines
            // 
            this.btnNavMedicines.Location = new System.Drawing.Point(0, 122);
            this.btnNavMedicines.Name = "btnNavMedicines";
            this.btnNavMedicines.Size = new System.Drawing.Size(280, 40);
            this.btnNavMedicines.TabIndex = 3;
            this.btnNavMedicines.Text = "Manage Medicines";
            this.btnNavMedicines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavMedicines.UseVisualStyleBackColor = false;
            this.btnNavMedicines.Click += new System.EventHandler(this.BtnNavMedicines_Click);
            // 
            // btnNavCustomers
            // 
            this.btnNavCustomers.Location = new System.Drawing.Point(0, 164);
            this.btnNavCustomers.Name = "btnNavCustomers";
            this.btnNavCustomers.Size = new System.Drawing.Size(280, 40);
            this.btnNavCustomers.TabIndex = 4;
            this.btnNavCustomers.Text = "Manage Customers";
            this.btnNavCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCustomers.UseVisualStyleBackColor = false;
            this.btnNavCustomers.Click += new System.EventHandler(this.BtnNavCustomers_Click);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Location = new System.Drawing.Point(0, 206);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(280, 40);
            this.btnNavOrders.TabIndex = 5;
            this.btnNavOrders.Text = "Manage Orders";
            this.btnNavOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavReports
            // 
            this.btnNavReports.Location = new System.Drawing.Point(0, 248);
            this.btnNavReports.Name = "btnNavReports";
            this.btnNavReports.Size = new System.Drawing.Size(280, 40);
            this.btnNavReports.TabIndex = 6;
            this.btnNavReports.Text = "Generate Reports";
            this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReports.UseVisualStyleBackColor = false;
            this.btnNavReports.Click += new System.EventHandler(this.BtnNavReports_Click);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNavLogout.Location = new System.Drawing.Point(0, 656);
            this.btnNavLogout.Name = "btnNavLogout";
            this.btnNavLogout.Size = new System.Drawing.Size(280, 40);
            this.btnNavLogout.TabIndex = 7;
            this.btnNavLogout.Text = "Logout";
            this.btnNavLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavLogout.UseVisualStyleBackColor = false;
            this.btnNavLogout.Click += new System.EventHandler(this.BtnNavLogout_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(280, 48);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(24);
            this.panelContent.Size = new System.Drawing.Size(904, 712);
            this.panelContent.TabIndex = 2;
            // 
            // AdminShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 760);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.Name = "AdminShellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Admin";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.ResumeLayout(false);
        }

        protected System.Windows.Forms.Panel panelTop;
        protected System.Windows.Forms.Label lblTopTitle;
        protected System.Windows.Forms.Label lblTopSubtitle;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Panel panelSidebar;
        protected System.Windows.Forms.Label lblNavBrand;
        protected System.Windows.Forms.Label lblNavTagline;
        protected System.Windows.Forms.Button btnNavOverview;
        protected System.Windows.Forms.Button btnNavMedicines;
        protected System.Windows.Forms.Button btnNavCustomers;
        protected System.Windows.Forms.Button btnNavOrders;
        protected System.Windows.Forms.Button btnNavReports;
        protected System.Windows.Forms.Button btnNavLogout;
        protected System.Windows.Forms.Panel panelContent;
    }
}
