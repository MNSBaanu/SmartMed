using System;
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
        public decimal ListPrice { get; set; }
        public decimal DiscountPercent { get; set; }
        public bool PromoApplied { get; set; }
        public bool RequiresPrescription { get; set; }
        public string PrescriptionPath { get; set; }
        public decimal Subtotal => UnitPrice * Quantity;

        public string DiscountDisplay => DiscountPercent > 0 ? $"{DiscountPercent:N0}%" : "—";
        public string PromoDisplay => PromoApplied ? "Active" : "—";
        public string OfferDisplay => PromoApplied ? $"{DiscountPercent:N0}% promo applied" : "—";
        public string PrescriptionDisplay => !RequiresPrescription
            ? "—"
            : string.IsNullOrWhiteSpace(PrescriptionPath) ? "Required" : System.IO.Path.GetFileName(PrescriptionPath);
    }

    public static class CartService
    {
        private static readonly List<CartLine> Lines = new List<CartLine>();

        public static IReadOnlyList<CartLine> Items => Lines;

        public static int ItemCount => Lines.Sum(l => l.Quantity);

        public static decimal Total => Lines.Sum(l => l.Subtotal);

        public static bool RequiresPrescription => Lines.Any(l => l.RequiresPrescription);

        public static IEnumerable<CartLine> MissingPrescriptions =>
            Lines.Where(l => l.RequiresPrescription && string.IsNullOrWhiteSpace(l.PrescriptionPath));

        public static string FirstPrescriptionPath =>
            Lines.FirstOrDefault(l => l.RequiresPrescription && !string.IsNullOrWhiteSpace(l.PrescriptionPath))?.PrescriptionPath;

        public static void SetPrescription(int medicineId, string path)
        {
            var line = Lines.FirstOrDefault(l => l.MedicineID == medicineId);
            if (line != null)
                line.PrescriptionPath = path;
        }

        public static void Clear(MedicineService medicines, bool restoreStock = true)
        {
            if (restoreStock && medicines != null)
            {
                foreach (var line in Lines.ToList())
                    medicines.RestoreStock(line.MedicineID, line.Quantity);
            }

            Lines.Clear();
        }

        public static void ReleaseAll(MedicineService medicines) => Clear(medicines, restoreStock: true);

        public static void Discard() => Lines.Clear();

        public static void Add(Medicine medicine, int quantity, MedicineService medicines)
        {
            if (medicines == null)
                throw new ArgumentNullException(nameof(medicines));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var fresh = medicines.GetById(medicine.MedicineID);
            if (fresh == null)
                throw new InvalidOperationException("Medicine not found.");
            medicines.ValidateForCustomerPurchase(fresh);

            var existing = Lines.FirstOrDefault(l => l.MedicineID == medicine.MedicineID);
            var newTotal = (existing?.Quantity ?? 0) + quantity;
            if (newTotal > fresh.StockQuantity)
                throw new InvalidOperationException("Quantity exceeds available stock.");

            medicines.ReduceStock(medicine.MedicineID, quantity);

            try
            {
                var promoApplied = medicines.IsPromotionActive(fresh);
                var unitPrice = medicines.GetEffectivePrice(fresh);

                if (existing != null)
                {
                    existing.Quantity += quantity;
                    existing.UnitPrice = unitPrice;
                    existing.ListPrice = fresh.Price;
                    existing.DiscountPercent = fresh.DiscountPercent;
                    existing.PromoApplied = promoApplied;
                    return;
                }

                Lines.Add(new CartLine
                {
                    MedicineID = fresh.MedicineID,
                    MedicineName = fresh.MedicineName,
                    Quantity = quantity,
                    UnitPrice = unitPrice,
                    ListPrice = fresh.Price,
                    DiscountPercent = fresh.DiscountPercent,
                    PromoApplied = promoApplied,
                    RequiresPrescription = fresh.RequiresPrescription
                });
            }
            catch
            {
                medicines.RestoreStock(medicine.MedicineID, quantity);
                throw;
            }
        }

        public static void Remove(int medicineId, MedicineService medicines)
        {
            var line = Lines.FirstOrDefault(l => l.MedicineID == medicineId);
            if (line == null) return;

            medicines?.RestoreStock(medicineId, line.Quantity);
            Lines.Remove(line);
        }

        public static void UpdateQuantity(int medicineId, int quantity, MedicineService medicines)
        {
            var line = Lines.FirstOrDefault(l => l.MedicineID == medicineId);
            if (line == null) return;

            if (quantity <= 0)
            {
                Remove(medicineId, medicines);
                return;
            }

            if (medicines == null)
            {
                line.Quantity = quantity;
                return;
            }

            var delta = quantity - line.Quantity;
            if (delta == 0) return;

            if (delta > 0)
            {
                var fresh = medicines.GetById(medicineId);
                if (fresh == null)
                    throw new InvalidOperationException("Medicine not found.");
                if (delta > fresh.StockQuantity)
                    throw new InvalidOperationException("Quantity exceeds available stock.");
                medicines.ReduceStock(medicineId, delta);
            }
            else
            {
                medicines.RestoreStock(medicineId, -delta);
            }

            line.Quantity = quantity;
        }
    }
}
