namespace SmartMed.UI.Views
{
    partial class AdminOverviewView
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
            this.lblSectionHeader = new System.Windows.Forms.Label();
            this.panelSales = new System.Windows.Forms.Panel();
            this.lblSalesTitle = new System.Windows.Forms.Label();
            this.lblSalesValue = new System.Windows.Forms.Label();
            this.panelStock = new System.Windows.Forms.Panel();
            this.lblStockTitle = new System.Windows.Forms.Label();
            this.lblStockValue = new System.Windows.Forms.Label();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.lblOrdersValue = new System.Windows.Forms.Label();
            this.panelSales.SuspendLayout();
            this.panelStock.SuspendLayout();
            this.panelOrders.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSectionHeader
            // 
            this.lblSectionHeader.AutoSize = true;
            this.lblSectionHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSectionHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblSectionHeader.Location = new System.Drawing.Point(0, 0);
            this.lblSectionHeader.Name = "lblSectionHeader";
            this.lblSectionHeader.Size = new System.Drawing.Size(161, 21);
            this.lblSectionHeader.TabIndex = 0;
            this.lblSectionHeader.Text = "Dashboard Overview";
            // 
            // panelSales
            // 
            this.panelSales.BackColor = System.Drawing.Color.White;
            this.panelSales.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelSales.Controls.Add(this.lblSalesTitle);
            this.panelSales.Controls.Add(this.lblSalesValue);
            this.panelSales.Location = new System.Drawing.Point(0, 40);
            this.panelSales.Name = "panelSales";
            this.panelSales.Size = new System.Drawing.Size(260, 100);
            this.panelSales.TabIndex = 1;
            // 
            // lblSalesTitle
            // 
            this.lblSalesTitle.AutoSize = true;
            this.lblSalesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSalesTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblSalesTitle.Location = new System.Drawing.Point(16, 16);
            this.lblSalesTitle.Name = "lblSalesTitle";
            this.lblSalesTitle.Size = new System.Drawing.Size(68, 15);
            this.lblSalesTitle.TabIndex = 0;
            this.lblSalesTitle.Text = "Total Sales";
            // 
            // lblSalesValue
            // 
            this.lblSalesValue.AutoSize = true;
            this.lblSalesValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblSalesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblSalesValue.Location = new System.Drawing.Point(16, 44);
            this.lblSalesValue.Name = "lblSalesValue";
            this.lblSalesValue.Size = new System.Drawing.Size(88, 21);
            this.lblSalesValue.TabIndex = 1;
            this.lblSalesValue.Text = "LKR 0.00";
            // 
            // panelStock
            // 
            this.panelStock.BackColor = System.Drawing.Color.White;
            this.panelStock.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelStock.Controls.Add(this.lblStockTitle);
            this.panelStock.Controls.Add(this.lblStockValue);
            this.panelStock.Location = new System.Drawing.Point(276, 40);
            this.panelStock.Name = "panelStock";
            this.panelStock.Size = new System.Drawing.Size(260, 100);
            this.panelStock.TabIndex = 2;
            // 
            // lblStockTitle
            // 
            this.lblStockTitle.AutoSize = true;
            this.lblStockTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStockTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblStockTitle.Location = new System.Drawing.Point(16, 16);
            this.lblStockTitle.Name = "lblStockTitle";
            this.lblStockTitle.Size = new System.Drawing.Size(125, 15);
            this.lblStockTitle.TabIndex = 0;
            this.lblStockTitle.Text = "Medicines in Stock";
            // 
            // lblStockValue
            // 
            this.lblStockValue.AutoSize = true;
            this.lblStockValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblStockValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblStockValue.Location = new System.Drawing.Point(16, 44);
            this.lblStockValue.Name = "lblStockValue";
            this.lblStockValue.Size = new System.Drawing.Size(68, 21);
            this.lblStockValue.TabIndex = 1;
            this.lblStockValue.Text = "0 units";
            // 
            // panelOrders
            // 
            this.panelOrders.BackColor = System.Drawing.Color.White;
            this.panelOrders.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelOrders.Controls.Add(this.lblOrdersTitle);
            this.panelOrders.Controls.Add(this.lblOrdersValue);
            this.panelOrders.Location = new System.Drawing.Point(552, 40);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(260, 100);
            this.panelOrders.TabIndex = 3;
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblOrdersTitle.Location = new System.Drawing.Point(16, 16);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(87, 15);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "Active Orders";
            // 
            // lblOrdersValue
            // 
            this.lblOrdersValue.AutoSize = true;
            this.lblOrdersValue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblOrdersValue.Location = new System.Drawing.Point(16, 44);
            this.lblOrdersValue.Name = "lblOrdersValue";
            this.lblOrdersValue.Size = new System.Drawing.Size(19, 21);
            this.lblOrdersValue.TabIndex = 1;
            this.lblOrdersValue.Text = "0";
            // 
            // AdminOverviewView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.panelOrders);
            this.Controls.Add(this.panelStock);
            this.Controls.Add(this.panelSales);
            this.Controls.Add(this.lblSectionHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "AdminOverviewView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.AdminOverviewView_Load);
            this.panelSales.ResumeLayout(false);
            this.panelSales.PerformLayout();
            this.panelStock.ResumeLayout(false);
            this.panelStock.PerformLayout();
            this.panelOrders.ResumeLayout(false);
            this.panelOrders.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblSectionHeader;
        private System.Windows.Forms.Panel panelSales;
        private System.Windows.Forms.Label lblSalesTitle;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.Panel panelStock;
        private System.Windows.Forms.Label lblStockTitle;
        private System.Windows.Forms.Label lblStockValue;
        private System.Windows.Forms.Panel panelOrders;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Label lblOrdersValue;
    }
}
