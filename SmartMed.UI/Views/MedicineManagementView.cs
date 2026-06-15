using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.Data.Models;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class MedicineManagementView : UserControl
    {
        private readonly MedicineService _service = new MedicineService();
        private int? _selectedId;

        public MedicineManagementView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void MedicineManagementView_Load(object sender, EventArgs e)
        {
            StitchUiHelper.SetupPageHeader(pageHeader, "Manage Medicines",
                "Update and monitor pharmaceutical inventory levels.");
            StitchUiHelper.StyleGridCard(gridCard);
            StitchUiHelper.StyleFormCard(formPanel);
            StitchUiHelper.ApplyPrimaryAccentButton(btnAdd);
            StitchUiHelper.ApplySecondaryAccentButton(btnUpdate);
            StitchUiHelper.ApplySecondaryButton(btnDelete);
            StitchUiHelper.ApplySecondaryButton(btnClear);
            StitchUiHelper.ApplyFieldLabel(lblName);
            StitchUiHelper.ApplyFieldLabel(lblCategory);
            StitchUiHelper.ApplyFieldLabel(lblDosage);
            StitchUiHelper.ApplyFieldLabel(lblPrice);
            StitchUiHelper.ApplyFieldLabel(lblStock);
            StitchUiHelper.ApplyFieldLabel(lblSupplier);
            StitchUiHelper.ApplyFieldLabel(lblExpiry);

            UiFactory.ApplyDataGridStyle(grid);
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UiFactory.ApplyTextBoxStyle(txtName, 170);
            UiFactory.ApplyTextBoxStyle(txtCategory, 170);
            UiFactory.ApplyTextBoxStyle(txtDosage, 170);
            UiFactory.ApplyTextBoxStyle(txtPrice, 170);
            UiFactory.ApplyTextBoxStyle(txtStock, 170);
            UiFactory.ApplyTextBoxStyle(txtSupplier, 170);

            if (UiFactory.IsDesignMode(this)) return;
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

        private void BtnAdd_Click(object sender, EventArgs e) => Save(false);

        private void BtnUpdate_Click(object sender, EventArgs e) => Save(true);

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

        private void BtnClear_Click(object sender, EventArgs e) => ClearForm();

        private void ClearForm()
        {
            _selectedId = null;
            txtName.Clear(); txtCategory.Clear(); txtDosage.Clear(); txtPrice.Clear(); txtStock.Clear(); txtSupplier.Clear();
            chkPrescription.Checked = false;
        }
    }
}
