using System;
using System.Collections.Generic;
using System.Data;
using SmartMed.Data.Repositories;
using SmartMed.Models;

namespace SmartMed.Business.Services
{
    public class OrderService
    {
        private readonly OrderRepository _repo = new OrderRepository();
        private readonly MedicineRepository _medicineRepo = new MedicineRepository();

        public List<Order> GetAllOrders() => _repo.GetAll();

        public List<Order> GetCustomerOrders(int customerId) => _repo.GetByCustomer(customerId);

        public List<OrderItem> GetOrderItems(int orderId) => _repo.GetItems(orderId);

        public void UpdateOrderStatus(int orderId, string status)
        {
            if (orderId <= 0)
                throw new ArgumentException("Select an order to update.");
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status is required.");
            if (status != "Pending" && status != "Ready for Pickup" && status != "Delivered")
                throw new ArgumentException("Invalid order status.");

            _repo.UpdateStatus(orderId, status);
        }

        public int PlaceOrder(int customerId, List<OrderItem> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Cart is empty.");

            foreach (var item in items)
            {
                var med = _medicineRepo.GetById(item.MedicineID);
                if (med == null)
                    throw new InvalidOperationException("Medicine not found.");
                if (med.StockQuantity < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {med.MedicineName}.");
            }

            int orderId = _repo.CreateOrder(customerId, items);

            foreach (var item in items)
                _medicineRepo.UpdateStock(item.MedicineID, -item.Quantity);

            return orderId;
        }

        public DataTable GetSalesReport() => _repo.GetSalesReport();

        public DataTable GetStockReport() => _repo.GetStockReport();

        public DataTable GetCustomerOrderHistory(int customerId) => _repo.GetCustomerOrderHistory(customerId);
    }
}
