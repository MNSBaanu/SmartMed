using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class SearchMedicinesForm : CustomerShellForm
    {
        private bool _pageBuilt;
        private MedicineService _medicines;
        private DataGridView grid;
        private TextBox txtName;
        private TextBox txtCategory;
        private TextBox txtMinPrice;
        private TextBox txtMaxPrice;
        private NumericUpDown numQty;
        private Label lblDetails;

        public SearchMedicinesForm()
            : base(CustomerNavItem.Browse, "Browse Medicines")
        {
            InitializeComponent();
        }

        internal SearchMedicinesForm(bool embedded)
            : base(CustomerNavItem.Browse, "Browse Medicines", embedded)
        {
        }

        private MedicineService Medicines => GetRuntimeService(ref _medicines);

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                Search();
        }

        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            var root = new Panel { Dock = DockStyle.Top, AutoSize = true, Width = GetScrollContentWidth() };

            var filter = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, WrapContents = true, Margin = UiTheme.CustomerSectionMargin };
            txtName = new TextBox { Width = 140, Margin = UiTheme.CustomerControlMargin };
            txtCategory = new TextBox { Width = 120, Margin = UiTheme.CustomerControlMargin };
            txtMinPrice = new TextBox { Width = 80, Margin = UiTheme.CustomerControlMargin };
            txtMaxPrice = new TextBox { Width = 80, Margin = UiTheme.CustomerControlMargin };
            var btnSearch = new Button { Text = "Search", Width = 80, Height = 28, Margin = UiTheme.CustomerControlMargin };
            btnSearch.Click += (s, e) => Search();
            filter.Controls.AddRange(new Control[]
            {
                new Label { Text = "Name:", AutoSize = true, Padding = new Padding(0, 6, 0, 0), Margin = UiTheme.CustomerControlMargin }, txtName,
                new Label { Text = "Category:", AutoSize = true, Padding = new Padding(0, 6, 0, 0), Margin = UiTheme.CustomerControlMargin }, txtCategory,
                new Label { Text = "Min:", AutoSize = true, Padding = new Padding(0, 6, 0, 0), Margin = UiTheme.CustomerControlMargin }, txtMinPrice,
                new Label { Text = "Max:", AutoSize = true, Padding = new Padding(0, 6, 0, 0), Margin = UiTheme.CustomerControlMargin }, txtMaxPrice,
                btnSearch
            });

            grid = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 280,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = UiTheme.CustomerSectionMargin
            };
            grid.SelectionChanged += Grid_SelectionChanged;

            lblDetails = new Label { Dock = DockStyle.Top, Height = 60, AutoSize = false, ForeColor = SystemColors.GrayText, Margin = UiTheme.CustomerSectionMargin };

            var cartRow = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Margin = new Padding(0, UiTheme.CustomerControlGap, 0, 0) };
            cartRow.Controls.Add(new Label { Text = "Qty:", AutoSize = true, Padding = new Padding(0, 6, 0, 0), Margin = UiTheme.CustomerControlMargin });
            numQty = new NumericUpDown { Minimum = 1, Maximum = 99, Value = 1, Width = 60, Margin = UiTheme.CustomerControlMargin };
            var btnAdd = new Button { Text = "Add to Cart", Width = 120, Height = 32, Margin = UiTheme.CustomerControlMargin };
            btnAdd.Click += BtnAdd_Click;
            cartRow.Controls.Add(numQty);
            cartRow.Controls.Add(btnAdd);

            // Dock.Top stacks with the last-added control at the top — add bottom sections first.
            root.Controls.Add(cartRow);
            root.Controls.Add(lblDetails);
            root.Controls.Add(grid);
            root.Controls.Add(filter);

            WireScrollRoot(root, minHeight: 420);
        }

        private void Search()
        {
            if (IsDesignHost() || Medicines == null) return;
            decimal? min = decimal.TryParse(txtMinPrice.Text, out var minVal) ? minVal : (decimal?)null;
            decimal? max = decimal.TryParse(txtMaxPrice.Text, out var maxVal) ? maxVal : (decimal?)null;
            var results = Medicines.SearchForCustomers(txtName.Text, txtCategory.Text, min, max)
                .Select(m => new
                {
                    m.MedicineID,
                    m.MedicineName,
                    m.Category,
                    Price = $"LKR {Medicines.GetEffectivePrice(m):N2}",
                    m.StockQuantity,
                    Rx = m.RequiresPrescription ? "Yes" : "No",
                    Promo = Medicines.IsPromotionActive(m) ? $"{m.DiscountPercent:N0}% off" : "-"
                }).ToList();
            grid.DataSource = results;
            if (grid.Columns.Contains("MedicineID"))
                grid.Columns["MedicineID"].Visible = false;
        }

        private void Grid_SelectionChanged(object sender, EventArgs e)
        {
            if (grid.CurrentRow == null) return;
            var name = grid.CurrentRow.Cells["MedicineName"].Value?.ToString();
            var category = grid.CurrentRow.Cells["Category"].Value?.ToString();
            var price = grid.CurrentRow.Cells["Price"].Value?.ToString();
            var stock = grid.CurrentRow.Cells["StockQuantity"].Value?.ToString();
            var rx = grid.CurrentRow.Cells["Rx"].Value?.ToString();
            var promo = grid.CurrentRow.Cells["Promo"].Value?.ToString();
            lblDetails.Text = $"{name} | {category} | {price} | Stock: {stock} | Rx: {rx} | {promo}";
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Medicines == null || grid.CurrentRow == null) return;
            try
            {
                var id = Convert.ToInt32(grid.CurrentRow.Cells["MedicineID"].Value);
                var medicine = Medicines.GetById(id);
                if (medicine == null) return;
                Medicines.ValidateForCustomerPurchase(medicine);
                var qty = (int)numQty.Value;
                CartService.Add(medicine, qty, Medicines.GetEffectivePrice(medicine));
                MessageBox.Show($"{medicine.MedicineName} added to cart.", "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadDesignTimePreview()
        {
            grid.DataSource = new[]
            {
                new { MedicineID = 1, MedicineName = "Paracetamol", Category = "Pain Relief", Price = "LKR 5.50", StockQuantity = 200, Rx = "No", Promo = "-" },
                new { MedicineID = 2, MedicineName = "Amoxicillin", Category = "Antibiotic", Price = "LKR 10.80", StockQuantity = 80, Rx = "Yes", Promo = "10% off" }
            };
            lblDetails.Text = "Paracetamol | Pain Relief | LKR 5.50 | Stock: 200";
        }
    }
}
