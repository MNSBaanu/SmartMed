using System;

namespace SmartMed.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public string PaymentReference { get; set; }
        public string CancellationReason { get; set; }

        public void CalculateTotal() { }
        public void UpdateStatus(string status) { }
    }
}
