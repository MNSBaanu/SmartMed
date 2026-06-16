using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class ThemeApplier
    {
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
            textBox.BackColor = AppTheme.SurfaceContainerLowest;
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
            button.BackColor = Color.Transparent;
            button.ForeColor = foreColor;
            button.Font = AppTheme.IconFontSmall;
            button.Cursor = Cursors.Hand;
        }

        public static void ApplyHeaderCloseButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseOverBackColor = AppTheme.HeaderHover;
            button.BackColor = Color.Transparent;
            button.ForeColor = AppTheme.OnPrimaryMuted;
            button.Font = AppTheme.IconFont;
            button.Cursor = Cursors.Hand;
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
    }
}
