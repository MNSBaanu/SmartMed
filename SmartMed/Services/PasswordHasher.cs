using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace SmartMed.Services
{
    public static class PasswordHasher
    {
        public const int DefaultIterations = 100000;
        private const int SaltSizeBytes = 16;
        private const int KeySizeBytes = 32;

        public static string Hash(string plain)
        {
            if (string.IsNullOrEmpty(plain))
                throw new ArgumentException("Password is required.", nameof(plain));

            // Create a protected password value that cannot be read back as plain text.
            var salt = new byte[SaltSizeBytes];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);

            var hash = Derive(plain, salt, DefaultIterations);
            return Format(DefaultIterations, salt, hash);
        }

        public static bool Verify(string plain, string stored)
        {
            if (string.IsNullOrEmpty(plain) || string.IsNullOrEmpty(stored))
                return false;

            // Support both protected passwords and older plain-text values during migration.
            if (!LooksHashed(stored))
                return FixedTimeEquals(
                    Encoding.UTF8.GetBytes(plain),
                    Encoding.UTF8.GetBytes(stored));

            if (!TryParse(stored, out int iterations, out byte[] salt, out byte[] expected))
                return false;

            var actual = Derive(plain, salt, iterations);
            return FixedTimeEquals(actual, expected);
        }

        public static bool LooksHashed(string stored)
        {
            if (string.IsNullOrWhiteSpace(stored))
                return false;

            var parts = stored.Split('$');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out int iterations)
                || iterations < 1)
                return false;

            try
            {
                var salt = Convert.FromBase64String(parts[1]);
                var hash = Convert.FromBase64String(parts[2]);
                return salt.Length > 0 && hash.Length > 0;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private static byte[] Derive(string plain, byte[] salt, int iterations)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, iterations, HashAlgorithmName.SHA256))
                return pbkdf2.GetBytes(KeySizeBytes);
        }

        private static string Format(int iterations, byte[] salt, byte[] hash)
        {
            return string.Format(
                CultureInfo.InvariantCulture,
                "{0}${1}${2}",
                iterations,
                Convert.ToBase64String(salt),
                Convert.ToBase64String(hash));
        }

        private static bool TryParse(string stored, out int iterations, out byte[] salt, out byte[] hash)
        {
            iterations = 0;
            salt = null;
            hash = null;

            var parts = stored.Split('$');
            if (parts.Length != 3)
                return false;

            if (!int.TryParse(parts[0], NumberStyles.None, CultureInfo.InvariantCulture, out iterations)
                || iterations < 1)
                return false;

            try
            {
                salt = Convert.FromBase64String(parts[1]);
                hash = Convert.FromBase64String(parts[2]);
            }
            catch (FormatException)
            {
                return false;
            }

            return salt.Length > 0 && hash.Length > 0;
        }

        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
                diff |= a[i] ^ b[i];
            return diff == 0;
        }
    }
}
