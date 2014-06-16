namespace SetPassword
{
    partial class frmSetPassword
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSetPassword));
            this.btnSelectPlace = new System.Windows.Forms.Button();
            this.lblPasswordPlace = new System.Windows.Forms.Label();
            this.fbdBrowserFolder = new System.Windows.Forms.FolderBrowserDialog();
            this.btnMakingUSBPassword = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnMakingSystemPassword = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectPlace
            // 
            this.btnSelectPlace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSelectPlace.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSelectPlace.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectPlace.Location = new System.Drawing.Point(15, 73);
            this.btnSelectPlace.Margin = new System.Windows.Forms.Padding(6);
            this.btnSelectPlace.Name = "btnSelectPlace";
            this.btnSelectPlace.Size = new System.Drawing.Size(400, 43);
            this.btnSelectPlace.TabIndex = 1;
            this.btnSelectPlace.Text = "Select Place";
            this.btnSelectPlace.UseVisualStyleBackColor = false;
            this.btnSelectPlace.Click += new System.EventHandler(this.btnSelectPlace_Click);
            // 
            // lblPasswordPlace
            // 
            this.lblPasswordPlace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPasswordPlace.AutoSize = true;
            this.lblPasswordPlace.Location = new System.Drawing.Point(12, 21);
            this.lblPasswordPlace.Name = "lblPasswordPlace";
            this.lblPasswordPlace.Size = new System.Drawing.Size(165, 25);
            this.lblPasswordPlace.TabIndex = 2;
            this.lblPasswordPlace.Text = "lblPasswordPlace";
            // 
            // btnMakingUSBPassword
            // 
            this.btnMakingUSBPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMakingUSBPassword.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnMakingUSBPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakingUSBPassword.Location = new System.Drawing.Point(15, 128);
            this.btnMakingUSBPassword.Margin = new System.Windows.Forms.Padding(6);
            this.btnMakingUSBPassword.Name = "btnMakingUSBPassword";
            this.btnMakingUSBPassword.Size = new System.Drawing.Size(400, 43);
            this.btnMakingUSBPassword.TabIndex = 1;
            this.btnMakingUSBPassword.Text = "Making USB Password";
            this.btnMakingUSBPassword.UseVisualStyleBackColor = false;
            this.btnMakingUSBPassword.Click += new System.EventHandler(this.btnMakingPassword_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(15, 238);
            this.btnClose.Margin = new System.Windows.Forms.Padding(6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(400, 43);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnMakingSystemPassword
            // 
            this.btnMakingSystemPassword.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMakingSystemPassword.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnMakingSystemPassword.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMakingSystemPassword.Location = new System.Drawing.Point(15, 183);
            this.btnMakingSystemPassword.Margin = new System.Windows.Forms.Padding(6);
            this.btnMakingSystemPassword.Name = "btnMakingSystemPassword";
            this.btnMakingSystemPassword.Size = new System.Drawing.Size(400, 43);
            this.btnMakingSystemPassword.TabIndex = 1;
            this.btnMakingSystemPassword.Text = "Making System Password";
            this.btnMakingSystemPassword.UseVisualStyleBackColor = false;
            this.btnMakingSystemPassword.Click += new System.EventHandler(this.btnMakingSystemPassword_Click);
            // 
            // frmSetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(430, 307);
            this.Controls.Add(this.lblPasswordPlace);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnMakingSystemPassword);
            this.Controls.Add(this.btnMakingUSBPassword);
            this.Controls.Add(this.btnSelectPlace);
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "frmSetPassword";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "  Set Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectPlace;
        private System.Windows.Forms.Label lblPasswordPlace;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowserFolder;
        private System.Windows.Forms.Button btnMakingUSBPassword;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnMakingSystemPassword;
    }
}

