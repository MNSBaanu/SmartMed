namespace SmartMed.UI
{
    public partial class ManageOrdersForm : AdminShellForm
    {
        public ManageOrdersForm()
            : base(AdminNavItem.Orders, "Manage Orders", "SmartMed - Manage Orders")
        {
        }

        protected override void InitializePageContent()
        {
            if (ordersPanel != null)
                return;
            InitializeComponent();
        }
    }
}
