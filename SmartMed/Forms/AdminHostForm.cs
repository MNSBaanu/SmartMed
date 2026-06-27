using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SmartMed.UI
{
    /// <summary>Single admin window — swaps only panelContent when navigating.</summary>
    public sealed partial class AdminHostForm : AdminShellForm
    {
        private AdminShellForm _embeddedPage;
        private AdminNavItem _activeNav;
        private readonly Dictionary<AdminNavItem, AdminShellForm> _pageCache =
            new Dictionary<AdminNavItem, AdminShellForm>();

        public AdminHostForm()
            : base(AdminNavItem.Overview, "Operational Dashboard")
        {
            InitializeComponent();
            HideTopChrome();
            Text = "SmartMed — Pharmacy Management";
            UiTheme.ApplyAdminClinicalShell(this, panelTop, panelSidebar, panelContent);
            CompleteDesignInitialization();
            panelContent.Resize += PanelContent_Resize;
            if (!IsDesignHost())
                FormClosed += (s, e) => DisposePageCache();
        }

        private void PanelContent_Resize(object sender, EventArgs e)
        {
            _embeddedPage?.SyncScrollRootWidth();
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

        private AdminShellForm GetOrCreatePage(AdminNavItem nav)
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

        private void RefreshEmbeddedPage(AdminNavItem nav)
        {
            if (nav == AdminNavItem.Overview && _embeddedPage is AdminDashboardForm dashboard)
                dashboard.RefreshData();
            else if (nav == AdminNavItem.Reports && _embeddedPage is ReportsForm reports)
                reports.RefreshReports();
        }
    }
}
