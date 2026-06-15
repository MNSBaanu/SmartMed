using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class OrderManagementView : UserControl
    {
        private DataGridView gridOrders, gridItems;
        private ComboBox cmbStatus;
        private readonly OrderService _service = new OrderService();
        private int? _selectedOrderId;

        public OrderManagementView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Manage Orders");
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
                {
                    _selectedOrderId = o.OrderID;
                    cmbStatus.Text = o.Status;
                    gridItems.DataSource = _service.GetOrderItems(o.OrderID);
                }
            };
            body.Controls.Add(gridOrders);

            var lblItems = UiFactory.CreateFieldLabel("Order Items");
            lblItems.Dock = DockStyle.Top;
            lblItems.Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, ClinicalPrecisionTheme.StackSm);
            body.Controls.Add(lblItems);

            gridItems = new DataGridView
            {
                Dock = DockStyle.Top,
                Height = 140,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(gridItems);
            body.Controls.Add(gridItems);

            var actionPanel = new Panel { Dock = DockStyle.Top, Height = 50, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            body.Controls.Add(actionPanel);

            var lblStatus = UiFactory.CreateFieldLabel("Status:");
            lblStatus.Location = new Point(0, 12);
            cmbStatus = new ComboBox { Location = new Point(60, 8), DropDownStyle = ComboBoxStyle.DropDownList };
            UiFactory.ApplyComboBoxStyle(cmbStatus, 200);
            cmbStatus.Items.AddRange(new object[] { "Pending", "Ready for Pickup", "Delivered" });

            var btnUpdate = UiFactory.CreatePrimaryButton("Update Status", 120);
            btnUpdate.Location = new Point(280, 6);
            btnUpdate.Click += BtnUpdate_Click;

            actionPanel.Controls.AddRange(new Control[] { lblStatus, cmbStatus, btnUpdate });
            LoadOrders();
        }

        private void LoadOrders()
        {
            gridOrders.DataSource = _service.GetAllOrders();
            if (gridOrders.Columns.Contains("OrderID")) gridOrders.Columns["OrderID"].Visible = false;
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
