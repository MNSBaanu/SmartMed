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
            this.btnWinMinimize = new System.Windows.Forms.Button();
            this.btnWinMaximize = new System.Windows.Forms.Button();
            this.btnWinClose = new System.Windows.Forms.Button();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusTime = new System.Windows.Forms.Label();
            this.lblStatusCloud = new System.Windows.Forms.Label();
            this.lblStatusHealth = new System.Windows.Forms.Label();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.flowNavButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnNavHome = new SmartMed.UI.NavButton();
            this.btnNavBrowse = new SmartMed.UI.NavButton();
            this.btnNavCart = new SmartMed.UI.NavButton();
            this.btnNavOrders = new SmartMed.UI.NavButton();
            this.btnNavProfile = new SmartMed.UI.NavButton();
            this.btnNavLogout = new SmartMed.UI.NavButton();
            this.panelSupport = new System.Windows.Forms.Panel();
            this.btnSupportContact = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTitleBar.SuspendLayout();
            this.panelWinControls.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.flowNavButtons.SuspendLayout();
            this.panelSupport.SuspendLayout();
            this.SuspendLayout();

            this.panelTitleBar.Controls.Add(this.lblTitleBar);
            this.panelTitleBar.Controls.Add(this.panelWinControls);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1280, 36);
            this.panelTitleBar.TabIndex = 0;

            this.lblTitleBar.AutoSize = true;
            this.lblTitleBar.Location = new System.Drawing.Point(12, 10);
            this.lblTitleBar.Name = "lblTitleBar";
            this.lblTitleBar.Size = new System.Drawing.Size(184, 15);
            this.lblTitleBar.TabIndex = 0;
            this.lblTitleBar.Text = "SmartMed Health Portal - Home";

            this.panelWinControls.Controls.Add(this.btnWinMinimize);
            this.panelWinControls.Controls.Add(this.btnWinMaximize);
            this.panelWinControls.Controls.Add(this.btnWinClose);
            this.panelWinControls.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelWinControls.Location = new System.Drawing.Point(1100, 0);
            this.panelWinControls.Name = "panelWinControls";
            this.panelWinControls.Size = new System.Drawing.Size(180, 36);
            this.panelWinControls.TabIndex = 1;

            this.btnWinMinimize.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinMinimize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMinimize.Location = new System.Drawing.Point(120, 0);
            this.btnWinMinimize.Name = "btnWinMinimize";
            this.btnWinMinimize.Size = new System.Drawing.Size(60, 36);
            this.btnWinMinimize.TabIndex = 0;
            this.btnWinMinimize.Text = "—";
            this.btnWinMinimize.UseVisualStyleBackColor = false;
            this.btnWinMinimize.Click += new System.EventHandler(this.BtnWinMinimize_Click);

            this.btnWinMaximize.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinMaximize.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinMaximize.Location = new System.Drawing.Point(60, 0);
            this.btnWinMaximize.Name = "btnWinMaximize";
            this.btnWinMaximize.Size = new System.Drawing.Size(60, 36);
            this.btnWinMaximize.TabIndex = 1;
            this.btnWinMaximize.Text = "☐";
            this.btnWinMaximize.UseVisualStyleBackColor = false;
            this.btnWinMaximize.Click += new System.EventHandler(this.BtnWinMaximize_Click);

            this.btnWinClose.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnWinClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnWinClose.Location = new System.Drawing.Point(0, 0);
            this.btnWinClose.Name = "btnWinClose";
            this.btnWinClose.Size = new System.Drawing.Size(60, 36);
            this.btnWinClose.TabIndex = 2;
            this.btnWinClose.Text = "✕";
            this.btnWinClose.UseVisualStyleBackColor = false;
            this.btnWinClose.Click += new System.EventHandler(this.BtnWinClose_Click);

            this.panelStatusBar.Controls.Add(this.lblStatusTime);
            this.panelStatusBar.Controls.Add(this.lblStatusCloud);
            this.panelStatusBar.Controls.Add(this.lblStatusHealth);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 740);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.panelStatusBar.Size = new System.Drawing.Size(1280, 28);
            this.panelStatusBar.TabIndex = 2;

            this.lblStatusTime.AutoSize = true;
            this.lblStatusTime.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatusTime.Location = new System.Drawing.Point(1175, 0);
            this.lblStatusTime.Name = "lblStatusTime";
            this.lblStatusTime.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.lblStatusTime.Size = new System.Drawing.Size(89, 19);
            this.lblStatusTime.TabIndex = 2;
            this.lblStatusTime.Text = "Local Time: --:--";
            this.lblStatusTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

            this.lblStatusCloud.AutoSize = true;
            this.lblStatusCloud.Location = new System.Drawing.Point(200, 4);
            this.lblStatusCloud.Name = "lblStatusCloud";
            this.lblStatusCloud.Size = new System.Drawing.Size(89, 15);
            this.lblStatusCloud.TabIndex = 1;
            this.lblStatusCloud.Text = "Secure Session";

            this.lblStatusHealth.AutoSize = true;
            this.lblStatusHealth.Location = new System.Drawing.Point(16, 4);
            this.lblStatusHealth.Name = "lblStatusHealth";
            this.lblStatusHealth.Size = new System.Drawing.Size(126, 15);
            this.lblStatusHealth.TabIndex = 0;
            this.lblStatusHealth.Text = "Welcome to SmartMed";

            this.panelSidebar.Controls.Add(this.flowNavButtons);
            this.panelSidebar.Controls.Add(this.panelSupport);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSidebar.Location = new System.Drawing.Point(0, 36);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(1280, 56);
            this.panelSidebar.TabIndex = 3;

            this.flowNavButtons.Controls.Add(this.btnNavHome);
            this.flowNavButtons.Controls.Add(this.btnNavBrowse);
            this.flowNavButtons.Controls.Add(this.btnNavCart);
            this.flowNavButtons.Controls.Add(this.btnNavOrders);
            this.flowNavButtons.Controls.Add(this.btnNavProfile);
            this.flowNavButtons.Controls.Add(this.btnNavLogout);
            this.flowNavButtons.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowNavButtons.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowNavButtons.Location = new System.Drawing.Point(0, 0);
            this.flowNavButtons.Name = "flowNavButtons";
            this.flowNavButtons.Padding = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.flowNavButtons.Size = new System.Drawing.Size(1148, 56);
            this.flowNavButtons.TabIndex = 0;
            this.flowNavButtons.WrapContents = false;

            this.btnNavHome.Location = new System.Drawing.Point(6, 8);
            this.btnNavHome.Margin = new System.Windows.Forms.Padding(2, 8, 2, 8);
            this.btnNavHome.Name = "btnNavHome";
            this.btnNavHome.Size = new System.Drawing.Size(120, 40);
            this.btnNavHome.TabIndex = 0;
            this.btnNavHome.Text = "Home";
            this.btnNavHome.UseVisualStyleBackColor = false;
            this.btnNavHome.Click += new System.EventHandler(this.BtnNavHome_Click);

            this.btnNavBrowse.Location = new System.Drawing.Point(130, 8);
            this.btnNavBrowse.Margin = new System.Windows.Forms.Padding(2, 8, 2, 8);
            this.btnNavBrowse.Name = "btnNavBrowse";
            this.btnNavBrowse.Size = new System.Drawing.Size(140, 40);
            this.btnNavBrowse.TabIndex = 1;
            this.btnNavBrowse.Text = "Browse Medicine";
            this.btnNavBrowse.UseVisualStyleBackColor = false;
            this.btnNavBrowse.Click += new System.EventHandler(this.BtnNavBrowse_Click);

            this.btnNavCart.Location = new System.Drawing.Point(274, 8);
            this.btnNavCart.Margin = new System.Windows.Forms.Padding(2, 8, 2, 8);
            this.btnNavCart.Name = "btnNavCart";
            this.btnNavCart.Size = new System.Drawing.Size(120, 40);
            this.btnNavCart.TabIndex = 2;
            this.btnNavCart.Text = "My Cart";
            this.btnNavCart.UseVisualStyleBackColor = false;
            this.btnNavCart.Click += new System.EventHandler(this.BtnNavCart_Click);

            this.btnNavOrders.Location = new System.Drawing.Point(398, 8);
            this.btnNavOrders.Margin = new System.Windows.Forms.Padding(2, 8, 2, 8);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(120, 40);
            this.btnNavOrders.TabIndex = 3;
            this.btnNavOrders.Text = "My Orders";
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);

            this.btnNavProfile.Location = new System.Drawing.Point(522, 8);
            this.btnNavProfile.Margin = new System.Windows.Forms.Padding(2, 8, 2, 8);
            this.btnNavProfile.Name = "btnNavProfile";
            this.btnNavProfile.Size = new System.Drawing.Size(120, 40);
            this.btnNavProfile.TabIndex = 4;
            this.btnNavProfile.Text = "My Profile";
            this.btnNavProfile.UseVisualStyleBackColor = false;
            this.btnNavProfile.Click += new System.EventHandler(this.BtnNavProfile_Click);

            this.btnNavLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNavLogout.Location = new System.Drawing.Point(646, 8);
            this.btnNavLogout.Margin = new System.Windows.Forms.Padding(2, 8, 2, 8);
            this.btnNavLogout.Name = "btnNavLogout";
            this.btnNavLogout.Size = new System.Drawing.Size(120, 40);
            this.btnNavLogout.TabIndex = 5;
            this.btnNavLogout.Text = "LOGOUT";
            this.btnNavLogout.UseVisualStyleBackColor = false;
            this.btnNavLogout.Click += new System.EventHandler(this.BtnNavLogout_Click);

            this.panelSupport.Controls.Add(this.btnSupportContact);
            this.panelSupport.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSupport.Location = new System.Drawing.Point(1148, 0);
            this.panelSupport.Name = "panelSupport";
            this.panelSupport.Padding = new System.Windows.Forms.Padding(8, 8, 12, 8);
            this.panelSupport.Size = new System.Drawing.Size(132, 56);
            this.panelSupport.TabIndex = 1;

            this.btnSupportContact.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSupportContact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupportContact.Location = new System.Drawing.Point(8, 8);
            this.btnSupportContact.Name = "btnSupportContact";
            this.btnSupportContact.Size = new System.Drawing.Size(112, 40);
            this.btnSupportContact.TabIndex = 0;
            this.btnSupportContact.Text = "CONTACT";
            this.btnSupportContact.UseVisualStyleBackColor = false;
            this.btnSupportContact.Click += new System.EventHandler(this.BtnSupportContact_Click);

            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 92);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(1280, 648);
            this.panelContent.TabIndex = 4;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1280, 768);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelStatusBar);
            this.Controls.Add(this.panelTitleBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(1170, 683);
            this.Name = "CustomerHostForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Customer Home";
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.panelWinControls.ResumeLayout(false);
            this.panelStatusBar.ResumeLayout(false);
            this.panelStatusBar.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.flowNavButtons.ResumeLayout(false);
            this.panelSupport.ResumeLayout(false);
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
        private SmartMed.UI.NavButton btnNavHome;
        private SmartMed.UI.NavButton btnNavBrowse;
        private SmartMed.UI.NavButton btnNavCart;
        private SmartMed.UI.NavButton btnNavOrders;
        private SmartMed.UI.NavButton btnNavProfile;
        private SmartMed.UI.NavButton btnNavLogout;
        private System.Windows.Forms.Panel panelSupport;
        private System.Windows.Forms.Button btnSupportContact;
        private System.Windows.Forms.Panel panelContent;
    }
}
