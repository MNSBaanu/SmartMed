using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Models;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public partial class RegistrationForm : Form
    {
        private readonly AuthService _auth = new AuthService();

        public RegistrationForm()
        {
            InitializeComponent();
            panelCard.Paint += PanelCard_Paint;
            UiFactory.ConfigureAuthForm(this, panelCard);
        }

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelCard.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                e.Graphics.DrawRectangle(pen, rect);
        }

        private void BtnCancel_Click(object sender, EventArgs e) => Close();

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtConfirm.Text)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }
                var customer = new Customer
                {
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Password = txtPassword.Text
                };
                _auth.RegisterCustomer(customer);
                MessageBox.Show("Registration successful. You can now login.");
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
