using System;
using SmartMed.Business.Services;

namespace SmartMed.Verify
{
    internal static class Program
    {
        private static int Main()
        {
            try
            {
                var auth = new AuthService();
                var admin = auth.AdminLogin("admin", "admin123");
                if (admin == null) throw new Exception("Admin login failed");
                Console.WriteLine("OK Admin login: " + admin.Username);

                var customer = auth.CustomerLogin("john@email.com", "customer123");
                if (customer == null) throw new Exception("Customer login failed");
                Console.WriteLine("OK Customer login: " + customer.Name);

                var medicines = new MedicineService().GetAll();
                if (medicines.Count < 5) throw new Exception("Expected >= 5 medicines");
                Console.WriteLine("OK Medicines: " + medicines.Count);

                var customers = new CustomerService().GetAll();
                if (customers.Count < 2) throw new Exception("Expected >= 2 customers");
                Console.WriteLine("OK Customers: " + customers.Count);

                var orders = new OrderService().GetAllOrders();
                if (orders.Count < 2) throw new Exception("Expected >= 2 orders");
                Console.WriteLine("OK Orders: " + orders.Count);

                var items = new OrderService().GetOrderItems(orders[0].OrderID);
                Console.WriteLine("OK Order items: " + items.Count);

                var dash = new DashboardService();
                Console.WriteLine("OK Dashboard - Sales: " + dash.TotalSales + ", Stock: " + dash.MedicinesInStock + ", Active: " + dash.ActiveOrders);

                var search = new MedicineService().Search("Para", "", null, null);
                if (search.Count < 1) throw new Exception("Search returned no results");
                Console.WriteLine("OK Search: " + search.Count);

                var sales = new OrderService().GetSalesReport();
                var stock = new OrderService().GetStockReport();
                var history = new OrderService().GetCustomerOrderHistory(customer.CustomerID);
                Console.WriteLine("OK Reports - Sales: " + sales.Rows.Count + ", Stock: " + stock.Rows.Count + ", History: " + history.Rows.Count);

                var custOrders = new OrderService().GetCustomerOrders(customer.CustomerID);
                Console.WriteLine("OK Customer orders: " + custOrders.Count);

                Console.WriteLine("ALL CHECKS PASSED");
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("FAILED: " + ex.Message);
                return 1;
            }
        }
    }
}
