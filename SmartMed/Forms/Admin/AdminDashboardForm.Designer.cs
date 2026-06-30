namespace SmartMed.UI
{
    public sealed partial class AdminDashboardForm
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
            // 
            // AdminDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "AdminDashboardForm";
            this.Size = new System.Drawing.Size(1060, 720);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Label lblStockValue;
        private System.Windows.Forms.Label lblOrdersValue;
        private System.Windows.Forms.Label lblSalesValue;
        private System.Windows.Forms.Label lblCustomersValue;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView gridLowStock;
        private System.Windows.Forms.DataGridView gridExpiry;
        private System.Windows.Forms.DataGridView gridRecent;
    }
}
