namespace SmartMed.UI
{
    public partial class AdminDashboardForm : AdminShellForm
    {
        private bool _pageBuilt;

        public AdminDashboardForm()
            : base(AdminNavItem.Overview, "Admin Dashboard", "SmartMed - Admin Dashboard")
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignTime)
                LoadDashboardData();
        }

        public void RefreshData() => LoadDashboardData();
    }
}
