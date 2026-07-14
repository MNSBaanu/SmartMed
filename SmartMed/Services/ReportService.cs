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
            // Build the sales report for the selected week, month, or year.
            var (from, toExclusive) = ReportPeriodHelper.GetRange(period);
            return _orders.GetSalesReport(from, toExclusive);
        }

        public DataTable GetSalesReport(DateTime fromInclusive, DateTime toInclusive)
        {
            var (from, toExclusive) = ToRepoRange(fromInclusive, toInclusive);
            return _orders.GetSalesReport(from, toExclusive);
        }

        public DataTable GetSalesReportDisplay(ReportPeriod period) =>
            ReportTableFormatter.FormatSalesReport(GetSalesReport(period));

        public DataTable GetSalesReportDisplay(DateTime fromInclusive, DateTime toInclusive) =>
            ReportTableFormatter.FormatSalesReport(GetSalesReport(fromInclusive, toInclusive));

        public DataTable GetStockReport() => _orders.GetStockReport();

        public DataTable GetStockReport(ReportPeriod period)
        {
            var (_, toExclusive) = ReportPeriodHelper.GetRange(period);
            return _orders.GetStockReport(toExclusive.AddDays(-1));
        }

        public DataTable GetStockReport(DateTime fromInclusive, DateTime toInclusive)
        {
            var (_, toExclusive) = ToRepoRange(fromInclusive, toInclusive);
            return _orders.GetStockReport(toExclusive.AddDays(-1));
        }

        public DataTable GetStockReportDisplay() =>
            ReportTableFormatter.FormatStockReport(GetStockReport());

        public DataTable GetStockReportDisplay(ReportPeriod period) =>
            ReportTableFormatter.FormatStockReport(GetStockReport(period));

        public DataTable GetStockReportDisplay(DateTime fromInclusive, DateTime toInclusive) =>
            ReportTableFormatter.FormatStockReport(GetStockReport(fromInclusive, toInclusive));

        public decimal GetOutstandingAmount(ReportPeriod period)
        {
            var (from, toExclusive) = ReportPeriodHelper.GetRange(period);
            return _orders.GetOutstandingAmount(from, toExclusive);
        }

        public decimal GetOutstandingAmount(DateTime fromInclusive, DateTime toInclusive)
        {
            var (from, toExclusive) = ToRepoRange(fromInclusive, toInclusive);
            return _orders.GetOutstandingAmount(from, toExclusive);
        }

        public DataTable GetExpiryReport() => _medicines.GetExpiryReport();

        /// <summary>
        /// Order history for one customer, or all customers when <paramref name="customerId"/> is &lt;= 0.
        /// </summary>
        public DataTable GetCustomerOrderHistory(int customerId, ReportPeriod period)
        {
            var (from, toExclusive) = ReportPeriodHelper.GetRange(period);
            return _orders.GetCustomerOrderHistory(customerId, from, toExclusive);
        }

        /// <summary>
        /// Order history for one customer, or all customers when <paramref name="customerId"/> is &lt;= 0.
        /// </summary>
        public DataTable GetCustomerOrderHistory(int customerId, DateTime fromInclusive, DateTime toInclusive)
        {
            var (from, toExclusive) = ToRepoRange(fromInclusive, toInclusive);
            return _orders.GetCustomerOrderHistory(customerId, from, toExclusive);
        }

        public DataTable GetCustomerOrderHistoryDisplay(int customerId, ReportPeriod period) =>
            ReportTableFormatter.FormatCustomerOrderHistory(GetCustomerOrderHistory(customerId, period));

        public DataTable GetCustomerOrderHistoryDisplay(int customerId, DateTime fromInclusive, DateTime toInclusive) =>
            ReportTableFormatter.FormatCustomerOrderHistory(GetCustomerOrderHistory(customerId, fromInclusive, toInclusive));

        /// <summary>
        /// Converts inclusive calendar dates to the inclusive-start / exclusive-end range expected by repositories.
        /// </summary>
        private static (DateTime From, DateTime ToExclusive) ToRepoRange(DateTime fromInclusive, DateTime toInclusive)
        {
            var from = fromInclusive.Date;
            var to = toInclusive.Date;
            if (to < from)
                throw new ArgumentException("Report end date must be on or after the start date.");
            return (from, to.AddDays(1));
        }

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
            // Save the report as a spreadsheet file for office use.
            ExportHelper.ExportDataTableToCsv(table, filePath);
        }

        public void ExportActiveReportToPdf(DataTable table, string filePath, string title, string subtitle = null)
        {
            // Save the report as a printable PDF document.
            ExportHelper.ExportDataTableToPdf(table, filePath, title, subtitle);
        }
    }
}
