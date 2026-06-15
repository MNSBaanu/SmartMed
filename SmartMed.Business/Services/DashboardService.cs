using SmartMed.Data.Repositories;

namespace SmartMed.Business.Services
{
    public class DashboardService
    {
        private readonly MedicineService _medicineService = new MedicineService();
        private readonly OrderRepository _orderRepo = new OrderRepository();

        public decimal TotalSales => _orderRepo.GetTotalSales();
        public int MedicinesInStock => _medicineService.GetTotalStockCount();
        public int ActiveOrders => _orderRepo.GetActiveOrderCount();
    }
}
