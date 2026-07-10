using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ManageHealthServicesForm : EmbeddedPageForm
    {
        private HealthServiceService _health;
        private CustomerService _customers;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public ManageHealthServicesForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _health = new HealthServiceService();
                _customers = new CustomerService();
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

        protected override void DoRefreshPage()
        {
            if (!_servicesReady || !_runtimeWired)
                return;

            LoadData();
        }

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();
            BindDesignTimeGrids();
        }

        private void BindDesignTimeGrids()
        {
            UiTheme.SetGridDataSource(gridServices, DesignTimePreviewData.HealthServiceRows());
            UiTheme.SetGridDataSource(gridRecords, DesignTimePreviewData.HealthServiceRecordRows());
            UiTheme.BeautifyGridHeaders(gridServices);
            UiTheme.BeautifyGridHeaders(gridRecords);
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);
            UiTheme.ApplyClinicalGrid(gridServices);
            UiTheme.ApplyClinicalGrid(gridRecords);
            UiTheme.StyleTextBox(txtRecordSearch);
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnAddService.Click += (s, e) => ShowServiceDialog(null);
            btnAddRecord.Click += (s, e) => ShowRecordDialog(null);
            btnReload.Click += (s, e) => RefreshPage();
            txtRecordSearch.TextChanged += (s, e) => BindRecords();
            gridServices.CellContentClick += GridServices_CellContentClick;
            gridRecords.CellContentClick += GridRecords_CellContentClick;
        }

        private void LoadData()
        {
            if (!_servicesReady)
                return;

            BindServices();
            BindRecords();
        }

        private void BindServices()
        {
            var rows = _health.GetAllServices().Select(s => new
            {
                s.ServiceID,
                s.ServiceName,
                Price = $"LKR {s.Price:N2}",
                Status = s.IsActive ? "Active" : "Inactive",
                s.Description,
                Edit = "Edit",
                Remove = s.IsActive ? "Deactivate" : "Activate"
            }).ToList();

            UiTheme.SetGridDataSource(gridServices, rows);
            if (gridServices.Columns.Contains("ServiceID"))
                gridServices.Columns["ServiceID"].Visible = false;
            UiTheme.BeautifyGridHeaders(gridServices);
            AddButtonColumns(gridServices, ("Edit", 60), ("Remove", 80));
        }

        private void BindRecords()
        {
            var keyword = txtRecordSearch?.Text?.Trim() ?? string.Empty;
            var rows = _health.GetAllRecords()
                .Where(r => string.IsNullOrEmpty(keyword)
                    || (r.CustomerName?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                    || (r.ServiceName?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0
                    || (r.Result?.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) ?? -1) >= 0)
                .Select(r => new
                {
                    r.RecordID,
                    ServiceDate = r.ServiceDate.ToString("yyyy-MM-dd"),
                    r.CustomerName,
                    r.ServiceName,
                    r.Result,
                    Pharmacist = string.IsNullOrWhiteSpace(r.PharmacistName) ? "—" : r.PharmacistName,
                    Notes = string.IsNullOrWhiteSpace(r.PharmacistNotes) ? "—" : r.PharmacistNotes,
                    Edit = "Edit",
                    Remove = "Remove"
                }).ToList();

            UiTheme.SetGridDataSource(gridRecords, rows);
            if (gridRecords.Columns.Contains("RecordID"))
                gridRecords.Columns["RecordID"].Visible = false;
            UiTheme.BeautifyGridHeaders(gridRecords);
            AddButtonColumns(gridRecords, ("Edit", 60), ("Remove", 70));
        }

        private static void AddButtonColumns(DataGridView grid, params (string Name, int Width)[] columns)
        {
            foreach (var col in columns)
            {
                if (grid.Columns.Contains(col.Name)) continue;
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = col.Name,
                    HeaderText = col.Name,
                    Text = col.Name,
                    UseColumnTextForButtonValue = true,
                    Width = col.Width,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }
        }

        private void GridServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_servicesReady || e.RowIndex < 0) return;
            var id = Convert.ToInt32(gridServices.Rows[e.RowIndex].Cells["ServiceID"].Value);
            var col = gridServices.Columns[e.ColumnIndex].Name;
            if (col == "Edit")
                ShowServiceDialog(_health.GetServiceById(id));
            else if (col == "Remove")
                ToggleServiceActive(id);
        }

        private void GridRecords_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!_servicesReady || e.RowIndex < 0) return;
            var id = Convert.ToInt32(gridRecords.Rows[e.RowIndex].Cells["RecordID"].Value);
            var col = gridRecords.Columns[e.ColumnIndex].Name;
            if (col == "Edit")
            {
                var record = _health.GetAllRecords().FirstOrDefault(r => r.RecordID == id);
                if (record != null) ShowRecordDialog(record);
            }
            else if (col == "Remove")
                RemoveRecord(id);
        }

        private void ToggleServiceActive(int serviceId)
        {
            try
            {
                var service = _health.GetServiceById(serviceId);
                if (service == null) return;
                service.IsActive = !service.IsActive;
                _health.UpdateService(service);
                RefreshPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void RemoveRecord(int recordId)
        {
            if (MessageBox.Show("Remove this service record?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                _health.DeleteRecord(recordId);
                RefreshPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Remove Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowServiceDialog(HealthService existing)
        {
            var isEdit = existing != null;
            using (var dlg = new Form
            {
                Text = isEdit ? "Edit Health Service" : "Add Health Service",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(460, 280),
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var txtName = new TextBox { Left = 16, Top = 40, Width = 420 };
                var txtDesc = new TextBox { Left = 16, Top = 96, Width = 420 };
                var txtPrice = new TextBox { Left = 16, Top = 152, Width = 140 };
                var chkActive = new CheckBox { Text = "Active", Left = 180, Top = 152, AutoSize = true, Checked = true, BackColor = UiTheme.AdminSurface };
                foreach (var tb in new[] { txtName, txtDesc, txtPrice }) UiTheme.StyleTextBox(tb);

                if (isEdit)
                {
                    txtName.Text = existing.ServiceName;
                    txtDesc.Text = existing.Description ?? string.Empty;
                    txtPrice.Text = existing.Price.ToString("F2");
                    chkActive.Checked = existing.IsActive;
                }

                dlg.Controls.Add(MakeLabel("Service Name *", 16, 20));
                dlg.Controls.Add(txtName);
                dlg.Controls.Add(MakeLabel("Description", 16, 76));
                dlg.Controls.Add(txtDesc);
                dlg.Controls.Add(MakeLabel("Price (LKR) *", 16, 132));
                dlg.Controls.Add(txtPrice);
                dlg.Controls.Add(chkActive);

                var btnSave = AdminUiHelpers.CreateWinButton(isEdit ? "Update" : "Add", true, 88);
                btnSave.Left = 264;
                btnSave.Top = 220;
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 88);
                btnCancel.Left = 358;
                btnCancel.Top = 220;
                btnCancel.DialogResult = DialogResult.Cancel;
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                btnSave.Click += (s, e) =>
                {
                    try
                    {
                        if (!decimal.TryParse(txtPrice.Text, out var price))
                            throw new ArgumentException("Enter a valid price.");
                        var item = new HealthService
                        {
                            ServiceID = existing?.ServiceID ?? 0,
                            ServiceName = txtName.Text.Trim(),
                            Description = txtDesc.Text.Trim(),
                            Price = price,
                            IsActive = chkActive.Checked
                        };
                        if (isEdit) _health.UpdateService(item);
                        else _health.AddService(item);
                        dlg.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                    RefreshPage();
            }
        }

        private void ShowRecordDialog(HealthServiceRecord existing)
        {
            var isEdit = existing != null;
            var services = _health.GetActiveServices();
            if (!isEdit && services.Count == 0)
            {
                MessageBox.Show("Register at least one active health service first.", "Health Services",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var customers = _customers.GetAll();
            using (var dlg = new Form
            {
                Text = isEdit ? "Edit Service Record" : "Record Service Delivery",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(480, 420),
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var cmbCustomer = new ComboBox { Left = 16, Top = 40, Width = 440, DropDownStyle = ComboBoxStyle.DropDownList };
                var cmbService = new ComboBox { Left = 16, Top = 96, Width = 440, DropDownStyle = ComboBoxStyle.DropDownList };
                var dtpDate = new DateTimePicker { Left = 16, Top = 152, Width = 200, Format = DateTimePickerFormat.Short };
                var txtResult = new TextBox { Left = 16, Top = 208, Width = 440 };
                var txtNotes = new TextBox { Left = 16, Top = 264, Width = 440, Height = 80, Multiline = true, ScrollBars = ScrollBars.Vertical };
                UiTheme.StyleComboBox(cmbCustomer);
                UiTheme.StyleComboBox(cmbService);
                UiTheme.StyleTextBox(txtResult);
                UiTheme.StyleTextBox(txtNotes);

                cmbCustomer.DisplayMember = "Name";
                cmbCustomer.ValueMember = "CustomerID";
                cmbCustomer.DataSource = customers.ToList();
                cmbService.DisplayMember = "ServiceName";
                cmbService.ValueMember = "ServiceID";
                cmbService.DataSource = isEdit ? _health.GetAllServices().Where(s => s.IsActive || s.ServiceID == existing.ServiceID).ToList() : services;

                if (isEdit)
                {
                    cmbCustomer.SelectedValue = existing.CustomerID;
                    cmbService.SelectedValue = existing.ServiceID;
                    dtpDate.Value = existing.ServiceDate;
                    txtResult.Text = existing.Result;
                    txtNotes.Text = existing.PharmacistNotes ?? string.Empty;
                }
                else
                {
                    dtpDate.Value = DateTime.Today;
                }

                dlg.Controls.Add(MakeLabel("Customer *", 16, 20));
                dlg.Controls.Add(cmbCustomer);
                dlg.Controls.Add(MakeLabel("Service *", 16, 76));
                dlg.Controls.Add(cmbService);
                dlg.Controls.Add(MakeLabel("Service Date *", 16, 132));
                dlg.Controls.Add(dtpDate);
                dlg.Controls.Add(MakeLabel("Result *", 16, 188));
                dlg.Controls.Add(txtResult);
                dlg.Controls.Add(MakeLabel("Pharmacist Notes", 16, 244));
                dlg.Controls.Add(txtNotes);

                var btnSave = AdminUiHelpers.CreateWinButton(isEdit ? "Update" : "Save", true, 88);
                btnSave.Left = 284;
                btnSave.Top = 360;
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 88);
                btnCancel.Left = 378;
                btnCancel.Top = 360;
                btnCancel.DialogResult = DialogResult.Cancel;
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                btnSave.Click += (s, e) =>
                {
                    try
                    {
                        var record = new HealthServiceRecord
                        {
                            RecordID = existing?.RecordID ?? 0,
                            CustomerID = Convert.ToInt32(cmbCustomer.SelectedValue),
                            ServiceID = Convert.ToInt32(cmbService.SelectedValue),
                            ServiceDate = dtpDate.Value.Date,
                            Result = txtResult.Text.Trim(),
                            PharmacistNotes = txtNotes.Text.Trim(),
                            AdminID = Session.CurrentAdmin?.AdminID
                        };
                        if (isEdit) _health.UpdateRecord(record);
                        else _health.AddRecord(record);
                        dlg.DialogResult = DialogResult.OK;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                if (dlg.ShowDialog(FindForm()) == DialogResult.OK)
                    RefreshPage();
            }
        }

        private static Label MakeLabel(string text, int left, int top) =>
            new Label
            {
                Text = text,
                Left = left,
                Top = top,
                AutoSize = true,
                ForeColor = UiTheme.AdminOnSurface,
                Font = UiTheme.UiFont
            };
    }
}
