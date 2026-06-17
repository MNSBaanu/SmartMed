namespace SmartMed.UI
{
    public partial class ManageMedicinesForm : AdminShellForm
    {
        public ManageMedicinesForm()
            : base(AdminNavItem.Medicines, "Manage Medicines", "SmartMed - Manage Medicines")
        {
        }

        protected override void InitializePageContent()
        {
            if (medicinesPanel != null)
                return;
            InitializeComponent();
        }
    }
}
