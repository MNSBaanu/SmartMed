using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Data;
using SmartMed.Models;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Controls
{
    public partial class ManageOrdersPanel : UserControl
    {
        private static readonly string[] OrderStatuses =
        {
            "Pending", "Ready for Pickup", "Delivered"
        };

        private OrderRepository _orders;
        private int? _selectedOrderId;
        private bool _dataLoaded;

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

        public ManageOrdersPanel()
        {
            InitializeComponent();
            FontManager.Initialize();
            DoubleBuffered = true;
            Dock = DockStyle.Fill;
            BuildContent();
            if (FontManager.IsDesignHost(this))
                LoadDesignTimePreview();
            Load += ManageOrdersPanel_Load;
        }

        private OrderRepository Orders
        {
            get
            {
                if (FontManager.IsDesignHost(this)) return null;
                return _orders ?? (_orders = new OrderRepository());
            }
        }

        private void ManageOrdersPanel_Load(object sender, EventArgs e)
        {
            if (_dataLoaded) return;
            _dataLoaded = true;
            if (!FontManager.IsDesignHost(this))
                LoadOrders();
        }

        private int GetScrollContentWidth()
        {
            var w = ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            if (w < 200)
                w = 850;
            return w;
        }

        private void BuildContent()
        {
            Controls.Clear();
            AutoScroll = true;

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

            Controls.Add(_scrollRoot);
            Resize += (s, e) =>
            {
                if (_scrollRoot != null)
                    _scrollRoot.Width = GetScrollContentWidth();
            };
        }

        private Panel CreatePageHeader()
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 64,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0, 0, 0, 16)
            };
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            var titleBlock = new Panel { Dock = DockStyle.Left, Width = 520 };
            titleBlock.Controls.Add(new Label
            {
                Text = "Review fulfillment queue and update order status.",
                Font = AppTheme.BodyFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                Dock = DockStyle.Top,
                Height = 20
            });
            titleBlock.Controls.Add(new Label
            {
                Text = "Manage Orders",
                Font = AppTheme.SectionHeaderFont,
                ForeColor = AppTheme.Primary,
                Dock = DockStyle.Top,
                Height = 24
            });

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };
            var btnFilter = CreateToolbarButton("\uE71C", "Filter");
            btnFilter.Click += (s, e) => ShowComingSoon("Filter");
            var btnNew = CreateToolbarButton("\uE710", "New Order");
            btnNew.Click += (s, e) => ShowComingSoon("New Order");
            actions.Controls.Add(btnFilter);
            actions.Controls.Add(btnNew);

            header.Controls.Add(actions);
            header.Controls.Add(titleBlock);
            return header;
        }

        private static Button CreateToolbarButton(string icon, string text)
        {
            var btn = new Button
            {
                Text = $"  {icon}  {text}",
                Height = 32,
                Width = 110,
                Margin = new Padding(4, 0, 0, 0)
            };
            ThemeApplier.ApplyPrimaryButton(btn);
            return btn;
        }

        private static void ShowComingSoon(string feature)
        {
            MessageBox.Show($"{feature} will be available in the next update.", "SmartMed",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
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

            row.Controls.Add(CreateStatTile("\uE8A1", "Total Orders", lblTotalOrders, AppTheme.Primary), 0, 0);
            row.Controls.Add(CreateStatTile("\uE823", "Pending", lblPendingOrders, AppTheme.OnSecondaryContainer), 1, 0);
            row.Controls.Add(CreateStatTile("\uE73E", "Delivered", lblDeliveredOrders, AppTheme.Success), 2, 0);

            wrap.Controls.Add(row);
            return wrap;
        }

        private Panel CreateStatTile(string icon, string title, Label valueLabel, Color accent)
        {
            var card = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.SurfaceContainerLowest,
                Padding = new Padding(16),
                Margin = new Padding(0, 0, 8, 0)
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            valueLabel.Text = "0";
            valueLabel.Font = AppTheme.StatValueFont;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(52, 36);
            valueLabel.AutoSize = true;

            card.Controls.Add(new Label
            {
                Text = icon,
                Font = AppTheme.IconFont,
                ForeColor = accent,
                Location = new Point(16, 20),
                AutoSize = true
            });
            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = AppTheme.LabelFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                Location = new Point(52, 16),
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
                BackColor = AppTheme.SurfaceContainerLowest,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = AppTheme.SurfaceContainer,
                Padding = new Padding(12, 10, 12, 8)
            };
            header.Controls.Add(new Label
            {
                Text = "  \uE8EF  Recent Orders",
                Font = AppTheme.SectionHeaderFont,
                ForeColor = AppTheme.Primary,
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
                BackColor = AppTheme.SurfaceContainerLowest,
                Padding = new Padding(1),
                Margin = new Padding(0, 0, 0, 16)
            };
            outer.Paint += (s, e) =>
            {
                var rect = outer.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(AppTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            lblOrderDetails = new Label
            {
                Text = "  \uE7C3  Order Details",
                Font = AppTheme.SectionHeaderFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                Dock = DockStyle.Top,
                Height = 36,
                Padding = new Padding(12, 8, 0, 0),
                BackColor = AppTheme.SurfaceContainer
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
            ThemeApplier.SetBackColor(panel, AppTheme.Primary);

            cmbStatus = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Width = 200,
                Location = new Point(88, 16)
            };
            ThemeApplier.ApplyComboBox(cmbStatus);
            cmbStatus.Items.AddRange(OrderStatuses);

            lblLastUpdated = new Label
            {
                Text = "Last Updated: —",
                ForeColor = AppTheme.OnPrimaryMuted,
                Font = AppTheme.BodyFont,
                AutoSize = true,
                Location = new Point(320, 20)
            };

            btnUpdateStatus = new Button
            {
                Text = "  \uE74E  Update Status",
                Width = 150,
                Height = 40,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            ThemeApplier.ApplyAccentButton(btnUpdateStatus);
            btnUpdateStatus.Click += BtnUpdateStatus_Click;

            panel.Controls.Add(new Label
            {
                Text = "STATUS:",
                ForeColor = AppTheme.OnPrimary,
                Font = AppTheme.LabelFont,
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
                BackgroundColor = AppTheme.SurfaceContainerLowest,
                BorderStyle = BorderStyle.None,
                EnableHeadersVisualStyles = false,
                MultiSelect = false,
                ScrollBars = ScrollBars.Vertical
            };
            ThemeApplier.ApplyDataGrid(grid);
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

            lblOrderDetails.Text = "  \uE7C3  Order Details: #ORD-9420";
            cmbStatus.SelectedItem = "Ready for Pickup";
            lblLastUpdated.Text = "Last Updated: Today, 10:42 AM";
            lblTotalOrders.Text = "4";
            lblPendingOrders.Text = "1";
            lblDeliveredOrders.Text = "2";
        }

        public void LoadOrders()
        {
            if (FontManager.IsDesignHost(this) || Orders == null) return;

            var all = Orders.GetAll();
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
            lblTotalOrders.Text = all.Count.ToString("N0");
            lblPendingOrders.Text = all.Count(o => o.Status == "Pending").ToString("N0");
            lblDeliveredOrders.Text = all.Count(o => o.Status == "Delivered").ToString("N0");
        }

        private void GridOrders_SelectionChanged(object sender, EventArgs e)
        {
            if (FontManager.IsDesignHost(this))
                return;

            if (gridOrders.CurrentRow == null) return;
            var idCell = gridOrders.CurrentRow.Cells["OrderID"];
            if (idCell?.Value == null) return;

            _selectedOrderId = Convert.ToInt32(idCell.Value);
            var status = gridOrders.CurrentRow.Cells["Status"].Value?.ToString() ?? "Pending";
            var orderRef = gridOrders.CurrentRow.Cells["OrderRef"].Value?.ToString() ?? string.Empty;

            lblOrderDetails.Text = $"  \uE7C3  Order Details: {orderRef}";
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
            lblOrderDetails.Text = "  \uE7C3  Order Details";
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
                if (_selectedOrderId.Value <= 0)
                    throw new ArgumentException("Select an order to update.");
                if (string.IsNullOrWhiteSpace(status))
                    throw new ArgumentException("Status is required.");
                if (status != "Pending" && status != "Ready for Pickup" && status != "Delivered")
                    throw new ArgumentException("Invalid order status.");
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
