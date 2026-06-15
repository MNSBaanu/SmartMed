namespace SmartMed.Business.Models
{
    public class OrderItem
    {
        public int OrderItemID { get; set; }
        public int OrderID { get; set; }
        public int MedicineID { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public decimal CalculateSubtotal() => Quantity * UnitPrice;
    }
}
