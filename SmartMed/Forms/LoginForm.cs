using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        private const string InvalidCredentialsMessage = "Invalid credentials.";

        private readonly AuthService _auth;
        private bool _passwordVisible;

        public event EventHandler LoginSucceeded;

        static LoginForm()
        {
            try
            {
                UiTheme.Init();
            }
            catch
            {
                // Designer host may initialize fonts later.
            }
        }

        public LoginForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            HideErrorPanel();
            if (!DesignHostHelper.IsDesignHost(this))
            {
                _auth = new AuthService();
                WireRuntimeBehavior();
            }
        }

        internal void ResetAfterLogout()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            _passwordVisible = false;
            HideErrorPanel();
            RestoreLoginAppearance();
        }

        private void HideErrorPanel()
        {
            if (panelError == null || lblError == null) return;

            panelError.Visible = false;
            lblError.Text = string.Empty;
            panelError.BackColor = Color.White;
            lblError.ForeColor = Color.White;
            lblError.BackColor = Color.White;
        }

        /// <summary>Runtime-only behavior. All layout, fonts, and colors come from LoginForm.Designer.cs.</summary>
        private void WireRuntimeBehavior()
        {
            UiTheme.Init();
            UiTheme.ApplyClinicalAuthCard(panelLoginCard);
            UiTheme.WireClinicalPlaceholderTextBox(txtUsername, "Enter email or username");
            UiTheme.WireClinicalPasswordField(pnlPasswordField, txtPassword, btnTogglePassword, "Enter your password");
            txtPassword.GotFocus -= TxtPassword_ApplyMask;
            txtPassword.GotFocus += TxtPassword_ApplyMask;

            HideErrorPanel();
            panelError.SendToBack();
            lnkForgot.BringToFront();
            SetPasswordVisible(_passwordVisible);
        }

        private void TxtPassword_ApplyMask(object sender, EventArgs e) =>
            SetPasswordVisible(_passwordVisible);

        internal void RestoreLoginAppearance()
        {
            UiTheme.ResetClinicalPlaceholder(txtUsername, "Enter email or username");
            UiTheme.ResetClinicalPlaceholder(txtPassword, "Enter your password");
            _passwordVisible = false;
            WireRuntimeBehavior();
            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
            Activate();
        }

        private void SetPasswordVisible(bool visible)
        {
            _passwordVisible = visible;
            if (!UiTheme.IsPlaceholderActive(txtPassword))
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.PasswordChar = visible ? '\0' : UiTheme.PasswordMaskChar;
            }

            UiTheme.SetPasswordToggleText(btnTogglePassword, visible);
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelMain.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            using (var brush = new LinearGradientBrush(rect, Color.White, UiTheme.AdminSurface, 45f))
                e.Graphics.FillRectangle(brush, rect);
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e) =>
            SetPasswordVisible(!_passwordVisible);

        private void TxtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            txtPassword.Focus();
        }

        private void TxtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            PerformLogin();
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

        private void BtnLogin_Click(object sender, EventArgs e) => PerformLogin();

        private void BtnQuickAdmin_Click(object sender, EventArgs e) =>
            PerformLoginWith(DemoAdminUsername, DemoAdminPassword);

        private void BtnQuickCustomer_Click(object sender, EventArgs e) =>
            PerformLoginWith(DemoCustomerEmail, DemoCustomerPassword);

        private void PerformLoginWith(string identity, string password)
        {
            txtUsername.Text = identity;
            txtUsername.ForeColor = UiTheme.AdminOnSurface;
            txtPassword.Text = password;
            txtPassword.ForeColor = UiTheme.AdminOnSurface;
            SetPasswordVisible(_passwordVisible);
            PerformLogin();
        }

        private void PerformLogin()
        {
            if (_auth == null) return;

            HideErrorPanel();

            try
            {
                Session.Clear();

                var identity = UiTheme.ReadTextBoxValue(txtUsername);
                var password = UiTheme.ReadTextBoxValue(txtPassword);

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
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                    Close();
                    return;
                }

                if (ValidationService.IsValidEmail(identity))
                {
                    var customer = _auth.CustomerLogin(identity, password);
                    if (customer != null)
                    {
                        Session.CurrentCustomer = customer;
                        LoginSucceeded?.Invoke(this, EventArgs.Empty);
                        Close();
                        return;
                    }
                }

                MessageBox.Show(InvalidCredentialsMessage, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void lblBrandIcon_Click(object sender, EventArgs e)
        {
        }
    }
}
