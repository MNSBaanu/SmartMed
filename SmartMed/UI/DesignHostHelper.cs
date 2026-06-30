using System.ComponentModel;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class DesignHostHelper
    {
        public static bool IsDesignHost(Control control) =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime
            || (control?.Site?.DesignMode ?? false);
    }
}
