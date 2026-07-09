using System;
using System.Collections.Generic;
using System.Data;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    internal static class DesignTimePreviewData
    {
        public static List<Medicine> Medicines() => new List<Medicine>
        {
            new Medicine
            {
                MedicineID = 1,
                MedicineName = "Amoxicillin 500mg",
                Category = "Antibiotic",
                Dosage = "Capsule",
                Price = 450m,
                StockQuantity = 12,
                Supplier = "PharmaCo",
                ExpiryDate = DateTime.Today.AddMonths(2),
                RequiresPrescription = true,
                DiscountPercent = 5m,
                IsOnPromotion = true,
                PromotionStartDate = DateTime.Today.AddDays(-7),
                PromotionEndDate = DateTime.Today.AddDays(23)
            },
            new Medicine
            {
                MedicineID = 2,
                MedicineName = "Paracetamol 500mg",
                Category = "Analgesic",
                Dosage = "Tablet",
                Price = 120m,
                StockQuantity = 85,
                Supplier = "MedSupply",
                ExpiryDate = DateTime.Today.AddMonths(10),
                RequiresPrescription = false,
                DiscountPercent = 0m
            },
            new Medicine
            {
                MedicineID = 3,
                MedicineName = "Metformin 850mg",
                Category = "Antidiabetic",
                Dosage = "Tablet",
                Price = 380m,
                StockQuantity = 8,
                Supplier = "HealthLine",
                ExpiryDate = DateTime.Today.AddDays(-5),
                RequiresPrescription = true,
                DiscountPercent = 10m
            }
        };

        public static Customer SampleCustomer() => new Customer
        {
            CustomerID = 1001,
            Name = "Jane Perera",
            Email = "jane.perera@example.com",
            Phone = "0771234567",
            Address = "12 Hospital Road, Colombo",
            IsActive = true
        };

        public static DataTable SalesReportTable()
        {
            var table = new DataTable();
            table.Columns.Add("OrderRef", typeof(string));
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("OrderDate", typeof(string));
            table.Columns.Add("TotalAmount", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Rows.Add("#SM-0001", "Jane Perera", DateTime.Today.AddDays(-2).ToString("yyyy-MM-dd"), "LKR 2,450.00", OrderService.StatusDelivered);
            table.Rows.Add("#SM-0002", "Kamal Silva", DateTime.Today.AddDays(-1).ToString("yyyy-MM-dd"), "LKR 890.00", OrderService.StatusPending);
            return table;
        }

        public static object[] CustomerDashboardOrders() => new object[]
        {
            new { OrderRef = "#SM-0001", OrderDate = DateTime.Today.AddDays(-1), Status = OrderService.StatusPending, Total = "LKR 1,250.00" },
            new { OrderRef = "#SM-0002", OrderDate = DateTime.Today.AddDays(-3), Status = OrderService.StatusDelivered, Total = "LKR 890.00" }
        };

        public static object[] SearchMedicineRows() => new object[]
        {
            new { MedicineID = 1, MedicineName = "Amoxicillin 500mg", Category = "Antibiotic", Price = "LKR 427.50", StockQuantity = 12, Rx = "Yes", Discount = "5%", Promo = "Active" },
            new { MedicineID = 2, MedicineName = "Paracetamol 500mg", Category = "Analgesic", Price = "LKR 120.00", StockQuantity = 85, Rx = "No", Discount = "—", Promo = "—" }
        };

        public static object[] CartRows() => new object[]
        {
            new { MedicineID = 1, MedicineName = "Amoxicillin 500mg", Quantity = 2, ListPrice = "LKR 450.00", UnitPrice = "LKR 427.50", DiscountDisplay = "5%", PromoDisplay = "Active", OfferDisplay = "5% off", Subtotal = "LKR 855.00", Rx = "Yes" },
            new { MedicineID = 2, MedicineName = "Paracetamol 500mg", Quantity = 1, ListPrice = "LKR 120.00", UnitPrice = "LKR 120.00", DiscountDisplay = "—", PromoDisplay = "—", OfferDisplay = "—", Subtotal = "LKR 120.00", Rx = "No" }
        };

        public static object[] TrackOrderRows() => new object[]
        {
            new { OrderID = 1, OrderRef = "#SM-0001", OrderDate = DateTime.Today.AddDays(-1).ToString("MMM dd, yyyy hh:mm tt"), Status = OrderService.StatusPending, Total = "LKR 1,250.00", Prescription = "Uploaded" },
            new { OrderID = 2, OrderRef = "#SM-0002", OrderDate = DateTime.Today.AddDays(-4).ToString("MMM dd, yyyy hh:mm tt"), Status = OrderService.StatusDelivered, Total = "LKR 890.00", Prescription = "—" }
        };

        public static object[] TrackOrderItemRows() => new object[]
        {
            new { MedicineName = "Amoxicillin 500mg", Quantity = 2, UnitPrice = "LKR 427.50", Discount = "5% off", Subtotal = "LKR 855.00" },
            new { MedicineName = "Paracetamol 500mg", Quantity = 1, UnitPrice = "LKR 120.00", Discount = "—", Subtotal = "LKR 120.00" }
        };
    }
}
