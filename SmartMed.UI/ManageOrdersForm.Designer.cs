namespace SmartMed.UI
{
    partial class ManageOrdersForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.ordersPanel = new SmartMed.UI.Controls.ManageOrdersPanel();
            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelTop.Size = new System.Drawing.Size(1546, 64);
            // 
            // btnClose
            // 
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.Location = new System.Drawing.Point(1502, 11);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            // 
            // panelSidebar
            // 
            this.panelSidebar.Location = new System.Drawing.Point(0, 64);
            this.panelSidebar.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSidebar.Padding = new System.Windows.Forms.Padding(0, 21, 0, 21);
            this.panelSidebar.Size = new System.Drawing.Size(320, 1111);
            // 
            // btnNavOverview
            // 
            this.btnNavOverview.FlatAppearance.BorderSize = 0;
            this.btnNavOverview.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNavOverview.Size = new System.Drawing.Size(280, 53);
            // 
            // btnNavMedicines
            // 
            this.btnNavMedicines.FlatAppearance.BorderSize = 0;
            this.btnNavMedicines.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.btnNavMedicines.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNavMedicines.Size = new System.Drawing.Size(280, 53);
            // 
            // btnNavCustomers
            // 
            this.btnNavCustomers.FlatAppearance.BorderSize = 0;
            this.btnNavCustomers.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.btnNavCustomers.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNavCustomers.Size = new System.Drawing.Size(280, 53);
            // 
            // btnNavOrders
            // 
            this.btnNavOrders.FlatAppearance.BorderSize = 0;
            this.btnNavOrders.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.btnNavOrders.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNavOrders.Size = new System.Drawing.Size(280, 53);
            // 
            // btnNavReports
            // 
            this.btnNavReports.FlatAppearance.BorderSize = 0;
            this.btnNavReports.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(255)))));
            this.btnNavReports.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNavReports.Size = new System.Drawing.Size(280, 53);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.FlatAppearance.BorderSize = 0;
            this.btnNavLogout.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.btnNavLogout.Location = new System.Drawing.Point(0, 1037);
            this.btnNavLogout.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnNavLogout.Size = new System.Drawing.Size(280, 53);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.ordersPanel);
            this.panelContent.Location = new System.Drawing.Point(320, 64);
            this.panelContent.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelContent.Padding = new System.Windows.Forms.Padding(27, 32, 27, 32);
            this.panelContent.Size = new System.Drawing.Size(1226, 1111);
            // 
            // ordersPanel
            // 
            this.ordersPanel.AutoScroll = true;
            this.ordersPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ordersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordersPanel.Location = new System.Drawing.Point(27, 32);
            this.ordersPanel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.ordersPanel.MinimumSize = new System.Drawing.Size(800, 1406);
            this.ordersPanel.Name = "ordersPanel";
            this.ordersPanel.Size = new System.Drawing.Size(1172, 1406);
            this.ordersPanel.TabIndex = 0;
            // 
            // ManageOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1546, 1175);
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(1300, 1102);
            this.Name = "ManageOrdersForm";
            this.Text = "SmartMed - Manage Orders";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Controls.ManageOrdersPanel ordersPanel;
    }
}
