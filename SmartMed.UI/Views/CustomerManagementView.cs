using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class CustomerManagementView : UserControl
    {
        private DataGridView grid;
        private TextBox txtName, txtEmail, txtPhone, txtAddress;
        private readonly CustomerService _service = new CustomerService();
        private int? _selectedId;

        public CustomerManagementView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Manage Customers");
            header.Dock = DockStyle.Top;
            Controls.Add(header);

            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            Controls.Add(body);

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 200,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(grid);
            grid.SelectionChanged += (s, e) =>
            {
                if (grid.CurrentRow?.DataBoundItem is Business.Models.Customer c)
                {
                    _selectedId = c.CustomerID;
                    txtName.Text = c.Name; txtEmail.Text = c.Email; txtPhone.Text = c.Phone; txtAddress.Text = c.Address;
                }
            };
            body.Controls.Add(grid);

            var formPanel = new Panel { Dock = DockStyle.Top, Height = 180, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            body.Controls.Add(formPanel);

            int y = 0;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Name:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtName = new TextBox(); UiFactory.ApplyTextBoxStyle(txtName, 300); txtName.Location = new Point(80, y); formPanel.Controls.Add(txtName);
            y += 35;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Email:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtEmail = new TextBox(); UiFactory.ApplyTextBoxStyle(txtEmail, 300); txtEmail.Location = new Point(80, y); formPanel.Controls.Add(txtEmail);
            y += 35;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Phone:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtPhone = new TextBox(); UiFactory.ApplyTextBoxStyle(txtPhone, 300); txtPhone.Location = new Point(80, y); formPanel.Controls.Add(txtPhone);
            y += 35;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Address:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(0, y);
            txtAddress = new TextBox(); UiFactory.ApplyTextBoxStyle(txtAddress, 500); txtAddress.Location = new Point(80, y); formPanel.Controls.Add(txtAddress);
            y += 45;

            var btnUpdate = UiFactory.CreatePrimaryButton("Update Customer", 140);
            btnUpdate.Location = new Point(80, y);
            btnUpdate.Click += BtnUpdate_Click;
            formPanel.Controls.Add(btnUpdate);

            LoadGrid();
        }

        private void LoadGrid()
        {
            grid.DataSource = _service.GetAll();
            if (grid.Columns.Contains("Password")) grid.Columns["Password"].Visible = false;
            if (grid.Columns.Contains("CustomerID")) grid.Columns["CustomerID"].Visible = false;
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
