namespace SmartMed.UI
{
    public class AdminNavItem
    {
        public string Key { get; }

        private AdminNavItem(string key) => Key = key;

        public static readonly AdminNavItem Overview = new AdminNavItem("Overview");
        public static readonly AdminNavItem Medicines = new AdminNavItem("Medicines");
        public static readonly AdminNavItem Customers = new AdminNavItem("Customers");
        public static readonly AdminNavItem Orders = new AdminNavItem("Orders");
        public static readonly AdminNavItem Reports = new AdminNavItem("Reports");
    }
}
