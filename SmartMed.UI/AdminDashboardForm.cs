using System;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.UI.Theming;
using SmartMed.UI.Views;

namespace SmartMed.UI
{
    public partial class AdminDashboardForm : Form
    {
        public AdminDashboardForm()
        {
            InitializeComponent();
            ApplyStitchShell();
            UiFactory.ApplyFormDefaults(this);
            if (!UiFactory.IsDesignMode(this))
                UiFactory.ApplyFullScreen(this);
        }

        private void ApplyStitchShell()
        {
            StitchUiHelper.ApplyAdminShell(this, headerPanel, lblHeaderTitle, txtGlobalSearch,
                sidebarPanel, lblBrandTitle, lblBrandSubtitle, btnLogout);

            StitchUiHelper.StyleNavButton(btnOverview, StitchUiHelper.NavIcon.Dashboard, "Dashboard Overview", true);
            StitchUiHelper.StyleNavButton(btnMedicines, StitchUiHelper.NavIcon.Medicines, "Manage Medicines", false);
            StitchUiHelper.StyleNavButton(btnCustomers, StitchUiHelper.NavIcon.Customers, "Manage Customers", false);
            StitchUiHelper.StyleNavButton(btnOrders, StitchUiHelper.NavIcon.Orders, "Manage Orders", false);
            StitchUiHelper.StyleNavButton(btnReports, StitchUiHelper.NavIcon.Reports, "Generate Reports", false);

            btnOverview.Location = new System.Drawing.Point(24, 88);
            btnMedicines.Location = new System.Drawing.Point(24, 132);
            btnCustomers.Location = new System.Drawing.Point(24, 176);
            btnOrders.Location = new System.Drawing.Point(24, 220);
            btnReports.Location = new System.Drawing.Point(24, 264);
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            if (UiFactory.IsDesignMode(this)) return;
            Navigate(new AdminOverviewView(), btnOverview);
        }

        private void BtnOverview_Click(object sender, EventArgs e) => Navigate(new AdminOverviewView(), btnOverview);
        private void BtnMedicines_Click(object sender, EventArgs e) => Navigate(new MedicineManagementView(), btnMedicines);
        private void BtnCustomers_Click(object sender, EventArgs e) => Navigate(new CustomerManagementView(), btnCustomers);
        private void BtnOrders_Click(object sender, EventArgs e) => Navigate(new OrderManagementView(), btnOrders);
        private void BtnReports_Click(object sender, EventArgs e) => Navigate(new ReportsView(), btnReports);

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Close();
            new LoginForm().Show();
        }

        private void Navigate(UserControl view, Button active)
        {
            UiFactory.NavigateTo(contentHost, view, active, false, btnOverview, btnMedicines, btnCustomers, btnOrders, btnReports);
        }
    }
}
