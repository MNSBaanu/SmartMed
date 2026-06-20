using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class AuthService
    {
        private readonly AdminRepository _adminRepo = new AdminRepository();
        private readonly CustomerRepository _customerRepo = new CustomerRepository();

        public Admin AdminLogin(string username, string password)
        {
            if (ValidationService.IsNullOrWhiteSpace(username) || ValidationService.IsNullOrWhiteSpace(password))
                throw new System.ArgumentException("Username and password are required.");

            return _adminRepo.GetByCredentials(username.Trim(), password);
        }

        public Customer CustomerLogin(string email, string password)
        {
            if (!ValidationService.IsValidEmail(email))
                throw new System.ArgumentException("Valid email is required.");
            if (ValidationService.IsNullOrWhiteSpace(password))
                throw new System.ArgumentException("Password is required.");

            return _customerRepo.GetByCredentials(email.Trim(), password);
        }

        public void RegisterCustomer(Customer customer)
        {
            if (ValidationService.IsNullOrWhiteSpace(customer.Name))
                throw new System.ArgumentException("Full name is required.");
            if (!ValidationService.IsValidEmail(customer.Email))
                throw new System.ArgumentException("Valid email is required.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Phone))
                throw new System.ArgumentException("Phone is required.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Address))
                throw new System.ArgumentException("Address is required.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Password))
                throw new System.ArgumentException("Password is required.");

            if (_customerRepo.EmailExists(customer.Email))
                throw new System.InvalidOperationException("Email already registered.");

            _customerRepo.Insert(customer);
        }
    }
}
