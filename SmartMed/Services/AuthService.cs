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

            var admin = _adminRepo.GetByUsernameOrEmail(username.Trim());
            if (admin == null || !PasswordHasher.Verify(password, admin.Password))
                return null;

            UpgradePasswordIfNeeded(
                admin.Password,
                password,
                hash =>
                {
                    _adminRepo.UpdatePassword(admin.AdminID, hash);
                    admin.Password = hash;
                });

            return admin;
        }

        public Customer CustomerLogin(string email, string password)
        {
            if (!ValidationService.IsValidEmail(email))
                throw new System.ArgumentException("Valid email is required.");
            if (ValidationService.IsNullOrWhiteSpace(password))
                throw new System.ArgumentException("Password is required.");

            var customer = _customerRepo.GetByEmail(email.Trim());
            if (customer == null || !PasswordHasher.Verify(password, customer.Password))
                return null;

            if (!customer.IsActive)
                throw new System.InvalidOperationException("This account has been deactivated. Please contact the pharmacy administrator.");

            UpgradePasswordIfNeeded(
                customer.Password,
                password,
                hash =>
                {
                    _customerRepo.UpdatePassword(customer.CustomerID, hash);
                    customer.Password = hash;
                });

            return customer;
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

            customer.Password = PasswordHasher.Hash(customer.Password);
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
            if (admin == null || !PasswordHasher.Verify(currentPassword, admin.Password))
                throw new System.InvalidOperationException("Current password is incorrect.");

            _adminRepo.UpdatePassword(adminId, PasswordHasher.Hash(newPassword));
        }

        public void ResetPassword(string identity, string newPassword)
        {
            if (ValidationService.IsNullOrWhiteSpace(identity))
                throw new System.ArgumentException("Email or username is required.");
            if (ValidationService.IsNullOrWhiteSpace(newPassword))
                throw new System.ArgumentException("New password is required.");
            if (newPassword.Length < 6)
                throw new System.ArgumentException("New password must be at least 6 characters.");

            identity = identity.Trim();
            var hashed = PasswordHasher.Hash(newPassword);

            var admin = _adminRepo.GetByUsernameOrEmail(identity);
            if (admin != null)
            {
                _adminRepo.UpdatePassword(admin.AdminID, hashed);
                return;
            }

            if (ValidationService.IsValidEmail(identity))
            {
                var customer = _customerRepo.GetByEmail(identity);
                if (customer != null)
                {
                    if (!customer.IsActive)
                        throw new System.InvalidOperationException(
                            "This account has been deactivated. Please contact the pharmacy administrator.");
                    _customerRepo.UpdatePassword(customer.CustomerID, hashed);
                    return;
                }
            }

            throw new System.InvalidOperationException("Account not found.");
        }

        private static void UpgradePasswordIfNeeded(string stored, string plain, System.Action<string> saveHash)
        {
            if (PasswordHasher.LooksHashed(stored))
                return;
            saveHash(PasswordHasher.Hash(plain));
        }
    }
}
