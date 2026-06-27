using SmartMedNew.Data;
using SmartMedNew.Models;

namespace SmartMedNew.Services
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
            if (!ValidationService.IsValidSriLankaPhone(customer.Phone))
                throw new System.ArgumentException(ValidationService.SriLankaPhoneMessage);
            if (ValidationService.IsNullOrWhiteSpace(customer.Address))
                throw new System.ArgumentException("Address is required.");
            if (ValidationService.IsNullOrWhiteSpace(customer.Password))
                throw new System.ArgumentException("Password is required.");

            if (_customerRepo.EmailExists(customer.Email))
                throw new System.InvalidOperationException("Email already registered.");

            _customerRepo.Insert(customer);
        }

        public void ChangeAdminPassword(int adminId, string currentPassword, string newPassword)
        {
            if (adminId <= 0)
                throw new System.ArgumentException("Admin session is required.");
            if (ValidationService.IsNullOrWhiteSpace(currentPassword) || ValidationService.IsNullOrWhiteSpace(newPassword))
                throw new System.ArgumentException("Current and new passwords are required.");
            if (newPassword.Length < 6)
                throw new System.ArgumentException("New password must be at least 6 characters.");

            var admin = _adminRepo.GetById(adminId);
            if (admin == null || admin.Password != currentPassword)
                throw new System.InvalidOperationException("Current password is incorrect.");

            _adminRepo.UpdatePassword(adminId, newPassword);
        }
    }
}
