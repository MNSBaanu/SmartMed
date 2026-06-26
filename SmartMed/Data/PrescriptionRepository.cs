using System;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class PrescriptionRepository
    {
        public void Insert(int customerId, int orderId, string filePath)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Prescription (CustomerID, OrderID, PrescriptionFile, UploadDate, Status)
                  VALUES (@cid, @oid, @file, GETDATE(), 'Pending')",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@oid", orderId),
                new SqlParameter("@file", filePath));
        }

        public Prescription GetByOrderId(int orderId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT PrescriptionID, CustomerID, OrderID, PrescriptionFile, UploadDate, Status
                  FROM Prescription WHERE OrderID=@oid",
                new SqlParameter("@oid", orderId));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public void DeleteByOrderId(int orderId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Prescription WHERE OrderID=@oid",
                new SqlParameter("@oid", orderId));
        }

        public void UpdateStatus(int orderId, string status)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Prescription SET Status=@status WHERE OrderID=@oid",
                new SqlParameter("@status", status),
                new SqlParameter("@oid", orderId));
        }

        public bool HasRecentUpload(int customerId, int withinHours = 24)
        {
            var result = DatabaseHelper.ExecuteScalar(
                @"SELECT COUNT(*) FROM Prescription
                  WHERE CustomerID=@cid AND UploadDate >= DATEADD(HOUR, -@h, GETDATE())",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@h", withinHours));
            return Convert.ToInt32(result) > 0;
        }

        private static Prescription Map(DataRow row)
        {
            return new Prescription
            {
                PrescriptionID = Convert.ToInt32(row["PrescriptionID"]),
                CustomerID = Convert.ToInt32(row["CustomerID"]),
                OrderID = row.Table.Columns.Contains("OrderID") && row["OrderID"] != DBNull.Value
                    ? Convert.ToInt32(row["OrderID"])
                    : 0,
                PrescriptionFile = row["PrescriptionFile"].ToString(),
                UploadDate = Convert.ToDateTime(row["UploadDate"]),
                Status = row.Table.Columns.Contains("Status") ? row["Status"].ToString() : null
            };
        }
    }
}
