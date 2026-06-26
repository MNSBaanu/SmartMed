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
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                Width = GetScrollContentWidth()
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            var row = 0;
            var heading = UiTheme.CreateSectionHeading("Personal Details");
            root.Controls.Add(heading, 0, row++);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            AddFieldRow(root, ref row, ValidationService.RequiredLabel("Full Name"), out txtName, first: true);
            AddFieldRow(root, ref row, ValidationService.RequiredLabel("Email"), out txtEmail);
            AddFieldRow(root, ref row, ValidationService.RequiredLabel("Phone"), out txtPhone);
            txtPhone.MaxLength = 14;
            AddFieldRow(root, ref row, ValidationService.RequiredLabel("Address"), out txtAddress, multiline: true);

            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0)
            };
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
            root.Controls.Add(actions, 0, row++);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            WireScrollRoot(root, minHeight: 420);
        }

        private static void AddFieldRow(TableLayoutPanel root, ref int row, string labelText, out TextBox textBox,
            bool first = false, bool multiline = false)
        {
            var label = new Label
            {
                Text = labelText,
                AutoSize = true,
                Margin = new Padding(
                    0,
                    first ? UiTheme.CustomerControlGap : UiTheme.CustomerSectionGap,
                    0,
                    4)
            };
            root.Controls.Add(label, 0, row++);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            textBox = new TextBox
            {
                Dock = DockStyle.Top,
                Height = multiline ? 52 : 32,
                Multiline = multiline,
                ScrollBars = multiline ? ScrollBars.Vertical : ScrollBars.None,
                Margin = new Padding(0, 0, 0, 0)
            };
            root.Controls.Add(textBox, 0, row++);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
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
