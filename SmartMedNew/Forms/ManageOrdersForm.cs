using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMedNew.Models;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public sealed partial class ManageOrdersForm : AdminPageControl
    {
        private const int PageSize = 10;

        private readonly OrderService _orders = new OrderService();
        private readonly CustomerService _customers = new CustomerService();

        private List<OrderRow> _allRows = new List<OrderRow>();
        private List<OrderRow> _filteredRows = new List<OrderRow>();
        private int _currentPage = 1;
        private int? _selectedOrderId;

        public ManageOrdersForm()
        {
            InitializeComponent();
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage()
        {
            SyncScrollRootWidth();
            LoadOrders();
        }

        private void BuildContent()
        {
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 4,
                MinimumSize = new Size(0, 700)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            root.Controls.Add(CreatePageHeader(), 0, 0);
            root.Controls.Add(CreateFilterPanel(), 0, 1);
            root.Controls.Add(CreateGridPanel(), 0, 2);
            root.Controls.Add(CreateFooterPanel(), 0, 3);

            WireScrollRoot(root);
        }

        private static Panel CreatePageHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                Margin = new Padding(0, 0, 0, 16),
                BackColor = UiTheme.AdminSurface
            };
            header.Controls.Add(new Label
            {
                Text = "Monitor and process pharmaceutical orders across all clinical departments.",
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Location = new Point(0, 40),
                AutoSize = true,
                BackColor = UiTheme.AdminSurface
            });
            header.Controls.Add(new Label
            {
                Text = "Manage Orders",
                Font = UiTheme.FontAt(20f, bold: true),
                ForeColor = UiTheme.PrimaryDark,
                Location = new Point(0, 4),
                AutoSize = true,
                BackColor = UiTheme.AdminSurface
            });
            return header;
        }

        private Panel CreateFilterPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 16),
                Padding = new Padding(16, 20, 16, 16),
                BackColor = Color.FromArgb(238, 245, 244)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var badge = new Label
            {
                Text = "  Filter Options  ",
                AutoSize = true,
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(12, 46, 43),
                Location = new Point(12, 0)
            };

            txtSearch = new TextBox { Width = 260 };
            UiTheme.StyleTextBox(txtSearch);

            cmbStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 160
            };
            UiTheme.StyleComboBox(cmbStatus);
            cmbStatus.Items.AddRange(new object[]
            {
                "All Statuses",
                OrderService.StatusPending,
                OrderService.StatusReadyForPickup,
                OrderService.StatusDelivered,
                "Flagged"
            });
            cmbStatus.SelectedIndex = 0;

            dtpFrom = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120 };
            dtpTo = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120 };
            chkDateRange = new CheckBox
            {
                Text = "Use date range",
                AutoSize = true,
                ForeColor = UiTheme.AdminOnSurface,
                BackColor = Color.FromArgb(238, 245, 244)
            };

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Top,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                Padding = new Padding(0, 12, 0, 0),
                BackColor = Color.FromArgb(238, 245, 244)
            };

            flow.Controls.Add(CreateFilterField("Search Orders:", CreateSearchBox()));
            flow.Controls.Add(CreateFilterField("Status Category:", cmbStatus));
            flow.Controls.Add(CreateFilterField("Service Date Range:", CreateDateRangePanel()));

            var actions = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Margin = new Padding(0, 22, 0, 0),
                BackColor = Color.FromArgb(238, 245, 244)
            };
            var btnApply = CreateWinButton("Apply Filters", primary: true, width: 120);
            btnApply.Click += (s, e) => ApplyFilters();
            var btnExport = CreateWinButton("Export Data", primary: false, width: 110);
            btnExport.Click += BtnExport_Click;
            actions.Controls.Add(btnApply);
            actions.Controls.Add(btnExport);
            flow.Controls.Add(actions);

            outer.Controls.Add(flow);
            outer.Controls.Add(badge);
            badge.BringToFront();
            return outer;
        }

        private Panel CreateSearchBox()
        {
            var wrap = new Panel
            {
                Width = 280,
                Height = 30,
                BackColor = Color.White
            };
            wrap.Paint += (s, e) =>
            {
                var rect = wrap.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Margin = new Padding(8, 0, 8, 0);
            wrap.Controls.Add(txtSearch);
            return wrap;
        }

        private Panel CreateDateRangePanel()
        {
            var panel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.FromArgb(238, 245, 244)
            };
            panel.Controls.Add(chkDateRange);
            panel.Controls.Add(dtpFrom);
            panel.Controls.Add(new Label
            {
                Text = "to",
                AutoSize = true,
                Margin = new Padding(6, 6, 6, 0),
                ForeColor = UiTheme.AdminMuted,
                BackColor = Color.FromArgb(238, 245, 244)
            });
            panel.Controls.Add(dtpTo);
            return panel;
        }

        private static Panel CreateFilterField(string label, Control input)
        {
            var wrap = new Panel
            {
                AutoSize = true,
                Margin = new Padding(0, 0, 20, 8),
                BackColor = Color.FromArgb(238, 245, 244)
            };
            var lbl = new Label
            {
                Text = label,
                AutoSize = true,
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.PrimaryDark,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(238, 245, 244)
            };
            input.Margin = new Padding(0, 4, 0, 0);
            wrap.Controls.Add(input);
            wrap.Controls.Add(lbl);
            return wrap;
        }

        private Panel CreateGridPanel()
        {
            gridOrders = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MinimumSize = new Size(0, 280)
            };
            UiTheme.ApplyClinicalGrid(gridOrders);
            gridOrders.CellFormatting += GridOrders_CellFormatting;
            gridOrders.CellContentClick += GridOrders_CellContentClick;
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
            outer.Controls.Add(gridOrders);
            return outer;
        }

        private Panel CreateFooterPanel()
        {
            var footer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                BackColor = UiTheme.AdminSurface
            };

            lblVolume = CreateFooterStat("VOLUME", UiTheme.AdminTeal);
            lblAvgTime = CreateFooterStat("AVG TIME", Color.FromArgb(41, 163, 122));
            lblFlags = CreateFooterStat("FLAGS", UiTheme.Danger);

            var stats = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = UiTheme.AdminSurface
            };
            stats.Controls.Add(WrapFooterCard(lblVolume, UiTheme.AdminTeal));
            stats.Controls.Add(WrapFooterCard(lblAvgTime, Color.FromArgb(41, 163, 122)));
            stats.Controls.Add(WrapFooterCard(lblFlags, UiTheme.Danger));

            btnPagePrev = CreateWinButton("<", false, 36);
            btnPagePrev.Height = 32;
            btnPagePrev.Click += (s, e) => ChangePage(-1);
            btnPageNext = CreateWinButton(">", false, 36);
            btnPageNext.Height = 32;
            btnPageNext.Click += (s, e) => ChangePage(1);
            lblPageInfo = new Label
            {
                AutoSize = true,
                Text = "Page 1",
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface,
                Margin = new Padding(8, 8, 8, 0)
            };

            var pager = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                BackColor = UiTheme.AdminSurface,
                Padding = new Padding(8, 4, 0, 0)
            };
            pager.Controls.Add(btnPagePrev);
            pager.Controls.Add(lblPageInfo);
            pager.Controls.Add(btnPageNext);

            footer.Controls.Add(pager);
            footer.Controls.Add(stats);
            return footer;
        }

        private static Label CreateFooterStat(string title, Color accent)
        {
            return new Label
            {
                Text = "0",
                Font = UiTheme.FontAt(16f, bold: true),
                ForeColor = UiTheme.PrimaryDark,
                AutoSize = true,
                Tag = title,
                BackColor = Color.White
            };
        }

        private static Panel WrapFooterCard(Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Width = 130,
                Height = 56,
                Margin = new Padding(0, 0, 12, 0),
                Padding = new Padding(12, 8, 8, 8),
                BackColor = Color.White
            };
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
            card.Controls.Add(valueLabel);
            card.Controls.Add(new Label
            {
                Text = valueLabel.Tag?.ToString() ?? "",
                Dock = DockStyle.Top,
                Height = 14,
                Font = UiTheme.FontAt(7.5f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                BackColor = Color.White
            });
            return card;
        }

        private void LoadOrders()
        {
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
                OrderDateValue = order.OrderDate
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
                ClientSize = new Size(360, 200),
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

                var btnRx = CreateWinButton("View Rx", false, 90);
                btnRx.Left = 16;
                btnRx.Top = 100;
                btnRx.Click += (s, e) => OpenPrescription(orderId);

                var btnVerify = CreateWinButton("Verify Rx", true, 90);
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

                var btnSave = CreateWinButton("Update", true, 90);
                btnSave.Left = 246;
                btnSave.Top = 140;
                btnSave.DialogResult = DialogResult.OK;
                var btnCancel = CreateWinButton("Cancel", false, 90);
                btnCancel.Left = 150;
                btnCancel.Top = 140;
                btnCancel.DialogResult = DialogResult.Cancel;

                dlg.Controls.Add(btnRx);
                dlg.Controls.Add(btnVerify);
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

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

        private static Button CreateWinButton(string text, bool primary, int width)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = 30,
                FlatStyle = FlatStyle.Flat,
                Font = UiTheme.UiFont,
                Cursor = Cursors.Hand,
                Margin = new Padding(0, 0, 8, 0)
            };
            btn.FlatAppearance.BorderSize = 1;
            if (primary)
            {
                btn.BackColor = Color.FromArgb(12, 46, 43);
                btn.ForeColor = Color.White;
                btn.FlatAppearance.BorderColor = UiTheme.AdminTealDark;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(59, 109, 100);
            }
            else
            {
                btn.BackColor = Color.White;
                btn.ForeColor = UiTheme.AdminOnSurface;
                btn.FlatAppearance.BorderColor = UiTheme.AdminOutline;
                btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(238, 245, 244);
            }
            btn.UseVisualStyleBackColor = false;
            return btn;
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
        }
    }
}
