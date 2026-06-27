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
            CenterLoginCard();

            panelTitleBar.BackColor = UiTheme.TitleBar;
            lblTitleBarText.ForeColor = UiTheme.AdminMuted;
            lblTitleBarText.BackColor = UiTheme.TitleBar;
            lblTitleBarText.Font = UiTheme.UiFont;
            pnlTitleIcon.BackColor = UiTheme.PrimaryContainer;
            lblTitleIcon.ForeColor = Color.White;
            lblTitleIcon.BackColor = UiTheme.PrimaryContainer;

            lblBrand.ForeColor = UiTheme.PrimaryDark;
            lblBrand.Font = UiTheme.UiFontTitle;
            lblBrand.BackColor = Color.White;
            lblVersion.ForeColor = UiTheme.AdminMuted;
            lblVersion.BackColor = Color.White;
            lblAuthTitle.ForeColor = UiTheme.AdminOnSurface;
            lblAuthTitle.Font = UiTheme.UiFontAuthTitle;
            lblAuthTitle.BackColor = Color.White;
            lblAuthSubtitle.ForeColor = UiTheme.AdminMuted;
            lblAuthSubtitle.BackColor = Color.White;

            pnlBrandIcon.BackColor = UiTheme.PrimaryContainer;
            lblBrandIcon.ForeColor = Color.White;
            lblBrandIcon.BackColor = UiTheme.PrimaryContainer;

            UiTheme.StyleClinicalFieldLabel(lblClinicalId);
            UiTheme.StyleClinicalFieldLabel(lblPassword);
            UiTheme.StyleTextBox(txtClinicalId);
            UiTheme.ApplyLoginButton(btnLogin);
            btnTogglePassword.BackColor = Color.White;
            btnTogglePassword.ForeColor = UiTheme.AdminMuted;
            btnTogglePassword.FlatAppearance.BorderSize = 0;
            chkStayLoggedIn.Font = UiTheme.UiFont;
            chkStayLoggedIn.ForeColor = UiTheme.AdminMuted;
            chkStayLoggedIn.BackColor = Color.White;

            panelFooter.BackColor = UiTheme.FooterBackground;
            lblSecurityLine.ForeColor = Color.FromArgb(153, UiTheme.AdminMuted);
            lblSecurityLine.BackColor = UiTheme.FooterBackground;
            lblCopyright.ForeColor = Color.FromArgb(102, UiTheme.AdminMuted);
            lblCopyright.BackColor = UiTheme.FooterBackground;

            panelError.BackColor = UiTheme.ErrorContainer;
            panelError.Visible = false;
            lblError.ForeColor = UiTheme.ErrorOnContainer;
            lblError.BackColor = UiTheme.ErrorContainer;

            SetPasswordVisible(_passwordVisible);
        }

        private void SetPasswordVisible(bool visible)
        {
            _passwordVisible = visible;
            UiTheme.StylePasswordBox(txtPassword, masked: !visible);
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

        private void PanelMain_Resize(object sender, EventArgs e) => CenterLoginCard();

        private void CenterLoginCard()
        {
            if (panelLoginCard == null || panelMain == null) return;
            panelLoginCard.Left = Math.Max(0, (panelMain.ClientSize.Width - panelLoginCard.Width) / 2);
            panelLoginCard.Top = Math.Max(0, (panelMain.ClientSize.Height - panelLoginCard.Height) / 2);
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
                Session.Clear();

                var identity = txtClinicalId.Text.Trim();
                var password = txtPassword.Text;

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
        }

        private void ShowAuthError(string message)
        {
            lblError.Text = message;
            panelError.Visible = true;
        }
    }
}
