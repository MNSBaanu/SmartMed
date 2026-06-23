using System.Collections.Generic;
using System.Linq;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class CartLine
    {
        public int MedicineID { get; set; }
        public string MedicineName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool RequiresPrescription { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;
    }

    public static class CartService
    {
        private static readonly List<CartLine> Lines = new List<CartLine>();

        public static IReadOnlyList<CartLine> Items => Lines;

        public static int ItemCount => Lines.Sum(l => l.Quantity);

        public static decimal Total => Lines.Sum(l => l.Subtotal);

        public static bool RequiresPrescription => Lines.Any(l => l.RequiresPrescription);

        public static void Clear() => Lines.Clear();

        public static void Add(Medicine medicine, int quantity, decimal unitPrice)
        {
            var existing = Lines.FirstOrDefault(l => l.MedicineID == medicine.MedicineID);
            if (existing != null)
            {
                existing.Quantity += quantity;
                return;
            }

            Lines.Add(new CartLine
            {
                MedicineID = medicine.MedicineID,
                MedicineName = medicine.MedicineName,
                Quantity = quantity,
                UnitPrice = unitPrice,
                RequiresPrescription = medicine.RequiresPrescription
            });
        }

        public static void Remove(int medicineId) =>
            Lines.RemoveAll(l => l.MedicineID == medicineId);

        public static void UpdateQuantity(int medicineId, int quantity)
        {
            var line = Lines.FirstOrDefault(l => l.MedicineID == medicineId);
            if (line == null) return;
            if (quantity <= 0)
                Lines.Remove(line);
            else
                line.Quantity = quantity;
        }
    }
}
