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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.btnRegister = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.lblTitle);
            this.panelCard.Controls.Add(this.lblFullName);
            this.panelCard.Controls.Add(this.txtName);
            this.panelCard.Controls.Add(this.lblEmail);
            this.panelCard.Controls.Add(this.txtEmail);
            this.panelCard.Controls.Add(this.lblPhone);
            this.panelCard.Controls.Add(this.txtPhone);
            this.panelCard.Controls.Add(this.lblAddress);
            this.panelCard.Controls.Add(this.txtAddress);
            this.panelCard.Controls.Add(this.lblPassword);
            this.panelCard.Controls.Add(this.txtPassword);
            this.panelCard.Controls.Add(this.lblConfirm);
            this.panelCard.Controls.Add(this.txtConfirm);
            this.panelCard.Controls.Add(this.btnRegister);
            this.panelCard.Controls.Add(this.btnCancel);
            this.panelCard.Location = new System.Drawing.Point(430, 140);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(24);
            this.panelCard.Size = new System.Drawing.Size(420, 440);
            this.panelCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(137, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Create Account";
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblFullName.Location = new System.Drawing.Point(24, 64);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(63, 15);
            this.lblFullName.TabIndex = 1;
            this.lblFullName.Text = "Full Name";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Location = new System.Drawing.Point(24, 84);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(372, 25);
            this.txtName.TabIndex = 2;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblEmail.Location = new System.Drawing.Point(24, 116);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(39, 15);
            this.lblEmail.TabIndex = 3;
            this.lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Location = new System.Drawing.Point(24, 136);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(372, 25);
            this.txtEmail.TabIndex = 4;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblPhone.Location = new System.Drawing.Point(24, 168);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(44, 15);
            this.lblPhone.TabIndex = 5;
            this.lblPhone.Text = "Phone";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Location = new System.Drawing.Point(24, 188);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(372, 25);
            this.txtPhone.TabIndex = 6;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblAddress.Location = new System.Drawing.Point(24, 220);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(52, 15);
            this.lblAddress.TabIndex = 7;
            this.lblAddress.Text = "Address";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Location = new System.Drawing.Point(24, 240);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(372, 25);
            this.txtAddress.TabIndex = 8;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblPassword.Location = new System.Drawing.Point(24, 272);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(64, 15);
            this.lblPassword.TabIndex = 9;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(24, 292);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(372, 25);
            this.txtPassword.TabIndex = 10;
            // 
            // lblConfirm
            // 
            this.lblConfirm.AutoSize = true;
            this.lblConfirm.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblConfirm.Location = new System.Drawing.Point(24, 324);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(107, 15);
            this.lblConfirm.TabIndex = 11;
            this.lblConfirm.Text = "Confirm Password";
            // 
            // txtConfirm
            // 
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirm.Location = new System.Drawing.Point(24, 344);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.PasswordChar = '*';
            this.txtConfirm.Size = new System.Drawing.Size(372, 25);
            this.txtConfirm.TabIndex = 12;
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Location = new System.Drawing.Point(24, 384);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(120, 36);
            this.btnRegister.TabIndex = 13;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(154, 384);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(120, 36);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // RegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "RegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Customer Registration";
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtName;
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
        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnCancel;
    }
}
