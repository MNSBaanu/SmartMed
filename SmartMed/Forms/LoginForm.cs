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

        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;

        public event EventHandler LoginSucceeded;

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
            panelError.Visible = false;
            RestoreLoginAppearance();
        }

        private void ApplyChrome()
        {
            UiTheme.ApplyLoginForm(this, panelMain, panelLoginCard, lnkForgot);
            LayoutLoginContent();

            panelTitleBar.BackColor = UiTheme.TitleBar;
            lblTitleBarText.ForeColor = UiTheme.AdminLabelText;
            lblTitleBarText.BackColor = UiTheme.TitleBar;
            lblTitleBarText.Font = UiTheme.UiFont;
            pnlTitleIcon.BackColor = UiTheme.PrimaryContainer;
            lblTitleIcon.ForeColor = Color.White;
            lblTitleIcon.BackColor = UiTheme.PrimaryContainer;
            lblTitleIcon.Font = UiTheme.FontAt(7F, bold: true);

            lblBrand.ForeColor = UiTheme.AdminTealDark;
            lblBrand.Font = UiTheme.UiFontTitle;
            lblBrand.BackColor = Color.White;
            lblVersion.ForeColor = UiTheme.FooterText;
            lblVersion.BackColor = Color.White;
            lblVersion.Font = UiTheme.FontAt(8.25F);
            lblAuthTitle.ForeColor = UiTheme.AdminOnSurface;
            lblAuthTitle.Font = UiTheme.UiFontAuthTitle;
            lblAuthTitle.BackColor = Color.White;
            lblAuthSubtitle.ForeColor = UiTheme.AdminLabelText;
            lblAuthSubtitle.BackColor = Color.White;
            lblAuthSubtitle.Font = UiTheme.UiFont;

            pnlBrandIcon.BackColor = UiTheme.PrimaryContainer;
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.BackColor = UiTheme.PrimaryContainer;
            lblBrandIcon.Font = UiTheme.FontAt(24F, bold: true);

            UiTheme.StyleClinicalFieldLabel(lblUsername);
            UiTheme.StyleClinicalFieldLabel(lblPassword);
            UiTheme.StyleClinicalTextBox(txtUsername, "Enter email or username");
            UiTheme.StyleClinicalPasswordBox(txtPassword, "Enter your password");
            txtPassword.GotFocus += (s, e) => SetPasswordVisible(_passwordVisible);
            UiTheme.ApplyLoginButton(btnLogin);
            UiTheme.ApplySecondaryButton(btnRegister);
            UiTheme.ApplySecondaryButton(btnQuickAdmin);
            UiTheme.ApplySecondaryButton(btnQuickCustomer);
            UiTheme.StyleLinkButton(lnkForgot);

            panelFooter.BackColor = UiTheme.AdminSurface;
            lblSecurityLine.ForeColor = UiTheme.FooterText;
            lblSecurityLine.BackColor = UiTheme.AdminSurface;
            lblSecurityLine.Font = UiTheme.FontAt(8.25F);
            lblCopyright.ForeColor = UiTheme.FooterText;
            lblCopyright.BackColor = UiTheme.AdminSurface;
            lblCopyright.Font = UiTheme.FontAt(8.25F);

            panelError.BackColor = UiTheme.ErrorContainer;
            panelError.Visible = false;
            lblError.ForeColor = UiTheme.ErrorOnContainer;
            lblError.BackColor = UiTheme.ErrorContainer;
            lblError.Font = UiTheme.FontAt(8.25F);

            SetPasswordVisible(_passwordVisible);
        }

        internal void RestoreLoginAppearance()
        {
            ApplyChrome();
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

            btnTogglePassword.Text = visible ? "Hide" : "Show";
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

        private void PanelMain_Resize(object sender, EventArgs e) => LayoutLoginContent();

        private void LayoutLoginContent()
        {
            if (panelLoginCard == null || panelMain == null || panelFooter == null) return;

            const int gap = 20;
            var totalHeight = panelLoginCard.Height + gap + panelFooter.Height;
            var left = Math.Max(0, (panelMain.ClientSize.Width - panelLoginCard.Width) / 2);
            var top = Math.Max(24, (panelMain.ClientSize.Height - totalHeight) / 2);

            panelLoginCard.Left = left;
            panelLoginCard.Top = top;

            panelFooter.Width = panelLoginCard.Width;
            panelFooter.Left = left;
            panelFooter.Top = panelLoginCard.Bottom + gap;
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
            txtUsername.Tag = "clinical-input";
            txtPassword.Text = password;
            txtPassword.ForeColor = UiTheme.AdminOnSurface;
            txtPassword.Tag = "clinical-input";
            SetPasswordVisible(_passwordVisible);
            PerformLogin();
        }

        private void PerformLogin()
        {
            panelError.Visible = false;

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

                MessageBox.Show("Invalid credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoginForm_Load(object sender, EventArgs e) => txtUsername.Focus();
    }
}
