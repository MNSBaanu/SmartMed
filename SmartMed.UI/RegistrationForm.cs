using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Models;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public class RegistrationForm : Form
    {
        private TextBox txtName, txtEmail, txtPhone, txtAddress, txtPassword, txtConfirm;
        private readonly AuthService _auth = new AuthService();

        public RegistrationForm()
        {
            UiFactory.StyleAuthForm(this, 480, 500);
            Text = "Customer Registration";

            var card = UiFactory.CreateCardPanel(420, 440);
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
                Text = "Create Account",
                Font = ClinicalPrecisionTheme.AppTitleFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(pad, y)
            };
            card.Controls.Add(lblTitle);
            y += 40;

            y = AddField(card, "Full Name", pad, y, out txtName);
            y = AddField(card, "Email", pad, y, out txtEmail);
            y = AddField(card, "Phone", pad, y, out txtPhone);
            y = AddField(card, "Address", pad, y, out txtAddress);
            y = AddField(card, "Password", pad, y, out txtPassword);
            txtPassword.PasswordChar = '*';
            y = AddField(card, "Confirm Password", pad, y, out txtConfirm);
            txtConfirm.PasswordChar = '*';
            y += 8;

            var btnRegister = UiFactory.CreatePrimaryButton("Register", 120);
            var btnCancel = UiFactory.CreateSecondaryButton("Cancel", 120);
            btnRegister.Location = new Point(pad, y);
            btnCancel.Location = new Point(pad + 130, y);
            btnRegister.Click += BtnRegister_Click;
            btnCancel.Click += (s, e) => Close();
            card.Controls.Add(btnRegister);
            card.Controls.Add(btnCancel);
        }

        private int AddField(Panel card, string label, int pad, int y, out TextBox textBox)
        {
            card.Controls.Add(UiFactory.CreateFieldLabel(label));
            card.Controls[card.Controls.Count - 1].Location = new Point(pad, y);
            y += 20;
            textBox = new TextBox { Location = new Point(pad, y) };
            UiFactory.ApplyTextBoxStyle(textBox, 372);
            card.Controls.Add(textBox);
            return y + 36;
        }

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
