namespace EMSSystem
{
    partial class frmNewClassTime
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.lblNewClassDay = new System.Windows.Forms.Label();
            this.cboNewClassDay = new System.Windows.Forms.ComboBox();
            this.lblNewClassTime = new System.Windows.Forms.Label();
            this.lblNewClassFrom = new System.Windows.Forms.Label();
            this.lblNewClassTo = new System.Windows.Forms.Label();
            this.cboNewClassFromHour = new System.Windows.Forms.ComboBox();
            this.cboNewClassToHour = new System.Windows.Forms.ComboBox();
            this.cboNewClassFromMiunte = new System.Windows.Forms.ComboBox();
            this.cboNewClassToMiunte = new System.Windows.Forms.ComboBox();
            this.lblNewClass24HR = new System.Windows.Forms.Label();
            this.lblNewClassTimeClassDayErrorMsg = new System.Windows.Forms.Label();
            this.lblNewClassTimeClassFromTimeErrorMsg = new System.Windows.Forms.Label();
            this.lblNewClassTimeClassToTimeErrorMsg = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(325, 114);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取 消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Enabled = false;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Location = new System.Drawing.Point(217, 114);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "新 增";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // lblNewClassDay
            // 
            this.lblNewClassDay.AutoSize = true;
            this.lblNewClassDay.Location = new System.Drawing.Point(42, 19);
            this.lblNewClassDay.Name = "lblNewClassDay";
            this.lblNewClassDay.Size = new System.Drawing.Size(68, 16);
            this.lblNewClassDay.TabIndex = 1;
            this.lblNewClassDay.Text = "上課日:";
            // 
            // cboNewClassDay
            // 
            this.cboNewClassDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewClassDay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNewClassDay.FormattingEnabled = true;
            this.cboNewClassDay.Items.AddRange(new object[] {
            "星期一",
            "星期二",
            "星期三",
            "星期四",
            "星期五",
            "星期六",
            "星期日"});
            this.cboNewClassDay.Location = new System.Drawing.Point(116, 16);
            this.cboNewClassDay.Name = "cboNewClassDay";
            this.cboNewClassDay.Size = new System.Drawing.Size(121, 24);
            this.cboNewClassDay.TabIndex = 2;
            // 
            // lblNewClassTime
            // 
            this.lblNewClassTime.AutoSize = true;
            this.lblNewClassTime.Location = new System.Drawing.Point(25, 56);
            this.lblNewClassTime.Name = "lblNewClassTime";
            this.lblNewClassTime.Size = new System.Drawing.Size(85, 16);
            this.lblNewClassTime.TabIndex = 1;
            this.lblNewClassTime.Text = "上課時間:";
            // 
            // lblNewClassFrom
            // 
            this.lblNewClassFrom.AutoSize = true;
            this.lblNewClassFrom.Location = new System.Drawing.Point(113, 56);
            this.lblNewClassFrom.Name = "lblNewClassFrom";
            this.lblNewClassFrom.Size = new System.Drawing.Size(25, 16);
            this.lblNewClassFrom.TabIndex = 1;
            this.lblNewClassFrom.Text = "從";
            // 
            // lblNewClassTo
            // 
            this.lblNewClassTo.AutoSize = true;
            this.lblNewClassTo.Location = new System.Drawing.Point(113, 88);
            this.lblNewClassTo.Name = "lblNewClassTo";
            this.lblNewClassTo.Size = new System.Drawing.Size(25, 16);
            this.lblNewClassTo.TabIndex = 1;
            this.lblNewClassTo.Text = "到";
            // 
            // cboNewClassFromHour
            // 
            this.cboNewClassFromHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewClassFromHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNewClassFromHour.FormattingEnabled = true;
            this.cboNewClassFromHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cboNewClassFromHour.Location = new System.Drawing.Point(144, 53);
            this.cboNewClassFromHour.Name = "cboNewClassFromHour";
            this.cboNewClassFromHour.Size = new System.Drawing.Size(74, 24);
            this.cboNewClassFromHour.TabIndex = 2;
            this.cboNewClassFromHour.SelectedIndexChanged += new System.EventHandler(this.cboNewClassFromHour_SelectedIndexChanged);
            // 
            // cboNewClassToHour
            // 
            this.cboNewClassToHour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewClassToHour.Enabled = false;
            this.cboNewClassToHour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNewClassToHour.FormattingEnabled = true;
            this.cboNewClassToHour.Items.AddRange(new object[] {
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"});
            this.cboNewClassToHour.Location = new System.Drawing.Point(144, 83);
            this.cboNewClassToHour.Name = "cboNewClassToHour";
            this.cboNewClassToHour.Size = new System.Drawing.Size(74, 24);
            this.cboNewClassToHour.TabIndex = 2;
            // 
            // cboNewClassFromMiunte
            // 
            this.cboNewClassFromMiunte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewClassFromMiunte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNewClassFromMiunte.FormattingEnabled = true;
            this.cboNewClassFromMiunte.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.cboNewClassFromMiunte.Location = new System.Drawing.Point(224, 53);
            this.cboNewClassFromMiunte.Name = "cboNewClassFromMiunte";
            this.cboNewClassFromMiunte.Size = new System.Drawing.Size(74, 24);
            this.cboNewClassFromMiunte.TabIndex = 2;
            // 
            // cboNewClassToMiunte
            // 
            this.cboNewClassToMiunte.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNewClassToMiunte.Enabled = false;
            this.cboNewClassToMiunte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNewClassToMiunte.FormattingEnabled = true;
            this.cboNewClassToMiunte.Items.AddRange(new object[] {
            "00",
            "15",
            "30",
            "45"});
            this.cboNewClassToMiunte.Location = new System.Drawing.Point(224, 83);
            this.cboNewClassToMiunte.Name = "cboNewClassToMiunte";
            this.cboNewClassToMiunte.Size = new System.Drawing.Size(74, 24);
            this.cboNewClassToMiunte.TabIndex = 2;
            // 
            // lblNewClass24HR
            // 
            this.lblNewClass24HR.AutoSize = true;
            this.lblNewClass24HR.Location = new System.Drawing.Point(322, 56);
            this.lblNewClass24HR.Name = "lblNewClass24HR";
            this.lblNewClass24HR.Size = new System.Drawing.Size(95, 16);
            this.lblNewClass24HR.TabIndex = 1;
            this.lblNewClass24HR.Text = "(24小時制)";
            // 
            // lblNewClassTimeClassDayErrorMsg
            // 
            this.lblNewClassTimeClassDayErrorMsg.AutoSize = true;
            this.lblNewClassTimeClassDayErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblNewClassTimeClassDayErrorMsg.Location = new System.Drawing.Point(243, 19);
            this.lblNewClassTimeClassDayErrorMsg.Name = "lblNewClassTimeClassDayErrorMsg";
            this.lblNewClassTimeClassDayErrorMsg.Size = new System.Drawing.Size(17, 16);
            this.lblNewClassTimeClassDayErrorMsg.TabIndex = 3;
            this.lblNewClassTimeClassDayErrorMsg.Text = "*";
            // 
            // lblNewClassTimeClassFromTimeErrorMsg
            // 
            this.lblNewClassTimeClassFromTimeErrorMsg.AutoSize = true;
            this.lblNewClassTimeClassFromTimeErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblNewClassTimeClassFromTimeErrorMsg.Location = new System.Drawing.Point(304, 56);
            this.lblNewClassTimeClassFromTimeErrorMsg.Name = "lblNewClassTimeClassFromTimeErrorMsg";
            this.lblNewClassTimeClassFromTimeErrorMsg.Size = new System.Drawing.Size(17, 16);
            this.lblNewClassTimeClassFromTimeErrorMsg.TabIndex = 3;
            this.lblNewClassTimeClassFromTimeErrorMsg.Text = "*";
            // 
            // lblNewClassTimeClassToTimeErrorMsg
            // 
            this.lblNewClassTimeClassToTimeErrorMsg.AutoSize = true;
            this.lblNewClassTimeClassToTimeErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lblNewClassTimeClassToTimeErrorMsg.Location = new System.Drawing.Point(304, 86);
            this.lblNewClassTimeClassToTimeErrorMsg.Name = "lblNewClassTimeClassToTimeErrorMsg";
            this.lblNewClassTimeClassToTimeErrorMsg.Size = new System.Drawing.Size(17, 16);
            this.lblNewClassTimeClassToTimeErrorMsg.TabIndex = 3;
            this.lblNewClassTimeClassToTimeErrorMsg.Text = "*";
            // 
            // frmNewClassTime
            // 
            this.AcceptButton = this.btnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(435, 163);
            this.ControlBox = false;
            this.Controls.Add(this.lblNewClassTimeClassToTimeErrorMsg);
            this.Controls.Add(this.lblNewClassTimeClassFromTimeErrorMsg);
            this.Controls.Add(this.lblNewClassTimeClassDayErrorMsg);
            this.Controls.Add(this.cboNewClassToHour);
            this.Controls.Add(this.cboNewClassToMiunte);
            this.Controls.Add(this.cboNewClassFromMiunte);
            this.Controls.Add(this.cboNewClassFromHour);
            this.Controls.Add(this.cboNewClassDay);
            this.Controls.Add(this.lblNewClassTo);
            this.Controls.Add(this.lblNewClass24HR);
            this.Controls.Add(this.lblNewClassFrom);
            this.Controls.Add(this.lblNewClassTime);
            this.Controls.Add(this.lblNewClassDay);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmNewClassTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " 新增課程時間";
            this.Load += new System.EventHandler(this.frmNewClassTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label lblNewClassDay;
        private System.Windows.Forms.ComboBox cboNewClassDay;
        private System.Windows.Forms.Label lblNewClassTime;
        private System.Windows.Forms.Label lblNewClassFrom;
        private System.Windows.Forms.Label lblNewClassTo;
        private System.Windows.Forms.ComboBox cboNewClassFromHour;
        private System.Windows.Forms.ComboBox cboNewClassToHour;
        private System.Windows.Forms.ComboBox cboNewClassFromMiunte;
        private System.Windows.Forms.ComboBox cboNewClassToMiunte;
        private System.Windows.Forms.Label lblNewClass24HR;
        private System.Windows.Forms.Label lblNewClassTimeClassDayErrorMsg;
        private System.Windows.Forms.Label lblNewClassTimeClassFromTimeErrorMsg;
        private System.Windows.Forms.Label lblNewClassTimeClassToTimeErrorMsg;
    }
}