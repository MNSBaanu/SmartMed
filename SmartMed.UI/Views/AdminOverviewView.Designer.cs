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
            this.lblWelcome = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelInventory = new System.Windows.Forms.Panel();
            this.lblInventoryHint = new System.Windows.Forms.Label();
            this.lblInventoryValue = new System.Windows.Forms.Label();
            this.lblInventoryTitle = new System.Windows.Forms.Label();
            this.panelOrders = new System.Windows.Forms.Panel();
            this.lblOrdersHint = new System.Windows.Forms.Label();
            this.lblOrdersValue = new System.Windows.Forms.Label();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.panelRevenue = new System.Windows.Forms.Panel();
            this.lblRevenueHint = new System.Windows.Forms.Label();
            this.lblRevenueValue = new System.Windows.Forms.Label();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            this.panelActivity = new System.Windows.Forms.Panel();
            this.lblActivityTitle = new System.Windows.Forms.Label();
            this.gridActivity = new System.Windows.Forms.DataGridView();
            this.panelQuickFulfillment = new System.Windows.Forms.Panel();
            this.lblQuickTitle = new System.Windows.Forms.Label();
            this.lblQuickDesc = new System.Windows.Forms.Label();
            this.panelStockAlerts = new System.Windows.Forms.Panel();
            this.lblAlertsTitle = new System.Windows.Forms.Label();
            this.lblAlert1 = new System.Windows.Forms.Label();
            this.panelInventory.SuspendLayout();
            this.panelOrders.SuspendLayout();
            this.panelRevenue.SuspendLayout();
            this.panelActivity.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridActivity)).BeginInit();
            this.panelQuickFulfillment.SuspendLayout();
            this.panelStockAlerts.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(161, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome, admin";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblStatus.Location = new System.Drawing.Point(0, 36);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(290, 19);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "System Status: Operational • Last sync: now";
            // 
            // panelInventory
            // 
            this.panelInventory.Controls.Add(this.lblInventoryHint);
            this.panelInventory.Controls.Add(this.lblInventoryValue);
            this.panelInventory.Controls.Add(this.lblInventoryTitle);
            this.panelInventory.Location = new System.Drawing.Point(0, 72);
            this.panelInventory.Name = "panelInventory";
            this.panelInventory.Size = new System.Drawing.Size(260, 110);
            this.panelInventory.TabIndex = 2;
            // 
            // lblInventoryTitle
            // 
            this.lblInventoryTitle.AutoSize = true;
            this.lblInventoryTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblInventoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblInventoryTitle.Location = new System.Drawing.Point(16, 16);
            this.lblInventoryTitle.Name = "lblInventoryTitle";
            this.lblInventoryTitle.Size = new System.Drawing.Size(99, 15);
            this.lblInventoryTitle.TabIndex = 0;
            this.lblInventoryTitle.Text = "Active Inventory";
            // 
            // lblInventoryValue
            // 
            this.lblInventoryValue.AutoSize = true;
            this.lblInventoryValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblInventoryValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblInventoryValue.Location = new System.Drawing.Point(14, 36);
            this.lblInventoryValue.Name = "lblInventoryValue";
            this.lblInventoryValue.Size = new System.Drawing.Size(56, 32);
            this.lblInventoryValue.TabIndex = 1;
            this.lblInventoryValue.Text = "0";
            // 
            // lblInventoryHint
            // 
            this.lblInventoryHint.AutoSize = true;
            this.lblInventoryHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblInventoryHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblInventoryHint.Location = new System.Drawing.Point(16, 78);
            this.lblInventoryHint.Name = "lblInventoryHint";
            this.lblInventoryHint.Size = new System.Drawing.Size(125, 13);
            this.lblInventoryHint.TabIndex = 2;
            this.lblInventoryHint.Text = "Medicines in stock (units)";
            // 
            // panelOrders
            // 
            this.panelOrders.Controls.Add(this.lblOrdersHint);
            this.panelOrders.Controls.Add(this.lblOrdersValue);
            this.panelOrders.Controls.Add(this.lblOrdersTitle);
            this.panelOrders.Location = new System.Drawing.Point(276, 72);
            this.panelOrders.Name = "panelOrders";
            this.panelOrders.Size = new System.Drawing.Size(260, 110);
            this.panelOrders.TabIndex = 3;
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblOrdersTitle.Location = new System.Drawing.Point(16, 16);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(95, 15);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "Pending Orders";
            // 
            // lblOrdersValue
            // 
            this.lblOrdersValue.AutoSize = true;
            this.lblOrdersValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblOrdersValue.Location = new System.Drawing.Point(14, 36);
            this.lblOrdersValue.Name = "lblOrdersValue";
            this.lblOrdersValue.Size = new System.Drawing.Size(28, 32);
            this.lblOrdersValue.TabIndex = 1;
            this.lblOrdersValue.Text = "0";
            // 
            // lblOrdersHint
            // 
            this.lblOrdersHint.AutoSize = true;
            this.lblOrdersHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblOrdersHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblOrdersHint.Location = new System.Drawing.Point(16, 78);
            this.lblOrdersHint.Name = "lblOrdersHint";
            this.lblOrdersHint.Size = new System.Drawing.Size(78, 13);
            this.lblOrdersHint.TabIndex = 2;
            this.lblOrdersHint.Text = "Active orders";
            // 
            // panelRevenue
            // 
            this.panelRevenue.Controls.Add(this.lblRevenueHint);
            this.panelRevenue.Controls.Add(this.lblRevenueValue);
            this.panelRevenue.Controls.Add(this.lblRevenueTitle);
            this.panelRevenue.Location = new System.Drawing.Point(552, 72);
            this.panelRevenue.Name = "panelRevenue";
            this.panelRevenue.Size = new System.Drawing.Size(260, 110);
            this.panelRevenue.TabIndex = 4;
            // 
            // lblRevenueTitle
            // 
            this.lblRevenueTitle.AutoSize = true;
            this.lblRevenueTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblRevenueTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblRevenueTitle.Location = new System.Drawing.Point(16, 16);
            this.lblRevenueTitle.Name = "lblRevenueTitle";
            this.lblRevenueTitle.Size = new System.Drawing.Size(84, 15);
            this.lblRevenueTitle.TabIndex = 0;
            this.lblRevenueTitle.Text = "Total Revenue";
            // 
            // lblRevenueValue
            // 
            this.lblRevenueValue.AutoSize = true;
            this.lblRevenueValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRevenueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblRevenueValue.Location = new System.Drawing.Point(14, 38);
            this.lblRevenueValue.Name = "lblRevenueValue";
            this.lblRevenueValue.Size = new System.Drawing.Size(88, 30);
            this.lblRevenueValue.TabIndex = 1;
            this.lblRevenueValue.Text = "LKR 0.00";
            // 
            // lblRevenueHint
            // 
            this.lblRevenueHint.AutoSize = true;
            this.lblRevenueHint.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblRevenueHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblRevenueHint.Location = new System.Drawing.Point(16, 78);
            this.lblRevenueHint.Name = "lblRevenueHint";
            this.lblRevenueHint.Size = new System.Drawing.Size(58, 13);
            this.lblRevenueHint.TabIndex = 2;
            this.lblRevenueHint.Text = "Total sales";
            // 
            // panelActivity
            // 
            this.panelActivity.Controls.Add(this.gridActivity);
            this.panelActivity.Controls.Add(this.lblActivityTitle);
            this.panelActivity.Location = new System.Drawing.Point(0, 200);
            this.panelActivity.Name = "panelActivity";
            this.panelActivity.Size = new System.Drawing.Size(500, 220);
            this.panelActivity.TabIndex = 5;
            // 
            // lblActivityTitle
            // 
            this.lblActivityTitle.AutoSize = true;
            this.lblActivityTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblActivityTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblActivityTitle.Location = new System.Drawing.Point(12, 12);
            this.lblActivityTitle.Name = "lblActivityTitle";
            this.lblActivityTitle.Size = new System.Drawing.Size(198, 21);
            this.lblActivityTitle.TabIndex = 0;
            this.lblActivityTitle.Text = "Recent Fulfillment Activity";
            // 
            // gridActivity
            // 
            this.gridActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridActivity.Location = new System.Drawing.Point(12, 40);
            this.gridActivity.Name = "gridActivity";
            this.gridActivity.ReadOnly = true;
            this.gridActivity.RowHeadersWidth = 51;
            this.gridActivity.Size = new System.Drawing.Size(476, 168);
            this.gridActivity.TabIndex = 1;
            // 
            // panelQuickFulfillment
            // 
            this.panelQuickFulfillment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.panelQuickFulfillment.Controls.Add(this.lblQuickDesc);
            this.panelQuickFulfillment.Controls.Add(this.lblQuickTitle);
            this.panelQuickFulfillment.Location = new System.Drawing.Point(516, 200);
            this.panelQuickFulfillment.Name = "panelQuickFulfillment";
            this.panelQuickFulfillment.Size = new System.Drawing.Size(296, 104);
            this.panelQuickFulfillment.TabIndex = 6;
            // 
            // lblQuickTitle
            // 
            this.lblQuickTitle.AutoSize = true;
            this.lblQuickTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblQuickTitle.ForeColor = System.Drawing.Color.White;
            this.lblQuickTitle.Location = new System.Drawing.Point(12, 12);
            this.lblQuickTitle.Name = "lblQuickTitle";
            this.lblQuickTitle.Size = new System.Drawing.Size(131, 21);
            this.lblQuickTitle.TabIndex = 0;
            this.lblQuickTitle.Text = "Quick Fulfillment";
            // 
            // lblQuickDesc
            // 
            this.lblQuickDesc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblQuickDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(255)))));
            this.lblQuickDesc.Location = new System.Drawing.Point(12, 40);
            this.lblQuickDesc.Name = "lblQuickDesc";
            this.lblQuickDesc.Size = new System.Drawing.Size(270, 52);
            this.lblQuickDesc.TabIndex = 1;
            this.lblQuickDesc.Text = "Scan RX barcode or enter ID manually to start immediate processing.";
            // 
            // panelStockAlerts
            // 
            this.panelStockAlerts.Controls.Add(this.lblAlert1);
            this.panelStockAlerts.Controls.Add(this.lblAlertsTitle);
            this.panelStockAlerts.Location = new System.Drawing.Point(516, 320);
            this.panelStockAlerts.Name = "panelStockAlerts";
            this.panelStockAlerts.Size = new System.Drawing.Size(296, 100);
            this.panelStockAlerts.TabIndex = 7;
            // 
            // lblAlertsTitle
            // 
            this.lblAlertsTitle.AutoSize = true;
            this.lblAlertsTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblAlertsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblAlertsTitle.Location = new System.Drawing.Point(12, 12);
            this.lblAlertsTitle.Name = "lblAlertsTitle";
            this.lblAlertsTitle.Size = new System.Drawing.Size(93, 21);
            this.lblAlertsTitle.TabIndex = 0;
            this.lblAlertsTitle.Text = "Stock Alerts";
            // 
            // lblAlert1
            // 
            this.lblAlert1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblAlert1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.lblAlert1.Location = new System.Drawing.Point(12, 44);
            this.lblAlert1.Name = "lblAlert1";
            this.lblAlert1.Size = new System.Drawing.Size(270, 40);
            this.lblAlert1.TabIndex = 1;
            this.lblAlert1.Text = "Review low-stock medicines in Manage Medicines.";
            // 
            // AdminOverviewView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.panelStockAlerts);
            this.Controls.Add(this.panelQuickFulfillment);
            this.Controls.Add(this.panelActivity);
            this.Controls.Add(this.panelRevenue);
            this.Controls.Add(this.panelOrders);
            this.Controls.Add(this.panelInventory);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblWelcome);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "AdminOverviewView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.AdminOverviewView_Load);
            this.panelInventory.ResumeLayout(false);
            this.panelInventory.PerformLayout();
            this.panelOrders.ResumeLayout(false);
            this.panelOrders.PerformLayout();
            this.panelRevenue.ResumeLayout(false);
            this.panelRevenue.PerformLayout();
            this.panelActivity.ResumeLayout(false);
            this.panelActivity.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridActivity)).EndInit();
            this.panelQuickFulfillment.ResumeLayout(false);
            this.panelQuickFulfillment.PerformLayout();
            this.panelStockAlerts.ResumeLayout(false);
            this.panelStockAlerts.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelInventory;
        private System.Windows.Forms.Label lblInventoryTitle;
        private System.Windows.Forms.Label lblInventoryValue;
        private System.Windows.Forms.Label lblInventoryHint;
        private System.Windows.Forms.Panel panelOrders;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Label lblOrdersValue;
        private System.Windows.Forms.Label lblOrdersHint;
        private System.Windows.Forms.Panel panelRevenue;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblRevenueValue;
        private System.Windows.Forms.Label lblRevenueHint;
        private System.Windows.Forms.Panel panelActivity;
        private System.Windows.Forms.Label lblActivityTitle;
        private System.Windows.Forms.DataGridView gridActivity;
        private System.Windows.Forms.Panel panelQuickFulfillment;
        private System.Windows.Forms.Label lblQuickTitle;
        private System.Windows.Forms.Label lblQuickDesc;
        private System.Windows.Forms.Panel panelStockAlerts;
        private System.Windows.Forms.Label lblAlertsTitle;
        private System.Windows.Forms.Label lblAlert1;
    }
}
