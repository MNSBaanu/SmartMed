using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ProfileManagementForm : CustomerPageControl
    {
        private readonly CustomerService _customers = new CustomerService();

        private TextBox _txtName;
        private TextBox _txtEmail;
        private TextBox _txtPhone;
        private TextBox _txtAddress;

        public ProfileManagementForm()
        {
            InitializeComponent();
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage() => LoadProfile();

        private void BuildContent()
        {
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 1,
                MinimumSize = new Size(0, 420)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            root.Controls.Add(AdminUiHelpers.CreatePageHeader("My Profile",
                "Update your contact details and password."));

            root.Controls.Add(new Label
            {
                Text = "PERSONAL DETAILS",
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 4),
                BackColor = UiTheme.AdminSurface
            });

            AddFieldRow(root, ValidationService.RequiredLabel("Full Name"), out _txtName, first: true);
            AddFieldRow(root, ValidationService.RequiredLabel("Email"), out _txtEmail);
            AddFieldRow(root, ValidationService.RequiredLabel("Phone"), out _txtPhone);
            _txtPhone.MaxLength = 14;
            AddFieldRow(root, ValidationService.RequiredLabel("Address"), out _txtAddress, multiline: true);

            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                Margin = new Padding(0, 16, 0, 0),
                BackColor = UiTheme.AdminSurface
            };

            var btnSave = AdminUiHelpers.CreateWinButton("Save Profile", primary: true, width: 120, height: 32);
            btnSave.Click += BtnSave_Click;
            var btnPassword = AdminUiHelpers.CreateWinButton("Change Password", primary: false, width: 140, height: 32);
            btnPassword.Click += (s, e) => ShowChangePasswordDialog();

            actions.Controls.Add(btnSave);
            actions.Controls.Add(btnPassword);
            root.Controls.Add(actions);

            WireScrollRoot(root);
        }

        private static void AddFieldRow(TableLayoutPanel root, string labelText, out TextBox textBox,
            bool first = false, bool multiline = false)
        {
            var label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Margin = new Padding(0, first ? 8 : 16, 0, 4),
                ForeColor = UiTheme.AdminLabelText,
                BackColor = UiTheme.AdminSurface
            };
            root.Controls.Add(label);

            textBox = new TextBox
            {
                Dock = DockStyle.Top,
                Height = multiline ? 52 : 32,
                Multiline = multiline,
                ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None
            };
            UiTheme.StyleTextBox(textBox);
            root.Controls.Add(textBox);
        }

        private void LoadProfile()
        {
            SyncScrollRootWidth();
            var customer = Session.CurrentCustomer;
            if (customer == null || _txtName == null) return;

            _txtName.Text = customer.Name;
            _txtEmail.Text = customer.Email;
            _txtPhone.Text = customer.Phone;
            _txtAddress.Text = customer.Address;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var updated = new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = _txtName.Text.Trim(),
                    Email = _txtEmail.Text.Trim(),
                    Phone = _txtPhone.Text.Trim(),
                    Address = _txtAddress.Text.Trim(),
                    Password = customer.Password
                };
                _customers.UpdateProfile(updated);
                Session.CurrentCustomer = _customers.GetById(customer.CustomerID);
                (FindForm() as CustomerHostForm)?.RefreshProfileDisplay();

                MessageBox.Show("Profile updated.", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Profile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowChangePasswordDialog()
        {
            using (var dlg = new Form
            {
                Text = "Change Password",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(360, 240),
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var lblCurrent = new Label { Text = "Current Password:", Location = new Point(12, 16), AutoSize = true };
                var txtCurrent = new TextBox
                {
                    Location = new Point(12, 36),
                    Width = 330,
                    UseSystemPasswordChar = true
                };
                UiTheme.StyleTextBox(txtCurrent);

                var lblNew = new Label { Text = "New Password:", Location = new Point(12, 72), AutoSize = true };
                var txtNew = new TextBox
                {
                    Location = new Point(12, 92),
                    Width = 330,
                    UseSystemPasswordChar = true
                };
                UiTheme.StyleTextBox(txtNew);

                var lblConfirm = new Label { Text = "Confirm Password:", Location = new Point(12, 128), AutoSize = true };
                var txtConfirm = new TextBox
                {
                    Location = new Point(12, 148),
                    Width = 330,
                    UseSystemPasswordChar = true
                };
                UiTheme.StyleTextBox(txtConfirm);

                var btnSave = AdminUiHelpers.CreateWinButton("Save", primary: true, width: 80, height: 30);
                btnSave.Location = new Point(180, 176);
                btnSave.DialogResult = DialogResult.OK;
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", primary: false, width: 80, height: 30);
                btnCancel.Location = new Point(266, 176);
                btnCancel.DialogResult = DialogResult.Cancel;

                dlg.Controls.AddRange(new Control[]
                {
                    lblCurrent, txtCurrent, lblNew, txtNew, lblConfirm, txtConfirm, btnSave, btnCancel
                });
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(FindForm()) != DialogResult.OK)
                    return;

                try
                {
                    if (!string.Equals(txtNew.Text, txtConfirm.Text, StringComparison.Ordinal))
                        throw new ArgumentException("New passwords do not match.");

                    var customer = Session.CurrentCustomer;
                    if (customer == null)
                        throw new InvalidOperationException("Please log in again.");

                    _customers.ChangePassword(customer.CustomerID, txtCurrent.Text, txtNew.Text);
                    customer.Password = txtNew.Text;
                    MessageBox.Show("Password changed.", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Change Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
