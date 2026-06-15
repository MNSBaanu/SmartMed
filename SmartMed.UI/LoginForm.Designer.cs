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
            this.panelCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblRole = new System.Windows.Forms.Label();
            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRegister = new System.Windows.Forms.Button();
            this.placeOrderView1 = new SmartMed.UI.Views.PlaceOrderView();
            this.panelCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCard
            // 
            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.placeOrderView1);
            this.panelCard.Controls.Add(this.lblTitle);
            this.panelCard.Controls.Add(this.lblSubtitle);
            this.panelCard.Controls.Add(this.lblRole);
            this.panelCard.Controls.Add(this.cmbRole);
            this.panelCard.Controls.Add(this.lblUsername);
            this.panelCard.Controls.Add(this.txtUsername);
            this.panelCard.Controls.Add(this.lblPassword);
            this.panelCard.Controls.Add(this.txtPassword);
            this.panelCard.Controls.Add(this.btnLogin);
            this.panelCard.Controls.Add(this.btnRegister);
            this.panelCard.Location = new System.Drawing.Point(440, 180);
            this.panelCard.Name = "panelCard";
            this.panelCard.Padding = new System.Windows.Forms.Padding(24);
            this.panelCard.Size = new System.Drawing.Size(400, 360);
            this.panelCard.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(24, 24);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 32);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "SmartMed Pharmacy";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblSubtitle.Location = new System.Drawing.Point(24, 60);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(188, 23);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Sign in to your account";
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblRole.Location = new System.Drawing.Point(24, 92);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(40, 20);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Role";
            // 
            // cmbRole
            // 
            this.cmbRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbRole.FormattingEnabled = true;
            this.cmbRole.Items.AddRange(new object[] {
            "Admin",
            "Customer"});
            this.cmbRole.Location = new System.Drawing.Point(24, 112);
            this.cmbRole.Name = "cmbRole";
            this.cmbRole.Size = new System.Drawing.Size(352, 29);
            this.cmbRole.TabIndex = 3;
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblUsername.Location = new System.Drawing.Point(24, 152);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(80, 20);
            this.lblUsername.TabIndex = 4;
            this.lblUsername.Text = "Username";
            // 
            // txtUsername
            // 
            this.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsername.Location = new System.Drawing.Point(24, 172);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(352, 29);
            this.txtUsername.TabIndex = 5;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblPassword.Location = new System.Drawing.Point(24, 212);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(76, 20);
            this.lblPassword.TabIndex = 6;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Location = new System.Drawing.Point(24, 232);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(352, 29);
            this.txtPassword.TabIndex = 7;
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Location = new System.Drawing.Point(24, 276);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(120, 36);
            this.btnLogin.TabIndex = 8;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnRegister
            // 
            this.btnRegister.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnRegister.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegister.Location = new System.Drawing.Point(154, 276);
            this.btnRegister.Name = "btnRegister";
            this.btnRegister.Size = new System.Drawing.Size(120, 36);
            this.btnRegister.TabIndex = 9;
            this.btnRegister.Text = "Register";
            this.btnRegister.UseVisualStyleBackColor = false;
            this.btnRegister.Click += new System.EventHandler(this.BtnRegister_Click);
            // 
            // placeOrderView1
            // 
            this.placeOrderView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.placeOrderView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.placeOrderView1.Location = new System.Drawing.Point(24, 24);
            this.placeOrderView1.Name = "placeOrderView1";
            this.placeOrderView1.Size = new System.Drawing.Size(352, 312);
            this.placeOrderView1.TabIndex = 10;
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelCard);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed Pharmacy - Login";
            this.panelCard.ResumeLayout(false);
            this.panelCard.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Button btnRegister;
        private Views.PlaceOrderView placeOrderView1;
    }
}
