using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class ManageOrdersForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ManageOrdersForm()
            : base(AdminNavItem.Orders, "Manage Orders")
        {
            InitializeComponent();
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                LoadOrders();
        }

        private static readonly string[] OrderStatuses =
        {
            "Pending", "Ready for Pickup", "Delivered"
        };

        private OrderService _orders;
        private int? _selectedOrderId;
        private string _statusFilter = "All";
        private ComboBox cmbStatusFilter;

        private DataGridView gridOrders;
        private DataGridView gridItems;
        private ComboBox cmbStatus;
        private Label lblOrderDetails;
        private Label lblLastUpdated;
        private Label lblTotalOrders;
        private Label lblPendingOrders;
        private Label lblDeliveredOrders;
        private Button btnUpdateStatus;
        private TableLayoutPanel _scrollRoot;

        private OrderService Orders => GetRuntimeService(ref _orders);

        private void BuildContent()
        {
            panelContent.Controls.Clear();

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 5,
                MinimumSize = new Size(0, 900),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 200f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateOrdersGridPanel(), 0, 2);
            _scrollRoot.Controls.Add(CreateItemsGridPanel(), 0, 3);
            _scrollRoot.Controls.Add(CreateStatusPanel(), 0, 4);
            WireScrollRoot(_scrollRoot);
        }

        private Panel CreatePageHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0, 0, 0, 16)
            };
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            var titleBlock = new Panel { Dock = DockStyle.Left, Width = 520 };
            titleBlock.Controls.Add(new Label
            {
                Text = "Review fulfillment queue and update order status.",
                Font = SystemFonts.DefaultFont,
                ForeColor = SystemColors.GrayText,
                Dock = DockStyle.Fill
            });

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };
            actions.Controls.Add(new Label { Text = "Status:", AutoSize = true, Padding = new Padding(0, 8, 0, 0) });
            cmbStatusFilter = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 150
            };
            cmbStatusFilter.Items.AddRange(new object[] { "All", "Pending", "Ready for Pickup", "Delivered" });
            cmbStatusFilter.SelectedIndex = 0;
            cmbStatusFilter.SelectedIndexChanged += (s, e) =>
            {
                _statusFilter = cmbStatusFilter.SelectedItem?.ToString() ?? "All";
                if (!IsDesignHost()) LoadOrders();
            };
            actions.Controls.Add(cmbStatusFilter);

            header.Controls.Add(actions);
            header.Controls.Add(titleBlock);
            return header;
        }

        private static Button CreateToolbarButton(string text)
        {
            var btn = new Button
            {
                Text = text,
                Height = 32,
                Width = 110,
                Margin = new Padding(4, 0, 0, 0)
            };
            return btn;
        }

        private Panel CreateStatsRow()
        {
            var wrap = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 88,
                Margin = new Padding(0, 0, 0, 16)
            };

            var row = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 3, RowCount = 1 };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));

            lblTotalOrders = new Label();
            lblPendingOrders = new Label();
            lblDeliveredOrders = new Label();

            row.Controls.Add(CreateStatTile("Total Orders", lblTotalOrders, SystemColors.Highlight), 0, 0);
            row.Controls.Add(CreateStatTile("Pending", lblPendingOrders, SystemColors.ControlText), 1, 0);
            row.Controls.Add(CreateStatTile("Delivered", lblDeliveredOrders, Color.Green), 2, 0);

            wrap.Controls.Add(row);
            return wrap;
        }

        private Panel CreateStatTile(string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 8, 0)
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            valueLabel.Text = "0";
            valueLabel.Font = SystemFonts.DefaultFont;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(16, 36);
            valueLabel.AutoSize = true;

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = SystemFonts.DefaultFont,
                ForeColor = SystemColors.GrayText,
                Location = new Point(16, 16),
                AutoSize = true
            });
            card.Controls.Add(valueLabel);
            return card;
        }

        private Panel CreateOrdersGridPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = SystemColors.Control,
                Padding = new Padding(12, 10, 12, 8)
            };
            header.Controls.Add(new Label
            {
                Text = "Recent Orders",
                Font = SystemFonts.DefaultFont,
                ForeColor = SystemColors.Highlight,
                Dock = DockStyle.Left,
                AutoSize = true
            });

            gridOrders = CreateGrid();
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            outer.Controls.Add(gridOrders);
            outer.Controls.Add(header);
            return outer;
        }

        private Panel CreateItemsGridPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.Window,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            lblOrderDetails = new Label
            {
                Text = "Order Details",
                Font = SystemFonts.DefaultFont,
                ForeColor = SystemColors.GrayText,
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(12, 8, 0, 0),
                BackColor = SystemColors.Control
            };

            gridItems = CreateGrid();
            outer.Controls.Add(gridItems);
            outer.Controls.Add(lblOrderDetails);
            return outer;
        }

        private Panel CreateStatusPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(0, 0, 0, 16)
            };

            cmbStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 200,
                Location = new Point(88, 16)
            };
            cmbStatus.Items.AddRange(OrderStatuses);

            lblLastUpdated = new Label
            {
                Text = "Last Updated: —",
                ForeColor = SystemColors.ControlLightLight,
                Font = SystemFonts.DefaultFont,
                AutoSize = true,
                Location = new Point(320, 20)
            };

            btnUpdateStatus = new Button
            {
                Text = "Update Status",
                Width = 150,
                Height = 40,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnUpdateStatus.Click += BtnUpdateStatus_Click;

            panel.Controls.Add(new Label
            {
                Text = "STATUS:",
                ForeColor = SystemColors.HighlightText,
                Font = SystemFonts.DefaultFont,
                AutoSize = true,
                Location = new Point(16, 20)
            });
            panel.Controls.Add(cmbStatus);
            panel.Controls.Add(lblLastUpdated);
            panel.Controls.Add(btnUpdateStatus);

            panel.Resize += (s, e) =>
            {
                btnUpdateStatus.Location = new Point(panel.Width - btnUpdateStatus.Width - 16, 14);
            };

            return panel;
        }

        private static DataGridView CreateGrid()
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BackgroundColor = SystemColors.Window,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                MultiSelect = false,
                ScrollBars = ScrollBars.Vertical
            };
            return grid;
        }

        private void LoadDesignTimePreview()
        {
            gridOrders.SelectionChanged -= GridOrders_SelectionChanged;
            gridOrders.DataSource = new[]
            {
                new { OrderID = 9421, OrderRef = "#ORD-9421", CustomerName = "Margaret Sullivan", OrderDate = "Oct 24, 2023", Status = "Pending", Total = "LKR 124.50" },
                new { OrderID = 9420, OrderRef = "#ORD-9420", CustomerName = "Jonathan Wick", OrderDate = "Oct 24, 2023", Status = "Ready for Pickup", Total = "LKR 45.00" },
                new { OrderID = 9419, OrderRef = "#ORD-9419", CustomerName = "Sarah Connor", OrderDate = "Oct 23, 2023", Status = "Delivered", Total = "LKR 312.20" }
            };
            HideOrderIdColumn();
            gridOrders.ClearSelection();
            gridOrders.SelectionChanged += GridOrders_SelectionChanged;

            gridItems.DataSource = new[]
            {
                new { MedicineName = "Amoxicillin 500mg (30 Caps)", Quantity = 1, UnitPrice = "LKR 15.00", Subtotal = "LKR 15.00" },
                new { MedicineName = "Lisinopril 10mg (90 Tabs)", Quantity = 1, UnitPrice = "LKR 30.00", Subtotal = "LKR 30.00" }
            };

            lblOrderDetails.Text = "Order Details: #ORD-9420";
            cmbStatus.SelectedItem = "Ready for Pickup";
            lblLastUpdated.Text = "Last Updated: Today, 10:42 AM";
            lblTotalOrders.Text = "4";
            lblPendingOrders.Text = "1";
            lblDeliveredOrders.Text = "2";
        }

        public void LoadOrders()
        {
            if (IsDesignHost() || Orders == null) return;

            var all = Orders.GetAll();
            if (_statusFilter != "All")
                all = all.Where(o => o.Status == _statusFilter).ToList();

            gridOrders.DataSource = all.Select(o => new
            {
                o.OrderID,
                OrderRef = $"#SM-{o.OrderID:D4}",
                o.CustomerName,
                OrderDate = o.OrderDate.ToString("MMM dd, yyyy"),
                o.Status,
                Total = $"LKR {o.TotalAmount:N2}"
            }).ToList();

            HideOrderIdColumn();
            UpdateStats(all);
            ClearSelection();
        }

        private void HideOrderIdColumn()
        {
            if (gridOrders.Columns.Contains("OrderID"))
                gridOrders.Columns["OrderID"].Visible = false;
        }

        private void UpdateStats(List<Order> all)
        {
            Orders.GetStatusStats(all, out var pending, out var delivered);
            lblTotalOrders.Text = all.Count.ToString("N0");
            lblPendingOrders.Text = pending.ToString("N0");
            lblDeliveredOrders.Text = delivered.ToString("N0");
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (IsDesignHost())
                return;

            if (gridOrders.CurrentRow == null) return;
            var idCell = gridOrders.CurrentRow.Cells["OrderID"];
            if (idCell?.Value == null) return;

            _selectedOrderId = Convert.ToInt32(idCell.Value);
            var status = gridOrders.CurrentRow.Cells["Status"].Value?.ToString() ?? "Pending";
            var orderRef = gridOrders.CurrentRow.Cells["OrderRef"].Value?.ToString() ?? string.Empty;

            lblOrderDetails.Text = $"Order Details: {orderRef}";
            cmbStatus.SelectedItem = status;
            if (cmbStatus.SelectedIndex < 0)
                cmbStatus.Text = status;

            lblLastUpdated.Text = $"Last Updated: {DateTime.Now:MMM dd, yyyy hh:mm tt}";

            if (Orders == null)
                return;

            var items = Orders.GetItems(_selectedOrderId.Value);
            gridItems.DataSource = items.Select(i => new
            {
                i.MedicineName,
                i.Quantity,
                UnitPrice = $"LKR {i.UnitPrice:N2}",
                Subtotal = $"LKR {i.Subtotal:N2}"
            }).ToList();
        }

        private void ClearSelection()
        {
            _selectedOrderId = null;
            gridOrders.ClearSelection();
            gridItems.DataSource = null;
            lblOrderDetails.Text = "Order Details";
            cmbStatus.SelectedIndex = -1;
            lblLastUpdated.Text = "Last Updated: —";
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (!_selectedOrderId.HasValue)
            {
                MessageBox.Show("Select an order from the grid to update status.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Select a valid status.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var status = cmbStatus.SelectedItem.ToString();
                if (Orders == null) return;
                Orders.UpdateStatus(_selectedOrderId.Value, status);
                LoadOrders();
                MessageBox.Show("Order status updated successfully.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
