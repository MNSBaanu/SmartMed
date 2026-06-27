using System;
using System.Drawing;
using System.Windows.Forms;
using SmartMedNew.Services;
using SmartMedNew.UI;

namespace SmartMedNew.UI
{
    public sealed partial class AdminHostForm : Form
    {
        private AdminDashboardForm _dashboard;
        private AdminNavItem _activeNav = AdminNavItem.Overview;
        private readonly Timer _clockTimer = new Timer { Interval = 30000 };

        public AdminHostForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UiTheme.ApplyAdminWinFormsShell(
                this, panelTitleBar, panelMenuBar, panelSidebar, panelContent, panelStatusBar);
            ApplyProfile();
            ApplyWinControls();
            WireMenuBar();
            SetActiveNav(AdminNavItem.Overview);
            ShowDashboard();

            _clockTimer.Tick += (s, e) => UpdateStatusTime();
            _clockTimer.Start();
            UpdateStatusTime();
        }

        private void ApplyProfile()
        {
            var displayName = Session.CurrentAdmin?.Username ?? "Administrator";
            lblProfileName.Text = displayName;
            lblProfileRole.Text = "Administrator";

            panelAvatar.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (var brush = new SolidBrush(UiTheme.AdminTeal))
                    e.Graphics.FillEllipse(brush, 0, 0, panelAvatar.Width - 1, panelAvatar.Height - 1);
                var initial = displayName.Length > 0 ? displayName.Substring(0, 1).ToUpperInvariant() : "A";
                TextRenderer.DrawText(e.Graphics, initial, UiTheme.UiFontBold, panelAvatar.ClientRectangle,
                    Color.White, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            };
        }

        private void ApplyWinControls()
        {
            foreach (Control c in panelWinControls.Controls)
            {
                if (c is Button btn)
                {
                    btn.FlatAppearance.BorderSize = 0;
                    btn.BackColor = Color.White;
                    btn.ForeColor = UiTheme.AdminMuted;
                    btn.Font = UiTheme.UiFont;
                    btn.Cursor = Cursors.Hand;
                    btn.UseVisualStyleBackColor = false;
                    btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(232, 239, 238);
                    if (btn == btnWinClose)
                        btn.FlatAppearance.MouseOverBackColor = UiTheme.Danger;
                }
            }
        }

        private void WireMenuBar()
        {
            foreach (Control c in panelMenuBar.Controls)
            {
                if (c is Label lbl)
                    lbl.Click += (s, e) => ShowComingSoon();
            }
        }

        private void ShowDashboard()
        {
            panelContent.Controls.Clear();
            panelContent.AutoScrollPosition = new Point(0, 0);

            if (_dashboard == null || _dashboard.IsDisposed)
                _dashboard = new AdminDashboardForm();

            _dashboard.Dock = DockStyle.Fill;
            panelContent.Controls.Add(_dashboard);
            _dashboard.RefreshData();
        }

        internal void SetActiveNav(AdminNavItem nav)
        {
            _activeNav = nav;
            StyleNavButton(btnNavDashboard, nav == AdminNavItem.Overview);
            StyleNavButton(btnNavMedicines, nav == AdminNavItem.Medicines);
            StyleNavButton(btnNavCustomers, nav == AdminNavItem.Customers);
            StyleNavButton(btnNavOrders, nav == AdminNavItem.Orders);
            StyleNavButton(btnNavReports, nav == AdminNavItem.Reports);
            StyleNavButton(btnNavConfig, false);
            StyleNavButton(btnNavAccess, false);
        }

        private static void StyleNavButton(Button button, bool active)
        {
            if (button == null) return;
            UiTheme.StyleWinFormsNavButton(button, active);
        }

        private void UpdateStatusTime()
        {
            lblStatusTime.Text = $"Local Time: {DateTime.Now:HH:mm}";
        }

        private void BtnNavDashboard_Click(object sender, EventArgs e)
        {
            SetActiveNav(AdminNavItem.Overview);
            ShowDashboard();
        }

        private void BtnNavComingSoon_Click(object sender, EventArgs e)
        {
            ShowComingSoon();
        }

        private static void ShowComingSoon()
        {
            MessageBox.Show(
                "This section is coming soon.",
                "SmartMed",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void BtnNavExit_Click(object sender, EventArgs e) => Logout();

        private void BtnWinMinimize_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void BtnWinMaximize_Click(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized
                ? FormWindowState.Normal
                : FormWindowState.Maximized;
        }

        private void BtnWinClose_Click(object sender, EventArgs e) => Logout();

        private void Logout()
        {
            _clockTimer.Stop();
            SmartMedApplicationContext.Current?.ShowLoginAfterLogout();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _clockTimer.Dispose();
            _dashboard?.Dispose();
            base.OnFormClosed(e);
        }
    }
}
