namespace SmartMed.UI
{
    partial class ManageCustomersForm
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
            // ManageCustomersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Name = "ManageCustomersForm";
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
        private System.Windows.Forms.DataGridView gridCustomers;
        private System.Windows.Forms.Label lblTotalCustomers;
        private System.Windows.Forms.Label lblActiveCustomers;
        private System.Windows.Forms.Label lblInactiveCustomers;
        private System.Windows.Forms.Label lblPageInfo;
        private System.Windows.Forms.Button btnPagePrev;
        private System.Windows.Forms.Button btnPageNext;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnReload;
    }
}
