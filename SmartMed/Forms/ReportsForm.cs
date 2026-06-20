namespace SmartMed.UI
{
    public partial class ReportsForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ReportsForm()
            : base(AdminNavItem.Reports, "Generate Reports", "SmartMed - Generate Reports")
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignTime)
                LoadActiveReport();
        }

        public void RefreshReports() => LoadActiveReport();
    }
}
