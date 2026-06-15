using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

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
            UiFactory.StyleAuthForm(this, 460, 420);
            Text = "SmartMed Pharmacy - Login";

            var card = UiFactory.CreateCardPanel(400, 360);
            card.Location = new Point(30, 24);
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };
            Controls.Add(card);

            int pad = ClinicalPrecisionTheme.ContainerPadding;
            int y = pad;

            var lblTitle = new Label
            {
                Text = "SmartMed Pharmacy",
                Font = ClinicalPrecisionTheme.AppTitleFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(pad, y)
            };
            card.Controls.Add(lblTitle);
            y += 36;

            var lblSubtitle = new Label
            {
                Text = "Sign in to your account",
                Font = ClinicalPrecisionTheme.BodyFont,
                ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant,
                AutoSize = true,
                Location = new Point(pad, y)
            };
            card.Controls.Add(lblSubtitle);
            y += 32;

            card.Controls.Add(UiFactory.CreateFieldLabel("Role"));
            card.Controls[card.Controls.Count - 1].Location = new Point(pad, y);
            y += 20;
            cmbRole = new ComboBox { Location = new Point(pad, y), DropDownStyle = ComboBoxStyle.DropDownList };
            UiFactory.ApplyComboBoxStyle(cmbRole, 352);
            cmbRole.Items.AddRange(new object[] { "Admin", "Customer" });
            cmbRole.SelectedIndex = 0;
            cmbRole.SelectedIndexChanged += (s, e) => UpdateLabels();
            card.Controls.Add(cmbRole);
            y += 40;

            lblUsername = UiFactory.CreateFieldLabel("Username");
            lblUsername.Location = new Point(pad, y);
            card.Controls.Add(lblUsername);
            y += 20;
            txtUsername = new TextBox { Location = new Point(pad, y) };
            UiFactory.ApplyTextBoxStyle(txtUsername, 352);
            card.Controls.Add(txtUsername);
            y += 40;

            card.Controls.Add(UiFactory.CreateFieldLabel("Password"));
            card.Controls[card.Controls.Count - 1].Location = new Point(pad, y);
            y += 20;
            txtPassword = new TextBox { Location = new Point(pad, y), PasswordChar = '*' };
            UiFactory.ApplyTextBoxStyle(txtPassword, 352);
            card.Controls.Add(txtPassword);
            y += 44;

            var btnLogin = UiFactory.CreatePrimaryButton("Login", 120);
            var btnRegister = UiFactory.CreateSecondaryButton("Register", 120);
            btnLogin.Location = new Point(pad, y);
            btnRegister.Location = new Point(pad + 130, y);
            btnLogin.Click += BtnLogin_Click;
            btnRegister.Click += (s, e) => { Hide(); new RegistrationForm().ShowDialog(); Show(); };
            card.Controls.Add(btnLogin);
            card.Controls.Add(btnRegister);

            UpdateLabels();
        }

        private void UpdateLabels()
        {
            lblUsername.Text = cmbRole.SelectedItem?.ToString() == "Admin" ? "Username" : "Email";
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
