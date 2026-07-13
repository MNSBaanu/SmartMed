using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class CustomerDashboardForm : EmbeddedPageForm
    {
        private readonly OrderService _orders;
        private readonly MedicineService _medicines;
        private readonly bool _servicesReady;
        private bool _chromeApplied;

        public CustomerDashboardForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _orders = new OrderService();
                _medicines = new MedicineService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
        }

        protected override void DoRefreshPage()
        {
            if (!_servicesReady) return;

            var customer = Session.CurrentCustomer;
            lblPageSubtitle.Text =
                $"Welcome, {customer?.Name ?? "Customer"} — browse medicines, manage your cart, and track orders.";
            lblCart.Text = CartService.ItemCount.ToString();

            var orders = _orders.GetByCustomer(customer?.CustomerID ?? 0);
            lblOrders.Text = orders.Count(o => o.Status != OrderService.StatusDelivered).ToString();
            lblPromotions.Text = _medicines.GetAll().Count(m => _medicines.IsPromotionActive(m)).ToString();

            var recent = orders
                .Take(5)
                .Select(o => new
                {
                    OrderRef = $"#SM-{o.OrderID:D4}",
                    o.OrderDate,
                    o.Status,
                    Total = $"LKR {o.TotalAmount:N2}"
                })
                .ToList();
            UiTheme.SetGridDataSource(gridRecent, recent);
            UiTheme.BeautifyGridHeaders(gridRecent);
        }

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            lblPageSubtitle.Text = "Welcome - browse medicines, manage your cart, and track orders.";
            lblCart.Text = "-";
            lblOrders.Text = "-";
            lblPromotions.Text = "-";
            UiTheme.SetGridDataSource(gridRecent, new object[0]);
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridRecent);
        }

        private void PanelGridOuter_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawOuterPanelBorder(panelGridOuter, e);

        private void PanelStatCart_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawStatCardAccent(panelStatCart, e, UiTheme.AdminTeal);

        private void PanelStatOrders_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawStatCardAccent(panelStatOrders, e, Color.FromArgb(59, 130, 246));

        private void PanelStatPromotions_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawStatCardAccent(panelStatPromotions, e, Color.FromArgb(16, 185, 129));

        private void lblOrders_Click(object sender, EventArgs e)
        {

        }

        private void lblStatOrdersTitle_Click(object sender, EventArgs e)
        {

        }

        private void lblPageSubtitle_Click(object sender, EventArgs e)
        {

        }
    }
}
