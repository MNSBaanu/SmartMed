using System;

namespace SmartMed.Models
{
    public class HealthServiceRecord
    {
        public int RecordID { get; set; }
        public int ServiceID { get; set; }
        public int CustomerID { get; set; }
        public int? AdminID { get; set; }
        public DateTime ServiceDate { get; set; }
        public string Result { get; set; }
        public string PharmacistNotes { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ServiceName { get; set; }
        public string CustomerName { get; set; }
        public string PharmacistName { get; set; }
    }
}
