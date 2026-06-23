using System;
using System.Collections.Generic;
using System.Linq;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _customers = new CustomerRepository();
        private readonly OrderRepository _orders = new OrderRepository();

        public List<Customer> GetAll() => _customers.GetAll();

        public Customer GetById(int id) => _customers.GetById(id);

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
            if (ValidationService.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.");
        }
    }
}
