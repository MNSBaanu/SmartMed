using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartMed.Data.Models;

namespace SmartMed.Data.Repositories
{
    public class MedicineRepository
    {
        public List<MedicineItem> GetAll()
        {
            var list = new List<MedicineItem>();
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT MedicineID, MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription FROM Medicine ORDER BY MedicineName");
            foreach (System.Data.DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public MedicineItem GetById(int id)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT MedicineID, MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription FROM Medicine WHERE MedicineID=@id",
                new SqlParameter("@id", id));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public void Insert(MedicineItem item)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Medicine (MedicineName, Category, Dosage, Price, StockQuantity, Supplier, ExpiryDate, RequiresPrescription)
                  VALUES (@n, @c, @d, @p, @s, @su, @e, @r)",
                new SqlParameter("@n", item.MedicineName),
                new SqlParameter("@c", item.Category),
                new SqlParameter("@d", item.Dosage),
                new SqlParameter("@p", item.Price),
                new SqlParameter("@s", item.StockQuantity),
                new SqlParameter("@su", item.Supplier),
                new SqlParameter("@e", item.ExpiryDate),
                new SqlParameter("@r", item.RequiresPrescription));
        }

        public void Update(MedicineItem item)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE Medicine SET MedicineName=@n, Category=@c, Dosage=@d, Price=@p, StockQuantity=@s,
                  Supplier=@su, ExpiryDate=@e, RequiresPrescription=@r WHERE MedicineID=@id",
                new SqlParameter("@n", item.MedicineName),
                new SqlParameter("@c", item.Category),
                new SqlParameter("@d", item.Dosage),
                new SqlParameter("@p", item.Price),
                new SqlParameter("@s", item.StockQuantity),
                new SqlParameter("@su", item.Supplier),
                new SqlParameter("@e", item.ExpiryDate),
                new SqlParameter("@r", item.RequiresPrescription),
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

        private static MedicineItem Map(System.Data.DataRow row)
        {
            return new MedicineItem
            {
                MedicineID = Convert.ToInt32(row["MedicineID"]),
                MedicineName = row["MedicineName"].ToString(),
                Category = row["Category"].ToString(),
                Dosage = row["Dosage"].ToString(),
                Price = Convert.ToDecimal(row["Price"]),
                StockQuantity = Convert.ToInt32(row["StockQuantity"]),
                Supplier = row["Supplier"].ToString(),
                ExpiryDate = Convert.ToDateTime(row["ExpiryDate"]),
                RequiresPrescription = Convert.ToBoolean(row["RequiresPrescription"])
            };
        }
    }
}
