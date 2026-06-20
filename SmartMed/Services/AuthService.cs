using SmartMed.Data.Repositories;
using SmartMed.Models;

namespace SmartMed.Business.Services
{
    public class AuthService
    {
        private readonly AdminRepository _adminRepo = new AdminRepository();
        private readonly CustomerRepository _customerRepo = new CustomerRepository();

        public Admin AdminLogin(string username, string password)
        {
            if (ValidationHelper.IsNullOrWhiteSpace(username) || ValidationHelper.IsNullOrWhiteSpace(password))
                throw new System.ArgumentException("Username and password are required.");

            return _adminRepo.GetByCredentials(username.Trim(), password);
        }

        public Customer CustomerLogin(string email, string password)
        {
            if (!ValidationHelper.IsValidEmail(email))
                throw new System.ArgumentException("Valid email is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(password))
                throw new System.ArgumentException("Password is required.");

            return _customerRepo.GetByCredentials(email.Trim(), password);
        }

        public void RegisterCustomer(Customer customer)
        {
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Name))
                throw new System.ArgumentException("Full name is required.");
            if (!ValidationHelper.IsValidEmail(customer.Email))
                throw new System.ArgumentException("Valid email is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Phone))
                throw new System.ArgumentException("Phone is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Address))
                throw new System.ArgumentException("Address is required.");
            if (ValidationHelper.IsNullOrWhiteSpace(customer.Password))
                throw new System.ArgumentException("Password is required.");

            if (_customerRepo.EmailExists(customer.Email))
                throw new System.InvalidOperationException("Email already registered.");

            _customerRepo.Insert(customer);
        }
    }
}
