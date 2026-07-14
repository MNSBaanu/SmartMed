using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class PrescriptionRepository
    {

        public sealed class PrescriptionAttachment
        {
            public int? MedicineID { get; set; }
            public string FilePath { get; set; }
        }

        public void Insert(int customerId, int orderId, string filePath, int? medicineId = null)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Prescription (CustomerID, OrderID, MedicineID, PrescriptionFile, UploadDate, Status)
                  VALUES (@cid, @oid, @mid, @file, GETDATE(), 'Pending')",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@oid", orderId),
                new SqlParameter("@mid", (object)medicineId ?? DBNull.Value),
                new SqlParameter("@file", filePath));
        }

        public void Insert(
            SqlConnection connection,
            SqlTransaction transaction,
            int customerId,
            int orderId,
            string filePath,
            int? medicineId = null)
        {
            DatabaseHelper.ExecuteNonQuery(
                connection, transaction,
                @"INSERT INTO Prescription (CustomerID, OrderID, MedicineID, PrescriptionFile, UploadDate, Status)
                  VALUES (@cid, @oid, @mid, @file, GETDATE(), 'Pending')",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@oid", orderId),
                new SqlParameter("@mid", (object)medicineId ?? DBNull.Value),
                new SqlParameter("@file", filePath));
        }

        public void InsertMany(
            SqlConnection connection,
            SqlTransaction transaction,
            int customerId,
            int orderId,
            IEnumerable<PrescriptionAttachment> attachments)
        {
            if (attachments == null)
                return;

            foreach (var attachment in attachments)
            {
                if (attachment == null || string.IsNullOrWhiteSpace(attachment.FilePath))
                    continue;

                Insert(connection, transaction, customerId, orderId, attachment.FilePath, attachment.MedicineID);
            }
        }

        public List<Prescription> GetAllByOrderId(int orderId)
        {
            var list = new List<Prescription>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT PrescriptionID, CustomerID, OrderID, MedicineID, PrescriptionFile, UploadDate, Status
                  FROM Prescription WHERE OrderID=@oid
                  ORDER BY PrescriptionID",
                new SqlParameter("@oid", orderId));
            foreach (DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public Prescription GetByOrderId(int orderId)
        {
            var all = GetAllByOrderId(orderId);
            return all.Count == 0 ? null : all[0];
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

        public void UpdateStatusById(int prescriptionId, string status)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Prescription SET Status=@status WHERE PrescriptionID=@id",
                new SqlParameter("@status", status),
                new SqlParameter("@id", prescriptionId));
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
                MedicineID = row.Table.Columns.Contains("MedicineID") && row["MedicineID"] != DBNull.Value
                    ? Convert.ToInt32(row["MedicineID"])
                    : (int?)null,
                PrescriptionFile = row["PrescriptionFile"].ToString(),
                UploadDate = Convert.ToDateTime(row["UploadDate"]),
                Status = row.Table.Columns.Contains("Status") ? row["Status"].ToString() : null
            };
        }
    }
}
