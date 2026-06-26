using System;
using System.IO;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepository _prescriptions = new PrescriptionRepository();

        public void SavePrescription(int customerId, int orderId, string sourcePath)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");
            if (orderId <= 0)
                throw new ArgumentException("Order is required.");
            if (ValidationService.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new ArgumentException("Select a valid prescription file.");

            var dest = CopyPrescriptionFile(customerId, sourcePath);
            _prescriptions.Insert(customerId, orderId, dest);
        }

        public Prescription GetByOrderId(int orderId) => _prescriptions.GetByOrderId(orderId);

        public void DeleteByOrderId(int orderId) => _prescriptions.DeleteByOrderId(orderId);

        public bool HasRecentUpload(int customerId) => _prescriptions.HasRecentUpload(customerId);

        public string GetDisplayName(int orderId)
        {
            var prescription = GetByOrderId(orderId);
            if (prescription == null || ValidationService.IsNullOrWhiteSpace(prescription.PrescriptionFile))
                return "—";
            return Path.GetFileName(prescription.PrescriptionFile);
        }

        public string GetFilePath(int orderId)
        {
            var prescription = GetByOrderId(orderId);
            return prescription?.PrescriptionFile;
        }

        private static string CopyPrescriptionFile(int customerId, string sourcePath)
        {
            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prescriptions");
            Directory.CreateDirectory(folder);
            var fileName = $"{customerId}_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(sourcePath)}";
            var dest = Path.Combine(folder, fileName);
            File.Copy(sourcePath, dest, true);
            return dest;
        }
    }
}
