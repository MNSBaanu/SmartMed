using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.Data.Models;
using SmartMed.UI.Theming;

namespace SmartMed.UI
{
    public partial class ManageMedicinesForm : AdminShellForm
    {
        private static readonly string[] DefaultCategories =
        {
            "Antibiotic", "Analgesic", "Antidiabetic", "Hypertension", "Antiviral", "Vitamin", "Other"
        };

        private readonly MedicineService _medicines = new MedicineService();
        private int? _selectedId;

        private DataGridView gridMedicines;
        private TextBox txtName;
        private TextBox txtDosage;
        private TextBox txtStock;
        private DateTimePicker dtpExpiry;
        private ComboBox cmbCategory;
        private TextBox txtPrice;
        private TextBox txtSupplier;
        private CheckBox chkPrescription;
        private Button btnClear;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private Label lblTotalItems;
        private Label lblLowStock;
        private Label lblCompliance;

        public ManageMedicinesForm()
            : base(AdminNavItem.Medicines, "Manage Medicines", "SmartMed - Manage Medicines")
        {
            BuildContent();
            LoadMedicines();
        }

        private void BuildContent()
        {
            panelContent.Controls.Clear();

            var root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 4
            };
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 220f));
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            root.Controls.Add(CreatePageHeader(), 0, 0);
            root.Controls.Add(CreateGridPanel(), 0, 1);
            root.Controls.Add(CreateFormPanel(), 0, 2);
            root.Controls.Add(CreateFooterStats(), 0, 3);

            panelContent.Controls.Add(root);
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

            var titleBlock = new Panel { Dock = DockStyle.Left, Width = 480 };
            var lblTitle = new Label
            {
                Text = "Manage Medicines",
                Font = AppTheme.SectionHeaderFont,
                ForeColor = AppTheme.Primary,
                Dock = DockStyle.Top,
                Height = 24
            };
            var lblSub = new Label
            {
                Text = "Update and monitor pharmaceutical inventory levels.",
                Font = AppTheme.BodyFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                Dock = DockStyle.Top,
                Height = 20
            };
            titleBlock.Controls.Add(lblSub);
            titleBlock.Controls.Add(lblTitle);

            var actions = new FlowLayoutPanel
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false,
                Padding = new Padding(0, 8, 0, 0)
            };
            var btnExport = CreateToolbarButton("\uE896", "Export");
            btnExport.Click += (s, e) => ShowComingSoon("Export");
            var btnPrint = CreateToolbarButton("\uE749", "Print");
            btnPrint.Click += (s, e) => ShowComingSoon("Print");
            actions.Controls.Add(btnExport);
            actions.Controls.Add(btnPrint);

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
                Width = 100,
                Margin = new Padding(4, 0, 0, 0)
            };
            ThemeApplier.ApplyPrimaryButton(btn);
            return btn;
        }

        private Panel CreateGridPanel()
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

            gridMedicines = new DataGridView
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
                MultiSelect = false
            };
            ThemeApplier.ApplyDataGrid(gridMedicines);
            gridMedicines.SelectionChanged += GridMedicines_SelectionChanged;

            outer.Controls.Add(gridMedicines);
            return outer;
        }

        private Panel CreateFormPanel()
        {
            var outer = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = AppTheme.SurfaceContainer,
                Padding = new Padding(24),
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
            txtDosage = new TextBox();
            txtStock = new TextBox();
            dtpExpiry = new DateTimePicker { Format = DateTimePickerFormat.Short, Height = AppTheme.InputHeight };
            cmbCategory = new ComboBox { DropDownStyle = ComboBoxStyle.DropDown };
            txtPrice = new TextBox();
            txtSupplier = new TextBox();
            chkPrescription = new CheckBox { Text = "Requires Prescription (Rx)", AutoSize = true };

            columns.Controls.Add(CreateFieldColumn(
                CreateField("Medicine Name", txtName),
                CreateField("Dosage / Form", txtDosage),
                CreateField("Current Stock", txtStock),
                CreateField("Expiry Date", dtpExpiry)), 0, 0);

            var rxPanel = new Panel { Height = AppTheme.InputHeight + 24, Dock = DockStyle.Top };
            chkPrescription.Font = AppTheme.LabelFont;
            chkPrescription.ForeColor = AppTheme.Primary;
            chkPrescription.Location = new Point(0, 28);
            rxPanel.Controls.Add(chkPrescription);
            rxPanel.Controls.Add(new Label
            {
                Text = "",
                Height = 20,
                Dock = DockStyle.Top
            });

            columns.Controls.Add(CreateFieldColumn(
                CreateField("Category", cmbCategory),
                CreateField("Unit Price (LKR)", txtPrice),
                CreateField("Supplier", txtSupplier),
                rxPanel), 1, 0);

            btnClear = new Button { Text = "Clear Form", Width = 110, Height = 40 };
            btnDelete = new Button { Text = "Delete Entry", Width = 110, Height = 40 };
            btnUpdate = new Button { Text = "Update Record", Width = 120, Height = 40 };
            btnAdd = new Button { Text = "Add New Medicine", Width = 150, Height = 40 };

            ThemeApplier.ApplySecondaryButton(btnClear);
            ThemeApplier.ApplyDangerButton(btnDelete);
            ThemeApplier.ApplySecondaryActionButton(btnUpdate);
            ThemeApplier.ApplyAccentButton(btnAdd);

            btnClear.Click += (s, e) => ClearForm();
            btnDelete.Click += BtnDelete_Click;
            btnUpdate.Click += BtnUpdate_Click;
            btnAdd.Click += BtnAdd_Click;

            var actions = new Panel { Dock = DockStyle.Top, Height = 56, Padding = new Padding(0, 16, 0, 0) };
            actions.Paint += (s, e) =>
            {
                using (var pen = new Pen(Color.FromArgb(128, AppTheme.OutlineVariant)))
                    e.Graphics.DrawLine(pen, 0, 0, actions.Width, 0);
            };
            var actionFlow = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.LeftToRight,
                WrapContents = false
            };
            var spacer = new Panel { Width = 200, Height = 1 };
            actionFlow.Controls.Add(btnClear);
            actionFlow.Controls.Add(btnDelete);
            actionFlow.Controls.Add(spacer);
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
            var wrap = new Panel { Height = AppTheme.InputHeight + 24, Dock = DockStyle.Top, Padding = new Padding(0, 0, 0, 8) };
            var lbl = new Label
            {
                Text = labelText,
                Dock = DockStyle.Top,
                Height = 20
            };
            ThemeApplier.ApplyFieldLabel(lbl);
            input.Dock = DockStyle.Top;
            input.Height = AppTheme.InputHeight;
            if (input is TextBox tb) ThemeApplier.ApplyTextBox(tb);
            if (input is ComboBox cb) ThemeApplier.ApplyComboBox(cb);
            wrap.Controls.Add(input);
            wrap.Controls.Add(lbl);
            return wrap;
        }

        private Panel CreateFooterStats()
        {
            var row = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 88,
                ColumnCount = 3,
                RowCount = 1
            };
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33f));
            row.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34f));

            lblTotalItems = new Label();
            lblLowStock = new Label();
            lblCompliance = new Label();

            row.Controls.Add(CreateStatTile("\uE7F4", "Total Items", lblTotalItems, AppTheme.Primary), 0, 0);
            row.Controls.Add(CreateStatTile("\uE7BA", "Low Stock Alert", lblLowStock, AppTheme.Error), 1, 0);
            row.Controls.Add(CreateStatTile("\uE73E", "Rx Required", lblCompliance, AppTheme.OnSecondaryContainer), 2, 0);

            return row;
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

            var iconBox = new Label
            {
                Text = icon,
                Font = AppTheme.IconFont,
                ForeColor = accent,
                Location = new Point(16, 20),
                AutoSize = true
            };
            var lblTitle = new Label
            {
                Text = title.ToUpperInvariant(),
                Font = AppTheme.LabelFont,
                ForeColor = AppTheme.OnSurfaceVariant,
                Location = new Point(52, 16),
                AutoSize = true
            };
            valueLabel.Text = "0";
            valueLabel.Font = AppTheme.StatValueFont;
            valueLabel.ForeColor = accent;
            valueLabel.Location = new Point(52, 36);
            valueLabel.AutoSize = true;

            card.Controls.AddRange(new Control[] { iconBox, lblTitle, valueLabel });
            return card;
        }

        private void LoadMedicines()
        {
            var all = _medicines.GetAll();
            gridMedicines.DataSource = all.Select(m => new
            {
                m.MedicineID,
                m.MedicineName,
                m.Category,
                m.Dosage,
                Price = m.Price.ToString("N2"),
                Stock = m.StockQuantity,
                m.Supplier,
                Expiry = m.ExpiryDate.ToString("yyyy-MM-dd"),
                Rx = m.RequiresPrescription ? "Yes" : "No"
            }).ToList();

            if (gridMedicines.Columns.Contains("MedicineID"))
                gridMedicines.Columns["MedicineID"].Visible = false;

            RefreshCategories(all);
            UpdateFooterStats(all);
        }

        private void RefreshCategories(System.Collections.Generic.List<MedicineItem> all)
        {
            var categories = DefaultCategories
                .Concat(all.Select(m => m.Category).Where(c => !string.IsNullOrWhiteSpace(c)))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(c => c)
                .ToArray();
            cmbCategory.Items.Clear();
            cmbCategory.Items.AddRange(categories);
        }

        private void UpdateFooterStats(System.Collections.Generic.List<MedicineItem> all)
        {
            lblTotalItems.Text = all.Count.ToString("N0");
            lblLowStock.Text = all.Count(m => m.StockQuantity <= 20).ToString("N0");
            var rxCount = all.Count(m => m.RequiresPrescription);
            var pct = all.Count == 0 ? 0m : (decimal)rxCount / all.Count * 100m;
            lblCompliance.Text = $"{pct:N1}%";
        }

        private void GridMedicines_SelectionChanged(object sender, EventArgs e)
        {
            if (gridMedicines.CurrentRow == null) return;
            var idCell = gridMedicines.CurrentRow.Cells["MedicineID"];
            if (idCell?.Value == null) return;

            _selectedId = Convert.ToInt32(idCell.Value);
            var item = _medicines.GetAll().FirstOrDefault(m => m.MedicineID == _selectedId);
            if (item == null) return;

            txtName.Text = item.MedicineName;
            txtDosage.Text = item.Dosage;
            txtStock.Text = item.StockQuantity.ToString();
            dtpExpiry.Value = item.ExpiryDate;
            cmbCategory.Text = item.Category;
            txtPrice.Text = item.Price.ToString("N2");
            txtSupplier.Text = item.Supplier;
            chkPrescription.Checked = item.RequiresPrescription;
        }

        private MedicineItem ReadForm()
        {
            if (!int.TryParse(txtStock.Text.Trim(), out var stock))
                throw new ArgumentException("Stock must be a valid number.");
            if (!decimal.TryParse(txtPrice.Text.Trim(), out var price))
                throw new ArgumentException("Price must be a valid number.");

            return new MedicineItem
            {
                MedicineID = _selectedId ?? 0,
                MedicineName = txtName.Text.Trim(),
                Category = cmbCategory.Text.Trim(),
                Dosage = txtDosage.Text.Trim(),
                Price = price,
                StockQuantity = stock,
                Supplier = txtSupplier.Text.Trim(),
                ExpiryDate = dtpExpiry.Value.Date,
                RequiresPrescription = chkPrescription.Checked
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
            cmbCategory.SelectedIndex = -1;
            cmbCategory.Text = string.Empty;
            dtpExpiry.Value = DateTime.Today.AddMonths(6);
            chkPrescription.Checked = false;
            gridMedicines.ClearSelection();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var item = ReadForm();
                _medicines.Add(item);
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

            try
            {
                var item = ReadForm();
                item.MedicineID = _selectedId.Value;
                _medicines.Update(item);
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

            var confirm = MessageBox.Show("Delete this medicine record?", "Confirm Delete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                _medicines.Delete(_selectedId.Value);
                ClearForm();
                LoadMedicines();
                MessageBox.Show("Medicine deleted.", "SmartMed", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
