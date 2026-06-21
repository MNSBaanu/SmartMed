using System;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth = new AuthService();

        public LoginForm()
        {
            InitializeComponent();
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

        private void OnAdminFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form form)
                form.FormClosed -= OnAdminFormClosed;
            if (!Session.IsAdminLoggedIn && !IsDisposed)
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
                    Hide();
                    var dashboard = new AdminDashboardForm();
                    AttachAdminReturn(dashboard);
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
                MessageBox.Show($"Welcome, {customer.Name}. Customer portal coming next.", "Login Successful",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
