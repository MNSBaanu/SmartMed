using System;
using System.Windows.Forms;
using SmartMedNew.Services;

namespace SmartMedNew.UI
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
            Session.Clear();
            _handingOffToLogin = true;

            var previous = MainForm;
            if (previous != null)
                previous.FormClosed -= OnMainFormClosed;

            ShowLogin(isAfterLogout: true);
            previous?.Close();
            _handingOffToLogin = false;
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

        private void OnLoginSucceeded(object sender, EventArgs e)
        {
            if (!(sender is LoginForm login))
                return;

            login.LoginSucceeded -= OnLoginSucceeded;

            if (Session.IsAdminLoggedIn)
            {
                login.Hide();
                var host = new AdminHostForm();
                host.FormClosed += OnMainFormClosed;
                MainForm = host;
                host.Show();
                return;
            }

            var displayName = Session.CurrentCustomer?.Name;
            MessageBox.Show(
                $"Welcome, {displayName}!\n\nSigned in as Customer.\nHost dashboards will be added next.",
                "SmartMed Clinical",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            login.FormClosed -= OnLoginFormClosed;
            ExitThread();
        }

        private void OnLoginFormClosed(object sender, FormClosedEventArgs e)
        {
            var closed = sender as LoginForm;
            if (closed != null)
            {
                closed.FormClosed -= OnLoginFormClosed;
                if (MainForm == closed)
                    ExitThread();
            }
        }

        private void OnMainFormClosed(object sender, FormClosedEventArgs e)
        {
            var form = sender as Form;
            if (form != null)
                form.FormClosed -= OnMainFormClosed;

            if (!_handingOffToLogin)
                ExitThread();
        }
    }
}
