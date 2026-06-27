using System;
using System.Data.SqlClient;
using SmartMedNew.Models;

namespace SmartMedNew.Data
{
    public class AdminRepository
    {
        public Admin GetByCredentials(string username, string password)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT AdminID, Username, Password, Email FROM Admin WHERE Username=@u AND Password=@p",
                new SqlParameter("@u", username),
                new SqlParameter("@p", password));

            if (table.Rows.Count == 0) return null;

            var row = table.Rows[0];
            return new Admin
            {
                AdminID = Convert.ToInt32(row["AdminID"]),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString(),
                Email = row["Email"].ToString(),
                Name = row["Username"].ToString()
            };
        }

        public Admin GetById(int adminId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT AdminID, Username, Password, Email FROM Admin WHERE AdminID=@id",
                new SqlParameter("@id", adminId));
            if (table.Rows.Count == 0) return null;
            var row = table.Rows[0];
            return new Admin
            {
                AdminID = Convert.ToInt32(row["AdminID"]),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString(),
                Email = row["Email"].ToString(),
                Name = row["Username"].ToString()
            };
        }

        public void UpdatePassword(int adminId, string newPassword)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Admin SET Password=@p WHERE AdminID=@id",
                new SqlParameter("@p", newPassword),
                new SqlParameter("@id", adminId));
        }
    }
}
