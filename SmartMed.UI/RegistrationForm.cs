using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Models;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class RegistrationForm : Form
    {
        private TextBox txtName, txtEmail, txtPhone, txtAddress, txtPassword, txtConfirm;
        private readonly AuthService _auth = new AuthService();

        public RegistrationForm()
        {
            Text = "Customer Registration";
            Size = new Size(450, 400);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            int y = 20;
            Controls.Add(MakeLabel("Full Name:", 30, y)); txtName = MakeText(150, y); y += 40;
            Controls.Add(MakeLabel("Email:", 30, y)); txtEmail = MakeText(150, y); y += 40;
            Controls.Add(MakeLabel("Phone:", 30, y)); txtPhone = MakeText(150, y); y += 40;
            Controls.Add(MakeLabel("Address:", 30, y)); txtAddress = MakeText(150, y); y += 40;
            Controls.Add(MakeLabel("Password:", 30, y)); txtPassword = MakeText(150, y); txtPassword.PasswordChar = '*'; y += 40;
            Controls.Add(MakeLabel("Confirm:", 30, y)); txtConfirm = MakeText(150, y); txtConfirm.PasswordChar = '*'; y += 50;

            var btnRegister = new Button { Text = "Register", Location = new Point(150, y), Width = 100 };
            var btnCancel = new Button { Text = "Cancel", Location = new Point(270, y), Width = 100 };
            btnRegister.Click += BtnRegister_Click;
            btnCancel.Click += (s, e) => Close();
            Controls.Add(btnRegister);
            Controls.Add(btnCancel);
        }

        private Label MakeLabel(string text, int x, int y) => new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true };
        private TextBox MakeText(int x, int y) { var t = new TextBox { Location = new Point(x, y), Width = 250 }; Controls.Add(t); return t; }

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
