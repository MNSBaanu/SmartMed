using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public sealed partial class TrackOrdersForm : CustomerPageControl
    {
        private readonly OrderService _orders = new OrderService();
        private readonly MedicineService _medicines = new MedicineService();
        private int? _selectedOrderId;

        public TrackOrdersForm()
        {
            InitializeComponent();
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage()
        {
            SyncScrollRootWidth();
            RefreshOrders();
        }

        private void BuildContent()
        {
            gridOrders = new DataGridView
            {
                Dock = DockStyle.Fill,
                Height = 220,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                Margin = new Padding(0, 0, 0, 12)
            };
            UiTheme.ApplyClinicalGrid(gridOrders);
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;
            gridOrders.CellDoubleClick += GridOrders_CellDoubleClick;

            gridItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                Height = 180,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiTheme.ApplyClinicalGrid(gridItems);

            var root = new TableLayoutPanel
            {
                AutoSize = true,
                ColumnCount = 1,
                MinimumSize = new Size(0, 520),
                BackColor = UiTheme.AdminSurface
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 180f));

            root.Controls.Add(AdminUiHelpers.CreatePageHeader("My Orders",
                "View order history, cancel pending orders, and export receipts."), 0, 0);
            root.Controls.Add(CreateActionsPanel(), 0, 1);
            root.Controls.Add(gridOrders, 0, 2);
            root.Controls.Add(new Label
            {
                Text = "Order Items",
                Font = UiTheme.FontAt(11f, semibold: true),
                ForeColor = UiTheme.PrimaryDark,
                AutoSize = true,
                Margin = new Padding(0, 12, 0, 8),
                BackColor = UiTheme.AdminSurface
            }, 0, 3);
            root.Controls.Add(gridItems, 0, 4);

            WireScrollRoot(root);
        }

        private FlowLayoutPanel CreateActionsPanel()
        {
            var actions = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = true,
                Margin = new Padding(0, 8, 0, 8),
                BackColor = UiTheme.AdminSurface
            };

            var btnCancel = AdminUiHelpers.CreateWinButton("Cancel Pending Order", false, 160);
            btnCancel.Click += BtnCancel_Click;
            var btnExportCsv = AdminUiHelpers.CreateWinButton("Export CSV", false, 110);
            btnExportCsv.Click += BtnExportCsv_Click;
            var btnExportPdf = AdminUiHelpers.CreateWinButton("Export PDF", false, 110);
            btnExportPdf.Click += BtnExportPdf_Click;

            actions.Controls.Add(btnCancel);
            actions.Controls.Add(btnExportCsv);
            actions.Controls.Add(btnExportPdf);
            return actions;
        }

        private void RefreshOrders()
        {
            var customerId = Session.CurrentCustomer?.CustomerID ?? 0;
            var orders = _orders.GetByCustomer(customerId);
            UiTheme.SetGridDataSource(gridOrders, orders.Select(o => new
            {
                o.OrderID,
                OrderRef = $"#SM-{o.OrderID:D4}",
                OrderDate = o.OrderDate.ToString("MMM dd, yyyy hh:mm tt"),
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}",
                Prescription = _orders.GetPrescriptionDisplay(o.OrderID)
            }).ToList());
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
            UiTheme.BeautifyGridHeaders(gridOrders);
            gridItems.DataSource = null;
            _selectedOrderId = null;
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (gridOrders?.CurrentRow == null) return;
            _selectedOrderId = Convert.ToInt32(gridOrders.CurrentRow.Cells["OrderID"].Value);
            var items = _orders.GetItems(_selectedOrderId.Value);
            UiTheme.SetGridDataSource(gridItems, items.Select(i => new
            {
                i.MedicineName,
                i.Quantity,
                UnitPrice = $"LKR {i.UnitPrice:N2}",
                Discount = _medicines.GetOrderLineOfferDisplay(i.UnitPrice, i.ListPrice, i.DiscountPercent),
                Subtotal = $"LKR {i.Subtotal:N2}"
            }).ToList());
            UiTheme.BeautifyGridHeaders(gridItems);
        }

        private void GridOrders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !gridOrders.Columns.Contains("Prescription")) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Prescription") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            var filePath = _orders.GetPrescriptionFilePath(orderId);
            if (string.IsNullOrWhiteSpace(filePath)) return;

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
                MessageBox.Show("Select a pending order to cancel.", "Cancel Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Cancel this order and restore stock?", "Confirm Cancel",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _orders.CancelOrder(_selectedOrderId.Value, Session.CurrentCustomer.CustomerID);
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
            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            using (var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = "my_orders.csv"
            })
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                try
                {
                    _orders.ExportCustomerOrderHistoryToCsv(customer.CustomerID, dialog.FileName);
                    MessageBox.Show("Order history exported to CSV.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            using (var dialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = "my_orders.pdf"
            })
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                try
                {
                    _orders.ExportCustomerOrderHistoryToPdf(customer.CustomerID, dialog.FileName, customer.Name);
                    MessageBox.Show("Order history exported to PDF.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
