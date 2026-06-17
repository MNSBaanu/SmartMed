using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class FontManager
    {
        private static PrivateFontCollection _fonts;
        private static FontFamily _interFamily;
        private static bool _initialized;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            try
            {
                var fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "Inter.ttf");
                if (!File.Exists(fontPath)) return;

                _fonts = new PrivateFontCollection();
                _fonts.AddFontFile(fontPath);
                if (_fonts.Families.Length > 0)
                    _interFamily = _fonts.Families[0];
            }
            catch
            {
                _interFamily = null;
            }
        }

        public static Font Get(float size, FontStyle style = FontStyle.Regular)
        {
            if (_interFamily != null)
                return new Font(_interFamily, size, style, GraphicsUnit.Point);

            return new Font("Segoe UI", size, style, GraphicsUnit.Point);
        }

        /// <summary>True when running inside the Visual Studio WinForms designer.</summary>
        public static bool IsDesignHost(Control control = null)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return true;

            if (control == null)
                return false;

            for (var current = control; current != null; current = current.Parent)
            {
                if (current.Site?.DesignMode == true)
                    return true;
            }

            return false;
        }

        public static bool IsDesignMode(Control control) => IsDesignHost(control);
    }
}
