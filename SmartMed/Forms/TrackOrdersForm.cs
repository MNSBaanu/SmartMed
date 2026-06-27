using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class TrackOrdersForm : CustomerShellForm
    {
        private bool _pageBuilt;
        private int _layoutVersion;
        private const int LayoutVersion = 3;
        private OrderService _orders;
        private DataGridView gridOrders;
        private DataGridView gridItems;
        private int? _selectedOrderId;

        private MedicineService _medicines;

        public TrackOrdersForm()
            : base(CustomerNavItem.Orders, "My Orders")
        {
            InitializeComponent();
            CompleteDesignInitialization();
        }

        internal TrackOrdersForm(bool embedded)
            : base(CustomerNavItem.Orders, "My Orders", embedded)
        {
        }

        private OrderService Orders => GetRuntimeService(ref _orders);
        private MedicineService Medicines => GetRuntimeService(ref _medicines);

        protected override void InitializePageContent()
        {
            if (_pageBuilt && _layoutVersion == LayoutVersion) return;
            _pageBuilt = true;
            _layoutVersion = LayoutVersion;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                RefreshOrders();
        }

        public void RefreshOrders()
        {
            if (IsDesignHost() || Orders == null || gridOrders == null) return;
            var customerId = Session.CurrentCustomer?.CustomerID ?? 0;
            var orders = Orders.GetByCustomer(customerId);
            ClinicalUi.BindGrid(gridOrders, orders.Select(o => new
            {
                o.OrderID,
                OrderRef = $"#SM-{o.OrderID:D4}",
                OrderDate = o.OrderDate.ToString("MMM dd, yyyy hh:mm tt"),
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}",
                Prescription = Orders.GetPrescriptionDisplay(o.OrderID)
            }).ToList());
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
            gridItems.DataSource = null;
            UiTheme.BeautifyGridHeaders(gridItems);
            _selectedOrderId = null;
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

            root.Controls.Add(ClinicalUi.CreatePageHeader("My Orders",
                "View order history, cancel pending orders, and export receipts."), 0, 0);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            var actions = CreateActionsPanel();
            root.Controls.Add(actions, 0, 1);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            gridOrders = ClinicalUi.CreateGrid();
            gridOrders.Dock = DockStyle.Fill;
            gridOrders.Height = 220;
            gridOrders.Margin = new Padding(0, UiTheme.CustomerControlGap, 0, UiTheme.CustomerSectionGap);
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;
            gridOrders.CellDoubleClick += GridOrders_CellDoubleClick;
            root.Controls.Add(gridOrders, 0, 2);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));

            var lblItems = ClinicalUi.CreateSectionHeading("Order Items");
            lblItems.Margin = new Padding(0, UiTheme.CustomerSectionGap, 0, UiTheme.CustomerControlGap);
            root.Controls.Add(lblItems, 0, 3);
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            gridItems = ClinicalUi.CreateGrid();
            gridItems.Dock = DockStyle.Fill;
            gridItems.Height = 180;
            gridItems.Margin = new Padding(0, UiTheme.CustomerControlGap, 0, 0);
            root.Controls.Add(gridItems, 0, 4);
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 180f));

            WireScrollRoot(root, minHeight: 520);
        }

        private FlowLayoutPanel CreateActionsPanel()
        {
            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                Dock = DockStyle.Fill,
                WrapContents = true,
                Margin = new Padding(0, UiTheme.CustomerControlGap, 0, UiTheme.CustomerSectionGap),
                BackColor = UiTheme.AdminSurface
            };
            var btnCancel = ClinicalUi.CreateButton("Cancel Pending Order", width: 160, height: 32);
            btnCancel.Click += BtnCancel_Click;
            var btnExportCsv = ClinicalUi.CreateButton("Export CSV", width: 110, height: 32);
            btnExportCsv.Click += BtnExportCsv_Click;
            var btnExportPdf = ClinicalUi.CreateButton("Export PDF", width: 110, height: 32);
            btnExportPdf.Click += BtnExportPdf_Click;
            actions.Controls.Add(btnCancel);
            actions.Controls.Add(btnExportCsv);
            actions.Controls.Add(btnExportPdf);
            return actions;
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (IsDesignHost() || Orders == null || gridOrders.CurrentRow == null) return;
            _selectedOrderId = Convert.ToInt32(gridOrders.CurrentRow.Cells["OrderID"].Value);
            var items = Orders.GetItems(_selectedOrderId.Value);
            ClinicalUi.BindGrid(gridItems, items.Select(i => new
            {
                i.MedicineName,
                i.Quantity,
                UnitPrice = $"LKR {i.UnitPrice:N2}",
                Discount = Medicines?.GetOrderLineOfferDisplay(i.UnitPrice, i.ListPrice, i.DiscountPercent) ?? "—",
                Subtotal = $"LKR {i.Subtotal:N2}"
            }).ToList());
        }

        private void GridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (IsDesignHost() || Orders == null || e.RowIndex < 0) return;
            if (!gridOrders.Columns.Contains("Prescription")) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Prescription") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            var filePath = Orders.GetPrescriptionFilePath(orderId);
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("Prescription file is no longer available on this device.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Could not open prescription file.\n{ex.Message}", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue)
            {
                MessageBox.Show("Select a pending order to cancel.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Cancel this order and restore stock?", "Confirm Cancel",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                Orders.CancelOrder(_selectedOrderId.Value, Session.CurrentCustomer.CustomerID);
                RefreshOrders();
                MessageBox.Show("Order cancelled.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Orders == null) return;
            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            using (var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "my_orders.csv"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    Orders.ExportCustomerOrderHistoryToCsv(customer.CustomerID, dialog.FileName);
                    MessageBox.Show("Order history exported to CSV.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Orders == null) return;
            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            using (var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "my_orders.pdf"
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    Orders.ExportCustomerOrderHistoryToPdf(customer.CustomerID, dialog.FileName, customer.Name);
                    MessageBox.Show("Order history exported to PDF.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void LoadDesignTimePreview()
        {
            ClinicalUi.BindGrid(gridOrders, new[]
            {
                new { OrderID = 2, OrderRef = "#SM-0002", OrderDate = "Jun 22, 2026 10:00 AM", Status = "Pending", Total = "LKR 15.00", Prescription = "2_20260622100000_rx.pdf" }
            });
            ClinicalUi.BindGrid(gridItems, new[]
            {
                new { MedicineName = "Vitamin C", Quantity = 1, UnitPrice = "LKR 15.00", Discount = "—", Subtotal = "LKR 15.00" }
            });
        }
    }
}
