using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class ProfileManagementView : UserControl
    {
        private TextBox txtName, txtEmail, txtPhone, txtAddress;
        private readonly CustomerService _service = new CustomerService();

        public ProfileManagementView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Manage Profile");
            header.Dock = DockStyle.Top;
            Controls.Add(header);

            var formPanel = new Panel { Dock = DockStyle.Top, Height = 260, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            Controls.Add(formPanel);

            var c = Session.CurrentCustomer;
            int y = 0;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Full Name:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtName = new TextBox(); UiFactory.ApplyTextBoxStyle(txtName, 300); txtName.Location = new Point(100, y); txtName.Text = c.Name; formPanel.Controls.Add(txtName);
            y += 40;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Email:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtEmail = new TextBox(); UiFactory.ApplyTextBoxStyle(txtEmail, 300); txtEmail.Location = new Point(100, y); txtEmail.Text = c.Email; formPanel.Controls.Add(txtEmail);
            y += 40;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Phone:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtPhone = new TextBox(); UiFactory.ApplyTextBoxStyle(txtPhone, 300); txtPhone.Location = new Point(100, y); txtPhone.Text = c.Phone; formPanel.Controls.Add(txtPhone);
            y += 40;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Address:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtAddress = new TextBox(); UiFactory.ApplyTextBoxStyle(txtAddress, 400); txtAddress.Location = new Point(100, y); txtAddress.Text = c.Address; formPanel.Controls.Add(txtAddress);
            y += 50;

            var btnSave = UiFactory.CreatePrimaryButton("Save", 100);
            btnSave.Location = new Point(100, y);
            btnSave.Click += BtnSave_Click;
            formPanel.Controls.Add(btnSave);
        }

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
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
