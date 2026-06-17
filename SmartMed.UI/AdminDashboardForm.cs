using System.Drawing;
using System.Windows.Forms;
using SmartMed.UI.Controls;

namespace SmartMed.UI
{
    public partial class AdminDashboardForm : AdminShellForm
    {
        private AdminDashboardPanel dashboardPanel;

        public AdminDashboardForm()
            : base(AdminNavItem.Overview, "Admin Dashboard", "SmartMed - Admin Dashboard")
        {
        }

        protected override void InitializePageContent()
        {
            if (dashboardPanel != null)
                return;

            dashboardPanel = new AdminDashboardPanel
            {
                Dock = DockStyle.Fill,
                Name = "dashboardPanel"
            };
            panelContent.AutoScroll = true;
            panelContent.Padding = new Padding(24);
            panelContent.Controls.Add(dashboardPanel);
        }

        public void RefreshData() => dashboardPanel?.RefreshData();
    }
}
