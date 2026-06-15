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
            StitchUiHelper.SetupPageHeader(pageHeader, "Place Order");
            StitchUiHelper.StyleGridCard(medicinesCard);
            StitchUiHelper.StyleGridCard(cartCard);
            StitchUiHelper.ApplySectionHeader(lblCart);
            StitchUiHelper.ApplyFieldLabel(lblQty);
            StitchUiHelper.ApplyPrimaryAccentButton(btnAdd);
            StitchUiHelper.ApplyPrimaryAccentButton(btnPlace);
            StitchUiHelper.ApplySecondaryButton(btnClear);

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
            int gap = 20;
            int half = (splitPanel.ClientSize.Width - gap) / 2;
            int top = 16;
            int height = splitPanel.ClientSize.Height - top;
            medicinesCard.SetBounds(0, top, half, height);
            cartCard.SetBounds(half + gap, top, splitPanel.ClientSize.Width - half - gap, height);
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
