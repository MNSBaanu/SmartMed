using System;

using System.Linq;

using System.Windows.Forms;

using ReaLTaiizor.Forms;

using SmartMed.Services;



namespace SmartMed.UI

{

    public partial class LoginForm : MaterialForm
    {
        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;

        public LoginForm()
        {

            InitializeComponent();

            if (Site?.DesignMode != true)
                ApplyChrome();
        }

        private void ApplyChrome()
        {
            UiTheme.ApplyLoginForm(this, panelBody, lnkForgot);
            UiTheme.ApplyFlatButton(btnLogin, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnRegister, UiButtonStyle.Success);
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

        internal static void PresentExisting()
        {
            var login = Application.OpenForms.OfType<LoginForm>().FirstOrDefault();
            if (login == null || login.IsDisposed) return;
            login.PresentAfterLogout();
        }

        private void PresentAfterLogout()
        {
            txtUsername.Clear();
            txtPassword.Clear();
            _passwordVisible = false;
            ApplyChrome();
            UiTheme.RevealForm(this);
            Activate();
            BringToFront();
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

            {

                registration.ShowDialog(this);

            }

        }



        private void BtnLogin_Click(object sender, EventArgs e) => PerformLogin();



        internal void AttachAdminReturn(AdminShellForm adminForm)

        {

            adminForm.FormClosed += OnAdminFormClosed;

        }



        internal void AttachCustomerReturn(CustomerShellForm customerForm)

        {

            customerForm.FormClosed += OnCustomerFormClosed;

        }



        private void OnAdminFormClosed(object sender, FormClosedEventArgs e)

        {

            if (sender is Form form)

                form.FormClosed -= OnAdminFormClosed;

            if (!Session.IsAdminLoggedIn && !IsDisposed)
                PresentAfterLogout();
        }



        private void OnCustomerFormClosed(object sender, FormClosedEventArgs e)

        {

            if (sender is Form form)

                form.FormClosed -= OnCustomerFormClosed;

            if (!Session.IsCustomerLoggedIn && !IsDisposed)
                PresentAfterLogout();
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

                    AttachAdminReturn(dashboard);

                    dashboard.PrepareForNavigation();
                    Hide();
                    UiTheme.RevealForm(dashboard);
                    return;

                }



                if (ValidationService.IsValidEmail(identity))

                {

                    var customer = _auth.CustomerLogin(identity, password);

                    if (customer != null)

                    {

                        Session.CurrentCustomer = customer;

                        var portal = new CustomerDashboardForm();

                        AttachCustomerReturn(portal);

                        portal.PrepareForNavigation();

                        Hide();

                        portal.Show();

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


