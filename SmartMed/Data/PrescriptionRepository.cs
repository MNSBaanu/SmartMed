using System;
using System.Data.SqlClient;

namespace SmartMed.Data
{
    public class PrescriptionRepository
    {
        public void Insert(int customerId, string filePath)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Prescription (CustomerID, PrescriptionFile, UploadDate, Status)
                  VALUES (@cid, @file, GETDATE(), 'Pending')",
                new SqlParameter("@cid", customerId),
                new SqlParameter("@file", filePath));
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
    }
}
