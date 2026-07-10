using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class HealthServiceRecordRepository
    {
        private const string SelectJoin =
            @"SELECT r.RecordID, r.ServiceID, r.CustomerID, r.AdminID, r.ServiceDate, r.Result, r.PharmacistNotes, r.CreatedAt,
                     s.ServiceName, c.FullName AS CustomerName, a.Username AS PharmacistName
              FROM HealthServiceRecord r
              INNER JOIN HealthService s ON s.ServiceID = r.ServiceID
              INNER JOIN Customer c ON c.CustomerID = r.CustomerID
              LEFT JOIN Admin a ON a.AdminID = r.AdminID";

        public List<HealthServiceRecord> GetAll()
        {
            var table = DatabaseHelper.ExecuteQuery(SelectJoin + " ORDER BY r.ServiceDate DESC, r.RecordID DESC");
            return MapList(table);
        }

        public List<HealthServiceRecord> GetByCustomerId(int customerId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                SelectJoin + " WHERE r.CustomerID=@cid ORDER BY r.ServiceDate DESC, r.RecordID DESC",
                new SqlParameter("@cid", customerId));
            return MapList(table);
        }

        public HealthServiceRecord GetById(int recordId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                SelectJoin + " WHERE r.RecordID=@id",
                new SqlParameter("@id", recordId));
            return table.Rows.Count == 0 ? null : Map(table.Rows[0]);
        }

        public int Insert(HealthServiceRecord record)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO HealthServiceRecord (ServiceID, CustomerID, AdminID, ServiceDate, Result, PharmacistNotes, CreatedAt)
                  VALUES (@sid, @cid, @aid, @date, @result, @notes, GETDATE())",
                new SqlParameter("@sid", record.ServiceID),
                new SqlParameter("@cid", record.CustomerID),
                new SqlParameter("@aid", (object)record.AdminID ?? DBNull.Value),
                new SqlParameter("@date", record.ServiceDate),
                new SqlParameter("@result", record.Result),
                new SqlParameter("@notes", (object)record.PharmacistNotes ?? DBNull.Value));

            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT CAST(SCOPE_IDENTITY() AS INT)"));
        }

        public void Update(HealthServiceRecord record)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE HealthServiceRecord SET ServiceID=@sid, CustomerID=@cid, AdminID=@aid,
                  ServiceDate=@date, Result=@result, PharmacistNotes=@notes
                  WHERE RecordID=@id",
                new SqlParameter("@sid", record.ServiceID),
                new SqlParameter("@cid", record.CustomerID),
                new SqlParameter("@aid", (object)record.AdminID ?? DBNull.Value),
                new SqlParameter("@date", record.ServiceDate),
                new SqlParameter("@result", record.Result),
                new SqlParameter("@notes", (object)record.PharmacistNotes ?? DBNull.Value),
                new SqlParameter("@id", record.RecordID));
        }

        public void Delete(int recordId)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM HealthServiceRecord WHERE RecordID=@id",
                new SqlParameter("@id", recordId));
        }

        public DataTable GetReport(DateTime from, DateTime toExclusive, int? customerId = null)
        {
            var sql = SelectJoin + " WHERE r.ServiceDate >= @from AND r.ServiceDate < @to";
            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@from", from),
                new SqlParameter("@to", toExclusive)
            };

            if (customerId.HasValue)
            {
                sql += " AND r.CustomerID=@cid";
                parameters.Add(new SqlParameter("@cid", customerId.Value));
            }

            sql += " ORDER BY r.ServiceDate DESC, r.RecordID DESC";
            return DatabaseHelper.ExecuteQuery(sql, parameters.ToArray());
        }

        private static List<HealthServiceRecord> MapList(DataTable table)
        {
            var list = new List<HealthServiceRecord>();
            foreach (DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        private static HealthServiceRecord Map(DataRow row)
        {
            return new HealthServiceRecord
            {
                RecordID = Convert.ToInt32(row["RecordID"]),
                ServiceID = Convert.ToInt32(row["ServiceID"]),
                CustomerID = Convert.ToInt32(row["CustomerID"]),
                AdminID = row["AdminID"] != DBNull.Value ? (int?)Convert.ToInt32(row["AdminID"]) : null,
                ServiceDate = Convert.ToDateTime(row["ServiceDate"]),
                Result = row["Result"].ToString(),
                PharmacistNotes = row.Table.Columns.Contains("PharmacistNotes") && row["PharmacistNotes"] != DBNull.Value
                    ? row["PharmacistNotes"].ToString()
                    : null,
                CreatedAt = Convert.ToDateTime(row["CreatedAt"]),
                ServiceName = row["ServiceName"].ToString(),
                CustomerName = row["CustomerName"].ToString(),
                PharmacistName = row.Table.Columns.Contains("PharmacistName") && row["PharmacistName"] != DBNull.Value
                    ? row["PharmacistName"].ToString()
                    : null
            };
        }
    }
}
