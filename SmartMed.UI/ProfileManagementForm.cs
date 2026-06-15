using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class ProfileManagementForm : Form
    {
        private TextBox txtName, txtEmail, txtPhone, txtAddress;
        private readonly CustomerService _service = new CustomerService();

        public ProfileManagementForm()
        {
            Text = "Manage Profile";
            Size = new Size(450, 320);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            var c = Session.CurrentCustomer;
            int y = 30;
            Controls.Add(Lbl("Full Name:", 30, y)); txtName = Txt(140, y); txtName.Text = c.Name; y += 40;
            Controls.Add(Lbl("Email:", 30, y)); txtEmail = Txt(140, y); txtEmail.Text = c.Email; y += 40;
            Controls.Add(Lbl("Phone:", 30, y)); txtPhone = Txt(140, y); txtPhone.Text = c.Phone; y += 40;
            Controls.Add(Lbl("Address:", 30, y)); txtAddress = Txt(140, y, 250); txtAddress.Text = c.Address; y += 50;

            var btnSave = new Button { Text = "Save", Location = new Point(140, y), Width = 100 };
            btnSave.Click += BtnSave_Click;
            Controls.Add(btnSave);
        }

        private Label Lbl(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y + 3), AutoSize = true };
        private TextBox Txt(int x, int y, int w = 250) { var t = new TextBox { Location = new Point(x, y), Width = w }; Controls.Add(t); return t; }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var c = Session.CurrentCustomer;
                c.Name = txtName.Text.Trim();
                c.Email = txtEmail.Text.Trim();
                c.Phone = txtPhone.Text.Trim();
                c.Address = txtAddress.Text.Trim();
                _service.Update(c);
                Session.CurrentCustomer = c;
                MessageBox.Show("Profile updated.");
                Close();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
