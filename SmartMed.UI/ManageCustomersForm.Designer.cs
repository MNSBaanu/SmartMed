namespace SmartMed.UI
{
    partial class ManageCustomersForm
    {
        private System.ComponentModel.IContainer components = null;

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
            // panelTop
            // 
            this.panelTop.Size = new System.Drawing.Size(1353, 48);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(1309, 8);
            // 
            // panelSidebar
            // 
            this.panelSidebar.Size = new System.Drawing.Size(280, 965);
            // 
            // btnNavLogout
            // 
            this.btnNavLogout.Location = new System.Drawing.Point(0, 909);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.customersPanel);
            this.panelContent.Size = new System.Drawing.Size(1073, 965);
            // 
            // customersPanel
            // 
            this.customersPanel.AutoScroll = true;
            this.customersPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.customersPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customersPanel.Location = new System.Drawing.Point(24, 24);
            this.customersPanel.MinimumSize = new System.Drawing.Size(800, 900);
            this.customersPanel.Name = "customersPanel";
            this.customersPanel.Size = new System.Drawing.Size(1025, 917);
            this.customersPanel.TabIndex = 0;
            // 
            // ManageCustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1013);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1140, 838);
            this.Name = "ManageCustomersForm";
            this.Text = "SmartMed - Manage Customers";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Controls.ManageCustomersPanel customersPanel;
    }
}
