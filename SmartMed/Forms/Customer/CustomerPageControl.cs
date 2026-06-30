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

    public abstract class CustomerPageControl : UserControl, ICustomerPage
    {
        private bool _pageBuilt;

        protected Panel ScrollHost { get; private set; }
        protected Control ScrollRoot { get; private set; }

        protected CustomerPageControl()
        {
            DoubleBuffered = true;
            BackColor = UiTheme.AdminSurface;
            Font = UiTheme.UiFont;
        }

        protected bool IsDesignHost() => DesignHostHelper.IsDesignHost(this);

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

        protected void EnsurePageContent()
        {
            if (_pageBuilt)
                return;

            _pageBuilt = true;
            BuildPageLayout();
            SyncScrollRootWidth();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                DoRefreshPage();
        }

        protected abstract void BuildPageLayout();
        protected abstract void DoRefreshPage();
        protected virtual void LoadDesignTimePreview() { }

        public void RefreshPage()
        {
            EnsurePageContent();
            if (IsDesignHost())
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
            UiTheme.EnableDoubleBuffer(ScrollHost);
            ScrollHost.Resize += (s, e) => SyncScrollRootWidth();
            UiTheme.EnableFontPropagation(this);
        }

        protected int GetScrollContentWidth(int fallback = 800)
        {
            var w = ScrollHost?.ClientSize.Width ?? Width;
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
