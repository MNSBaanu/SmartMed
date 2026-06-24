using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using MaterialButton = ReaLTaiizor.Controls.MaterialButton;
using MaterialForm = ReaLTaiizor.Forms.MaterialForm;
using ReaLTaiizor.Manager;

namespace SmartMed.UI
{
    public enum UiButtonStyle
    {
        Primary,
        Success,
        Danger,
        Warning,
        Secondary,
        Accent
    }

    /// <summary>Central ReaLTaiizor Material theme and flat control styling for SmartMed.</summary>
    public static class UiTheme
    {
        public static readonly Color PageBackground = Color.FromArgb(245, 247, 250);
        public static readonly Color CardBackground = Color.White;
        public static readonly Color SidebarBackground = Color.FromArgb(52, 73, 94);
        public static readonly Color HeaderBackground = Color.FromArgb(41, 128, 185);
        public static readonly Color Primary = Color.FromArgb(52, 152, 219);
        public static readonly Color PrimaryDark = Color.FromArgb(41, 128, 185);
        public static readonly Color Success = Color.FromArgb(39, 174, 96);
        public static readonly Color Danger = Color.FromArgb(231, 76, 60);
        public static readonly Color Warning = Color.FromArgb(243, 156, 18);
        public static readonly Color Muted = Color.FromArgb(127, 140, 141);
        public static readonly Color GridHeader = Color.FromArgb(236, 240, 241);
        public static readonly Color GridHeaderText = Color.FromArgb(44, 62, 80);
        public static readonly Font UiFont = new Font("Segoe UI", 9F);
        public static readonly Font UiFontBold = new Font("Segoe UI", 9F, FontStyle.Bold);

        private static bool _initialized;

        public static void Init()
        {
            if (_initialized) return;
            _initialized = true;

            var skin = MaterialSkinManager.Instance;
            skin.Theme = MaterialSkinManager.Themes.LIGHT;
        }

        public static void RegisterForm(MaterialForm form)
        {
            if (form == null || form.Site?.DesignMode == true) return;
            Init();
            MaterialSkinManager.Instance.AddFormToManage(form);
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
            {
                header.BackColor = HeaderBackground;
                foreach (Control c in header.Controls)
                {
                    if (c is Label lbl)
                    {
                        lbl.ForeColor = Color.White;
                        if (lbl.Font.Bold || lbl.Text == "SmartMed")
                            lbl.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
                    }
                    else if (c is Button btn)
                        StyleIconButton(btn);
                }
            }

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
            }
        }

        public static void ApplyLoginForm(MaterialForm form, params Control[] inputs)
        {
            RegisterForm(form);
            form.BackColor = CardBackground;
            form.Font = UiFont;
            form.Padding = new Padding(0, 64, 0, 0);

            foreach (var c in inputs)
            {
                if (c is Panel panel)
                    panel.BackColor = CardBackground;
                if (c is Label lbl)
                    lbl.ForeColor = GridHeaderText;
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

            switch (style)
            {
                case UiButtonStyle.Success:
                    button.BackColor = Success;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(34, 153, 84);
                    break;
                case UiButtonStyle.Danger:
                    button.BackColor = Danger;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(192, 57, 43);
                    break;
                case UiButtonStyle.Warning:
                    button.BackColor = Warning;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(211, 132, 18);
                    break;
                case UiButtonStyle.Secondary:
                    button.BackColor = Color.FromArgb(189, 195, 199);
                    button.ForeColor = Color.FromArgb(44, 62, 80);
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(149, 165, 166);
                    break;
                case UiButtonStyle.Accent:
                    button.BackColor = Color.FromArgb(155, 89, 182);
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = Color.FromArgb(142, 68, 173);
                    break;
                default:
                    button.BackColor = Primary;
                    button.ForeColor = Color.White;
                    button.FlatAppearance.MouseOverBackColor = PrimaryDark;
                    break;
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
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(44, 62, 80);
            button.FlatAppearance.MouseDownBackColor = PrimaryDark;
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
            button.FlatAppearance.MouseOverBackColor = PrimaryDark;
        }

        public static void StyleTextBox(TextBox textBox)
        {
            if (textBox == null) return;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = Color.White;
            textBox.ForeColor = GridHeaderText;
            textBox.Font = UiFont;
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
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 230, 250);
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
                btn.BackColor = Color.FromArgb(232, 242, 252);
                btn.ForeColor = PrimaryDark;
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
