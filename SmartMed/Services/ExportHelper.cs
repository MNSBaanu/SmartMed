using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SmartMed.Services
{
    public static class ExportHelper
    {
        public static void ExportDataTableToCsv(DataTable table, string filePath)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            var sb = new StringBuilder();
            for (var c = 0; c < table.Columns.Count; c++)
            {
                if (c > 0) sb.Append(',');
                sb.Append(EscapeCsv(table.Columns[c].ColumnName));
            }
            sb.AppendLine();

            foreach (DataRow row in table.Rows)
            {
                for (var c = 0; c < table.Columns.Count; c++)
                {
                    if (c > 0) sb.Append(',');
                    sb.Append(EscapeCsv(row[c]?.ToString() ?? string.Empty));
                }
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);
        }

        public static void PrintGrid(DataGridView grid, string title)
        {
            if (grid == null || grid.Rows.Count == 0)
                throw new InvalidOperationException("No data to print.");

            var rowIndex = 0;
            var bodyFont = SystemFonts.DefaultFont;
            var doc = new PrintDocument { DocumentName = title };
            doc.PrintPage += (s, e) =>
            {
                float y = e.MarginBounds.Top;
                float lineHeight = e.Graphics.MeasureString("X", bodyFont).Height + 4;

                using (var headerFont = new Font(bodyFont.FontFamily, 14, FontStyle.Bold))
                {
                    e.Graphics.DrawString(title, headerFont, Brushes.Black, e.MarginBounds.Left, y);
                    y += lineHeight * 2;
                }

                var headers = new StringBuilder();
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (!col.Visible) continue;
                    if (headers.Length > 0) headers.Append(" | ");
                    headers.Append(col.HeaderText);
                }
                e.Graphics.DrawString(headers.ToString(), new Font(bodyFont, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, y);
                y += lineHeight;

                while (rowIndex < grid.Rows.Count && y + lineHeight < e.MarginBounds.Bottom)
                {
                    var line = new StringBuilder();
                    foreach (DataGridViewColumn col in grid.Columns)
                    {
                        if (!col.Visible) continue;
                        if (line.Length > 0) line.Append(" | ");
                        line.Append(grid.Rows[rowIndex].Cells[col.Index].Value?.ToString() ?? string.Empty);
                    }
                    e.Graphics.DrawString(line.ToString(), bodyFont, Brushes.Black, e.MarginBounds.Left, y);
                    y += lineHeight;
                    rowIndex++;
                }

                e.HasMorePages = rowIndex < grid.Rows.Count;
            };

            using (var preview = new PrintPreviewDialog { Document = doc, Width = 900, Height = 650 })
                preview.ShowDialog();
        }

        private static string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            return "\"" + value.Replace("\"", "\"\"") + "\"";
        }
    }
}
