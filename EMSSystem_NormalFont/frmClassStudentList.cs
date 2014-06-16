using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EMSSystem.ClassLibrary;
using EMSSystem.Functions;

namespace EMSSystem
{
    public partial class frmClassStudentList : Form
    {
        frmEMS emsSystem = new frmEMS();
        string[] studentDataGridViewHeaderText = { "學生編號", "學生姓名", "學生性別", "學生生日", "就讀學校", "就讀年級", "父親姓名", "父親工作", "母親姓名", "母親工作", "負責家長", "負責家長電話", "緊急連絡人", "緊急連絡電話", "學生地址" };

        public frmClassStudentList()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        public void GetStudentInClassData(string classID, string className, List<StudentDefinition> studentSets)
        {
            if (studentSets != null && studentSets.Count > 0)
            {
                lblShowClassID.Text = classID;
                lblShowClassName.Text = className;

                if (dgvClassStudentList.Columns.Count > 0)
                    dgvClassStudentList.Columns.Clear();

                dgvClassStudentList.DataSource = studentSets;

                dgvClassStudentList.Columns.RemoveAt(dgvClassStudentList.Columns.Count - 1);

                for (int i = 0; i < studentDataGridViewHeaderText.Length; i++)
                    dgvClassStudentList.Columns[i + 1].HeaderText = studentDataGridViewHeaderText[i];

                dgvClassStudentList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvClassStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvClassStudentList.AllowUserToAddRows = false;

                if (dgvClassStudentList.Rows.Count > 0)
                    dgvClassStudentList.Rows[0].Selected = false;

                for (int i = 0; i < dgvClassStudentList.Columns.Count; i++)
                    dgvClassStudentList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnableButton();
            this.Close();
        }
    }
}
