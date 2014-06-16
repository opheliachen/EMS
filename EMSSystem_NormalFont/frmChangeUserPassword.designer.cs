namespace EMSSystem
{
    partial class frmChangeUserPassword
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
            this.panelChangePassword = new System.Windows.Forms.Panel();
            this.lblInvisibleCurrentMasterKey = new System.Windows.Forms.Label();
            this.lblInvisibleCurrentPassword = new System.Windows.Forms.Label();
            this.lblInvisibleUserName = new System.Windows.Forms.Label();
            this.lblErrorMsgIsShow = new System.Windows.Forms.Label();
            this.lblNewPassword = new System.Windows.Forms.Label();
            this.btnCancelChangePassword = new System.Windows.Forms.Button();
            this.btnChangePassword = new System.Windows.Forms.Button();
            this.lblChangePassword = new System.Windows.Forms.Label();
            this.lblCurrentPassword = new System.Windows.Forms.Label();
            this.lblNewMasterKey = new System.Windows.Forms.Label();
            this.lblConfirmPassword = new System.Windows.Forms.Label();
            this.txtCurrentPassword = new System.Windows.Forms.TextBox();
            this.txtNewMasterKey = new System.Windows.Forms.TextBox();
            this.txtConfirmPassword = new System.Windows.Forms.TextBox();
            this.txtNewPassword = new System.Windows.Forms.TextBox();
            this.panelChangePassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelChangePassword
            // 
            this.panelChangePassword.BackColor = System.Drawing.Color.Navy;
            this.panelChangePassword.Controls.Add(this.lblInvisibleCurrentMasterKey);
            this.panelChangePassword.Controls.Add(this.lblInvisibleCurrentPassword);
            this.panelChangePassword.Controls.Add(this.lblInvisibleUserName);
            this.panelChangePassword.Controls.Add(this.lblErrorMsgIsShow);
            this.panelChangePassword.Controls.Add(this.lblNewPassword);
            this.panelChangePassword.Controls.Add(this.btnCancelChangePassword);
            this.panelChangePassword.Controls.Add(this.btnChangePassword);
            this.panelChangePassword.Controls.Add(this.lblChangePassword);
            this.panelChangePassword.Controls.Add(this.lblCurrentPassword);
            this.panelChangePassword.Controls.Add(this.lblNewMasterKey);
            this.panelChangePassword.Controls.Add(this.lblConfirmPassword);
            this.panelChangePassword.Controls.Add(this.txtCurrentPassword);
            this.panelChangePassword.Controls.Add(this.txtNewMasterKey);
            this.panelChangePassword.Controls.Add(this.txtConfirmPassword);
            this.panelChangePassword.Controls.Add(this.txtNewPassword);
            this.panelChangePassword.Location = new System.Drawing.Point(9, -1);
            this.panelChangePassword.Name = "panelChangePassword";
            this.panelChangePassword.Size = new System.Drawing.Size(441, 255);
            this.panelChangePassword.TabIndex = 4;
            // 
            // lblInvisibleCurrentMasterKey
            // 
            this.lblInvisibleCurrentMasterKey.AutoSize = true;
            this.lblInvisibleCurrentMasterKey.Location = new System.Drawing.Point(346, 94);
            this.lblInvisibleCurrentMasterKey.Name = "lblInvisibleCurrentMasterKey";
            this.lblInvisibleCurrentMasterKey.Size = new System.Drawing.Size(160, 19);
            this.lblInvisibleCurrentMasterKey.TabIndex = 88;
            this.lblInvisibleCurrentMasterKey.Text = "CurrentMasterKey";
            this.lblInvisibleCurrentMasterKey.Visible = false;
            // 
            // lblInvisibleCurrentPassword
            // 
            this.lblInvisibleCurrentPassword.AutoSize = true;
            this.lblInvisibleCurrentPassword.Location = new System.Drawing.Point(346, 75);
            this.lblInvisibleCurrentPassword.Name = "lblInvisibleCurrentPassword";
            this.lblInvisibleCurrentPassword.Size = new System.Drawing.Size(148, 19);
            this.lblInvisibleCurrentPassword.TabIndex = 88;
            this.lblInvisibleCurrentPassword.Text = "CurrentPassword";
            this.lblInvisibleCurrentPassword.Visible = false;
            // 
            // lblInvisibleUserName
            // 
            this.lblInvisibleUserName.AutoSize = true;
            this.lblInvisibleUserName.Location = new System.Drawing.Point(346, 56);
            this.lblInvisibleUserName.Name = "lblInvisibleUserName";
            this.lblInvisibleUserName.Size = new System.Drawing.Size(94, 19);
            this.lblInvisibleUserName.TabIndex = 88;
            this.lblInvisibleUserName.Text = "UserName";
            this.lblInvisibleUserName.Visible = false;
            // 
            // lblErrorMsgIsShow
            // 
            this.lblErrorMsgIsShow.AutoSize = true;
            this.lblErrorMsgIsShow.Location = new System.Drawing.Point(243, 89);
            this.lblErrorMsgIsShow.Name = "lblErrorMsgIsShow";
            this.lblErrorMsgIsShow.Size = new System.Drawing.Size(87, 19);
            this.lblErrorMsgIsShow.TabIndex = 88;
            this.lblErrorMsgIsShow.Text = "ErrorMsg";
            this.lblErrorMsgIsShow.Visible = false;
            // 
            // lblNewPassword
            // 
            this.lblNewPassword.AutoSize = true;
            this.lblNewPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblNewPassword.Location = new System.Drawing.Point(56, 92);
            this.lblNewPassword.Name = "lblNewPassword";
            this.lblNewPassword.Size = new System.Drawing.Size(75, 19);
            this.lblNewPassword.TabIndex = 19;
            this.lblNewPassword.Text = "新密碼:";
            // 
            // btnCancelChangePassword
            // 
            this.btnCancelChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelChangePassword.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelChangePassword.FlatAppearance.BorderSize = 3;
            this.btnCancelChangePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelChangePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCancelChangePassword.Location = new System.Drawing.Point(313, 207);
            this.btnCancelChangePassword.Name = "btnCancelChangePassword";
            this.btnCancelChangePassword.Size = new System.Drawing.Size(120, 35);
            this.btnCancelChangePassword.TabIndex = 23;
            this.btnCancelChangePassword.Text = "取 消";
            this.btnCancelChangePassword.UseVisualStyleBackColor = true;
            this.btnCancelChangePassword.Click += new System.EventHandler(this.btnCancelChangePassword_Click);
            // 
            // btnChangePassword
            // 
            this.btnChangePassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChangePassword.FlatAppearance.BorderSize = 3;
            this.btnChangePassword.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChangePassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnChangePassword.Location = new System.Drawing.Point(187, 207);
            this.btnChangePassword.Name = "btnChangePassword";
            this.btnChangePassword.Size = new System.Drawing.Size(120, 35);
            this.btnChangePassword.TabIndex = 22;
            this.btnChangePassword.Text = "確 認";
            this.btnChangePassword.UseVisualStyleBackColor = true;
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            // 
            // lblChangePassword
            // 
            this.lblChangePassword.BackColor = System.Drawing.Color.DarkRed;
            this.lblChangePassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblChangePassword.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChangePassword.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblChangePassword.Location = new System.Drawing.Point(-18, 4);
            this.lblChangePassword.Name = "lblChangePassword";
            this.lblChangePassword.Size = new System.Drawing.Size(477, 40);
            this.lblChangePassword.TabIndex = 18;
            this.lblChangePassword.Text = "00-2.變更使用密碼";
            this.lblChangePassword.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCurrentPassword
            // 
            this.lblCurrentPassword.AutoSize = true;
            this.lblCurrentPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblCurrentPassword.Location = new System.Drawing.Point(36, 56);
            this.lblCurrentPassword.Name = "lblCurrentPassword";
            this.lblCurrentPassword.Size = new System.Drawing.Size(95, 19);
            this.lblCurrentPassword.TabIndex = 19;
            this.lblCurrentPassword.Text = "目前密碼:";
            // 
            // lblNewMasterKey
            // 
            this.lblNewMasterKey.AutoSize = true;
            this.lblNewMasterKey.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblNewMasterKey.Location = new System.Drawing.Point(37, 164);
            this.lblNewMasterKey.Name = "lblNewMasterKey";
            this.lblNewMasterKey.Size = new System.Drawing.Size(95, 19);
            this.lblNewMasterKey.TabIndex = 19;
            this.lblNewMasterKey.Text = "安全密碼:";
            // 
            // lblConfirmPassword
            // 
            this.lblConfirmPassword.AutoSize = true;
            this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblConfirmPassword.Location = new System.Drawing.Point(37, 128);
            this.lblConfirmPassword.Name = "lblConfirmPassword";
            this.lblConfirmPassword.Size = new System.Drawing.Size(95, 19);
            this.lblConfirmPassword.TabIndex = 19;
            this.lblConfirmPassword.Text = "確認密碼:";
            // 
            // txtCurrentPassword
            // 
            this.txtCurrentPassword.Location = new System.Drawing.Point(137, 53);
            this.txtCurrentPassword.MaxLength = 12;
            this.txtCurrentPassword.Name = "txtCurrentPassword";
            this.txtCurrentPassword.PasswordChar = '*';
            this.txtCurrentPassword.Size = new System.Drawing.Size(100, 30);
            this.txtCurrentPassword.TabIndex = 1;
            // 
            // txtNewMasterKey
            // 
            this.txtNewMasterKey.Location = new System.Drawing.Point(137, 161);
            this.txtNewMasterKey.MaxLength = 12;
            this.txtNewMasterKey.Name = "txtNewMasterKey";
            this.txtNewMasterKey.PasswordChar = '*';
            this.txtNewMasterKey.Size = new System.Drawing.Size(100, 30);
            this.txtNewMasterKey.TabIndex = 4;
            // 
            // txtConfirmPassword
            // 
            this.txtConfirmPassword.Location = new System.Drawing.Point(137, 125);
            this.txtConfirmPassword.MaxLength = 12;
            this.txtConfirmPassword.Name = "txtConfirmPassword";
            this.txtConfirmPassword.PasswordChar = '*';
            this.txtConfirmPassword.Size = new System.Drawing.Size(100, 30);
            this.txtConfirmPassword.TabIndex = 3;
            // 
            // txtNewPassword
            // 
            this.txtNewPassword.Location = new System.Drawing.Point(137, 89);
            this.txtNewPassword.MaxLength = 12;
            this.txtNewPassword.Name = "txtNewPassword";
            this.txtNewPassword.PasswordChar = '*';
            this.txtNewPassword.Size = new System.Drawing.Size(100, 30);
            this.txtNewPassword.TabIndex = 2;
            // 
            // frmChangeUserPassword
            // 
            this.AcceptButton = this.btnChangePassword;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.CancelButton = this.btnCancelChangePassword;
            this.ClientSize = new System.Drawing.Size(458, 253);
            this.ControlBox = false;
            this.Controls.Add(this.panelChangePassword);
            this.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "frmChangeUserPassword";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "密碼變更";
            this.panelChangePassword.ResumeLayout(false);
            this.panelChangePassword.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelChangePassword;
        private System.Windows.Forms.Label lblErrorMsgIsShow;
        private System.Windows.Forms.Label lblNewPassword;
        private System.Windows.Forms.Button btnCancelChangePassword;
        private System.Windows.Forms.Button btnChangePassword;
        internal System.Windows.Forms.Label lblChangePassword;
        private System.Windows.Forms.Label lblCurrentPassword;
        private System.Windows.Forms.Label lblConfirmPassword;
        private System.Windows.Forms.TextBox txtCurrentPassword;
        private System.Windows.Forms.TextBox txtConfirmPassword;
        private System.Windows.Forms.TextBox txtNewPassword;
        private System.Windows.Forms.Label lblInvisibleCurrentPassword;
        private System.Windows.Forms.Label lblInvisibleUserName;
        private System.Windows.Forms.Label lblNewMasterKey;
        private System.Windows.Forms.TextBox txtNewMasterKey;
        private System.Windows.Forms.Label lblInvisibleCurrentMasterKey;
    }
}