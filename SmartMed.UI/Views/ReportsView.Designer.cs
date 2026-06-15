namespace SmartMed.UI.Views
{
    partial class ReportsView
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
            this.toolbar = new System.Windows.Forms.Panel();
            this.gridCard = new System.Windows.Forms.Panel();
            this.btnHistory = new System.Windows.Forms.Button();
            this.btnStock = new System.Windows.Forms.Button();
            this.btnSales = new System.Windows.Forms.Button();
            this.grid = new System.Windows.Forms.DataGridView();
            this.toolbar.SuspendLayout();
            this.gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.Location = new System.Drawing.Point(0, 0);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Size = new System.Drawing.Size(812, 44);
            this.pageHeader.TabIndex = 0;
            // 
            // toolbar
            // 
            this.toolbar.Controls.Add(this.btnHistory);
            this.toolbar.Controls.Add(this.btnStock);
            this.toolbar.Controls.Add(this.btnSales);
            this.toolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.toolbar.Location = new System.Drawing.Point(0, 44);
            this.toolbar.Name = "toolbar";
            this.toolbar.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.toolbar.Size = new System.Drawing.Size(812, 50);
            this.toolbar.TabIndex = 1;
            // 
            // btnSales
            // 
            this.btnSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnSales.FlatAppearance.BorderSize = 0;
            this.btnSales.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSales.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnSales.Location = new System.Drawing.Point(0, 24);
            this.btnSales.Name = "btnSales";
            this.btnSales.Size = new System.Drawing.Size(120, 36);
            this.btnSales.TabIndex = 0;
            this.btnSales.Text = "Sales Report";
            this.btnSales.UseVisualStyleBackColor = false;
            this.btnSales.Click += new System.EventHandler(this.BtnSales_Click);
            // 
            // btnStock
            // 
            this.btnStock.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnStock.FlatAppearance.BorderSize = 0;
            this.btnStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStock.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnStock.Location = new System.Drawing.Point(130, 24);
            this.btnStock.Name = "btnStock";
            this.btnStock.Size = new System.Drawing.Size(120, 36);
            this.btnStock.TabIndex = 1;
            this.btnStock.Text = "Stock Report";
            this.btnStock.UseVisualStyleBackColor = false;
            this.btnStock.Click += new System.EventHandler(this.BtnStock_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnHistory.FlatAppearance.BorderSize = 0;
            this.btnHistory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHistory.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnHistory.Location = new System.Drawing.Point(260, 24);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(180, 36);
            this.btnHistory.TabIndex = 2;
            this.btnHistory.Text = "Customer Order History";
            this.btnHistory.UseVisualStyleBackColor = false;
            this.btnHistory.Click += new System.EventHandler(this.BtnHistory_Click);
            // 
            // gridCard
            // 
            this.gridCard.Controls.Add(this.grid);
            this.gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCard.Location = new System.Drawing.Point(0, 94);
            this.gridCard.Name = "gridCard";
            this.gridCard.Size = new System.Drawing.Size(812, 530);
            this.gridCard.TabIndex = 2;
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
            this.grid.Size = new System.Drawing.Size(812, 530);
            this.grid.TabIndex = 0;
            // 
            // ReportsView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.gridCard);
            this.Controls.Add(this.toolbar);
            this.Controls.Add(this.pageHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "ReportsView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.ReportsView_Load);
            this.toolbar.ResumeLayout(false);
            this.gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pageHeader;
        private System.Windows.Forms.Panel toolbar;
        private System.Windows.Forms.Panel gridCard;
        private System.Windows.Forms.Button btnSales;
        private System.Windows.Forms.Button btnStock;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.DataGridView grid;
    }
}
