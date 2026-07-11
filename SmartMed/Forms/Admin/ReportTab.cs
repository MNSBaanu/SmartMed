namespace SmartMed.UI
{
    public class ReportTab
    {
        public string Key { get; }

        private ReportTab(string key) => Key = key;

        public static readonly ReportTab SalesPerformance = new ReportTab("SalesPerformance");
        public static readonly ReportTab MedicineInventory = new ReportTab("MedicineInventory");
        public static readonly ReportTab CustomerOrderHistory = new ReportTab("CustomerOrderHistory");
    }
}

