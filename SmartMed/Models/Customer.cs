namespace SmartMed.Models
{
    public class Customer : User
    {
        public int CustomerID { get; set; }
        public string Address { get; set; }
    }
}
