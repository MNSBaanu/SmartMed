using System;

namespace SmartMed.Models
{
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
        public DateTime? PromotionStartDate { get; set; }
        public DateTime? PromotionEndDate { get; set; }

        public void AddMedicine() { }
        public void UpdateMedicine() { }
        public void DeleteMedicine() { }
        public void CheckExpiry() { }
    }
}
