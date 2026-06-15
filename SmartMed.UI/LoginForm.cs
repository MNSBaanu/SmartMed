using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class LoginForm : Form
    {
        private ComboBox cmbRole;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Label lblUsername;
        private readonly AuthService _auth = new AuthService();

        public LoginForm()
        {
            Text = "SmartMed Pharmacy - Login";
            Size = new Size(420, 320);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            BackColor = Color.White;

            var lblTitle = new Label { Text = "SmartMed Pharmacy", Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.FromArgb(0, 50, 150), AutoSize = true, Location = new Point(100, 20) };
            var lblRole = new Label { Text = "Role:", Location = new Point(40, 70), AutoSize = true };
            cmbRole = new ComboBox { Location = new Point(140, 67), Width = 220, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbRole.Items.AddRange(new object[] { "Admin", "Customer" });
            cmbRole.SelectedIndex = 0;
            cmbRole.SelectedIndexChanged += (s, e) => UpdateLabels();

            lblUsername = new Label { Text = "Username:", Location = new Point(40, 110), AutoSize = true };
            txtUsername = new TextBox { Location = new Point(140, 107), Width = 220 };
            var lblPassword = new Label { Text = "Password:", Location = new Point(40, 150), AutoSize = true };
            txtPassword = new TextBox { Location = new Point(140, 147), Width = 220, PasswordChar = '*' };

            var btnLogin = new Button { Text = "Login", Location = new Point(140, 195), Width = 100, BackColor = Color.FromArgb(180, 203, 249) };
            var btnRegister = new Button { Text = "Register", Location = new Point(260, 195), Width = 100 };
            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += (s, e) => { Hide(); new RegistrationForm().ShowDialog(); Show(); };

            Controls.AddRange(new Control[] { lblTitle, lblRole, cmbRole, lblUsername, txtUsername, lblPassword, txtPassword, btnLogin, btnRegister });
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            lblUsername.Text = cmbRole.SelectedItem?.ToString() == "Admin" ? "Username:" : "Email:";
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Clear();
                if (cmbRole.SelectedItem?.ToString() == "Admin")
                {
                    var admin = _auth.AdminLogin(txtUsername.Text, txtPassword.Text);
                    if (admin == null) { MessageBox.Show("Invalid admin credentials."); return; }
                    Session.CurrentAdmin = admin;
                    Hide();
                    new AdminDashboardForm().ShowDialog();
                    Close();
                }
                else
                {
                    var customer = _auth.CustomerLogin(txtUsername.Text, txtPassword.Text);
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
