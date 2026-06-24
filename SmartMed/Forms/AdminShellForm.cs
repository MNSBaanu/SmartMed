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
    public partial class AdminShellForm : MaterialForm
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



        public AdminShellForm()

        {

            InitializeComponent();

            if (IsDesignHost()) { SetActiveNav(AdminNavItem.Overview); SyncShellChrome(); }

        }



        protected AdminShellForm(AdminNavItem activeNav, string subtitle, bool embeddedPage = false)

            : this()

        {

            _isEmbeddedPage = embeddedPage;

            DoubleBuffered = true;

            Text = "SmartMed";

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



            using (UiTheme.BatchUpdate(this, panelTop, panelSidebar, PagePanel))
            {
                if (PagePanel != null)
                    PagePanel.Visible = false;

                InitializePageContent();

                if (!_isEmbeddedPage)
                {
                    UiTheme.ApplyFontTree(panelTop);
                    UiTheme.ApplyFontTree(panelSidebar);
                }
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

        internal void SetContentTarget(Panel host) => _contentTarget = host;

        /// <summary>Host content panel — uses AdminHostForm.panelContent when embedded.</summary>
        protected Panel PagePanel => _contentTarget ?? panelContent;

        protected int GetScrollContentWidth(int fallback = 850)
        {
            var w = PagePanel.ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            return w < 200 ? fallback : w;
        }

        protected void WireScrollRoot(Control scrollRoot, int fallback = 850)
        {
            UiTheme.EnableDoubleBuffer(scrollRoot);
            var host = PagePanel;
            host.Controls.Add(scrollRoot);
            host.Resize += (s, e) => scrollRoot.Width = GetScrollContentWidth(fallback);
        }



        protected T GetRuntimeService<T>(ref T service) where T : class, new()

        {

            if (IsDesignHost()) return null;

            return service ?? (service = new T());

        }



        protected void SetActiveNav(AdminNavItem active)

        {

            if (btnNavOverview == null) return;

            StyleNavButton(btnNavOverview, active == AdminNavItem.Overview);

            StyleNavButton(btnNavMedicines, active == AdminNavItem.Medicines);

            StyleNavButton(btnNavCustomers, active == AdminNavItem.Customers);

            StyleNavButton(btnNavOrders, active == AdminNavItem.Orders);

            StyleNavButton(btnNavReports, active == AdminNavItem.Reports);

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



        internal void HideShellChromeForEmbed()
        {
            panelTop.Visible = false;
            panelSidebar.Visible = false;
        }

        internal void PrepareForNavigation()

        {

            if (!IsHandleCreated)

                CreateControl();

            EnsurePageContent();

            PerformLayout();

        }



        protected AdminHostForm GetAdminHost()

        {

            for (var parent = Parent; parent != null; parent = parent.Parent)

            {

                if (parent is AdminHostForm host)

                    return host;

            }

            return this as AdminHostForm;

        }



        protected void GoToAdminSection(AdminNavItem nav)

        {

            var host = GetAdminHost();

            if (host != null)

                host.NavigateAdmin(nav);

            else

                NavigateAdmin(nav);

        }



        protected virtual void NavigateAdmin(AdminNavItem nav)

        {

            if (IsCurrentPage(nav))

            {

                RefreshCurrentPage(nav);

                return;

            }

            NavigateTo(CreatePageForm(nav));

        }



        private bool IsCurrentPage(AdminNavItem nav)

        {

            if (nav == AdminNavItem.Overview && this is AdminDashboardForm) return true;

            if (nav == AdminNavItem.Medicines && this is ManageMedicinesForm) return true;

            if (nav == AdminNavItem.Customers && this is ManageCustomersForm) return true;

            if (nav == AdminNavItem.Orders && this is ManageOrdersForm) return true;

            if (nav == AdminNavItem.Reports && this is ReportsForm) return true;

            return false;

        }



        private void RefreshCurrentPage(AdminNavItem nav)

        {

            if (nav == AdminNavItem.Overview && this is AdminDashboardForm dashboard)

                dashboard.RefreshData();

            else if (nav == AdminNavItem.Reports && this is ReportsForm reports)

                reports.RefreshReports();

        }



        private static AdminShellForm CreatePageForm(AdminNavItem nav)

        {

            if (nav == AdminNavItem.Overview) return new AdminDashboardForm();

            if (nav == AdminNavItem.Medicines) return new ManageMedicinesForm();

            if (nav == AdminNavItem.Customers) return new ManageCustomersForm();

            if (nav == AdminNavItem.Orders) return new ManageOrdersForm();

            if (nav == AdminNavItem.Reports) return new ReportsForm();

            throw new ArgumentException("Unknown admin section.");

        }



        protected void NavigateTo(AdminShellForm next)

        {

            next.StartPosition = FormStartPosition.Manual;

            next.Location = Location;

            next.Size = Size;

            next.WindowState = WindowState;



            var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();

            login?.AttachAdminReturn(next);



            next.PrepareForNavigation();

            Hide();

            UiTheme.RevealForm(next);

            Close();

        }



        private void BtnClose_Click(object sender, EventArgs e) => Logout();



        private void BtnNavLogout_Click(object sender, EventArgs e) => Logout();



        protected void Logout()

        {

            if (_isEmbeddedPage)

            {

                GetAdminHost()?.Logout();

                return;

            }



            Session.Clear();

            Close();

            var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();

            if (login != null && !login.IsDisposed)

                login.Show();

        }



        private void BtnNavOverview_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Overview);



        private void BtnNavMedicines_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Medicines);



        private void BtnNavCustomers_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Customers);



        private void BtnNavOrders_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Orders);



        private void BtnNavReports_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Reports);

    }

}


