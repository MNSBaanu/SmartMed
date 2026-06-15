using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.UI.Theming;
using SmartMed.UI.Views;

namespace SmartMed.UI
{
    public class AdminDashboardForm : Form
    {
        private Panel contentHost;
        private Button btnOverview, btnMedicines, btnCustomers, btnOrders, btnReports;

        public AdminDashboardForm()
        {
            UiFactory.ApplyFullScreen(this);
            Text = "SmartMed - Admin Dashboard";

            var welcome = Session.CurrentAdmin?.Username ?? "Admin";
            var header = UiFactory.CreateAppHeader("SmartMed Pharmacy", "Admin Dashboard  |  " + welcome, Logout);
            Controls.Add(header);

            var main = new Panel { Dock = DockStyle.Fill };
            Controls.Add(main);

            var sidebar = UiFactory.CreateSidebar();
            main.Controls.Add(sidebar);

            var lblWelcome = new Label
            {
                Text = $"Welcome, {welcome}",
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.StackMd),
                Width = ClinicalPrecisionTheme.NavWidth - ClinicalPrecisionTheme.ContainerPadding * 2
            };
            sidebar.Controls.Add(lblWelcome);

            int navY = 50;
            btnOverview = UiFactory.CreateNavButton("Dashboard Overview", null);
            btnMedicines = UiFactory.CreateNavButton("Manage Medicines", null);
            btnCustomers = UiFactory.CreateNavButton("Manage Customers", null);
            btnOrders = UiFactory.CreateNavButton("Manage Orders", null);
            btnReports = UiFactory.CreateNavButton("Generate Reports", null);

            btnOverview.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnMedicines.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnCustomers.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnOrders.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnReports.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);

            btnOverview.Click += (s, e) => Navigate(new AdminOverviewView(), btnOverview);
            btnMedicines.Click += (s, e) => Navigate(new MedicineManagementView(), btnMedicines);
            btnCustomers.Click += (s, e) => Navigate(new CustomerManagementView(), btnCustomers);
            btnOrders.Click += (s, e) => Navigate(new OrderManagementView(), btnOrders);
            btnReports.Click += (s, e) => Navigate(new ReportsView(), btnReports);

            sidebar.Controls.AddRange(new Control[] { btnOverview, btnMedicines, btnCustomers, btnOrders, btnReports });

            contentHost = UiFactory.CreateContentHost();
            main.Controls.Add(contentHost);

            Navigate(new AdminOverviewView(), btnOverview);
        }

        private void Navigate(UserControl view, Button active)
        {
            UiFactory.NavigateTo(contentHost, view, active, btnOverview, btnMedicines, btnCustomers, btnOrders, btnReports);
        }

        private void Logout()
        {
            Session.Clear();
            Close();
            new LoginForm().Show();
        }
    }
}
