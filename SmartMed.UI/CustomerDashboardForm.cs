using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.UI.Theming;
using SmartMed.UI.Views;

namespace SmartMed.UI
{
    public class CustomerDashboardForm : Form
    {
        private Panel contentHost;
        private Button btnSearch, btnPlace, btnTrack, btnProfile;

        public CustomerDashboardForm()
        {
            UiFactory.ApplyFormDefaults(this);
            Text = "SmartMed - Customer Portal";
            Size = new Size(1100, 700);
            StartPosition = FormStartPosition.CenterScreen;
            MinimumSize = new Size(900, 600);

            var welcome = Session.CurrentCustomer?.Name ?? "Customer";
            var header = UiFactory.CreateAppHeader("SmartMed Pharmacy", "Customer Portal  |  " + welcome, Logout);
            Controls.Add(header);

            var main = new Panel { Dock = DockStyle.Fill };
            Controls.Add(main);

            var sidebar = UiFactory.CreateSidebar();
            main.Controls.Add(sidebar);

            var lblWelcome = new Label
            {
                Text = $"Welcome, {welcome}",
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.StackMd),
                Width = ClinicalPrecisionTheme.NavWidth - ClinicalPrecisionTheme.ContainerPadding * 2
            };
            sidebar.Controls.Add(lblWelcome);

            int navY = 50;
            btnSearch = UiFactory.CreateNavButton("Search Medicines", null);
            btnPlace = UiFactory.CreateNavButton("Place Order", null);
            btnTrack = UiFactory.CreateNavButton("Track Orders", null);
            btnProfile = UiFactory.CreateNavButton("Manage Profile", null);

            btnSearch.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnPlace.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnTrack.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);
            navY += ClinicalPrecisionTheme.NavItemHeight + ClinicalPrecisionTheme.StackSm;
            btnProfile.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, navY);

            btnSearch.Click += (s, e) => Navigate(new SearchMedicinesView(), btnSearch);
            btnPlace.Click += (s, e) => Navigate(new PlaceOrderView(), btnPlace);
            btnTrack.Click += (s, e) => Navigate(new TrackOrdersView(), btnTrack);
            btnProfile.Click += (s, e) => Navigate(new ProfileManagementView(), btnProfile);

            sidebar.Controls.AddRange(new Control[] { btnSearch, btnPlace, btnTrack, btnProfile });

            contentHost = UiFactory.CreateContentHost();
            main.Controls.Add(contentHost);

            Navigate(new SearchMedicinesView(), btnSearch);
        }

        private void Navigate(UserControl view, Button active)
        {
            UiFactory.NavigateTo(contentHost, view, active, btnSearch, btnPlace, btnTrack, btnProfile);
        }

        private void Logout()
        {
            Session.Clear();
            Close();
            new LoginForm().Show();
        }
    }
}
