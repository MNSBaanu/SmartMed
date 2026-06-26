using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class ProfileManagementForm : CustomerShellForm
    {
        private bool _pageBuilt;
        private CustomerService _customers;
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtAddress;

        public ProfileManagementForm()
            : base(CustomerNavItem.Profile, "My Profile")
        {
            InitializeComponent();
        }

        internal ProfileManagementForm(bool embedded)
            : base(CustomerNavItem.Profile, "My Profile", embedded)
        {
        }

        private CustomerService Customers => GetRuntimeService(ref _customers);

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                LoadProfile();
        }

        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            var root = new Panel { Dock = DockStyle.Top, AutoSize = true, Width = GetScrollContentWidth() };

            var actions = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0) };
            var btnSave = new Button { Text = "Save Profile", Width = 120, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnSave.Click += BtnSave_Click;
            var btnPassword = new Button { Text = "Change Password", Width = 140, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnPassword.Click += (s, e) =>
            {
                using (var dlg = new ChangePasswordForm(isAdmin: false))
                    dlg.ShowDialog(this);
            };
            actions.Controls.Add(btnSave);
            actions.Controls.Add(btnPassword);

            var fieldAddress = CreateFieldGroup(ValidationService.RequiredLabel("Address"), out txtAddress);
            var fieldPhone = CreateFieldGroup(ValidationService.RequiredLabel("Phone"), out txtPhone);
            txtPhone.MaxLength = 14;
            var fieldEmail = CreateFieldGroup(ValidationService.RequiredLabel("Email"), out txtEmail);
            var fieldName = CreateFieldGroup(ValidationService.RequiredLabel("Full Name"), out txtName);

            // Dock.Top: last added appears at the top — add bottom sections first.
            root.Controls.Add(actions);
            root.Controls.Add(fieldAddress);
            root.Controls.Add(fieldPhone);
            root.Controls.Add(fieldEmail);
            root.Controls.Add(fieldName);

            WireScrollRoot(root, minHeight: 320);
        }

        private static Panel CreateFieldGroup(string labelText, out TextBox textBox)
        {
            var group = new Panel { Dock = DockStyle.Top, AutoSize = true, Margin = UiTheme.CustomerSectionMargin };
            textBox = new TextBox { Dock = DockStyle.Top, Width = 400 };
            var label = new Label
            {
                Text = labelText,
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 4)
            };
            group.Controls.Add(textBox);
            group.Controls.Add(label);
            return group;
        }

        public void RefreshProfile() => LoadProfile();

        private void LoadProfile()
        {
            var customer = Session.CurrentCustomer;
            if (customer == null) return;
            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Customers == null) return;
            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null) throw new InvalidOperationException("Please log in again.");

                var updated = new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Password = customer.Password
                };
                Customers.UpdateProfile(updated);
                Session.CurrentCustomer = Customers.GetById(customer.CustomerID);
                MessageBox.Show("Profile updated.", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Profile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDesignTimePreview()
        {
            txtName.Text = "Jane Doe";
            txtEmail.Text = "jane@email.com";
            txtPhone.Text = "0779876543";
            txtAddress.Text = "45 Park Road, Kandy";
        }
    }
}
