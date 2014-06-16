namespace EMSSystem
{
    partial class frmSelectPersonalOrClass
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
            this.btnSelectByPerson = new System.Windows.Forms.Button();
            this.btnSelectByClass = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectByPerson
            // 
            this.btnSelectByPerson.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectByPerson.Location = new System.Drawing.Point(69, 34);
            this.btnSelectByPerson.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectByPerson.Name = "btnSelectByPerson";
            this.btnSelectByPerson.Size = new System.Drawing.Size(130, 35);
            this.btnSelectByPerson.TabIndex = 0;
            this.btnSelectByPerson.Text = "個別查詢";
            this.btnSelectByPerson.UseVisualStyleBackColor = true;
            this.btnSelectByPerson.Click += new System.EventHandler(this.btnSelectByPerson_Click);
            // 
            // btnSelectByClass
            // 
            this.btnSelectByClass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectByClass.Location = new System.Drawing.Point(69, 79);
            this.btnSelectByClass.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSelectByClass.Name = "btnSelectByClass";
            this.btnSelectByClass.Size = new System.Drawing.Size(130, 35);
            this.btnSelectByClass.TabIndex = 0;
            this.btnSelectByClass.Text = "班別查詢";
            this.btnSelectByClass.UseVisualStyleBackColor = true;
            this.btnSelectByClass.Click += new System.EventHandler(this.btnSelectByClass_Click);
            // 
            // frmSelectPersonalOrClass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 22F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkBlue;
            this.ClientSize = new System.Drawing.Size(269, 148);
            this.Controls.Add(this.btnSelectByClass);
            this.Controls.Add(this.btnSelectByPerson);
            this.Font = new System.Drawing.Font("PMingLiU", 11.8209F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            this.Name = "frmSelectPersonalOrClass";
            this.Opacity = 0.9D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSelectPersonalOrClass";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSelectByPerson;
        private System.Windows.Forms.Button btnSelectByClass;
    }
}