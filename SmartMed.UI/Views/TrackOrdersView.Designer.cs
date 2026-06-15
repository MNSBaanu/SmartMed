namespace SmartMed.UI.Views
{
    partial class TrackOrdersView
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
            this.gridItemsCard = new System.Windows.Forms.Panel();
            this.lblItems = new System.Windows.Forms.Label();
            this.gridOrdersCard = new System.Windows.Forms.Panel();
            this.gridItems = new System.Windows.Forms.DataGridView();
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.body.SuspendLayout();
            this.gridItemsCard.SuspendLayout();
            this.gridOrdersCard.SuspendLayout();
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
            this.body.Controls.Add(this.gridItemsCard);
            this.body.Controls.Add(this.lblItems);
            this.body.Controls.Add(this.gridOrdersCard);
            this.body.Dock = System.Windows.Forms.DockStyle.Fill;
            this.body.Location = new System.Drawing.Point(0, 44);
            this.body.Name = "body";
            this.body.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.body.Size = new System.Drawing.Size(812, 587);
            this.body.TabIndex = 1;
            // 
            // gridOrdersCard
            // 
            this.gridOrdersCard.Controls.Add(this.gridOrders);
            this.gridOrdersCard.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridOrdersCard.Location = new System.Drawing.Point(0, 16);
            this.gridOrdersCard.Name = "gridOrdersCard";
            this.gridOrdersCard.Size = new System.Drawing.Size(812, 160);
            this.gridOrdersCard.TabIndex = 0;
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
            this.lblItems.Location = new System.Drawing.Point(0, 176);
            this.lblItems.Name = "lblItems";
            this.lblItems.Padding = new System.Windows.Forms.Padding(0, 16, 0, 8);
            this.lblItems.Size = new System.Drawing.Size(812, 39);
            this.lblItems.TabIndex = 1;
            this.lblItems.Text = "Order Items";
            // 
            // gridItemsCard
            // 
            this.gridItemsCard.Controls.Add(this.gridItems);
            this.gridItemsCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItemsCard.Location = new System.Drawing.Point(0, 215);
            this.gridItemsCard.Name = "gridItemsCard";
            this.gridItemsCard.Size = new System.Drawing.Size(812, 372);
            this.gridItemsCard.TabIndex = 2;
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
            this.gridItems.Size = new System.Drawing.Size(812, 372);
            this.gridItems.TabIndex = 0;
            // 
            // TrackOrdersView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.body);
            this.Controls.Add(this.pageHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "TrackOrdersView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.TrackOrdersView_Load);
            this.body.ResumeLayout(false);
            this.gridItemsCard.ResumeLayout(false);
            this.gridOrdersCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pageHeader;
        private System.Windows.Forms.Panel body;
        private System.Windows.Forms.Panel gridOrdersCard;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Label lblItems;
        private System.Windows.Forms.Panel gridItemsCard;
        private System.Windows.Forms.DataGridView gridItems;
    }
}
