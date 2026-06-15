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
            this.headerPanel = new System.Windows.Forms.Panel();
            this.txtGlobalSearch = new System.Windows.Forms.TextBox();
            this.lblHeaderTitle = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.contentHost = new System.Windows.Forms.Panel();
            this.searchMedicinesPreview = new SmartMed.UI.Views.SearchMedicinesView();
            this.sidebarPanel = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.btnTrack = new System.Windows.Forms.Button();
            this.btnPlace = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblBrandSubtitle = new System.Windows.Forms.Label();
            this.lblBrandTitle = new System.Windows.Forms.Label();
            this.headerPanel.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.contentHost.SuspendLayout();
            this.sidebarPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // headerPanel
            // 
            this.headerPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.headerPanel.Controls.Add(this.txtGlobalSearch);
            this.headerPanel.Controls.Add(this.lblHeaderTitle);
            this.headerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.headerPanel.Location = new System.Drawing.Point(0, 0);
            this.headerPanel.Name = "headerPanel";
            this.headerPanel.Size = new System.Drawing.Size(1280, 48);
            this.headerPanel.TabIndex = 0;
            // 
            // lblHeaderTitle
            // 
            this.lblHeaderTitle.AutoSize = true;
            this.lblHeaderTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblHeaderTitle.ForeColor = System.Drawing.Color.White;
            this.lblHeaderTitle.Location = new System.Drawing.Point(16, 12);
            this.lblHeaderTitle.Name = "lblHeaderTitle";
            this.lblHeaderTitle.Size = new System.Drawing.Size(230, 25);
            this.lblHeaderTitle.TabIndex = 0;
            this.lblHeaderTitle.Text = "SmartMed - Customer Portal";
            // 
            // txtGlobalSearch
            // 
            this.txtGlobalSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGlobalSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(50)))), ((int)(((byte)(150)))));
            this.txtGlobalSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGlobalSearch.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.txtGlobalSearch.ForeColor = System.Drawing.Color.White;
            this.txtGlobalSearch.Location = new System.Drawing.Point(1000, 12);
            this.txtGlobalSearch.Name = "txtGlobalSearch";
            this.txtGlobalSearch.Size = new System.Drawing.Size(260, 23);
            this.txtGlobalSearch.TabIndex = 1;
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.contentHost);
            this.panelMain.Controls.Add(this.sidebarPanel);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 48);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1280, 672);
            this.panelMain.TabIndex = 1;
            // 
            // contentHost
            // 
            this.contentHost.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.contentHost.Controls.Add(this.searchMedicinesPreview);
            this.contentHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentHost.Location = new System.Drawing.Point(420, 0);
            this.contentHost.Name = "contentHost";
            this.contentHost.Padding = new System.Windows.Forms.Padding(24);
            this.contentHost.Size = new System.Drawing.Size(860, 672);
            this.contentHost.TabIndex = 1;
            // 
            // searchMedicinesPreview
            // 
            this.searchMedicinesPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchMedicinesPreview.Location = new System.Drawing.Point(24, 24);
            this.searchMedicinesPreview.Name = "searchMedicinesPreview";
            this.searchMedicinesPreview.Size = new System.Drawing.Size(812, 624);
            this.searchMedicinesPreview.TabIndex = 0;
            // 
            // sidebarPanel
            // 
            this.sidebarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.sidebarPanel.Controls.Add(this.btnLogout);
            this.sidebarPanel.Controls.Add(this.btnProfile);
            this.sidebarPanel.Controls.Add(this.btnTrack);
            this.sidebarPanel.Controls.Add(this.btnPlace);
            this.sidebarPanel.Controls.Add(this.btnSearch);
            this.sidebarPanel.Controls.Add(this.lblBrandSubtitle);
            this.sidebarPanel.Controls.Add(this.lblBrandTitle);
            this.sidebarPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.sidebarPanel.Location = new System.Drawing.Point(0, 0);
            this.sidebarPanel.Name = "sidebarPanel";
            this.sidebarPanel.Padding = new System.Windows.Forms.Padding(24, 16, 24, 24);
            this.sidebarPanel.Size = new System.Drawing.Size(420, 672);
            this.sidebarPanel.TabIndex = 0;
            // 
            // lblBrandTitle
            // 
            this.lblBrandTitle.AutoSize = true;
            this.lblBrandTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblBrandTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(31)))), ((int)(((byte)(102)))));
            this.lblBrandTitle.Location = new System.Drawing.Point(24, 16);
            this.lblBrandTitle.Name = "lblBrandTitle";
            this.lblBrandTitle.Size = new System.Drawing.Size(78, 21);
            this.lblBrandTitle.TabIndex = 0;
            this.lblBrandTitle.Text = "SmartMed";
            // 
            // lblBrandSubtitle
            // 
            this.lblBrandSubtitle.AutoSize = true;
            this.lblBrandSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblBrandSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(70)))), ((int)(((byte)(82)))));
            this.lblBrandSubtitle.Location = new System.Drawing.Point(24, 40);
            this.lblBrandSubtitle.Name = "lblBrandSubtitle";
            this.lblBrandSubtitle.Size = new System.Drawing.Size(130, 15);
            this.lblBrandSubtitle.TabIndex = 1;
            this.lblBrandSubtitle.Text = "Patient Management";
            // 
            // btnSearch
            // 
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Location = new System.Drawing.Point(24, 88);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(372, 40);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "Search Medicines";
            this.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.BtnSearch_Click);
            // 
            // btnPlace
            // 
            this.btnPlace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlace.Location = new System.Drawing.Point(24, 132);
            this.btnPlace.Name = "btnPlace";
            this.btnPlace.Size = new System.Drawing.Size(372, 40);
            this.btnPlace.TabIndex = 3;
            this.btnPlace.Text = "Place Order";
            this.btnPlace.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnPlace.UseVisualStyleBackColor = true;
            this.btnPlace.Click += new System.EventHandler(this.BtnPlace_Click);
            // 
            // btnTrack
            // 
            this.btnTrack.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTrack.Location = new System.Drawing.Point(24, 176);
            this.btnTrack.Name = "btnTrack";
            this.btnTrack.Size = new System.Drawing.Size(372, 40);
            this.btnTrack.TabIndex = 4;
            this.btnTrack.Text = "Track Orders";
            this.btnTrack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTrack.UseVisualStyleBackColor = true;
            this.btnTrack.Click += new System.EventHandler(this.BtnTrack_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProfile.Location = new System.Drawing.Point(24, 220);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(372, 40);
            this.btnProfile.TabIndex = 5;
            this.btnProfile.Text = "Manage Profile";
            this.btnProfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.BtnProfile_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(26)))), ((int)(((byte)(26)))));
            this.btnLogout.Location = new System.Drawing.Point(24, 600);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(372, 40);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // CustomerDashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1280, 720);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.headerPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.Name = "CustomerDashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SmartMed - Customer Portal";
            this.Load += new System.EventHandler(this.CustomerDashboardForm_Load);
            this.headerPanel.ResumeLayout(false);
            this.headerPanel.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.contentHost.ResumeLayout(false);
            this.sidebarPanel.ResumeLayout(false);
            this.sidebarPanel.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel headerPanel;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.TextBox txtGlobalSearch;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel sidebarPanel;
        private System.Windows.Forms.Label lblBrandTitle;
        private System.Windows.Forms.Label lblBrandSubtitle;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnPlace;
        private System.Windows.Forms.Button btnTrack;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel contentHost;
        private Views.SearchMedicinesView searchMedicinesPreview;
    }
}
