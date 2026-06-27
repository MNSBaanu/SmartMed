using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;

namespace SmartMedNew.UI
{
    /// <summary>Clinical auth theme using Stitch Hanken Grotesk.</summary>
    public static class UiTheme
    {
        public static readonly Color AdminSurface = Color.FromArgb(244, 251, 250);
        public static readonly Color AdminTeal = Color.FromArgb(53, 103, 94);
        public static readonly Color AdminTealDark = Color.FromArgb(27, 79, 71);
        public static readonly Color PrimaryContainer = Color.FromArgb(12, 46, 43);
        public static readonly Color PrimaryDark = Color.FromArgb(0, 24, 22);
        public static readonly Color TitleBar = Color.FromArgb(241, 245, 249);
        public static readonly Color TitleBarBorder = Color.FromArgb(226, 232, 240);
        public static readonly Color AdminOutline = Color.FromArgb(193, 200, 198);
        public static readonly Color AdminOnSurface = Color.FromArgb(22, 29, 29);
        public static readonly Color AdminMuted = Color.FromArgb(65, 72, 71);
        public static readonly Color InputBackground = Color.FromArgb(238, 245, 244);
        public static readonly Color FooterBackground = Color.FromArgb(232, 239, 238);
        public static readonly Color ErrorContainer = Color.FromArgb(255, 218, 214);
        public static readonly Color Error = Color.FromArgb(186, 26, 26);
        public static readonly Color ErrorOnContainer = Color.FromArgb(147, 0, 10);

        public const string FontFamilyName = "Hanken Grotesk";
        public const float FontSize = 9F;
        public const char PasswordMaskChar = '\u2022';

        private static PrivateFontCollection _fontCollection;
        private static FontFamily _familyRegular;
        private static FontFamily _familySemiBold;
        private static FontFamily _familyBold;
        private static Font _uiFont;
        private static Font _uiFontBold;
        private static Font _uiFontSemibold;
        private static Font _uiFontAuthTitle;
        private static Font _uiFontTitle;
        private static bool _initialized;

        public static Font UiFont
        {
            get { EnsureFonts(); return _uiFont; }
        }

        public static Font UiFontBold
        {
            get { EnsureFonts(); return _uiFontBold; }
        }

        public static Font UiFontSemibold
        {
            get { EnsureFonts(); return _uiFontSemibold; }
        }

        public static Font UiFontAuthTitle
        {
            get { EnsureFonts(); return _uiFontAuthTitle; }
        }

        public static Font UiFontTitle
        {
            get { EnsureFonts(); return _uiFontTitle; }
        }

        public static void Init()
        {
            if (_initialized) return;
            EnsureFonts();
            _initialized = true;
        }

        private static void EnsureFonts()
        {
            if (_uiFont != null) return;

            TryLoadBundledFonts();

            if (_familyRegular != null)
            {
                _uiFont = new Font(_familyRegular, FontSize, FontStyle.Regular, GraphicsUnit.Point);
                _uiFontBold = _familyBold != null
                    ? new Font(_familyBold, FontSize, FontStyle.Regular, GraphicsUnit.Point)
                    : new Font(_familyRegular, FontSize, FontStyle.Bold, GraphicsUnit.Point);
                _uiFontSemibold = _familySemiBold != null
                    ? new Font(_familySemiBold, 10F, FontStyle.Regular, GraphicsUnit.Point)
                    : new Font(_familyRegular, 10F, FontStyle.Bold, GraphicsUnit.Point);
                _uiFontAuthTitle = _familySemiBold != null
                    ? new Font(_familySemiBold, 14F, FontStyle.Regular, GraphicsUnit.Point)
                    : new Font(_familyRegular, 14F, FontStyle.Bold, GraphicsUnit.Point);
                _uiFontTitle = _familyBold != null
                    ? new Font(_familyBold, 16F, FontStyle.Regular, GraphicsUnit.Point)
                    : new Font(_familyRegular, 16F, FontStyle.Bold, GraphicsUnit.Point);
                return;
            }

            _uiFont = new Font(FontFamilyName, FontSize, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontBold = new Font(FontFamilyName, FontSize, FontStyle.Bold, GraphicsUnit.Point);
            _uiFontSemibold = new Font(FontFamilyName, 10F, FontStyle.Bold, GraphicsUnit.Point);
            _uiFontAuthTitle = new Font(FontFamilyName, 14F, FontStyle.Bold, GraphicsUnit.Point);
            _uiFontTitle = new Font(FontFamilyName, 16F, FontStyle.Bold, GraphicsUnit.Point);
        }

        private static void TryLoadBundledFonts()
        {
            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var regularPath = Path.Combine(baseDir, "Assets", "Fonts", "HankenGrotesk-Regular.ttf");
                var semiBoldPath = Path.Combine(baseDir, "Assets", "Fonts", "HankenGrotesk-SemiBold.ttf");
                var boldPath = Path.Combine(baseDir, "Assets", "Fonts", "HankenGrotesk-Bold.ttf");

                if (!File.Exists(regularPath))
                    return;

                _fontCollection = new PrivateFontCollection();
                _fontCollection.AddFontFile(regularPath);
                _familyRegular = _fontCollection.Families[_fontCollection.Families.Length - 1];

                if (File.Exists(semiBoldPath))
                {
                    _fontCollection.AddFontFile(semiBoldPath);
                    _familySemiBold = _fontCollection.Families[_fontCollection.Families.Length - 1];
                }

                if (File.Exists(boldPath))
                {
                    _fontCollection.AddFontFile(boldPath);
                    _familyBold = _fontCollection.Families[_fontCollection.Families.Length - 1];
                }
            }
            catch
            {
                _familyRegular = null;
                _familySemiBold = null;
                _familyBold = null;
            }
        }

        public static Font FontAt(float size, bool semibold = false, bool bold = false)
        {
            EnsureFonts();
            if (bold && _familyBold != null)
                return new Font(_familyBold, size, FontStyle.Regular, GraphicsUnit.Point);
            if (semibold && _familySemiBold != null)
                return new Font(_familySemiBold, size, FontStyle.Regular, GraphicsUnit.Point);
            if (bold || semibold)
                return new Font(FontFamilyName, size, FontStyle.Bold, GraphicsUnit.Point);
            if (_familyRegular != null)
                return new Font(_familyRegular, size, FontStyle.Regular, GraphicsUnit.Point);
            return new Font(FontFamilyName, size, FontStyle.Regular, GraphicsUnit.Point);
        }

        public static void ApplyFontTree(Control root)
        {
            if (root == null) return;
            EnsureFonts();
            ApplyFontRecursive(root);
        }

        private static void ApplyFontRecursive(Control control)
        {
            if (control.Font != null)
            {
                var size = control.Font.Size;
                var bold = control.Font.Bold;
                control.Font = size >= 16F && bold
                    ? FontAt(size, bold: true)
                    : size >= 14F && bold
                        ? FontAt(size, semibold: true)
                        : bold
                            ? FontAt(size, bold: true)
                            : FontAt(size);
            }

            foreach (Control child in control.Controls)
                ApplyFontRecursive(child);
        }

        public static void ApplyLoginForm(Form form, Panel body, Panel card, LinkLabel helpLink)
        {
            form.BackColor = AdminSurface;
            form.Font = UiFont;

            if (body != null)
                body.BackColor = AdminSurface;

            ApplyClinicalAuthCard(card);

            if (helpLink != null)
            {
                helpLink.Font = UiFont;
                helpLink.BackColor = Color.White;
                helpLink.LinkColor = AdminTeal;
                helpLink.ActiveLinkColor = PrimaryContainer;
                helpLink.VisitedLinkColor = AdminTeal;
            }

            ApplyFontTree(form);
        }

        public static void ApplyClinicalAuthCard(Panel card)
        {
            if (card == null) return;
            card.BackColor = Color.White;
            if (card.Tag as string == "clinical-auth-card") return;
            card.Tag = "clinical-auth-card";
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        public static void StyleClinicalFieldLabel(Label label)
        {
            if (label == null) return;
            label.Font = UiFont;
            label.ForeColor = AdminMuted;
            label.BackColor = Color.White;
        }

        public static void ApplyLoginButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = UiFontSemibold;
            button.Cursor = Cursors.Hand;
            button.BackColor = PrimaryContainer;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = PrimaryDark;
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleTextBox(TextBox textBox)
        {
            if (textBox == null) return;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = InputBackground;
            textBox.ForeColor = AdminOnSurface;
            textBox.Font = UiFont;
        }

        public static void StylePasswordBox(TextBox textBox, bool masked)
        {
            if (textBox == null) return;
            StyleTextBox(textBox);
            textBox.UseSystemPasswordChar = false;
            textBox.PasswordChar = masked ? PasswordMaskChar : '\0';
        }
    }
}
