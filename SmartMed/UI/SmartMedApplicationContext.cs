using System;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed class SmartMedApplicationContext : ApplicationContext
    {
        private static SmartMedApplicationContext _current;
        private bool _handingOffToLogin;

        public static SmartMedApplicationContext Current => _current;

        public SmartMedApplicationContext()
        {
            _current = this;
            ShowLogin(isAfterLogout: false);
        }

        public void ShowLoginAfterLogout()
        {
            CartService.Clear();
            Session.Clear();
            _handingOffToLogin = true;

            var dashboard = MainForm;
            if (dashboard != null)
                dashboard.FormClosed -= OnDashboardFormClosed;

            ShowLogin(isAfterLogout: true);
            dashboard?.Close();
            _handingOffToLogin = false;
        }

        public void HandoffMainForm(Form next)
        {
            var previous = MainForm;
            if (previous != null && previous != next)
                previous.FormClosed -= OnDashboardFormClosed;

            next.FormClosed += OnDashboardFormClosed;
            MainForm = next;
        }

        private void ShowLogin(bool isAfterLogout)
        {
            var login = new LoginForm();
            if (isAfterLogout)
                login.ResetAfterLogout();

            login.LoginSucceeded += OnLoginSucceeded;
            login.FormClosed += OnLoginFormClosed;
            MainForm = login;
            login.Show();
        }

        private void OnLoginSucceeded(object sender, Form dashboard)
        {
            if (sender is LoginForm login)
                login.LoginSucceeded -= OnLoginSucceeded;

            dashboard.FormClosed += OnDashboardFormClosed;
            MainForm = dashboard;
            UiTheme.RevealForm(dashboard);
        }

        private void OnLoginFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is LoginForm login)
                login.FormClosed -= OnLoginFormClosed;
        }

        private void OnDashboardFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is Form dashboard)
                dashboard.FormClosed -= OnDashboardFormClosed;

            if (!_handingOffToLogin)
                ExitThread();
        }
    }
}
