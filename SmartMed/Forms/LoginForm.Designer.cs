namespace SmartMed.UI
{
    partial class LoginForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelLoginCard = new System.Windows.Forms.Panel();
            this.lnkForgot = new System.Windows.Forms.LinkLabel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlPasswordField = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.pnlUsernameField = new System.Windows.Forms.Panel();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.rdoAdmin = new System.Windows.Forms.RadioButton();
            this.rdoCustomer = new System.Windows.Forms.RadioButton();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblAuthSubtitle = new System.Windows.Forms.Label();
            this.lblAuthTitle = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.pnlBrandIcon = new System.Windows.Forms.Panel();
            this.lblBrandIcon = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelLoginCard.SuspendLayout();
            this.pnlPasswordField.SuspendLayout();
            this.pnlUsernameField.SuspendLayout();
            this.pnlBrandIcon.SuspendLayout();
            this.SuspendLayout();

            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.panelLoginCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(720, 700);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelMain_Paint);
            this.panelMain.Resize += new System.EventHandler(this.PanelMain_Resize);

            this.panelLoginCard.BackColor = System.Drawing.Color.White;
            this.panelLoginCard.Controls.Add(this.lnkForgot);
            this.panelLoginCard.Controls.Add(this.btnRegister);
            this.panelLoginCard.Controls.Add(this.btnLogin);
            this.panelLoginCard.Controls.Add(this.pnlPasswordField);
            this.panelLoginCard.Controls.Add(this.lblPassword);
            this.panelLoginCard.Controls.Add(this.pnlUsernameField);
            this.panelLoginCard.Controls.Add(this.lblUsername);
            this.panelLoginCard.Controls.Add(this.rdoAdmin);
            this.panelLoginCard.Controls.Add(this.rdoCustomer);
            this.panelLoginCard.Controls.Add(this.lblRole);
            this.panelLoginCard.Controls.Add(this.lblAuthSubtitle);
            this.panelLoginCard.Controls.Add(this.lblAuthTitle);
            this.panelLoginCard.Controls.Add(this.lblBrand);
            this.panelLoginCard.Controls.Add(this.pnlBrandIcon);
            this.panelLoginCard.Location = new System.Drawing.Point(140, 32);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Padding = new System.Windows.Forms.Padding(32);
            this.panelLoginCard.Size = new System.Drawing.Size(440, 536);
            this.panelLoginCard.TabIndex = 0;

            this.lnkForgot.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.lnkForgot.AutoSize = true;
            this.lnkForgot.BackColor = System.Drawing.Color.White;
            this.lnkForgot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lnkForgot.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lnkForgot.Location = new System.Drawing.Point(32, 484);
            this.lnkForgot.Name = "lnkForgot";
            this.lnkForgot.Size = new System.Drawing.Size(159, 20);
            this.lnkForgot.TabIndex = 13;
            this.lnkForgot.TabStop = true;
            this.lnkForgot.Text = "Forgot password?";
            this.lnkForgot.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lnkForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkForgot_LinkClicked);

            this.btnRegister.BackColor = System.Drawing.Color.White;
            this.btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnRegister.Location = new System.Drawing.Point(228, 432);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(180, 40);
            this.btnRegister.TabIndex = 12;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);

            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(32, 432);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(180, 40);
            this.btnLogin.TabIndex = 11;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);

            this.pnlPasswordField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlPasswordField.Controls.Add(this.txtPassword);
            this.pnlPasswordField.Controls.Add(this.btnTogglePassword);
            this.pnlPasswordField.Location = new System.Drawing.Point(32, 342);
            this.pnlPasswordField.Name = "pnlPasswordField";
            this.pnlPasswordField.Size = new System.Drawing.Size(376, 40);
            this.pnlPasswordField.TabIndex = 9;

            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtPassword.Location = new System.Drawing.Point(10, 10);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(289, 17);
            this.txtPassword.TabIndex = 0;
            this.txtPassword.Text = "Enter your password";
            this.txtPassword.GotFocus += new System.EventHandler(this.TxtPassword_GotFocus);

            this.btnTogglePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTogglePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnTogglePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnTogglePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnTogglePassword.Location = new System.Drawing.Point(291, 3);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(82, 34);
            this.btnTogglePassword.TabIndex = 1;
            this.btnTogglePassword.Text = "Show";
            this.btnTogglePassword.UseVisualStyleBackColor = false;
            this.btnTogglePassword.Click += new System.EventHandler(this.BtnTogglePassword_Click);

            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.White;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblPassword.Location = new System.Drawing.Point(32, 322);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(75, 18);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password";

            this.pnlUsernameField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlUsernameField.Controls.Add(this.txtUsername);
            this.pnlUsernameField.Location = new System.Drawing.Point(32, 274);
            this.pnlUsernameField.Name = "pnlUsernameField";
            this.pnlUsernameField.Size = new System.Drawing.Size(376, 40);
            this.pnlUsernameField.TabIndex = 7;

            this.txtUsername.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtUsername.Location = new System.Drawing.Point(10, 10);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(356, 17);
            this.txtUsername.TabIndex = 0;
            this.txtUsername.Text = "Enter your email";

            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.White;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblUsername.Location = new System.Drawing.Point(32, 254);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(45, 18);
            this.lblUsername.TabIndex = 6;
            this.lblUsername.Text = "Email";

            this.rdoAdmin.AutoSize = true;
            this.rdoAdmin.BackColor = System.Drawing.Color.White;
            this.rdoAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rdoAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.rdoAdmin.Location = new System.Drawing.Point(148, 224);
            this.rdoAdmin.Name = "rdoAdmin";
            this.rdoAdmin.Size = new System.Drawing.Size(70, 22);
            this.rdoAdmin.TabIndex = 5;
            this.rdoAdmin.Text = "Admin";
            this.rdoAdmin.UseVisualStyleBackColor = false;
            this.rdoAdmin.CheckedChanged += new System.EventHandler(this.Role_CheckedChanged);

            this.rdoCustomer.AutoSize = true;
            this.rdoCustomer.BackColor = System.Drawing.Color.White;
            this.rdoCustomer.Checked = true;
            this.rdoCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.rdoCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.rdoCustomer.Location = new System.Drawing.Point(32, 224);
            this.rdoCustomer.Name = "rdoCustomer";
            this.rdoCustomer.Size = new System.Drawing.Size(92, 22);
            this.rdoCustomer.TabIndex = 4;
            this.rdoCustomer.TabStop = true;
            this.rdoCustomer.Text = "Customer";
            this.rdoCustomer.UseVisualStyleBackColor = false;
            this.rdoCustomer.CheckedChanged += new System.EventHandler(this.Role_CheckedChanged);

            this.lblRole.AutoSize = true;
            this.lblRole.BackColor = System.Drawing.Color.White;
            this.lblRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblRole.Location = new System.Drawing.Point(32, 204);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(78, 18);
            this.lblRole.TabIndex = 3;
            this.lblRole.Text = "Sign in as";

            this.lblAuthSubtitle.AutoSize = false;
            this.lblAuthSubtitle.BackColor = System.Drawing.Color.White;
            this.lblAuthSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblAuthSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblAuthSubtitle.Location = new System.Drawing.Point(32, 178);
            this.lblAuthSubtitle.Name = "lblAuthSubtitle";
            this.lblAuthSubtitle.Size = new System.Drawing.Size(376, 20);
            this.lblAuthSubtitle.TabIndex = 2;
            this.lblAuthSubtitle.Text = "Pharmacy Management System";
            this.lblAuthSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblAuthTitle.AutoSize = false;
            this.lblAuthTitle.BackColor = System.Drawing.Color.White;
            this.lblAuthTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblAuthTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblAuthTitle.Location = new System.Drawing.Point(32, 154);
            this.lblAuthTitle.Name = "lblAuthTitle";
            this.lblAuthTitle.Size = new System.Drawing.Size(376, 28);
            this.lblAuthTitle.TabIndex = 1;
            this.lblAuthTitle.Text = "System Authentication";
            this.lblAuthTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblBrand.AutoSize = false;
            this.lblBrand.BackColor = System.Drawing.Color.White;
            this.lblBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lblBrand.Location = new System.Drawing.Point(32, 116);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(376, 32);
            this.lblBrand.TabIndex = 0;
            this.lblBrand.Text = "SmartMed Clinical";
            this.lblBrand.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.pnlBrandIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.pnlBrandIcon.Controls.Add(this.lblBrandIcon);
            this.pnlBrandIcon.Location = new System.Drawing.Point(188, 32);
            this.pnlBrandIcon.Name = "pnlBrandIcon";
            this.pnlBrandIcon.Size = new System.Drawing.Size(64, 64);
            this.pnlBrandIcon.TabIndex = 0;

            this.lblBrandIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrandIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblBrandIcon.ForeColor = System.Drawing.Color.White;
            this.lblBrandIcon.Location = new System.Drawing.Point(0, 0);
            this.lblBrandIcon.Name = "lblBrandIcon";
            this.lblBrandIcon.Size = new System.Drawing.Size(64, 64);
            this.lblBrandIcon.TabIndex = 0;
            this.lblBrandIcon.Text = "Rx";
            this.lblBrandIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.AcceptButton = this.btnLogin;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed \u2014 Clinical Login";
            this.panelMain.ResumeLayout(false);
            this.panelLoginCard.ResumeLayout(false);
            this.panelLoginCard.PerformLayout();
            this.pnlPasswordField.ResumeLayout(false);
            this.pnlPasswordField.PerformLayout();
            this.pnlUsernameField.ResumeLayout(false);
            this.pnlUsernameField.PerformLayout();
            this.pnlBrandIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelLoginCard;
        private System.Windows.Forms.Panel pnlBrandIcon;
        private System.Windows.Forms.Label lblBrandIcon;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblAuthTitle;
        private System.Windows.Forms.Label lblAuthSubtitle;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.RadioButton rdoCustomer;
        private System.Windows.Forms.RadioButton rdoAdmin;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlUsernameField;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPasswordField;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTogglePassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel lnkForgot;
    }
}
