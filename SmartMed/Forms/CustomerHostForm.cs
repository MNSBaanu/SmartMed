using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartMed.UI
{
    /// <summary>Single customer window — swaps only panelContent when navigating.</summary>
    public sealed partial class CustomerHostForm : CustomerShellForm
    {
        private CustomerShellForm _embeddedPage;
        private CustomerNavItem _activeNav;
        private readonly Dictionary<CustomerNavItem, CustomerShellForm> _pageCache =
            new Dictionary<CustomerNavItem, CustomerShellForm>();

        public CustomerHostForm()
            : base(CustomerNavItem.Home, "Customer Home")
        {
            InitializeComponent();
            HideTopChrome();
            Text = "SmartMed - Customer Home";
            panelContent.Resize += PanelContent_Resize;
            FormClosed += (s, e) => DisposePageCache();
        }

        private void PanelContent_Resize(object sender, EventArgs e)
        {
            _embeddedPage?.SyncScrollRootWidth();
        }

        protected override void NavigateCustomer(CustomerNavItem nav)
        {
            if (_embeddedPage != null && _activeNav == nav)
            {
                RefreshEmbeddedPage(nav);
                return;
            }

            var subtitle = GetPageSubtitle(nav);
            Text = $"SmartMed - {subtitle}";
            lblTopSubtitle.Text = subtitle;
            SetActiveNav(nav);

            using (UiTheme.BatchUpdate(this, panelSidebar, panelContent))
            {
                if (_embeddedPage != null)
                    _embeddedPage.DetachPageContent();

                panelContent.Controls.Clear();
                panelContent.AutoScrollPosition = new System.Drawing.Point(0, 0);

                _embeddedPage = GetOrCreatePage(nav);
                _embeddedPage.SetContentTarget(panelContent);
                _embeddedPage.PrepareForHostDisplay();
                UiTheme.ApplyFontTree(panelContent);
            }

            _activeNav = nav;
            RefreshEmbeddedPage(nav);
        }

        protected override void InitializePageContent()
        {
            if (_embeddedPage != null) return;
            NavigateCustomer(CustomerNavItem.Home);
        }

        private static string GetPageSubtitle(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home) return "Customer Home";
            if (nav == CustomerNavItem.Browse) return "Browse Medicines";
            if (nav == CustomerNavItem.Cart) return "My Cart & Checkout";
            if (nav == CustomerNavItem.Orders) return "My Orders";
            if (nav == CustomerNavItem.Profile) return "My Profile";
            return "Customer Portal";
        }

        private static CustomerShellForm CreateEmbeddedPage(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home) return new CustomerDashboardForm(true);
            if (nav == CustomerNavItem.Browse) return new SearchMedicinesForm(true);
            if (nav == CustomerNavItem.Cart) return new PlaceOrderForm(true);
            if (nav == CustomerNavItem.Orders) return new TrackOrdersForm(true);
            if (nav == CustomerNavItem.Profile) return new ProfileManagementForm(true);
            throw new ArgumentException("Unknown customer section.");
        }

        private CustomerShellForm GetOrCreatePage(CustomerNavItem nav)
        {
            if (_pageCache.TryGetValue(nav, out var page) && page != null && !page.IsDisposed)
                return page;

            page = CreateEmbeddedPage(nav);
            _pageCache[nav] = page;
            return page;
        }

        private void DisposePageCache()
        {
            foreach (var page in _pageCache.Values)
            {
                if (page == null || page.IsDisposed) continue;
                page.DetachPageContent();
                page.SetContentTarget(null);
                page.Dispose();
            }
            _pageCache.Clear();
            _embeddedPage = null;
        }

        private void RefreshEmbeddedPage(CustomerNavItem nav)
        {
            if (nav == CustomerNavItem.Home && _embeddedPage is CustomerDashboardForm dashboard)
                dashboard.RefreshData();
            else if (nav == CustomerNavItem.Cart && _embeddedPage is PlaceOrderForm cart)
                cart.RefreshCart();
            else if (nav == CustomerNavItem.Orders && _embeddedPage is TrackOrdersForm orders)
                orders.RefreshOrders();
            else if (nav == CustomerNavItem.Profile && _embeddedPage is ProfileManagementForm profile)
                profile.RefreshProfile();
        }
    }
}
