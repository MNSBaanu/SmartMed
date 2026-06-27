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
            this.lblTopBrand = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnNavLogout = new System.Windows.Forms.Button();
            this.btnNavReports = new System.Windows.Forms.Button();
            this.btnNavOrders = new System.Windows.Forms.Button();
            this.btnNavCustomers = new System.Windows.Forms.Button();
            this.btnNavMedicines = new System.Windows.Forms.Button();
            this.btnNavOverview = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnClose);
            this.panelTop.Controls.Add(this.lblTopSubtitle);
            this.panelTop.Controls.Add(this.lblTopBrand);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1184, 56);
            this.panelTop.TabIndex = 0;
            // 
            // lblTopBrand
            // 
            this.lblTopBrand.AutoSize = true;
            this.lblTopBrand.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.lblTopBrand.Font = new System.Drawing.Font("Roboto", 9F, System.Drawing.FontStyle.Bold);
            this.lblTopBrand.ForeColor = System.Drawing.Color.White;
            this.lblTopBrand.Location = new System.Drawing.Point(16, 10);
            this.lblTopBrand.Name = "lblTopBrand";
            this.lblTopBrand.Size = new System.Drawing.Size(74, 20);
            this.lblTopBrand.TabIndex = 0;
            this.lblTopBrand.Text = "SmartMed";
            // 
            // lblTopSubtitle
            // 
            this.lblTopSubtitle.AutoSize = true;
            this.lblTopSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(24)))));
            this.lblTopSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.lblTopSubtitle.Location = new System.Drawing.Point(16, 34);
            this.lblTopSubtitle.Name = "lblTopSubtitle";
            this.lblTopSubtitle.Size = new System.Drawing.Size(104, 15);
            this.lblTopSubtitle.TabIndex = 1;
            this.lblTopSubtitle.Text = "Admin Dashboard";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1140, 12);
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
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 56);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0);
            this.panelSidebar.Size = new System.Drawing.Size(260, 712);
            this.panelSidebar.TabIndex = 1;
            // 
            // btnNavOverview
            // 
            this.btnNavOverview.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavOverview.Location = new System.Drawing.Point(0, 88);
            this.btnNavOverview.Size = new System.Drawing.Size(260, 44);
            this.btnNavOverview.TabIndex = 2;
            this.btnNavOverview.Text = "Dashboard";
            this.btnNavOverview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOverview.UseVisualStyleBackColor = false;
            this.btnNavOverview.Click += new System.EventHandler(this.BtnNavOverview_Click);
            // 
            // btnNavMedicines
            // 
            this.btnNavMedicines.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavMedicines.Size = new System.Drawing.Size(260, 44);
            this.btnNavMedicines.TabIndex = 3;
            this.btnNavMedicines.Text = "Inventory";
            this.btnNavMedicines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavMedicines.UseVisualStyleBackColor = false;
            this.btnNavMedicines.Click += new System.EventHandler(this.BtnNavMedicines_Click);
            // 
            // btnNavCustomers
            // 
            this.btnNavCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavCustomers.Size = new System.Drawing.Size(260, 44);
            this.btnNavCustomers.TabIndex = 4;
            this.btnNavCustomers.Text = "Customers";
            this.btnNavCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCustomers.UseVisualStyleBackColor = false;
            this.btnNavCustomers.Click += new System.EventHandler(this.BtnNavCustomers_Click);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavOrders.Size = new System.Drawing.Size(260, 44);
            this.btnNavOrders.TabIndex = 5;
            this.btnNavOrders.Text = "Orders";
            this.btnNavOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavReports
            // 
            this.btnNavReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavReports.Size = new System.Drawing.Size(260, 44);
            this.btnNavReports.TabIndex = 6;
            this.btnNavReports.Text = "Reports";
            this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReports.UseVisualStyleBackColor = false;
            this.btnNavReports.Click += new System.EventHandler(this.BtnNavReports_Click);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnNavLogout.Size = new System.Drawing.Size(260, 44);
            this.btnNavLogout.TabIndex = 7;
            this.btnNavLogout.Text = "Sign Out";
            this.btnNavLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavLogout.UseVisualStyleBackColor = false;
            this.btnNavLogout.Click += new System.EventHandler(this.BtnNavLogout_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(280, 56);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(28, 24, 28, 0);
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
            this.Font = new System.Drawing.Font("Roboto", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.Name = "AdminShellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.ResumeLayout(false);
        }

        protected System.Windows.Forms.Panel panelTop;
        protected System.Windows.Forms.Label lblTopBrand;
        protected System.Windows.Forms.Label lblTopSubtitle;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Panel panelSidebar;
        protected System.Windows.Forms.Button btnNavOverview;
        protected System.Windows.Forms.Button btnNavMedicines;
        protected System.Windows.Forms.Button btnNavCustomers;
        protected System.Windows.Forms.Button btnNavOrders;
        protected System.Windows.Forms.Button btnNavReports;
        protected System.Windows.Forms.Button btnNavLogout;
        protected System.Windows.Forms.Panel panelContent;
    }
}
