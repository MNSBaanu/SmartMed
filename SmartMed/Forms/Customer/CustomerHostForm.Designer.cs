namespace SmartMed.UI
{
    partial class CustomerHostForm
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
            this.lblMenuAccount = new System.Windows.Forms.Label();
            this.lblMenuOrders = new System.Windows.Forms.Label();
            this.lblMenuShop = new System.Windows.Forms.Label();
            this.lblMenuView = new System.Windows.Forms.Label();
            this.lblMenuFile = new System.Windows.Forms.Label();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusTime = new System.Windows.Forms.Label();
            this.lblStatusCloud = new System.Windows.Forms.Label();
            this.lblStatusHealth = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnNavExit = new System.Windows.Forms.Button();
            this.btnNavProfile = new System.Windows.Forms.Button();
            this.lblNavAccount = new System.Windows.Forms.Label();
            this.btnNavOrders = new System.Windows.Forms.Button();
            this.btnNavCart = new System.Windows.Forms.Button();
            this.btnNavBrowse = new System.Windows.Forms.Button();
            this.btnNavHome = new System.Windows.Forms.Button();
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
            this.lblTitleBar.Size = new System.Drawing.Size(220, 15);
            this.lblTitleBar.TabIndex = 0;
            this.lblTitleBar.Text = "SmartMed Customer Portal - Home";
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
            this.panelMenuBar.Controls.Add(this.lblMenuAccount);
            this.panelMenuBar.Controls.Add(this.lblMenuOrders);
            this.panelMenuBar.Controls.Add(this.lblMenuShop);
            this.panelMenuBar.Controls.Add(this.lblMenuView);
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
            this.lblMenuHelp.Location = new System.Drawing.Point(268, 4);
            this.lblMenuHelp.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuHelp.Name = "lblMenuHelp";
            this.lblMenuHelp.Size = new System.Drawing.Size(32, 15);
            this.lblMenuHelp.TabIndex = 5;
            this.lblMenuHelp.Text = "Help";
            // 
            // lblMenuAccount
            // 
            this.lblMenuAccount.AutoSize = true;
            this.lblMenuAccount.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuAccount.Location = new System.Drawing.Point(200, 4);
            this.lblMenuAccount.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuAccount.Name = "lblMenuAccount";
            this.lblMenuAccount.Size = new System.Drawing.Size(52, 15);
            this.lblMenuAccount.TabIndex = 4;
            this.lblMenuAccount.Text = "Account";
            // 
            // lblMenuOrders
            // 
            this.lblMenuOrders.AutoSize = true;
            this.lblMenuOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuOrders.Location = new System.Drawing.Point(136, 4);
            this.lblMenuOrders.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuOrders.Name = "lblMenuOrders";
            this.lblMenuOrders.Size = new System.Drawing.Size(46, 15);
            this.lblMenuOrders.TabIndex = 3;
            this.lblMenuOrders.Text = "Orders";
            // 
            // lblMenuShop
            // 
            this.lblMenuShop.AutoSize = true;
            this.lblMenuShop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuShop.Location = new System.Drawing.Point(84, 4);
            this.lblMenuShop.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuShop.Name = "lblMenuShop";
            this.lblMenuShop.Size = new System.Drawing.Size(36, 15);
            this.lblMenuShop.TabIndex = 2;
            this.lblMenuShop.Text = "Shop";
            // 
            // lblMenuView
            // 
            this.lblMenuView.AutoSize = true;
            this.lblMenuView.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenuView.Location = new System.Drawing.Point(44, 4);
            this.lblMenuView.Margin = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblMenuView.Name = "lblMenuView";
            this.lblMenuView.Size = new System.Drawing.Size(34, 15);
            this.lblMenuView.TabIndex = 1;
            this.lblMenuView.Text = "View";
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
            this.lblStatusCloud.Size = new System.Drawing.Size(92, 15);
            this.lblStatusCloud.TabIndex = 1;
            this.lblStatusCloud.Text = "Secure Session";
            // 
            // lblStatusHealth
            // 
            this.lblStatusHealth.AutoSize = true;
            this.lblStatusHealth.Location = new System.Drawing.Point(12, 4);
            this.lblStatusHealth.Name = "lblStatusHealth";
            this.lblStatusHealth.Size = new System.Drawing.Size(138, 15);
            this.lblStatusHealth.TabIndex = 0;
            this.lblStatusHealth.Text = "Welcome to SmartMed";
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.btnNavExit);
            this.panelSidebar.Controls.Add(this.btnNavProfile);
            this.panelSidebar.Controls.Add(this.lblNavAccount);
            this.panelSidebar.Controls.Add(this.btnNavOrders);
            this.panelSidebar.Controls.Add(this.btnNavCart);
            this.panelSidebar.Controls.Add(this.btnNavBrowse);
            this.panelSidebar.Controls.Add(this.btnNavHome);
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
            this.btnNavExit.Location = new System.Drawing.Point(0, 332);
            this.btnNavExit.Name = "btnNavExit";
            this.btnNavExit.Size = new System.Drawing.Size(220, 36);
            this.btnNavExit.TabIndex = 7;
            this.btnNavExit.Text = "Exit";
            this.btnNavExit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavExit.UseVisualStyleBackColor = false;
            this.btnNavExit.Click += new System.EventHandler(this.BtnNavExit_Click);
            // 
            // btnNavProfile
            // 
            this.btnNavProfile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavProfile.Location = new System.Drawing.Point(0, 296);
            this.btnNavProfile.Name = "btnNavProfile";
            this.btnNavProfile.Size = new System.Drawing.Size(220, 36);
            this.btnNavProfile.TabIndex = 6;
            this.btnNavProfile.Text = "My Profile";
            this.btnNavProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavProfile.UseVisualStyleBackColor = false;
            this.btnNavProfile.Click += new System.EventHandler(this.BtnNavProfile_Click);
            // 
            // lblNavAccount
            // 
            this.lblNavAccount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavAccount.Location = new System.Drawing.Point(0, 272);
            this.lblNavAccount.Name = "lblNavAccount";
            this.lblNavAccount.Padding = new System.Windows.Forms.Padding(16, 8, 0, 4);
            this.lblNavAccount.Size = new System.Drawing.Size(220, 24);
            this.lblNavAccount.TabIndex = 5;
            this.lblNavAccount.Text = "MY ACCOUNT";
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavOrders.Location = new System.Drawing.Point(0, 236);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(220, 36);
            this.btnNavOrders.TabIndex = 4;
            this.btnNavOrders.Text = "My Orders";
            this.btnNavOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavCart
            // 
            this.btnNavCart.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavCart.Location = new System.Drawing.Point(0, 200);
            this.btnNavCart.Name = "btnNavCart";
            this.btnNavCart.Size = new System.Drawing.Size(220, 36);
            this.btnNavCart.TabIndex = 3;
            this.btnNavCart.Text = "My Cart";
            this.btnNavCart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCart.UseVisualStyleBackColor = false;
            this.btnNavCart.Click += new System.EventHandler(this.BtnNavCart_Click);
            // 
            // btnNavBrowse
            // 
            this.btnNavBrowse.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavBrowse.Location = new System.Drawing.Point(0, 164);
            this.btnNavBrowse.Name = "btnNavBrowse";
            this.btnNavBrowse.Size = new System.Drawing.Size(220, 36);
            this.btnNavBrowse.TabIndex = 2;
            this.btnNavBrowse.Text = "Browse Medicines";
            this.btnNavBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBrowse.UseVisualStyleBackColor = false;
            this.btnNavBrowse.Click += new System.EventHandler(this.BtnNavBrowse_Click);
            // 
            // btnNavHome
            // 
            this.btnNavHome.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNavHome.Location = new System.Drawing.Point(0, 128);
            this.btnNavHome.Name = "btnNavHome";
            this.btnNavHome.Size = new System.Drawing.Size(220, 36);
            this.btnNavHome.TabIndex = 1;
            this.btnNavHome.Text = "Home";
            this.btnNavHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHome.UseVisualStyleBackColor = false;
            this.btnNavHome.Click += new System.EventHandler(this.BtnNavHome_Click);
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
            this.lblProfileRole.Size = new System.Drawing.Size(58, 15);
            this.lblProfileRole.TabIndex = 2;
            this.lblProfileRole.Text = "Customer";
            // 
            // lblProfileName
            // 
            this.lblProfileName.AutoSize = true;
            this.lblProfileName.Location = new System.Drawing.Point(68, 28);
            this.lblProfileName.Name = "lblProfileName";
            this.lblProfileName.Size = new System.Drawing.Size(59, 15);
            this.lblProfileName.TabIndex = 1;
            this.lblProfileName.Text = "Customer";
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
            // CustomerHostForm
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
            this.Name = "CustomerHostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Customer Home";
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
        private System.Windows.Forms.Label lblMenuView;
        private System.Windows.Forms.Label lblMenuShop;
        private System.Windows.Forms.Label lblMenuOrders;
        private System.Windows.Forms.Label lblMenuAccount;
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
        private System.Windows.Forms.Button btnNavHome;
        private System.Windows.Forms.Button btnNavBrowse;
        private System.Windows.Forms.Button btnNavCart;
        private System.Windows.Forms.Button btnNavOrders;
        private System.Windows.Forms.Label lblNavAccount;
        private System.Windows.Forms.Button btnNavProfile;
        private System.Windows.Forms.Button btnNavExit;
        private System.Windows.Forms.Panel panelContent;
    }
}
