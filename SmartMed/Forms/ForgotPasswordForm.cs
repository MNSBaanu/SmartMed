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
            AuthFormView.StyleFieldLabels(lblIdentity, lblCurrent, lblNew, lblConfirm);
            AuthFormView.ApplySoftFieldSurfaces(txtIdentity, txtCurrent, txtNew, txtConfirm);
            UiTheme.ApplyFlatButton(btnSave, UiButtonStyle.Primary);
            UiTheme.ApplyFlatButton(btnCancel, UiButtonStyle.Secondary);
        }

        private void WireRuntimeBehavior()
        {
            AuthFormView.ApplySoftFieldSurfaces(txtIdentity, txtCurrent, txtNew, txtConfirm);
            UiTheme.WireClinicalPlaceholderTextBox(txtIdentity, "Email or username");
            UiTheme.WireClinicalPasswordTextBox(txtCurrent);
            UiTheme.WireClinicalPasswordTextBox(txtNew);
            UiTheme.WireClinicalPasswordTextBox(txtConfirm);
            UiTheme.AddPasswordToggleBeside(txtCurrent);
            UiTheme.AddPasswordToggleBeside(txtNew);
            UiTheme.AddPasswordToggleBeside(txtConfirm);
            UiTheme.EnableFieldNavigation(btnSave, txtIdentity, txtCurrent, txtNew, txtConfirm);
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

        private void BtnClose_Click(object sender, EventArgs e) => BtnCancel_Click(sender, e);

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var identity = UiTheme.ReadTextBoxValue(txtIdentity);
                var currentPassword = txtCurrent.Text.Trim();
                var newPassword = txtNew.Text.Trim();
                var confirm = txtConfirm.Text.Trim();

                if (newPassword != confirm)
                {
                    SmartMedMessageBox.Show("New passwords do not match.", Text,
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                new AuthService().ResetPassword(identity, currentPassword, newPassword);
                SmartMedMessageBox.Show(
                    "Password reset successfully. You can sign in with your new password.",
                    Text,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (ArgumentException ex)
            {
                SmartMedMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                SmartMedMessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show("Unable to reset password.\n" + ex.Message, Text,
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
