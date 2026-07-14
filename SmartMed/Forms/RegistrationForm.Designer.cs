namespace SmartMed.UI
{
    partial class RegistrationForm
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
            this.panelRegisterCard = new System.Windows.Forms.Panel();
            this.panelSuccess = new System.Windows.Forms.Panel();
            this.btnReturnLogin = new System.Windows.Forms.Button();
            this.lblSuccessMessage = new System.Windows.Forms.Label();
            this.lblSuccessTitle = new System.Windows.Forms.Label();
            this.lblSuccessIcon = new System.Windows.Forms.Label();
            this.panelBrandFooter = new System.Windows.Forms.Panel();
            this.lblBrandSubtitle = new System.Windows.Forms.Label();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.pnlBrandIcon = new System.Windows.Forms.Panel();
            this.lblBrandIcon = new System.Windows.Forms.Label();
            this.panelBody = new System.Windows.Forms.Panel();
            this.lnkBackLogin = new System.Windows.Forms.LinkLabel();
            this.btnRegister = new System.Windows.Forms.Button();
            this.chkTerms = new System.Windows.Forms.CheckBox();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.pnlFullNameField = new System.Windows.Forms.Panel();
            this.pnlEmailField = new System.Windows.Forms.Panel();
            this.pnlPhoneField = new System.Windows.Forms.Panel();
            this.pnlAddressField = new System.Windows.Forms.Panel();
            this.pnlPasswordField = new System.Windows.Forms.Panel();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.pnlConfirmField = new System.Windows.Forms.Panel();
            this.btnToggleConfirm = new System.Windows.Forms.Button();
            this.panelMain.SuspendLayout();
            this.panelRegisterCard.SuspendLayout();
            this.panelSuccess.SuspendLayout();
            this.panelBrandFooter.SuspendLayout();
            this.pnlBrandIcon.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.pnlFullNameField.SuspendLayout();
            this.pnlEmailField.SuspendLayout();
            this.pnlPhoneField.SuspendLayout();
            this.pnlAddressField.SuspendLayout();
            this.pnlPasswordField.SuspendLayout();
            this.pnlConfirmField.SuspendLayout();
            this.SuspendLayout();

            this.panelMain.Controls.Add(this.panelRegisterCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(720, 748);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelMain_Paint);
            this.panelMain.Resize += new System.EventHandler(this.PanelMain_Resize);

            this.panelRegisterCard.BackColor = System.Drawing.Color.White;
            this.panelRegisterCard.Controls.Add(this.panelBody);
            this.panelRegisterCard.Controls.Add(this.panelSuccess);
            this.panelRegisterCard.Controls.Add(this.panelBrandFooter);
            this.panelRegisterCard.Location = new System.Drawing.Point(100, 24);
            this.panelRegisterCard.Name = "panelRegisterCard";
            this.panelRegisterCard.Size = new System.Drawing.Size(520, 660);
            this.panelRegisterCard.TabIndex = 0;

            this.panelSuccess.BackColor = System.Drawing.Color.White;
            this.panelSuccess.Controls.Add(this.btnReturnLogin);
            this.panelSuccess.Controls.Add(this.lblSuccessMessage);
            this.panelSuccess.Controls.Add(this.lblSuccessTitle);
            this.panelSuccess.Controls.Add(this.lblSuccessIcon);
            this.panelSuccess.Location = new System.Drawing.Point(0, 0);
            this.panelSuccess.Name = "panelSuccess";
            this.panelSuccess.Size = new System.Drawing.Size(520, 564);
            this.panelSuccess.TabIndex = 3;
            this.panelSuccess.Visible = false;

            this.btnReturnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnReturnLogin.FlatAppearance.BorderSize = 0;
            this.btnReturnLogin.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnReturnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnLogin.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnReturnLogin.ForeColor = System.Drawing.Color.White;
            this.btnReturnLogin.Location = new System.Drawing.Point(32, 268);
            this.btnReturnLogin.Name = "btnReturnLogin";
            this.btnReturnLogin.Size = new System.Drawing.Size(456, 44);
            this.btnReturnLogin.TabIndex = 3;
            this.btnReturnLogin.Text = "Return to Login";
            this.btnReturnLogin.UseVisualStyleBackColor = false;
            this.btnReturnLogin.Click += new System.EventHandler(this.BtnReturnLogin_Click);

            this.lblSuccessMessage.BackColor = System.Drawing.Color.White;
            this.lblSuccessMessage.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSuccessMessage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblSuccessMessage.Location = new System.Drawing.Point(32, 176);
            this.lblSuccessMessage.Name = "lblSuccessMessage";
            this.lblSuccessMessage.Size = new System.Drawing.Size(456, 72);
            this.lblSuccessMessage.TabIndex = 2;
            this.lblSuccessMessage.Text = "You can now login to your SmartMed Pharmacy portal to manage prescriptions and orders.";
            this.lblSuccessMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            this.lblSuccessTitle.BackColor = System.Drawing.Color.White;
            this.lblSuccessTitle.Font = new System.Drawing.Font("Hanken Grotesk", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSuccessTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblSuccessTitle.Location = new System.Drawing.Point(32, 140);
            this.lblSuccessTitle.Name = "lblSuccessTitle";
            this.lblSuccessTitle.Size = new System.Drawing.Size(456, 28);
            this.lblSuccessTitle.TabIndex = 1;
            this.lblSuccessTitle.Text = "Registration successful";
            this.lblSuccessTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.lblSuccessIcon.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblSuccessIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.lblSuccessIcon.Location = new System.Drawing.Point(32, 80);
            this.lblSuccessIcon.Name = "lblSuccessIcon";
            this.lblSuccessIcon.Size = new System.Drawing.Size(456, 48);
            this.lblSuccessIcon.TabIndex = 0;
            this.lblSuccessIcon.Text = "\u2713";
            this.lblSuccessIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelBrandFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            this.panelBrandFooter.Controls.Add(this.lblBrandSubtitle);
            this.panelBrandFooter.Controls.Add(this.lblBrandTitle);
            this.panelBrandFooter.Controls.Add(this.pnlBrandIcon);
            this.panelBrandFooter.Location = new System.Drawing.Point(0, 564);
            this.panelBrandFooter.Name = "panelBrandFooter";
            this.panelBrandFooter.Size = new System.Drawing.Size(520, 96);
            this.panelBrandFooter.TabIndex = 2;

            this.lblBrandSubtitle.AutoSize = true;
            this.lblBrandSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            this.lblBrandSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBrandSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(98)))), ((int)(((byte)(96)))));
            this.lblBrandSubtitle.Location = new System.Drawing.Point(224, 50);
            this.lblBrandSubtitle.Name = "lblBrandSubtitle";
            this.lblBrandSubtitle.Size = new System.Drawing.Size(168, 15);
            this.lblBrandSubtitle.TabIndex = 2;
            this.lblBrandSubtitle.Text = "PHARMACY && CLINIC SUITE";

            this.lblBrandTitle.AutoSize = true;
            this.lblBrandTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(239)))), ((int)(((byte)(238)))));
            this.lblBrandTitle.Font = new System.Drawing.Font("Hanken Grotesk", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBrandTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblBrandTitle.Location = new System.Drawing.Point(224, 26);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(68, 15);
            this.lblBrandTitle.TabIndex = 1;
            this.lblBrandTitle.Text = "SmartMed";

            this.pnlBrandIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.pnlBrandIcon.Controls.Add(this.lblBrandIcon);
            this.pnlBrandIcon.Location = new System.Drawing.Point(176, 28);
            this.pnlBrandIcon.Name = "pnlBrandIcon";
            this.pnlBrandIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlBrandIcon.TabIndex = 0;

            this.lblBrandIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrandIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.lblBrandIcon.Font = new System.Drawing.Font("Hanken Grotesk", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBrandIcon.ForeColor = System.Drawing.Color.White;
            this.lblBrandIcon.Location = new System.Drawing.Point(0, 0);
            this.lblBrandIcon.Name = "lblBrandIcon";
            this.lblBrandIcon.Size = new System.Drawing.Size(40, 40);
            this.lblBrandIcon.TabIndex = 0;
            this.lblBrandIcon.Text = "+";
            this.lblBrandIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Controls.Add(this.lnkBackLogin);
            this.panelBody.Controls.Add(this.btnRegister);
            this.panelBody.Controls.Add(this.chkTerms);
            this.panelBody.Controls.Add(this.pnlConfirmField);
            this.panelBody.Controls.Add(this.lblConfirm);
            this.panelBody.Controls.Add(this.pnlPasswordField);
            this.panelBody.Controls.Add(this.lblPassword);
            this.panelBody.Controls.Add(this.pnlAddressField);
            this.panelBody.Controls.Add(this.lblAddress);
            this.panelBody.Controls.Add(this.pnlPhoneField);
            this.panelBody.Controls.Add(this.lblPhone);
            this.panelBody.Controls.Add(this.pnlEmailField);
            this.panelBody.Controls.Add(this.lblEmail);
            this.panelBody.Controls.Add(this.pnlFullNameField);
            this.panelBody.Controls.Add(this.lblFullName);
            this.panelBody.Controls.Add(this.lblPageSubtitle);
            this.panelBody.Controls.Add(this.lblPageTitle);
            this.panelBody.Location = new System.Drawing.Point(0, 0);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(520, 564);
            this.panelBody.TabIndex = 1;

            this.lnkBackLogin.ActiveLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.lnkBackLogin.AutoSize = true;
            this.lnkBackLogin.BackColor = System.Drawing.Color.White;
            this.lnkBackLogin.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lnkBackLogin.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lnkBackLogin.Location = new System.Drawing.Point(150, 528);
            this.lnkBackLogin.Name = "lnkBackLogin";
            this.lnkBackLogin.Size = new System.Drawing.Size(220, 15);
            this.lnkBackLogin.TabIndex = 16;
            this.lnkBackLogin.TabStop = true;
            this.lnkBackLogin.Text = "Already have an account? Back to Login";
            this.lnkBackLogin.VisitedLinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.lnkBackLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkBackLogin_LinkClicked);

            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnRegister.ForeColor = System.Drawing.Color.White;
            this.btnRegister.Location = new System.Drawing.Point(32, 468);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(456, 44);
            this.btnRegister.TabIndex = 15;
            this.btnRegister.Text = "Register Professional Account";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);

            this.chkTerms.BackColor = System.Drawing.Color.White;
            this.chkTerms.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chkTerms.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.chkTerms.Location = new System.Drawing.Point(32, 420);
            this.chkTerms.Name = "chkTerms";
            this.chkTerms.Size = new System.Drawing.Size(456, 36);
            this.chkTerms.TabIndex = 14;
            this.chkTerms.Text = "I agree to the Clinical Terms of Service and acknowledge the HIPAA compliance guidelines.";
            this.chkTerms.UseVisualStyleBackColor = true;


            this.pnlConfirmField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlConfirmField.Controls.Add(this.txtConfirm);
            this.pnlConfirmField.Controls.Add(this.btnToggleConfirm);
            this.pnlConfirmField.Location = new System.Drawing.Point(272, 368);
            this.pnlConfirmField.Name = "pnlConfirmField";
            this.pnlConfirmField.Size = new System.Drawing.Size(216, 40);
            this.pnlConfirmField.TabIndex = 13;

            this.txtConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtConfirm.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtConfirm.Location = new System.Drawing.Point(10, 10);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(136, 17);
            this.txtConfirm.TabIndex = 0;

            this.btnToggleConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnToggleConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleConfirm.FlatAppearance.BorderSize = 0;
            this.btnToggleConfirm.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnToggleConfirm.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnToggleConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleConfirm.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnToggleConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnToggleConfirm.Location = new System.Drawing.Point(146, 3);
            this.btnToggleConfirm.Name = "btnToggleConfirm";
            this.btnToggleConfirm.Size = new System.Drawing.Size(66, 34);
            this.btnToggleConfirm.TabIndex = 1;
            this.btnToggleConfirm.TabStop = false;
            this.btnToggleConfirm.Text = "Show";
            this.btnToggleConfirm.UseVisualStyleBackColor = false;

            this.lblConfirm.AutoSize = true;
            this.lblConfirm.BackColor = System.Drawing.Color.White;
            this.lblConfirm.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblConfirm.Location = new System.Drawing.Point(272, 348);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(54, 15);
            this.lblConfirm.TabIndex = 12;
            this.lblConfirm.Text = "CONFIRM";

            this.pnlPasswordField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlPasswordField.Controls.Add(this.txtPassword);
            this.pnlPasswordField.Controls.Add(this.btnTogglePassword);
            this.pnlPasswordField.Location = new System.Drawing.Point(32, 368);
            this.pnlPasswordField.Name = "pnlPasswordField";
            this.pnlPasswordField.Size = new System.Drawing.Size(216, 40);
            this.pnlPasswordField.TabIndex = 11;

            this.txtPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPassword.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtPassword.Location = new System.Drawing.Point(10, 10);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(136, 17);
            this.txtPassword.TabIndex = 0;

            this.btnTogglePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnTogglePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnTogglePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnTogglePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePassword.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnTogglePassword.Location = new System.Drawing.Point(146, 3);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(66, 34);
            this.btnTogglePassword.TabIndex = 1;
            this.btnTogglePassword.TabStop = false;
            this.btnTogglePassword.Text = "Show";
            this.btnTogglePassword.UseVisualStyleBackColor = false;

            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.White;
            this.lblPassword.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblPassword.Location = new System.Drawing.Point(32, 348);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 15);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "PASSWORD *";

            this.pnlAddressField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlAddressField.Controls.Add(this.txtAddress);
            this.pnlAddressField.Location = new System.Drawing.Point(32, 292);
            this.pnlAddressField.Name = "pnlAddressField";
            this.pnlAddressField.Size = new System.Drawing.Size(456, 40);
            this.pnlAddressField.TabIndex = 9;

            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtAddress.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtAddress.Location = new System.Drawing.Point(10, 10);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(436, 17);
            this.txtAddress.Text = "Enter your home address";
            this.txtAddress.TabIndex = 0;

            this.lblAddress.AutoSize = true;
            this.lblAddress.BackColor = System.Drawing.Color.White;
            this.lblAddress.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblAddress.Location = new System.Drawing.Point(32, 272);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(98, 15);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "HOME ADDRESS *";

            this.pnlPhoneField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlPhoneField.Controls.Add(this.txtPhone);
            this.pnlPhoneField.Location = new System.Drawing.Point(272, 216);
            this.pnlPhoneField.Name = "pnlPhoneField";
            this.pnlPhoneField.Size = new System.Drawing.Size(216, 40);
            this.pnlPhoneField.TabIndex = 7;

            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPhone.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtPhone.Location = new System.Drawing.Point(10, 10);
            this.txtPhone.MaxLength = 18;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(196, 17);
            this.txtPhone.Text = "0771234567 or +94771234567";
            this.txtPhone.TabIndex = 0;

            this.lblPhone.AutoSize = true;
            this.lblPhone.BackColor = System.Drawing.Color.White;
            this.lblPhone.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblPhone.Location = new System.Drawing.Point(272, 196);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(99, 15);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "PHONE NUMBER *";

            this.pnlEmailField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlEmailField.Controls.Add(this.txtEmail);
            this.pnlEmailField.Location = new System.Drawing.Point(32, 216);
            this.pnlEmailField.Name = "pnlEmailField";
            this.pnlEmailField.Size = new System.Drawing.Size(216, 40);
            this.pnlEmailField.TabIndex = 5;

            this.txtEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEmail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtEmail.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtEmail.Location = new System.Drawing.Point(10, 10);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(196, 17);
            this.txtEmail.Text = "jane@hospital.com";
            this.txtEmail.TabIndex = 0;

            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.White;
            this.lblEmail.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblEmail.Location = new System.Drawing.Point(32, 196);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(93, 15);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "CLINICAL EMAIL *";

            this.pnlFullNameField.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.pnlFullNameField.Controls.Add(this.txtFullName);
            this.pnlFullNameField.Location = new System.Drawing.Point(32, 140);
            this.pnlFullNameField.Name = "pnlFullNameField";
            this.pnlFullNameField.Size = new System.Drawing.Size(456, 40);
            this.pnlFullNameField.TabIndex = 3;

            this.txtFullName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFullName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtFullName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFullName.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(138)))), ((int)(((byte)(136)))));
            this.txtFullName.Location = new System.Drawing.Point(10, 10);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(436, 17);
            this.txtFullName.Text = "Dr. Jane Smith";
            this.txtFullName.TabIndex = 0;

            this.lblFullName.AutoSize = true;
            this.lblFullName.BackColor = System.Drawing.Color.White;
            this.lblFullName.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblFullName.Location = new System.Drawing.Point(32, 120);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(68, 15);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "FULL NAME *";


            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.White;
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(32, 60);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(330, 15);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Register to access SmartMed\u2019s secure clinical ecosystem.";

            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.White;
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(32, 32);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(220, 25);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Create Professional Account";

            this.AcceptButton = this.btnRegister;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(720, 748);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 720);
            this.Name = "RegistrationForm";
            this.ShowInTaskbar = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed \u2014 Clinical Registration";
            this.panelMain.ResumeLayout(false);
            this.panelRegisterCard.ResumeLayout(false);
            this.panelSuccess.ResumeLayout(false);
            this.panelBrandFooter.ResumeLayout(false);
            this.panelBrandFooter.PerformLayout();
            this.pnlBrandIcon.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.pnlFullNameField.ResumeLayout(false);
            this.pnlFullNameField.PerformLayout();
            this.pnlEmailField.ResumeLayout(false);
            this.pnlEmailField.PerformLayout();
            this.pnlPhoneField.ResumeLayout(false);
            this.pnlPhoneField.PerformLayout();
            this.pnlAddressField.ResumeLayout(false);
            this.pnlAddressField.PerformLayout();
            this.pnlPasswordField.ResumeLayout(false);
            this.pnlPasswordField.PerformLayout();
            this.pnlConfirmField.ResumeLayout(false);
            this.pnlConfirmField.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelRegisterCard;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.Panel pnlFullNameField;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Panel pnlEmailField;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Panel pnlPhoneField;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.Panel pnlAddressField;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Panel pnlPasswordField;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTogglePassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.Panel pnlConfirmField;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Button btnToggleConfirm;
        private System.Windows.Forms.CheckBox chkTerms;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.LinkLabel lnkBackLogin;
        private System.Windows.Forms.Panel panelBrandFooter;
        private System.Windows.Forms.Panel pnlBrandIcon;
        private System.Windows.Forms.Label lblBrandIcon;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSubtitle;
        private System.Windows.Forms.Panel panelSuccess;
        private System.Windows.Forms.Label lblSuccessIcon;
        private System.Windows.Forms.Label lblSuccessTitle;
        private System.Windows.Forms.Label lblSuccessMessage;
        private System.Windows.Forms.Button btnReturnLogin;
    }
}
