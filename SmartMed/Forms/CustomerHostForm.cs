using System;
using System.Windows.Forms;

namespace SmartMed.UI
{
    /// <summary>Single customer window — swaps only panelContent when navigating.</summary>
    public sealed partial class CustomerHostForm : CustomerShellForm
    {
        private CustomerShellForm _embeddedPage;
        private CustomerNavItem _activeNav;

        public CustomerHostForm()
            : base(CustomerNavItem.Home, "Customer Home")
        {
            InitializeComponent();
            HideTopChrome();
            Text = "SmartMed - Customer Home";
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
                panelContent.Visible = false;
                DisposeEmbeddedPage();
                panelContent.Controls.Clear();

                _embeddedPage = CreateEmbeddedPage(nav);
                _embeddedPage.SetContentTarget(panelContent);
                _embeddedPage.PrepareForNavigation();
                UiTheme.ApplyFontTree(panelContent);
                panelContent.Visible = true;
            }

            _activeNav = nav;
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

        private void DisposeEmbeddedPage()
        {
            if (_embeddedPage == null) return;
            _embeddedPage.SetContentTarget(null);
            _embeddedPage.Dispose();
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
