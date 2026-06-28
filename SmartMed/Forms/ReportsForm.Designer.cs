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
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ReportsForm";
            this.Size = new System.Drawing.Size(1060, 720);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.DataGridView gridReport;
        private System.Windows.Forms.ComboBox cmbCustomer;
        private System.Windows.Forms.Panel panelCustomerFilter;
        private System.Windows.Forms.Panel panelPeriodFilter;
        private System.Windows.Forms.Button btnWeekPeriod;
        private System.Windows.Forms.Button btnMonthPeriod;
        private System.Windows.Forms.Button btnYearPeriod;
        private System.Windows.Forms.Button btnSalesTab;
        private System.Windows.Forms.Button btnInventoryTab;
        private System.Windows.Forms.Button btnHistoryTab;
        private System.Windows.Forms.Button btnViewReport;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Label lblTotalRevenue;
        private System.Windows.Forms.Label lblTotalOrders;
        private System.Windows.Forms.Label lblLowStock;
        private System.Windows.Forms.Label lblOutstanding;
        private System.Windows.Forms.Label lblStatTitleRevenue;
        private System.Windows.Forms.Label lblStatTitleOrders;
        private System.Windows.Forms.Label lblStatTitleLowStock;
        private System.Windows.Forms.Label lblStatTitleOutstanding;
        private System.Windows.Forms.Label lblFooterStatus;
    }
}
