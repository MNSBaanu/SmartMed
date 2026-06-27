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
            this.panelLoginCard = new System.Windows.Forms.Panel();

            this.lblUsername = new System.Windows.Forms.Label();

            this.txtUsername = new System.Windows.Forms.TextBox();

            this.lblPassword = new System.Windows.Forms.Label();

            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnTogglePassword = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();

            this.btnRegister = new System.Windows.Forms.Button();
            this.btnQuickAdmin = new System.Windows.Forms.Button();
            this.btnQuickCustomer = new System.Windows.Forms.Button();
            this.lnkForgot = new System.Windows.Forms.LinkLabel();

            this.panelBody.SuspendLayout();

            this.SuspendLayout();

            // 

            // panelBody

            // 

            this.panelBody.Controls.Add(this.panelLoginCard);

            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;

            this.panelBody.Location = new System.Drawing.Point(0, 64);

            this.panelBody.Name = "panelBody";

            this.panelBody.Size = new System.Drawing.Size(1184, 696);

            this.panelBody.TabIndex = 0;
            this.panelBody.Resize += new System.EventHandler(this.PanelBody_Resize);
            // 
            // panelLoginCard
            // 
            this.panelLoginCard.Controls.Add(this.lnkForgot);
            this.panelLoginCard.Controls.Add(this.btnQuickCustomer);
            this.panelLoginCard.Controls.Add(this.btnQuickAdmin);
            this.panelLoginCard.Controls.Add(this.btnRegister);
            this.panelLoginCard.Controls.Add(this.btnLogin);
            this.panelLoginCard.Controls.Add(this.btnTogglePassword);
            this.panelLoginCard.Controls.Add(this.txtPassword);
            this.panelLoginCard.Controls.Add(this.lblPassword);
            this.panelLoginCard.Controls.Add(this.txtUsername);
            this.panelLoginCard.Controls.Add(this.lblUsername);
            this.panelLoginCard.Location = new System.Drawing.Point(408, 222);
            this.panelLoginCard.Name = "panelLoginCard";
            this.panelLoginCard.Padding = new System.Windows.Forms.Padding(24, 16, 24, 16);
            this.panelLoginCard.Size = new System.Drawing.Size(368, 252);
            this.panelLoginCard.TabIndex = 0;

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
            this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtUsername_KeyDown);

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
            this.txtPassword.Size = new System.Drawing.Size(272, 23);
            this.txtPassword.TabIndex = 2;
            // 
            // btnTogglePassword
            // 
            this.btnTogglePassword.BackColor = System.Drawing.Color.White;
            this.btnTogglePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTogglePassword.FlatAppearance.BorderSize = 0;
            this.btnTogglePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTogglePassword.Font = new System.Drawing.Font("Roboto", 8F);
            this.btnTogglePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.btnTogglePassword.Location = new System.Drawing.Point(296, 90);
            this.btnTogglePassword.Name = "btnTogglePassword";
            this.btnTogglePassword.Size = new System.Drawing.Size(48, 23);
            this.btnTogglePassword.TabIndex = 7;
            this.btnTogglePassword.Text = "Show";
            this.btnTogglePassword.UseVisualStyleBackColor = false;
            this.btnTogglePassword.Click += new System.EventHandler(this.BtnTogglePassword_Click);

            // 

            // btnLogin

            // 

            this.btnLogin.Location = new System.Drawing.Point(24, 132);

            this.btnLogin.Name = "btnLogin";

            this.btnLogin.Size = new System.Drawing.Size(155, 32);

            this.btnLogin.TabIndex = 3;

            this.btnLogin.Text = "Login";

            this.btnLogin.UseVisualStyleBackColor = true;

            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);

            // 

            // btnRegister

            // 

            this.btnRegister.Location = new System.Drawing.Point(189, 132);

            this.btnRegister.Name = "btnRegister";

            this.btnRegister.Size = new System.Drawing.Size(155, 32);

            this.btnRegister.TabIndex = 4;

            this.btnRegister.Text = "Register";

            this.btnRegister.UseVisualStyleBackColor = true;

            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);

            // 
            // btnQuickAdmin
            // 
            this.btnQuickAdmin.Location = new System.Drawing.Point(24, 176);
            this.btnQuickAdmin.Name = "btnQuickAdmin";
            this.btnQuickAdmin.Size = new System.Drawing.Size(155, 32);
            this.btnQuickAdmin.TabIndex = 5;
            this.btnQuickAdmin.Text = "Admin Login";
            this.btnQuickAdmin.UseVisualStyleBackColor = true;
            this.btnQuickAdmin.Click += new System.EventHandler(this.BtnQuickAdmin_Click);
            // 
            // btnQuickCustomer
            // 
            this.btnQuickCustomer.Location = new System.Drawing.Point(189, 176);
            this.btnQuickCustomer.Name = "btnQuickCustomer";
            this.btnQuickCustomer.Size = new System.Drawing.Size(155, 32);
            this.btnQuickCustomer.TabIndex = 6;
            this.btnQuickCustomer.Text = "Customer Login";
            this.btnQuickCustomer.UseVisualStyleBackColor = true;
            this.btnQuickCustomer.Click += new System.EventHandler(this.BtnQuickCustomer_Click);

            // 

            // lnkForgot

            // 

            this.lnkForgot.AutoSize = true;

            this.lnkForgot.Location = new System.Drawing.Point(24, 220);

            this.lnkForgot.Name = "lnkForgot";

            this.lnkForgot.Size = new System.Drawing.Size(94, 15);

            this.lnkForgot.TabIndex = 5;

            this.lnkForgot.TabStop = true;

            this.lnkForgot.Text = "Forgot password?";

            this.lnkForgot.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LnkForgot_LinkClicked);

            // 

            // LoginForm

            // 

            this.AcceptButton = this.btnLogin;

            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);

            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

            this.ClientSize = new System.Drawing.Size(1184, 760);

            this.Controls.Add(this.panelBody);

            this.MinimumSize = new System.Drawing.Size(1000, 640);

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
        private System.Windows.Forms.Panel panelLoginCard;

        private System.Windows.Forms.Label lblUsername;

        private System.Windows.Forms.TextBox txtUsername;

        private System.Windows.Forms.Label lblPassword;

        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnTogglePassword;
        private System.Windows.Forms.Button btnLogin;

        private System.Windows.Forms.Button btnRegister;
        private System.Windows.Forms.Button btnQuickAdmin;
        private System.Windows.Forms.Button btnQuickCustomer;

        private System.Windows.Forms.LinkLabel lnkForgot;

    }

}


