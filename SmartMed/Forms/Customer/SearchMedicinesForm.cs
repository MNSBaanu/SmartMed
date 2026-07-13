using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class SearchMedicinesForm : EmbeddedPageForm
    {
        private MedicineService _medicines;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public SearchMedicinesForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _medicines = new MedicineService();
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

        protected override void DoRefreshPage()
        {
            RefreshCategoryFilter();
            ApplyFilters();
        }

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            UiTheme.SetGridDataSource(grid, new object[0]);
            AddCartColumns();
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(grid);
            UiTheme.StyleTextBox(txtSearch);
            UiTheme.StyleTextBox(txtMinPrice);
            UiTheme.StyleTextBox(txtMaxPrice);
            UiTheme.StyleComboBox(cmbCategory);
            cmbCategory.FlatStyle = FlatStyle.Standard;
            UiTheme.ApplyFlatButton(btnSearch, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnClear, UiButtonStyle.Secondary);

            if (cmbCategory.Items.Count == 0)
            {
                cmbCategory.Items.Add("All categories");
                cmbCategory.SelectedIndex = 0;
            }
        }

        private void PanelGridOuter_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawOuterPanelBorder(panelGridOuter, e);

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            UiTheme.WireClinicalPlaceholderTextBox(txtSearch, "Search");
            UiTheme.WireClinicalPlaceholderTextBox(txtMinPrice, "Min");
            UiTheme.WireClinicalPlaceholderTextBox(txtMaxPrice, "Max");

            RefreshCategoryFilter();
            ApplyFilters();
        }

        private void BtnSearch_Click(object sender, EventArgs e) => ApplyFilters();

        private void BtnClear_Click(object sender, EventArgs e)
        {
            UiTheme.ResetClinicalPlaceholder(txtSearch, "Search");
            cmbCategory.SelectedIndex = 0;
            UiTheme.ResetClinicalPlaceholder(txtMinPrice, "Min");
            UiTheme.ResetClinicalPlaceholder(txtMaxPrice, "Max");
            ApplyFilters();
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyFilters();
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void TxtMinPrice_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void TxtMaxPrice_TextChanged(object sender, EventArgs e) => ApplyFilters();
        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void RefreshCategoryFilter()
        {
            if (!_servicesReady || cmbCategory == null) return;

            var selected = cmbCategory.SelectedItem?.ToString();
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All categories");

            var categories = _medicines.GetAll()
                .Where(_medicines.IsAvailableForSale)
                .Select(m => m.Category)
                .Where(c => !string.IsNullOrWhiteSpace(c))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c);

            foreach (var cat in categories)
                cmbCategory.Items.Add(cat);

            cmbCategory.SelectedIndex = 0;
            if (!string.IsNullOrEmpty(selected))
            {
                var idx = cmbCategory.Items.IndexOf(selected);
                if (idx >= 0)
                    cmbCategory.SelectedIndex = idx;
            }
        }

        private void ApplyFilters()
        {
            if (!_servicesReady) return;

            var results = GetFilteredMedicines()
                .Select(m => new
                {
                    m.MedicineID,
                    m.MedicineName,
                    m.Category,
                    Price = $"LKR {_medicines.GetEffectivePrice(m):N2}",
                    m.StockQuantity,
                    Rx = m.RequiresPrescription ? "Yes" : "No",
                    Discount = _medicines.GetCustomerDiscountDisplay(m),
                    Promo = _medicines.GetCustomerPromoDisplay(m)
                })
                .ToList();

            UiTheme.SetGridDataSource(grid, results);
            if (grid.Columns.Contains("MedicineID"))
                grid.Columns["MedicineID"].Visible = false;
            UiTheme.BeautifyGridHeaders(grid);
            AddCartColumns();
        }

        private System.Collections.Generic.List<Medicine> GetFilteredMedicines()
        {
            decimal? minPrice = decimal.TryParse(UiTheme.ReadTextBoxValue(txtMinPrice), out var min) ? min : (decimal?)null;
            decimal? maxPrice = decimal.TryParse(UiTheme.ReadTextBoxValue(txtMaxPrice), out var max) ? max : (decimal?)null;
            var category = cmbCategory?.SelectedIndex > 0 ? cmbCategory.SelectedItem?.ToString() : null;

            return _medicines.SearchForCustomers(
                UiTheme.ReadTextBoxValue(txtSearch),
                category,
                minPrice,
                maxPrice);
        }

        private void AddCartColumns()
        {
            grid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            foreach (DataGridViewColumn col in grid.Columns)
                col.ReadOnly = col.Name != "Qty";

            if (!grid.Columns.Contains("Qty"))
            {
                grid.Columns.Add(new DataGridViewNumericUpDownColumn
                {
                    Name = "Qty",
                    HeaderText = "Qty",
                    Width = 80,
                    ReadOnly = false,
                    Minimum = 1,
                    Maximum = 99,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }
            else
            {
                grid.Columns["Qty"].ReadOnly = false;
            }
            grid.Columns["Qty"].DefaultCellStyle.Padding = new Padding(2, 0, 20, 0);

            if (!grid.Columns.Contains("DetailsBtn"))
            {
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "DetailsBtn",
                    HeaderText = "",
                    Text = "Details",
                    UseColumnTextForButtonValue = true,
                    Width = 72,
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }

            if (!grid.Columns.Contains("AddBtn"))
            {
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "AddBtn",
                    HeaderText = "",
                    Text = "Add to Cart",
                    UseColumnTextForButtonValue = true,
                    Width = 120,
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Qty"].Value == null)
                    row.Cells["Qty"].Value = "1";
            }
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            var col = grid.Columns[e.ColumnIndex].Name;
            if (col == "DetailsBtn")
            {
                ShowMedicineDetails(grid.Rows[e.RowIndex]);
                return;
            }
            if (col != "AddBtn") return;
            grid.EndEdit();
            AddRowToCart(grid.Rows[e.RowIndex]);
        }

        private void Grid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            ShowMedicineDetails(grid.Rows[e.RowIndex]);
        }

        private void ShowMedicineDetails(DataGridViewRow row)
        {
            if (!_servicesReady || row == null) return;
            if (!grid.Columns.Contains("MedicineID")) return;

            var idValue = row.Cells["MedicineID"].Value;
            if (idValue == null || !int.TryParse(idValue.ToString(), out var id)) return;

            var medicine = _medicines.GetById(id);
            if (medicine == null) return;

            MedicineDetailsDialog.Show(FindForm(), medicine, _medicines);
        }

        private void AddRowToCart(DataGridViewRow row)
        {
            if (!_servicesReady || row == null) return;
            try
            {
                var id = Convert.ToInt32(row.Cells["MedicineID"].Value);
                var medicine = _medicines.GetById(id);
                if (medicine == null) return;

                _medicines.ValidateForCustomerPurchase(medicine);
                var qty = 1;
                if (decimal.TryParse(row.Cells["Qty"].Value?.ToString(), out var qtyVal) && qtyVal >= 1)
                    qty = (int)qtyVal;
                CartService.Add(medicine, qty, _medicines);
                var offer = _medicines.GetCustomerOfferDisplay(medicine);
                var message = offer != "—"
                    ? $"{medicine.MedicineName} added to cart.\n{offer}"
                    : $"{medicine.MedicineName} added to cart.";
                SmartMedMessageBox.Show(message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ApplyFilters();
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    internal sealed class DataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        public DataGridViewNumericUpDownColumn() : base(new SpinCell()) { }
        public decimal Minimum { get; set; } = 1;
        public decimal Maximum { get; set; } = 99;

        private sealed class SpinCell : DataGridViewTextBoxCell
        {
            private const int BtnW = 17;

            private static System.Drawing.Rectangle SpinArea(System.Drawing.Rectangle c) =>
                new System.Drawing.Rectangle(c.Right - BtnW - 2, c.Top + 2, BtnW, c.Height - 4);
            private static System.Drawing.Rectangle UpRect(System.Drawing.Rectangle c)
            { var a = SpinArea(c); return new System.Drawing.Rectangle(a.Left, a.Top, a.Width, a.Height / 2); }
            private static System.Drawing.Rectangle DownRect(System.Drawing.Rectangle c)
            { var a = SpinArea(c); return new System.Drawing.Rectangle(a.Left, a.Top + a.Height / 2, a.Width, a.Height / 2); }

            protected override void Paint(Graphics graphics, System.Drawing.Rectangle clipBounds,
                System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                object value, object formattedValue, string errorText,
                DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle,
                DataGridViewPaintParts paintParts)
            {
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue,
                    errorText, cellStyle, advancedBorderStyle, paintParts);

                if ((paintParts & DataGridViewPaintParts.ContentBackground) == 0) return;

                var up = UpRect(cellBounds);
                var down = DownRect(cellBounds);
                if (up.Width <= 0 || up.Height <= 0) return;

                ComboBoxRenderer.DrawDropDownButton(graphics, up, ComboBoxState.Normal);
                ComboBoxRenderer.DrawDropDownButton(graphics, down, ComboBoxState.Normal);

                var arrowSize = 4;
                var cx = up.Left + up.Width / 2;
                var cyUp = up.Top + up.Height / 2 + 1;
                var cyDown = down.Top + down.Height / 2 + 1;
                using (var brush = new SolidBrush(Color.FromArgb(65, 72, 71)))
                {
                    graphics.FillPolygon(brush, new[]
                    {
                        new Point(cx - arrowSize, cyUp - 1),
                        new Point(cx + arrowSize, cyUp - 1),
                        new Point(cx, cyUp - arrowSize - 1)
                    });
                    graphics.FillPolygon(brush, new[]
                    {
                        new Point(cx - arrowSize, cyDown - 1),
                        new Point(cx + arrowSize, cyDown - 1),
                        new Point(cx, cyDown + arrowSize - 1)
                    });
                }
            }

            protected override void OnClick(DataGridViewCellEventArgs e)
            {
                base.OnClick(e);
                if (DataGridView == null) return;

                var rect = DataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false);
                var pt = DataGridView.PointToClient(Cursor.Position);
                var local = new Point(pt.X - rect.X, pt.Y - rect.Y);
                var cellRect = new System.Drawing.Rectangle(Point.Empty, rect.Size);

                if (!SpinArea(cellRect).Contains(local)) return;

                var col = DataGridView.Columns[e.ColumnIndex] as DataGridViewNumericUpDownColumn;
                var min = col?.Minimum ?? 1;
                var max = col?.Maximum ?? 99;
                var current = 1m;
                decimal.TryParse(Value?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out current);

                if (UpRect(cellRect).Contains(local))
                    current = Math.Min(max, current + 1);
                else if (DownRect(cellRect).Contains(local))
                    current = Math.Max(min, current - 1);

                Value = ((int)current).ToString(CultureInfo.InvariantCulture);
                DataGridView.InvalidateCell(this);
            }
        }
    }
}
