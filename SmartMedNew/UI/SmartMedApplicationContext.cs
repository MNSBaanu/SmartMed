using System;
using System.Windows.Forms;
using SmartMedNew.Services;

namespace SmartMedNew.UI
{
    public sealed class SmartMedApplicationContext : ApplicationContext
    {
        public SmartMedApplicationContext()
        {
            var login = new LoginForm();
            login.LoginSucceeded += OnLoginSucceeded;
            login.FormClosed += OnLoginFormClosed;
            MainForm = login;
            login.Show();
        }

        private void OnLoginSucceeded(object sender, EventArgs e)
        {
            if (sender is LoginForm login)
                login.LoginSucceeded -= OnLoginSucceeded;

            var displayName = Session.IsAdminLoggedIn
                ? Session.CurrentAdmin?.Username
                : Session.CurrentCustomer?.Name;
            var role = Session.IsAdminLoggedIn ? "Administrator" : "Customer";

            MessageBox.Show(
                $"Welcome, {displayName}!\n\nSigned in as {role}.\nHost dashboards will be added next.",
                "SmartMed Clinical",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            ExitThread();
        }

        private void OnLoginFormClosed(object sender, FormClosedEventArgs e)
        {
            if (sender is LoginForm closed)
            {
                closed.FormClosed -= OnLoginFormClosed;
                if (MainForm == closed)
                    ExitThread();
            }
        }
    }
}
