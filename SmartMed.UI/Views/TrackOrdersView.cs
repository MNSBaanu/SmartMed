using System;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class TrackOrdersView : UserControl
    {
        private readonly OrderService _service = new OrderService();

        public TrackOrdersView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void TrackOrdersView_Load(object sender, EventArgs e)
        {
            UiFactory.ApplyDataGridStyle(gridOrders);
            UiFactory.ApplyDataGridStyle(gridItems);
            gridOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridOrders.CellFormatting += (s, ev) => StatusChipHelper.OnCellFormatting(gridOrders, ev);

            if (UiFactory.IsDesignMode(this)) return;
            gridOrders.DataSource = _service.GetCustomerOrders(Session.CurrentCustomer.CustomerID);
            if (gridOrders.Columns.Contains("OrderID")) gridOrders.Columns["OrderID"].Visible = false;
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (gridOrders.CurrentRow?.DataBoundItem is Data.Models.OrderRecord o)
                gridItems.DataSource = _service.GetOrderItems(o.OrderID);
        }
    }
}
