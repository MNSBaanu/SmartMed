using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public sealed partial class CustomerDashboardForm : CustomerPageControl
    {
        private readonly OrderService _orders = new OrderService();
        private readonly MedicineService _medicines = new MedicineService();
        private Label _headerSubtitle;

        public CustomerDashboardForm()
        {
            InitializeComponent();
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage()
        {
            SyncScrollRootWidth();
            var customer = Session.CurrentCustomer;
            if (_headerSubtitle != null)
                _headerSubtitle.Text = $"Welcome, {customer?.Name ?? "Customer"} — browse medicines, manage your cart, and track orders.";
            if (lblCart != null)
                lblCart.Text = CartService.ItemCount.ToString();
            if (lblOrders != null)
            {
                var orders = _orders.GetByCustomer(customer?.CustomerID ?? 0);
                lblOrders.Text = orders.Count(o => o.Status != "Delivered").ToString();
            }
            if (lblPromotions != null)
                lblPromotions.Text = _medicines.GetAll().Count(m => _medicines.IsPromotionActive(m)).ToString();
            if (gridRecent != null)
            {
                var recent = _orders.GetByCustomer(customer?.CustomerID ?? 0)
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
        }

        private void BuildContent()
        {
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                MinimumSize = new Size(0, 520)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var header = AdminUiHelpers.CreatePageHeader("Customer Home",
                "Browse medicines, manage your cart, and track orders.");
            foreach (Control c in header.Controls)
            {
                if (c is Label lbl && lbl.ForeColor == UiTheme.AdminMuted)
                {
                    _headerSubtitle = lbl;
                    break;
                }
            }
            root.Controls.Add(header, 0, 0);

            var stats = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 3,
                Height = 90,
                Margin = new Padding(0, 0, 0, 16),
                BackColor = UiTheme.AdminSurface
            };
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            stats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));
            lblCart = new Label();
            lblOrders = new Label();
            lblPromotions = new Label();
            stats.Controls.Add(AdminUiHelpers.CreateStatCard("Items in Cart", lblCart, UiTheme.AdminTeal), 0, 0);
            stats.Controls.Add(AdminUiHelpers.CreateStatCard("Active Orders", lblOrders, Color.FromArgb(59, 130, 246)), 1, 0);
            stats.Controls.Add(AdminUiHelpers.CreateStatCard("Promotions", lblPromotions, Color.FromArgb(16, 185, 129)), 2, 0);
            root.Controls.Add(stats, 0, 1);

            root.Controls.Add(new Label
            {
                Text = "Recent Orders",
                Font = UiTheme.FontAt(11f, semibold: true),
                ForeColor = UiTheme.PrimaryDark,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 8),
                BackColor = UiTheme.AdminSurface
            }, 0, 2);

            gridRecent = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 220,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 0, 0, 16)
            };
            UiTheme.ApplyClinicalGrid(gridRecent);
            root.Controls.Add(gridRecent, 0, 3);

            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                Margin = new Padding(0, 8, 0, 0),
                BackColor = UiTheme.AdminSurface
            };
            actions.Controls.Add(CreateNavButton("Browse Medicines", () => Navigate(CustomerNavItem.Browse)));
            actions.Controls.Add(CreateNavButton("View Cart", () => Navigate(CustomerNavItem.Cart), primary: true));
            actions.Controls.Add(CreateNavButton("Change Password", ShowChangePassword));
            root.Controls.Add(actions, 0, 4);

            WireScrollRoot(root);
        }

        private static Button CreateNavButton(string text, Action onClick, bool primary = false)
        {
            var btn = AdminUiHelpers.CreateWinButton(text, primary, 160);
            btn.Click += (s, e) => onClick();
            return btn;
        }

        private void Navigate(CustomerNavItem item) =>
            (FindForm() as CustomerHostForm)?.NavigateTo(item);

        private void ShowChangePassword()
        {
            using (var dlg = new ChangePasswordForm(isAdmin: false))
                dlg.ShowDialog(FindForm());
        }
    }
}
