using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class ClinicalPrecisionUiHelper
    {
        public static class NavIcon
        {
            public const string Dashboard = "\uE9D2";
            public const string Medicines = "\uE7C3";
            public const string Customers = "\uE716";
            public const string Orders = "\uE8A1";
            public const string Reports = "\uE9D9";
            public const string Search = "\uE721";
            public const string Cart = "\uE7BF";
            public const string Track = "\uE7C9";
            public const string Profile = "\uE77B";
            public const string Settings = "\uE713";
            public const string Support = "\uE897";
            public const string Logout = "\uE7E8";
            public const string Pharmacy = "\uE54C";
        }

        public static void ApplyAdminShell(
            Form form,
            Panel headerPanel,
            Label lblHeaderTitle,
            TextBox txtGlobalSearch,
            Panel sidebarPanel,
            Label lblBrandTitle,
            Label lblBrandSubtitle,
            Button btnLogout,
            params Button[] navButtons)
        {
            form.Text = "SmartMed - Admin Dashboard";
            headerPanel.BackColor = ClinicalPrecisionTheme.Primary;
            headerPanel.Height = ClinicalPrecisionTheme.HeaderHeight;
            lblHeaderTitle.Text = "SmartMed Pharmacy";
            lblHeaderTitle.Font = ClinicalPrecisionTheme.AppTitleFont;
            lblHeaderTitle.ForeColor = ClinicalPrecisionTheme.OnPrimary;

            StyleGlobalSearch(txtGlobalSearch);

            sidebarPanel.BackColor = ClinicalPrecisionTheme.Surface;
            sidebarPanel.Padding = new Padding(ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.StackMd, ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.ContainerPadding);
            sidebarPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                    e.Graphics.DrawLine(pen, sidebarPanel.Width - 1, 0, sidebarPanel.Width - 1, sidebarPanel.Height);
            };

            lblBrandTitle.Text = "SmartMed";
            lblBrandTitle.Font = ClinicalPrecisionTheme.AppTitleFont;
            lblBrandTitle.ForeColor = ClinicalPrecisionTheme.Primary;
            lblBrandSubtitle.Text = "Pharmacy Management System";
            lblBrandSubtitle.Font = ClinicalPrecisionTheme.LabelFont;
            lblBrandSubtitle.ForeColor = Color.FromArgb(180, ClinicalPrecisionTheme.OnSurfaceVariant);

            StyleNavButton(btnLogout, NavIcon.Logout, "Logout", false, true);
            btnLogout.ForeColor = ClinicalPrecisionTheme.Error;
            btnLogout.FlatAppearance.BorderSize = 0;

            PositionSidebarFooter(sidebarPanel, btnLogout);
            FontManager.ApplyInterFont(form);
        }

        public static void ApplyCustomerShell(
            Form form,
            Panel headerPanel,
            Label lblHeaderTitle,
            TextBox txtGlobalSearch,
            Panel sidebarPanel,
            Label lblBrandTitle,
            Label lblBrandSubtitle,
            Button btnLogout,
            params Button[] navButtons)
        {
            form.Text = "SmartMed - Customer Portal";
            headerPanel.BackColor = ClinicalPrecisionTheme.Primary;
            lblHeaderTitle.Text = "SmartMed - Customer Portal";
            lblHeaderTitle.Font = ClinicalPrecisionTheme.AppTitleFont;
            lblHeaderTitle.ForeColor = ClinicalPrecisionTheme.OnPrimary;
            StyleGlobalSearch(txtGlobalSearch);

            sidebarPanel.BackColor = ClinicalPrecisionTheme.Surface;
            sidebarPanel.Paint += (s, e) =>
            {
                using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                    e.Graphics.DrawLine(pen, sidebarPanel.Width - 1, 0, sidebarPanel.Width - 1, sidebarPanel.Height);
            };

            lblBrandTitle.Text = "SmartMed";
            lblBrandTitle.Font = ClinicalPrecisionTheme.SectionHeaderFont;
            lblBrandTitle.ForeColor = ClinicalPrecisionTheme.Primary;
            lblBrandSubtitle.Text = "Patient Management";
            lblBrandSubtitle.Font = ClinicalPrecisionTheme.LabelFont;
            lblBrandSubtitle.ForeColor = Color.FromArgb(180, ClinicalPrecisionTheme.OnSurfaceVariant);

            StyleNavButton(btnLogout, NavIcon.Logout, "Logout", false, true);
            btnLogout.ForeColor = ClinicalPrecisionTheme.Error;
            btnLogout.FlatAppearance.BorderSize = 0;
            PositionSidebarFooter(sidebarPanel, btnLogout);
            FontManager.ApplyInterFont(form);
        }

        public static void StyleNavButton(Button btn, string icon, string text, bool active, bool customerPortal = false)
        {
            btn.Height = ClinicalPrecisionTheme.NavItemHeight;
            btn.Width = ClinicalPrecisionTheme.NavWidth - ClinicalPrecisionTheme.ContainerPadding * 2;
            btn.FlatStyle = FlatStyle.Flat;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Text = $" {icon}   {text}";
            btn.Font = new Font("Segoe MDL2 Assets", 10f);
            btn.Padding = new Padding(4, 0, 0, 0);

            var labelPart = text;
            btn.Paint -= NavButton_Paint;
            btn.Paint += NavButton_Paint;
            btn.Tag = new NavButtonTag(icon, labelPart, active, customerPortal);

            ApplyNavButtonState(btn, active, customerPortal);
        }

        private static void NavButton_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is Button btn) || !(btn.Tag is NavButtonTag tag)) return;

            e.Graphics.Clear(btn.BackColor);
            using (var iconFont = new Font("Segoe MDL2 Assets", 11f))
            using (var textFont = tag.Active ? FontManager.Get(9f, FontStyle.Bold) : ClinicalPrecisionTheme.LabelFont)
            using (var brush = new SolidBrush(btn.ForeColor))
            {
                e.Graphics.DrawString(tag.Icon, iconFont, brush, 8, 10);
                e.Graphics.DrawString(tag.Text, textFont, brush, 36, 11);
            }
        }

        public static void ApplyNavButtonState(Button btn, bool active, bool customerPortal = false)
        {
            if (active)
            {
                if (customerPortal)
                {
                    btn.BackColor = ClinicalPrecisionTheme.NavActiveCustomer;
                    btn.ForeColor = ClinicalPrecisionTheme.OnPrimary;
                }
                else
                {
                    btn.BackColor = ClinicalPrecisionTheme.SecondaryContainer;
                    btn.ForeColor = ClinicalPrecisionTheme.OnSecondaryContainer;
                }
            }
            else
            {
                btn.BackColor = ClinicalPrecisionTheme.Surface;
                btn.ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant;
            }
            btn.FlatAppearance.BorderSize = 0;
            btn.Invalidate();
        }

        public static void SetActiveNav(Button active, bool customerPortal, params Button[] all)
        {
            foreach (var btn in all)
                if (btn.Tag is NavButtonTag tag)
                    ApplyNavButtonState(btn, btn == active, tag.CustomerPortal);
        }

        public static Panel CreatePageHeader(string title, string subtitle)
        {
            var panel = new Panel
            {
                Dock = DockStyle.Top,
                Height = string.IsNullOrEmpty(subtitle) ? 44 : 64,
                Padding = new Padding(0, 0, 0, ClinicalPrecisionTheme.StackMd)
            };
            panel.Paint += (s, e) =>
            {
                var y = panel.Height - 1;
                using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                    e.Graphics.DrawLine(pen, 0, y, panel.Width, y);
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(0, 0)
            };
            panel.Controls.Add(lblTitle);

            if (!string.IsNullOrEmpty(subtitle))
            {
                var lblSub = new Label
                {
                    Text = subtitle,
                    Font = ClinicalPrecisionTheme.BodyFont,
                    ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant,
                    AutoSize = true,
                    Location = new Point(0, 28)
                };
                panel.Controls.Add(lblSub);
            }

            return panel;
        }

        public static void StyleFilterCard(Panel panel)
        {
            panel.BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
            panel.Padding = new Padding(ClinicalPrecisionTheme.StackMd);
            panel.Paint += (s, e) => DrawBorder(e.Graphics, panel.ClientRectangle, ClinicalPrecisionTheme.OutlineVariant);
        }

        public static void StyleFormCard(Panel panel)
        {
            panel.BackColor = ClinicalPrecisionTheme.SurfaceContainer;
            panel.Padding = new Padding(ClinicalPrecisionTheme.StackMd + 8);
            panel.Paint += (s, e) => DrawBorder(e.Graphics, panel.ClientRectangle, ClinicalPrecisionTheme.OutlineVariant);
        }

        public static void StyleGridCard(Panel panel)
        {
            panel.BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
            panel.Padding = new Padding(0);
            panel.Paint += (s, e) => DrawBorder(e.Graphics, panel.ClientRectangle, ClinicalPrecisionTheme.OutlineVariant);
        }

        public static void StyleStatCard(Panel panel)
        {
            panel.BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
            panel.Padding = new Padding(ClinicalPrecisionTheme.StackMd);
            panel.Paint += (s, e) => DrawBorder(e.Graphics, panel.ClientRectangle, ClinicalPrecisionTheme.OutlineVariant);
        }

        public static void ApplyPrimaryAccentButton(Button btn)
        {
            btn.BackColor = ClinicalPrecisionTheme.AccentBlue;
            btn.ForeColor = ClinicalPrecisionTheme.OnSurface;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Height = ClinicalPrecisionTheme.ButtonHeight;
            btn.Font = ClinicalPrecisionTheme.LabelFont;
        }

        public static void ApplySecondaryAccentButton(Button btn)
        {
            btn.BackColor = ClinicalPrecisionTheme.SecondaryContainer;
            btn.ForeColor = ClinicalPrecisionTheme.OnSecondaryContainer;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Height = ClinicalPrecisionTheme.ButtonHeight;
            btn.Font = ClinicalPrecisionTheme.LabelFont;
        }

        public static void ApplySecondaryButton(Button btn)
        {
            btn.BackColor = ClinicalPrecisionTheme.SurfaceContainerHigh;
            btn.ForeColor = ClinicalPrecisionTheme.OnSurface;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderColor = ClinicalPrecisionTheme.OutlineVariant;
            btn.FlatAppearance.BorderSize = 1;
            btn.Height = ClinicalPrecisionTheme.ButtonHeight;
            btn.Font = ClinicalPrecisionTheme.BodyFont;
        }

        public static void ApplyFieldLabel(Label lbl)
        {
            lbl.Font = ClinicalPrecisionTheme.LabelFont;
            lbl.ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant;
        }

        public static void ApplySectionHeader(Label lbl)
        {
            lbl.Font = ClinicalPrecisionTheme.SectionHeaderFont;
            lbl.ForeColor = ClinicalPrecisionTheme.Primary;
        }

        public static void SetupPageHeader(Panel panel, string title, string subtitle = null)
        {
            panel.Controls.Clear();
            panel.Dock = DockStyle.Top;
            panel.Height = string.IsNullOrEmpty(subtitle) ? 44 : 64;
            panel.Padding = new Padding(0, 0, 0, ClinicalPrecisionTheme.StackMd);
            panel.Paint -= PageHeader_Paint;
            panel.Paint += PageHeader_Paint;

            var lblTitle = new Label
            {
                Text = title,
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(0, 0)
            };
            panel.Controls.Add(lblTitle);

            if (!string.IsNullOrEmpty(subtitle))
            {
                panel.Controls.Add(new Label
                {
                    Text = subtitle,
                    Font = ClinicalPrecisionTheme.BodyFont,
                    ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant,
                    AutoSize = true,
                    Location = new Point(0, 28)
                });
            }
        }

        public static void SetupPageHeaderWithRightLabel(Panel panel, string title, string rightLabel)
        {
            panel.Controls.Clear();
            panel.Dock = DockStyle.Top;
            panel.Height = 44;
            panel.Padding = new Padding(0, 0, 0, ClinicalPrecisionTheme.StackMd);

            var lblTitle = new Label
            {
                Text = title,
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Location = new Point(0, 0)
            };

            var lblRight = new Label
            {
                Text = rightLabel,
                Font = ClinicalPrecisionTheme.LabelFont,
                ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant,
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            panel.Controls.Add(lblTitle);
            panel.Controls.Add(lblRight);
            panel.Resize += (s, e) => lblRight.Location = new Point(panel.ClientSize.Width - lblRight.Width, 2);
            lblRight.Location = new Point(panel.ClientSize.Width - lblRight.Width, 2);
        }

        private static void PageHeader_Paint(object sender, PaintEventArgs e)
        {
            if (!(sender is Panel panel)) return;
            var y = panel.Height - 1;
            using (var pen = new Pen(ClinicalPrecisionTheme.OutlineVariant))
                e.Graphics.DrawLine(pen, 0, y, panel.Width, y);
        }

        private static void StyleGlobalSearch(TextBox txt)
        {
            if (txt == null) return;
            txt.BackColor = Color.FromArgb(48, ClinicalPrecisionTheme.PrimaryContainer);
            txt.ForeColor = ClinicalPrecisionTheme.OnPrimary;
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.Font = ClinicalPrecisionTheme.LabelFont;
        }

        private static void PositionSidebarFooter(Panel sidebar, Button btnLogout)
        {
            btnLogout.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnLogout.Location = new Point(ClinicalPrecisionTheme.ContainerPadding, sidebar.Height - 56);
            sidebar.Resize += (s, e) => btnLogout.Top = sidebar.ClientSize.Height - btnLogout.Height - 16;
        }

        private static void DrawBorder(Graphics g, Rectangle rect, Color color)
        {
            rect.Width -= 1;
            rect.Height -= 1;
            using (var pen = new Pen(color))
                g.DrawRectangle(pen, rect);
        }

        private sealed class NavButtonTag
        {
            public string Icon { get; }
            public string Text { get; }
            public bool Active { get; }
            public bool CustomerPortal { get; }

            public NavButtonTag(string icon, string text, bool active, bool customerPortal)
            {
                Icon = icon;
                Text = text;
                Active = active;
                CustomerPortal = customerPortal;
            }
        }
    }
}
