using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartMed.Services;

namespace SmartMed.UI
{
    public sealed partial class ForgotPasswordForm : Form
    {
        public ForgotPasswordForm()
        {
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
        }

        private void WireRuntimeBehavior()
        {
            UiTheme.WireClinicalPlaceholderTextBox(txtIdentity, "Email or username");
            UiTheme.WireClinicalPasswordTextBox(txtNew);
            UiTheme.WireClinicalPasswordTextBox(txtConfirm);
            UiTheme.EnableFieldNavigation(btnSave, txtIdentity, txtNew, txtConfirm);
        }

        private void panelMain_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelMain.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            using (var brush = new LinearGradientBrush(rect, Color.White, UiTheme.AdminSurface, 45f))
                e.Graphics.FillRectangle(brush, rect);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnClose_Click(object sender, EventArgs e) => btnCancel_Click(sender, e);

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var identity = UiTheme.ReadTextBoxValue(txtIdentity);
                var newPassword = txtNew.Text.Trim();
                var confirm = txtConfirm.Text.Trim();

                if (newPassword != confirm)
                {
                    SmartMedMessageBox.Show("New passwords do not match.", Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                new AuthService().ResetPassword(identity, newPassword);
                SmartMedMessageBox.Show(
                    "Password reset successfully. You can sign in with your new password.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
