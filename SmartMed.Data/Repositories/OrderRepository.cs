using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Data.Models;

namespace SmartMed.Data.Repositories
{
    public class OrderRepository
    {
        public List<OrderRecord> GetAll()
        {
            var list = new List<OrderRecord>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  ORDER BY o.OrderDate DESC");
            foreach (DataRow row in table.Rows)
                list.Add(MapOrder(row));
            return list;
        }

        public List<OrderRecord> GetByCustomer(int customerId)
        {
            var list = new List<OrderRecord>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  WHERE o.CustomerID=@cid ORDER BY o.OrderDate DESC",
                new SqlParameter("@cid", customerId));
            foreach (DataRow row in table.Rows)
                list.Add(MapOrder(row));
            return list;
        }

        public List<OrderItemRecord> GetItems(int orderId)
        {
            var list = new List<OrderItemRecord>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT oi.OrderItemID, oi.OrderID, oi.MedicineID, m.MedicineName, oi.Quantity, oi.UnitPrice, oi.Subtotal
                  FROM OrderItem oi INNER JOIN Medicine m ON oi.MedicineID = m.MedicineID
                  WHERE oi.OrderID=@oid",
                new SqlParameter("@oid", orderId));
            foreach (DataRow row in table.Rows)
                list.Add(MapItem(row));
            return list;
        }

        public int CreateOrder(int customerId, List<OrderItemRecord> items)
        {
            decimal total = 0;
            foreach (var item in items)
                total += item.Subtotal;

            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO [Order] (CustomerID, OrderDate, Status, TotalAmount)
                  VALUES (@cid, GETDATE(), 'Pending', @total)",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@total", total));

            int orderId = Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT MAX(OrderID) FROM [Order]"));

            foreach (var item in items)
            {
                DatabaseHelper.ExecuteNonQuery(
                    @"INSERT INTO OrderItem (OrderID, MedicineID, Quantity, UnitPrice, Subtotal)
                      VALUES (@oid, @mid, @q, @u, @s)",
                    new SqlParameter("@oid", orderId),
                    new SqlParameter("@mid", item.MedicineID),
                    new SqlParameter("@q", item.Quantity),
                    new SqlParameter("@u", item.UnitPrice),
                    new SqlParameter("@s", item.Subtotal));
            }

            return orderId;
        }

        public void UpdateStatus(int orderId, string status)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE [Order] SET Status=@s WHERE OrderID=@id",
                new SqlParameter("@s", status),
                new SqlParameter("@id", orderId));
        }

        public DataTable GetSalesReport()
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, c.FullName AS Customer, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  ORDER BY o.OrderDate DESC");
        }

        public DataTable GetStockReport()
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT MedicineName, Category, StockQuantity, Price, Supplier, ExpiryDate
                  FROM Medicine ORDER BY MedicineName");
        }

        public DataTable GetCustomerOrderHistory(int customerId)
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o WHERE o.CustomerID=@cid ORDER BY o.OrderDate DESC",
                new SqlParameter("@cid", customerId));
        }

        public decimal GetTotalSales()
        {
            var result = DatabaseHelper.ExecuteScalar("SELECT ISNULL(SUM(TotalAmount), 0) FROM [Order]");
            return Convert.ToDecimal(result);
        }

        public int GetActiveOrderCount()
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM [Order] WHERE Status <> 'Delivered'"));
        }

        private static OrderRecord MapOrder(DataRow row)
        {
            return new OrderRecord
            {
                OrderID = Convert.ToInt32(row["OrderID"]),
                CustomerID = Convert.ToInt32(row["CustomerID"]),
                CustomerName = row["CustomerName"].ToString(),
                OrderDate = Convert.ToDateTime(row["OrderDate"]),
                Status = row["Status"].ToString(),
                TotalAmount = Convert.ToDecimal(row["TotalAmount"])
            };
        }

        private static OrderItemRecord MapItem(DataRow row)
        {
            return new OrderItemRecord
            {
                OrderItemID = Convert.ToInt32(row["OrderItemID"]),
                OrderID = Convert.ToInt32(row["OrderID"]),
                MedicineID = Convert.ToInt32(row["MedicineID"]),
                MedicineName = row["MedicineName"].ToString(),
                Quantity = Convert.ToInt32(row["Quantity"]),
                UnitPrice = Convert.ToDecimal(row["UnitPrice"]),
                Subtotal = Convert.ToDecimal(row["Subtotal"])
            };
        }
    }
}
