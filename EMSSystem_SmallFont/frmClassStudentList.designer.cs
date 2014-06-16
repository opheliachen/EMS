namespace EMSSystem
{
    partial class frmClassStudentList
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvClassStudentList = new System.Windows.Forms.DataGridView();
            this.lblShowClassName = new System.Windows.Forms.Label();
            this.lblShowClassID = new System.Windows.Forms.Label();
            this.lblClassName = new System.Windows.Forms.Label();
            this.lblClassID = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassStudentList)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            this.btnClose.Location = new System.Drawing.Point(358, 541);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 39);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = " 離 開 ";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvClassStudentList
            // 
            this.dgvClassStudentList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClassStudentList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvClassStudentList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvClassStudentList.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvClassStudentList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvClassStudentList.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.dgvClassStudentList.Location = new System.Drawing.Point(32, 67);
            this.dgvClassStudentList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvClassStudentList.Name = "dgvClassStudentList";
            this.dgvClassStudentList.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvClassStudentList.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvClassStudentList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvClassStudentList.RowTemplate.Height = 24;
            this.dgvClassStudentList.Size = new System.Drawing.Size(795, 451);
            this.dgvClassStudentList.TabIndex = 7;
            // 
            // lblShowClassName
            // 
            this.lblShowClassName.AutoSize = true;
            this.lblShowClassName.Font = new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            this.lblShowClassName.Location = new System.Drawing.Point(662, 26);
            this.lblShowClassName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShowClassName.Name = "lblShowClassName";
            this.lblShowClassName.Size = new System.Drawing.Size(160, 23);
            this.lblShowClassName.TabIndex = 6;
            this.lblShowClassName.Text = "顯示班級名稱";
            // 
            // lblShowClassID
            // 
            this.lblShowClassID.AutoSize = true;
            this.lblShowClassID.Font = new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            this.lblShowClassID.Location = new System.Drawing.Point(176, 26);
            this.lblShowClassID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShowClassID.Name = "lblShowClassID";
            this.lblShowClassID.Size = new System.Drawing.Size(160, 23);
            this.lblShowClassID.TabIndex = 3;
            this.lblShowClassID.Text = "顯示班級編號";
            // 
            // lblClassName
            // 
            this.lblClassName.AutoSize = true;
            this.lblClassName.Font = new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            this.lblClassName.Location = new System.Drawing.Point(513, 26);
            this.lblClassName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClassName.Name = "lblClassName";
            this.lblClassName.Size = new System.Drawing.Size(135, 23);
            this.lblClassName.TabIndex = 4;
            this.lblClassName.Text = "班級名稱：";
            // 
            // lblClassID
            // 
            this.lblClassID.AutoSize = true;
            this.lblClassID.Font = new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            this.lblClassID.Location = new System.Drawing.Point(27, 26);
            this.lblClassID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClassID.Name = "lblClassID";
            this.lblClassID.Size = new System.Drawing.Size(135, 23);
            this.lblClassID.TabIndex = 5;
            this.lblClassID.Text = "班級編號：";
            // 
            // frmClassStudentList
            // 
            this.AcceptButton = this.btnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(854, 607);
            this.ControlBox = false;
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvClassStudentList);
            this.Controls.Add(this.lblShowClassName);
            this.Controls.Add(this.lblShowClassID);
            this.Controls.Add(this.lblClassName);
            this.Controls.Add(this.lblClassID);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmClassStudentList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = " 學生清單";
            ((System.ComponentModel.ISupportInitialize)(this.dgvClassStudentList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvClassStudentList;
        private System.Windows.Forms.Label lblShowClassName;
        private System.Windows.Forms.Label lblShowClassID;
        private System.Windows.Forms.Label lblClassName;
        private System.Windows.Forms.Label lblClassID;
    }
}