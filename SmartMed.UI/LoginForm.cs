using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public partial class LoginForm : Form
    {
        private readonly AuthService _auth = new AuthService();
        private bool _passwordVisible;
        private bool _usernamePlaceholder = true;

        public LoginForm()
        {
            InitializeComponent();
            cmbRole.SelectedIndex = 0;
            cmbRole.SelectedIndexChanged += (s, e) => UpdateRoleUi();
            panelCard.Paint += PanelCard_Paint;
            txtUsername.GotFocus += TxtUsername_GotFocus;
            txtUsername.LostFocus += TxtUsername_LostFocus;
            UiFactory.ConfigureAuthForm(this, panelCard);
            UpdateRoleUi();
        }

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelCard.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using (var pen = new Pen(Color.FromArgb(229, 231, 235)))
                e.Graphics.DrawRectangle(pen, rect);
        }

        private void UpdateRoleUi()
        {
            bool isCustomer = cmbRole.SelectedItem?.ToString() == "Customer";
            lblUsername.Text = isCustomer ? "Email Address" : "Username";
            SetUsernamePlaceholder(isCustomer ? "email@example.com" : "Enter your credentials");
            btnRegister.Visible = isCustomer;
        }

        private void SetUsernamePlaceholder(string placeholder)
        {
            _usernamePlaceholder = true;
            txtUsername.ForeColor = Color.Gray;
            txtUsername.Text = placeholder;
        }

        private void TxtUsername_GotFocus(object sender, EventArgs e)
        {
            if (!_usernamePlaceholder) return;
            txtUsername.Text = "";
            txtUsername.ForeColor = ClinicalPrecisionTheme.OnSurface;
            _usernamePlaceholder = false;
        }

        private void TxtUsername_LostFocus(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtUsername.Text)) return;
            bool isCustomer = cmbRole.SelectedItem?.ToString() == "Customer";
            SetUsernamePlaceholder(isCustomer ? "email@example.com" : "Enter your credentials");
        }

        private string GetUsernameValue()
        {
            return _usernamePlaceholder ? "" : txtUsername.Text.Trim();
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.PasswordChar = _passwordVisible ? '\0' : '●';
            btnTogglePassword.Text = _passwordVisible ? "\uED1A" : "\uE890";
        }

        private void LnkForgot_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please contact your pharmacy administrator to reset your password.",
                "Forgot Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            Hide();
            new RegistrationForm().ShowDialog();
            Show();
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                var identity = GetUsernameValue();
                if (cmbRole.SelectedItem?.ToString() == "Admin")
                {
                    var admin = _auth.AdminLogin(identity, txtPassword.Text);
                    if (admin == null) { MessageBox.Show("Invalid admin credentials."); return; }
                    Session.CurrentAdmin = admin;
                    Hide();
                    new AdminDashboardForm().ShowDialog();
                    Close();
                }
                else
                {
                    var customer = _auth.CustomerLogin(identity, txtPassword.Text);
                    if (customer == null) { MessageBox.Show("Invalid customer credentials."); return; }
                    Session.CurrentCustomer = customer;
                    Hide();
                    new CustomerDashboardForm().ShowDialog();
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
