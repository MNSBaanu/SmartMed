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
    [ToolboxItem(false)]
    public partial class CustomerShellForm : MaterialForm
    {
        private bool _pageContentInitialized;
        private readonly bool _isEmbeddedPage;

        private static readonly Lazy<bool> IsDesignToolsProcess = new Lazy<bool>(() =>
        {
            var name = Process.GetCurrentProcess().ProcessName;
            return name.IndexOf("devenv", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("DesignTools", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("XDesProc", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("WinFormsSurface", StringComparison.OrdinalIgnoreCase) >= 0;
        });

        public CustomerShellForm()
        {
            InitializeComponent();
            if (IsDesignHost())
            {
                UiTheme.ApplyAdminClinicalShell(this, panelTop, panelSidebar, panelContent);
                SetActiveNav(CustomerNavItem.Home);
                EnsureCustomerSidebarProfile();
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
            if (IsDesignHost())
            {
                if (!embeddedPage)
                    UiTheme.ApplyAdminClinicalShell(this, panelTop, panelSidebar, panelContent);
                EnsureCustomerSidebarProfile();
                SyncShellChrome();
                return;
            }

            if (!embeddedPage)
                UiTheme.ApplyAdminClinicalShell(this, panelTop, panelSidebar, panelContent);

            EnsureCustomerSidebarProfile();

            SyncShellChrome();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            TryLoadDesignPageContent();
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
                if (PagePanel != null)
                {
                    PagePanel.Visible = true;
                    PagePanel.PerformLayout();
                }
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

        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                TryLoadDesignPageContent();
            }
        }

        protected void CompleteDesignInitialization()
        {
            if (_isEmbeddedPage) return;
            TryLoadDesignPageContent();
        }

        protected void TryLoadDesignPageContent()
        {
            if (_pageContentInitialized || _isEmbeddedPage) return;
            if (!IsDesignHost()) return;
            EnsurePageContent();
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
        private Control _scrollRoot;
        private int _scrollWidthFallback = 800;

        internal void SetContentTarget(Panel host)
        {
            _contentTarget = host;
            _customerHost = host?.FindForm() as CustomerHostForm;
        }

        protected Panel PagePanel => _contentTarget ?? panelContent;

        protected int GetScrollContentWidth(int fallback = 800)
        {
            var host = PagePanel;
            if (host == null || host.IsDisposed)
                return fallback;

            var w = host.DisplayRectangle.Width;
            return w < 200 ? fallback : w;
        }

        protected void WireScrollRoot(Control scrollRoot, int fallback = 800, int minHeight = 0)
        {
            _scrollRoot = scrollRoot;
            _scrollWidthFallback = fallback;
            UiTheme.EnableDoubleBuffer(scrollRoot);

            scrollRoot.Dock = DockStyle.Top;
            scrollRoot.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            scrollRoot.Padding = new Padding(
                UiTheme.CustomerContentPadding,
                UiTheme.CustomerContentPadding,
                UiTheme.CustomerContentPadding,
                UiTheme.CustomerContentPadding);

            if (minHeight > 0)
                scrollRoot.MinimumSize = new Size(0, minHeight);

            SyncScrollRootWidth(fallback);
            AttachPageContent();
        }

        internal void SyncScrollRootWidth()
        {
            SyncScrollRootWidth(_scrollWidthFallback);
        }

        internal void SyncScrollRootWidth(int fallback)
        {
            if (_scrollRoot == null || _scrollRoot.IsDisposed)
                return;

            var width = GetScrollContentWidth(fallback);
            if (_scrollRoot.Width != width)
                _scrollRoot.Width = width;
            _scrollRoot.PerformLayout();
        }

        internal void DetachPageContent()
        {
            if (_scrollRoot == null || _scrollRoot.IsDisposed)
                return;

            var host = PagePanel;
            if (host != null && !host.IsDisposed && _scrollRoot.Parent == host)
                host.Controls.Remove(_scrollRoot);
        }

        internal void AttachPageContent()
        {
            if (_scrollRoot == null || _scrollRoot.IsDisposed)
                return;

            var host = PagePanel;
            if (host == null || host.IsDisposed)
                return;

            if (_scrollRoot.Parent != host)
                host.Controls.Add(_scrollRoot);

            SyncScrollRootWidth(_scrollWidthFallback);
        }

        internal void PrepareForHostDisplay()
        {
            if (!IsHandleCreated)
                CreateControl();
            EnsurePageContent();
            AttachPageContent();
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

        private Panel _sidebarProfile;
        private bool _sidebarProfileBuilt;

        private void EnsureCustomerSidebarProfile()
        {
            if (_sidebarProfileBuilt || panelSidebar == null || _isEmbeddedPage) return;
            _sidebarProfileBuilt = true;

            var displayName = IsDesignHost()
                ? "Customer"
                : (Session.CurrentCustomer?.Name ?? "Customer");

            _sidebarProfile = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                BackColor = Color.White,
                Padding = new Padding(16, 14, 16, 14)
            };
            _sidebarProfile.Paint += (s, e) =>
            {
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawLine(pen, 0, _sidebarProfile.Height - 1, _sidebarProfile.Width, _sidebarProfile.Height - 1);
            };

            var avatar = new Panel { Size = new Size(44, 44), Location = new Point(16, 14), BackColor = UiTheme.AdminTeal };
            avatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(UiTheme.AdminTeal))
                    e.Graphics.FillEllipse(brush, 0, 0, avatar.Width - 1, avatar.Height - 1);
                var name = displayName;
                var initial = name.Length > 0 ? name.Substring(0, 1).ToUpperInvariant() : "C";
                TextRenderer.DrawText(e.Graphics, initial, UiTheme.UiFontBold, avatar.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };

            var lblName = new Label
            {
                Location = new Point(68, 18),
                Size = new Size(170, 20),
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                Text = displayName
            };
            var lblRole = new Label
            {
                Location = new Point(68, 40),
                Size = new Size(170, 18),
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Text = "Customer"
            };

            _sidebarProfile.Controls.Add(lblRole);
            _sidebarProfile.Controls.Add(lblName);
            _sidebarProfile.Controls.Add(avatar);
            panelSidebar.Controls.Add(_sidebarProfile);
            panelSidebar.Controls.SetChildIndex(_sidebarProfile, 0);
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
