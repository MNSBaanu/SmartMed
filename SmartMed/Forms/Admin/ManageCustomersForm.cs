using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ManageCustomersForm : AdminPageControl
    {
        private const int PageSize = 10;
        private const int ActiveOrderDays = 90;

        private readonly CustomerService _customers = new CustomerService();
        private readonly OrderService _orders = new OrderService();

        private List<CustomerRow> _allRows = new List<CustomerRow>();
        private List<CustomerRow> _filteredRows = new List<CustomerRow>();
        private int _currentPage = 1;
        private int? _selectedId;

        public ManageCustomersForm()
        {
            InitializeComponent();
            BuildContent();
            RefreshPage();
        }

        public override void RefreshPage()
        {
            SyncScrollRootWidth();
            LoadCustomers();
        }

        private void BuildContent()
        {
            var root = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 1,
                RowCount = 4,
                MinimumSize = new Size(0, 680)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            root.Controls.Add(CreatePageHeader(), 0, 0);
            root.Controls.Add(CreateToolbar(), 0, 1);
            root.Controls.Add(CreateGridSection(), 0, 2);
            root.Controls.Add(CreateFooterPanel(), 0, 3);

            WireScrollRoot(root);
        }

        private static Panel CreatePageHeader() =>
            AdminUiHelpers.CreatePageHeader(
                "Manage Customers",
                "View and maintain customer records, contact details, and order activity.");

        private Panel CreateToolbar()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                Margin = new Padding(0, 0, 0, 12),
                BackColor = UiTheme.AdminSurface
            };

            btnAdd = AdminUiHelpers.CreateWinButton("+ Add Customer", primary: true, width: 130);
            btnAdd.Click += (s, e) => ShowCustomerDialog(null);

            btnEdit = AdminUiHelpers.CreateWinButton("Edit", false, 72);
            btnEdit.Click += (s, e) =>
            {
                if (!_selectedId.HasValue)
                {
                    MessageBox.Show("Select a customer to edit.", "Manage Customers",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ShowCustomerDialog(_customers.GetById(_selectedId.Value));
            };

            btnRemove = AdminUiHelpers.CreateWinButton("Remove", false, 84);
            btnRemove.Click += BtnRemove_Click;

            btnReload = AdminUiHelpers.CreateWinButton("Reload", false, 84);
            btnReload.Click += (s, e) => RefreshPage();

            var left = new FlowLayoutPanel
            {
                Dock = DockStyle.Left,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = UiTheme.AdminSurface
            };
            left.Controls.Add(btnAdd);
            left.Controls.Add(btnEdit);
            left.Controls.Add(btnRemove);
            left.Controls.Add(btnReload);

            txtSearch = new TextBox { Width = 200 };
            UiTheme.StyleTextBox(txtSearch);
            txtSearch.TextChanged += (s, e) => ApplyFilters();

            var right = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                AutoSize = true,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false,
                BackColor = UiTheme.AdminSurface,
                Padding = new Padding(0, 4, 0, 0)
            };
            right.Controls.Add(new Label
            {
                Text = "Search:",
                AutoSize = true,
                Margin = new Padding(0, 6, 6, 0),
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface
            });
            right.Controls.Add(txtSearch);

            panel.Controls.Add(right);
            panel.Controls.Add(left);
            return panel;
        }

        private Panel CreateGridSection()
        {
            gridCustomers = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                MinimumSize = new Size(0, 360)
            };
            UiTheme.ApplyClinicalGrid(gridCustomers);
            gridCustomers.CellFormatting += GridCustomers_CellFormatting;
            gridCustomers.SelectionChanged += GridCustomers_SelectionChanged;

            return AdminUiHelpers.CreateSectionPanel("Customer Records", gridCustomers);
        }

        private Panel CreateFooterPanel()
        {
            var footer = new Panel
            {
                Dock = DockStyle.Top,
                Height = 108,
                Margin = new Padding(0, 16, 0, 0),
                BackColor = UiTheme.AdminSurface
            };

            lblTotalCustomers = new Label();
            lblActiveCustomers = new Label();
            lblInactiveCustomers = new Label();

            var statsRow = new TableLayoutPanel
            {
                Dock = DockStyle.Left,
                Width = 520,
                Height = 108,
                ColumnCount = 3,
                RowCount = 1
            };
            for (var i = 0; i < 3; i++)
                statsRow.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));

            statsRow.Controls.Add(AdminUiHelpers.CreateStatCard("Total Customers", lblTotalCustomers, UiTheme.AdminTeal), 0, 0);
            statsRow.Controls.Add(AdminUiHelpers.CreateStatCard("Active", lblActiveCustomers, Color.FromArgb(16, 185, 129)), 1, 0);
            statsRow.Controls.Add(AdminUiHelpers.CreateStatCard("Inactive", lblInactiveCustomers, UiTheme.AdminMuted), 2, 0);

            btnPagePrev = AdminUiHelpers.CreateWinButton("<", false, 36);
            btnPagePrev.Height = 32;
            btnPagePrev.Click += (s, e) => ChangePage(-1);
            btnPageNext = AdminUiHelpers.CreateWinButton(">", false, 36);
            btnPageNext.Height = 32;
            btnPageNext.Click += (s, e) => ChangePage(1);
            lblPageInfo = new Label
            {
                AutoSize = true,
                Text = "Page 1 of 1",
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
                Padding = new Padding(8, 38, 0, 0)
            };
            pager.Controls.Add(btnPagePrev);
            pager.Controls.Add(lblPageInfo);
            pager.Controls.Add(btnPageNext);

            footer.Controls.Add(pager);
            footer.Controls.Add(statsRow);
            return footer;
        }

        private void LoadCustomers()
        {
            var orderLookup = _orders.GetAll()
                .GroupBy(o => o.CustomerID)
                .ToDictionary(
                    g => g.Key,
                    g => new { Count = g.Count(), Last = g.Max(o => o.OrderDate) });

            _allRows = _customers.GetAll()
                .OrderByDescending(c => c.CustomerID)
                .Select(c =>
                {
                    orderLookup.TryGetValue(c.CustomerID, out var stats);
                    var lastOrder = stats?.Last;
                    return new CustomerRow
                    {
                        CustomerID = c.CustomerID,
                        CustomerRef = $"#SM-{c.CustomerID:D5}",
                        Name = c.Name,
                        ContactInfo = $"{c.Email} / {c.Phone}",
                        LastOrder = lastOrder?.ToString("yyyy-MM-dd") ?? "—",
                        OrderCount = stats?.Count ?? 0,
                        Status = GetActivityStatus(lastOrder),
                        Customer = c
                    };
                })
                .ToList();

            ApplyFilters();
        }

        private static string GetActivityStatus(DateTime? lastOrder)
        {
            if (!lastOrder.HasValue)
                return "INACTIVE";
            return (DateTime.Today - lastOrder.Value.Date).TotalDays <= ActiveOrderDays
                ? "ACTIVE"
                : "INACTIVE";
        }

        private void ApplyFilters()
        {
            var rows = _allRows.AsEnumerable();
            var term = txtSearch?.Text?.Trim();
            if (!string.IsNullOrWhiteSpace(term))
            {
                rows = rows.Where(r =>
                    r.CustomerRef.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                    || r.Name.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0
                    || r.ContactInfo.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            _filteredRows = rows.ToList();
            _currentPage = 1;
            BindPage();
            UpdateStats();
        }

        private void UpdateStats()
        {
            lblTotalCustomers.Text = _allRows.Count.ToString("N0");
            lblActiveCustomers.Text = _allRows.Count(r => r.Status == "ACTIVE").ToString("N0");
            lblInactiveCustomers.Text = _allRows.Count(r => r.Status == "INACTIVE").ToString("N0");
        }

        private void BindPage()
        {
            var totalPages = Math.Max(1, (int)Math.Ceiling(_filteredRows.Count / (double)PageSize));
            if (_currentPage > totalPages) _currentPage = totalPages;

            var pageRows = _filteredRows
                .Skip((_currentPage - 1) * PageSize)
                .Take(PageSize)
                .Select(r => new
                {
                    r.CustomerID,
                    ID = r.CustomerRef,
                    r.Name,
                    r.ContactInfo,
                    r.LastOrder,
                    Orders = r.OrderCount,
                    r.Status
                })
                .ToList();

            UiTheme.SetGridDataSource(gridCustomers, pageRows);
            if (gridCustomers.Columns.Contains("CustomerID"))
                gridCustomers.Columns["CustomerID"].Visible = false;
            UiTheme.BeautifyGridHeaders(gridCustomers);

            lblPageInfo.Text = $"Page {_currentPage} of {totalPages}";
            btnPagePrev.Enabled = _currentPage > 1;
            btnPageNext.Enabled = _currentPage < totalPages;
        }

        private void ChangePage(int delta)
        {
            _currentPage += delta;
            BindPage();
        }

        private void GridCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (gridCustomers.CurrentRow == null)
            {
                _selectedId = null;
                return;
            }
            var cell = gridCustomers.CurrentRow.Cells["CustomerID"];
            _selectedId = cell?.Value != null ? Convert.ToInt32(cell.Value) : (int?)null;
        }

        private void ClearSelection()
        {
            _selectedId = null;
            gridCustomers.ClearSelection();
        }

        private void GridCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (gridCustomers.Columns[e.ColumnIndex].Name != "Status") return;

            var status = e.Value?.ToString() ?? "";
            if (string.Equals(status, "ACTIVE", StringComparison.OrdinalIgnoreCase))
            {
                e.CellStyle.BackColor = Color.FromArgb(184, 237, 226);
                e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
            else
            {
                e.CellStyle.BackColor = Color.FromArgb(232, 239, 238);
                e.CellStyle.ForeColor = UiTheme.AdminMuted;
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a customer to remove.", "Manage Customers",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Remove this customer record?", "Confirm Remove",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _customers.Delete(_selectedId.Value);
                ClearSelection();
                RefreshPage();
                MessageBox.Show("Customer removed.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Remove Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowCustomerDialog(Customer existing)
        {
            var isEdit = existing != null;
            using (var dlg = new Form
            {
                Text = isEdit ? "Edit Customer" : "Add New Customer",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(420, 300),
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var txtName = new TextBox { Left = 16, Top = 40, Width = 380 };
                var txtEmail = new TextBox { Left = 16, Top = 96, Width = 380 };
                var txtPhone = new TextBox { Left = 16, Top = 152, Width = 380, MaxLength = 14 };
                var txtAddress = new TextBox
                {
                    Left = 16,
                    Top = 208,
                    Width = 380,
                    Height = 48,
                    Multiline = true
                };
                UiTheme.StyleTextBox(txtName);
                UiTheme.StyleTextBox(txtEmail);
                UiTheme.StyleTextBox(txtPhone);
                UiTheme.StyleTextBox(txtAddress);

                if (isEdit)
                {
                    txtName.Text = existing.Name;
                    txtEmail.Text = existing.Email;
                    txtPhone.Text = existing.Phone;
                    txtAddress.Text = existing.Address;
                }

                dlg.Controls.Add(MakeFieldLabel("Full Name", 16, 24));
                dlg.Controls.Add(txtName);
                dlg.Controls.Add(MakeFieldLabel("Email", 16, 80));
                dlg.Controls.Add(txtEmail);
                dlg.Controls.Add(MakeFieldLabel("Phone", 16, 136));
                dlg.Controls.Add(txtPhone);
                dlg.Controls.Add(MakeFieldLabel("Address", 16, 192));
                dlg.Controls.Add(txtAddress);

                var btnSave = AdminUiHelpers.CreateWinButton(isEdit ? "Update" : "Add", true, 88);
                btnSave.Left = 224;
                btnSave.Top = 262;
                btnSave.DialogResult = DialogResult.OK;
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 88);
                btnCancel.Left = 318;
                btnCancel.Top = 262;
                btnCancel.DialogResult = DialogResult.Cancel;
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(FindForm()) != DialogResult.OK)
                    return;

                var customer = new Customer
                {
                    CustomerID = isEdit ? existing.CustomerID : 0,
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim()
                };

                try
                {
                    if (isEdit)
                        _customers.Update(customer);
                    else
                        _customers.Add(customer);
                    RefreshPage();
                    MessageBox.Show(isEdit ? "Customer updated." : "Customer added.", "SmartMed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static Label MakeFieldLabel(string text, int left, int top) =>
            new Label
            {
                Text = text,
                Left = left,
                Top = top,
                AutoSize = true,
                ForeColor = UiTheme.AdminMuted,
                BackColor = UiTheme.AdminSurface
            };

        private sealed class CustomerRow
        {
            public int CustomerID { get; set; }
            public string CustomerRef { get; set; }
            public string Name { get; set; }
            public string ContactInfo { get; set; }
            public string LastOrder { get; set; }
            public int OrderCount { get; set; }
            public string Status { get; set; }
            public Customer Customer { get; set; }
        }
    }
}
