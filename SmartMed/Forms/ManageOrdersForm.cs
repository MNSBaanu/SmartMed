namespace SmartMed.UI
{
    public partial class ManageOrdersForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ManageOrdersForm()
            : base(AdminNavItem.Orders, "Manage Orders", "SmartMed - Manage Orders")
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignTime)
                LoadOrders();
        }
    }
}
