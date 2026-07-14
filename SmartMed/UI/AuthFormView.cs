using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class AuthFormView
    {
        public static void EnsureTheme()
        {
            try
            {
                UiTheme.Init();
            }
            catch
            {
            }
        }

        public static void ApplyCardBorder(Panel card)
        {
            EnsureTheme();
            UiTheme.ApplyClinicalAuthCard(card);
        }

        public static void ApplyPasswordFieldBorder(Panel shell)
        {
            EnsureTheme();
            UiTheme.ApplyClinicalInputShellBorder(shell);
        }
    }
}
