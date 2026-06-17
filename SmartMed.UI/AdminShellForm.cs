using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public partial class AdminShellForm : Form
    {
        protected AdminShellForm(AdminNavItem activeNav, string subtitle, string windowTitle)
        {
            InitializeComponent();
            DoubleBuffered = true;
            Text = windowTitle;
            lblTopSubtitle.Text = subtitle;
            ApplyShellTheme();
            SetActiveNav(activeNav);
        }

        protected void ApplyShellTheme()
        {
            ThemeApplier.ApplyAdminShell(this, panelTop, lblTopTitle, lblTopSubtitle, btnClose, panelSidebar, panelContent);
            lblNavBrand.Font = AppTheme.AppTitleFont;
            lblNavBrand.ForeColor = AppTheme.Primary;
            lblNavTagline.Font = AppTheme.LabelFont;
            lblNavTagline.ForeColor = AppTheme.OnSurfaceVariant;
        }

        protected void SetActiveNav(AdminNavItem active)
        {
            ThemeApplier.ApplyNavButton(btnNavOverview, active == AdminNavItem.Overview);
            ThemeApplier.ApplyNavButton(btnNavMedicines, active == AdminNavItem.Medicines);
            ThemeApplier.ApplyNavButton(btnNavCustomers, active == AdminNavItem.Customers);
            ThemeApplier.ApplyNavButton(btnNavOrders, active == AdminNavItem.Orders);
            ThemeApplier.ApplyNavButton(btnNavReports, active == AdminNavItem.Reports);
            ThemeApplier.ApplyNavButton(btnNavLogout, isLogout: true);
        }

        protected void NavigateTo(AdminShellForm next)
        {
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.WindowState = WindowState;
            next.Show();
            Hide();
            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e) => Logout();

        private void BtnNavLogout_Click(object sender, EventArgs e) => Logout();

        protected void Logout()
        {
            Session.Clear();
            Close();
        }

        protected void ShowComingSoon(string feature)
        {
            MessageBox.Show($"{feature} will be available in the next update.", "SmartMed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnNavOverview_Click(object sender, EventArgs e)
        {
            if (this is AdminDashboardForm dashboard)
            {
                dashboard.RefreshData();
                return;
            }
            NavigateTo(new AdminDashboardForm());
        }

        private void BtnNavMedicines_Click(object sender, EventArgs e)
        {
            if (this is ManageMedicinesForm) return;
            NavigateTo(new ManageMedicinesForm());
        }

        private void BtnNavCustomers_Click(object sender, EventArgs e) => ShowComingSoon("Manage Customers");
        private void BtnNavOrders_Click(object sender, EventArgs e) => ShowComingSoon("Manage Orders");
        private void BtnNavReports_Click(object sender, EventArgs e) => ShowComingSoon("Generate Reports");
    }
}
