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
            panelContent.Controls.Clear();
            var root = new Panel { Dock = DockStyle.Top, AutoSize = true, Width = GetScrollContentWidth(), Padding = new Padding(0, 0, 0, 16) };

            txtName = CreateField(root, ValidationService.RequiredLabel("Full Name"), 0);
            txtEmail = CreateField(root, ValidationService.RequiredLabel("Email"), 40);
            txtPhone = CreateField(root, ValidationService.RequiredLabel("Phone"), 80);
            txtPhone.MaxLength = 14;
            txtAddress = CreateField(root, ValidationService.RequiredLabel("Address"), 120);

            var actions = new FlowLayoutPanel { Location = new Point(0, 170), AutoSize = true };
            var btnSave = new Button { Text = "Save Profile", Width = 120, Height = 32 };
            btnSave.Click += BtnSave_Click;
            var btnPassword = new Button { Text = "Change Password", Width = 140, Height = 32 };
            btnPassword.Click += (s, e) =>
            {
                using (var dlg = new ChangePasswordForm(isAdmin: false))
                    dlg.ShowDialog(this);
            };
            actions.Controls.Add(btnSave);
            actions.Controls.Add(btnPassword);
            root.Controls.Add(actions);

            WireScrollRoot(root);
        }

        private static TextBox CreateField(Panel parent, string label, int top)
        {
            parent.Controls.Add(new Label { Text = label, Location = new Point(0, top), AutoSize = true });
            var box = new TextBox { Location = new Point(0, top + 20), Width = 400 };
            parent.Controls.Add(box);
            return box;
        }

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
