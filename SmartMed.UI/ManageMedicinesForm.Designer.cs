namespace SmartMed.UI
{
    partial class ManageMedicinesForm
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
            this.medicinesPanel = new SmartMed.UI.Controls.ManageMedicinesPanel();
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
            this.panelContent.Controls.Add(this.medicinesPanel);
            this.panelContent.Size = new System.Drawing.Size(1073, 965);
            // 
            // medicinesPanel
            // 
            this.medicinesPanel.AutoScroll = true;
            this.medicinesPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.medicinesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.medicinesPanel.Location = new System.Drawing.Point(24, 24);
            this.medicinesPanel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.medicinesPanel.MinimumSize = new System.Drawing.Size(914, 934);
            this.medicinesPanel.Name = "medicinesPanel";
            this.medicinesPanel.Size = new System.Drawing.Size(1025, 934);
            this.medicinesPanel.TabIndex = 0;
            // 
            // ManageMedicinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1353, 1013);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1140, 838);
            this.Name = "ManageMedicinesForm";
            this.Text = "SmartMed - Manage Medicines";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelSidebar.ResumeLayout(false);
            this.panelSidebar.PerformLayout();
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Controls.ManageMedicinesPanel medicinesPanel;
    }
}
