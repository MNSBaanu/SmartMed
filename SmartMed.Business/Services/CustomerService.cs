using System;
using System.Collections.Generic;
using System.Linq;
using SmartMed.Business.Models;
using SmartMed.Data.Repositories;

namespace SmartMed.Business.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repo = new CustomerRepository();

        public List<Customer> GetAll()
        {
            var list = new List<Customer>();
            foreach (var c in _repo.GetAll())
                list.Add(Customer.FromDataModel(c));
            return list;
        }

        public Customer GetById(int id) => Customer.FromDataModel(_repo.GetById(id));

        public void Add(Customer customer)
        {
            ValidateCustomer(customer, isNew: true);
            if (_repo.EmailExists(customer.Email))
                throw new InvalidOperationException("Email already registered.");

            if (ValidationHelper.IsNullOrWhiteSpace(customer.Password))
                customer.Password = "customer123";

            customer.CustomerID = _repo.Insert(customer.ToDataModel());
        }

        public void Update(Customer customer)
        {
            ValidateCustomer(customer, isNew: false);
            var existing = _repo.GetById(customer.CustomerID);
            if (existing == null)
                throw new InvalidOperationException("Customer not found.");

            if (!string.Equals(existing.Email, customer.Email, StringComparison.OrdinalIgnoreCase)
                && _repo.EmailExists(customer.Email))
                throw new InvalidOperationException("Email already registered.");

            customer.Password = existing.Password;
            _repo.Update(customer.ToDataModel());
        }

        public void Delete(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Select a customer to delete.");
            if (_repo.GetById(id) == null)
                throw new InvalidOperationException("Customer not found.");
            _repo.Delete(id);
        }

        private static void ValidateCustomer(Customer customer, bool isNew)
        {
            if (!isNew && customer.CustomerID <= 0)
                throw new ArgumentException("Select a customer to update.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Full name is required.");
            if (!ValidationHelper.IsValidEmail(customer.Email))
                throw new ArgumentException("Valid email is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Phone is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.");
        }
    }
}
