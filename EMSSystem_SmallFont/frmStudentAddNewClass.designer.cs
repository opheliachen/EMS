namespace EMSSystem
{
    partial class frmStudentAddNewClass
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
            this.btnStudentAddClass = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblStudentAddClassClassID = new System.Windows.Forms.Label();
            this.lblStudentAddClassClassName = new System.Windows.Forms.Label();
            this.lblStudentAddClassShowClassName = new System.Windows.Forms.Label();
            this.lblStudentAddClassShowClassID = new System.Windows.Forms.Label();
            this.cbNewClassWednesday = new System.Windows.Forms.CheckBox();
            this.cbNewClassSaturday = new System.Windows.Forms.CheckBox();
            this.cbNewClassTuesday = new System.Windows.Forms.CheckBox();
            this.cbNewClassFriday = new System.Windows.Forms.CheckBox();
            this.cbNewClassMonday = new System.Windows.Forms.CheckBox();
            this.cbNewClassThursday = new System.Windows.Forms.CheckBox();
            this.cbNewClassSunday = new System.Windows.Forms.CheckBox();
            this.lblNewClassTime = new System.Windows.Forms.Label();
            this.dtpNewClassEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpNewClassStartDate = new System.Windows.Forms.DateTimePicker();
            this.txtNewClassPeriod = new System.Windows.Forms.TextBox();
            this.lblNewClassStartDate = new System.Windows.Forms.Label();
            this.lblNewClassPeriod = new System.Windows.Forms.Label();
            this.lblNewClassEndDate = new System.Windows.Forms.Label();
            this.lblStudentManageClassShowStudentID = new System.Windows.Forms.Label();
            this.lblStudentManageClassShowStudentName = new System.Windows.Forms.Label();
            this.lblNewClassPrice = new System.Windows.Forms.Label();
            this.txtNewClassPrice = new System.Windows.Forms.TextBox();
            this.lblNewClassMaterialFee = new System.Windows.Forms.Label();
            this.txtNewClassMaterialFee = new System.Windows.Forms.TextBox();
            this.lblNewClassApplyFee = new System.Windows.Forms.Label();
            this.txtNewClassApplyFee = new System.Windows.Forms.TextBox();
            this.lblInvisibleClassPrice = new System.Windows.Forms.Label();
            this.lblInsertErrorMsgIsShow = new System.Windows.Forms.Label();
            this.lblEachClassPrice = new System.Windows.Forms.Label();
            this.lblFromStudentOrClass = new System.Windows.Forms.Label();
            this.lblTempNewClassPeriod = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnStudentAddClass
            // 
            this.btnStudentAddClass.FlatAppearance.BorderSize = 3;
            this.btnStudentAddClass.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnStudentAddClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudentAddClass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnStudentAddClass.Location = new System.Drawing.Point(297, 316);
            this.btnStudentAddClass.Name = "btnStudentAddClass";
            this.btnStudentAddClass.Size = new System.Drawing.Size(100, 35);
            this.btnStudentAddClass.TabIndex = 42;
            this.btnStudentAddClass.Text = "確 認";
            this.btnStudentAddClass.UseVisualStyleBackColor = true;
            this.btnStudentAddClass.Click += new System.EventHandler(this.btnStudentAddClass_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.FlatAppearance.BorderSize = 3;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnClose.Location = new System.Drawing.Point(408, 316);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(100, 35);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "離 開";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblStudentAddClassClassID
            // 
            this.lblStudentAddClassClassID.AutoSize = true;
            this.lblStudentAddClassClassID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentAddClassClassID.Location = new System.Drawing.Point(34, 17);
            this.lblStudentAddClassClassID.Name = "lblStudentAddClassClassID";
            this.lblStudentAddClassClassID.Size = new System.Drawing.Size(72, 14);
            this.lblStudentAddClassClassID.TabIndex = 46;
            this.lblStudentAddClassClassID.Text = "課程編號:";
            // 
            // lblStudentAddClassClassName
            // 
            this.lblStudentAddClassClassName.AutoSize = true;
            this.lblStudentAddClassClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentAddClassClassName.Location = new System.Drawing.Point(273, 17);
            this.lblStudentAddClassClassName.Name = "lblStudentAddClassClassName";
            this.lblStudentAddClassClassName.Size = new System.Drawing.Size(72, 14);
            this.lblStudentAddClassClassName.TabIndex = 47;
            this.lblStudentAddClassClassName.Text = "課程名稱:";
            // 
            // lblStudentAddClassShowClassName
            // 
            this.lblStudentAddClassShowClassName.AutoSize = true;
            this.lblStudentAddClassShowClassName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentAddClassShowClassName.Location = new System.Drawing.Point(374, 17);
            this.lblStudentAddClassShowClassName.Name = "lblStudentAddClassShowClassName";
            this.lblStudentAddClassShowClassName.Size = new System.Drawing.Size(97, 14);
            this.lblStudentAddClassShowClassName.TabIndex = 44;
            this.lblStudentAddClassShowClassName.Text = "顯示課程名稱";
            // 
            // lblStudentAddClassShowClassID
            // 
            this.lblStudentAddClassShowClassID.AutoSize = true;
            this.lblStudentAddClassShowClassID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentAddClassShowClassID.Location = new System.Drawing.Point(135, 17);
            this.lblStudentAddClassShowClassID.Name = "lblStudentAddClassShowClassID";
            this.lblStudentAddClassShowClassID.Size = new System.Drawing.Size(97, 14);
            this.lblStudentAddClassShowClassID.TabIndex = 45;
            this.lblStudentAddClassShowClassID.Text = "顯示課程編號";
            // 
            // cbNewClassWednesday
            // 
            this.cbNewClassWednesday.AutoSize = true;
            this.cbNewClassWednesday.Enabled = false;
            this.cbNewClassWednesday.Location = new System.Drawing.Point(408, 62);
            this.cbNewClassWednesday.Name = "cbNewClassWednesday";
            this.cbNewClassWednesday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassWednesday.TabIndex = 57;
            this.cbNewClassWednesday.Text = "星期三";
            this.cbNewClassWednesday.UseVisualStyleBackColor = true;
            this.cbNewClassWednesday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // cbNewClassSaturday
            // 
            this.cbNewClassSaturday.AutoSize = true;
            this.cbNewClassSaturday.Enabled = false;
            this.cbNewClassSaturday.Location = new System.Drawing.Point(317, 84);
            this.cbNewClassSaturday.Name = "cbNewClassSaturday";
            this.cbNewClassSaturday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassSaturday.TabIndex = 59;
            this.cbNewClassSaturday.Text = "星期六";
            this.cbNewClassSaturday.UseVisualStyleBackColor = true;
            this.cbNewClassSaturday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // cbNewClassTuesday
            // 
            this.cbNewClassTuesday.AutoSize = true;
            this.cbNewClassTuesday.Enabled = false;
            this.cbNewClassTuesday.Location = new System.Drawing.Point(317, 61);
            this.cbNewClassTuesday.Name = "cbNewClassTuesday";
            this.cbNewClassTuesday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassTuesday.TabIndex = 60;
            this.cbNewClassTuesday.Text = "星期二";
            this.cbNewClassTuesday.UseVisualStyleBackColor = true;
            this.cbNewClassTuesday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // cbNewClassFriday
            // 
            this.cbNewClassFriday.AutoSize = true;
            this.cbNewClassFriday.Enabled = false;
            this.cbNewClassFriday.Location = new System.Drawing.Point(226, 84);
            this.cbNewClassFriday.Name = "cbNewClassFriday";
            this.cbNewClassFriday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassFriday.TabIndex = 61;
            this.cbNewClassFriday.Text = "星期五";
            this.cbNewClassFriday.UseVisualStyleBackColor = true;
            this.cbNewClassFriday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // cbNewClassMonday
            // 
            this.cbNewClassMonday.AutoSize = true;
            this.cbNewClassMonday.Enabled = false;
            this.cbNewClassMonday.Location = new System.Drawing.Point(226, 61);
            this.cbNewClassMonday.Name = "cbNewClassMonday";
            this.cbNewClassMonday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassMonday.TabIndex = 58;
            this.cbNewClassMonday.Text = "星期一";
            this.cbNewClassMonday.UseVisualStyleBackColor = true;
            this.cbNewClassMonday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // cbNewClassThursday
            // 
            this.cbNewClassThursday.AutoSize = true;
            this.cbNewClassThursday.Enabled = false;
            this.cbNewClassThursday.Location = new System.Drawing.Point(135, 84);
            this.cbNewClassThursday.Name = "cbNewClassThursday";
            this.cbNewClassThursday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassThursday.TabIndex = 55;
            this.cbNewClassThursday.Text = "星期四";
            this.cbNewClassThursday.UseVisualStyleBackColor = true;
            this.cbNewClassThursday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // cbNewClassSunday
            // 
            this.cbNewClassSunday.AutoSize = true;
            this.cbNewClassSunday.Enabled = false;
            this.cbNewClassSunday.Location = new System.Drawing.Point(135, 61);
            this.cbNewClassSunday.Name = "cbNewClassSunday";
            this.cbNewClassSunday.Size = new System.Drawing.Size(71, 18);
            this.cbNewClassSunday.TabIndex = 56;
            this.cbNewClassSunday.Text = "星期日";
            this.cbNewClassSunday.UseVisualStyleBackColor = true;
            this.cbNewClassSunday.CheckedChanged += new System.EventHandler(this.cbNewClassDays_CheckedChanged);
            // 
            // lblNewClassTime
            // 
            this.lblNewClassTime.AutoSize = true;
            this.lblNewClassTime.Location = new System.Drawing.Point(34, 62);
            this.lblNewClassTime.Name = "lblNewClassTime";
            this.lblNewClassTime.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassTime.TabIndex = 54;
            this.lblNewClassTime.Text = "上課時段:";
            // 
            // dtpNewClassEndDate
            // 
            this.dtpNewClassEndDate.Location = new System.Drawing.Point(135, 189);
            this.dtpNewClassEndDate.Name = "dtpNewClassEndDate";
            this.dtpNewClassEndDate.Size = new System.Drawing.Size(257, 24);
            this.dtpNewClassEndDate.TabIndex = 49;
            this.dtpNewClassEndDate.ValueChanged += new System.EventHandler(this.dtpNewClassEndDate_ValueChanged);
            // 
            // dtpNewClassStartDate
            // 
            this.dtpNewClassStartDate.Location = new System.Drawing.Point(135, 119);
            this.dtpNewClassStartDate.Name = "dtpNewClassStartDate";
            this.dtpNewClassStartDate.Size = new System.Drawing.Size(257, 24);
            this.dtpNewClassStartDate.TabIndex = 48;
            this.dtpNewClassStartDate.ValueChanged += new System.EventHandler(this.dtpNewClassStartDate_ValueChanged);
            // 
            // txtNewClassPeriod
            // 
            this.txtNewClassPeriod.Location = new System.Drawing.Point(135, 154);
            this.txtNewClassPeriod.MaxLength = 3;
            this.txtNewClassPeriod.Name = "txtNewClassPeriod";
            this.txtNewClassPeriod.Size = new System.Drawing.Size(47, 24);
            this.txtNewClassPeriod.TabIndex = 50;
            this.txtNewClassPeriod.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewClassPeriod_KeyPress);
            // 
            // lblNewClassStartDate
            // 
            this.lblNewClassStartDate.AutoSize = true;
            this.lblNewClassStartDate.Location = new System.Drawing.Point(34, 125);
            this.lblNewClassStartDate.Name = "lblNewClassStartDate";
            this.lblNewClassStartDate.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassStartDate.TabIndex = 53;
            this.lblNewClassStartDate.Text = "開課日期:";
            // 
            // lblNewClassPeriod
            // 
            this.lblNewClassPeriod.AutoSize = true;
            this.lblNewClassPeriod.Location = new System.Drawing.Point(34, 160);
            this.lblNewClassPeriod.Name = "lblNewClassPeriod";
            this.lblNewClassPeriod.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassPeriod.TabIndex = 52;
            this.lblNewClassPeriod.Text = "課程節數:";
            // 
            // lblNewClassEndDate
            // 
            this.lblNewClassEndDate.AutoSize = true;
            this.lblNewClassEndDate.Location = new System.Drawing.Point(34, 195);
            this.lblNewClassEndDate.Name = "lblNewClassEndDate";
            this.lblNewClassEndDate.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassEndDate.TabIndex = 51;
            this.lblNewClassEndDate.Text = "結束日期:";
            // 
            // lblStudentManageClassShowStudentID
            // 
            this.lblStudentManageClassShowStudentID.AutoSize = true;
            this.lblStudentManageClassShowStudentID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentManageClassShowStudentID.Location = new System.Drawing.Point(419, 166);
            this.lblStudentManageClassShowStudentID.Name = "lblStudentManageClassShowStudentID";
            this.lblStudentManageClassShowStudentID.Size = new System.Drawing.Size(67, 14);
            this.lblStudentManageClassShowStudentID.TabIndex = 44;
            this.lblStudentManageClassShowStudentID.Text = "學生編號";
            this.lblStudentManageClassShowStudentID.Visible = false;
            // 
            // lblStudentManageClassShowStudentName
            // 
            this.lblStudentManageClassShowStudentName.AutoSize = true;
            this.lblStudentManageClassShowStudentName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblStudentManageClassShowStudentName.Location = new System.Drawing.Point(419, 147);
            this.lblStudentManageClassShowStudentName.Name = "lblStudentManageClassShowStudentName";
            this.lblStudentManageClassShowStudentName.Size = new System.Drawing.Size(67, 14);
            this.lblStudentManageClassShowStudentName.TabIndex = 44;
            this.lblStudentManageClassShowStudentName.Text = "學生姓名";
            this.lblStudentManageClassShowStudentName.Visible = false;
            // 
            // lblNewClassPrice
            // 
            this.lblNewClassPrice.AutoSize = true;
            this.lblNewClassPrice.Location = new System.Drawing.Point(34, 230);
            this.lblNewClassPrice.Name = "lblNewClassPrice";
            this.lblNewClassPrice.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassPrice.TabIndex = 52;
            this.lblNewClassPrice.Text = "課程價格:";
            // 
            // txtNewClassPrice
            // 
            this.txtNewClassPrice.Location = new System.Drawing.Point(135, 224);
            this.txtNewClassPrice.MaxLength = 5;
            this.txtNewClassPrice.Name = "txtNewClassPrice";
            this.txtNewClassPrice.Size = new System.Drawing.Size(67, 24);
            this.txtNewClassPrice.TabIndex = 50;
            this.txtNewClassPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewClassPeriod_KeyPress);
            // 
            // lblNewClassMaterialFee
            // 
            this.lblNewClassMaterialFee.AutoSize = true;
            this.lblNewClassMaterialFee.Location = new System.Drawing.Point(34, 265);
            this.lblNewClassMaterialFee.Name = "lblNewClassMaterialFee";
            this.lblNewClassMaterialFee.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassMaterialFee.TabIndex = 52;
            this.lblNewClassMaterialFee.Text = "教材費用:";
            // 
            // txtNewClassMaterialFee
            // 
            this.txtNewClassMaterialFee.Location = new System.Drawing.Point(135, 259);
            this.txtNewClassMaterialFee.MaxLength = 5;
            this.txtNewClassMaterialFee.Name = "txtNewClassMaterialFee";
            this.txtNewClassMaterialFee.Size = new System.Drawing.Size(67, 24);
            this.txtNewClassMaterialFee.TabIndex = 50;
            this.txtNewClassMaterialFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewClassPeriod_KeyPress);
            // 
            // lblNewClassApplyFee
            // 
            this.lblNewClassApplyFee.AutoSize = true;
            this.lblNewClassApplyFee.Location = new System.Drawing.Point(34, 300);
            this.lblNewClassApplyFee.Name = "lblNewClassApplyFee";
            this.lblNewClassApplyFee.Size = new System.Drawing.Size(72, 14);
            this.lblNewClassApplyFee.TabIndex = 52;
            this.lblNewClassApplyFee.Text = "報名費用:";
            // 
            // txtNewClassApplyFee
            // 
            this.txtNewClassApplyFee.Location = new System.Drawing.Point(135, 294);
            this.txtNewClassApplyFee.MaxLength = 5;
            this.txtNewClassApplyFee.Name = "txtNewClassApplyFee";
            this.txtNewClassApplyFee.Size = new System.Drawing.Size(67, 24);
            this.txtNewClassApplyFee.TabIndex = 50;
            this.txtNewClassApplyFee.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNewClassPeriod_KeyPress);
            // 
            // lblInvisibleClassPrice
            // 
            this.lblInvisibleClassPrice.AutoSize = true;
            this.lblInvisibleClassPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lblInvisibleClassPrice.Location = new System.Drawing.Point(419, 125);
            this.lblInvisibleClassPrice.Name = "lblInvisibleClassPrice";
            this.lblInvisibleClassPrice.Size = new System.Drawing.Size(67, 14);
            this.lblInvisibleClassPrice.TabIndex = 44;
            this.lblInvisibleClassPrice.Text = "課程價格";
            this.lblInvisibleClassPrice.Visible = false;
            // 
            // lblInsertErrorMsgIsShow
            // 
            this.lblInsertErrorMsgIsShow.AutoSize = true;
            this.lblInsertErrorMsgIsShow.Location = new System.Drawing.Point(262, 210);
            this.lblInsertErrorMsgIsShow.Name = "lblInsertErrorMsgIsShow";
            this.lblInsertErrorMsgIsShow.Size = new System.Drawing.Size(66, 14);
            this.lblInsertErrorMsgIsShow.TabIndex = 87;
            this.lblInsertErrorMsgIsShow.Text = "ErrorMsg";
            this.lblInsertErrorMsgIsShow.Visible = false;
            // 
            // lblEachClassPrice
            // 
            this.lblEachClassPrice.AutoSize = true;
            this.lblEachClassPrice.Location = new System.Drawing.Point(262, 231);
            this.lblEachClassPrice.Name = "lblEachClassPrice";
            this.lblEachClassPrice.Size = new System.Drawing.Size(104, 14);
            this.lblEachClassPrice.TabIndex = 87;
            this.lblEachClassPrice.Text = "EachClassPrice";
            this.lblEachClassPrice.Visible = false;
            // 
            // lblFromStudentOrClass
            // 
            this.lblFromStudentOrClass.AutoSize = true;
            this.lblFromStudentOrClass.Location = new System.Drawing.Point(262, 253);
            this.lblFromStudentOrClass.Name = "lblFromStudentOrClass";
            this.lblFromStudentOrClass.Size = new System.Drawing.Size(137, 14);
            this.lblFromStudentOrClass.TabIndex = 87;
            this.lblFromStudentOrClass.Text = "FromStudentOrClass";
            this.lblFromStudentOrClass.Visible = false;
            // 
            // lblTempNewClassPeriod
            // 
            this.lblTempNewClassPeriod.AutoSize = true;
            this.lblTempNewClassPeriod.Location = new System.Drawing.Point(157, 41);
            this.lblTempNewClassPeriod.Name = "lblTempNewClassPeriod";
            this.lblTempNewClassPeriod.Size = new System.Drawing.Size(144, 14);
            this.lblTempNewClassPeriod.TabIndex = 89;
            this.lblTempNewClassPeriod.Text = "TempNewClassPeriod";
            this.lblTempNewClassPeriod.Visible = false;
            // 
            // frmStudentAddNewClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(520, 363);
            this.ControlBox = false;
            this.Controls.Add(this.lblTempNewClassPeriod);
            this.Controls.Add(this.lblFromStudentOrClass);
            this.Controls.Add(this.lblEachClassPrice);
            this.Controls.Add(this.lblInsertErrorMsgIsShow);
            this.Controls.Add(this.cbNewClassWednesday);
            this.Controls.Add(this.cbNewClassSaturday);
            this.Controls.Add(this.cbNewClassTuesday);
            this.Controls.Add(this.cbNewClassFriday);
            this.Controls.Add(this.cbNewClassMonday);
            this.Controls.Add(this.cbNewClassThursday);
            this.Controls.Add(this.cbNewClassSunday);
            this.Controls.Add(this.lblNewClassTime);
            this.Controls.Add(this.dtpNewClassEndDate);
            this.Controls.Add(this.dtpNewClassStartDate);
            this.Controls.Add(this.txtNewClassApplyFee);
            this.Controls.Add(this.txtNewClassMaterialFee);
            this.Controls.Add(this.txtNewClassPrice);
            this.Controls.Add(this.txtNewClassPeriod);
            this.Controls.Add(this.lblNewClassStartDate);
            this.Controls.Add(this.lblNewClassApplyFee);
            this.Controls.Add(this.lblNewClassMaterialFee);
            this.Controls.Add(this.lblNewClassPrice);
            this.Controls.Add(this.lblNewClassPeriod);
            this.Controls.Add(this.lblNewClassEndDate);
            this.Controls.Add(this.lblStudentAddClassClassID);
            this.Controls.Add(this.lblStudentAddClassClassName);
            this.Controls.Add(this.lblInvisibleClassPrice);
            this.Controls.Add(this.lblStudentManageClassShowStudentName);
            this.Controls.Add(this.lblStudentManageClassShowStudentID);
            this.Controls.Add(this.lblStudentAddClassShowClassName);
            this.Controls.Add(this.lblStudentAddClassShowClassID);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStudentAddClass);
            this.Font = new System.Drawing.Font("PMingLiU", 10.20895F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "frmStudentAddNewClass";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 新增課程";
            this.Load += new System.EventHandler(this.frmStudentAddNewClass_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStudentAddClass;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblStudentAddClassClassID;
        private System.Windows.Forms.Label lblStudentAddClassClassName;
        private System.Windows.Forms.Label lblStudentAddClassShowClassName;
        private System.Windows.Forms.Label lblStudentAddClassShowClassID;
        private System.Windows.Forms.CheckBox cbNewClassWednesday;
        private System.Windows.Forms.CheckBox cbNewClassSaturday;
        private System.Windows.Forms.CheckBox cbNewClassTuesday;
        private System.Windows.Forms.CheckBox cbNewClassFriday;
        private System.Windows.Forms.CheckBox cbNewClassMonday;
        private System.Windows.Forms.CheckBox cbNewClassThursday;
        private System.Windows.Forms.CheckBox cbNewClassSunday;
        private System.Windows.Forms.Label lblNewClassTime;
        private System.Windows.Forms.DateTimePicker dtpNewClassEndDate;
        private System.Windows.Forms.DateTimePicker dtpNewClassStartDate;
        private System.Windows.Forms.TextBox txtNewClassPeriod;
        private System.Windows.Forms.Label lblNewClassStartDate;
        private System.Windows.Forms.Label lblNewClassPeriod;
        private System.Windows.Forms.Label lblNewClassEndDate;
        private System.Windows.Forms.Label lblStudentManageClassShowStudentID;
        private System.Windows.Forms.Label lblStudentManageClassShowStudentName;
        private System.Windows.Forms.Label lblNewClassPrice;
        private System.Windows.Forms.TextBox txtNewClassPrice;
        private System.Windows.Forms.Label lblNewClassMaterialFee;
        private System.Windows.Forms.TextBox txtNewClassMaterialFee;
        private System.Windows.Forms.Label lblNewClassApplyFee;
        private System.Windows.Forms.TextBox txtNewClassApplyFee;
        private System.Windows.Forms.Label lblInvisibleClassPrice;
        private System.Windows.Forms.Label lblInsertErrorMsgIsShow;
        private System.Windows.Forms.Label lblEachClassPrice;
        private System.Windows.Forms.Label lblFromStudentOrClass;
        private System.Windows.Forms.Label lblTempNewClassPeriod;
    }
}