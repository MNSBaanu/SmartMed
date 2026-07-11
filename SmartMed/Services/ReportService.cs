using System;
using System.Data;
using SmartMed.Data;

namespace SmartMed.Services
{
    public class ReportService
    {
        private readonly OrderRepository _orders = new OrderRepository();
        private readonly MedicineRepository _medicines = new MedicineRepository();
        private readonly CustomerRepository _customers = new CustomerRepository();

        public DataTable GetSalesReport(ReportPeriod period)
        {
            var range = ReportPeriodHelper.GetRange(period);
            return _orders.GetSalesReport(range.From, range.ToExclusive);
        }

        public DataTable GetSalesReportDisplay(ReportPeriod period) =>
            ReportTableFormatter.FormatSalesReport(GetSalesReport(period));

        public DataTable GetStockReport() => _orders.GetStockReport();

        public DataTable GetStockReportDisplay() =>
            ReportTableFormatter.FormatStockReport(GetStockReport());

        public decimal GetOutstandingAmount(ReportPeriod period)
        {
            var range = ReportPeriodHelper.GetRange(period);
            return _orders.GetOutstandingAmount(range.From, range.ToExclusive);
        }

        public DataTable GetExpiryReport() => _medicines.GetExpiryReport();

        public DataTable GetCustomerOrderHistory(int customerId, ReportPeriod period)
        {
            var range = ReportPeriodHelper.GetRange(period);
            return _orders.GetCustomerOrderHistory(customerId, range.From, range.ToExclusive);
        }

        public DataTable GetCustomerOrderHistoryDisplay(int customerId, ReportPeriod period) =>
            ReportTableFormatter.FormatCustomerOrderHistory(GetCustomerOrderHistory(customerId, period));

        public decimal TotalSales => _orders.GetTotalSales();

        public int ActiveOrders => _orders.GetActiveOrderCount();

        public int RegisteredCustomers => _customers.GetCount();

        public int MedicinesInStock
        {
            get
            {
                int total = 0;
                foreach (var m in _medicines.GetAll())
                    total += m.StockQuantity;
                return total;
            }
        }

        public void ExportActiveReportToCsv(DataTable table, string filePath)
        {
            ExportHelper.ExportDataTableToCsv(table, filePath);
        }

        public void ExportActiveReportToPdf(DataTable table, string filePath, string title, string subtitle = null)
        {
            ExportHelper.ExportDataTableToPdf(table, filePath, title, subtitle);
        }
    }
}
