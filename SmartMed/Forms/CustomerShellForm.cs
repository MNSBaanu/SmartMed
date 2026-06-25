using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ReaLTaiizor.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    [DesignerCategory("Form")]
    public partial class CustomerShellForm : MaterialForm
    {
        private bool _pageContentInitialized;
        private readonly bool _isEmbeddedPage;

        private static readonly Lazy<bool> IsDesignToolsProcess = new Lazy<bool>(() =>
        {
            var name = Process.GetCurrentProcess().ProcessName;
            return name.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("DesignToolsServer", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("XDesProc", StringComparison.OrdinalIgnoreCase) >= 0;
        });

        public CustomerShellForm()
        {
            InitializeComponent();
            if (IsDesignHost())
            {
                SetActiveNav(CustomerNavItem.Home);
                SyncShellChrome();
            }
        }

        protected CustomerShellForm(CustomerNavItem activeNav, string subtitle, bool embeddedPage = false)
            : this()
        {
            _isEmbeddedPage = embeddedPage;
            DoubleBuffered = true;
            Text = "SmartMed Customer Portal";
            lblTopSubtitle.Text = subtitle;
            SetActiveNav(activeNav);
            if (!IsDesignHost() && !embeddedPage)
                UiTheme.ApplyShell(this, panelTop, panelSidebar, panelContent);
            SyncShellChrome();
            if (IsDesignHost())
                EnsurePageContent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!_isEmbeddedPage)
                EnsurePageContent();
        }

        protected void EnsurePageContent()
        {
            if (_pageContentInitialized) return;
            _pageContentInitialized = true;

            if (IsDesignHost())
            {
                InitializePageContent();
                SyncShellChrome();
                return;
            }

            if (_isEmbeddedPage)
            {
                InitializePageContent();
                if (PagePanel != null)
                    UiTheme.ApplyFontTree(PagePanel);
                return;
            }

            using (UiTheme.BatchUpdate(this, panelTop, panelSidebar, PagePanel))
            {
                if (PagePanel != null)
                    PagePanel.Visible = false;

                InitializePageContent();

                UiTheme.ApplyFontTree(panelTop);
                UiTheme.ApplyFontTree(panelSidebar);
                UiTheme.ApplyFontTree(PagePanel);

                if (PagePanel != null)
                    PagePanel.Visible = true;
            }

            SyncShellChrome();
        }

        protected virtual void InitializePageContent()
        {
        }

        protected bool IsDesignHost()
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;
            if (Site?.DesignMode == true)
                return true;
            return IsDesignToolsProcess.Value;
        }

        private Panel _contentTarget;
        private CustomerHostForm _customerHost;

        internal void SetContentTarget(Panel host)
        {
            _contentTarget = host;
            _customerHost = host?.FindForm() as CustomerHostForm;
        }

        protected Panel PagePanel => _contentTarget ?? panelContent;

        protected int GetScrollContentWidth(int fallback = 800)
        {
            var w = PagePanel.ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            return w < 200 ? fallback : w;
        }

        protected void WireScrollRoot(Control scrollRoot, int fallback = 800, int minHeight = 0)
        {
            UiTheme.EnableDoubleBuffer(scrollRoot);
            if (scrollRoot is Panel panel)
            {
                panel.AutoSize = true;
                panel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }

            var host = PagePanel;
            scrollRoot.Width = GetScrollContentWidth(fallback);
            if (minHeight > 0)
                scrollRoot.MinimumSize = new Size(0, minHeight);

            host.Controls.Add(scrollRoot);
            host.Resize += (s, e) => scrollRoot.Width = GetScrollContentWidth(fallback);
            scrollRoot.PerformLayout();
        }

        protected T GetRuntimeService<T>(ref T service) where T : class, new()
        {
            if (IsDesignHost()) return null;
            return service ?? (service = new T());
        }

        protected void SetActiveNav(CustomerNavItem active)
        {
            if (btnNavHome == null) return;
            StyleNavButton(btnNavHome, active == CustomerNavItem.Home);
            StyleNavButton(btnNavBrowse, active == CustomerNavItem.Browse);
            StyleNavButton(btnNavCart, active == CustomerNavItem.Cart);
            StyleNavButton(btnNavOrders, active == CustomerNavItem.Orders);
            StyleNavButton(btnNavProfile, active == CustomerNavItem.Profile);
        }

        private void StyleNavButton(Button button, bool active)
        {
            if (button == null) return;
            if (active)
            {
                button.BackColor = UiTheme.PrimaryDark;
                button.ForeColor = Color.White;
                button.Font = UiTheme.UiFontBold;
            }
            else
            {
                UiTheme.StyleNavButton(button);
            }
        }

        protected void SyncShellChrome()
        {
            if (panelTop == null || !panelTop.Visible) return;

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

        protected void HideTopChrome()
        {
            if (panelTop == null) return;
            panelTop.Visible = false;
            panelTop.Height = 0;
            if (btnClose != null)
                btnClose.Visible = false;
        }

        internal void PrepareForNavigation()
        {
            if (!IsHandleCreated)
                CreateControl();
            EnsurePageContent();
            PerformLayout();
        }

        protected CustomerHostForm GetCustomerHost()
        {
            if (_customerHost != null && !_customerHost.IsDisposed)
                return _customerHost;

            for (var parent = Parent; parent != null; parent = parent.Parent)
            {
                if (parent is CustomerHostForm host)
                    return host;
            }
            return this as CustomerHostForm;
        }

        protected void GoToCustomerSection(CustomerNavItem nav)
        {
            var host = GetCustomerHost();
            if (host != null)
                host.NavigateCustomer(nav);
            else
                NavigateCustomer(nav);
        }

        protected virtual void NavigateCustomer(CustomerNavItem nav)
        {
            if (IsCurrentPage(nav))
            {
                RefreshCurrentPage(nav);
                return;
            }
            NavigateTo(CreatePageForm(nav));
        }

        private bool IsCurrentPage(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home && this is CustomerDashboardForm) return true;
            if (nav == CustomerNavItem.Browse && this is SearchMedicinesForm) return true;
            if (nav == CustomerNavItem.Cart && this is PlaceOrderForm) return true;
            if (nav == CustomerNavItem.Orders && this is TrackOrdersForm) return true;
            if (nav == CustomerNavItem.Profile && this is ProfileManagementForm) return true;
            return false;
        }

        private void RefreshCurrentPage(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home && this is CustomerDashboardForm dashboard)
                dashboard.RefreshData();
            else if (nav == CustomerNavItem.Cart && this is PlaceOrderForm cart)
                cart.RefreshCart();
            else if (nav == CustomerNavItem.Orders && this is TrackOrdersForm orders)
                orders.RefreshOrders();
            else if (nav == CustomerNavItem.Profile && this is ProfileManagementForm profile)
                profile.RefreshProfile();
        }

        private static CustomerShellForm CreatePageForm(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home) return new CustomerDashboardForm();
            if (nav == CustomerNavItem.Browse) return new SearchMedicinesForm();
            if (nav == CustomerNavItem.Cart) return new PlaceOrderForm();
            if (nav == CustomerNavItem.Orders) return new TrackOrdersForm();
            if (nav == CustomerNavItem.Profile) return new ProfileManagementForm();
            throw new ArgumentException("Unknown customer section.");
        }

        protected void NavigateTo(CustomerShellForm next)
        {
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.WindowState = WindowState;

            next.PrepareForNavigation();

            SmartMedApplicationContext.Current?.HandoffMainForm(next);

            Hide();
            UiTheme.RevealForm(next);
            Close();
        }

        private void BtnClose_Click(object sender, EventArgs e) => ExitApplication();

        private void BtnNavLogout_Click(object sender, EventArgs e) => Logout();

        protected void ExitApplication()
        {
            if (_isEmbeddedPage)
            {
                GetCustomerHost()?.ExitApplication();
                return;
            }

            CartService.Clear();
            Session.Clear();
            Close();
        }

        protected void Logout()
        {
            if (_isEmbeddedPage)
            {
                GetCustomerHost()?.Logout();
                return;
            }

            SmartMedApplicationContext.Current?.ShowLoginAfterLogout();
        }

        private void BtnNavHome_Click(object sender, EventArgs e) => NavigateCustomer(CustomerNavItem.Home);

        private void BtnNavBrowse_Click(object sender, EventArgs e) => NavigateCustomer(CustomerNavItem.Browse);

        private void BtnNavCart_Click(object sender, EventArgs e) => NavigateCustomer(CustomerNavItem.Cart);

        private void BtnNavOrders_Click(object sender, EventArgs e) => NavigateCustomer(CustomerNavItem.Orders);

        private void BtnNavProfile_Click(object sender, EventArgs e) => NavigateCustomer(CustomerNavItem.Profile);
    }
}
