using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.Data.Models;

namespace SmartMed.UI
{
    public class MedicineManagementForm : Form
    {
        private DataGridView grid;
        private TextBox txtName, txtCategory, txtDosage, txtPrice, txtStock, txtSupplier;
        private DateTimePicker dtpExpiry;
        private CheckBox chkPrescription;
        private readonly MedicineService _service = new MedicineService();
        private int? _selectedId;

        public MedicineManagementForm()
        {
            Text = "Manage Medicines";
            Size = new Size(900, 550);
            StartPosition = FormStartPosition.CenterParent;

            grid = new DataGridView { Location = new Point(20, 20), Size = new Size(840, 200), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            grid.SelectionChanged += Grid_SelectionChanged;

            int x = 20, y = 240;
            Controls.Add(grid);
            Controls.Add(Lbl("Name:", x, y)); txtName = Txt(x + 100, y); Controls.Add(txtName);
            Controls.Add(Lbl("Category:", x + 300, y)); txtCategory = Txt(x + 400, y); Controls.Add(txtCategory); y += 35;
            Controls.Add(Lbl("Dosage:", x, y)); txtDosage = Txt(x + 100, y); Controls.Add(txtDosage);
            Controls.Add(Lbl("Price:", x + 300, y)); txtPrice = Txt(x + 400, y); Controls.Add(txtPrice); y += 35;
            Controls.Add(Lbl("Stock:", x, y)); txtStock = Txt(x + 100, y); Controls.Add(txtStock);
            Controls.Add(Lbl("Supplier:", x + 300, y)); txtSupplier = Txt(x + 400, y); Controls.Add(txtSupplier); y += 35;
            Controls.Add(Lbl("Expiry:", x, y)); dtpExpiry = new DateTimePicker { Location = new Point(x + 100, y), Width = 180 }; Controls.Add(dtpExpiry);
            chkPrescription = new CheckBox { Text = "Requires Prescription", Location = new Point(x + 400, y), AutoSize = true }; Controls.Add(chkPrescription); y += 45;

            var btnAdd = new Button { Text = "Add", Location = new Point(x, y), Width = 80 };
            var btnUpdate = new Button { Text = "Update", Location = new Point(x + 90, y), Width = 80 };
            var btnDelete = new Button { Text = "Delete", Location = new Point(x + 180, y), Width = 80 };
            var btnClear = new Button { Text = "Clear", Location = new Point(x + 270, y), Width = 80 };
            btnAdd.Click += (s, e) => Save(false);
            btnUpdate.Click += (s, e) => Save(true);
            btnDelete.Click += BtnDelete_Click;
            btnClear.Click += (s, e) => ClearForm();
            Controls.AddRange(new Control[] { btnAdd, btnUpdate, btnDelete, btnClear });

            LoadGrid();
        }

        private Label Lbl(string t, int x, int y) => new Label { Text = t, Location = new Point(x, y + 3), AutoSize = true };
        private TextBox Txt(int x, int y) => new TextBox { Location = new Point(x, y), Width = 170 };

        private void LoadGrid()
        {
            grid.DataSource = null;
            grid.DataSource = _service.GetAll();
            grid.Columns["MedicineID"].Visible = false;
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
