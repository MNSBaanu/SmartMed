using System;
using SmartMed.Business.Models;
using SmartMed.Data.Repositories;

namespace SmartMed.Business.Services
{
    public class AuthService
    {
        private readonly AdminRepository _adminRepo = new AdminRepository();
        private readonly CustomerRepository _customerRepo = new CustomerRepository();

        public Admin AdminLogin(string username, string password)
        {
            if (ValidationHelper.IsNullOrWhiteSpace(username) || ValidationHelper.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username and password are required.");

            var admin = _adminRepo.GetByCredentials(username.Trim(), password);
            return Admin.FromDataModel(admin);
        }

        public Customer CustomerLogin(string email, string password)
        {
            if (!ValidationHelper.IsValidEmail(email))
                throw new ArgumentException("Valid email is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password is required.");

            var customer = _customerRepo.GetByCredentials(email.Trim(), password);
            return Customer.FromDataModel(customer);
        }

        public void RegisterCustomer(Customer customer)
        {
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Name))
                throw new ArgumentException("Full name is required.");
            if (!ValidationHelper.IsValidEmail(customer.Email))
                throw new ArgumentException("Valid email is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Phone))
                throw new ArgumentException("Phone is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Address))
                throw new ArgumentException("Address is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Password))
                throw new ArgumentException("Password is required.");

            if (_customerRepo.EmailExists(customer.Email))
                throw new InvalidOperationException("Email already registered.");

            _customerRepo.Insert(customer.ToDataModel());
        }
    }
}
