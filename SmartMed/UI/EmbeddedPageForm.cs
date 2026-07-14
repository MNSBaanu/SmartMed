using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SmartMed.UI
{
    public interface IEmbeddedPage
    {
        void RefreshPage();
    }

    [ToolboxItem(false)]
    public class EmbeddedPageForm : Form, IEmbeddedPage
    {
        private bool _pageBuilt;

        public EmbeddedPageForm()
        {
            ConfigureEmbeddedShell(this);
        }

        internal static void ConfigureEmbeddedShell(Form form)
        {
            form.FormBorderStyle = FormBorderStyle.None;
            form.ControlBox = false;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.ShowIcon = false;
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.Manual;
        }

        public override ISite Site
        {
            get => base.Site;
            set
            {
                base.Site = value;
                if (value != null)
                    EnsurePageContent();
            }
        }

        protected bool IsDesignHost() => DesignHostHelper.IsDesignHost(this);

        protected virtual bool PreferDesignTimePreview() => IsDesignHost();

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            EnsurePageContent();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            EnsurePageContent();
        }

        protected override void SetVisibleCore(bool value)
        {
            EnsurePageContent();
            base.SetVisibleCore(value);
        }

        protected void EnsurePageContent()
        {
            if (_pageBuilt)
                return;

            try
            {
                if (PreferDesignTimePreview())
                    LoadDesignTimePreview();
                else
                    DoRefreshPage();

                if (!IsDesignHost())
                    DoubleBuffered = true;

                PerformLayout();
                Invalidate(true);
                _pageBuilt = true;
            }
            catch (Exception ex) when (IsDesignHost() || LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                ShowDesignTimeBuildError(ex);
                _pageBuilt = true;
            }
        }

        private void ShowDesignTimeBuildError(Exception ex)
        {
            Controls.Clear();
            Controls.Add(new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.DarkRed,
                BackColor = Color.FromArgb(244, 251, 250),
                Padding = new Padding(16),
                Text = "Design preview could not be built:" + Environment.NewLine + Environment.NewLine + ex.Message
            });
        }

        protected virtual void DoRefreshPage() { }
        protected virtual void LoadDesignTimePreview() { }

        public void RefreshPage()
        {
            EnsurePageContent();
            if (PreferDesignTimePreview())
                return;
            DoRefreshPage();
        }
    }
}
