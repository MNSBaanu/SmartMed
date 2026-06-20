using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    [DesignerCategory("Form")]
    public partial class AdminShellForm : Form
    {
        public AdminShellForm()
        {
            FontManager.Initialize();
            InitializeComponent();
            if (IsDesignTime)
            {
                ApplyShellTheme();
                SetActiveNav(AdminNavItem.Overview);
                SyncShellChrome();
            }
        }

        protected AdminShellForm(AdminNavItem activeNav, string subtitle, string windowTitle)
            : this()
        {
            DoubleBuffered = true;
            Text = windowTitle;
            lblTopSubtitle.Text = subtitle;
            ApplyShellTheme();
            SetActiveNav(activeNav);
            InitializePageContent();
            SyncShellChrome();
        }

        protected virtual void InitializePageContent()
        {
            // Derived admin forms override this and call their Designer InitializeComponent()
            // so panelContent is populated for both runtime and the Visual Studio designer.
        }

        protected static bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

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

        /// <summary>Keep anchored chrome aligned with shell panels in designer and at runtime.</summary>
        protected void SyncShellChrome()
        {
            var closeLeft = Math.Max(8, panelTop.ClientSize.Width - btnClose.Width - 8);
            if (btnClose.Left != closeLeft)
                btnClose.Left = closeLeft;

            var logoutTop = Math.Max(0, panelSidebar.ClientSize.Height - btnNavLogout.Height - panelSidebar.Padding.Bottom);
            if (btnNavLogout.Top != logoutTop)
                btnNavLogout.Top = logoutTop;
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (panelTop != null && panelSidebar != null && btnClose != null && btnNavLogout != null)
                SyncShellChrome();
        }

        protected void NavigateTo(AdminShellForm next)
        {
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.WindowState = WindowState;

            var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            login?.AttachAdminReturn(next);

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
            var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            if (login != null && !login.IsDisposed)
                login.Show();
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

        private void BtnNavCustomers_Click(object sender, EventArgs e)
        {
            if (this is ManageCustomersForm) return;
            NavigateTo(new ManageCustomersForm());
        }
        private void BtnNavOrders_Click(object sender, EventArgs e)
        {
            if (this is ManageOrdersForm) return;
            NavigateTo(new ManageOrdersForm());
        }
        private void BtnNavReports_Click(object sender, EventArgs e)
        {
            if (this is ReportsForm reports)
            {
                reports.RefreshReports();
                return;
            }
            NavigateTo(new ReportsForm());
        }
    }
}
