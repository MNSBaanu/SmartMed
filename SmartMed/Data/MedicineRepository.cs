using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class MedicineRepository
    {
        private const string SelectColumns =
            "MedicineID, MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription, DiscountPercent, IsOnPromotion, PromotionStartDate, PromotionEndDate, Description, ActiveIngredient, UsageInstructions, Warnings, SideEffects, PackSize, IsActive";

        public List<Medicine> GetAll()
        {
            var list = new List<Medicine>();
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Medicine ORDER BY MedicineName");
            foreach (DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public List<Medicine> GetActive()
        {
            var list = new List<Medicine>();
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Medicine WHERE IsActive = 1 ORDER BY MedicineName");
            foreach (DataRow row in table.Rows)
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

        public void Insert(Medicine item)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Medicine (MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription, DiscountPercent, IsOnPromotion, PromotionStartDate, PromotionEndDate, Description, ActiveIngredient, UsageInstructions, Warnings, SideEffects, PackSize, IsActive)
                  VALUES (@n, @c, @d, @p, @s, @su, @e, @r, @disc, @promo, @pStart, @pEnd, @desc, @ai, @usage, @warn, @side, @pack, @active)",
                BuildWriteParameters(item));
        }

        public int Update(Medicine item)
        {
            var parameters = new System.Collections.Generic.List<SqlParameter>(BuildWriteParameters(item))
            {
                new SqlParameter("@id", item.MedicineID)
            };
            return DatabaseHelper.ExecuteNonQuery(
                @"UPDATE Medicine SET MedicineName=@n, Category=@c, Dosage=@d, Price=@p, StockQuantity=@s,
                  Supplier=@su, ExpiryDate=@e, RequiresPrescription=@r, DiscountPercent=@disc, IsOnPromotion=@promo,
                  PromotionStartDate=@pStart, PromotionEndDate=@pEnd, Description=@desc, ActiveIngredient=@ai,
                  UsageInstructions=@usage, Warnings=@warn, SideEffects=@side, PackSize=@pack, IsActive=@active
                  WHERE MedicineID=@id",
                parameters.ToArray());
        }

        private static SqlParameter[] BuildWriteParameters(Medicine item) =>
            new[]
            {
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
                new SqlParameter("@pStart", (object)item.PromotionStartDate ?? DBNull.Value),
                new SqlParameter("@pEnd", (object)item.PromotionEndDate ?? DBNull.Value),
                new SqlParameter("@desc", (object)item.Description ?? DBNull.Value),
                new SqlParameter("@ai", (object)item.ActiveIngredient ?? DBNull.Value),
                new SqlParameter("@usage", (object)item.UsageInstructions ?? DBNull.Value),
                new SqlParameter("@warn", (object)item.Warnings ?? DBNull.Value),
                new SqlParameter("@side", (object)item.SideEffects ?? DBNull.Value),
                new SqlParameter("@pack", (object)item.PackSize ?? DBNull.Value),
                new SqlParameter("@active", item.IsActive)
            };

        public void SetActive(int medicineId, bool isActive)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Medicine SET IsActive=@active WHERE MedicineID=@id",
                new SqlParameter("@active", isActive),
                new SqlParameter("@id", medicineId));
        }

        public void UpdateStock(int medicineId, int quantityChange)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Medicine SET StockQuantity = StockQuantity + @q WHERE MedicineID=@id",
                new SqlParameter("@q", quantityChange),
                new SqlParameter("@id", medicineId));
        }

        public bool ReduceStock(int medicineId, int quantity)
        {
            if (quantity <= 0) return false;
            return DatabaseHelper.ExecuteNonQuery(
                    "UPDATE Medicine SET StockQuantity = StockQuantity - @q WHERE MedicineID=@id AND StockQuantity >= @q",
                    new SqlParameter("@q", quantity),
                    new SqlParameter("@id", medicineId)) > 0;
        }

        public bool ReduceStock(SqlConnection connection, SqlTransaction transaction, int medicineId, int quantity)
        {
            if (quantity <= 0) return false;
            return DatabaseHelper.ExecuteNonQuery(
                    connection, transaction,
                    "UPDATE Medicine SET StockQuantity = StockQuantity - @q WHERE MedicineID=@id AND StockQuantity >= @q",
                    new SqlParameter("@q", quantity),
                    new SqlParameter("@id", medicineId)) > 0;
        }

        public void RestoreStock(int medicineId, int quantity)
        {
            if (quantity <= 0) return;
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Medicine SET StockQuantity = StockQuantity + @q WHERE MedicineID=@id",
                new SqlParameter("@q", quantity),
                new SqlParameter("@id", medicineId));
        }

        public DataTable GetExpiryReport()
        {
            return DatabaseHelper.ExecuteQuery(
                @"SELECT MedicineName, Category, StockQuantity, ExpiryDate,
                  CASE WHEN ExpiryDate < CAST(GETDATE() AS DATE) THEN 'Expired'
                       WHEN ExpiryDate <= DATEADD(day, 30, CAST(GETDATE() AS DATE)) THEN 'Near Expiry'
                       ELSE 'Valid' END AS ExpiryStatus
                  FROM Medicine
                  WHERE IsActive = 1
                    AND ExpiryDate <= DATEADD(day, 30, CAST(GETDATE() AS DATE))
                  ORDER BY ExpiryDate");
        }

        private static Medicine Map(DataRow row)
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
                    && Convert.ToBoolean(row["IsOnPromotion"]),
                PromotionStartDate = ReadNullableDate(row, "PromotionStartDate"),
                PromotionEndDate = ReadNullableDate(row, "PromotionEndDate"),
                Description = ReadOptionalString(row, "Description"),
                ActiveIngredient = ReadOptionalString(row, "ActiveIngredient"),
                UsageInstructions = ReadOptionalString(row, "UsageInstructions"),
                Warnings = ReadOptionalString(row, "Warnings"),
                SideEffects = ReadOptionalString(row, "SideEffects"),
                PackSize = ReadOptionalString(row, "PackSize"),
                IsActive = !row.Table.Columns.Contains("IsActive") || row["IsActive"] == DBNull.Value
                    || Convert.ToBoolean(row["IsActive"])
            };
        }

        private static DateTime? ReadNullableDate(DataRow row, string column)
        {
            if (!row.Table.Columns.Contains(column) || row[column] == DBNull.Value)
                return null;
            return Convert.ToDateTime(row[column]);
        }

        private static string ReadOptionalString(DataRow row, string column)
        {
            if (!row.Table.Columns.Contains(column) || row[column] == DBNull.Value)
                return null;
            var value = row[column].ToString();
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
    }
}
