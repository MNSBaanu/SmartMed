using System.Windows.Forms;

namespace SmartMed.UI
{
    /// <summary>Visual chrome shared by Login/Registration designer and runtime (borders, theme init).</summary>
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
                // Designer host may not load embedded fonts until later.
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
