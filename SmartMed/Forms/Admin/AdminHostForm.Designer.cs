namespace SmartMed.UI
{
    partial class AdminHostForm
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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitleBar = new System.Windows.Forms.Label();
            this.panelWinControls = new System.Windows.Forms.Panel();
            this.btnWinMinimize = new System.Windows.Forms.Button();
            this.btnWinMaximize = new System.Windows.Forms.Button();
            this.btnWinClose = new System.Windows.Forms.Button();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusTime = new System.Windows.Forms.Label();
            this.lblStatusCloud = new System.Windows.Forms.Label();
            this.lblStatusHealth = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.flowNavButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNavDashboard = new SmartMed.UI.NavButton();
            this.btnNavMedicines = new SmartMed.UI.NavButton();
            this.btnNavCustomers = new SmartMed.UI.NavButton();
            this.btnNavOrders = new SmartMed.UI.NavButton();
            this.btnNavReports = new SmartMed.UI.NavButton();
            this.btnNavLogout = new SmartMed.UI.NavButton();
            this.panelProfile = new System.Windows.Forms.Panel();
            this.lblProfileRole = new System.Windows.Forms.Label();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.panelAvatar = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTitleBar.SuspendLayout();
            this.panelWinControls.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.flowNavButtons.SuspendLayout();
            this.panelProfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.Controls.Add(this.lblTitleBar);
            this.panelTitleBar.Controls.Add(this.panelWinControls);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1463, 38);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblTitleBar
            // 
            this.lblTitleBar.AutoSize = true;
            this.lblTitleBar.Location = new System.Drawing.Point(14, 11);
            this.lblTitleBar.Name = "lblTitleBar";
            this.lblTitleBar.Size = new System.Drawing.Size(348, 16);
            this.lblTitleBar.TabIndex = 0;
            this.lblTitleBar.Text = "SmartMed Clinical Management - Operational Dashboard";
            // 
            // panelWinControls
            // 
            this.panelWinControls.Controls.Add(this.btnWinMinimize);
            this.panelWinControls.Controls.Add(this.btnWinMaximize);
            this.panelWinControls.Controls.Add(this.btnWinClose);
            this.panelWinControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelWinControls.Location = new System.Drawing.Point(1257, 0);
            this.panelWinControls.Name = "panelWinControls";
            this.panelWinControls.Size = new System.Drawing.Size(206, 38);
            this.panelWinControls.TabIndex = 1;
            // 
            // btnWinMinimize
            // 
            this.btnWinMinimize.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMinimize.Location = new System.Drawing.Point(138, 0);
            this.btnWinMinimize.Name = "btnWinMinimize";
            this.btnWinMinimize.Size = new System.Drawing.Size(69, 38);
            this.btnWinMinimize.TabIndex = 0;
            this.btnWinMinimize.Text = "—";
            this.btnWinMinimize.UseVisualStyleBackColor = false;
            this.btnWinMinimize.Click += new System.EventHandler(this.BtnWinMinimize_Click);
            // 
            // btnWinMaximize
            // 
            this.btnWinMaximize.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMaximize.Location = new System.Drawing.Point(69, 0);
            this.btnWinMaximize.Name = "btnWinMaximize";
            this.btnWinMaximize.Size = new System.Drawing.Size(69, 38);
            this.btnWinMaximize.TabIndex = 1;
            this.btnWinMaximize.Text = "☐";
            this.btnWinMaximize.UseVisualStyleBackColor = false;
            this.btnWinMaximize.Click += new System.EventHandler(this.BtnWinMaximize_Click);
            // 
            // btnWinClose
            // 
            this.btnWinClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinClose.Location = new System.Drawing.Point(0, 0);
            this.btnWinClose.Name = "btnWinClose";
            this.btnWinClose.Size = new System.Drawing.Size(69, 38);
            this.btnWinClose.TabIndex = 2;
            this.btnWinClose.Text = "✕";
            this.btnWinClose.UseVisualStyleBackColor = false;
            this.btnWinClose.Click += new System.EventHandler(this.BtnWinClose_Click);
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.Controls.Add(this.lblStatusTime);
            this.panelStatusBar.Controls.Add(this.lblStatusCloud);
            this.panelStatusBar.Controls.Add(this.lblStatusHealth);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 789);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Padding = new System.Windows.Forms.Padding(18, 0, 18, 0);
            this.panelStatusBar.Size = new System.Drawing.Size(1463, 30);
            this.panelStatusBar.TabIndex = 2;
            // 
            // lblStatusTime
            // 
            this.lblStatusTime.AutoSize = true;
            this.lblStatusTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatusTime.Location = new System.Drawing.Point(1346, 0);
            this.lblStatusTime.Name = "lblStatusTime";
            this.lblStatusTime.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblStatusTime.Size = new System.Drawing.Size(99, 20);
            this.lblStatusTime.TabIndex = 2;
            this.lblStatusTime.Text = "Local Time: --:--";
            this.lblStatusTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusCloud
            // 
            this.lblStatusCloud.AutoSize = true;
            this.lblStatusCloud.Location = new System.Drawing.Point(299, 4);
            this.lblStatusCloud.Name = "lblStatusCloud";
            this.lblStatusCloud.Size = new System.Drawing.Size(115, 16);
            this.lblStatusCloud.TabIndex = 1;
            this.lblStatusCloud.Text = "Cloud Sync Active";
            // 
            // lblStatusHealth
            // 
            this.lblStatusHealth.AutoSize = true;
            this.lblStatusHealth.Location = new System.Drawing.Point(18, 4);
            this.lblStatusHealth.Name = "lblStatusHealth";
            this.lblStatusHealth.Size = new System.Drawing.Size(144, 16);
            this.lblStatusHealth.TabIndex = 0;
            this.lblStatusHealth.Text = "System Status: Healthy";
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.flowNavButtons);
            this.panelSidebar.Controls.Add(this.panelProfile);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSidebar.Location = new System.Drawing.Point(0, 38);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(1463, 60);
            this.panelSidebar.TabIndex = 3;
            // 
            // flowNavButtons
            // 
            this.flowNavButtons.Controls.Add(this.btnNavDashboard);
            this.flowNavButtons.Controls.Add(this.btnNavMedicines);
            this.flowNavButtons.Controls.Add(this.btnNavCustomers);
            this.flowNavButtons.Controls.Add(this.btnNavOrders);
            this.flowNavButtons.Controls.Add(this.btnNavReports);
            this.flowNavButtons.Controls.Add(this.btnNavLogout);
            this.flowNavButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNavButtons.Location = new System.Drawing.Point(0, 0);
            this.flowNavButtons.Name = "flowNavButtons";
            this.flowNavButtons.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.flowNavButtons.Size = new System.Drawing.Size(1248, 60);
            this.flowNavButtons.TabIndex = 0;
            this.flowNavButtons.WrapContents = false;
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.Location = new System.Drawing.Point(7, 9);
            this.btnNavDashboard.Margin = new System.Windows.Forms.Padding(2, 9, 2, 9);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Size = new System.Drawing.Size(137, 43);
            this.btnNavDashboard.TabIndex = 0;
            this.btnNavDashboard.Text = "Dashboard";
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.BtnNavDashboard_Click);
            // 
            // btnNavMedicines
            // 
            this.btnNavMedicines.Location = new System.Drawing.Point(148, 9);
            this.btnNavMedicines.Margin = new System.Windows.Forms.Padding(2, 9, 2, 9);
            this.btnNavMedicines.Name = "btnNavMedicines";
            this.btnNavMedicines.Size = new System.Drawing.Size(137, 43);
            this.btnNavMedicines.TabIndex = 1;
            this.btnNavMedicines.Text = "Medicine";
            this.btnNavMedicines.UseVisualStyleBackColor = false;
            this.btnNavMedicines.Click += new System.EventHandler(this.BtnNavMedicines_Click);
            // 
            // btnNavCustomers
            // 
            this.btnNavCustomers.Location = new System.Drawing.Point(289, 9);
            this.btnNavCustomers.Margin = new System.Windows.Forms.Padding(2, 9, 2, 9);
            this.btnNavCustomers.Name = "btnNavCustomers";
            this.btnNavCustomers.Size = new System.Drawing.Size(137, 43);
            this.btnNavCustomers.TabIndex = 2;
            this.btnNavCustomers.Text = "Customers";
            this.btnNavCustomers.UseVisualStyleBackColor = false;
            this.btnNavCustomers.Click += new System.EventHandler(this.BtnNavCustomers_Click);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Location = new System.Drawing.Point(430, 9);
            this.btnNavOrders.Margin = new System.Windows.Forms.Padding(2, 9, 2, 9);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(137, 43);
            this.btnNavOrders.TabIndex = 3;
            this.btnNavOrders.Text = "Orders";
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavReports
            // 
            this.btnNavReports.Location = new System.Drawing.Point(571, 9);
            this.btnNavReports.Margin = new System.Windows.Forms.Padding(2, 9, 2, 9);
            this.btnNavReports.Name = "btnNavReports";
            this.btnNavReports.Size = new System.Drawing.Size(137, 43);
            this.btnNavReports.TabIndex = 4;
            this.btnNavReports.Text = "Reports";
            this.btnNavReports.UseVisualStyleBackColor = false;
            this.btnNavReports.Click += new System.EventHandler(this.BtnNavReports_Click);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNavLogout.Location = new System.Drawing.Point(712, 9);
            this.btnNavLogout.Margin = new System.Windows.Forms.Padding(2, 9, 2, 9);
            this.btnNavLogout.Name = "btnNavLogout";
            this.btnNavLogout.Size = new System.Drawing.Size(137, 43);
            this.btnNavLogout.TabIndex = 5;
            this.btnNavLogout.Text = "LOGOUT";
            this.btnNavLogout.UseVisualStyleBackColor = false;
            this.btnNavLogout.Click += new System.EventHandler(this.BtnNavLogout_Click);
            // 
            // panelProfile
            // 
            this.panelProfile.Controls.Add(this.lblProfileRole);
            this.panelProfile.Controls.Add(this.lblProfileName);
            this.panelProfile.Controls.Add(this.panelAvatar);
            this.panelProfile.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelProfile.Location = new System.Drawing.Point(1248, 0);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Padding = new System.Windows.Forms.Padding(9, 9, 14, 9);
            this.panelProfile.Size = new System.Drawing.Size(215, 60);
            this.panelProfile.TabIndex = 1;
            // 
            // lblProfileRole
            // 
            this.lblProfileRole.AutoSize = true;
            this.lblProfileRole.Location = new System.Drawing.Point(55, 32);
            this.lblProfileRole.Name = "lblProfileRole";
            this.lblProfileRole.Size = new System.Drawing.Size(109, 16);
            this.lblProfileRole.TabIndex = 2;
            this.lblProfileRole.Text = "SYSTEM ADMIN";
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(55, 13);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(119, 16);
            this.lblProfileName.TabIndex = 1;
            this.lblProfileName.Text = "ADMINISTRATOR";
            // 
            // panelAvatar
            // 
            this.panelAvatar.Location = new System.Drawing.Point(9, 13);
            this.panelAvatar.Name = "panelAvatar";
            this.panelAvatar.Size = new System.Drawing.Size(37, 34);
            this.panelAvatar.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 98);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1463, 691);
            this.panelContent.TabIndex = 4;
            // 
            // AdminHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1463, 819);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1337, 729);
            this.Name = "AdminHostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Operational Dashboard";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelWinControls.ResumeLayout(false);
            this.panelStatusBar.ResumeLayout(false);
            this.panelStatusBar.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.flowNavButtons.ResumeLayout(false);
            this.panelProfile.ResumeLayout(false);
            this.panelProfile.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitleBar;
        private System.Windows.Forms.Panel panelWinControls;
        private System.Windows.Forms.Button btnWinMinimize;
        private System.Windows.Forms.Button btnWinMaximize;
        private System.Windows.Forms.Button btnWinClose;
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.Label lblStatusHealth;
        private System.Windows.Forms.Label lblStatusCloud;
        private System.Windows.Forms.Label lblStatusTime;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.FlowLayoutPanel flowNavButtons;
        private System.Windows.Forms.Panel panelProfile;
        private System.Windows.Forms.Panel panelAvatar;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Label lblProfileRole;
        private SmartMed.UI.NavButton btnNavDashboard;
        private SmartMed.UI.NavButton btnNavMedicines;
        private SmartMed.UI.NavButton btnNavCustomers;
        private SmartMed.UI.NavButton btnNavOrders;
        private SmartMed.UI.NavButton btnNavReports;
        private SmartMed.UI.NavButton btnNavLogout;
        private System.Windows.Forms.Panel panelContent;
    }
}
