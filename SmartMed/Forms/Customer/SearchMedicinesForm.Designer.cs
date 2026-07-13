namespace SmartMed.UI
{
    partial class SearchMedicinesForm
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
            this.flowFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.lblCategoryLabel = new System.Windows.Forms.Label();
            this.cmbCategory = new System.Windows.Forms.ComboBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblPriceLabel = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.panelGridOuter = new System.Windows.Forms.Panel();
            this.panelGridBody = new System.Windows.Forms.Panel();
            this.grid = new System.Windows.Forms.DataGridView();
            this.panelGridHeader = new System.Windows.Forms.Panel();
            this.lblGridTitle = new System.Windows.Forms.Label();
            this.panelScrollHost.SuspendLayout();
            this.tableLayoutRoot.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.flowFilter.SuspendLayout();
            this.panelGridOuter.SuspendLayout();
            this.panelGridBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
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
            this.tableLayoutRoot.Controls.Add(this.flowFilter, 0, 1);
            this.tableLayoutRoot.Controls.Add(this.panelGridOuter, 0, 2);
            this.tableLayoutRoot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRoot.Location = new System.Drawing.Point(24, 24);
            this.tableLayoutRoot.Name = "tableLayoutRoot";
            this.tableLayoutRoot.RowCount = 3;
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutRoot.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRoot.Size = new System.Drawing.Size(1012, 420);
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
            this.lblPageSubtitle.Size = new System.Drawing.Size(320, 18);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Browse medications and wellness products — add items to your cart.";
            // 
            // lblPageTitle
            // 
            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 20F, System.Drawing.FontStyle.Bold);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(0, 8);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(198, 36);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Browse Catalog";
            // 
            // flowFilter
            // 
            this.flowFilter.AutoSize = true;
            this.flowFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.flowFilter.Controls.Add(this.lblCategoryLabel);
            this.flowFilter.Controls.Add(this.cmbCategory);
            this.flowFilter.Controls.Add(this.txtSearch);
            this.flowFilter.Controls.Add(this.btnSearch);
            this.flowFilter.Controls.Add(this.lblPriceLabel);
            this.flowFilter.Controls.Add(this.txtMinPrice);
            this.flowFilter.Controls.Add(this.txtMaxPrice);
            this.flowFilter.Controls.Add(this.btnClear);
            this.flowFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowFilter.Location = new System.Drawing.Point(0, 92);
            this.flowFilter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.flowFilter.Name = "flowFilter";
            this.flowFilter.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.flowFilter.Size = new System.Drawing.Size(1012, 30);
            this.flowFilter.TabIndex = 1;
            this.flowFilter.WrapContents = false;
            // 
            // lblCategoryLabel
            // 
            this.lblCategoryLabel.AutoSize = true;
            this.lblCategoryLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblCategoryLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblCategoryLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblCategoryLabel.Location = new System.Drawing.Point(0, 8);
            this.lblCategoryLabel.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblCategoryLabel.Name = "lblCategoryLabel";
            this.lblCategoryLabel.Size = new System.Drawing.Size(68, 18);
            this.lblCategoryLabel.TabIndex = 0;
            this.lblCategoryLabel.Text = "Category:";
            // 
            // cmbCategory
            // 
            this.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategory.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.cmbCategory.FormattingEnabled = true;
            this.cmbCategory.Items.AddRange(new object[] {
            "All categories"});
            this.cmbCategory.Location = new System.Drawing.Point(72, 4);
            this.cmbCategory.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.cmbCategory.Name = "cmbCategory";
            this.cmbCategory.Size = new System.Drawing.Size(130, 26);
            this.cmbCategory.TabIndex = 1;
            this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.CmbCategory_SelectedIndexChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtSearch.Location = new System.Drawing.Point(210, 4);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(140, 25);
            this.txtSearch.TabIndex = 2;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtSearch_KeyDown);
            this.txtSearch.TextChanged += new System.EventHandler(this.TxtSearch_TextChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearch.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.Location = new System.Drawing.Point(358, 4);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(72, 26);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // lblPriceLabel
            // 
            this.lblPriceLabel.AutoSize = true;
            this.lblPriceLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.lblPriceLabel.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.lblPriceLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblPriceLabel.Location = new System.Drawing.Point(438, 8);
            this.lblPriceLabel.Margin = new System.Windows.Forms.Padding(0, 6, 4, 0);
            this.lblPriceLabel.Name = "lblPriceLabel";
            this.lblPriceLabel.Size = new System.Drawing.Size(40, 18);
            this.lblPriceLabel.TabIndex = 4;
            this.lblPriceLabel.Text = "Price:";
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtMinPrice.Location = new System.Drawing.Point(482, 4);
            this.txtMinPrice.Margin = new System.Windows.Forms.Padding(0, 2, 4, 0);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.Size = new System.Drawing.Size(56, 25);
            this.txtMinPrice.TabIndex = 5;
            this.txtMinPrice.TextChanged += new System.EventHandler(this.TxtMinPrice_TextChanged);
            // 
            // txtMaxPrice
            // 
            this.txtMaxPrice.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.txtMaxPrice.Location = new System.Drawing.Point(542, 4);
            this.txtMaxPrice.Margin = new System.Windows.Forms.Padding(0, 2, 8, 0);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.Size = new System.Drawing.Size(56, 25);
            this.txtMaxPrice.TabIndex = 6;
            this.txtMaxPrice.TextChanged += new System.EventHandler(this.TxtMaxPrice_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Hanken Grotesk", 9F);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnClear.Location = new System.Drawing.Point(606, 4);
            this.btnClear.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(64, 26);
            this.btnClear.TabIndex = 7;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            this.btnClear.Click += new System.EventHandler(this.BtnClear_Click);
            // 
            // panelGridOuter
            // 
            this.panelGridOuter.BackColor = System.Drawing.Color.White;
            this.panelGridOuter.Controls.Add(this.panelGridBody);
            this.panelGridOuter.Controls.Add(this.panelGridHeader);
            this.panelGridOuter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridOuter.Location = new System.Drawing.Point(0, 134);
            this.panelGridOuter.Margin = new System.Windows.Forms.Padding(0, 0, 0, 12);
            this.panelGridOuter.Name = "panelGridOuter";
            this.panelGridOuter.Padding = new System.Windows.Forms.Padding(1);
            this.panelGridOuter.Size = new System.Drawing.Size(1012, 318);
            this.panelGridOuter.TabIndex = 2;
            this.panelGridOuter.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelGridOuter_Paint);
            // 
            // panelGridBody
            // 
            this.panelGridBody.BackColor = System.Drawing.Color.White;
            this.panelGridBody.Controls.Add(this.grid);
            this.panelGridBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGridBody.Location = new System.Drawing.Point(1, 37);
            this.panelGridBody.Name = "panelGridBody";
            this.panelGridBody.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panelGridBody.Size = new System.Drawing.Size(1010, 280);
            this.panelGridBody.TabIndex = 1;
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grid.BackgroundColor = System.Drawing.Color.White;
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.ColumnHeadersHeight = 36;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.grid.Location = new System.Drawing.Point(0, 4);
            this.grid.Name = "grid";
            this.grid.ReadOnly = false;
            this.grid.RowHeadersVisible = false;
            this.grid.RowTemplate.Height = 36;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(1010, 276);
            this.grid.TabIndex = 0;
            this.grid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellContentClick);
            this.grid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Grid_CellDoubleClick);
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
            this.lblGridTitle.Size = new System.Drawing.Size(108, 16);
            this.lblGridTitle.TabIndex = 0;
            this.lblGridTitle.Text = "Product & Service Catalog";
            // 
            // SearchMedicinesForm
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
            this.Name = "SearchMedicinesForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Browse Medicines";
            this.panelScrollHost.ResumeLayout(false);
            this.panelScrollHost.PerformLayout();
            this.tableLayoutRoot.ResumeLayout(false);
            this.tableLayoutRoot.PerformLayout();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.flowFilter.ResumeLayout(false);
            this.flowFilter.PerformLayout();
            this.panelGridOuter.ResumeLayout(false);
            this.panelGridBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
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
        private System.Windows.Forms.FlowLayoutPanel flowFilter;
        private System.Windows.Forms.Label lblCategoryLabel;
        private System.Windows.Forms.ComboBox cmbCategory;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label lblPriceLabel;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Panel panelGridOuter;
        private System.Windows.Forms.Panel panelGridBody;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.Panel panelGridHeader;
        private System.Windows.Forms.Label lblGridTitle;
    }
}
