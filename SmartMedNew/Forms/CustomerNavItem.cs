namespace SmartMedNew.UI
{
    public class CustomerNavItem
    {
        public string Key { get; }

        private CustomerNavItem(string key) => Key = key;

        public static readonly CustomerNavItem Home = new CustomerNavItem("Home");
        public static readonly CustomerNavItem Browse = new CustomerNavItem("Browse");
        public static readonly CustomerNavItem Cart = new CustomerNavItem("Cart");
        public static readonly CustomerNavItem Orders = new CustomerNavItem("Orders");
        public static readonly CustomerNavItem Profile = new CustomerNavItem("Profile");
    }
}
