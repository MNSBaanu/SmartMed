using System;
using System.Collections.Generic;
using System.Linq;
using SmartMed.Models;

namespace SmartMed.Services
{
    public static class SearchService
    {
        public static List<Medicine> SearchByName(List<Medicine> medicines, string keyword)
        {
            var results = new List<Medicine>();
            if (string.IsNullOrWhiteSpace(keyword)) return medicines;

            // Find medicines whose names contain the search text.
            string key = keyword.Trim().ToLower();
            foreach (var medicine in medicines)
            {
                if (medicine.MedicineName.ToLower().Contains(key))
                    results.Add(medicine);
            }
            return results;
        }

        public static List<Medicine> FilterByCategory(List<Medicine> medicines, string category)
        {
            var results = new List<Medicine>();
            if (string.IsNullOrWhiteSpace(category)) return medicines;

            string cat = category.Trim().ToLower();
            foreach (var medicine in medicines)
            {
                if (medicine.Category.ToLower().Contains(cat))
                    results.Add(medicine);
            }
            return results;
        }

        public static List<Medicine> FilterByPriceRange(List<Medicine> medicines, decimal minPrice, decimal maxPrice)
        {
            var results = new List<Medicine>();
            foreach (var medicine in medicines)
            {
                if (medicine.Price >= minPrice && medicine.Price <= maxPrice)
                    results.Add(medicine);
            }
            return results;
        }

        public static List<Medicine> Search(List<Medicine> medicines, string name, string category, decimal? minPrice, decimal? maxPrice)
        {
            // Narrow the medicine list by name, category, and price in that order.
            var results = medicines;
            if (!string.IsNullOrWhiteSpace(name))
                results = SearchByName(results, name);
            if (!string.IsNullOrWhiteSpace(category))
                results = FilterByCategory(results, category);
            if (minPrice.HasValue || maxPrice.HasValue)
            {
                decimal min = minPrice ?? 0;
                decimal max = maxPrice ?? decimal.MaxValue;
                results = FilterByPriceRange(results, min, max);
            }
            return results;
        }

        public static List<Customer> SearchCustomers(IList<Customer> customers, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Customer>(customers);

            // Find customers by ID, name, email, or phone number.
            var key = keyword.Trim();
            var keyDigits = ValidationService.NormalizePhoneDigits(key);
            var results = new List<Customer>();

            foreach (var customer in customers)
            {
                if (MatchesCustomerId(customer, key))
                {
                    results.Add(customer);
                    continue;
                }
                if (customer.Name != null
                    && customer.Name.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(customer);
                    continue;
                }
                if (customer.Email != null
                    && customer.Email.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(customer);
                    continue;
                }
                if (keyDigits.Length > 0
                    && customer.Phone != null
                    && ValidationService.NormalizePhoneDigits(customer.Phone)
                        .IndexOf(keyDigits, StringComparison.Ordinal) >= 0)
                    results.Add(customer);
            }
            return results;
        }

        private static bool MatchesCustomerId(Customer customer, string key)
        {
            if (int.TryParse(key, out var id) && customer.CustomerID == id)
                return true;
            if (key.All(char.IsDigit))
                return customer.CustomerID.ToString().IndexOf(key, StringComparison.Ordinal) >= 0;
            return false;
        }

        public static List<Order> SearchOrders(IList<Order> orders, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Order>(orders);

            // Find orders by reference number, customer name, or status.
            var key = keyword.Trim();
            var results = new List<Order>();

            foreach (var order in orders)
            {
                if (MatchesOrderId(order, key))
                {
                    results.Add(order);
                    continue;
                }
                if (order.CustomerName != null
                    && order.CustomerName.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    results.Add(order);
                    continue;
                }
                if (order.Status != null
                    && order.Status.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0)
                    results.Add(order);
            }

            return results;
        }

        private static bool MatchesOrderId(Order order, string key)
        {
            if (int.TryParse(key, out var id) && order.OrderID == id)
                return true;

            var normalized = key.TrimStart('#');
            if (normalized.StartsWith("SM-", StringComparison.OrdinalIgnoreCase))
                normalized = normalized.Substring(3);
            else if (normalized.StartsWith("ORD-", StringComparison.OrdinalIgnoreCase))
                normalized = normalized.Substring(4);

            if (int.TryParse(normalized, out id) && order.OrderID == id)
                return true;

            if (key.All(char.IsDigit))
                return order.OrderID.ToString().IndexOf(key, StringComparison.Ordinal) >= 0;

            var orderRef = $"SM-{order.OrderID:D4}";
            return orderRef.IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0
                || $"#{orderRef}".IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
