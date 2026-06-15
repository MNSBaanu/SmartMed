using System;

namespace SmartMed.Business.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }

        public decimal CalculateTotal() => TotalAmount;
        public void UpdateStatus(string status) => Status = status;
    }
}
