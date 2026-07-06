using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class CustomerDashboardForm : EmbeddedPageForm
    {
        private OrderService _orders;
        private MedicineService _medicines;
        private bool _servicesReady;
        private bool _runtimeWired;
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
            if (_servicesReady)
                WireRuntimeBehavior();
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

            lblPageSubtitle.Text =
                "Welcome, Jane Perera — browse medicines, manage your cart, and track orders.";
            lblCart.Text = "2";
            lblOrders.Text = "1";
            lblPromotions.Text = "3";
            UiTheme.SetGridDataSource(gridRecent, DesignTimePreviewData.CustomerDashboardOrders());
            UiTheme.BeautifyGridHeaders(gridRecent);
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridRecent);

            WirePanelBorder(panelGridOuter);
            WireStatCard(panelStatCart, UiTheme.AdminTeal);
            WireStatCard(panelStatOrders, Color.FromArgb(59, 130, 246));
            WireStatCard(panelStatPromotions, Color.FromArgb(16, 185, 129));
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

        private static void WireStatCard(Panel card, Color accent)
        {
            if (card == null || card.Tag as string == "dash-stat") return;
            card.Tag = "dash-stat";
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
                using (var brush = new SolidBrush(accent))
                    e.Graphics.FillRectangle(brush, 0, 0, 4, rect.Height);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnBrowseMedicines.Click += (s, e) => Navigate(CustomerHostForm.CustomerNavItem.Browse);
            btnViewCart.Click += (s, e) => Navigate(CustomerHostForm.CustomerNavItem.Cart);
            btnChangePassword.Click += (s, e) => ShowChangePassword();
        }

        private void Navigate(CustomerHostForm.CustomerNavItem item) =>
            (FindForm() as CustomerHostForm)?.NavigateTo(item);

        private void ShowChangePassword()
        {
            using (var dlg = new ChangePasswordForm(isAdmin: false))
                dlg.ShowDialog(FindForm());
        }
    }
}
