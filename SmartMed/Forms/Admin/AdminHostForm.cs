using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class AdminHostForm : Form
    {
        public class AdminNavItem
        {
            public string Key { get; }
            private AdminNavItem(string key) => Key = key;
            public static readonly AdminNavItem Overview = new AdminNavItem("Overview");
            public static readonly AdminNavItem Medicines = new AdminNavItem("Medicines");
            public static readonly AdminNavItem Customers = new AdminNavItem("Customers");
            public static readonly AdminNavItem Orders = new AdminNavItem("Orders");
            public static readonly AdminNavItem Reports = new AdminNavItem("Reports");
        }

        private AdminDashboardForm _dashboard;
        private ManageMedicinesForm _medicinesPage;
        private ManageCustomersForm _customersPage;
        private ManageOrdersForm _ordersPage;
        private ReportsForm _reportsPage;
        private AdminNavItem _activeNav = AdminNavItem.Overview;
        private readonly Timer _clockTimer = new Timer { Interval = 30000 };
        private bool _chromeApplied;
        private bool _runtimeWired;
        private bool _avatarWired;
        private SmartMed.UI.NavButton btnNavLogout;

        public AdminHostForm()
        {
            InitializeComponent();

            btnNavLogout = new SmartMed.UI.NavButton
            {
                Text = "LOGOUT",
                Name = "btnNavLogout",
                Cursor = Cursors.Hand,
                Font = new Font(SystemFonts.DefaultFont.FontFamily, 11F, FontStyle.Bold)
            };
            btnNavLogout.Click += (s, e) => Logout();
            panelSidebar.Controls.Add(btnNavLogout);

            DoubleBuffered = true;
            ApplyViewChrome();

            if (DesignHostHelper.IsDesignHost(this))
            {
                LoadDesignTimePreview();
                return;
            }

            WireRuntimeBehavior();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            UiTheme.ApplyFormFonts(this);
            UiTheme.ApplyAdminWinFormsShell(
                this, panelTitleBar, panelMenuBar, panelSidebar, panelContent, panelStatusBar);
            panelMenuBar.Visible = false;
            ApplySidebarChrome();
            ApplyWinControls();
            ApplyProfileDisplay();
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            WireProfileActions();
            WireMenuBar();

            SetActiveNav(AdminNavItem.Overview);
            ShowDashboard();

            _clockTimer.Tick += (s, e) => UpdateStatusTime();
            _clockTimer.Start();
            UpdateStatusTime();
        }

        private void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            SetActiveNav(AdminNavItem.Overview);

            var preview = new AdminDashboardForm();
            HostPageHelper.ShowInPanel(panelContent, preview);
            UpdateTitleBar("Operational Dashboard");
        }

        private void ApplySidebarChrome()
        {
            UiTheme.ConfigureTopNavigation(
                panelSidebar,
                panelBrand,
                panelProfile,
                panelNavSpacer,
                btnNavDashboard,
                btnNavMedicines,
                btnNavCustomers,
                btnNavOrders,
                btnNavReports,
                btnNavLogout);
            UiTheme.StyleSidebarBrand(panelBrand, panelBrandIcon, lblBrandTitle, lblBrandSubtitle, customerPortal: false, topNav: true);
            panelBrand.Visible = false;
            UiTheme.StyleSidebarProfileFooter(panelProfile, panelAvatar, lblProfileName, lblProfileRole, topNav: true);
            StyleNavButton(btnNavDashboard, _activeNav == AdminNavItem.Overview);
            StyleNavButton(btnNavMedicines, _activeNav == AdminNavItem.Medicines);
            StyleNavButton(btnNavCustomers, _activeNav == AdminNavItem.Customers);
            StyleNavButton(btnNavOrders, _activeNav == AdminNavItem.Orders);
            StyleNavButton(btnNavReports, _activeNav == AdminNavItem.Reports);
            StyleNavButton(btnNavLogout, false);
            btnNavLogout.Font = new Font(SystemFonts.DefaultFont.FontFamily, 11F, FontStyle.Bold);
        }

        private void ApplyProfileDisplay()
        {
            var displayName = DesignHostHelper.IsDesignHost(this)
                ? "Administrator"
                : (Session.CurrentAdmin?.Username ?? "Administrator");
            lblProfileName.Text = displayName.ToUpperInvariant();
            lblProfileRole.Text = "SYSTEM ADMIN";
            WireProfileAvatar();
        }

        private void WireProfileAvatar()
        {
            if (_avatarWired) return;
            _avatarWired = true;

            panelAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(UiTheme.AdminTeal))
                    e.Graphics.FillEllipse(brush, 0, 0, panelAvatar.Width - 1, panelAvatar.Height - 1);
                var name = lblProfileName.Text ?? string.Empty;
                var initial = name.Length > 0 ? name.Substring(0, 1).ToUpperInvariant() : "A";
                TextRenderer.DrawText(e.Graphics, initial, UiTheme.UiFontBold, panelAvatar.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private void WireProfileActions()
        {
            panelProfile.Cursor = Cursors.Hand;
            panelProfile.Click += (s, e) => ShowChangePasswordDialog();
            lblProfileName.Cursor = Cursors.Hand;
            lblProfileName.Click += (s, e) => ShowChangePasswordDialog();
            lblProfileRole.Cursor = Cursors.Hand;
            lblProfileRole.Click += (s, e) => ShowChangePasswordDialog();
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
            if (_dashboard == null || _dashboard.IsDisposed)
                _dashboard = new AdminDashboardForm();

            HostPageHelper.ShowInPanel(panelContent, _dashboard);
            _dashboard.RefreshData();
            UpdateTitleBar("Operational Dashboard");
        }

        private void ShowMedicines()
        {
            if (_medicinesPage == null || _medicinesPage.IsDisposed)
                _medicinesPage = new ManageMedicinesForm();

            HostPageHelper.ShowInPanel(panelContent, _medicinesPage);
            _medicinesPage.RefreshPage();
            UpdateTitleBar("Manage Medicines");
        }

        private void ShowCustomers()
        {
            if (_customersPage == null || _customersPage.IsDisposed)
                _customersPage = new ManageCustomersForm();

            HostPageHelper.ShowInPanel(panelContent, _customersPage);
            _customersPage.RefreshPage();
            UpdateTitleBar("Manage Customers");
        }

        private void ShowOrders()
        {
            if (_ordersPage == null || _ordersPage.IsDisposed)
                _ordersPage = new ManageOrdersForm();

            HostPageHelper.ShowInPanel(panelContent, _ordersPage);
            _ordersPage.RefreshPage();
            UpdateTitleBar("Manage Orders");
        }

        private void ShowReports()
        {
            if (_reportsPage == null || _reportsPage.IsDisposed)
                _reportsPage = new ReportsForm();

            HostPageHelper.ShowInPanel(panelContent, _reportsPage);
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
            SmartMedMessageBox.Show(
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

        private void BtnWinClose_Click(object sender, EventArgs e)
        {
            _clockTimer.Stop();
            Close();
        }

        private void Logout()
        {
            if (SmartMedMessageBox.Show("Are you sure you want to log out?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

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
