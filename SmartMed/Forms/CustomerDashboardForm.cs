using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class CustomerDashboardForm : CustomerShellForm
    {
        private bool _pageBuilt;
        private Label lblWelcome;
        private Label lblCart;
        private Label lblOrders;
        private Label lblPromotions;
        private DataGridView gridRecent;

        public CustomerDashboardForm()
            : base(CustomerNavItem.Home, "Customer Home")
        {
            InitializeComponent();
        }

        internal CustomerDashboardForm(bool embedded)
            : base(CustomerNavItem.Home, "Customer Home", embedded)
        {
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                RefreshData();
        }

        public void RefreshData()
        {
            if (IsDesignHost() || lblWelcome == null || gridRecent == null) return;
            var customer = Session.CurrentCustomer;
            lblWelcome.Text = $"Welcome, {customer?.Name ?? "Customer"}";
            lblCart.Text = CartService.ItemCount.ToString();
            var orders = new OrderService().GetByCustomer(customer?.CustomerID ?? 0);
            lblOrders.Text = orders.Count(o => o.Status != "Delivered").ToString();
            var medicineService = new MedicineService();
            var promos = medicineService.GetAll().Count(m => medicineService.IsPromotionActive(m));
            lblPromotions.Text = promos.ToString();
            gridRecent.DataSource = orders.Take(5).Select(o => new
            {
                OrderRef = $"#SM-{o.OrderID:D4}",
                o.OrderDate,
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}"
            }).ToList();
        }

        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                Width = GetScrollContentWidth()
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            lblWelcome = new Label { AutoSize = true, Font = UiTheme.UiFontBold, Margin = UiTheme.CustomerSectionMargin };
            root.Controls.Add(lblWelcome);

            var stats = new TableLayoutPanel { Dock = DockStyle.Top, ColumnCount = 3, Height = 90, Margin = UiTheme.CustomerSectionMargin };
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));
            lblCart = new Label();
            lblOrders = new Label();
            lblPromotions = new Label();
            stats.Controls.Add(CreateStatCard("Items in Cart", lblCart), 0, 0);
            stats.Controls.Add(CreateStatCard("Active Orders", lblOrders), 1, 0);
            stats.Controls.Add(CreateStatCard("Promotions", lblPromotions), 2, 0);
            root.Controls.Add(stats);

            root.Controls.Add(UiTheme.CreateSectionHeading("Recent Orders"));
            gridRecent = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 220,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = UiTheme.CustomerSectionMargin
            };
            root.Controls.Add(gridRecent);

            var actions = new FlowLayoutPanel { AutoSize = true, Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0) };
            actions.Controls.Add(CreateNavButton("Browse Medicines", () => GoToCustomerSection(CustomerNavItem.Browse)));
            actions.Controls.Add(CreateNavButton("View Cart", () => GoToCustomerSection(CustomerNavItem.Cart)));
            actions.Controls.Add(CreateNavButton("Change Password", () =>
            {
                using (var dlg = new ChangePasswordForm(isAdmin: false))
                    dlg.ShowDialog(this);
            }));
            root.Controls.Add(actions);

            WireScrollRoot(root, minHeight: 520);
        }

        private static Panel CreateStatCard(string title, Label valueLabel)
        {
            var card = new Panel { Dock = DockStyle.Fill, Height = 80, Padding = new Padding(16), Margin = new Padding(0, 0, UiTheme.CustomerControlGap, 0) };
            card.Controls.Add(new Label { Text = title, Dock = DockStyle.Top, Height = 20, ForeColor = SystemColors.GrayText });
            valueLabel.Text = "0";
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.Font = UiTheme.UiFontBold;
            card.Controls.Add(valueLabel);
            return card;
        }

        private Button CreateNavButton(string text, Action onClick)
        {
            var btn = new Button { Text = text, Width = 160, Height = 36, Margin = UiTheme.CustomerControlMargin };
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private void LoadDesignTimePreview()
        {
            lblWelcome.Text = "Welcome, Jane Doe";
            lblCart.Text = "2";
            lblOrders.Text = "1";
            lblPromotions.Text = "3";
            gridRecent.DataSource = new[]
            {
                new { OrderRef = "#SM-0002", OrderDate = DateTime.Today, Status = "Pending", Total = "LKR 15.00" }
            };
        }
    }
}
