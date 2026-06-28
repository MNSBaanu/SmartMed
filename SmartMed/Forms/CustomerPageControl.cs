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
        protected Panel ScrollHost { get; private set; }
        protected Control ScrollRoot { get; private set; }

        protected CustomerPageControl()
        {
            DoubleBuffered = true;
            BackColor = UiTheme.AdminSurface;
            Font = UiTheme.UiFont;
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
            return System.Math.Max(600, w - 48);
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

        public abstract void RefreshPage();
    }
}
