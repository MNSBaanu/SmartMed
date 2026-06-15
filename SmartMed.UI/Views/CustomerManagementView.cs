using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class CustomerManagementView : UserControl
    {
        private readonly CustomerService _service = new CustomerService();
        private int? _selectedId;

        public CustomerManagementView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void CustomerManagementView_Load(object sender, EventArgs e)
        {
            StitchUiHelper.SetupPageHeader(pageHeader, "Manage Customers",
                "View and update registered customer records.");
            StitchUiHelper.StyleGridCard(gridCard);
            StitchUiHelper.StyleFormCard(formPanel);
            StitchUiHelper.ApplySecondaryAccentButton(btnUpdate);
            StitchUiHelper.ApplyFieldLabel(lblName);
            StitchUiHelper.ApplyFieldLabel(lblEmail);
            StitchUiHelper.ApplyFieldLabel(lblPhone);
            StitchUiHelper.ApplyFieldLabel(lblAddress);

            UiFactory.ApplyDataGridStyle(grid);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UiFactory.ApplyTextBoxStyle(txtName, 300);
            UiFactory.ApplyTextBoxStyle(txtEmail, 300);
            UiFactory.ApplyTextBoxStyle(txtPhone, 300);
            UiFactory.ApplyTextBoxStyle(txtAddress, 500);

            if (UiFactory.IsDesignMode(this)) return;
            LoadGrid();
        }

        private void LoadGrid()
        {
            grid.DataSource = _service.GetAll();
            if (grid.Columns.Contains("Password")) grid.Columns["Password"].Visible = false;
            if (grid.Columns.Contains("CustomerID")) grid.Columns["CustomerID"].Visible = false;
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.CurrentRow?.DataBoundItem is Business.Models.Customer c)
            {
                _selectedId = c.CustomerID;
                txtName.Text = c.Name; txtEmail.Text = c.Email; txtPhone.Text = c.Phone; txtAddress.Text = c.Address;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue) { MessageBox.Show("Select a customer."); return; }
            try
            {
                var existing = _service.GetById(_selectedId.Value);
                existing.Name = txtName.Text.Trim();
                existing.Email = txtEmail.Text.Trim();
                existing.Phone = txtPhone.Text.Trim();
                existing.Address = txtAddress.Text.Trim();
                _service.Update(existing);
                LoadGrid();
                MessageBox.Show("Customer updated.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
