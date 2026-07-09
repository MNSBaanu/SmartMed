using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    public sealed partial class CustomerHostForm : Form
    {
        public class CustomerNavItem
        {
            public string Key { get; }
            private CustomerNavItem(string key) => Key = key;
            public static readonly CustomerNavItem Home = new CustomerNavItem("Home");
            public static readonly CustomerNavItem Browse = new CustomerNavItem("Browse");
            public static readonly CustomerNavItem Cart = new CustomerNavItem("Cart");
            public static readonly CustomerNavItem Orders = new CustomerNavItem("Orders");
            public static readonly CustomerNavItem Profile = new CustomerNavItem("Profile");
        }

        private CustomerDashboardForm _home;
        private SearchMedicinesForm _browse;
        private PlaceOrderForm _cart;
        private TrackOrdersForm _orders;
        private ProfileManagementForm _profile;
        private CustomerNavItem _activeNav = CustomerNavItem.Home;
        private readonly Timer _clockTimer = new Timer { Interval = 30000 };
        private bool _chromeApplied;
        private bool _runtimeWired;

        public CustomerHostForm()
        {
            InitializeComponent();

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
                this, panelTitleBar, null, panelSidebar, panelContent, panelStatusBar);
            ApplySidebarChrome();
            ApplyWinControls();
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            SetActiveNav(CustomerNavItem.Home);
            ShowHome();

            _clockTimer.Tick += (s, e) => UpdateStatusTime();
            _clockTimer.Start();
            UpdateStatusTime();
        }

        private void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            SetActiveNav(CustomerNavItem.Home);

            var preview = new CustomerDashboardForm();
            HostPageHelper.ShowInPanel(panelContent, preview);
            lblTitleBar.Text = "SmartMed Health Portal - Home";
            Text = "SmartMed - Home";
        }

        internal void RefreshProfileDisplay()
        {
            _profile?.RefreshPage();
        }

        private void ApplySidebarChrome()
        {
            UiTheme.ConfigureTopNavigation(
                panelSidebar,
                null,
                panelSupport,
                null,
                btnNavHome,
                btnNavBrowse,
                btnNavCart,
                btnNavOrders,
                btnNavProfile,
                btnNavLogout);
            UiTheme.StyleCustomerSupportPanel(panelSupport, null, null, btnSupportContact, topNav: true);
            StyleNavButton(btnNavHome, _activeNav == CustomerNavItem.Home);
            StyleNavButton(btnNavBrowse, _activeNav == CustomerNavItem.Browse);
            StyleNavButton(btnNavCart, _activeNav == CustomerNavItem.Cart);
            StyleNavButton(btnNavOrders, _activeNav == CustomerNavItem.Orders);
            StyleNavButton(btnNavProfile, _activeNav == CustomerNavItem.Profile);
            StyleNavButton(btnNavLogout, false);
        }

        private void ApplyWinControls()
        {
            UiTheme.ArrangeWindowControls(panelWinControls, btnWinMinimize, btnWinMaximize, btnWinClose);
            UiTheme.StyleWindowControlButton(btnWinMinimize);
            UiTheme.StyleWindowControlButton(btnWinMaximize);
            UiTheme.StyleWindowControlButton(btnWinClose, isClose: true);
        }

        private void ShowPage(Form page, string title)
        {
            HostPageHelper.ShowInPanel(panelContent, page);
            lblTitleBar.Text = $"SmartMed Health Portal - {title}";
            Text = $"SmartMed - {title}";
        }

        private void ShowHome()
        {
            if (_home == null || _home.IsDisposed)
                _home = new CustomerDashboardForm();
            ShowPage(_home, "Home");
            _home.RefreshPage();
        }

        private void ShowBrowse()
        {
            if (_browse == null || _browse.IsDisposed)
                _browse = new SearchMedicinesForm();
            ShowPage(_browse, "Browse Medicine");
            _browse.RefreshPage();
        }

        private void ShowCart()
        {
            if (_cart == null || _cart.IsDisposed)
                _cart = new PlaceOrderForm();
            ShowPage(_cart, "My Cart");
            _cart.RefreshPage();
        }

        private void ShowOrders()
        {
            if (_orders == null || _orders.IsDisposed)
                _orders = new TrackOrdersForm();
            ShowPage(_orders, "My Orders");
            _orders.RefreshPage();
        }

        private void ShowProfile()
        {
            if (_profile == null || _profile.IsDisposed)
                _profile = new ProfileManagementForm();
            ShowPage(_profile, "My Profile");
            _profile.RefreshPage();
        }

        internal void SetActiveNav(CustomerNavItem nav)
        {
            _activeNav = nav;
            StyleNavButton(btnNavHome, nav == CustomerNavItem.Home);
            StyleNavButton(btnNavBrowse, nav == CustomerNavItem.Browse);
            StyleNavButton(btnNavCart, nav == CustomerNavItem.Cart);
            StyleNavButton(btnNavOrders, nav == CustomerNavItem.Orders);
            StyleNavButton(btnNavProfile, nav == CustomerNavItem.Profile);
        }

        internal void NavigateTo(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home) { SetActiveNav(nav); ShowHome(); }
            else if (nav == CustomerNavItem.Browse) { SetActiveNav(nav); ShowBrowse(); }
            else if (nav == CustomerNavItem.Cart) { SetActiveNav(nav); ShowCart(); }
            else if (nav == CustomerNavItem.Orders) { SetActiveNav(nav); ShowOrders(); }
            else if (nav == CustomerNavItem.Profile) { SetActiveNav(nav); ShowProfile(); }
        }

        private static void StyleNavButton(Button button, bool active)
        {
            if (button == null) return;
            UiTheme.StyleWinFormsNavButton(button, active);
        }

        private void UpdateStatusTime() =>
            lblStatusTime.Text = $"Local Time: {DateTime.Now:HH:mm}";

        private void BtnNavHome_Click(object sender, EventArgs e) => NavigateTo(CustomerNavItem.Home);
        private void BtnNavBrowse_Click(object sender, EventArgs e) => NavigateTo(CustomerNavItem.Browse);
        private void BtnNavCart_Click(object sender, EventArgs e) => NavigateTo(CustomerNavItem.Cart);
        private void BtnNavOrders_Click(object sender, EventArgs e) => NavigateTo(CustomerNavItem.Orders);
        private void BtnNavProfile_Click(object sender, EventArgs e) => NavigateTo(CustomerNavItem.Profile);
        private void BtnNavLogout_Click(object sender, EventArgs e) => Logout();

        private void BtnSupportContact_Click(object sender, EventArgs e)
        {
            SmartMedMessageBox.Show(
                "A member of our clinical team will contact you shortly.",
                "SmartMed Support",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnWinMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void BtnWinMaximize_Click(object sender, EventArgs e) =>
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
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
            _home?.Dispose();
            _browse?.Dispose();
            _cart?.Dispose();
            _orders?.Dispose();
            _profile?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
