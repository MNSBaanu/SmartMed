namespace SmartMed.UI.Views
{
    partial class OrderManagementView
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
            this.actionPanel = new System.Windows.Forms.Panel();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.gridItemsCard = new System.Windows.Forms.Panel();
            this.gridItems = new System.Windows.Forms.DataGridView();
            this.lblItems = new System.Windows.Forms.Label();
            this.gridOrdersCard = new System.Windows.Forms.Panel();
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.lblOrders = new System.Windows.Forms.Label();
            this.body.SuspendLayout();
            this.actionPanel.SuspendLayout();
            this.gridItemsCard.SuspendLayout();
            this.gridOrdersCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
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
            // body
            // 
            this.body.Controls.Add(this.actionPanel);
            this.body.Controls.Add(this.gridItemsCard);
            this.body.Controls.Add(this.lblItems);
            this.body.Controls.Add(this.gridOrdersCard);
            this.body.Controls.Add(this.lblOrders);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 44);
            this.body.Name = "body";
            this.body.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.body.Size = new System.Drawing.Size(812, 587);
            this.body.TabIndex = 1;
            // 
            // lblOrders
            // 
            this.lblOrders.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrders.Location = new System.Drawing.Point(0, 16);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblOrders.Size = new System.Drawing.Size(812, 28);
            this.lblOrders.TabIndex = 0;
            this.lblOrders.Text = "Recent Orders";
            // 
            // gridOrdersCard
            // 
            this.gridOrdersCard.Controls.Add(this.gridOrders);
            this.gridOrdersCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridOrdersCard.Location = new System.Drawing.Point(0, 44);
            this.gridOrdersCard.Name = "gridOrdersCard";
            this.gridOrdersCard.Size = new System.Drawing.Size(812, 160);
            this.gridOrdersCard.TabIndex = 1;
            // 
            // gridOrders
            // 
            this.gridOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrders.Location = new System.Drawing.Point(0, 0);
            this.gridOrders.Name = "gridOrders";
            this.gridOrders.ReadOnly = true;
            this.gridOrders.RowHeadersWidth = 51;
            this.gridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.Size = new System.Drawing.Size(812, 160);
            this.gridOrders.TabIndex = 0;
            this.gridOrders.SelectionChanged += new System.EventHandler(this.GridOrders_SelectionChanged);
            // 
            // lblItems
            // 
            this.lblItems.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblItems.Location = new System.Drawing.Point(0, 204);
            this.lblItems.Name = "lblItems";
            this.lblItems.Padding = new System.Windows.Forms.Padding(0, 16, 0, 8);
            this.lblItems.Size = new System.Drawing.Size(812, 39);
            this.lblItems.TabIndex = 2;
            this.lblItems.Text = "Order Items";
            // 
            // gridItemsCard
            // 
            this.gridItemsCard.Controls.Add(this.gridItems);
            this.gridItemsCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridItemsCard.Location = new System.Drawing.Point(0, 243);
            this.gridItemsCard.Name = "gridItemsCard";
            this.gridItemsCard.Size = new System.Drawing.Size(812, 140);
            this.gridItemsCard.TabIndex = 3;
            // 
            // gridItems
            // 
            this.gridItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItems.Location = new System.Drawing.Point(0, 0);
            this.gridItems.Name = "gridItems";
            this.gridItems.ReadOnly = true;
            this.gridItems.RowHeadersWidth = 51;
            this.gridItems.Size = new System.Drawing.Size(812, 140);
            this.gridItems.TabIndex = 0;
            // 
            // actionPanel
            // 
            this.actionPanel.Controls.Add(this.btnUpdate);
            this.actionPanel.Controls.Add(this.cmbStatus);
            this.actionPanel.Controls.Add(this.lblStatus);
            this.actionPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.actionPanel.Location = new System.Drawing.Point(0, 383);
            this.actionPanel.Name = "actionPanel";
            this.actionPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.actionPanel.Size = new System.Drawing.Size(812, 50);
            this.actionPanel.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblStatus.Location = new System.Drawing.Point(0, 28);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Status:";
            // 
            // cmbStatus
            // 
            this.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Items.AddRange(new object[] {
            "Pending",
            "Ready for Pickup",
            "Delivered"});
            this.cmbStatus.Location = new System.Drawing.Point(60, 24);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(200, 25);
            this.cmbStatus.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnUpdate.FlatAppearance.BorderSize = 0;
            this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdate.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnUpdate.Location = new System.Drawing.Point(280, 22);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(120, 36);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update Status";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.BtnUpdate_Click);
            // 
            // OrderManagementView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.body);
            this.Controls.Add(this.pageHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "OrderManagementView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.OrderManagementView_Load);
            this.body.ResumeLayout(false);
            this.actionPanel.ResumeLayout(false);
            this.actionPanel.PerformLayout();
            this.gridItemsCard.ResumeLayout(false);
            this.gridOrdersCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pageHeader;
        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Panel gridOrdersCard;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Panel gridItemsCard;
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.Panel actionPanel;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.Button btnUpdate;
    }
}
