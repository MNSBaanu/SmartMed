using System.Drawing;
using System.Windows.Forms;

namespace SmartMedNew.UI
{
    /// <summary>Minimal clinical auth theme (Segoe UI, no ReaLTaiizor).</summary>
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

        public const string FontFamilyName = "Segoe UI";
        public const float FontSize = 9F;
        public const char PasswordMaskChar = '\u2022';

        private static Font _uiFont;
        private static Font _uiFontBold;
        private static Font _uiFontSemibold;
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
            _uiFont = new Font(FontFamilyName, FontSize, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontBold = new Font(FontFamilyName, FontSize, FontStyle.Bold, GraphicsUnit.Point);
            _uiFontSemibold = new Font(FontFamilyName, 10F, FontStyle.Bold, GraphicsUnit.Point);
            _uiFontTitle = new Font(FontFamilyName, 16F, FontStyle.Bold, GraphicsUnit.Point);
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
