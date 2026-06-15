using SmartMed.Business.Models;

namespace SmartMed.Business
{
    public static class Session
    {
        public static Admin CurrentAdmin { get; set; }
        public static Customer CurrentCustomer { get; set; }

        public static bool IsAdminLoggedIn => CurrentAdmin != null;
        public static bool IsCustomerLoggedIn => CurrentCustomer != null;

        public static void Clear()
        {
            CurrentAdmin = null;
            CurrentCustomer = null;
        }
    }
}
