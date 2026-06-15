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
            UiFactory.ApplyFormDefaults(this);
            if (!UiFactory.IsDesignMode(this))
                UiFactory.ApplyFullScreen(this);
        }

        private void CustomerDashboardForm_Load(object sender, EventArgs e)
        {
            if (UiFactory.IsDesignMode(this)) return;

            var welcome = Session.CurrentCustomer?.Name ?? "Customer";
            lblWelcome.Text = $"Welcome, {welcome}";
            lblHeaderSubtitle.Text = $"Customer Portal  |  {welcome}";
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
            UiFactory.NavigateTo(contentHost, view, active, btnSearch, btnPlace, btnTrack, btnProfile);
        }
    }
}
