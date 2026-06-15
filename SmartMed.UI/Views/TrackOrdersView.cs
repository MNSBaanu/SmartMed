using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class TrackOrdersView : UserControl
    {
        private DataGridView gridOrders, gridItems;
        private readonly OrderService _service = new OrderService();

        public TrackOrdersView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Track Orders");
            header.Dock = DockStyle.Top;
            Controls.Add(header);

            var body = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            Controls.Add(body);

            gridOrders = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 160,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(gridOrders);
            gridOrders.CellFormatting += (s, e) => StatusChipHelper.OnCellFormatting(gridOrders, e);
            gridOrders.SelectionChanged += (s, e) =>
            {
                if (gridOrders.CurrentRow?.DataBoundItem is Data.Models.OrderRecord o)
                    gridItems.DataSource = _service.GetOrderItems(o.OrderID);
            };
            body.Controls.Add(gridOrders);

            var lblItems = UiFactory.CreateFieldLabel("Order Items");
            lblItems.Dock = DockStyle.Top;
            lblItems.Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, ClinicalPrecisionTheme.StackSm);
            body.Controls.Add(lblItems);

            gridItems = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(gridItems);
            body.Controls.Add(gridItems);

            gridOrders.DataSource = _service.GetCustomerOrders(Session.CurrentCustomer.CustomerID);
            if (gridOrders.Columns.Contains("OrderID")) gridOrders.Columns["OrderID"].Visible = false;
        }
    }
}
