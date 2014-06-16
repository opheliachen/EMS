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
    public partial class frmStudentPaymentHistory : Form
    {
        frmEMS emsSystem = new frmEMS();

        public frmStudentPaymentHistory()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 10F, System.Drawing.FontStyle.Bold);
            }
        }

        public void GetStudentInClassData(List<ClassPaymentDefinition> classPaymentSets)
        {
            if (classPaymentSets != null && classPaymentSets.Count > 0)
            {
                if (dgvStudentPaymentHistory.Columns.Count > 0)
                    dgvStudentPaymentHistory.Columns.Clear();

                DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "課程編號";
                dgvStudentPaymentHistory.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "課程名稱";
                dgvStudentPaymentHistory.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "繳費日期";
                dgvStudentPaymentHistory.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "繳費金額";
                dgvStudentPaymentHistory.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "繳費方式";
                dgvStudentPaymentHistory.Columns.Add(newColumn);

                foreach (var classPaymentSingle in classPaymentSets)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewCell newCell;

                    lblShowStudentID.Text = classPaymentSingle.StudentID;
                    lblShowStudentName.Text = classPaymentSingle.StudentName;

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classPaymentSingle.ClassID;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classPaymentSingle.ClassName;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classPaymentSingle.PayDate;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classPaymentSingle.Paid.ToString();
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classPaymentSingle.PaymentType;
                    newRow.Cells.Add(newCell);

                    dgvStudentPaymentHistory.Rows.Add(newRow);
                }

                dgvStudentPaymentHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                dgvStudentPaymentHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentPaymentHistory.AllowUserToAddRows = false;

                dgvStudentPaymentHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentPaymentHistory.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentPaymentHistory.AllowUserToAddRows = false;

                if (dgvStudentPaymentHistory.Rows.Count > 0)
                    dgvStudentPaymentHistory.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentPaymentHistory.Rows.Count; i++)
                    dgvStudentPaymentHistory.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentPaymentHistory.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvStudentPaymentHistory.Columns.Count; i++)
                {
                    dgvStudentPaymentHistory.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentPaymentHistory.Columns[i].Resizable = DataGridViewTriState.False;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMS();
            emsSystem.EnableButton();
            this.Close();
        }
    }
}
