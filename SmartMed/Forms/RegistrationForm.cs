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
        private readonly AuthService _auth = new AuthService();

        public RegistrationForm()
        {
            InitializeComponent();
            DoubleBuffered = true;
            ApplyChrome();
        }

        private void ApplyChrome()
        {
            UiTheme.ApplyRegistrationForm(this, panelMain, panelRegisterCard);
            UiTheme.ApplyRegistrationCardHeader(panelCardHeader, lblCardTitle, btnClose);

            pnlHeaderIcon.BackColor = UiTheme.PrimaryContainer;
            lblHeaderIcon.ForeColor = Color.White;
            lblHeaderIcon.BackColor = UiTheme.PrimaryContainer;
            lblHeaderIcon.Font = UiTheme.FontAt(7F, bold: true);

            lblPageTitle.Font = UiTheme.FontAt(18F, semibold: true);
            lblPageTitle.ForeColor = UiTheme.PrimaryDark;
            lblPageTitle.BackColor = Color.White;
            lblPageSubtitle.ForeColor = UiTheme.AdminLabelText;
            lblPageSubtitle.BackColor = Color.White;
            lblPageSubtitle.Font = UiTheme.UiFont;

            UiTheme.StyleRegistrationFieldLabel(lblFullName);
            UiTheme.StyleRegistrationFieldLabel(lblEmail);
            UiTheme.StyleRegistrationFieldLabel(lblPhone);
            UiTheme.StyleRegistrationFieldLabel(lblAddress);
            UiTheme.StyleRegistrationFieldLabel(lblPassword);
            UiTheme.StyleRegistrationFieldLabel(lblConfirm);

            UiTheme.StyleClinicalTextBox(txtFullName, "Dr. Jane Smith");
            UiTheme.StyleClinicalTextBox(txtEmail, "jane@hospital.com");
            UiTheme.StyleClinicalTextBox(txtPhone, "0771234567");
            UiTheme.StyleClinicalTextBox(txtAddress, "Enter your home address");
            UiTheme.StyleTextBox(txtPassword);
            UiTheme.StylePasswordBox(txtPassword, masked: true);
            UiTheme.StyleTextBox(txtConfirm);
            UiTheme.StylePasswordBox(txtConfirm, masked: true);

            chkTerms.Font = UiTheme.UiFont;
            chkTerms.ForeColor = UiTheme.AdminLabelText;
            chkTerms.BackColor = Color.White;

            UiTheme.ApplyRegisterButton(btnRegister);
            lnkBackLogin.BackColor = Color.White;
            UiTheme.StyleLinkButton(lnkBackLogin);

            UiTheme.StyleRegistrationBrandFooter(panelBrandFooter, lblBrandTitle, lblBrandSubtitle, pnlBrandIcon);
            lblBrandIcon.Font = UiTheme.FontAt(16F, semibold: true);

            panelStatusBar.BackColor = UiTheme.FooterBackground;
            lblStatusLeft.ForeColor = UiTheme.FooterText;
            lblStatusLeft.BackColor = UiTheme.FooterBackground;
            lblStatusLeft.Font = UiTheme.FontAt(8.25F);
            lblStatusCenter.ForeColor = UiTheme.FooterText;
            lblStatusCenter.BackColor = UiTheme.FooterBackground;
            lblStatusCenter.Font = UiTheme.FontAt(8.25F);
            lblStatusRight.ForeColor = UiTheme.FooterText;
            lblStatusRight.BackColor = UiTheme.FooterBackground;
            lblStatusRight.Font = UiTheme.FontAt(8.25F);

            panelBody.BackColor = Color.White;
            panelSuccess.BackColor = Color.White;
            lblSuccessIcon.ForeColor = UiTheme.AdminTeal;
            lblSuccessIcon.Font = UiTheme.FontAt(20F, bold: true);
            lblSuccessTitle.ForeColor = UiTheme.AdminOnSurface;
            lblSuccessTitle.BackColor = Color.White;
            lblSuccessTitle.Font = UiTheme.FontAt(12F, bold: true);
            lblSuccessMessage.ForeColor = UiTheme.AdminLabelText;
            lblSuccessMessage.BackColor = Color.White;
            lblSuccessMessage.Font = UiTheme.UiFont;
            UiTheme.ApplyRegisterButton(btnReturnLogin);

            LayoutRegistrationContent();
        }

        private void PanelMain_Paint(object sender, PaintEventArgs e)
        {
            var rect = panelMain.ClientRectangle;
            if (rect.Width <= 0 || rect.Height <= 0) return;
            using (var brush = new LinearGradientBrush(rect, Color.White, UiTheme.AdminSurface, 45f))
                e.Graphics.FillRectangle(brush, rect);
        }

        private void PanelMain_Resize(object sender, EventArgs e) => LayoutRegistrationContent();

        private void LayoutRegistrationContent()
        {
            if (panelRegisterCard == null || panelMain == null) return;

            const int cardW = 520;
            const int cardH = 700;
            const int pad = 32;
            const int gap = 16;
            const int labelGap = 6;
            const int inputH = 40;
            const int headerH = 40;
            const int footerH = 96;
            const int fieldW = cardW - pad * 2;
            const int halfW = (fieldW - gap) / 2;

            var left = Math.Max(0, (panelMain.ClientSize.Width - cardW) / 2);
            var top = Math.Max(16, (panelMain.ClientSize.Height - cardH) / 2);
            panelRegisterCard.SetBounds(left, top, cardW, cardH);

            panelCardHeader.SetBounds(0, 0, cardW, headerH);
            pnlHeaderIcon.SetBounds(12, 10, 20, 20);
            lblCardTitle.SetBounds(36, 12, cardW - 80, 16);
            btnClose.SetBounds(cardW - 44, 6, 32, 28);

            panelBrandFooter.SetBounds(0, cardH - footerH, cardW, footerH);
            pnlBrandIcon.SetBounds((cardW - 180) / 2, 28, 40, 40);
            lblBrandTitle.SetBounds(pnlBrandIcon.Right + 8, 26, 140, 24);
            lblBrandSubtitle.SetBounds(pnlBrandIcon.Right + 8, 50, 180, 16);

            var bodyTop = headerH;
            var bodyH = cardH - headerH - footerH;
            panelBody.SetBounds(0, bodyTop, cardW, bodyH);
            panelSuccess.SetBounds(0, bodyTop, cardW, bodyH);

            int y = pad;
            lblPageTitle.SetBounds(pad, y, fieldW, 28);
            y += 28 + 4;
            lblPageSubtitle.SetBounds(pad, y, fieldW, 20);
            y += 20 + 24;

            lblFullName.SetBounds(pad, y, fieldW, 16);
            y += 16 + labelGap;
            txtFullName.SetBounds(pad, y, fieldW, inputH);
            y += inputH + gap;

            lblEmail.SetBounds(pad, y, halfW, 16);
            lblPhone.SetBounds(pad + halfW + gap, y, halfW, 16);
            y += 16 + labelGap;
            txtEmail.SetBounds(pad, y, halfW, inputH);
            txtPhone.SetBounds(pad + halfW + gap, y, halfW, inputH);
            y += inputH + gap;

            lblAddress.SetBounds(pad, y, fieldW, 16);
            y += 16 + labelGap;
            txtAddress.SetBounds(pad, y, fieldW, inputH);
            y += inputH + gap;

            lblPassword.SetBounds(pad, y, halfW, 16);
            lblConfirm.SetBounds(pad + halfW + gap, y, halfW, 16);
            y += 16 + labelGap;
            txtPassword.SetBounds(pad, y, halfW, inputH);
            txtConfirm.SetBounds(pad + halfW + gap, y, halfW, inputH);
            y += inputH + gap;

            chkTerms.SetBounds(pad, y, fieldW, 36);
            y += 40;

            btnRegister.SetBounds(pad, y, fieldW, 44);
            y += 44 + 16;

            lnkBackLogin.AutoSize = true;
            lnkBackLogin.Top = y;
            lnkBackLogin.Left = pad + Math.Max(0, (fieldW - lnkBackLogin.PreferredWidth) / 2);

            lblSuccessIcon.SetBounds(pad, 80, fieldW, 48);
            lblSuccessTitle.SetBounds(pad, 140, fieldW, 28);
            lblSuccessMessage.SetBounds(pad, 176, fieldW, 72);
            btnReturnLogin.SetBounds(pad, 268, fieldW, 44);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void LnkBackLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => BtnClose_Click(sender, e);

        private void BtnReturnLogin_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                if (!chkTerms.Checked)
                {
                    MessageBox.Show("Please accept the terms of service to continue.", "Registration",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var password = txtPassword.Text.Trim();
                var confirm = txtConfirm.Text.Trim();
                if (password != confirm)
                {
                    MessageBox.Show("Passwords do not match.", "Registration",
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
