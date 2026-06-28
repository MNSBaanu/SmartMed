namespace SmartMed.UI
{
    partial class ManageOrdersForm
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
            // ManageOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "ManageOrdersForm";
            this.Size = new System.Drawing.Size(1060, 720);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.CheckBox chkDateRange;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Label lblVolume;
        private System.Windows.Forms.Label lblAvgTime;
        private System.Windows.Forms.Label lblFlags;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnPagePrev;
        private System.Windows.Forms.Button btnPageNext;
    }
}
