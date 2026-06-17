namespace SmartMed.UI
{
    partial class AdminDashboardForm
    {
        private Controls.AdminDashboardPanel dashboardPanel;

        private void InitializeContentPanel()
        {
            this.dashboardPanel = new Controls.AdminDashboardPanel();
            this.dashboardPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dashboardPanel.Name = "dashboardPanel";
            this.dashboardPanel.TabIndex = 0;
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.dashboardPanel);
        }
    }
}
