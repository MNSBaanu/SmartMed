namespace SmartMed.UI
{
    public class ReportTab
    {
        public string Key { get; }

        private ReportTab(string key) => Key = key;

        public static readonly ReportTab Sales = new ReportTab("Sales");
        public static readonly ReportTab Stock = new ReportTab("Stock");
        public static readonly ReportTab Expiry = new ReportTab("Expiry");
        public static readonly ReportTab History = new ReportTab("History");
    }
}
