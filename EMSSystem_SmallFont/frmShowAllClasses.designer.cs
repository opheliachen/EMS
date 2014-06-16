namespace EMSSystem
{
    partial class frmShowAllClasses
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnSelectClass = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.dgvShowAllClasses = new System.Windows.Forms.DataGridView();
            this.btnPrintClass = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowAllClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectClass
            // 
            this.btnSelectClass.FlatAppearance.BorderSize = 3;
            this.btnSelectClass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnSelectClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectClass.Location = new System.Drawing.Point(159, 411);
            this.btnSelectClass.Name = "btnSelectClass";
            this.btnSelectClass.Size = new System.Drawing.Size(110, 35);
            this.btnSelectClass.TabIndex = 23;
            this.btnSelectClass.Text = "確 認";
            this.btnSelectClass.UseVisualStyleBackColor = true;
            this.btnSelectClass.Click += new System.EventHandler(this.btnSelectClass_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(306, 411);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 23;
            this.btnClose.Text = "關 閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // dgvShowAllClasses
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LimeGreen;
            this.dgvShowAllClasses.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvShowAllClasses.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvShowAllClasses.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvShowAllClasses.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowAllClasses.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvShowAllClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvShowAllClasses.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvShowAllClasses.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvShowAllClasses.Location = new System.Drawing.Point(12, 12);
            this.dgvShowAllClasses.MultiSelect = false;
            this.dgvShowAllClasses.Name = "dgvShowAllClasses";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvShowAllClasses.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvShowAllClasses.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.dgvShowAllClasses.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvShowAllClasses.RowTemplate.Height = 30;
            this.dgvShowAllClasses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvShowAllClasses.Size = new System.Drawing.Size(404, 393);
            this.dgvShowAllClasses.TabIndex = 24;
            this.dgvShowAllClasses.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowAllClasses_CellClick);
            this.dgvShowAllClasses.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowAllClasses_CellClick);
            this.dgvShowAllClasses.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowAllClasses_CellClick);
            this.dgvShowAllClasses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvShowAllClasses_CellClick);
            // 
            // btnPrintClass
            // 
            this.btnPrintClass.FlatAppearance.BorderSize = 3;
            this.btnPrintClass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnPrintClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrintClass.Location = new System.Drawing.Point(12, 411);
            this.btnPrintClass.Name = "btnPrintClass";
            this.btnPrintClass.Size = new System.Drawing.Size(110, 35);
            this.btnPrintClass.TabIndex = 23;
            this.btnPrintClass.Text = "列 印";
            this.btnPrintClass.UseVisualStyleBackColor = true;
            this.btnPrintClass.Click += new System.EventHandler(this.btnPrintClass_Click);
            // 
            // frmShowAllClasses
            // 
            this.AcceptButton = this.btnSelectClass;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(428, 458);
            this.ControlBox = false;
            this.Controls.Add(this.dgvShowAllClasses);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnPrintClass);
            this.Controls.Add(this.btnSelectClass);
            this.Font = new System.Drawing.Font("PMingLiU", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmShowAllClasses";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 選擇課程";
            this.Load += new System.EventHandler(this.frmShowAllClasses_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowAllClasses)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectClass;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridView dgvShowAllClasses;
        private System.Windows.Forms.Button btnPrintClass;
    }
}