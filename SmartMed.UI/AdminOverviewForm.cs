using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class AdminOverviewForm : Form
    {
        public AdminOverviewForm()
        {
            Text = "Dashboard Overview";
            Size = new Size(400, 280);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;

            var service = new DashboardService();
            var lblSales = new Label { Text = $"Total Sales: LKR {service.TotalSales:N2}", Location = new Point(30, 30), AutoSize = true, Font = new Font("Segoe UI", 11) };
            var lblStock = new Label { Text = $"Medicines in Stock (units): {service.MedicinesInStock}", Location = new Point(30, 70), AutoSize = true, Font = new Font("Segoe UI", 11) };
            var lblOrders = new Label { Text = $"Active Orders: {service.ActiveOrders}", Location = new Point(30, 110), AutoSize = true, Font = new Font("Segoe UI", 11) };
            var btnClose = new Button { Text = "Close", Location = new Point(150, 170), Width = 100 };
            btnClose.Click += (s, e) => Close();
            Controls.AddRange(new Control[] { lblSales, lblStock, lblOrders, btnClose });
        }
    }
}
