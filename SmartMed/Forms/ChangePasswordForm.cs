using System;
using System.Drawing;
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
            ClientSize = new Size(392, 252);
            Padding = new Padding(24);

            var body = new Panel { Dock = DockStyle.Fill, AutoSize = true };

            var lblCurrent = new Label { Text = "Current Password:", Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 0, 0, 4) };
            txtCurrent = new TextBox { Dock = DockStyle.Top, Margin = UiTheme.CustomerSectionMargin };

            var lblNew = new Label { Text = "New Password:", Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 0, 0, 4) };
            txtNew = new TextBox { Dock = DockStyle.Top, Margin = UiTheme.CustomerSectionMargin };

            var lblConfirm = new Label { Text = "Confirm Password:", Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, 0, 0, 4) };
            txtConfirm = new TextBox { Dock = DockStyle.Top, Margin = UiTheme.CustomerSectionMargin };

            var buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0)
            };
            var btnSave = new Button { Text = "Save", Width = 80, DialogResult = DialogResult.None, Margin = UiTheme.CustomerControlMargin };
            var btnCancel = new Button { Text = "Cancel", Width = 80, DialogResult = DialogResult.Cancel, Margin = UiTheme.CustomerControlMargin };
            btnSave.Click += BtnSave_Click;
            buttons.Controls.Add(btnCancel);
            buttons.Controls.Add(btnSave);

            // Dock.Top stacks bottom-up — add in reverse visual order.
            body.Controls.Add(txtConfirm);
            body.Controls.Add(lblConfirm);
            body.Controls.Add(txtNew);
            body.Controls.Add(lblNew);
            body.Controls.Add(txtCurrent);
            body.Controls.Add(lblCurrent);

            Controls.Add(buttons);
            Controls.Add(body);
            AcceptButton = btnSave;
            CancelButton = btnCancel;

            Font = UiTheme.UiFont;
            UiTheme.ApplyFontTree(this);
            UiTheme.ApplyFlatButton(btnSave, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnCancel, UiButtonStyle.Secondary);
            UiTheme.StylePasswordBox(txtCurrent);
            UiTheme.StylePasswordBox(txtNew);
            UiTheme.StylePasswordBox(txtConfirm);
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
