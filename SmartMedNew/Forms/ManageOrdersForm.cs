using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SmartMedNew.Models;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public sealed class ManageOrdersForm : AdminPageControl
    {
        private const int PageSize = 10;
        private const string StatusAll = "All Statuses";
        private const string StatusFlagged = "Flagged";

        private readonly OrderService _orders = new OrderService();
        private readonly CustomerService _customers = new CustomerService();

        private TextBox txtSearch;
        private ComboBox cmbStatus;
        private DateTimePicker dtpFrom;
        private DateTimePicker dtpTo;
        private DataGridView gridOrders;
        private Label lblVolume;
        private Label lblAvgTime;
        private Label lblFlags;
        private Label lblPageInfo;
        private Button btnPrevPage;
        private Button btnNextPage;
        private FlowLayoutPanel pnlPageNumbers;

        private List<Order> _allOrders = new List<Order>();
        private List<Order> _filteredOrders = new List<Order>();
        private int _currentPage;

        public ManageOrdersForm()
        {
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage()
        {
            SyncScrollRootWidth();
            _allOrders = _orders.GetAll();
            ApplyFilters();
        }

        private void BuildContent()
        {
            lblVolume = new Label();
            lblAvgTime = new Label();
            lblFlags = new Label();

            var scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 4,
                BackColor = UiTheme.AdminSurface
            };
            scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 400f));
            scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            scrollRoot.Controls.Add(CreateFilterPanel(), 0, 1);
            scrollRoot.Controls.Add(CreateGridPanel(), 0, 2);
            scrollRoot.Controls.Add(CreateFooterPanel(), 0, 3);

            WireScrollRoot(scrollRoot);
        }

        private Panel CreatePageHeader()
        {
            return AdminUiHelpers.CreatePageHeader(
                "Manage Orders",
                "Monitor and process pharmaceutical orders across all clinical departments.");
        }

        private Panel CreateFilterPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 108,
                Margin = new Padding(0, 0, 0, 20),
                BackColor = Color.FromArgb(238, 245, 244),
                Padding = new Padding(16, 20, 16, 12)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var legend = new Label
            {
                Text = "  Filter Options  ",
                AutoSize = true,
                BackColor = UiTheme.PrimaryDark,
                ForeColor = Color.White,
                Font = UiTheme.FontAt(9f, semibold: true),
                Location = new Point(12, -2)
            };
            outer.Controls.Add(legend);

            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = true,
                BackColor = Color.FromArgb(238, 245, 244),
                Padding = new Padding(0, 8, 0, 0)
            };

            flow.Controls.Add(CreateFilterField("Search Orders:", CreateSearchBox(), 288));
            flow.Controls.Add(CreateFilterField("Status Category:", CreateStatusCombo(), 192));

            var dateWrap = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                BackColor = Color.FromArgb(238, 245, 244)
            };
            dtpFrom = CreateDatePicker(DateTime.Today.AddMonths(-1));
            dtpTo = CreateDatePicker(DateTime.Today);
            dateWrap.Controls.Add(dtpFrom);
            dateWrap.Controls.Add(new Label
            {
                Text = "to",
                AutoSize = true,
                ForeColor = UiTheme.AdminMuted,
                Font = UiTheme.FontAt(9f, semibold: true),
                Margin = new Padding(6, 6, 6, 0),
                BackColor = Color.FromArgb(238, 245, 244)
            });
            dateWrap.Controls.Add(dtpTo);
            flow.Controls.Add(CreateFilterField("Service Date Range:", dateWrap, 280));

            var actions = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                BackColor = Color.FromArgb(238, 245, 244),
                Margin = new Padding(12, 22, 0, 0)
            };
            var btnApply = AdminUiHelpers.CreateWinButton("Apply Filters", primary: true, width: 128, height: 32);
            btnApply.Margin = new Padding(0, 0, 8, 0);
            btnApply.Click += (s, e) => ApplyFilters();
            var btnExport = AdminUiHelpers.CreateWinButton("Export Data", primary: false, width: 118, height: 32);
            btnExport.BackColor = Color.White;
            btnExport.Click += BtnExport_Click;
            actions.Controls.Add(btnApply);
            actions.Controls.Add(btnExport);
            flow.Controls.Add(actions);

            outer.Controls.Add(flow);
            legend.BringToFront();
            return outer;
        }

        private static Panel CreateFilterField(string labelText, Control input, int width)
        {
            var wrap = new Panel
            {
                Width = width,
                Height = 58,
                Margin = new Padding(0, 0, 16, 0),
                BackColor = Color.FromArgb(238, 245, 244)
            };
            wrap.Controls.Add(new Label
            {
                Text = labelText,
                Font = UiTheme.FontAt(9f, semibold: true),
                ForeColor = UiTheme.PrimaryDark,
                Location = new Point(0, 0),
                AutoSize = true,
                BackColor = Color.FromArgb(238, 245, 244)
            });
            input.Location = new Point(0, 22);
            wrap.Controls.Add(input);
            return wrap;
        }

        private Panel CreateSearchBox()
        {
            txtSearch = new TextBox { Width = 288, Height = 28 };
            UiTheme.StyleTextBox(txtSearch);

            var wrap = new Panel
            {
                Width = 288,
                Height = 28,
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

            txtSearch.BorderStyle = BorderStyle.None;
            txtSearch.Dock = DockStyle.Fill;
            txtSearch.Margin = new Padding(8, 0, 8, 0);
            wrap.Controls.Add(txtSearch);
            return wrap;
        }

        private ComboBox CreateStatusCombo()
        {
            cmbStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 192,
                Height = 28
            };
            UiTheme.StyleComboBox(cmbStatus);
            cmbStatus.Items.AddRange(new object[]
            {
                StatusAll,
                OrderService.StatusPending,
                OrderService.StatusReadyForPickup,
                OrderService.StatusDelivered,
                StatusFlagged
            });
            cmbStatus.SelectedIndex = 0;
            return cmbStatus;
        }

        private static DateTimePicker CreateDatePicker(DateTime value)
        {
            var dtp = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Width = 118,
                Height = 28,
                Value = value
            };
            return dtp;
        }

        private Panel CreateGridPanel()
        {
            gridOrders = new DataGridView
            {
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ScrollBars = ScrollBars.Vertical,
                MultiSelect = false
            };
            UiTheme.ApplyClinicalGrid(gridOrders);
            gridOrders.CellFormatting += GridOrders_CellFormatting;
            gridOrders.CellContentClick += GridOrders_CellContentClick;
            gridOrders.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridOrders.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridOrders.RowTemplate.Height = 48;

            return AdminUiHelpers.CreateSectionPanel(string.Empty, gridOrders);
        }

        private Panel CreateFooterPanel()
        {
            var footer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 88,
                Margin = new Padding(0, 20, 0, 0),
                BackColor = UiTheme.AdminSurface
            };

            var stats = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = UiTheme.AdminSurface
            };
            stats.Controls.Add(CreateFooterStatCard("Volume", lblVolume, UiTheme.AdminTeal));
            stats.Controls.Add(CreateFooterStatCard("Avg Time", lblAvgTime, UiTheme.AdminTeal));
            stats.Controls.Add(CreateFooterStatCard("Flags", lblFlags, UiTheme.Danger));

            pnlPageNumbers = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = Color.FromArgb(232, 239, 238),
                Padding = new Padding(6, 4, 6, 4),
                Margin = new Padding(0)
            };
            pnlPageNumbers.Paint += (s, e) =>
            {
                var rect = pnlPageNumbers.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            btnPrevPage = CreatePageButton("<");
            btnPrevPage.Click += (s, e) => { _currentPage--; BindCurrentPage(); };
            btnNextPage = CreatePageButton(">");
            btnNextPage.Click += (s, e) => { _currentPage++; BindCurrentPage(); };

            lblPageInfo = new Label
            {
                AutoSize = true,
                Font = UiTheme.FontAt(9f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                Margin = new Padding(8, 8, 8, 0),
                BackColor = Color.FromArgb(232, 239, 238)
            };

            pnlPageNumbers.Controls.Add(btnPrevPage);
            pnlPageNumbers.Controls.Add(lblPageInfo);
            pnlPageNumbers.Controls.Add(btnNextPage);

            var pagerWrap = new Panel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                BackColor = UiTheme.AdminSurface,
                Padding = new Padding(0, 20, 0, 0)
            };
            pagerWrap.Controls.Add(pnlPageNumbers);

            footer.Controls.Add(pagerWrap);
            footer.Controls.Add(stats);
            return footer;
        }

        private static Panel CreateFooterStatCard(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Width = 144,
                Height = 68,
                Margin = new Padding(0, 0, 14, 0),
                Padding = new Padding(12, 10, 10, 10),
                BackColor = Color.FromArgb(232, 239, 238)
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

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = UiTheme.FontAt(8.25f, semibold: true),
                ForeColor = UiTheme.AdminMuted,
                Dock = DockStyle.Top,
                Height = 14,
                BackColor = Color.FromArgb(232, 239, 238)
            });

            valueLabel.Text = "0";
            valueLabel.Font = UiTheme.FontAt(18f, bold: true);
            valueLabel.ForeColor = title == "Flags" ? UiTheme.Danger : UiTheme.PrimaryDark;
            valueLabel.Dock = DockStyle.Fill;
            valueLabel.TextAlign = ContentAlignment.MiddleLeft;
            valueLabel.BackColor = Color.FromArgb(232, 239, 238);
            card.Controls.Add(valueLabel);
            return card;
        }

        private static Button CreatePageButton(string text)
        {
            var btn = AdminUiHelpers.CreateWinButton(text, primary: false, width: 36, height: 32);
            btn.Margin = new Padding(2, 0, 2, 0);
            btn.BackColor = Color.White;
            return btn;
        }

        private void ApplyFilters()
        {
            IEnumerable<Order> query = _allOrders;

            var statusFilter = cmbStatus.SelectedItem?.ToString() ?? StatusAll;
            if (statusFilter == StatusFlagged)
                query = query.Where(IsFlagged);
            else if (statusFilter != StatusAll)
                query = query.Where(o => string.Equals(o.Status, statusFilter, StringComparison.OrdinalIgnoreCase));

            var from = dtpFrom.Value.Date;
            var to = dtpTo.Value.Date.AddDays(1).AddTicks(-1);
            query = query.Where(o => o.OrderDate >= from && o.OrderDate <= to);

            var list = query.ToList();
            list = SearchService.SearchOrders(list, txtSearch?.Text);
            _filteredOrders = list;
            _currentPage = 0;
            UpdateStats();
            BindCurrentPage();
        }

        private void UpdateStats()
        {
            lblVolume.Text = _filteredOrders.Count.ToString("N0");
            if (_filteredOrders.Count == 0)
            {
                lblAvgTime.Text = "—";
                lblFlags.Text = "00";
                return;
            }

            var avgMinutes = _filteredOrders.Average(o => (DateTime.Now - o.OrderDate).TotalMinutes);
            lblAvgTime.Text = avgMinutes >= 60
                ? $"{avgMinutes / 60.0:F1}h"
                : $"{avgMinutes:F1}m";
            lblFlags.Text = _filteredOrders.Count(IsFlagged).ToString("D2");
        }

        private void BindCurrentPage()
        {
            var totalPages = Math.Max(1, (int)Math.Ceiling(_filteredOrders.Count / (double)PageSize));
            if (_currentPage >= totalPages) _currentPage = totalPages - 1;
            if (_currentPage < 0) _currentPage = 0;

            var pageItems = _filteredOrders
                .Skip(_currentPage * PageSize)
                .Take(PageSize)
                .Select(BuildRow)
                .ToList();

            UiTheme.SetGridDataSource(gridOrders, pageItems);
            HideInternalColumns();
            EnsureActionColumns();
            UiTheme.BeautifyGridHeaders(gridOrders);
            ApplyColumnHeaders();

            lblPageInfo.Text = $"Page {_currentPage + 1} of {totalPages}";
            btnPrevPage.Enabled = _currentPage > 0;
            btnNextPage.Enabled = _currentPage < totalPages - 1;
            RebuildPageNumberButtons(totalPages);
        }

        private object BuildRow(Order order)
        {
            return new
            {
                order.OrderID,
                OrderRef = FormatOrderRef(order.OrderID),
                CustomerDisplay = $"{order.CustomerName}{Environment.NewLine}{FormatPatientId(order.CustomerID)}",
                OrderDate = order.OrderDate.ToString("MMM dd, yyyy HH:mm"),
                TotalAmount = $"LKR {order.TotalAmount:N2}",
                Status = GetDisplayStatus(order),
                RawStatus = order.Status
            };
        }

        private void HideInternalColumns()
        {
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
            if (gridOrders.Columns.Contains("RawStatus"))
                gridOrders.Columns["RawStatus"].Visible = false;
        }

        private void EnsureActionColumns()
        {
            if (!gridOrders.Columns.Contains("View"))
            {
                gridOrders.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "View",
                    HeaderText = "Actions",
                    Text = "View",
                    UseColumnTextForButtonValue = true,
                    Width = 64,
                    FlatStyle = FlatStyle.Flat
                });
            }
            if (!gridOrders.Columns.Contains("Edit"))
            {
                gridOrders.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "Edit",
                    HeaderText = "",
                    Text = "Edit",
                    UseColumnTextForButtonValue = true,
                    Width = 52,
                    FlatStyle = FlatStyle.Flat
                });
            }

            if (gridOrders.Columns.Contains("View"))
                gridOrders.Columns["View"].DisplayIndex = gridOrders.Columns.Count - 2;
            if (gridOrders.Columns.Contains("Edit"))
                gridOrders.Columns["Edit"].DisplayIndex = gridOrders.Columns.Count - 1;
        }

        private void ApplyColumnHeaders()
        {
            SetHeader("OrderRef", "Order ID");
            SetHeader("CustomerDisplay", "Customer Name & ID");
            SetHeader("OrderDate", "Order Date");
            SetHeader("TotalAmount", "Total Amount");
            SetHeader("Status", "Status");
        }

        private void SetHeader(string columnName, string headerText)
        {
            if (gridOrders.Columns.Contains(columnName))
                gridOrders.Columns[columnName].HeaderText = headerText;
        }

        private void RebuildPageNumberButtons(int totalPages)
        {
            foreach (Control c in pnlPageNumbers.Controls.Cast<Control>().ToList())
            {
                if (c != btnPrevPage && c != btnNextPage && c != lblPageInfo)
                    pnlPageNumbers.Controls.Remove(c);
            }

            var insertIndex = pnlPageNumbers.Controls.GetChildIndex(lblPageInfo);
            var start = Math.Max(0, Math.Min(_currentPage - 1, totalPages - 3));
            var end = Math.Min(totalPages, start + 3);

            for (var i = start; i < end; i++)
            {
                var pageIndex = i;
                var btn = CreatePageButton((i + 1).ToString());
                if (i == _currentPage)
                {
                    btn.BackColor = UiTheme.PrimaryDark;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = UiTheme.PrimaryDark;
                }
                btn.Click += (s, e) =>
                {
                    _currentPage = pageIndex;
                    BindCurrentPage();
                };
                pnlPageNumbers.Controls.Add(btn);
                pnlPageNumbers.Controls.SetChildIndex(btn, insertIndex++);
            }

            if (end < totalPages)
            {
                var ellipsis = new Label
                {
                    Text = "...",
                    AutoSize = true,
                    Margin = new Padding(4, 8, 4, 0),
                    BackColor = Color.FromArgb(232, 239, 238)
                };
                pnlPageNumbers.Controls.Add(ellipsis);
                pnlPageNumbers.Controls.SetChildIndex(ellipsis, insertIndex++);
            }
        }

        private void GridOrders_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridOrders.Columns[e.ColumnIndex].Name != "Status") return;

            var status = e.Value?.ToString() ?? string.Empty;
            e.CellStyle.Font = UiTheme.UiFontBold;
            e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (string.Equals(status, StatusFlagged, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 218, 214);
                e.CellStyle.ForeColor = UiTheme.Danger;
                return;
            }
            if (string.Equals(status, OrderService.StatusPending, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 218, 219);
                e.CellStyle.ForeColor = Color.FromArgb(104, 57, 61);
                return;
            }
            if (string.Equals(status, OrderService.StatusReadyForPickup, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(199, 234, 228);
                e.CellStyle.ForeColor = UiTheme.AdminTeal;
                e.Value = "Ready";
                return;
            }
            if (string.Equals(status, OrderService.StatusDelivered, StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(184, 237, 226);
                e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
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
            if (order == null)
            {
                MessageBox.Show("Order not found.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var customer = _customers.GetById(order.CustomerID);
            var items = _orders.GetItems(orderId);
            var rxDisplay = _orders.GetPrescriptionDisplay(orderId);
            var rxStatus = _orders.GetPrescriptionStatusDisplay(orderId);
            var hasRx = _orders.OrderHasPrescription(orderId);

            using (var dlg = new Form
            {
                Text = $"Order {FormatOrderRef(orderId)}",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(520, 420),
                Font = UiTheme.UiFont,
                BackColor = Color.White
            })
            {
                var info = new Label
                {
                    Text = $"Customer: {order.CustomerName} ({FormatPatientId(order.CustomerID)})" + Environment.NewLine +
                           $"Email: {customer?.Email ?? "—"}" + Environment.NewLine +
                           $"Order Date: {order.OrderDate:MMM dd, yyyy HH:mm}" + Environment.NewLine +
                           $"Status: {GetDisplayStatus(order)}" + Environment.NewLine +
                           $"Total: LKR {order.TotalAmount:N2}" + Environment.NewLine +
                           (hasRx ? $"Prescription: {rxDisplay} ({rxStatus})" : "Prescription: Not required"),
                    Location = new Point(16, 16),
                    Size = new Size(488, 110),
                    BackColor = Color.White
                };
                dlg.Controls.Add(info);

                var grid = new DataGridView
                {
                    Location = new Point(16, 132),
                    Size = new Size(488, 200),
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    RowHeadersVisible = false,
                    AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                };
                UiTheme.ApplyClinicalGrid(grid);
                grid.DataSource = items.Select(i => new
                {
                    i.MedicineName,
                    i.Quantity,
                    UnitPrice = $"LKR {i.UnitPrice:N2}",
                    Subtotal = $"LKR {i.Subtotal:N2}",
                    Rx = i.RequiresPrescription ? "Yes" : "No"
                }).ToList();
                UiTheme.BeautifyGridHeaders(grid);
                dlg.Controls.Add(grid);

                var actions = new FlowLayoutPanel
                {
                    Dock = DockStyle.Bottom,
                    Height = 48,
                    FlowDirection = FlowDirection.RightToLeft,
                    Padding = new Padding(12, 8, 12, 8),
                    BackColor = UiTheme.AdminSidebar
                };

                var btnClose = AdminUiHelpers.CreateWinButton("Close", false, 88, 32);
                btnClose.Click += (s, e) => dlg.Close();
                actions.Controls.Add(btnClose);

                if (hasRx)
                {
                    var btnOpenRx = AdminUiHelpers.CreateWinButton("View Prescription", false, 140, 32);
                    btnOpenRx.Click += (s, e) => OpenPrescriptionFile(orderId);
                    actions.Controls.Add(btnOpenRx);

                    if (string.Equals(rxStatus, PrescriptionService.StatusPending, StringComparison.OrdinalIgnoreCase))
                    {
                        var btnVerify = AdminUiHelpers.CreateWinButton("Verify", true, 80, 32);
                        btnVerify.Click += (s, e) =>
                        {
                            if (ConfirmPrescriptionAction("Verify this prescription?"))
                            {
                                try
                                {
                                    _orders.VerifyPrescription(orderId);
                                    dlg.Close();
                                    RefreshPage();
                                    MessageBox.Show("Prescription verified.", "SmartMed",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Verify Failed",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        };
                        var btnReject = AdminUiHelpers.CreateWinButton("Reject", false, 80, 32);
                        btnReject.Click += (s, e) =>
                        {
                            if (ConfirmPrescriptionAction("Reject this prescription?"))
                            {
                                try
                                {
                                    _orders.RejectPrescription(orderId);
                                    dlg.Close();
                                    RefreshPage();
                                    MessageBox.Show("Prescription rejected.", "SmartMed",
                                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message, "Reject Failed",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        };
                        actions.Controls.Add(btnReject);
                        actions.Controls.Add(btnVerify);
                    }
                }

                dlg.Controls.Add(actions);
                dlg.ShowDialog(FindForm());
            }
        }

        private void ShowStatusEditor(int orderId)
        {
            var order = _orders.GetById(orderId);
            if (order == null) return;

            if (IsFlagged(order))
            {
                MessageBox.Show(
                    "This order is flagged because the prescription was rejected. Review the prescription from View before updating status.",
                    "Order Flagged",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var allowed = OrderService.GetAllowedNextStatuses(order.Status);
            if (allowed.Count == 0)
            {
                MessageBox.Show("This order status cannot be changed.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using (var dlg = new Form
            {
                Text = "Update Order Status",
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false,
                ClientSize = new Size(360, 160),
                Font = UiTheme.UiFont,
                BackColor = Color.White
            })
            {
                dlg.Controls.Add(new Label
                {
                    Text = $"Order {FormatOrderRef(orderId)} — current: {order.Status}",
                    Location = new Point(16, 16),
                    AutoSize = true,
                    BackColor = Color.White
                });

                var cmb = new ComboBox
                {
                    DropDownStyle = ComboBoxStyle.DropDownList,
                    Location = new Point(16, 48),
                    Width = 320
                };
                UiTheme.StyleComboBox(cmb);
                cmb.Items.AddRange(allowed.Cast<object>().ToArray());
                cmb.SelectedIndex = 0;
                dlg.Controls.Add(cmb);

                var btnSave = AdminUiHelpers.CreateWinButton("Update", true, 96, 32);
                btnSave.Location = new Point(240, 96);
                btnSave.Click += (s, e) =>
                {
                    try
                    {
                        _orders.UpdateStatus(orderId, cmb.SelectedItem.ToString());
                        dlg.Close();
                        RefreshPage();
                        MessageBox.Show("Order status updated.", "SmartMed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Update Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 96, 32);
                btnCancel.Location = new Point(136, 96);
                btnCancel.Click += (s, e) => dlg.Close();

                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.ShowDialog(FindForm());
            }
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (_filteredOrders.Count == 0)
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
                    _orders.ExportOrdersToCsv(_filteredOrders, dlg.FileName);
                    MessageBox.Show("Orders exported successfully.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Export Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static bool ConfirmPrescriptionAction(string message) =>
            MessageBox.Show(message, "Prescription Review", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes;

        private void OpenPrescriptionFile(int orderId)
        {
            var filePath = _orders.GetPrescriptionFilePath(orderId);
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
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
                MessageBox.Show($"Could not open prescription file.\n{ex.Message}", "Prescription",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool IsFlagged(Order order)
        {
            if (!_orders.OrderHasPrescription(order.OrderID))
                return false;
            var rxStatus = _orders.GetPrescriptionStatusDisplay(order.OrderID);
            return string.Equals(rxStatus, PrescriptionService.StatusRejected, StringComparison.OrdinalIgnoreCase);
        }

        private string GetDisplayStatus(Order order) =>
            IsFlagged(order) ? StatusFlagged : order.Status;

        private static string FormatOrderRef(int orderId) => $"#ORD-{orderId:D4}";

        private static string FormatPatientId(int customerId) =>
            $"PAT-{customerId / 100:D3}-{customerId % 100:D2}";
    }
}
