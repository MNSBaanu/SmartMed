using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class StatusChipHelper
    {
        public static void OnCellFormatting(DataGridView grid, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0 || e.RowIndex < 0) return;
            var col = grid.Columns[e.ColumnIndex];
            if (col.Name != "Status" && col.HeaderText != "Status") return;

            var status = e.Value?.ToString() ?? "";
            GetStatusColors(status, out Color back, out Color fore);
            e.CellStyle.BackColor = back;
            e.CellStyle.ForeColor = fore;
            e.CellStyle.Font = new Font(ClinicalPrecisionTheme.DataGridFont, FontStyle.Bold);
            e.CellStyle.SelectionBackColor = back;
            e.CellStyle.SelectionForeColor = fore;
        }

        public static void GetStatusColors(string status, out Color back, out Color fore)
        {
            switch (status)
            {
                case "Ready for Pickup":
                    back = ClinicalPrecisionTheme.StatusReadyBg;
                    fore = ClinicalPrecisionTheme.StatusReadyFg;
                    break;
                case "Delivered":
                    back = ClinicalPrecisionTheme.StatusDeliveredBg;
                    fore = ClinicalPrecisionTheme.StatusDeliveredFg;
                    break;
                default:
                    back = ClinicalPrecisionTheme.StatusPendingBg;
                    fore = ClinicalPrecisionTheme.StatusPendingFg;
                    break;
            }
        }
    }
}
