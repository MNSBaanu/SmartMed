using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    public static class AdminUiHelpers
    {
        public static Panel CreatePageHeader(string title, string subtitle, Action<FlowLayoutPanel> addActions = null)
        {
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
                Font = UiTheme.FontAt(20f, bold: true),
                ForeColor = UiTheme.PrimaryDark,
                Location = new Point(0, 8),
                AutoSize = true,
                BackColor = UiTheme.AdminSurface
            });
            header.Controls.Add(new Label
            {
                Text = subtitle ?? string.Empty,
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Location = new Point(0, 44),
                AutoSize = true,
                Visible = !string.IsNullOrWhiteSpace(subtitle),
                BackColor = UiTheme.AdminSurface
            });

            if (addActions != null)
            {
                var actions = new FlowLayoutPanel
                {
                    Dock = DockStyle.Right,
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    WrapContents = false,
                    Padding = new Padding(0, 16, 0, 0),
                    BackColor = UiTheme.AdminSurface
                };
                addActions(actions);
                header.Controls.Add(actions);
            }

            return header;
        }

        public static Panel CreateStatCard(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 14, 0),
                Padding = new Padding(16, 14, 14, 14),
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
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                Dock = DockStyle.Top,
                Height = 16,
                BackColor = Color.White
            });

            valueLabel.Text = "0";
            valueLabel.Font = UiTheme.FontAt(22f, bold: true);
            valueLabel.ForeColor = UiTheme.PrimaryDark;
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            valueLabel.BackColor = Color.White;
            card.Controls.Add(valueLabel);
            return card;
        }

        public static Panel CreateSectionPanel(string title, Control body)
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
                Height = 36,
                BackColor = UiTheme.AdminSidebar,
                Padding = new Padding(12, 8, 10, 4)
            };
            header.Controls.Add(new Label
            {
                Text = title,
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.AdminOnSurface,
                Dock = DockStyle.Left,
                AutoSize = true,
                BackColor = UiTheme.AdminSidebar
            });

            if (body != null)
            {
                body.Dock = DockStyle.Fill;
                var bodyWrap = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 4, 0, 0), BackColor = Color.White };
                bodyWrap.Controls.Add(body);
                outer.Controls.Add(bodyWrap);
            }

            outer.Controls.Add(header);
            return outer;
        }

        public static Button CreateWinButton(string text, bool primary, int width = 96, int height = 30)
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
                btn.BackColor = Color.FromArgb(238, 245, 244);
                btn.ForeColor = UiTheme.AdminOnSurface;
                btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(227, 234, 233);
            }
            btn.UseVisualStyleBackColor = false;
            return btn;
        }
    }
}
