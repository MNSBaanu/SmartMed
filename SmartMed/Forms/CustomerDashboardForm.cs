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
        private Label lblCart;
        private Label lblOrders;
        private Label lblPromotions;
        private Label _headerSubtitle;
        private DataGridView gridRecent;

        public CustomerDashboardForm()
            : base(CustomerNavItem.Home, "Customer Home")
        {
            InitializeComponent();
            CompleteDesignInitialization();
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
            if (IsDesignHost() || _headerSubtitle == null || gridRecent == null) return;
            var customer = Session.CurrentCustomer;
            _headerSubtitle.Text = $"Welcome, {customer?.Name ?? "Customer"} — browse medicines, manage your cart, and track orders.";
            lblCart.Text = CartService.ItemCount.ToString();
            var orders = new OrderService().GetByCustomer(customer?.CustomerID ?? 0);
            lblOrders.Text = orders.Count(o => o.Status != "Delivered").ToString();
            var medicineService = new MedicineService();
            var promos = medicineService.GetAll().Count(m => medicineService.IsPromotionActive(m));
            lblPromotions.Text = promos.ToString();
            ClinicalUi.BindGrid(gridRecent, orders.Take(5).Select(o => new
            {
                OrderRef = $"#SM-{o.OrderID:D4}",
                o.OrderDate,
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}"
            }).ToList());
        }

        private void BuildContent()
        {
            ClinicalUi.PreparePagePanel(PagePanel);
            PagePanel.Controls.Clear();

            var root = new TableLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                Width = GetScrollContentWidth(),
                BackColor = UiTheme.AdminSurface
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));

            var header = ClinicalUi.CreatePageHeaderBlock("Customer Home",
                "Browse medicines, manage your cart, and track orders.");
            _headerSubtitle = header.SubtitleLabel;
            root.Controls.Add(header.Panel);

            var stats = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                Height = 90,
                Margin = UiTheme.CustomerSectionMargin,
                BackColor = UiTheme.AdminSurface
            };
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));
            lblCart = new Label();
            lblOrders = new Label();
            lblPromotions = new Label();
            stats.Controls.Add(ClinicalUi.CreateStatCard("Items in Cart", lblCart, UiTheme.AdminTeal), 0, 0);
            stats.Controls.Add(ClinicalUi.CreateStatCard("Active Orders", lblOrders, Color.FromArgb(59, 130, 246)), 1, 0);
            stats.Controls.Add(ClinicalUi.CreateStatCard("Promotions", lblPromotions, Color.FromArgb(16, 185, 129)), 2, 0);
            root.Controls.Add(stats);

            root.Controls.Add(ClinicalUi.CreateSectionHeading("Recent Orders"));
            gridRecent = ClinicalUi.CreateGrid();
            gridRecent.Dock = DockStyle.Top;
            gridRecent.Height = 220;
            gridRecent.Margin = UiTheme.CustomerSectionMargin;
            root.Controls.Add(gridRecent);

            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, 0),
                BackColor = UiTheme.AdminSurface
            };
            actions.Controls.Add(CreateNavButton("Browse Medicines", () => GoToCustomerSection(CustomerNavItem.Browse)));
            actions.Controls.Add(CreateNavButton("View Cart", () => GoToCustomerSection(CustomerNavItem.Cart), primary: true));
            actions.Controls.Add(CreateNavButton("Change Password", () =>
            {
                using (var dlg = new ChangePasswordForm(isAdmin: false))
                    dlg.ShowDialog(this);
            }));
            root.Controls.Add(actions);

            WireScrollRoot(root, minHeight: 520);
        }

        private static Button CreateNavButton(string text, Action onClick, bool primary = false)
        {
            var btn = ClinicalUi.CreateButton(text, primary, width: 160);
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private void LoadDesignTimePreview()
        {
            _headerSubtitle.Text = "Welcome, Jane Doe — browse medicines, manage your cart, and track orders.";
            lblCart.Text = "2";
            lblOrders.Text = "1";
            lblPromotions.Text = "3";
            ClinicalUi.BindGrid(gridRecent, new[]
            {
                new { OrderRef = "#SM-0002", OrderDate = DateTime.Today, Status = "Pending", Total = "LKR 15.00" }
            });
        }
    }
}
