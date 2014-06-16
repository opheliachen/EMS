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
    public partial class frmSearchRecordData : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        List<ClassDefinition> classSets;
        List<StudentDefinition> studentSets;
        List<RecordDefinition> recordSets;
        ClassDefinition classData;
        StudentDefinition studentData;
        StudentPaymentManager studentPaymentManager;

        public frmSearchRecordData()
        {
            InitializeComponent();
            SetRecordSearchDefault();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 10F, System.Drawing.FontStyle.Bold);
            }
        }

        public frmSearchRecordData(string fromPanel)
        {
            InitializeComponent();
            SetRecordSearchDefault();
            lblFromPanel.Text = fromPanel;
            lblOriginalFromPanel.Text = fromPanel;
            //lblPersonOrClass.Text = personOrClass;
            SetSearchFunction();
        }

        private void ReloadRecordSearchComboBox()
        {
            cboRecordSearchBy.Items.Clear();
            cboRecordSearchBy.Items.Add("學生編號");
            cboRecordSearchBy.Items.Add("學生姓名");
            cboRecordSearchBy.Items.Add("班級編號");
            cboRecordSearchBy.Items.Add("班級名稱");
        }

        private void SetSearchFunction()
        {
            ReloadRecordSearchComboBox();
            if (lblFromPanel.Text.IndexOf("全班") > -1)
            {
                cboRecordSearchBy.Items.RemoveAt(1);
                cboRecordSearchBy.Items.RemoveAt(0);
            }

            if (lblFromPanel.Text.IndexOf("04") > -1 || lblFromPanel.Text.IndexOf("06") > -1)
                panelRecordSearchByDate.Visible = false;
            else
                panelRecordSearchByDate.Visible = true;

            if (lblFromPanel.Text.IndexOf("05") > -1 || (lblFromPanel.Text.IndexOf("11") > -1 && lblFromPanel.Text.IndexOf("預收") == -1))
            {
                this.Enabled = false;
                frmSelectPersonalOrClass selectPersonalOrClass = new frmSelectPersonalOrClass();
                selectPersonalOrClass.Owner = this;
                selectPersonalOrClass.Show();
            }

            txtRecordSearch.Text = "";
            txtRecordSearchEndContinueNumber.Text = "";
        }

        public void SearchByPersonOrClass(string selectBy)
        {
            this.Enabled = true;
            lblFromPanel.Text = lblOriginalFromPanel.Text + selectBy;

            ReloadRecordSearchComboBox();
            if (lblFromPanel.Text.IndexOf("全班") > -1)
            {
                cboRecordSearchBy.Items.RemoveAt(1);
                cboRecordSearchBy.Items.RemoveAt(0);
            }
        }

        private void frmSearchRecordData_Load(object sender, EventArgs e)
        {
            cboRecordSearchBy.Visible = true;
            cboRecordSearchBy.SelectedIndex = -1;
            btnRecordSearch.Enabled = true;

            dtpRecordSearchFromDate.Checked = false;
            dtpRecordSearchEndDate.Checked = false;
        }

        private void SetRecordSearchDefault()
        {
            SetRecordSearchPanelDefault();

            panelRecordSearchByDate.Visible = false;
            dtpRecordSearchFromDate.Checked = false;
            dtpRecordSearchEndDate.Checked = false;
            dtpRecordSearchFromDate.Value = DateTime.Now;
            dtpRecordSearchEndDate.Value = DateTime.Now;
            txtRecordSearch.Visible = false;
            txtRecordSearch.Text = "";
            txtRecordSearchEndContinueNumber.Text = "";
            panelRecordSearchContinueNumber.Visible = false;
            btnRecordSearch.Enabled = false;
            cboRecordSearchBy.SelectedIndex = -1;

            lblRecordSearchShowFromDate.Text = "";
            lblRecordSearchShowEndDate.Text = "";

            if (lblFromPanel.Text.IndexOf("06") > -1)
                SearchByPersonOrClass(cboRecordSearchBy.SelectedItem.ToString());
        }

        private void SetRecordSearchPanelDefault()
        {
            panelRecordSearchStudentInClass.Visible = false;
            panelRecordSearchShowClassList.Visible = false;

            if (dgvRecordSearchShowStudentInClassList.Columns.Count > 0)
                dgvRecordSearchShowStudentInClassList.Columns.Clear();

            if (dgvRecordSearchClassList.Columns.Count > 0)
                dgvRecordSearchClassList.Columns.Clear();
        }

        private void cboRecordSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRecordSearchBy.SelectedIndex > -1)
            {
                SetRecordSearchPanelDefault();

                btnRecordSearch.Enabled = true;
                txtRecordSearch.Visible = true;
                txtRecordSearch.Text = "";

                if (lblFromPanel.Text.IndexOf("04") > -1 || lblFromPanel.Text.IndexOf("06") > -1)
                {
                    if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("學生編號") > -1)
                        panelRecordSearchContinueNumber.Visible = true;
                    else
                        panelRecordSearchContinueNumber.Visible = false;
                }
            }
        }

        private void btnRecordSearch_Click(object sender, EventArgs e)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            string selectBy = null, selectData = null;
            string continueNumber = null;
            bool isContinueNumberOK = true;
            object catchObject = null;
            classSets = null;

            SetRecordSearchPanelDefault();
            recordSets = new List<RecordDefinition>();

            if (cboRecordSearchBy.SelectedIndex > -1)
                selectBy = cboRecordSearchBy.SelectedItem.ToString();

            selectBy = CheckRecordSearchItem(selectBy, txtRecordSearch.Text.Trim(), "查詢條件: ");
            selectData = txtRecordSearch.Text.Trim();
            lblRecordSearchInfo.Text = txtRecordSearch.Text.Trim();

            if (txtRecordSearchEndContinueNumber.Text.Trim() != "")
            {
                selectBy = CheckRecordSearchItem(cboRecordSearchBy.SelectedItem.ToString(), txtRecordSearchEndContinueNumber.Text.Trim(), "連號條件: ");
                continueNumber = txtRecordSearchEndContinueNumber.Text.Trim();

                if (selectBy != null)
                    isContinueNumberOK = CheckContinueNumber();
            }

            if (selectBy != null && isContinueNumberOK)
            {
                if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                {
                    if (selectBy == "ID" && continueNumber != null)
                    {
                        int firstID = int.Parse(selectData);
                        int lastID = int.Parse(selectData);

                        if (continueNumber != "")
                            lastID = int.Parse(continueNumber);

                        int numberGap = lastID - firstID;

                        if (numberGap < 0)
                        {
                            int tempID = firstID;
                            firstID = lastID;
                            lastID = tempID;
                            numberGap = lastID - firstID;
                        }

                        if (numberGap != 0)
                        {
                            for (int i = firstID; i <= lastID; i++)
                            {
                                catchObject = SelectRecordSearch("Student", selectBy, i.ToString());

                                if (catchObject != null)
                                {
                                    foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                        recordSets.Add(recordSingle);
                                }
                            }
                        }
                        else
                            catchObject = SelectRecordSearch("Student", selectBy, selectData);
                    }
                    else if (selectBy == "Name")
                    {
                        studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)StaticFunction.SetEncodingString(selectData));

                        if (studentSets == null)
                            studentSets = new List<StudentDefinition>();

                        List<StudentDefinition> tempStudentSet = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)selectData);
                        if (tempStudentSet != null && tempStudentSet.Count > 0)
                            studentSets.AddRange(tempStudentSet);


                        foreach (var studentSingle in studentSets)
                        {
                            catchObject = SelectRecordSearch("Student", "ID", studentSingle.ID);

                            if (catchObject != null)
                            {
                                foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                    recordSets.Add(recordSingle);
                            }
                        }
                    }
                    else
                        catchObject = SelectRecordSearch("Student", selectBy, selectData);
                }
                else if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
                {
                    if (selectBy == "ID" && continueNumber != null)
                    {
                        int firstID = int.Parse(selectData.Substring(int.Parse(lblRecordSearchClassIDLastLetterIndex.Text) + 1));
                        int lastID = int.Parse(selectData.Substring(int.Parse(lblRecordSearchClassIDLastLetterIndex.Text) + 1));

                        if (txtRecordSearchEndContinueNumber.Text.Trim() != "")
                            lastID = int.Parse(txtRecordSearchEndContinueNumber.Text.Trim().Substring(int.Parse(lblRecordSearchClassIDLastLetterIndex.Text) + 1));

                        string idLetter = selectData.Substring(0, int.Parse(lblRecordSearchClassIDLastLetterIndex.Text) + 1);
                        int numberGap = lastID - firstID;

                        if (numberGap < 0)
                        {
                            int tempID = firstID;
                            firstID = lastID;
                            lastID = tempID;
                            numberGap = lastID - firstID;
                        }

                        if (numberGap != 0)
                        {
                            for (int i = firstID; i <= lastID; i++)
                            {
                                catchObject = SelectRecordSearch("Class", selectBy, i.ToString());

                                foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                    recordSets.Add(recordSingle);
                            }
                        }
                        else
                            catchObject = SelectRecordSearch("Class", selectBy, selectData);
                    }
                    else if (selectBy == "Name")
                    {
                        List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classall", (object)selectBy, (object)selectData);

                        foreach (var classSingle in tempClassSets)
                        {
                            catchObject = SelectRecordSearch("Class", "ID", classSingle.ID);

                            if (catchObject != null)
                            {

                                foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                    recordSets.Add(recordSingle);
                            }
                        }
                    }
                    else
                        catchObject = SelectRecordSearch("Class", selectBy, selectData);
                }

                if ((recordSets == null || recordSets.Count == 0) && catchObject != null)
                    recordSets = (List<RecordDefinition>)catchObject;
            }
            else
                recordSets = (List<RecordDefinition>)SelectRecordSearch("", "", "");

            //Check Search Result
            bool notNull = false;

            if (recordSets != null && recordSets.Count > 0)
            {
                if (cboRecordSearchBy.SelectedIndex > -1)
                {
                    if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                    {
                        //SearchStudentRecordShowRecordList();
                        SendSearchInfo(recordSets);
                        notNull = true;
                    }
                    else
                    {
                        if (lblFromPanel.Text.IndexOf("全班") == -1)
                        {
                            if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("編號") > -1)
                            {
                                ////panelStudentRecordList.Visible = true;
                                //panelStudentRecordInfo.Visible = true;

                                //lblStudentRecordShowInfoID.Text = recordSets.ElementAt(0).Data2ID;
                                //lblStudentRecordShowInfoName.Text = recordSets.ElementAt(0).Data2Name;

                                List<RecordDefinition> tempRecord = recordSets;
                                recordSets = new List<RecordDefinition>();

                                foreach (var recordSingle in tempRecord)
                                    catchObject = SelectRecordSearch("FromClass", "Class", selectData);

                                recordSets = (List<RecordDefinition>)catchObject;

                                if (recordSets != null && recordSets.Count > 0)
                                {
                                    notNull = true;

                                    if (lblFromPanel.Text.IndexOf("06") > -1)
                                    {
                                        ClassDefinition classData = (ClassDefinition)facade.FacadeFunctions("select", "class", "ID", (object)selectData);
                                        lblFromPanel.Text += "," + classData.Name + "(" + classData.ID + ")";
                                    }

                                    SendSearchInfo(recordSets);
                                    //SearchStudentRecordShowRecordList();
                                }
                            }
                            else
                            {
                                List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classall", (object)selectBy, (object)selectData);
                                classSets = new List<ClassDefinition>();

                                if (lblFromPanel.Text.IndexOf("明細") > -1)
                                {
                                    notNull = true;
                                    classSets = tempClassSets;
                                }
                                else
                                {
                                    foreach (var classSingle in tempClassSets)
                                    {
                                        foreach (var recordSingle in recordSets)
                                        {
                                            if (classSingle.ID == recordSingle.Data2ID)
                                            {
                                                classSets.Add(classSingle);
                                                notNull = true;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            SendSearchInfo(recordSets);
                            //SearchStudentRecordShowRecordList();
                            notNull = true;
                        }
                    }
                }
                else
                {
                    SendSearchInfo(recordSets);
                    //SearchStudentRecordShowRecordList();
                    notNull = true;
                }
            }
            else if (recordSets == null)
                notNull = true;

            if (!notNull)
            {
                if (classSets != null && classSets.Count > 0)
                    MessageBox.Show("此課程無相關記錄!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("查無此指定資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetSearchFunction();
            }
            else
            {
                //if (lblFromPanel.Text.IndexOf("12") == -1)
                if (lblFromPanel.Text.IndexOf("全班") == -1)
                {
                    if (classSets != null && classSets.Count > 0)
                    {
                        var classSet = classSets.Distinct();
                        classSets = classSet.ToList();
                        //panelStudentRecordList.Visible = false;
                        //btnStudentRecordReturnClassList.Visible = true;
                        //panelStudentSearchShowClassList.Visible = true;

                        SearchStudentShowClassList();
                    }
                }
                //else
                //    SendSearchInfo(recordSets);
            }
        }

        private string CheckRecordSearchItem(string item, string data, string extraNote)
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

        private bool CheckContinueNumber()
        {
            bool isTrue = false;
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (txtRecordSearchEndContinueNumber.Text.Trim() != "")
            {
                if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                {
                    //if (txtStudentSearchRecordEndContinueNumber.Text.Trim().Length == 8)
                    //{
                    if (int.Parse(txtRecordSearchEndContinueNumber.Text.Trim()) != 0)
                        isTrue = true;
                    else
                        MessageBox.Show("查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //    MessageBox.Show("學生編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
                {
                    if (txtRecordSearchEndContinueNumber.Text.Trim().Length > 0)
                    {
                        if (txtRecordSearchEndContinueNumber.Text.Trim().Length <= 7)
                        {
                            if (txtRecordSearchEndContinueNumber.Text.Trim().Length == txtRecordSearch.Text.Trim().Length)
                            {
                                bool isFound = false;
                                int lastLetterIndex = -1;
                                for (int i = txtRecordSearchEndContinueNumber.Text.Trim().Length - 1; i >= 0; i--)
                                {
                                    bool isNumber = (bool)facade.FacadeFunctions("check", "number", (object)txtRecordSearch.Text.Trim().Substring(i, 1), null);

                                    if (!isNumber && !isFound)
                                    {
                                        lastLetterIndex = i;
                                        isFound = true;
                                    }
                                }

                                string temp = txtRecordSearchEndContinueNumber.Text.Trim().Substring(0, lastLetterIndex + 1);
                                string temp1 = txtRecordSearch.Text.Trim().Substring(0, lastLetterIndex + 1);

                                if (txtRecordSearchEndContinueNumber.Text.Trim().Substring(0, lastLetterIndex + 1) == txtRecordSearch.Text.Trim().Substring(0, lastLetterIndex + 1))
                                {
                                    lblRecordSearchClassIDLastLetterIndex.Text = lastLetterIndex.ToString();
                                    isTrue = true;
                                }
                                else
                                    MessageBox.Show("此編號非連續編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                                MessageBox.Show("此編號非連續編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("連續編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                isTrue = true;

            return isTrue;
        }

        private bool CheckSearchDate()
        {
            bool isOK = false;
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            lblRecordSearchShowFromDate.Text = "";
            lblRecordSearchShowEndDate.Text = "";

            if (dtpRecordSearchFromDate.Checked || dtpRecordSearchEndDate.Checked)
            {
                if (dtpRecordSearchFromDate.Checked && dtpRecordSearchEndDate.Checked)
                {
                    if (dtpRecordSearchFromDate.Value <= dtpRecordSearchEndDate.Value)
                    {
                        isOK = true;
                        lblRecordSearchShowFromDate.Text = (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpRecordSearchFromDate.Value, null);
                        lblRecordSearchShowEndDate.Text = (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpRecordSearchEndDate.Value, null);
                    }
                    else
                        MessageBox.Show("日期條件: 起始日期不能小於結束日期都要選擇!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("日期條件: 起始與結束日期都要選擇!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                isOK = true;

            return isOK;
        }

        private object SelectRecordSearch(string select, string selectBy, string selectData)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            string fromDate = null, endDate = null;
            string[] dateSelect = new string[3];
            List<StudentPaymentDefinition> tempStudentPaymentSets = new List<StudentPaymentDefinition>();
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();
            object returnObject = null;
            panelRecordSearchByDate.Visible = false;

            if (CheckSearchDate())
            {
                if (dtpRecordSearchFromDate.Checked && dtpRecordSearchEndDate.Checked)
                {
                    fromDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpRecordSearchFromDate.Value, null).ToString();
                    endDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpRecordSearchEndDate.Value, null).ToString();

                    lblRecordSearchShowFromDate.Text = fromDate;
                    lblRecordSearchShowEndDate.Text = endDate;

                    //lblStudentRecordShowDateInfo.Text = "選擇日期從 " + fromDate + " 到 " + endDate;
                    panelRecordSearchByDate.Visible = true;
                }

                if ((selectData != "" || fromDate != null) || (lblFromPanel.Text.IndexOf("06") > -1))
                {
                    //lblRecordCheckerSelectWay.Text = lblFromPanel.Text;

                    if (lblFromPanel.Text.IndexOf("04") > -1)
                    {
                        //if (select == "FromClass")
                        //    returnObject = facade.FacadeFunctions("select", "studentpaymentbyclassid", (object)selectData, null);
                        //else
                        returnObject = facade.FacadeFunctions("select", "studentpaymenttotalforrecord", (object)(select + selectBy), selectData);
                    }
                    else if (lblFromPanel.Text.IndexOf("05") > -1)
                    {
                        panelRecordSearchByDate.Visible = false;

                        if (selectData == "")
                        {
                            if (lblFromPanel.Text.IndexOf("個別") > -1)
                                select = "WithStudentID";
                            else
                                select = "WithClassID";
                        }

                        if (select == "FromClass")
                            tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentrefundstudentlistbyclassid", (object)selectData, null);
                        else
                            tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentrefundforrecord", (object)(select + selectBy), selectData);

                        if (tempRecordSets != null)
                        {
                            if (fromDate != null)
                            {
                                dateSelect[0] = "ClassRefund";
                                dateSelect[1] = fromDate;
                                dateSelect[2] = endDate;

                                tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("reusefunction", "dateselect", (object)tempRecordSets, (object)dateSelect);
                            }
                        }

                        returnObject = (object)tempRecordSets;
                    }
                    if (lblFromPanel.Text.IndexOf("06") > -1)
                    {
                        if (select == "FromClass")
                            returnObject = facade.FacadeFunctions("select", "studentpaymentbyclassid", (object)selectData, null);
                        else
                            returnObject = facade.FacadeFunctions("select", "studentpaymenttotalforrecord", (object)(select + selectBy), selectData);
                    }
                    else if (lblFromPanel.Text.IndexOf("11") > -1)
                    {
                        string selectType = null;

                        if (selectData != "")
                        {
                            if (cboRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                                selectType = "WithStudentID";
                            else
                                selectType = "WithClassID";

                            if (select == "FromClass")
                                selectType = "FromClass";

                            dateSelect[0] = selectData;
                            dateSelect[1] = fromDate;
                            dateSelect[2] = endDate;

                            if (lblFromPanel.Text.IndexOf("明細") > -1)
                                returnObject = facade.FacadeFunctions("reusefunction", "studentprepaidhistorytotal", (object)selectType, (object)dateSelect);
                            else
                            {
                                int totalMoney = 0;
                                if (selectType == "WithStudentID")
                                {
                                    //*****************************************************
                                    //Prepaid Money Start
                                    tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("reusefunction", "studentprepaidhistorytotal", (object)selectType, (object)dateSelect);

                                    if (tempRecordSets != null && tempRecordSets.Count > 0)
                                        totalMoney = tempRecordSets.ElementAt(0).Money1;
                                    //Prepaid Money End
                                    //*****************************************************

                                    tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("reusefunction", "studentpaymentrecord", (object)selectType, (object)dateSelect);
                                    if (tempRecordSets != null && tempRecordSets.Count > 0)
                                    {
                                        totalMoney = totalMoney + int.Parse(tempRecordSets.ElementAt(0).Note2);
                                        tempRecordSets.ElementAt(0).Note2 = totalMoney.ToString();
                                        returnObject = (object)tempRecordSets;
                                    }
                                }
                                else
                                    returnObject = facade.FacadeFunctions("reusefunction", "studentpaymentrecord", (object)selectType, (object)dateSelect);
                            }
                        }
                        else
                        {

                            if (lblFromPanel.Text.IndexOf("全班") == -1)
                                selectType = "WithStudentID";
                            else
                                selectType = "WithClassID";

                            dateSelect[0] = selectData;
                            dateSelect[1] = fromDate;
                            dateSelect[2] = endDate;

                            string dates = fromDate + "," + endDate;

                            if (lblFromPanel.Text.IndexOf("明細") > -1)
                                returnObject = facade.FacadeFunctions("reusefunction", "studentprepaidhistorytotal", (object)selectType, (object)dateSelect);
                            else
                            {
                                studentPaymentManager = new StudentPaymentManager();
                                returnObject = studentPaymentManager.GetStudentPaymentWithPrepaid(facade, selectType, dateSelect, dates);
                            }
                        }
                    }
                    else if (lblFromPanel.Text.IndexOf("12") > -1)
                    {
                        if (selectData != "")
                        {
                            dateSelect[0] = selectData;
                            dateSelect[1] = fromDate;
                            dateSelect[2] = endDate;

                            returnObject = facade.FacadeFunctions("reusefunction", "studentinclass", (object)dateSelect, null);
                        }
                        else
                            returnObject = facade.FacadeFunctions("count", "studentinclass", (object)fromDate, (object)endDate);
                    }
                }
                else
                {
                    MessageBox.Show("請輸入查詢條件!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    SetSearchFunction();
                }
            }

            return returnObject;
        }

        private void SearchStudentShowStudentList()
        {
            if (dgvRecordSearchShowStudentInClassList.Columns.Count > 0)
                dgvRecordSearchShowStudentInClassList.Columns.Clear();

            //Add Student ID
            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生編號";
            dgvRecordSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生姓名";
            dgvRecordSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生性別";
            dgvRecordSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "學生生日";
            dgvRecordSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "就讀學校";
            dgvRecordSearchShowStudentInClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "就讀年級";
            dgvRecordSearchShowStudentInClassList.Columns.Add(newColumn);

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

                dgvRecordSearchShowStudentInClassList.Rows.Add(newRow);
            }

            dgvRecordSearchShowStudentInClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecordSearchShowStudentInClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvRecordSearchShowStudentInClassList.AllowUserToAddRows = false;

            if (dgvRecordSearchShowStudentInClassList.Rows.Count > 0)
                dgvRecordSearchShowStudentInClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvRecordSearchShowStudentInClassList.Rows.Count; i++)
                dgvRecordSearchShowStudentInClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvRecordSearchShowStudentInClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvRecordSearchShowStudentInClassList.Columns.Count; i++)
            {
                dgvRecordSearchShowStudentInClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRecordSearchShowStudentInClassList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvRecordSearchShowStudentInClassList.ReadOnly = true;
            }

            btnRecordByStudentInClass.Enabled = false;
        }

        private void SearchStudentShowClassList()
        {
            if (dgvRecordSearchClassList.Columns.Count > 0)
                dgvRecordSearchClassList.Columns.Clear();

            //Add Student ID
            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程編號";
            dgvRecordSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程名稱";
            dgvRecordSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "起始日期";
            //dgvRecordSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "結束日期";
            //dgvRecordSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "目前人數";
            //dgvRecordSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "課程價格";
            //dgvRecordSearchClassList.Columns.Add(newColumn);

            //newColumn = new DataGridViewTextBoxColumn();
            //newColumn.HeaderText = "教材費用";
            //dgvRecordSearchClassList.Columns.Add(newColumn);

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

                dgvRecordSearchClassList.Rows.Add(newRow);
            }

            dgvRecordSearchClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecordSearchClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvRecordSearchClassList.AllowUserToAddRows = false;

            if (dgvRecordSearchClassList.Rows.Count > 0)
                dgvRecordSearchClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvRecordSearchClassList.Rows.Count; i++)
                dgvRecordSearchClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvRecordSearchClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvRecordSearchClassList.Columns.Count; i++)
            {
                dgvRecordSearchClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRecordSearchClassList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvRecordSearchClassList.ReadOnly = true;
            }

            btnRecordSearchShowStudent.Enabled = false;
            panelRecordSearchShowClassList.Visible = true;
        }

        private void dgvRecordSearchShowStudentInClassList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRecordSearchShowStudentInClassList_CellDoubleClick(sender, e);
        }

        private void dgvRecordSearchShowStudentInClassList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvRecordSearchShowStudentInClassList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvRecordSearchShowStudentInClassList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvRecordSearchShowStudentInClassList.ReadOnly = false;
                dgvRecordSearchShowStudentInClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            if (selectItem == 0)
                btnRecordByStudentInClass.Enabled = false;
            else if (selectItem == 1)
                btnRecordByStudentInClass.Enabled = true;
            else if (selectItem > 1)
                btnRecordByStudentInClass.Enabled = false;
        }

        private void dgvRecordSearchClassList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRecordSearchClassList_CellDoubleClick(sender, e);
        }

        private void dgvRecordSearchClassList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvRecordSearchClassList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvRecordSearchClassList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvRecordSearchClassList.ReadOnly = false;
                selectItem++;
            }

            if (selectItem == 0)
                btnRecordSearchShowStudent.Enabled = false;
            else if (selectItem == 1)
                btnRecordSearchShowStudent.Enabled = true;
            else if (selectItem > 1)
                btnRecordSearchShowStudent.Enabled = false;
        }

        private void btnRecordSearchShowStudent_Click(object sender, EventArgs e)
        {
            try
            {
                int selectIndex = -1;
                int countIndex = 0;

                if (dgvRecordSearchShowStudentInClassList.Columns.Count > 0)
                    dgvRecordSearchShowStudentInClassList.Columns.Clear();

                foreach (DataGridViewRow dgvRow in this.dgvRecordSearchClassList.Rows)
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

                    btnRecordSearchReturnClassList.Visible = true;
                    panelRecordSearchStudentInClass.Visible = true;
                    panelRecordSearchShowClassList.Visible = false;

                    lblRecordSearchClassID.Visible = true;
                    lblRecordSearchShowClassID.Visible = true;
                    lblRecordSearchClassName.Visible = true;
                    lblRecordSearchShowClassName.Visible = true;

                    lblRecordSearchShowClassID.Text = classData.ID;
                    lblRecordSearchShowClassName.Text = classData.Name;

                    studentSets = null;

                    studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

                    if (studentSets != null)
                    {
                        if (studentSets.Count > 0)
                            SearchStudentShowStudentList();
                        else
                        {
                            MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RecordSearchReturnClassList();
                        }
                    }
                    else
                    {
                        MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        RecordSearchReturnClassList();
                    }
                }
            }
            catch
            {
            }
        }

        private void btnRecordSearchReturnClassList_Click(object sender, EventArgs e)
        {
            RecordSearchReturnClassList();
        }

        private void RecordSearchReturnClassList()
        {
            classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "class", (object)"Name", (object)lblRecordSearchInfo.Text);
            SearchStudentShowClassList();

            btnRecordSearchReturnClassList.Visible = false;
            panelRecordSearchStudentInClass.Visible = false;
            panelRecordSearchShowClassList.Visible = true;
        }

        private void btnRecordByStudentInClass_Click(object sender, EventArgs e)
        {
            try
            {
                int selectIndex = -1;
                int countIndex = 0;

                foreach (DataGridViewRow dgvRow in this.dgvRecordSearchShowStudentInClassList.Rows)
                {
                    if (dgvRow.Selected)
                        selectIndex = countIndex;

                    countIndex++;
                }

                studentData = null;
                studentData = studentSets.ElementAt(selectIndex);

                //SendSearchInfo(studentData.ID, studentData.Name);
            }
            catch
            {
            }
        }

        private void SendSearchInfo(List<RecordDefinition> recordList)
        {
            string searchDate = "";

            if (lblRecordSearchShowFromDate.Text.Trim() != "")
                searchDate = lblRecordSearchShowFromDate.Text + lblRecordSearchShowEndDate.Text;

            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.GetRecordListFromRecordSearch(recordList, searchDate, lblFromPanel.Text);
            //emsSystem.EnablefrmEMS();
            //this.Close();
            CloseSearchRecordData();
        }

        private void CloseSearchRecordData()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMS();
            //emsSystem.EnablefrmEMSAndRestart();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseSearchRecordData();
        }
    }
}
