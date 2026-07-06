using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
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

        protected override void DoRefreshPage() => Search();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            UiTheme.SetGridDataSource(grid, DesignTimePreviewData.SearchMedicineRows());
            if (grid.Columns.Contains("MedicineID"))
                grid.Columns["MedicineID"].Visible = false;
            UiTheme.BeautifyGridHeaders(grid);
            lblDetails.Text =
                "Amoxicillin 500mg | Antibiotic | LKR 427.50 | Stock: 12 | Rx: Yes | Discount: 5% | Promo: Active";
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(grid);
            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtCategory);
            UiTheme.StyleTextBox(txtMinPrice);
            UiTheme.StyleTextBox(txtMaxPrice);

            WirePanelBorder(panelGridOuter);
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "dash-border") return;
            panel.Tag = "dash-border";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnSearch.Click += (s, e) => Search();
            btnAddToCart.Click += BtnAdd_Click;
            grid.SelectionChanged += Grid_SelectionChanged;
        }

        private void Search()
        {
            if (!_servicesReady) return;

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
            lblDetails.Text =
                $"{name} | {category} | {price} | Stock: {stock} | Rx: {rx} | Discount: {discount} | Promo: {promo}";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!_servicesReady || grid?.CurrentRow == null) return;
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
                SmartMedMessageBox.Show(message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
