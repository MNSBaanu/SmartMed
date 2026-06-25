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
            if (IsDesignHost()) SyncShellChrome();
        }

        protected CustomerShellForm(CustomerNavItem activeNav, string subtitle)
            : this()
        {
            DoubleBuffered = true;
            Text = "SmartMed Customer Portal";
            lblTopSubtitle.Text = subtitle;
            if (!IsDesignHost())
                UiTheme.ApplyShell(this, panelTop, panelSidebar, panelContent);
            SyncShellChrome();
            if (IsDesignHost())
                EnsurePageContent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsurePageContent();
        }

        protected void EnsurePageContent()
        {
            if (_pageContentInitialized) return;
            _pageContentInitialized = true;
            InitializePageContent();
            if (!IsDesignHost())
            {
                UiTheme.ApplyFontTree(panelTop);
                UiTheme.ApplyFontTree(panelSidebar);
                UiTheme.ApplyFontTree(panelContent);
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

        protected int GetScrollContentWidth(int fallback = 800)
        {
            var w = panelContent.ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            return w < 200 ? fallback : w;
        }

        protected void WireScrollRoot(Control scrollRoot, int fallback = 800)
        {
            panelContent.Controls.Add(scrollRoot);
            panelContent.Resize += (s, e) => scrollRoot.Width = GetScrollContentWidth(fallback);
        }

        protected T GetRuntimeService<T>(ref T service) where T : class, new()
        {
            if (IsDesignHost()) return null;
            return service ?? (service = new T());
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

        internal void PrepareForNavigation()
        {
            SuspendLayout();
            panelContent?.SuspendLayout();
            try
            {
                EnsurePageContent();
            }
            finally
            {
                panelContent?.ResumeLayout(true);
                ResumeLayout(true);
                SyncShellChrome();
            }
        }

        protected void NavigateTo(CustomerShellForm next)
        {
            next.StartPosition = FormStartPosition.Manual;
            next.Location = Location;
            next.Size = Size;
            next.WindowState = WindowState;

            next.PrepareForNavigation();

            SmartMedApplicationContext.Current?.HandoffMainForm(next);

            SuspendLayout();
            try
            {
                next.Show();
                Hide();
            }
            finally
            {
                ResumeLayout(false);
                Close();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e) => ExitApplication();

        private void BtnNavLogout_Click(object sender, EventArgs e) => Logout();

        protected void ExitApplication()
        {
            CartService.Clear();
            Session.Clear();
            Close();
        }

        protected void Logout()
        {
            SmartMedApplicationContext.Current?.ShowLoginAfterLogout();
        }

        private void BtnNavHome_Click(object sender, EventArgs e)
        {
            if (this is CustomerDashboardForm dashboard)
            {
                dashboard.RefreshData();
                return;
            }
            NavigateTo(new CustomerDashboardForm());
        }

        private void BtnNavBrowse_Click(object sender, EventArgs e)
        {
            if (this is SearchMedicinesForm) return;
            NavigateTo(new SearchMedicinesForm());
        }

        private void BtnNavCart_Click(object sender, EventArgs e)
        {
            if (this is PlaceOrderForm) return;
            NavigateTo(new PlaceOrderForm());
        }

        private void BtnNavOrders_Click(object sender, EventArgs e)
        {
            if (this is TrackOrdersForm orders)
            {
                orders.RefreshOrders();
                return;
            }
            NavigateTo(new TrackOrdersForm());
        }

        private void BtnNavProfile_Click(object sender, EventArgs e)
        {
            if (this is ProfileManagementForm) return;
            NavigateTo(new ProfileManagementForm());
        }
    }
}
