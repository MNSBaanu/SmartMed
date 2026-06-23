using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class ManageCustomersForm : AdminShellForm
    {
        private bool _pageBuilt;

        public ManageCustomersForm()
            : base(AdminNavItem.Customers, "Manage Customers")
        {
            InitializeComponent();
        }

        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildPageContent();
            if (!IsDesignHost())
                LoadCustomers();
        }

        private CustomerService _customers;
        private int? _selectedId;
        private bool _dataLoaded;

        private DataGridView gridCustomers;
        private TextBox txtName;
        private TextBox txtEmail;
        private TextBox txtPhone;
        private TextBox txtAddress;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Label lblTotalCustomers;
        private Label lblWithOrders;
        private Label lblWithoutOrders;
        private TableLayoutPanel _scrollRoot;

        private void BuildPageContent() {
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            
        }

        private CustomerService Customers
        {
            get
            {
                if (IsDesignHost()) return null;
                return _customers ?? (_customers = new CustomerService());
            }
        }

        private void LoadPageData(object sender, EventArgs e)
        {
            if (_dataLoaded) return;
            _dataLoaded = true;
            if (!IsDesignHost())
                LoadCustomers();
        }

        private int GetScrollContentWidth()
        {
            var w = panelContent.ClientSize.Width;
            if (w < 200 && Parent != null)
                w = Parent.ClientSize.Width - 48;
            if (w < 200)
                w = 850;
            return w;
        }

        private void BuildContent()
        {
            panelContent.Controls.Clear();

            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 4,
                MinimumSize = new Size(0, 900),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateGridPanel(), 0, 2);
            _scrollRoot.Controls.Add(CreateFormPanel(), 0, 3);

            panelContent.Controls.Add(_scrollRoot);
            panelContent.Resize += (s, e) =>
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
                Height = 44,
                Padding = new Padding(0, 0, 0, 8),
                Margin = new Padding(0, 0, 0, 16)
            };
            header.Paint += (s, e) =>
            {
                using (var pen = new Pen(SystemColors.ControlDark))
                    e.Graphics.DrawLine(pen, 0, header.Height - 1, header.Width, header.Height - 1);
            };

            var titleBlock = new Panel { Dock = DockStyle.Left, Width = 480 };
            titleBlock.Controls.Add(new Label
            {
                Text = "View and update registered customer contact details.",
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
            var btnExport = CreateToolbarButton("Export");
            btnExport.Click += (s, e) => ShowComingSoon("Export");
            var btnPrint = CreateToolbarButton("Print");
            btnPrint.Click += (s, e) => ShowComingSoon("Print");
            actions.Controls.Add(btnExport);
            actions.Controls.Add(btnPrint);

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
                Width = 100,
                Margin = new Padding(4, 0, 0, 0)
            };
            return btn;
        }

        private Panel CreateGridPanel()
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

            gridCustomers = new DataGridView
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
            gridCustomers.SelectionChanged += GridCustomers_SelectionChanged;

            outer.Controls.Add(gridCustomers);
            return outer;
        }

        private Panel CreateFormPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = SystemColors.Control,
                Padding = new Padding(24),
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

            var columns = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                ColumnCount = 2,
                RowCount = 1,
                Height = 220
            };
            columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            txtName = new TextBox();
            txtEmail = new TextBox();
            txtPhone = new TextBox();
            txtAddress = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Height = 23 + 52
            };

            columns.Controls.Add(CreateFieldColumn(
                CreateField("Full Name", txtName),
                CreateField("Email", txtEmail),
                CreateField("Phone", txtPhone)), 0, 0);

            columns.Controls.Add(CreateFieldColumn(
                CreateAddressField("Residential Address", txtAddress)), 1, 0);

            btnClear = new Button { Text = "Clear Form", Width = 110, Height = 40 };
            btnDelete = new Button { Text = "Delete Entry", Width = 110, Height = 40 };
            btnUpdate = new Button { Text = "Update Record", Width = 120, Height = 40 };
            btnAdd = new Button { Text = "Add New Customer", Width = 150, Height = 40 };

            btnClear.Click += (s, e) => ClearForm();
            btnDelete.Click += BtnDelete_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnAdd.Click += BtnAdd_Click;

            var actions = new Panel { Dock = DockStyle.Top, Height = 56, Padding = new Padding(0, 16, 0, 0) };
            actions.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(128, SystemColors.ControlDark)))
                    e.Graphics.DrawLine(pen, 0, 0, actions.Width, 0);
            };
            var actionFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            actionFlow.Controls.Add(btnClear);
            actionFlow.Controls.Add(btnDelete);
            actionFlow.Controls.Add(new Panel { Width = 200, Height = 1 });
            actionFlow.Controls.Add(btnUpdate);
            actionFlow.Controls.Add(btnAdd);
            actions.Controls.Add(actionFlow);

            outer.Controls.Add(actions);
            outer.Controls.Add(columns);
            return outer;
        }

        private static Panel CreateFieldColumn(params Control[] fields)
        {
            var col = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 16, 0) };
            foreach (var field in fields)
            {
                field.Dock = DockStyle.Top;
                col.Controls.Add(field);
            }
            return col;
        }

        private Panel CreateField(string labelText, Control input)
        {
            var wrap = new Panel { Height = 23 + 24, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 8) };
            var lbl = new Label { Text = labelText, Dock = DockStyle.Top, Height = 20 };
            input.Dock = DockStyle.Top;
            input.Height = 23;
            if (input is TextBox tb) { tb.BorderStyle = BorderStyle.Fixed3D; }
            wrap.Controls.Add(input);
            wrap.Controls.Add(lbl);
            return wrap;
        }

        private Panel CreateAddressField(string labelText, TextBox input)
        {
            var wrap = new Panel { Height = 23 + 76, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 8) };
            var lbl = new Label { Text = labelText, Dock = DockStyle.Top, Height = 20 };
            input.Dock = DockStyle.Top;
            wrap.Controls.Add(input);
            wrap.Controls.Add(lbl);
            return wrap;
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

            lblTotalCustomers = new Label();
            lblWithOrders = new Label();
            lblWithoutOrders = new Label();

            row.Controls.Add(CreateStatTile("Total Customers", lblTotalCustomers, SystemColors.Highlight), 0, 0);
            row.Controls.Add(CreateStatTile("With Orders", lblWithOrders, SystemColors.ControlText), 1, 0);
            row.Controls.Add(CreateStatTile("Without Orders", lblWithoutOrders, Color.Red), 2, 0);

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

        private void LoadDesignTimePreview()
        {
            gridCustomers.DataSource = new[]
            {
                new { CustomerID = 1, Name = "Alice Thompson", Email = "a.thompson@email.com", Phone = "(555) 012-3456", Address = "123 Pine St, Seattle, WA 98101" },
                new { CustomerID = 2, Name = "Robert Miller", Email = "r.miller88@email.com", Phone = "(555) 987-6543", Address = "456 Oak Lane, Portland, OR 97205" },
                new { CustomerID = 3, Name = "Elena Rodriguez", Email = "elena.rod@provider.net", Phone = "(555) 234-5678", Address = "789 Maple Ave, San Francisco, CA 94103" }
            };
            HideCustomerIdColumn();

            txtName.Text = "Robert Miller";
            txtEmail.Text = "r.miller88@email.com";
            txtPhone.Text = "(555) 987-6543";
            txtAddress.Text = "456 Oak Lane,\r\nApartment 12B,\r\nPortland, OR 97205";

            lblTotalCustomers.Text = "4";
            lblWithOrders.Text = "2";
            lblWithoutOrders.Text = "2";
        }

        public void LoadCustomers()
        {
            if (IsDesignHost() || Customers == null) return;

            var all = Customers.GetAll();
            gridCustomers.DataSource = all.Select(c => new
            {
                c.CustomerID,
                Name = c.Name,
                c.Email,
                c.Phone,
                c.Address
            }).ToList();

            HideCustomerIdColumn();
            UpdateStats(all);
        }

        private void HideCustomerIdColumn()
        {
            if (gridCustomers.Columns.Contains("CustomerID"))
                gridCustomers.Columns["CustomerID"].Visible = false;
        }

        private void UpdateStats(List<Customer> all)
        {
            Customers.GetOrderStats(all, out var withOrders, out var withoutOrders);
            lblTotalCustomers.Text = all.Count.ToString("N0");
            lblWithOrders.Text = withOrders.ToString("N0");
            lblWithoutOrders.Text = withoutOrders.ToString("N0");
        }

        private void GridCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (gridCustomers.CurrentRow == null) return;
            var idCell = gridCustomers.CurrentRow.Cells["CustomerID"];
            if (idCell?.Value == null) return;

            _selectedId = Convert.ToInt32(idCell.Value);
            var customer = Customers.GetById(_selectedId.Value);
            if (customer == null) return;

            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
        }

        private Customer ReadForm()
        {
            return new Customer
            {
                CustomerID = _selectedId ?? 0,
                Name = txtName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                Phone = txtPhone.Text.Trim(),
                Address = txtAddress.Text.Trim()
            };
        }

        private void ClearForm()
        {
            _selectedId = null;
            txtName.Clear();
            txtEmail.Clear();
            txtPhone.Clear();
            txtAddress.Clear();
            gridCustomers.ClearSelection();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Customers.Add(ReadForm());
                ClearForm();
                LoadCustomers();
                MessageBox.Show("Customer added successfully.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a customer from the grid to update.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var customer = ReadForm();
                customer.CustomerID = _selectedId.Value;
                Customers.Update(customer);
                LoadCustomers();
                MessageBox.Show("Customer updated successfully.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a customer from the grid to delete.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Delete this customer record?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                Customers.Delete(_selectedId.Value);
                ClearForm();
                LoadCustomers();
                MessageBox.Show("Customer deleted.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
