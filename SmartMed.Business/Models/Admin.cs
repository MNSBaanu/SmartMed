using SmartMed.Data.Models;

namespace SmartMed.Business.Models
{
    public class Admin : Person
    {
        public int AdminID { get; set; }
        public string Username { get; set; }

        public void Login() { }
        public void ManageMedicines() { }
        public void ManageCustomers() { }
        public void ManageOrders() { }
        public void GenerateReports() { }
        public void ViewDashboard() { }

        public static Admin FromDataModel(AdminUser user)
        {
            if (user == null) return null;
            return new Admin
            {
                AdminID = user.AdminID,
                Username = user.Username,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password
            };
        }
    }
}
