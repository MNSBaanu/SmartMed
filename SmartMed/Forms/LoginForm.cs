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
        private const int ContentTopMargin = 32;

        private readonly AuthService _auth;
        private bool _passwordVisible;
        private bool _fieldNavigationWired;

        public event EventHandler LoginSucceeded;

        static LoginForm()
        {
            try
            {
                UiTheme.Init();
            }
            catch
            {
            }
        }

        public LoginForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyViewChrome();
            if (!DesignHostHelper.IsDesignHost(this))
            {
                _auth = new AuthService();
                WireRuntimeBehavior();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
        }

        private void ApplyViewChrome()
        {
            AuthFormView.ApplyCardBorder(panelLoginCard);
            AuthFormView.ApplyPasswordFieldBorder(pnlUsernameField);
            AuthFormView.ApplyPasswordFieldBorder(pnlPasswordField);
            AuthFormView.StyleFieldLabels(lblUsername, lblPassword);
            AuthFormView.ApplySoftFieldSurfaces(txtUsername, txtPassword);
            lblUsername.BringToFront();
            lblPassword.BringToFront();
            LayoutLoginContent();
        }

        internal void ResetAfterLogout()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            _passwordVisible = false;
            RestoreLoginAppearance();
        }

        private void WireRuntimeBehavior()
        {
            UiTheme.WireClinicalPlaceholderTextBox(txtUsername, "Enter email or username");
            UiTheme.WireClinicalPasswordField(pnlPasswordField, txtPassword, btnTogglePassword, "Enter your password");
            SetPasswordVisible(_passwordVisible);
            if (!_fieldNavigationWired)
            {
                UiTheme.EnableFieldNavigation(btnLogin, txtUsername, txtPassword);
                _fieldNavigationWired = true;
            }
        }

        private void LayoutLoginContent()
        {
            if (panelLoginCard == null || panelMain == null) return;

            var left = Math.Max(0, (panelMain.ClientSize.Width - panelLoginCard.Width) / 2);
            var top = Math.Max(ContentTopMargin, (panelMain.ClientSize.Height - panelLoginCard.Height) / 2);

            panelLoginCard.Left = left;
            panelLoginCard.Top = top;
            CenterHeaderStack();
        }

        private void CenterHeaderStack()
        {
            var pad = panelLoginCard.Padding;
            var contentWidth = Math.Max(0, panelLoginCard.ClientSize.Width - pad.Left - pad.Right);
            var contentLeft = pad.Left;

            if (pnlBrandIcon != null)
                pnlBrandIcon.Left = contentLeft + Math.Max(0, (contentWidth - pnlBrandIcon.Width) / 2);

            CenterHeaderLabel(lblBrand, contentLeft, contentWidth);
            CenterHeaderLabel(lblAuthTitle, contentLeft, contentWidth);
            CenterHeaderLabel(lblAuthSubtitle, contentLeft, contentWidth);
        }

        private static void CenterHeaderLabel(Label label, int contentLeft, int contentWidth)
        {
            if (label == null) return;
            label.AutoSize = false;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Left = contentLeft;
            label.Width = contentWidth;
        }

        private void PanelMain_Resize(object sender, EventArgs e) => LayoutLoginContent();

        private void TxtPassword_GotFocus(object sender, EventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                if (!IsDisposed && txtPassword != null && !txtPassword.IsDisposed)
                    SetPasswordVisible(_passwordVisible);
            }));
        }

        internal void RestoreLoginAppearance()
        {
            UiTheme.ResetClinicalPlaceholder(txtUsername, "Enter email or username");
            UiTheme.ResetClinicalPlaceholder(txtPassword, "Enter your password");
            _passwordVisible = false;
            WireRuntimeBehavior();
            LayoutLoginContent();
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

        private void LnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var dlg = new ForgotPasswordForm())
            {
                dlg.ShowDialog(this);
            }
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

            try
            {
                CartService.Unload();
                Session.Clear();

                var identity = UiTheme.ReadTextBoxValue(txtUsername);
                var password = UiTheme.ReadTextBoxValue(txtPassword);

                if (ValidationService.IsNullOrWhiteSpace(identity) || ValidationService.IsNullOrWhiteSpace(password))
                {
                    SmartMedMessageBox.Show("Email/username and password are required.", "Login",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Try admin login first, then customer login when an email was entered.
                var admin = _auth.AdminLogin(identity, password);
                if (admin != null)
                {
                    admin.Password = null;
                    Session.CurrentAdmin = admin;
                    LoginSucceeded?.Invoke(this, EventArgs.Empty);
                    return;
                }

                if (ValidationService.IsValidEmail(identity))
                {
                    var customer = _auth.CustomerLogin(identity, password);
                    if (customer != null)
                    {
                        customer.Password = null;
                        Session.CurrentCustomer = customer;
                        CartService.LoadForCustomer(customer.CustomerID, new MedicineService());
                        LoginSucceeded?.Invoke(this, EventArgs.Empty);
                        return;
                    }
                }

                SmartMedMessageBox.Show(InvalidCredentialsMessage, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ArgumentException ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show("Unable to sign in.\n" + ex.Message, "Login Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
