using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    /// <summary>
    /// A drop-in replacement for <see cref="MessageBox"/> that renders with the
    /// Hanken Grotesk font and the SmartMed clinical colour palette.
    /// </summary>
    public static class SmartMedMessageBox
    {
        public static DialogResult Show(string text)
            => ShowCore(null, text, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.None);

        public static DialogResult Show(string text, string caption)
            => ShowCore(null, text, caption, MessageBoxButtons.OK, MessageBoxIcon.None);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
            => ShowCore(null, text, caption, buttons, MessageBoxIcon.None);

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => ShowCore(null, text, caption, buttons, icon);

        public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            => ShowCore(owner, text, caption, buttons, icon);

        private static DialogResult ShowCore(
            IWin32Window owner,
            string text,
            string caption,
            MessageBoxButtons buttons,
            MessageBoxIcon icon)
        {
            using (var dlg = new ThemedMessageForm(text, caption, buttons, icon))
            {
                return owner != null ? dlg.ShowDialog(owner) : dlg.ShowDialog();
            }
        }

        private sealed class ThemedMessageForm : Form
        {
            private DialogResult _result = DialogResult.Cancel;

            public ThemedMessageForm(string body, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
            {
                Text = caption ?? string.Empty;
                FormBorderStyle = FormBorderStyle.FixedDialog;
                StartPosition = FormStartPosition.CenterParent;
                MaximizeBox = false;
                MinimizeBox = false;
                ShowInTaskbar = false;
                BackColor = UiTheme.AdminSurface;
                Font = UiTheme.UiFont;
                Padding = new Padding(24);

                var iconPanel = CreateIconPanel(icon);
                var lblBody = new Label
                {
                    Text = body ?? string.Empty,
                    Font = UiTheme.UiFont,
                    ForeColor = UiTheme.AdminOnSurface,
                    AutoSize = true,
                    MaximumSize = new Size(360, 0),
                    Padding = new Padding(0, 4, 0, 4)
                };

                var contentPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.LeftToRight,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    WrapContents = false,
                    Padding = new Padding(24, 20, 24, 12),
                    BackColor = UiTheme.AdminSurface
                };

                if (iconPanel != null)
                    contentPanel.Controls.Add(iconPanel);
                contentPanel.Controls.Add(lblBody);

                var buttonPanel = new FlowLayoutPanel
                {
                    FlowDirection = FlowDirection.RightToLeft,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    WrapContents = false,
                    Padding = new Padding(16, 8, 16, 16),
                    Dock = DockStyle.Bottom,
                    BackColor = UiTheme.AdminSurface
                };

                AddButtons(buttonPanel, buttons);

                contentPanel.Dock = DockStyle.Fill;
                Controls.Add(contentPanel);
                Controls.Add(buttonPanel);

                // Size the form
                int w = Math.Max(contentPanel.PreferredSize.Width, buttonPanel.PreferredSize.Width) + 16;
                int h = contentPanel.PreferredSize.Height + buttonPanel.PreferredSize.Height + 16;
                ClientSize = new Size(Math.Max(320, Math.Min(w, 500)), Math.Max(150, Math.Min(h, 400)));
            }

            protected override void OnShown(EventArgs e)
            {
                base.OnShown(e);
                // Re-measure after layout
                int w = Math.Max(320, Math.Min(PreferredSize.Width + 16, 500));
                int h = Math.Max(150, Math.Min(PreferredSize.Height + 16, 400));
                ClientSize = new Size(w, h);
            }

            private Panel CreateIconPanel(MessageBoxIcon icon)
            {
                if (icon == MessageBoxIcon.None) return null;

                Color colour;
                string symbol;
                switch (icon)
                {
                    case MessageBoxIcon.Error:
                        colour = UiTheme.Error;
                        symbol = "\u2716";
                        break;
                    case MessageBoxIcon.Warning:
                        colour = Color.FromArgb(202, 138, 4);
                        symbol = "\u26A0";
                        break;
                    case MessageBoxIcon.Information:
                        colour = UiTheme.AdminTeal;
                        symbol = "\u2139";
                        break;
                    case MessageBoxIcon.Question:
                        colour = Color.FromArgb(59, 130, 246);
                        symbol = "?";
                        break;
                    default:
                        colour = UiTheme.AdminMuted;
                        symbol = "\u2139";
                        break;
                }

                var panel = new Panel
                {
                    Size = new Size(40, 40),
                    Margin = new Padding(0, 4, 12, 0),
                    BackColor = Color.Transparent
                };
                panel.Paint += (s, e) =>
                {
                    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var brush = new SolidBrush(Color.FromArgb(30, colour)))
                        e.Graphics.FillEllipse(brush, 0, 0, 39, 39);
                    TextRenderer.DrawText(e.Graphics, symbol, UiTheme.UiFontBold,
                        panel.ClientRectangle, colour,
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
                };
                return panel;
            }

            private void AddButtons(FlowLayoutPanel panel, MessageBoxButtons buttons)
            {
                switch (buttons)
                {
                    case MessageBoxButtons.OK:
                        panel.Controls.Add(MakeButton("OK", DialogResult.OK, primary: true));
                        AcceptButton = (Button)panel.Controls[0];
                        break;
                    case MessageBoxButtons.OKCancel:
                        panel.Controls.Add(MakeButton("Cancel", DialogResult.Cancel, primary: false));
                        panel.Controls.Add(MakeButton("OK", DialogResult.OK, primary: true));
                        AcceptButton = (Button)panel.Controls[1];
                        CancelButton = (Button)panel.Controls[0];
                        break;
                    case MessageBoxButtons.YesNo:
                        panel.Controls.Add(MakeButton("No", DialogResult.No, primary: false));
                        panel.Controls.Add(MakeButton("Yes", DialogResult.Yes, primary: true));
                        AcceptButton = (Button)panel.Controls[1];
                        CancelButton = (Button)panel.Controls[0];
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        panel.Controls.Add(MakeButton("Cancel", DialogResult.Cancel, primary: false));
                        panel.Controls.Add(MakeButton("No", DialogResult.No, primary: false));
                        panel.Controls.Add(MakeButton("Yes", DialogResult.Yes, primary: true));
                        AcceptButton = (Button)panel.Controls[2];
                        CancelButton = (Button)panel.Controls[0];
                        break;
                    case MessageBoxButtons.RetryCancel:
                        panel.Controls.Add(MakeButton("Cancel", DialogResult.Cancel, primary: false));
                        panel.Controls.Add(MakeButton("Retry", DialogResult.Retry, primary: true));
                        AcceptButton = (Button)panel.Controls[1];
                        CancelButton = (Button)panel.Controls[0];
                        break;
                }
            }

            private Button MakeButton(string text, DialogResult result, bool primary)
            {
                var btn = new Button
                {
                    Text = text,
                    DialogResult = result,
                    Font = UiTheme.UiFontSemibold ?? UiTheme.UiFont,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(100, 36),
                    Margin = new Padding(6, 0, 6, 0),
                    Cursor = Cursors.Hand
                };

                if (primary)
                {
                    btn.BackColor = UiTheme.AdminTeal;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.FlatAppearance.MouseOverBackColor = UiTheme.AdminTealDark;
                }
                else
                {
                    btn.BackColor = UiTheme.AdminSurface;
                    btn.ForeColor = UiTheme.AdminOnSurface;
                    btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                    btn.FlatAppearance.BorderSize = 1;
                    btn.FlatAppearance.MouseOverBackColor = UiTheme.SurfaceContainerHigh;
                }

                btn.Click += (s, e) =>
                {
                    _result = result;
                    DialogResult = result;
                };

                return btn;
            }
        }
    }
}
