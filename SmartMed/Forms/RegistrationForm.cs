using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SmartMed.Models;
using SmartMed.Services;

namespace SmartMed.UI
{
    public partial class RegistrationForm : Form
    {
        private const int ContentTopMargin = 24;

        private readonly AuthService _auth;

        public RegistrationForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyViewChrome();
            if (!DesignHostHelper.IsDesignHost(this))
            {
                _auth = new AuthService();
                WireRuntimeBehavior();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            ApplyViewChrome();
        }

        private void ApplyViewChrome()
        {
            AuthFormView.ApplyCardBorder(panelRegisterCard);
            LayoutRegistrationContent();
        }

        private void WireRuntimeBehavior()
        {
            UiTheme.WireClinicalPlaceholderTextBox(txtFullName, "Dr. Jane Smith");
            UiTheme.WireClinicalPlaceholderTextBox(txtEmail, "jane@hospital.com");
            UiTheme.WireClinicalPlaceholderTextBox(txtPhone, "0771234567");
            UiTheme.WireClinicalPlaceholderTextBox(txtAddress, "Enter your home address");
            UiTheme.WireClinicalPasswordTextBox(txtPassword);
            UiTheme.WireClinicalPasswordTextBox(txtConfirm);

            UiTheme.EnableFieldNavigation(btnRegister,
                txtFullName, txtEmail, txtPhone, txtAddress, txtPassword, txtConfirm);
        }

        private void LayoutRegistrationContent()
        {
            if (panelRegisterCard == null || panelMain == null) return;

            var left = Math.Max(0, (panelMain.ClientSize.Width - panelRegisterCard.Width) / 2);
            var top = Math.Max(ContentTopMargin, (panelMain.ClientSize.Height - panelRegisterCard.Height) / 2);

            panelRegisterCard.Left = left;
            panelRegisterCard.Top = top;
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelMain.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            using (var brush = new LinearGradientBrush(rect, Color.White, UiTheme.AdminSurface, 45f))
                e.Graphics.FillRectangle(brush, rect);
        }

        private void PanelMain_Resize(object sender, EventArgs e) => LayoutRegistrationContent();

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LnkBackLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) =>
            BtnClose_Click(sender, e);

        private void BtnReturnLogin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            if (_auth == null) return;

            try
            {
                if (!chkTerms.Checked)
                {
                    SmartMedMessageBox.Show("Please accept the terms of service to continue.", "Registration",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var password = txtPassword.Text.Trim();
                var confirm = txtConfirm.Text.Trim();
                if (password != confirm)
                {
                    SmartMedMessageBox.Show("Passwords do not match.", "Registration",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var customer = new Customer
                {
                    Name = UiTheme.ReadTextBoxValue(txtFullName),
                    Email = UiTheme.ReadTextBoxValue(txtEmail),
                    Phone = UiTheme.ReadTextBoxValue(txtPhone),
                    Address = UiTheme.ReadTextBoxValue(txtAddress),
                    Password = password
                };

                _auth.RegisterCustomer(customer);
                ShowSuccess();
            }
            catch (ArgumentException ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (InvalidOperationException ex)
            {
                SmartMedMessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                SmartMedMessageBox.Show("Unable to complete registration.\n" + ex.Message, "Registration Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ShowSuccess()
        {
            panelBody.Visible = false;
            panelBrandFooter.Visible = false;
            panelSuccess.Visible = true;
            panelSuccess.BringToFront();
        }
    }
}
