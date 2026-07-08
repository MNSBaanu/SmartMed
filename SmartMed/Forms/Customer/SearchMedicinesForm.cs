using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class SearchMedicinesForm : EmbeddedPageForm
    {
        private MedicineService _medicines;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;

        public SearchMedicinesForm()
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

        protected override void DoRefreshPage() => Search();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            UiTheme.SetGridDataSource(grid, DesignTimePreviewData.SearchMedicineRows());
            if (grid.Columns.Contains("MedicineID"))
                grid.Columns["MedicineID"].Visible = false;
            UiTheme.BeautifyGridHeaders(grid);
            AddCartColumns();
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.ApplyClinicalGrid(grid);
            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtCategory);
            UiTheme.StyleTextBox(txtMinPrice);
            UiTheme.StyleTextBox(txtMaxPrice);

            WirePanelBorder(panelGridOuter);
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

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnSearch.Click += (s, e) => Search();
            grid.CellContentClick += Grid_CellContentClick;
        }

        private void Search()
        {
            if (!_servicesReady) return;

            decimal? min = decimal.TryParse(txtMinPrice?.Text, out var minVal) ? minVal : (decimal?)null;
            decimal? max = decimal.TryParse(txtMaxPrice?.Text, out var maxVal) ? maxVal : (decimal?)null;

            var results = _medicines.SearchForCustomers(txtName?.Text ?? "", txtCategory?.Text ?? "", min, max)
                .Select(m => new
                {
                    m.MedicineID,
                    m.MedicineName,
                    m.Category,
                    Price = $"LKR {_medicines.GetEffectivePrice(m):N2}",
                    m.StockQuantity,
                    Rx = m.RequiresPrescription ? "Yes" : "No",
                    Discount = _medicines.GetCustomerDiscountDisplay(m),
                    Promo = _medicines.GetCustomerPromoDisplay(m)
                })
                .ToList();

            UiTheme.SetGridDataSource(grid, results);
            if (grid.Columns.Contains("MedicineID"))
                grid.Columns["MedicineID"].Visible = false;
            UiTheme.BeautifyGridHeaders(grid);
            AddCartColumns();
        }

        private void AddCartColumns()
        {
            grid.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;

            foreach (DataGridViewColumn col in grid.Columns)
                col.ReadOnly = col.Name != "Qty";

            if (!grid.Columns.Contains("Qty"))
            {
                grid.Columns.Add(new DataGridViewNumericUpDownColumn
                {
                    Name = "Qty",
                    HeaderText = "Qty",
                    Width = 80,
                    ReadOnly = false,
                    Minimum = 1,
                    Maximum = 99,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }
            else
            {
                grid.Columns["Qty"].ReadOnly = false;
            }
            grid.Columns["Qty"].DefaultCellStyle.Padding = new Padding(2, 0, 20, 0);

            if (!grid.Columns.Contains("AddBtn"))
            {
                grid.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "AddBtn",
                    HeaderText = "",
                    Text = "Add to Cart",
                    UseColumnTextForButtonValue = true,
                    Width = 120,
                    ReadOnly = true,
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.None
                });
            }

            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                if (row.Cells["Qty"].Value == null)
                    row.Cells["Qty"].Value = "1";
            }
        }

        private void Grid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            if (grid.Columns[e.ColumnIndex].Name != "AddBtn") return;
            grid.EndEdit();
            AddRowToCart(grid.Rows[e.RowIndex]);
        }

        private void AddRowToCart(DataGridViewRow row)
        {
            if (!_servicesReady || row == null) return;
            try
            {
                var id = Convert.ToInt32(row.Cells["MedicineID"].Value);
                var medicine = _medicines.GetById(id);
                if (medicine == null) return;

                _medicines.ValidateForCustomerPurchase(medicine);
                var qty = 1;
                if (decimal.TryParse(row.Cells["Qty"].Value?.ToString(), out var qtyVal) && qtyVal >= 1)
                    qty = (int)qtyVal;
                CartService.Add(medicine, qty, _medicines);
                var offer = _medicines.GetCustomerOfferDisplay(medicine);
                var message = offer != "—"
                    ? $"{medicine.MedicineName} added to cart.\n{offer}"
                    : $"{medicine.MedicineName} added to cart.";
                SmartMedMessageBox.Show(message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Cart", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }

    internal sealed class DataGridViewNumericUpDownColumn : DataGridViewColumn
    {
        public DataGridViewNumericUpDownColumn() : base(new SpinCell()) { }
        public decimal Minimum { get; set; } = 1;
        public decimal Maximum { get; set; } = 99;

        private sealed class SpinCell : DataGridViewTextBoxCell
        {
            private const int BtnW = 17;

            private static System.Drawing.Rectangle SpinArea(System.Drawing.Rectangle c) =>
                new System.Drawing.Rectangle(c.Right - BtnW - 2, c.Top + 2, BtnW, c.Height - 4);
            private static System.Drawing.Rectangle UpRect(System.Drawing.Rectangle c)
            { var a = SpinArea(c); return new System.Drawing.Rectangle(a.Left, a.Top, a.Width, a.Height / 2); }
            private static System.Drawing.Rectangle DownRect(System.Drawing.Rectangle c)
            { var a = SpinArea(c); return new System.Drawing.Rectangle(a.Left, a.Top + a.Height / 2, a.Width, a.Height - a.Height / 2); }

            protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle clipBounds,
                System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState,
                object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
                DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
            {
                base.Paint(g, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
                if ((paintParts & DataGridViewPaintParts.ContentForeground) == 0) return;
                DrawArrow(g, UpRect(cellBounds), true);
                DrawArrow(g, DownRect(cellBounds), false);
            }

            private static void DrawArrow(System.Drawing.Graphics g, System.Drawing.Rectangle r, bool up)
            {
                if (Application.RenderWithVisualStyles)
                    new VisualStyleRenderer(up ? VisualStyleElement.Spin.Up.Normal : VisualStyleElement.Spin.Down.Normal)
                        .DrawBackground(g, r);
                else
                    ControlPaint.DrawScrollButton(g, r, up ? ScrollButton.Up : ScrollButton.Down, ButtonState.Normal);
            }

            protected override void OnMouseDown(DataGridViewCellMouseEventArgs e)
            {
                base.OnMouseDown(e);
                if (DataGridView == null) return;
                var size = DataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, false).Size;
                var local = new System.Drawing.Rectangle(System.Drawing.Point.Empty, size);
                decimal delta;
                if (UpRect(local).Contains(e.Location)) delta = 1;
                else if (DownRect(local).Contains(e.Location)) delta = -1;
                else return;

                if (DataGridView.IsCurrentCellInEditMode)
                    DataGridView.EndEdit();

                var col = OwningColumn as DataGridViewNumericUpDownColumn;
                decimal min = col?.Minimum ?? 1, max = col?.Maximum ?? 99;
                decimal.TryParse(GetValue(e.RowIndex)?.ToString(), out var v);
                v = Math.Max(min, Math.Min(max, v + delta));
                SetValue(e.RowIndex, v.ToString(CultureInfo.InvariantCulture));
                DataGridView.InvalidateCell(ColumnIndex, e.RowIndex);
                DataGridView.Update();
            }
        }
    }
}
