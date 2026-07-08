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

        public static void Clear() => Lines.Clear();

        public static void Add(Medicine medicine, int quantity, MedicineService medicines)
        {
            if (medicines == null)
                throw new ArgumentNullException(nameof(medicines));

            ValidateLine(medicine, quantity);

            var promoApplied = medicines.IsPromotionActive(medicine);
            var unitPrice = medicines.GetEffectivePrice(medicine);

            var existing = Lines.FirstOrDefault(l => l.MedicineID == medicine.MedicineID);
            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.UnitPrice = unitPrice;
                existing.ListPrice = medicine.Price;
                existing.DiscountPercent = medicine.DiscountPercent;
                existing.PromoApplied = promoApplied;
                return;
            }

            Lines.Add(new CartLine
            {
                MedicineID = medicine.MedicineID,
                MedicineName = medicine.MedicineName,
                Quantity = quantity,
                UnitPrice = unitPrice,
                ListPrice = medicine.Price,
                DiscountPercent = medicine.DiscountPercent,
                PromoApplied = promoApplied,
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

        private static void ValidateLine(Medicine medicine, int quantity)
        {
            if (medicine == null)
                throw new InvalidOperationException("Medicine not found.");
            if (medicine.ExpiryDate.Date < DateTime.Today)
                throw new InvalidOperationException($"{medicine.MedicineName} has expired and cannot be purchased.");
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (quantity > medicine.StockQuantity)
                throw new InvalidOperationException("Quantity exceeds available stock.");
        }
    }
}
