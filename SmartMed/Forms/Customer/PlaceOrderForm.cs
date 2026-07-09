using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class PlaceOrderForm : EmbeddedPageForm
    {
        private OrderService _orders;
        private MedicineService _medicines;
        private bool _servicesReady;
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

            UiTheme.SetGridDataSource(gridCart, DesignTimePreviewData.CartRows());
            BeautifyCartGrid();
            lblTotal.Text = "Total: LKR 975.00 (2 items)";
            lblRxNote.Visible = true;
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridCart);

            WirePanelBorder(panelGridOuter);
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "dash-border") return;
            panel.Tag = "dash-border";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnRemoveSelected.Click += BtnRemove_Click;
            btnClearCart.Click += (s, e) =>
            {
                CartService.Clear(_medicines);
                RefreshCart();
            };
            btnPlaceOrder.Click += BtnPlace_Click;
            gridCart.CellContentClick += GridCart_CellContentClick;
            gridCart.CellClick += GridCart_CellClick;
            gridCart.CellFormatting += GridCart_CellFormatting;
        }

        private void RefreshCart()
        {
            if (gridCart == null) return;

            if (PreferDesignTimePreview())
                return;

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
            lblTotal.Text = $"Total: LKR {CartService.Total:N2} ({CartService.ItemCount} items)";
            lblRxNote.Visible = CartService.RequiresPrescription;
        }

        private void BeautifyCartGrid()
        {
            if (gridCart.Columns.Contains("MedicineID"))
                gridCart.Columns["MedicineID"].Visible = false;
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

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (gridCart.CurrentRow == null) return;
            var id = Convert.ToInt32(gridCart.CurrentRow.Cells["MedicineID"].Value);
            CartService.Remove(id, _medicines);
            RefreshCart();
        }

        private void BtnPlace_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var missing = CartService.MissingPrescriptions.Select(l => l.MedicineName).ToList();
                if (missing.Count > 0)
                    throw new InvalidOperationException(
                        "Upload a prescription for: " + string.Join(", ", missing));

                var orderId = _orders.PlaceOrder(customer.CustomerID, CartService.Items, CartService.FirstPrescriptionPath);
                CartService.Discard();
                RefreshCart();
                SmartMedMessageBox.Show($"Order placed successfully. Reference #SM-{orderId:D4}", "Order",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Order Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
