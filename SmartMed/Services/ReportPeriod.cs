using System;

namespace SmartMed.Services
{
    public enum ReportPeriod
    {
        Week,
        Month,
        Year
    }

    public static class ReportPeriodHelper
    {
        public static (DateTime From, DateTime ToExclusive) GetRange(ReportPeriod period)
        {
            var today = DateTime.Today;
            switch (period)
            {
                case ReportPeriod.Week:
                    return (today.AddDays(-6), today.AddDays(1));
                case ReportPeriod.Year:
                    return (new DateTime(today.Year, 1, 1), today.AddDays(1));
                default:
                    return (new DateTime(today.Year, today.Month, 1), today.AddDays(1));
            }
        }

        public static string GetLabel(ReportPeriod period)
        {
            if (period == ReportPeriod.Week) return "Week";
            if (period == ReportPeriod.Year) return "Year";
            return "Month";
        }

        public static string GetFileSuffix(ReportPeriod period) =>
            GetLabel(period).ToLowerInvariant();
    }
}
