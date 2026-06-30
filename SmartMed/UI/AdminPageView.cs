using System.Windows.Forms;

namespace SmartMed.UI
{
    /// <summary>Visual chrome shared by admin page controls in designer and runtime.</summary>
    internal static class AdminPageView
    {
        public static void EnsureTheme() => AuthFormView.EnsureTheme();

        public static void ApplyChrome(Control page)
        {
            EnsureTheme();
            if (page == null) return;

            page.BackColor = UiTheme.AdminSurface;
            page.Font = UiTheme.UiFont;
            UiTheme.EnableDoubleBuffer(page);
        }
    }
}
