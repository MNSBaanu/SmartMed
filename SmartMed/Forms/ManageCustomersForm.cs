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
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                LoadCustomers();
        }

        private CustomerService _customers;
        private int? _selectedId;
        private List<Customer> _allCustomers = new List<Customer>();

        private DataGridView gridCustomers;
        private TextBox txtSearch;
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

        private CustomerService Customers => GetRuntimeService(ref _customers);

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
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // header
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // stats
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // search
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f)); // grid
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // form

            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateSearchPanel(), 0, 2);
            _scrollRoot.Controls.Add(CreateGridPanel(), 0, 3);
            _scrollRoot.Controls.Add(CreateFormPanel(), 0, 4);
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

            var titleBlock = new Panel { Dock = DockStyle.Left, Width = 480 };
            titleBlock.Controls.Add(new Label
            {
                Text = "View and update registered customer contact details.",
                Font = UiTheme.UiFont,
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
            btnExport.Click += BtnExport_Click;
            var btnPrint = CreateToolbarButton("Print");
            btnPrint.Click += BtnPrint_Click;
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

        private Panel CreateSearchPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Margin = new Padding(0, 0, 0, 12)
            };
            txtSearch = new TextBox { Width = 320 };
            var btnClearSearch = new Button { Text = "Clear", Width = 70, Height = 28 };
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            flow.Controls.Add(new Label
            {
                Text = "Search:",
                AutoSize = true,
                Padding = new Padding(0, 6, 4, 0)
            });
            flow.Controls.Add(txtSearch);
            flow.Controls.Add(new Label
            {
                Text = "(name, email, phone, or customer ID)",
                AutoSize = true,
                ForeColor = SystemColors.GrayText,
                Padding = new Padding(8, 6, 0, 0)
            });
            flow.Controls.Add(btnClearSearch);
            txtSearch.TextChanged += (s, e) => ApplySearchFilter();
            btnClearSearch.Click += (s, e) =>
            {
                txtSearch.Clear();
                ApplySearchFilter();
            };
            panel.Controls.Add(flow);
            return panel;
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
            txtPhone = new TextBox { MaxLength = 14 };
            txtAddress = new TextBox
            {
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Height = 23 + 52
            };

            columns.Controls.Add(CreateFieldColumn(
                CreateField("Full Name", txtName, required: true),
                CreateField("Email", txtEmail, required: true),
                CreateField("Phone", txtPhone, required: true)), 0, 0);

            columns.Controls.Add(CreateFieldColumn(
                CreateAddressField("Residential Address", txtAddress, required: true)), 1, 0);

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

        private Panel CreateField(string labelText, Control input, bool required = false)
        {
            var wrap = new Panel { Height = 23 + 24, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 8) };
            var lbl = new Label
            {
                Text = required ? ValidationService.RequiredLabel(labelText) : labelText,
                Dock = DockStyle.Top,
                Height = 20
            };
            input.Dock = DockStyle.Top;
            input.Height = 23;
            if (input is TextBox tb) { tb.BorderStyle = BorderStyle.Fixed3D; }
            wrap.Controls.Add(input);
            wrap.Controls.Add(lbl);
            return wrap;
        }

        private Panel CreateAddressField(string labelText, TextBox input, bool required = false)
        {
            var wrap = new Panel { Height = 23 + 76, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 8) };
            var lbl = new Label
            {
                Text = required ? ValidationService.RequiredLabel(labelText) : labelText,
                Dock = DockStyle.Top,
                Height = 20
            };
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
            valueLabel.Font = UiTheme.UiFont;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(16, 36);
            valueLabel.AutoSize = true;

            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                Location = new Point(16, 16),
                AutoSize = true
            });
            card.Controls.Add(valueLabel);
            return card;
        }

        private void LoadDesignTimePreview()
        {
            _allCustomers = new List<Customer>
            {
                new Customer { CustomerID = 1, Name = "Alice Thompson", Email = "a.thompson@email.com", Phone = "0770123456", Address = "123 Pine St, Colombo" },
                new Customer { CustomerID = 2, Name = "Robert Miller", Email = "r.miller88@email.com", Phone = "0779876543", Address = "456 Oak Lane, Kandy" },
                new Customer { CustomerID = 3, Name = "Elena Rodriguez", Email = "elena.rod@provider.net", Phone = "0772345678", Address = "789 Maple Ave, Galle" }
            };
            ApplySearchFilter();

            txtName.Text = "Robert Miller";
            txtEmail.Text = "r.miller88@email.com";
            txtPhone.Text = "0779876543";
            txtAddress.Text = "456 Oak Lane,\r\nApartment 12B,\r\nPortland, OR 97205";

            lblTotalCustomers.Text = "4";
            lblWithOrders.Text = "2";
            lblWithoutOrders.Text = "2";
        }

        public void LoadCustomers()
        {
            if (IsDesignHost() || Customers == null) return;

            _allCustomers = Customers.GetAll();
            ApplySearchFilter();
        }

        private void ApplySearchFilter()
        {
            if (gridCustomers == null) return;

            var filtered = string.IsNullOrWhiteSpace(txtSearch?.Text)
                ? _allCustomers
                : SearchService.SearchCustomers(_allCustomers, txtSearch.Text);

            BindGrid(filtered);
            if (!IsDesignHost() && Customers != null)
                UpdateStats(_allCustomers);
        }

        private void BindGrid(List<Customer> customers)
        {
            gridCustomers.DataSource = customers.Select(c => new
            {
                c.CustomerID,
                Name = c.Name,
                c.Email,
                c.Phone,
                c.Address
            }).ToList();
            HideCustomerIdColumn();
        }

        private List<Customer> GetFilteredCustomers()
        {
            return string.IsNullOrWhiteSpace(txtSearch?.Text)
                ? _allCustomers
                : SearchService.SearchCustomers(_allCustomers, txtSearch.Text);
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            if (IsDesignHost() || Customers == null) return;
            try
            {
                using (var dialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = "customers.csv"
                })
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    Customers.ExportToCsv(GetFilteredCustomers(), dialog.FileName);
                    MessageBox.Show("Customers exported.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (gridCustomers.Rows.Count == 0)
            {
                MessageBox.Show("No customers to print.", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                ExportHelper.PrintGrid(gridCustomers, "Customer List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
