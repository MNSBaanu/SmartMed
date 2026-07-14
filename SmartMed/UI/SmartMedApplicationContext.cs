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
            CartService.Unload();
            Session.Clear();
            _handingOffToLogin = true;

            var previous = MainForm;
            if (previous != null)
                previous.FormClosed -= OnMainFormClosed;

            ShowLogin(isAfterLogout: true, source: previous);
            previous?.Close();
            _handingOffToLogin = false;
        }

        private void ShowLogin(bool isAfterLogout, Form source = null)
        {
            var login = new LoginForm();
            if (isAfterLogout)
                login.ResetAfterLogout();

            CopyWindowStateAndSize(source, login);

            login.LoginSucceeded += OnLoginSucceeded;
            login.FormClosed += OnLoginFormClosed;
            MainForm = login;
            login.Show();
        }

        private static void CopyWindowStateAndSize(Form source, Form target)
        {
            if (source == null || target == null)
                return;

            target.StartPosition = FormStartPosition.Manual;

            if (source.WindowState == FormWindowState.Maximized)
            {
                var restore = source.RestoreBounds;
                if (restore.Width > 0 && restore.Height > 0)
                {
                    target.Location = restore.Location;
                    target.Size = restore.Size;
                }
                target.WindowState = FormWindowState.Maximized;
            }
            else if (source.WindowState == FormWindowState.Normal)
            {
                target.WindowState = FormWindowState.Normal;
                target.Location = source.Location;
                target.Size = source.Size;
            }
        }

        private void OnLoginSucceeded(object sender, EventArgs e)
        {
            if (!(sender is LoginForm login))
                return;

            login.LoginSucceeded -= OnLoginSucceeded;
            login.FormClosed -= OnLoginFormClosed;

            if (Session.IsAdminLoggedIn)
            {
                var adminHost = new AdminHostForm();
                CopyWindowStateAndSize(login, adminHost);
                adminHost.FormClosed += OnMainFormClosed;
                MainForm = adminHost;
                adminHost.Show();
                login.Close();
                return;
            }

            var customerHost = new CustomerHostForm();
            CopyWindowStateAndSize(login, customerHost);
            customerHost.FormClosed += OnMainFormClosed;
            MainForm = customerHost;
            customerHost.Show();
            login.Close();
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
