using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal sealed class ReportPreviewDialog : Form
    {
        private ReportPreviewDialog(DataTable table, string title, string subtitle)
        {
            Text = "Report Preview";
            StartPosition = FormStartPosition.CenterParent;
            Width = 960;
            Height = 620;
            MinimumSize = new Size(720, 480);
            MinimizeBox = false;
            MaximizeBox = true;
            FormBorderStyle = FormBorderStyle.Sizable;
            ShowInTaskbar = false;
            BackColor = UiTheme.AdminSurface;
            Font = UiTheme.UiFont;

            var panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = subtitle != null ? 72 : 56,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = Color.FromArgb(238, 245, 244)
            };

            var lblTitle = new Label
            {
                Text = title ?? "Report",
                AutoSize = true,
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                Location = new Point(16, 12)
            };
            panelHeader.Controls.Add(lblTitle);

            if (!string.IsNullOrWhiteSpace(subtitle))
            {
                var lblSubtitle = new Label
                {
                    Text = subtitle,
                    AutoSize = true,
                    MaximumSize = new Size(880, 40),
                    Font = UiTheme.UiFont,
                    ForeColor = UiTheme.AdminMuted,
                    Location = new Point(16, 36)
                };
                panelHeader.Controls.Add(lblSubtitle);
            }

            var panelGridOuter = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = UiTheme.AdminSurface
            };

            var panelGridInner = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(1)
            };

            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BorderStyle = BorderStyle.None,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = Color.White
            };
            UiTheme.ApplyClinicalGrid(grid);
            grid.AutoGenerateColumns = true;
            UiTheme.SetGridDataSource(grid, table ?? new DataTable());
            UiTheme.BeautifyGridHeaders(grid);

            panelGridInner.Controls.Add(grid);
            panelGridOuter.Controls.Add(panelGridInner);

            var panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                Padding = new Padding(16, 8, 16, 12),
                BackColor = UiTheme.AdminSurface
            };

            var rowCount = table?.Rows.Count ?? 0;
            var lblStatus = new Label
            {
                Text = rowCount == 0
                    ? "No records found for the selected filters."
                    : $"{rowCount:N0} record{(rowCount == 1 ? string.Empty : "s")}",
                AutoSize = true,
                ForeColor = UiTheme.AdminMuted,
                Font = UiTheme.UiFont,
                Location = new Point(16, 14)
            };

            var btnClose = AdminUiHelpers.CreateWinButton("Close", true, 96);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.Location = new Point(panelFooter.Width - 112, 8);
            btnClose.DialogResult = DialogResult.OK;
            panelFooter.Resize += (s, e) => btnClose.Left = panelFooter.ClientSize.Width - btnClose.Width - 16;

            panelFooter.Controls.Add(lblStatus);
            panelFooter.Controls.Add(btnClose);

            Controls.Add(panelGridOuter);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);

            AcceptButton = btnClose;
            CancelButton = btnClose;
        }

        public static void Show(IWin32Window owner, DataTable table, string title, string subtitle = null)
        {
            using (var dlg = new ReportPreviewDialog(table, title, subtitle))
            {
                if (owner != null)
                    dlg.ShowDialog(owner);
                else
                    dlg.ShowDialog();
            }
        }
    }
}
