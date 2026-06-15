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
            if (!UiFactory.IsDesignMode(this))
                UiFactory.ApplyFullScreen(this);
            FontManager.ApplyInterFont(this);
        }

        private void AdminDashboardForm_Load(object sender, EventArgs e)
        {
            if (UiFactory.IsDesignMode(this)) return;

            var welcome = Session.CurrentAdmin?.Username ?? "Admin";
            lblWelcome.Text = $"Welcome, {welcome}";
            lblHeaderSubtitle.Text = $"Admin Dashboard  |  {welcome}";
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
            UiFactory.NavigateTo(contentHost, view, active, btnOverview, btnMedicines, btnCustomers, btnOrders, btnReports);
        }
    }
}
