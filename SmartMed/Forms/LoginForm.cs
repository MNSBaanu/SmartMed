using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class LoginForm : Form
    {
        private const string DemoAdminUsername = "admin";
        private const string DemoAdminPassword = "admin123";
        private const string DemoCustomerEmail = "customer@gmail.com";
        private const string DemoCustomerPassword = "Customer123";

        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;

        public event EventHandler<Form> LoginSucceeded;

        public LoginForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyChrome();
        }

        internal void ResetAfterLogout()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            _passwordVisible = false;
            RestoreLoginAppearance();
        }

        private void ApplyChrome()
        {
            UiTheme.ApplyLoginForm(this, panelBody, panelLoginCard, lnkForgot);
            CenterLoginCard();
            UiTheme.StyleClinicalFieldLabel(lblUsername);
            UiTheme.StyleClinicalFieldLabel(lblPassword);
            lblBrand.ForeColor = UiTheme.AdminTeal;
            lblBrand.Font = UiTheme.UiFontBold;
            lblBrand.BackColor = Color.White;
            lblSubtitle.ForeColor = UiTheme.AdminMuted;
            lblSubtitle.BackColor = Color.White;
            UiTheme.ApplyClinicalAuthButton(btnLogin, primary: true);
            UiTheme.ApplyClinicalAuthButton(btnRegister, primary: false);
            UiTheme.ApplyClinicalAuthButton(btnQuickAdmin, primary: true);
            UiTheme.ApplyClinicalAuthButton(btnQuickCustomer, primary: false);
            UiTheme.StyleTextBox(txtUsername);
            UiTheme.StylePasswordBox(txtPassword, masked: !_passwordVisible);
            btnTogglePassword.BackColor = Color.White;
            btnTogglePassword.ForeColor = UiTheme.AdminMuted;
            btnTogglePassword.FlatAppearance.BorderColor = UiTheme.AdminOutline;
            SetPasswordVisible(_passwordVisible);
        }

        private void SetPasswordVisible(bool visible)
        {
            _passwordVisible = visible;
            UiTheme.StylePasswordBox(txtPassword, masked: !visible);
            btnTogglePassword.Text = visible ? "Hide" : "Show";
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e) =>
            SetPasswordVisible(!_passwordVisible);

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            txtPassword.Focus();
        }

        private void PanelBody_Resize(object sender, EventArgs e) => CenterLoginCard();

        private void CenterLoginCard()
        {
            if (panelLoginCard == null || panelBody == null) return;
            panelLoginCard.Left = Math.Max(0, (panelBody.ClientSize.Width - panelLoginCard.Width) / 2);
            panelLoginCard.Top = Math.Max(0, (panelBody.ClientSize.Height - panelLoginCard.Height) / 2);
        }

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
            Hide();
            try
            {
                using (var registration = new RegistrationForm())
                {
                    registration.StartPosition = FormStartPosition.CenterScreen;
                    registration.ShowInTaskbar = true;
                    registration.ShowDialog();
                }
            }
            finally
            {
                RestoreLoginAppearance();
            }
        }

        internal void RestoreLoginAppearance()
        {
            ApplyChrome();
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
            Activate();
        }

        private void BtnLogin_Click(object sender, EventArgs e) => PerformLogin();

        private void BtnQuickAdmin_Click(object sender, EventArgs e) =>
            PerformLoginWith(DemoAdminUsername, DemoAdminPassword);

        private void BtnQuickCustomer_Click(object sender, EventArgs e) =>
            PerformLoginWith(DemoCustomerEmail, DemoCustomerPassword);

        private void PerformLoginWith(string identity, string password)
        {
            txtUsername.Text = identity;
            txtPassword.Text = password;
            PerformLogin();
        }

        private void PerformLogin()
        {
            try
            {
                Session.Clear();

                var identity = txtUsername.Text.Trim();
                var password = txtPassword.Text;

                if (ValidationService.IsNullOrWhiteSpace(identity) || ValidationService.IsNullOrWhiteSpace(password))
                {
                    MessageBox.Show("Email/username and password are required.", "Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var admin = _auth.AdminLogin(identity, password);
                if (admin != null)
                {
                    Session.CurrentAdmin = admin;
                    var dashboard = new AdminHostForm();
                    dashboard.PrepareForNavigation();
                    LoginSucceeded?.Invoke(this, dashboard);
                    Close();
                    return;
                }

                if (ValidationService.IsValidEmail(identity))
                {
                    var customer = _auth.CustomerLogin(identity, password);
                    if (customer != null)
                    {
                        Session.CurrentCustomer = customer;
                        var portal = new CustomerHostForm();
                        portal.PrepareForNavigation();
                        LoginSucceeded?.Invoke(this, portal);
                        Close();
                        return;
                    }
                }

                MessageBox.Show("Invalid credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
