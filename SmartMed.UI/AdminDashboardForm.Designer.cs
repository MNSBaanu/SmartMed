namespace SmartMed.UI
{
    partial class AdminDashboardForm
    {
        private System.ComponentModel.IContainer components = null;
        private SmartMed.UI.Controls.AdminDashboardPanel dashboardPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dashboardPanel = new SmartMed.UI.Controls.AdminDashboardPanel();
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
            this.btnClose.Location = new System.Drawing.Point(1309, 8);
            // 
            // panelSidebar
            // 
            this.panelSidebar.Size = new System.Drawing.Size(280, 965);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Location = new System.Drawing.Point(0, 909);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.dashboardPanel);
            this.panelContent.Size = new System.Drawing.Size(1073, 965);
            // 
            // dashboardPanel
            // 
            this.dashboardPanel.AutoScroll = true;
            this.dashboardPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.dashboardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardPanel.Location = new System.Drawing.Point(24, 24);
            this.dashboardPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dashboardPanel.MinimumSize = new System.Drawing.Size(800, 1062);
            this.dashboardPanel.Name = "dashboardPanel";
            this.dashboardPanel.Size = new System.Drawing.Size(1025, 1062);
            this.dashboardPanel.TabIndex = 0;
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1013);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1140, 838);
            this.Name = "AdminDashboardForm";
            this.Text = "SmartMed - Admin Dashboard";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
