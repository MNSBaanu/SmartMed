using System.Data;
using SmartMed.Data;

namespace SmartMed.Services
{
    public class ReportService
    {
        private readonly OrderRepository _orders = new OrderRepository();
        private readonly MedicineRepository _medicines = new MedicineRepository();

        public DataTable GetSalesReport() => _orders.GetSalesReport();

        public DataTable GetStockReport() => _orders.GetStockReport();

        public DataTable GetExpiryReport() => _medicines.GetExpiryReport();

        public DataTable GetCustomerOrderHistory(int customerId) =>
            _orders.GetCustomerOrderHistory(customerId);

        public decimal TotalSales => _orders.GetTotalSales();

        public int ActiveOrders => _orders.GetActiveOrderCount();

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
    }
}
