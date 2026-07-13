using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ProfileManagementForm : EmbeddedPageForm
    {
        private CustomerService _customers;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public ProfileManagementForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _customers = new CustomerService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
        }

        protected override void DoRefreshPage() => LoadProfile();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAddress.Text = string.Empty;
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtEmail);
            UiTheme.StyleTextBox(txtPhone);
            UiTheme.StyleTextBox(txtAddress);

        }

        private void PanelFormOuter_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawOuterPanelBorder(panelFormOuter, e);

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            UiTheme.EnableFieldNavigation(btnSaveProfile, txtName, txtEmail, txtPhone, txtAddress);
        }

        private void LoadProfile()
        {
            if (!_servicesReady) return;

            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
        }

        private void BtnSaveProfile_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var updated = new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Password = customer.Password
                };
                _customers.UpdateProfile(updated);
                Session.CurrentCustomer = _customers.GetById(customer.CustomerID);
                if (FindForm() is CustomerHostForm host)
                    host.RefreshProfileDisplay();

                SmartMedMessageBox.Show("Profile updated.", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Profile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnChangePassword_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            using (var dlg = new ChangePasswordForm(isAdmin: false))
                dlg.ShowDialog(FindForm());
        }
    }
}
