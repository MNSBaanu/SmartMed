namespace SmartMed.Models
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
    }
}
