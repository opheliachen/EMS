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
    public partial class frmShowAllClasses : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;

        public frmShowAllClasses()
        {
            InitializeComponent();
        }

        private void frmShowAllClasses_Load(object sender, EventArgs e)
        {
            ShowAllClasses();
        }

        private void ShowAllClasses()
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            List<ClassDefinition> classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "whole", (object)"Class", null);

            if (dgvShowAllClasses.Columns.Count > 0)
                dgvShowAllClasses.Columns.Clear();

            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程編號";
            dgvShowAllClasses.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程名稱";
            dgvShowAllClasses.Columns.Add(newColumn);

            foreach (var classSingle in classSets)
            {
                if (classSingle.IsDeleted == '0')
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewCell newCell;

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classSingle.ID;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = classSingle.Name;
                    newRow.Cells.Add(newCell);

                    dgvShowAllClasses.Rows.Add(newRow);
                }
            }

            dgvShowAllClasses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvShowAllClasses.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvShowAllClasses.AllowUserToAddRows = false;

            if (dgvShowAllClasses.Rows.Count > 0)
                dgvShowAllClasses.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvShowAllClasses.Rows.Count; i++)
                dgvShowAllClasses.Rows[i].Resizable = DataGridViewTriState.False;
            dgvShowAllClasses.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvShowAllClasses.Columns.Count; i++)
            {
                dgvShowAllClasses.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvShowAllClasses.Columns[i].Resizable = DataGridViewTriState.False;
                dgvShowAllClasses.ReadOnly = true;
            }

            btnSelectClass.Enabled = false;
        }

        private void CloseShowAllClasses()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnableButton();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseShowAllClasses();
        }

        private void btnSelectClass_Click(object sender, EventArgs e)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;

            bool isSelect = false;
            int dgvRowIndex = 0;
            foreach (DataGridViewRow dgvRow in this.dgvShowAllClasses.Rows)
            {
                if (dgvRow.Selected)
                {
                    isSelect = true;
                    emsSystem.GetClassIDFromShowClasses(dgvRow.Cells[0].Value.ToString());
                }
                dgvRowIndex += 1;
            }

            if (isSelect)
                CloseShowAllClasses();
            else
                MessageBox.Show("請選擇課程!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void dgvShowAllClasses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvShowAllClasses_CellDoubleClick(sender, e);
        }

        private void dgvShowAllClasses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvShowAllClasses.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvShowAllClasses.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvShowAllClasses.ReadOnly = false;
                dgvShowAllClasses.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            if (selectItem == 0)
                btnSelectClass.Enabled = false;
            else if (selectItem == 1)
                btnSelectClass.Enabled = true;
            else if (selectItem > 1)
                btnSelectClass.Enabled = false;
        }

        private void btnPrintClass_Click(object sender, EventArgs e)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            facade.FacadeFunctions("reusefunction", "classlistbitmap", null, null);
        }
    }
}
