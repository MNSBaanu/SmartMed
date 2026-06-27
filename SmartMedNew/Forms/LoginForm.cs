using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;
        private const string LoginButtonText = "Login";
        private const string LoginBusyText = "Signing in\u2026";

        public event EventHandler LoginSucceeded;

        public LoginForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyChrome();
        }

        private void ApplyChrome()
        {
            UiTheme.ApplyLoginForm(this, panelMain, panelLoginCard, lnkTerminalHelp);
            LayoutLoginContent();

            panelTitleBar.BackColor = UiTheme.TitleBar;
            lblTitleBarText.ForeColor = UiTheme.AdminLabelText;
            lblTitleBarText.BackColor = UiTheme.TitleBar;
            lblTitleBarText.Font = UiTheme.UiFont;
            pnlTitleIcon.BackColor = UiTheme.PrimaryContainer;
            lblTitleIcon.ForeColor = Color.White;
            lblTitleIcon.BackColor = UiTheme.PrimaryContainer;

            lblBrand.ForeColor = UiTheme.PrimaryDark;
            lblBrand.Font = UiTheme.UiFontTitle;
            lblBrand.BackColor = Color.White;
            lblVersion.ForeColor = UiTheme.FooterText;
            lblVersion.BackColor = Color.White;
            lblAuthTitle.ForeColor = UiTheme.AdminOnSurface;
            lblAuthTitle.Font = UiTheme.UiFontAuthTitle;
            lblAuthTitle.BackColor = Color.White;
            lblAuthSubtitle.ForeColor = UiTheme.AdminLabelText;
            lblAuthSubtitle.BackColor = Color.White;

            pnlBrandIcon.BackColor = UiTheme.PrimaryContainer;
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.BackColor = UiTheme.PrimaryContainer;

            UiTheme.StyleClinicalFieldLabel(lblClinicalId);
            UiTheme.StyleClinicalFieldLabel(lblPassword);
            UiTheme.StyleClinicalTextBox(txtClinicalId, "Enter Clinical ID");
            UiTheme.StyleClinicalPasswordBox(txtPassword, "Enter your password");
            txtPassword.GotFocus += (s, e) => SetPasswordVisible(_passwordVisible);
            UiTheme.ApplyLoginButton(btnLogin);
            UiTheme.StyleLinkButton(btnTogglePassword);
            chkStayLoggedIn.Font = UiTheme.UiFont;
            chkStayLoggedIn.ForeColor = UiTheme.AdminLabelText;
            chkStayLoggedIn.BackColor = Color.White;

            panelFooter.BackColor = UiTheme.AdminSurface;
            lblSecurityLine.ForeColor = UiTheme.FooterText;
            lblSecurityLine.BackColor = UiTheme.AdminSurface;
            lblCopyright.ForeColor = UiTheme.FooterText;
            lblCopyright.BackColor = UiTheme.AdminSurface;

            panelError.BackColor = UiTheme.ErrorContainer;
            panelError.Visible = false;
            lblError.ForeColor = UiTheme.ErrorOnContainer;
            lblError.BackColor = UiTheme.ErrorContainer;

            SetPasswordVisible(_passwordVisible);
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

        private void TxtClinicalId_KeyDown(object sender, KeyEventArgs e)
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

        private void LnkTerminalHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show(
                "Contact your pharmacy IT administrator for terminal access assistance.",
                "Terminal Help",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnLogin_Click(object sender, EventArgs e) => PerformLogin();

        private void PerformLogin()
        {
            panelError.Visible = false;

            try
            {
                SetLoginBusy(true);

                Session.Clear();

                var identity = UiTheme.ReadTextBoxValue(txtClinicalId);
                var password = UiTheme.ReadTextBoxValue(txtPassword);

                if (ValidationService.IsNullOrWhiteSpace(identity) || ValidationService.IsNullOrWhiteSpace(password))
                {
                    ShowAuthError("Clinical ID and password are required.");
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

                ShowAuthError("Authentication failed. Please verify your Clinical ID and try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                if (!IsDisposed)
                    SetLoginBusy(false);
            }
        }

        private void SetLoginBusy(bool busy)
        {
            btnLogin.Enabled = !busy;
            txtClinicalId.Enabled = !busy;
            txtPassword.Enabled = !busy;
            btnTogglePassword.Enabled = !busy;
            chkStayLoggedIn.Enabled = !busy;
            lnkTerminalHelp.Enabled = !busy;
            btnLogin.Text = busy ? LoginBusyText : LoginButtonText;
            UseWaitCursor = busy;
        }

        private void ShowAuthError(string message)
        {
            lblError.Text = message;
            panelError.Visible = true;
        }

        private void LoginForm_Load(object sender, EventArgs e) => txtClinicalId.Focus();
    }
}
