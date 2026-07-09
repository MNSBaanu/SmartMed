using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SmartMed.Data
{
    public sealed class CartRepository
    {
        public sealed class StoredCartLine
        {
            public int MedicineID { get; set; }
            public int Quantity { get; set; }
            public string PrescriptionPath { get; set; }
        }

        public List<StoredCartLine> GetByCustomer(int customerId)
        {
            var list = new List<StoredCartLine>();
            var table = DatabaseHelper.ExecuteQuery(
                @"SELECT MedicineID, Quantity, PrescriptionPath
                  FROM CartItem
                  WHERE CustomerID = @cid
                  ORDER BY AddedAt",
                new SqlParameter("@cid", customerId));

            foreach (DataRow row in table.Rows)
            {
                list.Add(new StoredCartLine
                {
                    MedicineID = Convert.ToInt32(row["MedicineID"]),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    PrescriptionPath = row["PrescriptionPath"] == DBNull.Value
                        ? null
                        : row["PrescriptionPath"].ToString()
                });
            }

            return list;
        }

        public void SaveAll(int customerId, IReadOnlyList<StoredCartLine> lines)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM CartItem WHERE CustomerID = @cid",
                new SqlParameter("@cid", customerId));

            if (lines == null || lines.Count == 0)
                return;

            foreach (var line in lines)
            {
                DatabaseHelper.ExecuteNonQuery(
                    @"INSERT INTO CartItem (CustomerID, MedicineID, Quantity, PrescriptionPath)
                      VALUES (@cid, @mid, @qty, @rx)",
                    new SqlParameter("@cid", customerId),
                    new SqlParameter("@mid", line.MedicineID),
                    new SqlParameter("@qty", line.Quantity),
                    new SqlParameter("@rx", (object)line.PrescriptionPath ?? DBNull.Value));
            }
        }

        public void Clear(int customerId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM CartItem WHERE CustomerID = @cid",
                new SqlParameter("@cid", customerId));
        }
    }
}
