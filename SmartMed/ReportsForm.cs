namespace SmartMed.UI
{
    public partial class ReportsForm : AdminShellForm
    {
        public ReportsForm()
            : base(AdminNavItem.Reports, "Generate Reports", "SmartMed - Generate Reports")
        {
        }

        protected override void InitializePageContent()
        {
            if (reportsPanel != null)
                return;
            InitializeComponent();
        }

        public void RefreshReports() => reportsPanel?.RefreshReports();
    }
}
