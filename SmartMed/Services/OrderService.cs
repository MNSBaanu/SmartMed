using System;
using System.Collections.Generic;
using System.Linq;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class OrderService
    {
        private static readonly string[] ValidStatuses = { "Pending", "Ready for Pickup", "Delivered" };

        private readonly OrderRepository _orders = new OrderRepository();

        public List<Order> GetAll() => _orders.GetAll();

        public List<OrderItem> GetItems(int orderId) => _orders.GetItems(orderId);

        public void UpdateStatus(int orderId, string status)
        {
            if (orderId <= 0)
                throw new ArgumentException("Select an order to update.");
            if (ValidationService.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status is required.");
            if (!ValidStatuses.Contains(status))
                throw new ArgumentException("Invalid order status.");
            _orders.UpdateStatus(orderId, status);
        }

        public void GetStatusStats(IReadOnlyList<Order> all, out int pending, out int delivered)
        {
            pending = all.Count(o => o.Status == "Pending");
            delivered = all.Count(o => o.Status == "Delivered");
        }

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
