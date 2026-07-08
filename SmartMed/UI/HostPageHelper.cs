using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class HostPageHelper
    {
        public static void ShowInPanel(Panel host, Form page)
        {
            if (host == null || page == null) return;

            host.SuspendLayout();
            try
            {
                UiTheme.EnableDoubleBuffer(host);

                host.Controls.Clear();
                host.AutoScrollPosition = Point.Empty;

                page.Visible = false;
                page.TopLevel = false;
                page.FormBorderStyle = FormBorderStyle.None;
                page.Dock = DockStyle.Fill;
                page.ShowInTaskbar = false;

                host.Controls.Add(page);
                page.PerformLayout();
                page.Show();
                page.Visible = true;
            }
            finally
            {
                host.ResumeLayout(true);
            }
        }
    }
}
