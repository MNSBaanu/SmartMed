namespace SmartMed.UI
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelScrollHost = new System.Windows.Forms.Panel();
            this.tableLayoutRoot = new System.Windows.Forms.TableLayoutPanel();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.gridCart = new System.Windows.Forms.DataGridView();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.flowActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRemoveSelected = new System.Windows.Forms.Button();
            this.btnClearCart = new System.Windows.Forms.Button();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblRxNote = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).BeginInit();
            this.panelGridHeader.SuspendLayout();
            this.flowActions.SuspendLayout();
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
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.flowActions, 0, 2);
            this.tableLayoutRoot.Controls.Add(this.lblTotal, 0, 3);
            this.tableLayoutRoot.Controls.Add(this.lblRxNote, 0, 4);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 5;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 400);
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
            this.lblPageSubtitle.Size = new System.Drawing.Size(380, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Review items, upload prescriptions, and complete payment.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(220, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "My Cart & Checkout";
            // 
            // panelGridOuter
            // 
            this.panelGridOuter.BackColor = System.Drawing.Color.White;
            this.panelGridOuter.Controls.Add(this.panelGridBody);
            this.panelGridOuter.Controls.Add(this.panelGridHeader);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 92);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 298);
            this.panelGridOuter.TabIndex = 1;
            this.panelGridOuter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelGridOuter_Paint);
            // 
            // panelGridBody
            // 
            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.gridCart);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(1, 37);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 260);
            this.panelGridBody.TabIndex = 1;
            // 
            // gridCart
            // 
            this.gridCart.AllowUserToAddRows = false;
            this.gridCart.AllowUserToDeleteRows = false;
            this.gridCart.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gridCart.BackgroundColor = System.Drawing.Color.White;
            this.gridCart.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.gridCart.ColumnHeadersHeight = 36;
            this.gridCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCart.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.gridCart.Location = new System.Drawing.Point(0, 4);
            this.gridCart.Name = "gridCart";
            this.gridCart.ReadOnly = true;
            this.gridCart.RowHeadersVisible = false;
            this.gridCart.RowTemplate.Height = 36;
            this.gridCart.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gridCart.Size = new System.Drawing.Size(1010, 256);
            this.gridCart.TabIndex = 0;
            this.gridCart.CurrentCellDirtyStateChanged += new System.EventHandler(this.GridCart_CurrentCellDirtyStateChanged);
            this.gridCart.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCart_CellValueChanged);
            this.gridCart.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCart_CellContentClick);
            this.gridCart.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCart_CellClick);
            this.gridCart.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.GridCart_CellFormatting);
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
            this.lblGridTitle.Text = "Shopping Cart";
            // 
            // flowActions
            // 
            this.flowActions.AutoSize = true;
            this.flowActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowActions.Controls.Add(this.btnRemoveSelected);
            this.flowActions.Controls.Add(this.btnClearCart);
            this.flowActions.Controls.Add(this.btnPlaceOrder);
            this.flowActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowActions.Location = new System.Drawing.Point(0, 402);
            this.flowActions.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.flowActions.Name = "flowActions";
            this.flowActions.Size = new System.Drawing.Size(1012, 30);
            this.flowActions.TabIndex = 2;
            this.flowActions.WrapContents = false;
            // 
            // btnRemoveSelected
            // 
            this.btnRemoveSelected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnRemoveSelected.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRemoveSelected.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnRemoveSelected.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveSelected.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnRemoveSelected.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnRemoveSelected.Location = new System.Drawing.Point(0, 0);
            this.btnRemoveSelected.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnRemoveSelected.Name = "btnRemoveSelected";
            this.btnRemoveSelected.Size = new System.Drawing.Size(130, 30);
            this.btnRemoveSelected.TabIndex = 0;
            this.btnRemoveSelected.Text = "Remove Selected";
            this.btnRemoveSelected.UseVisualStyleBackColor = false;
            this.btnRemoveSelected.Click += new System.EventHandler(this.BtnRemoveSelected_Click);
            // 
            // btnClearCart
            // 
            this.btnClearCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnClearCart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearCart.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnClearCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearCart.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnClearCart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnClearCart.Location = new System.Drawing.Point(140, 0);
            this.btnClearCart.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.btnClearCart.Name = "btnClearCart";
            this.btnClearCart.Size = new System.Drawing.Size(100, 30);
            this.btnClearCart.TabIndex = 1;
            this.btnClearCart.Text = "Clear Cart";
            this.btnClearCart.UseVisualStyleBackColor = false;
            this.btnClearCart.Click += new System.EventHandler(this.BtnClearCart_Click);
            // 
            // btnPlaceOrder
            // 
            this.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnPlaceOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPlaceOrder.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnPlaceOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnPlaceOrder.ForeColor = System.Drawing.Color.White;
            this.btnPlaceOrder.Location = new System.Drawing.Point(250, 0);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(150, 30);
            this.btnPlaceOrder.TabIndex = 2;
            this.btnPlaceOrder.Text = "Proceed to Payment";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.BtnPlaceOrder_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblTotal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTotal.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Bold);
            this.lblTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.lblTotal.Location = new System.Drawing.Point(0, 444);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(1012, 28);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Checkout total: -";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblRxNote
            // 
            this.lblRxNote.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblRxNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRxNote.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblRxNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblRxNote.Location = new System.Drawing.Point(0, 484);
            this.lblRxNote.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.lblRxNote.Name = "lblRxNote";
            this.lblRxNote.Size = new System.Drawing.Size(1012, 24);
            this.lblRxNote.TabIndex = 4;
            this.lblRxNote.Text = "Rx medicines require a prescription upload.";
            this.lblRxNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PlaceOrderForm
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
            this.Name = "PlaceOrderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "My Cart & Checkout";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.tableLayoutRoot.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).EndInit();
            this.panelGridHeader.ResumeLayout(false);
            this.panelGridHeader.PerformLayout();
            this.flowActions.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelScrollHost;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRoot;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView gridCart;
        private System.Windows.Forms.Panel panelGridHeader;
        private System.Windows.Forms.Label lblGridTitle;
        private System.Windows.Forms.FlowLayoutPanel flowActions;
        private System.Windows.Forms.Button btnRemoveSelected;
        private System.Windows.Forms.Button btnClearCart;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblRxNote;
    }
}
