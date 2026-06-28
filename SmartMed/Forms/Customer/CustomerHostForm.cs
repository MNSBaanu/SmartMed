using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class CustomerHostForm : Form
    {
        private CustomerDashboardForm _home;
        private SearchMedicinesForm _browse;
        private PlaceOrderForm _cart;
        private TrackOrdersForm _orders;
        private ProfileManagementForm _profile;
        private CustomerNavItem _activeNav = CustomerNavItem.Home;
        private readonly Timer _clockTimer = new Timer { Interval = 30000 };

        public CustomerHostForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UiTheme.ApplyFormFonts(this);
            UiTheme.ApplyAdminWinFormsShell(
                this, panelTitleBar, panelMenuBar, panelSidebar, panelContent, panelStatusBar);
            ApplyProfile();
            ApplyWinControls();
            WireMenuBar();
            SetActiveNav(CustomerNavItem.Home);
            ShowHome();

            _clockTimer.Tick += (s, e) => UpdateStatusTime();
            _clockTimer.Start();
            UpdateStatusTime();
        }

        internal void RefreshProfileDisplay()
        {
            lblProfileName.Text = Session.CurrentCustomer?.Name ?? "Customer";
            panelAvatar.Invalidate();
        }

        private void ApplyProfile()
        {
            var displayName = Session.CurrentCustomer?.Name ?? "Customer";
            lblProfileName.Text = displayName;
            lblProfileRole.Text = "Patient";

            panelAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(UiTheme.AdminTeal))
                    e.Graphics.FillEllipse(brush, 0, 0, panelAvatar.Width - 1, panelAvatar.Height - 1);
                var initial = displayName.Length > 0 ? displayName.Substring(0, 1).ToUpperInvariant() : "C";
                TextRenderer.DrawText(e.Graphics, initial, UiTheme.UiFontBold, panelAvatar.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
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
                    lbl.Click += (s, e) => { };
            }
        }

        private void ShowPage(Control page, string title)
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);
            page.Dock = DockStyle.Fill;
            panelContent.Controls.Add(page);
            lblTitleBar.Text = $"SmartMed Customer Portal - {title}";
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
            ShowPage(_browse, "Browse Medicines");
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
        private void BtnNavExit_Click(object sender, EventArgs e) => Logout();
        private void BtnWinMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;
        private void BtnWinMaximize_Click(object sender, EventArgs e) =>
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        private void BtnWinClose_Click(object sender, EventArgs e) => Logout();

        private void Logout()
        {
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
