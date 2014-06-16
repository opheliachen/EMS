namespace EMSSystem
{
    partial class frmSearchStudentData
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
            this.panelStudentSearchPage = new System.Windows.Forms.Panel();
            this.btnStudentSearch = new System.Windows.Forms.Button();
            this.cboStudentSearchBy = new System.Windows.Forms.ComboBox();
            this.txtStudentSearchByText = new System.Windows.Forms.TextBox();
            this.lblStudentSearchBy = new System.Windows.Forms.Label();
            this.panelStudentSearchShowClassList = new System.Windows.Forms.Panel();
            this.lblStudentSearchClassList = new System.Windows.Forms.Label();
            this.btnStudentSearchShowStudent = new System.Windows.Forms.Button();
            this.dgvStudentSearchClassList = new System.Windows.Forms.DataGridView();
            this.panelStudentSearchStudentInClass = new System.Windows.Forms.Panel();
            this.btnStudentSearchReturnClassList = new System.Windows.Forms.Button();
            this.lblStudentSearchShowStudentInClassList = new System.Windows.Forms.Label();
            this.lblStudentSearchShowClassName = new System.Windows.Forms.Label();
            this.lblStudentSearchShowClassID = new System.Windows.Forms.Label();
            this.lblStudentSearchClassName = new System.Windows.Forms.Label();
            this.lblStudentSearchClassID = new System.Windows.Forms.Label();
            this.btnStudentByStudentInClass = new System.Windows.Forms.Button();
            this.dgvStudentSearchShowStudentInClassList = new System.Windows.Forms.DataGridView();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStudentSearchInfo = new System.Windows.Forms.Label();
            this.lblFromPanel = new System.Windows.Forms.Label();
            this.panelStudentSearchPage.SuspendLayout();
            this.panelStudentSearchShowClassList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentSearchClassList)).BeginInit();
            this.panelStudentSearchStudentInClass.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentSearchShowStudentInClassList)).BeginInit();
            this.SuspendLayout();
            // 
            // panelStudentSearchPage
            // 
            this.panelStudentSearchPage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelStudentSearchPage.Controls.Add(this.btnStudentSearch);
            this.panelStudentSearchPage.Controls.Add(this.cboStudentSearchBy);
            this.panelStudentSearchPage.Controls.Add(this.txtStudentSearchByText);
            this.panelStudentSearchPage.Controls.Add(this.lblStudentSearchBy);
            this.panelStudentSearchPage.Location = new System.Drawing.Point(10, 24);
            this.panelStudentSearchPage.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelStudentSearchPage.Name = "panelStudentSearchPage";
            this.panelStudentSearchPage.Size = new System.Drawing.Size(661, 53);
            this.panelStudentSearchPage.TabIndex = 42;
            // 
            // btnStudentSearch
            // 
            this.btnStudentSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStudentSearch.FlatAppearance.BorderSize = 3;
            this.btnStudentSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStudentSearch.Location = new System.Drawing.Point(548, 4);
            this.btnStudentSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStudentSearch.Name = "btnStudentSearch";
            this.btnStudentSearch.Size = new System.Drawing.Size(110, 35);
            this.btnStudentSearch.TabIndex = 24;
            this.btnStudentSearch.Text = "搜 尋";
            this.btnStudentSearch.UseVisualStyleBackColor = true;
            this.btnStudentSearch.Click += new System.EventHandler(this.btnStudentSearch_Click);
            // 
            // cboStudentSearchBy
            // 
            this.cboStudentSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStudentSearchBy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboStudentSearchBy.FormattingEnabled = true;
            this.cboStudentSearchBy.Items.AddRange(new object[] {
            "學生編號",
            "學生姓名",
            "班級編號",
            "班級名稱"});
            this.cboStudentSearchBy.Location = new System.Drawing.Point(122, 6);
            this.cboStudentSearchBy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboStudentSearchBy.Name = "cboStudentSearchBy";
            this.cboStudentSearchBy.Size = new System.Drawing.Size(130, 30);
            this.cboStudentSearchBy.TabIndex = 23;
            this.cboStudentSearchBy.SelectedIndexChanged += new System.EventHandler(this.cboStudentSearchBy_SelectedIndexChanged);
            // 
            // txtStudentSearchByText
            // 
            this.txtStudentSearchByText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtStudentSearchByText.Location = new System.Drawing.Point(258, 4);
            this.txtStudentSearchByText.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtStudentSearchByText.MaxLength = 50;
            this.txtStudentSearchByText.Name = "txtStudentSearchByText";
            this.txtStudentSearchByText.Size = new System.Drawing.Size(284, 34);
            this.txtStudentSearchByText.TabIndex = 23;
            // 
            // lblStudentSearchBy
            // 
            this.lblStudentSearchBy.AutoSize = true;
            this.lblStudentSearchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchBy.Location = new System.Drawing.Point(7, 10);
            this.lblStudentSearchBy.Name = "lblStudentSearchBy";
            this.lblStudentSearchBy.Size = new System.Drawing.Size(109, 22);
            this.lblStudentSearchBy.TabIndex = 22;
            this.lblStudentSearchBy.Text = "搜尋方式:";
            // 
            // panelStudentSearchShowClassList
            // 
            this.panelStudentSearchShowClassList.Controls.Add(this.lblStudentSearchClassList);
            this.panelStudentSearchShowClassList.Controls.Add(this.btnStudentSearchShowStudent);
            this.panelStudentSearchShowClassList.Controls.Add(this.dgvStudentSearchClassList);
            this.panelStudentSearchShowClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelStudentSearchShowClassList.Location = new System.Drawing.Point(10, 94);
            this.panelStudentSearchShowClassList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelStudentSearchShowClassList.Name = "panelStudentSearchShowClassList";
            this.panelStudentSearchShowClassList.Size = new System.Drawing.Size(777, 345);
            this.panelStudentSearchShowClassList.TabIndex = 45;
            // 
            // lblStudentSearchClassList
            // 
            this.lblStudentSearchClassList.AutoSize = true;
            this.lblStudentSearchClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchClassList.Location = new System.Drawing.Point(13, 11);
            this.lblStudentSearchClassList.Name = "lblStudentSearchClassList";
            this.lblStudentSearchClassList.Size = new System.Drawing.Size(109, 22);
            this.lblStudentSearchClassList.TabIndex = 31;
            this.lblStudentSearchClassList.Text = "班級列表:";
            // 
            // btnStudentSearchShowStudent
            // 
            this.btnStudentSearchShowStudent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStudentSearchShowStudent.FlatAppearance.BorderSize = 3;
            this.btnStudentSearchShowStudent.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnStudentSearchShowStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentSearchShowStudent.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStudentSearchShowStudent.Location = new System.Drawing.Point(636, 301);
            this.btnStudentSearchShowStudent.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStudentSearchShowStudent.Name = "btnStudentSearchShowStudent";
            this.btnStudentSearchShowStudent.Size = new System.Drawing.Size(130, 35);
            this.btnStudentSearchShowStudent.TabIndex = 24;
            this.btnStudentSearchShowStudent.Text = "顯示學生";
            this.btnStudentSearchShowStudent.UseVisualStyleBackColor = true;
            this.btnStudentSearchShowStudent.Click += new System.EventHandler(this.btnStudentSearchShowStudent_Click);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvStudentSearchClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStudentSearchClassList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvStudentSearchClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchClassList.Location = new System.Drawing.Point(16, 44);
            this.dgvStudentSearchClassList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvStudentSearchClassList.Name = "dgvStudentSearchClassList";
            this.dgvStudentSearchClassList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
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
            this.dgvStudentSearchClassList.Size = new System.Drawing.Size(750, 245);
            this.dgvStudentSearchClassList.TabIndex = 32;
            this.dgvStudentSearchClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            this.dgvStudentSearchClassList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchClassList_CellClick);
            // 
            // panelStudentSearchStudentInClass
            // 
            this.panelStudentSearchStudentInClass.Controls.Add(this.btnStudentSearchReturnClassList);
            this.panelStudentSearchStudentInClass.Controls.Add(this.lblStudentSearchShowStudentInClassList);
            this.panelStudentSearchStudentInClass.Controls.Add(this.lblStudentSearchShowClassName);
            this.panelStudentSearchStudentInClass.Controls.Add(this.lblStudentSearchShowClassID);
            this.panelStudentSearchStudentInClass.Controls.Add(this.lblStudentSearchClassName);
            this.panelStudentSearchStudentInClass.Controls.Add(this.lblStudentSearchClassID);
            this.panelStudentSearchStudentInClass.Controls.Add(this.btnStudentByStudentInClass);
            this.panelStudentSearchStudentInClass.Controls.Add(this.dgvStudentSearchShowStudentInClassList);
            this.panelStudentSearchStudentInClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.panelStudentSearchStudentInClass.Location = new System.Drawing.Point(10, 94);
            this.panelStudentSearchStudentInClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panelStudentSearchStudentInClass.Name = "panelStudentSearchStudentInClass";
            this.panelStudentSearchStudentInClass.Size = new System.Drawing.Size(777, 347);
            this.panelStudentSearchStudentInClass.TabIndex = 43;
            // 
            // btnStudentSearchReturnClassList
            // 
            this.btnStudentSearchReturnClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStudentSearchReturnClassList.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnStudentSearchReturnClassList.FlatAppearance.BorderSize = 3;
            this.btnStudentSearchReturnClassList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentSearchReturnClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStudentSearchReturnClassList.Location = new System.Drawing.Point(16, 301);
            this.btnStudentSearchReturnClassList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStudentSearchReturnClassList.Name = "btnStudentSearchReturnClassList";
            this.btnStudentSearchReturnClassList.Size = new System.Drawing.Size(130, 35);
            this.btnStudentSearchReturnClassList.TabIndex = 34;
            this.btnStudentSearchReturnClassList.Text = "返回班級列表";
            this.btnStudentSearchReturnClassList.UseVisualStyleBackColor = false;
            this.btnStudentSearchReturnClassList.Click += new System.EventHandler(this.btnStudentSearchReturnClassList_Click);
            // 
            // lblStudentSearchShowStudentInClassList
            // 
            this.lblStudentSearchShowStudentInClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblStudentSearchShowStudentInClassList.AutoSize = true;
            this.lblStudentSearchShowStudentInClassList.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchShowStudentInClassList.Location = new System.Drawing.Point(13, -90);
            this.lblStudentSearchShowStudentInClassList.Name = "lblStudentSearchShowStudentInClassList";
            this.lblStudentSearchShowStudentInClassList.Size = new System.Drawing.Size(109, 22);
            this.lblStudentSearchShowStudentInClassList.TabIndex = 31;
            this.lblStudentSearchShowStudentInClassList.Text = "學生列表:";
            // 
            // lblStudentSearchShowClassName
            // 
            this.lblStudentSearchShowClassName.AutoSize = true;
            this.lblStudentSearchShowClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchShowClassName.Location = new System.Drawing.Point(524, 7);
            this.lblStudentSearchShowClassName.Name = "lblStudentSearchShowClassName";
            this.lblStudentSearchShowClassName.Size = new System.Drawing.Size(148, 22);
            this.lblStudentSearchShowClassName.TabIndex = 26;
            this.lblStudentSearchShowClassName.Text = "顯示班級名稱";
            // 
            // lblStudentSearchShowClassID
            // 
            this.lblStudentSearchShowClassID.AutoSize = true;
            this.lblStudentSearchShowClassID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchShowClassID.Location = new System.Drawing.Point(220, 7);
            this.lblStudentSearchShowClassID.Name = "lblStudentSearchShowClassID";
            this.lblStudentSearchShowClassID.Size = new System.Drawing.Size(148, 22);
            this.lblStudentSearchShowClassID.TabIndex = 26;
            this.lblStudentSearchShowClassID.Text = "顯示班級編號";
            // 
            // lblStudentSearchClassName
            // 
            this.lblStudentSearchClassName.AutoSize = true;
            this.lblStudentSearchClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchClassName.Location = new System.Drawing.Point(409, 7);
            this.lblStudentSearchClassName.Name = "lblStudentSearchClassName";
            this.lblStudentSearchClassName.Size = new System.Drawing.Size(109, 22);
            this.lblStudentSearchClassName.TabIndex = 26;
            this.lblStudentSearchClassName.Text = "班級名稱:";
            // 
            // lblStudentSearchClassID
            // 
            this.lblStudentSearchClassID.AutoSize = true;
            this.lblStudentSearchClassID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchClassID.Location = new System.Drawing.Point(105, 7);
            this.lblStudentSearchClassID.Name = "lblStudentSearchClassID";
            this.lblStudentSearchClassID.Size = new System.Drawing.Size(109, 22);
            this.lblStudentSearchClassID.TabIndex = 26;
            this.lblStudentSearchClassID.Text = "班級編號:";
            // 
            // btnStudentByStudentInClass
            // 
            this.btnStudentByStudentInClass.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStudentByStudentInClass.FlatAppearance.BorderSize = 3;
            this.btnStudentByStudentInClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentByStudentInClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStudentByStudentInClass.Location = new System.Drawing.Point(656, 301);
            this.btnStudentByStudentInClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnStudentByStudentInClass.Name = "btnStudentByStudentInClass";
            this.btnStudentByStudentInClass.Size = new System.Drawing.Size(110, 35);
            this.btnStudentByStudentInClass.TabIndex = 24;
            this.btnStudentByStudentInClass.Text = "確 認";
            this.btnStudentByStudentInClass.UseVisualStyleBackColor = true;
            this.btnStudentByStudentInClass.Click += new System.EventHandler(this.btnStudentByStudentInClass_Click);
            // 
            // dgvStudentSearchShowStudentInClassList
            // 
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.LimeGreen;
            this.dgvStudentSearchShowStudentInClassList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvStudentSearchShowStudentInClassList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvStudentSearchShowStudentInClassList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchShowStudentInClassList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvStudentSearchShowStudentInClassList.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle7.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchShowStudentInClassList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvStudentSearchShowStudentInClassList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvStudentSearchShowStudentInClassList.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvStudentSearchShowStudentInClassList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.dgvStudentSearchShowStudentInClassList.Location = new System.Drawing.Point(16, 39);
            this.dgvStudentSearchShowStudentInClassList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvStudentSearchShowStudentInClassList.Name = "dgvStudentSearchShowStudentInClassList";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.LimeGreen;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvStudentSearchShowStudentInClassList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvStudentSearchShowStudentInClassList.RowHeadersVisible = false;
            this.dgvStudentSearchShowStudentInClassList.RowTemplate.Height = 24;
            this.dgvStudentSearchShowStudentInClassList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudentSearchShowStudentInClassList.Size = new System.Drawing.Size(750, 258);
            this.dgvStudentSearchShowStudentInClassList.TabIndex = 32;
            this.dgvStudentSearchShowStudentInClassList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchShowStudentInClassList_CellClick);
            this.dgvStudentSearchShowStudentInClassList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchShowStudentInClassList_CellClick);
            this.dgvStudentSearchShowStudentInClassList.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchShowStudentInClassList_CellClick);
            this.dgvStudentSearchShowStudentInClassList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudentSearchShowStudentInClassList_CellClick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClose.Location = new System.Drawing.Point(677, 28);
            this.btnClose.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(110, 35);
            this.btnClose.TabIndex = 24;
            this.btnClose.Text = "關 閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStudentSearchInfo
            // 
            this.lblStudentSearchInfo.AutoSize = true;
            this.lblStudentSearchInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentSearchInfo.Location = new System.Drawing.Point(10, 71);
            this.lblStudentSearchInfo.Name = "lblStudentSearchInfo";
            this.lblStudentSearchInfo.Size = new System.Drawing.Size(102, 22);
            this.lblStudentSearchInfo.TabIndex = 25;
            this.lblStudentSearchInfo.Text = "搜尋條件";
            this.lblStudentSearchInfo.Visible = false;
            // 
            // lblFromPanel
            // 
            this.lblFromPanel.AutoSize = true;
            this.lblFromPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblFromPanel.Location = new System.Drawing.Point(121, 71);
            this.lblFromPanel.Name = "lblFromPanel";
            this.lblFromPanel.Size = new System.Drawing.Size(108, 22);
            this.lblFromPanel.TabIndex = 25;
            this.lblFromPanel.Text = "FromPanel";
            this.lblFromPanel.Visible = false;
            // 
            // frmSearchStudentData
            // 
            this.AcceptButton = this.btnStudentSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(799, 452);
            this.ControlBox = false;
            this.Controls.Add(this.lblFromPanel);
            this.Controls.Add(this.lblStudentSearchInfo);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.panelStudentSearchPage);
            this.Controls.Add(this.panelStudentSearchShowClassList);
            this.Controls.Add(this.panelStudentSearchStudentInClass);
            this.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "frmSearchStudentData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 學生查詢";
            this.panelStudentSearchPage.ResumeLayout(false);
            this.panelStudentSearchPage.PerformLayout();
            this.panelStudentSearchShowClassList.ResumeLayout(false);
            this.panelStudentSearchShowClassList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentSearchClassList)).EndInit();
            this.panelStudentSearchStudentInClass.ResumeLayout(false);
            this.panelStudentSearchStudentInClass.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudentSearchShowStudentInClassList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelStudentSearchPage;
        private System.Windows.Forms.Button btnStudentSearch;
        private System.Windows.Forms.ComboBox cboStudentSearchBy;
        private System.Windows.Forms.TextBox txtStudentSearchByText;
        private System.Windows.Forms.Label lblStudentSearchBy;
        private System.Windows.Forms.Panel panelStudentSearchShowClassList;
        private System.Windows.Forms.Label lblStudentSearchClassList;
        private System.Windows.Forms.Button btnStudentSearchShowStudent;
        private System.Windows.Forms.DataGridView dgvStudentSearchClassList;
        private System.Windows.Forms.Panel panelStudentSearchStudentInClass;
        private System.Windows.Forms.Button btnStudentSearchReturnClassList;
        private System.Windows.Forms.Label lblStudentSearchShowStudentInClassList;
        private System.Windows.Forms.Label lblStudentSearchShowClassName;
        private System.Windows.Forms.Label lblStudentSearchShowClassID;
        private System.Windows.Forms.Label lblStudentSearchClassName;
        private System.Windows.Forms.Label lblStudentSearchClassID;
        private System.Windows.Forms.Button btnStudentByStudentInClass;
        private System.Windows.Forms.DataGridView dgvStudentSearchShowStudentInClassList;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStudentSearchInfo;
        private System.Windows.Forms.Label lblFromPanel;
    }
}