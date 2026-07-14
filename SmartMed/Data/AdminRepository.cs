using System;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class AdminRepository
    {
        public Admin GetByUsernameOrEmail(string identity)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT AdminID, Username, Password, Email FROM Admin WHERE Username=@i OR Email=@i",
                new SqlParameter("@i", identity));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public Admin GetById(int adminId)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT AdminID, Username, Password, Email FROM Admin WHERE AdminID=@id",
                new SqlParameter("@id", adminId));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public void UpdatePassword(int adminId, string newPassword)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Admin SET Password=@p WHERE AdminID=@id",
                new SqlParameter("@p", newPassword),
                new SqlParameter("@id", adminId));
        }

        private static Admin Map(System.Data.DataRow row)
        {
            return new Admin
            {
                AdminID = Convert.ToInt32(row["AdminID"]),
                Username = row["Username"].ToString(),
                Password = row["Password"].ToString(),
                Email = row["Email"].ToString(),
                Name = row["Username"].ToString()
            };
        }
    }
}
