using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    public sealed class ClinicalPageHeader
    {
        public Panel Panel { get; internal set; }
        public Label SubtitleLabel { get; internal set; }
    }

    /// <summary>Shared clinical WinForms chrome for admin and customer pages.</summary>
    public static class ClinicalUi
    {
        public static void StylePagePanel(Panel pagePanel) => PreparePagePanel(pagePanel);

        public static void PreparePagePanel(Panel pagePanel)
        {
            if (pagePanel == null) return;
            pagePanel.BackColor = UiTheme.AdminSurface;
        }

        public static Panel CreatePageHeader(string title, string subtitle, Action<FlowLayoutPanel> addActions = null)
            => CreatePageHeaderBlock(title, subtitle, addActions).Panel;

        public static ClinicalPageHeader CreatePageHeaderBlock(string title, string subtitle, Action<FlowLayoutPanel> addActions = null)
        {
            var subtitleLabel = new Label
            {
                Text = subtitle ?? string.Empty,
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Location = new Point(0, 44),
                AutoSize = true,
                Visible = !string.IsNullOrWhiteSpace(subtitle)
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = string.IsNullOrWhiteSpace(subtitle) ? 56 : 76,
                Margin = new Padding(0, 0, 0, 20),
                BackColor = UiTheme.AdminSurface
            };

            header.Controls.Add(new Label
            {
                Text = title,
                Font = new Font(UiTheme.UiFont.FontFamily, 20f, FontStyle.Bold),
                ForeColor = UiTheme.AdminOnSurface,
                Location = new Point(0, 8),
                AutoSize = true
            });
            header.Controls.Add(subtitleLabel);

            if (addActions != null)
            {
                var actions = new FlowLayoutPanel
                {
                    Dock = DockStyle.Right,
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    Padding = new Padding(0, 16, 0, 0)
                };
                addActions(actions);
                header.Controls.Add(actions);
            }

            return new ClinicalPageHeader { Panel = header, SubtitleLabel = subtitleLabel };
        }

        public static Label CreateSectionHeading(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font(UiTheme.UiFont, FontStyle.Bold),
                ForeColor = UiTheme.AdminOnSurface,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = UiTheme.AdminSurface
            };
        }

        public static Label StyleFieldLabel(Label label)
        {
            if (label == null) return null;
            label.Font = UiTheme.UiFont;
            label.ForeColor = UiTheme.AdminOnSurface;
            label.BackColor = UiTheme.AdminSurface;
            return label;
        }

        public static Label StyleFieldLabel(string text) => StyleFieldLabel(new Label { Text = text, AutoSize = true });

        public static Label StyleMutedLabel(Label label)
        {
            if (label == null) return null;
            label.Font = UiTheme.UiFont;
            label.ForeColor = UiTheme.AdminMuted;
            label.BackColor = UiTheme.AdminSurface;
            return label;
        }

        public static Label StyleMutedLabel(string text) => StyleMutedLabel(new Label { Text = text, AutoSize = true });

        public static Panel CreateStatCard(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 14, 0),
                Padding = new Padding(18, 14, 14, 14),
                BackColor = Color.White
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
                using (var brush = new SolidBrush(accent))
                    e.Graphics.FillRectangle(brush, 0, 0, 4, rect.Height);
            };

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = new Font(UiTheme.UiFont.FontFamily, 8.5f, FontStyle.Bold),
                ForeColor = UiTheme.AdminMuted,
                Dock = DockStyle.Top,
                Height = 16
            });

            valueLabel.Text = "0";
            valueLabel.Font = new Font(UiTheme.UiFont.FontFamily, 20f, FontStyle.Bold);
            valueLabel.ForeColor = UiTheme.AdminOnSurface;
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            card.Controls.Add(valueLabel);
            return card;
        }

        public static Panel CreateSectionPanel(string title, Control body, Action<FlowLayoutPanel> headerActions = null)
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = UiTheme.AdminSidebar,
                Padding = new Padding(14, 10, 10, 6)
            };
            header.Controls.Add(new Label
            {
                Text = title,
                Font = new Font(UiTheme.UiFont, FontStyle.Bold),
                ForeColor = UiTheme.AdminOnSurface,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            if (headerActions != null)
            {
                var actions = new FlowLayoutPanel
                {
                    Dock = DockStyle.Right,
                    AutoSize = true,
                    FlowDirection = FlowDirection.LeftToRight
                };
                headerActions(actions);
                header.Controls.Add(actions);
            }

            if (body != null)
            {
                body.Dock = DockStyle.Fill;
                var bodyWrap = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 4, 0, 0) };
                bodyWrap.Controls.Add(body);
                outer.Controls.Add(bodyWrap);
            }

            outer.Controls.Add(header);
            return outer;
        }

        public static Button CreateWinButton(string text, bool primary, int width = 96, int height = 34)
            => CreateButton(text, primary, width, height);

        public static Button CreateButton(string text, bool primary = false, int width = 96, int height = 34)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = height,
                FlatStyle = FlatStyle.Flat,
                Font = UiTheme.UiFont,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 10, 0)
            };
            btn.FlatAppearance.BorderSize = 1;
            if (primary)
            {
                btn.BackColor = UiTheme.AdminTeal;
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = UiTheme.AdminTealDark;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 109, 100);
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = UiTheme.AdminOnSurface;
                btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(245, 250, 249);
            }
            btn.UseVisualStyleBackColor = false;
            return btn;
        }

        public static DataGridView CreateGrid()
        {
            var grid = new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ScrollBars = ScrollBars.Vertical,
                BorderStyle = BorderStyle.None,
                BackgroundColor = Color.White
            };
            UiTheme.ApplyClinicalGrid(grid);
            return grid;
        }

        public static void BindGrid(DataGridView grid, IEnumerable data)
        {
            if (grid == null) return;
            grid.DataSource = data;
            UiTheme.BeautifyGridHeaders(grid);
        }

        public static Panel CreateSearchBox(out TextBox textBox, string hint = "Search...")
        {
            var box = new TextBox { BorderStyle = BorderStyle.None, Margin = new Padding(8, 0, 8, 0) };
            UiTheme.StyleTextBox(box);
            textBox = box;

            var wrap = new Panel
            {
                Width = 200,
                Height = 34,
                Margin = new Padding(0, 0, 10, 0),
                BackColor = Color.White
            };
            wrap.Paint += (s, e) =>
            {
                var rect = wrap.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            box.Dock = DockStyle.Fill;
            wrap.Controls.Add(box);

            var hintLabel = new Label
            {
                Text = hint,
                ForeColor = UiTheme.AdminMuted,
                BackColor = Color.White,
                Bounds = new Rectangle(10, 0, 170, 34),
                TextAlign = ContentAlignment.MiddleLeft,
                Cursor = Cursors.IBeam
            };
            wrap.Controls.Add(hintLabel);
            hintLabel.BringToFront();
            box.GotFocus += (s, e) => hintLabel.Visible = false;
            box.LostFocus += (s, e) => hintLabel.Visible = string.IsNullOrEmpty(box.Text);
            wrap.Click += (s, e) => box.Focus();
            hintLabel.Click += (s, e) => box.Focus();
            return wrap;
        }

        public static Panel CreateStatusFooter(string leftText, Label timeLabel)
        {
            var bar = new Panel
            {
                Height = 32,
                BackColor = UiTheme.AdminTeal,
                Padding = new Padding(16, 0, 16, 0),
                Dock = DockStyle.Bottom
            };
            bar.Controls.Add(new Label
            {
                Text = leftText,
                ForeColor = Color.White,
                Font = UiTheme.UiFont,
                Dock = DockStyle.Left,
                AutoSize = true,
                Padding = new Padding(0, 8, 0, 0)
            });
            timeLabel.ForeColor = Color.White;
            timeLabel.Font = UiTheme.UiFont;
            timeLabel.Dock = DockStyle.Right;
            timeLabel.AutoSize = true;
            timeLabel.Padding = new Padding(0, 8, 0, 0);
            bar.Controls.Add(timeLabel);
            return bar;
        }
    }
}
