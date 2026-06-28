using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public sealed partial class SearchMedicinesForm : CustomerPageControl
    {
        private readonly MedicineService _medicines = new MedicineService();

        public SearchMedicinesForm()
        {
            InitializeComponent();
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage()
        {
            SyncScrollRootWidth();
            Search();
        }

        private void BuildContent()
        {
            lblDetails = new Label
            {
                Dock = DockStyle.Top,
                Height = 48,
                AutoSize = false,
                Margin = new Padding(0, 0, 0, 12),
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface
            };

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 280,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 0, 0, 12)
            };
            UiTheme.ApplyClinicalGrid(grid);
            grid.SelectionChanged += Grid_SelectionChanged;

            var root = new Panel
            {
                AutoSize = true,
                MinimumSize = new Size(0, 420),
                BackColor = UiTheme.AdminSurface
            };

            root.Controls.Add(CreateCartRow());
            root.Controls.Add(lblDetails);
            root.Controls.Add(grid);
            root.Controls.Add(CreateFilterPanel());
            root.Controls.Add(AdminUiHelpers.CreatePageHeader("Browse Medicines",
                "Search the catalog and add items to your cart."));

            WireScrollRoot(root);
        }

        private Panel CreateFilterPanel()
        {
            var filter = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                WrapContents = true,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = UiTheme.AdminSurface
            };

            txtName = new TextBox { Width = 140, Margin = new Padding(0, 0, 8, 0) };
            txtCategory = new TextBox { Width = 120, Margin = new Padding(0, 0, 8, 0) };
            txtMinPrice = new TextBox { Width = 80, Margin = new Padding(0, 0, 8, 0) };
            txtMaxPrice = new TextBox { Width = 80, Margin = new Padding(0, 0, 8, 0) };
            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtCategory);
            UiTheme.StyleTextBox(txtMinPrice);
            UiTheme.StyleTextBox(txtMaxPrice);

            var btnSearch = AdminUiHelpers.CreateWinButton("Search", true, 80);
            btnSearch.Click += (s, e) => Search();

            filter.Controls.Add(MakeFilterLabel("Name:"));
            filter.Controls.Add(txtName);
            filter.Controls.Add(MakeFilterLabel("Category:"));
            filter.Controls.Add(txtCategory);
            filter.Controls.Add(MakeFilterLabel("Min:"));
            filter.Controls.Add(txtMinPrice);
            filter.Controls.Add(MakeFilterLabel("Max:"));
            filter.Controls.Add(txtMaxPrice);
            filter.Controls.Add(btnSearch);
            return filter;
        }

        private Panel CreateCartRow()
        {
            var cartRow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 12, 0, 0),
                BackColor = UiTheme.AdminSurface
            };

            numQty = new NumericUpDown { Minimum = 1, Maximum = 99, Value = 1, Width = 60, Margin = new Padding(0, 0, 8, 0) };
            numQty.Font = UiTheme.UiFont;

            var btnAdd = AdminUiHelpers.CreateWinButton("Add to Cart", true, 120);
            btnAdd.Click += BtnAdd_Click;

            cartRow.Controls.Add(MakeFilterLabel("Qty:"));
            cartRow.Controls.Add(numQty);
            cartRow.Controls.Add(btnAdd);
            return cartRow;
        }

        private static Label MakeFilterLabel(string text) =>
            new Label
            {
                Text = text,
                AutoSize = true,
                Padding = new Padding(0, 6, 4, 0),
                Margin = new Padding(0, 0, 4, 0),
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface
            };

        private void Search()
        {
            decimal? min = decimal.TryParse(txtMinPrice?.Text, out var minVal) ? minVal : (decimal?)null;
            decimal? max = decimal.TryParse(txtMaxPrice?.Text, out var maxVal) ? maxVal : (decimal?)null;

            var results = _medicines.SearchForCustomers(txtName?.Text ?? "", txtCategory?.Text ?? "", min, max)
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
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid?.CurrentRow == null) return;
            var name = grid.CurrentRow.Cells["MedicineName"].Value?.ToString();
            var category = grid.CurrentRow.Cells["Category"].Value?.ToString();
            var price = grid.CurrentRow.Cells["Price"].Value?.ToString();
            var stock = grid.CurrentRow.Cells["StockQuantity"].Value?.ToString();
            var rx = grid.CurrentRow.Cells["Rx"].Value?.ToString();
            var discount = grid.CurrentRow.Cells["Discount"].Value?.ToString();
            var promo = grid.CurrentRow.Cells["Promo"].Value?.ToString();
            lblDetails.Text = $"{name} | {category} | {price} | Stock: {stock} | Rx: {rx} | Discount: {discount} | Promo: {promo}";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (grid?.CurrentRow == null) return;
            try
            {
                var id = Convert.ToInt32(grid.CurrentRow.Cells["MedicineID"].Value);
                var medicine = _medicines.GetById(id);
                if (medicine == null) return;

                _medicines.ValidateForCustomerPurchase(medicine);
                var qty = (int)numQty.Value;
                CartService.Add(medicine, qty, _medicines);
                var offer = _medicines.GetCustomerOfferDisplay(medicine);
                var message = offer != "—"
                    ? $"{medicine.MedicineName} added to cart.\n{offer}"
                    : $"{medicine.MedicineName} added to cart.";
                MessageBox.Show(message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
