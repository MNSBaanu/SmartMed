using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class PrescriptionService
    {
        public const string StatusPending = "Pending";
        public const string StatusVerified = "Verified";
        public const string StatusRejected = "Rejected";

        private readonly PrescriptionRepository _prescriptions = new PrescriptionRepository();

        /// <summary>Copy one Rx file and insert a Prescription row for (orderId, medicineId).</summary>
        public void SavePrescription(int customerId, int orderId, int medicineId, string sourcePath)
        {
            if (orderId <= 0)
                throw new ArgumentException("Order is required.");
            if (medicineId <= 0)
                throw new ArgumentException("Medicine is required.");

            var dest = PreparePrescriptionFile(customerId, sourcePath, medicineId);
            _prescriptions.Insert(customerId, orderId, dest, medicineId);
        }

        /// <summary>Copy and insert every medicineId → source path for an order.</summary>
        public void SavePrescriptions(int customerId, int orderId, IEnumerable<KeyValuePair<int, string>> medicineSourcePaths)
        {
            if (orderId <= 0)
                throw new ArgumentException("Order is required.");
            if (medicineSourcePaths == null)
                throw new ArgumentNullException(nameof(medicineSourcePaths));

            foreach (var pair in medicineSourcePaths)
                SavePrescription(customerId, orderId, pair.Key, pair.Value);
        }

        /// <summary>
        /// Copies source files into the Prescriptions folder and builds CreateOrder attachments.
        /// Caller should delete dest paths if the order transaction fails.
        /// </summary>
        public List<PrescriptionRepository.PrescriptionAttachment> PrepareAttachments(
            int customerId,
            IEnumerable<KeyValuePair<int, string>> medicineSourcePaths)
        {
            if (medicineSourcePaths == null)
                throw new ArgumentNullException(nameof(medicineSourcePaths));

            var attachments = new List<PrescriptionRepository.PrescriptionAttachment>();
            foreach (var pair in medicineSourcePaths)
            {
                if (pair.Key <= 0)
                    throw new ArgumentException("Medicine is required for each prescription.");
                if (ValidationService.IsNullOrWhiteSpace(pair.Value))
                    throw new ArgumentException("Select a valid prescription file for each Rx medicine.");

                var dest = PreparePrescriptionFile(customerId, pair.Value, pair.Key);
                attachments.Add(new PrescriptionRepository.PrescriptionAttachment
                {
                    MedicineID = pair.Key,
                    FilePath = dest
                });
            }

            return attachments;
        }

        public string PreparePrescriptionFile(int customerId, string sourcePath, int? medicineId = null)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");
            if (ValidationService.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new ArgumentException("Select a valid prescription file.");

            return CopyPrescriptionFile(customerId, sourcePath, medicineId);
        }

        public List<Prescription> GetAllByOrderId(int orderId) => _prescriptions.GetAllByOrderId(orderId);

        /// <summary>First prescription for the order (backward compatible).</summary>
        public Prescription GetByOrderId(int orderId) => _prescriptions.GetByOrderId(orderId);

        public void DeleteByOrderId(int orderId) => _prescriptions.DeleteByOrderId(orderId);

        public bool HasRecentUpload(int customerId) => _prescriptions.HasRecentUpload(customerId);

        /// <summary>Single file name, or "name (+N)" when the order has multiple Rx files.</summary>
        public string GetDisplayName(int orderId)
        {
            var all = GetAllByOrderId(orderId);
            if (all == null || all.Count == 0)
                return "—";

            var named = all
                .Where(p => !ValidationService.IsNullOrWhiteSpace(p.PrescriptionFile))
                .Select(p => Path.GetFileName(p.PrescriptionFile))
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .ToList();

            if (named.Count == 0)
                return "—";
            if (named.Count == 1)
                return named[0];

            return $"{named[0]} (+{named.Count - 1})";
        }

        /// <summary>First file path (open-compat). Prefer <see cref="GetFilePaths"/> for multi-Rx.</summary>
        public string GetFilePath(int orderId) => GetByOrderId(orderId)?.PrescriptionFile;

        public IReadOnlyList<string> GetFilePaths(int orderId)
        {
            return GetAllByOrderId(orderId)
                .Where(p => !ValidationService.IsNullOrWhiteSpace(p.PrescriptionFile))
                .Select(p => p.PrescriptionFile)
                .ToList();
        }

        public bool HasPrescription(int orderId) => GetAllByOrderId(orderId).Count > 0;

        /// <summary>
        /// Order-level aggregate: Pending if any Pending; else Rejected if any Rejected;
        /// Verified only when every row is Verified.
        /// </summary>
        public string GetStatus(int orderId)
        {
            var all = GetAllByOrderId(orderId);
            return AggregateStatus(all);
        }

        public static string AggregateStatus(IReadOnlyList<Prescription> prescriptions)
        {
            if (prescriptions == null || prescriptions.Count == 0)
                return null;

            if (prescriptions.Any(p => string.Equals(p.Status, StatusPending, StringComparison.OrdinalIgnoreCase)))
                return StatusPending;
            if (prescriptions.Any(p => string.Equals(p.Status, StatusRejected, StringComparison.OrdinalIgnoreCase)))
                return StatusRejected;
            if (prescriptions.All(p => string.Equals(p.Status, StatusVerified, StringComparison.OrdinalIgnoreCase)))
                return StatusVerified;

            return StatusPending;
        }

        public string GetStatusDisplay(int orderId)
        {
            var status = GetStatus(orderId);
            return string.IsNullOrWhiteSpace(status) ? "—" : status;
        }

        /// <summary>Sets every prescription on the order to Verified (admin order-level).</summary>
        public void Verify(int orderId) => SetOrderStatus(orderId, StatusVerified, StatusPending);

        /// <summary>Sets every prescription on the order to Rejected (admin order-level).</summary>
        public void Reject(int orderId) => SetOrderStatus(orderId, StatusRejected, StatusPending);

        public void VerifyById(int orderId, int prescriptionId) =>
            SetPrescriptionStatus(orderId, prescriptionId, StatusVerified, StatusPending);

        public void RejectById(int orderId, int prescriptionId) =>
            SetPrescriptionStatus(orderId, prescriptionId, StatusRejected, StatusPending);

        private void SetOrderStatus(int orderId, string newStatus, string requiredAggregate)
        {
            var all = GetAllByOrderId(orderId);
            if (all.Count == 0)
                throw new InvalidOperationException("This order has no prescription to review.");

            var current = AggregateStatus(all);
            if (!string.Equals(current, requiredAggregate, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Prescription is already {current}.");

            _prescriptions.UpdateStatus(orderId, newStatus);
        }

        private void SetPrescriptionStatus(int orderId, int prescriptionId, string newStatus, string requiredCurrent)
        {
            if (prescriptionId <= 0)
                throw new ArgumentException("Prescription is required.");

            var prescription = GetAllByOrderId(orderId)
                .FirstOrDefault(p => p.PrescriptionID == prescriptionId)
                ?? throw new InvalidOperationException("This order has no prescription to review.");

            if (!string.Equals(prescription.Status, requiredCurrent, StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException($"Prescription is already {prescription.Status}.");

            _prescriptions.UpdateStatusById(prescriptionId, newStatus);
        }

        private static string CopyPrescriptionFile(int customerId, string sourcePath, int? medicineId)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prescriptions");
            Directory.CreateDirectory(folder);
            var mid = medicineId.HasValue ? $"{medicineId.Value}_" : string.Empty;
            var fileName = $"{customerId}_{mid}{DateTime.Now:yyyyMMddHHmmssfff}_{Path.GetFileName(sourcePath)}";
            var dest = Path.Combine(folder, fileName);
            File.Copy(sourcePath, dest, true);
            return dest;
        }
    }
}
