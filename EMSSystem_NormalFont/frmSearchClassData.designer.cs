namespace EMSSystem
{
    partial class frmSearchClassData
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
            this.panelStudentSearchPage = new System.Windows.Forms.Panel();
            this.btnClassSearch = new System.Windows.Forms.Button();
            this.cboClassSearchBy = new System.Windows.Forms.ComboBox();
            this.txtClassSearchByText = new System.Windows.Forms.TextBox();
            this.lblClassSearchBy = new System.Windows.Forms.Label();
            this.panelClassSearchShowClassList = new System.Windows.Forms.Panel();
            this.lblClassSearchClassList = new System.Windows.Forms.Label();
            this.btnClassSearchSelectClass = new System.Windows.Forms.Button();
            this.dgvStudentSearchClassList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblClassSearchInfo = new System.Windows.Forms.Label();
            this.lblFromPanel = new System.Windows.Forms.Label();
            this.panelStudentSearchPage.SuspendLayout();
            this.panelClassSearchShowClassList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentSearchClassList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStudentSearchPage
            // 
            this.panelStudentSearchPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStudentSearchPage.Controls.Add(this.btnClassSearch);
            this.panelStudentSearchPage.Controls.Add(this.cboClassSearchBy);
            this.panelStudentSearchPage.Controls.Add(this.txtClassSearchByText);
            this.panelStudentSearchPage.Controls.Add(this.lblClassSearchBy);
            this.panelStudentSearchPage.Location = new System.Drawing.Point(12, 12);
            this.panelStudentSearchPage.Name = "panelStudentSearchPage";
            this.panelStudentSearchPage.Size = new System.Drawing.Size(539, 42);
            this.panelStudentSearchPage.TabIndex = 42;
            // 
            // btnClassSearch
            // 
            this.btnClassSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassSearch.FlatAppearance.BorderSize = 3;
            this.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClassSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClassSearch.Location = new System.Drawing.Point(415, 4);
            this.btnClassSearch.Name = "btnClassSearch";
            this.btnClassSearch.Size = new System.Drawing.Size(120, 35);
            this.btnClassSearch.TabIndex = 24;
            this.btnClassSearch.Text = "搜 尋";
            this.btnClassSearch.UseVisualStyleBackColor = true;
            this.btnClassSearch.Click += new System.EventHandler(this.btnStudentSearch_Click);
            // 
            // cboClassSearchBy
            // 
            this.cboClassSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClassSearchBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboClassSearchBy.FormattingEnabled = true;
            this.cboClassSearchBy.Items.AddRange(new object[] {
            "班級編號",
            "班級名稱"});
            this.cboClassSearchBy.Location = new System.Drawing.Point(105, 7);
            this.cboClassSearchBy.Name = "cboClassSearchBy";
            this.cboClassSearchBy.Size = new System.Drawing.Size(121, 27);
            this.cboClassSearchBy.TabIndex = 23;
            this.cboClassSearchBy.SelectedIndexChanged += new System.EventHandler(this.cboStudentSearchBy_SelectedIndexChanged);
            // 
            // txtClassSearchByText
            // 
            this.txtClassSearchByText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClassSearchByText.Location = new System.Drawing.Point(232, 6);
            this.txtClassSearchByText.MaxLength = 50;
            this.txtClassSearchByText.Name = "txtClassSearchByText";
            this.txtClassSearchByText.Size = new System.Drawing.Size(177, 30);
            this.txtClassSearchByText.TabIndex = 23;
            // 
            // lblClassSearchBy
            // 
            this.lblClassSearchBy.AutoSize = true;
            this.lblClassSearchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblClassSearchBy.Location = new System.Drawing.Point(4, 10);
            this.lblClassSearchBy.Name = "lblClassSearchBy";
            this.lblClassSearchBy.Size = new System.Drawing.Size(95, 19);
            this.lblClassSearchBy.TabIndex = 22;
            this.lblClassSearchBy.Text = "搜尋方式:";
            // 
            // panelClassSearchShowClassList
            // 
            this.panelClassSearchShowClassList.Controls.Add(this.lblClassSearchClassList);
            this.panelClassSearchShowClassList.Controls.Add(this.btnClassSearchSelectClass);
            this.panelClassSearchShowClassList.Controls.Add(this.dgvStudentSearchClassList);
            this.panelClassSearchShowClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelClassSearchShowClassList.Location = new System.Drawing.Point(12, 72);
            this.panelClassSearchShowClassList.Name = "panelClassSearchShowClassList";
            this.panelClassSearchShowClassList.Size = new System.Drawing.Size(678, 392);
            this.panelClassSearchShowClassList.TabIndex = 45;
            // 
            // lblClassSearchClassList
            // 
            this.lblClassSearchClassList.AutoSize = true;
            this.lblClassSearchClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblClassSearchClassList.Location = new System.Drawing.Point(15, 13);
            this.lblClassSearchClassList.Name = "lblClassSearchClassList";
            this.lblClassSearchClassList.Size = new System.Drawing.Size(95, 19);
            this.lblClassSearchClassList.TabIndex = 31;
            this.lblClassSearchClassList.Text = "班級列表:";
            // 
            // btnClassSearchSelectClass
            // 
            this.btnClassSearchSelectClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassSearchSelectClass.FlatAppearance.BorderSize = 3;
            this.btnClassSearchSelectClass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClassSearchSelectClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClassSearchSelectClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClassSearchSelectClass.Location = new System.Drawing.Point(555, 350);
            this.btnClassSearchSelectClass.Name = "btnClassSearchSelectClass";
            this.btnClassSearchSelectClass.Size = new System.Drawing.Size(110, 35);
            this.btnClassSearchSelectClass.TabIndex = 24;
            this.btnClassSearchSelectClass.Text = "確 認";
            this.btnClassSearchSelectClass.UseVisualStyleBackColor = true;
            this.btnClassSearchSelectClass.Click += new System.EventHandler(this.btnStudentSearchShowStudent_Click);
            // 
            // dgvStudentSearchClassList
            // 
            this.dgvStudentSearchClassList.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LimeGreen;
            this.dgvStudentSearchClassList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvStudentSearchClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudentSearchClassList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStudentSearchClassList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvStudentSearchClassList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvStudentSearchClassList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStudentSearchClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStudentSearchClassList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStudentSearchClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.Location = new System.Drawing.Point(18, 36);
            this.dgvStudentSearchClassList.Name = "dgvStudentSearchClassList";
            this.dgvStudentSearchClassList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvStudentSearchClassList.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvStudentSearchClassList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvStudentSearchClassList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvStudentSearchClassList.RowTemplate.Height = 24;
            this.dgvStudentSearchClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentSearchClassList.Size = new System.Drawing.Size(647, 306);
            this.dgvStudentSearchClassList.TabIndex = 32;
            this.dgvStudentSearchClassList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClose.Location = new System.Drawing.Point(557, 16);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "關 閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblClassSearchInfo
            // 
            this.lblClassSearchInfo.AutoSize = true;
            this.lblClassSearchInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblClassSearchInfo.Location = new System.Drawing.Point(12, 50);
            this.lblClassSearchInfo.Name = "lblClassSearchInfo";
            this.lblClassSearchInfo.Size = new System.Drawing.Size(89, 19);
            this.lblClassSearchInfo.TabIndex = 25;
            this.lblClassSearchInfo.Text = "搜尋條件";
            this.lblClassSearchInfo.Visible = false;
            // 
            // lblFromPanel
            // 
            this.lblFromPanel.AutoSize = true;
            this.lblFromPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblFromPanel.Location = new System.Drawing.Point(107, 50);
            this.lblFromPanel.Name = "lblFromPanel";
            this.lblFromPanel.Size = new System.Drawing.Size(97, 19);
            this.lblFromPanel.TabIndex = 46;
            this.lblFromPanel.Text = "FromPanel";
            this.lblFromPanel.Visible = false;
            // 
            // frmSearchClassData
            // 
            this.AcceptButton = this.btnClassSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(703, 478);
            this.ControlBox = false;
            this.Controls.Add(this.lblFromPanel);
            this.Controls.Add(this.lblClassSearchInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelStudentSearchPage);
            this.Controls.Add(this.panelClassSearchShowClassList);
            this.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "frmSearchClassData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "班級查詢";
            this.panelStudentSearchPage.ResumeLayout(false);
            this.panelStudentSearchPage.PerformLayout();
            this.panelClassSearchShowClassList.ResumeLayout(false);
            this.panelClassSearchShowClassList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentSearchClassList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelStudentSearchPage;
        private System.Windows.Forms.Button btnClassSearch;
        private System.Windows.Forms.ComboBox cboClassSearchBy;
        private System.Windows.Forms.TextBox txtClassSearchByText;
        private System.Windows.Forms.Label lblClassSearchBy;
        private System.Windows.Forms.Panel panelClassSearchShowClassList;
        private System.Windows.Forms.Label lblClassSearchClassList;
        private System.Windows.Forms.Button btnClassSearchSelectClass;
        private System.Windows.Forms.DataGridView dgvStudentSearchClassList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblClassSearchInfo;
        private System.Windows.Forms.Label lblFromPanel;
    }
}