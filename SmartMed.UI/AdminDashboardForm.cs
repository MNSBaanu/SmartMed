namespace SmartMed.UI
{
    public partial class AdminDashboardForm : AdminShellForm
    {
        public AdminDashboardForm()
            : base(AdminNavItem.Overview, "Admin Dashboard", "SmartMed - Admin Dashboard")
        {
            InitializeContentPanel();
        }

        public void RefreshData() => dashboardPanel?.RefreshData();
    }
}
