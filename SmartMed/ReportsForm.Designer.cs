namespace SmartMed.UI
{
    partial class ReportsForm
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
            this.reportsPanel = new SmartMed.UI.Controls.ReportsPanel();
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
            this.panelContent.Controls.Add(this.reportsPanel);
            this.panelContent.Size = new System.Drawing.Size(1073, 965);
            // 
            // reportsPanel
            // 
            this.reportsPanel.AutoScroll = true;
            this.reportsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.reportsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportsPanel.Location = new System.Drawing.Point(24, 24);
            this.reportsPanel.MinimumSize = new System.Drawing.Size(800, 900);
            this.reportsPanel.Name = "reportsPanel";
            this.reportsPanel.Size = new System.Drawing.Size(1025, 917);
            this.reportsPanel.TabIndex = 0;
            // 
            // ReportsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1013);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1140, 838);
            this.Name = "ReportsForm";
            this.Text = "SmartMed - Generate Reports";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Controls.ReportsPanel reportsPanel;
    }
}
