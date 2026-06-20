using System.Collections.Generic;
using SmartMed.Data.Repositories;
using SmartMed.Models;

namespace SmartMed.Business.Services
{
    public class MedicineService
    {
        private readonly MedicineRepository _repo = new MedicineRepository();

        public List<Medicine> GetAll() => _repo.GetAll();

        public void Add(Medicine item)
        {
            if (string.IsNullOrWhiteSpace(item.MedicineName))
                throw new System.ArgumentException("Medicine name is required.");
            if (item.Price < 0 || item.StockQuantity < 0)
                throw new System.ArgumentException("Price and stock must be non-negative.");
            _repo.Insert(item);
        }

        public void Update(Medicine item) => _repo.Update(item);

        public void Delete(int id) => _repo.Delete(id);

        public List<Medicine> Search(string name, string category, decimal? minPrice, decimal? maxPrice)
        {
            var all = _repo.GetAll();
            return SearchHelper.Search(all, name, category, minPrice, maxPrice);
        }

        public int GetTotalStockCount()
        {
            int total = 0;
            foreach (var m in _repo.GetAll())
                total += m.StockQuantity;
            return total;
        }

        public List<Medicine> GetExpiringSoon(int days = 90)
        {
            var list = new List<Medicine>();
            var threshold = System.DateTime.Today.AddDays(days);
            foreach (var m in _repo.GetAll())
            {
                if (m.ExpiryDate <= threshold)
                    list.Add(m);
            }
            return list;
        }
    }
}
