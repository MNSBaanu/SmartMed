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

                UiTheme.ApplyAdminClinicalShell(this, panelTop, panelSidebar, panelContent);

            EnsureAdminSidebarProfile();

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
        private AdminHostForm _adminHost;

        internal void SetContentTarget(Panel host)
        {
            _contentTarget = host;
            _adminHost = host?.FindForm() as AdminHostForm;
        }

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



        private Panel _sidebarProfile;
        private bool _sidebarProfileBuilt;

        private void EnsureAdminSidebarProfile()
        {
            if (_sidebarProfileBuilt || panelSidebar == null || IsDesignHost() || _isEmbeddedPage) return;
            _sidebarProfileBuilt = true;

            _sidebarProfile = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.FromArgb(255, 255, 255),
                Padding = new Padding(16, 14, 16, 14)
            };
            _sidebarProfile.Paint += (s, e) =>
            {
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawLine(pen, 0, _sidebarProfile.Height - 1, _sidebarProfile.Width, _sidebarProfile.Height - 1);
            };

            var avatar = new Panel
            {
                Size = new Size(44, 44),
                Location = new Point(16, 14),
                BackColor = UiTheme.AdminTeal
            };
            avatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(UiTheme.AdminTeal))
                    e.Graphics.FillEllipse(brush, 0, 0, avatar.Width - 1, avatar.Height - 1);
                var initial = (Session.CurrentAdmin?.Username ?? "A").Substring(0, 1).ToUpperInvariant();
                TextRenderer.DrawText(e.Graphics, initial, UiTheme.UiFontBold, avatar.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            var lblName = new Label
            {
                AutoSize = false,
                Location = new Point(68, 18),
                Size = new Size(170, 20),
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                Text = Session.CurrentAdmin?.Username ?? "Administrator"
            };
            var lblRole = new Label
            {
                AutoSize = false,
                Location = new Point(68, 40),
                Size = new Size(170, 18),
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Text = "Administrator"
            };

            _sidebarProfile.Controls.Add(lblRole);
            _sidebarProfile.Controls.Add(lblName);
            _sidebarProfile.Controls.Add(avatar);
            panelSidebar.Controls.Add(_sidebarProfile);
            panelSidebar.Controls.SetChildIndex(_sidebarProfile, 0);
        }

        protected void RefreshSidebarProfile()
        {
            if (_sidebarProfile == null) return;
            foreach (Control c in _sidebarProfile.Controls)
            {
                if (c is Label lbl && lbl.Font.Bold)
                    lbl.Text = Session.CurrentAdmin?.Username ?? "Administrator";
            }
            _sidebarProfile.Invalidate(true);
        }



        private void StyleNavButton(Button button, bool active)
        {
            if (button == null) return;
            if (button == btnNavLogout)
            {
                button.FlatStyle = FlatStyle.Flat;
                button.FlatAppearance.BorderSize = 0;
                button.BackColor = UiTheme.AdminSidebar;
                button.ForeColor = UiTheme.AdminMuted;
                button.Font = UiTheme.UiFont;
                button.TextAlign = ContentAlignment.MiddleLeft;
                button.Padding = new Padding(20, 0, 12, 0);
                button.Cursor = Cursors.Hand;
                button.UseVisualStyleBackColor = false;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 230, 230);
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 210, 210);
                return;
            }

            UiTheme.StyleAdminNavButton(button, active);
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
            if (_adminHost != null && !_adminHost.IsDisposed)
                return _adminHost;

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

                GetAdminHost()?.ExitApplication();

                return;

            }



            Session.Clear();

            Close();

        }



        protected void Logout()

        {

            if (_isEmbeddedPage)

            {

                GetAdminHost()?.Logout();

                return;

            }



            SmartMedApplicationContext.Current?.ShowLoginAfterLogout();

        }



        private void BtnNavOverview_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Overview);



        private void BtnNavMedicines_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Medicines);



        private void BtnNavCustomers_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Customers);



        private void BtnNavOrders_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Orders);



        private void BtnNavReports_Click(object sender, EventArgs e) => NavigateAdmin(AdminNavItem.Reports);

    }

}


