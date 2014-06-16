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
    public partial class frmSearchClassData : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        List<ClassDefinition> classSets;
        List<StudentDefinition> studentSets;
        ClassDefinition classData;
        
        public frmSearchClassData()
        {
            InitializeComponent();
            SetClassSearchDefault();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        public frmSearchClassData(string fromPanel)
        {
            InitializeComponent();
            SetClassSearchDefault();
            lblFromPanel.Text = fromPanel;
        }

        private void SetClassSearchDefault()
        {
            SetStudentSearchPanelDefault();

            btnClassSearch.Enabled = false;
            txtClassSearchByText.Visible = false;

            btnClassSearchSelectClass.Enabled = false;

            txtClassSearchByText.Text = "";
        }

        private void SetStudentSearchPanelDefault()
        {
            panelClassSearchShowClassList.Visible = false;

            if (dgvStudentSearchClassList.Columns.Count > 0)
                dgvStudentSearchClassList.Columns.Clear();
        }

        private void cboStudentSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetClassSearchDefault();

            if (cboClassSearchBy.SelectedIndex > -1)
            {
                btnClassSearch.Enabled = true;

                txtClassSearchByText.Visible = true;
                btnClassSearch.Text = "搜 尋";
            }
        }

        private void btnStudentSearch_Click(object sender, EventArgs e)
        {
            string selectBy = null, selectData = null;
            studentSets = null;
            classData = null;
            classSets = null;
            bool isRefund = false;

            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            SetStudentSearchPanelDefault();

            selectBy = CheckStudentSearchItem(cboClassSearchBy.SelectedItem.ToString(), txtClassSearchByText.Text.Trim(), "");
            selectData = txtClassSearchByText.Text.Trim();
            lblClassSearchInfo.Text = txtClassSearchByText.Text.Trim();

            if (selectBy != null)
            {
                if (cboClassSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
                {
                    if (selectBy == "ID")
                        classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);
                    else if (selectBy == "Name")
                        classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);

                    if (lblFromPanel.Text.IndexOf("05") > -1)
                    {
                        if (selectBy == "ID")
                            classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
                        else if (selectBy == "Name")
                            classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);

                        isRefund = true;
                    }

                    if (classData != null && classData.ID != null)
                    {
                        studentSets = null;
                        studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

                        if (isRefund)
                            studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundinstudentlist", (object)"ClassID", (object)classData.ID);

                        if (studentSets != null)
                        {
                            if (studentSets.Count > 0)
                                SendSearchInfo(classData.ID, classData.Name);
                            else
                                MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (classSets != null && classSets.Count > 0)
                    {
                        panelClassSearchShowClassList.Visible = true;

                        SearchStudentShowClassList();
                    }
                    else
                        MessageBox.Show("查無此班級資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private string CheckStudentSearchItem(string item, string data, string extraNote)
        {
            string selectItem = null;

            if (data != "")
            {
                if (item.IndexOf("班級") > -1)
                {
                    if (item.IndexOf("編號") > -1)
                    {
                        if (data.Length > 0)
                        {
                            if (data.Length <= 7)
                                selectItem = "ID";
                            else
                                MessageBox.Show(extraNote + "班級編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show(extraNote + "請輸入班級編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (data.Length > 0)
                            selectItem = "Name";
                        else
                            MessageBox.Show(extraNote + "請輸入班級名稱!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                if (extraNote == "")
                    MessageBox.Show(extraNote + "請輸入查詢資料!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return selectItem;
        }

        private void SearchStudentShowClassList()
        {
            if (dgvStudentSearchClassList.Columns.Count > 0)
                dgvStudentSearchClassList.Columns.Clear();

            //Add Student ID
            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程編號";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程名稱";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "起始日期";
            //dgvStudentSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "結束日期";
            //dgvStudentSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "目前人數";
            //dgvStudentSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "課程價格";
            //dgvStudentSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "教材費用";
            //dgvStudentSearchClassList.Columns.Add(newColumn);

            foreach (var classSingle in classSets)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewCell newCell;

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.ID;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.Name;
                newRow.Cells.Add(newCell);

                //newCell = new DataGridViewTextBoxCell();
                //newCell.Value = classSingle.StartDate;
                //newRow.Cells.Add(newCell);

                //newCell = new DataGridViewTextBoxCell();
                //newCell.Value = classSingle.EndDate;
                //newRow.Cells.Add(newCell);

                //newCell = new DataGridViewTextBoxCell();
                //newCell.Value = classSingle.Seat;
                //newRow.Cells.Add(newCell);

                //newCell = new DataGridViewTextBoxCell();
                //newCell.Value = classSingle.Price;
                //newRow.Cells.Add(newCell);

                //newCell = new DataGridViewTextBoxCell();
                //newCell.Value = classSingle.MaterialFee;
                //newRow.Cells.Add(newCell);

                dgvStudentSearchClassList.Rows.Add(newRow);
            }

            dgvStudentSearchClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudentSearchClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvStudentSearchClassList.AllowUserToAddRows = false;

            if (dgvStudentSearchClassList.Rows.Count > 0)
                dgvStudentSearchClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentSearchClassList.Rows.Count; i++)
                dgvStudentSearchClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentSearchClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvStudentSearchClassList.Columns.Count; i++)
            {
                dgvStudentSearchClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStudentSearchClassList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvStudentSearchClassList.ReadOnly = true;
            }

            btnClassSearchSelectClass.Enabled = false;
        }

        private void dgvStudentSearchClassList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvStudentSearchClassList_CellDoubleClick(sender, e);
        }

        private void dgvStudentSearchClassList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvStudentSearchClassList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvStudentSearchClassList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentSearchClassList.ReadOnly = false;
                selectItem++;
            }

            if (selectItem == 0)
                btnClassSearchSelectClass.Enabled = false;
            else if (selectItem == 1)
                btnClassSearchSelectClass.Enabled = true;
            else if (selectItem > 1)
                btnClassSearchSelectClass.Enabled = false;
        }

        private void btnStudentSearchShowStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int selectIndex = -1;
                int countIndex = 0;

                emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

                foreach (DataGridViewRow dgvRow in this.dgvStudentSearchClassList.Rows)
                {
                    if (dgvRow.Selected)
                        selectIndex = countIndex;

                    countIndex++;
                }

                if (classSets != null && classSets.Count > 0)
                {
                    classData = null;
                    classData = classSets.ElementAt(selectIndex);
                }

                if (classData != null && classData.ID != null)
                {
                    studentSets = null;

                    studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

                    if (studentSets != null)
                    {
                        if (studentSets.Count > 0)
                            SendSearchInfo(classData.ID, classData.Name);
                        else
                            MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
            }
        }

        private void SendSearchInfo(string id, string name)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.AfterStudentSearch(id, name);
            emsSystem.EnablefrmEMS();
            this.Close();
            //CloseSearchStudentData();
        }

        private void CloseSearchStudentData()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMSAndRestart();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseSearchStudentData();
        }
    }
}
