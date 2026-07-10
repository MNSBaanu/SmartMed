using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SmartMed.UI
{
    internal static class ExpiryAlertsDialog
    {
        public static void Show(IWin32Window owner, IList<string> alertLines)
        {
            if (alertLines == null || alertLines.Count == 0)
            {
                MessageBox.Show(owner, "No expiry alerts. All medicines are within safe expiry dates.",
                    "Expiry Alerts", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new Form
            {
                Text = "Expiry Alerts",
                StartPosition = FormStartPosition.CenterParent,
                Width = 520,
                Height = 420,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var list = new ListBox
                {
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle,
                    IntegralHeight = false,
                    Font = UiTheme.UiFont
                };
                list.Items.AddRange(alertLines as object[] ?? alertLines.ToArray());

                var btnClose = AdminUiHelpers.CreateWinButton("Close", false, 88);
                btnClose.Dock = DockStyle.Bottom;
                btnClose.Height = 36;
                btnClose.DialogResult = DialogResult.OK;
                dlg.Controls.Add(btnClose);
                dlg.Controls.Add(list);
                dlg.AcceptButton = btnClose;

                if (owner != null)
                    dlg.ShowDialog(owner);
                else
                    dlg.ShowDialog();
            }
        }
    }
}
