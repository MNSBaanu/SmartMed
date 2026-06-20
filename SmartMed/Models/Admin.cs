namespace SmartMed.Models
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
    }
}
