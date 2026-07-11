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
            },
            new Medicine
            {
                MedicineID = 4,
                MedicineName = "Vitamin C 500mg",
                Category = "Wellness",
                Dosage = "Tablet",
                Price = 250m,
                StockQuantity = 40,
                Supplier = "WellLife",
                ExpiryDate = DateTime.Today.AddMonths(8),
                RequiresPrescription = false,
                DiscountPercent = 5m,
                IsOnPromotion = true,
                PromotionStartDate = DateTime.Today,
                PromotionEndDate = DateTime.Today.AddDays(30)
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
            table.Columns.Add("Order Ref", typeof(string));
            table.Columns.Add("Customer", typeof(string));
            table.Columns.Add("Order Date", typeof(string));
            table.Columns.Add("Total", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Rows.Add("#SM-0001", "Jane Perera", DateTime.Today.AddDays(-2).ToString("MMM dd, yyyy hh:mm tt"), "LKR 2,450.00", OrderService.StatusDelivered);
            table.Rows.Add("#SM-0002", "Kamal Silva", DateTime.Today.AddDays(-1).ToString("MMM dd, yyyy hh:mm tt"), "LKR 890.00", OrderService.StatusDelivered);
            return table;
        }

        public static DataTable InventoryReportTable()
        {
            var table = new DataTable();
            table.Columns.Add("Medicine", typeof(string));
            table.Columns.Add("Category", typeof(string));
            table.Columns.Add("Stock", typeof(string));
            table.Columns.Add("Price", typeof(string));
            table.Columns.Add("Supplier", typeof(string));
            table.Columns.Add("Expiry Date", typeof(string));
            table.Columns.Add("Stock Status", typeof(string));
            table.Columns.Add("Expiry Status", typeof(string));
            table.Columns.Add("Inventory Status", typeof(string));
            table.Rows.Add("Amoxicillin 500mg", "Antibiotic", "12", "LKR 450.00", "PharmaCo", DateTime.Today.AddMonths(2).ToString("yyyy-MM-dd"), "Low Stock", "Current", "Low Stock");
            table.Rows.Add("Metformin 850mg", "Antidiabetic", "8", "LKR 380.00", "HealthLine", DateTime.Today.AddDays(-5).ToString("yyyy-MM-dd"), "Low Stock", "Expired", "Expired");
            return table;
        }

        public static DataTable CustomerHistoryReportTable()
        {
            var table = new DataTable();
            table.Columns.Add("Order Ref", typeof(string));
            table.Columns.Add("Order Date", typeof(string));
            table.Columns.Add("Status", typeof(string));
            table.Columns.Add("Total", typeof(string));
            table.Rows.Add("#SM-0001", DateTime.Today.AddDays(-1).ToString("MMM dd, yyyy hh:mm tt"), OrderService.StatusPending, "LKR 1,250.00");
            table.Rows.Add("#SM-0002", DateTime.Today.AddDays(-3).ToString("MMM dd, yyyy hh:mm tt"), OrderService.StatusDelivered, "LKR 890.00");
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
            new { MedicineID = 2, MedicineName = "Paracetamol 500mg", Category = "Analgesic", Price = "LKR 120.00", StockQuantity = 85, Rx = "No", Discount = "—", Promo = "—" },
            new { MedicineID = 4, MedicineName = "Vitamin C 500mg", Category = "Wellness", Price = "LKR 237.50", StockQuantity = 40, Rx = "No", Discount = "5%", Promo = "Active" }
        };

        public static object[] CartRows() => new object[]
        {
            new { MedicineID = 1, MedicineName = "Amoxicillin 500mg", Quantity = 2, ListPrice = "LKR 450.00", UnitPrice = "LKR 427.50", DiscountDisplay = "5%", PromoDisplay = "Active", Applied = "5% off", Subtotal = "LKR 855.00", Rx = "Yes", Prescription = "Required" },
            new { MedicineID = 2, MedicineName = "Paracetamol 500mg", Quantity = 1, ListPrice = "LKR 120.00", UnitPrice = "LKR 120.00", DiscountDisplay = "—", PromoDisplay = "—", Applied = "—", Subtotal = "LKR 120.00", Rx = "No", Prescription = "—" }
        };

        public static object[] TrackOrderRows() => new object[]
        {
            new { OrderID = 1, OrderRef = "#SM-0001", OrderDate = DateTime.Today.AddDays(-1).ToString("MMM dd, yyyy hh:mm tt"), Status = OrderService.StatusPending, Total = "LKR 1,250.00", Payment = "Card (Paid) — CARD-4242", Prescription = "Uploaded" },
            new { OrderID = 2, OrderRef = "#SM-0002", OrderDate = DateTime.Today.AddDays(-4).ToString("MMM dd, yyyy hh:mm tt"), Status = OrderService.StatusDelivered, Total = "LKR 890.00", Payment = "Cash on Pickup (Pay on Pickup)", Prescription = "—" }
        };

        public static object[] TrackOrderItemRows() => new object[]
        {
            new { MedicineName = "Amoxicillin 500mg", Quantity = 2, UnitPrice = "LKR 427.50", Discount = "5% off", Subtotal = "LKR 855.00" },
            new { MedicineName = "Paracetamol 500mg", Quantity = 1, UnitPrice = "LKR 120.00", Discount = "—", Subtotal = "LKR 120.00" }
        };
    }
}
