using System;

namespace SmartMedNew.Models
{
    public class Prescription
    {
        public int PrescriptionID { get; set; }
        public int CustomerID { get; set; }
        public int OrderID { get; set; }
        public string PrescriptionFile { get; set; }
        public DateTime UploadDate { get; set; }
        public string Status { get; set; }
    }
}
