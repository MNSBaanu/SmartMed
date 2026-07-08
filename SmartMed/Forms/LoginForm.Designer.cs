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
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.lblTitleBarText = new System.Windows.Forms.Label();
            this.pnlTitleIcon = new System.Windows.Forms.Panel();
            this.lblTitleIcon = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblSecurityLine = new System.Windows.Forms.Label();
            this.panelLoginCard = new System.Windows.Forms.Panel();
            this.panelError = new System.Windows.Forms.Panel();
            this.lblError = new System.Windows.Forms.Label();
            this.lnkForgot = new System.Windows.Forms.LinkLabel();
            this.btnQuickCustomer = new System.Windows.Forms.Button();
            this.btnQuickAdmin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.pnlPasswordField = new System.Windows.Forms.Panel();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblAuthSubtitle = new System.Windows.Forms.Label();
            this.lblAuthTitle = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.lblBrand = new System.Windows.Forms.Label();
            this.pnlBrandIcon = new System.Windows.Forms.Panel();
            this.lblBrandIcon = new System.Windows.Forms.Label();
            this.panelTitleBar.SuspendLayout();
            this.pnlTitleIcon.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelLoginCard.SuspendLayout();
            this.panelError.SuspendLayout();
            this.pnlPasswordField.SuspendLayout();
            this.pnlBrandIcon.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.panelTitleBar.Controls.Add(this.lblTitleBarText);
            this.panelTitleBar.Controls.Add(this.pnlTitleIcon);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.panelTitleBar.Size = new System.Drawing.Size(720, 40);
            this.panelTitleBar.TabIndex = 0;
            // 
            // lblTitleBarText
            // 
            this.lblTitleBarText.AutoSize = true;
            this.lblTitleBarText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.lblTitleBarText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblTitleBarText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblTitleBarText.Location = new System.Drawing.Point(36, 12);
            this.lblTitleBarText.Name = "lblTitleBarText";
            this.lblTitleBarText.Size = new System.Drawing.Size(180, 18);
            this.lblTitleBarText.TabIndex = 1;
            this.lblTitleBarText.Text = "SmartMed ? Clinical Login";
            // 
            // pnlTitleIcon
            // 
            this.pnlTitleIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.pnlTitleIcon.Controls.Add(this.lblTitleIcon);
            this.pnlTitleIcon.Location = new System.Drawing.Point(16, 10);
            this.pnlTitleIcon.Name = "pnlTitleIcon";
            this.pnlTitleIcon.Size = new System.Drawing.Size(24, 20);
            this.pnlTitleIcon.TabIndex = 0;
            // 
            // lblTitleIcon
            // 
            this.lblTitleIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitleIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Bold);
            this.lblTitleIcon.ForeColor = System.Drawing.Color.White;
            this.lblTitleIcon.Location = new System.Drawing.Point(0, 0);
            this.lblTitleIcon.Name = "lblTitleIcon";
            this.lblTitleIcon.Size = new System.Drawing.Size(24, 20);
            this.lblTitleIcon.TabIndex = 0;
            this.lblTitleIcon.Text = "Rx";
            this.lblTitleIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.panelFooter);
            this.panelMain.Controls.Add(this.panelLoginCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 40);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(720, 660);
            this.panelMain.TabIndex = 1;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelMain_Paint);
            // 
            // panelFooter
            // 
            this.panelFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelFooter.Controls.Add(this.lblCopyright);
            this.panelFooter.Controls.Add(this.lblSecurityLine);
            this.panelFooter.Location = new System.Drawing.Point(140, 620);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(440, 44);
            this.panelFooter.TabIndex = 2;
            // 
            // lblCopyright
            // 
            this.lblCopyright.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblCopyright.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(96)))));
            this.lblCopyright.Location = new System.Drawing.Point(0, 24);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(440, 20);
            this.lblCopyright.TabIndex = 1;
            this.lblCopyright.Text = "? 2024 SmartMed Pharmacy ? Licensed to Clinical Precision Ltd.";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSecurityLine
            // 
            this.lblSecurityLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblSecurityLine.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSecurityLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblSecurityLine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(96)))));
            this.lblSecurityLine.Location = new System.Drawing.Point(0, 0);
            this.lblSecurityLine.Name = "lblSecurityLine";
            this.lblSecurityLine.Size = new System.Drawing.Size(440, 20);
            this.lblSecurityLine.TabIndex = 0;
            this.lblSecurityLine.Text = "CLINICAL SECURITY STANDARD ? SSL ACTIVE";
            this.lblSecurityLine.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelLoginCard
            // 
            this.panelLoginCard.BackColor = System.Drawing.Color.White;
            this.panelLoginCard.Controls.Add(this.panelError);
            this.panelLoginCard.Controls.Add(this.lnkForgot);
            this.panelLoginCard.Controls.Add(this.btnQuickCustomer);
            this.panelLoginCard.Controls.Add(this.btnQuickAdmin);
            this.panelLoginCard.Controls.Add(this.btnRegister);
            this.panelLoginCard.Controls.Add(this.btnLogin);
            this.panelLoginCard.Controls.Add(this.pnlPasswordField);
            this.panelLoginCard.Controls.Add(this.lblPassword);
            this.panelLoginCard.Controls.Add(this.txtUsername);
            this.panelLoginCard.Controls.Add(this.lblUsername);
            this.panelLoginCard.Controls.Add(this.lblAuthSubtitle);
            this.panelLoginCard.Controls.Add(this.lblAuthTitle);
            this.panelLoginCard.Controls.Add(this.lblVersion);
            this.panelLoginCard.Controls.Add(this.lblBrand);
            this.panelLoginCard.Controls.Add(this.pnlBrandIcon);
            this.panelLoginCard.Location = new System.Drawing.Point(140, 32);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Padding = new System.Windows.Forms.Padding(32);
            this.panelLoginCard.Size = new System.Drawing.Size(440, 568);
            this.panelLoginCard.TabIndex = 0;
            // 
            // panelError
            // 
            this.panelError.BackColor = System.Drawing.Color.White;
            this.panelError.Controls.Add(this.lblError);
            this.panelError.Location = new System.Drawing.Point(32, 500);
            this.panelError.Name = "panelError";
            this.panelError.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.panelError.Size = new System.Drawing.Size(0, 0);
            this.panelError.TabIndex = 15;
            this.panelError.Visible = false;
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.White;
            this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblError.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(12, 8);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 0);
            this.lblError.TabIndex = 0;
            // 
            // lnkForgot
            // 
            this.lnkForgot.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.lnkForgot.AutoSize = true;
            this.lnkForgot.BackColor = System.Drawing.Color.White;
            this.lnkForgot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lnkForgot.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lnkForgot.Location = new System.Drawing.Point(32, 516);
            this.lnkForgot.Name = "lnkForgot";
            this.lnkForgot.Size = new System.Drawing.Size(159, 20);
            this.lnkForgot.TabIndex = 14;
            this.lnkForgot.TabStop = true;
            this.lnkForgot.Text = "Forgot password?";
            this.lnkForgot.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lnkForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkForgot_LinkClicked);
            // 
            // btnQuickCustomer
            // 
            this.btnQuickCustomer.BackColor = System.Drawing.Color.White;
            this.btnQuickCustomer.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnQuickCustomer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnQuickCustomer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuickCustomer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnQuickCustomer.Location = new System.Drawing.Point(228, 468);
            this.btnQuickCustomer.Name = "btnQuickCustomer";
            this.btnQuickCustomer.Size = new System.Drawing.Size(180, 36);
            this.btnQuickCustomer.TabIndex = 13;
            this.btnQuickCustomer.Text = "Customer Login";
            this.btnQuickCustomer.UseVisualStyleBackColor = false;
            this.btnQuickCustomer.Click += new System.EventHandler(this.BtnQuickCustomer_Click);
            // 
            // btnQuickAdmin
            // 
            this.btnQuickAdmin.BackColor = System.Drawing.Color.White;
            this.btnQuickAdmin.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnQuickAdmin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnQuickAdmin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnQuickAdmin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnQuickAdmin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnQuickAdmin.Location = new System.Drawing.Point(32, 468);
            this.btnQuickAdmin.Name = "btnQuickAdmin";
            this.btnQuickAdmin.Size = new System.Drawing.Size(180, 36);
            this.btnQuickAdmin.TabIndex = 12;
            this.btnQuickAdmin.Text = "Admin Login";
            this.btnQuickAdmin.UseVisualStyleBackColor = false;
            this.btnQuickAdmin.Click += new System.EventHandler(this.BtnQuickAdmin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.White;
            this.btnRegister.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(250)))), ((int)(((byte)(249)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegister.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnRegister.Location = new System.Drawing.Point(228, 416);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(180, 40);
            this.btnRegister.TabIndex = 11;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.ForeColor = System.Drawing.Color.White;
            this.btnLogin.Location = new System.Drawing.Point(32, 416);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(180, 40);
            this.btnLogin.TabIndex = 10;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // pnlPasswordField
            // 
            this.pnlPasswordField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlPasswordField.Controls.Add(this.txtPassword);
            this.pnlPasswordField.Controls.Add(this.btnTogglePassword);
            this.pnlPasswordField.Location = new System.Drawing.Point(32, 326);
            this.pnlPasswordField.Name = "pnlPasswordField";
            this.pnlPasswordField.Size = new System.Drawing.Size(376, 40);
            this.pnlPasswordField.TabIndex = 8;
            // 
            // txtPassword
            // 
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
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtPassword_KeyDown);
            // 
            // btnTogglePassword
            // 
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
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.White;
            this.lblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblPassword.Location = new System.Drawing.Point(32, 306);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(75, 18);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Password";
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.txtUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtUsername.Location = new System.Drawing.Point(32, 258);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(376, 24);
            this.txtUsername.TabIndex = 6;
            this.txtUsername.Text = "Enter email or username";
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUsername_KeyDown);
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.White;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblUsername.Location = new System.Drawing.Point(32, 238);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(126, 18);
            this.lblUsername.TabIndex = 5;
            this.lblUsername.Text = "Email / Username";
            // 
            // lblAuthSubtitle
            // 
            this.lblAuthSubtitle.AutoSize = true;
            this.lblAuthSubtitle.BackColor = System.Drawing.Color.White;
            this.lblAuthSubtitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.lblAuthSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblAuthSubtitle.Location = new System.Drawing.Point(32, 206);
            this.lblAuthSubtitle.Name = "lblAuthSubtitle";
            this.lblAuthSubtitle.Size = new System.Drawing.Size(219, 18);
            this.lblAuthSubtitle.TabIndex = 4;
            this.lblAuthSubtitle.Text = "Pharmacy Management System";
            // 
            // lblAuthTitle
            // 
            this.lblAuthTitle.AutoSize = true;
            this.lblAuthTitle.BackColor = System.Drawing.Color.White;
            this.lblAuthTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblAuthTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblAuthTitle.Location = new System.Drawing.Point(32, 182);
            this.lblAuthTitle.Name = "lblAuthTitle";
            this.lblAuthTitle.Size = new System.Drawing.Size(269, 29);
            this.lblAuthTitle.TabIndex = 3;
            this.lblAuthTitle.Text = "System Authentication";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.BackColor = System.Drawing.Color.White;
            this.lblVersion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(96)))));
            this.lblVersion.Location = new System.Drawing.Point(32, 152);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(189, 17);
            this.lblVersion.TabIndex = 2;
            this.lblVersion.Text = "CLINICAL PRECISION V4.2.0";
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.BackColor = System.Drawing.Color.White;
            this.lblBrand.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold);
            this.lblBrand.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lblBrand.Location = new System.Drawing.Point(32, 124);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(250, 31);
            this.lblBrand.TabIndex = 1;
            this.lblBrand.Text = "SmartMed Clinical";
            // 
            // pnlBrandIcon
            // 
            this.pnlBrandIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.pnlBrandIcon.Controls.Add(this.lblBrandIcon);
            this.pnlBrandIcon.Location = new System.Drawing.Point(188, 32);
            this.pnlBrandIcon.Name = "pnlBrandIcon";
            this.pnlBrandIcon.Size = new System.Drawing.Size(64, 64);
            this.pnlBrandIcon.TabIndex = 0;
            // 
            // lblBrandIcon
            // 
            this.lblBrandIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrandIcon.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.lblBrandIcon.ForeColor = System.Drawing.Color.White;
            this.lblBrandIcon.Location = new System.Drawing.Point(0, 0);
            this.lblBrandIcon.Name = "lblBrandIcon";
            this.lblBrandIcon.Size = new System.Drawing.Size(64, 64);
            this.lblBrandIcon.TabIndex = 0;
            this.lblBrandIcon.Text = "Rx";
            this.lblBrandIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBrandIcon.Click += new System.EventHandler(this.lblBrandIcon_Click);
            // 
            // LoginForm
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1280, 800);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelTitleBar);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
            this.MinimumSize = new System.Drawing.Size(1024, 640);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed ? Clinical Login";
            this.Load += new System.EventHandler(this.LoginForm_Load);
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            this.pnlTitleIcon.ResumeLayout(false);
            this.panelMain.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelLoginCard.ResumeLayout(false);
            this.panelLoginCard.PerformLayout();
            this.panelError.ResumeLayout(false);
            this.pnlPasswordField.ResumeLayout(false);
            this.pnlPasswordField.PerformLayout();
            this.pnlBrandIcon.ResumeLayout(false);
            this.ResumeLayout(false);

        }



        #endregion



        private System.Windows.Forms.Panel panelTitleBar;

        private System.Windows.Forms.Label lblTitleBarText;

        private System.Windows.Forms.Panel pnlTitleIcon;

        private System.Windows.Forms.Label lblTitleIcon;

        private System.Windows.Forms.Panel panelMain;

        private System.Windows.Forms.Panel panelLoginCard;

        private System.Windows.Forms.Panel pnlBrandIcon;

        private System.Windows.Forms.Label lblBrandIcon;

        private System.Windows.Forms.Label lblBrand;

        private System.Windows.Forms.Label lblVersion;

        private System.Windows.Forms.Label lblAuthTitle;

        private System.Windows.Forms.Label lblAuthSubtitle;

        private System.Windows.Forms.Label lblUsername;

        private System.Windows.Forms.TextBox txtUsername;

        private System.Windows.Forms.Label lblPassword;

        private System.Windows.Forms.Panel pnlPasswordField;

        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnTogglePassword;

        private System.Windows.Forms.Button btnLogin;

        private System.Windows.Forms.Button btnRegister;

        private System.Windows.Forms.Button btnQuickAdmin;

        private System.Windows.Forms.Button btnQuickCustomer;

        private System.Windows.Forms.LinkLabel lnkForgot;

        private System.Windows.Forms.Panel panelError;

        private System.Windows.Forms.Label lblError;

        private System.Windows.Forms.Panel panelFooter;

        private System.Windows.Forms.Label lblSecurityLine;

        private System.Windows.Forms.Label lblCopyright;

    }

}


