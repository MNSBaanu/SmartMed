namespace SmartMed.UI
{
    public partial class ManageMedicinesForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ManageMedicinesForm()
            : base(AdminNavItem.Medicines, "Manage Medicines", "SmartMed - Manage Medicines")
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignTime)
                LoadMedicines();
        }
    }
}
