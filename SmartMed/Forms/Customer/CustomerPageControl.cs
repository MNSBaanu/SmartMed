using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    public interface ICustomerPage
    {
        void RefreshPage();
        void SyncScrollRootWidth(int fallback);
    }

    [ToolboxItem(false)]
    public class CustomerPageControl : Form, ICustomerPage
    {
        private bool _pageBuilt;

        protected Panel ScrollHost { get; private set; }
        protected Control ScrollRoot { get; private set; }

        public CustomerPageControl()
        {
            AdminPageControl.ConfigureEmbeddedPageShell(this);
        }

        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                if (value != null)
                    EnsurePageContent();
            }
        }

        protected bool IsDesignHost() => DesignHostHelper.IsDesignHost(this);

        protected virtual bool PreferDesignTimePreview() => IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsurePageContent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            EnsurePageContent();
        }

        protected override void SetVisibleCore(bool value)
        {
            EnsurePageContent();
            base.SetVisibleCore(value);
        }

        protected void EnsurePageContent()
        {
            if (_pageBuilt)
                return;

            try
            {
                AdminPageView.EnsureTheme();
                AdminPageView.ApplyChrome(this);

                BuildPageLayout();
                SyncScrollRootWidth();
                if (PreferDesignTimePreview())
                    LoadDesignTimePreview();
                else
                    DoRefreshPage();

                if (!IsDesignHost())
                    DoubleBuffered = true;

                PerformLayout();
                Invalidate(true);
                _pageBuilt = true;
            }
            catch (Exception ex) when (IsDesignHost() || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                ShowDesignTimeBuildError(ex);
                _pageBuilt = true;
            }
        }

        private void ShowDesignTimeBuildError(Exception ex)
        {
            Controls.Clear();
            Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.DarkRed,
                BackColor = Color.FromArgb(244, 251, 250),
                Padding = new Padding(16),
                Text = "Design preview could not be built:" + Environment.NewLine + Environment.NewLine + ex.Message
            });
        }

        protected virtual void BuildPageLayout() { }
        protected virtual void DoRefreshPage() { }
        protected virtual void LoadDesignTimePreview() { }

        public void RefreshPage()
        {
            EnsurePageContent();
            if (PreferDesignTimePreview())
                return;
            DoRefreshPage();
        }

        protected void WireScrollRoot(Control scrollRoot)
        {
            ScrollRoot = scrollRoot;
            Controls.Clear();

            ScrollHost = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = UiTheme.AdminSurface,
                Padding = new Padding(24, 24, 24, 24)
            };

            scrollRoot.Dock = DockStyle.Top;
            scrollRoot.Width = GetScrollContentWidth();
            ScrollHost.Controls.Add(scrollRoot);
            Controls.Add(ScrollHost);

            if (!IsDesignHost())
            {
                UiTheme.EnableDoubleBuffer(ScrollHost);
                UiTheme.EnableFontPropagation(this);
            }

            ScrollHost.Resize += (s, e) => SyncScrollRootWidth();
        }

        protected int GetScrollContentWidth(int fallback = 800)
        {
            var w = ScrollHost?.ClientSize.Width ?? ClientSize.Width;
            if (w < 200) w = fallback;
            return Math.Max(600, w - 48);
        }

        public virtual void SyncScrollRootWidth(int fallback = 800)
        {
            if (ScrollHost == null || ScrollRoot == null || ScrollHost.IsDisposed)
                return;

            var width = GetScrollContentWidth(fallback);
            if (ScrollRoot.Width != width)
                ScrollRoot.Width = width;
            ScrollRoot.PerformLayout();
        }
    }
}
