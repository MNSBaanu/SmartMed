namespace SmartMed.UI
{
    partial class ManageCustomersForm
    {
        private System.ComponentModel.IContainer components = null;
        private SmartMed.UI.Controls.ManageCustomersPanel customersPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.customersPanel = new SmartMed.UI.Controls.ManageCustomersPanel();
            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.customersPanel);
            this.panelContent.Padding = new System.Windows.Forms.Padding(24);
            // 
            // customersPanel
            // 
            this.customersPanel.AutoScroll = true;
            this.customersPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.customersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customersPanel.Location = new System.Drawing.Point(24, 24);
            this.customersPanel.MinimumSize = new System.Drawing.Size(800, 900);
            this.customersPanel.Name = "customersPanel";
            this.customersPanel.Size = new System.Drawing.Size(856, 664);
            this.customersPanel.TabIndex = 0;
            // 
            // ManageCustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 760);
            this.Name = "ManageCustomersForm";
            this.Text = "SmartMed - Manage Customers";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}
