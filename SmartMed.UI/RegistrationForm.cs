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
        private bool _passwordVisible;

        public RegistrationForm()
        {
            InitializeComponent();
            panelCard.Paint += PanelCard_Paint;
            SetupPlaceholders();
            UiFactory.ConfigureAuthForm(this, panelCard);
            if (!UiFactory.IsDesignMode(this))
                BackColor = ClinicalPrecisionTheme.ModalOverlay;
        }

        private void SetupPlaceholders()
        {
            SetPlaceholder(txtName, "John Doe");
            SetPlaceholder(txtEmail, "john@example.com");
            SetPlaceholder(txtPhone, "(555) 000-0000");
            SetPlaceholder(txtAddress, "123 Medical Way, Health City");
        }

        private static void SetPlaceholder(TextBox box, string placeholder)
        {
            box.ForeColor = Color.Gray;
            box.Text = placeholder;
            box.GotFocus += (s, e) =>
            {
                if (box.ForeColor != Color.Gray) return;
                box.Text = "";
                box.ForeColor = ClinicalPrecisionTheme.OnSurface;
            };
            box.LostFocus += (s, e) =>
            {
                if (!string.IsNullOrWhiteSpace(box.Text)) return;
                box.ForeColor = Color.Gray;
                box.Text = placeholder;
            };
        }

        private static string GetFieldValue(TextBox box)
        {
            return box.ForeColor == Color.Gray ? "" : box.Text.Trim();
        }

        private void PanelCard_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelCard.ClientRectangle;
            rect.Width -= 1;
            rect.Height -= 1;
            using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                e.Graphics.DrawRectangle(pen, rect);
        }

        private void PanelHeader_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                e.Graphics.DrawLine(pen, 0, panelHeader.Height - 1, panelHeader.Width, panelHeader.Height - 1);
        }

        private void PanelFooter_Paint(object sender, PaintEventArgs e)
        {
            using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                e.Graphics.DrawLine(pen, 0, 0, panelFooter.Width, 0);
        }

        private void BtnTogglePassword_Click(object sender, EventArgs e)
        {
            _passwordVisible = !_passwordVisible;
            txtPassword.PasswordChar = _passwordVisible ? '\0' : '●';
            btnTogglePassword.Text = _passwordVisible ? "\uED1A" : "\uE890";
        }

        private void BtnCancel_Click(object sender, EventArgs e) => Close();

        private void BtnReturnLogin_Click(object sender, EventArgs e) => Close();

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
                    Name = GetFieldValue(txtName),
                    Email = GetFieldValue(txtEmail),
                    Phone = GetFieldValue(txtPhone),
                    Address = GetFieldValue(txtAddress),
                    Password = txtPassword.Text
                };

                _auth.RegisterCustomer(customer);
                ShowSuccess();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowSuccess()
        {
            panelBody.Visible = false;
            panelFooter.Visible = false;
            panelHeader.Visible = false;
            panelSuccess.Visible = true;
            panelSuccess.BringToFront();
        }
    }
}
