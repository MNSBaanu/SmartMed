using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.Data
{
    public class OrderRepository
    {
        public List<Order> GetAll()
        {
            var list = new List<Order>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount,
                         o.PaymentMethod, o.PaymentStatus, o.PaymentReference, o.CancellationReason
                  FROM [Order] o INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                  ORDER BY o.OrderDate DESC");
            foreach (DataRow row in table.Rows)
                list.Add(MapOrder(row));
            return list;
        }

        public Order GetById(int orderId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount,
                         o.PaymentMethod, o.PaymentStatus, o.PaymentReference, o.CancellationReason
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
                @"SELECT o.OrderID, o.CustomerID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount,
                         o.PaymentMethod, o.PaymentStatus, o.PaymentReference, o.CancellationReason
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
                @"SELECT oi.OrderItemID, oi.OrderID, oi.MedicineID, m.MedicineName, oi.Quantity, oi.UnitPrice, oi.Subtotal,
                         m.Price AS ListPrice, m.DiscountPercent, m.RequiresPrescription
                  FROM OrderItem oi INNER JOIN Medicine m ON oi.MedicineID = m.MedicineID
                  WHERE oi.OrderID=@oid",
                new SqlParameter("@oid", orderId));
            foreach (DataRow row in table.Rows)
                list.Add(MapItem(row));
            return list;
        }

        public int CreateOrder(
            int customerId,
            List<OrderItem> items,
            string paymentMethod,
            string paymentStatus,
            string paymentReference,
            IList<PrescriptionRepository.PrescriptionAttachment> prescriptions = null,
            int? rxCustomerId = null)
        {
            // Build the order total from each line before saving.
            decimal total = 0;
            foreach (var item in items)
                total += item.Subtotal;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        var prefParam = new SqlParameter("@pref", (object)paymentReference ?? DBNull.Value);
                        int orderId = Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                            conn, tx,
                            @"INSERT INTO [Order] (CustomerID, OrderDate, Status, TotalAmount, PaymentMethod, PaymentStatus, PaymentReference)
                              OUTPUT INSERTED.OrderID
                              VALUES (@cid, GETDATE(), 'Pending', @total, @pm, @ps, @pref)",
                            new SqlParameter("@cid", customerId),
                            new SqlParameter("@total", total),
                            new SqlParameter("@pm", paymentMethod),
                            new SqlParameter("@ps", paymentStatus),
                            prefParam));

                        var medicines = new MedicineRepository();
                        foreach (var item in items)
                        {
                            DatabaseHelper.ExecuteNonQuery(
                                conn, tx,
                                @"INSERT INTO OrderItem (OrderID, MedicineID, Quantity, UnitPrice, Subtotal)
                                  VALUES (@oid, @mid, @q, @u, @s)",
                                new SqlParameter("@oid", orderId),
                                new SqlParameter("@mid", item.MedicineID),
                                new SqlParameter("@q", item.Quantity),
                                new SqlParameter("@u", item.UnitPrice),
                                new SqlParameter("@s", item.Subtotal));

                            // Reduce stock when the order is confirmed so items are reserved.
                            if (!medicines.ReduceStock(conn, tx, item.MedicineID, item.Quantity))
                                throw new InvalidOperationException(
                                    $"Insufficient stock for medicine ID {item.MedicineID}.");
                        }

                        if (prescriptions != null && prescriptions.Count > 0)
                        {
                            // Attach prescriptions linked to this order for pharmacy review.
                            int prescriptionCustomerId = rxCustomerId ?? customerId;
                            new PrescriptionRepository().InsertMany(
                                conn, tx, prescriptionCustomerId, orderId, prescriptions);
                        }

                        tx.Commit();
                        return orderId;
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch {  }
                        throw;
                    }
                }
            }
        }

        public int CreateOrder(
            int customerId,
            List<OrderItem> items,
            string paymentMethod,
            string paymentStatus,
            string paymentReference,
            string prescriptionFilePath,
            int? rxCustomerId = null)
        {
            IList<PrescriptionRepository.PrescriptionAttachment> prescriptions = null;
            if (!string.IsNullOrWhiteSpace(prescriptionFilePath))
            {
                prescriptions = new List<PrescriptionRepository.PrescriptionAttachment>
                {
                    new PrescriptionRepository.PrescriptionAttachment
                    {
                        MedicineID = null,
                        FilePath = prescriptionFilePath
                    }
                };
            }

            return CreateOrder(
                customerId, items, paymentMethod, paymentStatus, paymentReference,
                prescriptions, rxCustomerId);
        }

        public void UpdateStatus(int orderId, string status)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE [Order] SET Status=@s WHERE OrderID=@id",
                new SqlParameter("@s", status),
                new SqlParameter("@id", orderId));
        }

        public void MarkCancelled(int orderId, string reason)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE [Order]
                  SET Status=@s, CancellationReason=@r
                  WHERE OrderID=@id",
                new SqlParameter("@s", "Cancelled"),
                new SqlParameter("@r", reason),
                new SqlParameter("@id", orderId));
        }

        public void DeleteOrder(int orderId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Prescription WHERE OrderID=@id",
                new SqlParameter("@id", orderId));
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
            // Sales report includes only completed (delivered) orders in the selected period.
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
            // Outstanding value is the total of orders not yet delivered or cancelled.
            var result = DatabaseHelper.ExecuteScalar(
                @"SELECT ISNULL(SUM(TotalAmount), 0)
                  FROM [Order]
                  WHERE OrderDate >= @from AND OrderDate < @to
                    AND Status NOT IN ('Delivered', 'Cancelled')",
                new SqlParameter("@from", from),
                new SqlParameter("@to", toExclusive));
            return Convert.ToDecimal(result);
        }

        public DataTable GetStockReport() =>
            GetStockReport(DateTime.Today);

        public DataTable GetStockReport(DateTime asOfDate)
        {
            // Inventory flags low stock / near-expiry relative to the report period end date.
            var asOf = asOfDate.Date;
            return DatabaseHelper.ExecuteQuery(
                @"SELECT MedicineName, Category, StockQuantity, Price, Supplier, ExpiryDate,
                  CASE WHEN StockQuantity <= 20 THEN 'Low Stock' ELSE 'Current' END AS StockStatus,
                  CASE WHEN ExpiryDate < @asOf THEN 'Expired'
                       WHEN ExpiryDate <= DATEADD(day, 30, @asOf) THEN 'Near Expiry'
                       ELSE 'Current' END AS ExpiryStatus,
                  CASE WHEN ExpiryDate < @asOf THEN 'Expired'
                       WHEN ExpiryDate <= DATEADD(day, 30, @asOf) THEN 'Near Expiry'
                       WHEN StockQuantity <= 20 THEN 'Low Stock'
                       ELSE 'Current' END AS InventoryStatus
                  FROM Medicine
                  ORDER BY
                    CASE
                      WHEN ExpiryDate < @asOf THEN 0
                      WHEN ExpiryDate <= DATEADD(day, 30, @asOf) THEN 1
                      WHEN StockQuantity <= 20 THEN 2
                      ELSE 3
                    END,
                    MedicineName",
                new SqlParameter("@asOf", asOf));
        }

        public DataTable GetCustomerOrderHistory(int customerId) =>
            GetCustomerOrderHistory(customerId, new DateTime(2000, 1, 1), DateTime.MaxValue);

        public DataTable GetCustomerOrderHistory(int customerId, DateTime from, DateTime toExclusive)
        {
            if (customerId <= 0)
            {
                return DatabaseHelper.ExecuteQuery(
                    @"SELECT o.OrderID, c.FullName AS CustomerName, o.OrderDate, o.Status, o.TotalAmount, o.CancellationReason
                      FROM [Order] o
                      INNER JOIN Customer c ON o.CustomerID = c.CustomerID
                      WHERE o.OrderDate >= @from AND o.OrderDate < @to
                      ORDER BY o.OrderDate DESC",
                    new SqlParameter("@from", from),
                    new SqlParameter("@to", toExclusive));
            }

            return DatabaseHelper.ExecuteQuery(
                @"SELECT o.OrderID, o.OrderDate, o.Status, o.TotalAmount, o.CancellationReason
                  FROM [Order] o
                  WHERE o.CustomerID=@cid AND o.OrderDate >= @from AND o.OrderDate < @to
                  ORDER BY o.OrderDate DESC",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@from", from),
                new SqlParameter("@to", toExclusive));
        }

        public decimal GetTotalSales()
        {
            var result = DatabaseHelper.ExecuteScalar(
                "SELECT ISNULL(SUM(TotalAmount), 0) FROM [Order] WHERE Status = 'Delivered'");
            return Convert.ToDecimal(result);
        }

        public int GetActiveOrderCount()
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM [Order] WHERE Status NOT IN ('Delivered', 'Cancelled')"));
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
                TotalAmount = Convert.ToDecimal(row["TotalAmount"]),
                PaymentMethod = row.Table.Columns.Contains("PaymentMethod") && row["PaymentMethod"] != DBNull.Value
                    ? row["PaymentMethod"].ToString()
                    : PaymentService.MethodCashOnPickup,
                PaymentStatus = row.Table.Columns.Contains("PaymentStatus") && row["PaymentStatus"] != DBNull.Value
                    ? row["PaymentStatus"].ToString()
                    : PaymentService.StatusPayOnPickup,
                PaymentReference = row.Table.Columns.Contains("PaymentReference") && row["PaymentReference"] != DBNull.Value
                    ? row["PaymentReference"].ToString()
                    : null,
                CancellationReason = row.Table.Columns.Contains("CancellationReason") && row["CancellationReason"] != DBNull.Value
                    ? row["CancellationReason"].ToString()
                    : null
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
                Subtotal = Convert.ToDecimal(row["Subtotal"]),
                ListPrice = row.Table.Columns.Contains("ListPrice") && row["ListPrice"] != DBNull.Value
                    ? Convert.ToDecimal(row["ListPrice"])
                    : 0m,
                DiscountPercent = row.Table.Columns.Contains("DiscountPercent") && row["DiscountPercent"] != DBNull.Value
                    ? Convert.ToDecimal(row["DiscountPercent"])
                    : 0m,
                RequiresPrescription = row.Table.Columns.Contains("RequiresPrescription")
                    && row["RequiresPrescription"] != DBNull.Value
                    && Convert.ToBoolean(row["RequiresPrescription"])
            };
        }
    }
}
