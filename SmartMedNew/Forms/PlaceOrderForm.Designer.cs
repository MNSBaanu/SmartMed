namespace SmartMedNew.UI
{
    partial class PlaceOrderForm
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
            this.Name = "PlaceOrderForm";
            this.Size = new System.Drawing.Size(1060, 720);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView gridCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblRxNote;
        private System.Windows.Forms.TextBox txtPrescriptionPath;
    }
}
