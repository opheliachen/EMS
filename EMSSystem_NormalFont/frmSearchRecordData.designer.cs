namespace EMSSystem
{
    partial class frmSearchRecordData
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelRecordSearchShowClassList = new System.Windows.Forms.Panel();
            this.lblRecordSearchClassList = new System.Windows.Forms.Label();
            this.btnRecordSearchShowStudent = new System.Windows.Forms.Button();
            this.dgvRecordSearchClassList = new System.Windows.Forms.DataGridView();
            this.panelRecordSearchStudentInClass = new System.Windows.Forms.Panel();
            this.btnRecordSearchReturnClassList = new System.Windows.Forms.Button();
            this.lblStudentSearchShowStudentInClassList = new System.Windows.Forms.Label();
            this.lblRecordSearchShowClassName = new System.Windows.Forms.Label();
            this.lblRecordSearchShowClassID = new System.Windows.Forms.Label();
            this.lblRecordSearchClassName = new System.Windows.Forms.Label();
            this.lblRecordSearchClassID = new System.Windows.Forms.Label();
            this.btnRecordByStudentInClass = new System.Windows.Forms.Button();
            this.dgvRecordSearchShowStudentInClassList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblRecordSearchInfo = new System.Windows.Forms.Label();
            this.lblFromPanel = new System.Windows.Forms.Label();
            this.panelRecordSearcBy = new System.Windows.Forms.Panel();
            this.panelRecordSearchByDate = new System.Windows.Forms.Panel();
            this.lblRecordSearchFromDate = new System.Windows.Forms.Label();
            this.lblRecordSearchEndDate = new System.Windows.Forms.Label();
            this.dtpRecordSearchFromDate = new System.Windows.Forms.DateTimePicker();
            this.dtpRecordSearchEndDate = new System.Windows.Forms.DateTimePicker();
            this.btnRecordSearch = new System.Windows.Forms.Button();
            this.cboRecordSearchBy = new System.Windows.Forms.ComboBox();
            this.txtRecordSearch = new System.Windows.Forms.TextBox();
            this.panelRecordSearchContinueNumber = new System.Windows.Forms.Panel();
            this.lblRecordSearchContinueNumber = new System.Windows.Forms.Label();
            this.lblRecordSearchToContinueNumber = new System.Windows.Forms.Label();
            this.txtRecordSearchEndContinueNumber = new System.Windows.Forms.TextBox();
            this.lblStudentSearchRecord = new System.Windows.Forms.Label();
            this.lblPersonOrClass = new System.Windows.Forms.Label();
            this.lblRecordSearchShowEndDate = new System.Windows.Forms.Label();
            this.lblRecordSearchShowFromDate = new System.Windows.Forms.Label();
            this.lblRecordSearchClassIDLastLetterIndex = new System.Windows.Forms.Label();
            this.lblOriginalFromPanel = new System.Windows.Forms.Label();
            this.panelRecordSearchShowClassList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordSearchClassList)).BeginInit();
            this.panelRecordSearchStudentInClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordSearchShowStudentInClassList)).BeginInit();
            this.panelRecordSearcBy.SuspendLayout();
            this.panelRecordSearchByDate.SuspendLayout();
            this.panelRecordSearchContinueNumber.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelRecordSearchShowClassList
            // 
            this.panelRecordSearchShowClassList.Controls.Add(this.lblRecordSearchClassList);
            this.panelRecordSearchShowClassList.Controls.Add(this.btnRecordSearchShowStudent);
            this.panelRecordSearchShowClassList.Controls.Add(this.dgvRecordSearchClassList);
            this.panelRecordSearchShowClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelRecordSearchShowClassList.Location = new System.Drawing.Point(12, 129);
            this.panelRecordSearchShowClassList.Name = "panelRecordSearchShowClassList";
            this.panelRecordSearchShowClassList.Size = new System.Drawing.Size(678, 335);
            this.panelRecordSearchShowClassList.TabIndex = 45;
            // 
            // lblRecordSearchClassList
            // 
            this.lblRecordSearchClassList.AutoSize = true;
            this.lblRecordSearchClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchClassList.Location = new System.Drawing.Point(15, 13);
            this.lblRecordSearchClassList.Name = "lblRecordSearchClassList";
            this.lblRecordSearchClassList.Size = new System.Drawing.Size(95, 19);
            this.lblRecordSearchClassList.TabIndex = 31;
            this.lblRecordSearchClassList.Text = "班級列表:";
            // 
            // btnRecordSearchShowStudent
            // 
            this.btnRecordSearchShowStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecordSearchShowStudent.FlatAppearance.BorderSize = 3;
            this.btnRecordSearchShowStudent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnRecordSearchShowStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecordSearchShowStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecordSearchShowStudent.Location = new System.Drawing.Point(555, 293);
            this.btnRecordSearchShowStudent.Name = "btnRecordSearchShowStudent";
            this.btnRecordSearchShowStudent.Size = new System.Drawing.Size(110, 35);
            this.btnRecordSearchShowStudent.TabIndex = 24;
            this.btnRecordSearchShowStudent.Text = "顯示學生";
            this.btnRecordSearchShowStudent.UseVisualStyleBackColor = true;
            this.btnRecordSearchShowStudent.Click += new System.EventHandler(this.btnRecordSearchShowStudent_Click);
            // 
            // dgvRecordSearchClassList
            // 
            this.dgvRecordSearchClassList.AllowUserToOrderColumns = true;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.LimeGreen;
            this.dgvRecordSearchClassList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvRecordSearchClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecordSearchClassList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvRecordSearchClassList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvRecordSearchClassList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dgvRecordSearchClassList.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dgvRecordSearchClassList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecordSearchClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvRecordSearchClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecordSearchClassList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvRecordSearchClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvRecordSearchClassList.Location = new System.Drawing.Point(18, 36);
            this.dgvRecordSearchClassList.Name = "dgvRecordSearchClassList";
            this.dgvRecordSearchClassList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecordSearchClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvRecordSearchClassList.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvRecordSearchClassList.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvRecordSearchClassList.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvRecordSearchClassList.RowTemplate.DefaultCellStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.dgvRecordSearchClassList.RowTemplate.Height = 24;
            this.dgvRecordSearchClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecordSearchClassList.Size = new System.Drawing.Size(647, 249);
            this.dgvRecordSearchClassList.TabIndex = 32;
            this.dgvRecordSearchClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchClassList_CellClick);
            this.dgvRecordSearchClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchClassList_CellClick);
            this.dgvRecordSearchClassList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchClassList_CellClick);
            this.dgvRecordSearchClassList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchClassList_CellClick);
            // 
            // panelRecordSearchStudentInClass
            // 
            this.panelRecordSearchStudentInClass.Controls.Add(this.btnRecordSearchReturnClassList);
            this.panelRecordSearchStudentInClass.Controls.Add(this.lblStudentSearchShowStudentInClassList);
            this.panelRecordSearchStudentInClass.Controls.Add(this.lblRecordSearchShowClassName);
            this.panelRecordSearchStudentInClass.Controls.Add(this.lblRecordSearchShowClassID);
            this.panelRecordSearchStudentInClass.Controls.Add(this.lblRecordSearchClassName);
            this.panelRecordSearchStudentInClass.Controls.Add(this.lblRecordSearchClassID);
            this.panelRecordSearchStudentInClass.Controls.Add(this.btnRecordByStudentInClass);
            this.panelRecordSearchStudentInClass.Controls.Add(this.dgvRecordSearchShowStudentInClassList);
            this.panelRecordSearchStudentInClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelRecordSearchStudentInClass.Location = new System.Drawing.Point(12, 129);
            this.panelRecordSearchStudentInClass.Name = "panelRecordSearchStudentInClass";
            this.panelRecordSearchStudentInClass.Size = new System.Drawing.Size(678, 335);
            this.panelRecordSearchStudentInClass.TabIndex = 43;
            // 
            // btnRecordSearchReturnClassList
            // 
            this.btnRecordSearchReturnClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRecordSearchReturnClassList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnRecordSearchReturnClassList.FlatAppearance.BorderSize = 3;
            this.btnRecordSearchReturnClassList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecordSearchReturnClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecordSearchReturnClassList.Location = new System.Drawing.Point(18, 292);
            this.btnRecordSearchReturnClassList.Name = "btnRecordSearchReturnClassList";
            this.btnRecordSearchReturnClassList.Size = new System.Drawing.Size(120, 35);
            this.btnRecordSearchReturnClassList.TabIndex = 34;
            this.btnRecordSearchReturnClassList.Text = "返回班級列表";
            this.btnRecordSearchReturnClassList.UseVisualStyleBackColor = false;
            this.btnRecordSearchReturnClassList.Click += new System.EventHandler(this.btnRecordSearchReturnClassList_Click);
            // 
            // lblStudentSearchShowStudentInClassList
            // 
            this.lblStudentSearchShowStudentInClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStudentSearchShowStudentInClassList.AutoSize = true;
            this.lblStudentSearchShowStudentInClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchShowStudentInClassList.Location = new System.Drawing.Point(15, -202);
            this.lblStudentSearchShowStudentInClassList.Name = "lblStudentSearchShowStudentInClassList";
            this.lblStudentSearchShowStudentInClassList.Size = new System.Drawing.Size(95, 19);
            this.lblStudentSearchShowStudentInClassList.TabIndex = 31;
            this.lblStudentSearchShowStudentInClassList.Text = "學生列表:";
            // 
            // lblRecordSearchShowClassName
            // 
            this.lblRecordSearchShowClassName.AutoSize = true;
            this.lblRecordSearchShowClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchShowClassName.Location = new System.Drawing.Point(473, 8);
            this.lblRecordSearchShowClassName.Name = "lblRecordSearchShowClassName";
            this.lblRecordSearchShowClassName.Size = new System.Drawing.Size(129, 19);
            this.lblRecordSearchShowClassName.TabIndex = 26;
            this.lblRecordSearchShowClassName.Text = "顯示班級名稱";
            // 
            // lblRecordSearchShowClassID
            // 
            this.lblRecordSearchShowClassID.AutoSize = true;
            this.lblRecordSearchShowClassID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchShowClassID.Location = new System.Drawing.Point(168, 8);
            this.lblRecordSearchShowClassID.Name = "lblRecordSearchShowClassID";
            this.lblRecordSearchShowClassID.Size = new System.Drawing.Size(129, 19);
            this.lblRecordSearchShowClassID.TabIndex = 26;
            this.lblRecordSearchShowClassID.Text = "顯示班級編號";
            // 
            // lblRecordSearchClassName
            // 
            this.lblRecordSearchClassName.AutoSize = true;
            this.lblRecordSearchClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchClassName.Location = new System.Drawing.Point(382, 8);
            this.lblRecordSearchClassName.Name = "lblRecordSearchClassName";
            this.lblRecordSearchClassName.Size = new System.Drawing.Size(95, 19);
            this.lblRecordSearchClassName.TabIndex = 26;
            this.lblRecordSearchClassName.Text = "班級名稱:";
            // 
            // lblRecordSearchClassID
            // 
            this.lblRecordSearchClassID.AutoSize = true;
            this.lblRecordSearchClassID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchClassID.Location = new System.Drawing.Point(77, 8);
            this.lblRecordSearchClassID.Name = "lblRecordSearchClassID";
            this.lblRecordSearchClassID.Size = new System.Drawing.Size(95, 19);
            this.lblRecordSearchClassID.TabIndex = 26;
            this.lblRecordSearchClassID.Text = "班級編號:";
            // 
            // btnRecordByStudentInClass
            // 
            this.btnRecordByStudentInClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecordByStudentInClass.FlatAppearance.BorderSize = 3;
            this.btnRecordByStudentInClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecordByStudentInClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecordByStudentInClass.Location = new System.Drawing.Point(545, 292);
            this.btnRecordByStudentInClass.Name = "btnRecordByStudentInClass";
            this.btnRecordByStudentInClass.Size = new System.Drawing.Size(120, 35);
            this.btnRecordByStudentInClass.TabIndex = 24;
            this.btnRecordByStudentInClass.Text = "確 認";
            this.btnRecordByStudentInClass.UseVisualStyleBackColor = true;
            this.btnRecordByStudentInClass.Click += new System.EventHandler(this.btnRecordByStudentInClass_Click);
            // 
            // dgvRecordSearchShowStudentInClassList
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LimeGreen;
            this.dgvRecordSearchShowStudentInClassList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvRecordSearchShowStudentInClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecordSearchShowStudentInClassList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvRecordSearchShowStudentInClassList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvRecordSearchShowStudentInClassList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecordSearchShowStudentInClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvRecordSearchShowStudentInClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecordSearchShowStudentInClassList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvRecordSearchShowStudentInClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvRecordSearchShowStudentInClassList.Location = new System.Drawing.Point(18, 36);
            this.dgvRecordSearchShowStudentInClassList.Name = "dgvRecordSearchShowStudentInClassList";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecordSearchShowStudentInClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRecordSearchShowStudentInClassList.RowHeadersVisible = false;
            this.dgvRecordSearchShowStudentInClassList.RowTemplate.Height = 24;
            this.dgvRecordSearchShowStudentInClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecordSearchShowStudentInClassList.Size = new System.Drawing.Size(647, 250);
            this.dgvRecordSearchShowStudentInClassList.TabIndex = 32;
            this.dgvRecordSearchShowStudentInClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchShowStudentInClassList_CellClick);
            this.dgvRecordSearchShowStudentInClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchShowStudentInClassList_CellClick);
            this.dgvRecordSearchShowStudentInClassList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchShowStudentInClassList_CellClick);
            this.dgvRecordSearchShowStudentInClassList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvRecordSearchShowStudentInClassList_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClose.Location = new System.Drawing.Point(557, 45);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 35);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "關 閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblRecordSearchInfo
            // 
            this.lblRecordSearchInfo.AutoSize = true;
            this.lblRecordSearchInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchInfo.Location = new System.Drawing.Point(12, 107);
            this.lblRecordSearchInfo.Name = "lblRecordSearchInfo";
            this.lblRecordSearchInfo.Size = new System.Drawing.Size(89, 19);
            this.lblRecordSearchInfo.TabIndex = 25;
            this.lblRecordSearchInfo.Text = "搜尋條件";
            this.lblRecordSearchInfo.Visible = false;
            // 
            // lblFromPanel
            // 
            this.lblFromPanel.AutoSize = true;
            this.lblFromPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblFromPanel.Location = new System.Drawing.Point(107, 107);
            this.lblFromPanel.Name = "lblFromPanel";
            this.lblFromPanel.Size = new System.Drawing.Size(97, 19);
            this.lblFromPanel.TabIndex = 25;
            this.lblFromPanel.Text = "FromPanel";
            this.lblFromPanel.Visible = false;
            // 
            // panelRecordSearcBy
            // 
            this.panelRecordSearcBy.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecordSearcBy.Controls.Add(this.panelRecordSearchByDate);
            this.panelRecordSearcBy.Controls.Add(this.btnRecordSearch);
            this.panelRecordSearcBy.Controls.Add(this.cboRecordSearchBy);
            this.panelRecordSearcBy.Controls.Add(this.txtRecordSearch);
            this.panelRecordSearcBy.Controls.Add(this.panelRecordSearchContinueNumber);
            this.panelRecordSearcBy.Controls.Add(this.lblStudentSearchRecord);
            this.panelRecordSearcBy.Location = new System.Drawing.Point(12, 12);
            this.panelRecordSearcBy.Name = "panelRecordSearcBy";
            this.panelRecordSearcBy.Size = new System.Drawing.Size(539, 104);
            this.panelRecordSearcBy.TabIndex = 46;
            // 
            // panelRecordSearchByDate
            // 
            this.panelRecordSearchByDate.Controls.Add(this.lblRecordSearchFromDate);
            this.panelRecordSearchByDate.Controls.Add(this.lblRecordSearchEndDate);
            this.panelRecordSearchByDate.Controls.Add(this.dtpRecordSearchFromDate);
            this.panelRecordSearchByDate.Controls.Add(this.dtpRecordSearchEndDate);
            this.panelRecordSearchByDate.Location = new System.Drawing.Point(98, 0);
            this.panelRecordSearchByDate.Name = "panelRecordSearchByDate";
            this.panelRecordSearchByDate.Size = new System.Drawing.Size(404, 32);
            this.panelRecordSearchByDate.TabIndex = 31;
            // 
            // lblRecordSearchFromDate
            // 
            this.lblRecordSearchFromDate.AutoSize = true;
            this.lblRecordSearchFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchFromDate.Location = new System.Drawing.Point(6, 7);
            this.lblRecordSearchFromDate.Name = "lblRecordSearchFromDate";
            this.lblRecordSearchFromDate.Size = new System.Drawing.Size(29, 19);
            this.lblRecordSearchFromDate.TabIndex = 22;
            this.lblRecordSearchFromDate.Text = "從";
            // 
            // lblRecordSearchEndDate
            // 
            this.lblRecordSearchEndDate.AutoSize = true;
            this.lblRecordSearchEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchEndDate.Location = new System.Drawing.Point(208, 7);
            this.lblRecordSearchEndDate.Name = "lblRecordSearchEndDate";
            this.lblRecordSearchEndDate.Size = new System.Drawing.Size(29, 19);
            this.lblRecordSearchEndDate.TabIndex = 22;
            this.lblRecordSearchEndDate.Text = "到";
            // 
            // dtpRecordSearchFromDate
            // 
            this.dtpRecordSearchFromDate.Checked = false;
            this.dtpRecordSearchFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpRecordSearchFromDate.Location = new System.Drawing.Point(37, 2);
            this.dtpRecordSearchFromDate.Name = "dtpRecordSearchFromDate";
            this.dtpRecordSearchFromDate.ShowCheckBox = true;
            this.dtpRecordSearchFromDate.Size = new System.Drawing.Size(154, 30);
            this.dtpRecordSearchFromDate.TabIndex = 26;
            // 
            // dtpRecordSearchEndDate
            // 
            this.dtpRecordSearchEndDate.Checked = false;
            this.dtpRecordSearchEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpRecordSearchEndDate.Location = new System.Drawing.Point(239, 2);
            this.dtpRecordSearchEndDate.Name = "dtpRecordSearchEndDate";
            this.dtpRecordSearchEndDate.RightToLeftLayout = true;
            this.dtpRecordSearchEndDate.ShowCheckBox = true;
            this.dtpRecordSearchEndDate.Size = new System.Drawing.Size(154, 30);
            this.dtpRecordSearchEndDate.TabIndex = 26;
            // 
            // btnRecordSearch
            // 
            this.btnRecordSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRecordSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnRecordSearch.FlatAppearance.BorderSize = 3;
            this.btnRecordSearch.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnRecordSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRecordSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnRecordSearch.Location = new System.Drawing.Point(410, 33);
            this.btnRecordSearch.Name = "btnRecordSearch";
            this.btnRecordSearch.Size = new System.Drawing.Size(120, 35);
            this.btnRecordSearch.TabIndex = 30;
            this.btnRecordSearch.Text = "搜 尋";
            this.btnRecordSearch.UseVisualStyleBackColor = false;
            this.btnRecordSearch.Click += new System.EventHandler(this.btnRecordSearch_Click);
            // 
            // cboRecordSearchBy
            // 
            this.cboRecordSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRecordSearchBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboRecordSearchBy.FormattingEnabled = true;
            this.cboRecordSearchBy.Items.AddRange(new object[] {
            "學生編號",
            "學生姓名",
            "班級編號",
            "班級名稱"});
            this.cboRecordSearchBy.Location = new System.Drawing.Point(108, 37);
            this.cboRecordSearchBy.Name = "cboRecordSearchBy";
            this.cboRecordSearchBy.Size = new System.Drawing.Size(121, 27);
            this.cboRecordSearchBy.TabIndex = 27;
            this.cboRecordSearchBy.SelectedIndexChanged += new System.EventHandler(this.cboRecordSearchBy_SelectedIndexChanged);
            // 
            // txtRecordSearch
            // 
            this.txtRecordSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordSearch.Location = new System.Drawing.Point(235, 35);
            this.txtRecordSearch.MaxLength = 50;
            this.txtRecordSearch.Name = "txtRecordSearch";
            this.txtRecordSearch.Size = new System.Drawing.Size(169, 30);
            this.txtRecordSearch.TabIndex = 28;
            // 
            // panelRecordSearchContinueNumber
            // 
            this.panelRecordSearchContinueNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelRecordSearchContinueNumber.Controls.Add(this.lblRecordSearchContinueNumber);
            this.panelRecordSearchContinueNumber.Controls.Add(this.lblRecordSearchToContinueNumber);
            this.panelRecordSearchContinueNumber.Controls.Add(this.txtRecordSearchEndContinueNumber);
            this.panelRecordSearchContinueNumber.Location = new System.Drawing.Point(135, 68);
            this.panelRecordSearchContinueNumber.Name = "panelRecordSearchContinueNumber";
            this.panelRecordSearchContinueNumber.Size = new System.Drawing.Size(298, 34);
            this.panelRecordSearchContinueNumber.TabIndex = 27;
            // 
            // lblRecordSearchContinueNumber
            // 
            this.lblRecordSearchContinueNumber.AutoSize = true;
            this.lblRecordSearchContinueNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchContinueNumber.Location = new System.Drawing.Point(16, 5);
            this.lblRecordSearchContinueNumber.Name = "lblRecordSearchContinueNumber";
            this.lblRecordSearchContinueNumber.Size = new System.Drawing.Size(95, 19);
            this.lblRecordSearchContinueNumber.TabIndex = 22;
            this.lblRecordSearchContinueNumber.Text = "編號連號:";
            // 
            // lblRecordSearchToContinueNumber
            // 
            this.lblRecordSearchToContinueNumber.AutoSize = true;
            this.lblRecordSearchToContinueNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchToContinueNumber.Location = new System.Drawing.Point(117, 5);
            this.lblRecordSearchToContinueNumber.Name = "lblRecordSearchToContinueNumber";
            this.lblRecordSearchToContinueNumber.Size = new System.Drawing.Size(29, 19);
            this.lblRecordSearchToContinueNumber.TabIndex = 22;
            this.lblRecordSearchToContinueNumber.Text = "到";
            // 
            // txtRecordSearchEndContinueNumber
            // 
            this.txtRecordSearchEndContinueNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRecordSearchEndContinueNumber.Location = new System.Drawing.Point(152, 1);
            this.txtRecordSearchEndContinueNumber.MaxLength = 50;
            this.txtRecordSearchEndContinueNumber.Name = "txtRecordSearchEndContinueNumber";
            this.txtRecordSearchEndContinueNumber.Size = new System.Drawing.Size(117, 30);
            this.txtRecordSearchEndContinueNumber.TabIndex = 29;
            // 
            // lblStudentSearchRecord
            // 
            this.lblStudentSearchRecord.AutoSize = true;
            this.lblStudentSearchRecord.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchRecord.Location = new System.Drawing.Point(7, 41);
            this.lblStudentSearchRecord.Name = "lblStudentSearchRecord";
            this.lblStudentSearchRecord.Size = new System.Drawing.Size(95, 19);
            this.lblStudentSearchRecord.TabIndex = 22;
            this.lblStudentSearchRecord.Text = "搜尋方式:";
            // 
            // lblPersonOrClass
            // 
            this.lblPersonOrClass.AutoSize = true;
            this.lblPersonOrClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblPersonOrClass.Location = new System.Drawing.Point(204, 107);
            this.lblPersonOrClass.Name = "lblPersonOrClass";
            this.lblPersonOrClass.Size = new System.Drawing.Size(129, 19);
            this.lblPersonOrClass.TabIndex = 25;
            this.lblPersonOrClass.Text = "PersonOrClass";
            this.lblPersonOrClass.Visible = false;
            // 
            // lblRecordSearchShowEndDate
            // 
            this.lblRecordSearchShowEndDate.AutoSize = true;
            this.lblRecordSearchShowEndDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchShowEndDate.Location = new System.Drawing.Point(620, 9);
            this.lblRecordSearchShowEndDate.Name = "lblRecordSearchShowEndDate";
            this.lblRecordSearchShowEndDate.Size = new System.Drawing.Size(89, 19);
            this.lblRecordSearchShowEndDate.TabIndex = 49;
            this.lblRecordSearchShowEndDate.Text = "結束日期";
            this.lblRecordSearchShowEndDate.Visible = false;
            // 
            // lblRecordSearchShowFromDate
            // 
            this.lblRecordSearchShowFromDate.AutoSize = true;
            this.lblRecordSearchShowFromDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchShowFromDate.Location = new System.Drawing.Point(525, 9);
            this.lblRecordSearchShowFromDate.Name = "lblRecordSearchShowFromDate";
            this.lblRecordSearchShowFromDate.Size = new System.Drawing.Size(89, 19);
            this.lblRecordSearchShowFromDate.TabIndex = 50;
            this.lblRecordSearchShowFromDate.Text = "起始日期";
            this.lblRecordSearchShowFromDate.Visible = false;
            // 
            // lblRecordSearchClassIDLastLetterIndex
            // 
            this.lblRecordSearchClassIDLastLetterIndex.AutoSize = true;
            this.lblRecordSearchClassIDLastLetterIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblRecordSearchClassIDLastLetterIndex.Location = new System.Drawing.Point(339, 107);
            this.lblRecordSearchClassIDLastLetterIndex.Name = "lblRecordSearchClassIDLastLetterIndex";
            this.lblRecordSearchClassIDLastLetterIndex.Size = new System.Drawing.Size(209, 19);
            this.lblRecordSearchClassIDLastLetterIndex.TabIndex = 48;
            this.lblRecordSearchClassIDLastLetterIndex.Text = "課程編號最後文字位置";
            this.lblRecordSearchClassIDLastLetterIndex.Visible = false;
            // 
            // lblOriginalFromPanel
            // 
            this.lblOriginalFromPanel.AutoSize = true;
            this.lblOriginalFromPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblOriginalFromPanel.Location = new System.Drawing.Point(554, 107);
            this.lblOriginalFromPanel.Name = "lblOriginalFromPanel";
            this.lblOriginalFromPanel.Size = new System.Drawing.Size(165, 19);
            this.lblOriginalFromPanel.TabIndex = 25;
            this.lblOriginalFromPanel.Text = "OriginalFromPanel";
            this.lblOriginalFromPanel.Visible = false;
            // 
            // frmSearchRecordData
            // 
            this.AcceptButton = this.btnRecordSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(705, 474);
            this.ControlBox = false;
            this.Controls.Add(this.lblRecordSearchShowEndDate);
            this.Controls.Add(this.lblRecordSearchShowFromDate);
            this.Controls.Add(this.lblRecordSearchClassIDLastLetterIndex);
            this.Controls.Add(this.lblPersonOrClass);
            this.Controls.Add(this.lblOriginalFromPanel);
            this.Controls.Add(this.lblFromPanel);
            this.Controls.Add(this.lblRecordSearchInfo);
            this.Controls.Add(this.panelRecordSearcBy);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelRecordSearchShowClassList);
            this.Controls.Add(this.panelRecordSearchStudentInClass);
            this.Font = new System.Drawing.Font("PMingLiU", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.Name = "frmSearchRecordData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "記錄查詢";
            this.Load += new System.EventHandler(this.frmSearchRecordData_Load);
            this.panelRecordSearchShowClassList.ResumeLayout(false);
            this.panelRecordSearchShowClassList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordSearchClassList)).EndInit();
            this.panelRecordSearchStudentInClass.ResumeLayout(false);
            this.panelRecordSearchStudentInClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecordSearchShowStudentInClassList)).EndInit();
            this.panelRecordSearcBy.ResumeLayout(false);
            this.panelRecordSearcBy.PerformLayout();
            this.panelRecordSearchByDate.ResumeLayout(false);
            this.panelRecordSearchByDate.PerformLayout();
            this.panelRecordSearchContinueNumber.ResumeLayout(false);
            this.panelRecordSearchContinueNumber.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelRecordSearchShowClassList;
        private System.Windows.Forms.Label lblRecordSearchClassList;
        private System.Windows.Forms.Button btnRecordSearchShowStudent;
        private System.Windows.Forms.DataGridView dgvRecordSearchClassList;
        private System.Windows.Forms.Panel panelRecordSearchStudentInClass;
        private System.Windows.Forms.Button btnRecordSearchReturnClassList;
        private System.Windows.Forms.Label lblStudentSearchShowStudentInClassList;
        private System.Windows.Forms.Label lblRecordSearchShowClassName;
        private System.Windows.Forms.Label lblRecordSearchShowClassID;
        private System.Windows.Forms.Label lblRecordSearchClassName;
        private System.Windows.Forms.Label lblRecordSearchClassID;
        private System.Windows.Forms.Button btnRecordByStudentInClass;
        private System.Windows.Forms.DataGridView dgvRecordSearchShowStudentInClassList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblRecordSearchInfo;
        private System.Windows.Forms.Label lblFromPanel;
        private System.Windows.Forms.Panel panelRecordSearcBy;
        private System.Windows.Forms.Panel panelRecordSearchByDate;
        private System.Windows.Forms.Label lblRecordSearchFromDate;
        private System.Windows.Forms.Label lblRecordSearchEndDate;
        private System.Windows.Forms.DateTimePicker dtpRecordSearchFromDate;
        private System.Windows.Forms.DateTimePicker dtpRecordSearchEndDate;
        private System.Windows.Forms.Button btnRecordSearch;
        private System.Windows.Forms.ComboBox cboRecordSearchBy;
        private System.Windows.Forms.TextBox txtRecordSearch;
        private System.Windows.Forms.Panel panelRecordSearchContinueNumber;
        private System.Windows.Forms.Label lblRecordSearchContinueNumber;
        private System.Windows.Forms.Label lblRecordSearchToContinueNumber;
        private System.Windows.Forms.TextBox txtRecordSearchEndContinueNumber;
        private System.Windows.Forms.Label lblStudentSearchRecord;
        private System.Windows.Forms.Label lblPersonOrClass;
        private System.Windows.Forms.Label lblRecordSearchShowEndDate;
        private System.Windows.Forms.Label lblRecordSearchShowFromDate;
        private System.Windows.Forms.Label lblRecordSearchClassIDLastLetterIndex;
        private System.Windows.Forms.Label lblOriginalFromPanel;
    }
}