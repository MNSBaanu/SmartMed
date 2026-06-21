using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    [DesignerCategory("Form")]
    public partial class AdminShellForm : Form
    {
        private bool _pageContentInitialized;

        private static readonly Lazy<bool> IsDesignToolsProcess = new Lazy<bool>(() =>
        {
            var name = Process.GetCurrentProcess().ProcessName;
            return name.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("DesignToolsServer", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("XDesProc", StringComparison.OrdinalIgnoreCase) >= 0;
        });

        public AdminShellForm()
        {
            InitializeComponent();
            if (IsDesignHost()) { SetActiveNav(AdminNavItem.Overview); SyncShellChrome(); }
        }

        protected AdminShellForm(AdminNavItem activeNav, string subtitle)
            : this()
        {
            DoubleBuffered = true;
            Text = "Pharmacy Management System";
            lblTopSubtitle.Text = subtitle;
            SetActiveNav(activeNav);
            SyncShellChrome();
            if (IsDesignHost())
                EnsurePageContent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsurePageContent();
        }

        protected void EnsurePageContent()
        {
            if (_pageContentInitialized) return;
            _pageContentInitialized = true;
            InitializePageContent();
            SyncShellChrome();
        }

        protected virtual void InitializePageContent()
        {
            // Derived admin forms override this to build panelContent at Load time
            // so DesignMode and LicenseUsageMode are reliable in the VS designer.
        }

        protected static bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        /// <summary>True in the VS WinForms designer (not only at ctor time).</summary>
        protected bool IsDesignHost()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;
            if (Site?.DesignMode == true)
                return true;
            return IsDesignToolsProcess.Value;
        }
        protected void SetActiveNav(AdminNavItem active)
        {
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
