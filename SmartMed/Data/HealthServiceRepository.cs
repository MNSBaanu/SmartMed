using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class HealthServiceRepository
    {
        private const string SelectColumns =
            "ServiceID, ServiceName, Description, Price, IsActive";

        public List<HealthService> GetAll()
        {
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM HealthService ORDER BY ServiceName");
            var list = new List<HealthService>();
            foreach (DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public List<HealthService> GetActive()
        {
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM HealthService WHERE IsActive = 1 ORDER BY ServiceName");
            var list = new List<HealthService>();
            foreach (DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public HealthService GetById(int serviceId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM HealthService WHERE ServiceID=@id",
                new SqlParameter("@id", serviceId));
            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }

        public int Insert(HealthService item)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO HealthService (ServiceName, Description, Price, IsActive)
                  VALUES (@name, @desc, @price, @active)",
                new SqlParameter("@name", item.ServiceName),
                new SqlParameter("@desc", (object)item.Description ?? DBNull.Value),
                new SqlParameter("@price", item.Price),
                new SqlParameter("@active", item.IsActive));

            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT CAST(SCOPE_IDENTITY() AS INT)"));
        }

        public void Update(HealthService item)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE HealthService SET ServiceName=@name, Description=@desc, Price=@price, IsActive=@active
                  WHERE ServiceID=@id",
                new SqlParameter("@name", item.ServiceName),
                new SqlParameter("@desc", (object)item.Description ?? DBNull.Value),
                new SqlParameter("@price", item.Price),
                new SqlParameter("@active", item.IsActive),
                new SqlParameter("@id", item.ServiceID));
        }

        public void Delete(int serviceId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM HealthService WHERE ServiceID=@id",
                new SqlParameter("@id", serviceId));
        }

        public bool HasRecords(int serviceId)
        {
            var count = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM HealthServiceRecord WHERE ServiceID=@id",
                new SqlParameter("@id", serviceId));
            return Convert.ToInt32(count) > 0;
        }

        private static HealthService Map(DataRow row)
        {
            return new HealthService
            {
                ServiceID = Convert.ToInt32(row["ServiceID"]),
                ServiceName = row["ServiceName"].ToString(),
                Description = row.Table.Columns.Contains("Description") && row["Description"] != DBNull.Value
                    ? row["Description"].ToString()
                    : null,
                Price = Convert.ToDecimal(row["Price"]),
                IsActive = Convert.ToBoolean(row["IsActive"])
            };
        }
    }
}
