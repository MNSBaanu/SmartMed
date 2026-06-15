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
            ApplyStitchShell();
            UiFactory.ApplyFormDefaults(this);
            if (!UiFactory.IsDesignMode(this))
                UiFactory.ApplyFullScreen(this);
        }

        private void ApplyStitchShell()
        {
            StitchUiHelper.ApplyCustomerShell(this, headerPanel, lblHeaderTitle, txtGlobalSearch,
                sidebarPanel, lblBrandTitle, lblBrandSubtitle, btnLogout);

            StitchUiHelper.StyleNavButton(btnSearch, StitchUiHelper.NavIcon.Search, "Search Medicines", true, true);
            StitchUiHelper.StyleNavButton(btnPlace, StitchUiHelper.NavIcon.Cart, "Place Order", false, true);
            StitchUiHelper.StyleNavButton(btnTrack, StitchUiHelper.NavIcon.Track, "Track Orders", false, true);
            StitchUiHelper.StyleNavButton(btnProfile, StitchUiHelper.NavIcon.Profile, "Manage Profile", false, true);

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
