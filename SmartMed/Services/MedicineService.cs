using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Data;
using SmartMed.Models;
using SmartMed.UI;

namespace SmartMed.Services
{
    public class MedicineService
    {
        public const string ExpiryValid = "Valid";
        public const string ExpiryExpiringSoon = "ExpiringSoon";
        public const string ExpiryExpired = "Expired";

        private readonly MedicineRepository _medicines = new MedicineRepository();

        public List<Medicine> GetAll() => _medicines.GetAll();

        public Medicine GetById(int id) => _medicines.GetById(id);

        public List<Medicine> Search(string name, string category, decimal? minPrice, decimal? maxPrice)
        {
            return SearchService.Search(GetAll(), name, category, minPrice, maxPrice);
        }

        public List<Medicine> SearchForCustomers(string name, string category, decimal? minPrice, decimal? maxPrice)
        {
            return Search(name, category, minPrice, maxPrice).Where(IsAvailableForSale).ToList();
        }

        public bool IsExpired(Medicine m) => CheckExpiry(m) == ExpiryExpired;

        public bool IsAvailableForSale(Medicine m) =>
            !IsExpired(m) && m.StockQuantity > 0;

        public void ValidateForCustomerPurchase(Medicine m)
        {
            if (m == null)
                throw new InvalidOperationException("Medicine not found.");
            if (IsExpired(m))
                throw new InvalidOperationException($"{m.MedicineName} has expired and cannot be purchased.");
            if (m.StockQuantity <= 0)
                throw new InvalidOperationException($"{m.MedicineName} is out of stock.");
        }

        public void ReduceStock(int medicineId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            if (!_medicines.ReduceStock(medicineId, quantity))
                throw new InvalidOperationException("Insufficient stock.");
        }

        public void RestoreStock(int medicineId, int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero.");
            _medicines.RestoreStock(medicineId, quantity);
        }

        public List<Medicine> GetExpiredMedicines() =>
            GetAll().Where(IsExpired).OrderBy(m => m.ExpiryDate).ToList();

        public List<Medicine> GetExpiringSoonMedicines(int warningDays = 30) =>
            GetAll()
                .Where(m => CheckExpiry(m, warningDays) == ExpiryExpiringSoon)
                .OrderBy(m => m.ExpiryDate)
                .ToList();

        public List<string> GetExpiryAlertMessages(int warningDays = 30) =>
            GetAll()
                .Where(m => CheckExpiry(m, warningDays) != ExpiryValid)
                .OrderBy(m => m.ExpiryDate)
                .Select(m =>
                {
                    var status = CheckExpiry(m, warningDays) == ExpiryExpired ? "Expired" : "Expiring soon";
                    return $"{status} — {m.MedicineName} (exp. {m.ExpiryDate:yyyy-MM-dd})";
                })
                .ToList();

        public bool HasExpiryAlerts(int warningDays = 30) =>
            GetAll().Any(m => CheckExpiry(m, warningDays) != ExpiryValid);

        public void ValidateMedicine(Medicine item, bool isNew)
        {
            if (!isNew && item.MedicineID <= 0)
                throw new ArgumentException("Select a medicine to update.");
            if (ValidationService.IsNullOrWhiteSpace(item.MedicineName))
                throw new ArgumentException("Medicine name is required.");
            if (ValidationService.IsNullOrWhiteSpace(item.Category))
                throw new ArgumentException("Category is required.");
            if (ValidationService.IsNullOrWhiteSpace(item.Dosage))
                throw new ArgumentException("Dosage is required.");
            if (ValidationService.IsNullOrWhiteSpace(item.Supplier))
                throw new ArgumentException("Supplier is required.");
            if (item.Price < 0)
                throw new ArgumentException("Price must be non-negative.");
            if (item.StockQuantity < 0)
                throw new ArgumentException("Stock quantity must be 0 or greater.");
            if (isNew && item.StockQuantity <= 0)
                throw new ArgumentException("Stock quantity must be greater than 0 when adding a new medicine.");
            if (item.DiscountPercent < 0 || item.DiscountPercent > 100)
                throw new ArgumentException("Discount must be between 0 and 100.");
            if (item.IsOnPromotion)
            {
                if (!item.PromotionStartDate.HasValue || !item.PromotionEndDate.HasValue)
                    throw new ArgumentException("Promotion start date and end date are required when a medicine is on promotion.");
                if (item.PromotionStartDate.Value.Date > item.PromotionEndDate.Value.Date)
                    throw new ArgumentException("Promotion start date cannot be after the end date.");
            }
            else
            {
                item.PromotionStartDate = null;
                item.PromotionEndDate = null;
            }
            if (isNew && item.ExpiryDate.Date < DateTime.Today)
                throw new ArgumentException("Expiry date cannot be in the past.");

            item.Description = NormalizeOptional(item.Description, 500, "Description");
            item.ActiveIngredient = NormalizeOptional(item.ActiveIngredient, 200, "Active ingredient");
            item.UsageInstructions = NormalizeOptional(item.UsageInstructions, 500, "Usage instructions");
            item.Warnings = NormalizeOptional(item.Warnings, 500, "Warnings");
            item.SideEffects = NormalizeOptional(item.SideEffects, 300, "Side effects");
            item.PackSize = NormalizeOptional(item.PackSize, 100, "Pack size");
        }

        private static string NormalizeOptional(string value, int maxLength, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
                return null;
            var trimmed = value.Trim();
            if (trimmed.Length > maxLength)
                throw new ArgumentException($"{fieldName} must be {maxLength} characters or fewer.");
            return trimmed;
        }

        public void Add(Medicine item)
        {
            ValidateMedicine(item, isNew: true);
            if (_medicines.NameExists(item.MedicineName))
                throw new InvalidOperationException("This medicine is already in the inventory list.");
            _medicines.Insert(item);
        }

        public void Update(Medicine item)
        {
            ValidateMedicine(item, isNew: false);
            var existing = _medicines.GetById(item.MedicineID);
            if (existing == null)
                throw new InvalidOperationException("Medicine not found.");
            if (!string.Equals(existing.MedicineName, item.MedicineName, StringComparison.OrdinalIgnoreCase)
                && _medicines.NameExists(item.MedicineName))
                throw new InvalidOperationException("This medicine is already in the inventory list.");
            if (_medicines.Update(item) == 0)
                throw new InvalidOperationException("Medicine not found. Select an existing medicine from the list to update.");
        }

        public void Delete(int medicineId)
        {
            if (medicineId <= 0)
                throw new ArgumentException("Select a medicine to delete.");
            if (_medicines.GetById(medicineId) == null)
                throw new InvalidOperationException("Medicine not found.");
            if (_medicines.HasActiveOrPendingOrderItems(medicineId))
                throw new InvalidOperationException(
                    "Cannot delete this medicine because it is linked to active or pending orders.");
            _medicines.RemoveDeliveredOrderItemReferences(medicineId);
            _medicines.Delete(medicineId);
        }

        public string CheckExpiry(Medicine m, int warningDays = 30)
        {
            var today = DateTime.Today;
            if (m.ExpiryDate.Date < today)
                return ExpiryExpired;
            if (m.ExpiryDate.Date <= today.AddDays(warningDays))
                return ExpiryExpiringSoon;
            return ExpiryValid;
        }

        public bool IsLowStock(Medicine m, int threshold = 20) => m.StockQuantity <= threshold;

        public bool IsPromotionActive(Medicine m)
        {
            if (m == null || !m.IsOnPromotion || m.DiscountPercent <= 0)
                return false;

            var today = DateTime.Today;
            if (m.PromotionStartDate.HasValue && m.PromotionStartDate.Value.Date > today)
                return false;
            if (m.PromotionEndDate.HasValue && m.PromotionEndDate.Value.Date < today)
                return false;
            return true;
        }

        public decimal GetEffectivePrice(Medicine m)
        {
            if (IsPromotionActive(m))
                return Math.Round(m.Price * (1 - m.DiscountPercent / 100m), 2);
            return m.Price;
        }

        public string GetCustomerDiscountDisplay(Medicine m) =>
            m != null && m.DiscountPercent > 0 ? $"{m.DiscountPercent:N0}%" : "—";

        public string GetCustomerPromoDisplay(Medicine m)
        {
            if (m == null) return "—";
            if (IsPromotionActive(m)) return "Active";
            if (m.IsOnPromotion && m.DiscountPercent > 0) return "Scheduled";
            return "—";
        }

        public string GetCustomerOfferDisplay(Medicine m) =>
            m != null && IsPromotionActive(m) ? $"{m.DiscountPercent:N0}% promo applied" : "—";

        public string GetCustomerStockDisplay(Medicine m)
        {
            if (m == null) return "—";
            if (IsExpired(m)) return "Unavailable (expired)";
            if (m.StockQuantity <= 0) return "Out of stock";
            if (IsLowStock(m)) return $"Limited stock ({m.StockQuantity} left)";
            return $"In stock ({m.StockQuantity} available)";
        }

        public string GetOrderLineOfferDisplay(decimal unitPrice, decimal listPrice, decimal discountPercent)
        {
            if (listPrice <= 0 || unitPrice >= listPrice)
                return "—";

            if (discountPercent > 0)
                return $"{discountPercent:N0}% promo applied";

            var pct = Math.Round((1 - unitPrice / listPrice) * 100m, 0);
            return pct > 0 ? $"{pct:N0}% discount applied" : "—";
        }

        public int CountExpired(IEnumerable<Medicine> items) =>
            items.Count(m => CheckExpiry(m) == ExpiryExpired);

        public int CountExpiringSoon(IEnumerable<Medicine> items, int warningDays = 30) =>
            items.Count(m => CheckExpiry(m, warningDays) == ExpiryExpiringSoon);

        public decimal CompliancePercent(IEnumerable<Medicine> items)
        {
            var list = items.ToList();
            if (list.Count == 0) return 0m;
            var compliant = list.Count(m => CheckExpiry(m) != ExpiryExpired);
            return (decimal)compliant / list.Count * 100m;
        }

        public List<Medicine> GetLowStock(int threshold = 20, int maxCount = 4) =>
            GetAll()
                .Where(m => IsLowStock(m, threshold))
                .OrderBy(m => m.StockQuantity)
                .Take(maxCount)
                .ToList();

        public void ExportToCsv(IList<Medicine> medicines, string filePath)
        {
            ExportHelper.ExportDataTableToCsv(BuildInventoryExportTable(medicines), filePath);
        }

        public void ExportToPdf(IList<Medicine> medicines, string filePath)
        {
            if (medicines == null || medicines.Count == 0)
                throw new InvalidOperationException("No medicines to export.");

            var subtitle = $"Inventory export  |  Generated: {DateTime.Now:MMM dd, yyyy HH:mm}  |  Items: {medicines.Count}";
            ExportHelper.ExportDataTableToPdf(
                BuildInventoryExportTable(medicines),
                filePath,
                "SmartMed Medicine Inventory",
                subtitle);
        }

        private DataTable BuildInventoryExportTable(IList<Medicine> medicines)
        {
            var table = new DataTable();
            table.Columns.Add("Name");
            table.Columns.Add("Category");
            table.Columns.Add("Dosage");
            table.Columns.Add("Stock");
            table.Columns.Add("Price (LKR)");
            table.Columns.Add("Effective (LKR)");
            table.Columns.Add("Supplier");
            table.Columns.Add("Expiry");
            table.Columns.Add("Rx");
            table.Columns.Add("Discount %");
            table.Columns.Add("Promo");

            foreach (var m in medicines ?? Array.Empty<Medicine>())
            {
                table.Rows.Add(
                    m.MedicineName,
                    m.Category,
                    m.Dosage,
                    m.StockQuantity.ToString(),
                    m.Price.ToString("N2"),
                    GetEffectivePrice(m).ToString("N2"),
                    m.Supplier,
                    m.ExpiryDate.ToString("yyyy-MM-dd"),
                    m.RequiresPrescription ? "Yes" : "No",
                    m.DiscountPercent.ToString("N0"),
                    m.IsOnPromotion ? "Yes" : "No");
            }

            return table;
        }

        public void PrintInventory(IList<Medicine> medicines, string title)
        {
            if (medicines == null || medicines.Count == 0)
                throw new InvalidOperationException("No medicines to print.");

            var index = 0;
            var bodyFont = UiTheme.UiFont;
            var doc = new PrintDocument { DocumentName = title };
            doc.PrintPage += (s, e) =>
            {
                float y = e.MarginBounds.Top;
                float lineHeight = e.Graphics.MeasureString("X", bodyFont).Height + 4;

                e.Graphics.DrawString(title, UiTheme.UiFontBold, Brushes.Black, e.MarginBounds.Left, y);
                y += lineHeight * 2;

                e.Graphics.DrawString(
                    "Name | Category | Stock | Price (LKR) | Expiry | Rx",
                    UiTheme.UiFontBold,
                    Brushes.Black,
                    e.MarginBounds.Left,
                    y);
                y += lineHeight;

                while (index < medicines.Count && y + lineHeight < e.MarginBounds.Bottom)
                {
                    var m = medicines[index++];
                    var line = $"{m.MedicineName} | {m.Category} | {m.StockQuantity} | {GetEffectivePrice(m):N2} | {m.ExpiryDate:yyyy-MM-dd} | {(m.RequiresPrescription ? "Yes" : "No")}";
                    e.Graphics.DrawString(line, bodyFont, Brushes.Black, e.MarginBounds.Left, y);
                    y += lineHeight;
                }

                e.HasMorePages = index < medicines.Count;
            };

            using (var preview = new PrintPreviewDialog { Document = doc, Width = 900, Height = 650 })
                preview.ShowDialog();
        }
    }
}
