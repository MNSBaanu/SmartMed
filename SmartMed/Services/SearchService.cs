using System;
using System.Collections.Generic;
using SmartMed.Models;

namespace SmartMed.Services
{
    public static class SearchService
    {
        public static List<Medicine> SearchByName(List<Medicine> medicines, string keyword)
        {
            var results = new List<Medicine>();
            if (string.IsNullOrWhiteSpace(keyword)) return medicines;

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

        /// <summary>Linear search O(n) — matches name, email, phone, or customer ID.</summary>
        public static List<Customer> SearchCustomers(IList<Customer> customers, string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new List<Customer>(customers);

            var key = keyword.Trim();
            var keyLower = key.ToLowerInvariant();
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
                if (customer.Phone != null
                    && ValidationService.NormalizePhoneDigits(customer.Phone)
                        .IndexOf(keyDigits, StringComparison.OrdinalIgnoreCase) >= 0)
                    results.Add(customer);
            }
            return results;
        }

        private static bool MatchesCustomerId(Customer customer, string key)
        {
            if (int.TryParse(key, out var id) && customer.CustomerID == id)
                return true;
            return customer.CustomerID.ToString().IndexOf(key, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
