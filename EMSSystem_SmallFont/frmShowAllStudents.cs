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
using EMSSystem.StaticFunctions;

namespace EMSSystem
{
    public partial class frmShowAllStudents : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        List<StudentDefinition> studentSets;
        //string[] studentDataGridViewHeaderText = { "學生編號", "學生姓名", "學生性別", "學生生日", "就讀學校", "就讀年級", "父親姓名", "父親工作", "母親姓名", "母親工作", "負責家長", "負責家長電話", "緊急連絡人", "緊急連絡電話", "學生地址" };
        string[] studentDataGridViewHeaderText = { "學生編號", "學生姓名", "學生性別", "學生生日", "就讀學校", "父親姓名", "母親姓名", "負責家長" };

        public frmShowAllStudents()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 10F, System.Drawing.FontStyle.Bold);
            }
        }

        public bool GetStudentName(string studentName)
        {
            if (dgvClassStudentList.Columns.Count > 0)
                dgvClassStudentList.Columns.Clear();

            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", "Name", StaticFunction.SetEncodingString(studentName));

            if (studentSets == null || studentSets.Count == 0)
                studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", "Name", studentName);

            if (studentSets != null && studentSets.Count > 0)
            {
                DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "學生編號";
                dgvClassStudentList.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "學生姓名";
                dgvClassStudentList.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "學生性別";
                dgvClassStudentList.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "學生生日";
                dgvClassStudentList.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "就讀學校";
                dgvClassStudentList.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "就讀年級";
                dgvClassStudentList.Columns.Add(newColumn);

                foreach (var studentSingle in studentSets)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewCell newCell;

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = studentSingle.ID;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = studentSingle.Name;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = studentSingle.Sex;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = studentSingle.DateOfBirth;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = studentSingle.School;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = studentSingle.StudyYear;
                    newRow.Cells.Add(newCell);

                    dgvClassStudentList.Rows.Add(newRow);
                }

                dgvClassStudentList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClassStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvClassStudentList.AllowUserToAddRows = false;

                if (dgvClassStudentList.Rows.Count > 0)
                    dgvClassStudentList.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvClassStudentList.Rows.Count; i++)
                    dgvClassStudentList.Rows[i].Resizable = DataGridViewTriState.False;
                dgvClassStudentList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvClassStudentList.Columns.Count; i++)
                {
                    dgvClassStudentList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvClassStudentList.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvClassStudentList.ReadOnly = true;
                }

                return true;
            }
            else
            {
                MessageBox.Show("查無此學生姓名!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CloseShowAllStudents();
                return false;
            }
        }

        private void CloseShowAllStudents()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnableButton();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseShowAllStudents();
        }

        private void btnSelectStudent_Click(object sender, EventArgs e)
        {
            StudentDefinition studentData = null;

            bool isSelect = false;
            int dgvRowIndex = 0;
            foreach (DataGridViewRow dgvRow in this.dgvClassStudentList.Rows)
            {
                if (dgvRow.Selected)
                {
                    isSelect = true;
                    studentData = studentSets.ElementAt(dgvRowIndex);
                }

                dgvRowIndex += 1;
            }

            if (isSelect)
            {
                emsSystem = new frmEMS();
                emsSystem = (frmEMS)this.Owner;
                emsSystem.LoadStudentDataByStudentName(studentData);
                CloseShowAllStudents();
            }
            else
                MessageBox.Show("請選擇學生!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
