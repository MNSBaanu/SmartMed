namespace SmartMedNew.UI
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ManageMedicinesForm";
            this.Size = new System.Drawing.Size(1060, 720);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.DataGridView gridMedicines;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label lblExpiringSoon;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnReload;
    }
}
