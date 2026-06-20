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
            this.panelTop.SuspendLayout();
            this.panelSidebar.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            this.panelTop.Size = new System.Drawing.Size(1353, 48);
            this.btnClose.Location = new System.Drawing.Point(1309, 8);
            this.panelSidebar.Size = new System.Drawing.Size(280, 965);
            this.btnNavLogout.Location = new System.Drawing.Point(0, 909);
            this.panelContent.Size = new System.Drawing.Size(1073, 965);
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
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
    }
}
