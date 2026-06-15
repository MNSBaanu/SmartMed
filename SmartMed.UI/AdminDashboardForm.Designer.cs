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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblHeaderSubtitle = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panelMain = new System.Windows.Forms.Panel();
            this.contentHost = new System.Windows.Forms.Panel();
            this.adminOverviewPreview = new SmartMed.UI.Views.AdminOverviewView();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnOverview = new System.Windows.Forms.Button();
            this.btnMedicines = new System.Windows.Forms.Button();
            this.btnCustomers = new System.Windows.Forms.Button();
            this.btnOrders = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.headerPanel.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.headerPanel.Controls.Add(this.lblHeaderTitle);
            this.headerPanel.Controls.Add(this.lblHeaderSubtitle);
            this.headerPanel.Controls.Add(this.btnLogout);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1280, 48);
            this.headerPanel.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(16, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(178, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "SmartMed Pharmacy";
            // 
            // lblHeaderSubtitle
            // 
            this.lblHeaderSubtitle.AutoSize = true;
            this.lblHeaderSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblHeaderSubtitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderSubtitle.Location = new System.Drawing.Point(220, 16);
            this.lblHeaderSubtitle.Name = "lblHeaderSubtitle";
            this.lblHeaderSubtitle.Size = new System.Drawing.Size(180, 15);
            this.lblHeaderSubtitle.TabIndex = 1;
            this.lblHeaderSubtitle.Text = "Admin Dashboard  |  admin";
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Location = new System.Drawing.Point(1174, 8);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(90, 32);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.contentHost);
            this.panelMain.Controls.Add(this.sidebarPanel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 48);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1280, 672);
            this.panelMain.TabIndex = 1;
            // 
            // contentHost
            // 
            this.contentHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.contentHost.Controls.Add(this.adminOverviewPreview);
            this.contentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentHost.Location = new System.Drawing.Point(420, 0);
            this.contentHost.Name = "contentHost";
            this.contentHost.Padding = new System.Windows.Forms.Padding(24);
            this.contentHost.Size = new System.Drawing.Size(860, 672);
            this.contentHost.TabIndex = 1;
            // 
            // adminOverviewPreview
            // 
            this.adminOverviewPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.adminOverviewPreview.Location = new System.Drawing.Point(24, 24);
            this.adminOverviewPreview.Name = "adminOverviewPreview";
            this.adminOverviewPreview.Size = new System.Drawing.Size(812, 624);
            this.adminOverviewPreview.TabIndex = 0;
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.White;
            this.sidebarPanel.Controls.Add(this.lblWelcome);
            this.sidebarPanel.Controls.Add(this.btnOverview);
            this.sidebarPanel.Controls.Add(this.btnMedicines);
            this.sidebarPanel.Controls.Add(this.btnCustomers);
            this.sidebarPanel.Controls.Add(this.btnOrders);
            this.sidebarPanel.Controls.Add(this.btnReports);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 24);
            this.sidebarPanel.Size = new System.Drawing.Size(420, 672);
            this.sidebarPanel.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblWelcome.Location = new System.Drawing.Point(24, 16);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(141, 21);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, admin";
            // 
            // btnOverview
            // 
            this.btnOverview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnOverview.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnOverview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOverview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnOverview.Location = new System.Drawing.Point(24, 50);
            this.btnOverview.Name = "btnOverview";
            this.btnOverview.Size = new System.Drawing.Size(372, 40);
            this.btnOverview.TabIndex = 1;
            this.btnOverview.Text = "  Dashboard Overview";
            this.btnOverview.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOverview.UseVisualStyleBackColor = false;
            this.btnOverview.Click += new System.EventHandler(this.BtnOverview_Click);
            // 
            // btnMedicines
            // 
            this.btnMedicines.BackColor = System.Drawing.Color.White;
            this.btnMedicines.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnMedicines.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMedicines.Location = new System.Drawing.Point(24, 98);
            this.btnMedicines.Name = "btnMedicines";
            this.btnMedicines.Size = new System.Drawing.Size(372, 40);
            this.btnMedicines.TabIndex = 2;
            this.btnMedicines.Text = "  Manage Medicines";
            this.btnMedicines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMedicines.UseVisualStyleBackColor = false;
            this.btnMedicines.Click += new System.EventHandler(this.BtnMedicines_Click);
            // 
            // btnCustomers
            // 
            this.btnCustomers.BackColor = System.Drawing.Color.White;
            this.btnCustomers.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnCustomers.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCustomers.Location = new System.Drawing.Point(24, 146);
            this.btnCustomers.Name = "btnCustomers";
            this.btnCustomers.Size = new System.Drawing.Size(372, 40);
            this.btnCustomers.TabIndex = 3;
            this.btnCustomers.Text = "  Manage Customers";
            this.btnCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCustomers.UseVisualStyleBackColor = false;
            this.btnCustomers.Click += new System.EventHandler(this.BtnCustomers_Click);
            // 
            // btnOrders
            // 
            this.btnOrders.BackColor = System.Drawing.Color.White;
            this.btnOrders.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOrders.Location = new System.Drawing.Point(24, 194);
            this.btnOrders.Name = "btnOrders";
            this.btnOrders.Size = new System.Drawing.Size(372, 40);
            this.btnOrders.TabIndex = 4;
            this.btnOrders.Text = "  Manage Orders";
            this.btnOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOrders.UseVisualStyleBackColor = false;
            this.btnOrders.Click += new System.EventHandler(this.BtnOrders_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.White;
            this.btnReports.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Location = new System.Drawing.Point(24, 242);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(372, 40);
            this.btnReports.TabIndex = 5;
            this.btnReports.Text = "  Generate Reports";
            this.btnReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.BtnReports_Click);
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "AdminDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Admin Dashboard";
            this.Load += new System.EventHandler(this.AdminDashboardForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblHeaderSubtitle;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnOverview;
        private System.Windows.Forms.Button btnMedicines;
        private System.Windows.Forms.Button btnCustomers;
        private System.Windows.Forms.Button btnOrders;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Panel contentHost;
        private Views.AdminOverviewView adminOverviewPreview;
    }
}
