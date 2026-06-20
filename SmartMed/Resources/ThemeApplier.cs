using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.Resources
{
    internal static class ThemeApplier
    {
        private static bool IsDesignTime =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime;

        /// <summary>Sets BackColor using a designer-safe opaque color.</summary>
        public static void SetBackColor(Control control, Color color)
        {
            control.BackColor = AppTheme.EnsureOpaque(color);
        }

        public static void ApplyForm(Form form)
        {
            form.BackColor = AppTheme.Surface;
            form.Font = AppTheme.BodyFont;
        }

        public static void ApplyFieldLabel(Label label)
        {
            label.Font = AppTheme.LabelFont;
            label.ForeColor = AppTheme.OnSurfaceVariant;
        }

        public static void ApplyTextBox(TextBox textBox)
        {
            textBox.Font = AppTheme.BodyFont;
            textBox.ForeColor = AppTheme.OnSurface;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            SetBackColor(textBox, AppTheme.SurfaceContainerLowest);
        }

        public static void ApplyTextBoxOnPrimary(TextBox textBox)
        {
            ApplyTextBox(textBox);
            SetBackColor(textBox, AppTheme.OnPrimaryField);
        }

        public static void ApplyComboBox(ComboBox comboBox)
        {
            comboBox.Font = AppTheme.BodyFont;
            comboBox.ForeColor = AppTheme.OnSurface;
            comboBox.BackColor = AppTheme.SurfaceContainerLowest;
            comboBox.FlatStyle = FlatStyle.Standard;
        }

        public static void ApplyPrimaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = AppTheme.SecondaryContainer;
            button.ForeColor = AppTheme.OnSecondaryContainer;
            button.Font = AppTheme.LabelFont;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplySecondaryButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = AppTheme.OutlineVariant;
            button.FlatAppearance.BorderSize = 1;
            button.BackColor = AppTheme.SurfaceContainer;
            button.ForeColor = AppTheme.OnSurface;
            button.Font = AppTheme.LabelFont;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyLink(LinkLabel link)
        {
            link.Font = AppTheme.LinkFont;
            link.LinkColor = AppTheme.Primary;
            link.ActiveLinkColor = AppTheme.Primary;
            link.VisitedLinkColor = AppTheme.Primary;
        }

        public static void ApplyVersionLabel(Label label)
        {
            label.Font = AppTheme.VersionFont;
            label.ForeColor = AppTheme.Outline;
        }

        public static void ApplyIconButton(Button button, Color foreColor)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            SetBackColor(button, AppTheme.SurfaceContainerLowest);
            button.ForeColor = foreColor;
            button.Font = AppTheme.IconFontSmall;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyHeaderCloseButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            SetBackColor(button, AppTheme.Primary);
            button.ForeColor = AppTheme.OnPrimaryMuted;
            button.Font = AppTheme.IconFont;
            button.Cursor = Cursors.Hand;
            if (!IsDesignTime)
                button.FlatAppearance.MouseOverBackColor = AppTheme.HeaderHover;
        }

        public static void ApplyLoginCard(Panel card, Panel header, Panel body)
        {
            card.BackColor = AppTheme.SurfaceContainerLowest;
            header.BackColor = AppTheme.Primary;
            body.BackColor = AppTheme.SurfaceContainerLowest;
        }

        public static void ApplyLoginHeader(Label medicalIcon, Label title, Label lockIcon)
        {
            medicalIcon.Font = AppTheme.IconFont;
            medicalIcon.ForeColor = AppTheme.OnPrimary;
            title.Font = AppTheme.AppTitleFont;
            title.ForeColor = AppTheme.OnPrimary;
            lockIcon.Font = AppTheme.IconFont;
            lockIcon.ForeColor = AppTheme.OnPrimaryFaint;
        }

        public static void ApplyLoginForm(
            Form form,
            Panel card,
            Panel header,
            Panel body,
            Label medicalIcon,
            Label title,
            Label lockIcon,
            Button closeButton,
            Label roleLabel,
            ComboBox roleCombo,
            Label usernameLabel,
            TextBox usernameBox,
            Label passwordLabel,
            TextBox passwordBox,
            Button togglePassword,
            Button loginButton,
            Button registerButton,
            LinkLabel forgotLink,
            Label versionLabel)
        {
            ApplyForm(form);
            ApplyLoginCard(card, header, body);
            ApplyLoginHeader(medicalIcon, title, lockIcon);
            ApplyHeaderCloseButton(closeButton);
            ApplyFieldLabel(roleLabel);
            ApplyFieldLabel(usernameLabel);
            ApplyFieldLabel(passwordLabel);
            ApplyComboBox(roleCombo);
            ApplyTextBox(usernameBox);
            ApplyTextBox(passwordBox);
            ApplyIconButton(togglePassword, AppTheme.OnSurfaceVariant);
            ApplyPrimaryButton(loginButton);
            ApplySecondaryButton(registerButton);
            ApplyLink(forgotLink);
            ApplyVersionLabel(versionLabel);
        }

        public static void ApplyRegistrationForm(
            Form form,
            Panel card,
            Panel header,
            Panel body,
            Panel footer,
            Label medicalIcon,
            Label title,
            Label lockIcon,
            Button closeButton,
            Label fullNameLabel,
            TextBox fullNameBox,
            Label emailLabel,
            TextBox emailBox,
            Label phoneLabel,
            TextBox phoneBox,
            Label addressLabel,
            TextBox addressBox,
            Label passwordLabel,
            TextBox passwordBox,
            Button togglePassword,
            Label confirmLabel,
            TextBox confirmBox,
            Button registerButton,
            Button cancelButton,
            Panel successPanel,
            Label successIcon,
            Label successTitle,
            Label successMessage,
            Button returnLoginButton)
        {
            ApplyForm(form);
            card.BackColor = AppTheme.SurfaceContainerLowest;
            header.BackColor = AppTheme.Primary;
            body.BackColor = AppTheme.SurfaceContainerLowest;
            footer.BackColor = AppTheme.SurfaceContainerLowest;
            ApplyLoginHeader(medicalIcon, title, lockIcon);
            ApplyHeaderCloseButton(closeButton);
            ApplyFieldLabel(fullNameLabel);
            ApplyFieldLabel(emailLabel);
            ApplyFieldLabel(phoneLabel);
            ApplyFieldLabel(addressLabel);
            ApplyFieldLabel(passwordLabel);
            ApplyFieldLabel(confirmLabel);
            ApplyTextBox(fullNameBox);
            ApplyTextBox(emailBox);
            ApplyTextBox(phoneBox);
            ApplyTextBox(addressBox);
            ApplyTextBox(passwordBox);
            ApplyTextBox(confirmBox);
            ApplyIconButton(togglePassword, AppTheme.OnSurfaceVariant);
            ApplyPrimaryButton(registerButton);
            ApplySecondaryButton(cancelButton);
            successPanel.BackColor = AppTheme.SurfaceContainerLowest;
            successIcon.Font = new Font("Segoe MDL2 Assets", 20f);
            successIcon.ForeColor = AppTheme.Primary;
            successTitle.Font = FontManager.Get(12f, FontStyle.Bold);
            successTitle.ForeColor = AppTheme.Primary;
            successMessage.Font = AppTheme.BodyFont;
            successMessage.ForeColor = AppTheme.OnSurfaceVariant;
            returnLoginButton.FlatStyle = FlatStyle.Flat;
            returnLoginButton.FlatAppearance.BorderSize = 0;
            returnLoginButton.BackColor = AppTheme.Primary;
            returnLoginButton.ForeColor = AppTheme.OnPrimary;
            returnLoginButton.Font = AppTheme.LabelFont;
            returnLoginButton.Cursor = Cursors.Hand;
        }

        public static void ApplyAdminShell(
            Form form,
            Panel topBar,
            Label topTitle,
            Label topSubtitle,
            Button closeButton,
            Panel sidebar,
            Panel content)
        {
            ApplyForm(form);
            topBar.BackColor = AppTheme.Primary;
            topTitle.Font = AppTheme.AppTitleFont;
            topTitle.ForeColor = AppTheme.OnPrimary;
            topSubtitle.Font = AppTheme.LabelFont;
            topSubtitle.ForeColor = AppTheme.OnPrimary;
            ApplyHeaderCloseButton(closeButton);
            sidebar.BackColor = AppTheme.Surface;
            content.BackColor = AppTheme.Surface;
        }

        public static void ApplyDataGrid(DataGridView grid)
        {
            grid.ColumnHeadersDefaultCellStyle.BackColor = AppTheme.SurfaceContainerHigh;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = AppTheme.Primary;
            grid.ColumnHeadersDefaultCellStyle.Font = AppTheme.LabelFont;
            grid.DefaultCellStyle.Font = AppTheme.BodyFont;
            grid.DefaultCellStyle.ForeColor = AppTheme.OnSurface;
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 255);
            grid.GridColor = AppTheme.OutlineVariant;
        }

        public static void ApplyDangerButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderColor = AppTheme.Error;
            button.FlatAppearance.BorderSize = 1;
            button.BackColor = AppTheme.SurfaceContainerLowest;
            button.ForeColor = AppTheme.Error;
            button.Font = AppTheme.LabelFont;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplySecondaryActionButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = AppTheme.Secondary;
            button.ForeColor = AppTheme.OnSecondary;
            button.Font = AppTheme.LabelFont;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyAccentButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = AppTheme.OnPrimaryContainer;
            button.ForeColor = AppTheme.Primary;
            button.Font = FontManager.Get(9f, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyNavButton(Button button, bool active = false, bool isLogout = false)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.Font = AppTheme.LabelFont;
            button.Cursor = Cursors.Hand;
            button.Height = 40;

            if (isLogout)
            {
                button.BackColor = AppTheme.Surface;
                button.ForeColor = AppTheme.Error;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 245, 245);
                return;
            }

            if (active)
            {
                button.BackColor = AppTheme.SecondaryContainer;
                button.ForeColor = AppTheme.OnSecondaryContainer;
            }
            else
            {
                button.BackColor = AppTheme.Surface;
                button.ForeColor = AppTheme.OnSurfaceVariant;
                button.FlatAppearance.MouseOverBackColor = Color.FromArgb(248, 250, 255);
            }
        }
    }
}
