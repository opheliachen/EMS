namespace RestoreMySQL
{
    partial class RestoreMySQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RestoreMySQL));
            this.btnRestoreLastTimeBackupByComputer = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRestoreOriginalSetting = new System.Windows.Forms.Button();
            this.btnRestoreLastTimeBackupByUSB = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRestoreLastTimeBackupByComputer
            // 
            this.btnRestoreLastTimeBackupByComputer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreLastTimeBackupByComputer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreLastTimeBackupByComputer.Location = new System.Drawing.Point(15, 90);
            this.btnRestoreLastTimeBackupByComputer.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnRestoreLastTimeBackupByComputer.Name = "btnRestoreLastTimeBackupByComputer";
            this.btnRestoreLastTimeBackupByComputer.Size = new System.Drawing.Size(489, 40);
            this.btnRestoreLastTimeBackupByComputer.TabIndex = 0;
            this.btnRestoreLastTimeBackupByComputer.Text = "還原至前次備份(電腦硬碟)";
            this.btnRestoreLastTimeBackupByComputer.UseVisualStyleBackColor = true;
            this.btnRestoreLastTimeBackupByComputer.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Font = new System.Drawing.Font("PMingLiU", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnClose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClose.Location = new System.Drawing.Point(15, 13);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(489, 69);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "關 閉";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnRestoreOriginalSetting
            // 
            this.btnRestoreOriginalSetting.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreOriginalSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreOriginalSetting.Location = new System.Drawing.Point(15, 186);
            this.btnRestoreOriginalSetting.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnRestoreOriginalSetting.Name = "btnRestoreOriginalSetting";
            this.btnRestoreOriginalSetting.Size = new System.Drawing.Size(489, 40);
            this.btnRestoreOriginalSetting.TabIndex = 0;
            this.btnRestoreOriginalSetting.Text = "還原至初始化";
            this.btnRestoreOriginalSetting.UseVisualStyleBackColor = true;
            this.btnRestoreOriginalSetting.Click += new System.EventHandler(this.btnRestoreOriginalSetting_Click);
            // 
            // btnRestoreLastTimeBackupByUSB
            // 
            this.btnRestoreLastTimeBackupByUSB.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRestoreLastTimeBackupByUSB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestoreLastTimeBackupByUSB.Location = new System.Drawing.Point(15, 138);
            this.btnRestoreLastTimeBackupByUSB.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.btnRestoreLastTimeBackupByUSB.Name = "btnRestoreLastTimeBackupByUSB";
            this.btnRestoreLastTimeBackupByUSB.Size = new System.Drawing.Size(489, 40);
            this.btnRestoreLastTimeBackupByUSB.TabIndex = 0;
            this.btnRestoreLastTimeBackupByUSB.Text = "還原至前次備份(外接硬碟)";
            this.btnRestoreLastTimeBackupByUSB.UseVisualStyleBackColor = true;
            this.btnRestoreLastTimeBackupByUSB.Click += new System.EventHandler(this.btnRestoreLastTimeBackupByUSB_Click);
            // 
            // RestoreMySQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(519, 243);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnRestoreOriginalSetting);
            this.Controls.Add(this.btnRestoreLastTimeBackupByUSB);
            this.Controls.Add(this.btnRestoreLastTimeBackupByComputer);
            this.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RestoreMySQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "   資料還原";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRestoreLastTimeBackupByComputer;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnRestoreOriginalSetting;
        private System.Windows.Forms.Button btnRestoreLastTimeBackupByUSB;
    }
}

