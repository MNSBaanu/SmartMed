using System;
using System.Text.RegularExpressions;

namespace SmartMed.Business
{
    public static class ValidationHelper
    {
        public static bool IsNullOrWhiteSpace(string value) =>
            string.IsNullOrWhiteSpace(value);

        public static bool IsValidEmail(string email) =>
            !IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        public static bool IsPositiveDecimal(string value, out decimal result) =>
            decimal.TryParse(value, out result) && result >= 0;

        public static bool IsPositiveInt(string value, out int result) =>
            int.TryParse(value, out result) && result > 0;

        public static bool IsNonNegativeInt(string value, out int result) =>
            int.TryParse(value, out result) && result >= 0;
    }
}
