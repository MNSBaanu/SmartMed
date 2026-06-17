namespace SmartMed.UI
{
    public partial class ManageMedicinesForm : AdminShellForm
    {
        public ManageMedicinesForm()
            : base(AdminNavItem.Medicines, "Manage Medicines", "SmartMed - Manage Medicines")
        {
            InitializeContentPanel();
        }
    }
}
