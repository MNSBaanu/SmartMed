using System;

namespace SmartMed.Business.Models
{
    public class Medicine
    {
        public int MedicineID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Dosage { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Supplier { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool RequiresPrescription { get; set; }

        public void AddMedicine() { }
        public void UpdateMedicine() { }
        public void DeleteMedicine() { }
        public void CheckExpiry() { }
    }
}
