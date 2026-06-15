using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.Business.Services;
using SmartMed.Data.Models;

namespace SmartMed.UI
{
    public class PlaceOrderForm : Form
    {
        private DataGridView gridMedicines, gridCart;
        private readonly MedicineService _medicineService = new MedicineService();
        private readonly OrderService _orderService = new OrderService();
        private readonly List<OrderItemRecord> _cart = new List<OrderItemRecord>();

        public PlaceOrderForm()
        {
            Text = "Place Order";
            Size = new Size(900, 520);
            StartPosition = FormStartPosition.CenterParent;

            gridMedicines = new DataGridView { Location = new Point(20, 20), Size = new Size(400, 350), ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            gridCart = new DataGridView { Location = new Point(440, 20), Size = new Size(420, 300), ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

            var lblQty = new Label { Text = "Qty:", Location = new Point(20, 380), AutoSize = true };
            var txtQty = new TextBox { Location = new Point(60, 377), Width = 50, Text = "1" };
            var btnAdd = new Button { Text = "Add to Cart", Location = new Point(120, 375), Width = 100 };
            btnAdd.Click += (s, e) => AddToCart(txtQty);

            var btnPlace = new Button { Text = "Place Order", Location = new Point(440, 340), Width = 120, BackColor = Color.FromArgb(180, 203, 249) };
            var btnClear = new Button { Text = "Clear Cart", Location = new Point(580, 340), Width = 100 };
            btnPlace.Click += BtnPlace_Click;
            btnClear.Click += (s, e) => { _cart.Clear(); RefreshCart(); };

            Controls.AddRange(new Control[] { gridMedicines, gridCart, lblQty, txtQty, btnAdd, btnPlace, btnClear });
            gridMedicines.DataSource = _medicineService.GetAll();
            RefreshCart();
        }

        private void AddToCart(TextBox txtQty)
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
