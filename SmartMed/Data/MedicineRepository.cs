using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class MedicineRepository
    {
        private const string SelectColumns =
            "MedicineID, MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription, DiscountPercent, IsOnPromotion";

        public List<Medicine> GetAll()
        {
            var list = new List<Medicine>();
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Medicine ORDER BY MedicineName");
            foreach (System.Data.DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public Medicine GetById(int id)
        {
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Medicine WHERE MedicineID=@id",
                new SqlParameter("@id", id));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public bool NameExists(string name, int? excludeMedicineId = null)
        {
            var sql = excludeMedicineId.HasValue
                ? "SELECT COUNT(*) FROM Medicine WHERE LOWER(MedicineName) = LOWER(@n) AND MedicineID <> @id"
                : "SELECT COUNT(*) FROM Medicine WHERE LOWER(MedicineName) = LOWER(@n)";
            var parameters = excludeMedicineId.HasValue
                ? new[] { new SqlParameter("@n", name), new SqlParameter("@id", excludeMedicineId.Value) }
                : new[] { new SqlParameter("@n", name) };
            var result = DatabaseHelper.ExecuteScalar(sql, parameters);
            return Convert.ToInt32(result) > 0;
        }

        public bool HasActiveOrPendingOrderItems(int medicineId)
        {
            var result = DatabaseHelper.ExecuteScalar(
                @"SELECT COUNT(*) FROM OrderItem oi
                  INNER JOIN [Order] o ON o.OrderID = oi.OrderID
                  WHERE oi.MedicineID = @id AND o.Status IN ('Pending', 'Ready for Pickup')",
                new SqlParameter("@id", medicineId));
            return Convert.ToInt32(result) > 0;
        }

        public void RemoveDeliveredOrderItemReferences(int medicineId)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"DELETE oi FROM OrderItem oi
                  INNER JOIN [Order] o ON o.OrderID = oi.OrderID
                  WHERE oi.MedicineID = @id AND o.Status = 'Delivered'",
                new SqlParameter("@id", medicineId));
        }

        public void Insert(Medicine item)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Medicine (MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription, DiscountPercent, IsOnPromotion)
                  VALUES (@n, @c, @d, @p, @s, @su, @e, @r, @disc, @promo)",
                new SqlParameter("@n", item.MedicineName),
                new SqlParameter("@c", item.Category),
                new SqlParameter("@d", item.Dosage),
                new SqlParameter("@p", item.Price),
                new SqlParameter("@s", item.StockQuantity),
                new SqlParameter("@su", item.Supplier),
                new SqlParameter("@e", item.ExpiryDate),
                new SqlParameter("@r", item.RequiresPrescription),
                new SqlParameter("@disc", item.DiscountPercent),
                new SqlParameter("@promo", item.IsOnPromotion));
        }

        public int Update(Medicine item)
        {
            return DatabaseHelper.ExecuteNonQuery(
                @"UPDATE Medicine SET MedicineName=@n, Category=@c, Dosage=@d, Price=@p, StockQuantity=@s,
                  Supplier=@su, ExpiryDate=@e, RequiresPrescription=@r, DiscountPercent=@disc, IsOnPromotion=@promo
                  WHERE MedicineID=@id",
                new SqlParameter("@n", item.MedicineName),
                new SqlParameter("@c", item.Category),
                new SqlParameter("@d", item.Dosage),
                new SqlParameter("@p", item.Price),
                new SqlParameter("@s", item.StockQuantity),
                new SqlParameter("@su", item.Supplier),
                new SqlParameter("@e", item.ExpiryDate),
                new SqlParameter("@r", item.RequiresPrescription),
                new SqlParameter("@disc", item.DiscountPercent),
                new SqlParameter("@promo", item.IsOnPromotion),
                new SqlParameter("@id", item.MedicineID));
        }

        public void Delete(int id)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Medicine WHERE MedicineID=@id",
                new SqlParameter("@id", id));
        }

        public void UpdateStock(int medicineId, int quantityChange)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Medicine SET StockQuantity = StockQuantity + @q WHERE MedicineID=@id",
                new SqlParameter("@q", quantityChange),
                new SqlParameter("@id", medicineId));
        }

        private static Medicine Map(System.Data.DataRow row)
        {
            return new Medicine
            {
                MedicineID = Convert.ToInt32(row["MedicineID"]),
                MedicineName = row["MedicineName"].ToString(),
                Category = row["Category"].ToString(),
                Dosage = row["Dosage"].ToString(),
                Price = Convert.ToDecimal(row["Price"]),
                StockQuantity = Convert.ToInt32(row["StockQuantity"]),
                Supplier = row["Supplier"].ToString(),
                ExpiryDate = Convert.ToDateTime(row["ExpiryDate"]),
                RequiresPrescription = Convert.ToBoolean(row["RequiresPrescription"]),
                DiscountPercent = row.Table.Columns.Contains("DiscountPercent")
                    ? Convert.ToDecimal(row["DiscountPercent"])
                    : 0m,
                IsOnPromotion = row.Table.Columns.Contains("IsOnPromotion")
                    && Convert.ToBoolean(row["IsOnPromotion"])
            };
        }
    }
}
