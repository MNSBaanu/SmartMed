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

        public LoginForm()
        {
            InitializeComponent();
            if (Site?.DesignMode != true)
            {
                UiTheme.ApplyLoginForm(this, lblRole, lblUsername, lblPassword, cmbRole, txtUsername, txtPassword);
                UiTheme.ApplyFlatButton(btnLogin, UiButtonStyle.Primary);
                UiTheme.ApplyFlatButton(btnRegister, UiButtonStyle.Success);
                UiTheme.StyleTextBox(txtUsername);
                UiTheme.StyleTextBox(txtPassword);
                UiTheme.StyleComboBox(cmbRole);
            }
            UpdateRoleUi();
        }

        private void UpdateRoleUi()
        {
            bool isCustomer = cmbRole.SelectedItem?.ToString() == "Customer";
            lblUsername.Text = isCustomer ? "Email" : "Username";
            btnRegister.Visible = isCustomer;
        }

        private void CmbRole_SelectedIndexChanged(object sender, EventArgs e) => UpdateRoleUi();

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
                Show();
        }

        private void OnCustomerFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form form)
                form.FormClosed -= OnCustomerFormClosed;
            if (!Session.IsCustomerLoggedIn && !IsDisposed)
                Show();
        }

        private void PerformLogin()
        {
            try
            {
                Session.Clear();
                var identity = txtUsername.Text.Trim();
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
                    var dashboard = new AdminDashboardForm();
                    AttachAdminReturn(dashboard);
                    dashboard.PrepareForNavigation();
                    Hide();
                    dashboard.Show();
                    return;
                }

                var customer = _auth.CustomerLogin(identity, password);
                if (customer == null)
                {
                    MessageBox.Show("Invalid customer credentials.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Session.CurrentCustomer = customer;
                var portal = new CustomerDashboardForm();
                AttachCustomerReturn(portal);
                portal.PrepareForNavigation();
                Hide();
                portal.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
