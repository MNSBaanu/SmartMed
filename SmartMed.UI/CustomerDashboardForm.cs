using System;
using System.Windows.Forms;
using SmartMed.Business;
using SmartMed.UI.Theming;
using SmartMed.UI.Views;

namespace SmartMed.UI
{
    public partial class CustomerDashboardForm : Form
    {
        public CustomerDashboardForm()
        {
            InitializeComponent();
            ApplyDashboardShell();
            UiFactory.ApplyFormDefaults(this);
            if (!UiFactory.IsDesignMode(this))
                UiFactory.ApplyFullScreen(this);
        }

        private void ApplyDashboardShell()
        {
            ClinicalPrecisionUiHelper.ApplyCustomerShell(this, headerPanel, lblHeaderTitle, txtGlobalSearch,
                sidebarPanel, lblBrandTitle, lblBrandSubtitle, btnLogout);

            ClinicalPrecisionUiHelper.StyleNavButton(btnSearch, ClinicalPrecisionUiHelper.NavIcon.Search, "Search Medicines", true, true);
            ClinicalPrecisionUiHelper.StyleNavButton(btnPlace, ClinicalPrecisionUiHelper.NavIcon.Cart, "Place Order", false, true);
            ClinicalPrecisionUiHelper.StyleNavButton(btnTrack, ClinicalPrecisionUiHelper.NavIcon.Track, "Track Orders", false, true);
            ClinicalPrecisionUiHelper.StyleNavButton(btnProfile, ClinicalPrecisionUiHelper.NavIcon.Profile, "Manage Profile", false, true);

            btnSearch.Location = new System.Drawing.Point(24, 88);
            btnPlace.Location = new System.Drawing.Point(24, 132);
            btnTrack.Location = new System.Drawing.Point(24, 176);
            btnProfile.Location = new System.Drawing.Point(24, 220);
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {
            if (UiFactory.IsDesignMode(this)) return;
            Navigate(new SearchMedicinesView(), btnSearch);
        }

        private void BtnSearch_Click(object sender, EventArgs e) => Navigate(new SearchMedicinesView(), btnSearch);
        private void BtnPlace_Click(object sender, EventArgs e) => Navigate(new PlaceOrderView(), btnPlace);
        private void BtnTrack_Click(object sender, EventArgs e) => Navigate(new TrackOrdersView(), btnTrack);
        private void BtnProfile_Click(object sender, EventArgs e) => Navigate(new ProfileManagementView(), btnProfile);

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Close();
            new LoginForm().Show();
        }

        private void Navigate(UserControl view, Button active)
        {
            UiFactory.NavigateTo(contentHost, view, active, true, btnSearch, btnPlace, btnTrack, btnProfile);
        }
    }
}

