using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartMed.Models;

namespace SmartMed.Data
{
    public class CustomerRepository
    {
        private const string SelectColumns =
            "CustomerID, FullName, Email, Phone, Address, Password, IsActive";

        public Customer GetByCredentials(string email, string password)
        {
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Customer WHERE Email=@e AND Password=@p",
                new SqlParameter("@e", email),
                new SqlParameter("@p", password));

            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public Customer GetById(int id)
        {
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Customer WHERE CustomerID=@id",
                new SqlParameter("@id", id));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public List<Customer> GetAll()
        {
            var list = new List<Customer>();
            var table = DatabaseHelper.ExecuteQuery(
                $"SELECT {SelectColumns} FROM Customer ORDER BY FullName");
            foreach (System.Data.DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public int GetCount()
        {
            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT COUNT(*) FROM Customer"));
        }

        public bool EmailExists(string email)
        {
            var result = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Customer WHERE Email=@e",
                new SqlParameter("@e", email));
            return Convert.ToInt32(result) > 0;
        }

        public int Insert(Customer customer)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Customer (FullName, Email, Phone, Address, Password, IsActive)
                  VALUES (@n, @e, @ph, @a, @pw, @active)",
                new SqlParameter("@n", customer.Name),
                new SqlParameter("@e", customer.Email),
                new SqlParameter("@ph", customer.Phone),
                new SqlParameter("@a", customer.Address),
                new SqlParameter("@pw", customer.Password),
                new SqlParameter("@active", customer.IsActive));

            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT MAX(CustomerID) FROM Customer"));
        }

        public void Update(Customer customer)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE Customer SET FullName=@n, Email=@e, Phone=@ph, Address=@a
                  WHERE CustomerID=@id",
                new SqlParameter("@n", customer.Name),
                new SqlParameter("@e", customer.Email),
                new SqlParameter("@ph", customer.Phone),
                new SqlParameter("@a", customer.Address),
                new SqlParameter("@id", customer.CustomerID));
        }

        public void Delete(int id)
        {
            DatabaseHelper.ExecuteNonQuery(
                "DELETE FROM Customer WHERE CustomerID=@id",
                new SqlParameter("@id", id));
        }

        public void SetActiveStatus(int customerId, bool isActive)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Customer SET IsActive=@active WHERE CustomerID=@id",
                new SqlParameter("@active", isActive),
                new SqlParameter("@id", customerId));
        }

        public void UpdatePassword(int customerId, string newPassword)
        {
            DatabaseHelper.ExecuteNonQuery(
                "UPDATE Customer SET Password=@p WHERE CustomerID=@id",
                new SqlParameter("@p", newPassword),
                new SqlParameter("@id", customerId));
        }

        private static Customer Map(System.Data.DataRow row)
        {
            return new Customer
            {
                CustomerID = Convert.ToInt32(row["CustomerID"]),
                Name = row["FullName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Password = row["Password"].ToString(),
                IsActive = row.Table.Columns.Contains("IsActive") && row["IsActive"] != DBNull.Value
                    && Convert.ToBoolean(row["IsActive"])
            };
        }
    }
}
