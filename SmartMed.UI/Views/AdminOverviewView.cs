using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business.Services;
using SmartMed.UI.Theming;

namespace SmartMed.UI.Views
{
    public class AdminOverviewView : UserControl
    {
        public AdminOverviewView()
        {
            BackColor = ClinicalPrecisionTheme.Surface;
            Dock = DockStyle.Fill;

            var header = UiFactory.CreateSectionHeader("Dashboard Overview");
            header.Location = new Point(0, 0);
            Controls.Add(header);

            var service = new DashboardService();
            int y = 40;

            Controls.Add(CreateStatCard("Total Sales", $"LKR {service.TotalSales:N2}", 0, y));
            Controls.Add(CreateStatCard("Medicines in Stock", service.MedicinesInStock.ToString("N0") + " units", 260, y));
            Controls.Add(CreateStatCard("Active Orders", service.ActiveOrders.ToString(), 520, y));

            Resize += (s, e) =>
            {
                int cardWidth = Math.Max(200, (ClientSize.Width - 48) / 3);
                int idx = 0;
                foreach (Control c in Controls)
                {
                    if (!(c is Panel card)) continue;
                    card.SetBounds(idx * (cardWidth + 16), 40, cardWidth, 100);
                    idx++;
                }
            };
        }

        private Panel CreateStatCard(string title, string value, int x, int y)
        {
            var card = new Panel
            {
                Location = new Point(x, y),
                Size = new Size(240, 100),
                BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest,
                Padding = new Padding(16)
            };
            card.Paint += (s, e) =>
            {
                var rect = card.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                    e.Graphics.DrawRectangle(pen, rect);
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = ClinicalPrecisionTheme.LabelFont,
                ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant,
                Location = new Point(16, 16),
                AutoSize = true
            };
            var lblValue = new Label
            {
                Text = value,
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                Location = new Point(16, 44),
                AutoSize = true
            };
            card.Controls.Add(lblTitle);
            card.Controls.Add(lblValue);
            return card;
        }
    }
}
