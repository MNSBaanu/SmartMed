using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

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
            LayoutForm();
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            LayoutForm();
        }

        private void LayoutForm()
        {
            const int pad = 24;
            const int gap = 16;
            const int labelGap = 4;
            const int inputH = 36;
            const int headerH = 56;
            const int footerH = 124;
            int cardW = 450;
            int x = pad;
            int fieldW = cardW - pad * 2;
            int halfW = (fieldW - gap) / 2;

            panelCard.SetBounds(
                Math.Max(0, (ClientSize.Width - cardW) / 2),
                Math.Max(0, (ClientSize.Height - 620) / 2),
                cardW, 620);

            panelHeader.SetBounds(0, 0, cardW, headerH);
            panelFooter.SetBounds(0, 620 - footerH, cardW, footerH);
            panelBody.SetBounds(0, headerH, cardW, 620 - headerH - footerH);
            panelSuccess.SetBounds(0, headerH, cardW, 620 - headerH - footerH);

            int y = pad;

            lblFullName.SetBounds(x, y, fieldW, 16);
            y += 16 + labelGap;
            txtFullName.SetBounds(x, y, fieldW, inputH);
            y += inputH + gap;

            lblEmail.SetBounds(x, y, halfW, 16);
            lblPhone.SetBounds(x + halfW + gap, y, halfW, 16);
            y += 16 + labelGap;
            txtEmail.SetBounds(x, y, halfW, inputH);
            txtPhone.SetBounds(x + halfW + gap, y, halfW, inputH);
            y += inputH + gap;

            lblAddress.SetBounds(x, y, fieldW, 16);
            y += 16 + labelGap;
            txtAddress.SetBounds(x, y, fieldW, 52);
            y += 52 + gap;

            lblPassword.SetBounds(x, y, fieldW, 16);
            y += 16 + labelGap;
            txtPassword.SetBounds(x, y, fieldW, inputH);
            btnTogglePassword.SetBounds(x + fieldW - 36, y + 1, 36, inputH - 2);
            y += inputH + gap;

            lblConfirm.SetBounds(x, y, fieldW, 16);
            y += 16 + labelGap;
            txtConfirm.SetBounds(x, y, fieldW, inputH);
            btnToggleConfirm.SetBounds(x + fieldW - 36, y + 1, 36, inputH - 2);

            btnRegister.SetBounds(pad, 16, fieldW, 40);
            btnCancel.SetBounds(pad, 64, fieldW, 40);

            lblSuccessIcon.SetBounds(pad, 80, fieldW, 48);
            lblSuccessTitle.SetBounds(pad, 140, fieldW, 28);
            lblSuccessMessage.SetBounds(pad, 176, fieldW, 72);
            btnReturnLogin.SetBounds(pad, 268, fieldW, 40);
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e) => SetPasswordVisible(!_passwordVisible);

        private void BtnToggleConfirm_Click(object sender, EventArgs e) => SetPasswordVisible(!_passwordVisible);

        private void SetPasswordVisible(bool visible)
        {
            _passwordVisible = visible;
            txtPassword.UseSystemPasswordChar = !visible;
            txtConfirm.UseSystemPasswordChar = !visible;
            var icon = visible ? "\uED1A" : "\uE890";
            btnTogglePassword.Text = icon;
            btnToggleConfirm.Text = icon;
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
