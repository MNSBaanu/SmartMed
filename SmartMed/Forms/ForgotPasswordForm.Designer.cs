namespace SmartMed.UI
{
    partial class ForgotPasswordForm
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
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelCard = new System.Windows.Forms.Panel();
            this.panelActions = new System.Windows.Forms.Panel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.panelBody = new System.Windows.Forms.Panel();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.lblConfirm = new System.Windows.Forms.Label();
            this.txtNew = new System.Windows.Forms.TextBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.txtCurrent = new System.Windows.Forms.TextBox();
            this.lblCurrent = new System.Windows.Forms.Label();
            this.txtIdentity = new System.Windows.Forms.TextBox();
            this.lblIdentity = new System.Windows.Forms.Label();
            this.lblPageSubtitle = new System.Windows.Forms.Label();
            this.lblPageTitle = new System.Windows.Forms.Label();
            this.panelCardHeader = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblCardTitle = new System.Windows.Forms.Label();
            this.pnlHeaderIcon = new System.Windows.Forms.Panel();
            this.lblHeaderIcon = new System.Windows.Forms.Label();
            this.panelMain.SuspendLayout();
            this.panelCard.SuspendLayout();
            this.panelActions.SuspendLayout();
            this.panelBody.SuspendLayout();
            this.panelCardHeader.SuspendLayout();
            this.pnlHeaderIcon.SuspendLayout();
            this.SuspendLayout();

            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.panelMain.Controls.Add(this.panelCard);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(484, 536);
            this.panelMain.TabIndex = 0;
            this.panelMain.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelMain_Paint);

            this.panelCard.BackColor = System.Drawing.Color.White;
            this.panelCard.Controls.Add(this.panelBody);
            this.panelCard.Controls.Add(this.panelActions);
            this.panelCard.Controls.Add(this.panelCardHeader);
            this.panelCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCard.Location = new System.Drawing.Point(20, 20);
            this.panelCard.Name = "panelCard";
            this.panelCard.Size = new System.Drawing.Size(444, 496);
            this.panelCard.TabIndex = 0;

            this.panelActions.BackColor = System.Drawing.Color.White;
            this.panelActions.Controls.Add(this.btnSave);
            this.panelActions.Controls.Add(this.btnCancel);
            this.panelActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelActions.Location = new System.Drawing.Point(0, 440);
            this.panelActions.Name = "panelActions";
            this.panelActions.Padding = new System.Windows.Forms.Padding(24, 8, 24, 16);
            this.panelActions.Size = new System.Drawing.Size(444, 56);
            this.panelActions.TabIndex = 2;

            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(103)))), ((int)(((byte)(94)))));
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(27)))), ((int)(((byte)(79)))), ((int)(((byte)(71)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(252, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 32);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Reset";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.BtnSave_Click);

            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(200)))), ((int)(((byte)(198)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.btnCancel.Location = new System.Drawing.Point(340, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 32);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);

            this.panelBody.BackColor = System.Drawing.Color.White;
            this.panelBody.Controls.Add(this.txtConfirm);
            this.panelBody.Controls.Add(this.lblConfirm);
            this.panelBody.Controls.Add(this.txtNew);
            this.panelBody.Controls.Add(this.lblNew);
            this.panelBody.Controls.Add(this.txtCurrent);
            this.panelBody.Controls.Add(this.lblCurrent);
            this.panelBody.Controls.Add(this.txtIdentity);
            this.panelBody.Controls.Add(this.lblIdentity);
            this.panelBody.Controls.Add(this.lblPageSubtitle);
            this.panelBody.Controls.Add(this.lblPageTitle);
            this.panelBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelBody.Location = new System.Drawing.Point(0, 40);
            this.panelBody.Name = "panelBody";
            this.panelBody.Padding = new System.Windows.Forms.Padding(24, 16, 24, 8);
            this.panelBody.Size = new System.Drawing.Size(444, 400);
            this.panelBody.TabIndex = 1;

            this.txtConfirm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtConfirm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtConfirm.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtConfirm.Location = new System.Drawing.Point(24, 328);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(396, 40);
            this.txtConfirm.TabIndex = 9;

            this.lblConfirm.AutoSize = true;
            this.lblConfirm.BackColor = System.Drawing.Color.White;
            this.lblConfirm.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblConfirm.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblConfirm.Location = new System.Drawing.Point(24, 308);
            this.lblConfirm.Name = "lblConfirm";
            this.lblConfirm.Size = new System.Drawing.Size(128, 15);
            this.lblConfirm.TabIndex = 8;
            this.lblConfirm.Text = "CONFIRM PASSWORD";

            this.txtNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNew.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtNew.Location = new System.Drawing.Point(24, 252);
            this.txtNew.Name = "txtNew";
            this.txtNew.Size = new System.Drawing.Size(396, 40);
            this.txtNew.TabIndex = 7;

            this.lblNew.AutoSize = true;
            this.lblNew.BackColor = System.Drawing.Color.White;
            this.lblNew.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblNew.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblNew.Location = new System.Drawing.Point(24, 232);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(96, 15);
            this.lblNew.TabIndex = 6;
            this.lblNew.Text = "NEW PASSWORD";

            this.txtCurrent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtCurrent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCurrent.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtCurrent.Location = new System.Drawing.Point(24, 176);
            this.txtCurrent.Name = "txtCurrent";
            this.txtCurrent.Size = new System.Drawing.Size(396, 40);
            this.txtCurrent.TabIndex = 5;

            this.lblCurrent.AutoSize = true;
            this.lblCurrent.BackColor = System.Drawing.Color.White;
            this.lblCurrent.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCurrent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblCurrent.Location = new System.Drawing.Point(24, 156);
            this.lblCurrent.Name = "lblCurrent";
            this.lblCurrent.Size = new System.Drawing.Size(120, 15);
            this.lblCurrent.TabIndex = 4;
            this.lblCurrent.Text = "CURRENT PASSWORD";

            this.txtIdentity.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(245)))), ((int)(((byte)(244)))));
            this.txtIdentity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtIdentity.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtIdentity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(29)))), ((int)(((byte)(29)))));
            this.txtIdentity.Location = new System.Drawing.Point(24, 100);
            this.txtIdentity.Name = "txtIdentity";
            this.txtIdentity.Size = new System.Drawing.Size(396, 40);
            this.txtIdentity.TabIndex = 3;

            this.lblIdentity.AutoSize = true;
            this.lblIdentity.BackColor = System.Drawing.Color.White;
            this.lblIdentity.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblIdentity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(65)))), ((int)(((byte)(72)))), ((int)(((byte)(71)))));
            this.lblIdentity.Location = new System.Drawing.Point(24, 80);
            this.lblIdentity.Name = "lblIdentity";
            this.lblIdentity.Size = new System.Drawing.Size(136, 15);
            this.lblIdentity.TabIndex = 2;
            this.lblIdentity.Text = "EMAIL OR USERNAME";

            this.lblPageSubtitle.AutoSize = true;
            this.lblPageSubtitle.BackColor = System.Drawing.Color.White;
            this.lblPageSubtitle.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblPageSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(56)))), ((int)(((byte)(55)))));
            this.lblPageSubtitle.Location = new System.Drawing.Point(24, 48);
            this.lblPageSubtitle.Name = "lblPageSubtitle";
            this.lblPageSubtitle.Size = new System.Drawing.Size(390, 15);
            this.lblPageSubtitle.TabIndex = 1;
            this.lblPageSubtitle.Text = "Enter your email/username, current password, and a new password.";

            this.lblPageTitle.AutoSize = true;
            this.lblPageTitle.BackColor = System.Drawing.Color.White;
            this.lblPageTitle.Font = new System.Drawing.Font("Hanken Grotesk", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblPageTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblPageTitle.Location = new System.Drawing.Point(24, 16);
            this.lblPageTitle.Name = "lblPageTitle";
            this.lblPageTitle.Size = new System.Drawing.Size(150, 20);
            this.lblPageTitle.TabIndex = 0;
            this.lblPageTitle.Text = "Forgot Password";

            this.panelCardHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.panelCardHeader.Controls.Add(this.btnClose);
            this.panelCardHeader.Controls.Add(this.lblCardTitle);
            this.panelCardHeader.Controls.Add(this.pnlHeaderIcon);
            this.panelCardHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCardHeader.Location = new System.Drawing.Point(0, 0);
            this.panelCardHeader.Name = "panelCardHeader";
            this.panelCardHeader.Size = new System.Drawing.Size(444, 40);
            this.panelCardHeader.TabIndex = 0;

            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Hanken Grotesk", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(404, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 28);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "\u00d7";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.BtnClose_Click);

            this.lblCardTitle.AutoSize = true;
            this.lblCardTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(24)))), ((int)(((byte)(22)))));
            this.lblCardTitle.Font = new System.Drawing.Font("Hanken Grotesk", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblCardTitle.ForeColor = System.Drawing.Color.White;
            this.lblCardTitle.Location = new System.Drawing.Point(40, 12);
            this.lblCardTitle.Name = "lblCardTitle";
            this.lblCardTitle.Size = new System.Drawing.Size(200, 15);
            this.lblCardTitle.TabIndex = 1;
            this.lblCardTitle.Text = "SmartMed \u2014 Account Security";

            this.pnlHeaderIcon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(46)))), ((int)(((byte)(43)))));
            this.pnlHeaderIcon.Controls.Add(this.lblHeaderIcon);
            this.pnlHeaderIcon.Location = new System.Drawing.Point(12, 10);
            this.pnlHeaderIcon.Name = "pnlHeaderIcon";
            this.pnlHeaderIcon.Size = new System.Drawing.Size(24, 20);
            this.pnlHeaderIcon.TabIndex = 0;

            this.lblHeaderIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeaderIcon.Font = new System.Drawing.Font("Hanken Grotesk", 6.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblHeaderIcon.ForeColor = System.Drawing.Color.White;
            this.lblHeaderIcon.Location = new System.Drawing.Point(0, 0);
            this.lblHeaderIcon.Name = "lblHeaderIcon";
            this.lblHeaderIcon.Size = new System.Drawing.Size(24, 20);
            this.lblHeaderIcon.TabIndex = 0;
            this.lblHeaderIcon.Text = "Rx";
            this.lblHeaderIcon.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            this.AcceptButton = this.btnSave;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(251)))), ((int)(((byte)(250)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(484, 536);
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Hanken Grotesk", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ForgotPasswordForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Forgot Password";
            this.panelMain.ResumeLayout(false);
            this.panelCard.ResumeLayout(false);
            this.panelActions.ResumeLayout(false);
            this.panelBody.ResumeLayout(false);
            this.panelBody.PerformLayout();
            this.panelCardHeader.ResumeLayout(false);
            this.panelCardHeader.PerformLayout();
            this.pnlHeaderIcon.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelCard;
        private System.Windows.Forms.Panel panelCardHeader;
        private System.Windows.Forms.Panel pnlHeaderIcon;
        private System.Windows.Forms.Label lblHeaderIcon;
        private System.Windows.Forms.Label lblCardTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Panel panelBody;
        private System.Windows.Forms.Label lblPageTitle;
        private System.Windows.Forms.Label lblPageSubtitle;
        private System.Windows.Forms.Label lblIdentity;
        private System.Windows.Forms.TextBox txtIdentity;
        private System.Windows.Forms.Label lblCurrent;
        private System.Windows.Forms.TextBox txtCurrent;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.TextBox txtNew;
        private System.Windows.Forms.Label lblConfirm;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.Panel panelActions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
    }
}
