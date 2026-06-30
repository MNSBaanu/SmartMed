using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ProfileManagementForm : CustomerPageControl
    {
        private CustomerService _customers;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public ProfileManagementForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _customers = new CustomerService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
        }

        protected override void BuildPageLayout()
        {
            // Layout lives in ProfileManagementForm.Designer.cs.
        }

        protected override void DoRefreshPage() => LoadProfile();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            var customer = DesignTimePreviewData.SampleCustomer();
            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtEmail);
            UiTheme.StyleTextBox(txtPhone);
            UiTheme.StyleTextBox(txtAddress);

            WirePanelBorder(panelFormOuter);
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "dash-border") return;
            panel.Tag = "dash-border";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnSaveProfile.Click += BtnSave_Click;
            btnChangePassword.Click += (s, e) => ShowChangePasswordDialog();
        }

        private void LoadProfile()
        {
            if (!_servicesReady) return;

            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var updated = new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
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
            if (!_servicesReady) return;

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
