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
    public partial class frmShowStudentRefundClass : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        List<ClassRefundDetailDefinition> classRefundDetailSets;
        string[] classRefundDataDetailGridViewHeaderText = { "學生編號", "學生姓名", "班級編號", "班級姓名", "繳費金額" };

        public frmShowStudentRefundClass()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        public void GetRefundDetail(List<ClassRefundDetailDefinition> classRefundDetail) 
        {
            classRefundDetailSets = classRefundDetail;

            if (dgvStudentRefundClass.Columns.Count > 0)
                dgvStudentRefundClass.Columns.Clear();

            //dgvStudentRefundClass.DataSource = classRefundDetail;

            //for (int i = 0; i < classRefundDataDetailGridViewHeaderText.Length; i++)
            //    dgvStudentRefundClass.Columns[i].HeaderText = classRefundDataDetailGridViewHeaderText[i];

            //dgvStudentRefundClass.Columns.Remove("RefundID");
            //dgvStudentRefundClass.Columns.Remove("ID");

            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生編號";
            dgvStudentRefundClass.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生姓名";
            dgvStudentRefundClass.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "班級編號";
            dgvStudentRefundClass.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "班級姓名";
            dgvStudentRefundClass.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "繳費金額";
            dgvStudentRefundClass.Columns.Add(newColumn);

            foreach (var classRefundSingle in classRefundDetail)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewCell newCell;

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classRefundSingle.StudentID;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classRefundSingle.StudentName;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classRefundSingle.ClassID;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classRefundSingle.ClassName;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classRefundSingle.HavePaid;
                newRow.Cells.Add(newCell);

                dgvStudentRefundClass.Rows.Add(newRow);
            }

            dgvStudentRefundClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudentRefundClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvStudentRefundClass.AllowUserToAddRows = false;

            if (dgvStudentRefundClass.Rows.Count > 0)
                dgvStudentRefundClass.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentRefundClass.Rows.Count; i++)
                dgvStudentRefundClass.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentRefundClass.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvStudentRefundClass.Columns.Count; i++)
            {
                dgvStudentRefundClass.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStudentRefundClass.Columns[i].Resizable = DataGridViewTriState.False;
                dgvStudentRefundClass.ReadOnly = true;
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
