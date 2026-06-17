namespace SmartMed.UI
{
    public partial class AdminDashboardForm : AdminShellForm
    {
        public AdminDashboardForm()
            : base(AdminNavItem.Overview, "Admin Dashboard", "SmartMed - Admin Dashboard")
        {
        }

        protected override void InitializePageContent()
        {
            if (dashboardPanel != null)
                return;
            InitializeComponent();
        }

        public void RefreshData() => dashboardPanel?.RefreshData();
    }
}
