using System;

namespace SmartMed.Services
{
    public sealed class PaymentRequest
    {
        public string Method { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string CardExpiry { get; set; }
        public string CardCvv { get; set; }
        public string BankName { get; set; }
        public string DepositorName { get; set; }
        public string BankReference { get; set; }
        public string TransferDate { get; set; }
    }

    public sealed class PaymentResult
    {
        public string Method { get; set; }
        public string Status { get; set; }
        public string Reference { get; set; }
    }

    public static class PaymentService
    {
        public const string MethodCard = "Card";
        public const string MethodCashOnPickup = "Cash on Pickup";
        public const string MethodBankTransfer = "Bank Transfer";

        public const string StatusPaid = "Paid";
        public const string StatusPayOnPickup = "Pay on Pickup";

        public const string PharmacyBankName = "Bank of Ceylon";
        public const string PharmacyAccountName = "SmartMed Pharmacy (Pvt) Ltd";
        public const string PharmacyAccountNumber = "001234567890";
        public const string PharmacyBranch = "Colombo Main";

        public static PaymentResult Process(PaymentRequest request, decimal amount)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (amount <= 0)
                throw new InvalidOperationException("Payment amount must be greater than zero.");
            if (ValidationService.IsNullOrWhiteSpace(request.Method))
                throw new ArgumentException("Select a payment method.");

            switch (request.Method)
            {
                case MethodCard:
                    return ProcessCard(request);
                case MethodCashOnPickup:
                    return new PaymentResult
                    {
                        Method = MethodCashOnPickup,
                        Status = StatusPayOnPickup,
                        Reference = null
                    };
                case MethodBankTransfer:
                    return ProcessBankTransfer(request);
                default:
                    throw new ArgumentException("Invalid payment method.");
            }
        }

        private static PaymentResult ProcessCard(PaymentRequest request)
        {
            var digits = ValidationService.NormalizePhoneDigits(request.CardNumber);
            if (digits.Length != 16)
                throw new ArgumentException("Enter a valid 16-digit card number.");

            if (ValidationService.IsNullOrWhiteSpace(request.CardName))
                throw new ArgumentException("Cardholder name is required.");

            var expiry = request.CardExpiry?.Trim() ?? string.Empty;
            if (!System.Text.RegularExpressions.Regex.IsMatch(expiry, @"^(0[1-9]|1[0-2])\/\d{2}$"))
                throw new ArgumentException("Expiry date must be in MM/YY format.");

            if (!IsExpiryInFuture(expiry))
                throw new ArgumentException("Card has expired. Check the expiry date.");

            var cvv = ValidationService.NormalizePhoneDigits(request.CardCvv);
            if (cvv.Length < 3 || cvv.Length > 4)
                throw new ArgumentException("Enter a valid 3 or 4 digit CVV / security code.");

            return new PaymentResult
            {
                Method = MethodCard,
                Status = StatusPaid,
                Reference = $"CARD-{digits.Substring(digits.Length - 4)}"
            };
        }

        private static PaymentResult ProcessBankTransfer(PaymentRequest request)
        {
            if (ValidationService.IsNullOrWhiteSpace(request.BankName))
                throw new ArgumentException("Your bank name is required.");

            if (ValidationService.IsNullOrWhiteSpace(request.DepositorName))
                throw new ArgumentException("Account holder / depositor name is required.");

            var reference = request.BankReference?.Trim();
            if (ValidationService.IsNullOrWhiteSpace(reference) || reference.Length < 6)
                throw new ArgumentException("Transaction reference must be at least 6 characters.");

            if (!string.IsNullOrWhiteSpace(request.TransferDate)
                && !DateTime.TryParse(request.TransferDate, out _))
                throw new ArgumentException("Transfer date must be a valid date.");

            return new PaymentResult
            {
                Method = MethodBankTransfer,
                Status = StatusPaid,
                Reference = reference.ToUpperInvariant()
            };
        }

        private static bool IsExpiryInFuture(string expiry)
        {
            var parts = expiry.Split('/');
            if (parts.Length != 2) return false;

            if (!int.TryParse(parts[0], out var month) || month < 1 || month > 12)
                return false;
            if (!int.TryParse(parts[1], out var yearTwoDigits))
                return false;

            var year = 2000 + yearTwoDigits;
            var lastDay = DateTime.DaysInMonth(year, month);
            var expiryDate = new DateTime(year, month, lastDay);
            return expiryDate.Date >= DateTime.Today;
        }
    }
}
