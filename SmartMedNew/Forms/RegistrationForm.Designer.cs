namespace SmartMedNew.UI
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
            this.panelCardHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.pnlHeaderIcon = new System.Windows.Forms.Panel();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.panelStatusBar = new System.Windows.Forms.Panel();
            this.lblStatusRight = new System.Windows.Forms.Label();
            this.lblStatusCenter = new System.Windows.Forms.Label();
            this.lblStatusLeft = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelRegisterCard.SuspendLayout();
            this.panelSuccess.SuspendLayout();
            this.panelBrandFooter.SuspendLayout();
            this.pnlBrandIcon.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelCardHeader.SuspendLayout();
            this.pnlHeaderIcon.SuspendLayout();
            this.panelStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.panelRegisterCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(720, 748);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelMain_Paint);
            this.panelMain.Resize += new System.EventHandler(this.PanelMain_Resize);
            // 
            // panelRegisterCard
            // 
            this.panelRegisterCard.BackColor = System.Drawing.Color.White;
            this.panelRegisterCard.Controls.Add(this.panelBody);
            this.panelRegisterCard.Controls.Add(this.panelSuccess);
            this.panelRegisterCard.Controls.Add(this.panelBrandFooter);
            this.panelRegisterCard.Controls.Add(this.panelCardHeader);
            this.panelRegisterCard.Location = new System.Drawing.Point(100, 24);
            this.panelRegisterCard.Name = "panelRegisterCard";
            this.panelRegisterCard.Size = new System.Drawing.Size(520, 700);
            this.panelRegisterCard.TabIndex = 0;
            // 
            // panelSuccess
            // 
            this.panelSuccess.Controls.Add(this.btnReturnLogin);
            this.panelSuccess.Controls.Add(this.lblSuccessMessage);
            this.panelSuccess.Controls.Add(this.lblSuccessTitle);
            this.panelSuccess.Controls.Add(this.lblSuccessIcon);
            this.panelSuccess.Location = new System.Drawing.Point(0, 40);
            this.panelSuccess.Name = "panelSuccess";
            this.panelSuccess.Size = new System.Drawing.Size(520, 564);
            this.panelSuccess.TabIndex = 3;
            this.panelSuccess.Visible = false;
            // 
            // btnReturnLogin
            // 
            this.btnReturnLogin.Location = new System.Drawing.Point(32, 268);
            this.btnReturnLogin.Name = "btnReturnLogin";
            this.btnReturnLogin.Size = new System.Drawing.Size(456, 44);
            this.btnReturnLogin.TabIndex = 3;
            this.btnReturnLogin.Text = "Return to Login";
            this.btnReturnLogin.UseVisualStyleBackColor = false;
            this.btnReturnLogin.Click += new System.EventHandler(this.BtnReturnLogin_Click);
            // 
            // lblSuccessMessage
            // 
            this.lblSuccessMessage.Location = new System.Drawing.Point(32, 176);
            this.lblSuccessMessage.Name = "lblSuccessMessage";
            this.lblSuccessMessage.Size = new System.Drawing.Size(456, 72);
            this.lblSuccessMessage.TabIndex = 2;
            this.lblSuccessMessage.Text = "You can now login to your SmartMed Pharmacy portal to manage prescriptions and orders.";
            this.lblSuccessMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblSuccessTitle
            // 
            this.lblSuccessTitle.Font = new System.Drawing.Font("Hanken Grotesk", 12F, System.Drawing.FontStyle.Bold);
            this.lblSuccessTitle.Location = new System.Drawing.Point(32, 140);
            this.lblSuccessTitle.Name = "lblSuccessTitle";
            this.lblSuccessTitle.Size = new System.Drawing.Size(456, 28);
            this.lblSuccessTitle.TabIndex = 1;
            this.lblSuccessTitle.Text = "Registration successful";
            this.lblSuccessTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSuccessIcon
            // 
            this.lblSuccessIcon.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblSuccessIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.lblSuccessIcon.Location = new System.Drawing.Point(32, 80);
            this.lblSuccessIcon.Name = "lblSuccessIcon";
            this.lblSuccessIcon.Size = new System.Drawing.Size(456, 48);
            this.lblSuccessIcon.TabIndex = 0;
            this.lblSuccessIcon.Text = "\u2713";
            this.lblSuccessIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBrandFooter
            // 
            this.panelBrandFooter.Controls.Add(this.lblBrandSubtitle);
            this.panelBrandFooter.Controls.Add(this.lblBrandTitle);
            this.panelBrandFooter.Controls.Add(this.pnlBrandIcon);
            this.panelBrandFooter.Location = new System.Drawing.Point(0, 604);
            this.panelBrandFooter.Name = "panelBrandFooter";
            this.panelBrandFooter.Size = new System.Drawing.Size(520, 96);
            this.panelBrandFooter.TabIndex = 2;
            // 
            // lblBrandSubtitle
            // 
            this.lblBrandSubtitle.AutoSize = true;
            this.lblBrandSubtitle.Location = new System.Drawing.Point(224, 50);
            this.lblBrandSubtitle.Name = "lblBrandSubtitle";
            this.lblBrandSubtitle.Size = new System.Drawing.Size(168, 15);
            this.lblBrandSubtitle.TabIndex = 2;
            this.lblBrandSubtitle.Text = "PHARMACY && CLINIC SUITE";
            // 
            // lblBrandTitle
            // 
            this.lblBrandTitle.AutoSize = true;
            this.lblBrandTitle.Location = new System.Drawing.Point(224, 26);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(68, 15);
            this.lblBrandTitle.TabIndex = 1;
            this.lblBrandTitle.Text = "SmartMed";
            // 
            // pnlBrandIcon
            // 
            this.pnlBrandIcon.Controls.Add(this.lblBrandIcon);
            this.pnlBrandIcon.Location = new System.Drawing.Point(176, 28);
            this.pnlBrandIcon.Name = "pnlBrandIcon";
            this.pnlBrandIcon.Size = new System.Drawing.Size(40, 40);
            this.pnlBrandIcon.TabIndex = 0;
            // 
            // lblBrandIcon
            // 
            this.lblBrandIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblBrandIcon.ForeColor = System.Drawing.Color.White;
            this.lblBrandIcon.Location = new System.Drawing.Point(0, 0);
            this.lblBrandIcon.Name = "lblBrandIcon";
            this.lblBrandIcon.Size = new System.Drawing.Size(40, 40);
            this.lblBrandIcon.TabIndex = 0;
            this.lblBrandIcon.Text = "+";
            this.lblBrandIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.lnkBackLogin);
            this.panelBody.Controls.Add(this.btnRegister);
            this.panelBody.Controls.Add(this.chkTerms);
            this.panelBody.Controls.Add(this.txtConfirm);
            this.panelBody.Controls.Add(this.lblConfirm);
            this.panelBody.Controls.Add(this.txtPassword);
            this.panelBody.Controls.Add(this.lblPassword);
            this.panelBody.Controls.Add(this.txtAddress);
            this.panelBody.Controls.Add(this.lblAddress);
            this.panelBody.Controls.Add(this.txtPhone);
            this.panelBody.Controls.Add(this.lblPhone);
            this.panelBody.Controls.Add(this.txtEmail);
            this.panelBody.Controls.Add(this.lblEmail);
            this.panelBody.Controls.Add(this.txtFullName);
            this.panelBody.Controls.Add(this.lblFullName);
            this.panelBody.Controls.Add(this.lblPageSubtitle);
            this.panelBody.Controls.Add(this.lblPageTitle);
            this.panelBody.Location = new System.Drawing.Point(0, 40);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(520, 564);
            this.panelBody.TabIndex = 1;
            // 
            // lnkBackLogin
            // 
            this.lnkBackLogin.AutoSize = true;
            this.lnkBackLogin.Location = new System.Drawing.Point(150, 528);
            this.lnkBackLogin.Name = "lnkBackLogin";
            this.lnkBackLogin.Size = new System.Drawing.Size(220, 15);
            this.lnkBackLogin.TabIndex = 16;
            this.lnkBackLogin.TabStop = true;
            this.lnkBackLogin.Text = "Already have an account? Back to Login";
            this.lnkBackLogin.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkBackLogin_LinkClicked);
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(32, 468);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(456, 44);
            this.btnRegister.TabIndex = 15;
            this.btnRegister.Text = "Register Professional Account";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // chkTerms
            // 
            this.chkTerms.Location = new System.Drawing.Point(32, 420);
            this.chkTerms.Name = "chkTerms";
            this.chkTerms.Size = new System.Drawing.Size(456, 36);
            this.chkTerms.TabIndex = 14;
            this.chkTerms.Text = "I agree to the Clinical Terms of Service and acknowledge the HIPAA compliance guidelines.";
            this.chkTerms.UseVisualStyleBackColor = true;
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(272, 368);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(216, 23);
            this.txtConfirm.TabIndex = 13;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Location = new System.Drawing.Point(272, 348);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(54, 15);
            this.lblConfirm.TabIndex = 12;
            this.lblConfirm.Text = "CONFIRM";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(32, 368);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(216, 23);
            this.txtPassword.TabIndex = 11;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(32, 348);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(67, 15);
            this.lblPassword.TabIndex = 10;
            this.lblPassword.Text = "PASSWORD";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(32, 292);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(456, 23);
            this.txtAddress.TabIndex = 9;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(32, 272);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(98, 15);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "HOME ADDRESS";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(272, 216);
            this.txtPhone.MaxLength = 14;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(216, 23);
            this.txtPhone.TabIndex = 7;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(272, 196);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(99, 15);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "PHONE NUMBER";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(32, 216);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(216, 23);
            this.txtEmail.TabIndex = 5;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(32, 196);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(93, 15);
            this.lblEmail.TabIndex = 4;
            this.lblEmail.Text = "CLINICAL EMAIL";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(32, 140);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(456, 23);
            this.txtFullName.TabIndex = 3;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(32, 120);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(68, 15);
            this.lblFullName.TabIndex = 2;
            this.lblFullName.Text = "FULL NAME";
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.Location = new System.Drawing.Point(32, 60);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(330, 15);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Register to access SmartMed\u2019s secure clinical ecosystem.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 14F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.Location = new System.Drawing.Point(32, 32);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(220, 25);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Create Professional Account";
            // 
            // panelCardHeader
            // 
            this.panelCardHeader.Controls.Add(this.btnClose);
            this.panelCardHeader.Controls.Add(this.lblCardTitle);
            this.panelCardHeader.Controls.Add(this.pnlHeaderIcon);
            this.panelCardHeader.Location = new System.Drawing.Point(0, 0);
            this.panelCardHeader.Name = "panelCardHeader";
            this.panelCardHeader.Size = new System.Drawing.Size(520, 40);
            this.panelCardHeader.TabIndex = 0;
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(476, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "\u00d7";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // lblCardTitle
            // 
            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.Location = new System.Drawing.Point(36, 12);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(240, 15);
            this.lblCardTitle.TabIndex = 1;
            this.lblCardTitle.Text = "SmartMed \u2014 Clinical Registration (v4.2.0)";
            // 
            // pnlHeaderIcon
            // 
            this.pnlHeaderIcon.Controls.Add(this.lblHeaderIcon);
            this.pnlHeaderIcon.Location = new System.Drawing.Point(12, 10);
            this.pnlHeaderIcon.Name = "pnlHeaderIcon";
            this.pnlHeaderIcon.Size = new System.Drawing.Size(20, 20);
            this.pnlHeaderIcon.TabIndex = 0;
            // 
            // lblHeaderIcon
            // 
            this.lblHeaderIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.White;
            this.lblHeaderIcon.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(20, 20);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "Rx";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelStatusBar
            // 
            this.panelStatusBar.Controls.Add(this.lblStatusRight);
            this.panelStatusBar.Controls.Add(this.lblStatusCenter);
            this.panelStatusBar.Controls.Add(this.lblStatusLeft);
            this.panelStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelStatusBar.Location = new System.Drawing.Point(0, 748);
            this.panelStatusBar.Name = "panelStatusBar";
            this.panelStatusBar.Padding = new System.Windows.Forms.Padding(16, 0, 16, 0);
            this.panelStatusBar.Size = new System.Drawing.Size(720, 32);
            this.panelStatusBar.TabIndex = 1;
            // 
            // lblStatusRight
            // 
            this.lblStatusRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatusRight.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F);
            this.lblStatusRight.Location = new System.Drawing.Point(464, 0);
            this.lblStatusRight.Name = "lblStatusRight";
            this.lblStatusRight.Size = new System.Drawing.Size(240, 32);
            this.lblStatusRight.TabIndex = 2;
            this.lblStatusRight.Text = "STATION_ID: CLN-402";
            this.lblStatusRight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblStatusCenter
            // 
            this.lblStatusCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusCenter.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F);
            this.lblStatusCenter.Location = new System.Drawing.Point(296, 0);
            this.lblStatusCenter.Name = "lblStatusCenter";
            this.lblStatusCenter.Size = new System.Drawing.Size(408, 32);
            this.lblStatusCenter.TabIndex = 1;
            this.lblStatusCenter.Text = "AES-256 ENCRYPTION ENABLED";
            this.lblStatusCenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStatusLeft
            // 
            this.lblStatusLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatusLeft.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F);
            this.lblStatusLeft.Location = new System.Drawing.Point(16, 0);
            this.lblStatusLeft.Name = "lblStatusLeft";
            this.lblStatusLeft.Size = new System.Drawing.Size(280, 32);
            this.lblStatusLeft.TabIndex = 0;
            this.lblStatusLeft.Text = "CLINICAL SECURITY STANDARD \u2022 SSL ACTIVE";
            this.lblStatusLeft.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RegistrationForm
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(720, 780);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelStatusBar);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 760);
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
            this.panelCardHeader.ResumeLayout(false);
            this.panelCardHeader.PerformLayout();
            this.pnlHeaderIcon.ResumeLayout(false);
            this.panelStatusBar.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelRegisterCard;
        private System.Windows.Forms.Panel panelCardHeader;
        private System.Windows.Forms.Panel pnlHeaderIcon;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
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
        private System.Windows.Forms.Panel panelStatusBar;
        private System.Windows.Forms.Label lblStatusLeft;
        private System.Windows.Forms.Label lblStatusCenter;
        private System.Windows.Forms.Label lblStatusRight;
    }
}
