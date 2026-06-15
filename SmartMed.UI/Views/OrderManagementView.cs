using System;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class OrderManagementView : UserControl
    {
        private readonly OrderService _service = new OrderService();
        private int? _selectedOrderId;

        public OrderManagementView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void OrderManagementView_Load(object sender, EventArgs e)
        {
            UiFactory.ApplyDataGridStyle(gridOrders);
            UiFactory.ApplyDataGridStyle(gridItems);
            gridOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UiFactory.ApplyComboBoxStyle(cmbStatus, 200);
            gridOrders.CellFormatting += (s, ev) => StatusChipHelper.OnCellFormatting(gridOrders, ev);

            if (UiFactory.IsDesignMode(this)) return;
            LoadOrders();
        }

        private void LoadOrders()
        {
            gridOrders.DataSource = _service.GetAllOrders();
            if (gridOrders.Columns.Contains("OrderID")) gridOrders.Columns["OrderID"].Visible = false;
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (gridOrders.CurrentRow?.DataBoundItem is Data.Models.OrderRecord o)
            {
                _selectedOrderId = o.OrderID;
                cmbStatus.Text = o.Status;
                gridItems.DataSource = _service.GetOrderItems(o.OrderID);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue || cmbStatus.SelectedItem == null) return;
            try
            {
                _service.UpdateOrderStatus(_selectedOrderId.Value, cmbStatus.SelectedItem.ToString());
                LoadOrders();
                MessageBox.Show("Order status updated.");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
