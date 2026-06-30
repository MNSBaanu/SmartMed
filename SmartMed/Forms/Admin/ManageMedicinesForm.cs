using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ManageMedicinesForm : EmbeddedPageForm
    {
        private static readonly string[] DefaultCategories =
        {
            "Antibiotic", "Analgesic", "Antidiabetic", "Hypertension", "Antiviral", "Vitamin", "Other"
        };

        private MedicineService _medicines;
        private MedicineService _medicineRules;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        private List<Medicine> _allMedicines = new List<Medicine>();
        private List<string> _expiryAlertLines = new List<string>();
        private int? _selectedId;

        private MedicineService Rules =>
            _servicesReady ? _medicines : (_medicineRules ?? (_medicineRules = new MedicineService()));

        public ManageMedicinesForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _medicines = new MedicineService();
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

        protected override void DoRefreshPage() => LoadMedicines();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            _allMedicines = DesignTimePreviewData.Medicines();
            RefreshCategoryFilter();
            BindGrid(_allMedicines);
            UpdateStats(_allMedicines);
            UpdateExpiryAlerts(_allMedicines);
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(gridMedicines);
            UiTheme.StyleTextBox(txtSearch);
            UiTheme.StyleTextBox(txtMinPrice);
            UiTheme.StyleTextBox(txtMaxPrice);
            UiTheme.StyleComboBox(cmbCategory);

            WirePanelBorder(panelGridOuter);
            WireExpiryPanelBorder(panelExpiryAlerts);
            WireStatCard(panelStatTotal, UiTheme.AdminTeal);
            WireStatCard(panelStatLow, UiTheme.Danger);
            WireStatCard(panelStatCompliance, Color.FromArgb(16, 185, 129));

            if (cmbCategory.Items.Count == 0)
            {
                cmbCategory.Items.Add("All categories");
                cmbCategory.SelectedIndex = 0;
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

        private static void WireExpiryPanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "expiry-border") return;
            panel.Tag = "expiry-border";
            panel.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(200, 120, 0)))
                    e.Graphics.DrawRectangle(pen, 0, 0, panel.Width - 1, panel.Height - 1);
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

            btnAdd.Click += (s, e) => ShowMedicineDialog(null);
            btnEdit.Click += BtnEdit_Click;
            btnRemove.Click += BtnRemove_Click;
            btnReload.Click += (s, e) => RefreshPage();
            btnExport.Click += BtnExport_Click;
            btnPrint.Click += BtnPrint_Click;
            btnViewExpiryAlerts.Click += BtnViewExpiryAlerts_Click;
            btnClear.Click += BtnClear_Click;
            txtSearch.TextChanged += (s, e) => ApplyFilters();
            cmbCategory.SelectedIndexChanged += (s, e) => ApplyFilters();
            txtMinPrice.TextChanged += (s, e) => ApplyFilters();
            txtMaxPrice.TextChanged += (s, e) => ApplyFilters();
            gridMedicines.CellFormatting += GridMedicines_CellFormatting;
            gridMedicines.RowPrePaint += GridMedicines_RowPrePaint;
            gridMedicines.SelectionChanged += GridMedicines_SelectionChanged;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a medicine to edit.", "Manage Medicines",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ShowMedicineDialog(_medicines.GetById(_selectedId.Value));
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cmbCategory.SelectedIndex = 0;
            txtMinPrice.Clear();
            txtMaxPrice.Clear();
            ApplyFilters();
        }

        private void LoadMedicines()
        {
            if (!_servicesReady) return;

            _allMedicines = _medicines.GetAll();
            RefreshCategoryFilter();
            ApplyFilters();
        }

        private void RefreshCategoryFilter()
        {
            var selected = cmbCategory?.SelectedItem?.ToString();
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add("All categories");
            var categories = DefaultCategories
                .Concat(_allMedicines.Select(m => m.Category).Where(c => !string.IsNullOrWhiteSpace(c)))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c);
            foreach (var cat in categories)
                cmbCategory.Items.Add(cat);
            cmbCategory.SelectedIndex = 0;
            if (selected != null)
            {
                var idx = cmbCategory.Items.IndexOf(selected);
                if (idx >= 0) cmbCategory.SelectedIndex = idx;
            }
        }

        private List<Medicine> GetFilteredMedicines()
        {
            decimal? minPrice = decimal.TryParse(txtMinPrice?.Text?.Trim(), out var min) ? min : (decimal?)null;
            decimal? maxPrice = decimal.TryParse(txtMaxPrice?.Text?.Trim(), out var max) ? max : (decimal?)null;
            var category = cmbCategory?.SelectedIndex > 0 ? cmbCategory.SelectedItem?.ToString() : null;
            if (_servicesReady)
                return _medicines.Search(txtSearch?.Text ?? string.Empty, category, minPrice, maxPrice);
            return SearchService.Search(_allMedicines, txtSearch?.Text ?? string.Empty, category, minPrice, maxPrice);
        }

        private void ApplyFilters()
        {
            if (gridMedicines == null) return;
            var filtered = GetFilteredMedicines();
            BindGrid(filtered);
            UpdateStats(_allMedicines);
            UpdateExpiryAlerts(_allMedicines);
        }

        private void BindGrid(List<Medicine> items)
        {
            var keepId = _selectedId;
            var rows = items.Select(m => new
            {
                m.MedicineID,
                ID = $"#M-{m.MedicineID:D4}",
                Name = m.MedicineName,
                m.Category,
                Stock = m.StockQuantity,
                Price = Rules.GetEffectivePrice(m).ToString("N2"),
                Expiry = m.ExpiryDate.ToString("yyyy-MM-dd"),
                Rx = m.RequiresPrescription ? "Rx" : "—",
                Discount = $"{m.DiscountPercent:N0}%",
                StartDate = FormatPromoDate(m.PromotionStartDate),
                EndDate = FormatPromoDate(m.PromotionEndDate),
                Promo = FormatPromotionStatus(m),
                Status = GetStatusLabel(m)
            }).ToList();

            UiTheme.SetGridDataSource(gridMedicines, rows);
            if (gridMedicines.Columns.Contains("MedicineID"))
                gridMedicines.Columns["MedicineID"].Visible = false;
            UiTheme.BeautifyGridHeaders(gridMedicines);

            if (keepId.HasValue)
                SelectGridRowById(keepId.Value);
        }

        private string GetStatusLabel(Medicine m)
        {
            var expiry = Rules.CheckExpiry(m);
            if (expiry == MedicineService.ExpiryExpired) return "Expired";
            if (expiry == MedicineService.ExpiryExpiringSoon) return "Expiring Soon";
            if (Rules.IsLowStock(m)) return "Low Stock";
            return "In Stock";
        }

        private void SelectGridRowById(int id)
        {
            foreach (DataGridViewRow row in gridMedicines.Rows)
            {
                if (row.IsNewRow || row.Cells["MedicineID"]?.Value == null) continue;
                if (Convert.ToInt32(row.Cells["MedicineID"].Value) != id) continue;
                row.Selected = true;
                if (row.Cells.Count > 1)
                    gridMedicines.CurrentCell = row.Cells[1];
                return;
            }
        }

        private void UpdateStats(List<Medicine> all)
        {
            lblTotalItems.Text = all.Count.ToString("N0");
            lblLowStock.Text = all.Count(m => Rules.IsLowStock(m)).ToString("N0");
            lblExpiringSoon.Text = $"{Rules.CompliancePercent(all):N1}%";
        }

        private void UpdateExpiryAlerts(List<Medicine> all)
        {
            if (lblExpirySummary == null) return;

            var expired = Rules.CountExpired(all);
            var expiring = Rules.CountExpiringSoon(all);
            _expiryAlertLines = BuildExpiryAlertLines(all);

            if (expired == 0 && expiring == 0)
            {
                lblExpirySummary.Text = "No expiry alerts. All medicines are within safe expiry dates.";
                lblExpirySummary.ForeColor = Color.FromArgb(0, 100, 0);
                btnViewExpiryAlerts.Visible = false;
                return;
            }

            var parts = new List<string>();
            if (expired > 0) parts.Add($"{expired} expired");
            if (expiring > 0) parts.Add($"{expiring} expiring within 30 days");
            lblExpirySummary.Text = string.Join(" · ", parts);
            lblExpirySummary.ForeColor = expired > 0 ? Color.DarkRed : Color.FromArgb(140, 70, 0);
            btnViewExpiryAlerts.Text = $"View All Alerts ({_expiryAlertLines.Count})";
            btnViewExpiryAlerts.Visible = true;
        }

        private List<string> BuildExpiryAlertLines(IEnumerable<Medicine> all) =>
            all
                .Where(m => Rules.CheckExpiry(m) != MedicineService.ExpiryValid)
                .OrderBy(m => m.ExpiryDate)
                .Select(m =>
                {
                    var status = Rules.CheckExpiry(m) == MedicineService.ExpiryExpired
                        ? "Expired"
                        : "Expiring soon";
                    return $"{status} — {m.MedicineName} (exp. {m.ExpiryDate:yyyy-MM-dd})";
                })
                .ToList();

        private void BtnViewExpiryAlerts_Click(object sender, EventArgs e)
        {
            if (_expiryAlertLines.Count == 0) return;

            using (var dlg = new Form
            {
                Text = "Expiry Alerts",
                StartPosition = FormStartPosition.CenterParent,
                Width = 520,
                Height = 420,
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var list = new ListBox
                {
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.FixedSingle,
                    IntegralHeight = false,
                    Font = UiTheme.UiFont
                };
                list.Items.AddRange(_expiryAlertLines.ToArray());
                var btnClose = AdminUiHelpers.CreateWinButton("Close", false, 88);
                btnClose.Dock = DockStyle.Bottom;
                btnClose.Height = 36;
                btnClose.DialogResult = DialogResult.OK;
                dlg.Controls.Add(btnClose);
                dlg.Controls.Add(list);
                dlg.AcceptButton = btnClose;
                dlg.ShowDialog(FindForm());
            }
        }

        private static string FormatPromoDate(DateTime? date) =>
            date?.ToString("yyyy-MM-dd") ?? "—";

        private string FormatPromotionStatus(Medicine m)
        {
            if (!m.IsOnPromotion) return "No";
            return Rules.IsPromotionActive(m) ? "Active" : "Scheduled";
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                var items = GetFilteredMedicines();
                using (var dialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv",
                    FileName = $"SmartMed_Inventory_{DateTime.Now:yyyyMMdd}.csv"
                })
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    _medicines.ExportToCsv(items, dialog.FileName);
                    MessageBox.Show("Inventory exported successfully.", "SmartMed",
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
            try
            {
                var items = GetFilteredMedicines();
                _medicines.PrintInventory(items, "SmartMed — Medicine Inventory");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridMedicines_SelectionChanged(object sender, EventArgs e)
        {
            if (gridMedicines.CurrentRow == null || gridMedicines.CurrentRow.IsNewRow)
            {
                _selectedId = null;
                return;
            }
            var cell = gridMedicines.CurrentRow.Cells["MedicineID"];
            _selectedId = cell?.Value != null ? Convert.ToInt32(cell.Value) : (int?)null;
        }

        private void GridMedicines_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || gridMedicines.Rows[e.RowIndex].IsNewRow) return;
            var idCell = gridMedicines.Rows[e.RowIndex].Cells["MedicineID"];
            if (idCell?.Value == null) return;
            var item = _allMedicines.FirstOrDefault(m => m.MedicineID == Convert.ToInt32(idCell.Value));
            if (item == null) return;

            var row = gridMedicines.Rows[e.RowIndex];
            if (Rules.CheckExpiry(item) == MedicineService.ExpiryExpired)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                row.DefaultCellStyle.ForeColor = Color.DarkRed;
            }
            else
            {
                row.DefaultCellStyle.BackColor = Color.White;
                row.DefaultCellStyle.ForeColor = SystemColors.ControlText;
            }
        }

        private void GridMedicines_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || gridMedicines.Rows[e.RowIndex].Cells["MedicineID"]?.Value == null)
                return;

            var id = Convert.ToInt32(gridMedicines.Rows[e.RowIndex].Cells["MedicineID"].Value);
            var item = _allMedicines.FirstOrDefault(m => m.MedicineID == id);
            if (item == null) return;

            var columnName = gridMedicines.Columns[e.ColumnIndex].Name;
            var expiryStatus = Rules.CheckExpiry(item);

            if (columnName == "Stock")
            {
                e.CellStyle.ForeColor = Color.White;
                e.CellStyle.Font = UiTheme.UiFontBold;
                if (item.StockQuantity <= 20)
                    e.CellStyle.BackColor = Color.FromArgb(220, 53, 69);
                else if (item.StockQuantity <= 50)
                    e.CellStyle.BackColor = Color.FromArgb(255, 193, 7);
                else
                    e.CellStyle.BackColor = Color.FromArgb(40, 167, 69);
                e.Value = $"{item.StockQuantity}";
            }
            else if (columnName == "Status")
            {
                var status = e.Value?.ToString() ?? "";
                if (string.Equals(status, "Expired", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 220, 220);
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(status, "Expiring Soon", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(140, 70, 0);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(status, "Low Stock", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 242, 230);
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(184, 237, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
            }
            else if (columnName == "Rx")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (item.RequiresPrescription)
                {
                    e.CellStyle.BackColor = Color.FromArgb(232, 239, 255);
                    e.CellStyle.ForeColor = Color.FromArgb(37, 99, 235);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else
                {
                    e.CellStyle.ForeColor = UiTheme.AdminMuted;
                }
            }
            else if (columnName == "Expiry" &&
                     (expiryStatus == MedicineService.ExpiryExpired || expiryStatus == MedicineService.ExpiryExpiringSoon))
            {
                e.CellStyle.ForeColor = expiryStatus == MedicineService.ExpiryExpired ? Color.DarkRed : Color.DarkOrange;
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            if (!_selectedId.HasValue)
            {
                MessageBox.Show("Select a medicine to remove.", "Manage Medicines",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (MessageBox.Show("Remove this medicine from inventory?", "Confirm Remove",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _medicines.Delete(_selectedId.Value);
                _selectedId = null;
                RefreshPage();
                MessageBox.Show("Medicine removed.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Remove Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowMedicineDialog(Medicine existing)
        {
            var isEdit = existing != null;
            using (var dlg = new Form
            {
                Text = isEdit ? "Edit Medicine" : "Add Medicine",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent,
                ClientSize = new Size(480, 520),
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                var txtName = new TextBox { Left = 16, Top = 40, Width = 440 };
                var cmbCat = new ComboBox { Left = 16, Top = 96, Width = 210, DropDownStyle = ComboBoxStyle.DropDown };
                var txtDosage = new TextBox { Left = 246, Top = 96, Width = 210 };
                var txtPrice = new TextBox { Left = 16, Top = 152, Width = 140 };
                var txtStock = new TextBox { Left = 170, Top = 152, Width = 100, MaxLength = 6 };
                var dtpExpiry = new DateTimePicker { Left = 286, Top = 152, Width = 170, Format = DateTimePickerFormat.Short };
                var txtSupplier = new TextBox { Left = 16, Top = 208, Width = 440 };
                var txtDiscount = new TextBox { Left = 16, Top = 264, Width = 100, Text = "0" };
                var chkRx = new CheckBox { Text = "Requires Prescription (Rx)", Left = 130, Top = 264, AutoSize = true, BackColor = UiTheme.AdminSurface };
                var chkPromo = new CheckBox { Text = "On Promotion", Left = 16, Top = 300, AutoSize = true, BackColor = UiTheme.AdminSurface };
                var dtpPromoStart = new DateTimePicker { Left = 16, Top = 356, Width = 210, Format = DateTimePickerFormat.Short, Enabled = false };
                var dtpPromoEnd = new DateTimePicker { Left = 246, Top = 356, Width = 210, Format = DateTimePickerFormat.Short, Enabled = false };

                foreach (var tb in new[] { txtName, txtDosage, txtPrice, txtStock, txtSupplier, txtDiscount })
                    UiTheme.StyleTextBox(tb);
                UiTheme.StyleComboBox(cmbCat);
                cmbCat.Items.AddRange(DefaultCategories);

                chkPromo.CheckedChanged += (s, e) =>
                {
                    var on = chkPromo.Checked;
                    dtpPromoStart.Enabled = on;
                    dtpPromoEnd.Enabled = on;
                    if (on && !isEdit)
                    {
                        dtpPromoStart.Value = DateTime.Today;
                        dtpPromoEnd.Value = DateTime.Today.AddDays(30);
                    }
                };

                if (isEdit)
                {
                    txtName.Text = existing.MedicineName;
                    cmbCat.Text = existing.Category;
                    txtDosage.Text = existing.Dosage;
                    txtPrice.Text = existing.Price.ToString("F2");
                    txtStock.Text = existing.StockQuantity.ToString();
                    dtpExpiry.Value = existing.ExpiryDate;
                    txtSupplier.Text = existing.Supplier;
                    txtDiscount.Text = existing.DiscountPercent.ToString("N0");
                    chkRx.Checked = existing.RequiresPrescription;
                    chkPromo.Checked = existing.IsOnPromotion;
                    if (existing.PromotionStartDate.HasValue) dtpPromoStart.Value = existing.PromotionStartDate.Value;
                    if (existing.PromotionEndDate.HasValue) dtpPromoEnd.Value = existing.PromotionEndDate.Value;
                    dtpPromoStart.Enabled = existing.IsOnPromotion;
                    dtpPromoEnd.Enabled = existing.IsOnPromotion;
                }
                else
                {
                    cmbCat.SelectedIndex = 0;
                    dtpExpiry.Value = DateTime.Today.AddMonths(6);
                }

                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Medicine Name"), 16, 24));
                dlg.Controls.Add(txtName);
                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Category"), 16, 80));
                dlg.Controls.Add(cmbCat);
                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Dosage / Form"), 246, 80));
                dlg.Controls.Add(txtDosage);
                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Unit Price (LKR)"), 16, 136));
                dlg.Controls.Add(txtPrice);
                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Stock"), 170, 136));
                dlg.Controls.Add(txtStock);
                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Expiry Date"), 286, 136));
                dlg.Controls.Add(dtpExpiry);
                dlg.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Supplier"), 16, 192));
                dlg.Controls.Add(txtSupplier);
                dlg.Controls.Add(MakeFieldLabel("Discount %", 16, 248));
                dlg.Controls.Add(txtDiscount);
                dlg.Controls.Add(chkRx);
                dlg.Controls.Add(chkPromo);
                dlg.Controls.Add(MakeFieldLabel("Promotion Start", 16, 340));
                dlg.Controls.Add(dtpPromoStart);
                dlg.Controls.Add(MakeFieldLabel("Promotion End", 246, 340));
                dlg.Controls.Add(dtpPromoEnd);

                var btnSave = AdminUiHelpers.CreateWinButton(isEdit ? "Update" : "Add", true, 88);
                btnSave.Left = 284;
                btnSave.Top = 480;
                btnSave.DialogResult = DialogResult.OK;
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 88);
                btnCancel.Left = 378;
                btnCancel.Top = 480;
                btnCancel.DialogResult = DialogResult.Cancel;
                dlg.Controls.Add(btnSave);
                dlg.Controls.Add(btnCancel);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                if (dlg.ShowDialog(FindForm()) != DialogResult.OK)
                    return;

                try
                {
                    var medicine = ReadDialogFields(existing?.MedicineID ?? 0, txtName, cmbCat, txtDosage,
                        txtPrice, txtStock, dtpExpiry, txtSupplier, txtDiscount, chkRx, chkPromo,
                        dtpPromoStart, dtpPromoEnd);

                    if (isEdit)
                        _medicines.Update(medicine);
                    else
                        _medicines.Add(medicine);

                    RefreshPage();
                    MessageBox.Show(isEdit ? "Medicine updated." : "Medicine added.", "SmartMed",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private static Medicine ReadDialogFields(
            int medicineId, TextBox txtName, ComboBox cmbCat, TextBox txtDosage, TextBox txtPrice,
            TextBox txtStock, DateTimePicker dtpExpiry, TextBox txtSupplier, TextBox txtDiscount,
            CheckBox chkRx, CheckBox chkPromo, DateTimePicker dtpPromoStart, DateTimePicker dtpPromoEnd)
        {
            if (!int.TryParse(txtStock.Text.Trim(), out var stock))
                throw new ArgumentException("Stock quantity must be a valid number.");
            if (!decimal.TryParse(txtPrice.Text.Trim(), out var price))
                throw new ArgumentException("Price must be a valid number.");
            if (!decimal.TryParse(txtDiscount.Text.Trim(), out var discount))
                throw new ArgumentException("Discount must be a valid number.");

            return new Medicine
            {
                MedicineID = medicineId,
                MedicineName = txtName.Text.Trim(),
                Category = cmbCat.Text.Trim(),
                Dosage = txtDosage.Text.Trim(),
                Price = price,
                StockQuantity = stock,
                Supplier = txtSupplier.Text.Trim(),
                ExpiryDate = dtpExpiry.Value.Date,
                RequiresPrescription = chkRx.Checked,
                DiscountPercent = discount,
                IsOnPromotion = chkPromo.Checked,
                PromotionStartDate = chkPromo.Checked ? (DateTime?)dtpPromoStart.Value.Date : null,
                PromotionEndDate = chkPromo.Checked ? (DateTime?)dtpPromoEnd.Value.Date : null
            };
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
    }
}
