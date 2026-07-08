namespace SmartMed.UI
{
    partial class TrackOrdersForm
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
            this.panelScrollHost = new System.Windows.Forms.Panel();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnCancelPending = new System.Windows.Forms.Button();
            this.btnExportCsv = new System.Windows.Forms.Button();
            this.btnExportPdf = new System.Windows.Forms.Button();
            this.panelOrdersOuter = new System.Windows.Forms.Panel();
            this.panelOrdersBody = new System.Windows.Forms.Panel();
            this.gridOrders = new System.Windows.Forms.DataGridView();
            this.panelOrdersHeader = new System.Windows.Forms.Panel();
            this.lblOrdersTitle = new System.Windows.Forms.Label();
            this.panelItemsOuter = new System.Windows.Forms.Panel();
            this.panelItemsBody = new System.Windows.Forms.Panel();
            this.gridItems = new System.Windows.Forms.DataGridView();
            this.panelItemsHeader = new System.Windows.Forms.Panel();
            this.lblItemsTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowActions.SuspendLayout();
            this.panelOrdersOuter.SuspendLayout();
            this.panelOrdersBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).BeginInit();
            this.panelOrdersHeader.SuspendLayout();
            this.panelItemsOuter.SuspendLayout();
            this.panelItemsBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).BeginInit();
            this.panelItemsHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelScrollHost
            // 
            this.panelScrollHost.AutoScroll = true;
            this.panelScrollHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelScrollHost.Controls.Add(this.tableLayoutRoot);
            this.panelScrollHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelScrollHost.Location = new System.Drawing.Point(0, 0);
            this.panelScrollHost.Name = "panelScrollHost";
            this.panelScrollHost.Padding = new System.Windows.Forms.Padding(24);
            this.panelScrollHost.Size = new System.Drawing.Size(1060, 720);
            this.panelScrollHost.TabIndex = 0;
            // 
            // tableLayoutRoot
            // 
            this.tableLayoutRoot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.tableLayoutRoot.ColumnCount = 1;
            this.tableLayoutRoot.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Controls.Add(this.panelHeader, 0, 0);
            this.tableLayoutRoot.Controls.Add(this.flowActions, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelOrdersOuter, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.panelItemsOuter, 0, 3);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 4;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 520);
            this.tableLayoutRoot.TabIndex = 0;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelHeader.Controls.Add(this.lblPageSubtitle);
            this.panelHeader.Controls.Add(this.lblPageTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1012, 76);
            this.panelHeader.TabIndex = 0;
            // 
            // lblPageSubtitle
            // 
            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(0, 44);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(400, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "View order history, cancel pending orders, and export receipts.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(130, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "My Orders";
            // 
            // flowActions
            // 
            this.flowActions.AutoSize = true;
            this.flowActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowActions.Controls.Add(this.btnCancelPending);
            this.flowActions.Controls.Add(this.btnExportCsv);
            this.flowActions.Controls.Add(this.btnExportPdf);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.Location = new System.Drawing.Point(0, 92);
            this.flowActions.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.flowActions.Name = "flowActions";
            this.flowActions.Size = new System.Drawing.Size(1012, 30);
            this.flowActions.TabIndex = 1;
            this.flowActions.WrapContents = true;
            // 
            // btnCancelPending
            // 
            this.btnCancelPending.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnCancelPending.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelPending.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnCancelPending.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelPending.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnCancelPending.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnCancelPending.Location = new System.Drawing.Point(0, 0);
            this.btnCancelPending.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnCancelPending.Name = "btnCancelPending";
            this.btnCancelPending.Size = new System.Drawing.Size(160, 30);
            this.btnCancelPending.TabIndex = 0;
            this.btnCancelPending.Text = "Cancel Pending Order";
            this.btnCancelPending.UseVisualStyleBackColor = false;
            // 
            // btnExportCsv
            // 
            this.btnExportCsv.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExportCsv.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportCsv.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExportCsv.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportCsv.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExportCsv.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExportCsv.Location = new System.Drawing.Point(170, 0);
            this.btnExportCsv.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnExportCsv.Name = "btnExportCsv";
            this.btnExportCsv.Size = new System.Drawing.Size(110, 30);
            this.btnExportCsv.TabIndex = 1;
            this.btnExportCsv.Text = "Export CSV";
            this.btnExportCsv.UseVisualStyleBackColor = false;
            // 
            // btnExportPdf
            // 
            this.btnExportPdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnExportPdf.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportPdf.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnExportPdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExportPdf.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnExportPdf.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnExportPdf.Location = new System.Drawing.Point(290, 0);
            this.btnExportPdf.Name = "btnExportPdf";
            this.btnExportPdf.Size = new System.Drawing.Size(110, 30);
            this.btnExportPdf.TabIndex = 2;
            this.btnExportPdf.Text = "Export PDF";
            this.btnExportPdf.UseVisualStyleBackColor = false;
            // 
            // panelOrdersOuter
            // 
            this.panelOrdersOuter.BackColor = System.Drawing.Color.White;
            this.panelOrdersOuter.Controls.Add(this.panelOrdersBody);
            this.panelOrdersOuter.Controls.Add(this.panelOrdersHeader);
            this.panelOrdersOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrdersOuter.Location = new System.Drawing.Point(0, 134);
            this.panelOrdersOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelOrdersOuter.Name = "panelOrdersOuter";
            this.panelOrdersOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelOrdersOuter.Size = new System.Drawing.Size(1012, 258);
            this.panelOrdersOuter.TabIndex = 2;
            // 
            // panelOrdersBody
            // 
            this.panelOrdersBody.BackColor = System.Drawing.Color.White;
            this.panelOrdersBody.Controls.Add(this.gridOrders);
            this.panelOrdersBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOrdersBody.Location = new System.Drawing.Point(1, 37);
            this.panelOrdersBody.Name = "panelOrdersBody";
            this.panelOrdersBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelOrdersBody.Size = new System.Drawing.Size(1010, 220);
            this.panelOrdersBody.TabIndex = 1;
            // 
            // gridOrders
            // 
            this.gridOrders.AllowUserToAddRows = false;
            this.gridOrders.AllowUserToDeleteRows = false;
            this.gridOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridOrders.BackgroundColor = System.Drawing.Color.White;
            this.gridOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridOrders.ColumnHeadersHeight = 36;
            this.gridOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridOrders.Location = new System.Drawing.Point(0, 4);
            this.gridOrders.Name = "gridOrders";
            this.gridOrders.ReadOnly = true;
            this.gridOrders.RowHeadersVisible = false;
            this.gridOrders.RowTemplate.Height = 36;
            this.gridOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridOrders.Size = new System.Drawing.Size(1010, 216);
            this.gridOrders.TabIndex = 0;
            // 
            // panelOrdersHeader
            // 
            this.panelOrdersHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelOrdersHeader.Controls.Add(this.lblOrdersTitle);
            this.panelOrdersHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelOrdersHeader.Location = new System.Drawing.Point(1, 1);
            this.panelOrdersHeader.Name = "panelOrdersHeader";
            this.panelOrdersHeader.Padding = new System.Windows.Forms.Padding(12, 8, 10, 4);
            this.panelOrdersHeader.Size = new System.Drawing.Size(1010, 36);
            this.panelOrdersHeader.TabIndex = 0;
            // 
            // lblOrdersTitle
            // 
            this.lblOrdersTitle.AutoSize = true;
            this.lblOrdersTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblOrdersTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblOrdersTitle.Location = new System.Drawing.Point(12, 10);
            this.lblOrdersTitle.Name = "lblOrdersTitle";
            this.lblOrdersTitle.Size = new System.Drawing.Size(82, 16);
            this.lblOrdersTitle.TabIndex = 0;
            this.lblOrdersTitle.Text = "Your Orders";
            // 
            // panelItemsOuter
            // 
            this.panelItemsOuter.BackColor = System.Drawing.Color.White;
            this.panelItemsOuter.Controls.Add(this.panelItemsBody);
            this.panelItemsOuter.Controls.Add(this.panelItemsHeader);
            this.panelItemsOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelItemsOuter.Location = new System.Drawing.Point(0, 404);
            this.panelItemsOuter.Name = "panelItemsOuter";
            this.panelItemsOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelItemsOuter.Size = new System.Drawing.Size(1012, 216);
            this.panelItemsOuter.TabIndex = 3;
            // 
            // panelItemsBody
            // 
            this.panelItemsBody.BackColor = System.Drawing.Color.White;
            this.panelItemsBody.Controls.Add(this.gridItems);
            this.panelItemsBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelItemsBody.Location = new System.Drawing.Point(1, 37);
            this.panelItemsBody.Name = "panelItemsBody";
            this.panelItemsBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelItemsBody.Size = new System.Drawing.Size(1010, 178);
            this.panelItemsBody.TabIndex = 1;
            // 
            // gridItems
            // 
            this.gridItems.AllowUserToAddRows = false;
            this.gridItems.AllowUserToDeleteRows = false;
            this.gridItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridItems.BackgroundColor = System.Drawing.Color.White;
            this.gridItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridItems.ColumnHeadersHeight = 36;
            this.gridItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridItems.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridItems.Location = new System.Drawing.Point(0, 4);
            this.gridItems.Name = "gridItems";
            this.gridItems.ReadOnly = true;
            this.gridItems.RowHeadersVisible = false;
            this.gridItems.RowTemplate.Height = 36;
            this.gridItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridItems.Size = new System.Drawing.Size(1010, 174);
            this.gridItems.TabIndex = 0;
            // 
            // panelItemsHeader
            // 
            this.panelItemsHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelItemsHeader.Controls.Add(this.lblItemsTitle);
            this.panelItemsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelItemsHeader.Location = new System.Drawing.Point(1, 1);
            this.panelItemsHeader.Name = "panelItemsHeader";
            this.panelItemsHeader.Padding = new System.Windows.Forms.Padding(12, 8, 10, 4);
            this.panelItemsHeader.Size = new System.Drawing.Size(1010, 36);
            this.panelItemsHeader.TabIndex = 0;
            // 
            // lblItemsTitle
            // 
            this.lblItemsTitle.AutoSize = true;
            this.lblItemsTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblItemsTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblItemsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblItemsTitle.Location = new System.Drawing.Point(12, 10);
            this.lblItemsTitle.Name = "lblItemsTitle";
            this.lblItemsTitle.Size = new System.Drawing.Size(80, 16);
            this.lblItemsTitle.TabIndex = 0;
            this.lblItemsTitle.Text = "Order Items";
            // 
            // TrackOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1060, 720);
            this.ControlBox = false;
            this.Controls.Add(this.panelScrollHost);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TrackOrdersForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "My Orders";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.tableLayoutRoot.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowActions.ResumeLayout(false);
            this.panelOrdersOuter.ResumeLayout(false);
            this.panelOrdersBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridOrders)).EndInit();
            this.panelOrdersHeader.ResumeLayout(false);
            this.panelOrdersHeader.PerformLayout();
            this.panelItemsOuter.ResumeLayout(false);
            this.panelItemsBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridItems)).EndInit();
            this.panelItemsHeader.ResumeLayout(false);
            this.panelItemsHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnCancelPending;
        private System.Windows.Forms.Button btnExportCsv;
        private System.Windows.Forms.Button btnExportPdf;
        private System.Windows.Forms.Panel panelOrdersOuter;
        private System.Windows.Forms.Panel panelOrdersBody;
        private System.Windows.Forms.DataGridView gridOrders;
        private System.Windows.Forms.Panel panelOrdersHeader;
        private System.Windows.Forms.Label lblOrdersTitle;
        private System.Windows.Forms.Panel panelItemsOuter;
        private System.Windows.Forms.Panel panelItemsBody;
        private System.Windows.Forms.DataGridView gridItems;
        private System.Windows.Forms.Panel panelItemsHeader;
        private System.Windows.Forms.Label lblItemsTitle;
    }
}
