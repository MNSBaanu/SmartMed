using System;
using System.IO;
using SmartMed.Data;

namespace SmartMed.Services
{
    public class PrescriptionService
    {
        private readonly PrescriptionRepository _prescriptions = new PrescriptionRepository();

        public string SavePrescription(int customerId, string sourcePath)
        {
            if (customerId <= 0)
                throw new ArgumentException("Customer is required.");
            if (ValidationService.IsNullOrWhiteSpace(sourcePath) || !File.Exists(sourcePath))
                throw new ArgumentException("Select a valid prescription file.");

            var folder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Prescriptions");
            Directory.CreateDirectory(folder);
            var fileName = $"{customerId}_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(sourcePath)}";
            var dest = Path.Combine(folder, fileName);
            File.Copy(sourcePath, dest, true);
            _prescriptions.Insert(customerId, dest);
            return dest;
        }

        public bool HasRecentUpload(int customerId) => _prescriptions.HasRecentUpload(customerId);
    }
}
