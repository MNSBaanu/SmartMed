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

            ClinicalUi.PreparePagePanel(PagePanel);

            PagePanel.Controls.Clear();



            var root = new Panel

            {

                Dock = DockStyle.Top,

                AutoSize = true,

                Width = GetScrollContentWidth(),

                BackColor = UiTheme.AdminSurface

            };



            root.Controls.Add(ClinicalUi.CreatePageHeader("Browse Medicines",
                "Search the catalog and add items to your cart."));

            var filter = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                WrapContents = true,
                Margin = UiTheme.CustomerSectionMargin,
                BackColor = UiTheme.AdminSurface
            };

            txtName = new TextBox { Width = 140, Margin = UiTheme.CustomerControlMargin };

            txtCategory = new TextBox { Width = 120, Margin = UiTheme.CustomerControlMargin };

            txtMinPrice = new TextBox { Width = 80, Margin = UiTheme.CustomerControlMargin };

            txtMaxPrice = new TextBox { Width = 80, Margin = UiTheme.CustomerControlMargin };

            UiTheme.StyleTextBox(txtName);

            UiTheme.StyleTextBox(txtCategory);

            UiTheme.StyleTextBox(txtMinPrice);

            UiTheme.StyleTextBox(txtMaxPrice);



            var btnSearch = ClinicalUi.CreateButton("Search", primary: true, width: 80, height: 32);

            btnSearch.Click += (s, e) => Search();



            void AddFilterLabel(string text)

            {

                var lbl = new Label { Text = text, AutoSize = true, Padding = new Padding(0, 8, 0, 0), Margin = UiTheme.CustomerControlMargin };

                ClinicalUi.StyleFieldLabel(lbl);

                filter.Controls.Add(lbl);

            }



            AddFilterLabel("Name:");

            filter.Controls.Add(txtName);

            AddFilterLabel("Category:");

            filter.Controls.Add(txtCategory);

            AddFilterLabel("Min:");

            filter.Controls.Add(txtMinPrice);

            AddFilterLabel("Max:");

            filter.Controls.Add(txtMaxPrice);

            filter.Controls.Add(btnSearch);



            grid = ClinicalUi.CreateGrid();

            grid.Dock = DockStyle.Top;

            grid.Height = 280;

            grid.Margin = UiTheme.CustomerSectionMargin;

            grid.SelectionChanged += Grid_SelectionChanged;



            lblDetails = new Label

            {

                Dock = DockStyle.Top,

                Height = 60,

                AutoSize = false,

                Margin = UiTheme.CustomerSectionMargin,

                BackColor = UiTheme.AdminSurface

            };

            ClinicalUi.StyleMutedLabel(lblDetails);



            var cartRow = new FlowLayoutPanel

            {

                Dock = DockStyle.Top,

                AutoSize = true,

                Margin = new Padding(0, UiTheme.CustomerControlGap, 0, 0),

                BackColor = UiTheme.AdminSurface

            };

            var lblQty = new Label { Text = "Qty:", AutoSize = true, Padding = new Padding(0, 8, 0, 0), Margin = UiTheme.CustomerControlMargin };

            ClinicalUi.StyleFieldLabel(lblQty);

            numQty = new NumericUpDown { Minimum = 1, Maximum = 99, Value = 1, Width = 60, Margin = UiTheme.CustomerControlMargin };

            numQty.Font = UiTheme.UiFont;

            var btnAdd = ClinicalUi.CreateButton("Add to Cart", primary: true, width: 120, height: 32);

            btnAdd.Click += BtnAdd_Click;

            cartRow.Controls.Add(lblQty);

            cartRow.Controls.Add(numQty);

            cartRow.Controls.Add(btnAdd);



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

                    Discount = Medicines.GetCustomerDiscountDisplay(m),

                    Promo = Medicines.GetCustomerPromoDisplay(m)

                }).ToList();

            ClinicalUi.BindGrid(grid, results);

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

            var discount = grid.CurrentRow.Cells["Discount"].Value?.ToString();

            var promo = grid.CurrentRow.Cells["Promo"].Value?.ToString();

            lblDetails.Text = $"{name} | {category} | {price} | Stock: {stock} | Rx: {rx} | Discount: {discount} | Promo: {promo}";

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

                CartService.Add(medicine, qty, Medicines);

                var offer = Medicines.GetCustomerOfferDisplay(medicine);

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



        private void LoadDesignTimePreview()

        {

            ClinicalUi.BindGrid(grid, new[]

            {

                new { MedicineID = 1, MedicineName = "Paracetamol", Category = "Pain Relief", Price = "LKR 5.50", StockQuantity = 200, Rx = "No", Discount = "—", Promo = "—" },

                new { MedicineID = 2, MedicineName = "Amoxicillin", Category = "Antibiotic", Price = "LKR 10.80", StockQuantity = 80, Rx = "Yes", Discount = "10%", Promo = "Active" }

            });

            lblDetails.Text = "Paracetamol | Pain Relief | LKR 5.50 | Stock: 200";

        }

    }

}


