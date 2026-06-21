using System;

namespace SmartMed.Models
{
    public enum ExpiryStatus
    {
        Valid,
        ExpiringSoon,
        Expired
    }

    public class Medicine
    {
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public string Category { get; set; }
        public string Dosage { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Supplier { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool RequiresPrescription { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool IsOnPromotion { get; set; }

        public ExpiryStatus CheckExpiry(int warningDays = 30)
        {
            var today = DateTime.Today;
            if (ExpiryDate.Date < today)
                return ExpiryStatus.Expired;
            if (ExpiryDate.Date <= today.AddDays(warningDays))
                return ExpiryStatus.ExpiringSoon;
            return ExpiryStatus.Valid;
        }

        public bool IsLowStock(int threshold = 20) => StockQuantity <= threshold;

        public decimal GetEffectivePrice()
        {
            if (IsOnPromotion && DiscountPercent > 0)
                return Math.Round(Price * (1 - DiscountPercent / 100m), 2);
            return Price;
        }

        public void AddMedicine()
        {
            new Services.MedicineService().Add(this);
        }

        public void UpdateMedicine()
        {
            new Services.MedicineService().Update(this);
        }

        public void DeleteMedicine()
        {
            new Services.MedicineService().Delete(MedicineID);
        }
    }
}
