using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Fonts;

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
        public static readonly Color AdminLabelText = Color.FromArgb(48, 56, 55);
        public static readonly Color FooterText = Color.FromArgb(90, 98, 96);
        public static readonly Color LinkTeal = Color.FromArgb(27, 79, 71);
        public static readonly Color InputBackground = Color.FromArgb(238, 245, 244);
        public static readonly Color InputFocusBackground = Color.White;
        public static readonly Color PlaceholderText = Color.FromArgb(130, 138, 136);
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
            EnsurePdfFonts();
            EnsureFonts();
            _initialized = true;
        }

        public static XFont PdfFont(float size, bool bold = false) =>
            new XFont(FontFamilyName, size, bold ? XFontStyle.Bold : XFontStyle.Regular);

        private static void EnsurePdfFonts()
        {
            if (GlobalFontSettings.FontResolver == null)
                GlobalFontSettings.FontResolver = new HankenGroteskFontResolver();
        }

        private static void EnsureFonts()
        {
            if (_uiFont != null) return;

            TryLoadBundledFonts();
            if (_familyRegular == null)
            {
                throw new InvalidOperationException(
                    "Hanken Grotesk font files are required in Assets/Fonts (Regular, SemiBold, Bold).");
            }

            _uiFont = new Font(_familyRegular, FontSize, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontBold = _familyBold != null
                ? new Font(_familyBold, FontSize, FontStyle.Regular, GraphicsUnit.Point)
                : new Font(_familySemiBold ?? _familyRegular, FontSize, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontSemibold = _familySemiBold != null
                ? new Font(_familySemiBold, 10F, FontStyle.Regular, GraphicsUnit.Point)
                : new Font(_familyBold ?? _familyRegular, 10F, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontAuthTitle = _familySemiBold != null
                ? new Font(_familySemiBold, 14F, FontStyle.Regular, GraphicsUnit.Point)
                : new Font(_familyBold ?? _familyRegular, 14F, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontTitle = _familyBold != null
                ? new Font(_familyBold, 16F, FontStyle.Regular, GraphicsUnit.Point)
                : new Font(_familySemiBold ?? _familyRegular, 16F, FontStyle.Regular, GraphicsUnit.Point);
        }

        private static void TryLoadBundledFonts()
        {
            _fontCollection = new PrivateFontCollection();
            _familyRegular = TryRegisterFamily("HankenGrotesk-Regular.ttf");
            _familySemiBold = TryRegisterFamily("HankenGrotesk-SemiBold.ttf");
            _familyBold = TryRegisterFamily("HankenGrotesk-Bold.ttf");
        }

        private static FontFamily TryRegisterFamily(string fileName)
        {
            try
            {
                var path = FontAssets.GetFontFilePath(fileName);
                _fontCollection.AddFontFile(path);
                return _fontCollection.Families[_fontCollection.Families.Length - 1];
            }
            catch
            {
                return null;
            }
        }

        public static Font FontAt(float size, bool semibold = false, bool bold = false)
        {
            EnsureFonts();
            if (bold)
                return _familyBold != null
                    ? new Font(_familyBold, size, FontStyle.Regular, GraphicsUnit.Point)
                    : new Font(_familySemiBold ?? _familyRegular, size, FontStyle.Regular, GraphicsUnit.Point);
            if (semibold && _familySemiBold != null)
                return new Font(_familySemiBold, size, FontStyle.Regular, GraphicsUnit.Point);
            return new Font(_familyRegular, size, FontStyle.Regular, GraphicsUnit.Point);
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
                helpLink.Font = UiFontSemibold;
                helpLink.BackColor = Color.White;
                helpLink.LinkColor = LinkTeal;
                helpLink.ActiveLinkColor = PrimaryContainer;
                helpLink.VisitedLinkColor = LinkTeal;
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
            label.ForeColor = AdminLabelText;
            label.BackColor = Color.White;
        }

        public static bool IsPlaceholderActive(TextBox textBox) =>
            textBox != null && textBox.ForeColor == PlaceholderText;

        public static string ReadTextBoxValue(TextBox textBox)
        {
            if (textBox == null || IsPlaceholderActive(textBox))
                return string.Empty;
            return textBox.Text.Trim();
        }

        private static void ApplyPlaceholder(TextBox textBox, string placeholder)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text) && !IsPlaceholderActive(textBox))
                return;

            textBox.Text = placeholder;
            textBox.ForeColor = PlaceholderText;
            textBox.PasswordChar = '\0';
        }

        private static void ClearPlaceholder(TextBox textBox)
        {
            if (!IsPlaceholderActive(textBox))
                return;

            textBox.Text = string.Empty;
            textBox.ForeColor = AdminOnSurface;
        }

        public static void StyleClinicalTextBox(TextBox textBox, string placeholder)
        {
            if (textBox == null || textBox.Tag as string == "clinical-input") return;
            textBox.Tag = "clinical-input";
            textBox.AccessibleDescription = placeholder;
            StyleTextBox(textBox);

            textBox.GotFocus += (s, e) =>
            {
                ClearPlaceholder(textBox);
                textBox.BackColor = InputFocusBackground;
            };
            textBox.LostFocus += (s, e) =>
            {
                textBox.BackColor = InputBackground;
                ApplyPlaceholder(textBox, placeholder);
            };

            ApplyPlaceholder(textBox, placeholder);
        }

        public static void StyleClinicalPasswordBox(TextBox textBox, string placeholder)
        {
            StyleClinicalTextBox(textBox, placeholder);
        }

        public static void StylePasswordToggleButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.Text = string.Empty;
            button.Cursor = Cursors.Hand;
            button.BackColor = InputBackground;
            button.ForeColor = LinkTeal;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = InputFocusBackground;
            button.UseVisualStyleBackColor = false;
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.TabStop = false;
        }

        public static void SetPasswordToggleIcon(Button button, bool visible)
        {
            if (button == null) return;
            var previous = button.Image;
            button.Image = ClinicalIcons.CreateVisibilityIcon(visible, LinkTeal, 20);
            previous?.Dispose();
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

        public static void ApplySecondaryButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = UiFontSemibold;
            button.Cursor = Cursors.Hand;
            button.BackColor = Color.White;
            button.ForeColor = AdminOnSurface;
            button.FlatAppearance.BorderSize = 1;
            button.FlatAppearance.BorderColor = AdminOutline;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 250, 249);
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleLinkButton(LinkLabel link)
        {
            if (link == null) return;
            link.Font = UiFontSemibold;
            link.BackColor = Color.White;
            link.LinkColor = LinkTeal;
            link.ActiveLinkColor = PrimaryContainer;
            link.VisitedLinkColor = LinkTeal;
        }

        public static void ApplyRegistrationForm(Form form, Panel body, Panel card)
        {
            form.BackColor = AdminSurface;
            form.Font = UiFont;
            if (body != null)
                body.BackColor = AdminSurface;
            ApplyClinicalAuthCard(card);
            ApplyFontTree(form);
        }

        public static void ApplyRegistrationCardHeader(Panel header, Label titleLabel, Button closeButton)
        {
            if (header != null)
                header.BackColor = PrimaryDark;
            if (titleLabel != null)
            {
                titleLabel.BackColor = PrimaryDark;
                titleLabel.ForeColor = Color.White;
                titleLabel.Font = FontAt(8.25F, semibold: true);
            }
            if (closeButton != null)
            {
                closeButton.FlatStyle = FlatStyle.Flat;
                closeButton.FlatAppearance.BorderSize = 0;
                closeButton.BackColor = PrimaryDark;
                closeButton.ForeColor = Color.FromArgb(220, 255, 255, 255);
                closeButton.Font = UiFontBold;
                closeButton.Cursor = Cursors.Hand;
                closeButton.UseVisualStyleBackColor = false;
                closeButton.FlatAppearance.MouseOverBackColor = PrimaryContainer;
            }
        }

        public static void StyleRegistrationFieldLabel(Label label)
        {
            if (label == null) return;
            label.Font = FontAt(8.25F, semibold: true);
            label.ForeColor = AdminMuted;
            label.BackColor = Color.White;
        }

        public static void ApplyRegisterButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = FontAt(8.25F, semibold: true);
            button.Cursor = Cursors.Hand;
            button.BackColor = AdminTeal;
            button.ForeColor = Color.White;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = AdminTealDark;
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleRegistrationBrandFooter(Panel footer, Label brandTitle, Label brandSubtitle, Panel brandIcon)
        {
            if (footer != null)
                footer.BackColor = FooterBackground;
            if (brandTitle != null)
            {
                brandTitle.Font = FontAt(14F, semibold: true);
                brandTitle.ForeColor = PrimaryDark;
                brandTitle.BackColor = FooterBackground;
            }
            if (brandSubtitle != null)
            {
                brandSubtitle.Font = FontAt(8.25F, semibold: true);
                brandSubtitle.ForeColor = FooterText;
                brandSubtitle.BackColor = FooterBackground;
            }
            if (brandIcon != null)
            {
                brandIcon.BackColor = AdminTeal;
                foreach (Control child in brandIcon.Controls)
                {
                    child.BackColor = AdminTeal;
                    child.ForeColor = Color.White;
                }
            }
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
