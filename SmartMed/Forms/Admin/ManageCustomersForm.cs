using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ManageCustomersForm : EmbeddedPageForm
    {
        private const int ActiveOrderDays = 90;

        private CustomerService _customers;
        private OrderService _orders;
        private bool _servicesReady;

        private List<CustomerRow> _allRows = new List<CustomerRow>();
        private List<CustomerRow> _filteredRows = new List<CustomerRow>();
        private int? _selectedId;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public ManageCustomersForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _customers = new CustomerService();
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

        protected override void DoRefreshPage() => LoadCustomers();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            _allRows = new List<CustomerRow>();
            _filteredRows = _allRows;
            BindPage();
            lblTotalCustomers.Text = "-";
            lblActiveCustomers.Text = "-";
            lblInactiveCustomers.Text = "-";
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridCustomers);
            ConfigureGridScrolling();
            UiTheme.StyleTextBox(txtSearch);

            WirePanelBorder(panelGridOuter);
            WireStatCard(panelStatTotal, UiTheme.AdminTeal);
            WireStatCard(panelStatActive, Color.FromArgb(16, 185, 129));
            WireStatCard(panelStatInactive, UiTheme.AdminMuted);
        }

        private void ConfigureGridScrolling()
        {
            panelScrollHost.AutoScroll = false;
            panelGridBody.AutoScroll = false;

            gridCustomers.AutoSize = false;
            gridCustomers.Dock = DockStyle.Fill;
            gridCustomers.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            gridCustomers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            gridCustomers.ScrollBars = ScrollBars.Both;
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

            btnAdd.Click += (s, e) => ShowCustomerDialog(null);
            btnReload.Click += (s, e) => RefreshPage();
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            btnExport.Click += BtnExport_Click;
            btnPrint.Click += BtnPrint_Click;
            gridCustomers.CellFormatting += GridCustomers_CellFormatting;
            gridCustomers.CellContentClick += GridCustomers_CellContentClick;
            gridCustomers.SelectionChanged += GridCustomers_SelectionChanged;
        }

        private void EditCustomer(int customerId)
        {
            var customer = _customers?.GetById(customerId);
            if (customer == null) return;
            _selectedId = customerId;
            ShowCustomerDialog(customer);
        }

        private void RemoveCustomer(int customerId)
        {
            if (MessageBox.Show("Remove this customer record?", "Confirm Remove",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _customers.Delete(customerId);
                _selectedId = null;
                RefreshPage();
                MessageBox.Show("Customer removed.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Remove Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToggleAccountStatus(int customerId, bool activate)
        {
            var action = activate ? "activate" : "deactivate";
            if (MessageBox.Show($"Are you sure you want to {action} this customer account?",
                    activate ? "Confirm Activate" : "Confirm Deactivate",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                _customers.SetAccountActive(customerId, activate);
                RefreshPage();
                MessageBox.Show(activate ? "Customer account activated." : "Customer account deactivated.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Account Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadCustomers()
        {
            if (!_servicesReady) return;

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
                        ActivityStatus = GetActivityStatus(lastOrder),
                        AccountStatus = c.IsActive ? "ENABLED" : "DISABLED",
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
            BindPage();
            UpdateStats();
        }

        private void UpdateStats()
        {
            lblTotalCustomers.Text = _allRows.Count.ToString("N0");
            lblActiveCustomers.Text = _allRows.Count(r => r.Customer?.IsActive == true).ToString("N0");
            lblInactiveCustomers.Text = _allRows.Count(r => r.Customer?.IsActive == false).ToString("N0");
        }

        private void BindPage()
        {
            var pageRows = _filteredRows
                .Select(r => new
                {
                    r.CustomerID,
                    ID = r.CustomerRef,
                    r.Name,
                    r.ContactInfo,
                    r.LastOrder,
                    Orders = r.OrderCount,
                    Activity = r.ActivityStatus,
                    AccountAction = r.Customer?.IsActive == true ? "Deactivate" : "Activate"
                })
                .ToList();

            UiTheme.SetGridDataSource(gridCustomers, pageRows);
            if (gridCustomers.Columns.Contains("colCustomerID"))
                gridCustomers.Columns["colCustomerID"].Visible = false;
            UiTheme.BeautifyGridHeaders(gridCustomers);
            EnsureGridActionColumns();

            if (gridCustomers.Rows.Count > 0)
                gridCustomers.FirstDisplayedScrollingRowIndex = 0;

            lblPageInfo.Text = pageRows.Count == 1
                ? "1 customer"
                : $"{pageRows.Count:N0} customers";
            btnPagePrev.Visible = false;
            btnPageNext.Visible = false;
        }

        private void EnsureGridActionColumns()
        {
            AddOrConfigureButtonColumn("Edit", "Edit", 68);
            AddOrConfigureButtonColumn("AccountAction", "Account", 112, "AccountAction");
            AddOrConfigureButtonColumn("Remove", "Remove", 80);

            if (gridCustomers.Columns.Contains("Edit"))
                gridCustomers.Columns["Edit"].DisplayIndex = gridCustomers.Columns.Count - 3;
            if (gridCustomers.Columns.Contains("AccountAction"))
                gridCustomers.Columns["AccountAction"].DisplayIndex = gridCustomers.Columns.Count - 2;
            if (gridCustomers.Columns.Contains("Remove"))
                gridCustomers.Columns["Remove"].DisplayIndex = gridCustomers.Columns.Count - 1;
        }

        private void AddOrConfigureButtonColumn(string name, string text, int width, string dataPropertyName = null)
        {
            if (gridCustomers.Columns[name] is DataGridViewButtonColumn existing)
            {
                existing.HeaderText = text;
                existing.Width = width;
                existing.MinimumWidth = width;
                if (!string.IsNullOrEmpty(dataPropertyName))
                {
                    existing.DataPropertyName = dataPropertyName;
                    existing.UseColumnTextForButtonValue = false;
                }
                else
                {
                    existing.Text = text;
                    existing.UseColumnTextForButtonValue = true;
                }
                return;
            }

            if (gridCustomers.Columns.Contains(name))
                gridCustomers.Columns.Remove(name);

            var column = new DataGridViewButtonColumn
            {
                Name = name,
                HeaderText = text,
                Width = width,
                MinimumWidth = width,
                FlatStyle = FlatStyle.Flat,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };

            if (!string.IsNullOrEmpty(dataPropertyName))
            {
                column.DataPropertyName = dataPropertyName;
                column.UseColumnTextForButtonValue = false;
            }
            else
            {
                column.Text = text;
                column.UseColumnTextForButtonValue = true;
            }

            gridCustomers.Columns.Add(column);
        }

        private void GridCustomers_SelectionChanged(object sender, EventArgs e)
        {
            if (gridCustomers.CurrentRow == null)
            {
                _selectedId = null;
                return;
            }
            var cell = gridCustomers.CurrentRow.Cells["colCustomerID"];
            _selectedId = cell?.Value != null ? Convert.ToInt32(cell.Value) : (int?)null;
        }

        private void ClearSelection()
        {
            _selectedId = null;
            gridCustomers.ClearSelection();
        }

        private void GridCustomers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !_servicesReady) return;

            var colName = gridCustomers.Columns[e.ColumnIndex].Name;
            if (colName != "Edit" && colName != "Remove" && colName != "AccountAction") return;

            var idCell = gridCustomers.Rows[e.RowIndex].Cells["colCustomerID"];
            if (idCell?.Value == null) return;

            var id = Convert.ToInt32(idCell.Value);
            if (colName == "Edit")
                EditCustomer(id);
            else if (colName == "AccountAction")
            {
                var row = _allRows.FirstOrDefault(r => r.CustomerID == id);
                ToggleAccountStatus(id, row?.Customer?.IsActive != true);
            }
            else
                RemoveCustomer(id);
        }

        private void GridCustomers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (UiTheme.IsSelectedRow(gridCustomers, e.RowIndex))
            {
                UiTheme.ApplySelectedRowCellStyle(e.CellStyle);
                return;
            }

            var columnName = gridCustomers.Columns[e.ColumnIndex].Name;
            if (columnName == "Edit")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.ForeColor = UiTheme.AdminTeal;
                e.CellStyle.Font = UiTheme.UiFontBold;
                return;
            }

            if (columnName == "AccountAction")
            {
                var action = e.Value?.ToString() ?? "";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.ForeColor = string.Equals(action, "Deactivate", StringComparison.OrdinalIgnoreCase)
                    ? UiTheme.Danger
                    : Color.FromArgb(16, 185, 129);
                e.CellStyle.Font = UiTheme.UiFontBold;
                return;
            }

            if (columnName == "Remove")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.ForeColor = UiTheme.Danger;
                e.CellStyle.Font = UiTheme.UiFontBold;
                return;
            }

            if (columnName != "colStatus") return;

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
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 88);
                btnCancel.Left = 318;
                btnCancel.Top = 262;
                btnCancel.DialogResult = DialogResult.Cancel;
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                UiTheme.EnableFieldNavigation(btnSave, txtName, txtEmail, txtPhone, txtAddress);

                // Validate/save inside the dialog so it stays open and keeps the entered
                // values when validation fails, instead of closing on OK first.
                btnSave.Click += (s, e) =>
                {
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
                        dlg.DialogResult = DialogResult.OK;
                        dlg.Close();
                        MessageBox.Show(isEdit ? "Customer updated." : "Customer added.", "SmartMed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                dlg.ShowDialog(FindForm());
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

        private List<Customer> GetFilteredCustomers() =>
            _filteredRows.Select(r => r.Customer).Where(c => c != null).ToList();

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                using (var dialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"SmartMed_Customers_{DateTime.Now:yyyyMMdd}.csv"
                })
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    _customers.ExportToCsv(GetFilteredCustomers(), dialog.FileName);
                    MessageBox.Show("Customers exported.", "Export",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private sealed class CustomerRow
        {
            public int CustomerID { get; set; }
            public string CustomerRef { get; set; }
            public string Name { get; set; }
            public string ContactInfo { get; set; }
            public string LastOrder { get; set; }
            public int OrderCount { get; set; }
            public string ActivityStatus { get; set; }
            public string AccountStatus { get; set; }
            public Customer Customer { get; set; }
        }
    }
}
