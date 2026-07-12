namespace SmartMed.UI
{
    partial class CustomerDashboardForm
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
            this.tableStatsRow = new System.Windows.Forms.TableLayoutPanel();
            this.panelStatCart = new System.Windows.Forms.Panel();
            this.lblCart = new System.Windows.Forms.Label();
            this.lblStatCartTitle = new System.Windows.Forms.Label();
            this.panelStatOrders = new System.Windows.Forms.Panel();
            this.lblOrders = new System.Windows.Forms.Label();
            this.lblStatOrdersTitle = new System.Windows.Forms.Label();
            this.panelStatPromotions = new System.Windows.Forms.Panel();
            this.lblPromotions = new System.Windows.Forms.Label();
            this.lblStatPromotionsTitle = new System.Windows.Forms.Label();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.gridRecent = new System.Windows.Forms.DataGridView();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.tableStatsRow.SuspendLayout();
            this.panelStatCart.SuspendLayout();
            this.panelStatOrders.SuspendLayout();
            this.panelStatPromotions.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridRecent)).BeginInit();
            this.panelGridHeader.SuspendLayout();
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
            this.tableLayoutRoot.Controls.Add(this.tableStatsRow, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 2);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 3;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
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
            this.panelHeader.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
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
            this.lblPageSubtitle.Size = new System.Drawing.Size(420, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Welcome - browse medicines, manage your cart, and track orders.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(178, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Customer Home";
            // 
            // tableStatsRow
            // 
            this.tableStatsRow.ColumnCount = 3;
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableStatsRow.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableStatsRow.Controls.Add(this.panelStatCart, 0, 0);
            this.tableStatsRow.Controls.Add(this.panelStatOrders, 1, 0);
            this.tableStatsRow.Controls.Add(this.panelStatPromotions, 2, 0);
            this.tableStatsRow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableStatsRow.Location = new System.Drawing.Point(0, 96);
            this.tableStatsRow.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.tableStatsRow.Name = "tableStatsRow";
            this.tableStatsRow.RowCount = 1;
            this.tableStatsRow.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableStatsRow.Size = new System.Drawing.Size(1012, 90);
            this.tableStatsRow.TabIndex = 1;
            // 
            // panelStatCart
            // 
            this.panelStatCart.BackColor = System.Drawing.Color.White;
            this.panelStatCart.Controls.Add(this.lblCart);
            this.panelStatCart.Controls.Add(this.lblStatCartTitle);
            this.panelStatCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatCart.Location = new System.Drawing.Point(0, 0);
            this.panelStatCart.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatCart.Name = "panelStatCart";
            this.panelStatCart.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatCart.Size = new System.Drawing.Size(323, 90);
            this.panelStatCart.TabIndex = 0;
            // 
            // lblCart
            // 
            this.lblCart.BackColor = System.Drawing.Color.White;
            this.lblCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCart.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblCart.Location = new System.Drawing.Point(16, 30);
            this.lblCart.Name = "lblCart";
            this.lblCart.Size = new System.Drawing.Size(293, 46);
            this.lblCart.TabIndex = 1;
            this.lblCart.Text = "-";
            this.lblCart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatCartTitle
            // 
            this.lblStatCartTitle.BackColor = System.Drawing.Color.White;
            this.lblStatCartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatCartTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatCartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatCartTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatCartTitle.Name = "lblStatCartTitle";
            this.lblStatCartTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatCartTitle.TabIndex = 0;
            this.lblStatCartTitle.Text = "ITEMS IN CART";
            // 
            // panelStatOrders
            // 
            this.panelStatOrders.BackColor = System.Drawing.Color.White;
            this.panelStatOrders.Controls.Add(this.lblOrders);
            this.panelStatOrders.Controls.Add(this.lblStatOrdersTitle);
            this.panelStatOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatOrders.Location = new System.Drawing.Point(337, 0);
            this.panelStatOrders.Margin = new System.Windows.Forms.Padding(0, 0, 14, 0);
            this.panelStatOrders.Name = "panelStatOrders";
            this.panelStatOrders.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatOrders.Size = new System.Drawing.Size(323, 90);
            this.panelStatOrders.TabIndex = 1;
            // 
            // lblOrders
            // 
            this.lblOrders.BackColor = System.Drawing.Color.White;
            this.lblOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOrders.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblOrders.Location = new System.Drawing.Point(16, 30);
            this.lblOrders.Name = "lblOrders";
            this.lblOrders.Size = new System.Drawing.Size(293, 46);
            this.lblOrders.TabIndex = 1;
            this.lblOrders.Text = "-";
            this.lblOrders.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatOrdersTitle
            // 
            this.lblStatOrdersTitle.BackColor = System.Drawing.Color.White;
            this.lblStatOrdersTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatOrdersTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatOrdersTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatOrdersTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatOrdersTitle.Name = "lblStatOrdersTitle";
            this.lblStatOrdersTitle.Size = new System.Drawing.Size(293, 16);
            this.lblStatOrdersTitle.TabIndex = 0;
            this.lblStatOrdersTitle.Text = "ACTIVE ORDERS";
            // 
            // panelStatPromotions
            // 
            this.panelStatPromotions.BackColor = System.Drawing.Color.White;
            this.panelStatPromotions.Controls.Add(this.lblPromotions);
            this.panelStatPromotions.Controls.Add(this.lblStatPromotionsTitle);
            this.panelStatPromotions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelStatPromotions.Location = new System.Drawing.Point(674, 0);
            this.panelStatPromotions.Name = "panelStatPromotions";
            this.panelStatPromotions.Padding = new System.Windows.Forms.Padding(16, 14, 14, 14);
            this.panelStatPromotions.Size = new System.Drawing.Size(338, 90);
            this.panelStatPromotions.TabIndex = 2;
            // 
            // lblPromotions
            // 
            this.lblPromotions.BackColor = System.Drawing.Color.White;
            this.lblPromotions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPromotions.Font = new System.Drawing.Font("Hanken Grotesk", 22F, System.Drawing.FontStyle.Bold);
            this.lblPromotions.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPromotions.Location = new System.Drawing.Point(16, 30);
            this.lblPromotions.Name = "lblPromotions";
            this.lblPromotions.Size = new System.Drawing.Size(308, 46);
            this.lblPromotions.TabIndex = 1;
            this.lblPromotions.Text = "-";
            this.lblPromotions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatPromotionsTitle
            // 
            this.lblStatPromotionsTitle.BackColor = System.Drawing.Color.White;
            this.lblStatPromotionsTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatPromotionsTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblStatPromotionsTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblStatPromotionsTitle.Location = new System.Drawing.Point(16, 14);
            this.lblStatPromotionsTitle.Name = "lblStatPromotionsTitle";
            this.lblStatPromotionsTitle.Size = new System.Drawing.Size(308, 16);
            this.lblStatPromotionsTitle.TabIndex = 0;
            this.lblStatPromotionsTitle.Text = "PROMOTIONS";
            // 
            // panelGridOuter
            // 
            this.panelGridOuter.BackColor = System.Drawing.Color.White;
            this.panelGridOuter.Controls.Add(this.panelGridBody);
            this.panelGridOuter.Controls.Add(this.panelGridHeader);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 202);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 16);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 258);
            this.panelGridOuter.TabIndex = 2;
            // 
            // panelGridBody
            // 
            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.gridRecent);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(1, 37);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 220);
            this.panelGridBody.TabIndex = 1;
            // 
            // gridRecent
            // 
            this.gridRecent.AllowUserToAddRows = false;
            this.gridRecent.AllowUserToDeleteRows = false;
            this.gridRecent.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridRecent.BackgroundColor = System.Drawing.Color.White;
            this.gridRecent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridRecent.ColumnHeadersHeight = 36;
            this.gridRecent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridRecent.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridRecent.Location = new System.Drawing.Point(0, 4);
            this.gridRecent.Name = "gridRecent";
            this.gridRecent.ReadOnly = true;
            this.gridRecent.RowHeadersVisible = false;
            this.gridRecent.RowTemplate.Height = 36;
            this.gridRecent.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridRecent.Size = new System.Drawing.Size(1010, 216);
            this.gridRecent.TabIndex = 0;
            // 
            // panelGridHeader
            // 
            this.panelGridHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.panelGridHeader.Controls.Add(this.lblGridTitle);
            this.panelGridHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGridHeader.Location = new System.Drawing.Point(1, 1);
            this.panelGridHeader.Name = "panelGridHeader";
            this.panelGridHeader.Padding = new System.Windows.Forms.Padding(12, 8, 10, 4);
            this.panelGridHeader.Size = new System.Drawing.Size(1010, 36);
            this.panelGridHeader.TabIndex = 0;
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.AutoSize = true;
            this.lblGridTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.lblGridTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblGridTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblGridTitle.Location = new System.Drawing.Point(12, 10);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Size = new System.Drawing.Size(96, 16);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Recent Orders";
            // 
            // CustomerDashboardForm
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
            this.Name = "CustomerDashboardForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Customer Home";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.tableLayoutRoot.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.tableStatsRow.ResumeLayout(false);
            this.panelStatCart.ResumeLayout(false);
            this.panelStatOrders.ResumeLayout(false);
            this.panelStatPromotions.ResumeLayout(false);
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridRecent)).EndInit();
            this.panelGridHeader.ResumeLayout(false);
            this.panelGridHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.TableLayoutPanel tableStatsRow;
        private System.Windows.Forms.Panel panelStatCart;
        private System.Windows.Forms.Label lblCart;
        private System.Windows.Forms.Label lblStatCartTitle;
        private System.Windows.Forms.Panel panelStatOrders;
        private System.Windows.Forms.Label lblOrders;
        private System.Windows.Forms.Label lblStatOrdersTitle;
        private System.Windows.Forms.Panel panelStatPromotions;
        private System.Windows.Forms.Label lblPromotions;
        private System.Windows.Forms.Label lblStatPromotionsTitle;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView gridRecent;
        private System.Windows.Forms.Panel panelGridHeader;
        private System.Windows.Forms.Label lblGridTitle;
    }
}
