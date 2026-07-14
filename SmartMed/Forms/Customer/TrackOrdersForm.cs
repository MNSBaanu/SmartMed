using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class TrackOrdersForm : EmbeddedPageForm
    {
        private readonly OrderService _orders;
        private readonly MedicineService _medicines;
        private readonly bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;
        private int? _selectedOrderId;

        public TrackOrdersForm()
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

        protected override void DoRefreshPage() => RefreshOrders();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            UiTheme.SetGridDataSource(gridOrders, new object[0]);
            UiTheme.SetGridDataSource(gridItems, new object[0]);
            _selectedOrderId = null;
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridOrders);
            UiTheme.ApplyClinicalGrid(gridItems);
        }

        private void PanelOrdersOuter_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawOuterPanelBorder(panelOrdersOuter, e);

        private void PanelItemsOuter_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawOuterPanelBorder(panelItemsOuter, e);

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;
        }

        private void RefreshOrders()
        {
            if (!_servicesReady) return;

            var customerId = Session.CurrentCustomer?.CustomerID ?? 0;
            var orders = _orders.GetByCustomer(customerId);
            UiTheme.SetGridDataSource(gridOrders, orders.Select(o => new
            {
                o.OrderID,
                OrderRef = $"#SM-{o.OrderID:D4}",
                OrderDate = o.OrderDate.ToString("MMM dd, yyyy hh:mm tt"),
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}",
                Payment = FormatPayment(o),
                Prescription = _orders.GetPrescriptionDisplay(o.OrderID),
                CancelReason = string.IsNullOrWhiteSpace(o.CancellationReason) ? "—" : o.CancellationReason
            }).ToList());
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
            if (gridOrders.Columns.Contains("CancelReason"))
                gridOrders.Columns["CancelReason"].HeaderText = "Cancel Reason";
            UiTheme.BeautifyGridHeaders(gridOrders);
            gridItems.DataSource = null;
            _selectedOrderId = null;
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (!_servicesReady || gridOrders?.CurrentRow == null) return;

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
            if (!_servicesReady || e.RowIndex < 0 || !gridOrders.Columns.Contains("Prescription")) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Prescription") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            if (!_orders.OrderHasPrescription(orderId)) return;

            PrescriptionFilesDialog.Open(FindForm(), _orders.GetPrescriptionFilePaths(orderId));
        }

        private void BtnCancelPending_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            if (!_selectedOrderId.HasValue)
            {
                SmartMedMessageBox.Show("Select a pending order to cancel.", "Cancel Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CancelReasonDialog.TryGetReason(FindForm(), out var reason))
                return;

            try
            {
                _orders.CancelOrder(_selectedOrderId.Value, Session.CurrentCustomer.CustomerID, reason);
                RefreshOrders();
                SmartMedMessageBox.Show("Order cancelled.", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnExportCsv_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

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
                    SmartMedMessageBox.Show("Order history exported to CSV.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    SmartMedMessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

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
                    SmartMedMessageBox.Show("Order history exported to PDF.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    SmartMedMessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static string FormatPayment(Order order)
        {
            if (order == null) return "—";
            var summary = $"{order.PaymentMethod} ({order.PaymentStatus})";
            return string.IsNullOrWhiteSpace(order.PaymentReference)
                ? summary
                : $"{summary} — {order.PaymentReference}";
        }
    }
}
