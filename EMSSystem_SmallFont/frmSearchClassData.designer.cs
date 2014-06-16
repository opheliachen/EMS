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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.panelStudentSearchPage.Controls.Add(this.lblFromPanel);
            this.panelStudentSearchPage.Controls.Add(this.btnClassSearch);
            this.panelStudentSearchPage.Controls.Add(this.cboClassSearchBy);
            this.panelStudentSearchPage.Controls.Add(this.txtClassSearchByText);
            this.panelStudentSearchPage.Controls.Add(this.lblClassSearchBy);
            this.panelStudentSearchPage.Location = new System.Drawing.Point(10, 10);
            this.panelStudentSearchPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelStudentSearchPage.Name = "panelStudentSearchPage";
            this.panelStudentSearchPage.Size = new System.Drawing.Size(651, 45);
            this.panelStudentSearchPage.TabIndex = 42;
            // 
            // btnClassSearch
            // 
            this.btnClassSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClassSearch.FlatAppearance.BorderSize = 3;
            this.btnClassSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClassSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClassSearch.Location = new System.Drawing.Point(538, 3);
            this.btnClassSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClassSearch.Name = "btnClassSearch";
            this.btnClassSearch.Size = new System.Drawing.Size(110, 35);
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
            this.cboClassSearchBy.Location = new System.Drawing.Point(127, 5);
            this.cboClassSearchBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboClassSearchBy.Name = "cboClassSearchBy";
            this.cboClassSearchBy.Size = new System.Drawing.Size(141, 30);
            this.cboClassSearchBy.TabIndex = 23;
            this.cboClassSearchBy.SelectedIndexChanged += new System.EventHandler(this.cboStudentSearchBy_SelectedIndexChanged);
            // 
            // txtClassSearchByText
            // 
            this.txtClassSearchByText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtClassSearchByText.Location = new System.Drawing.Point(274, 3);
            this.txtClassSearchByText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtClassSearchByText.MaxLength = 50;
            this.txtClassSearchByText.Name = "txtClassSearchByText";
            this.txtClassSearchByText.Size = new System.Drawing.Size(258, 34);
            this.txtClassSearchByText.TabIndex = 23;
            // 
            // lblClassSearchBy
            // 
            this.lblClassSearchBy.AutoSize = true;
            this.lblClassSearchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblClassSearchBy.Location = new System.Drawing.Point(12, 9);
            this.lblClassSearchBy.Name = "lblClassSearchBy";
            this.lblClassSearchBy.Size = new System.Drawing.Size(109, 22);
            this.lblClassSearchBy.TabIndex = 22;
            this.lblClassSearchBy.Text = "搜尋方式:";
            // 
            // panelClassSearchShowClassList
            // 
            this.panelClassSearchShowClassList.Controls.Add(this.lblClassSearchClassList);
            this.panelClassSearchShowClassList.Controls.Add(this.btnClassSearchSelectClass);
            this.panelClassSearchShowClassList.Controls.Add(this.dgvStudentSearchClassList);
            this.panelClassSearchShowClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelClassSearchShowClassList.Location = new System.Drawing.Point(10, 59);
            this.panelClassSearchShowClassList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelClassSearchShowClassList.Name = "panelClassSearchShowClassList";
            this.panelClassSearchShowClassList.Size = new System.Drawing.Size(763, 390);
            this.panelClassSearchShowClassList.TabIndex = 45;
            // 
            // lblClassSearchClassList
            // 
            this.lblClassSearchClassList.AutoSize = true;
            this.lblClassSearchClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblClassSearchClassList.Location = new System.Drawing.Point(13, 11);
            this.lblClassSearchClassList.Name = "lblClassSearchClassList";
            this.lblClassSearchClassList.Size = new System.Drawing.Size(109, 22);
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
            this.btnClassSearchSelectClass.Location = new System.Drawing.Point(642, 347);
            this.btnClassSearchSelectClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LimeGreen;
            this.dgvStudentSearchClassList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStudentSearchClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudentSearchClassList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStudentSearchClassList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvStudentSearchClassList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvStudentSearchClassList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvStudentSearchClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStudentSearchClassList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvStudentSearchClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.Location = new System.Drawing.Point(16, 40);
            this.dgvStudentSearchClassList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvStudentSearchClassList.Name = "dgvStudentSearchClassList";
            this.dgvStudentSearchClassList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvStudentSearchClassList.RowHeadersVisible = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvStudentSearchClassList.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvStudentSearchClassList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvStudentSearchClassList.RowTemplate.Height = 24;
            this.dgvStudentSearchClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentSearchClassList.Size = new System.Drawing.Size(736, 299);
            this.dgvStudentSearchClassList.TabIndex = 32;
            this.dgvStudentSearchClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClose.Location = new System.Drawing.Point(667, 13);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "關 閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblClassSearchInfo
            // 
            this.lblClassSearchInfo.AutoSize = true;
            this.lblClassSearchInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblClassSearchInfo.Location = new System.Drawing.Point(80, 41);
            this.lblClassSearchInfo.Name = "lblClassSearchInfo";
            this.lblClassSearchInfo.Size = new System.Drawing.Size(102, 22);
            this.lblClassSearchInfo.TabIndex = 25;
            this.lblClassSearchInfo.Text = "搜尋條件";
            this.lblClassSearchInfo.Visible = false;
            // 
            // lblFromPanel
            // 
            this.lblFromPanel.AutoSize = true;
            this.lblFromPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblFromPanel.Location = new System.Drawing.Point(178, 31);
            this.lblFromPanel.Name = "lblFromPanel";
            this.lblFromPanel.Size = new System.Drawing.Size(108, 22);
            this.lblFromPanel.TabIndex = 46;
            this.lblFromPanel.Text = "FromPanel";
            this.lblFromPanel.Visible = false;
            // 
            // frmSearchClassData
            // 
            this.AcceptButton = this.btnClassSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(789, 460);
            this.ControlBox = false;
            this.Controls.Add(this.lblClassSearchInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelStudentSearchPage);
            this.Controls.Add(this.panelClassSearchShowClassList);
            this.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
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