namespace SmartMed.UI.Views
{
    partial class SearchMedicinesView
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
            this.pageHeader = new System.Windows.Forms.Panel();
            this.filterPanel = new System.Windows.Forms.Panel();
            this.gridCard = new System.Windows.Forms.Panel();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtMaxPrice = new System.Windows.Forms.TextBox();
            this.lblMax = new System.Windows.Forms.Label();
            this.txtMinPrice = new System.Windows.Forms.TextBox();
            this.lblMin = new System.Windows.Forms.Label();
            this.txtCategory = new System.Windows.Forms.TextBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.grid = new System.Windows.Forms.DataGridView();
            this.filterPanel.SuspendLayout();
            this.gridCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // pageHeader
            // 
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.Location = new System.Drawing.Point(0, 0);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Size = new System.Drawing.Size(812, 44);
            this.pageHeader.TabIndex = 0;
            // 
            // filterPanel
            // 
            this.filterPanel.Controls.Add(this.btnShowAll);
            this.filterPanel.Controls.Add(this.btnSearch);
            this.filterPanel.Controls.Add(this.txtMaxPrice);
            this.filterPanel.Controls.Add(this.lblMax);
            this.filterPanel.Controls.Add(this.txtMinPrice);
            this.filterPanel.Controls.Add(this.lblMin);
            this.filterPanel.Controls.Add(this.txtCategory);
            this.filterPanel.Controls.Add(this.lblCategory);
            this.filterPanel.Controls.Add(this.txtName);
            this.filterPanel.Controls.Add(this.lblName);
            this.filterPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.filterPanel.Location = new System.Drawing.Point(0, 44);
            this.filterPanel.Name = "filterPanel";
            this.filterPanel.Padding = new System.Windows.Forms.Padding(0, 16, 0, 0);
            this.filterPanel.Size = new System.Drawing.Size(812, 80);
            this.filterPanel.TabIndex = 1;
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblName.Location = new System.Drawing.Point(0, 16);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 15);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "Name:";
            // 
            // txtName
            // 
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtName.Location = new System.Drawing.Point(50, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(140, 25);
            this.txtName.TabIndex = 1;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblCategory.Location = new System.Drawing.Point(210, 16);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(64, 15);
            this.lblCategory.TabIndex = 2;
            this.lblCategory.Text = "Category:";
            // 
            // txtCategory
            // 
            this.txtCategory.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtCategory.Location = new System.Drawing.Point(280, 12);
            this.txtCategory.Name = "txtCategory";
            this.txtCategory.Size = new System.Drawing.Size(140, 25);
            this.txtCategory.TabIndex = 3;
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblMin.Location = new System.Drawing.Point(440, 16);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(32, 15);
            this.lblMin.TabIndex = 4;
            this.lblMin.Text = "Min:";
            // 
            // txtMinPrice
            // 
            this.txtMinPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMinPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMinPrice.Location = new System.Drawing.Point(480, 12);
            this.txtMinPrice.Name = "txtMinPrice";
            this.txtMinPrice.Size = new System.Drawing.Size(60, 25);
            this.txtMinPrice.TabIndex = 5;
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblMax.Location = new System.Drawing.Point(550, 16);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(34, 15);
            this.lblMax.TabIndex = 6;
            this.lblMax.Text = "Max:";
            // 
            // txtMaxPrice
            // 
            this.txtMaxPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMaxPrice.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.txtMaxPrice.Location = new System.Drawing.Point(590, 12);
            this.txtMaxPrice.Name = "txtMaxPrice";
            this.txtMaxPrice.Size = new System.Drawing.Size(60, 25);
            this.txtMaxPrice.TabIndex = 7;
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(203)))), ((int)(((byte)(249)))));
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnSearch.Location = new System.Drawing.Point(0, 47);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 36);
            this.btnSearch.TabIndex = 8;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnShowAll
            // 
            this.btnShowAll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(232)))), ((int)(((byte)(232)))));
            this.btnShowAll.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(196)))), ((int)(((byte)(197)))), ((int)(((byte)(212)))));
            this.btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowAll.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnShowAll.Location = new System.Drawing.Point(110, 47);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(100, 36);
            this.btnShowAll.TabIndex = 9;
            this.btnShowAll.Text = "Show All";
            this.btnShowAll.UseVisualStyleBackColor = false;
            this.btnShowAll.Click += new System.EventHandler(this.BtnShowAll_Click);
            // 
            // gridCard
            // 
            this.gridCard.Controls.Add(this.grid);
            this.gridCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCard.Location = new System.Drawing.Point(0, 124);
            this.gridCard.Name = "gridCard";
            this.gridCard.Size = new System.Drawing.Size(812, 500);
            this.gridCard.TabIndex = 2;
            // 
            // grid
            // 
            this.grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowHeadersWidth = 51;
            this.grid.Size = new System.Drawing.Size(812, 500);
            this.grid.TabIndex = 0;
            // 
            // SearchMedicinesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.Controls.Add(this.gridCard);
            this.Controls.Add(this.filterPanel);
            this.Controls.Add(this.pageHeader);
            this.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "SearchMedicinesView";
            this.Size = new System.Drawing.Size(812, 624);
            this.Load += new System.EventHandler(this.SearchMedicinesView_Load);
            this.filterPanel.ResumeLayout(false);
            this.filterPanel.PerformLayout();
            this.gridCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pageHeader;
        private System.Windows.Forms.Panel filterPanel;
        private System.Windows.Forms.Panel gridCard;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.TextBox txtCategory;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.TextBox txtMinPrice;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.TextBox txtMaxPrice;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.DataGridView grid;
    }
}
