using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Models;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public partial class RegistrationForm : Form
    {
        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;

        public RegistrationForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyTheme();
            CenterCard();
        }

        private void ApplyTheme()
        {
            ThemeApplier.ApplyRegistrationForm(
                this, panelCard, panelHeader, panelBody, panelFooter,
                lblMedicalIcon, lblHeaderTitle, lblLockIcon, btnClose,
                lblFullName, txtFullName,
                lblEmail, txtEmail,
                lblPhone, txtPhone,
                lblAddress, txtAddress,
                lblPassword, txtPassword, btnTogglePassword,
                lblConfirm, txtConfirm,
                btnRegister, btnCancel,
                panelSuccess, lblSuccessIcon, lblSuccessTitle, lblSuccessMessage, btnReturnLogin);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterCard();
        }

        private void CenterCard()
        {
            panelCard.Left = Math.Max(0, (ClientSize.Width - panelCard.Width) / 2);
            panelCard.Top = Math.Max(0, (ClientSize.Height - panelCard.Height) / 2);
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.UseSystemPasswordChar = !_passwordVisible;
            txtConfirm.UseSystemPasswordChar = !_passwordVisible;
            btnTogglePassword.Text = _passwordVisible ? "\uED1A" : "\uE890";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnReturnLogin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Registration", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = new Customer
                {
                    Name = txtFullName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Password = txtPassword.Text
                };

                _auth.RegisterCustomer(customer);
                ShowSuccess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowSuccess()
        {
            panelBody.Visible = false;
            panelFooter.Visible = false;
            panelSuccess.Visible = true;
            panelSuccess.BringToFront();
        }
    }
}
