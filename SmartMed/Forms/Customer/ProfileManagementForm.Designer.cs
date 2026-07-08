namespace SmartMed.UI
{
    partial class ProfileManagementForm
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
            this.panelScrollHost = new System.Windows.Forms.Panel();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.lblSectionTitle = new System.Windows.Forms.Label();
            this.panelFormOuter = new System.Windows.Forms.Panel();
            this.tableFields = new System.Windows.Forms.TableLayoutPanel();
            this.lblFullName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSaveProfile = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelFormOuter.SuspendLayout();
            this.tableFields.SuspendLayout();
            this.flowActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelScrollHost
            // 
            this.panelScrollHost.AutoScroll = true;
            this.panelScrollHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelScrollHost.Controls.Add(this.tableLayoutRoot);
            this.panelScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScrollHost.Location = new System.Drawing.Point(0, 0);
            this.panelScrollHost.Name = "panelScrollHost";
            this.panelScrollHost.Padding = new System.Windows.Forms.Padding(24);
            this.panelScrollHost.Size = new System.Drawing.Size(1060, 720);
            this.panelScrollHost.TabIndex = 0;
            // 
            // tableLayoutRoot
            // 
            this.tableLayoutRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.lblSectionTitle, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelFormOuter, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.flowActions, 0, 3);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 4;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 420);
            this.tableLayoutRoot.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(280, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Update your contact details and password.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(130, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "My Profile";
            // 
            // lblSectionTitle
            // 
            this.lblSectionTitle.AutoSize = true;
            this.lblSectionTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblSectionTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSectionTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblSectionTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblSectionTitle.Location = new System.Drawing.Point(0, 92);
            this.lblSectionTitle.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblSectionTitle.Name = "lblSectionTitle";
            this.lblSectionTitle.Size = new System.Drawing.Size(1012, 16);
            this.lblSectionTitle.TabIndex = 1;
            this.lblSectionTitle.Text = "PERSONAL DETAILS";
            // 
            // panelFormOuter
            // 
            this.panelFormOuter.BackColor = System.Drawing.Color.White;
            this.panelFormOuter.Controls.Add(this.tableFields);
            this.panelFormOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFormOuter.Location = new System.Drawing.Point(0, 112);
            this.panelFormOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelFormOuter.Name = "panelFormOuter";
            this.panelFormOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelFormOuter.Size = new System.Drawing.Size(1012, 260);
            this.panelFormOuter.TabIndex = 2;
            // 
            // tableFields
            // 
            this.tableFields.BackColor = System.Drawing.Color.White;
            this.tableFields.ColumnCount = 1;
            this.tableFields.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableFields.Controls.Add(this.lblFullName, 0, 0);
            this.tableFields.Controls.Add(this.txtName, 0, 1);
            this.tableFields.Controls.Add(this.lblEmail, 0, 2);
            this.tableFields.Controls.Add(this.txtEmail, 0, 3);
            this.tableFields.Controls.Add(this.lblPhone, 0, 4);
            this.tableFields.Controls.Add(this.txtPhone, 0, 5);
            this.tableFields.Controls.Add(this.lblAddress, 0, 6);
            this.tableFields.Controls.Add(this.txtAddress, 0, 7);
            this.tableFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableFields.Location = new System.Drawing.Point(1, 1);
            this.tableFields.Name = "tableFields";
            this.tableFields.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.tableFields.RowCount = 8;
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableFields.Size = new System.Drawing.Size(1010, 258);
            this.tableFields.TabIndex = 0;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.BackColor = System.Drawing.Color.White;
            this.lblFullName.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblFullName.Location = new System.Drawing.Point(16, 12);
            this.lblFullName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(74, 18);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "Full Name *";
            // 
            // txtName
            // 
            this.txtName.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtName.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtName.Location = new System.Drawing.Point(16, 34);
            this.txtName.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(978, 25);
            this.txtName.TabIndex = 1;
            this.txtName.Text = "Jane Perera";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.White;
            this.lblEmail.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblEmail.Location = new System.Drawing.Point(16, 67);
            this.lblEmail.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(50, 18);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email *";
            // 
            // txtEmail
            // 
            this.txtEmail.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtEmail.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtEmail.Location = new System.Drawing.Point(16, 89);
            this.txtEmail.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(978, 25);
            this.txtEmail.TabIndex = 3;
            this.txtEmail.Text = "jane.perera@example.com";
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.BackColor = System.Drawing.Color.White;
            this.lblPhone.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblPhone.Location = new System.Drawing.Point(16, 122);
            this.lblPhone.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(56, 18);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone *";
            // 
            // txtPhone
            // 
            this.txtPhone.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtPhone.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtPhone.Location = new System.Drawing.Point(16, 144);
            this.txtPhone.Margin = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.txtPhone.MaxLength = 14;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(978, 25);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.Text = "0771234567";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.BackColor = System.Drawing.Color.White;
            this.lblAddress.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblAddress.Location = new System.Drawing.Point(16, 177);
            this.lblAddress.Margin = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(66, 18);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address *";
            // 
            // txtAddress
            // 
            this.txtAddress.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtAddress.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtAddress.Location = new System.Drawing.Point(16, 199);
            this.txtAddress.Margin = new System.Windows.Forms.Padding(0);
            this.txtAddress.Multiline = true;
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAddress.Size = new System.Drawing.Size(978, 52);
            this.txtAddress.TabIndex = 7;
            this.txtAddress.Text = "12 Hospital Road, Colombo";
            // 
            // flowActions
            // 
            this.flowActions.AutoSize = true;
            this.flowActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowActions.Controls.Add(this.btnSaveProfile);
            this.flowActions.Controls.Add(this.btnChangePassword);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.Location = new System.Drawing.Point(0, 388);
            this.flowActions.Margin = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.flowActions.Name = "flowActions";
            this.flowActions.Size = new System.Drawing.Size(1012, 32);
            this.flowActions.TabIndex = 3;
            this.flowActions.WrapContents = false;
            // 
            // btnSaveProfile
            // 
            this.btnSaveProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnSaveProfile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSaveProfile.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnSaveProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaveProfile.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnSaveProfile.ForeColor = System.Drawing.Color.White;
            this.btnSaveProfile.Location = new System.Drawing.Point(0, 0);
            this.btnSaveProfile.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnSaveProfile.Name = "btnSaveProfile";
            this.btnSaveProfile.Size = new System.Drawing.Size(120, 32);
            this.btnSaveProfile.TabIndex = 0;
            this.btnSaveProfile.Text = "Save Profile";
            this.btnSaveProfile.UseVisualStyleBackColor = false;
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnChangePassword.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangePassword.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnChangePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnChangePassword.Location = new System.Drawing.Point(130, 0);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(140, 32);
            this.btnChangePassword.TabIndex = 1;
            this.btnChangePassword.Text = "Change Password";
            this.btnChangePassword.UseVisualStyleBackColor = false;
            // 
            // ProfileManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.ControlBox = false;
            this.Controls.Add(this.panelScrollHost);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProfileManagementForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "My Profile";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.tableLayoutRoot.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelFormOuter.ResumeLayout(false);
            this.tableFields.ResumeLayout(false);
            this.tableFields.PerformLayout();
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblSectionTitle;
        private System.Windows.Forms.Panel panelFormOuter;
        private System.Windows.Forms.TableLayoutPanel tableFields;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnChangePassword;
    }
}
