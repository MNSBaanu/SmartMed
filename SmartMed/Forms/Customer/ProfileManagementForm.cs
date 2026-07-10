using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ProfileManagementForm : EmbeddedPageForm
    {
        private CustomerService _customers;
        private HealthServiceService _health;
        private bool _servicesReady;
        private bool _runtimeWired;
        private bool _chromeApplied;
        private bool _historyBuilt;
        private DataGridView _gridServiceHistory;
        private Label _lblServiceHistoryTitle;

        public ProfileManagementForm()
        {
            InitializeComponent();
            if (!IsDesignHost())
            {
                _customers = new CustomerService();
                _health = new HealthServiceService();
                _servicesReady = true;
            }
        }

        protected override bool PreferDesignTimePreview() => !_servicesReady || IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
            if (_servicesReady)
                WireRuntimeBehavior();
        }

        protected override void DoRefreshPage() => LoadProfile();

        protected override void LoadDesignTimePreview()
        {
            ApplyViewChrome();

            var customer = DesignTimePreviewData.SampleCustomer();
            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
            BindServiceHistoryPreview();
        }

        private void ApplyViewChrome()
        {
            if (_chromeApplied) return;
            _chromeApplied = true;

            AdminPageView.EnsureTheme();
            AdminPageView.ApplyChrome(this);

            UiTheme.StyleTextBox(txtName);
            UiTheme.StyleTextBox(txtEmail);
            UiTheme.StyleTextBox(txtPhone);
            UiTheme.StyleTextBox(txtAddress);

            WirePanelBorder(panelFormOuter);
            EnsureServiceHistorySection();
        }

        private static void WirePanelBorder(Panel panel)
        {
            if (panel == null || panel.Tag as string == "dash-border") return;
            panel.Tag = "dash-border";
            panel.Paint += (s, e) =>
            {
                var rect = panel.ClientRectangle;
                rect.Width -= 1;
                rect.Height -= 1;
                using (var pen = new Pen(UiTheme.AdminOutline))
                    e.Graphics.DrawRectangle(pen, rect);
            };
        }

        private void WireRuntimeBehavior()
        {
            if (_runtimeWired) return;
            _runtimeWired = true;

            btnSaveProfile.Click += BtnSave_Click;
            btnChangePassword.Click += (s, e) => ShowChangePasswordDialog();

            UiTheme.EnableFieldNavigation(btnSaveProfile, txtName, txtEmail, txtPhone, txtAddress);
        }

        private void LoadProfile()
        {
            if (!_servicesReady) return;

            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            txtName.Text = customer.Name;
            txtEmail.Text = customer.Email;
            txtPhone.Text = customer.Phone;
            txtAddress.Text = customer.Address;
            LoadServiceHistory();
        }

        private void EnsureServiceHistorySection()
        {
            if (_historyBuilt) return;
            _historyBuilt = true;

            _lblServiceHistoryTitle = new Label
            {
                Text = "MY HEALTH SERVICE HISTORY",
                Dock = DockStyle.Fill,
                Font = UiTheme.UiFontBold,
                ForeColor = UiTheme.AdminMuted,
                Margin = new Padding(0, 16, 0, 4),
                AutoSize = true
            };

            var panelHistoryOuter = new Panel
            {
                Dock = DockStyle.Fill,
                Height = 220,
                BackColor = Color.White,
                Margin = new Padding(0, 0, 0, 8),
                Padding = new Padding(1)
            };
            WirePanelBorder(panelHistoryOuter);

            _gridServiceHistory = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            UiTheme.ApplyClinicalGrid(_gridServiceHistory);
            panelHistoryOuter.Controls.Add(_gridServiceHistory);

            tableLayoutRoot.RowCount = 6;
            tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 40F));
            tableLayoutRoot.Controls.Add(_lblServiceHistoryTitle, 0, 4);
            tableLayoutRoot.Controls.Add(panelHistoryOuter, 0, 5);
        }

        private void BindServiceHistoryPreview()
        {
            if (_gridServiceHistory == null) return;
            UiTheme.SetGridDataSource(_gridServiceHistory, new[]
            {
                new { ServiceDate = DateTime.Today.AddDays(-5).ToString("yyyy-MM-dd"), Service = "Blood Pressure Check", Result = "120/80 mmHg", Notes = "Within normal range." }
            });
            UiTheme.BeautifyGridHeaders(_gridServiceHistory);
        }

        private void LoadServiceHistory()
        {
            if (!_servicesReady || _gridServiceHistory == null) return;

            var customer = Session.CurrentCustomer;
            if (customer == null) return;

            var rows = _health.GetCustomerRecords(customer.CustomerID)
                .Select(r => new
                {
                    ServiceDate = r.ServiceDate.ToString("yyyy-MM-dd"),
                    Service = r.ServiceName,
                    r.Result,
                    Notes = string.IsNullOrWhiteSpace(r.PharmacistNotes) ? "—" : r.PharmacistNotes
                }).ToList();

            UiTheme.SetGridDataSource(_gridServiceHistory, rows);
            UiTheme.BeautifyGridHeaders(_gridServiceHistory);
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!_servicesReady) return;

            try
            {
                var customer = Session.CurrentCustomer;
                if (customer == null)
                    throw new InvalidOperationException("Please log in again.");

                var updated = new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = txtName.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Password = customer.Password
                };
                _customers.UpdateProfile(updated);
                Session.CurrentCustomer = _customers.GetById(customer.CustomerID);
                (FindForm() as CustomerHostForm)?.RefreshProfileDisplay();

                SmartMedMessageBox.Show("Profile updated.", "Profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Profile", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowChangePasswordDialog()
        {
            if (!_servicesReady) return;

            using (var dlg = new ChangePasswordForm(isAdmin: false))
                dlg.ShowDialog(FindForm());
        }
    }
}
