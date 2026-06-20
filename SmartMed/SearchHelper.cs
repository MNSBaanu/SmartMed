using System.Collections.Generic;
using SmartMed.Models;

namespace SmartMed.Business
{
    /// <summary>
    /// Linear search and filter algorithms for medicine catalogue.
    /// </summary>
    public static class SearchHelper
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
    }
}
