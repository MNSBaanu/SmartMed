namespace SmartMed.Services
{
    internal static class MedicineExpiryNotifications
    {
        private static bool _dashboardPromptShown;

        public static bool TryMarkDashboardPromptShown()
        {
            if (_dashboardPromptShown)
                return false;

            _dashboardPromptShown = true;
            return true;
        }
    }
}
