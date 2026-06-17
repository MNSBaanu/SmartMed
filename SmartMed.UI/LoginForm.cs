using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;
        private bool _usernamePlaceholder = true;

        public LoginForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyTheme();
            CenterLoginCard();
            UpdateRoleUi();
        }

        private void ApplyTheme()
        {
            ThemeApplier.ApplyLoginForm(
                this, panelCard, panelHeader, panelBody,
                lblMedicalIcon, lblHeaderTitle, lblLockIcon, btnClose,
                lblRole, cmbRole, lblUsername, txtUsername,
                lblPassword, txtPassword, btnTogglePassword,
                btnLogin, btnRegister, lnkForgot, lblVersion);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            CenterLoginCard();
        }

        private void CenterLoginCard()
        {
            panelCard.Left = Math.Max(0, (ClientSize.Width - panelCard.Width) / 2);
            panelCard.Top = Math.Max(0, (ClientSize.Height - panelCard.Height) / 2);
        }

        private void UpdateRoleUi()
        {
            bool isCustomer = cmbRole.SelectedItem?.ToString() == "Customer";
            lblUsername.Text = isCustomer ? "Email Address" : "Username";
            SetUsernamePlaceholder(isCustomer ? "email@example.com" : "Enter admin username");
            btnRegister.Visible = isCustomer;
            LayoutBody();
        }

        private void LayoutBody()
        {
            const int pad = 24;
            const int gap = 16;
            const int labelGap = 4;
            const int inputH = 40;
            int w = panelBody.ClientSize.Width - pad * 2;
            int x = pad;
            int y = pad;

            lblRole.SetBounds(x, y, w, 16);
            y += 16 + labelGap;
            cmbRole.SetBounds(x, y, w, inputH);
            y += inputH + gap;

            lblUsername.SetBounds(x, y, w, 16);
            y += 16 + labelGap;
            txtUsername.SetBounds(x, y, w, inputH);
            y += inputH + gap;

            lblPassword.SetBounds(x, y, w, 16);
            y += 16 + labelGap;
            txtPassword.SetBounds(x, y, w, inputH);
            btnTogglePassword.SetBounds(x + w - 36, y + 1, 36, inputH - 2);
            y += inputH + gap + 8;

            btnLogin.SetBounds(x, y, w, inputH);
            y += inputH + 8;

            if (btnRegister.Visible)
            {
                btnRegister.SetBounds(x, y, w, inputH);
                y += inputH + 8;
            }

            lnkForgot.Location = new Point(x, y);
            lblVersion.SetBounds(x + w - 130, y, 130, 16);

            panelCard.Height = panelHeader.Height + y + 24;
            CenterLoginCard();
        }

        private void SetUsernamePlaceholder(string placeholder)
        {
            _usernamePlaceholder = true;
            txtUsername.ForeColor = AppTheme.Placeholder;
            txtUsername.Text = placeholder;
        }

        private string GetUsernameValue()
        {
            return _usernamePlaceholder ? string.Empty : txtUsername.Text.Trim();
        }

        private void TxtUsername_GotFocus(object sender, EventArgs e)
        {
            if (!_usernamePlaceholder) return;
            txtUsername.Text = string.Empty;
            txtUsername.ForeColor = AppTheme.OnSurface;
            _usernamePlaceholder = false;
        }

        private void TxtUsername_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text)) return;
            bool isCustomer = cmbRole.SelectedItem?.ToString() == "Customer";
            SetUsernamePlaceholder(isCustomer ? "email@example.com" : "Enter admin username");
        }

        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e) => UpdateRoleUi();

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.UseSystemPasswordChar = !_passwordVisible;
            btnTogglePassword.Text = _passwordVisible ? "\uED1A" : "\uE890";
        }

        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private void LnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Please contact your pharmacy administrator to reset your password.",
                "Forgot Password",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            using (var registration = new RegistrationForm())
            {
                registration.ShowDialog(this);
            }
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                var identity = GetUsernameValue();
                var password = txtPassword.Text;

                if (cmbRole.SelectedItem?.ToString() == "Admin")
                {
                    var admin = _auth.AdminLogin(identity, password);
                    if (admin == null)
                    {
                        MessageBox.Show("Invalid admin credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Session.CurrentAdmin = admin;
                    Hide();
                    var dashboard = new AdminDashboardForm();
                    dashboard.FormClosed += (s, args) =>
                    {
                        if (!IsDisposed)
                            Show();
                    };
                    dashboard.Show();
                    return;
                }
                else
                {
                    var customer = _auth.CustomerLogin(identity, password);
                    if (customer == null)
                    {
                        MessageBox.Show("Invalid customer credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    Session.CurrentCustomer = customer;
                    MessageBox.Show($"Welcome, {customer.Name}. Customer portal coming next.", "Login Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
