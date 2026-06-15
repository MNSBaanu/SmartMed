namespace SmartMed.UI.Views
{
    partial class MedicineManagementView
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.chkPrescription = new System.Windows.Forms.CheckBox();
            this.dtpExpiry = new System.Windows.Forms.DateTimePicker();
            this.lblExpiry = new System.Windows.Forms.Label();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.lblStock = new System.Windows.Forms.Label();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.lblDosage = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
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
            this.formPanel.Controls.Add(this.btnClear);
            this.formPanel.Controls.Add(this.btnDelete);
            this.formPanel.Controls.Add(this.btnUpdate);
            this.formPanel.Controls.Add(this.btnAdd);
            this.formPanel.Controls.Add(this.chkPrescription);
            this.formPanel.Controls.Add(this.dtpExpiry);
            this.formPanel.Controls.Add(this.lblExpiry);
            this.formPanel.Controls.Add(this.txtSupplier);
            this.formPanel.Controls.Add(this.lblSupplier);
            this.formPanel.Controls.Add(this.txtStock);
            this.formPanel.Controls.Add(this.lblStock);
            this.formPanel.Controls.Add(this.txtPrice);
            this.formPanel.Controls.Add(this.lblPrice);
            this.formPanel.Controls.Add(this.txtDosage);
            this.formPanel.Controls.Add(this.lblDosage);
            this.formPanel.Controls.Add(this.txtCategory);
            this.formPanel.Controls.Add(this.lblCategory);
            this.formPanel.Controls.Add(this.txtName);
            this.formPanel.Controls.Add(this.lblName);
            this.formPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.formPanel.Location = new System.Drawing.Point(0, 216);
            this.formPanel.Name = "formPanel";
            this.formPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.formPanel.Size = new System.Drawing.Size(812, 200);
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
            this.txtName.Size = new System.Drawing.Size(170, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblCategory.Location = new System.Drawing.Point(280, 16);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(64, 15);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category:";
            // 
            // txtCategory
            // 
            this.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtCategory.Location = new System.Drawing.Point(360, 12);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(170, 25);
            this.txtCategory.TabIndex = 3;
            // 
            // lblDosage
            // 
            this.lblDosage.AutoSize = true;
            this.lblDosage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDosage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblDosage.Location = new System.Drawing.Point(0, 51);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(53, 15);
            this.lblDosage.TabIndex = 4;
            this.lblDosage.Text = "Dosage:";
            // 
            // txtDosage
            // 
            this.txtDosage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDosage.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtDosage.Location = new System.Drawing.Point(80, 47);
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(170, 25);
            this.txtDosage.TabIndex = 5;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblPrice.Location = new System.Drawing.Point(280, 51);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(38, 15);
            this.lblPrice.TabIndex = 6;
            this.lblPrice.Text = "Price:";
            // 
            // txtPrice
            // 
            this.txtPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtPrice.Location = new System.Drawing.Point(360, 47);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(170, 25);
            this.txtPrice.TabIndex = 7;
            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblStock.Location = new System.Drawing.Point(0, 86);
            this.lblStock.Name = "lblStock";
            this.lblStock.Size = new System.Drawing.Size(42, 15);
            this.lblStock.TabIndex = 8;
            this.lblStock.Text = "Stock:";
            // 
            // txtStock
            // 
            this.txtStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtStock.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtStock.Location = new System.Drawing.Point(80, 82);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(170, 25);
            this.txtStock.TabIndex = 9;
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblSupplier.Location = new System.Drawing.Point(280, 86);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(58, 15);
            this.lblSupplier.TabIndex = 10;
            this.lblSupplier.Text = "Supplier:";
            // 
            // txtSupplier
            // 
            this.txtSupplier.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSupplier.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtSupplier.Location = new System.Drawing.Point(360, 82);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(170, 25);
            this.txtSupplier.TabIndex = 11;
            // 
            // lblExpiry
            // 
            this.lblExpiry.AutoSize = true;
            this.lblExpiry.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblExpiry.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblExpiry.Location = new System.Drawing.Point(0, 121);
            this.lblExpiry.Name = "lblExpiry";
            this.lblExpiry.Size = new System.Drawing.Size(45, 15);
            this.lblExpiry.TabIndex = 12;
            this.lblExpiry.Text = "Expiry:";
            // 
            // dtpExpiry
            // 
            this.dtpExpiry.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dtpExpiry.Location = new System.Drawing.Point(80, 117);
            this.dtpExpiry.Name = "dtpExpiry";
            this.dtpExpiry.Size = new System.Drawing.Size(170, 25);
            this.dtpExpiry.TabIndex = 13;
            // 
            // chkPrescription
            // 
            this.chkPrescription.AutoSize = true;
            this.chkPrescription.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.chkPrescription.Location = new System.Drawing.Point(360, 119);
            this.chkPrescription.Name = "chkPrescription";
            this.chkPrescription.Size = new System.Drawing.Size(154, 21);
            this.chkPrescription.TabIndex = 14;
            this.chkPrescription.Text = "Requires Prescription";
            this.chkPrescription.UseVisualStyleBackColor = true;
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAdd.Location = new System.Drawing.Point(0, 155);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(80, 36);
            this.btnAdd.TabIndex = 15;
            this.btnAdd.Text = "Add";
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnUpdate.Location = new System.Drawing.Point(90, 155);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(80, 36);
            this.btnUpdate.TabIndex = 16;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnDelete.Location = new System.Drawing.Point(180, 155);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 36);
            this.btnDelete.TabIndex = 17;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.BtnDelete_Click);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnClear.Location = new System.Drawing.Point(270, 155);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(80, 36);
            this.btnClear.TabIndex = 18;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // MedicineManagementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.body);
            this.Controls.Add(this.pageHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "MedicineManagementView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.MedicineManagementView_Load);
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
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Label lblStock;
        private System.Windows.Forms.TextBox txtStock;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label lblExpiry;
        private System.Windows.Forms.DateTimePicker dtpExpiry;
        private System.Windows.Forms.CheckBox chkPrescription;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnClear;
    }
}
