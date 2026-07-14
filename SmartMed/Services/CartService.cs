using System;
using System.Collections.Generic;
using System.Linq;
using SmartMed.Data;
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
        public bool SelectedForCheckout { get; set; } = true;
        public decimal Subtotal => UnitPrice * Quantity;

        public string DiscountDisplay => PromoApplied ? $"{DiscountPercent:N0}%" : "—";
        public string PromoDisplay => PromoApplied ? "Active" : "—";
        public string OfferDisplay => PromoApplied ? $"{DiscountPercent:N0}% off applied" : "—";
        public string PrescriptionDisplay => !RequiresPrescription
            ? "—"
            : string.IsNullOrWhiteSpace(PrescriptionPath) ? "Required" : System.IO.Path.GetFileName(PrescriptionPath);
    }

    public static class CartService
    {
        private static readonly List<CartLine> Lines = new List<CartLine>();
        private static readonly CartRepository Repository = new CartRepository();
        private static int? _customerId;

        public static IReadOnlyList<CartLine> Items => Lines;

        public static IReadOnlyList<CartLine> SelectedItems =>
            Lines.Where(l => l.SelectedForCheckout).ToList();

        public static int ItemCount => Lines.Sum(l => l.Quantity);

        public static int SelectedItemCount => SelectedItems.Sum(l => l.Quantity);

        public static decimal Total => Lines.Sum(l => l.Subtotal);

        public static decimal SelectedTotal => SelectedItems.Sum(l => l.Subtotal);

        public static bool RequiresPrescription => Lines.Any(l => l.RequiresPrescription);

        public static bool SelectedRequiresPrescription =>
            SelectedItems.Any(l => l.RequiresPrescription);

        public static IEnumerable<CartLine> MissingPrescriptions =>
            Lines.Where(l => l.RequiresPrescription && string.IsNullOrWhiteSpace(l.PrescriptionPath));

        public static IEnumerable<CartLine> SelectedMissingPrescriptions =>
            SelectedItems.Where(l => l.RequiresPrescription && string.IsNullOrWhiteSpace(l.PrescriptionPath));

        public static string FirstPrescriptionPath =>
            Lines.FirstOrDefault(l => l.RequiresPrescription && !string.IsNullOrWhiteSpace(l.PrescriptionPath))?.PrescriptionPath;

        public static string SelectedFirstPrescriptionPath =>
            SelectedItems.FirstOrDefault(l => l.RequiresPrescription && !string.IsNullOrWhiteSpace(l.PrescriptionPath))?.PrescriptionPath;

        public static void LoadForCustomer(int customerId, MedicineService medicines)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");
            if (medicines == null)
                throw new ArgumentNullException(nameof(medicines));

            _customerId = customerId;
            Lines.Clear();

            foreach (var stored in Repository.GetByCustomer(customerId))
            {
                var fresh = medicines.GetById(stored.MedicineID);
                if (fresh == null)
                    continue;

                try
                {
                    medicines.ValidateForCustomerPurchase(fresh);
                }
                catch
                {
                    // Skip cart lines that can no longer be sold.
                    continue;
                }

                // Cap the quantity if stock has fallen since the item was added.
                if (stored.Quantity > fresh.StockQuantity)
                    stored.Quantity = fresh.StockQuantity;

                if (stored.Quantity <= 0)
                    continue;

                Lines.Add(BuildLine(fresh, stored.Quantity, stored.PrescriptionPath, medicines));
            }

            Persist();
        }

        public static void Unload()
        {
            _customerId = null;
            Lines.Clear();
        }

        public static void SetPrescription(int medicineId, string path)
        {
            var line = Lines.FirstOrDefault(l => l.MedicineID == medicineId);
            if (line == null) return;

            line.PrescriptionPath = path;
            Persist();
        }

        public static void Clear(MedicineService _ = null)
        {
            Lines.Clear();
            if (_customerId.HasValue)
                Repository.Clear(_customerId.Value);
        }

        public static void ReleaseAll(MedicineService _) => Unload();

        public static void Discard()
        {
            Clear();
        }

        public static void Add(Medicine medicine, int quantity, MedicineService medicines)
        {
            EnsureCustomerLoaded();
            if (medicines == null)
                throw new ArgumentNullException(nameof(medicines));
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");

            var fresh = medicines.GetById(medicine.MedicineID)
                ?? throw new InvalidOperationException("Medicine not found.");
            // Ensure the medicine is available before adding it to the cart.
            medicines.ValidateForCustomerPurchase(fresh);

            var existing = Lines.FirstOrDefault(l => l.MedicineID == medicine.MedicineID);
            var newTotal = (existing?.Quantity ?? 0) + quantity;
            if (newTotal > fresh.StockQuantity)
                throw new InvalidOperationException("Quantity exceeds available stock.");

            var promoApplied = medicines.IsDiscountApplicable(fresh);
            var unitPrice = medicines.GetEffectivePrice(fresh);

            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.UnitPrice = unitPrice;
                existing.ListPrice = fresh.Price;
                existing.DiscountPercent = fresh.DiscountPercent;
                existing.PromoApplied = promoApplied;
            }
            else
            {
                Lines.Add(BuildLine(fresh, quantity, null, medicines));
            }

            Persist();
        }

        public static void Remove(int medicineId, MedicineService _)
        {
            var line = Lines.FirstOrDefault(l => l.MedicineID == medicineId);
            if (line == null) return;

            Lines.Remove(line);
            Persist();
        }

        public static void RemoveMedicineFromAllCarts(int medicineId)
        {
            if (medicineId <= 0) return;

            Repository.DeleteByMedicineId(medicineId);
            Lines.RemoveAll(l => l.MedicineID == medicineId);
        }

        public static void RemoveMany(IEnumerable<int> medicineIds)
        {
            if (medicineIds == null) return;
            var ids = new HashSet<int>(medicineIds);
            if (ids.Count == 0) return;

            Lines.RemoveAll(l => ids.Contains(l.MedicineID));
            Persist();
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

            if (medicines != null)
            {
                var fresh = medicines.GetById(medicineId)
                    ?? throw new InvalidOperationException("Medicine not found.");
                medicines.ValidateForCustomerPurchase(fresh);
                if (quantity > fresh.StockQuantity)
                    throw new InvalidOperationException("Quantity exceeds available stock.");

                var promoApplied = medicines.IsDiscountApplicable(fresh);
                line.UnitPrice = medicines.GetEffectivePrice(fresh);
                line.ListPrice = fresh.Price;
                line.DiscountPercent = fresh.DiscountPercent;
                line.PromoApplied = promoApplied;
            }

            line.Quantity = quantity;
            Persist();
        }

        public static void RefreshPrices(MedicineService medicines)
        {
            if (medicines == null)
                throw new ArgumentNullException(nameof(medicines));

            // Update cart prices so checkout matches the current pharmacy offer.
            foreach (var line in Lines)
            {
                var fresh = medicines.GetById(line.MedicineID);
                if (fresh == null)
                    continue;

                line.UnitPrice = medicines.GetEffectivePrice(fresh);
                line.ListPrice = fresh.Price;
                line.DiscountPercent = fresh.DiscountPercent;
                line.PromoApplied = medicines.IsDiscountApplicable(fresh);
                line.RequiresPrescription = fresh.RequiresPrescription;
                line.MedicineName = fresh.MedicineName;
            }
        }

        private static CartLine BuildLine(Medicine fresh, int quantity, string prescriptionPath, MedicineService medicines)
        {
            return new CartLine
            {
                MedicineID = fresh.MedicineID,
                MedicineName = fresh.MedicineName,
                Quantity = quantity,
                UnitPrice = medicines.GetEffectivePrice(fresh),
                ListPrice = fresh.Price,
                DiscountPercent = fresh.DiscountPercent,
                PromoApplied = medicines.IsDiscountApplicable(fresh),
                RequiresPrescription = fresh.RequiresPrescription,
                PrescriptionPath = prescriptionPath,
                SelectedForCheckout = true
            };
        }

        private static void EnsureCustomerLoaded()
        {
            if (!_customerId.HasValue)
                throw new InvalidOperationException("Please log in again to use your cart.");
        }

        private static void Persist()
        {
            if (!_customerId.HasValue)
                return;

            Repository.SaveAll(_customerId.Value, Lines.Select(l => new CartRepository.StoredCartLine
            {
                MedicineID = l.MedicineID,
                Quantity = l.Quantity,
                PrescriptionPath = l.PrescriptionPath
            }).ToList());
        }
    }
}
