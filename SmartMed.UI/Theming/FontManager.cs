using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class FontManager
    {
        private static readonly PrivateFontCollection Collection = new PrivateFontCollection();
        private static FontFamily _interFamily;
        private static bool _initialized;

        public static bool IsAvailable => _interFamily != null;

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            var fontPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Fonts", "Inter.ttf");
            if (!File.Exists(fontPath)) return;

            Collection.AddFontFile(fontPath);
            if (Collection.Families.Length > 0)
                _interFamily = Collection.Families[0];
        }

        public static Font Get(float size, FontStyle style = FontStyle.Regular)
        {
            Initialize();
            if (!IsAvailable)
                return new Font("Segoe UI", size, style);

            var adjustedStyle = style;
            if (style.HasFlag(FontStyle.Bold) && !_interFamily.IsStyleAvailable(FontStyle.Bold))
                adjustedStyle = style & ~FontStyle.Bold;

            return new Font(_interFamily, size, adjustedStyle, GraphicsUnit.Point);
        }

        public static void ApplyInterFont(Control control)
        {
            if (control == null || IsDesignMode(control)) return;

            Initialize();
            if (!IsAvailable) return;

            ApplyInterFontRecursive(control);
        }

        private static bool IsDesignMode(Control control)
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return true;
            return control.Site?.DesignMode ?? false;
        }

        private static void ApplyInterFontRecursive(Control control)
        {
            if (ShouldSkipFont(control))
            {
                foreach (Control child in control.Controls)
                    ApplyInterFontRecursive(child);
                return;
            }

            var current = control.Font ?? ClinicalPrecisionTheme.BodyFont;
            control.Font = Get(current.Size, current.Style);

            if (control is DataGridView grid)
            {
                grid.ColumnHeadersDefaultCellStyle.Font = GetFrom(grid.ColumnHeadersDefaultCellStyle.Font, grid);
                grid.DefaultCellStyle.Font = GetFrom(grid.DefaultCellStyle.Font, grid);
                grid.AlternatingRowsDefaultCellStyle.Font = GetFrom(grid.AlternatingRowsDefaultCellStyle.Font, grid);
            }

            foreach (Control child in control.Controls)
                ApplyInterFontRecursive(child);
        }

        private static Font GetFrom(Font cellFont, DataGridView grid)
        {
            var current = cellFont ?? grid.Font ?? ClinicalPrecisionTheme.BodyFont;
            return Get(current.Size, current.Style);
        }

        private static bool ShouldSkipFont(Control control)
        {
            var name = control.Font?.FontFamily?.Name ?? "";
            return name.IndexOf("MDL2", StringComparison.OrdinalIgnoreCase) >= 0
                || name.IndexOf("Marlett", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
