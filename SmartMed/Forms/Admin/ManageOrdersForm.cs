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

        private readonly OrderService _orders;
        private readonly bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        private List<OrderRow> _allRows = new List<OrderRow>();
        private List<OrderRow> _filteredRows = new List<OrderRow>();
        private int _currentPage = 1;
        private int? _selectedOrderId;
        private bool _suppressGridEvents;
        private string _statusEditOriginal;

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
            _allRows = new List<OrderRow>();
            _filteredRows = _allRows;
            _currentPage = 1;
            BindPage();
            lblVolume.Text = "-";
            lblAvgTime.Text = "-";
            lblFlags.Text = "-";
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
                    OrderService.StatusCancelled,
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

            // Static alert bus is not a Designer control.
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

        private void BtnApplyFilters_Click(object sender, EventArgs e) => ApplyFilters();

        private void BtnPagePrev_Click(object sender, EventArgs e) => ChangePage(-1);

        private void BtnPageNext_Click(object sender, EventArgs e) => ChangePage(1);

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
            if (!string.Equals(order.Status, OrderService.StatusCancelled, StringComparison.OrdinalIgnoreCase)
                && string.Equals(rxStatus, PrescriptionService.StatusRejected, StringComparison.OrdinalIgnoreCase))
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
                HasPrescription = _orders.OrderHasPrescription(order.OrderID),
                OrderDateValue = order.OrderDate,
                IsNew = AdminOrderAlerts.IsNew(order),
                CancellationReason = order.CancellationReason
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
                .Select(r => new OrderGridRow
                {
                    OrderID = r.OrderID,
                    OrderRef = r.OrderRef,
                    Customer = $"{r.CustomerName} ({r.CustomerRef})",
                    OrderDate = r.OrderDate,
                    TotalAmount = r.TotalAmount,
                    Prescription = r.Prescription,
                    RxStatus = r.RxStatus,
                    Status = r.RawStatus,
                    CancelReason = string.IsNullOrWhiteSpace(r.CancellationReason) ? "—" : r.CancellationReason,
                    Verify = "Verify",
                    Reject = "Reject",
                    Cancel = "Cancel",
                    View = "View"
                })
                .ToList();

            _suppressGridEvents = true;
            UiTheme.SetGridDataSource(gridOrders, pageRows);
            _suppressGridEvents = false;
            HideInternalColumns();
            UiTheme.BeautifyGridHeaders(gridOrders);
            EnsureGridColumns();
            ApplyStatusCellEditability();

            if (gridOrders.Columns.Contains("Customer"))
                gridOrders.Columns["Customer"].DefaultCellStyle.WrapMode = DataGridViewTriState.False;

            lblPageInfo.Text = $"Page {_currentPage} / {totalPages}";
            btnPagePrev.Enabled = _currentPage > 1;
            btnPageNext.Enabled = _currentPage < totalPages;
        }

        private void HideInternalColumns()
        {
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
        }

        private static bool CanReviewRx(OrderRow row) =>
            row != null
            && row.HasPrescription
            && string.Equals(row.RxStatus, PrescriptionService.StatusPending, StringComparison.OrdinalIgnoreCase);

        private static bool CanCancelOrder(OrderRow row) =>
            row != null
            && string.Equals(row.RawStatus, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase);

        private void EnsureGridColumns()
        {
            if (gridOrders.Columns.Contains("Edit"))
                gridOrders.Columns.Remove("Edit");

            gridOrders.ReadOnly = false;
            gridOrders.EditMode = DataGridViewEditMode.EditOnEnter;
            foreach (DataGridViewColumn col in gridOrders.Columns)
                col.ReadOnly = true;

            EnsureStatusComboColumn();
            ConfigureActionButtonColumn("Verify", "Verify", 72);
            ConfigureActionButtonColumn("Reject", "Reject", 72);
            ConfigureActionButtonColumn("Cancel", "Cancel", 72);
            ConfigureActionButtonColumn("View", "View", 64);

            if (gridOrders.Columns.Contains("Prescription"))
            {
                var col = gridOrders.Columns["Prescription"];
                col.HeaderText = "Prescription";
                col.MinimumWidth = 96;
            }

            if (gridOrders.Columns.Contains("RxStatus"))
            {
                var col = gridOrders.Columns["RxStatus"];
                col.HeaderText = "Rx Status";
                col.MinimumWidth = 88;
            }

            SetDisplayIndex("OrderRef", 0);
            SetDisplayIndex("Customer", 1);
            SetDisplayIndex("OrderDate", 2);
            SetDisplayIndex("TotalAmount", 3);
            SetDisplayIndex("Prescription", 4);
            SetDisplayIndex("RxStatus", 5);
            SetDisplayIndex("Status", 6);
            SetDisplayIndex("CancelReason", 7);
            SetDisplayIndex("Verify", 8);
            SetDisplayIndex("Reject", 9);
            SetDisplayIndex("Cancel", 10);
            SetDisplayIndex("View", 11);

            if (gridOrders.Columns.Contains("CancelReason"))
            {
                var col = gridOrders.Columns["CancelReason"];
                col.HeaderText = "Cancel Reason";
                col.MinimumWidth = 120;
                col.ReadOnly = true;
            }
        }

        private void SetDisplayIndex(string columnName, int displayIndex)
        {
            if (gridOrders.Columns.Contains(columnName))
                gridOrders.Columns[columnName].DisplayIndex = displayIndex;
        }

        private static bool CanUpdateStatus(OrderRow row) =>
            row != null && OrderService.GetAllowedNextStatuses(row.RawStatus).Count > 0;

        private void ApplyStatusCellEditability()
        {
            if (!gridOrders.Columns.Contains("Status")) return;

            foreach (DataGridViewRow gridRow in gridOrders.Rows)
            {
                if (gridRow.IsNewRow) continue;

                var orderRow = GetOrderRow(gridRow.Index);
                var canUpdate = CanUpdateStatus(orderRow);
                var statusCell = gridRow.Cells["Status"];
                statusCell.ReadOnly = !canUpdate;

                if (statusCell is DataGridViewComboBoxCell comboCell)
                {
                    comboCell.DisplayStyle = canUpdate
                        ? DataGridViewComboBoxDisplayStyle.ComboBox
                        : DataGridViewComboBoxDisplayStyle.Nothing;
                }
            }
        }

        private void EnsureStatusComboColumn()
        {
            const string name = "Status";
            DataGridViewComboBoxColumn comboCol;

            if (gridOrders.Columns[name] is DataGridViewComboBoxColumn existingCombo)
            {
                comboCol = existingCombo;
            }
            else
            {
                var textCol = gridOrders.Columns[name];
                var displayIndex = textCol?.DisplayIndex ?? 6;
                var width = textCol?.Width ?? 120;
                if (textCol != null)
                    gridOrders.Columns.Remove(name);

                comboCol = new DataGridViewComboBoxColumn
                {
                    Name = name,
                    HeaderText = "Status",
                    DataPropertyName = name,
                    DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox,
                    DisplayStyleForCurrentCellOnly = true,
                    FlatStyle = FlatStyle.Flat,
                    Width = width,
                    MinimumWidth = 100,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                };
                gridOrders.Columns.Add(comboCol);
                comboCol.DisplayIndex = displayIndex;
            }

            comboCol.ReadOnly = false;
            comboCol.ValueType = typeof(string);
            comboCol.Items.Clear();
            comboCol.Items.Add(OrderService.StatusPending);
            comboCol.Items.Add(OrderService.StatusReadyForPickup);
            comboCol.Items.Add(OrderService.StatusDelivered);
        }

        private void ConfigureActionButtonColumn(string name, string headerText, int width)
        {
            var displayIndex = gridOrders.Columns.Contains(name)
                ? gridOrders.Columns[name].DisplayIndex
                : gridOrders.Columns.Count;

            if (gridOrders.Columns[name] is DataGridViewButtonColumn buttonCol)
            {
                buttonCol.HeaderText = headerText;
                buttonCol.DataPropertyName = name;
                buttonCol.UseColumnTextForButtonValue = false;
                buttonCol.FlatStyle = FlatStyle.Flat;
                buttonCol.Width = width;
                buttonCol.MinimumWidth = width;
                buttonCol.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                buttonCol.ReadOnly = true;
                return;
            }

            if (gridOrders.Columns.Contains(name))
                gridOrders.Columns.Remove(name);

            buttonCol = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = headerText,
                DataPropertyName = name,
                UseColumnTextForButtonValue = false,
                FlatStyle = FlatStyle.Flat,
                Width = width,
                MinimumWidth = width,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None,
                ReadOnly = true
            };
            gridOrders.Columns.Add(buttonCol);
            buttonCol.DisplayIndex = displayIndex;
        }

        private OrderRow GetOrderRow(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex >= gridOrders.Rows.Count) return null;
            var orderIdCell = gridOrders.Rows[rowIndex].Cells["OrderID"];
            if (orderIdCell?.Value == null) return null;
            var orderId = Convert.ToInt32(orderIdCell.Value);
            return _filteredRows.FirstOrDefault(r => r.OrderID == orderId);
        }

        private void GridOrders_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (gridOrders.IsCurrentCellDirty
                && gridOrders.CurrentCell is DataGridViewComboBoxCell)
            {
                gridOrders.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void GridOrders_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (gridOrders.CurrentCell?.OwningColumn?.Name != "Status") return;
            if (!(e.Control is ComboBox combo)) return;

            UiTheme.StyleComboBox(combo);

            var row = GetOrderRow(gridOrders.CurrentCell.RowIndex);
            if (row == null) return;

            _statusEditOriginal = row.RawStatus;
            combo.DataSource = null;
            combo.Items.Clear();
            combo.Items.Add(row.RawStatus);
            foreach (var status in OrderService.GetAllowedNextStatuses(row.RawStatus))
            {
                if (!combo.Items.Contains(status))
                    combo.Items.Add(status);
            }
            combo.SelectedItem = row.RawStatus;
        }

        private void GridOrders_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Status") return;
            e.ThrowException = false;
        }

        private void GridOrders_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (_suppressGridEvents || e.RowIndex < 0 || !_servicesReady) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Status") return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            var newStatus = gridOrders.Rows[e.RowIndex].Cells["Status"].Value?.ToString();
            if (string.IsNullOrWhiteSpace(newStatus) || newStatus == _statusEditOriginal)
                return;

            try
            {
                _orders.UpdateStatus(orderId, newStatus);
                LoadOrders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _suppressGridEvents = true;
                gridOrders.Rows[e.RowIndex].Cells["Status"].Value = _statusEditOriginal;
                _suppressGridEvents = false;
            }
        }

        private void GridOrders_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Status") return;
            if (!CanUpdateStatus(GetOrderRow(e.RowIndex)))
                e.Cancel = true;
        }

        private void GridOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !_servicesReady) return;
            var colName = gridOrders.Columns[e.ColumnIndex].Name;

            if (colName == "Prescription")
            {
                var row = GetOrderRow(e.RowIndex);
                if (row?.HasPrescription != true) return;

                var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
                OpenPrescription(orderId);
                return;
            }

            if (colName == "Status" && CanUpdateStatus(GetOrderRow(e.RowIndex)))
                gridOrders.BeginEdit(true);
        }

        private void UpdateStats()
        {
            lblVolume.Text = _filteredRows.Count.ToString("N0");

            if (_filteredRows.Count == 0)
            {
                lblAvgTime.Text = "-";
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
                lblAvgTime.Text = "-";
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

            var columnName = gridOrders.Columns[e.ColumnIndex].Name;
            var row = GetOrderRow(e.RowIndex);

            if (UiTheme.IsSelectedRow(gridOrders, e.RowIndex))
            {
                UiTheme.ApplySelectedRowCellStyle(e.CellStyle);
                return;
            }

            if (row?.IsNew == true)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 251, 235);
                e.CellStyle.Font = UiTheme.UiFontBold;
            }

            if (columnName == "Prescription")
            {
                var file = e.Value?.ToString() ?? string.Empty;
                if (row?.HasPrescription == true && file != "—" && !string.IsNullOrWhiteSpace(file))
                {
                    e.CellStyle.ForeColor = UiTheme.AdminTeal;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                return;
            }

            if (columnName == "RxStatus")
            {
                var rx = e.Value?.ToString() ?? string.Empty;
                if (string.Equals(rx, PrescriptionService.StatusVerified, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(220, 245, 238);
                    e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(rx, PrescriptionService.StatusRejected, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(rx, PrescriptionService.StatusPending, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(140, 70, 0);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                return;
            }

            if (columnName == "Status")
            {
                var status = e.Value?.ToString() ?? string.Empty;
                var canUpdate = row != null && CanUpdateStatus(row);

                if (row != null && row.Status == "Flagged")
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 218, 214);
                    e.CellStyle.ForeColor = Color.FromArgb(104, 57, 61);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                    return;
                }

                if (string.Equals(status, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(140, 70, 0);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(status, OrderService.StatusReadyForPickup, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(199, 234, 228);
                    e.CellStyle.ForeColor = UiTheme.AdminTeal;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(status, OrderService.StatusDelivered, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(184, 237, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(status, OrderService.StatusCancelled, StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(243, 244, 246);
                    e.CellStyle.ForeColor = Color.FromArgb(55, 65, 81);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }

                if (canUpdate)
                    e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);

                return;
            }

            if (columnName == "Verify" || columnName == "Reject" || columnName == "Cancel" || columnName == "View")
            {
                var text = e.Value?.ToString() ?? string.Empty;
                if (string.IsNullOrEmpty(text))
                {
                    e.Value = string.Empty;
                    return;
                }

                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.Font = UiTheme.UiFontBold;

                var allowed = columnName == "View"
                    || (columnName == "Cancel" ? CanCancelOrder(row) : CanReviewRx(row));

                if (!allowed)
                {
                    e.CellStyle.ForeColor = UiTheme.AdminMuted;
                    return;
                }

                e.CellStyle.ForeColor = columnName == "Verify" || columnName == "View"
                    ? UiTheme.AdminTeal
                    : UiTheme.Danger;
                return;
            }
        }

        private void GridOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !_servicesReady) return;

            var colName = gridOrders.Columns[e.ColumnIndex].Name;
            if (colName != "View" && colName != "Verify" && colName != "Reject" && colName != "Cancel")
                return;

            var orderId = Convert.ToInt32(gridOrders.Rows[e.RowIndex].Cells["OrderID"].Value);
            var row = GetOrderRow(e.RowIndex);

            if (colName == "View")
            {
                ShowOrderDetails(orderId);
                return;
            }

            if (colName == "Verify")
            {
                if (!CanReviewRx(row))
                {
                    MessageBox.Show(
                        "Verify is only available when a prescription is pending review.",
                        "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                try
                {
                    _orders.VerifyPrescription(orderId);
                    LoadOrders();
                    MessageBox.Show("Prescription verified.", "SmartMed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Verify Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (colName == "Reject")
            {
                if (!CanReviewRx(row))
                {
                    MessageBox.Show(
                        "Reject is only available when a prescription is pending review.",
                        "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (MessageBox.Show(
                        "Reject this prescription? The order cannot move forward until a valid prescription is provided.",
                        "Reject Prescription", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

                try
                {
                    _orders.RejectPrescription(orderId);
                    LoadOrders();
                    MessageBox.Show("Prescription rejected.", "SmartMed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Reject Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return;
            }

            if (colName == "Cancel")
            {
                if (!CanCancelOrder(row))
                {
                    MessageBox.Show(
                        "Cancel is only available while the order is Pending.",
                        "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (!CancelReasonDialog.TryGetReason(FindForm(), out var reason))
                    return;

                try
                {
                    _orders.CancelOrderAsAdmin(orderId, reason);
                    LoadOrders();
                    MessageBox.Show("Order cancelled and stock restored.", "SmartMed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cancel Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
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
                $"Payment: {order.PaymentMethod} ({order.PaymentStatus})",
                $"Prescription: {_orders.GetPrescriptionDisplay(orderId)}",
                $"Rx Status: {_orders.GetPrescriptionStatusDisplay(orderId)}",
                "",
                "Line items:"
            };
            if (!string.IsNullOrWhiteSpace(order.CancellationReason))
                lines.Insert(5, $"Cancel reason: {order.CancellationReason}");
            if (!string.IsNullOrWhiteSpace(order.PaymentReference))
                lines.Insert(string.IsNullOrWhiteSpace(order.CancellationReason) ? 7 : 8, $"Payment Ref: {order.PaymentReference}");
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

        private sealed class OrderGridRow
        {
            public int OrderID { get; set; }
            public string OrderRef { get; set; }
            public string Customer { get; set; }
            public string OrderDate { get; set; }
            public string TotalAmount { get; set; }
            public string Prescription { get; set; }
            public string RxStatus { get; set; }
            public string Status { get; set; }
            public string Verify { get; set; }
            public string Reject { get; set; }
            public string Cancel { get; set; }
            public string View { get; set; }
            public string CancelReason { get; set; }
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
            public bool HasPrescription { get; set; }
            public bool IsNew { get; set; }
            public string CancellationReason { get; set; }
        }
    }
}
