using System;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI.Theming
{
    internal static class UiFactory
    {
        public static void ApplyFormDefaults(Form form)
        {
            form.Font = ClinicalPrecisionTheme.BodyFont;
            form.BackColor = ClinicalPrecisionTheme.Surface;
        }

        public static void ApplyFullScreen(Form form)
        {
            ApplyFormDefaults(form);
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MaximizeBox = true;
            form.MinimizeBox = true;
            form.WindowState = FormWindowState.Maximized;
        }

        public static void CenterControlOnForm(Form form, Control control)
        {
            void Center()
            {
                control.Left = Math.Max(0, (form.ClientSize.Width - control.Width) / 2);
                control.Top = Math.Max(0, (form.ClientSize.Height - control.Height) / 2);
            }

            form.Load += (s, e) => Center();
            form.Resize += (s, e) => Center();
            Center();
        }

        public static Panel CreateAppHeader(string title, string subtitle, Action onLogout)
        {
            var header = new Panel
            {
                Dock = DockStyle.Top,
                Height = ClinicalPrecisionTheme.HeaderHeight,
                BackColor = ClinicalPrecisionTheme.Primary,
                Padding = new Padding(16, 0, 16, 0)
            };

            var lblTitle = new Label
            {
                Text = title,
                Font = ClinicalPrecisionTheme.AppTitleFont,
                ForeColor = ClinicalPrecisionTheme.OnPrimary,
                AutoSize = true,
                Location = new Point(16, 12)
            };

            var lblSubtitle = new Label
            {
                Text = subtitle,
                Font = ClinicalPrecisionTheme.LabelFont,
                ForeColor = ClinicalPrecisionTheme.OnPrimary,
                AutoSize = true,
                Location = new Point(lblTitle.Right + 24, 16)
            };

            var btnLogout = CreateSecondaryButton("Logout", 90);
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.Location = new Point(header.Width - 110, 8);
            btnLogout.Click += (s, e) => onLogout?.Invoke();
            header.Controls.Add(btnLogout);
            header.Controls.Add(lblTitle);
            header.Controls.Add(lblSubtitle);

            header.Resize += (s, e) =>
            {
                btnLogout.Location = new Point(header.ClientSize.Width - btnLogout.Width - 16, 8);
            };

            return header;
        }

        public static Panel CreateSidebar()
        {
            return new Panel
            {
                Dock = DockStyle.Left,
                Width = ClinicalPrecisionTheme.NavWidth,
                BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest,
                Padding = new Padding(ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.StackMd, ClinicalPrecisionTheme.ContainerPadding, ClinicalPrecisionTheme.ContainerPadding)
            };
        }

        public static Panel CreateContentHost()
        {
            return new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = ClinicalPrecisionTheme.Surface,
                Padding = new Padding(ClinicalPrecisionTheme.ContainerPadding)
            };
        }

        public static Button CreateNavButton(string text, EventHandler onClick)
        {
            var btn = new Button
            {
                Text = "  " + text,
                Height = ClinicalPrecisionTheme.NavItemHeight,
                Width = ClinicalPrecisionTheme.NavWidth - ClinicalPrecisionTheme.ContainerPadding * 2,
                FlatStyle = FlatStyle.Flat,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = ClinicalPrecisionTheme.BodyFont,
                ForeColor = ClinicalPrecisionTheme.OnSurface,
                BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest,
                Margin = new Padding(0, 0, 0, ClinicalPrecisionTheme.StackSm),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = ClinicalPrecisionTheme.OutlineVariant;
            btn.FlatAppearance.BorderSize = 1;
            btn.Click += onClick;
            return btn;
        }

        public static void SetActiveNav(Button active, params Button[] allNavButtons)
        {
            foreach (var btn in allNavButtons)
            {
                var isActive = btn == active;
                btn.BackColor = isActive ? ClinicalPrecisionTheme.SecondaryContainer : ClinicalPrecisionTheme.SurfaceContainerLowest;
                btn.Font = isActive
                    ? new Font(ClinicalPrecisionTheme.BodyFont, FontStyle.Bold)
                    : ClinicalPrecisionTheme.BodyFont;
            }
        }

        public static Button CreatePrimaryButton(string text, int width = 100)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = ClinicalPrecisionTheme.ButtonHeight,
                FlatStyle = FlatStyle.Flat,
                BackColor = ClinicalPrecisionTheme.SecondaryContainer,
                ForeColor = ClinicalPrecisionTheme.OnSurface,
                Font = ClinicalPrecisionTheme.BodyFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        public static Button CreateSecondaryButton(string text, int width = 100)
        {
            var btn = new Button
            {
                Text = text,
                Width = width,
                Height = ClinicalPrecisionTheme.ButtonHeight,
                FlatStyle = FlatStyle.Flat,
                BackColor = ClinicalPrecisionTheme.SurfaceContainerHigh,
                ForeColor = ClinicalPrecisionTheme.OnSurface,
                Font = ClinicalPrecisionTheme.BodyFont,
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderColor = ClinicalPrecisionTheme.OutlineVariant;
            btn.FlatAppearance.BorderSize = 1;
            return btn;
        }

        public static Label CreateSectionHeader(string text)
        {
            return new Label
            {
                Text = text,
                Font = ClinicalPrecisionTheme.SectionHeaderFont,
                ForeColor = ClinicalPrecisionTheme.Primary,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, ClinicalPrecisionTheme.StackMd)
            };
        }

        public static Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                Font = ClinicalPrecisionTheme.LabelFont,
                ForeColor = ClinicalPrecisionTheme.OnSurfaceVariant,
                AutoSize = true
            };
        }

        public static void ApplyTextBoxStyle(TextBox textBox, int width = 220)
        {
            textBox.Width = width;
            textBox.Font = ClinicalPrecisionTheme.BodyFont;
            textBox.BorderStyle = BorderStyle.FixedSingle;
            textBox.BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
        }

        public static void ApplyComboBoxStyle(ComboBox comboBox, int width = 220)
        {
            comboBox.Width = width;
            comboBox.Font = ClinicalPrecisionTheme.BodyFont;
            comboBox.FlatStyle = FlatStyle.Flat;
            comboBox.BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
        }

        public static void ApplyDataGridStyle(DataGridView grid)
        {
            grid.Font = ClinicalPrecisionTheme.DataGridFont;
            grid.BackgroundColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.GridColor = ClinicalPrecisionTheme.OutlineVariant;
            grid.RowHeadersVisible = false;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = ClinicalPrecisionTheme.SurfaceContainerHigh;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = ClinicalPrecisionTheme.Primary;
            grid.ColumnHeadersDefaultCellStyle.Font = ClinicalPrecisionTheme.LabelFont;
            grid.ColumnHeadersDefaultCellStyle.Padding = new Padding(8, 4, 8, 4);
            grid.ColumnHeadersHeight = 32;
            grid.DefaultCellStyle.BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest;
            grid.DefaultCellStyle.ForeColor = ClinicalPrecisionTheme.OnSurface;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(128, ClinicalPrecisionTheme.SecondaryContainer);
            grid.DefaultCellStyle.SelectionForeColor = ClinicalPrecisionTheme.OnSurface;
            grid.AlternatingRowsDefaultCellStyle.BackColor = ClinicalPrecisionTheme.Surface;
            grid.RowTemplate.Height = 28;
        }

        public static Panel CreateCardPanel(int width, int height)
        {
            return new Panel
            {
                Width = width,
                Height = height,
                BackColor = ClinicalPrecisionTheme.SurfaceContainerLowest,
                Padding = new Padding(ClinicalPrecisionTheme.ContainerPadding)
            };
        }

        public static void StyleAuthForm(Form form)
        {
            ApplyFullScreen(form);
        }

        public static void NavigateTo(Panel contentHost, UserControl view, Button activeNav, params Button[] allNavButtons)
        {
            contentHost.Controls.Clear();
            view.Dock = DockStyle.Fill;
            contentHost.Controls.Add(view);
            if (activeNav != null)
                SetActiveNav(activeNav, allNavButtons);
        }
    }
}
