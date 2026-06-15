using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.Data.Models;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public partial class PlaceOrderView : UserControl
    {
        private readonly MedicineService _medicineService = new MedicineService();
        private readonly OrderService _orderService = new OrderService();
        private readonly List<OrderItemRecord> _cart = new List<OrderItemRecord>();

        public PlaceOrderView()
        {
            InitializeComponent();
            UiFactory.ApplyViewChrome(this);
        }

        private void PlaceOrderView_Load(object sender, EventArgs e)
        {
            UiFactory.ApplyDataGridStyle(gridMedicines);
            UiFactory.ApplyDataGridStyle(gridCart);
            gridMedicines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            UiFactory.ApplyTextBoxStyle(txtQty, 50);

            LayoutSplit();
            splitPanel.Resize += SplitPanel_Resize;
            Resize += (s, ev) => LayoutSplit();

            if (UiFactory.IsDesignMode(this)) return;
            gridMedicines.DataSource = _medicineService.GetAll();
            RefreshCart();
        }

        private void SplitPanel_Resize(object sender, EventArgs e) => LayoutSplit();

        private void LayoutSplit()
        {
            int half = (splitPanel.ClientSize.Width - 20) / 2;
            gridMedicines.Width = half;
            gridMedicines.Height = splitPanel.ClientSize.Height - 50;
            lblCart.Location = new Point(half + 20, 0);
            gridCart.Location = new Point(half + 20, 24);
            gridCart.Width = splitPanel.ClientSize.Width - half - 20;
            gridCart.Height = splitPanel.ClientSize.Height - 80;
            btnPlace.Location = new Point(half + 20, splitPanel.ClientSize.Height - 44);
            btnClear.Location = new Point(half + 150, splitPanel.ClientSize.Height - 44);
            lblQty.Location = new Point(0, splitPanel.ClientSize.Height - 40);
            txtQty.Location = new Point(40, splitPanel.ClientSize.Height - 44);
            btnAdd.Location = new Point(100, splitPanel.ClientSize.Height - 46);
        }

        private void BtnAdd_Click(object sender, EventArgs e) => AddToCart();

        private void AddToCart()
        {
            if (!(gridMedicines.CurrentRow?.DataBoundItem is MedicineItem medicine))
            {
                MessageBox.Show("Select a medicine.");
                return;
            }
            if (!int.TryParse(txtQty.Text, out int qty) || qty <= 0) { MessageBox.Show("Enter valid quantity."); return; }
            if (qty > medicine.StockQuantity) { MessageBox.Show("Insufficient stock."); return; }

            _cart.Add(new OrderItemRecord
            {
                MedicineID = medicine.MedicineID,
                MedicineName = medicine.MedicineName,
                Quantity = qty,
                UnitPrice = medicine.Price,
                Subtotal = medicine.Price * qty
            });
            RefreshCart();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            _cart.Clear();
            RefreshCart();
        }

        private void RefreshCart()
        {
            gridCart.DataSource = null;
            gridCart.DataSource = _cart;
        }

        private void BtnPlace_Click(object sender, EventArgs e)
        {
            try
            {
                int orderId = _orderService.PlaceOrder(Session.CurrentCustomer.CustomerID, _cart);
                MessageBox.Show($"Order placed successfully. Order ID: {orderId}");
                _cart.Clear();
                RefreshCart();
                gridMedicines.DataSource = _medicineService.GetAll();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
