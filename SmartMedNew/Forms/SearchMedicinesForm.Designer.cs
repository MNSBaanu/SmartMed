namespace SmartMedNew.UI
{
    partial class SearchMedicinesForm
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
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SearchMedicinesForm";
            this.Size = new System.Drawing.Size(1060, 720);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.NumericUpDown numQty;
        private System.Windows.Forms.Label lblDetails;
    }
}
