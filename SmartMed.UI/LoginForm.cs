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

        public LoginForm()
        {
            InitializeComponent();
            cmbRole.SelectedIndex = 0;
            cmbRole.SelectedIndexChanged += (s, e) => UpdateLabels();
            panelCard.Paint += PanelCard_Paint;
            UiFactory.ConfigureAuthForm(this, panelCard);
            UpdateLabels();
        }

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelCard.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                e.Graphics.DrawRectangle(pen, rect);
        }

        private void UpdateLabels()
        {
            lblUsername.Text = cmbRole.SelectedItem?.ToString() == "Admin" ? "Username" : "Email";
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
