using System;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class ProfileManagementView : UserControl
    {
        private readonly CustomerService _service = new CustomerService();

        public ProfileManagementView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void ProfileManagementView_Load(object sender, EventArgs e)
        {
            UiFactory.ApplyTextBoxStyle(txtName, 300);
            UiFactory.ApplyTextBoxStyle(txtEmail, 300);
            UiFactory.ApplyTextBoxStyle(txtPhone, 300);
            UiFactory.ApplyTextBoxStyle(txtAddress, 400);

            if (UiFactory.IsDesignMode(this)) return;

            var c = Session.CurrentCustomer;
            txtName.Text = c.Name;
            txtEmail.Text = c.Email;
            txtPhone.Text = c.Phone;
            txtAddress.Text = c.Address;
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
