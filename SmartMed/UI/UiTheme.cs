using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Fonts;

namespace SmartMed.UI
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
        public static readonly Color AdminSidebar = Color.FromArgb(238, 245, 244);
        public static readonly Color SurfaceContainer = Color.FromArgb(232, 239, 238);
        public static readonly Color SurfaceContainerHigh = Color.FromArgb(227, 234, 233);
        public static readonly Color SurfaceContainerLow = Color.FromArgb(238, 245, 244);
        public static readonly Color OnPrimaryContainer = Color.FromArgb(117, 151, 146);
        public static readonly Color SecondaryContainer = Color.FromArgb(184, 237, 226);
        public static readonly Color OnSecondaryContainer = Color.FromArgb(59, 109, 100);
        public static readonly Color OnSecondaryFixedVariant = Color.FromArgb(27, 79, 71);
        public const int SidebarWidth = 260;
        public static readonly Color ErrorContainer = Color.FromArgb(255, 218, 214);
        public static readonly Color Error = Color.FromArgb(186, 26, 26);
        public static readonly Color ErrorOnContainer = Color.FromArgb(147, 0, 10);
        public static readonly Color Danger = Color.FromArgb(186, 26, 26);
        public static readonly Color Success = Color.FromArgb(16, 185, 129);
        public static readonly Color GridHeaderText = Color.FromArgb(48, 56, 55);

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

        /// <summary>Sets the form root font and applies Hanken Grotesk to all child controls.</summary>
        public static void ApplyFormFonts(Form form)
        {
            if (form == null) return;
            EnsureFonts();
            form.Font = UiFont;
            EnableFontPropagation(form);
            ApplyFontTree(form);
        }

        private static bool IsBundledFamily(FontFamily family)
        {
            if (family == null || _familyRegular == null) return false;
            var name = family.Name;
            return (_familyRegular != null && name == _familyRegular.Name)
                || (_familySemiBold != null && name == _familySemiBold.Name)
                || (_familyBold != null && name == _familyBold.Name);
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
            FontAssets.RegisterProcessFonts();
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

        private static readonly HashSet<Control> FontPropagationRoots = new HashSet<Control>();

        public static void ApplyFontTree(Control root)
        {
            if (root == null) return;
            EnsureFonts();
            ApplyFontRecursive(root);
        }

        public static void EnableFontPropagation(Control root)
        {
            if (root == null) return;
            EnsureFonts();
            if (FontPropagationRoots.Add(root))
                root.ControlAdded += OnControlAddedForFont;
            ApplyFontTree(root);
        }

        private static void OnControlAddedForFont(object sender, ControlEventArgs e)
        {
            if (e.Control == null) return;
            ApplyFontTree(e.Control);
            EnableFontPropagation(e.Control);
        }

        private static void ApplyFontRecursive(Control control)
        {
            if (control == null) return;

            control.Font = MapFont(control.Font);

            if (control is DataGridView grid)
                ApplyGridFonts(grid);

            foreach (Control child in control.Controls)
                ApplyFontRecursive(child);
        }

        private static Font MapFont(Font current)
        {
            if (current == null) return UiFont;
            if (IsBundledFamily(current.FontFamily))
                return current;

            var size = current.Size;
            var style = current.Style;
            var bold = (style & FontStyle.Bold) == FontStyle.Bold || current.Bold;
            var semiboldName = current.FontFamily?.Name?.IndexOf("Semi", StringComparison.OrdinalIgnoreCase) >= 0;

            if (bold)
            {
                if (size >= 16F) return FontAt(size, bold: true);
                if (size >= 13F) return FontAt(size, semibold: true);
                return FontAt(size, bold: true);
            }

            if (semiboldName) return FontAt(size, semibold: true);
            return FontAt(size);
        }

        private static void ApplyGridFonts(DataGridView grid)
        {
            var cellFont = FontAt(UiFont.Size);
            grid.DefaultCellStyle.Font = cellFont;
            grid.AlternatingRowsDefaultCellStyle.Font = cellFont;
            grid.ColumnHeadersDefaultCellStyle.Font = FontAt(UiFont.Size, bold: true);
            grid.RowHeadersDefaultCellStyle.Font = cellFont;
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

            EnableFontPropagation(form);
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
            EnableFontPropagation(form);
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

        public static void EnableDoubleBuffer(Control control)
        {
            if (control == null) return;
            typeof(Control).InvokeMember(
                "DoubleBuffered",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty,
                null,
                control,
                new object[] { true });
        }

        public static void ApplyClinicalGrid(DataGridView grid)
        {
            if (grid == null) return;
            EnableDoubleBuffer(grid);
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Color.FromArgb(238, 245, 244);
            grid.ColumnHeadersHeight = 36;
            grid.RowTemplate.Height = 36;
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = AdminOnSurface;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 240, 236);
            grid.DefaultCellStyle.SelectionForeColor = AdminOnSurface;
            grid.DefaultCellStyle.Font = UiFont;
            grid.DefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 252, 252);
            grid.ColumnHeadersDefaultCellStyle.BackColor = SurfaceContainer;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = AdminMuted;
            grid.ColumnHeadersDefaultCellStyle.Font = UiFontBold;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(10, 0, 10, 0);
            grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = grid.ColumnHeadersDefaultCellStyle.BackColor;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        }

        public static void BeautifyGridHeaders(DataGridView grid)
        {
            if (grid?.Columns == null) return;
            foreach (DataGridViewColumn col in grid.Columns)
            {
                if (col.Name == "Actions") continue;
                col.HeaderText = SplitCamelCase(col.Name);
            }
        }

        private static string SplitCamelCase(string name)
        {
            if (string.IsNullOrEmpty(name)) return name;
            var result = new StringBuilder();
            for (var i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i]))
                    result.Append(' ');
                result.Append(name[i]);
            }
            return result.ToString();
        }

        public static void ApplyAdminWinFormsShell(
            Form form,
            Panel titleBar,
            Panel menuBar,
            Panel sidebar,
            Panel content,
            Panel statusBar)
        {
            if (form != null)
            {
                form.BackColor = AdminSurface;
                form.Font = UiFont;
                EnableDoubleBuffer(form);
            }

            if (titleBar != null)
            {
                titleBar.BackColor = Color.White;
                EnableDoubleBuffer(titleBar);
            }

            if (menuBar != null)
            {
                menuBar.BackColor = Color.White;
                menuBar.Font = FontAt(8.25F);
                EnableDoubleBuffer(menuBar);
                foreach (Control c in menuBar.Controls)
                {
                    if (c is Label lbl)
                    {
                        lbl.ForeColor = AdminLabelText;
                        lbl.BackColor = Color.White;
                        lbl.Cursor = Cursors.Default;
                    }
                }
            }

            if (sidebar != null)
            {
                sidebar.BackColor = AdminSidebar;
                EnableDoubleBuffer(sidebar);
            }

            if (content != null)
            {
                content.BackColor = AdminSurface;
                content.Padding = Padding.Empty;
                EnableDoubleBuffer(content);
            }

            if (statusBar != null)
            {
                statusBar.BackColor = AdminTeal;
                EnableDoubleBuffer(statusBar);
                foreach (Control c in statusBar.Controls)
                {
                    if (c is Label lbl)
                    {
                        lbl.ForeColor = Color.White;
                        lbl.BackColor = AdminTeal;
                        lbl.Font = FontAt(8.25F);
                    }
                }
            }

            if (form != null)
                EnableFontPropagation(form);
        }

        /// <summary>
        /// Lays out custom title-bar buttons left-to-right: Minimize, Maximize, Close (matches native Login chrome).
        /// </summary>
        public static void ArrangeWindowControls(Panel host, Button minimize, Button maximize, Button close)
        {
            if (host == null) return;

            host.SuspendLayout();
            host.Controls.Clear();

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                Margin = Padding.Empty,
                Padding = Padding.Empty,
                BackColor = host.BackColor
            };

            foreach (var btn in new[] { minimize, maximize, close })
            {
                if (btn == null) continue;
                btn.Dock = DockStyle.None;
                btn.Margin = Padding.Empty;
                btn.Size = new Size(46, 32);
                flow.Controls.Add(btn);
            }

            host.Controls.Add(flow);
            host.ResumeLayout(true);
        }

        public static void StyleWindowControlButton(Button button, bool isClose = false)
        {
            if (button == null) return;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = Color.White;
            button.ForeColor = AdminMuted;
            button.Font = UiFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.FlatAppearance.MouseOverBackColor = isClose
                ? Danger
                : Color.FromArgb(232, 239, 238);
        }

        public static void StyleWinFormsNavButton(Button button, bool active)
        {
            if (button == null) return;
            button.Paint -= NavButton_LegacyPaint;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Height = Math.Max(button.Height, 40);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(40, 0, 12, 0);
            button.Margin = new Padding(12, 2, 12, 2);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.Font = active ? UiFontBold : UiFont;
            button.Tag = active;

            if (active)
            {
                button.BackColor = AdminSidebar;
                button.ForeColor = OnPrimaryContainer;
                button.FlatAppearance.MouseOverBackColor = AdminSidebar;
            }
            else
            {
                button.BackColor = AdminSidebar;
                button.ForeColor = AdminMuted;
                button.FlatAppearance.MouseOverBackColor = SurfaceContainerHigh;
                button.FlatAppearance.MouseDownBackColor = SurfaceContainer;
            }

            if (button is NavButton)
                button.Invalidate();
            else
                button.Paint += NavButton_LegacyPaint;
        }

        internal static void PaintNavButton(Button button, Graphics graphics)
        {
            var active = button.Tag is bool isActive && isActive;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            using (var clear = new SolidBrush(button.BackColor))
                graphics.FillRectangle(clear, button.ClientRectangle);

            var bounds = new Rectangle(12, 2, button.Width - 24, button.Height - 4);
            if (active)
            {
                using (var fill = new SolidBrush(PrimaryContainer))
                    graphics.FillRectangle(fill, bounds);
                using (var accent = new SolidBrush(PrimaryDark))
                    graphics.FillRectangle(accent, bounds.Left, bounds.Top, 4, bounds.Height);
            }

            var fore = active ? OnPrimaryContainer : button.ForeColor;
            var font = active ? UiFontBold : UiFont;
            var iconBounds = new Rectangle(bounds.Left + 12, bounds.Top, 24, bounds.Height);
            TextRenderer.DrawText(graphics, GetNavGlyph(button.Name), font, iconBounds, fore,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            var textBounds = new Rectangle(bounds.Left + 40, bounds.Top, bounds.Width - 44, bounds.Height);
            TextRenderer.DrawText(graphics, button.Text, font, textBounds, fore,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        private static void NavButton_LegacyPaint(object sender, PaintEventArgs e) =>
            PaintNavButton((Button)sender, e.Graphics);

        public static void StyleSidebarBrand(Panel brand, Panel iconHost, Label title, Label subtitle, bool customerPortal)
        {
            if (brand == null) return;
            brand.BackColor = AdminSidebar;
            brand.Padding = new Padding(24, 20, 24, 16);
            brand.Height = 88;

            if (iconHost != null)
            {
                iconHost.Size = new Size(40, 40);
                iconHost.Location = new Point(24, 20);
                iconHost.BackColor = customerPortal ? AdminTeal : PrimaryContainer;
                iconHost.Paint -= SidebarBrandIcon_Paint;
                iconHost.Paint += SidebarBrandIcon_Paint;
                iconHost.Tag = customerPortal;
            }

            if (title != null)
            {
                title.Text = "SmartMed";
                title.Font = UiFontBold;
                title.ForeColor = PrimaryDark;
                title.BackColor = AdminSidebar;
                title.AutoSize = true;
                title.Location = new Point(72, 22);
            }

            if (subtitle != null)
            {
                subtitle.Text = customerPortal ? "Health Portal" : "Clinical Management";
                subtitle.Font = FontAt(8.25F);
                subtitle.ForeColor = OnSecondaryFixedVariant;
                subtitle.BackColor = AdminSidebar;
                subtitle.AutoSize = true;
                subtitle.Location = new Point(72, 44);
            }
        }

        public static void StyleSidebarProfileFooter(Panel profile, Panel avatar, Label name, Label role)
        {
            if (profile == null) return;
            profile.BackColor = SurfaceContainer;
            profile.Padding = new Padding(24, 16, 24, 16);
            profile.Height = 72;

            if (avatar != null)
            {
                avatar.Size = new Size(32, 32);
                avatar.Location = new Point(24, 20);
            }

            if (name != null)
            {
                name.Font = UiFontBold;
                name.ForeColor = PrimaryDark;
                name.BackColor = SurfaceContainer;
                name.AutoSize = true;
                name.Location = new Point(64, 20);
            }

            if (role != null)
            {
                role.Font = FontAt(7.5F);
                role.ForeColor = AdminMuted;
                role.BackColor = SurfaceContainer;
                role.AutoSize = true;
                role.Location = new Point(64, 40);
                role.Text = role.Text?.ToUpperInvariant() ?? string.Empty;
            }
        }

        public static void StyleCustomerSupportPanel(Panel panel, Label heading, Label body, Button contact)
        {
            if (panel == null) return;
            panel.BackColor = SecondaryContainer;
            panel.Padding = new Padding(16);
            panel.Margin = new Padding(16, 0, 16, 16);

            if (heading != null)
            {
                heading.Text = "Need Help?";
                heading.Font = UiFontBold;
                heading.ForeColor = OnSecondaryContainer;
                heading.BackColor = SecondaryContainer;
                heading.AutoSize = true;
                heading.Location = new Point(16, 16);
            }

            if (body != null)
            {
                body.Text = "Our clinical staff is online to assist you with prescriptions.";
                body.Font = FontAt(8.25F);
                body.ForeColor = OnSecondaryContainer;
                body.BackColor = SecondaryContainer;
                body.Location = new Point(16, 36);
                body.Size = new Size(212, 36);
            }

            if (contact != null)
            {
                contact.Text = "CONTACT US";
                contact.FlatStyle = FlatStyle.Flat;
                contact.FlatAppearance.BorderSize = 0;
                contact.BackColor = AdminTeal;
                contact.ForeColor = Color.White;
                contact.Font = FontAt(8.25F);
                contact.Cursor = Cursors.Hand;
                contact.UseVisualStyleBackColor = false;
                contact.FlatAppearance.MouseOverBackColor = AdminTealDark;
                contact.Size = new Size(212, 32);
                contact.Location = new Point(16, 76);
            }
        }

        private static void SidebarBrandIcon_Paint(object sender, PaintEventArgs e)
        {
            var panel = (Panel)sender;
            var customer = panel.Tag is bool b && b;
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using (var brush = new SolidBrush(panel.BackColor))
                e.Graphics.FillRectangle(brush, panel.ClientRectangle);
            var glyph = customer ? "+" : "✚";
            var color = customer ? Color.White : OnPrimaryContainer;
            TextRenderer.DrawText(e.Graphics, glyph, UiFontBold, panel.ClientRectangle, color,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }


        private static string GetNavGlyph(string buttonName)
        {
            if (string.IsNullOrEmpty(buttonName)) return "•";
            if (buttonName.IndexOf("Dashboard", StringComparison.OrdinalIgnoreCase) >= 0
                || buttonName.IndexOf("Home", StringComparison.OrdinalIgnoreCase) >= 0)
                return "▣";
            if (buttonName.IndexOf("Medicine", StringComparison.OrdinalIgnoreCase) >= 0
                || buttonName.IndexOf("Inventory", StringComparison.OrdinalIgnoreCase) >= 0
                || buttonName.IndexOf("Browse", StringComparison.OrdinalIgnoreCase) >= 0)
                return "▤";
            if (buttonName.IndexOf("Customer", StringComparison.OrdinalIgnoreCase) >= 0
                || buttonName.IndexOf("Profile", StringComparison.OrdinalIgnoreCase) >= 0)
                return "◉";
            if (buttonName.IndexOf("Order", StringComparison.OrdinalIgnoreCase) >= 0)
                return "▥";
            if (buttonName.IndexOf("Report", StringComparison.OrdinalIgnoreCase) >= 0)
                return "▦";
            if (buttonName.IndexOf("Cart", StringComparison.OrdinalIgnoreCase) >= 0)
                return "▧";
            return "•";
        }

        public static void SetGridDataSource(DataGridView grid, object dataSource)
        {
            if (grid == null) return;
            grid.SuspendLayout();
            try { grid.DataSource = dataSource; }
            finally { grid.ResumeLayout(false); }
        }

        public static void StyleComboBox(ComboBox comboBox)
        {
            if (comboBox == null) return;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = AdminOnSurface;
            comboBox.Font = UiFont;
        }

        public static void StyleTabButton(Button btn, bool active)
        {
            if (btn == null) return;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Cursor = Cursors.Hand;
            btn.UseVisualStyleBackColor = false;
            if (active)
            {
                btn.BackColor = Color.White;
                btn.ForeColor = AdminOnSurface;
                btn.Font = UiFontBold;
            }
            else
            {
                btn.BackColor = AdminSidebar;
                btn.ForeColor = AdminMuted;
                btn.Font = UiFont;
            }
        }

        public static Button CreateFlatButton(string text, UiButtonStyle style, int width = 120, int height = 36)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = height,
                Font = UiFont,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            ApplyFlatButton(btn, style);
            return btn;
        }

        public static void ApplyFlatButton(Button button, UiButtonStyle style)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = UiFont;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;

            if (style == UiButtonStyle.Success)
            {
                button.BackColor = Success;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(5, 150, 105);
            }
            else if (style == UiButtonStyle.Danger)
            {
                button.BackColor = Danger;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(153, 27, 27);
            }
            else if (style == UiButtonStyle.Secondary)
            {
                button.BackColor = Color.FromArgb(238, 245, 244);
                button.ForeColor = AdminOnSurface;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(227, 234, 233);
            }
            else
            {
                button.BackColor = AdminTeal;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = AdminTealDark;
            }
        }
    }
}
