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
            this.panelCard = new System.Windows.Forms.Panel();
            this.panelSuccess = new System.Windows.Forms.Panel();
            this.btnReturnLogin = new System.Windows.Forms.Button();
            this.lblSuccessMessage = new System.Windows.Forms.Label();
            this.lblSuccessTitle = new System.Windows.Forms.Label();
            this.lblSuccessIcon = new System.Windows.Forms.Label();
            this.panelFooter = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.Panel();
            this.btnToggleConfirm = new System.Windows.Forms.Button();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.btnTogglePassword = new System.Windows.Forms.Button();
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
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblMedicalIcon = new System.Windows.Forms.Label();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.lblLockIcon = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            this.panelSuccess.SuspendLayout();
            this.panelFooter.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCard
            // 
            this.panelCard.Controls.Add(this.panelHeader);
            this.panelCard.Controls.Add(this.panelFooter);
            this.panelCard.Controls.Add(this.panelBody);
            this.panelCard.Controls.Add(this.panelSuccess);
            this.panelCard.Location = new System.Drawing.Point(15, 15);
            this.panelCard.Name = "panelCard";
            this.panelCard.Size = new System.Drawing.Size(450, 620);
            this.panelCard.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.lblMedicalIcon);
            this.panelHeader.Controls.Add(this.lblHeaderTitle);
            this.panelHeader.Controls.Add(this.lblLockIcon);
            this.panelHeader.Controls.Add(this.btnClose);
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(450, 56);
            this.panelHeader.TabIndex = 0;
            // 
            // lblMedicalIcon
            // 
            this.lblMedicalIcon.AutoSize = true;
            this.lblMedicalIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11F);
            this.lblMedicalIcon.ForeColor = System.Drawing.Color.White;
            this.lblMedicalIcon.Location = new System.Drawing.Point(24, 18);
            this.lblMedicalIcon.Name = "lblMedicalIcon";
            this.lblMedicalIcon.Size = new System.Drawing.Size(15, 15);
            this.lblMedicalIcon.TabIndex = 0;
            this.lblMedicalIcon.Text = "\uE95E";
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(48, 16);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(250, 20);
            this.lblHeaderTitle.TabIndex = 1;
            this.lblHeaderTitle.Text = "SmartMed Pharmacy - Registration";
            // 
            // lblLockIcon
            // 
            this.lblLockIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLockIcon.AutoSize = true;
            this.lblLockIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11F);
            this.lblLockIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblLockIcon.Location = new System.Drawing.Point(362, 18);
            this.lblLockIcon.Name = "lblLockIcon";
            this.lblLockIcon.Size = new System.Drawing.Size(15, 15);
            this.lblLockIcon.TabIndex = 2;
            this.lblLockIcon.Text = "\uE72E";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe MDL2 Assets", 11F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClose.Location = new System.Drawing.Point(390, 12);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 32);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "\uE711";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);
            // 
            // panelBody
            // 
            this.panelBody.Controls.Add(this.btnToggleConfirm);
            this.panelBody.Controls.Add(this.txtConfirm);
            this.panelBody.Controls.Add(this.lblConfirm);
            this.panelBody.Controls.Add(this.btnTogglePassword);
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
            this.panelBody.Location = new System.Drawing.Point(0, 56);
            this.panelBody.Name = "panelBody";
            this.panelBody.Size = new System.Drawing.Size(450, 440);
            this.panelBody.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(24, 24);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(62, 15);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name";
            // 
            // txtFullName
            // 
            this.txtFullName.Location = new System.Drawing.Point(24, 44);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(402, 23);
            this.txtFullName.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new System.Drawing.Point(24, 80);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(36, 15);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(24, 100);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(193, 23);
            this.txtEmail.TabIndex = 3;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(233, 80);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(41, 15);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone";
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(233, 100);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(193, 23);
            this.txtPhone.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(24, 136);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(49, 15);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(24, 156);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(402, 48);
            this.txtAddress.TabIndex = 7;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(24, 216);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(57, 15);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(24, 236);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(402, 23);
            this.txtPassword.TabIndex = 9;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.BackColor = System.Drawing.Color.White;
            this.btnTogglePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePassword.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10F);
            this.btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.btnTogglePassword.Location = new System.Drawing.Point(390, 237);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(36, 22);
            this.btnTogglePassword.TabIndex = 10;
            this.btnTogglePassword.Text = "\uE890";
            this.btnTogglePassword.UseVisualStyleBackColor = false;
            this.btnTogglePassword.Click += new System.EventHandler(this.BtnTogglePassword_Click);
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Location = new System.Drawing.Point(24, 276);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(104, 15);
            this.lblConfirm.TabIndex = 11;
            this.lblConfirm.Text = "Confirm Password";
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(24, 296);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(402, 23);
            this.txtConfirm.TabIndex = 12;
            this.txtConfirm.UseSystemPasswordChar = true;
            // 
            // btnToggleConfirm
            // 
            this.btnToggleConfirm.BackColor = System.Drawing.Color.White;
            this.btnToggleConfirm.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnToggleConfirm.FlatAppearance.BorderSize = 0;
            this.btnToggleConfirm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnToggleConfirm.Font = new System.Drawing.Font("Segoe MDL2 Assets", 10F);
            this.btnToggleConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.btnToggleConfirm.Location = new System.Drawing.Point(390, 297);
            this.btnToggleConfirm.Name = "btnToggleConfirm";
            this.btnToggleConfirm.Size = new System.Drawing.Size(36, 22);
            this.btnToggleConfirm.TabIndex = 13;
            this.btnToggleConfirm.Text = "\uE890";
            this.btnToggleConfirm.UseVisualStyleBackColor = false;
            this.btnToggleConfirm.Click += new System.EventHandler(this.BtnToggleConfirm_Click);
            // 
            // panelFooter
            // 
            this.panelFooter.Controls.Add(this.btnCancel);
            this.panelFooter.Controls.Add(this.btnRegister);
            this.panelFooter.Location = new System.Drawing.Point(0, 496);
            this.panelFooter.Name = "panelFooter";
            this.panelFooter.Size = new System.Drawing.Size(450, 124);
            this.panelFooter.TabIndex = 2;
            // 
            // btnRegister
            // 
            this.btnRegister.Location = new System.Drawing.Point(24, 16);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(402, 40);
            this.btnRegister.TabIndex = 0;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(24, 64);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(402, 40);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // panelSuccess
            // 
            this.panelSuccess.Controls.Add(this.btnReturnLogin);
            this.panelSuccess.Controls.Add(this.lblSuccessMessage);
            this.panelSuccess.Controls.Add(this.lblSuccessTitle);
            this.panelSuccess.Controls.Add(this.lblSuccessIcon);
            this.panelSuccess.Location = new System.Drawing.Point(0, 56);
            this.panelSuccess.Name = "panelSuccess";
            this.panelSuccess.Size = new System.Drawing.Size(450, 440);
            this.panelSuccess.TabIndex = 3;
            this.panelSuccess.Visible = false;
            // 
            // lblSuccessIcon
            // 
            this.lblSuccessIcon.Font = new System.Drawing.Font("Segoe MDL2 Assets", 20F);
            this.lblSuccessIcon.Location = new System.Drawing.Point(24, 80);
            this.lblSuccessIcon.Name = "lblSuccessIcon";
            this.lblSuccessIcon.Size = new System.Drawing.Size(402, 48);
            this.lblSuccessIcon.TabIndex = 0;
            this.lblSuccessIcon.Text = "\uE73E";
            this.lblSuccessIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSuccessTitle
            // 
            this.lblSuccessTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSuccessTitle.Location = new System.Drawing.Point(24, 140);
            this.lblSuccessTitle.Name = "lblSuccessTitle";
            this.lblSuccessTitle.Size = new System.Drawing.Size(402, 28);
            this.lblSuccessTitle.TabIndex = 1;
            this.lblSuccessTitle.Text = "Registration successful";
            this.lblSuccessTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSuccessMessage
            // 
            this.lblSuccessMessage.Location = new System.Drawing.Point(24, 176);
            this.lblSuccessMessage.Name = "lblSuccessMessage";
            this.lblSuccessMessage.Size = new System.Drawing.Size(402, 72);
            this.lblSuccessMessage.TabIndex = 2;
            this.lblSuccessMessage.Text = "You can now login to your SmartMed Pharmacy portal to manage prescriptions and orders.";
            this.lblSuccessMessage.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnReturnLogin
            // 
            this.btnReturnLogin.Location = new System.Drawing.Point(24, 268);
            this.btnReturnLogin.Name = "btnReturnLogin";
            this.btnReturnLogin.Size = new System.Drawing.Size(402, 40);
            this.btnReturnLogin.TabIndex = 3;
            this.btnReturnLogin.Text = "Return to Login";
            this.btnReturnLogin.UseVisualStyleBackColor = false;
            this.btnReturnLogin.Click += new System.EventHandler(this.BtnReturnLogin_Click);
            // 
            // RegistrationForm
            // 
            this.AcceptButton = this.btnRegister;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(500, 660);
            this.Controls.Add(this.panelCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RegistrationForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SmartMed Pharmacy - Customer Registration";
            this.panelCard.ResumeLayout(false);
            this.panelSuccess.ResumeLayout(false);
            this.panelFooter.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblMedicalIcon;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblLockIcon;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBody;
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
        private System.Windows.Forms.Button btnTogglePassword;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Button btnToggleConfirm;
        private System.Windows.Forms.Panel panelFooter;
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel panelSuccess;
        private System.Windows.Forms.Label lblSuccessIcon;
        private System.Windows.Forms.Label lblSuccessTitle;
        private System.Windows.Forms.Label lblSuccessMessage;
        private System.Windows.Forms.Button btnReturnLogin;
    }
}
