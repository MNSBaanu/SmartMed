namespace SmartMed.UI
{
    partial class CustomerHostForm
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
            this.ClientSize = new System.Drawing.Size(1184, 768);
            this.MinimumSize = new System.Drawing.Size(1000, 640);
            this.Name = "CustomerHostForm";
            this.Text = "SmartMed Customer Portal";
            this.ResumeLayout(false);
        }
    }
}
