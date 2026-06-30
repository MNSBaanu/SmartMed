using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ChangePasswordForm : Form
    {
        private readonly bool _isAdmin;

        public ChangePasswordForm() : this(isAdmin: true)
        {
        }

        public ChangePasswordForm(bool isAdmin)
        {
            _isAdmin = isAdmin;
            InitializeComponent();
            DoubleBuffered = true;
            ApplyViewChrome();
            if (!DesignHostHelper.IsDesignHost(this))
                WireRuntimeBehavior();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
        }

        private void ApplyViewChrome()
        {
            AuthFormView.ApplyCardBorder(panelCard);
            UiTheme.ApplyFlatButton(btnSave, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnCancel, UiButtonStyle.Secondary);

            lblPageSubtitle.Text = _isAdmin
                ? "Update your administrator credentials for the clinical management portal."
                : "Enter your current password and choose a new secure password for your health portal account.";
            lblCardTitle.Text = _isAdmin
                ? "SmartMed \u2014 Administrator Security"
                : "SmartMed \u2014 Account Security";
        }

        private void WireRuntimeBehavior()
        {
            UiTheme.WireClinicalPasswordTextBox(txtCurrent);
            UiTheme.WireClinicalPasswordTextBox(txtNew);
            UiTheme.WireClinicalPasswordTextBox(txtConfirm);
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelMain.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            using (var brush = new LinearGradientBrush(rect, Color.White, UiTheme.AdminSurface, 45f))
                e.Graphics.FillRectangle(brush, rect);
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNew.Text != txtConfirm.Text)
                {
                    MessageBox.Show("New passwords do not match.", Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var auth = new AuthService();
                var customers = new CustomerService();

                if (_isAdmin)
                {
                    if (Session.CurrentAdmin == null)
                        throw new InvalidOperationException("Admin session expired.");
                    auth.ChangeAdminPassword(Session.CurrentAdmin.AdminID, txtCurrent.Text, txtNew.Text);
                    Session.CurrentAdmin.Password = txtNew.Text;
                }
                else
                {
                    if (Session.CurrentCustomer == null)
                        throw new InvalidOperationException("Customer session expired.");
                    customers.ChangePassword(Session.CurrentCustomer.CustomerID, txtCurrent.Text, txtNew.Text);
                    Session.CurrentCustomer.Password = txtNew.Text;
                }

                MessageBox.Show("Password updated successfully.", Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
