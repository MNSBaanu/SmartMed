using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed class ChangePasswordForm : Form
    {
        private readonly bool _isAdmin;
        private readonly TextBox txtCurrent;
        private readonly TextBox txtNew;
        private readonly TextBox txtConfirm;

        public ChangePasswordForm(bool isAdmin)
        {
            _isAdmin = isAdmin;
            Text = "Change Password";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(392, 252);
            BackColor = UiTheme.AdminSurface;
            Font = UiTheme.UiFont;

            var body = new Panel { Dock = DockStyle.Fill, AutoSize = true, BackColor = UiTheme.AdminSurface };

            txtCurrent = new TextBox { Dock = DockStyle.Top, Margin = new Padding(0, 0, 0, 8) };
            txtNew = new TextBox { Dock = DockStyle.Top, Margin = new Padding(0, 0, 0, 8) };
            txtConfirm = new TextBox { Dock = DockStyle.Top, Margin = new Padding(0, 0, 0, 8) };

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = UiTheme.AdminSurface
            };
            var btnSave = new Button { Text = "Save", Width = 80, DialogResult = DialogResult.None, Margin = new Padding(8, 0, 0, 0) };
            var btnCancel = new Button { Text = "Cancel", Width = 80, DialogResult = DialogResult.Cancel };
            btnSave.Click += BtnSave_Click;
            buttons.Controls.Add(btnCancel);
            buttons.Controls.Add(btnSave);

            body.Controls.Add(txtConfirm);
            body.Controls.Add(MakeLabel("Confirm Password:"));
            body.Controls.Add(txtNew);
            body.Controls.Add(MakeLabel("New Password:"));
            body.Controls.Add(txtCurrent);
            body.Controls.Add(MakeLabel("Current Password:"));

            Controls.Add(buttons);
            Controls.Add(body);
            AcceptButton = btnSave;
            CancelButton = btnCancel;

            UiTheme.ApplyFlatButton(btnSave, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnCancel, UiButtonStyle.Secondary);
            UiTheme.StylePasswordBox(txtCurrent, masked: true);
            UiTheme.StylePasswordBox(txtNew, masked: true);
            UiTheme.StylePasswordBox(txtConfirm, masked: true);
        }

        private static Label MakeLabel(string text) =>
            new Label
            {
                Text = text,
                Dock = DockStyle.Top,
                AutoSize = true,
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface,
                Margin = new Padding(0, 0, 0, 4)
            };

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("New passwords do not match.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var auth = new AuthService();
                var customers = new CustomerService();

                if (_isAdmin)
                {
                    if (Session.CurrentAdmin == null)
                        throw new InvalidOperationException("Admin session expired.");
                    auth.ChangeAdminPassword(Session.CurrentAdmin.AdminID, txtCurrent.Text, txtNew.Text);
                    Session.CurrentAdmin.Password = txtNew.Text;
                }
                else
                {
                    if (Session.CurrentCustomer == null)
                        throw new InvalidOperationException("Customer session expired.");
                    customers.ChangePassword(Session.CurrentCustomer.CustomerID, txtCurrent.Text, txtNew.Text);
                    Session.CurrentCustomer.Password = txtNew.Text;
                }

                MessageBox.Show("Password updated successfully.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
