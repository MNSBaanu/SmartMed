using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class AdminOverviewView : UserControl
    {
        public AdminOverviewView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
            LayoutCards();
            Resize += (s, e) => LayoutCards();
        }

        private void AdminOverviewView_Load(object sender, EventArgs e)
        {
            if (UiFactory.IsDesignMode(this)) return;

            var service = new DashboardService();
            lblSalesValue.Text = $"LKR {service.TotalSales:N2}";
            lblStockValue.Text = service.MedicinesInStock.ToString("N0") + " units";
            lblOrdersValue.Text = service.ActiveOrders.ToString();
            LayoutCards();
        }

        private void LayoutCards()
        {
            int cardWidth = Math.Max(200, (ClientSize.Width - 48) / 3);
            panelSales.SetBounds(0, 40, cardWidth, 100);
            panelStock.SetBounds(cardWidth + 16, 40, cardWidth, 100);
            panelOrders.SetBounds((cardWidth + 16) * 2, 40, cardWidth, 100);
        }
    }
}
