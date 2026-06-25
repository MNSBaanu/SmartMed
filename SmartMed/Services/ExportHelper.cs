using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using SmartMed.UI;

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

        public static void ExportDataTableToPdf(DataTable table, string filePath, string title, string subtitle = null)
        {
            if (table == null) throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path is required.", nameof(filePath));

            var doc = new PdfDocument();
            doc.Info.Title = title;

            const double margin = 40;
            const double rowHeight = 16;
            var titleFont = new XFont("Arial", 14, XFontStyle.Bold);
            var subtitleFont = new XFont("Arial", 9);
            var headerFont = new XFont("Arial", 9, XFontStyle.Bold);
            var bodyFont = new XFont("Arial", 8);

            var colCount = table.Columns.Count;
            var colWidths = BuildColumnWidths(table, XUnit.FromPoint(595).Point - margin * 2);

            PdfPage page = null;
            XGraphics gfx = null;
            var y = margin;

            void BeginPage(bool withHeader)
            {
                page = doc.AddPage();
                page.Size = PdfSharp.PageSize.A4;
                gfx = XGraphics.FromPdfPage(page);
                y = margin;

                gfx.DrawString(title, titleFont, XBrushes.Black,
                    new XRect(margin, y, page.Width.Point - margin * 2, 20), XStringFormats.TopLeft);
                y += 22;

                if (!string.IsNullOrWhiteSpace(subtitle))
                {
                    gfx.DrawString(subtitle, subtitleFont, XBrushes.DarkGray,
                        new XRect(margin, y, page.Width.Point - margin * 2, 16), XStringFormats.TopLeft);
                    y += 18;
                }

                if (withHeader)
                    DrawTableHeader();
            }

            void DrawTableHeader()
            {
                var x = margin;
                for (var c = 0; c < colCount; c++)
                {
                    var rect = new XRect(x, y, colWidths[c], rowHeight);
                    gfx.DrawRectangle(XPens.Gray, rect);
                    gfx.DrawString(table.Columns[c].ColumnName, headerFont, XBrushes.Black, rect, XStringFormats.CenterLeft);
                    x += colWidths[c];
                }
                y += rowHeight;
            }

            BeginPage(true);

            foreach (DataRow row in table.Rows)
            {
                if (y + rowHeight > page.Height.Point - margin)
                    BeginPage(true);

                var x = margin;
                for (var c = 0; c < colCount; c++)
                {
                    var text = row[c]?.ToString() ?? string.Empty;
                    var rect = new XRect(x, y, colWidths[c], rowHeight);
                    gfx.DrawRectangle(XPens.LightGray, rect);
                    gfx.DrawString(TrimForCell(text, colWidths[c], bodyFont, gfx), bodyFont, XBrushes.Black, rect, XStringFormats.CenterLeft);
                    x += colWidths[c];
                }
                y += rowHeight;
            }

            doc.Save(filePath);
        }

        public static void PrintGrid(DataGridView grid, string title)
        {
            if (grid == null || grid.Rows.Count == 0)
                throw new InvalidOperationException("No data to print.");

            var rowIndex = 0;
            var bodyFont = UiTheme.UiFont;
            var doc = new PrintDocument { DocumentName = title };
            doc.PrintPage += (s, e) =>
            {
                float y = e.MarginBounds.Top;
                float lineHeight = e.Graphics.MeasureString("X", bodyFont).Height + 4;

                e.Graphics.DrawString(title, UiTheme.UiFontBold, Brushes.Black, e.MarginBounds.Left, y);
                y += lineHeight * 2;

                var headers = new StringBuilder();
                foreach (DataGridViewColumn col in grid.Columns)
                {
                    if (!col.Visible) continue;
                    if (headers.Length > 0) headers.Append(" | ");
                    headers.Append(col.HeaderText);
                }
                e.Graphics.DrawString(headers.ToString(), UiTheme.UiFontBold, Brushes.Black, e.MarginBounds.Left, y);
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

        private static double[] BuildColumnWidths(DataTable table, double totalWidth)
        {
            var weights = new double[table.Columns.Count];
            for (var i = 0; i < table.Columns.Count; i++)
            {
                var maxLen = table.Columns[i].ColumnName.Length;
                foreach (DataRow row in table.Rows)
                {
                    var len = (row[i]?.ToString() ?? string.Empty).Length;
                    if (len > maxLen) maxLen = len;
                }
                weights[i] = Math.Max(maxLen, 4);
            }

            var sum = weights.Sum();
            var widths = new double[table.Columns.Count];
            for (var i = 0; i < widths.Length; i++)
                widths[i] = totalWidth * (weights[i] / sum);
            return widths;
        }

        private static string TrimForCell(string text, double width, XFont font, XGraphics gfx)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            const int pad = 6;
            var available = width - pad;
            if (gfx.MeasureString(text, font).Width <= available)
                return text;

            var trimmed = text;
            while (trimmed.Length > 1 && gfx.MeasureString(trimmed + "…", font).Width > available)
                trimmed = trimmed.Substring(0, trimmed.Length - 1);
            return trimmed + "…";
        }
    }
}
