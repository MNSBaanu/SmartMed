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
            this.panelProfile = new System.Windows.Forms.Panel();
            this.lblProfileRole = new System.Windows.Forms.Label();
            this.lblProfileName = new System.Windows.Forms.Label();
            this.panelAvatar = new System.Windows.Forms.Panel();
            this.panelNavSpacer = new System.Windows.Forms.Panel();
            this.btnNavReports = new SmartMed.UI.NavButton();
            this.btnNavOrders = new SmartMed.UI.NavButton();
            this.btnNavCustomers = new SmartMed.UI.NavButton();
            this.btnNavMedicines = new SmartMed.UI.NavButton();
            this.btnNavDashboard = new SmartMed.UI.NavButton();
            this.panelBrand = new System.Windows.Forms.Panel();
            this.lblBrandSubtitle = new System.Windows.Forms.Label();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.panelBrandIcon = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTitleBar.SuspendLayout();
            this.panelWinControls.SuspendLayout();
            this.panelMenuBar.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelProfile.SuspendLayout();
            this.panelBrand.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.Controls.Add(this.lblTitleBar);
            this.panelTitleBar.Controls.Add(this.panelWinControls);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1672, 36);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblTitleBar
            // 
            this.lblTitleBar.AutoSize = true;
            this.lblTitleBar.Location = new System.Drawing.Point(13, 10);
            this.lblTitleBar.Name = "lblTitleBar";
            this.lblTitleBar.Size = new System.Drawing.Size(297, 16);
            this.lblTitleBar.TabIndex = 0;
            this.lblTitleBar.Text = "SmartMed - Clinical Management System (v4.2.0)";
            // 
            // panelWinControls
            // 
            this.panelWinControls.Controls.Add(this.btnWinMinimize);
            this.panelWinControls.Controls.Add(this.btnWinMaximize);
            this.panelWinControls.Controls.Add(this.btnWinClose);
            this.panelWinControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelWinControls.Location = new System.Drawing.Point(1491, 0);
            this.panelWinControls.Name = "panelWinControls";
            this.panelWinControls.Size = new System.Drawing.Size(181, 36);
            this.panelWinControls.TabIndex = 1;
            // 
            // btnWinMinimize
            // 
            this.btnWinMinimize.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMinimize.Location = new System.Drawing.Point(122, 0);
            this.btnWinMinimize.Name = "btnWinMinimize";
            this.btnWinMinimize.Size = new System.Drawing.Size(61, 36);
            this.btnWinMinimize.TabIndex = 0;
            this.btnWinMinimize.Text = "—";
            this.btnWinMinimize.UseVisualStyleBackColor = false;
            this.btnWinMinimize.Click += new System.EventHandler(this.BtnWinMinimize_Click);
            // 
            // btnWinMaximize
            // 
            this.btnWinMaximize.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMaximize.Location = new System.Drawing.Point(61, 0);
            this.btnWinMaximize.Name = "btnWinMaximize";
            this.btnWinMaximize.Size = new System.Drawing.Size(61, 36);
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
            this.btnWinClose.Size = new System.Drawing.Size(61, 36);
            this.btnWinClose.TabIndex = 2;
            this.btnWinClose.Text = "✕";
            this.btnWinClose.UseVisualStyleBackColor = false;
            this.btnWinClose.Click += new System.EventHandler(this.BtnWinClose_Click);
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
            this.panelMenuBar.Location = new System.Drawing.Point(0, 36);
            this.panelMenuBar.Name = "panelMenuBar";
            this.panelMenuBar.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.panelMenuBar.Size = new System.Drawing.Size(1672, 28);
            this.panelMenuBar.TabIndex = 1;
            // 
            // lblMenuHelp
            // 
            this.lblMenuHelp.AutoSize = true;
            this.lblMenuHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuHelp.Location = new System.Drawing.Point(415, 4);
            this.lblMenuHelp.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMenuHelp.Name = "lblMenuHelp";
            this.lblMenuHelp.Size = new System.Drawing.Size(36, 16);
            this.lblMenuHelp.TabIndex = 6;
            this.lblMenuHelp.Text = "Help";
            // 
            // lblMenuTools
            // 
            this.lblMenuTools.AutoSize = true;
            this.lblMenuTools.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuTools.Location = new System.Drawing.Point(342, 4);
            this.lblMenuTools.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMenuTools.Name = "lblMenuTools";
            this.lblMenuTools.Size = new System.Drawing.Size(42, 16);
            this.lblMenuTools.TabIndex = 5;
            this.lblMenuTools.Text = "Tools";
            // 
            // lblMenuReports
            // 
            this.lblMenuReports.AutoSize = true;
            this.lblMenuReports.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuReports.Location = new System.Drawing.Point(246, 4);
            this.lblMenuReports.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMenuReports.Name = "lblMenuReports";
            this.lblMenuReports.Size = new System.Drawing.Size(55, 16);
            this.lblMenuReports.TabIndex = 4;
            this.lblMenuReports.Text = "Reports";
            // 
            // lblMenuInventory
            // 
            this.lblMenuInventory.AutoSize = true;
            this.lblMenuInventory.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuInventory.Location = new System.Drawing.Point(146, 4);
            this.lblMenuInventory.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMenuInventory.Name = "lblMenuInventory";
            this.lblMenuInventory.Size = new System.Drawing.Size(61, 16);
            this.lblMenuInventory.TabIndex = 3;
            this.lblMenuInventory.Text = "Medicine";
            // 
            // lblMenuView
            // 
            this.lblMenuView.AutoSize = true;
            this.lblMenuView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuView.Location = new System.Drawing.Point(89, 4);
            this.lblMenuView.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMenuView.Name = "lblMenuView";
            this.lblMenuView.Size = new System.Drawing.Size(36, 16);
            this.lblMenuView.TabIndex = 2;
            this.lblMenuView.Text = "View";
            // 
            // lblMenuEdit
            // 
            this.lblMenuEdit.AutoSize = true;
            this.lblMenuEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuEdit.Location = new System.Drawing.Point(47, 4);
            this.lblMenuEdit.Margin = new System.Windows.Forms.Padding(16, 0, 0, 0);
            this.lblMenuEdit.Name = "lblMenuEdit";
            this.lblMenuEdit.Size = new System.Drawing.Size(30, 16);
            this.lblMenuEdit.TabIndex = 1;
            this.lblMenuEdit.Text = "Edit";
            // 
            // lblMenuFile
            // 
            this.lblMenuFile.AutoSize = true;
            this.lblMenuFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuFile.Location = new System.Drawing.Point(10, 4);
            this.lblMenuFile.Name = "lblMenuFile";
            this.lblMenuFile.Size = new System.Drawing.Size(29, 16);
            this.lblMenuFile.TabIndex = 0;
            this.lblMenuFile.Text = "File";
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.Controls.Add(this.lblStatusTime);
            this.panelStatusBar.Controls.Add(this.lblStatusCloud);
            this.panelStatusBar.Controls.Add(this.lblStatusHealth);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 882);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.panelStatusBar.Size = new System.Drawing.Size(1672, 28);
            this.panelStatusBar.TabIndex = 2;
            // 
            // lblStatusTime
            // 
            this.lblStatusTime.AutoSize = true;
            this.lblStatusTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatusTime.Location = new System.Drawing.Point(1557, 0);
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
            this.lblStatusCloud.Location = new System.Drawing.Point(262, 4);
            this.lblStatusCloud.Name = "lblStatusCloud";
            this.lblStatusCloud.Size = new System.Drawing.Size(115, 16);
            this.lblStatusCloud.TabIndex = 1;
            this.lblStatusCloud.Text = "Cloud Sync Active";
            // 
            // lblStatusHealth
            // 
            this.lblStatusHealth.AutoSize = true;
            this.lblStatusHealth.Location = new System.Drawing.Point(16, 4);
            this.lblStatusHealth.Name = "lblStatusHealth";
            this.lblStatusHealth.Size = new System.Drawing.Size(144, 16);
            this.lblStatusHealth.TabIndex = 0;
            this.lblStatusHealth.Text = "System Status: Healthy";
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.panelProfile);
            this.panelSidebar.Controls.Add(this.panelNavSpacer);
            this.panelSidebar.Controls.Add(this.btnNavReports);
            this.panelSidebar.Controls.Add(this.btnNavOrders);
            this.panelSidebar.Controls.Add(this.btnNavCustomers);
            this.panelSidebar.Controls.Add(this.btnNavMedicines);
            this.panelSidebar.Controls.Add(this.btnNavDashboard);
            this.panelSidebar.Controls.Add(this.panelBrand);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSidebar.Location = new System.Drawing.Point(0, 64);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(1672, 60);
            this.panelSidebar.TabIndex = 3;
            // 
            // panelProfile
            // 
            this.panelProfile.Controls.Add(this.lblProfileRole);
            this.panelProfile.Controls.Add(this.lblProfileName);
            this.panelProfile.Controls.Add(this.panelAvatar);
            this.panelProfile.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelProfile.Location = new System.Drawing.Point(0, -22);
            this.panelProfile.Name = "panelProfile";
            this.panelProfile.Size = new System.Drawing.Size(1672, 82);
            this.panelProfile.TabIndex = 8;
            // 
            // lblProfileRole
            // 
            this.lblProfileRole.AutoSize = true;
            this.lblProfileRole.Location = new System.Drawing.Point(83, 46);
            this.lblProfileRole.Name = "lblProfileRole";
            this.lblProfileRole.Size = new System.Drawing.Size(109, 16);
            this.lblProfileRole.TabIndex = 2;
            this.lblProfileRole.Text = "SYSTEM ADMIN";
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(83, 22);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(45, 16);
            this.lblProfileName.TabIndex = 1;
            this.lblProfileName.Text = "Admin";
            // 
            // panelAvatar
            // 
            this.panelAvatar.Location = new System.Drawing.Point(31, 22);
            this.panelAvatar.Name = "panelAvatar";
            this.panelAvatar.Size = new System.Drawing.Size(42, 36);
            this.panelAvatar.TabIndex = 0;
            // 
            // panelNavSpacer
            // 
            this.panelNavSpacer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelNavSpacer.Location = new System.Drawing.Point(0, 330);
            this.panelNavSpacer.Name = "panelNavSpacer";
            this.panelNavSpacer.Size = new System.Drawing.Size(1672, 0);
            this.panelNavSpacer.TabIndex = 7;
            // 
            // btnNavReports
            // 
            this.btnNavReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavReports.Location = new System.Drawing.Point(0, 284);
            this.btnNavReports.Name = "btnNavReports";
            this.btnNavReports.Size = new System.Drawing.Size(1672, 46);
            this.btnNavReports.TabIndex = 5;
            this.btnNavReports.Text = "Reports";
            this.btnNavReports.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavReports.UseVisualStyleBackColor = false;
            this.btnNavReports.Click += new System.EventHandler(this.BtnNavReports_Click);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavOrders.Location = new System.Drawing.Point(0, 238);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(1672, 46);
            this.btnNavOrders.TabIndex = 4;
            this.btnNavOrders.Text = "Orders";
            this.btnNavOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavCustomers
            // 
            this.btnNavCustomers.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavCustomers.Location = new System.Drawing.Point(0, 192);
            this.btnNavCustomers.Name = "btnNavCustomers";
            this.btnNavCustomers.Size = new System.Drawing.Size(1672, 46);
            this.btnNavCustomers.TabIndex = 3;
            this.btnNavCustomers.Text = "Customers";
            this.btnNavCustomers.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCustomers.UseVisualStyleBackColor = false;
            this.btnNavCustomers.Click += new System.EventHandler(this.BtnNavCustomers_Click);
            // 
            // btnNavMedicines
            // 
            this.btnNavMedicines.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavMedicines.Location = new System.Drawing.Point(0, 146);
            this.btnNavMedicines.Name = "btnNavMedicines";
            this.btnNavMedicines.Size = new System.Drawing.Size(1672, 46);
            this.btnNavMedicines.TabIndex = 2;
            this.btnNavMedicines.Text = "Medicine";
            this.btnNavMedicines.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavMedicines.UseVisualStyleBackColor = false;
            this.btnNavMedicines.Click += new System.EventHandler(this.BtnNavMedicines_Click);
            // 
            // btnNavDashboard
            // 
            this.btnNavDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavDashboard.Location = new System.Drawing.Point(0, 100);
            this.btnNavDashboard.Name = "btnNavDashboard";
            this.btnNavDashboard.Size = new System.Drawing.Size(1672, 46);
            this.btnNavDashboard.TabIndex = 1;
            this.btnNavDashboard.Text = "Dashboard";
            this.btnNavDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavDashboard.UseVisualStyleBackColor = false;
            this.btnNavDashboard.Click += new System.EventHandler(this.BtnNavDashboard_Click);
            // 
            // panelBrand
            // 
            this.panelBrand.Controls.Add(this.lblBrandSubtitle);
            this.panelBrand.Controls.Add(this.lblBrandTitle);
            this.panelBrand.Controls.Add(this.panelBrandIcon);
            this.panelBrand.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelBrand.Location = new System.Drawing.Point(0, 0);
            this.panelBrand.Name = "panelBrand";
            this.panelBrand.Size = new System.Drawing.Size(1672, 100);
            this.panelBrand.TabIndex = 0;
            // 
            // lblBrandSubtitle
            // 
            this.lblBrandSubtitle.AutoSize = true;
            this.lblBrandSubtitle.Location = new System.Drawing.Point(94, 50);
            this.lblBrandSubtitle.Name = "lblBrandSubtitle";
            this.lblBrandSubtitle.Size = new System.Drawing.Size(132, 16);
            this.lblBrandSubtitle.TabIndex = 2;
            this.lblBrandSubtitle.Text = "Clinical Management";
            // 
            // lblBrandTitle
            // 
            this.lblBrandTitle.AutoSize = true;
            this.lblBrandTitle.Location = new System.Drawing.Point(94, 25);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(69, 16);
            this.lblBrandTitle.TabIndex = 1;
            this.lblBrandTitle.Text = "SmartMed";
            // 
            // panelBrandIcon
            // 
            this.panelBrandIcon.Location = new System.Drawing.Point(31, 22);
            this.panelBrandIcon.Name = "panelBrandIcon";
            this.panelBrandIcon.Size = new System.Drawing.Size(53, 46);
            this.panelBrandIcon.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 124);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1672, 758);
            this.panelContent.TabIndex = 4;
            // 
            // AdminHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1672, 910);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelMenuBar);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1337, 729);
            this.Name = "AdminHostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Operational Dashboard";
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
            this.panelBrand.ResumeLayout(false);
            this.panelBrand.PerformLayout();
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
        private System.Windows.Forms.Panel panelBrand;
        private System.Windows.Forms.Panel panelBrandIcon;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSubtitle;
        private System.Windows.Forms.Panel panelNavSpacer;
        private System.Windows.Forms.Panel panelProfile;
        private System.Windows.Forms.Panel panelAvatar;
        private System.Windows.Forms.Label lblProfileName;
        private System.Windows.Forms.Label lblProfileRole;
        private SmartMed.UI.NavButton btnNavDashboard;
        private SmartMed.UI.NavButton btnNavMedicines;
        private SmartMed.UI.NavButton btnNavCustomers;
        private SmartMed.UI.NavButton btnNavOrders;
        private SmartMed.UI.NavButton btnNavReports;
        private System.Windows.Forms.Panel panelContent;
    }
}
