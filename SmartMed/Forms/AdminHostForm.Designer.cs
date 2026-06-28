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
            this.btnWinClose = new System.Windows.Forms.Button();
            this.btnWinMaximize = new System.Windows.Forms.Button();
            this.btnWinMinimize = new System.Windows.Forms.Button();
            this.panelMenuBar = new System.Windows.Forms.Panel();
            this.lblMenuHelp = new System.Windows.Forms.Label();
            this.lblMenuTools = new System.Windows.Forms.Label();
            this.lblMenuReports = new System.Windows.Forms.Label();
            this.lblMenuInventory = new System.Windows.Forms.Label();
            this.lblMenuView = new System.Windows.Forms.Label();
            this.lblMenuEdit = new System.Windows.Forms.Label();
            this.lblMenuFile = new System.Windows.Forms.Label();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusTime = new System.Windows.Forms.Label();
            this.lblStatusCloud = new System.Windows.Forms.Label();
            this.lblStatusHealth = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnNavExit = new System.Windows.Forms.Button();
            this.btnNavAccess = new System.Windows.Forms.Button();
            this.btnNavConfig = new System.Windows.Forms.Button();
            this.lblNavSystem = new System.Windows.Forms.Label();
            this.btnNavReports = new System.Windows.Forms.Button();
            this.btnNavOrders = new System.Windows.Forms.Button();
            this.btnNavCustomers = new System.Windows.Forms.Button();
            this.btnNavMedicines = new System.Windows.Forms.Button();
            this.btnNavDashboard = new System.Windows.Forms.Button();
            this.panelProfile = new System.Windows.Forms.Panel();
            this.lblProfileRole = new System.Windows.Forms.Label();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.panelAvatar = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTitleBar.SuspendLayout();
            this.panelWinControls.SuspendLayout();
            this.panelMenuBar.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.panelSidebar.SuspendLayout();
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
            this.panelTitleBar.Size = new System.Drawing.Size(1280, 32);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblTitleBar
            // 
            this.lblTitleBar.AutoSize = true;
            this.lblTitleBar.Location = new System.Drawing.Point(10, 8);
            this.lblTitleBar.Name = "lblTitleBar";
            this.lblTitleBar.Size = new System.Drawing.Size(285, 15);
            this.lblTitleBar.TabIndex = 0;
            this.lblTitleBar.Text = "SmartMed - Clinical Management System (v4.2.0)";
            // 
            // panelWinControls
            // 
            this.panelWinControls.Controls.Add(this.btnWinClose);
            this.panelWinControls.Controls.Add(this.btnWinMaximize);
            this.panelWinControls.Controls.Add(this.btnWinMinimize);
            this.panelWinControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelWinControls.Location = new System.Drawing.Point(1142, 0);
            this.panelWinControls.Name = "panelWinControls";
            this.panelWinControls.Size = new System.Drawing.Size(138, 32);
            this.panelWinControls.TabIndex = 1;
            // 
            // btnWinClose
            // 
            this.btnWinClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWinClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinClose.Location = new System.Drawing.Point(92, 0);
            this.btnWinClose.Name = "btnWinClose";
            this.btnWinClose.Size = new System.Drawing.Size(46, 32);
            this.btnWinClose.TabIndex = 2;
            this.btnWinClose.Text = "✕";
            this.btnWinClose.UseVisualStyleBackColor = false;
            this.btnWinClose.Click += new System.EventHandler(this.BtnWinClose_Click);
            // 
            // btnWinMaximize
            // 
            this.btnWinMaximize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWinMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMaximize.Location = new System.Drawing.Point(46, 0);
            this.btnWinMaximize.Name = "btnWinMaximize";
            this.btnWinMaximize.Size = new System.Drawing.Size(46, 32);
            this.btnWinMaximize.TabIndex = 1;
            this.btnWinMaximize.Text = "☐";
            this.btnWinMaximize.UseVisualStyleBackColor = false;
            this.btnWinMaximize.Click += new System.EventHandler(this.BtnWinMaximize_Click);
            // 
            // btnWinMinimize
            // 
            this.btnWinMinimize.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnWinMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMinimize.Location = new System.Drawing.Point(0, 0);
            this.btnWinMinimize.Name = "btnWinMinimize";
            this.btnWinMinimize.Size = new System.Drawing.Size(46, 32);
            this.btnWinMinimize.TabIndex = 0;
            this.btnWinMinimize.Text = "—";
            this.btnWinMinimize.UseVisualStyleBackColor = false;
            this.btnWinMinimize.Click += new System.EventHandler(this.BtnWinMinimize_Click);
            // 
            // panelMenuBar
            // 
            this.panelMenuBar.Controls.Add(this.lblMenuHelp);
            this.panelMenuBar.Controls.Add(this.lblMenuTools);
            this.panelMenuBar.Controls.Add(this.lblMenuReports);
            this.panelMenuBar.Controls.Add(this.lblMenuInventory);
            this.panelMenuBar.Controls.Add(this.lblMenuView);
            this.panelMenuBar.Controls.Add(this.lblMenuEdit);
            this.panelMenuBar.Controls.Add(this.lblMenuFile);
            this.panelMenuBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMenuBar.Location = new System.Drawing.Point(0, 32);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.panelMenuBar.Size = new System.Drawing.Size(1280, 24);
            this.panelMenuBar.TabIndex = 1;
            // 
            // lblMenuHelp
            // 
            this.lblMenuHelp.AutoSize = true;
            this.lblMenuHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuHelp.Location = new System.Drawing.Point(318, 4);
            this.lblMenuHelp.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuHelp.Name = "lblMenuHelp";
            this.lblMenuHelp.Size = new System.Drawing.Size(32, 15);
            this.lblMenuHelp.TabIndex = 6;
            this.lblMenuHelp.Text = "Help";
            // 
            // lblMenuTools
            // 
            this.lblMenuTools.AutoSize = true;
            this.lblMenuTools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuTools.Location = new System.Drawing.Point(262, 4);
            this.lblMenuTools.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuTools.Name = "lblMenuTools";
            this.lblMenuTools.Size = new System.Drawing.Size(38, 15);
            this.lblMenuTools.TabIndex = 5;
            this.lblMenuTools.Text = "Tools";
            // 
            // lblMenuReports
            // 
            this.lblMenuReports.AutoSize = true;
            this.lblMenuReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuReports.Location = new System.Drawing.Point(188, 4);
            this.lblMenuReports.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuReports.Name = "lblMenuReports";
            this.lblMenuReports.Size = new System.Drawing.Size(52, 15);
            this.lblMenuReports.TabIndex = 4;
            this.lblMenuReports.Text = "Reports";
            // 
            // lblMenuInventory
            // 
            this.lblMenuInventory.AutoSize = true;
            this.lblMenuInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuInventory.Location = new System.Drawing.Point(112, 4);
            this.lblMenuInventory.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuInventory.Name = "lblMenuInventory";
            this.lblMenuInventory.Size = new System.Drawing.Size(60, 15);
            this.lblMenuInventory.TabIndex = 3;
            this.lblMenuInventory.Text = "Inventory";
            // 
            // lblMenuView
            // 
            this.lblMenuView.AutoSize = true;
            this.lblMenuView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuView.Location = new System.Drawing.Point(68, 4);
            this.lblMenuView.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuView.Name = "lblMenuView";
            this.lblMenuView.Size = new System.Drawing.Size(34, 15);
            this.lblMenuView.TabIndex = 2;
            this.lblMenuView.Text = "View";
            // 
            // lblMenuEdit
            // 
            this.lblMenuEdit.AutoSize = true;
            this.lblMenuEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuEdit.Location = new System.Drawing.Point(36, 4);
            this.lblMenuEdit.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuEdit.Name = "lblMenuEdit";
            this.lblMenuEdit.Size = new System.Drawing.Size(27, 15);
            this.lblMenuEdit.TabIndex = 1;
            this.lblMenuEdit.Text = "Edit";
            // 
            // lblMenuFile
            // 
            this.lblMenuFile.AutoSize = true;
            this.lblMenuFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuFile.Location = new System.Drawing.Point(8, 4);
            this.lblMenuFile.Name = "lblMenuFile";
            this.lblMenuFile.Size = new System.Drawing.Size(25, 15);
            this.lblMenuFile.TabIndex = 0;
            this.lblMenuFile.Text = "File";
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.Controls.Add(this.lblStatusTime);
            this.panelStatusBar.Controls.Add(this.lblStatusCloud);
            this.panelStatusBar.Controls.Add(this.lblStatusHealth);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 776);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Padding = new System.Windows.Forms.Padding(12, 0, 12, 0);
            this.panelStatusBar.Size = new System.Drawing.Size(1280, 24);
            this.panelStatusBar.TabIndex = 2;
            // 
            // lblStatusTime
            // 
            this.lblStatusTime.AutoSize = true;
            this.lblStatusTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatusTime.Location = new System.Drawing.Point(1168, 0);
            this.lblStatusTime.Name = "lblStatusTime";
            this.lblStatusTime.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblStatusTime.Size = new System.Drawing.Size(100, 19);
            this.lblStatusTime.TabIndex = 2;
            this.lblStatusTime.Text = "Local Time: --:--";
            this.lblStatusTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusCloud
            // 
            this.lblStatusCloud.AutoSize = true;
            this.lblStatusCloud.Location = new System.Drawing.Point(200, 4);
            this.lblStatusCloud.Name = "lblStatusCloud";
            this.lblStatusCloud.Size = new System.Drawing.Size(108, 15);
            this.lblStatusCloud.TabIndex = 1;
            this.lblStatusCloud.Text = "Cloud Sync Active";
            // 
            // lblStatusHealth
            // 
            this.lblStatusHealth.AutoSize = true;
            this.lblStatusHealth.Location = new System.Drawing.Point(12, 4);
            this.lblStatusHealth.Name = "lblStatusHealth";
            this.lblStatusHealth.Size = new System.Drawing.Size(138, 15);
            this.lblStatusHealth.TabIndex = 0;
            this.lblStatusHealth.Text = "System Status: Healthy";
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.btnNavExit);
            this.panelSidebar.Controls.Add(this.btnNavAccess);
            this.panelSidebar.Controls.Add(this.btnNavConfig);
            this.panelSidebar.Controls.Add(this.lblNavSystem);
            this.panelSidebar.Controls.Add(this.btnNavReports);
            this.panelSidebar.Controls.Add(this.btnNavOrders);
            this.panelSidebar.Controls.Add(this.btnNavCustomers);
            this.panelSidebar.Controls.Add(this.btnNavMedicines);
            this.panelSidebar.Controls.Add(this.btnNavDashboard);
            this.panelSidebar.Controls.Add(this.panelProfile);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 56);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(220, 720);
            this.panelSidebar.TabIndex = 3;
            // 
            // btnNavExit
            // 
            this.btnNavExit.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavExit.Location = new System.Drawing.Point(0, 404);
            this.btnNavExit.Name = "btnNavExit";
            this.btnNavExit.Size = new System.Drawing.Size(220, 36);
            this.btnNavExit.TabIndex = 9;
            this.btnNavExit.Text = "Exit Application";
            this.btnNavExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavExit.UseVisualStyleBackColor = false;
            this.btnNavExit.Click += new System.EventHandler(this.BtnNavExit_Click);
            // 
            // btnNavAccess
            // 
            this.btnNavAccess.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavAccess.Location = new System.Drawing.Point(0, 368);
            this.btnNavAccess.Name = "btnNavAccess";
            this.btnNavAccess.Size = new System.Drawing.Size(220, 36);
            this.btnNavAccess.TabIndex = 8;
            this.btnNavAccess.Text = "Access Control";
            this.btnNavAccess.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavAccess.UseVisualStyleBackColor = false;
            this.btnNavAccess.Click += new System.EventHandler(this.BtnNavComingSoon_Click);
            // 
            // btnNavConfig
            // 
            this.btnNavConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavConfig.Location = new System.Drawing.Point(0, 332);
            this.btnNavConfig.Name = "btnNavConfig";
            this.btnNavConfig.Size = new System.Drawing.Size(220, 36);
            this.btnNavConfig.TabIndex = 7;
            this.btnNavConfig.Text = "Configuration";
            this.btnNavConfig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavConfig.UseVisualStyleBackColor = false;
            this.btnNavConfig.Click += new System.EventHandler(this.BtnNavComingSoon_Click);
            // 
            // lblNavSystem
            // 
            this.lblNavSystem.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavSystem.Location = new System.Drawing.Point(0, 308);
            this.lblNavSystem.Name = "lblNavSystem";
            this.lblNavSystem.Padding = new System.Windows.Forms.Padding(16, 8, 0, 4);
            this.lblNavSystem.Size = new System.Drawing.Size(220, 24);
            this.lblNavSystem.TabIndex = 6;
            this.lblNavSystem.Text = "SYSTEM";
            // 
            // btnNavReports
            // 
            this.btnNavReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavReports.Location = new System.Drawing.Point(0, 272);
            this.btnNavReports.Name = "btnNavReports";
            this.btnNavReports.Size = new System.Drawing.Size(220, 36);
            this.btnNavReports.TabIndex = 5;
            this.btnNavReports.Text = "Reporting Services";
            this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReports.UseVisualStyleBackColor = false;
            this.btnNavReports.Click += new System.EventHandler(this.BtnNavReports_Click);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavOrders.Location = new System.Drawing.Point(0, 236);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(220, 36);
            this.btnNavOrders.TabIndex = 4;
            this.btnNavOrders.Text = "Order Management";
            this.btnNavOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavCustomers
            // 
            this.btnNavCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavCustomers.Location = new System.Drawing.Point(0, 200);
            this.btnNavCustomers.Name = "btnNavCustomers";
            this.btnNavCustomers.Size = new System.Drawing.Size(220, 36);
            this.btnNavCustomers.TabIndex = 3;
            this.btnNavCustomers.Text = "Customer Database";
            this.btnNavCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCustomers.UseVisualStyleBackColor = false;
            this.btnNavCustomers.Click += new System.EventHandler(this.BtnNavCustomers_Click);
            // 
            // btnNavMedicines
            // 
            this.btnNavMedicines.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavMedicines.Location = new System.Drawing.Point(0, 164);
            this.btnNavMedicines.Name = "btnNavMedicines";
            this.btnNavMedicines.Size = new System.Drawing.Size(220, 36);
            this.btnNavMedicines.TabIndex = 2;
            this.btnNavMedicines.Text = "Inventory Explorer";
            this.btnNavMedicines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavMedicines.UseVisualStyleBackColor = false;
            this.btnNavMedicines.Click += new System.EventHandler(this.BtnNavMedicines_Click);
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDashboard.Location = new System.Drawing.Point(0, 128);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Size = new System.Drawing.Size(220, 36);
            this.btnNavDashboard.TabIndex = 1;
            this.btnNavDashboard.Text = "Dashboard";
            this.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.BtnNavDashboard_Click);
            // 
            // panelProfile
            // 
            this.panelProfile.Controls.Add(this.lblProfileRole);
            this.panelProfile.Controls.Add(this.lblProfileName);
            this.panelProfile.Controls.Add(this.panelAvatar);
            this.panelProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProfile.Location = new System.Drawing.Point(0, 0);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Size = new System.Drawing.Size(220, 128);
            this.panelProfile.TabIndex = 0;
            // 
            // lblProfileRole
            // 
            this.lblProfileRole.AutoSize = true;
            this.lblProfileRole.Location = new System.Drawing.Point(68, 52);
            this.lblProfileRole.Name = "lblProfileRole";
            this.lblProfileRole.Size = new System.Drawing.Size(78, 15);
            this.lblProfileRole.TabIndex = 2;
            this.lblProfileRole.Text = "Administrator";
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(68, 28);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(33, 15);
            this.lblProfileName.TabIndex = 1;
            this.lblProfileName.Text = "Admin";
            // 
            // panelAvatar
            // 
            this.panelAvatar.Location = new System.Drawing.Point(16, 24);
            this.panelAvatar.Name = "panelAvatar";
            this.panelAvatar.Size = new System.Drawing.Size(40, 40);
            this.panelAvatar.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(220, 56);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1060, 720);
            this.panelContent.TabIndex = 4;
            // 
            // AdminHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelMenuBar);
            this.Controls.Add(this.panelTitleBar);
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "AdminHostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Operational Dashboard";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelWinControls.ResumeLayout(false);
            this.panelMenuBar.ResumeLayout(false);
            this.panelMenuBar.PerformLayout();
            this.panelStatusBar.ResumeLayout(false);
            this.panelStatusBar.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panelMenuBar;
        private System.Windows.Forms.Label lblMenuFile;
        private System.Windows.Forms.Label lblMenuEdit;
        private System.Windows.Forms.Label lblMenuView;
        private System.Windows.Forms.Label lblMenuInventory;
        private System.Windows.Forms.Label lblMenuReports;
        private System.Windows.Forms.Label lblMenuTools;
        private System.Windows.Forms.Label lblMenuHelp;
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.Label lblStatusHealth;
        private System.Windows.Forms.Label lblStatusCloud;
        private System.Windows.Forms.Label lblStatusTime;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelProfile;
        private System.Windows.Forms.Panel panelAvatar;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Label lblProfileRole;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Button btnNavMedicines;
        private System.Windows.Forms.Button btnNavCustomers;
        private System.Windows.Forms.Button btnNavOrders;
        private System.Windows.Forms.Button btnNavReports;
        private System.Windows.Forms.Label lblNavSystem;
        private System.Windows.Forms.Button btnNavConfig;
        private System.Windows.Forms.Button btnNavAccess;
        private System.Windows.Forms.Button btnNavExit;
        private System.Windows.Forms.Panel panelContent;
    }
}
