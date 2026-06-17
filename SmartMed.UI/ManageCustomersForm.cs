namespace SmartMed.UI
{
    public partial class ManageCustomersForm : AdminShellForm
    {
        public ManageCustomersForm()
            : base(AdminNavItem.Customers, "Manage Customers", "SmartMed - Manage Customers")
        {
        }

        protected override void InitializePageContent()
        {
            if (customersPanel != null)
                return;
            InitializeComponent();
        }
    }
}
