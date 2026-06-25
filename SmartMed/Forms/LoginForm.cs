using System;
using System.Windows.Forms;
using ReaLTaiizor.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class LoginForm : MaterialForm
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
            if (Site?.DesignMode != true)
                ApplyChrome();
        }

        internal void ResetAfterLogout()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            _passwordVisible = false;
            ApplyChrome();
        }

        private void ApplyChrome()
        {
            UiTheme.ApplyLoginForm(this, panelBody, lnkForgot);
            UiTheme.ApplyFlatButton(btnLogin, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnRegister, UiButtonStyle.Success);
            UiTheme.ApplyFlatButton(btnQuickAdmin, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnQuickCustomer, UiButtonStyle.Success);
            UiTheme.StyleTextBox(txtUsername);
            UiTheme.StyleTextBox(txtPassword);
            SetPasswordVisible(false);
        }

        private void SetPasswordVisible(bool visible)
        {
            _passwordVisible = visible;
            txtPassword.UseSystemPasswordChar = false;
            txtPassword.PasswordChar = visible ? '\0' : '\u2022';
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
                registration.ShowDialog(this);
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
                        var portal = new CustomerDashboardForm();
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
