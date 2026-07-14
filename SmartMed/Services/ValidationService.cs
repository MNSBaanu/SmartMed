using System;
using System.Text.RegularExpressions;

namespace SmartMed.Services
{
    public static class ValidationService
    {
        public const string SriLankaPhoneMessage =
            "Enter a valid Sri Lankan phone number (e.g. 0712345678 or +94 71 234 5678).";

        // Mobile: 07XXXXXXXX. Landline: known Sri Lankan area codes + 7 digits.
        private static readonly Regex SriLankaMobileLocal =
            new Regex(@"^07\d{8}$", RegexOptions.Compiled);

        private static readonly Regex SriLankaLandlineLocal =
            new Regex(
                @"^0(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91)\d{7}$",
                RegexOptions.Compiled);

        public static bool IsNullOrWhiteSpace(string value) =>
            string.IsNullOrWhiteSpace(value);

        // Confirm the email address is in a basic valid format.
        public static bool IsValidEmail(string email) =>
            !IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static string RequiredLabel(string label) => label + " *";

        public static string NormalizePhoneDigits(string phone) =>
            string.IsNullOrWhiteSpace(phone) ? string.Empty : Regex.Replace(phone, @"\D", "");

        /// <summary>
        /// Converts +94 / 94 / 0 forms to local digits (e.g. 0712345678). Returns empty if blank.
        /// </summary>
        public static string ToSriLankaLocalDigits(string phone)
        {
            var digits = NormalizePhoneDigits(phone);
            if (digits.Length == 0)
                return string.Empty;

            // International: 94 + 9-digit national number → 0XXXXXXXXX
            if (digits.StartsWith("94", StringComparison.Ordinal) && digits.Length == 11)
                return "0" + digits.Substring(2);

            return digits;
        }

        public static string ToCanonicalSriLankaPhone(string phone)
        {
            if (!TryGetCanonicalSriLankaPhone(phone, out var canonical))
                throw new ArgumentException(SriLankaPhoneMessage);
            return canonical;
        }

        public static bool TryGetCanonicalSriLankaPhone(string phone, out string canonical)
        {
            canonical = null;
            var local = ToSriLankaLocalDigits(phone);
            if (!IsValidSriLankaLocalDigits(local))
                return false;
            canonical = local;
            return true;
        }

        public static bool IsValidSriLankaPhone(string phone) =>
            TryGetCanonicalSriLankaPhone(phone, out _);

        private static bool IsValidSriLankaLocalDigits(string local) =>
            local != null
            && local.Length == 10
            && (SriLankaMobileLocal.IsMatch(local) || SriLankaLandlineLocal.IsMatch(local));

        public static bool IsNonNegativeDecimal(string value, out decimal result) =>
            decimal.TryParse(value, out result) && result >= 0;

        public static bool IsPositiveDecimal(string value, out decimal result) =>
            IsNonNegativeDecimal(value, out result);

        public static bool IsPositiveInt(string value, out int result) =>
            int.TryParse(value, out result) && result > 0;

        public static bool IsNonNegativeInt(string value, out int result) =>
            int.TryParse(value, out result) && result >= 0;
    }
}
