using System;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    /// <summary>Single admin window — swaps only panelContent when navigating.</summary>
    public sealed partial class AdminHostForm : AdminShellForm
    {
        private AdminShellForm _embeddedPage;
        private AdminNavItem _activeNav;

        public AdminHostForm()
            : base(AdminNavItem.Overview, "Operational Dashboard")
        {
            InitializeComponent();
            HideTopChrome();
            Text = "SmartMed — Pharmacy Management";
            if (!IsDesignHost())
                UiTheme.ApplyAdminClinicalShell(this, panelTop, panelSidebar, panelContent);
        }

        protected override void NavigateAdmin(AdminNavItem nav)
        {
            if (_embeddedPage != null && _activeNav == nav)
            {
                RefreshEmbeddedPage(nav);
                return;
            }

            var subtitle = GetPageSubtitle(nav);
            Text = $"SmartMed - {subtitle}";
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
            NavigateAdmin(AdminNavItem.Overview);
        }

        private static string GetPageSubtitle(AdminNavItem nav)
        {
            if (nav == AdminNavItem.Overview) return "Operational Dashboard";
            if (nav == AdminNavItem.Medicines) return "Manage Medicines";
            if (nav == AdminNavItem.Customers) return "Manage Customers";
            if (nav == AdminNavItem.Orders) return "Manage Orders";
            if (nav == AdminNavItem.Reports) return "Generate Reports";
            return "Admin";
        }

        private static AdminShellForm CreateEmbeddedPage(AdminNavItem nav)
        {
            if (nav == AdminNavItem.Overview) return new AdminDashboardForm(true);
            if (nav == AdminNavItem.Medicines) return new ManageMedicinesForm(true);
            if (nav == AdminNavItem.Customers) return new ManageCustomersForm(true);
            if (nav == AdminNavItem.Orders) return new ManageOrdersForm(true);
            if (nav == AdminNavItem.Reports) return new ReportsForm(true);
            throw new ArgumentException("Unknown admin section.");
        }

        private void DisposeEmbeddedPage()
        {
            if (_embeddedPage == null) return;
            _embeddedPage.SetContentTarget(null);
            _embeddedPage.Dispose();
            _embeddedPage = null;
        }

        private void RefreshEmbeddedPage(AdminNavItem nav)
        {
            if (nav == AdminNavItem.Overview && _embeddedPage is AdminDashboardForm dashboard)
                dashboard.RefreshData();
            else if (nav == AdminNavItem.Reports && _embeddedPage is ReportsForm reports)
                reports.RefreshReports();
        }
    }
}
