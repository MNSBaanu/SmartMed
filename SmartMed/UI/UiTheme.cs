using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MaterialButton = ReaLTaiizor.Controls.MaterialButton;
using MaterialForm = ReaLTaiizor.Forms.MaterialForm;
using ReaLTaiizor.Colors;
using ReaLTaiizor.Manager;
using ReaLTaiizor.Util;

namespace SmartMed.UI
{
    /// <summary>Central ReaLTaiizor Material theme and flat control styling for SmartMed.</summary>
    public static class UiTheme
    {
        public static readonly Color PageBackground = Color.FromArgb(245, 245, 245);
        public static readonly Color CardBackground = Color.White;
        public static readonly Color SidebarBackground = Color.FromArgb(30, 30, 30);
        public static readonly Color HeaderBackground = Color.FromArgb(24, 24, 24);
        public static readonly Color Primary = Color.FromArgb(33, 33, 33);
        public static readonly Color PrimaryDark = Color.FromArgb(18, 18, 18);
        public static readonly Color Success = Color.FromArgb(39, 174, 96);
        public static readonly Color Danger = Color.FromArgb(231, 76, 60);
        public static readonly Color Warning = Color.FromArgb(243, 156, 18);
        public static readonly Color HeaderSubtitle = Color.FromArgb(190, 190, 190);
        public static readonly Color Muted = Color.FromArgb(127, 140, 141);
        public static readonly Color GridHeader = Color.FromArgb(236, 240, 241);
        public static readonly Color GridHeaderText = Color.FromArgb(44, 62, 80);

        // Clinical admin shell (Stitch / SmartMed v4)
        public static readonly Color AdminTeal = Color.FromArgb(53, 103, 94);
        public static readonly Color AdminTealDark = Color.FromArgb(27, 79, 71);
        public static readonly Color AdminSidebar = Color.FromArgb(232, 239, 238);
        public static readonly Color AdminSurface = Color.FromArgb(244, 251, 250);
        public static readonly Color AdminOutline = Color.FromArgb(193, 200, 198);
        public static readonly Color AdminOnSurface = Color.FromArgb(22, 29, 29);
        public static readonly Color AdminMuted = Color.FromArgb(65, 72, 71);

        public const string FontFamilyName = "Roboto";
        public const float FontSize = 9F;

        /// <summary>Vertical gap between major page sections on customer portal screens.</summary>
        public const int CustomerSectionGap = 16;

        /// <summary>Gap between related controls in a row (buttons, filters).</summary>
        public const int CustomerControlGap = 8;

        /// <summary>Inner inset between page scroll area and text/controls (all customer pages).</summary>
        public const int CustomerContentPadding = 16;

        /// <summary>Outer inset of the customer content panel from the shell edge.</summary>
        public static readonly Padding CustomerPageInset = new Padding(32, 24, 32, 24);

        public static Padding CustomerSectionMargin => new Padding(0, 0, 0, CustomerSectionGap);

        public static Padding CustomerControlMargin => new Padding(0, 0, CustomerControlGap, CustomerControlGap);

        public static Label CreateSectionHeading(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = UiFontBold,
                Margin = new Padding(0, 0, 0, CustomerControlGap)
            };
        }

        private static PrivateFontCollection _fontCollection;
        private static Font _uiFont;
        private static Font _uiFontBold;
        private static bool _initialized;

        public static Font UiFont
        {
            get
            {
                EnsureFonts();
                return _uiFont;
            }
        }

        public static Font UiFontBold
        {
            get
            {
                EnsureFonts();
                return _uiFontBold;
            }
        }

        public static void Init()
        {
            if (_initialized) return;
            EnsureFonts();
            _initialized = true;

            var skin = MaterialSkinManager.Instance;
            skin.Theme = MaterialSkinManager.Themes.LIGHT;
            skin.ColorScheme = new MaterialColorScheme(
                HeaderBackground,
                PrimaryDark,
                Color.FromArgb(50, 50, 50),
                Primary,
                MaterialTextShade.WHITE);
        }

        private static void EnsureFonts()
        {
            if (_uiFont != null) return;

            try
            {
                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var regularPath = Path.Combine(baseDir, "Assets", "Fonts", "Roboto-Regular.ttf");
                var boldPath = Path.Combine(baseDir, "Assets", "Fonts", "Roboto-Bold.ttf");

                if (File.Exists(regularPath) && File.Exists(boldPath))
                {
                    _fontCollection = new PrivateFontCollection();
                    _fontCollection.AddFontFile(regularPath);
                    _fontCollection.AddFontFile(boldPath);

                    FontFamily regularFamily = null;
                    FontFamily boldFamily = null;
                    foreach (var family in _fontCollection.Families)
                    {
                        if (family.IsStyleAvailable(FontStyle.Regular))
                            regularFamily = family;
                        if (family.IsStyleAvailable(FontStyle.Bold))
                            boldFamily = family;
                    }

                    if (regularFamily != null)
                    {
                        _uiFont = new Font(regularFamily, FontSize, FontStyle.Regular, GraphicsUnit.Point);
                        _uiFontBold = boldFamily != null
                            ? new Font(boldFamily, FontSize, FontStyle.Bold, GraphicsUnit.Point)
                            : new Font(regularFamily, FontSize, FontStyle.Bold, GraphicsUnit.Point);
                        return;
                    }
                }
            }
            catch
            {
                // Fall back to installed Roboto if bundled files cannot be loaded.
            }

            _uiFont = new Font(FontFamilyName, FontSize, FontStyle.Regular, GraphicsUnit.Point);
            _uiFontBold = new Font(FontFamilyName, FontSize, FontStyle.Bold, GraphicsUnit.Point);
        }

        public static void ApplyFontTree(Control root)
        {
            if (root == null) return;
            EnsureFonts();

            if (root is DataGridView grid)
                ApplyGrid(grid);
            else
                root.Font = root.Font.Bold ? UiFontBold : UiFont;

            if (root is TextBox textBox)
                StyleTextBox(textBox);
            else if (root is ComboBox comboBox)
                StyleComboBox(comboBox);
            else if (root is CheckBox checkBox)
                checkBox.Font = UiFont;
            else if (root is NumericUpDown numeric)
                numeric.Font = UiFont;
            else if (root is LinkLabel link)
                link.Font = UiFont;

            foreach (Control child in root.Controls)
                ApplyFontTree(child);
        }

        public static void RegisterForm(MaterialForm form)
        {
            if (form == null || form.Site?.DesignMode == true) return;
            Init();
            MaterialSkinManager.Instance.AddFormToManage(form);
        }

        public static void RegisterForm(Form form)
        {
            if (form == null || form.Site?.DesignMode == true) return;
            Init();
            form.Font = UiFont;
            EnableDoubleBuffer(form);
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

        private const int WmSetRedraw = 0x000B;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, bool wParam, int lParam);

        public static IDisposable BatchUpdate(Control root, params Control[] alsoSuspend)
        {
            return new BatchUpdateScope(root, alsoSuspend);
        }

        public static void RevealForm(Form form)
        {
            if (form == null || form.IsDisposed) return;

            var targetOpacity = form.Opacity;
            if (targetOpacity < 0.01) targetOpacity = 1;

            form.Opacity = 0;
            if (!form.Visible)
                form.Show();

            form.SuspendLayout();
            try { form.PerformLayout(); }
            finally { form.ResumeLayout(false); }

            form.Refresh();
            form.Opacity = targetOpacity;
        }

        public static void SetGridDataSource(DataGridView grid, object dataSource)
        {
            if (grid == null) return;
            grid.SuspendLayout();
            try { grid.DataSource = dataSource; }
            finally { grid.ResumeLayout(false); }
        }

        private sealed class BatchUpdateScope : IDisposable
        {
            private readonly Control _root;
            private readonly Control[] _extras;
            private bool _disposed;

            public BatchUpdateScope(Control root, Control[] extras)
            {
                _root = root;
                _extras = extras ?? Array.Empty<Control>();
                if (_root == null) return;

                EnsureHandle(_root);
                _root.SuspendLayout();
                SetRedraw(_root, false);

                foreach (var extra in _extras)
                {
                    if (extra == null) continue;
                    EnsureHandle(extra);
                    extra.SuspendLayout();
                    SetRedraw(extra, false);
                }
            }

            public void Dispose()
            {
                if (_disposed || _root == null) return;
                _disposed = true;

                foreach (var extra in _extras)
                {
                    if (extra == null) continue;
                    SetRedraw(extra, true);
                    extra.ResumeLayout(false);
                }

                SetRedraw(_root, true);
                _root.ResumeLayout(false);
                _root.Invalidate(true);
            }

            private static void EnsureHandle(Control control)
            {
                if (!control.IsHandleCreated)
                    control.CreateControl();
            }

            private static void SetRedraw(Control control, bool enable)
            {
                if (control.IsHandleCreated)
                    SendMessage(control.Handle, WmSetRedraw, enable, 0);
            }
        }

        public static void ApplyHeaderPanel(Panel header)
        {
            if (header == null) return;
            EnableDoubleBuffer(header);
            header.BackColor = HeaderBackground;
            foreach (Control c in header.Controls)
            {
                if (c is Label lbl)
                {
                    lbl.BackColor = HeaderBackground;
                    var isBrand = lbl.Font.Bold || string.Equals(lbl.Text, "SmartMed", StringComparison.Ordinal);
                    lbl.ForeColor = isBrand ? Color.White : HeaderSubtitle;
                    lbl.Font = isBrand ? UiFontBold : UiFont;
                }
                else if (c is Button btn)
                    StyleIconButton(btn);
            }
        }

        public static void ApplyShell(MaterialForm form, Panel header, Panel sidebar, Panel content)
        {
            RegisterForm(form);
            form.BackColor = PageBackground;
            form.Font = UiFont;
            EnableDoubleBuffer(form);
            EnableDoubleBuffer(header);
            EnableDoubleBuffer(sidebar);
            EnableDoubleBuffer(content);

            if (header != null)
                ApplyHeaderPanel(header);

            if (sidebar != null)
            {
                sidebar.BackColor = SidebarBackground;
                foreach (Control c in sidebar.Controls)
                {
                    if (c is Button nav)
                        StyleNavButton(nav);
                }
            }

            if (content != null)
            {
                content.BackColor = PageBackground;
                content.Font = UiFont;
            }
        }

        /// <summary>Light clinical admin chrome — sidebar, content, optional header.</summary>
        public static void ApplyAdminClinicalShell(MaterialForm form, Panel header, Panel sidebar, Panel content)
        {
            RegisterForm(form);
            form.BackColor = AdminSurface;
            form.Font = UiFont;
            EnableDoubleBuffer(form);
            if (header != null) EnableDoubleBuffer(header);
            EnableDoubleBuffer(sidebar);
            EnableDoubleBuffer(content);

            if (header != null && header.Visible)
            {
                header.BackColor = Color.White;
                header.Height = Math.Max(header.Height, 48);
                foreach (Control c in header.Controls)
                {
                    if (c is Label lbl)
                    {
                        lbl.BackColor = Color.White;
                        lbl.ForeColor = lbl.Font.Bold ? AdminOnSurface : AdminMuted;
                    }
                    else if (c is Button btn)
                    {
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.BackColor = Color.White;
                        btn.ForeColor = AdminMuted;
                        btn.FlatAppearance.BorderColor = AdminOutline;
                        btn.FlatAppearance.BorderSize = 1;
                    }
                }
            }

            if (sidebar != null)
            {
                sidebar.BackColor = AdminSidebar;
                sidebar.Padding = new Padding(0);
                sidebar.Width = Math.Max(sidebar.Width, 260);
                foreach (Control c in sidebar.Controls)
                {
                    if (c is Button nav)
                        StyleAdminNavButton(nav, active: false);
                }
            }

            if (content != null)
            {
                content.BackColor = AdminSurface;
                content.Padding = new Padding(28, 24, 28, 0);
                content.Font = UiFont;
            }
        }

        public static void StyleAdminNavButton(Button button, bool active)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Height = Math.Max(button.Height, 44);
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(20, 0, 12, 0);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.Font = active ? UiFontBold : UiFont;

            if (active)
            {
                button.BackColor = Color.White;
                button.ForeColor = AdminTeal;
                button.FlatAppearance.MouseOverBackColor = Color.White;
            }
            else
            {
                button.BackColor = AdminSidebar;
                button.ForeColor = AdminMuted;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 232, 230);
                button.FlatAppearance.MouseDownBackColor = Color.FromArgb(210, 224, 222);
            }
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
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 239, 238);
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
            var result = new System.Text.StringBuilder();
            for (var i = 0; i < name.Length; i++)
            {
                if (i > 0 && char.IsUpper(name[i]))
                    result.Append(' ');
                result.Append(name[i]);
            }
            return result.ToString();
        }

        public static void ApplyLoginForm(Form form, Panel body, Panel card = null, LinkLabel forgotLink = null)
        {
            RegisterForm(form);
            form.BackColor = AdminSurface;
            form.Font = UiFont;
            form.Padding = Padding.Empty;

            if (body != null)
            {
                body.BackColor = AdminSurface;
                ApplyFontTree(body);
            }

            ApplyClinicalAuthCard(card);

            if (forgotLink != null)
            {
                forgotLink.Font = UiFont;
                forgotLink.BackColor = Color.White;
                forgotLink.LinkColor = AdminTeal;
                forgotLink.ActiveLinkColor = AdminTealDark;
                forgotLink.VisitedLinkColor = AdminTeal;
            }
        }

        /// <summary>Clinical card shell for login / registration dialogs.</summary>
        public static void ApplyClinicalAuthCard(Panel card)
        {
            if (card == null) return;
            card.BackColor = Color.White;
            ApplyFontTree(card);
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

        public static void ApplyClinicalAuthHeader(Panel header, Label titleLabel, Button closeButton = null)
        {
            if (header == null) return;
            EnableDoubleBuffer(header);
            header.BackColor = AdminTeal;
            if (titleLabel != null)
            {
                titleLabel.BackColor = AdminTeal;
                titleLabel.ForeColor = Color.White;
                titleLabel.Font = UiFontBold;
            }
            if (closeButton != null)
            {
                closeButton.FlatStyle = FlatStyle.Flat;
                closeButton.FlatAppearance.BorderSize = 0;
                closeButton.BackColor = AdminTeal;
                closeButton.ForeColor = Color.FromArgb(220, 255, 255, 255);
                closeButton.Font = UiFontBold;
                closeButton.Cursor = Cursors.Hand;
                closeButton.UseVisualStyleBackColor = false;
                closeButton.FlatAppearance.MouseOverBackColor = AdminTealDark;
            }
        }

        public static void StyleClinicalFieldLabel(Label label)
        {
            if (label == null) return;
            label.Font = UiFont;
            label.ForeColor = AdminMuted;
            label.BackColor = Color.White;
        }

        public static void ApplyClinicalAuthButton(Button button, bool primary)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.Font = UiFont;
            button.Cursor = Cursors.Hand;
            button.FlatAppearance.BorderSize = 1;
            if (primary)
            {
                button.BackColor = AdminTeal;
                button.ForeColor = Color.White;
                button.FlatAppearance.BorderColor = AdminTealDark;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 109, 100);
            }
            else
            {
                button.BackColor = Color.White;
                button.ForeColor = AdminOnSurface;
                button.FlatAppearance.BorderColor = AdminOutline;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 250, 249);
            }
            button.UseVisualStyleBackColor = false;
        }

        public static void ApplyRegistrationForm(Form form, Panel card, Panel header, Label titleLabel, Button closeButton, params Control[] fieldRoots)
        {
            RegisterForm(form);
            form.BackColor = AdminSurface;
            form.Font = UiFont;
            ApplyClinicalAuthCard(card);
            ApplyClinicalAuthHeader(header, titleLabel, closeButton);
            foreach (var root in fieldRoots)
            {
                if (root == null) continue;
                ApplyFontTree(root);
                StyleClinicalControls(root);
            }
        }

        private static void StyleClinicalControls(Control root)
        {
            foreach (Control c in root.Controls)
            {
                if (c is Label lbl && !(c is LinkLabel))
                    StyleClinicalFieldLabel(lbl);
                else if (c is TextBox tb)
                    StyleTextBox(tb);
                StyleClinicalControls(c);
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

        public static MaterialButton CreateMaterialButton(string text, bool highEmphasis = true, int width = 120, int height = 36)
        {
            Init();
            return new MaterialButton
            {
                Text = text,
                Size = new Size(width, height),
                Type = MaterialButton.MaterialButtonType.Contained,
                HighEmphasis = highEmphasis,
                UseAccentColor = false,
                Density = MaterialButton.MaterialButtonDensity.Default
            };
        }

        public static void ApplyFlatButton(Button button, UiButtonStyle style)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = UiFont;
            button.Cursor = Cursors.Hand;
            button.Height = Math.Max(button.Height, 36);

            if (style == UiButtonStyle.Success)
            {
                button.BackColor = Success;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 153, 84);
            }
            else if (style == UiButtonStyle.Danger)
            {
                button.BackColor = Danger;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
            }
            else if (style == UiButtonStyle.Warning)
            {
                button.BackColor = Warning;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(211, 132, 18);
            }
            else if (style == UiButtonStyle.Secondary)
            {
                button.BackColor = Color.FromArgb(189, 195, 199);
                button.ForeColor = Color.FromArgb(44, 62, 80);
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(149, 165, 166);
            }
            else if (style == UiButtonStyle.Accent)
            {
                button.BackColor = Color.FromArgb(155, 89, 182);
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(142, 68, 173);
            }
            else
            {
                button.BackColor = Primary;
                button.ForeColor = Color.White;
                button.FlatAppearance.MouseOverBackColor = PrimaryDark;
            }
            button.UseVisualStyleBackColor = false;
        }

        public static void StyleNavButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = SidebarBackground;
            button.ForeColor = Color.FromArgb(236, 240, 241);
            button.Font = UiFont;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.Padding = new Padding(16, 0, 0, 0);
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(60, 60, 60);
        }

        public static void StyleIconButton(Button button)
        {
            if (button == null) return;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = HeaderBackground;
            button.ForeColor = Color.White;
            button.Font = UiFontBold;
            button.Cursor = Cursors.Hand;
            button.UseVisualStyleBackColor = false;
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(50, 50, 50);
        }

        public const char PasswordMaskChar = '\u2022';

        public static void StyleTextBox(TextBox textBox)
        {
            if (textBox == null) return;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = GridHeaderText;
            textBox.Font = UiFont;
        }

        public static void StylePasswordBox(TextBox textBox, bool masked = true)
        {
            if (textBox == null) return;
            StyleTextBox(textBox);
            textBox.UseSystemPasswordChar = false;
            textBox.PasswordChar = masked ? PasswordMaskChar : '\0';
        }

        public static void StyleComboBox(ComboBox comboBox)
        {
            if (comboBox == null) return;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = Color.White;
            comboBox.ForeColor = GridHeaderText;
            comboBox.Font = UiFont;
        }

        public static void ApplyGrid(DataGridView grid)
        {
            if (grid == null) return;
            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = CardBackground;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = Color.FromArgb(220, 224, 228);
            grid.DefaultCellStyle.BackColor = CardBackground;
            grid.DefaultCellStyle.ForeColor = GridHeaderText;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(225, 225, 225);
            grid.DefaultCellStyle.SelectionForeColor = GridHeaderText;
            grid.DefaultCellStyle.Font = UiFont;
            grid.ColumnHeadersDefaultCellStyle.BackColor = GridHeader;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = GridHeaderText;
            grid.ColumnHeadersDefaultCellStyle.Font = UiFontBold;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(4);
            grid.ColumnHeadersHeight = 36;
            grid.RowTemplate.Height = 32;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(250, 251, 252);
        }

        public static Panel CreateCardPanel(int height = 0)
        {
            var panel = new Panel
            {
                BackColor = CardBackground,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 0, 12)
            };
            if (height > 0) panel.Height = height;
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(Color.FromArgb(220, 224, 228)))
                    e.Graphics.DrawRectangle(pen, rect);
            };
            return panel;
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
                btn.BackColor = Color.FromArgb(235, 235, 235);
                btn.ForeColor = GridHeaderText;
                btn.Font = UiFontBold;
            }
            else
            {
                btn.BackColor = Color.FromArgb(236, 240, 241);
                btn.ForeColor = Muted;
                btn.Font = UiFont;
            }
        }
    }
}
