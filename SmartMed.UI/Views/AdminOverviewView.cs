using System;
using System.Windows.Forms;
using SmartMed.Business;
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
            Resize += (s, e) => LayoutDashboard();
            LayoutDashboard();
        }

        private void AdminOverviewView_Load(object sender, EventArgs e)
        {
            ClinicalPrecisionUiHelper.StyleStatCard(panelInventory);
            ClinicalPrecisionUiHelper.StyleStatCard(panelOrders);
            ClinicalPrecisionUiHelper.StyleStatCard(panelRevenue);
            ClinicalPrecisionUiHelper.StyleGridCard(panelActivity);
            UiFactory.ApplyDataGridStyle(gridActivity);

            if (UiFactory.IsDesignMode(this))
            {
                lblWelcome.Text = "Welcome, admin";
                return;
            }

            var welcome = Session.CurrentAdmin?.Username ?? "Admin";
            lblWelcome.Text = $"Welcome, {welcome}";

            var service = new DashboardService();
            lblInventoryValue.Text = service.MedicinesInStock.ToString("N0");
            lblOrdersValue.Text = service.ActiveOrders.ToString();
            lblRevenueValue.Text = $"LKR {service.TotalSales:N2}";

            try
            {
                gridActivity.DataSource = new OrderService().GetAllOrders();
                if (gridActivity.Columns.Contains("OrderID")) gridActivity.Columns["OrderID"].Visible = false;
            }
            catch { }

            LayoutDashboard();
        }

        private void LayoutDashboard()
        {
            int w = ClientSize.Width;
            if (w < 100) return;
            int cardW = Math.Max(180, (w - 32) / 3);
            panelInventory.SetBounds(0, 72, cardW, 110);
            panelOrders.SetBounds(cardW + 16, 72, cardW, 110);
            panelRevenue.SetBounds((cardW + 16) * 2, 72, cardW, 110);

            int below = 200;
            int activityW = Math.Max(400, (int)(w * 0.62));
            panelActivity.SetBounds(0, below, activityW, 220);
            panelQuickFulfillment.SetBounds(activityW + 16, below, w - activityW - 16, 104);
            panelStockAlerts.SetBounds(activityW + 16, below + 120, w - activityW - 16, 100);
        }
    }
}

