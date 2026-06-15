using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using SmartMed.Data.Models;

namespace SmartMed.Data.Repositories
{
    public class CustomerRepository
    {
        public CustomerUser GetByCredentials(string email, string password)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT CustomerID, FullName, Email, Phone, Address, Password FROM Customer WHERE Email=@e AND Password=@p",
                new SqlParameter("@e", email),
                new SqlParameter("@p", password));

            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public CustomerUser GetById(int id)
        {
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT CustomerID, FullName, Email, Phone, Address, Password FROM Customer WHERE CustomerID=@id",
                new SqlParameter("@id", id));
            if (table.Rows.Count == 0) return null;
            return Map(table.Rows[0]);
        }

        public List<CustomerUser> GetAll()
        {
            var list = new List<CustomerUser>();
            var table = DatabaseHelper.ExecuteQuery(
                "SELECT CustomerID, FullName, Email, Phone, Address, Password FROM Customer ORDER BY FullName");
            foreach (System.Data.DataRow row in table.Rows)
                list.Add(Map(row));
            return list;
        }

        public bool EmailExists(string email)
        {
            var result = DatabaseHelper.ExecuteScalar(
                "SELECT COUNT(*) FROM Customer WHERE Email=@e",
                new SqlParameter("@e", email));
            return Convert.ToInt32(result) > 0;
        }

        public int Insert(CustomerUser customer)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"INSERT INTO Customer (FullName, Email, Phone, Address, Password)
                  VALUES (@n, @e, @ph, @a, @pw)",
                new SqlParameter("@n", customer.FullName),
                new SqlParameter("@e", customer.Email),
                new SqlParameter("@ph", customer.Phone),
                new SqlParameter("@a", customer.Address),
                new SqlParameter("@pw", customer.Password));

            return Convert.ToInt32(DatabaseHelper.ExecuteScalar("SELECT MAX(CustomerID) FROM Customer"));
        }

        public void Update(CustomerUser customer)
        {
            DatabaseHelper.ExecuteNonQuery(
                @"UPDATE Customer SET FullName=@n, Email=@e, Phone=@ph, Address=@a
                  WHERE CustomerID=@id",
                new SqlParameter("@n", customer.FullName),
                new SqlParameter("@e", customer.Email),
                new SqlParameter("@ph", customer.Phone),
                new SqlParameter("@a", customer.Address),
                new SqlParameter("@id", customer.CustomerID));
        }

        private static CustomerUser Map(System.Data.DataRow row)
        {
            return new CustomerUser
            {
                CustomerID = Convert.ToInt32(row["CustomerID"]),
                FullName = row["FullName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"].ToString(),
                Address = row["Address"].ToString(),
                Password = row["Password"].ToString()
            };
        }
    }
}
