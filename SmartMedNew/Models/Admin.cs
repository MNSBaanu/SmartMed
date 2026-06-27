namespace SmartMedNew.Models
{
    public class Admin : User
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
