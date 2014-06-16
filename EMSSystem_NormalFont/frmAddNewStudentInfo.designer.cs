namespace EMSSystem
{
    partial class frmAddNewStudentInfo
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
            this.panelInsertStudentQuick = new System.Windows.Forms.Panel();
            this.lblInsertErrorMsgIsShow = new System.Windows.Forms.Label();
            this.lblInsertStudentQuickStudentName = new System.Windows.Forms.Label();
            this.btnCancelStudentQuick = new System.Windows.Forms.Button();
            this.btnInsertStudentQuick = new System.Windows.Forms.Button();
            this.lblInsertStudentQuick = new System.Windows.Forms.Label();
            this.lblInsertStudentQuickStudentID = new System.Windows.Forms.Label();
            this.lblInsertStudentQuickStudentSex = new System.Windows.Forms.Label();
            this.txtInsertStudentQuickStudentID = new System.Windows.Forms.TextBox();
            this.cboInsertStudentQuickStudentSex = new System.Windows.Forms.ComboBox();
            this.txtInsertStudentQuickStudentName = new System.Windows.Forms.TextBox();
            this.panelInsertStudentQuick.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelInsertStudentQuick
            // 
            this.panelInsertStudentQuick.BackColor = System.Drawing.Color.Navy;
            this.panelInsertStudentQuick.Controls.Add(this.lblInsertErrorMsgIsShow);
            this.panelInsertStudentQuick.Controls.Add(this.lblInsertStudentQuickStudentName);
            this.panelInsertStudentQuick.Controls.Add(this.btnCancelStudentQuick);
            this.panelInsertStudentQuick.Controls.Add(this.btnInsertStudentQuick);
            this.panelInsertStudentQuick.Controls.Add(this.lblInsertStudentQuick);
            this.panelInsertStudentQuick.Controls.Add(this.lblInsertStudentQuickStudentID);
            this.panelInsertStudentQuick.Controls.Add(this.lblInsertStudentQuickStudentSex);
            this.panelInsertStudentQuick.Controls.Add(this.txtInsertStudentQuickStudentID);
            this.panelInsertStudentQuick.Controls.Add(this.cboInsertStudentQuickStudentSex);
            this.panelInsertStudentQuick.Controls.Add(this.txtInsertStudentQuickStudentName);
            this.panelInsertStudentQuick.Location = new System.Drawing.Point(12, 12);
            this.panelInsertStudentQuick.Name = "panelInsertStudentQuick";
            this.panelInsertStudentQuick.Size = new System.Drawing.Size(441, 203);
            this.panelInsertStudentQuick.TabIndex = 3;
            // 
            // lblInsertErrorMsgIsShow
            // 
            this.lblInsertErrorMsgIsShow.AutoSize = true;
            this.lblInsertErrorMsgIsShow.Location = new System.Drawing.Point(243, 89);
            this.lblInsertErrorMsgIsShow.Name = "lblInsertErrorMsgIsShow";
            this.lblInsertErrorMsgIsShow.Size = new System.Drawing.Size(87, 19);
            this.lblInsertErrorMsgIsShow.TabIndex = 88;
            this.lblInsertErrorMsgIsShow.Text = "ErrorMsg";
            this.lblInsertErrorMsgIsShow.Visible = false;
            // 
            // lblInsertStudentQuickStudentName
            // 
            this.lblInsertStudentQuickStudentName.AutoSize = true;
            this.lblInsertStudentQuickStudentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInsertStudentQuickStudentName.Location = new System.Drawing.Point(36, 89);
            this.lblInsertStudentQuickStudentName.Name = "lblInsertStudentQuickStudentName";
            this.lblInsertStudentQuickStudentName.Size = new System.Drawing.Size(95, 19);
            this.lblInsertStudentQuickStudentName.TabIndex = 19;
            this.lblInsertStudentQuickStudentName.Text = "學生姓名:";
            // 
            // btnCancelStudentQuick
            // 
            this.btnCancelStudentQuick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelStudentQuick.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelStudentQuick.FlatAppearance.BorderSize = 3;
            this.btnCancelStudentQuick.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnCancelStudentQuick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelStudentQuick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnCancelStudentQuick.Location = new System.Drawing.Point(313, 155);
            this.btnCancelStudentQuick.Name = "btnCancelStudentQuick";
            this.btnCancelStudentQuick.Size = new System.Drawing.Size(120, 35);
            this.btnCancelStudentQuick.TabIndex = 23;
            this.btnCancelStudentQuick.Text = "取 消";
            this.btnCancelStudentQuick.UseVisualStyleBackColor = true;
            this.btnCancelStudentQuick.Click += new System.EventHandler(this.btnCancelStudentQuick_Click);
            // 
            // btnInsertStudentQuick
            // 
            this.btnInsertStudentQuick.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInsertStudentQuick.FlatAppearance.BorderSize = 3;
            this.btnInsertStudentQuick.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnInsertStudentQuick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInsertStudentQuick.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnInsertStudentQuick.Location = new System.Drawing.Point(187, 155);
            this.btnInsertStudentQuick.Name = "btnInsertStudentQuick";
            this.btnInsertStudentQuick.Size = new System.Drawing.Size(120, 35);
            this.btnInsertStudentQuick.TabIndex = 22;
            this.btnInsertStudentQuick.Text = "確 認";
            this.btnInsertStudentQuick.UseVisualStyleBackColor = true;
            this.btnInsertStudentQuick.Click += new System.EventHandler(this.btnInsertStudentQuick_Click);
            // 
            // lblInsertStudentQuick
            // 
            this.lblInsertStudentQuick.BackColor = System.Drawing.Color.DarkRed;
            this.lblInsertStudentQuick.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblInsertStudentQuick.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInsertStudentQuick.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.lblInsertStudentQuick.Location = new System.Drawing.Point(-18, 4);
            this.lblInsertStudentQuick.Name = "lblInsertStudentQuick";
            this.lblInsertStudentQuick.Size = new System.Drawing.Size(477, 40);
            this.lblInsertStudentQuick.TabIndex = 18;
            this.lblInsertStudentQuick.Text = "01.新生選課管理 > 新增學生";
            this.lblInsertStudentQuick.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblInsertStudentQuickStudentID
            // 
            this.lblInsertStudentQuickStudentID.AutoSize = true;
            this.lblInsertStudentQuickStudentID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInsertStudentQuickStudentID.Location = new System.Drawing.Point(36, 56);
            this.lblInsertStudentQuickStudentID.Name = "lblInsertStudentQuickStudentID";
            this.lblInsertStudentQuickStudentID.Size = new System.Drawing.Size(95, 19);
            this.lblInsertStudentQuickStudentID.TabIndex = 19;
            this.lblInsertStudentQuickStudentID.Text = "學生編號:";
            // 
            // lblInsertStudentQuickStudentSex
            // 
            this.lblInsertStudentQuickStudentSex.AutoSize = true;
            this.lblInsertStudentQuickStudentSex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInsertStudentQuickStudentSex.Location = new System.Drawing.Point(37, 122);
            this.lblInsertStudentQuickStudentSex.Name = "lblInsertStudentQuickStudentSex";
            this.lblInsertStudentQuickStudentSex.Size = new System.Drawing.Size(95, 19);
            this.lblInsertStudentQuickStudentSex.TabIndex = 19;
            this.lblInsertStudentQuickStudentSex.Text = "學生性別:";
            // 
            // txtInsertStudentQuickStudentID
            // 
            this.txtInsertStudentQuickStudentID.Location = new System.Drawing.Point(137, 53);
            this.txtInsertStudentQuickStudentID.MaxLength = 8;
            this.txtInsertStudentQuickStudentID.Name = "txtInsertStudentQuickStudentID";
            this.txtInsertStudentQuickStudentID.Size = new System.Drawing.Size(100, 30);
            this.txtInsertStudentQuickStudentID.TabIndex = 1;
            // 
            // cboInsertStudentQuickStudentSex
            // 
            this.cboInsertStudentQuickStudentSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInsertStudentQuickStudentSex.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboInsertStudentQuickStudentSex.FormattingEnabled = true;
            this.cboInsertStudentQuickStudentSex.Items.AddRange(new object[] {
            "男",
            "女"});
            this.cboInsertStudentQuickStudentSex.Location = new System.Drawing.Point(138, 119);
            this.cboInsertStudentQuickStudentSex.Name = "cboInsertStudentQuickStudentSex";
            this.cboInsertStudentQuickStudentSex.Size = new System.Drawing.Size(70, 27);
            this.cboInsertStudentQuickStudentSex.TabIndex = 2;
            // 
            // txtInsertStudentQuickStudentName
            // 
            this.txtInsertStudentQuickStudentName.Location = new System.Drawing.Point(137, 86);
            this.txtInsertStudentQuickStudentName.MaxLength = 5;
            this.txtInsertStudentQuickStudentName.Name = "txtInsertStudentQuickStudentName";
            this.txtInsertStudentQuickStudentName.Size = new System.Drawing.Size(100, 30);
            this.txtInsertStudentQuickStudentName.TabIndex = 1;
            // 
            // frmAddNewStudentInfo
            // 
            this.AcceptButton = this.btnInsertStudentQuick;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Navy;
            this.CancelButton = this.btnCancelStudentQuick;
            this.ClientSize = new System.Drawing.Size(466, 227);
            this.ControlBox = false;
            this.Controls.Add(this.panelInsertStudentQuick);
            this.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(6, 4, 6, 4);
            this.Name = "frmAddNewStudentInfo";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 新生報名";
            this.Load += new System.EventHandler(this.frmAddNewStudentInfo_Load);
            this.panelInsertStudentQuick.ResumeLayout(false);
            this.panelInsertStudentQuick.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelInsertStudentQuick;
        private System.Windows.Forms.Label lblInsertStudentQuickStudentName;
        private System.Windows.Forms.Button btnCancelStudentQuick;
        private System.Windows.Forms.Button btnInsertStudentQuick;
        internal System.Windows.Forms.Label lblInsertStudentQuick;
        private System.Windows.Forms.Label lblInsertStudentQuickStudentID;
        private System.Windows.Forms.Label lblInsertStudentQuickStudentSex;
        private System.Windows.Forms.TextBox txtInsertStudentQuickStudentID;
        private System.Windows.Forms.ComboBox cboInsertStudentQuickStudentSex;
        private System.Windows.Forms.TextBox txtInsertStudentQuickStudentName;
        private System.Windows.Forms.Label lblInsertErrorMsgIsShow;
    }
}