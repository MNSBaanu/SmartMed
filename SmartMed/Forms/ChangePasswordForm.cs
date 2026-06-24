using System;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public class ChangePasswordForm : Form
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
            ClientSize = new System.Drawing.Size(360, 220);

            var lblCurrent = new Label { Text = "Current Password:", Left = 16, Top = 20, AutoSize = true };
            txtCurrent = new TextBox { Left = 16, Top = 40, Width = 320, UseSystemPasswordChar = true };

            var lblNew = new Label { Text = "New Password:", Left = 16, Top = 72, AutoSize = true };
            txtNew = new TextBox { Left = 16, Top = 92, Width = 320, UseSystemPasswordChar = true };

            var lblConfirm = new Label { Text = "Confirm Password:", Left = 16, Top = 124, AutoSize = true };
            txtConfirm = new TextBox { Left = 16, Top = 144, Width = 320, UseSystemPasswordChar = true };

            var btnSave = new Button { Text = "Save", Left = 168, Top = 176, Width = 80, DialogResult = DialogResult.None };
            var btnCancel = new Button { Text = "Cancel", Left = 256, Top = 176, Width = 80, DialogResult = DialogResult.Cancel };
            btnSave.Click += BtnSave_Click;

            Controls.AddRange(new Control[]
            {
                lblCurrent, txtCurrent, lblNew, txtNew, lblConfirm, txtConfirm, btnSave, btnCancel
            });
            AcceptButton = btnSave;
            CancelButton = btnCancel;

            Font = UiTheme.UiFont;
            UiTheme.ApplyFontTree(this);
            UiTheme.ApplyFlatButton(btnSave, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnCancel, UiButtonStyle.Secondary);
            UiTheme.StyleTextBox(txtCurrent);
            UiTheme.StyleTextBox(txtNew);
            UiTheme.StyleTextBox(txtConfirm);
        }

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
