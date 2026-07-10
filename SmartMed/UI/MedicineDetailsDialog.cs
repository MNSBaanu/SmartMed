using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    internal sealed class MedicineDetailsDialog : Form
    {
        private MedicineDetailsDialog(Medicine medicine, MedicineService medicines)
        {
            Text = "Medicine Details";
            StartPosition = FormStartPosition.CenterParent;
            Width = 480;
            Height = 520;
            MinimumSize = new Size(420, 460);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            BackColor = UiTheme.AdminSurface;
            Font = UiTheme.UiFont;

            var panelHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 72,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = Color.FromArgb(238, 245, 244)
            };

            var lblTitle = new Label
            {
                Text = medicine?.MedicineName ?? "Medicine",
                AutoSize = false,
                Width = 420,
                Height = 28,
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminOnSurface,
                Location = new Point(16, 12)
            };
            var lblSubtitle = new Label
            {
                Text = medicine?.Category ?? string.Empty,
                AutoSize = true,
                Font = UiTheme.UiFont,
                ForeColor = UiTheme.AdminMuted,
                Location = new Point(16, 40)
            };
            panelHeader.Controls.Add(lblTitle);
            panelHeader.Controls.Add(lblSubtitle);

            var panelBody = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(16, 12, 16, 8),
                BackColor = UiTheme.AdminSurface
            };

            var stack = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                Dock = DockStyle.Top,
                BackColor = UiTheme.AdminSurface
            };

            var table = new TableLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                ColumnCount = 2,
                BackColor = UiTheme.AdminSurface,
                Width = 400
            };
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 140));
            table.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            var effectivePrice = medicines.GetEffectivePrice(medicine);
            var listPrice = medicine.Price;
            var hasPromo = effectivePrice < listPrice;

            AddDetailRow(table, "Dosage / Form", medicine.Dosage ?? "—");
            AddDetailRow(table, "List Price", $"LKR {listPrice:N2}");
            AddDetailRow(table, "Your Price", $"LKR {effectivePrice:N2}", hasPromo);
            AddDetailRow(table, "Discount", medicines.GetCustomerDiscountDisplay(medicine));
            AddDetailRow(table, "Promotion", medicines.GetCustomerPromoDisplay(medicine));
            AddDetailRow(table, "Availability", medicines.GetCustomerStockDisplay(medicine));
            AddDetailRow(table, "Prescription", medicine.RequiresPrescription ? "Required" : "Not required");
            AddDetailRow(table, "Valid Until", medicine.ExpiryDate.ToString("dd MMM yyyy"));
            AddDetailRow(table, "Product ID", $"#M-{medicine.MedicineID:D4}");

            stack.Controls.Add(table);

            if (medicine.RequiresPrescription)
                stack.Controls.Add(CreateNotePanel(
                    "A valid prescription must be uploaded when you place an order for this item."));

            if (medicines.IsLowStock(medicine) && medicine.StockQuantity > 0)
                stack.Controls.Add(CreateNotePanel(
                    "Stock is limited. Order soon to avoid missing out."));

            panelBody.Controls.Add(stack);

            var panelFooter = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 52,
                Padding = new Padding(16, 8, 16, 12),
                BackColor = UiTheme.AdminSurface
            };

            var btnClose = AdminUiHelpers.CreateWinButton("Close", true, 96);
            btnClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnClose.DialogResult = DialogResult.OK;
            panelFooter.Controls.Add(btnClose);
            panelFooter.Resize += (s, e) =>
            {
                btnClose.Left = panelFooter.ClientSize.Width - btnClose.Width - 16;
                btnClose.Top = 8;
            };
            btnClose.Location = new Point(panelFooter.Width - 112, 8);

            Controls.Add(panelBody);
            Controls.Add(panelFooter);
            Controls.Add(panelHeader);

            AcceptButton = btnClose;
            CancelButton = btnClose;
        }

        private static Panel CreateNotePanel(string text)
        {
            var panel = new Panel
            {
                AutoSize = true,
                Width = 400,
                Margin = new Padding(0, 8, 0, 0),
                BackColor = Color.FromArgb(255, 248, 232),
                Padding = new Padding(12, 8, 12, 8)
            };
            panel.Controls.Add(new Label
            {
                Text = text,
                AutoSize = true,
                MaximumSize = new Size(360, 0),
                ForeColor = Color.FromArgb(140, 70, 0),
                Font = UiTheme.UiFont,
                BackColor = Color.FromArgb(255, 248, 232)
            });
            return panel;
        }

        private static void AddDetailRow(TableLayoutPanel table, string label, string value, bool highlightValue = false)
        {
            var row = table.RowCount++;
            table.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            table.Controls.Add(new Label
            {
                Text = label,
                AutoSize = true,
                ForeColor = UiTheme.AdminMuted,
                Font = UiTheme.UiFont,
                Margin = new Padding(0, 0, 8, 10),
                BackColor = UiTheme.AdminSurface
            }, 0, row);

            table.Controls.Add(new Label
            {
                Text = value ?? "—",
                AutoSize = true,
                MaximumSize = new Size(280, 0),
                ForeColor = highlightValue ? UiTheme.AdminTeal : UiTheme.AdminOnSurface,
                Font = highlightValue ? UiTheme.UiFontBold : UiTheme.UiFont,
                Margin = new Padding(0, 0, 0, 10),
                BackColor = UiTheme.AdminSurface
            }, 1, row);
        }

        public static void Show(IWin32Window owner, Medicine medicine, MedicineService medicines)
        {
            if (medicine == null || medicines == null) return;

            using (var dlg = new MedicineDetailsDialog(medicine, medicines))
            {
                if (owner != null)
                    dlg.ShowDialog(owner);
                else
                    dlg.ShowDialog();
            }
        }
    }
}
