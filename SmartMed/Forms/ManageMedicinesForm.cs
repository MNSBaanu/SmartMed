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
    public partial class ManageMedicinesForm : AdminShellForm
    {
        private bool _pageBuilt;
        public ManageMedicinesForm()
            : base(AdminNavItem.Medicines, "Manage Medicines")
        {
            InitializeComponent();
        }

        internal ManageMedicinesForm(bool embedded)
            : base(AdminNavItem.Medicines, "Manage Medicines", embedded)
        {
        }
        protected override void InitializePageContent()
        {
            if (_pageBuilt) return;
            _pageBuilt = true;
            BuildContent();
            if (IsDesignHost())
                LoadDesignTimePreview();
            else
                LoadMedicines();
        }
        private static readonly string[] DefaultCategories =
        {
            "Antibiotic", "Analgesic", "Antidiabetic", "Hypertension", "Antiviral", "Vitamin", "Other"
        };
        private MedicineService _medicineService;
        private MedicineService Medicines => GetRuntimeService(ref _medicineService);
        private List<Medicine> _allMedicines = new List<Medicine>();
        private int? _selectedId;
        private DataGridView gridMedicines;
        private TextBox txtName;
        private TextBox txtDosage;
        private TextBox txtStock;
        private TextBox txtDiscount;
        private DateTimePicker dtpExpiry;
        private ComboBox cmbCategory;
        private TextBox txtPrice;
        private TextBox txtSupplier;
        private CheckBox chkPrescription;
        private CheckBox chkPromotion;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Label lblTotalItems;
        private Label lblLowStock;
        private Label lblCompliance;
        private Label lblExpirySummary;
        private Button btnViewExpiryAlerts;
        private Panel panelExpiryAlerts;
        private List<string> _expiryAlertLines = new List<string>();
        private TextBox txtSearch;
        private ComboBox cmbSearchCategory;
        private TextBox txtMinPrice;
        private TextBox txtMaxPrice;
        private TableLayoutPanel _scrollRoot;
        private void BuildContent()
        {
            PagePanel.Controls.Clear();
            _scrollRoot = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Dock = DockStyle.Top,
                ColumnCount = 1,
                RowCount = 6,
                MinimumSize = new Size(0, 1000),
                Width = GetScrollContentWidth()
            };
            _scrollRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // header
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // stats
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // search
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // expiry alerts
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f)); // grid
            _scrollRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));      // form
            _scrollRoot.Controls.Add(CreatePageHeader(), 0, 0);
            _scrollRoot.Controls.Add(CreateStatsRow(), 0, 1);
            _scrollRoot.Controls.Add(CreateSearchPanel(), 0, 2);
            _scrollRoot.Controls.Add(CreateExpiryAlertPanel(), 0, 3);
            _scrollRoot.Controls.Add(CreateGridPanel(), 0, 4);
            _scrollRoot.Controls.Add(CreateFormPanel(), 0, 5);
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
                Text = "Update and monitor pharmaceutical inventory levels.",
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
        private Panel CreateSearchPanel()
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                Margin = new Padding(0, 0, 0, 12)
            };
            txtSearch = new TextBox { Width = 220 };
            cmbSearchCategory = new ComboBox { Width = 140, DropDownStyle = ComboBoxStyle.DropDownList };
            txtMinPrice = new TextBox { Width = 80 };
            txtMaxPrice = new TextBox { Width = 80 };
            var btnClearSearch = new Button { Text = "Clear", Width = 70, Height = 28 };
            cmbSearchCategory.Items.Add("All categories");
            cmbSearchCategory.SelectedIndex = 0;
            var flow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            flow.Controls.Add(new Label { Text = "Search:", AutoSize = true, Padding = new Padding(0, 6, 4, 0) });
            flow.Controls.Add(txtSearch);
            flow.Controls.Add(new Label { Text = "Category:", AutoSize = true, Padding = new Padding(8, 6, 4, 0) });
            flow.Controls.Add(cmbSearchCategory);
            flow.Controls.Add(new Label { Text = "Price:", AutoSize = true, Padding = new Padding(8, 6, 4, 0) });
            flow.Controls.Add(txtMinPrice);
            flow.Controls.Add(txtMaxPrice);
            flow.Controls.Add(btnClearSearch);
            txtSearch.TextChanged += (s, e) => ApplySearchFilter();
            cmbSearchCategory.SelectedIndexChanged += (s, e) => ApplySearchFilter();
            txtMinPrice.TextChanged += (s, e) => ApplySearchFilter();
            txtMaxPrice.TextChanged += (s, e) => ApplySearchFilter();
            btnClearSearch.Click += (s, e) =>
            {
                txtSearch.Clear();
                cmbSearchCategory.SelectedIndex = 0;
                txtMinPrice.Clear();
                txtMaxPrice.Clear();
                ApplySearchFilter();
            };
            panel.Controls.Add(flow);
            return panel;
        }
        private Panel CreateExpiryAlertPanel()
        {
            panelExpiryAlerts = new Panel
            {
                Dock = DockStyle.Top,
                Height = 40,
                BackColor = Color.FromArgb(255, 248, 240),
                Padding = new Padding(12, 8, 12, 8),
                Margin = new Padding(0, 0, 0, 12)
            };
            panelExpiryAlerts.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(200, 120, 0)))
                    e.Graphics.DrawRectangle(pen, 0, 0, panelExpiryAlerts.Width - 1, panelExpiryAlerts.Height - 1);
            };
            btnViewExpiryAlerts = new Button
            {
                Text = "View All Alerts",
                Width = 130,
                Height = 26,
                Dock = DockStyle.Right,
                Visible = false
            };
            btnViewExpiryAlerts.Click += BtnViewExpiryAlerts_Click;
            lblExpirySummary = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.FromArgb(140, 70, 0),
                Text = "Expiry alerts will appear after loading medicines.",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };
            panelExpiryAlerts.Controls.Add(lblExpirySummary);
            panelExpiryAlerts.Controls.Add(btnViewExpiryAlerts);
            return panelExpiryAlerts;
        }
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
                FormBorderStyle = FormBorderStyle.FixedDialog
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
                var btnClose = new Button
                {
                    Text = "Close",
                    Dock = DockStyle.Bottom,
                    Height = 36,
                    DialogResult = DialogResult.OK
                };
                dlg.Controls.Add(btnClose);
                dlg.Controls.Add(list);
                dlg.AcceptButton = btnClose;
                dlg.ShowDialog(FindForm());
            }
        }
        private static Button CreateToolbarButton(string text)
        {
            return new Button
            {
                Text = text,
                Height = 32,
                Width = 100,
                Margin = new Padding(4, 0, 0, 0)
            };
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
            gridMedicines = new DataGridView
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
                ScrollBars = ScrollBars.Vertical,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(232, 232, 232),
                    ForeColor = Color.FromArgb(0, 31, 102),
                    Font = UiTheme.UiFontBold
                },
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    SelectionBackColor = Color.FromArgb(180, 203, 249),
                    SelectionForeColor = SystemColors.ControlText
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.FromArgb(250, 250, 250)
                }
            };
            gridMedicines.SelectionChanged += GridMedicines_SelectionChanged;
            gridMedicines.CellFormatting += GridMedicines_CellFormatting;
            gridMedicines.RowPrePaint += GridMedicines_RowPrePaint;
            UiTheme.ApplyGrid(gridMedicines);
            outer.Controls.Add(gridMedicines);
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
                Height = 280
            };
            columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            columns.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            txtName = new TextBox();
            txtDosage = new TextBox();
            txtStock = new TextBox { MaxLength = 6 };
            txtDiscount = new TextBox { Text = "0" };
            dtpExpiry = new DateTimePicker { Format = DateTimePickerFormat.Short, Height = 23 };
            cmbCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown };
            txtPrice = new TextBox();
            txtSupplier = new TextBox();
            chkPrescription = new CheckBox { Text = "Requires Prescription (Rx)", AutoSize = true };
            chkPromotion = new CheckBox { Text = "On Promotion", AutoSize = true };
            columns.Controls.Add(CreateFieldColumn(
                CreateField("Medicine Name", txtName, required: true),
                CreateField("Dosage / Form", txtDosage, required: true),
                CreateField("Current Stock", txtStock, required: true),
                CreateField("Expiry Date", dtpExpiry, required: true)), 0, 0);
            var promoPanel = new Panel { Height = 23 + 24, Dock = DockStyle.Top };
            chkPrescription.Font = UiTheme.UiFont;
            chkPrescription.ForeColor = SystemColors.Highlight;
            chkPrescription.Location = new Point(0, 28);
            promoPanel.Controls.Add(chkPrescription);
            promoPanel.Controls.Add(new Label { Text = "", Height = 20, Dock = DockStyle.Top });
            var discountPanel = CreateField("Discount %", txtDiscount);
            var promotionPanel = new Panel { Height = 23 + 24, Dock = DockStyle.Top };
            chkPromotion.Location = new Point(0, 28);
            chkPromotion.Font = UiTheme.UiFont;
            promotionPanel.Controls.Add(chkPromotion);
            promotionPanel.Controls.Add(new Label { Text = "Promotion", Dock = DockStyle.Top, Height = 20 });
            columns.Controls.Add(CreateFieldColumn(
                CreateField("Category", cmbCategory, required: true),
                CreateField("Unit Price (LKR)", txtPrice, required: true),
                CreateField("Supplier", txtSupplier, required: true),
                discountPanel,
                promotionPanel,
                promoPanel), 1, 0);
            btnClear = UiTheme.CreateFlatButton("Clear Form", UiButtonStyle.Secondary, 110, 40);
            btnDelete = UiTheme.CreateFlatButton("Delete Entry", UiButtonStyle.Danger, 110, 40);
            btnUpdate = UiTheme.CreateFlatButton("Update Record", UiButtonStyle.Primary, 120, 40);
            btnAdd = UiTheme.CreateFlatButton("Add New Medicine", UiButtonStyle.Success, 150, 40);
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
            SyncActionButtons();
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
            if (input is TextBox tb)
            {
                tb.BorderStyle = BorderStyle.FixedSingle;
                UiTheme.StyleTextBox(tb);
            }
            if (input is ComboBox cb)
            {
                cb.DropDownStyle = ComboBoxStyle.DropDown;
                UiTheme.StyleComboBox(cb);
            }
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
            lblTotalItems = new Label();
            lblLowStock = new Label();
            lblCompliance = new Label();
            row.Controls.Add(CreateStatTile("\uD83D\uDCE6", "Total Items", lblTotalItems, SystemColors.Highlight), 0, 0);
            row.Controls.Add(CreateStatTile("\u26A0", "Low Stock Alert", lblLowStock, Color.Red), 1, 0);
            row.Controls.Add(CreateStatTile("\u2713", "Compliance", lblCompliance, Color.FromArgb(72, 95, 135)), 2, 0);
            wrap.Controls.Add(row);
            return wrap;
        }
        private Panel CreateStatTile(string icon, string title, Label valueLabel, Color accent)
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
            var iconLabel = new Label
            {
                Text = icon,
                Font = UiTheme.UiFont,
                Location = new Point(16, 16),
                AutoSize = true
            };
            valueLabel.Text = "0";
            valueLabel.Font = UiTheme.UiFontBold;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(52, 34);
            valueLabel.AutoSize = true;
            card.Controls.Add(new Label
            {
                Text = title.ToUpperInvariant(),
                Font = UiTheme.UiFont,
                ForeColor = SystemColors.GrayText,
                Location = new Point(52, 16),
                AutoSize = true
            });
            card.Controls.Add(valueLabel);
            card.Controls.Add(iconLabel);
            return card;
        }
        private void LoadDesignTimePreview()
        {
            _allMedicines = CreateDesignTimeMedicines();
            BindGrid(_allMedicines);
            foreach (var cat in DefaultCategories)
                cmbSearchCategory.Items.Add(cat);
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(DefaultCategories);
            cmbCategory.SelectedIndex = 0;
            txtName.Text = "Amoxicillin 500mg";
            txtDosage.Text = "500mg Caps";
            txtStock.Text = "120";
            txtPrice.Text = "14.50";
            txtSupplier.Text = "MediDist Ltd";
            txtDiscount.Text = "0";
            dtpExpiry.Value = new DateTime(2025, 12, 1);
            chkPrescription.Checked = true;
            chkPromotion.Checked = false;
            UpdateStats(_allMedicines);
            UpdateExpiryAlerts(_allMedicines);
        }
        private static List<Medicine> CreateDesignTimeMedicines()
        {
            return new List<Medicine>
            {
                new Medicine
                {
                    MedicineID = 1,
                    MedicineName = "Amoxicillin 500mg",
                    Category = "Antibiotic",
                    Dosage = "500mg Caps",
                    Price = 14.50m,
                    StockQuantity = 120,
                    Supplier = "MediDist Ltd",
                    ExpiryDate = new DateTime(2025, 12, 1),
                    RequiresPrescription = true
                },
                new Medicine
                {
                    MedicineID = 2,
                    MedicineName = "Lisinopril 10mg",
                    Category = "Hypertension",
                    Dosage = "10mg Tablets",
                    Price = 8.20m,
                    StockQuantity = 45,
                    Supplier = "Global Pharma",
                    ExpiryDate = new DateTime(2024, 8, 15),
                    RequiresPrescription = true,
                    DiscountPercent = 10,
                    IsOnPromotion = true
                },
                new Medicine
                {
                    MedicineID = 3,
                    MedicineName = "Ibuprofen 200mg",
                    Category = "Analgesic",
                    Dosage = "200mg Softgels",
                    Price = 5.99m,
                    StockQuantity = 12,
                    Supplier = "OTC Supply Co",
                    ExpiryDate = new DateTime(2026, 1, 30),
                    RequiresPrescription = false
                }
            };
        }
        public void LoadMedicines()
        {
            if (IsDesignHost() || Medicines == null) return;
            _allMedicines = Medicines.GetAll();
            RefreshSearchCategories();
            ApplySearchFilter();
        }
        private void RefreshSearchCategories()
        {
            var selected = cmbSearchCategory?.SelectedItem?.ToString();
            cmbSearchCategory.Items.Clear();
            cmbSearchCategory.Items.Add("All categories");
            var categories = DefaultCategories
                .Concat(_allMedicines.Select(m => m.Category).Where(c => !string.IsNullOrWhiteSpace(c)))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c);
            foreach (var cat in categories)
                cmbSearchCategory.Items.Add(cat);
            cmbSearchCategory.SelectedIndex = 0;
            if (selected != null)
            {
                var idx = cmbSearchCategory.Items.IndexOf(selected);
                if (idx >= 0) cmbSearchCategory.SelectedIndex = idx;
            }
        }
        private List<Medicine> GetFilteredMedicines()
        {
            decimal? minPrice = decimal.TryParse(txtMinPrice?.Text?.Trim(), out var min) ? min : (decimal?)null;
            decimal? maxPrice = decimal.TryParse(txtMaxPrice?.Text?.Trim(), out var max) ? max : (decimal?)null;
            var category = cmbSearchCategory?.SelectedIndex > 0
                ? cmbSearchCategory.SelectedItem?.ToString()
                : null;
            return SearchService.Search(
                _allMedicines,
                txtSearch?.Text ?? string.Empty,
                category,
                minPrice,
                maxPrice);
        }
        private void ApplySearchFilter()
        {
            if (gridMedicines == null) return;
            if (IsDesignHost() && _allMedicines.Count == 0)
                return;
            var filtered = GetFilteredMedicines();
            BindGrid(filtered);
            RefreshCategories(filtered);
            UpdateStats(_allMedicines);
            UpdateExpiryAlerts(_allMedicines);
        }
        private void BindGrid(List<Medicine> items)
        {
            var keepId = _selectedId;
            gridMedicines.DataSource = items.Select(m => new
            {
                m.MedicineID,
                m.MedicineName,
                m.Category,
                m.Dosage,
                Price = Medicines.GetEffectivePrice(m).ToString("N2"),
                Stock = m.StockQuantity,
                m.Supplier,
                Expiry = m.ExpiryDate.ToString("yyyy-MM-dd"),
                Status = Medicines.CheckExpiry(m) == MedicineService.ExpiryExpired ? "Expired"
                    : Medicines.CheckExpiry(m) == MedicineService.ExpiryExpiringSoon ? "Expiring Soon"
                    : "Valid",
                Rx = m.RequiresPrescription ? "\u2713" : "\u2717",
                Discount = $"{m.DiscountPercent:N0}%",
                Promo = m.IsOnPromotion ? "Yes" : "No"
            }).ToList();
            HideMedicineIdColumn();
            if (keepId.HasValue)
                SelectGridRowById(keepId.Value);
        }
        private void SelectGridRowById(int id)
        {
            if (gridMedicines.Rows.Count == 0) return;
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
        private void SyncActionButtons()
        {
            if (btnAdd == null || btnUpdate == null || btnDelete == null) return;
            var editing = _selectedId.HasValue;
            btnAdd.Enabled = !editing;
            btnUpdate.Enabled = editing;
            btnDelete.Enabled = editing;
        }
        private void HideMedicineIdColumn()
        {
            if (gridMedicines.Columns.Contains("MedicineID"))
                gridMedicines.Columns["MedicineID"].Visible = false;
        }
        private void RefreshCategories(List<Medicine> all)
        {
            if (cmbCategory == null) return;
            var categories = DefaultCategories
                .Concat(all.Select(m => m.Category).Where(c => !string.IsNullOrWhiteSpace(c)))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c)
                .ToArray();
            var current = cmbCategory.Text;
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(categories);
            if (!string.IsNullOrWhiteSpace(current))
                cmbCategory.Text = current;
        }
        private void UpdateStats(List<Medicine> all)
        {
            lblTotalItems.Text = all.Count.ToString("N0");
            lblLowStock.Text = all.Count(m => Medicines.IsLowStock(m)).ToString("N0");
            lblCompliance.Text = $"{Medicines.CompliancePercent(all):N1}%";
        }
        private void UpdateExpiryAlerts(List<Medicine> all)
        {
            var expired = Medicines.CountExpired(all);
            var expiring = Medicines.CountExpiringSoon(all);
            _expiryAlertLines = BuildExpiryAlertLines(all);

            if (expired == 0 && expiring == 0)
            {
                lblExpirySummary.Text = "No expiry alerts. All medicines are within safe expiry dates.";
                lblExpirySummary.ForeColor = Color.FromArgb(0, 100, 0);
                btnViewExpiryAlerts.Visible = false;
                panelExpiryAlerts.Height = 40;
                return;
            }

            var parts = new List<string>();
            if (expired > 0) parts.Add($"{expired} expired");
            if (expiring > 0) parts.Add($"{expiring} expiring within 30 days");
            lblExpirySummary.Text = string.Join(" · ", parts);
            lblExpirySummary.ForeColor = expired > 0 ? Color.DarkRed : Color.FromArgb(140, 70, 0);
            btnViewExpiryAlerts.Text = $"View All Alerts ({_expiryAlertLines.Count})";
            btnViewExpiryAlerts.Visible = true;
            panelExpiryAlerts.Height = 40;
        }

        private List<string> BuildExpiryAlertLines(IEnumerable<Medicine> all)
        {
            return all
                .Where(m => Medicines.CheckExpiry(m) != MedicineService.ExpiryValid)
                .OrderBy(m => m.ExpiryDate)
                .Select(m =>
                {
                    var status = Medicines.CheckExpiry(m) == MedicineService.ExpiryExpired
                        ? "Expired"
                        : "Expiring soon";
                    return $"{status} — {m.MedicineName} (exp. {m.ExpiryDate:yyyy-MM-dd})";
                })
                .ToList();
        }
        private void GridMedicines_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < 0 || gridMedicines.Rows[e.RowIndex].IsNewRow) return;
            var idCell = gridMedicines.Rows[e.RowIndex].Cells["MedicineID"];
            if (idCell?.Value == null) return;
            var id = Convert.ToInt32(idCell.Value);
            var item = _allMedicines.FirstOrDefault(m => m.MedicineID == id);
            if (item == null) return;

            var row = gridMedicines.Rows[e.RowIndex];
            if (Medicines.CheckExpiry(item) == MedicineService.ExpiryExpired)
            {
                row.DefaultCellStyle.BackColor = Color.FromArgb(255, 230, 230);
                row.DefaultCellStyle.ForeColor = Color.DarkRed;
            }
            else
            {
                row.DefaultCellStyle.BackColor = SystemColors.Window;
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
            var expiryStatus = Medicines.CheckExpiry(item);
            if (columnName != "Stock" && expiryStatus == MedicineService.ExpiryExpired)
            {
                e.CellStyle.BackColor = Color.FromArgb(255, 230, 230);
                e.CellStyle.ForeColor = Color.DarkRed;
            }
            if (columnName == "Stock")
            {
                e.CellStyle.ForeColor = Color.White;
                if (item.StockQuantity <= 20)
                    e.CellStyle.BackColor = Color.FromArgb(220, 53, 69);
                else if (item.StockQuantity <= 50)
                    e.CellStyle.BackColor = Color.FromArgb(255, 193, 7);
                else
                    e.CellStyle.BackColor = Color.FromArgb(40, 167, 69);
                e.Value = $"{item.StockQuantity} Units";
            }
            else if (columnName == "Status")
            {
                if (expiryStatus == MedicineService.ExpiryExpired)
                {
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (expiryStatus == MedicineService.ExpiryExpiringSoon)
                {
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
            }
            else if (columnName == "Expiry")
            {
                var status = Medicines.CheckExpiry(item);
                if (status == MedicineService.ExpiryExpired)
                {
                    e.CellStyle.ForeColor = Color.DarkRed;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
                else if (status == MedicineService.ExpiryExpiringSoon)
                {
                    e.CellStyle.ForeColor = Color.DarkOrange;
                    e.CellStyle.Font = UiTheme.UiFontBold;
                }
            }
            else if (columnName == "Rx")
            {
                e.CellStyle.ForeColor = item.RequiresPrescription
                    ? Color.FromArgb(0, 31, 102)
                    : SystemColors.GrayText;
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }
        private void GridMedicines_SelectionChanged(object sender, EventArgs e)
        {
            if (gridMedicines.CurrentRow == null || gridMedicines.CurrentRow.IsNewRow)
            {
                _selectedId = null;
                SyncActionButtons();
                return;
            }
            var idCell = gridMedicines.CurrentRow.Cells["MedicineID"];
            if (idCell?.Value == null)
            {
                _selectedId = null;
                SyncActionButtons();
                return;
            }
            var id = Convert.ToInt32(idCell.Value);
            var item = IsDesignHost()
                ? _allMedicines.FirstOrDefault(m => m.MedicineID == id)
                : Medicines.GetById(id);
            if (item == null)
            {
                _selectedId = null;
                SyncActionButtons();
                return;
            }
            _selectedId = id;
            txtName.Text = item.MedicineName;
            txtDosage.Text = item.Dosage;
            txtStock.Text = item.StockQuantity.ToString();
            dtpExpiry.Value = item.ExpiryDate;
            cmbCategory.Text = item.Category;
            txtPrice.Text = item.Price.ToString("N2");
            txtSupplier.Text = item.Supplier;
            txtDiscount.Text = item.DiscountPercent.ToString("N0");
            chkPrescription.Checked = item.RequiresPrescription;
            chkPromotion.Checked = item.IsOnPromotion;
            SyncActionButtons();
        }
        private Medicine ReadForm()
        {
            if (!int.TryParse(txtStock.Text.Trim(), out var stock))
                throw new ArgumentException("Stock quantity must be a valid number.");
            if (stock < 0)
                throw new ArgumentException("Stock quantity must be 0 or greater.");
            if (!decimal.TryParse(txtPrice.Text.Trim(), out var price))
                throw new ArgumentException("Price must be a valid number.");
            if (!decimal.TryParse(txtDiscount.Text.Trim(), out var discount))
                throw new ArgumentException("Discount must be a valid number.");

            return new Medicine
            {
                MedicineID = _selectedId ?? 0,
                MedicineName = txtName.Text.Trim(),
                Category = cmbCategory.Text.Trim(),
                Dosage = txtDosage.Text.Trim(),
                Price = price,
                StockQuantity = stock,
                Supplier = txtSupplier.Text.Trim(),
                ExpiryDate = dtpExpiry.Value.Date,
                RequiresPrescription = chkPrescription.Checked,
                DiscountPercent = discount,
                IsOnPromotion = chkPromotion.Checked
            };
        }
        private void ClearForm()
        {
            _selectedId = null;
            txtName.Clear();
            txtDosage.Clear();
            txtStock.Clear();
            txtPrice.Clear();
            txtSupplier.Clear();
            txtDiscount.Text = "0";
            cmbCategory.SelectedIndex = -1;
            cmbCategory.Text = string.Empty;
            dtpExpiry.Value = DateTime.Today.AddMonths(6);
            chkPrescription.Checked = false;
            chkPromotion.Checked = false;
            gridMedicines.ClearSelection();
            SyncActionButtons();
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (_selectedId.HasValue)
            {
                MessageBox.Show(
                    "A medicine is selected from the list. Use Update Record to save changes, or Clear Form before adding a new medicine.",
                    "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                var item = ReadForm();
                if (item.StockQuantity <= 0)
                    throw new ArgumentException("Stock quantity must be greater than 0 when adding a new medicine.");
                Medicines.Add(item);
                ClearForm();
                LoadMedicines();
                MessageBox.Show("Medicine added successfully.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Select a medicine from the grid to update.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Medicines.GetById(_selectedId.Value) == null)
            {
                MessageBox.Show(
                    "The selected medicine is no longer in the inventory. Select an existing record or use Add New Medicine.",
                    "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ClearForm();
                LoadMedicines();
                return;
            }
            try
            {
                var item = ReadForm();
                item.MedicineID = _selectedId.Value;
                Medicines.Update(item);
                LoadMedicines();
                MessageBox.Show("Medicine updated successfully.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Select a medicine from the grid to delete.", "SmartMed",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Delete this medicine record?", "Confirm Delete",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                return;
            try
            {
                Medicines.Delete(_selectedId.Value);
                ClearForm();
                LoadMedicines();
                MessageBox.Show("Medicine deleted.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                    Medicines.ExportToCsv(items, dialog.FileName);
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
                Medicines.PrintInventory(items, "SmartMed — Medicine Inventory");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Print Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
