namespace SmartMed.UI
{
    partial class CustomerShellForm
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
            this.btnNavProfile = new System.Windows.Forms.Button();
            this.btnNavOrders = new System.Windows.Forms.Button();
            this.btnNavCart = new System.Windows.Forms.Button();
            this.btnNavBrowse = new System.Windows.Forms.Button();
            this.btnNavHome = new System.Windows.Forms.Button();
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
            this.lblTopSubtitle.Size = new System.Drawing.Size(108, 15);
            this.lblTopSubtitle.TabIndex = 1;
            this.lblTopSubtitle.Text = "Customer Portal";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1140, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 32);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "X";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelSidebar
            // 
            this.panelSidebar.Controls.Add(this.btnNavLogout);
            this.panelSidebar.Controls.Add(this.btnNavProfile);
            this.panelSidebar.Controls.Add(this.btnNavOrders);
            this.panelSidebar.Controls.Add(this.btnNavCart);
            this.panelSidebar.Controls.Add(this.btnNavBrowse);
            this.panelSidebar.Controls.Add(this.btnNavHome);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 56);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 8, 0, 16);
            this.panelSidebar.Size = new System.Drawing.Size(240, 712);
            this.panelSidebar.TabIndex = 1;
            // 
            // btnNavHome
            // 
            this.btnNavHome.Location = new System.Drawing.Point(0, 8);
            this.btnNavHome.Name = "btnNavHome";
            this.btnNavHome.Size = new System.Drawing.Size(240, 40);
            this.btnNavHome.TabIndex = 0;
            this.btnNavHome.Text = "Home";
            this.btnNavHome.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavHome.UseVisualStyleBackColor = false;
            this.btnNavHome.Click += new System.EventHandler(this.BtnNavHome_Click);
            // 
            // btnNavBrowse
            // 
            this.btnNavBrowse.Location = new System.Drawing.Point(0, 50);
            this.btnNavBrowse.Name = "btnNavBrowse";
            this.btnNavBrowse.Size = new System.Drawing.Size(240, 40);
            this.btnNavBrowse.TabIndex = 1;
            this.btnNavBrowse.Text = "Browse Medicines";
            this.btnNavBrowse.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavBrowse.UseVisualStyleBackColor = false;
            this.btnNavBrowse.Click += new System.EventHandler(this.BtnNavBrowse_Click);
            // 
            // btnNavCart
            // 
            this.btnNavCart.Location = new System.Drawing.Point(0, 92);
            this.btnNavCart.Name = "btnNavCart";
            this.btnNavCart.Size = new System.Drawing.Size(240, 40);
            this.btnNavCart.TabIndex = 2;
            this.btnNavCart.Text = "My Cart";
            this.btnNavCart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavCart.UseVisualStyleBackColor = false;
            this.btnNavCart.Click += new System.EventHandler(this.BtnNavCart_Click);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.Location = new System.Drawing.Point(0, 134);
            this.btnNavOrders.Name = "btnNavOrders";
            this.btnNavOrders.Size = new System.Drawing.Size(240, 40);
            this.btnNavOrders.TabIndex = 3;
            this.btnNavOrders.Text = "My Orders";
            this.btnNavOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavOrders.UseVisualStyleBackColor = false;
            this.btnNavOrders.Click += new System.EventHandler(this.BtnNavOrders_Click);
            // 
            // btnNavProfile
            // 
            this.btnNavProfile.Location = new System.Drawing.Point(0, 176);
            this.btnNavProfile.Name = "btnNavProfile";
            this.btnNavProfile.Size = new System.Drawing.Size(240, 40);
            this.btnNavProfile.TabIndex = 4;
            this.btnNavProfile.Text = "My Profile";
            this.btnNavProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavProfile.UseVisualStyleBackColor = false;
            this.btnNavProfile.Click += new System.EventHandler(this.BtnNavProfile_Click);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNavLogout.Location = new System.Drawing.Point(0, 656);
            this.btnNavLogout.Name = "btnNavLogout";
            this.btnNavLogout.Size = new System.Drawing.Size(240, 40);
            this.btnNavLogout.TabIndex = 5;
            this.btnNavLogout.Text = "Logout";
            this.btnNavLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNavLogout.UseVisualStyleBackColor = false;
            this.btnNavLogout.Click += new System.EventHandler(this.BtnNavLogout_Click);
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(240, 56);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(24);
            this.panelContent.Size = new System.Drawing.Size(944, 712);
            this.panelContent.TabIndex = 2;
            // 
            // CustomerShellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 768);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSidebar);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Roboto", 9F);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.Name = "CustomerShellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed Customer Portal";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        protected System.Windows.Forms.Panel panelTop;
        protected System.Windows.Forms.Label lblTopBrand;
        protected System.Windows.Forms.Label lblTopSubtitle;
        protected System.Windows.Forms.Button btnClose;
        protected System.Windows.Forms.Panel panelSidebar;
        protected System.Windows.Forms.Button btnNavHome;
        protected System.Windows.Forms.Button btnNavBrowse;
        protected System.Windows.Forms.Button btnNavCart;
        protected System.Windows.Forms.Button btnNavOrders;
        protected System.Windows.Forms.Button btnNavProfile;
        protected System.Windows.Forms.Button btnNavLogout;
        protected System.Windows.Forms.Panel panelContent;
    }
}
