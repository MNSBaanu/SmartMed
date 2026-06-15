using System;

namespace SmartMed.Data.Models
{
    public class MedicineItem
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
    }
}
