using System;
using System.Data;

namespace SmartMed.Services
{
    internal static class ReportTableFormatter
    {
        public static DataTable FormatSalesReport(DataTable source)
        {
            var table = CreateTable(
                ("Order Ref", typeof(string)),
                ("Customer", typeof(string)),
                ("Order Date", typeof(string)),
                ("Total", typeof(string)),
                ("Status", typeof(string)));

            if (source == null) return table;

            foreach (DataRow row in source.Rows)
            {
                var orderId = Convert.ToInt32(row["OrderID"]);
                table.Rows.Add(
                    $"#SM-{orderId:D4}",
                    row["Customer"]?.ToString(),
                    FormatDateTime(row["OrderDate"]),
                    FormatCurrency(row["TotalAmount"]),
                    row["Status"]?.ToString());
            }

            return table;
        }

        public static DataTable FormatCustomerOrderHistory(DataTable source)
        {
            var table = CreateTable(
                ("Order Ref", typeof(string)),
                ("Order Date", typeof(string)),
                ("Status", typeof(string)),
                ("Total", typeof(string)));

            if (source == null) return table;

            foreach (DataRow row in source.Rows)
            {
                var orderId = Convert.ToInt32(row["OrderID"]);
                table.Rows.Add(
                    $"#SM-{orderId:D4}",
                    FormatDateTime(row["OrderDate"]),
                    row["Status"]?.ToString(),
                    FormatCurrency(row["TotalAmount"]));
            }

            return table;
        }

        public static DataTable FormatStockReport(DataTable source)
        {
            var table = CreateTable(
                ("Medicine", typeof(string)),
                ("Category", typeof(string)),
                ("Stock", typeof(string)),
                ("Price", typeof(string)),
                ("Supplier", typeof(string)),
                ("Expiry Date", typeof(string)),
                ("Stock Status", typeof(string)),
                ("Expiry Status", typeof(string)),
                ("Inventory Status", typeof(string)));

            if (source == null) return table;

            foreach (DataRow row in source.Rows)
            {
                table.Rows.Add(
                    row["MedicineName"]?.ToString(),
                    row["Category"]?.ToString(),
                    Convert.ToInt32(row["StockQuantity"]).ToString("N0"),
                    FormatCurrency(row["Price"]),
                    row["Supplier"]?.ToString(),
                    FormatDate(row["ExpiryDate"]),
                    row["StockStatus"]?.ToString(),
                    row["ExpiryStatus"]?.ToString(),
                    row["InventoryStatus"]?.ToString());
            }

            return table;
        }

        public static decimal SumAmountColumn(DataTable source, string columnName = "TotalAmount")
        {
            if (source == null || !source.Columns.Contains(columnName)) return 0m;
            decimal total = 0;
            foreach (DataRow row in source.Rows)
                total += Convert.ToDecimal(row[columnName]);
            return total;
        }

        public static int CountColumnValue(DataTable table, string column, string value)
        {
            if (table == null || !table.Columns.Contains(column)) return 0;
            var count = 0;
            foreach (DataRow row in table.Rows)
            {
                if (string.Equals(row[column]?.ToString(), value, StringComparison.OrdinalIgnoreCase))
                    count++;
            }
            return count;
        }

        private static DataTable CreateTable(params (string Name, Type Type)[] columns)
        {
            var table = new DataTable();
            foreach (var column in columns)
                table.Columns.Add(column.Name, column.Type);
            return table;
        }

        private static string FormatCurrency(object value) =>
            $"LKR {Convert.ToDecimal(value):N2}";

        private static string FormatDateTime(object value) =>
            Convert.ToDateTime(value).ToString("MMM dd, yyyy hh:mm tt");

        private static string FormatDate(object value) =>
            Convert.ToDateTime(value).ToString("yyyy-MM-dd");
    }
}
