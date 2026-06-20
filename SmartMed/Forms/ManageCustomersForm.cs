namespace SmartMed.UI
{
    public partial class ManageCustomersForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ManageCustomersForm()
            : base(AdminNavItem.Customers, "Manage Customers", "SmartMed - Manage Customers")
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignTime)
                LoadCustomers();
        }
    }
}
