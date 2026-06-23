using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SmartMed.Services
{
    public static class ValidationService
    {
        public const string SriLankaPhoneMessage =
            "Phone number must contain exactly 10 digits.";

        public static bool IsNullOrWhiteSpace(string value) =>
            string.IsNullOrWhiteSpace(value);

        public static bool IsValidEmail(string email) =>
            !IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static string RequiredLabel(string label) => label + " *";

        public static string NormalizePhoneDigits(string phone) =>
            string.IsNullOrWhiteSpace(phone) ? string.Empty : Regex.Replace(phone, @"\D", "");

        public static bool IsValidSriLankaPhone(string phone)
        {
            var digits = NormalizePhoneDigits(phone);
            return digits.Length == 10 && digits.All(char.IsDigit);
        }

        public static bool IsPositiveDecimal(string value, out decimal result) =>
            decimal.TryParse(value, out result) && result >= 0;

        public static bool IsPositiveInt(string value, out int result) =>
            int.TryParse(value, out result) && result > 0;

        public static bool IsNonNegativeInt(string value, out int result) =>
            int.TryParse(value, out result) && result >= 0;
    }
}
