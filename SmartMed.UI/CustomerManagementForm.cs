using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class CustomerManagementForm : Form
    {
        private DataGridView grid;
        private TextBox txtName, txtEmail, txtPhone, txtAddress;
        private readonly CustomerService _service = new CustomerService();
        private int? _selectedId;

        public CustomerManagementForm()
        {
            Text = "Manage Customers";
            Size = new Size(800, 480);
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView { Location = new Point(20, 20), Size = new Size(740, 200), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            grid.SelectionChanged += (s, e) =>
            {
                if (grid.CurrentRow?.DataBoundItem is Business.Models.Customer c)
                {
                    _selectedId = c.CustomerID;
                    txtName.Text = c.Name; txtEmail.Text = c.Email; txtPhone.Text = c.Phone; txtAddress.Text = c.Address;
                }
            };

            int y = 240;
            Controls.Add(grid);
            Controls.Add(Lbl("Name:", 20, y)); txtName = Txt(120, y); y += 35;
            Controls.Add(Lbl("Email:", 20, y)); txtEmail = Txt(120, y); y += 35;
            Controls.Add(Lbl("Phone:", 20, y)); txtPhone = Txt(120, y); y += 35;
            Controls.Add(Lbl("Address:", 20, y)); txtAddress = Txt(120, y, 500); y += 45;

            var btnUpdate = new Button { Text = "Update Customer", Location = new Point(120, y), Width = 140 };
            btnUpdate.Click += BtnUpdate_Click;
            Controls.Add(btnUpdate);
            LoadGrid();
        }

        private Label Lbl(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y + 3), AutoSize = true };
        private TextBox Txt(int x, int y, int w = 250) { var t = new TextBox { Location = new Point(x, y), Width = w }; Controls.Add(t); return t; }

        private void LoadGrid() { grid.DataSource = _service.GetAll(); if (grid.Columns.Contains("Password")) grid.Columns["Password"].Visible = false; }

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
