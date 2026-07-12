using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class CancelReasonDialog
    {
        public static bool TryGetReason(IWin32Window owner, out string reason)
        {
            reason = null;
            string captured = null;

            using (var dlg = new Form
            {
                Text = "Cancel Order",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(420, 220),
                MaximizeBox = false,
                MinimizeBox = false,
                ShowInTaskbar = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var lbl = new Label
                {
                    Text = "Please enter a reason for cancelling this order:",
                    Left = 16,
                    Top = 16,
                    Width = 380,
                    AutoSize = false,
                    Height = 36,
                    ForeColor = UiTheme.AdminOnSurface,
                    BackColor = UiTheme.AdminSurface
                };

                var txt = new TextBox
                {
                    Left = 16,
                    Top = 56,
                    Width = 380,
                    Height = 88,
                    Multiline = true,
                    ScrollBars = ScrollBars.Vertical,
                    MaxLength = 500
                };
                UiTheme.StyleTextBox(txt);

                var btnOk = AdminUiHelpers.CreateWinButton("Cancel Order", true, 110);
                btnOk.Left = 186;
                btnOk.Top = 164;
                var btnCancel = AdminUiHelpers.CreateWinButton("Keep Order", false, 100);
                btnCancel.Left = 304;
                btnCancel.Top = 164;
                btnCancel.DialogResult = DialogResult.Cancel;

                btnOk.Click += (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(txt.Text))
                    {
                        MessageBox.Show(dlg, "A cancellation reason is required.", "Cancel Order",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    captured = txt.Text.Trim();
                    dlg.DialogResult = DialogResult.OK;
                    dlg.Close();
                };

                dlg.Controls.Add(lbl);
                dlg.Controls.Add(txt);
                dlg.Controls.Add(btnOk);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnOk;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(owner) != DialogResult.OK || string.IsNullOrWhiteSpace(captured))
                    return false;

                reason = captured;
                return true;
            }
        }
    }
}
