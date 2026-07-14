using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class PrescriptionFilesDialog
    {
        public static void Open(IWin32Window owner, IReadOnlyList<string> filePaths)
        {
            var paths = (filePaths ?? Array.Empty<string>())
                .Where(p => !string.IsNullOrWhiteSpace(p))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            if (paths.Count == 0)
            {
                SmartMedMessageBox.Show(owner, "No prescription file for this order.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (paths.Count == 1)
            {
                TryOpenPath(owner, paths[0]);
                return;
            }

            using (var dlg = new Form
            {
                Text = "Prescriptions",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(420, 260),
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var lbl = new Label
                {
                    Text = $"{paths.Count} prescription files — select one to open:",
                    Left = 16,
                    Top = 16,
                    Width = 380,
                    AutoSize = false,
                    Height = 28,
                    ForeColor = UiTheme.AdminOnSurface,
                    BackColor = UiTheme.AdminSurface
                };

                var list = new ListBox
                {
                    Left = 16,
                    Top = 48,
                    Width = 380,
                    Height = 148,
                    DisplayMember = "Name",
                    ValueMember = "Path"
                };
                list.Items.AddRange(paths.Select(p => new FileEntry
                {
                    Name = Path.GetFileName(p),
                    Path = p
                }).Cast<object>().ToArray());
                if (list.Items.Count > 0)
                    list.SelectedIndex = 0;

                var btnOpen = AdminUiHelpers.CreateWinButton("Open", true, 90);
                btnOpen.Left = 206;
                btnOpen.Top = 212;
                var btnClose = AdminUiHelpers.CreateWinButton("Close", false, 90);
                btnClose.Left = 306;
                btnClose.Top = 212;
                btnClose.DialogResult = DialogResult.Cancel;

                void OpenSelected()
                {
                    if (!(list.SelectedItem is FileEntry entry))
                        return;
                    TryOpenPath(dlg, entry.Path);
                }

                btnOpen.Click += (s, e) => OpenSelected();
                list.DoubleClick += (s, e) => OpenSelected();

                dlg.Controls.Add(lbl);
                dlg.Controls.Add(list);
                dlg.Controls.Add(btnOpen);
                dlg.Controls.Add(btnClose);
                dlg.AcceptButton = btnOpen;
                dlg.CancelButton = btnClose;
                dlg.ShowDialog(owner);
            }
        }

        private static void TryOpenPath(IWin32Window owner, string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                SmartMedMessageBox.Show(owner, "Prescription file is not available.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Process.Start(filePath);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(owner, ex.Message, "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private sealed class FileEntry
        {
            public string Name { get; set; }
            public string Path { get; set; }
        }
    }
}
