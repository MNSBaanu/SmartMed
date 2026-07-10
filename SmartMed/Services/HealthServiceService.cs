using System;
using System.Collections.Generic;
using System.Data;
using SmartMed.Data;
using SmartMed.Models;

namespace SmartMed.Services
{
    public class HealthServiceService
    {
        private readonly HealthServiceRepository _services = new HealthServiceRepository();
        private readonly HealthServiceRecordRepository _records = new HealthServiceRecordRepository();

        public List<HealthService> GetAllServices() => _services.GetAll();

        public List<HealthService> GetActiveServices() => _services.GetActive();

        public HealthService GetServiceById(int serviceId) => _services.GetById(serviceId);

        public void AddService(HealthService item)
        {
            ValidateService(item, isNew: true);
            _services.Insert(item);
        }

        public void UpdateService(HealthService item)
        {
            ValidateService(item, isNew: false);
            _services.Update(item);
        }

        public void DeleteService(int serviceId)
        {
            if (serviceId <= 0)
                throw new ArgumentException("Select a service to remove.");
            if (_services.HasRecords(serviceId))
                throw new InvalidOperationException("Cannot remove a service that has delivery records.");
            _services.Delete(serviceId);
        }

        public List<HealthServiceRecord> GetAllRecords() => _records.GetAll();

        public List<HealthServiceRecord> GetCustomerRecords(int customerId) => _records.GetByCustomerId(customerId);

        public void AddRecord(HealthServiceRecord record)
        {
            ValidateRecord(record, isNew: true);
            _records.Insert(record);
        }

        public void UpdateRecord(HealthServiceRecord record)
        {
            ValidateRecord(record, isNew: false);
            _records.Update(record);
        }

        public void DeleteRecord(int recordId)
        {
            if (recordId <= 0)
                throw new ArgumentException("Select a record to remove.");
            _records.Delete(recordId);
        }

        public DataTable GetHealthServicesReport(ReportPeriod period, int? customerId = null)
        {
            var range = ReportPeriodHelper.GetRange(period);
            return _records.GetReport(range.From, range.ToExclusive, customerId);
        }

        public DataTable GetHealthServicesReportDisplay(ReportPeriod period, int? customerId = null) =>
            ReportTableFormatter.FormatHealthServicesReport(GetHealthServicesReport(period, customerId));

        private static void ValidateService(HealthService item, bool isNew)
        {
            if (!isNew && item.ServiceID <= 0)
                throw new ArgumentException("Select a service to update.");
            if (ValidationService.IsNullOrWhiteSpace(item.ServiceName))
                throw new ArgumentException("Service name is required.");
            if (item.Price < 0)
                throw new ArgumentException("Price cannot be negative.");
        }

        private static void ValidateRecord(HealthServiceRecord record, bool isNew)
        {
            if (!isNew && record.RecordID <= 0)
                throw new ArgumentException("Select a record to update.");
            if (record.ServiceID <= 0)
                throw new ArgumentException("Select a health service.");
            if (record.CustomerID <= 0)
                throw new ArgumentException("Select a customer.");
            if (record.ServiceDate == default)
                throw new ArgumentException("Service date is required.");
            if (ValidationService.IsNullOrWhiteSpace(record.Result))
                throw new ArgumentException("Result is required.");
        }
    }
}
