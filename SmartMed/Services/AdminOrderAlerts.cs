using System;
using System.Collections.Generic;
using System.Linq;
using SmartMed.Models;

namespace SmartMed.Services
{
    /// <summary>
    /// Tracks customer-placed orders the admin has not acknowledged yet (in-memory, per app session).
    /// </summary>
    public static class AdminOrderAlerts
    {
        private static int _lastAcknowledgedOrderId;
        private static readonly HashSet<int> PlacedThisSession = new HashSet<int>();

        public static event EventHandler AlertsChanged;

        public static void NotifyOrderPlaced(int orderId)
        {
            if (orderId <= 0) return;
            PlacedThisSession.Add(orderId);
            AlertsChanged?.Invoke(null, EventArgs.Empty);
        }

        public static bool IsNew(Order order)
        {
            if (order == null) return false;
            if (!string.Equals(order.Status, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase))
                return false;

            return order.OrderID > _lastAcknowledgedOrderId || PlacedThisSession.Contains(order.OrderID);
        }

        public static int GetNewCount(IEnumerable<Order> orders) =>
            orders?.Count(IsNew) ?? 0;

        public static void AcknowledgeAll(IEnumerable<Order> orders)
        {
            var maxId = orders?.Select(o => o.OrderID).DefaultIfEmpty(0).Max() ?? 0;
            if (maxId > _lastAcknowledgedOrderId)
                _lastAcknowledgedOrderId = maxId;
            PlacedThisSession.Clear();
            AlertsChanged?.Invoke(null, EventArgs.Empty);
        }
    }
}
