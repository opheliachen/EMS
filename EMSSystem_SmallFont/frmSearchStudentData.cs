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
    public partial class frmSearchStudentData : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        List<ClassDefinition> classSets;
        List<StudentDefinition> studentSets;
        ClassDefinition classData;
        StudentDefinition studentData;

        public frmSearchStudentData()
        {
            InitializeComponent();
            SetStudentSearchDefault();
        }

        public frmSearchStudentData(string fromPanel)
        {
            InitializeComponent();
            SetStudentSearchDefault();
            lblFromPanel.Text = fromPanel;
        }

        private void SetStudentSearchDefault()
        {
            SetStudentSearchPanelDefault();

            btnStudentSearch.Enabled = false;
            txtStudentSearchByText.Visible = false;

            btnStudentSearchShowStudent.Enabled = false;
            btnStudentByStudentInClass.Enabled = false;

            txtStudentSearchByText.Text = "";
        }

        private void SetStudentSearchPanelDefault()
        {
            panelStudentSearchStudentInClass.Visible = false;
            panelStudentSearchShowClassList.Visible = false;

            if (dgvStudentSearchShowStudentInClassList.Columns.Count > 0)
                dgvStudentSearchShowStudentInClassList.Columns.Clear();

            if (dgvStudentSearchClassList.Columns.Count > 0)
                dgvStudentSearchClassList.Columns.Clear();
        }

        private void cboStudentSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudentSearchDefault();

            if (cboStudentSearchBy.SelectedIndex > -1)
            {
                btnStudentSearch.Enabled = true;

                txtStudentSearchByText.Visible = true;
                btnStudentSearch.Text = "搜 尋";
            }
        }

        private void btnStudentSearch_Click(object sender, EventArgs e)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            string selectBy = null, selectData = null;
            studentData = null;
            studentSets = null;
            classData = null;
            classSets = null;
            bool isRefund = false;

            SetStudentSearchPanelDefault();

            selectBy = CheckStudentSearchItem(cboStudentSearchBy.SelectedItem.ToString(), txtStudentSearchByText.Text.Trim(), "");
            selectData = txtStudentSearchByText.Text.Trim();
            lblStudentSearchInfo.Text = txtStudentSearchByText.Text.Trim();

            if (selectBy != null)
            {
                if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                {
                    if (selectBy == "ID")
                        studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)selectBy, (object)selectData);
                    else if (selectBy == "Name")
                    {
                        studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)StaticFunction.SetEncodingString(selectData));

                        if (studentSets == null)
                            studentSets = new List<StudentDefinition>();

                        List<StudentDefinition> tempStudentSet = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)selectData);
                        if (tempStudentSet != null && tempStudentSet.Count > 0)
                            studentSets.AddRange(tempStudentSet);
                    }

                    if (lblFromPanel.Text.IndexOf("05") > -1)
                    {
                        if (selectBy == "ID")
                        {
                            studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)"StudentID", (object)selectData);
                            if (studentSets != null && studentSets.Count > 0)
                                studentData = studentSets.ElementAt(0);
                        }
                    }

                    if (studentData != null && studentData.ID != null && int.Parse(studentData.ID) > 0)
                    {
                        SendSearchInfo(studentData.ID, studentData.Name);
                    }
                    else if (studentSets != null && studentSets.Count > 0)
                    {
                        panelStudentSearchStudentInClass.Visible = true;

                        lblStudentSearchClassID.Visible = false;
                        lblStudentSearchShowClassID.Visible = false;
                        lblStudentSearchClassName.Visible = false;
                        lblStudentSearchShowClassName.Visible = false;

                        SearchStudentShowStudentList();
                    }
                    else
                        MessageBox.Show("查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
                {
                    if (selectBy == "ID")
                        classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);
                    else if (selectBy == "Name")
                        classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);

                    if (lblFromPanel.Text.IndexOf("05") > -1)
                    {
                        if (selectBy == "ID" && (classData == null || classData.ID == null))
                        {
                            classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
                            isRefund = true;
                        }
                        else if (selectBy == "Name")
                        {
                            List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);

                            classSets.AddRange(tempClassSets.AsEnumerable());

                            isRefund = true;
                        }
                    }

                    if (classData != null && classData.ID != null)
                    {
                        panelStudentSearchStudentInClass.Visible = true;

                        lblStudentSearchClassID.Visible = true;
                        lblStudentSearchShowClassID.Visible = true;
                        lblStudentSearchClassName.Visible = true;
                        lblStudentSearchShowClassName.Visible = true;

                        lblStudentSearchShowClassID.Text = classData.ID;
                        lblStudentSearchShowClassName.Text = classData.Name;

                        studentSets = null;
                        studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

                        if (isRefund)
                            studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundinstudentlist", (object)"ClassID", (object)classData.ID);

                        if (studentSets != null)
                        {
                            if (studentSets.Count > 0)
                                SearchStudentShowStudentList();
                            else
                                MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (classSets != null && classSets.Count > 0)
                    {
                        panelStudentSearchShowClassList.Visible = true;

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
                if (item.IndexOf("學生") > -1)
                {
                    emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

                    if (item.IndexOf("編號") > -1)
                    {
                        //if (data.Length == 8)
                        //{
                        if ((bool)facade.FacadeFunctions("check", "number", data, null))
                        {
                            if (int.Parse(data) != 0)
                                selectItem = "ID";
                            else
                                MessageBox.Show(extraNote + "查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            MessageBox.Show(extraNote + "學生編號只能為數字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else
                        //    MessageBox.Show(extraNote + "學生編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        selectItem = "Name";
                }
                else if (item.IndexOf("班級") > -1)
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
                else if (item.IndexOf("員工") > -1)
                {
                    if (item.IndexOf("編號") > -1)
                    {
                        if (data.Length > 0)
                            if ((bool)facade.FacadeFunctions("check", "number", data, null))
                                selectItem = "ID";
                            else
                                MessageBox.Show(extraNote + "員工編號只能為數字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show(extraNote + "請輸入員工編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (item.IndexOf("姓名") > -1)
                    {
                        if (data.Length > 0)
                            selectItem = "Name";
                        else
                            MessageBox.Show(extraNote + "請輸入員工姓名!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        if (data.Length > 0)
                            selectItem = "EnglishName";
                        else
                            MessageBox.Show(extraNote + "請輸入員工英文名字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void SearchStudentShowStudentList()
        {
            if (dgvStudentSearchShowStudentInClassList.Columns.Count > 0)
                dgvStudentSearchShowStudentInClassList.Columns.Clear();

            //Add Student ID
            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生編號";
            dgvStudentSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生姓名";
            dgvStudentSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生性別";
            dgvStudentSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生生日";
            dgvStudentSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "就讀學校";
            dgvStudentSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "就讀年級";
            dgvStudentSearchShowStudentInClassList.Columns.Add(newColumn);

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

                dgvStudentSearchShowStudentInClassList.Rows.Add(newRow);
            }

            dgvStudentSearchShowStudentInClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudentSearchShowStudentInClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvStudentSearchShowStudentInClassList.AllowUserToAddRows = false;

            if (dgvStudentSearchShowStudentInClassList.Rows.Count > 0)
                dgvStudentSearchShowStudentInClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentSearchShowStudentInClassList.Rows.Count; i++)
                dgvStudentSearchShowStudentInClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentSearchShowStudentInClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvStudentSearchShowStudentInClassList.Columns.Count; i++)
            {
                dgvStudentSearchShowStudentInClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStudentSearchShowStudentInClassList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvStudentSearchShowStudentInClassList.ReadOnly = true;
            }

            btnStudentByStudentInClass.Enabled = false;
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

            btnStudentSearchShowStudent.Enabled = false;
        }

        private void dgvStudentSearchShowStudentInClassList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvStudentSearchShowStudentInClassList_CellDoubleClick(sender, e);
        }

        private void dgvStudentSearchShowStudentInClassList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvStudentSearchShowStudentInClassList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvStudentSearchShowStudentInClassList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentSearchShowStudentInClassList.ReadOnly = false;
                dgvStudentSearchShowStudentInClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            if (selectItem == 0)
                btnStudentByStudentInClass.Enabled = false;
            else if (selectItem == 1)
                btnStudentByStudentInClass.Enabled = true;
            else if (selectItem > 1)
                btnStudentByStudentInClass.Enabled = false;
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
                btnStudentSearchShowStudent.Enabled = false;
            else if (selectItem == 1)
                btnStudentSearchShowStudent.Enabled = true;
            else if (selectItem > 1)
                btnStudentSearchShowStudent.Enabled = false;
        }

        private void btnStudentSearchShowStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int selectIndex = -1;
                int countIndex = 0;

                if (dgvStudentSearchShowStudentInClassList.Columns.Count > 0)
                    dgvStudentSearchShowStudentInClassList.Columns.Clear();

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

                    btnStudentSearchReturnClassList.Visible = true;
                    panelStudentSearchStudentInClass.Visible = true;
                    panelStudentSearchShowClassList.Visible = false;

                    lblStudentSearchClassID.Visible = true;
                    lblStudentSearchShowClassID.Visible = true;
                    lblStudentSearchClassName.Visible = true;
                    lblStudentSearchShowClassName.Visible = true;

                    lblStudentSearchShowClassID.Text = classData.ID;
                    lblStudentSearchShowClassName.Text = classData.Name;

                    studentSets = null;

                    studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

                    if (studentSets != null)
                    {
                        if (studentSets.Count > 0)
                            SearchStudentShowStudentList();
                        else
                        {
                            MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            StudentSearchReturnClassList();
                        }
                    }
                    else
                    {
                        MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        StudentSearchReturnClassList();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnStudentSearchReturnClassList_Click(object sender, EventArgs e)
        {
            StudentSearchReturnClassList();
        }

        private void StudentSearchReturnClassList()
        {
            classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "class", (object)"Name", (object)lblStudentSearchInfo.Text);
            SearchStudentShowClassList();

            btnStudentSearchReturnClassList.Visible = false;
            panelStudentSearchStudentInClass.Visible = false;
            panelStudentSearchShowClassList.Visible = true;
        }

        //private void btnStudentByStudentID_Click(object sender, EventArgs e)
        //{
        //    SendSearchInfo(lblStudentSearchShowStudentID.Text, lblStudentSearchShowStudentName.Text);
        //}

        private void btnStudentByStudentInClass_Click(object sender, EventArgs e)
        {
            try
            {
                int selectIndex = -1;
                int countIndex = 0;

                foreach (DataGridViewRow dgvRow in this.dgvStudentSearchShowStudentInClassList.Rows)
                {
                    if (dgvRow.Selected)
                        selectIndex = countIndex;

                    countIndex++;
                }

                studentData = null;
                studentData = studentSets.ElementAt(selectIndex);

                if (lblFromPanel.Text.IndexOf("05") > -1)
                {
                    List<StudentDefinition> tempStudentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)"StudentID", (object)studentData.ID);
                    if (tempStudentSets != null && tempStudentSets.Count > 0)
                    {
                        studentData = tempStudentSets.ElementAt(0);
                        SendSearchInfo(studentData.ID, studentData.Name);
                    }
                    else
                        MessageBox.Show("此學生無需退費!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    SendSearchInfo(studentData.ID, studentData.Name);
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
