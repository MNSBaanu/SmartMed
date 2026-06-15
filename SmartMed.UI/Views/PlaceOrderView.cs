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
    public class PlaceOrderView : UserControl
    {
        private DataGridView gridMedicines, gridCart;
        private readonly MedicineService _medicineService = new MedicineService();
        private readonly OrderService _orderService = new OrderService();
        private readonly List<OrderItemRecord> _cart = new List<OrderItemRecord>();

        public PlaceOrderView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Place Order");
            header.Dock = DockStyle.Top;
            Controls.Add(header);

            var split = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, ClinicalPrecisionTheme.StackMd, 0, 0) };
            Controls.Add(split);

            gridMedicines = new DataGridView
            {
                Location = new Point(0, 0),
                Size = new Size(400, 320),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(gridMedicines);

            var lblCart = UiFactory.CreateFieldLabel("Shopping Cart");
            lblCart.Location = new Point(420, 0);

            gridCart = new DataGridView
            {
                Location = new Point(420, 24),
                Size = new Size(400, 260),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiFactory.ApplyDataGridStyle(gridCart);

            var lblQty = UiFactory.CreateFieldLabel("Qty:");
            lblQty.Location = new Point(0, 330);
            var txtQty = new TextBox { Location = new Point(40, 326), Width = 50, Text = "1" };
            UiFactory.ApplyTextBoxStyle(txtQty, 50);
            var btnAdd = UiFactory.CreatePrimaryButton("Add to Cart", 110);
            btnAdd.Location = new Point(100, 324);
            btnAdd.Click += (s, e) => AddToCart(txtQty);

            var btnPlace = UiFactory.CreatePrimaryButton("Place Order", 120);
            var btnClear = UiFactory.CreateSecondaryButton("Clear Cart", 100);
            btnPlace.Location = new Point(420, 300);
            btnClear.Location = new Point(550, 300);
            btnPlace.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPlace.Click += BtnPlace_Click;
            btnClear.Click += (s, e) => { _cart.Clear(); RefreshCart(); };

            split.Controls.AddRange(new Control[] { gridMedicines, lblCart, gridCart, lblQty, txtQty, btnAdd, btnPlace, btnClear });

            split.Resize += (s, e) =>
            {
                int half = (split.ClientSize.Width - 20) / 2;
                gridMedicines.Width = half;
                gridMedicines.Height = split.ClientSize.Height - 50;
                lblCart.Location = new Point(half + 20, 0);
                gridCart.Location = new Point(half + 20, 24);
                gridCart.Width = split.ClientSize.Width - half - 20;
                gridCart.Height = split.ClientSize.Height - 80;
                btnPlace.Location = new Point(half + 20, split.ClientSize.Height - 44);
                btnClear.Location = new Point(half + 150, split.ClientSize.Height - 44);
                lblQty.Location = new Point(0, split.ClientSize.Height - 40);
                txtQty.Location = new Point(40, split.ClientSize.Height - 44);
                btnAdd.Location = new Point(100, split.ClientSize.Height - 46);
            };

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
