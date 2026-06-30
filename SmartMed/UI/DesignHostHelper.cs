using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class DesignHostHelper
    {
        public static bool IsDesignHost(Control control)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            for (var current = control; current != null; current = current.Parent)
            {
                var site = current.Site;
                if (site == null)
                    continue;

                if (site.DesignMode)
                    return true;

                if (site.GetService(typeof(IDesignerHost)) != null)
                    return true;
            }

            return false;
        }
    }
}
