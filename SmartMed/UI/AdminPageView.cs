using System.Windows.Forms;

namespace SmartMed.UI
{
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
