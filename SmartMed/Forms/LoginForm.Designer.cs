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

            this.panelBody = new System.Windows.Forms.Panel();

            this.lblUsername = new System.Windows.Forms.Label();

            this.txtUsername = new System.Windows.Forms.TextBox();

            this.lblPassword = new System.Windows.Forms.Label();

            this.txtPassword = new System.Windows.Forms.TextBox();

            this.btnLogin = new System.Windows.Forms.Button();

            this.btnRegister = new System.Windows.Forms.Button();

            this.lnkForgot = new System.Windows.Forms.LinkLabel();

            this.panelBody.SuspendLayout();

            this.SuspendLayout();

            // 

            // panelBody

            // 

            this.panelBody.Controls.Add(this.lnkForgot);

            this.panelBody.Controls.Add(this.btnRegister);

            this.panelBody.Controls.Add(this.btnLogin);

            this.panelBody.Controls.Add(this.txtPassword);

            this.panelBody.Controls.Add(this.lblPassword);

            this.panelBody.Controls.Add(this.txtUsername);

            this.panelBody.Controls.Add(this.lblUsername);

            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;

            this.panelBody.Location = new System.Drawing.Point(0, 64);

            this.panelBody.Name = "panelBody";

            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);

            this.panelBody.Size = new System.Drawing.Size(368, 220);

            this.panelBody.TabIndex = 0;

            // 

            // lblUsername

            // 

            this.lblUsername.AutoSize = true;

            this.lblUsername.Location = new System.Drawing.Point(24, 16);

            this.lblUsername.Name = "lblUsername";

            this.lblUsername.Size = new System.Drawing.Size(96, 15);

            this.lblUsername.TabIndex = 0;

            this.lblUsername.Text = "Email / Username";

            // 

            // txtUsername

            // 

            this.txtUsername.Location = new System.Drawing.Point(24, 34);

            this.txtUsername.Name = "txtUsername";

            this.txtUsername.Size = new System.Drawing.Size(320, 23);

            this.txtUsername.TabIndex = 1;

            // 

            // lblPassword

            // 

            this.lblPassword.AutoSize = true;

            this.lblPassword.Location = new System.Drawing.Point(24, 72);

            this.lblPassword.Name = "lblPassword";

            this.lblPassword.Size = new System.Drawing.Size(57, 15);

            this.lblPassword.TabIndex = 2;

            this.lblPassword.Text = "Password";

            // 

            // txtPassword

            // 

            this.txtPassword.Location = new System.Drawing.Point(24, 90);

            this.txtPassword.Name = "txtPassword";

            this.txtPassword.Size = new System.Drawing.Size(320, 23);

            this.txtPassword.TabIndex = 3;

            this.txtPassword.UseSystemPasswordChar = true;

            // 

            // btnLogin

            // 

            this.btnLogin.Location = new System.Drawing.Point(24, 132);

            this.btnLogin.Name = "btnLogin";

            this.btnLogin.Size = new System.Drawing.Size(155, 32);

            this.btnLogin.TabIndex = 4;

            this.btnLogin.Text = "Login";

            this.btnLogin.UseVisualStyleBackColor = true;

            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);

            // 

            // btnRegister

            // 

            this.btnRegister.Location = new System.Drawing.Point(189, 132);

            this.btnRegister.Name = "btnRegister";

            this.btnRegister.Size = new System.Drawing.Size(155, 32);

            this.btnRegister.TabIndex = 5;

            this.btnRegister.Text = "Register";

            this.btnRegister.UseVisualStyleBackColor = true;

            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);

            // 

            // lnkForgot

            // 

            this.lnkForgot.AutoSize = true;

            this.lnkForgot.Location = new System.Drawing.Point(24, 176);

            this.lnkForgot.Name = "lnkForgot";

            this.lnkForgot.Size = new System.Drawing.Size(94, 15);

            this.lnkForgot.TabIndex = 6;

            this.lnkForgot.TabStop = true;

            this.lnkForgot.Text = "Forgot password?";

            this.lnkForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkForgot_LinkClicked);

            // 

            // LoginForm

            // 

            this.AcceptButton = this.btnLogin;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(368, 284);

            this.Controls.Add(this.panelBody);

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;

            this.MinimizeBox = false;

            this.Name = "LoginForm";

            this.Font = new System.Drawing.Font("Roboto", 9F);

            this.Padding = new System.Windows.Forms.Padding(0, 64, 0, 0);

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            this.Text = "SmartMed - Login";

            this.panelBody.ResumeLayout(false);

            this.panelBody.PerformLayout();

            this.ResumeLayout(false);

        }



        #endregion



        private System.Windows.Forms.Panel panelBody;

        private System.Windows.Forms.Label lblUsername;

        private System.Windows.Forms.TextBox txtUsername;

        private System.Windows.Forms.Label lblPassword;

        private System.Windows.Forms.TextBox txtPassword;

        private System.Windows.Forms.Button btnLogin;

        private System.Windows.Forms.Button btnRegister;

        private System.Windows.Forms.LinkLabel lnkForgot;

    }

}


