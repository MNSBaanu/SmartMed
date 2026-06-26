using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class OrderService
    {
        public const string StatusPending = "Pending";
        public const string StatusReadyForPickup = "Ready for Pickup";
        public const string StatusDelivered = "Delivered";

        private static readonly string[] ValidStatuses =
        {
            StatusPending, StatusReadyForPickup, StatusDelivered
        };

        public static IReadOnlyList<string> GetAllowedNextStatuses(string currentStatus)
        {
            if (string.IsNullOrWhiteSpace(currentStatus))
                return Array.Empty<string>();

            switch (currentStatus)
            {
                case StatusPending:
                    return new[] { StatusReadyForPickup };
                case StatusReadyForPickup:
                    return new[] { StatusDelivered };
                default:
                    return Array.Empty<string>();
            }
        }

        private readonly OrderRepository _orders = new OrderRepository();
        private readonly MedicineRepository _medicines = new MedicineRepository();
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

            var order = _orders.GetById(orderId);
            if (order == null)
                throw new InvalidOperationException("Order not found.");

            var transitionError = GetTransitionError(order.Status, status);
            if (transitionError != null)
                throw new InvalidOperationException(transitionError);

            _orders.UpdateStatus(orderId, status);
        }

        private static string GetTransitionError(string currentStatus, string newStatus)
        {
            if (currentStatus == newStatus)
                return null;

            if (currentStatus == StatusDelivered)
                return "Delivered orders cannot be changed.";

            if (newStatus == StatusReadyForPickup && currentStatus != StatusPending)
                return "Order must be Pending before it can be marked Ready for Pickup.";

            if (newStatus == StatusDelivered && currentStatus != StatusReadyForPickup)
                return "Order must be Ready for Pickup before it can be marked Delivered.";

            if (newStatus == StatusPending)
                return "Order status cannot be changed back to Pending.";

            return "Invalid order status transition.";
        }

        public int PlaceOrder(int customerId, IReadOnlyList<CartLine> cart, string prescriptionSourcePath)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");
            if (cart == null || cart.Count == 0)
                throw new InvalidOperationException("Your cart is empty.");

            var orderItems = new List<OrderItem>();
            var requiresRx = false;

            foreach (var line in cart)
            {
                var medicine = _medicines.GetById(line.MedicineID);
                if (medicine == null)
                    throw new InvalidOperationException($"Medicine not found: {line.MedicineName}");
                if (medicine.ExpiryDate.Date < DateTime.Today)
                    throw new InvalidOperationException($"{medicine.MedicineName} has expired and cannot be ordered.");
                if (medicine.StockQuantity < line.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {medicine.MedicineName}.");
                if (medicine.RequiresPrescription)
                    requiresRx = true;

                orderItems.Add(new OrderItem
                {
                    MedicineID = line.MedicineID,
                    Quantity = line.Quantity,
                    UnitPrice = line.UnitPrice,
                    Subtotal = line.Subtotal
                });
            }

            if (requiresRx && ValidationService.IsNullOrWhiteSpace(prescriptionSourcePath))
                throw new InvalidOperationException("Upload a prescription for Rx medicines before placing the order.");

            var orderId = _orders.CreateOrder(customerId, orderItems);
            if (requiresRx)
                _prescriptions.SavePrescription(customerId, orderId, prescriptionSourcePath);

            foreach (var item in orderItems)
                _medicines.UpdateStock(item.MedicineID, -item.Quantity);

            return orderId;
        }

        public void CancelOrder(int orderId, int customerId)
        {
            var order = _orders.GetById(orderId);
            if (order == null)
                throw new InvalidOperationException("Order not found.");
            if (order.CustomerID != customerId)
                throw new InvalidOperationException("You can only cancel your own orders.");
            if (order.Status != StatusPending)
                throw new InvalidOperationException("Only pending orders can be cancelled.");

            var items = _orders.GetItems(orderId);
            foreach (var item in items)
                _medicines.UpdateStock(item.MedicineID, item.Quantity);

            _orders.DeleteOrder(orderId);
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
