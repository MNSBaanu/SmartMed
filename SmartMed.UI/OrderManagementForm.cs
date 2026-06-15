using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;

namespace SmartMed.UI
{
    public class OrderManagementForm : Form
    {
        private DataGridView gridOrders, gridItems;
        private ComboBox cmbStatus;
        private readonly OrderService _service = new OrderService();
        private int? _selectedOrderId;

        public OrderManagementForm()
        {
            Text = "Manage Orders";
            Size = new Size(850, 520);
            StartPosition = FormStartPosition.CenterParent;

            gridOrders = new DataGridView { Location = new Point(20, 20), Size = new Size(800, 180), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridItems = new DataGridView { Location = new Point(20, 220), Size = new Size(800, 150), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridOrders.SelectionChanged += (s, e) =>
            {
                if (gridOrders.CurrentRow?.DataBoundItem is Data.Models.OrderRecord o)
                {
                    _selectedOrderId = o.OrderID;
                    cmbStatus.Text = o.Status;
                    gridItems.DataSource = _service.GetOrderItems(o.OrderID);
                }
            };

            cmbStatus = new ComboBox { Location = new Point(120, 390), Width = 200, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new object[] { "Pending", "Ready for Pickup", "Delivered" });

            var btnUpdate = new Button { Text = "Update Status", Location = new Point(340, 388), Width = 120 };
            btnUpdate.Click += BtnUpdate_Click;

            Controls.AddRange(new Control[] { gridOrders, gridItems, new Label { Text = "Status:", Location = new Point(20, 393), AutoSize = true }, cmbStatus, btnUpdate });
            LoadOrders();
        }

        private void LoadOrders() { gridOrders.DataSource = _service.GetAllOrders(); }

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
