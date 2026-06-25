using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class OrderRepository
    {
        public List<Order> GetAll()
        {
            var list = new List<Order>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  ORDER BY o.OrderDate DESC");
            foreach (DataRow row in table.Rows)
                list.Add(MapOrder(row));
            return list;
        }

        public Order GetById(int orderId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  WHERE o.OrderID=@id",
                new SqlParameter("@id", orderId));
            if (table.Rows.Count == 0) return null;
            return MapOrder(table.Rows[0]);
        }

        public List<Order> GetByCustomer(int customerId)
        {
            var list = new List<Order>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  WHERE o.CustomerID=@cid ORDER BY o.OrderDate DESC",
                new SqlParameter("@cid", customerId));
            foreach (DataRow row in table.Rows)
                list.Add(MapOrder(row));
            return list;
        }

        public List<OrderItem> GetItems(int orderId)
        {
            var list = new List<OrderItem>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT oi.OrderItemID, oi.OrderID, oi.MedicineID, m.MedicineName, oi.Quantity, oi.UnitPrice, oi.Subtotal
                  FROM OrderItem oi INNER JOIN Medicine m ON oi.MedicineID = m.MedicineID
                  WHERE oi.OrderID=@oid",
                new SqlParameter("@oid", orderId));
            foreach (DataRow row in table.Rows)
                list.Add(MapItem(row));
            return list;
        }

        public int CreateOrder(int customerId, List<OrderItem> items)
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

        public void DeleteOrder(int orderId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM OrderItem WHERE OrderID=@id",
                new SqlParameter("@id", orderId));
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM [Order] WHERE OrderID=@id",
                new SqlParameter("@id", orderId));
        }

        public DataTable GetSalesReport() =>
            GetSalesReport(new DateTime(2000, 1, 1), DateTime.MaxValue);

        public DataTable GetSalesReport(DateTime from, DateTime toExclusive)
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, c.FullName AS Customer, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  WHERE o.OrderDate >= @from AND o.OrderDate < @to
                    AND o.Status = 'Delivered'
                  ORDER BY o.OrderDate DESC",
                new SqlParameter("@from", from),
                new SqlParameter("@to", toExclusive));
        }

        public decimal GetOutstandingAmount(DateTime from, DateTime toExclusive)
        {
            var result = DatabaseHelper.ExecuteScalar(
                @"SELECT ISNULL(SUM(TotalAmount), 0)
                  FROM [Order]
                  WHERE OrderDate >= @from AND OrderDate < @to
                    AND Status <> 'Delivered'",
                new SqlParameter("@from", from),
                new SqlParameter("@to", toExclusive));
            return Convert.ToDecimal(result);
        }

        public DataTable GetStockReport()
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT MedicineName, Category, StockQuantity, Price, Supplier, ExpiryDate,
                  CASE WHEN StockQuantity <= 20 THEN 'Low Stock' ELSE 'Current' END AS StockStatus,
                  CASE WHEN ExpiryDate < CAST(GETDATE() AS DATE) THEN 'Expired'
                       WHEN ExpiryDate <= DATEADD(day, 30, CAST(GETDATE() AS DATE)) THEN 'Near Expiry'
                       ELSE 'Current' END AS ExpiryStatus,
                  CASE WHEN ExpiryDate < CAST(GETDATE() AS DATE) THEN 'Expired'
                       WHEN ExpiryDate <= DATEADD(day, 30, CAST(GETDATE() AS DATE)) THEN 'Near Expiry'
                       WHEN StockQuantity <= 20 THEN 'Low Stock'
                       ELSE 'Current' END AS InventoryStatus
                  FROM Medicine
                  ORDER BY
                    CASE
                      WHEN ExpiryDate < CAST(GETDATE() AS DATE) THEN 0
                      WHEN ExpiryDate <= DATEADD(day, 30, CAST(GETDATE() AS DATE)) THEN 1
                      WHEN StockQuantity <= 20 THEN 2
                      ELSE 3
                    END,
                    MedicineName");
        }

        public DataTable GetCustomerOrderHistory(int customerId) =>
            GetCustomerOrderHistory(customerId, new DateTime(2000, 1, 1), DateTime.MaxValue);

        public DataTable GetCustomerOrderHistory(int customerId, DateTime from, DateTime toExclusive)
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.OrderDate, o.Status, o.TotalAmount
                  FROM [Order] o
                  WHERE o.CustomerID=@cid AND o.OrderDate >= @from AND o.OrderDate < @to
                  ORDER BY o.OrderDate DESC",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@from", from),
                new SqlParameter("@to", toExclusive));
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

        private static Order MapOrder(DataRow row)
        {
            return new Order
            {
                OrderID = Convert.ToInt32(row["OrderID"]),
                CustomerID = Convert.ToInt32(row["CustomerID"]),
                CustomerName = row["CustomerName"].ToString(),
                OrderDate = Convert.ToDateTime(row["OrderDate"]),
                Status = row["Status"].ToString(),
                TotalAmount = Convert.ToDecimal(row["TotalAmount"])
            };
        }

        private static OrderItem MapItem(DataRow row)
        {
            return new OrderItem
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
