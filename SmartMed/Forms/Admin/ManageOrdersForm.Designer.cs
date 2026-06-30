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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ManageOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "ManageOrdersForm";
                        this.ClientSize = new System.Drawing.Size(1060, 720);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
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
