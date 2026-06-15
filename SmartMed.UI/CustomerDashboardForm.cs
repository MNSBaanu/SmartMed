using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    public class CustomerDashboardForm : Form
    {
        public CustomerDashboardForm()
        {
            Text = "SmartMed - Customer Portal";
            Size = new Size(500, 400);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;

            var lbl = new Label
            {
                Text = $"Welcome, {Business.Session.CurrentCustomer?.Name}",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(0, 50, 150),
                Location = new Point(30, 20),
                AutoSize = true
            };

            int y = 70;
            Controls.Add(lbl);
            Controls.Add(MakeNavButton("Search Medicines", 30, y, () => new SearchMedicinesForm().ShowDialog())); y += 50;
            Controls.Add(MakeNavButton("Place Order", 30, y, () => new PlaceOrderForm().ShowDialog())); y += 50;
            Controls.Add(MakeNavButton("Track Orders", 30, y, () => new TrackOrdersForm().ShowDialog())); y += 50;
            Controls.Add(MakeNavButton("Manage Profile", 30, y, () => new ProfileManagementForm().ShowDialog())); y += 50;

            var btnLogout = new Button { Text = "Logout", Location = new Point(30, y), Width = 420, Height = 35 };
            btnLogout.Click += (s, e) => { Business.Session.Clear(); Close(); new LoginForm().Show(); };
            Controls.Add(btnLogout);
        }

        private Button MakeNavButton(string text, int x, int y, Action action)
        {
            var btn = new Button { Text = text, Location = new Point(x, y), Width = 420, Height = 40, TextAlign = ContentAlignment.MiddleLeft, BackColor = Color.FromArgb(180, 203, 249) };
            btn.Click += (s, e) => action();
            return btn;
        }
    }
}
