namespace EMSSystem
{
    partial class frmSaftyChecker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelContainSaftyChecker = new System.Windows.Forms.Panel();
            this.lblCurrentPage = new System.Windows.Forms.Label();
            this.lblInvisibleStaffPassword = new System.Windows.Forms.Label();
            this.lblInvisibleStaffMasterKey = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnSaftyChecker = new System.Windows.Forms.Button();
            this.txtSaftyCheckerMasterKey = new System.Windows.Forms.TextBox();
            this.txtSaftyCheckerPassword = new System.Windows.Forms.TextBox();
            this.lblSaftyCheckerPassword = new System.Windows.Forms.Label();
            this.panelContainSaftyChecker.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContainSaftyChecker
            // 
            this.panelContainSaftyChecker.BackColor = System.Drawing.Color.Navy;
            this.panelContainSaftyChecker.Controls.Add(this.lblCurrentPage);
            this.panelContainSaftyChecker.Controls.Add(this.lblInvisibleStaffPassword);
            this.panelContainSaftyChecker.Controls.Add(this.lblInvisibleStaffMasterKey);
            this.panelContainSaftyChecker.Controls.Add(this.btnClose);
            this.panelContainSaftyChecker.Controls.Add(this.btnSaftyChecker);
            this.panelContainSaftyChecker.Controls.Add(this.txtSaftyCheckerMasterKey);
            this.panelContainSaftyChecker.Controls.Add(this.txtSaftyCheckerPassword);
            this.panelContainSaftyChecker.Controls.Add(this.lblSaftyCheckerPassword);
            this.panelContainSaftyChecker.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelContainSaftyChecker.Location = new System.Drawing.Point(12, 12);
            this.panelContainSaftyChecker.Name = "panelContainSaftyChecker";
            this.panelContainSaftyChecker.Size = new System.Drawing.Size(361, 96);
            this.panelContainSaftyChecker.TabIndex = 1;
            // 
            // lblCurrentPage
            // 
            this.lblCurrentPage.AutoSize = true;
            this.lblCurrentPage.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblCurrentPage.Location = new System.Drawing.Point(3, 0);
            this.lblCurrentPage.Name = "lblCurrentPage";
            this.lblCurrentPage.Size = new System.Drawing.Size(76, 16);
            this.lblCurrentPage.TabIndex = 29;
            this.lblCurrentPage.Text = "目前頁面";
            this.lblCurrentPage.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCurrentPage.Visible = false;
            // 
            // lblInvisibleStaffPassword
            // 
            this.lblInvisibleStaffPassword.AutoSize = true;
            this.lblInvisibleStaffPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInvisibleStaffPassword.Location = new System.Drawing.Point(133, 0);
            this.lblInvisibleStaffPassword.Name = "lblInvisibleStaffPassword";
            this.lblInvisibleStaffPassword.Size = new System.Drawing.Size(110, 16);
            this.lblInvisibleStaffPassword.TabIndex = 29;
            this.lblInvisibleStaffPassword.Text = "員工登入密碼";
            this.lblInvisibleStaffPassword.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblInvisibleStaffPassword.Visible = false;
            // 
            // lblInvisibleStaffMasterKey
            // 
            this.lblInvisibleStaffMasterKey.AutoSize = true;
            this.lblInvisibleStaffMasterKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInvisibleStaffMasterKey.Location = new System.Drawing.Point(251, 0);
            this.lblInvisibleStaffMasterKey.Name = "lblInvisibleStaffMasterKey";
            this.lblInvisibleStaffMasterKey.Size = new System.Drawing.Size(110, 16);
            this.lblInvisibleStaffMasterKey.TabIndex = 28;
            this.lblInvisibleStaffMasterKey.Text = "員工安全密碼";
            this.lblInvisibleStaffMasterKey.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblInvisibleStaffMasterKey.Visible = false;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(237, 53);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 30);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = " 取 消 ";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSaftyChecker
            // 
            this.btnSaftyChecker.FlatAppearance.BorderSize = 3;
            this.btnSaftyChecker.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnSaftyChecker.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSaftyChecker.Location = new System.Drawing.Point(237, 18);
            this.btnSaftyChecker.Name = "btnSaftyChecker";
            this.btnSaftyChecker.Size = new System.Drawing.Size(100, 30);
            this.btnSaftyChecker.TabIndex = 3;
            this.btnSaftyChecker.Text = " 確 定 ";
            this.btnSaftyChecker.UseVisualStyleBackColor = true;
            this.btnSaftyChecker.Click += new System.EventHandler(this.btnSaftyChecker_Click);
            // 
            // txtSaftyCheckerMasterKey
            // 
            this.txtSaftyCheckerMasterKey.Location = new System.Drawing.Point(76, 53);
            this.txtSaftyCheckerMasterKey.MaxLength = 15;
            this.txtSaftyCheckerMasterKey.Name = "txtSaftyCheckerMasterKey";
            this.txtSaftyCheckerMasterKey.PasswordChar = '*';
            this.txtSaftyCheckerMasterKey.Size = new System.Drawing.Size(154, 27);
            this.txtSaftyCheckerMasterKey.TabIndex = 2;
            this.txtSaftyCheckerMasterKey.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaftyCheckerMasterKey_KeyPress);
            // 
            // txtSaftyCheckerPassword
            // 
            this.txtSaftyCheckerPassword.Location = new System.Drawing.Point(77, 19);
            this.txtSaftyCheckerPassword.MaxLength = 15;
            this.txtSaftyCheckerPassword.Name = "txtSaftyCheckerPassword";
            this.txtSaftyCheckerPassword.PasswordChar = '*';
            this.txtSaftyCheckerPassword.Size = new System.Drawing.Size(154, 27);
            this.txtSaftyCheckerPassword.TabIndex = 1;
            this.txtSaftyCheckerPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSaftyCheckerPassword_KeyPress);
            // 
            // lblSaftyCheckerPassword
            // 
            this.lblSaftyCheckerPassword.AutoSize = true;
            this.lblSaftyCheckerPassword.Location = new System.Drawing.Point(20, 25);
            this.lblSaftyCheckerPassword.Name = "lblSaftyCheckerPassword";
            this.lblSaftyCheckerPassword.Size = new System.Drawing.Size(47, 16);
            this.lblSaftyCheckerPassword.TabIndex = 10;
            this.lblSaftyCheckerPassword.Text = "密碼:";
            // 
            // frmSaftyChecker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Navy;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(386, 119);
            this.ControlBox = false;
            this.Controls.Add(this.panelContainSaftyChecker);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmSaftyChecker";
            this.Opacity = 0.9;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "安全設定";
            this.Load += new System.EventHandler(this.frmSaftyChecker_Load);
            this.panelContainSaftyChecker.ResumeLayout(false);
            this.panelContainSaftyChecker.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelContainSaftyChecker;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnSaftyChecker;
        private System.Windows.Forms.TextBox txtSaftyCheckerMasterKey;
        private System.Windows.Forms.TextBox txtSaftyCheckerPassword;
        private System.Windows.Forms.Label lblSaftyCheckerPassword;
        private System.Windows.Forms.Label lblInvisibleStaffPassword;
        private System.Windows.Forms.Label lblInvisibleStaffMasterKey;
        private System.Windows.Forms.Label lblCurrentPage;
    }
}