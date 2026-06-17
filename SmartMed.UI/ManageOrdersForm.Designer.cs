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
            this.panelTop.Size = new System.Drawing.Size(1353, 48);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1478, 8);
            // 
            // panelSidebar
            // 
            this.panelSidebar.Size = new System.Drawing.Size(280, 965);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Location = new System.Drawing.Point(0, 1162);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.ordersPanel);
            this.panelContent.Size = new System.Drawing.Size(1073, 965);
            // 
            // ordersPanel
            // 
            this.ordersPanel.AutoScroll = true;
            this.ordersPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ordersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ordersPanel.Location = new System.Drawing.Point(24, 24);
            this.ordersPanel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.ordersPanel.MinimumSize = new System.Drawing.Size(914, 1125);
            this.ordersPanel.Name = "ordersPanel";
            this.ordersPanel.Size = new System.Drawing.Size(1025, 1125);
            this.ordersPanel.TabIndex = 0;
            // 
            // ManageOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1013);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1140, 838);
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
