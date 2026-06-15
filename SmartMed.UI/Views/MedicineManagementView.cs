using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.Data.Models;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class MedicineManagementView : UserControl
    {
        private DataGridView grid;
        private TextBox txtName, txtCategory, txtDosage, txtPrice, txtStock, txtSupplier;
        private DateTimePicker dtpExpiry;
        private CheckBox chkPrescription;
        private readonly MedicineService _service = new MedicineService();
        private int? _selectedId;

        public MedicineManagementView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Manage Medicines");
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
            grid.SelectionChanged += Grid_SelectionChanged;
            body.Controls.Add(grid);

            var formPanel = new Panel { Dock = DockStyle.Top, Height = 200, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            body.Controls.Add(formPanel);

            int x = 0, y = 0;
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Name:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x, y);
            txtName = new TextBox(); UiFactory.ApplyTextBoxStyle(txtName, 170); txtName.Location = new Point(x + 80, y); formPanel.Controls.Add(txtName);
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Category:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x + 280, y);
            txtCategory = new TextBox(); UiFactory.ApplyTextBoxStyle(txtCategory, 170); txtCategory.Location = new Point(x + 360, y); formPanel.Controls.Add(txtCategory);
            y += 35;

            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Dosage:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x, y);
            txtDosage = new TextBox(); UiFactory.ApplyTextBoxStyle(txtDosage, 170); txtDosage.Location = new Point(x + 80, y); formPanel.Controls.Add(txtDosage);
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Price:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x + 280, y);
            txtPrice = new TextBox(); UiFactory.ApplyTextBoxStyle(txtPrice, 170); txtPrice.Location = new Point(x + 360, y); formPanel.Controls.Add(txtPrice);
            y += 35;

            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Stock:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x, y);
            txtStock = new TextBox(); UiFactory.ApplyTextBoxStyle(txtStock, 170); txtStock.Location = new Point(x + 80, y); formPanel.Controls.Add(txtStock);
            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Supplier:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x + 280, y);
            txtSupplier = new TextBox(); UiFactory.ApplyTextBoxStyle(txtSupplier, 170); txtSupplier.Location = new Point(x + 360, y); formPanel.Controls.Add(txtSupplier);
            y += 35;

            formPanel.Controls.Add(UiFactory.CreateFieldLabel("Expiry:")); formPanel.Controls[formPanel.Controls.Count - 1].Location = new Point(x, y);
            dtpExpiry = new DateTimePicker { Location = new Point(x + 80, y), Width = 170, Font = ClinicalPrecisionTheme.BodyFont };
            formPanel.Controls.Add(dtpExpiry);
            chkPrescription = new CheckBox { Text = "Requires Prescription", Location = new Point(x + 360, y + 2), AutoSize = true, Font = ClinicalPrecisionTheme.BodyFont };
            formPanel.Controls.Add(chkPrescription);
            y += 45;

            var btnAdd = UiFactory.CreatePrimaryButton("Add", 80);
            var btnUpdate = UiFactory.CreatePrimaryButton("Update", 80);
            var btnDelete = UiFactory.CreateSecondaryButton("Delete", 80);
            var btnClear = UiFactory.CreateSecondaryButton("Clear", 80);
            btnAdd.Location = new Point(x, y);
            btnUpdate.Location = new Point(x + 90, y);
            btnDelete.Location = new Point(x + 180, y);
            btnClear.Location = new Point(x + 270, y);
            btnAdd.Click += (s, e) => Save(false);
            btnUpdate.Click += (s, e) => Save(true);
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();
            formPanel.Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            LoadGrid();
        }

        private void LoadGrid()
        {
            grid.DataSource = null;
            grid.DataSource = _service.GetAll();
            if (grid.Columns.Contains("MedicineID")) grid.Columns["MedicineID"].Visible = false;
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null) return;
            var item = grid.CurrentRow.DataBoundItem as MedicineItem;
            if (item == null) return;
            _selectedId = item.MedicineID;
            txtName.Text = item.MedicineName;
            txtCategory.Text = item.Category;
            txtDosage.Text = item.Dosage;
            txtPrice.Text = item.Price.ToString();
            txtStock.Text = item.StockQuantity.ToString();
            txtSupplier.Text = item.Supplier;
            dtpExpiry.Value = item.ExpiryDate;
            chkPrescription.Checked = item.RequiresPrescription;
        }

        private void Save(bool isUpdate)
        {
            try
            {
                if (!decimal.TryParse(txtPrice.Text, out decimal price) || !int.TryParse(txtStock.Text, out int stock))
                {
                    MessageBox.Show("Enter valid price and stock.");
                    return;
                }
                var item = new MedicineItem
                {
                    MedicineID = isUpdate ? _selectedId ?? 0 : 0,
                    MedicineName = txtName.Text.Trim(),
                    Category = txtCategory.Text.Trim(),
                    Dosage = txtDosage.Text.Trim(),
                    Price = price,
                    StockQuantity = stock,
                    Supplier = txtSupplier.Text.Trim(),
                    ExpiryDate = dtpExpiry.Value.Date,
                    RequiresPrescription = chkPrescription.Checked
                };
                if (isUpdate) _service.Update(item); else _service.Add(item);
                LoadGrid();
                ClearForm();
                MessageBox.Show(isUpdate ? "Medicine updated." : "Medicine added.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue) return;
            if (MessageBox.Show("Delete this medicine?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try { _service.Delete(_selectedId.Value); LoadGrid(); ClearForm(); }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void ClearForm()
        {
            _selectedId = null;
            txtName.Clear(); txtCategory.Clear(); txtDosage.Clear(); txtPrice.Clear(); txtStock.Clear(); txtSupplier.Clear();
            chkPrescription.Checked = false;
        }
    }
}
