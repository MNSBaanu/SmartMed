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
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // medicinesPanel
            // 
            this.medicinesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.medicinesPanel.Location = new System.Drawing.Point(24, 24);
            this.medicinesPanel.Name = "medicinesPanel";
            this.medicinesPanel.Size = new System.Drawing.Size(856, 664);
            this.medicinesPanel.TabIndex = 0;
            // 
            // panelContent
            // 
            this.panelContent.AutoScroll = true;
            this.panelContent.Controls.Add(this.medicinesPanel);
            this.panelContent.Padding = new System.Windows.Forms.Padding(24);
            // 
            // ManageMedicinesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 760);
            this.Name = "ManageMedicinesForm";
            this.Text = "SmartMed - Manage Medicines";
            this.panelContent.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private Controls.ManageMedicinesPanel medicinesPanel;
    }
}
