using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
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

        public List<Medicine> GetExpiredMedicines() =>
            GetAll().Where(IsExpired).OrderBy(m => m.ExpiryDate).ToList();

        public List<Medicine> GetExpiringSoonMedicines(int warningDays = 30) =>
            GetAll()
                .Where(m => CheckExpiry(m, warningDays) == ExpiryExpiringSoon)
                .OrderBy(m => m.ExpiryDate)
                .ToList();

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
                if (item.DiscountPercent <= 0)
                    throw new ArgumentException("Discount must be greater than 0 for a promotion.");
            }
            else
            {
                item.PromotionStartDate = null;
                item.PromotionEndDate = null;
            }
            if (isNew && item.ExpiryDate.Date < DateTime.Today)
                throw new ArgumentException("Expiry date cannot be in the past.");
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
            var sb = new StringBuilder();
            sb.AppendLine("MedicineName,Category,Dosage,Price,Stock,Supplier,Expiry,Rx,DiscountPercent,OnPromotion,EffectivePrice");
            foreach (var m in medicines)
            {
                sb.Append(EscapeCsv(m.MedicineName)).Append(',');
                sb.Append(EscapeCsv(m.Category)).Append(',');
                sb.Append(EscapeCsv(m.Dosage)).Append(',');
                sb.Append(m.Price.ToString("F2")).Append(',');
                sb.Append(m.StockQuantity).Append(',');
                sb.Append(EscapeCsv(m.Supplier)).Append(',');
                sb.Append(m.ExpiryDate.ToString("yyyy-MM-dd")).Append(',');
                sb.Append(m.RequiresPrescription ? "Yes" : "No").Append(',');
                sb.Append(m.DiscountPercent.ToString("F2")).Append(',');
                sb.Append(m.IsOnPromotion ? "Yes" : "No").Append(',');
                sb.AppendLine(GetEffectivePrice(m).ToString("F2"));
            }
            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
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

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
