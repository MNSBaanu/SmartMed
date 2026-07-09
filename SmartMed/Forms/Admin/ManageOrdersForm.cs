using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ManageOrdersForm : EmbeddedPageForm
    {
        private const int PageSize = 10;

        private OrderService _orders;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        private List<OrderRow> _allRows = new List<OrderRow>();
        private List<OrderRow> _filteredRows = new List<OrderRow>();
        private int _currentPage = 1;
        private int? _selectedOrderId;

        public ManageOrdersForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _orders = new OrderService();
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

        protected override void DoRefreshPage() => LoadOrders();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            var now = DateTime.Now;
            _allRows = new List<OrderRow>
            {
                new OrderRow
                {
                    OrderID = 1,
                    OrderRef = "#ORD-0001",
                    CustomerName = "Jane Perera",
                    CustomerRef = "PAT-001-01",
                    OrderDate = now.AddDays(-1).ToString("MMM dd, yyyy HH:mm"),
                    TotalAmount = "LKR 1,250.00",
                    Status = OrderService.StatusPending,
                    RawStatus = OrderService.StatusPending,
                    RxStatus = "Approved",
                    Prescription = "Uploaded",
                    OrderDateValue = now.AddDays(-1),
                    IsNew = true
                },
                new OrderRow
                {
                    OrderID = 2,
                    OrderRef = "#ORD-0002",
                    CustomerName = "Kamal Silva",
                    CustomerRef = "PAT-002-02",
                    OrderDate = now.AddDays(-3).ToString("MMM dd, yyyy HH:mm"),
                    TotalAmount = "LKR 890.00",
                    Status = OrderService.StatusDelivered,
                    RawStatus = OrderService.StatusDelivered,
                    RxStatus = "—",
                    Prescription = "—",
                    OrderDateValue = now.AddDays(-3)
                }
            };
            _filteredRows = _allRows;
            _currentPage = 1;
            BindPage();
            UpdateStats();
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridOrders);
            UiTheme.StyleTextBox(txtSearch);
            UiTheme.StyleComboBox(cmbStatus);

            WirePanelBorder(panelFilterOuter);
            WirePanelBorder(panelGridOuter);
            WireStatCard(panelStatVolume, UiTheme.AdminTeal);
            WireStatCard(panelStatAvg, Color.FromArgb(41, 163, 122));
            WireStatCard(panelStatFlags, UiTheme.Danger);
            WireSearchWrapBorder(panelSearchWrap);

            if (cmbStatus.Items.Count == 0)
            {
                cmbStatus.Items.AddRange(new object[]
                {
                    "All Statuses",
                    OrderService.StatusPending,
                    OrderService.StatusReadyForPickup,
                    OrderService.StatusDelivered,
                    "Flagged"
                });
                cmbStatus.SelectedIndex = 0;
            }
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

        private static void WireSearchWrapBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "search-wrap") return;
            panel.Tag = "search-wrap";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private static void WireStatCard(Panel card, Color accent)
        {
            if (card == null || card.Tag as string == "dash-stat") return;
            card.Tag = "dash-stat";
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
                using (var brush = new SolidBrush(accent))
                    e.Graphics.FillRectangle(brush, 0, 0, 4, rect.Height);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnApplyFilters.Click += (s, e) => ApplyFilters();
            btnExport.Click += BtnExport_Click;
            btnPagePrev.Click += (s, e) => ChangePage(-1);
            btnPageNext.Click += (s, e) => ChangePage(1);
            gridOrders.CellFormatting += GridOrders_CellFormatting;
            gridOrders.CellContentClick += GridOrders_CellContentClick;
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            AdminOrderAlerts.AlertsChanged += (s, e) =>
            {
                if (IsDisposed || !_servicesReady) return;
                if (InvokeRequired)
                {
                    BeginInvoke(new Action(() => DoRefreshPage()));
                    return;
                }
                DoRefreshPage();
            };
        }

        private void LoadOrders()
        {
            if (!_servicesReady) return;

            _allRows = _orders.GetAll()
                .OrderByDescending(o => o.OrderDate)
                .Select(BuildRow)
                .ToList();
            ApplyFilters();
        }

        private OrderRow BuildRow(Order order)
        {
            var rxStatus = _orders.GetPrescriptionStatusDisplay(order.OrderID);
            var displayStatus = order.Status;
            if (string.Equals(rxStatus, PrescriptionService.StatusRejected, StringComparison.OrdinalIgnoreCase))
                displayStatus = "Flagged";

            return new OrderRow
            {
                OrderID = order.OrderID,
                OrderRef = $"#ORD-{order.OrderID:D4}",
                CustomerName = order.CustomerName ?? "—",
                CustomerRef = FormatPatientRef(order.CustomerID, order.OrderID),
                OrderDate = order.OrderDate.ToString("MMM dd, yyyy HH:mm"),
                TotalAmount = $"LKR {order.TotalAmount:N2}",
                Status = displayStatus,
                RawStatus = order.Status,
                RxStatus = rxStatus,
                Prescription = _orders.GetPrescriptionDisplay(order.OrderID),
                OrderDateValue = order.OrderDate,
                IsNew = AdminOrderAlerts.IsNew(order)
            };
        }

        private static string FormatPatientRef(int customerId, int orderId) =>
            $"PAT-{customerId:D3}-{(orderId % 100):D2}";

        private void ApplyFilters()
        {
            IEnumerable<OrderRow> rows = _allRows;

            var term = txtSearch?.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(term))
            {
                rows = rows.Where(r =>
                    r.OrderRef.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                    || r.CustomerName.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                    || r.CustomerRef.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                    || r.Status.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            var statusFilter = cmbStatus?.SelectedItem?.ToString() ?? "All Statuses";
            if (statusFilter != "All Statuses")
            {
                if (statusFilter == "Flagged")
                    rows = rows.Where(r => r.Status == "Flagged");
                else
                    rows = rows.Where(r => r.RawStatus == statusFilter);
            }

            if (chkDateRange != null && chkDateRange.Checked)
            {
                var from = dtpFrom.Value.Date;
                var to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
                rows = rows.Where(r => r.OrderDateValue >= from && r.OrderDateValue <= to);
            }

            _filteredRows = rows.ToList();
            _currentPage = 1;
            BindPage();
            UpdateStats();
        }

        private void BindPage()
        {
            var totalPages = Math.Max(1, (int)Math.Ceiling(_filteredRows.Count / (double)PageSize));
            if (_currentPage > totalPages) _currentPage = totalPages;
            if (_currentPage < 1) _currentPage = 1;

            var pageRows = _filteredRows
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .Select(r => new
                {
                    r.OrderID,
                    r.OrderRef,
                    Customer = $"{r.CustomerName}\n{r.CustomerRef}",
                    r.OrderDate,
                    r.TotalAmount,
                    r.Status,
                    View = "View",
                    Edit = "Edit"
                })
                .ToList();

            UiTheme.SetGridDataSource(gridOrders, pageRows);
            HideInternalColumns();
            UiTheme.BeautifyGridHeaders(gridOrders);

            if (gridOrders.Columns.Contains("Customer"))
                gridOrders.Columns["Customer"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            lblPageInfo.Text = $"Page {_currentPage} / {totalPages}";
            btnPagePrev.Enabled = _currentPage > 1;
            btnPageNext.Enabled = _currentPage < totalPages;
        }

        private void HideInternalColumns()
        {
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
        }

        private void UpdateStats()
        {
            lblVolume.Text = _filteredRows.Count.ToString("N0");

            if (_filteredRows.Count == 0)
            {
                lblAvgTime.Text = "—";
                lblFlags.Text = "0";
                return;
            }

            var delivered = _filteredRows.Where(r => r.RawStatus == OrderService.StatusDelivered).ToList();
            if (delivered.Count > 0)
            {
                var avgMinutes = delivered.Average(r => (DateTime.Now - r.OrderDateValue).TotalMinutes);
                lblAvgTime.Text = avgMinutes >= 60
                    ? $"{avgMinutes / 60:0.#}h"
                    : $"{avgMinutes:0.#}m";
            }
            else
            {
                lblAvgTime.Text = "—";
            }

            lblFlags.Text = _filteredRows.Count(r => r.Status == "Flagged").ToString("D2");
        }

        private void ChangePage(int delta)
        {
            _currentPage += delta;
            BindPage();
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (gridOrders.CurrentRow == null)
            {
                _selectedOrderId = null;
                return;
            }
            var cell = gridOrders.CurrentRow.Cells["OrderID"];
            if (cell?.Value != null)
                _selectedOrderId = Convert.ToInt32(cell.Value);
        }

        private void GridOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var orderIdCell = gridOrders.Rows[e.RowIndex].Cells["OrderID"];
            OrderRow row = null;
            if (orderIdCell?.Value != null)
            {
                var orderId = Convert.ToInt32(orderIdCell.Value);
                row = _filteredRows.FirstOrDefault(r => r.OrderID == orderId);
            }

            if (row?.IsNew == true)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 251, 235);
                e.CellStyle.Font = UiTheme.UiFontBold;
            }

            if (gridOrders.Columns[e.ColumnIndex].Name != "Status") return;

            var status = e.Value?.ToString() ?? "";
            if (string.Equals(status, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase)
                || status == "Flagged")
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 218, 214);
                e.CellStyle.ForeColor = Color.FromArgb(104, 57, 61);
                e.CellStyle.Font = UiTheme.UiFontBold;
                if (status == OrderService.StatusPending)
                    e.Value = "Pending";
            }
            else if (string.Equals(status, OrderService.StatusReadyForPickup, StringComparison.OrdinalIgnoreCase)
                     || string.Equals(status, "Ready", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(199, 234, 228);
                e.CellStyle.ForeColor = UiTheme.AdminTeal;
                e.CellStyle.Font = UiTheme.UiFontBold;
                e.Value = "Ready";
            }
            else if (string.Equals(status, OrderService.StatusDelivered, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(184, 237, 226);
                e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
        }

        private void GridOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var colName = gridOrders.Columns[e.ColumnIndex].Name;
            if (colName != "View" && colName != "Edit") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            if (colName == "View")
                ShowOrderDetails(orderId);
            else
                ShowStatusEditor(orderId);
        }

        private void ShowOrderDetails(int orderId)
        {
            var order = _orders.GetById(orderId);
            if (order == null) return;

            var items = _orders.GetItems(orderId);
            var lines = new List<string>
            {
                $"Order: #ORD-{order.OrderID:D4}",
                $"Customer: {order.CustomerName} ({FormatPatientRef(order.CustomerID, order.OrderID)})",
                $"Date: {order.OrderDate:MMM dd, yyyy HH:mm}",
                $"Status: {order.Status}",
                $"Total: LKR {order.TotalAmount:N2}",
                $"Prescription: {_orders.GetPrescriptionDisplay(orderId)}",
                $"Rx Status: {_orders.GetPrescriptionStatusDisplay(orderId)}",
                "",
                "Line items:"
            };
            foreach (var item in items)
                lines.Add($"  • {item.MedicineName} x{item.Quantity} — LKR {item.Subtotal:N2}");

            var result = MessageBox.Show(
                string.Join(Environment.NewLine, lines) + Environment.NewLine + Environment.NewLine + "Open prescription file?",
                "Order Details",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
                OpenPrescription(orderId);
        }

        private void ShowStatusEditor(int orderId)
        {
            var order = _orders.GetById(orderId);
            if (order == null) return;

            var nextStatuses = OrderService.GetAllowedNextStatuses(order.Status);
            if (nextStatuses.Count == 0)
            {
                MessageBox.Show("This order cannot be updated further.", "Manage Orders",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new Form
            {
                Text = $"Update Order #ORD-{orderId:D4}",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(400, 200),
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var cmb = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Left = 16,
                    Top = 48,
                    Width = 320
                };
                UiTheme.StyleComboBox(cmb);
                foreach (var status in nextStatuses)
                    cmb.Items.Add(status);
                cmb.SelectedIndex = 0;

                dlg.Controls.Add(new Label
                {
                    Text = $"Current status: {order.Status}",
                    Left = 16,
                    Top = 16,
                    AutoSize = true,
                    ForeColor = UiTheme.AdminOnSurface,
                    BackColor = UiTheme.AdminSurface
                });
                dlg.Controls.Add(new Label
                {
                    Text = "New status:",
                    Left = 16,
                    Top = 30,
                    AutoSize = true,
                    ForeColor = UiTheme.AdminMuted,
                    BackColor = UiTheme.AdminSurface
                });
                dlg.Controls.Add(cmb);

                var btnRx = AdminUiHelpers.CreateWinButton("View Rx", false, 90);
                btnRx.Left = 16;
                btnRx.Top = 100;
                btnRx.Click += (s, e) => OpenPrescription(orderId);

                var btnVerify = AdminUiHelpers.CreateWinButton("Verify Rx", true, 90);
                btnVerify.Left = 112;
                btnVerify.Top = 100;
                btnVerify.Click += (s, e) =>
                {
                    try
                    {
                        _orders.VerifyPrescription(orderId);
                        MessageBox.Show("Prescription verified.", "SmartMed");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Verify Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                var btnReject = AdminUiHelpers.CreateWinButton("Reject Rx", false, 90);
                btnReject.Left = 208;
                btnReject.Top = 100;
                btnReject.Click += (s, e) =>
                {
                    if (MessageBox.Show(
                            "Reject this prescription? The order cannot move forward until a valid prescription is provided.",
                            "Reject Prescription", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                        return;

                    try
                    {
                        _orders.RejectPrescription(orderId);
                        MessageBox.Show("Prescription rejected.", "SmartMed");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Reject Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                var btnSave = AdminUiHelpers.CreateWinButton("Update", true, 90);
                btnSave.Left = 246;
                btnSave.Top = 140;
                btnSave.DialogResult = DialogResult.OK;
                var btnClose = AdminUiHelpers.CreateWinButton("Close", false, 90);
                btnClose.Left = 150;
                btnClose.Top = 140;
                btnClose.DialogResult = DialogResult.Cancel;

                dlg.Controls.Add(btnRx);
                dlg.Controls.Add(btnVerify);
                dlg.Controls.Add(btnReject);
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnClose);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnClose;

                if (order.Status == OrderService.StatusPending)
                {
                    var btnCancelOrder = AdminUiHelpers.CreateWinButton("Cancel Order", false, 110);
                    btnCancelOrder.Left = 16;
                    btnCancelOrder.Top = 140;
                    btnCancelOrder.Click += (s, e) =>
                    {
                        if (MessageBox.Show("Cancel this order and restore stock?", "Confirm Cancel",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            return;

                        try
                        {
                            _orders.CancelOrderAsAdmin(orderId);
                            dlg.DialogResult = DialogResult.Cancel;
                            dlg.Close();
                            LoadOrders();
                            MessageBox.Show("Order cancelled and stock restored.", "SmartMed",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    };
                    dlg.Controls.Add(btnCancelOrder);
                    dlg.ClientSize = new Size(400, 220);
                    btnSave.Top = 160;
                    btnClose.Top = 160;
                    btnCancelOrder.Top = 160;
                }

                if (dlg.ShowDialog(FindForm()) != DialogResult.OK || cmb.SelectedItem == null)
                    return;

                try
                {
                    _orders.UpdateStatus(orderId, cmb.SelectedItem.ToString());
                    LoadOrders();
                    MessageBox.Show("Order status updated.", "SmartMed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void OpenPrescription(int orderId)
        {
            var filePath = _orders.GetPrescriptionFilePath(orderId);
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("No prescription file for this order.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!System.IO.File.Exists(filePath))
            {
                MessageBox.Show("Prescription file is not available.", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prescription", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_filteredRows.Count == 0)
            {
                MessageBox.Show("No orders to export.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                FileName = $"orders_{DateTime.Now:yyyyMMdd}.csv"
            })
            {
                if (dlg.ShowDialog(FindForm()) != DialogResult.OK) return;
                try
                {
                    var orders = _filteredRows
                        .Select(r => _orders.GetById(r.OrderID))
                        .Where(o => o != null)
                        .ToList();
                    _orders.ExportOrdersToCsv(orders, dlg.FileName);
                    MessageBox.Show("Export complete.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private sealed class OrderRow
        {
            public int OrderID { get; set; }
            public string OrderRef { get; set; }
            public string CustomerName { get; set; }
            public string CustomerRef { get; set; }
            public string OrderDate { get; set; }
            public DateTime OrderDateValue { get; set; }
            public string TotalAmount { get; set; }
            public string Status { get; set; }
            public string RawStatus { get; set; }
            public string RxStatus { get; set; }
            public string Prescription { get; set; }
            public bool IsNew { get; set; }
        }
    }
}
