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
            "Antibiotic", "Analgesic", "Antidiabetic", "Hypertension", "Antiviral", "Vitamin",
            "Wellness", "Other"
        };

        private readonly MedicineService _medicines;
        private MedicineService _medicineRules;
        private readonly bool _servicesReady;
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
            // Do not BindGrid here: an empty BindingList clears Designer columns in the VS host.
            if (gridMedicines != null)
            {
                gridMedicines.DataSource = null;
                gridMedicines.AutoGenerateColumns = false;
                gridMedicines.ColumnHeadersVisible = true;
                EnsureGridActionColumns();
                gridMedicines.BringToFront();
            }

            panelGridBody?.BringToFront();
            lblTotalItems.Text = "-";
            lblLowStock.Text = "-";
            lblExpiringSoon.Text = "-";
            _expiryAlertLines = new List<string>();
            if (btnViewExpiryAlerts != null)
                btnViewExpiryAlerts.Text = "Alerts (0)";
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
            cmbCategory.FlatStyle = FlatStyle.Standard;
            UiTheme.ApplyFlatButton(btnSearch, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnClear, UiButtonStyle.Secondary);

            WirePanelBorder(panelGridOuter);
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
            if (panel is null || panel.Tag is "dash-border") return;
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
            if (card is null || card.Tag is "dash-stat") return;
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

            UiTheme.WireClinicalPlaceholderTextBox(txtSearch, "Search");
            UiTheme.WireClinicalPlaceholderTextBox(txtMinPrice, "Min");
            UiTheme.WireClinicalPlaceholderTextBox(txtMaxPrice, "Max");
        }

        private void BtnAdd_Click(object sender, EventArgs e) => ShowMedicineDialog(null);

        private void BtnSearch_Click(object sender, EventArgs e) => ApplyFilters();

        private void TxtSearch_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void CmbCategory_SelectedIndexChanged(object sender, EventArgs e) => ApplyFilters();

        private void TxtMinPrice_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void TxtMaxPrice_TextChanged(object sender, EventArgs e) => ApplyFilters();

        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearMedicineFilters();
        }

        private void ClearMedicineFilters()
        {
            ClearFilterInput(txtSearch, "Search");
            ClearFilterInput(txtMinPrice, "Min");
            ClearFilterInput(txtMaxPrice, "Max");

            if (cmbCategory != null && cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;

            _selectedId = null;
            ApplyFilters();
        }

        private static void ClearFilterInput(TextBox textBox, string placeholder)
        {
            if (textBox == null) return;

            textBox.ForeColor = UiTheme.PlaceholderText;
            textBox.Text = placeholder;
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyFilters();
            }
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
            decimal? minPrice = ValidationService.IsNonNegativeDecimal(UiTheme.ReadTextBoxValue(txtMinPrice), out var min)
                ? min : (decimal?)null;
            decimal? maxPrice = ValidationService.IsNonNegativeDecimal(UiTheme.ReadTextBoxValue(txtMaxPrice), out var max)
                ? max : (decimal?)null;
            var category = cmbCategory?.SelectedIndex > 0 ? cmbCategory.SelectedItem?.ToString() : null;
            if (_servicesReady)
                return _medicines.Search(UiTheme.ReadTextBoxValue(txtSearch), category, minPrice, maxPrice);
            return SearchService.Search(_allMedicines, UiTheme.ReadTextBoxValue(txtSearch), category, minPrice, maxPrice);
        }

        private void ApplyFilters()
        {
            if (gridMedicines == null) return;
            var filtered = GetFilteredMedicines();
            BindGrid(filtered);
            UpdateStats(_allMedicines);
            UpdateExpiryAlerts();
        }

        private void BindGrid(List<Medicine> items)
        {
            var keepId = _selectedId;
            var rowList = (items ?? new List<Medicine>())
                .Select(ToGridRow)
                .ToList();

            UiTheme.SetGridDataSource(
                gridMedicines,
                new System.ComponentModel.BindingList<MedicineGridRow>(rowList));
            if (gridMedicines.Columns.Contains("colMedicineID"))
                gridMedicines.Columns["colMedicineID"].Visible = false;
            // col* names are skipped by BeautifyGridHeaders (same as Customers).
            UiTheme.BeautifyGridHeaders(gridMedicines);
            EnsureGridActionColumns();

            if (keepId.HasValue)
                SelectGridRowById(keepId.Value);
        }

        private MedicineGridRow ToGridRow(Medicine m) =>
            new MedicineGridRow
            {
                MedicineID = m.MedicineID,
                ID = $"#M-{m.MedicineID:D4}",
                Name = m.MedicineName,
                Category = m.Category,
                Dosage = m.Dosage,
                Stock = m.StockQuantity,
                Price = Rules.GetEffectivePrice(m).ToString("N2"),
                Expiry = m.ExpiryDate.ToString("yyyy-MM-dd"),
                Rx = m.RequiresPrescription ? "Rx" : "—",
                Discount = m.DiscountPercent > 0 ? $"{m.DiscountPercent:N0}%" : "—",
                StartDate = FormatPromoDate(m.PromotionStartDate),
                EndDate = FormatPromoDate(m.PromotionEndDate),
                Promo = FormatPromotionStatus(m),
                Status = GetStatusLabel(m),
                CatalogAction = m.IsActive ? "Deactivate" : "Activate"
            };

        public sealed class MedicineGridRow
        {
            public int MedicineID { get; set; }
            public string ID { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Dosage { get; set; }
            public int Stock { get; set; }
            public string Price { get; set; }
            public string Expiry { get; set; }
            public string Rx { get; set; }
            public string Discount { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Promo { get; set; }
            public string Status { get; set; }
            public string CatalogAction { get; set; }
        }

        private void EnsureGridActionColumns()
        {
            AddOrConfigureButtonColumn("Edit", "Edit", 68);
            AddOrConfigureButtonColumn("CatalogAction", "Catalog", 112, "CatalogAction");
            AddOrConfigureButtonColumn("Delete", "Delete", 80);

            if (gridMedicines.Columns.Contains("Deactivate"))
                gridMedicines.Columns.Remove("Deactivate");

            // Data columns stay at the front; action buttons pinned to the end (Customers pattern).
            var actionNames = new HashSet<string>(StringComparer.Ordinal) { "Edit", "CatalogAction", "Delete" };
            var display = 0;
            foreach (DataGridViewColumn column in gridMedicines.Columns)
            {
                if (column == null || !column.Visible || actionNames.Contains(column.Name))
                    continue;
                column.DisplayIndex = display++;
            }

            if (gridMedicines.Columns.Contains("Edit"))
                gridMedicines.Columns["Edit"].DisplayIndex = display++;
            if (gridMedicines.Columns.Contains("CatalogAction"))
                gridMedicines.Columns["CatalogAction"].DisplayIndex = display++;
            if (gridMedicines.Columns.Contains("Delete"))
                gridMedicines.Columns["Delete"].DisplayIndex = display;
        }

        private void AddOrConfigureButtonColumn(string name, string text, int width, string dataPropertyName = null)
        {
            if (gridMedicines.Columns[name] is DataGridViewButtonColumn existing)
            {
                existing.HeaderText = text;
                existing.Width = width;
                existing.MinimumWidth = width;
                existing.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
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

            if (gridMedicines.Columns.Contains(name))
                gridMedicines.Columns.Remove(name);

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

            gridMedicines.Columns.Add(column);
        }

        private string GetStatusLabel(Medicine m)
        {
            if (!m.IsActive) return "Inactive";
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
                if (row.IsNewRow || row.Cells["colMedicineID"]?.Value == null) continue;
                if (Convert.ToInt32(row.Cells["colMedicineID"].Value) != id) continue;
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

        private void UpdateExpiryAlerts()
        {
            if (btnViewExpiryAlerts == null) return;

            _expiryAlertLines = Rules.GetExpiryAlertMessages();
            var count = _expiryAlertLines.Count;
            btnViewExpiryAlerts.Text = $"Alerts ({count})";
            btnViewExpiryAlerts.ForeColor = count > 0
                ? Color.FromArgb(180, 70, 0)
                : Color.FromArgb(53, 103, 94);
        }

        private void BtnViewExpiryAlerts_Click(object sender, EventArgs e) =>
            ExpiryAlertsDialog.Show(FindForm(), _expiryAlertLines);

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
                if (items == null || items.Count == 0)
                {
                    SmartMedMessageBox.Show("No medicines to export.", "Export PDF",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (var dialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = $"SmartMed_Inventory_{DateTime.Now:yyyyMMdd}.pdf"
                })
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    _medicines.ExportToPdf(items, dialog.FileName);
                    SmartMedMessageBox.Show("Inventory exported to PDF.", "Export PDF",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Export Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridMedicines_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || !_servicesReady) return;

            var colName = gridMedicines.Columns[e.ColumnIndex].Name;
            if (colName != "Edit" && colName != "CatalogAction" && colName != "Delete") return;

            var idCell = gridMedicines.Rows[e.RowIndex].Cells["colMedicineID"];
            if (idCell?.Value == null) return;

            var id = Convert.ToInt32(idCell.Value);
            if (colName == "Edit")
            {
                EditMedicine(id);
                return;
            }

            if (colName == "Delete")
            {
                DeleteMedicine(id);
                return;
            }

            var actionCell = gridMedicines.Rows[e.RowIndex].Cells["CatalogAction"];
            var activate = string.Equals(actionCell?.Value?.ToString(), "Activate", StringComparison.OrdinalIgnoreCase);
            ToggleCatalogStatus(id, activate);
        }

        private void EditMedicine(int medicineId)
        {
            var medicine = _medicines?.GetById(medicineId);
            if (medicine == null) return;
            _selectedId = medicineId;
            ShowMedicineDialog(medicine);
        }

        private void ToggleCatalogStatus(int medicineId, bool activate)
        {
            var action = activate ? "activate" : "deactivate";
            var message = activate
                ? "Activate this medicine and show it in the customer catalog again?"
                : "Deactivate this medicine? It will be hidden from customers. Order history is kept.";
            if (SmartMedMessageBox.Show(message,
                    activate ? "Confirm Activate" : "Confirm Deactivate",
                    MessageBoxButtons.YesNo,
                    activate ? MessageBoxIcon.Question : MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _medicines.SetActive(medicineId, activate);
                RefreshPage();
                SmartMedMessageBox.Show(
                    activate ? "Medicine activated." : "Medicine deactivated.",
                    "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Catalog Update Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteMedicine(int medicineId)
        {
            if (SmartMedMessageBox.Show(
                    "Permanently delete this medicine? This cannot be undone. Prefer deactivate if the medicine appears in past orders.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;

            try
            {
                _medicines.Delete(medicineId);
                _selectedId = null;
                RefreshPage();
                SmartMedMessageBox.Show("Medicine deleted.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void GridMedicines_SelectionChanged(object sender, EventArgs e)
        {
            if (gridMedicines.CurrentRow == null || gridMedicines.CurrentRow.IsNewRow)
            {
                _selectedId = null;
                return;
            }
            var cell = gridMedicines.CurrentRow.Cells["colMedicineID"];
            _selectedId = cell?.Value != null ? Convert.ToInt32(cell.Value) : (int?)null;
        }

        private void GridMedicines_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || gridMedicines.Rows[e.RowIndex].IsNewRow) return;
            var idCell = gridMedicines.Rows[e.RowIndex].Cells["colMedicineID"];
            if (idCell?.Value == null) return;
            var item = _allMedicines.FirstOrDefault(m => m.MedicineID == Convert.ToInt32(idCell.Value));
            if (item == null) return;

            var row = gridMedicines.Rows[e.RowIndex];
            if (row.Selected)
            {
                row.DefaultCellStyle.BackColor = UiTheme.GridSelectionBack;
                row.DefaultCellStyle.ForeColor = UiTheme.GridSelectionFore;
                return;
            }

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
            if (e.RowIndex < 0 || gridMedicines.Rows[e.RowIndex].Cells["colMedicineID"]?.Value == null)
                return;

            var row = gridMedicines.Rows[e.RowIndex];
            if (row.Selected)
            {
                UiTheme.ApplySelectedRowCellStyle(e.CellStyle);
                return;
            }

            var id = Convert.ToInt32(gridMedicines.Rows[e.RowIndex].Cells["colMedicineID"].Value);
            var item = _allMedicines.FirstOrDefault(m => m.MedicineID == id);
            if (item == null) return;

            var columnName = gridMedicines.Columns[e.ColumnIndex].Name;
            var expiryStatus = Rules.CheckExpiry(item);

            if (columnName == "colStock")
            {
                e.CellStyle.Font = UiTheme.UiFontBold;
                if (item.StockQuantity <= 20)
                {
                    e.CellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    e.CellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                }
                else if (item.StockQuantity <= 50)
                {
                    e.CellStyle.BackColor = Color.FromArgb(255, 243, 205);
                    e.CellStyle.ForeColor = Color.FromArgb(140, 70, 0);
                }
                else
                {
                    e.CellStyle.BackColor = Color.FromArgb(220, 245, 238);
                    e.CellStyle.ForeColor = Color.FromArgb(27, 79, 71);
                }
                e.Value = $"{item.StockQuantity}";
            }
            else if (columnName == "colStatus")
            {
                var status = e.Value?.ToString() ?? "";
                if (string.Equals(status, "Inactive", StringComparison.OrdinalIgnoreCase))
                {
                    e.CellStyle.BackColor = Color.FromArgb(226, 232, 240);
                    e.CellStyle.ForeColor = Color.FromArgb(71, 85, 105);
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (string.Equals(status, "Expired", StringComparison.OrdinalIgnoreCase))
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
            else if (columnName == "colRx")
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
            else if (columnName == "colExpiry" &&
                     (expiryStatus == MedicineService.ExpiryExpired || expiryStatus == MedicineService.ExpiryExpiringSoon))
            {
                e.CellStyle.ForeColor = expiryStatus == MedicineService.ExpiryExpired ? Color.DarkRed : Color.DarkOrange;
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
            else if (columnName == "Edit")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.ForeColor = UiTheme.AdminTeal;
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
            else if (columnName == "CatalogAction")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                var action = e.Value?.ToString() ?? string.Empty;
                e.CellStyle.ForeColor = string.Equals(action, "Deactivate", StringComparison.OrdinalIgnoreCase)
                    ? UiTheme.Danger
                    : UiTheme.AdminTeal;
                e.CellStyle.Font = UiTheme.UiFontBold;
            }
            else if (columnName == "Delete")
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                e.CellStyle.ForeColor = UiTheme.Danger;
                e.CellStyle.Font = UiTheme.UiFontBold;
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
                ClientSize = new Size(520, 680),
                MaximizeBox = false,
                MinimizeBox = false,
                Font = UiTheme.UiFont,
                BackColor = UiTheme.AdminSurface
            })
            {
                const int fieldWidth = 472;

                var panelFooter = new Panel
                {
                    Dock = DockStyle.Bottom,
                    Height = 56,
                    BackColor = UiTheme.AdminSurface
                };
                var panelBody = new Panel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    BackColor = UiTheme.AdminSurface,
                    Padding = new Padding(0, 0, 8, 8)
                };

                var txtName = new TextBox { Left = 16, Top = 40, Width = fieldWidth };
                var cmbCat = new ComboBox { Left = 16, Top = 96, Width = 220, DropDownStyle = ComboBoxStyle.DropDown };
                var txtDosage = new TextBox { Left = 252, Top = 96, Width = 236 };
                var txtPrice = new TextBox { Left = 16, Top = 152, Width = 140 };
                var txtStock = new TextBox { Left = 170, Top = 152, Width = 100, MaxLength = 6 };
                var dtpExpiry = new DateTimePicker { Left = 286, Top = 152, Width = 202, Format = DateTimePickerFormat.Short };
                var txtSupplier = new TextBox { Left = 16, Top = 208, Width = fieldWidth };
                var txtDiscount = new TextBox { Left = 16, Top = 264, Width = 100 };
                var chkRx = new CheckBox { Text = "Requires Prescription (Rx)", Left = 130, Top = 264, AutoSize = true, BackColor = UiTheme.AdminSurface };
                var chkPromo = new CheckBox { Text = "On Promotion", Left = 16, Top = 300, AutoSize = true, BackColor = UiTheme.AdminSurface };
                var dtpPromoStart = new DateTimePicker { Left = 16, Top = 356, Width = 220, Format = DateTimePickerFormat.Short, Enabled = false };
                var dtpPromoEnd = new DateTimePicker { Left = 252, Top = 356, Width = 236, Format = DateTimePickerFormat.Short, Enabled = false };

                var txtDescription = new TextBox
                {
                    Left = 16, Top = 428, Width = fieldWidth, Height = 48,
                    Multiline = true, ScrollBars = ScrollBars.Vertical
                };
                var txtActiveIngredient = new TextBox { Left = 16, Top = 508, Width = fieldWidth };
                var txtUsageInstructions = new TextBox
                {
                    Left = 16, Top = 564, Width = fieldWidth, Height = 48,
                    Multiline = true, ScrollBars = ScrollBars.Vertical
                };
                var txtWarnings = new TextBox
                {
                    Left = 16, Top = 644, Width = fieldWidth, Height = 48,
                    Multiline = true, ScrollBars = ScrollBars.Vertical
                };
                var txtSideEffects = new TextBox { Left = 16, Top = 724, Width = fieldWidth };
                var txtPackSize = new TextBox { Left = 16, Top = 780, Width = fieldWidth };

                foreach (var tb in new[]
                {
                    txtName, txtDosage, txtPrice, txtStock, txtSupplier, txtDiscount,
                    txtDescription, txtActiveIngredient, txtUsageInstructions,
                    txtWarnings, txtSideEffects, txtPackSize
                })
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
                    txtDiscount.Text = existing.DiscountPercent > 0
                        ? existing.DiscountPercent.ToString("N0")
                        : string.Empty;
                    chkRx.Checked = existing.RequiresPrescription;
                    chkPromo.Checked = existing.IsOnPromotion;
                    if (existing.PromotionStartDate.HasValue) dtpPromoStart.Value = existing.PromotionStartDate.Value;
                    if (existing.PromotionEndDate.HasValue) dtpPromoEnd.Value = existing.PromotionEndDate.Value;
                    dtpPromoStart.Enabled = existing.IsOnPromotion;
                    dtpPromoEnd.Enabled = existing.IsOnPromotion;
                    txtDescription.Text = existing.Description ?? string.Empty;
                    txtActiveIngredient.Text = existing.ActiveIngredient ?? string.Empty;
                    txtUsageInstructions.Text = existing.UsageInstructions ?? string.Empty;
                    txtWarnings.Text = existing.Warnings ?? string.Empty;
                    txtSideEffects.Text = existing.SideEffects ?? string.Empty;
                    txtPackSize.Text = existing.PackSize ?? string.Empty;
                }
                else
                {
                    cmbCat.SelectedIndex = 0;
                    dtpExpiry.Value = DateTime.Today.AddMonths(6);
                }

                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Medicine Name"), 16, 24));
                panelBody.Controls.Add(txtName);
                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Category"), 16, 80));
                panelBody.Controls.Add(cmbCat);
                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Dosage / Form"), 252, 80));
                panelBody.Controls.Add(txtDosage);
                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Unit Price (LKR)"), 16, 136));
                panelBody.Controls.Add(txtPrice);
                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Stock"), 170, 136));
                panelBody.Controls.Add(txtStock);
                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Expiry Date"), 286, 136));
                panelBody.Controls.Add(dtpExpiry);
                panelBody.Controls.Add(MakeFieldLabel(ValidationService.RequiredLabel("Supplier"), 16, 192));
                panelBody.Controls.Add(txtSupplier);
                panelBody.Controls.Add(MakeFieldLabel("Discount % (optional)", 16, 248));
                panelBody.Controls.Add(txtDiscount);
                panelBody.Controls.Add(chkRx);
                panelBody.Controls.Add(chkPromo);
                panelBody.Controls.Add(MakeFieldLabel("Promotion Start", 16, 340));
                panelBody.Controls.Add(dtpPromoStart);
                panelBody.Controls.Add(MakeFieldLabel("Promotion End", 252, 340));
                panelBody.Controls.Add(dtpPromoEnd);

                panelBody.Controls.Add(MakeFieldLabel("Description (What it's for)", 16, 412));
                panelBody.Controls.Add(txtDescription);
                panelBody.Controls.Add(MakeFieldLabel("Active Ingredient", 16, 492));
                panelBody.Controls.Add(txtActiveIngredient);
                panelBody.Controls.Add(MakeFieldLabel("Usage Instructions (How to take)", 16, 548));
                panelBody.Controls.Add(txtUsageInstructions);
                panelBody.Controls.Add(MakeFieldLabel("Warnings", 16, 628));
                panelBody.Controls.Add(txtWarnings);
                panelBody.Controls.Add(MakeFieldLabel("Side Effects", 16, 708));
                panelBody.Controls.Add(txtSideEffects);
                panelBody.Controls.Add(MakeFieldLabel("Pack Size", 16, 764));
                panelBody.Controls.Add(txtPackSize);

                var btnSave = AdminUiHelpers.CreateWinButton(isEdit ? "Update" : "Add", true, 88);
                btnSave.Left = 316;
                btnSave.Top = 12;
                var btnCancel = AdminUiHelpers.CreateWinButton("Cancel", false, 88);
                btnCancel.Left = 410;
                btnCancel.Top = 12;
                btnCancel.DialogResult = DialogResult.Cancel;
                panelFooter.Controls.Add(btnSave);
                panelFooter.Controls.Add(btnCancel);

                dlg.Controls.Add(panelBody);
                dlg.Controls.Add(panelFooter);
                dlg.AcceptButton = btnSave;
                dlg.CancelButton = btnCancel;

                UiTheme.EnableFieldNavigation(btnSave,
                    txtName, cmbCat, txtDosage, txtPrice, txtStock, dtpExpiry,
                    txtSupplier, txtDiscount, chkRx, chkPromo, dtpPromoStart, dtpPromoEnd,
                    txtDescription, txtActiveIngredient, txtUsageInstructions,
                    txtWarnings, txtSideEffects, txtPackSize);

                btnSave.Click += (s, e) =>
                {
                    try
                    {
                        var medicine = ReadDialogFields(existing?.MedicineID ?? 0, txtName, cmbCat, txtDosage,
                            txtPrice, txtStock, dtpExpiry, txtSupplier, txtDiscount, chkRx, chkPromo,
                            dtpPromoStart, dtpPromoEnd, txtDescription, txtActiveIngredient,
                            txtUsageInstructions, txtWarnings, txtSideEffects, txtPackSize);

                        if (isEdit)
                            _medicines.Update(medicine);
                        else
                            _medicines.Add(medicine);

                        RefreshPage();
                        dlg.DialogResult = DialogResult.OK;
                        dlg.Close();
                        SmartMedMessageBox.Show(isEdit ? "Medicine updated." : "Medicine added.", "SmartMed",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (ArgumentException ex)
                    {
                        SmartMedMessageBox.Show(ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (InvalidOperationException ex)
                    {
                        SmartMedMessageBox.Show(ex.Message, "Save Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        SmartMedMessageBox.Show("Unable to save this medicine.\n" + ex.Message, "Save Failed",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };

                dlg.ShowDialog(FindForm());
            }
        }

        private static Medicine ReadDialogFields(
            int medicineId, TextBox txtName, ComboBox cmbCat, TextBox txtDosage, TextBox txtPrice,
            TextBox txtStock, DateTimePicker dtpExpiry, TextBox txtSupplier, TextBox txtDiscount,
            CheckBox chkRx, CheckBox chkPromo, DateTimePicker dtpPromoStart, DateTimePicker dtpPromoEnd,
            TextBox txtDescription, TextBox txtActiveIngredient, TextBox txtUsageInstructions,
            TextBox txtWarnings, TextBox txtSideEffects, TextBox txtPackSize)
        {
            // Use shared numeric validation so dialogs match ValidationService rules.
            if (!ValidationService.IsNonNegativeInt(txtStock.Text.Trim(), out var stock))
                throw new ArgumentException("Stock quantity must be a valid number that is 0 or greater.");
            if (!ValidationService.IsNonNegativeDecimal(txtPrice.Text.Trim(), out var price))
                throw new ArgumentException("Price must be a valid number that is 0 or greater.");

            var discountText = txtDiscount.Text.Trim();
            var discount = 0m;
            if (!string.IsNullOrEmpty(discountText)
                && !ValidationService.IsNonNegativeDecimal(discountText, out discount))
                throw new ArgumentException("Discount must be a valid number that is 0 or greater.");

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
                PromotionEndDate = chkPromo.Checked ? (DateTime?)dtpPromoEnd.Value.Date : null,
                Description = OptionalText(txtDescription),
                ActiveIngredient = OptionalText(txtActiveIngredient),
                UsageInstructions = OptionalText(txtUsageInstructions),
                Warnings = OptionalText(txtWarnings),
                SideEffects = OptionalText(txtSideEffects),
                PackSize = OptionalText(txtPackSize)
            };
        }

        private static string OptionalText(TextBox textBox) =>
            string.IsNullOrWhiteSpace(textBox?.Text) ? null : textBox.Text.Trim();

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
