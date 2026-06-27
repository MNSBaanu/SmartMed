using System;
using System.Collections.Generic;
using System.Linq;
using SmartMedNew.Data;
using SmartMedNew.Models;

namespace SmartMedNew.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customers = new CustomerRepository();
        private readonly OrderRepository _orders = new OrderRepository();

        public List<Customer> GetAll() => _customers.GetAll();

        public int GetRegisteredCount() => _customers.GetCount();

        public Customer GetById(int id) => _customers.GetById(id);

        public List<Customer> Search(string keyword) =>
            SearchService.SearchCustomers(GetAll(), keyword);

        public void UpdateProfile(Customer customer)
        {
            Update(customer);
        }

        public void ChangePassword(int customerId, string currentPassword, string newPassword)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer session is required.");
            if (ValidationService.IsNullOrWhiteSpace(currentPassword) || ValidationService.IsNullOrWhiteSpace(newPassword))
                throw new ArgumentException("Current and new passwords are required.");
            if (newPassword.Length < 6)
                throw new ArgumentException("New password must be at least 6 characters.");

            var customer = _customers.GetById(customerId);
            if (customer == null || customer.Password != currentPassword)
                throw new InvalidOperationException("Current password is incorrect.");

            _customers.UpdatePassword(customerId, newPassword);
            customer.Password = newPassword;
        }

        public void ExportToCsv(IList<Customer> customers, string filePath)
        {
            var sb = new System.Text.StringBuilder();
            sb.AppendLine("CustomerID,Name,Email,Phone,Address");
            foreach (var c in customers)
            {
                sb.Append(c.CustomerID).Append(',');
                sb.Append(EscapeCsv(c.Name)).Append(',');
                sb.Append(EscapeCsv(c.Email)).Append(',');
                sb.Append(EscapeCsv(c.Phone)).Append(',');
                sb.AppendLine(EscapeCsv(c.Address));
            }
            System.IO.File.WriteAllText(filePath, sb.ToString(), System.Text.Encoding.UTF8);
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }

        public void Add(Customer customer, string defaultPassword = "customer123")
        {
            ValidateCustomer(customer, isNew: true);
            if (_customers.EmailExists(customer.Email))
                throw new InvalidOperationException("Email already registered.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Password))
                customer.Password = defaultPassword;
            _customers.Insert(customer);
        }

        public void Update(Customer customer)
        {
            if (customer.CustomerID <= 0)
                throw new ArgumentException("Select a customer to update.");
            ValidateCustomer(customer, isNew: false);
            var existing = _customers.GetById(customer.CustomerID);
            if (existing == null)
                throw new InvalidOperationException("Customer not found.");
            if (!string.Equals(existing.Email, customer.Email, StringComparison.OrdinalIgnoreCase)
                && _customers.EmailExists(customer.Email))
                throw new InvalidOperationException("Email already registered.");
            customer.Password = existing.Password;
            _customers.Update(customer);
        }

        public void Delete(int customerId)
        {
            if (customerId <= 0)
                throw new ArgumentException("Select a customer to delete.");
            if (_customers.GetById(customerId) == null)
                throw new InvalidOperationException("Customer not found.");
            _customers.Delete(customerId);
        }

        public void GetOrderStats(IReadOnlyList<Customer> all, out int withOrders, out int withoutOrders)
        {
            var orderCustomerIds = new HashSet<int>(_orders.GetAll().Select(o => o.CustomerID));
            withOrders = all.Count(c => orderCustomerIds.Contains(c.CustomerID));
            withoutOrders = all.Count - withOrders;
        }

        private static void ValidateCustomer(Customer customer, bool isNew)
        {
            if (!isNew && customer.CustomerID <= 0)
                throw new ArgumentException("Select a customer to update.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Full name is required.");
            if (!ValidationService.IsValidEmail(customer.Email))
                throw new ArgumentException("Valid email is required.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Phone is required.");
            if (!ValidationService.IsValidSriLankaPhone(customer.Phone))
                throw new ArgumentException(ValidationService.SriLankaPhoneMessage);
            if (ValidationService.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.");
        }
    }
}
