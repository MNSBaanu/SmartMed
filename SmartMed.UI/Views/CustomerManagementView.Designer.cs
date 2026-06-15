namespace SmartMed.UI.Views
{
    partial class CustomerManagementView
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pageHeader = new System.Windows.Forms.Panel();
            this.body = new System.Windows.Forms.Panel();
            this.formPanel = new System.Windows.Forms.Panel();
            this.gridCard = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.body.SuspendLayout();
            this.formPanel.SuspendLayout();
            this.gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.Location = new System.Drawing.Point(0, 0);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Size = new System.Drawing.Size(812, 64);
            this.pageHeader.TabIndex = 0;
            // 
            // body
            // 
            this.body.Controls.Add(this.formPanel);
            this.body.Controls.Add(this.gridCard);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 64);
            this.body.Name = "body";
            this.body.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.body.Size = new System.Drawing.Size(812, 587);
            this.body.TabIndex = 1;
            // 
            // gridCard
            // 
            this.gridCard.Controls.Add(this.grid);
            this.gridCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridCard.Location = new System.Drawing.Point(0, 16);
            this.gridCard.Name = "gridCard";
            this.gridCard.Size = new System.Drawing.Size(812, 200);
            this.gridCard.TabIndex = 0;
            // 
            // grid
            // 
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 51;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(812, 200);
            this.grid.TabIndex = 0;
            this.grid.SelectionChanged += new System.EventHandler(this.Grid_SelectionChanged);
            // 
            // formPanel
            // 
            this.formPanel.Controls.Add(this.btnUpdate);
            this.formPanel.Controls.Add(this.txtAddress);
            this.formPanel.Controls.Add(this.lblAddress);
            this.formPanel.Controls.Add(this.txtPhone);
            this.formPanel.Controls.Add(this.lblPhone);
            this.formPanel.Controls.Add(this.txtEmail);
            this.formPanel.Controls.Add(this.lblEmail);
            this.formPanel.Controls.Add(this.txtName);
            this.formPanel.Controls.Add(this.lblName);
            this.formPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.formPanel.Location = new System.Drawing.Point(0, 216);
            this.formPanel.Name = "formPanel";
            this.formPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.formPanel.Size = new System.Drawing.Size(812, 180);
            this.formPanel.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblName.Location = new System.Drawing.Point(0, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtName.Location = new System.Drawing.Point(80, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(300, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblEmail.Location = new System.Drawing.Point(0, 51);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(42, 15);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email:";
            // 
            // txtEmail
            // 
            this.txtEmail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtEmail.Location = new System.Drawing.Point(80, 47);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(300, 25);
            this.txtEmail.TabIndex = 3;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPhone.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblPhone.Location = new System.Drawing.Point(0, 86);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(47, 15);
            this.lblPhone.TabIndex = 4;
            this.lblPhone.Text = "Phone:";
            // 
            // txtPhone
            // 
            this.txtPhone.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPhone.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPhone.Location = new System.Drawing.Point(80, 82);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(300, 25);
            this.txtPhone.TabIndex = 5;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAddress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblAddress.Location = new System.Drawing.Point(0, 121);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(57, 15);
            this.lblAddress.TabIndex = 6;
            this.lblAddress.Text = "Address:";
            // 
            // txtAddress
            // 
            this.txtAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAddress.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtAddress.Location = new System.Drawing.Point(80, 117);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(500, 25);
            this.txtAddress.TabIndex = 7;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnUpdate.Location = new System.Drawing.Point(80, 152);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(140, 36);
            this.btnUpdate.TabIndex = 8;
            this.btnUpdate.Text = "Update Customer";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // CustomerManagementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.body);
            this.Controls.Add(this.pageHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "CustomerManagementView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.CustomerManagementView_Load);
            this.body.ResumeLayout(false);
            this.formPanel.ResumeLayout(false);
            this.formPanel.PerformLayout();
            this.gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pageHeader;
        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Panel gridCard;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel formPanel;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Button btnUpdate;
    }
}
