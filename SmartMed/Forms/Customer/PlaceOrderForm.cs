using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class PlaceOrderForm : EmbeddedPageForm
    {
        private readonly OrderService _orders;
        private readonly MedicineService _medicines;
        private readonly bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public PlaceOrderForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _orders = new OrderService();
                _medicines = new MedicineService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
        }

        protected override void DoRefreshPage() => RefreshCart();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            UiTheme.SetGridDataSource(gridCart, new object[0]);
            BeautifyCartGrid();
            lblTotal.Text = "Checkout total: -";
            lblRxNote.Visible = false;
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridCart);
        }

        private void PanelGridOuter_Paint(object sender, PaintEventArgs e) =>
            UiTheme.DrawOuterPanelBorder(panelGridOuter, e);

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            gridCart.ReadOnly = false;
            gridCart.EditMode = DataGridViewEditMode.EditOnEnter;
            gridCart.SelectionMode = DataGridViewSelectionMode.CellSelect;
        }

        private void RefreshCart()
        {
            if (gridCart == null) return;

            if (PreferDesignTimePreview())
                return;

            SyncCartPrices();

            UiTheme.SetGridDataSource(gridCart, CartService.Items.Select(l => new
            {
                l.MedicineID,
                l.MedicineName,
                l.Quantity,
                ListPrice = $"LKR {l.ListPrice:N2}",
                UnitPrice = $"LKR {l.UnitPrice:N2}",
                l.DiscountDisplay,
                l.PromoDisplay,
                Applied = l.OfferDisplay,
                Subtotal = $"LKR {l.Subtotal:N2}",
                Rx = l.RequiresPrescription ? "Yes" : "No",
                Prescription = l.PrescriptionDisplay
            }).ToList());
            BeautifyCartGrid();
            UpdateCartTotals();
            lblRxNote.Visible = CartService.SelectedRequiresPrescription;
        }

        private void SyncCartPrices()
        {
            if (!_servicesReady || _medicines == null) return;
            // Refresh prices before checkout so the customer pays the current amount.
            CartService.RefreshPrices(_medicines);
        }

        private void UpdateCartTotals()
        {
            var selectedCount = CartService.SelectedItemCount;
            var cartCount = CartService.ItemCount;
            if (selectedCount == cartCount)
                lblTotal.Text = $"Checkout total: LKR {CartService.SelectedTotal:N2} ({selectedCount} items)";
            else
                lblTotal.Text =
                    $"Checkout total: LKR {CartService.SelectedTotal:N2} ({selectedCount} of {cartCount} items selected)";
        }

        private void BeautifyCartGrid()
        {
            if (gridCart.Columns.Contains("MedicineID"))
                gridCart.Columns["MedicineID"].Visible = false;

            if (!gridCart.Columns.Contains("Checkout"))
            {
                var checkoutCol = new DataGridViewCheckBoxColumn
                {
                    Name = "Checkout",
                    HeaderText = "Checkout",
                    Width = 72,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                gridCart.Columns.Insert(0, checkoutCol);
            }
            else if (gridCart.Columns["Checkout"].DisplayIndex != 0)
            {
                gridCart.Columns["Checkout"].DisplayIndex = 0;
            }

            EnsureCheckoutColumnEditable();

            if (gridCart.Columns.Contains("DiscountDisplay"))
                gridCart.Columns["DiscountDisplay"].HeaderText = "Discount";
            if (gridCart.Columns.Contains("PromoDisplay"))
                gridCart.Columns["PromoDisplay"].HeaderText = "Promo";
            UiTheme.BeautifyGridHeaders(gridCart);

            if (!gridCart.Columns.Contains("UploadBtn"))
            {
                gridCart.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "UploadBtn",
                    HeaderText = "",
                    Text = "Upload Rx",
                    UseColumnTextForButtonValue = true,
                    Width = 110,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }

            if (!gridCart.Columns.Contains("PreviewBtn"))
            {
                gridCart.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "PreviewBtn",
                    HeaderText = "",
                    Text = "Preview",
                    UseColumnTextForButtonValue = true,
                    Width = 90,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }

            foreach (DataGridViewRow row in gridCart.Rows)
            {
                if (row.IsNewRow) continue;

                if (gridCart.Columns.Contains("Checkout"))
                {
                    if (PreferDesignTimePreview())
                        row.Cells["Checkout"].Value = true;
                    else
                    {
                        var medicineId = Convert.ToInt32(row.Cells["MedicineID"].Value);
                        var line = CartService.Items.FirstOrDefault(l => l.MedicineID == medicineId);
                        if (line != null)
                            row.Cells["Checkout"].Value = line.SelectedForCheckout;
                    }
                }

                var isRx = string.Equals(row.Cells["Rx"].Value?.ToString(), "Yes", StringComparison.OrdinalIgnoreCase);
                var prescription = row.Cells["Prescription"].Value?.ToString() ?? string.Empty;
                var hasUploaded = isRx
                    && !string.IsNullOrWhiteSpace(prescription)
                    && !string.Equals(prescription, "Required", StringComparison.OrdinalIgnoreCase);

                var uploadCell = row.Cells["UploadBtn"] as DataGridViewButtonCell;
                if (uploadCell != null)
                    uploadCell.Value = isRx ? (hasUploaded ? "Change" : "Upload Rx") : string.Empty;

                var previewCell = row.Cells["PreviewBtn"] as DataGridViewButtonCell;
                if (previewCell != null)
                    previewCell.Value = hasUploaded ? "Preview" : string.Empty;
            }

            if (gridCart.Columns.Contains("Prescription"))
            {
                gridCart.Columns["Prescription"].DefaultCellStyle.ForeColor = UiTheme.AdminTeal;
                gridCart.Columns["Prescription"].DefaultCellStyle.Font = UiTheme.UiFont;
            }
        }

        private void EnsureCheckoutColumnEditable()
        {
            if (!gridCart.Columns.Contains("Checkout")) return;

            gridCart.Columns["Checkout"].ReadOnly = false;
            foreach (DataGridViewColumn col in gridCart.Columns)
            {
                if (col.Name != "Checkout")
                    col.ReadOnly = true;
            }
        }

        private void GridCart_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridCart.IsCurrentCellDirty)
                gridCart.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        private void GridCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridCart.Columns[e.ColumnIndex].Name != "Checkout") return;
            SyncCheckoutSelection(e.RowIndex);
        }

        private void SyncCheckoutSelection(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= gridCart.Rows.Count) return;
            var row = gridCart.Rows[rowIndex];
            var id = Convert.ToInt32(row.Cells["MedicineID"].Value);
            var line = CartService.Items.FirstOrDefault(l => l.MedicineID == id);
            if (line == null) return;

            line.SelectedForCheckout = Convert.ToBoolean(row.Cells["Checkout"].Value);
            UpdateCartTotals();
            lblRxNote.Visible = CartService.SelectedRequiresPrescription;
        }

        private void GridCart_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var colName = gridCart.Columns[e.ColumnIndex].Name;
            if (colName == "PreviewBtn")
            {
                PreviewPrescriptionForRow(e.RowIndex);
                return;
            }

            if (colName != "UploadBtn") return;

            var row = gridCart.Rows[e.RowIndex];
            if (!string.Equals(row.Cells["Rx"].Value?.ToString(), "Yes", StringComparison.OrdinalIgnoreCase))
                return;

            using (var dialog = new OpenFileDialog
            {
                Filter = "Prescription files|*.pdf;*.jpg;*.jpeg;*.png;*.bmp|All files|*.*",
                Title = "Select Prescription"
            })
            {
                if (dialog.ShowDialog(FindForm()) != DialogResult.OK) return;
                var id = Convert.ToInt32(row.Cells["MedicineID"].Value);
                CartService.SetPrescription(id, dialog.FileName);
                RefreshCart();
            }
        }

        private void GridCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridCart.Columns[e.ColumnIndex].Name != "Prescription") return;
            PreviewPrescriptionForRow(e.RowIndex);
        }

        private void GridCart_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (UiTheme.IsSelectedRow(gridCart, e.RowIndex))
            {
                UiTheme.ApplySelectedRowCellStyle(e.CellStyle);
                return;
            }
            if (gridCart.Columns[e.ColumnIndex].Name != "Prescription") return;

            var value = e.Value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(value) || value == "—" || value == "Required")
            {
                e.CellStyle.ForeColor = UiTheme.AdminMuted;
                e.CellStyle.Font = UiTheme.UiFont;
                return;
            }

            e.CellStyle.ForeColor = UiTheme.AdminTeal;
            e.CellStyle.Font = UiTheme.UiFontBold;
        }

        private void PreviewPrescriptionForRow(int rowIndex)
        {
            var row = gridCart.Rows[rowIndex];
            if (!string.Equals(row.Cells["Rx"].Value?.ToString(), "Yes", StringComparison.OrdinalIgnoreCase))
                return;

            var medicineId = Convert.ToInt32(row.Cells["MedicineID"].Value);
            var line = CartService.Items.FirstOrDefault(l => l.MedicineID == medicineId);
            var filePath = line?.PrescriptionPath;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                SmartMedMessageBox.Show("Upload a prescription first.", "Preview Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!System.IO.File.Exists(filePath))
            {
                SmartMedMessageBox.Show("Prescription file is no longer available on this device.", "Preview Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show($"Could not open prescription file.\n{ex.Message}", "Preview Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (gridCart.CurrentRow == null) return;
            var id = Convert.ToInt32(gridCart.CurrentRow.Cells["MedicineID"].Value);
            CartService.Remove(id, _medicines);
            RefreshCart();
        }

        private void BtnClearCart_Click(object sender, EventArgs e)
        {
            CartService.Clear(_medicines);
            RefreshCart();
        }

        private void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                if (CartService.ItemCount == 0)
                    throw new InvalidOperationException("Your cart is empty.");

                var selected = CartService.SelectedItems.ToList();
                if (selected.Count == 0)
                    throw new InvalidOperationException("Select at least one item to checkout.");

                var missing = CartService.SelectedMissingPrescriptions.Select(l => l.MedicineName).ToList();
                if (missing.Count > 0)
                    throw new InvalidOperationException(
                        "Upload a prescription for selected Rx items: " + string.Join(", ", missing));

                SyncCartPrices();
                UpdateCartTotals();

                PaymentResult payment;
                using (var paymentDialog = new PaymentCheckoutDialog(CartService.SelectedTotal))
                {
                    if (paymentDialog.ShowDialog(FindForm()) != DialogResult.OK || paymentDialog.Result == null)
                        return;
                    payment = paymentDialog.Result;
                }

                var orderId = _orders.PlaceOrder(
                    customer.CustomerID,
                    selected,
                    payment.Method,
                    payment.Status,
                    payment.Reference);
                CartService.RemoveMany(selected.Select(l => l.MedicineID));
                RefreshCart();
                SmartMedMessageBox.Show(
                    $"Order placed successfully. Reference #SM-{orderId:D4}\nPayment: {payment.Method} ({payment.Status})",
                    "Order",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
