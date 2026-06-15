using SmartMed.Data.Models;

namespace SmartMed.Business.Models
{
    public class Customer : Person
    {
        public int CustomerID { get; set; }
        public string Address { get; set; }

        public void Register() { }
        public void UpdateProfile() { }
        public void SearchMedicine() { }
        public void PlaceOrder() { }
        public void TrackOrder() { }
        public void Login() { }

        public static Customer FromDataModel(CustomerUser user)
        {
            if (user == null) return null;
            return new Customer
            {
                CustomerID = user.CustomerID,
                Name = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                Password = user.Password
            };
        }

        public CustomerUser ToDataModel()
        {
            return new CustomerUser
            {
                CustomerID = CustomerID,
                FullName = Name,
                Email = Email,
                Phone = Phone,
                Address = Address,
                Password = Password
            };
        }
    }
}
