using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class OrderService
    {
        public const string StatusPending = "Pending";
        public const string StatusReadyForPickup = "Ready for Pickup";
        public const string StatusDelivered = "Delivered";
        public const string StatusCancelled = "Cancelled";

        private static readonly string[] ValidStatuses =
        {
            StatusPending, StatusReadyForPickup, StatusDelivered
        };

        public static IReadOnlyList<string> GetAllowedNextStatuses(string currentStatus)
        {
            // Show only the next statuses the pharmacy is allowed to choose.
            if (string.IsNullOrWhiteSpace(currentStatus))
                return Array.Empty<string>();

            switch (currentStatus)
            {
                case StatusPending:
                    return new[] { StatusReadyForPickup, StatusDelivered };
                case StatusReadyForPickup:
                    return new[] { StatusDelivered };
                default:
                    return Array.Empty<string>();
            }
        }

        private readonly OrderRepository _orders = new OrderRepository();
        private readonly MedicineRepository _medicines = new MedicineRepository();
        private readonly MedicineService _medicineService = new MedicineService();
        private readonly PrescriptionService _prescriptions = new PrescriptionService();

        public List<Order> GetAll() => _orders.GetAll();

        public List<Order> GetByCustomer(int customerId) => _orders.GetByCustomer(customerId);

        public Order GetById(int orderId) => _orders.GetById(orderId);

        public List<OrderItem> GetItems(int orderId) => _orders.GetItems(orderId);

        public void UpdateStatus(int orderId, string status)
        {
            if (orderId <= 0)
                throw new ArgumentException("Select an order to update.");
            if (ValidationService.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status is required.");
            if (!ValidStatuses.Contains(status))
                throw new ArgumentException("Invalid order status.");

            var order = _orders.GetById(orderId)
                ?? throw new InvalidOperationException("Order not found.");

            var transitionError = GetTransitionError(order.Status, status);
            if (transitionError != null)
                throw new InvalidOperationException(transitionError);

            // Prescription must be verified before the order can move forward.
            if (status == StatusReadyForPickup || status == StatusDelivered)
            {
                if (_prescriptions.HasPrescription(orderId))
                {
                    var rxStatus = _prescriptions.GetStatus(orderId);
                    if (string.Equals(rxStatus, PrescriptionService.StatusPending, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("Verify the prescription before updating this order.");
                    if (string.Equals(rxStatus, PrescriptionService.StatusRejected, StringComparison.OrdinalIgnoreCase))
                        throw new InvalidOperationException("This prescription was rejected. The order cannot proceed until a valid prescription is provided.");
                }
            }

            _orders.UpdateStatus(orderId, status);
        }

        // Keep order progress one-way: Pending → Ready for Pickup → Delivered.
        private static string GetTransitionError(string currentStatus, string newStatus)
        {
            if (currentStatus == newStatus)
                return null;

            if (currentStatus == StatusDelivered)
                return "Delivered orders cannot be changed.";

            if (newStatus == StatusReadyForPickup && currentStatus != StatusPending)
                return "Order must be Pending before it can be marked Ready for Pickup.";

            if (newStatus == StatusDelivered
                && currentStatus != StatusReadyForPickup
                && currentStatus != StatusPending)
                return "Order must be Pending or Ready for Pickup before it can be marked Delivered.";

            if (newStatus == StatusPending)
                return "Order status cannot be changed back to Pending.";

            return "Invalid order status transition.";
        }

        public int PlaceOrder(int customerId, IReadOnlyList<CartLine> cart,
            string paymentMethod, string paymentStatus, string paymentReference)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");
            if (cart == null || cart.Count == 0)
                throw new InvalidOperationException("Your cart is empty.");

            var orderItems = new List<OrderItem>();
            var rxPaths = new List<KeyValuePair<int, string>>();

            foreach (var line in cart)
            {
                var medicine = _medicineService.GetById(line.MedicineID)
                    ?? throw new InvalidOperationException($"Medicine not found: {line.MedicineName}");

                // Ensure the medicine is available before allowing the purchase.
                _medicineService.ValidateForCustomerPurchase(medicine);
                if (medicine.StockQuantity < line.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {medicine.MedicineName}.");

                // Check whether a prescription is required before confirming the order.
                if (medicine.RequiresPrescription)
                {
                    if (ValidationService.IsNullOrWhiteSpace(line.PrescriptionPath))
                        throw new InvalidOperationException(
                            $"Upload a prescription for {medicine.MedicineName} before placing the order.");
                    rxPaths.Add(new KeyValuePair<int, string>(line.MedicineID, line.PrescriptionPath));
                }

                // Calculate the final amount the customer needs to pay for this item.
                var unitPrice = _medicineService.GetEffectivePrice(medicine);
                orderItems.Add(new OrderItem
                {
                    MedicineID = line.MedicineID,
                    Quantity = line.Quantity,
                    UnitPrice = unitPrice,
                    Subtotal = unitPrice * line.Quantity
                });
            }

            if (ValidationService.IsNullOrWhiteSpace(paymentMethod))
                throw new ArgumentException("Payment method is required.");
            if (ValidationService.IsNullOrWhiteSpace(paymentStatus))
                throw new ArgumentException("Payment status is required.");

            List<PrescriptionRepository.PrescriptionAttachment> attachments = null;
            if (rxPaths.Count > 0)
                attachments = _prescriptions.PrepareAttachments(customerId, rxPaths);

            try
            {
                // Record the order so the pharmacy can process it.
                var orderId = _orders.CreateOrder(
                    customerId,
                    orderItems,
                    paymentMethod,
                    paymentStatus,
                    paymentReference,
                    attachments,
                    attachments != null && attachments.Count > 0 ? (int?)customerId : null);

                AdminOrderAlerts.NotifyOrderPlaced(orderId);
                return orderId;
            }
            catch
            {
                // Remove copied prescription files if the order could not be saved.
                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        if (attachment == null || string.IsNullOrWhiteSpace(attachment.FilePath))
                            continue;
                        try { File.Delete(attachment.FilePath); } catch { }
                    }
                }
                throw;
            }
        }

        public int PlaceOrder(int customerId, IReadOnlyList<CartLine> cart, string prescriptionSourcePath,
            string paymentMethod, string paymentStatus, string paymentReference)
        {
            return PlaceOrder(customerId, cart, paymentMethod, paymentStatus, paymentReference);
        }

        public void CancelOrder(int orderId, int customerId, string reason)
        {
            CancelPendingOrder(orderId, customerId, reason);
        }

        public void CancelOrderAsAdmin(int orderId, string reason)
        {
            CancelPendingOrder(orderId, null, reason);
        }

        private void CancelPendingOrder(int orderId, int? customerId, string reason)
        {
            if (ValidationService.IsNullOrWhiteSpace(reason))
                throw new ArgumentException("A cancellation reason is required.");
            reason = reason.Trim();
            if (reason.Length > 500)
                throw new ArgumentException("Cancellation reason must be 500 characters or fewer.");

            var order = _orders.GetById(orderId)
                ?? throw new InvalidOperationException("Order not found.");
            if (customerId.HasValue && order.CustomerID != customerId.Value)
                throw new InvalidOperationException("You can only cancel your own orders.");
            if (order.Status != StatusPending)
                throw new InvalidOperationException("Only pending orders can be cancelled.");

            // Return reserved stock when a pending order is cancelled.
            var items = _orders.GetItems(orderId);
            foreach (var item in items)
                _medicines.RestoreStock(item.MedicineID, item.Quantity);

            _orders.MarkCancelled(orderId, reason);
        }

        public DataTable BuildCustomerOrderExportTable(int customerId)
        {
            var table = new DataTable();
            table.Columns.Add("Order Ref");
            table.Columns.Add("Order Date");
            table.Columns.Add("Status");
            table.Columns.Add("Total (LKR)");
            table.Columns.Add("Prescription");
            table.Columns.Add("Cancellation Reason");

            foreach (var order in GetByCustomer(customerId))
            {
                table.Rows.Add(
                    $"#SM-{order.OrderID:D4}",
                    order.OrderDate.ToString("MMM dd, yyyy hh:mm tt"),
                    order.Status,
                    order.TotalAmount.ToString("N2"),
                    GetPrescriptionDisplay(order.OrderID),
                    string.IsNullOrWhiteSpace(order.CancellationReason) ? "—" : order.CancellationReason);
            }

            return table;
        }

        public void ExportCustomerOrderHistoryToCsv(int customerId, string filePath)
        {
            ExportHelper.ExportDataTableToCsv(BuildCustomerOrderExportTable(customerId), filePath);
        }

        public void ExportCustomerOrderHistoryToPdf(int customerId, string filePath, string customerName)
        {
            var subtitle = $"Customer: {customerName}  |  Generated: {DateTime.Now:MMM dd, yyyy HH:mm}";
            ExportHelper.ExportDataTableToPdf(
                BuildCustomerOrderExportTable(customerId),
                filePath,
                "My Order History",
                subtitle);
        }

        public void ExportOrdersToCsv(IEnumerable<Order> orders, string filePath)
        {
            var sb = new StringBuilder();
            sb.AppendLine("OrderID,OrderDate,Status,TotalAmount");
            foreach (var order in orders)
            {
                sb.Append(order.OrderID).Append(',');
                sb.Append(order.OrderDate.ToString("yyyy-MM-dd HH:mm")).Append(',');
                sb.Append(EscapeCsv(order.Status)).Append(',');
                sb.AppendLine(order.TotalAmount.ToString("F2"));
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public void GetStatusStats(IReadOnlyList<Order> all, out int pending, out int delivered)
        {
            pending = all.Count(o => o.Status == StatusPending);
            delivered = all.Count(o => o.Status == StatusDelivered);
        }

        public string GetPrescriptionDisplay(int orderId) => _prescriptions.GetDisplayName(orderId);

        public string GetPrescriptionFilePath(int orderId) => _prescriptions.GetFilePath(orderId);

        public IReadOnlyList<string> GetPrescriptionFilePaths(int orderId) =>
            _prescriptions.GetFilePaths(orderId);

        public List<Prescription> GetPrescriptions(int orderId) =>
            _prescriptions.GetAllByOrderId(orderId);

        public string GetPrescriptionStatusDisplay(int orderId) => _prescriptions.GetStatusDisplay(orderId);

        public bool OrderHasPrescription(int orderId) => _prescriptions.HasPrescription(orderId);

        public void VerifyPrescription(int orderId) => _prescriptions.Verify(orderId);

        public void RejectPrescription(int orderId) => _prescriptions.Reject(orderId);

        public void VerifyPrescriptionById(int orderId, int prescriptionId) =>
            _prescriptions.VerifyById(orderId, prescriptionId);

        public void RejectPrescriptionById(int orderId, int prescriptionId) =>
            _prescriptions.RejectById(orderId, prescriptionId);

        public List<RecentOrderSummary> GetRecentSummaries(int take)
        {
            return GetAll()
                .Take(take)
                .Select(o =>
                {
                    var items = GetItems(o.OrderID);
                    var med = items.Count > 0 ? items[0].MedicineName : "-";
                    if (items.Count > 1) med += $" (+{items.Count - 1})";
                    return new RecentOrderSummary
                    {
                        OrderId = $"#SM-{o.OrderID:D4}",
                        Patient = o.CustomerName,
                        Medication = med,
                        Status = o.Status,
                        Time = o.OrderDate.ToString("hh:mm tt")
                    };
                })
                .ToList();
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }

    public class RecentOrderSummary
    {
        public string OrderId { get; set; }
        public string Patient { get; set; }
        public string Medication { get; set; }
        public string Status { get; set; }
        public string Time { get; set; }
    }
}
