using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;
using SmartMed.UI;

namespace SmartMed.UI
{
    public sealed partial class AdminHostForm : Form
    {
        private AdminDashboardForm _dashboard;
        private ManageMedicinesForm _medicinesPage;
        private ManageCustomersForm _customersPage;
        private ManageOrdersForm _ordersPage;
        private ReportsForm _reportsPage;
        private AdminNavItem _activeNav = AdminNavItem.Overview;
        private readonly Timer _clockTimer = new Timer { Interval = 30000 };

        public AdminHostForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UiTheme.ApplyFormFonts(this);
            UiTheme.ApplyAdminWinFormsShell(
                this, panelTitleBar, panelMenuBar, panelSidebar, panelContent, panelStatusBar);
            ApplyProfile();
            ApplySidebarChrome();
            ApplyWinControls();
            WireMenuBar();
            SetActiveNav(AdminNavItem.Overview);
            ShowDashboard();

            _clockTimer.Tick += (s, e) => UpdateStatusTime();
            _clockTimer.Start();
            UpdateStatusTime();
        }

        private void ApplySidebarChrome()
        {
            UiTheme.StyleSidebarBrand(panelBrand, panelBrandIcon, lblBrandTitle, lblBrandSubtitle, customerPortal: false);
            UiTheme.StyleSidebarProfileFooter(panelProfile, panelAvatar, lblProfileName, lblProfileRole);
        }

        private void ApplyProfile()
        {
            var displayName = Session.CurrentAdmin?.Username ?? "Administrator";
            lblProfileName.Text = displayName;
            lblProfileRole.Text = "System Admin";

            panelProfile.Cursor = Cursors.Hand;
            panelProfile.Click += (s, e) => ShowChangePasswordDialog();
            lblProfileName.Cursor = Cursors.Hand;
            lblProfileName.Click += (s, e) => ShowChangePasswordDialog();
            lblProfileRole.Cursor = Cursors.Hand;
            lblProfileRole.Click += (s, e) => ShowChangePasswordDialog();

            panelAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(UiTheme.AdminTeal))
                    e.Graphics.FillEllipse(brush, 0, 0, panelAvatar.Width - 1, panelAvatar.Height - 1);
                var initial = displayName.Length > 0 ? displayName.Substring(0, 1).ToUpperInvariant() : "A";
                TextRenderer.DrawText(e.Graphics, initial, UiTheme.UiFontBold, panelAvatar.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private void ShowChangePasswordDialog()
        {
            using (var dlg = new ChangePasswordForm(isAdmin: true))
                dlg.ShowDialog(this);
        }

        private void ApplyWinControls()
        {
            UiTheme.ArrangeWindowControls(panelWinControls, btnWinMinimize, btnWinMaximize, btnWinClose);
            UiTheme.StyleWindowControlButton(btnWinMinimize);
            UiTheme.StyleWindowControlButton(btnWinMaximize);
            UiTheme.StyleWindowControlButton(btnWinClose, isClose: true);
        }

        private void WireMenuBar()
        {
            foreach (Control c in panelMenuBar.Controls)
            {
                if (c is Label lbl)
                    lbl.Click += (s, e) => ShowComingSoon();
            }
        }

        private void ShowDashboard()
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);

            if (_dashboard == null || _dashboard.IsDisposed)
                _dashboard = new AdminDashboardForm();

            _dashboard.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_dashboard);
            _dashboard.RefreshData();
            UpdateTitleBar("Operational Dashboard");
        }

        private void ShowMedicines()
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);

            if (_medicinesPage == null || _medicinesPage.IsDisposed)
                _medicinesPage = new ManageMedicinesForm();

            _medicinesPage.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_medicinesPage);
            _medicinesPage.RefreshPage();
            UpdateTitleBar("Manage Medicines");
        }

        private void ShowCustomers()
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);

            if (_customersPage == null || _customersPage.IsDisposed)
                _customersPage = new ManageCustomersForm();

            _customersPage.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_customersPage);
            _customersPage.RefreshPage();
            UpdateTitleBar("Manage Customers");
        }

        private void ShowOrders()
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);

            if (_ordersPage == null || _ordersPage.IsDisposed)
                _ordersPage = new ManageOrdersForm();

            _ordersPage.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_ordersPage);
            _ordersPage.RefreshPage();
            UpdateTitleBar("Manage Orders");
        }

        private void ShowReports()
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);

            if (_reportsPage == null || _reportsPage.IsDisposed)
                _reportsPage = new ReportsForm();

            _reportsPage.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_reportsPage);
            _reportsPage.RefreshPage();
            UpdateTitleBar("Generate Reports");
        }

        private void UpdateTitleBar(string section)
        {
            lblTitleBar.Text = $"SmartMed Clinical Management - {section}";
            Text = section == "Operational Dashboard"
                ? "SmartMed - Operational Dashboard"
                : $"SmartMed - {section}";
        }

        internal void SetActiveNav(AdminNavItem nav)
        {
            _activeNav = nav;
            StyleNavButton(btnNavDashboard, nav == AdminNavItem.Overview);
            StyleNavButton(btnNavMedicines, nav == AdminNavItem.Medicines);
            StyleNavButton(btnNavCustomers, nav == AdminNavItem.Customers);
            StyleNavButton(btnNavOrders, nav == AdminNavItem.Orders);
            StyleNavButton(btnNavReports, nav == AdminNavItem.Reports);
        }

        internal void NavigateTo(AdminNavItem nav)
        {
            if (nav == AdminNavItem.Overview) { SetActiveNav(nav); ShowDashboard(); }
            else if (nav == AdminNavItem.Medicines) { SetActiveNav(nav); ShowMedicines(); }
            else if (nav == AdminNavItem.Customers) { SetActiveNav(nav); ShowCustomers(); }
            else if (nav == AdminNavItem.Orders) { SetActiveNav(nav); ShowOrders(); }
            else if (nav == AdminNavItem.Reports) { SetActiveNav(nav); ShowReports(); }
        }

        private static void StyleNavButton(Button button, bool active)
        {
            if (button == null) return;
            UiTheme.StyleWinFormsNavButton(button, active);
        }

        private void UpdateStatusTime()
        {
            lblStatusTime.Text = $"Local Time: {DateTime.Now:HH:mm}";
        }

        private void BtnNavDashboard_Click(object sender, EventArgs e)
        {
            SetActiveNav(AdminNavItem.Overview);
            ShowDashboard();
        }

        private void BtnNavMedicines_Click(object sender, EventArgs e)
        {
            SetActiveNav(AdminNavItem.Medicines);
            ShowMedicines();
        }

        private void BtnNavCustomers_Click(object sender, EventArgs e)
        {
            SetActiveNav(AdminNavItem.Customers);
            ShowCustomers();
        }

        private void BtnNavOrders_Click(object sender, EventArgs e)
        {
            SetActiveNav(AdminNavItem.Orders);
            ShowOrders();
        }

        private void BtnNavReports_Click(object sender, EventArgs e)
        {
            SetActiveNav(AdminNavItem.Reports);
            ShowReports();
        }

        private static void ShowComingSoon()
        {
            MessageBox.Show(
                "This section is coming soon.",
                "SmartMed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnWinMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void BtnWinMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        private void BtnWinClose_Click(object sender, EventArgs e) => Logout();

        private void Logout()
        {
            _clockTimer.Stop();
            SmartMedApplicationContext.Current?.ShowLoginAfterLogout();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _clockTimer.Dispose();
            _dashboard?.Dispose();
            _medicinesPage?.Dispose();
            _customersPage?.Dispose();
            _ordersPage?.Dispose();
            _reportsPage?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
