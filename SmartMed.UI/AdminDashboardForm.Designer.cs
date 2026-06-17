namespace SmartMed.UI
{
    partial class AdminDashboardForm
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
            this.dashboardPanel = new SmartMed.UI.Controls.AdminDashboardPanel();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // dashboardPanel
            // 
            this.dashboardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardPanel.Location = new System.Drawing.Point(24, 24);
            this.dashboardPanel.Name = "dashboardPanel";
            this.dashboardPanel.Size = new System.Drawing.Size(856, 664);
            this.dashboardPanel.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.dashboardPanel);
            this.panelContent.Padding = new System.Windows.Forms.Padding(24);
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 760);
            this.Name = "AdminDashboardForm";
            this.Text = "SmartMed - Admin Dashboard";
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Controls.AdminDashboardPanel dashboardPanel;
    }
}
