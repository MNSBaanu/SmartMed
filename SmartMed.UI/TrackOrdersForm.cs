using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class TrackOrdersForm : Form
    {
        private DataGridView gridOrders, gridItems;
        private readonly OrderService _service = new OrderService();

        public TrackOrdersForm()
        {
            Text = "Track Orders";
            Size = new Size(800, 480);
            StartPosition = FormStartPosition.CenterParent;

            gridOrders = new DataGridView { Location = new Point(20, 20), Size = new Size(740, 180), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridItems = new DataGridView { Location = new Point(20, 220), Size = new Size(740, 200), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridOrders.SelectionChanged += (s, e) =>
            {
                if (gridOrders.CurrentRow?.DataBoundItem is Data.Models.OrderRecord o)
                    gridItems.DataSource = _service.GetOrderItems(o.OrderID);
            };

            Controls.AddRange(new Control[] { gridOrders, gridItems });
            gridOrders.DataSource = _service.GetCustomerOrders(Session.CurrentCustomer.CustomerID);
        }
    }
}
