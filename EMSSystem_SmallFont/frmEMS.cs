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
using System.Data.SqlClient;
using System.IO;
using System.Diagnostics;
using EMSSystem.StaticFunctions;


namespace EMSSystem
{
    public partial class frmEMS : Form
    {
        #region Varieties

        frmAddNewStudentInfo addNewStudentInfo;
        frmChangeUserPassword changeUserPassword;
        frmErrorMessage errorMsg;
        frmLogin login;
        frmNewClassTime newClassTime;
        frmPaymentDiscount paymentDiscount;
        frmPrintNeedToPayNotice printNeedToPayNotice;
        frmSaftyChecker saftyChecker;
        frmSearchClassData searchClassData;
        frmSearchRecordData searchRecordData;
        frmSearchStudentData searchStudentData;
        frmShowAllClasses showAllClasses;
        frmShowAllStudents showAllStudents;
        frmShowStudentRefundClass showStudentRefundClass;
        frmStudentAddNewClass studentAddNewClass;
        frmStudentPayment studentPayment;
        frmStudentPaymentHistory studentPaymentHistory;
        frmStudentPrepaid studentPrepaid;
        FacadeLayer facade;
        ClassDefinition classData;
        ClassTimeDefinition classTimeData;
        ClassPaymentDefinition classPayment;
        ClassRefundDefinition classRefund;
        ClassRefundDetailDefinition classRefundDetail;
        ClassRefundDetailDefinition classRefundListDetail;
        CompanyInfoDefinition companyInfo;
        ExpanseDefinition expanseData;
        StaffDefinition staffData;
        StaffAccountDefinition staffAccountData;
        StudentDefinition studentData;
        StudentInClassDefinition studentInClassData;
        StudentPaymentDefinition studentPaymentData;
        StudentPrepaidDefinition studentPrepaidData;
        SystemLogsDefinition systemLogData;
        List<ClassDefinition> classSets;
        List<ClassPaymentDefinition> classPaymentSets;
        List<ClassRefundDefinition> classRefundSets;
        List<ClassRefundDefinition> classRefundRecordSets;
        List<ClassRefundDetailDefinition> classRefundDetailSets;
        List<ClassCategoryDefinition> classCateSets;
        List<ClassTimeDefinition> classTimeSets;
        List<ExpanseDefinition> expanseSets;
        List<RecordDefinition> recordSets;
        List<RecordDefinition> recordShowListSets;
        List<RecordDefinition> recordDetails;
        List<SideFunctionsDefinition> sideFunctionsSets;
        List<StaffDefinition> staffSets;
        List<StaffAccountDefinition> staffAccountSets;
        List<StudentDefinition> studentSets;
        List<StudentInClassDefinition> studentInClassSets;
        List<StudentInClassDefinition> tempStudentInClassSets = null;
        List<StudentPaymentDefinition> studentPaymentSets;
        List<StudentPaymentDefinition> tempStudentPaymentSets = null;
        List<SystemLogsDefinition> systemLogsSets;
        string[] studentDataGridViewHeaderText = { "學生編號", "學生姓名", "學生性別", "學生生日", "就讀學校", "就讀年級", "父親姓名", "父親工作", "母親姓名", "母親工作", "負責家長", "負責家長電話", "緊急連絡人", "緊急連絡電話", "學生地址" };
        string[] classDataGridViewHeaderText = { "課程編號", "課程名稱", "上課日期", "結束日期", "目前人數", "課程價格", "教材費用", "課程老師", "課程備註" };
        string[] studentPaymentDataGridViewHeaderText = { "學生編號", "學生姓名", "課程編號", "課程名稱", "上課日期", "結束日期", "課程價格", "教材費用", "報名費用", "折扣金額", "已繳金額", "需繳金額" };
        //string[] studentPaymentDataGridViewHeaderText = { "學生編號", "學生姓名", "上課日期", "結束日期", "課程價格", "教材費用", "報名費用", "折扣金額", "已繳金額", "需繳金額" };
        string[] studentSelectClassDataGridViewHeaderText = { "學生編號", "學生姓名", "課程編號", "課程名稱", "上課日期", "結束日期", "課程價格", "教材費用", "報名費用", "折扣金額", "已繳金額", "需繳金額" };
        string[] studentPaymentCountDataGridViewHeaderText = { "學生編號", "學生姓名", "未繳課程", "已繳金額", "需繳金額" };
        string[] classPaymentCountDataGridViewHeaderText = { "班級編號", "班級名稱", "未繳學生數", "已繳部份金額", "需繳金額" };
        string[] classRefundDataGridViewHeaderText = { "資料編號", "資料姓名", "退費日期", "退費金額", "退費折扣", "經手員工" };
        string[] classRefundDataListGridViewHeaderText = { "學生編號", "學生姓名", "退費金額", "退費日期", "收費人員", "退費方式", "退費課程" };
        string[] classRefundDataDetailGridViewHeaderText = { "學生編號", "學生姓名", "班級編號", "班級姓名", "繳費金額" };
        string[] studentInClassCountDataGridViewHeaderText = { "班級編號", "班級名稱", "加選人數", "退選人數" };
        string[] studentPaymentRecordByStudentDataGridViewHeaderText = { "學生編號", "學生姓名", "報名班級", "已繳費用" };
        string[] studentPaymentRecordByClassDataGridViewHeaderText = { "班級編號", "班級名稱", "班級人數", "已繳費用" };
        string[] moneyInfo = new string[8];

        #endregion

        public frmEMS()
        {
            InitializeComponent();

            Font newFont = new System.Drawing.Font("MingLiU", 8.5F, System.Drawing.FontStyle.Bold);

            foreach (Control control in this.Controls)
                control.Font = newFont;
        }

        private void frmEMS_Load(object sender, EventArgs e)
        {
            LoadSystemSetting();
            SetTime();
            errorMsg = new frmErrorMessage();
            lblInsertErrorMsgIsShow.Text = "false";
            //lblInvisibleDisplayEndClass.Text = "false";

            facade = new FacadeLayer(SystemTypeForDB);
            staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "getcurrentuser", null, null);
            companyInfo = (CompanyInfoDefinition)facade.FacadeFunctions("select", "whole", "CompanyInfo", null);

            if (companyInfo.CompanyName != "" && companyInfo.CompanyName != null)
                lblSystemTitle.Text = companyInfo.CompanyName;
            else
                lblSystemTitle.Text = "EMS System";

            CallfrmEMSFromLogin(staffAccountData.StaffName, staffAccountData.Password, staffAccountData.MasterKey, staffAccountData.StaffRoleID.ToString(), staffAccountData.StaffRole);

            lblSystemDate.Text = facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString();
            lblShowToday.Text = facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString();

            ReSize();
            DefaultSetting();
            LoadStaffRole();

            //lblInvisibleStaffEnglishName.Text = "Owen";
            //lblGotGranted.Text = "true";

            if (!bool.Parse(lblGotGranted.Text))
                panelMainSecretScreen.Visible = false;

            //LoadSideFunctions();
            LoadStudentDOBYear();
        }

        #region Set Time

        private void SetTime()
        {
            timerSystemTime.Enabled = true;
            timerSystemTime.Interval = 1000;
        }

        private void timerSystemTime_Tick(object sender, EventArgs e)
        {
            lblSystemTime.Text = System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
            lblShowNowTime.Text = System.DateTime.Now.ToString("HH:mm:ss");
        }

        #endregion

        #region ReSize Pages

        private void frmEMS_SizeChanged(object sender, EventArgs e)
        {
            ReSize();
        }

        private void ReSize()
        {
            int mainWidth = panelMainMenuScreen.Width;
            int mainHeight = panelMainMenuScreen.Height;
            int mainGeneralScreenX, mainGeneralScreenY;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;

            //Set MainScreen's Location
            if (this.Width < screenWidth)
                this.Location = new Point((screenWidth - this.Width) / 2, (screenHeight - this.Height) / 2);

            //Set MainScreen Buttons' Locations
            mainGeneralScreenX = (mainWidth - panelMainGeneralScreen.Width) / 2;
            mainGeneralScreenY = (mainHeight - panelMainGeneralScreen.Height) / 4 * 3;
            panelMainGeneralScreen.Location = new Point(mainGeneralScreenX, mainGeneralScreenY);

            int systemTimeWidth = 0;
            if (lblSystemTime.Text.Length == 4)
                systemTimeWidth = lblSystemTime.Width + 390;
            else
                systemTimeWidth = lblSystemTime.Width;

            //Set System Title Locations
            lblSystemTitle.Location = new Point((mainWidth - lblSystemTitle.Width) / 2,
                                                (mainHeight - panelMainGeneralScreen.Height) / 3);
            lblSystemDate.Location = new Point((mainWidth - (lblSystemDate.Width + lblSystemTime.Width)) / 2,
                                                lblSystemTitle.Location.Y + 70);
            lblSystemTime.Location = new Point((mainWidth - systemTimeWidth) / 2,
                                               lblSystemTitle.Location.Y + 100);

            //Set TopScreen Current Location
            //lblCurrentPage.Location = new Point((//panelTopScreen.Width / 2) - (lblCurrentPage.Width / 2) + 50, lblCurrentPage.Location.Y);

            //Set Sub Buttons
            //panelShowSubButtons.Location = new Point((panelSubButtons.Width / 2) - (panelShowSubButtons.Width / 2), (panelSubButtons.Height / 2) - (panelShowSubButtons.Height / 2));

            //Set Safty Checker
            panelContainSaftyChecker.Location = new Point((panelSaftyChecker.Width / 2) - (panelContainSaftyChecker.Width / 2), (panelSaftyChecker.Height / 2) - (panelContainSaftyChecker.Height / 2));

            //Set InsertScreen Panels
            panelInsertStudentQuick.Location = new Point((panelInsertScreen.Width - panelInsertStudentQuick.Width) / 2, panelInsertStudentQuick.Location.Y);
            panelInsertStudent.Location = new Point((panelInsertScreen.Width - panelInsertStudent.Width) / 2, panelInsertStudent.Location.Y);
            panelInsertClass.Location = new Point((panelInsertScreen.Width - panelInsertClass.Width) / 2, panelInsertClass.Location.Y);
            panelInsertStaff.Location = new Point((panelInsertScreen.Width - panelInsertStaff.Width) / 2, panelInsertStaff.Location.Y);

            //Set Search Student Screen
            lblSearchStudentScreen.Location = new Point((panelSearchStudentScreen.Width - lblSearchStudentScreen.Width) / 2, lblSearchStudentScreen.Location.Y);
            panelStudentSearchStudentInfo.Location = new Point((panelSearchStudentScreen.Width - panelStudentSearchStudentInfo.Width) / 2, panelStudentSearchStudentInfo.Location.Y);
            panelStudentSearchStudentInClass.Location = new Point((panelSearchStudentScreen.Width - panelStudentSearchStudentInClass.Width) / 2, panelStudentSearchStudentInClass.Location.Y);
            panelStudentRecordList.Location = new Point((panelSearchStudentScreen.Width - panelStudentRecordList.Width) / 2, panelStudentRecordList.Location.Y);
            panelStudentRecordInfo.Location = new Point((panelStudentRecordList.Width - panelStudentRecordInfo.Width) / 2, panelStudentRecordInfo.Location.Y);
            panelStudentRecordDateInfo.Location = new Point((panelStudentRecordList.Width - panelStudentRecordDateInfo.Width) / 2, panelStudentRecordDateInfo.Location.Y);

            //Set Record List Screen
            lblRecordChecker.Location = new Point((panelRecordChecker.Width - lblRecordChecker.Width) / 2, lblRecordChecker.Location.Y);
            //panelRecordCheckerCheckInfo.Location = new Point((panelRecordChecker.Width - //panelRecordCheckerCheckInfo.Width) / 2, //panelRecordCheckerCheckInfo.Location.Y);
            panelStudentSearchStudentInClass.Location = new Point((panelRecordChecker.Width - panelStudentSearchStudentInClass.Width) / 2, panelStudentSearchStudentInClass.Location.Y);
            panelStudentRecordList.Location = new Point((panelSearchStudentScreen.Width - panelStudentRecordList.Width) / 2, panelStudentRecordList.Location.Y);

            //Set Student Manage Class Panel
            lblStudentManageClassTitle.Location = new Point((panelStudentManageClass.Width - lblStudentManageClassTitle.Width) / 2, lblStudentManageClassTitle.Location.Y);
            panelStudentManageClassStudentInfo.Location = new Point((panelStudentManageClass.Width - panelStudentManageClassStudentInfo.Width) / 2, panelStudentManageClassStudentInfo.Location.Y);

            //Set Student Payment Panel
            lblStudentPaymentTitle.Location = new Point((panelStudentPayment.Width - lblStudentPaymentTitle.Width) / 2, lblStudentPaymentTitle.Location.Y);
            panelStudentPaymentDetailInfo.Location = new Point((panelStudentPayment.Width - panelStudentPaymentDetailInfo.Width) / 2, panelStudentPaymentDetailInfo.Location.Y);
            panelStudentPaymentButtonsPanel.Location = new Point((panelStudentPaymentManagementPage.Width - panelStudentPaymentButtonsPanel.Width) / 2, panelStudentPaymentButtonsPanel.Location.Y);
            //panelStudentPaymentPayMoneyInfo.Location = new Point((panelStudentPaymentPayMoneyPage.Width - panelStudentPaymentPayMoneyInfo.Width) / 2, panelStudentPaymentPayMoneyInfo.Location.Y);
            //panelStudentPaymentPaymentInfo.Location = new Point((panelStudentPaymentPayMoneyPage.Width - panelStudentPaymentPaymentInfo.Width) / 2, panelStudentPaymentPaymentInfo.Location.Y);
            //panelStudentPaymentClassDiscountInfo.Location = new Point((panelStudentPaymentDiscountPage.Width - panelStudentPaymentClassDiscountInfo.Width) / 2, panelStudentPaymentClassDiscountInfo.Location.Y);
            //panelStudentPaymentPrepaidInfo.Location = new Point((panelStudentPaymentPrepaidPage.Width - panelStudentPaymentPrepaidInfo.Width) / 2, panelStudentPaymentPrepaidInfo.Location.Y);
            panelStudentPaymentByClassPaymentInfo.Location = new Point((panelStudentPaymentByClassPaymentPage.Width - panelStudentPaymentByClassPaymentInfo.Width) / 2, panelStudentPaymentByClassPaymentInfo.Location.Y);

            //Set Student Refund Panel
            lblStudentRefund.Location = new Point((panelStudentRefund.Width - lblStudentRefund.Width) / 2, lblStudentRefund.Location.Y);
            panelStudentRefundStudentInfo.Location = new Point((panelStudentRefund.Width - panelStudentRefundStudentInfo.Width) / 2, panelStudentRefundStudentInfo.Location.Y);
            panelStudentRefundByPerson.Location = new Point((panelStudentRefundPayMoney.Width - panelStudentRefundByPerson.Width) / 2, panelStudentRefundByPerson.Location.Y);
            panelStudentRefundByClass.Location = new Point((panelStudentRefundPayMoney.Width - panelStudentRefundByClass.Width) / 2, panelStudentRefundByClass.Location.Y);

            //Set Daily Expanse Panel
            lblDailyExpanseTitle.Location = new Point((panelDailyExpanse.Width - lblDailyExpanseTitle.Width) / 2, lblDailyExpanseTitle.Location.Y);
            panelSearchExpanse.Location = new Point((panelShowDailyExpanse.Width - panelSearchExpanse.Width) / 2, panelSearchExpanse.Location.Y);
            panelManageDailyExpanse.Location = new Point((panelShowDailyExpanse.Width - panelSearchExpanse.Width) / 2, panelManageDailyExpanse.Location.Y);

            //Set Company Info Panel
            lblCompanyDetail.Location = new Point((panelCompanyDetail.Width - lblCompanyDetail.Width) / 2, lblCompanyDetail.Location.Y);
            panelSystemCompanyInfo.Location = new Point((panelCompanyDetail.Width - panelSystemCompanyInfo.Width) / 2, panelSystemCompanyInfo.Location.Y);

            //Set System Log Panel
            lblSystemLog.Location = new Point((panelSystemLog.Width - lblSystemLog.Width) / 2, lblSystemLog.Location.Y);
            panelSystemLogSearchBy.Location = new Point((panelSystemLog.Width - panelSystemLogSearchBy.Width) / 2, panelSystemLogSearchBy.Location.Y);
        }

        #endregion

        public void DefaultSetting()
        {
            DefaultItems();
            SetStudentSearchDefault();
            SetRecordListDefault();
            SetInsertScreenDefault();
            SetStudentManageClassDefault();
            SetStudentPaymentDefault();
            SetStudentRefundDefault();
            SetManageSystemInfoDefault();
            SetDailyExpanseDefault();

            panelMainMenuScreen.Visible = true;
            ////panelTopScreen.Visible = false;
            ////panelSideFunctions.Visible = false;
            panelSubButtons.Visible = false;
            panelSaftyChecker.Visible = false;
            panelSearchStudentScreen.Visible = false;
            panelRecordChecker.Visible = false;
            panelStudentManageClass.Visible = false;
            panelStudentPayment.Visible = false;
            panelStudentRefund.Visible = false;
            panelInsertScreen.Visible = false;
            panelInsertStudent.Visible = false;
            panelCompanyDetail.Visible = false;
            panelSystemLog.Visible = false;
            panelDailyExpanse.Visible = false;

            //panelStudent.Visible = false;
            //panelClass.Visible = false;

            //SetStudentPanelDefault();
            //SetClassPanelDefault();
        }

        private void DefaultItems()
        {
            classData = null;
            classTimeData = null;
            classPayment = null;
            classRefund = null;
            classRefundDetail = null;
            classRefundListDetail = null;
            companyInfo = null;
            staffData = null;
            staffAccountData = null;
            studentData = null;
            studentInClassData = null;
            studentPaymentData = null;
            studentPrepaidData = null;
            systemLogData = null;
            classSets = null;
            classPaymentSets = null;
            classRefundSets = null;
            classRefundRecordSets = null;
            classRefundDetailSets = null;
            classCateSets = null;
            classTimeSets = null;
            recordSets = null;
            recordShowListSets = null;
            //sideFunctionsSets = null;
            staffSets = null;
            staffAccountSets = null;
            studentSets = null;
            studentInClassSets = null;
            tempStudentInClassSets = null;
            studentPaymentSets = null;
            tempStudentPaymentSets = null;
            systemLogsSets = null;
        }

        DataGridViewComboBoxColumn CreateComboBoxoWithEnums()
        {
            DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
            //combo.DataSource = Enum.GetValues("");
            combo.DataPropertyName = "Title";
            combo.Name = "Title";
            return combo;
        }

        #region System Setting

        private void cbSearchClassByEndDate_CheckedChanged(object sender, EventArgs e)
        {
            if (cbDisplayEndClass.Checked != bool.Parse(lblInvisibleDisplayEndClass.Text))
            {
                if (cbDisplayEndClass.Checked)
                    lblInvisibleDisplayEndClass.Text = "true";
                else
                    lblInvisibleDisplayEndClass.Text = "false";

                facade = new FacadeLayer(SystemTypeForDB);
                facade.FacadeFunctions("systemsetting", "getsetting", "SearchClassByEndDate", lblInvisibleDisplayEndClass.Text);
            }
        }

        #endregion

        #region Connect to Other Forms

        private static void ThreadProc()
        {
            Application.Run(new frmLogin());
        }

        private void CallfrmChangeUserPassword()
        {
            this.Enabled = false;
            changeUserPassword = new frmChangeUserPassword();
            changeUserPassword.GetChangePasswordInfo(lblInvisibleStaffEnglishName.Text, lblInvisibleStaffPassword.Text, lblInvisibleStaffMasterKey.Text);
            changeUserPassword.Owner = this;
            changeUserPassword.Show();
        }

        private void CallfrmSaftyChecker(bool needMasterKey)
        {
            saftyChecker = new frmSaftyChecker();
            saftyChecker.GetSaftyCheckerInfo(lblCurrentPage.Text, lblInvisibleStaffPassword.Text, lblInvisibleStaffMasterKey.Text, needMasterKey);
            saftyChecker.Owner = this;
            saftyChecker.Show();
        }

        public void CallfrmEMSFromLogin(string username, string password, string masterKey, string staffRoleID, string staffRole)
        {
            lblInvisibleStaffEnglishName.Text = username;
            lblInvisibleStaffPassword.Text = password;
            lblInvisibleStaffMasterKey.Text = masterKey;
            lblInvisibleStaffRoleID.Text = staffRoleID;
            lblInvisibleStaffRole.Text = staffRole;

            if (masterKey == "")
                lblGotGranted.Text = "false";
            else
                lblGotGranted.Text = "true";
        }

        private void CallfrmErrorMessage()
        {
            if (!bool.Parse(lblInsertErrorMsgIsShow.Text))
            {
                errorMsg = new frmErrorMessage();
                errorMsg.Owner = this;
                errorMsg.Show();
            }
        }

        public void CloseErrorMessage()
        {
            lblInsertErrorMsgIsShow.Text = "false";
        }

        private void CallfrmAddNewStudentInfo()
        {
            this.Enabled = false;
            addNewStudentInfo = new frmAddNewStudentInfo();
            addNewStudentInfo.Owner = this;
            addNewStudentInfo.Show();
        }

        private void CallfrmSearchClassData()
        {
            DefaultItems();
            this.Enabled = false;
            searchClassData = new frmSearchClassData();
            searchClassData.Owner = this;
            searchClassData.Show();
        }

        private void CallfrmSearchClassData(string fromPanel)
        {
            DefaultItems();
            this.Enabled = false;
            searchClassData = new frmSearchClassData(fromPanel);
            searchClassData.Owner = this;
            searchClassData.Show();
        }

        private void CallfrmSearchRecordData(string fromPanel)
        {
            DefaultItems();
            this.Enabled = false;
            searchRecordData = new frmSearchRecordData(fromPanel);
            searchRecordData.Owner = this;
            searchRecordData.Show();
        }

        private void CallfrmSearchStudentData()
        {
            this.Enabled = false;
            searchStudentData = new frmSearchStudentData();
            searchStudentData.Owner = this;
            searchStudentData.Show();
        }

        private void CallfrmSearchStudentData(string fromPanel)
        {
            this.Enabled = false;
            searchStudentData = new frmSearchStudentData(fromPanel);
            searchStudentData.Owner = this;
            searchStudentData.Show();
        }

        private void CallfrmNewClassTime()
        {
            newClassTime = new frmNewClassTime();
            newClassTime.Owner = this;
            newClassTime.Show();
        }

        private void CallfrmStudentAddNewClass(string classID)
        {
            studentAddNewClass = new frmStudentAddNewClass();
            studentAddNewClass.Owner = this;
            studentAddNewClass.GetClassInfo(lblStudentManageClassShowStudentID.Text, lblStudentManageClassShowStudentName.Text, classID);
            studentAddNewClass.Show();
        }

        private void CallfrmStudentPaymentHistory(List<ClassPaymentDefinition> classPaymentSets)
        {
            studentPaymentHistory = new frmStudentPaymentHistory();
            studentPaymentHistory.Owner = this;
            studentPaymentHistory.GetStudentInClassData(classPaymentSets);
            studentPaymentHistory.Show();
        }

        private void CallfrmPaymentDiscount(StudentPaymentDefinition studentPaymentDatas, string fromStudentOrClass)
        {
            this.Enabled = false;
            paymentDiscount = new frmPaymentDiscount();
            paymentDiscount.Owner = this;
            paymentDiscount.GetStudentDiscountInfo(studentPaymentDatas, lblInvisibleStaffEnglishName.Text, fromStudentOrClass);
            paymentDiscount.Show();
        }

        private void CallfrmPrintNeedToPayNotice()
        {
            this.Enabled = false;
            printNeedToPayNotice = new frmPrintNeedToPayNotice();
            printNeedToPayNotice.Owner = this;
            printNeedToPayNotice.Show();
        }

        private void CallfrmStudentPrepaid(string studentID, string studentName)
        {
            this.Enabled = false;
            studentPrepaid = new frmStudentPrepaid();
            studentPrepaid.Owner = this;
            studentPrepaid.GetStudentPrepaidInfo(studentID, studentName, lblInvisibleStaffEnglishName.Text);
            studentPrepaid.Show();
        }

        private void CallfrmStudentPayment(List<StudentPaymentDefinition> studentPaymentDatas, string fromPage, string refundID, string haveRefundMoney)
        {
            studentPayment = new frmStudentPayment();
            studentPayment.Owner = this;
            studentPayment.GetStudentPaymentInfo(studentPaymentDatas, fromPage, refundID, haveRefundMoney, lblInvisibleStaffEnglishName.Text);
            studentPayment.Show();
        }

        private void CallfrmShowAllClasses()
        {
            btnNewClassShowClasses.Enabled = false;
            showAllClasses = new frmShowAllClasses();
            showAllClasses.Owner = this;
            showAllClasses.Show();
        }

        private void CallfrmShowAllStudents(string studentName)
        {
            btnNewStudentCheckID.Enabled = false;
            showAllStudents = new frmShowAllStudents();
            showAllStudents.Owner = this;
            if (showAllStudents.GetStudentName(studentName))
                showAllStudents.Show();
        }

        private void CallfrmShowStudentRefundClass()
        {
            btnRecordCheckerWithDetailDelete.Enabled = false;
            showStudentRefundClass = new frmShowStudentRefundClass();
            showStudentRefundClass.Owner = this;
            showStudentRefundClass.GetRefundDetail(classRefundDetailSets);
            showStudentRefundClass.Show();
        }

        public void EnablefrmEMS()
        {
            this.Enabled = true;
            EnableAllMainButtons();
        }

        public void EnablefrmEMSAndRestart()
        {
            this.Enabled = true;
            LoadSubButtons(lblInvisibleMainFunction.Text);
        }

        public void EnableButton()
        {
            btnAddNewClassTime.Enabled = true;
            btnStudentPaymentHistory.Enabled = true;
            btnStudentPaymentSinglePay.Enabled = true;
            btnStudentPaymentAllPay.Enabled = true;
            dgvStudentPaymentShowStudentUnpaidClass.Enabled = true;
            lblStudentPaymentIsDoubleWorking.Text = "false";
            btnNewClassShowClasses.Enabled = true;
            btnNewStudentCheckID.Enabled = true;
            btnRecordCheckerWithDetailDelete.Enabled = true;
        }

        public void EnablePrepaidButtonOnly()
        {
            btnStudentPaymentPrepaid.Enabled = true;
        }

        #endregion

        #region Load Data

        public string SystemTypeForDB
        {
            get
            {
                return lblSystemTypeForDB.Text;
            }
        }

        private void LoadSystemSetting()
        {
            try
            {
                facade = new FacadeLayer("");
                string settings = facade.FacadeFunctions("systemsetting", "getsetting", "", "").ToString();
                string[] settingValue = settings.Trim().Substring(0, settings.Length - 1).Split(';');

                lblInvisibleDisplayEndClass.Text = "false";
                lblSystemTypeForDB.Text = "x86";

                if (settingValue.Length > 0)
                {
                    lblSystemTypeForDB.Text = settingValue[0];

                    if (settingValue.Length > 1)
                    {
                        lblInvisibleDisplayEndClass.Text = settingValue[1];
                        cbDisplayEndClass.Checked = bool.Parse(lblInvisibleDisplayEndClass.Text);
                    }
                }
            }
            catch
            {
            }
        }

        private void LoadStudentDOBYear()
        {
            int currentYear = DateTime.Now.Year;

            cboNewStudentDOBYear.Items.Clear();
            for (int i = currentYear - 15; i <= currentYear - 5; i++)
                cboNewStudentDOBYear.Items.Add(i);
        }

        public void LoadSubButtons(string mainFunctionText)
        {
            facade = new FacadeLayer(SystemTypeForDB);

            //lblInvisibleCheckNodeOneIsSelected.Text = "false";
            //tvSideFunctions.SelectedNode = null;
            lblInvisibleMainFunction.Text = "";
            string mainID = mainFunctionText.Trim().Substring(0, 2);

            if (panelShowSubButtons.Controls.Count > 0)
                panelShowSubButtons.Controls.Clear();

            if ((bool)facade.FacadeFunctions("check", "number", mainID, null))
            {
                SettingCurrentPage(mainFunctionText);
                sideFunctionsSets = (List<SideFunctionsDefinition>)facade.FacadeFunctions("select", "sidesubfunctions", mainID, null);

                DefaultSetting();

                cboStudentSearchRecord.SelectedIndex = -1;
                cboStudentSearchBy.SelectedIndex = -1;
                panelSubButtons.Visible = true;
                panelMainMenuScreen.Visible = false;

                lblInvisibleMainFunction.Text = mainFunctionText.Trim();

                int lblWidth = 123, lblHeight = 70;
                Button newButton = new Button();
                newButton.Location = new Point(8, 4);
                newButton.Size = new Size(lblWidth, lblHeight);
                newButton.FlatStyle = FlatStyle.Flat;
                newButton.TextAlign = ContentAlignment.MiddleCenter;
                newButton.BackColor = Color.FromArgb(0, 0, 64);
                newButton.ForeColor = Color.FromArgb(255, 255, 128);
                newButton.FlatAppearance.MouseOverBackColor = Color.Maroon;
                newButton.FlatAppearance.MouseDownBackColor = Color.Brown;
                newButton.FlatAppearance.BorderSize = 5;
                newButton.Font = new Font(new FontFamily("MingLiU"), 11F, FontStyle.Bold);

                if (sideFunctionsSets != null && sideFunctionsSets.Count > 0)
                {
                    //DefaultSetting();

                    //cboStudentSearchRecord.SelectedIndex = -1;
                    //cboStudentSearchBy.SelectedIndex = -1;

                    //////panelTopScreen.Visible = true;
                    //////panelSideFunctions.Visible = true;
                    //panelSubButtons.Visible = true;
                    //panelMainMenuScreen.Visible = false;
                    newButton.Text = sideFunctionsSets.ElementAt(0).Function;
                    newButton.Click += new System.EventHandler(this.subFunctionClick);
                    panelShowSubButtons.Controls.Add(newButton);

                    newButton = new Button();
                    newButton.Location = new Point(8, 88);
                    newButton.Size = new Size(lblWidth, lblHeight);
                    newButton.Text = sideFunctionsSets.ElementAt(1).Function;
                    newButton.FlatStyle = FlatStyle.Flat;
                    newButton.TextAlign = ContentAlignment.MiddleCenter;
                    newButton.BackColor = Color.FromArgb(0, 0, 64);
                    newButton.ForeColor = Color.FromArgb(255, 255, 128);
                    newButton.FlatAppearance.MouseOverBackColor = Color.Maroon;
                    newButton.FlatAppearance.MouseDownBackColor = Color.Brown;
                    newButton.FlatAppearance.BorderSize = 5;
                    newButton.Font = new Font(new FontFamily("MingLiU"), 11F, FontStyle.Bold);
                    newButton.Click += new System.EventHandler(this.subFunctionClick);
                    panelShowSubButtons.Controls.Add(newButton);

                    if (sideFunctionsSets.Count == 3)
                    {
                        newButton = new Button();
                        newButton.Location = new Point(8, 172);
                        newButton.Size = new Size(lblWidth, lblHeight);
                        newButton.Text = sideFunctionsSets.ElementAt(2).Function;
                        newButton.FlatStyle = FlatStyle.Flat;
                        newButton.TextAlign = ContentAlignment.MiddleCenter;
                        newButton.BackColor = Color.FromArgb(0, 0, 64);
                        newButton.ForeColor = Color.FromArgb(255, 255, 128);
                        newButton.FlatAppearance.MouseOverBackColor = Color.Maroon;
                        newButton.FlatAppearance.MouseDownBackColor = Color.Brown;
                        newButton.FlatAppearance.BorderSize = 5;
                        newButton.Font = new Font(new FontFamily("MingLiU"), 11F, FontStyle.Bold);
                        newButton.Click += new System.EventHandler(this.subFunctionClick);
                        panelShowSubButtons.Controls.Add(newButton);
                    }
                    else if (sideFunctionsSets.Count == 4)
                    {
                        newButton = new Button();
                        newButton.Location = new Point(8, 172);
                        newButton.Size = new Size(lblWidth, lblHeight);
                        newButton.Text = sideFunctionsSets.ElementAt(2).Function;
                        newButton.FlatStyle = FlatStyle.Flat;
                        newButton.TextAlign = ContentAlignment.MiddleCenter;
                        newButton.BackColor = Color.FromArgb(0, 0, 64);
                        newButton.ForeColor = Color.FromArgb(255, 255, 128);
                        newButton.FlatAppearance.MouseOverBackColor = Color.Maroon;
                        newButton.FlatAppearance.MouseDownBackColor = Color.Brown;
                        newButton.FlatAppearance.BorderSize = 5;
                        newButton.Font = new Font(new FontFamily("MingLiU"), 11F, FontStyle.Bold);
                        newButton.Click += new System.EventHandler(this.subFunctionClick);
                        panelShowSubButtons.Controls.Add(newButton);

                        newButton = new Button();
                        newButton.Location = new Point(8, 256);
                        newButton.Size = new Size(lblWidth, lblHeight);
                        newButton.Text = sideFunctionsSets.ElementAt(3).Function;
                        newButton.FlatStyle = FlatStyle.Flat;
                        newButton.TextAlign = ContentAlignment.MiddleCenter;
                        newButton.BackColor = Color.FromArgb(0, 0, 64);
                        newButton.ForeColor = Color.FromArgb(255, 255, 128);
                        newButton.FlatAppearance.MouseOverBackColor = Color.Maroon;
                        newButton.FlatAppearance.MouseDownBackColor = Color.Brown;
                        newButton.FlatAppearance.BorderSize = 5;
                        newButton.Font = new Font(new FontFamily("MingLiU"), 11F, FontStyle.Bold);
                        newButton.Click += new System.EventHandler(this.subFunctionClick);
                        panelShowSubButtons.Controls.Add(newButton);
                    }
                }
                else
                {
                    if (mainID != "07" && mainID != "08" && mainID != "10" && mainID != "13")
                    {
                        newButton.Text = "查 詢";
                        //else
                        //    newButton.Text = "本日支出";

                        newButton.Click += new System.EventHandler(this.mainFunctionClick);
                        panelShowSubButtons.Controls.Add(newButton);

                    }

                    ShowPages(0, mainFunctionText, "");
                }
            }
        }

        private void subFunctionClick(System.Object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            ShowPages(1, lblInvisibleMainFunction.Text, ctrl.Text.Trim());
        }

        private void mainFunctionClick(System.Object sender, EventArgs e)
        {
            Control ctrl = (Control)sender;
            ShowPages(0, lblInvisibleMainFunction.Text, "");
        }

        private void SearchAgain()
        {
            int searchIndex = 0;
            string mainFunction = lblCurrentPage.Text.Substring(0, lblCurrentPage.Text.IndexOf('>'));
            string subFunction = lblCurrentPage.Text.Substring(lblCurrentPage.Text.IndexOf('>') + 1);

            if (subFunction.Trim() != "")
                searchIndex = 1;

            ShowPages(searchIndex, mainFunction, subFunction);
            //this.Enabled = false;
        }

        private void LoadStaffRole()
        {
            if (bool.Parse(lblGotGranted.Text))
            {
                List<string> roleSet = (List<string>)facade.FacadeFunctions("select", "staffrolematchnamelist", lblInvisibleStaffRoleID.Text, null);

                cboInsertStaffRole.DataSource = roleSet;
                
            }
        }

        #endregion

        #region MainScreen Panel

        public void EnableAllMainButtons()
        {
            //this.Enabled = true;
            panelMainGeneralScreen.Enabled = true;
            btnMainNewStudent.Enabled = true;
            btnMainSystemChangePassword.Enabled = true;
            btnMainOldStudent.Enabled = true;
            btnMainStudentPayment.Enabled = true;
            btnMainSearchPayment.Enabled = true;
            btnMainSearchRefund.Enabled = true;
            btnMainPrintPaymentNote.Enabled = true;
            btnMainManageStudentData.Enabled = true;
            btnMainManageClassData.Enabled = true;
            btnMainDailyBackup.Enabled = true;
            btnMainSearchPaymentRecord.Enabled = true;
            btnMainSearchStudentNumber.Enabled = true;
            btnMainManageStaffData.Enabled = true;
            btnMainSearchDailyExpanse.Enabled = true;
            btnMainManageSystemData.Enabled = true;

            btnMainNewStudent.BackColor = Color.FromArgb(0, 0, 64);
            btnMainSystemChangePassword.BackColor = Color.FromArgb(0, 0, 64);
            btnMainOldStudent.BackColor = Color.FromArgb(0, 0, 64);
            btnMainStudentPayment.BackColor = Color.FromArgb(0, 0, 64);
            btnMainSearchPayment.BackColor = Color.FromArgb(0, 0, 64);
            btnMainSearchRefund.BackColor = Color.FromArgb(0, 0, 64);
            btnMainPrintPaymentNote.BackColor = Color.FromArgb(0, 0, 64);
            btnMainManageStudentData.BackColor = Color.FromArgb(0, 0, 64);
            btnMainManageClassData.BackColor = Color.FromArgb(0, 0, 64);
            btnMainDailyBackup.BackColor = Color.FromArgb(0, 0, 64);
            btnMainSearchPaymentRecord.BackColor = Color.FromArgb(0, 0, 64);
            btnMainSearchStudentNumber.BackColor = Color.FromArgb(0, 0, 64);
            btnMainManageStaffData.BackColor = Color.FromArgb(0, 0, 64);
            btnMainSearchDailyExpanse.BackColor = Color.FromArgb(0, 0, 64);
            btnMainManageSystemData.BackColor = Color.FromArgb(0, 0, 64);
        }

        private void btnMainSystemLogInOut_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Threading.Thread newForm = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            newForm.Start();
        }

        private void btnMainSystemChangePassword_Click(object sender, EventArgs e)
        {
            DefaultSetting();
            btnMainSystemChangePassword.BackColor = Color.Brown;
            CallfrmChangeUserPassword();
        }

        private void DisableAllMainButtons()
        {
            //this.Enabled = false;
            panelMainGeneralScreen.Enabled = false;
            btnMainNewStudent.Enabled = false;
            btnMainOldStudent.Enabled = false;
            btnMainStudentPayment.Enabled = false;
            btnMainSearchPayment.Enabled = false;
            btnMainSearchRefund.Enabled = false;
            btnMainPrintPaymentNote.Enabled = false;
            btnMainManageStudentData.Enabled = false;
            btnMainManageClassData.Enabled = false;
            btnMainDailyBackup.Enabled = false;
            btnMainSearchPaymentRecord.Enabled = false;
            btnMainSearchStudentNumber.Enabled = false;
            btnMainManageStaffData.Enabled = false;
            btnMainSearchDailyExpanse.Enabled = false;
            btnMainManageSystemData.Enabled = false;
        }

        private void btnMainNewStudent_Click(object sender, EventArgs e)
        {
            DefaultSetting();
            LoadStudentDOBYear();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelInsertScreen.Visible = true;
            //panelInsertStudentQuick.Visible = true;
            //panelMainMenuScreen.Visible = false;
            //lblInvisibleCheckNodeOneIsSelected.Text = "true";
            btnMainNewStudent.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainNewStudent.Text);
            //SettingCurrentPage("新生選課管理 > 新增學生");
        }

        private void btnMainOldStudent_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            ////panelSearchStudentScreen.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainOldStudent.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainOldStudent.Text);
            //SettingCurrentPage("舊生加選課程 > 個別加選 > 學生查詢");
        }

        private void btnMainStudentPayment_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            ////panelSearchStudentScreen.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainStudentPayment.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainStudentPayment.Text);

            //SettingCurrentPage("學生收費管理 > 查找學生/班級");
        }

        private void btnMainSearchPayment_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainSearchPayment.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainSearchPayment.Text);
            //SettingCurrentPage("應收費用查詢 > 單科明細");
        }

        private void btnMainSearchRefund_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainSearchRefund.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainSearchRefund.Text);

            //SettingCurrentPage("退費紀錄查詢 > 退費管理 > 查找學生/班級");
        }

        private void btnMainPrintPaymentNote_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainPrintPaymentNote.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainPrintPaymentNote.Text);
        }

        private void btnMainManageStudentData_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainManageStudentData.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainManageStudentData.Text);
            //cboStudentDataManagementSearchBy.SelectedIndex = -1;

            //SettingCurrentPage("學生資料管理 > 學生資料查詢");
        }

        private void btnMainManageClassData_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainManageClassData.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainManageClassData.Text);
            //cboClassDataManagementSearchBy.SelectedIndex = -1;

            //SettingCurrentPage("課程內容建檔 > 課程查詢");
        }

        private void btnMainDailyBackup_Click(object sender, EventArgs e)
        {
            //DefaultSetting();

            //////panelTopScreen.Visible = true;
            //////panelSideFunctions.Visible = true;
            ////panelMainMenuScreen.Visible = false;
            //btnMainDailyBackup.BackColor = Color.Brown;
            //ShowSaftyChecker(btnMainDailyBackup.Text);

            //string[] argumentsToBatchFile = { "educatemanagement", "root", "123456", "backupfile.sql" };
            //ExecuteBatchFile("MySql_Backup.bat", argumentsToBatchFile);

            //ExecuteBackup();
            ExecuteBatchFile();
        }

        private void btnMainSearchPaymentRecord_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainSearchPaymentRecord.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainSearchPaymentRecord.Text);
        }

        private void btnMainSearchStudentNumber_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainSearchStudentNumber.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainSearchStudentNumber.Text);
        }

        private void btnMainManageStaffData_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainManageStaffData.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainManageStaffData.Text);
        }

        private void btnMainSearchDailyExpanse_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainSearchDailyExpanse.BackColor = Color.Brown;
            ShowSaftyChecker(btnMainSearchDailyExpanse.Text);
        }

        private void btnMainManageSystemData_Click(object sender, EventArgs e)
        {
            DefaultSetting();

            ////panelTopScreen.Visible = true;
            //panelMainMenuScreen.Visible = false;
            btnMainManageSystemData.BackColor = Color.Brown;
            //SettingCurrentPage("系統資料管理 > 公司基本資料");
            ShowSaftyChecker(btnMainManageSystemData.Text);
        }

        #endregion

        #region TopScreen Panel

        private void btnBackToMainScreen_Click(object sender, EventArgs e)
        {
            DefaultSetting();
        }

        private void btnReturnSearchPage_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text.IndexOf("01") > -1)
                SetNewStudentAddNewClass(lblStudentPaymentShowStudentID.Text, lblStudentPaymentShowStudentName.Text);
            else if (lblStudentPaymentFromPage.Text == "StudentRefund")
                RefurnStudentRefundFromStudentPayment();
            else
                ReturnToStudentSearch();
        }

        private void SettingCurrentPage(string currentPage)
        {
            lblCurrentPage.Text = currentPage;
            
            lblStudentManageClassTitle.Text = currentPage;
            lblStudentPaymentTitle.Text = currentPage;
            lblStudentRefund.Text = currentPage;
            lblInsertClassTitle.Text = currentPage;
            lblInsertStudentTitle.Text = currentPage;
            lblInsertStaff.Text = currentPage;
            lblRecordChecker.Text = currentPage;
            lblCompanyDetail.Text = currentPage;
            lblSystemLog.Text = currentPage;

            ReSize();
        }

        #endregion

        #region Safty Checker

        private void ShowSaftyChecker(string currentPage)
        {
            //DefaultSetting();

            //panelTopScreen.Visible = true;
            //panelSideFunctions.Visible = true;
            //panelSubButtons.Visible = true;
            
            //panelSaftyChecker.Visible = true;
            //panelMainMenuScreen.Visible = false;

            //btnReturnSearchPage.Visible = false;

            Control c = GetNextControl((Control)panelContainSaftyChecker, true);
            if (c != null)
                c.Focus();

            if (panelShowSubButtons.Controls.Count > 0)
                panelShowSubButtons.Controls.Clear();

            bool needMasterKey = false;
            if (currentPage.IndexOf("01") > -1 || currentPage.IndexOf("02") > -1 || currentPage.IndexOf("03") > -1 || currentPage.IndexOf("04") > -1 ||
                currentPage.IndexOf("05") > -1 || currentPage.IndexOf("06") > -1 || currentPage.IndexOf("07") > -1 || currentPage.IndexOf("08") > -1 ||
                currentPage.IndexOf("09") > -1 || currentPage.IndexOf("10") > -1 || !bool.Parse(lblGotGranted.Text))
            {
                txtSaftyCheckerMasterKey.Visible = false;
            }
            else
            {
                txtSaftyCheckerMasterKey.Visible = true;
                needMasterKey = true;
            }

            DisableAllMainButtons();
            txtSaftyCheckerPassword.Text = "";
            txtSaftyCheckerMasterKey.Text = "";

            SettingCurrentPage(currentPage);
            CallfrmSaftyChecker(needMasterKey);
        }

        private void btnSaftyChecker_Click(object sender, EventArgs e)
        {
            StaftyChecker();
        }

        private void txtSaftyCheckerPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (txtSaftyCheckerMasterKey.Visible)
                {
                    e.Handled = true;
                    Control c = GetNextControl((Control)sender, true);
                    if (c != null)
                        c.Focus();
                }
                else
                    StaftyChecker();
            }
        }

        private void txtSaftyCheckerMasterKey_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                StaftyChecker();
        }

        private void StaftyChecker()
        {
            bool isOK = false;
            if (txtSaftyCheckerPassword.Text.Trim() == lblInvisibleStaffPassword.Text)
            {
                if (txtSaftyCheckerMasterKey.Visible)
                {
                    if (txtSaftyCheckerMasterKey.Text.Trim() == lblInvisibleStaffMasterKey.Text)
                        isOK = true;
                }
                else
                    isOK = true;
            }

            txtSaftyCheckerPassword.Text = "";
            txtSaftyCheckerMasterKey.Text = "";

            if (isOK)
                LoadSubButtons(lblCurrentPage.Text);
            else
                MessageBox.Show("密碼錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region SideFunctions Panel

        //private void tvSideFunctions_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        //{
        //    if (e.Node.Index == 0)
        //    {
        //        lblInvisibleCheckNodeOneIsSelected.Text = "true";
        //    }
        //}

        //private void tvSideFunctions_AfterSelect(object sender, TreeViewEventArgs e)
        //{
        //    string currentSelect = null;

        //    //if (tvSideFunctions.SelectedNode.Level == 0)
        //    //{
        //    if (e.Node.Index == 0)
        //    {
        //        if (lblInvisibleCheckNodeOneIsSelected.Text == "true")
        //        {
        //            if (lblCurrentPage.Text.IndexOf(e.Node.Text) > -1)
        //                LoadSubButtons(tvSideFunctions.SelectedNode.Text);
        //            else
        //            {
        //                currentSelect = tvSideFunctions.SelectedNode.Text.Trim();
        //                ShowSaftyChecker(currentSelect);
        //            }
        //        }
        //        else
        //            tvSideFunctions.SelectedNode = null;
        //    }
        //    else
        //    {
        //        if (lblCurrentPage.Text.IndexOf(e.Node.Text) > -1)
        //            LoadSubButtons(tvSideFunctions.SelectedNode.Text);
        //        else
        //        {
        //            currentSelect = tvSideFunctions.SelectedNode.Text.Trim();
        //            ShowSaftyChecker(currentSelect);
        //        }
        //    }
        //    //}
        //    //else if (tvSideFunctions.SelectedNode.Level == 1)
        //    //{
        //    //    if (lblCurrentPage.Text.IndexOf(tvSideFunctions.SelectedNode.Parent.Text) > -1)
        //    //        ShowPages();
        //    //    else
        //    //    {
        //    //        currentSelect = tvSideFunctions.SelectedNode.Parent.Text.Trim();
        //    //        ShowSaftyChecker(currentSelect);
        //    //    }
        //    //}
        //    //currentSelect = tvSideFunctions.SelectedNode.Parent.Text.Trim();
        //}

        private void ShowPages(int subFunctionsNum, string mainFunctionText, string subFunctionText)
        {
            DefaultSetting();

            cboStudentSearchRecord.SelectedIndex = -1;
            cboStudentSearchBy.SelectedIndex = -1;

            panelSubButtons.Visible = true;
            //panelTopScreen.Visible = true;
            //panelSideFunctions.Visible = true;
            //cboStudentSearchRecord.Visible = false;
            panelStudentSearchPage.Visible = false;
            panelStudentSearchRecordBy.Visible = false;
            //cboStudentSearchBy.Visible = false;
            panelStudentSearchPage.Visible = false;
            panelMainMenuScreen.Visible = false;

            btnReturnSearchPage.Visible = false;
            btnStudentSearchReturnClassListFromClassInfo.Visible = false;

            string currentSelect = null;

            btnStudentSearch.Text = "搜 尋";

            cboStudentSearchRecord.Items.Clear();
            cboStudentSearchRecord.Items.Add("個人記錄");
            cboStudentSearchRecord.Items.Add("全班記錄");

            cboStudentSearchBy.Items.Clear();
            cboStudentSearchBy.Items.Add("學生編號");
            cboStudentSearchBy.Items.Add("學生姓名");
            cboStudentSearchBy.Items.Add("班級編號");
            cboStudentSearchBy.Items.Add("班級名稱");

            lblStudentSearchClassList.Text = "班級列表:";

            if (subFunctionsNum == 0)
            {
                currentSelect = mainFunctionText;

                if (mainFunctionText.IndexOf("01") > -1)
                {
                    //panelInsertScreen.Visible = true;
                    //panelInsertStudentQuick.Visible = true;

                    //btnStudentAddNewClassSearchStudent.Visible = false;
                    btnStudentManageClassByPersonRemoveClass.Visible = false;
                    btnNewStudentClassPayment.Visible = true;
                    panelSubButtons.Visible = true;
                    panelStudentManageClass.Visible = true;
                    panelMainMenuScreen.Visible = false;
                    panelStudentManageClassByPersonShowClassInfo.Visible = true;
                    panelStudentManageClassAddNewClassAddClass.Visible = true;
                    currentSelect += " > 新生加選";
                    CallfrmAddNewStudentInfo();
                }
                else if (mainFunctionText.IndexOf("04") > -1)
                {
                    panelRecordChecker.Visible = true;
                    panelRecordCheckerWithoutDetail.Visible = true;
                    CallfrmSearchRecordData(mainFunctionText + "全班");
                    //panelRecordCheckerRecordWithDetail.Visible = false;
                    //panelRecordCheckerWithDetailList.Visible = true;
                    //panelRecordCheckerWithTwoDetailList.Visible = false;
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons(mainFunctionText + "> 學生資料查詢", "應收費用", "應收費用", "", "顯示學生");
                }
                else if (mainFunctionText.IndexOf("06") > -1)
                {
                    panelRecordChecker.Visible = true;
                    panelRecordCheckerWithoutDetail.Visible = true;
                    CallfrmSearchRecordData(mainFunctionText);
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons(mainFunctionText + "> 學生資料查詢", "繳費通知", "繳費通知", "", "顯示學生");

                    //cboStudentSearchBy.Items.Insert(0, "新增學生");
                }
                else if (mainFunctionText.IndexOf("07") > -1)
                {
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons("學生資料查詢", "學生管理", "學生管理", "", "顯示學生");

                    //cboStudentSearchBy.Items.Insert(0, "新增學生");
                    panelInsertScreen.Visible = true;
                    panelInsertStudent.Visible = true;
                }
                else if (mainFunctionText.IndexOf("08") > -1)
                {
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons("課程資料查詢", "課程管理", "課程管理", "", "顯示課程");

                    //cboStudentSearchBy.Items.RemoveAt(1);
                    //cboStudentSearchBy.Items.RemoveAt(0);
                    //cboStudentSearchBy.Items.Insert(0, "新增課程");
                    panelInsertScreen.Visible = true;
                    panelInsertClass.Visible = true;
                }
                else if (mainFunctionText.IndexOf("10") > -1)
                {
                    facade = new FacadeLayer(SystemTypeForDB);
                    panelDailyExpanse.Visible = true;
                    panelSearchExpanse.Visible = bool.Parse(lblGotGranted.Text);
                    ShowsDailyExpanseData(facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseStartDate.Value, null).ToString(),
                                                        facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseEndDate.Value.AddDays(1), null).ToString());
                }
                else if (mainFunctionText.IndexOf("13") > -1)
                {
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons("員工資料查詢", "員工管理", "員工管理", "", "顯示員工");

                    //lblStudentSearchClassList.Text = "員工列表:";

                    //cboStudentSearchBy.Items.Clear();
                    //cboStudentSearchBy.Items.Add("新增員工");
                    //cboStudentSearchBy.Items.Add("員工編號");
                    //cboStudentSearchBy.Items.Add("員工姓名");
                    //cboStudentSearchBy.Items.Add("員工英文");
                    panelInsertScreen.Visible = true;
                    panelInsertStaff.Visible = true;
                }
            }
            else if (subFunctionsNum == 1)
            {
                if (mainFunctionText.IndexOf("02") > -1)
                {
                    btnNewStudentClassPayment.Visible = true;
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 學生資料查詢", "加選課程", "加選課程", "", "顯示學生");
                    panelSubButtons.Visible = true;
                    panelStudentManageClass.Visible = true;
                    panelMainMenuScreen.Visible = false;
                    //btnStudentAddNewClassSearchStudent.Visible = true;

                    panelStudentManageClassByPersonShowClassInfo.Visible = true;
                    panelStudentManageClassAddNewClassAddClass.Visible = true;
                    btnStudentManageClassByPersonRemoveClass.Visible = false;
                    //btnNewStudentClassPayment.Visible = false;

                    if (subFunctionText.IndexOf("全班") > -1)
                    {
                        //btnStudentAddNewClassSearchStudent.Text = "班級查詢";
                        btnNewStudentClassPayment.Visible = false;
                        panelStudentManageClassByClassShowClassInfo.Visible = true;
                        panelStudentManageClassByPersonShowClassInfo.Visible = false;
                        panelStudentManageClassAddNewClassAddClass.Visible = false;

                        CallfrmSearchClassData();

                        //cboStudentSearchBy.Items.RemoveAt(1);
                        //cboStudentSearchBy.Items.RemoveAt(0);

                        //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 班級資料查詢", "加選課程", "全班加選", "", "顯示學生");

                        //if (subFunctionText.IndexOf("刪除") > -1)
                        //    SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 班級資料查詢", "加選課程", "全班刪除", "", "顯示學生");
                    }
                    else
                    {
                        CallfrmSearchStudentData();

                        if (subFunctionText.IndexOf("刪除") > -1)
                        {
                            btnNewStudentClassPayment.Visible = false;
                            panelStudentManageClassAddNewClassAddClass.Visible = false;
                            btnStudentManageClassByPersonRemoveClass.Visible = true;
                        }
                    }
                    //else if (subFunctionText.IndexOf("刪除") > -1)
                    //    SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 學生資料查詢", "刪除課程", "刪除課程", "", "顯示學生");
                }
                else if (mainFunctionText.IndexOf("03") > -1)
                {
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 學生資料查詢", "學生付費", "學生付費", "", "顯示學生");

                    panelSubButtons.Visible = true;
                    panelStudentPayment.Visible = true;
                    panelMainMenuScreen.Visible = false;

                    panelStudentPaymentManagementPage.Visible = true;
                    panelStudentPaymentByClassPaymentPage.Visible = false;

                    if (subFunctionText.IndexOf("全班") > -1)
                    {
                        //cboStudentSearchBy.Items.RemoveAt(1);
                        //cboStudentSearchBy.Items.RemoveAt(0);

                        //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 班級資料查詢", "學生付費", "全班付費", "", "顯示學生");
                        CallfrmSearchClassData();

                        panelStudentPaymentManagementPage.Visible = false;
                        panelStudentPaymentByClassPaymentPage.Visible = true;
                    }
                    else
                        CallfrmSearchStudentData();
                }
                else if (mainFunctionText.IndexOf("05") > -1)
                {
                    ////panelSearchStudentScreen.Visible = true;
                    //btnStudentByClass.Visible = false;
                    //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 學生資料查詢", "學生退費", "學生退費", "", "顯示學生");

                    panelStudentRefund.Visible = true;
                    lblStudentRefundStudentID.Text = "學生編號:";
                    lblStudentRefundStudentName.Text = "學生姓名:";

                    if (subFunctionText.IndexOf("個別") > -1)
                        CallfrmSearchStudentData(mainFunctionText);
                    if (subFunctionText.IndexOf("全班") > -1)
                    {
                        lblStudentRefundStudentID.Text = "班級編號:";
                        lblStudentRefundStudentName.Text = "班級名稱:";
                        CallfrmSearchClassData(mainFunctionText);
                        //cboStudentSearchBy.Items.RemoveAt(1);
                        //cboStudentSearchBy.Items.RemoveAt(0);

                        //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 班級資料查詢", "學生退費", "全班退費", "", "顯示學生");
                    }
                    else if (subFunctionText.IndexOf("記錄") > -1)
                    {
                        panelStudentRefund.Visible = false;
                        panelRecordChecker.Visible = true;
                        panelRecordCheckerWithoutDetail.Visible = true;
                        CallfrmSearchRecordData(mainFunctionText);
                    }
                        //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 退費記錄查詢", "退費記錄", "退費記錄", "", "顯示學生");
                }
                else if (mainFunctionText.IndexOf("11") > -1)
                {
                    ////panelSearchStudentScreen.Visible = true;
                    btnStudentByClass.Visible = false;

                    if (subFunctionText.IndexOf("預收") > -1)
                    {
                        //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 預收明細查詢", "預收明細", "預收明細", "", "顯示學生");
                        //cboStudentSearchRecord.Items.RemoveAt(1);
                        panelRecordChecker.Visible = true;
                        panelRecordCheckerWithoutDetail.Visible = true;
                        CallfrmSearchRecordData(mainFunctionText + subFunctionText);
                    }
                    else
                    {
                        panelRecordChecker.Visible = true;
                        panelRecordCheckerWithoutDetail.Visible = true;
                        CallfrmSearchRecordData(mainFunctionText + subFunctionText);
                    }
                        //SetStudentSearchLableButtons(mainFunctionText + " > " + subFunctionText + "> 繳費記錄查詢", "繳費記錄", "繳費記錄", "", "顯示學生");
                }
                else if (mainFunctionText.IndexOf("12") > -1)
                {
                    EnablefrmEMSAndRestart();
                    if (subFunctionText.IndexOf("加退") > -1)
                    {
                        panelRecordChecker.Visible = true;
                        panelRecordCheckerWithoutDetail.Visible = true;
                        CallfrmSearchRecordData(mainFunctionText + "全班");
                    }
                    else
                    {
                        lblRecordCheckerWithDetailRecordList.Text = lblShowToday.Text + " 本日學生總數";
                        recordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentinclasstotalnumber", "", "");
                        ShowRecordList(recordSets, false);
                        string studentTotalCount = facade.FacadeFunctions("select", "studenttotalnumber", "", "").ToString();
                        lblRecordCheckerWithoutDetailRecordListNote.Text += ", 總人數: " + studentTotalCount;
                    }
                }
                else if (mainFunctionText.IndexOf("14") > -1)
                {
                    if (subFunctionText.IndexOf("公司") > -1)
                    {
                        panelCompanyDetail.Visible = true;

                        facade = new FacadeLayer(SystemTypeForDB);
                        companyInfo = (CompanyInfoDefinition)facade.FacadeFunctions("select", "whole", "CompanyInfo", null);

                        txtCompanyName.Text = companyInfo.CompanyName;
                        txtCompanyManager.Text = companyInfo.CompanyManager;
                        lblShowStartTime.Text = companyInfo.StartTime;
                        btnStartSystem.Visible = false;
                        lblShowStartTime.Visible = false;
                        if (companyInfo.StartTime == "" || companyInfo.StartTime == null)
                            btnStartSystem.Visible = true;
                        else
                            lblShowStartTime.Visible = true;
                    }
                    else
                    {
                        panelSystemLog.Visible = true;
                        cboSystemLogSearchBy.SelectedIndex = 0;
                    }
                }

                currentSelect = mainFunctionText + " > " + subFunctionText;
            }

            //if (currentSelect.IndexOf("記錄") > -1 || currentSelect.IndexOf("04") > -1 || currentSelect.IndexOf("06") > -1 || currentSelect.IndexOf("12") > -1)
            //{
            //    panelStudentSearchRecordBy.Visible = true;
            //    panelStudentSearchPage.Visible = false;
            //}
            //else
            //{
            //    panelStudentSearchRecordBy.Visible = false;
            //    panelStudentSearchPage.Visible = true;
            //}

            SettingCurrentPage(currentSelect);
        }

        //private void ShowPages()
        //{
        //    DefaultSetting();

        //    cboStudentSearchRecord.SelectedIndex = -1;
        //    cboStudentSearchBy.SelectedIndex = -1;

        //    //panelTopScreen.Visible = true;
        //    //panelSideFunctions.Visible = true;
        //    //cboStudentSearchRecord.Visible = false;
        //    panelStudentSearchPage.Visible = false;
        //    panelStudentSearchRecordBy.Visible = false;
        //    //cboStudentSearchBy.Visible = false;
        //    panelStudentSearchPage.Visible = false;
        //    panelMainMenuScreen.Visible = false;

        //    btnStudentSearchReturnClassListFromClassInfo.Visible = false;

        //    string currentSelect = null;

        //    btnStudentSearch.Text = "搜 尋";

        //    cboStudentSearchRecord.Items.Clear();
        //    cboStudentSearchRecord.Items.Add("個人記錄");
        //    cboStudentSearchRecord.Items.Add("全班記錄");

        //    cboStudentSearchBy.Items.Clear();
        //    cboStudentSearchBy.Items.Add("學生編號");
        //    cboStudentSearchBy.Items.Add("學生姓名");
        //    cboStudentSearchBy.Items.Add("班級編號");
        //    cboStudentSearchBy.Items.Add("班級名稱");

        //    lblStudentSearchClassList.Text = "班級列表:";

        //    if (tvSideFunctions.SelectedNode.Level == 0)
        //    {
        //        currentSelect = tvSideFunctions.SelectedNode.Text.Trim();

        //        if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("01") > -1)
        //        {
        //            panelInsertScreen.Visible = true;
        //            panelInsertStudentQuick.Visible = true;

        //            currentSelect += " > 新增學生";
        //        }
        //        else if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("04") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("學生資料查詢", "應收費用", "應收費用", "", "顯示學生");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("06") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("學生資料查詢", "繳費通知", "繳費通知", "", "顯示學生");

        //            cboStudentSearchBy.Items.Insert(0, "新增學生");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("07") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("學生資料查詢", "學生管理", "學生管理", "", "顯示學生");

        //            cboStudentSearchBy.Items.Insert(0, "新增學生");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("08") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("課程資料查詢", "課程管理", "課程管理", "", "顯示課程");

        //            cboStudentSearchBy.Items.RemoveAt(1);
        //            cboStudentSearchBy.Items.RemoveAt(0);
        //            cboStudentSearchBy.Items.Insert(0, "新增課程");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("12") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            cboStudentSearchRecord.Items.RemoveAt(0);
        //            SetStudentSearchLableButtons("課程人次查詢", "應收費用", "應收費用", "", "顯示學生");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Text.Trim().IndexOf("13") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("員工資料查詢", "員工管理", "員工管理", "", "顯示員工");

        //            lblStudentSearchClassList.Text = "員工列表:";

        //            cboStudentSearchBy.Items.Clear();
        //            cboStudentSearchBy.Items.Add("新增員工");
        //            cboStudentSearchBy.Items.Add("員工編號");
        //            cboStudentSearchBy.Items.Add("員工姓名");
        //            cboStudentSearchBy.Items.Add("員工英文");
        //        }
        //    }
        //    else if (tvSideFunctions.SelectedNode.Level == 1)
        //    {
        //        if (tvSideFunctions.SelectedNode.Parent.Text.Trim().IndexOf("02") > -1)
        //        {
        //            btnNewStudentClassPayment.Visible = false;
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("學生資料查詢", "加選課程", "加選課程", "", "顯示學生");

        //            if (tvSideFunctions.SelectedNode.Text.IndexOf("全班") > -1)
        //            {
        //                cboStudentSearchBy.Items.RemoveAt(1);
        //                cboStudentSearchBy.Items.RemoveAt(0);

        //                SetStudentSearchLableButtons("班級資料查詢", "加選課程", "全班加選", "", "顯示學生");

        //                if (tvSideFunctions.SelectedNode.NextNode == null)
        //                    SetStudentSearchLableButtons("班級資料查詢", "加選課程", "全班刪除", "", "顯示學生");
        //            }
        //            else if (tvSideFunctions.SelectedNode.PrevNode != null)
        //                SetStudentSearchLableButtons("學生資料查詢", "刪除課程", "刪除課程", "", "顯示學生");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Parent.Text.Trim().IndexOf("03") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("學生資料查詢", "學生付費", "學生付費", "", "顯示學生");

        //            if (tvSideFunctions.SelectedNode.Text.IndexOf("全班") > -1)
        //            {
        //                cboStudentSearchBy.Items.RemoveAt(1);
        //                cboStudentSearchBy.Items.RemoveAt(0);

        //                SetStudentSearchLableButtons("班級資料查詢", "學生付費", "全班付費", "", "顯示學生");
        //            }
        //        }
        //        else if (tvSideFunctions.SelectedNode.Parent.Text.Trim().IndexOf("05") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;
        //            SetStudentSearchLableButtons("學生資料查詢", "學生退費", "學生退費", "", "顯示學生");

        //            if (tvSideFunctions.SelectedNode.Text.IndexOf("全班") > -1)
        //            {
        //                cboStudentSearchBy.Items.RemoveAt(1);
        //                cboStudentSearchBy.Items.RemoveAt(0);

        //                SetStudentSearchLableButtons("班級資料查詢", "學生退費", "全班退費", "", "顯示學生");
        //            }
        //            else if (tvSideFunctions.SelectedNode.Text.IndexOf("記錄") > -1)
        //                SetStudentSearchLableButtons("退費記錄查詢", "退費記錄", "退費記錄", "", "顯示學生");
        //        }
        //        else if (tvSideFunctions.SelectedNode.Parent.Text.Trim().IndexOf("11") > -1)
        //        {
        //            //panelSearchStudentScreen.Visible = true;
        //            btnStudentByClass.Visible = false;

        //            if (tvSideFunctions.SelectedNode.Text.IndexOf("預收") > -1)
        //            {
        //                SetStudentSearchLableButtons("預收明細查詢", "預收明細", "預收明細", "", "顯示學生");
        //                cboStudentSearchRecord.Items.RemoveAt(1);
        //            }
        //            else
        //                SetStudentSearchLableButtons("繳費記錄查詢", "繳費記錄", "繳費記錄", "", "顯示學生");
        //        }

        //        currentSelect = tvSideFunctions.SelectedNode.Parent.Text.Trim() + " > " + tvSideFunctions.SelectedNode.Text.Trim();
        //    }

        //    if (currentSelect.IndexOf("記錄") > -1 || currentSelect.IndexOf("04") > -1 || currentSelect.IndexOf("06") > -1 || currentSelect.IndexOf("12") > -1)
        //    {
        //        panelStudentSearchRecordBy.Visible = true;
        //        panelStudentSearchPage.Visible = false;
        //    }
        //    else
        //    {
        //        panelStudentSearchRecordBy.Visible = false;
        //        panelStudentSearchPage.Visible = true;
        //    }

        //    SettingCurrentPage(currentSelect);
        //}

        #endregion

        #region Search Student Panel

        private void SetStudentSearchDefault()
        {
            SetStudentSearchPanelDefault();

            //Search Record
            panelStudentSearchRecordByDate.Visible = false;
            dtpStudentSearchRecordFromDate.Checked = false;
            dtpStudentSearchRecordEndDate.Checked = false;
            dtpStudentSearchRecordFromDate.Value = DateTime.Now;
            dtpStudentSearchRecordEndDate.Value = DateTime.Now;
            txtStudentSearchRecord.Visible = false;
            txtStudentSearchRecord.Text = "";
            txtStudentSearchRecordEndContinueNumber.Text = "";
            panelStudentSearchRecordContinueNumber.Visible = false;
            btnStudentSearchRecord.Enabled = false;
            cboStudentSearchRecordSearchBy.SelectedIndex = -1;
            cboStudentSearchRecordSearchBy.Visible = false;

            lblStudentSearchStudentID.Text = "學生編號:";
            lblStudentSearchStudentName.Text = "學生姓名:";
            lblStudentSearchStudentSex.Text = "學生性別:";
            lblStudentSearchStudentDOB.Text = "學生生日:";
            lblStudentSearchStudentSchool.Text = "學生學校:";
            lblStudentSearchStudentStudyYear.Text = "學生年級:";

            btnStudentSearch.Enabled = false;
            txtStudentSearchByText.Visible = false;
            btnStudentSearchReturnClassList.Visible = false;

            btnStudentSearchShowStudent.Enabled = false;
            btnStudentByStudentInClass.Enabled = false;

            txtStudentSearchByText.Text = "";
        }

        private void SetStudentSearchPanelDefault()
        {
            panelStudentSearchStudentInfo.Visible = false;
            panelStudentSearchStudentInClass.Visible = false;
            panelStudentSearchShowClassList.Visible = false;
            panelStudentRecordList.Visible = false;

            if (dgvStudentSearchShowStudentInClassList.Columns.Count > 0)
                dgvStudentSearchShowStudentInClassList.Columns.Clear();

            if (dgvStudentSearchClassList.Columns.Count > 0)
                dgvStudentSearchClassList.Columns.Clear();
        }

        private void SetStudentSearchLableButtons(string searchLabel, string singleStudent, string studentInClass, string singleClass, string showStudent)
        {
            lblSearchStudentScreen.Text = searchLabel;
            btnStudentByStudentID.Text = singleStudent;
            btnStudentByStudentInClass.Text = studentInClass;
            btnStudentByClass.Text = singleClass;
            btnStudentSearchShowStudent.Text = showStudent;
        }

        private void cboStudentSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudentSearchDefault();

            if (cboStudentSearchBy.SelectedIndex > -1)
            {
                btnStudentSearch.Enabled = true;

                if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("新增") == -1)
                {
                    txtStudentSearchByText.Visible = true;
                    btnStudentSearch.Text = "搜 尋";
                }
                else
                    btnStudentSearch.Text = "新 增";
            }
        }

        private void btnStudentSearch_Click(object sender, EventArgs e)
        {
            bool isRefund = false;
            string selectBy = null, selectData = null;
            studentData = null;
            studentSets = null;
            classData = null;
            classSets = null;
            staffData = null;
            staffSets = null;

            SetStudentSearchPanelDefault();

            if (btnStudentSearch.Text == "新 增")
                AfterStudentSearch("", "");
            else
            {
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
                            studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)selectData);

                        if (studentData != null && studentData.ID != null && int.Parse(studentData.ID) > 0)
                        {
                            panelStudentSearchStudentInfo.Visible = true;

                            lblStudentSearchShowStudentID.Text = studentData.ID;
                            lblStudentSearchShowStudentName.Text = studentData.Name;
                            lblStudentSearchShowStudentSex.Text = studentData.Sex;
                            lblStudentSearchShowStudentDOB.Text = studentData.DateOfBirth;
                            lblStudentSearchShowStudentSchool.Text = studentData.School;
                            lblStudentSearchShowStudentStudyYear.Text = studentData.StudyYear;
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

                        if (lblCurrentPage.Text.IndexOf("退費") > -1)
                        {
                            if (selectBy == "ID" && (classData == null || classData.ID == null))
                            {
                                classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
                                isRefund = true;
                            }
                            else if (lblCurrentPage.Text.IndexOf("全班") > -1)
                            {
                                if (selectBy == "ID")
                                    classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
                                else if (selectBy == "Name")
                                    classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);

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
                            if (lblCurrentPage.Text.IndexOf("08") > -1)
                            {
                                btnStudentSearchReturnClassListFromClassInfo.Visible = false;
                                ShowClassInfoAfterSearch();
                            }
                            else
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

                                //if (lblCurrentPage.Text.IndexOf("退費") > -1 && lblCurrentPage.Text.IndexOf("全班") > -1)
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
                        }
                        else if (classSets != null && classSets.Count > 0)
                        {
                            panelStudentSearchShowClassList.Visible = true;

                            SearchStudentShowClassList();
                        }
                        else
                            MessageBox.Show("查無此班級資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("員工") > -1)
                    {
                        if (selectBy == "ID")
                            staffData = (StaffDefinition)facade.FacadeFunctions("select", "staffbyid", (object)selectData, null);
                        else if (selectBy == "Name")
                            staffSets = (List<StaffDefinition>)facade.FacadeFunctions("select", "staffbyname", (object)selectData, null);
                        else
                            staffData = (StaffDefinition)facade.FacadeFunctions("select", "staffbyenglishname", (object)selectData, null);

                        if (staffData != null && staffData.ID != null)
                        {
                            btnStudentSearchReturnClassListFromClassInfo.Visible = false;
                            ShowStaffInfoAfterSearch();
                        }
                        else if (staffSets != null && staffSets.Count > 0)
                        {
                            panelStudentSearchShowClassList.Visible = true;

                            SearchStaffShowStaffList();
                        }
                        else
                            MessageBox.Show("查無此員工資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            //selectData = txtStudentSearchByText.Text.Trim();
            //if (txtStudentSearchByText.Text.Trim() != "")
            //{
            //    lblStudentSearchInfo.Text = txtStudentSearchByText.Text.Trim();

            //    if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
            //    {
            //        facade = new FacadeLayer(SystemTypeForDB);

            //        if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("編號") > -1)
            //        {
            //            if (txtStudentSearchByText.Text.Trim().Length == 6)
            //            {
            //                if (int.Parse(txtStudentSearchByText.Text.Trim()) != 0)
            //                {
            //                    if (CheckContinueNumber("Student"))
            //                    {
            //                        selectBy = "ID";
            //                        continueNumber = txtStudentSearchRecordEndContinueNumber.Text.Trim();
            //                    }
            //                }
            //                else
            //                    MessageBox.Show("查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //            else
            //                MessageBox.Show("學生編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        else
            //            selectBy = "Name";

            //        if (selectBy != null)
            //        {
            //            if (selectBy == "ID" && lblCurrentPage.Text.IndexOf("記錄") > -1)
            //            {
            //                studentSets = new List<StudentDefinition>();
            //                int firstID = int.Parse(txtStudentSearchByText.Text);
            //                int lastID = int.Parse(txtStudentSearchByText.Text);

            //                if (continueNumber != "")
            //                    lastID = int.Parse(continueNumber);

            //                int numberGap = lastID - firstID;

            //                if (numberGap < 0)
            //                {
            //                    int tempID = firstID;
            //                    firstID = lastID;
            //                    lastID = tempID;
            //                    numberGap = lastID - firstID;
            //                }

            //                if (numberGap != 0)
            //                {
            //                    for (int i = firstID; i <= lastID; i++)
            //                    {
            //                        studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)selectBy, (object)i);

            //                        if (studentData != null)
            //                            studentSets.Add(studentData);

            //                        studentData = null;
            //                    }
            //                }
            //                else
            //                    studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)selectBy, (object)txtStudentSearchByText.Text.Trim());
            //            }
            //            else if (selectBy == "ID")
            //                studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)selectBy, (object)txtStudentSearchByText.Text.Trim());
            //            else if (selectBy == "Name")
            //                studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)txtStudentSearchByText.Text.Trim());

            //            if (studentData != null && int.Parse(studentData.ID) > 0)
            //            {
            //                panelStudentSearchStudentInfo.Visible = true;

            //                lblStudentSearchShowStudentID.Text = studentData.ID;
            //                lblStudentSearchShowStudentName.Text = studentData.Name;
            //                lblStudentSearchShowStudentSex.Text = studentData.Sex;
            //                lblStudentSearchShowStudentDOB.Text = studentData.DateOfBirth;
            //                lblStudentSearchShowStudentSchool.Text = studentData.School;
            //                lblStudentSearchShowStudentStudyYear.Text = studentData.StudyYear;
            //            }
            //            else if (studentSets != null && studentSets.Count > 0)
            //            {
            //                panelStudentSearchStudentInClass.Visible = true;

            //                lblStudentSearchClassID.Visible = false;
            //                lblStudentSearchShowClassID.Visible = false;
            //                lblStudentSearchClassName.Visible = false;
            //                lblStudentSearchShowClassName.Visible = false;

            //                SearchStudentShowStudentList();
            //            }
            //            else
            //                MessageBox.Show("查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //    else if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
            //    {
            //        studentData = null;
            //        classData = null;
            //        classSets = null;

            //        facade = new FacadeLayer(SystemTypeForDB);
            //        //string selectBy = null, selectData = null;

            //        if (cboStudentSearchBy.SelectedItem.ToString().IndexOf("編號") > -1)
            //        {
            //            if (txtStudentSearchByText.Text.Trim().Length > 0)
            //            {
            //                if (txtStudentSearchByText.Text.Trim().Length <= 7)
            //                {
            //                    if (CheckContinueNumber("Class"))
            //                    {
            //                        selectBy = "ID";
            //                        //selectData = txtStudentSearchByText.Text.Trim();
            //                        continueNumber = txtStudentSearchRecordEndContinueNumber.Text.Trim();
            //                    }
            //                }
            //                else
            //                    MessageBox.Show("班級編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            }
            //            else
            //                MessageBox.Show("請輸入班級編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //        else
            //        {
            //            if (txtStudentSearchByText.Text.Trim().Length > 0)
            //            {
            //                selectBy = "Name";
            //                //selectData = txtStudentSearchByText.Text.Trim();
            //            }
            //            else
            //                MessageBox.Show("請輸入班級名稱!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }

            //        if (selectBy != null)
            //        {
            //            if (selectBy == "ID" && lblCurrentPage.Text.IndexOf("記錄") > -1)
            //            {
            //                classSets = new List<ClassDefinition>();
            //                int firstID = int.Parse(selectData.Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));
            //                int lastID = int.Parse(selectData.Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));

            //                if (txtStudentSearchRecordEndContinueNumber.Text.Trim() != "")
            //                    lastID = int.Parse(txtStudentSearchRecordEndContinueNumber.Text.Trim().Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));


            //                string idLetter = selectData.Substring(0, int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1);
            //                int numberGap = lastID - firstID;

            //                if (numberGap < 0)
            //                {
            //                    int tempID = firstID;
            //                    firstID = lastID;
            //                    lastID = tempID;
            //                    numberGap = lastID - firstID;
            //                }

            //                if (numberGap != 0)
            //                {
            //                    for (int i = firstID; i <= lastID; i++)
            //                    {
            //                        classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)selectBy, (object)idLetter + i.ToString());

            //                        if (classData != null)
            //                            classSets.Add(classData);

            //                        classData = null;
            //                    }
            //                }
            //                else
            //                    classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);
            //            }
            //            else if (selectBy == "ID")
            //                classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);
            //            else if (selectBy == "Name")
            //                classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "class", (object)selectBy, (object)selectData);

            //            if (lblCurrentPage.Text.IndexOf("退費") > -1)
            //            {
            //                if (selectBy == "ID" && lblCurrentPage.Text.IndexOf("記錄") > -1)
            //                {
            //                    classSets = new List<ClassDefinition>();
            //                    int firstID = int.Parse(selectData.Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));
            //                    int lastID = int.Parse(selectData.Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));

            //                    if (txtStudentSearchRecordEndContinueNumber.Text.Trim() != "")
            //                        lastID = int.Parse(txtStudentSearchRecordEndContinueNumber.Text.Trim().Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));

            //                    string idLetter = selectData.Substring(0, int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1);
            //                    int numberGap = lastID - firstID;

            //                    if (numberGap < 0)
            //                    {
            //                        int tempID = firstID;
            //                        firstID = lastID;
            //                        lastID = tempID;
            //                        numberGap = lastID - firstID;
            //                    }

            //                    if (numberGap != 0)
            //                    {
            //                        for (int i = firstID; i <= lastID; i++)
            //                        {
            //                            classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)idLetter + i.ToString());

            //                            if (classData != null)
            //                                classSets.Add(classData);

            //                            classData = null;
            //                        }
            //                    }
            //                    else
            //                        classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
            //                }
            //                else if (selectBy == "ID")
            //                    classData = (ClassDefinition)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
            //                else if (selectBy == "Name")
            //                    classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "studenthavetorefundbyclassidorname", (object)selectBy, (object)selectData);
            //            }

            //            if (classData != null && classData.ID != null)
            //            {
            //                panelStudentSearchStudentInClass.Visible = true;

            //                lblStudentSearchClassID.Visible = true;
            //                lblStudentSearchShowClassID.Visible = true;
            //                lblStudentSearchClassName.Visible = true;
            //                lblStudentSearchShowClassName.Visible = true;

            //                lblStudentSearchShowClassID.Text = classData.ID;
            //                lblStudentSearchShowClassName.Text = classData.Name;

            //                studentSets = null;

            //                studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

            //                if (lblCurrentPage.Text.IndexOf("退費") > -1)
            //                    studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundinstudentlist", (object)"ClassID", (object)classData.ID);

            //                if (studentSets != null)
            //                {
            //                    if (studentSets.Count > 0)
            //                        SearchStudentShowStudentList();
            //                    else
            //                        MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                }
            //                else
            //                    MessageBox.Show("尚無學生選擇此班級!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            }
            //            else if (classSets != null && classSets.Count > 0)
            //            {
            //                panelStudentSearchShowClassList.Visible = true;

            //                SearchStudentShowClassList();
            //            }
            //            else
            //                MessageBox.Show("查無此班級資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //}
            //else
            //    MessageBox.Show("請輸入查詢資料!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private string CheckStudentSearchItem(string item, string data, string extraNote)
        {
            string selectItem = null;

            if (data != "")
            {
                if (item.IndexOf("學生") > -1)
                {
                    facade = new FacadeLayer(SystemTypeForDB);

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

            //if (lblCurrentPage.Text.IndexOf("全班") == -1)
            //{
            //    if (dgvStudentSearchShowStudentInClassList.Columns.Count == 0 || dgvStudentSearchShowStudentInClassList.Columns[0].Name != "Check")
            //    {
            //        // Initialize and add a check box column.
            //        DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            //        column.Name = "Check";
            //        dgvStudentSearchShowStudentInClassList.Columns.Add(column);
            //        column.HeaderCell.Value = string.Empty;
            //    }
            //}

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

                //if (lblCurrentPage.Text.IndexOf("全班") == -1)
                //{
                //    newCell = new DataGridViewCheckBoxCell();
                //    newRow.Cells.Add(newCell);
                //}

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
            //CreateComboBoxWithEnums();

            if (dgvStudentSearchShowStudentInClassList.Rows.Count > 0)
                dgvStudentSearchShowStudentInClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentSearchShowStudentInClassList.Rows.Count; i++)
                dgvStudentSearchShowStudentInClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentSearchShowStudentInClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //dgvStudentSearchShowStudentInClassList.Columns[0].Width = 20;
            for (int i = 0; i < dgvStudentSearchShowStudentInClassList.Columns.Count; i++)
            {
                dgvStudentSearchShowStudentInClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStudentSearchShowStudentInClassList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvStudentSearchShowStudentInClassList.ReadOnly = true;
            }

            if (lblCurrentPage.Text.IndexOf("全班") == -1)
                btnStudentByStudentInClass.Enabled = false;
            else
                btnStudentByStudentInClass.Enabled = true;
        }

        private void SearchStudentShowClassList()
        {
            if (dgvStudentSearchClassList.Columns.Count > 0)
                dgvStudentSearchClassList.Columns.Clear();

            //if (lblCurrentPage.Text.IndexOf("記錄") > -1 && cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") > -1)
            //{
            //}
            //else
            //{
            //    if (dgvStudentSearchClassList.Columns.Count == 0 || dgvStudentSearchClassList.Columns[0].Name != "Check")
            //    {
            //        // Initialize and add a check box column.
            //        DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            //        column.Name = "Check";
            //        dgvStudentSearchClassList.Columns.Add(column);
            //        column.HeaderCell.Value = string.Empty;
            //    }
            //}

            //Add Student ID
            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程編號";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程名稱";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "起始日期";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "結束日期";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "目前人數";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "課程價格";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "教材費用";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            foreach (var classSingle in classSets)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewCell newCell;

                //if (lblCurrentPage.Text.IndexOf("記錄") > -1 && cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") > -1)
                //{
                //}
                //else
                //{
                //    newCell = new DataGridViewCheckBoxCell();
                //    newRow.Cells.Add(newCell);
                //}

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.ID;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.Name;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.StartDate;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.EndDate;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.Seat;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.Price;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = classSingle.MaterialFee;
                newRow.Cells.Add(newCell);

                dgvStudentSearchClassList.Rows.Add(newRow);
            }

            dgvStudentSearchClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudentSearchClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvStudentSearchClassList.AllowUserToAddRows = false;
            //CreateComboBoxWithEnums();

            if (dgvStudentSearchClassList.Rows.Count > 0)
                dgvStudentSearchClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentSearchClassList.Rows.Count; i++)
                dgvStudentSearchClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentSearchClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //dgvStudentSearchClassList.Columns[0].Width = 20;
            for (int i = 0; i < dgvStudentSearchClassList.Columns.Count; i++)
            {
                dgvStudentSearchClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStudentSearchClassList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvStudentSearchClassList.ReadOnly = true;
            }

            if (lblCurrentPage.Text.IndexOf("記錄") > -1 && cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") > -1)
                btnStudentSearchShowStudent.Enabled = true;
            else
                btnStudentSearchShowStudent.Enabled = false;
        }

        private void SearchStaffShowStaffList()
        {
            if (dgvStudentSearchClassList.Columns.Count > 0)
                dgvStudentSearchClassList.Columns.Clear();

            //if (dgvStudentSearchClassList.Columns.Count == 0 || dgvStudentSearchClassList.Columns[0].Name != "Check")
            //{
            //    // Initialize and add a check box column.
            //    DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            //    column.Name = "Check";
            //    dgvStudentSearchClassList.Columns.Add(column);
            //    column.HeaderCell.Value = string.Empty;
            //}

            //Add Student ID
            DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "員工編號";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "員工姓名";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "英文名字";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            newColumn = new DataGridViewTextBoxColumn();
            newColumn.HeaderText = "起始日期";
            dgvStudentSearchClassList.Columns.Add(newColumn);

            foreach (var staffSingle in staffSets)
            {
                DataGridViewRow newRow = new DataGridViewRow();
                DataGridViewCell newCell;

                //newCell = new DataGridViewCheckBoxCell();
                //newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = staffSingle.ID;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = staffSingle.Name;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = staffSingle.EnglishName;
                newRow.Cells.Add(newCell);

                newCell = new DataGridViewTextBoxCell();
                newCell.Value = staffSingle.StartDate;
                newRow.Cells.Add(newCell);

                dgvStudentSearchClassList.Rows.Add(newRow);
            }

            dgvStudentSearchClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudentSearchClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvStudentSearchClassList.AllowUserToAddRows = false;
            //CreateComboBoxWithEnums();

            if (dgvStudentSearchClassList.Rows.Count > 0)
                dgvStudentSearchClassList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentSearchClassList.Rows.Count; i++)
                dgvStudentSearchClassList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentSearchClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //dgvStudentSearchClassList.Columns[0].Width = 20;
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
            if (lblCurrentPage.Text.IndexOf("全班") == -1)
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
                    //btnStudentByStudentInClass.Enabled = true;
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentSearchShowStudentInClassList.ReadOnly = false;
                //btnStudentByStudentInClass.Enabled = false;
                dgvStudentSearchShowStudentInClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvStudentSearchShowStudentInClassList.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvStudentSearchShowStudentInClassList.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvStudentSearchShowStudentInClassList.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvStudentSearchShowStudentInClassList.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvStudentSearchShowStudentInClassList.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvStudentSearchShowStudentInClassList.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvStudentSearchShowStudentInClassList.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

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
                //btnStudentSearchShowStudent.Enabled = false;
                selectItem++;
            }

            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvStudentSearchClassList.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvStudentSearchClassList.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvStudentSearchClassList.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvStudentSearchClassList.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvStudentSearchClassList.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvStudentSearchClassList.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvStudentSearchClassList.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

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
                    //if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                    if (dgvRow.Selected)
                        selectIndex = countIndex;

                    countIndex++;
                }

                if (classSets != null && classSets.Count > 0)
                {
                    classData = null;
                    classData = classSets.ElementAt(selectIndex);
                }
                else if (staffSets != null && staffSets.Count > 0)
                {
                    staffData = null;
                    staffData = staffSets.ElementAt(selectIndex);
                }

                if (classData != null && classData.ID != null)
                {

                    if (lblCurrentPage.Text.IndexOf("08") > -1) //Show Class Info
                    {
                        panelStudentSearchShowClassList.Visible = false;
                        btnStudentSearchReturnClassListFromClassInfo.Visible = true;
                        ShowClassInfoAfterSearch();
                    }
                    else
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

                        if (lblCurrentPage.Text.IndexOf("退費") > -1)
                        {
                            if (lblCurrentPage.Text.IndexOf("記錄") > -1)
                                recordSets = (List<RecordDefinition>)SelectStudentSearchRecord("FromClass", "Class", classData.ID);
                            else if (lblCurrentPage.Text.IndexOf("全班") > -1)
                                studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundinstudentlist", (object)"ClassID", (object)classData.ID);
                        }
                        else if (lblCurrentPage.Text.IndexOf("04") > -1)
                            recordSets = (List<RecordDefinition>)SelectStudentSearchRecord("FromClass", "Class", classData.ID);
                        else if (lblCurrentPage.Text.IndexOf("11") > -1)
                            recordSets = (List<RecordDefinition>)SelectStudentSearchRecord("FromClass", "Class", classData.ID);

                        if (lblCurrentPage.Text.IndexOf("04") > -1 ||
                            (lblCurrentPage.Text.IndexOf("退費") > -1 && lblCurrentPage.Text.IndexOf("記錄") > -1) ||
                            lblCurrentPage.Text.IndexOf("11") > -1)
                        {
                            if (recordSets.Count > 0)
                            {
                                btnStudentSearchReturnClassList.Visible = false;
                                panelStudentSearchStudentInClass.Visible = false;

                                panelStudentRecordList.Visible = true;
                                panelStudentRecordInfo.Visible = true;

                                lblStudentRecordShowInfoID.Text = classData.ID;
                                lblStudentRecordShowInfoName.Text = classData.Name;

                                panelStudentRecordList.Visible = true;
                                btnStudentRecordReturnClassList.Visible = true;
                                SearchStudentRecordShowRecordList();
                            }
                            else
                            {
                                MessageBox.Show("此班級無相關記錄!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                StudentSearchReturnClassList();
                            }
                        }
                        else if (studentSets != null)
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
                else if (lblCurrentPage.Text.IndexOf("13") > -1) //Show Class Info
                {
                    panelStudentSearchShowClassList.Visible = false;
                    btnStudentSearchReturnClassListFromClassInfo.Visible = true;
                    ShowStaffInfoAfterSearch();
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
            if (lblCurrentPage.Text.IndexOf("04") > -1 || (lblCurrentPage.Text.IndexOf("退費") > -1 && lblCurrentPage.Text.IndexOf("記錄") > -1))
            {
            }
            else if (lblCurrentPage.Text.IndexOf("13") > -1)
            {
                staffSets = (List<StaffDefinition>)facade.FacadeFunctions("select", "staffbyname", (object)lblStudentSearchInfo.Text, null);
                SearchStaffShowStaffList();
            }
            else
            {
                classSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "class", (object)"Name", (object)lblStudentSearchInfo.Text);
                SearchStudentShowClassList();
            }

            btnStudentSearchReturnClassList.Visible = false;
            panelStudentSearchStudentInClass.Visible = false;
            panelStudentSearchStudentInfo.Visible = false;
            panelStudentRecordList.Visible = false;
            panelStudentSearchShowClassList.Visible = true;
        }

        private void btnStudentByStudentID_Click(object sender, EventArgs e)
        {
            AfterStudentSearch(lblStudentSearchShowStudentID.Text, lblStudentSearchShowStudentName.Text);
        }

        private void btnStudentByStudentInClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (lblCurrentPage.Text.IndexOf("全班") == -1)
                {
                    int selectIndex = -1;
                    int countIndex = 0;

                    foreach (DataGridViewRow dgvRow in this.dgvStudentSearchShowStudentInClassList.Rows)
                    {
                        //if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                        if (dgvRow.Selected)
                            selectIndex = countIndex;

                        countIndex++;
                    }

                    studentData = null;
                    studentData = studentSets.ElementAt(selectIndex);

                    AfterStudentSearch(studentData.ID, studentData.Name);
                }
                else if (lblCurrentPage.Text.IndexOf("全班") != -1)
                    AfterStudentSearch(classData.ID, classData.Name);
                else if (lblCurrentPage.Text.IndexOf("員工") == -1)
                {
                    int selectIndex = -1;
                    int countIndex = 0;

                    foreach (DataGridViewRow dgvRow in this.dgvStudentSearchShowStudentInClassList.Rows)
                    {
                        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                            selectIndex = countIndex;

                        countIndex++;
                    }

                    staffData = null;
                    staffData = staffSets.ElementAt(selectIndex);

                    AfterStudentSearch(staffData.ID, staffData.Name);
                }
            }
            catch
            {
            }
        }

        public void AfterStudentSearch(string dataID, string dataName)
        {
            btnReturnSearchPage.Visible = true;

            if ((lblCurrentPage.Text.IndexOf("01") > -1 || lblCurrentPage.Text.IndexOf("02") > -1) && lblCurrentPage.Text.IndexOf("加選") > -1)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelSubButtons.Visible = true;
                panelStudentManageClass.Visible = true;
                panelMainMenuScreen.Visible = false;
                //btnStudentManageClassReturnSearchPage.Visible = true;

                if (lblCurrentPage.Text.IndexOf("加選") > -1)
                {
                    if (lblCurrentPage.Text.IndexOf("全班") > -1)
                        classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)"ID", (object)dataID);

                    List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classbyenddate", "All", "");
                    if (tempClassSets != null && tempClassSets.Count > 0)
                        StudentManageClassShowClassList(tempClassSets);
                }

                //Student Manage Class
                if (lblCurrentPage.Text.IndexOf("01") > -1 || lblCurrentPage.Text.IndexOf("個別") > -1)
                {
                    //btnStudentAddNewClassSearchStudent.Text = "學生查詢";
                    lblStudentManageClassStudentID.Text = "學生編號:";
                    lblStudentManageClassStudentName.Text = "學生姓名:";
                    lblStudentManageClassShowStudentID.Text = dataID;
                    lblStudentManageClassShowStudentName.Text = dataName;

                    panelStudentManageClassByPersonShowClassInfo.Visible = true;
                    panelStudentManageClassByClassShowClassInfo.Visible = false;

                    ShowStudentInClassAmount();
                }
                else
                {
                    //btnStudentAddNewClassSearchStudent.Text = "班級查詢";

                    lblStudentManageClassStudentID.Text = "班級編號:";
                    lblStudentManageClassStudentName.Text = "班級名稱:";
                    lblStudentManageClassShowStudentID.Text = dataID;
                    lblStudentManageClassShowStudentName.Text = dataName;

                    lblStudentManageClassShowClassID.Text = dataID;
                    lblStudentManageClassShowClassName.Text = dataName;

                    studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)dataID);

                    lblStudentManageClassShowClassSeats.Text = "共" + studentSets.Count + "個學生";
                    lblStudentManageClassShowSelectNumber.Text = "目前共選擇" + studentSets.Count + "個學生";

                    panelStudentManageClassByPersonShowClassInfo.Visible = false;
                    panelStudentManageClassByClassShowClassInfo.Visible = true;

                    ShowStudentListByClass();
                }

                if (lblCurrentPage.Text.IndexOf("刪除") > -1)
                {
                    panelStudentManageClassAddNewClassAddClass.Visible = false;
                    btnStudentManageClassByPersonRemoveClass.Visible = true;
                    btnStudentManageClassByClassRemoveClass.Visible = true;
                    lblStudentManageClassTitle.Text = "課程刪除";
                }
                else
                {
                    panelStudentManageClassAddNewClassAddClass.Visible = true;
                    btnStudentManageClassByPersonRemoveClass.Visible = false;
                    btnStudentManageClassByClassRemoveClass.Visible = false;
                    lblStudentManageClassTitle.Text = "課程加選";

                    foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassByClassStudentList.Rows)
                        dgvRow.Selected = true;
                }
            }

            //Student Payment
            else if ((lblCurrentPage.Text.IndexOf("01") > -1 && lblCurrentPage.Text.IndexOf("付費") > -1) || (lblCurrentPage.Text.IndexOf("02") > -1 && lblCurrentPage.Text.IndexOf("收費") > -1) || lblCurrentPage.Text.IndexOf("03") > -1)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelSubButtons.Visible = true;
                panelStudentPayment.Visible = true;
                panelMainMenuScreen.Visible = false;
                //btnStudentManageClassReturnSearchPage.Visible = true;

                if (lblCurrentPage.Text.IndexOf("01") == -1 && lblCurrentPage.Text.IndexOf("02") == -1)
                    btnStudentPaymentReturnStudentSearchPage.Visible = false;

                if (lblCurrentPage.Text.IndexOf("01") > -1 || lblCurrentPage.Text.IndexOf("個別") > -1)
                {
                    lblStudentPaymentStudentID.Text = "學生編號:";
                    lblStudentPaymentStudentName.Text = "學生姓名:";
                    lblStudentPaymentShowStudentID.Text = dataID;
                    lblStudentPaymentShowStudentName.Text = dataName;

                    panelStudentPaymentManagementPage.Visible = true;
                    panelStudentPaymentByClassPaymentPage.Visible = false;

                    ShowsStudentNeedToPayAmount();
                }
                else
                {
                    lblStudentPaymentStudentID.Text = "班級編號:";
                    lblStudentPaymentStudentName.Text = "班級名稱:";
                    lblStudentPaymentShowStudentID.Text = dataID;
                    lblStudentPaymentShowStudentName.Text = dataName;

                    panelStudentPaymentManagementPage.Visible = false;
                    panelStudentPaymentByClassPaymentPage.Visible = true;

                    ShowsClassNeedToPayStudentList();
                }
            }

            //Student Refund
            else if (lblCurrentPage.Text.IndexOf("05") > -1)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelSubButtons.Visible = true;
                panelStudentRefund.Visible = true;
                panelMainMenuScreen.Visible = false;
                //btnStudentManageClassReturnSearchPage.Visible = true;

                if (lblCurrentPage.Text.IndexOf("個別") > -1)
                {
                    lblStudnetRefundInfo.Text = "學生退費";
                    lblStudentRefundStudentID.Text = "學生編號:";
                    lblStudentRefundStudentName.Text = "學生姓名:";
                    lblStudentRefundShowStudentID.Text = dataID;
                    lblStudentRefundShowStudentName.Text = dataName;

                    panelStudentRefundByPerson.Visible = true;
                    panelStudentRefundByClass.Visible = false;

                    ShowsStudentRefundClassAmount();
                }
                else if (lblCurrentPage.Text.IndexOf("全班") > -1)
                {
                    lblStudnetRefundInfo.Text = "班級退費";
                    lblStudentRefundStudentID.Text = "班級編號:";
                    lblStudentRefundStudentName.Text = "班級名稱:";
                    lblStudentRefundShowStudentID.Text = dataID;
                    lblStudentRefundShowStudentName.Text = dataName;

                    panelStudentRefundByPerson.Visible = false;
                    panelStudentRefundByClass.Visible = true;

                    ShowsStudentRefundStudnetAmount();
                }
                else
                {
                    lblStudnetRefundInfo.Text = "班級退費";
                    lblStudentRefundStudentID.Text = "班級編號:";
                    lblStudentRefundStudentName.Text = "班級名稱:";
                    lblStudentRefundShowStudentID.Text = dataID;
                    lblStudentRefundShowStudentName.Text = dataName;

                    panelStudentRefundByPerson.Visible = false;
                    panelStudentRefundByClass.Visible = true;

                    ShowsStudentRefundStudnetAmount();
                }
            }

            //Student Manage Data
            else if (lblCurrentPage.Text.IndexOf("07") > -1)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelSubButtons.Visible = true;
                panelInsertScreen.Visible = true;
                panelInsertStudent.Visible = true;
                panelMainMenuScreen.Visible = false;

                if (btnStudentSearch.Text == "搜 尋")
                {
                    lblInvisibleStudentDataStatus.Text = "Update";
                    btnInsertStudentReturnStudentManagement.Visible = true;
                    lblInsertStudentTitle.Text = "修改學生";
                    btnInsertNewStudent.Text = "修 改";

                    LoadStudentDataByUpdating();
                }
                else if (btnStudentSearch.Text == "新 增")
                {
                    lblInvisibleStudentDataStatus.Text = "Insert";
                    btnInsertStudentReturnStudentManagement.Visible = true;
                    lblInsertStudentTitle.Text = "新增學生";
                    btnInsertNewStudent.Text = "新 增";
                }
            }

            //Class Manage Data
            else if (lblCurrentPage.Text.IndexOf("08") > -1)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelSubButtons.Visible = true;
                panelInsertScreen.Visible = true;
                panelInsertClass.Visible = true;
                panelMainMenuScreen.Visible = false;

                btnRemoveNewClassTime.Enabled = false;

                if (btnStudentSearch.Text == "搜 尋")
                {
                    lblInvisibleClassDataStatus.Text = "Update";
                    lblInsertClassTitle.Text = "修改課程";
                    btnInsertNewClass.Text = "修 改";
                    btnDeleteExistClass.Visible = true;

                    ShowClassDataForUpdating();
                }
                else if (btnStudentSearch.Text == "新 增")
                {
                    lblInvisibleClassDataStatus.Text = "Insert";
                    lblInsertClassTitle.Text = "新增課程";
                    btnInsertNewClass.Text = "新 增";
                    btnDeleteExistClass.Visible = false;
                }
            }

            //Class Manage Data
            else if (lblCurrentPage.Text.IndexOf("13") > -1)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelSubButtons.Visible = true;
                panelInsertScreen.Visible = true;
                panelInsertStaff.Visible = true;
                panelMainMenuScreen.Visible = false;

                if (btnStudentSearch.Text == "搜 尋")
                {
                    lblInvisibleStaffDataStatus.Text = "Update";
                    lblInsertStaff.Text = "修改員工";
                    btnInsertStaff.Text = "修 改";
                    btnInsertStaffDelete.Visible = true;

                    ShowStaffDataForUpdating();
                }
                else if (btnStudentSearch.Text == "新 增")
                {
                    lblInvisibleStaffDataStatus.Text = "Insert";
                    lblInsertStaff.Text = "新增課程";
                    btnInsertStaff.Text = "新 增";
                    btnInsertStaffDelete.Visible = false;
                }
            }

            SettingCurrentPage(lblCurrentPage.Text);
            ////Record List
            //else if (lblCurrentPage.Text.IndexOf("記錄") > -1)
            //{
            //    DefaultSetting();

            //    //panelTopScreen.Visible = true;
            //    //panelSideFunctions.Visible = true;
            //    panelRecordChecker.Visible = true;
            //    panelMainMenuScreen.Visible = false;
            //}
        }

        private void ReturnToStudentSearch()
        {
            DefaultSetting();

            //panelTopScreen.Visible = true;
            //panelSideFunctions.Visible = true;
            panelSubButtons.Visible = true;
            //panelSearchStudentScreen.Visible = true;
            panelMainMenuScreen.Visible = false;

            btnReturnSearchPage.Visible = false;

            if (lblCurrentPage.Text.IndexOf("記錄") > -1 || lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("06") > -1)
            {
                panelStudentRecordList.Visible = true;

                //foreach (DataGridViewRow dgvRow in this.dgvStudentRecordData.Rows)
                //    dgvRow.Cells[0].Value = false;

                btnStudentRecordSelectAll.Enabled = true;
                btnShowStudentRecordData.Enabled = false;
                btnStudentRecordUnselectAll.Enabled = false;
            }
            else if (studentSets != null && studentSets.Count > 0)
            {
                SearchStudentShowStudentList();
                panelStudentSearchStudentInClass.Visible = true;

                if (classSets != null && classSets.Count > 0)
                {
                    btnStudentSearchReturnClassList.Visible = true;
                    SearchStudentShowClassList();
                }
            }
            else if (studentData != null && studentData.ID != null)
                panelStudentSearchStudentInfo.Visible = true;
            else if (lblCurrentPage.Text.IndexOf("08") > -1)
            {
                if (classSets != null && classSets.Count > 0)
                {
                    panelStudentSearchShowClassList.Visible = true;
                    btnStudentSearchReturnClassList.Visible = true;
                    SearchStudentShowClassList();
                }
            }
            else if (lblCurrentPage.Text.IndexOf("13") > -1)
            {
                if (staffSets != null && staffSets.Count > 0)
                {
                    panelStudentSearchShowClassList.Visible = true;
                    SearchStaffShowStaffList();
                }
                else if (staffData != null && staffData.ID != "")
                {
                    panelStudentSearchStudentInfo.Visible = true;
                    ShowStaffInfoAfterSearch();
                }
            }
        }

        //Search Record
        private void cboStudentSearchRecord_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudentSearchDefault();

            if (cboStudentSearchRecord.SelectedIndex > -1)
            {
                if (lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("06") > -1)
                    panelStudentSearchRecordByDate.Visible = false;
                else
                    panelStudentSearchRecordByDate.Visible = true;

                cboStudentSearchRecordSearchBy.Items.Clear();
                cboStudentSearchRecordSearchBy.Items.Add("學生編號");
                cboStudentSearchRecordSearchBy.Items.Add("學生姓名");
                cboStudentSearchRecordSearchBy.Items.Add("班級編號");
                cboStudentSearchRecordSearchBy.Items.Add("班級名稱");

                cboStudentSearchRecordSearchBy.Visible = true;
                cboStudentSearchRecordSearchBy.SelectedIndex = -1;
                btnStudentSearchRecord.Enabled = true;

                dtpStudentSearchRecordFromDate.Checked = false;
                dtpStudentSearchRecordEndDate.Checked = false;

                if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") > -1)
                {
                    cboStudentSearchRecordSearchBy.Items.RemoveAt(1);
                    cboStudentSearchRecordSearchBy.Items.RemoveAt(0);
                }
            }
        }

        private void cboStudentSearchRecordSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboStudentSearchRecordSearchBy.SelectedIndex > -1)
            {
                SetStudentSearchPanelDefault();

                btnStudentSearchRecord.Enabled = true;
                txtStudentSearchRecord.Visible = true;
                txtStudentSearchRecord.Text = "";

                //if (lblCurrentPage.Text.IndexOf("記錄") > -1 || lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("06") > -1)
                if (lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("06") > -1)
                {
                    if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("編號") > -1)
                        panelStudentSearchRecordContinueNumber.Visible = true;
                    else
                        panelStudentSearchRecordContinueNumber.Visible = false;
                }
            }
        }

        private void btnStudentSearchRecord_Click(object sender, EventArgs e)
        {
            string selectBy = null, selectData = null;
            string continueNumber = null;
            bool isContinueNumberOK = true;
            object catchObject = null;
            classSets = null;
            panelStudentRecordInfo.Visible = false;
            btnStudentRecordReturnClassList.Visible = false;

            if (dgvStudentRecordData.Columns.Count > 0)
                dgvStudentRecordData.Columns.Clear();

            SetStudentSearchPanelDefault();
            recordSets = new List<RecordDefinition>();

            btnStudentRecordReturnClassList.Visible = false;
            lblStudentSearchRecordShowFromDate.Text = "";
            lblStudentSearchRecordShowEndDate.Text = "";

            //if (lblCurrentPage.Text.IndexOf("04") > -1)
            //    studentPaymentSets = new List<StudentPaymentDefinition>();
            //else if (lblCurrentPage.Text.IndexOf("退費") > -1)
            //    classRefundSets = new List<ClassRefundDefinition>();

            if (cboStudentSearchRecordSearchBy.SelectedIndex > -1)
                selectBy = cboStudentSearchRecordSearchBy.SelectedItem.ToString();

            selectBy = CheckStudentSearchItem(selectBy, txtStudentSearchRecord.Text.Trim(), "查詢條件: ");
            selectData = txtStudentSearchRecord.Text.Trim();
            lblStudentSearchInfo.Text = txtStudentSearchByText.Text.Trim();

            if (txtStudentSearchRecordEndContinueNumber.Text.Trim() != "")
            {
                selectBy = CheckStudentSearchItem(cboStudentSearchRecordSearchBy.SelectedItem.ToString(), txtStudentSearchRecordEndContinueNumber.Text.Trim(), "連號條件: ");
                continueNumber = txtStudentSearchRecordEndContinueNumber.Text.Trim();

                if (selectBy != null)
                    isContinueNumberOK = CheckContinueNumber();
            }

            if (selectBy != null && isContinueNumberOK)
            {
                if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
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
                                catchObject = SelectStudentSearchRecord("Student", selectBy, i.ToString());

                                if (catchObject != null)
                                {
                                    foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                        recordSets.Add(recordSingle);

                                    //if (lblCurrentPage.Text.IndexOf("04") > -1)
                                    //{
                                    //    foreach (var studentPaymentSingle in (List<StudentPaymentDefinition>)catchObject)
                                    //        studentPaymentSets.Add(studentPaymentSingle);
                                    //}
                                    //else if (lblCurrentPage.Text.IndexOf("退費") > -1)
                                    //{
                                    //    foreach (var classRefundSingle in (List<ClassRefundDefinition>)catchObject)
                                    //        classRefundSets.Add(classRefundSingle);
                                    //}
                                }
                            }
                        }
                        else
                        {
                            //if (lblCurrentPage.Text.IndexOf("退費") > -1)
                            //classRefundSets = (List<ClassRefundDefinition>)SelectStudentSearchRecord("Student", selectBy, selectData);
                            catchObject = SelectStudentSearchRecord("Student", selectBy, selectData);
                        }
                    }
                    else if (selectBy == "Name")
                    {
                        studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "student", (object)selectBy, (object)selectData);

                        foreach (var studentSingle in studentSets)
                        {
                            catchObject = SelectStudentSearchRecord("Student", "ID", studentSingle.ID);

                            if (catchObject != null)
                            {
                                foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                    recordSets.Add(recordSingle);

                                //if (lblCurrentPage.Text.IndexOf("04") > -1)
                                //{
                                //    foreach (var studentPaymentSingle in (List<StudentPaymentDefinition>)catchObject)
                                //        studentPaymentSets.Add(studentPaymentSingle);
                                //}
                                //else if (lblCurrentPage.Text.IndexOf("退費") > -1)
                                //{
                                //    foreach (var classRefundSingle in (List<ClassRefundDefinition>)catchObject)
                                //        classRefundSets.Add(classRefundSingle);
                                //}
                            }
                        }
                    }
                    else
                    {
                        //if (lblCurrentPage.Text.IndexOf("退費") > -1)
                        //    classRefundSets = (List<ClassRefundDefinition>)SelectStudentSearchRecord("Student", selectBy, selectData);
                        catchObject = SelectStudentSearchRecord("Student", selectBy, selectData);
                    }
                }
                else if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
                {
                    if (selectBy == "ID" && continueNumber != null)
                    {
                        int firstID = int.Parse(selectData.Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));
                        int lastID = int.Parse(selectData.Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));

                        if (txtStudentSearchRecordEndContinueNumber.Text.Trim() != "")
                            lastID = int.Parse(txtStudentSearchRecordEndContinueNumber.Text.Trim().Substring(int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1));

                        string idLetter = selectData.Substring(0, int.Parse(lblStudentSearchRecordClassIDLastLetterIndex.Text) + 1);
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
                                catchObject = SelectStudentSearchRecord("Class", selectBy, i.ToString());

                                foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                    recordSets.Add(recordSingle);
                                //if (catchObject != null)
                                //{
                                //    if (lblCurrentPage.Text.IndexOf("04") > -1)
                                //    {
                                //        foreach (var studentPaymentSingle in (List<StudentPaymentDefinition>)catchObject)
                                //            studentPaymentSets.Add(studentPaymentSingle);
                                //    }
                                //    else if (lblCurrentPage.Text.IndexOf("退費") > -1)
                                //    {
                                //        foreach (var classRefundSingle in (List<ClassRefundDefinition>)catchObject)
                                //            classRefundSets.Add(classRefundSingle);
                                //    }
                                //}
                            }
                        }
                        else
                        {
                            catchObject = SelectStudentSearchRecord("Class", selectBy, selectData);
                            //if (lblCurrentPage.Text.IndexOf("退費") > -1)
                            //    classRefundSets = (List<ClassRefundDefinition>)SelectStudentSearchRecord("Class", selectBy, selectData);
                        }
                    }
                    else if (selectBy == "Name")
                    {
                        List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classall", (object)selectBy, (object)selectData);
                        //classSets = new List<ClassDefinition>();

                        foreach (var classSingle in tempClassSets)
                        {
                            catchObject = SelectStudentSearchRecord("Class", "ID", classSingle.ID);

                            if (catchObject != null)
                            {

                                foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                    recordSets.Add(recordSingle);

                                //if (lblCurrentPage.Text.IndexOf("04") > -1)
                                //{
                                //    //foreach (var studentPaymentSingle in (List<StudentPaymentDefinition>)catchObject)
                                //    //{
                                //    //    studentPaymentSets.Add(studentPaymentSingle);
                                //    //    //classSets.Add(classSingle);
                                //    //}
                                //}
                                //else if (lblCurrentPage.Text.IndexOf("退費") > -1)
                                //{
                                //    //foreach (var classRefundSingle in (List<ClassRefundDefinition>)catchObject)
                                //    foreach (var recordSingle in (List<RecordDefinition>)catchObject)
                                //    {
                                //        recordSets.Add(recordSingle);
                                //        //classSets.Add(classSingle);
                                //    }
                                //}
                            }
                        }
                    }
                    else
                    {
                        catchObject = SelectStudentSearchRecord("Class", selectBy, selectData);
                        //if (lblCurrentPage.Text.IndexOf("退費") > -1)
                        //    classRefundSets = (List<ClassRefundDefinition>)SelectStudentSearchRecord("Class", selectBy, selectData);
                    }
                }

                if ((recordSets == null || recordSets.Count == 0) && catchObject != null)
                    recordSets = (List<RecordDefinition>)catchObject;

                //if (lblCurrentPage.Text.IndexOf("04") > -1)
                //{
                //    if ((studentPaymentSets == null || studentPaymentSets.Count == 0) && catchObject != null)
                //        studentPaymentSets = (List<StudentPaymentDefinition>)catchObject;
                //}
                //else if (lblCurrentPage.Text.IndexOf("退費") > -1)
                //{
                //    if ((recordSets == null || recordSets.Count == 0) && catchObject != null)
                //        recordSets = (List<RecordDefinition>)catchObject;
                //}
            }
            else
                recordSets = (List<RecordDefinition>)SelectStudentSearchRecord("", "", "");

            //Check Search Result
            bool notNull = false;

            if (recordSets != null && recordSets.Count > 0)
            {
                panelStudentRecordList.Visible = true;

                if (cboStudentSearchRecordSearchBy.SelectedIndex > -1)
                {
                    if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                    {
                        SearchStudentRecordShowRecordList();
                        notNull = true;
                    }
                    else
                    {
                        if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") == -1)
                        {
                            if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("編號") > -1)
                            {
                                //panelStudentRecordList.Visible = true;
                                panelStudentRecordInfo.Visible = true;

                                lblStudentRecordShowInfoID.Text = recordSets.ElementAt(0).Data2ID;
                                lblStudentRecordShowInfoName.Text = recordSets.ElementAt(0).Data2Name;

                                List<RecordDefinition> tempRecord = recordSets;
                                recordSets = new List<RecordDefinition>();

                                foreach (var recordSingle in tempRecord)
                                    catchObject = SelectStudentSearchRecord("FromClass", "Class", selectData);

                                recordSets = (List<RecordDefinition>)catchObject;

                                if (recordSets != null && recordSets.Count > 0)
                                {
                                    notNull = true;
                                    SearchStudentRecordShowRecordList();
                                }
                            }
                            else
                            {
                                List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classall", (object)selectBy, (object)selectData);
                                classSets = new List<ClassDefinition>();

                                if (lblCurrentPage.Text.IndexOf("明細") > -1)
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
                            SearchStudentRecordShowRecordList();
                            notNull = true;
                        }
                    }
                }
                else
                {
                    SearchStudentRecordShowRecordList();
                    notNull = true;
                }
            }
            else if (recordSets == null)
                notNull = true;
            //if (lblCurrentPage.Text.IndexOf("04") > -1)
            //{
            //    if (studentPaymentSets != null && studentPaymentSets.Count > 0)
            //    {
            //        if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
            //        {
            //            panelStudentRecordList.Visible = true;
            //            SearchStudentRecordShowRecordList();
            //            notNull = true;
            //        }
            //        else if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") == -1 && cboStudentSearchRecordSearchBy.SelectedItem.ToString() == "班級名稱")
            //        {
            //            List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classall", (object)selectBy, (object)selectData);
            //            classSets = new List<ClassDefinition>();

            //            foreach (var classSingle in tempClassSets)
            //            {
            //                foreach (var studentPaymentSingle in studentPaymentSets)
            //                {
            //                    if (classSingle.ID == studentPaymentSingle.ClassID)
            //                    {
            //                        classSets.Add(classSingle);
            //                        notNull = true;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    else if (studentPaymentSets == null)
            //        notNull = true;
            //}
            //else if (lblCurrentPage.Text.IndexOf("退費") > -1)
            //{
            //    if (classRefundSets != null && classRefundSets.Count > 0)
            //    {
            //        if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
            //        {
            //            panelStudentRecordList.Visible = true;
            //            SearchStudentRecordShowRecordList();
            //            notNull = true;
            //        }
            //        else if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") == -1 && cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
            //        {
            //            List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classall", (object)selectBy, (object)selectData);
            //            classSets = new List<ClassDefinition>();

            //            foreach (var classSingle in tempClassSets)
            //            {
            //                foreach (var classRefundSingle in classRefundSets)
            //                {
            //                    if (classSingle.ID == classRefundSingle.StudentID)
            //                        classSets.Add(classSingle);
            //                }
            //            }
            //        }
            //    }
            //    else if (classRefundSets == null)
            //        notNull = true;
            //}

            if (!notNull)
            {
                if (classSets != null && classSets.Count > 0)
                    MessageBox.Show("此課程無相關記錄!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("查無此指定資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (lblCurrentPage.Text.IndexOf("12") == -1)
                {
                    if (classSets != null && classSets.Count > 0)
                    {
                        var classSet = classSets.Distinct();
                        classSets = classSet.ToList();
                        panelStudentRecordList.Visible = false;
                        btnStudentRecordReturnClassList.Visible = true;
                        panelStudentSearchShowClassList.Visible = true;

                        SearchStudentShowClassList();
                    }
                }
            }
        }

        private bool CheckContinueNumber()
        {
            bool isTrue = false;
            facade = new FacadeLayer(SystemTypeForDB);

            if (txtStudentSearchRecordEndContinueNumber.Text.Trim() != "")
            {
                if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                {
                    //if (txtStudentSearchRecordEndContinueNumber.Text.Trim().Length == 8)
                    //{
                        if (int.Parse(txtStudentSearchRecordEndContinueNumber.Text.Trim()) != 0)
                            isTrue = true;
                        else
                            MessageBox.Show("查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //    MessageBox.Show("學生編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("班級") > -1)
                {
                    if (txtStudentSearchRecordEndContinueNumber.Text.Trim().Length > 0)
                    {
                        if (txtStudentSearchRecordEndContinueNumber.Text.Trim().Length <= 7)
                        {
                            if (txtStudentSearchRecordEndContinueNumber.Text.Trim().Length == txtStudentSearchRecord.Text.Trim().Length)
                            {
                                bool isFound = false;
                                int lastLetterIndex = -1;
                                for (int i = txtStudentSearchRecordEndContinueNumber.Text.Trim().Length - 1; i >= 0; i--)
                                {
                                    bool isNumber = (bool)facade.FacadeFunctions("check", "number", (object)txtStudentSearchRecord.Text.Trim().Substring(i, 1), null);

                                    if (!isNumber && !isFound)
                                    {
                                        lastLetterIndex = i;
                                        isFound = true;
                                    }
                                }

                                string temp = txtStudentSearchRecordEndContinueNumber.Text.Trim().Substring(0, lastLetterIndex + 1);
                                string temp1 = txtStudentSearchRecord.Text.Trim().Substring(0, lastLetterIndex + 1);

                                if (txtStudentSearchRecordEndContinueNumber.Text.Trim().Substring(0, lastLetterIndex + 1) == txtStudentSearchRecord.Text.Trim().Substring(0, lastLetterIndex + 1))
                                {
                                    lblStudentSearchRecordClassIDLastLetterIndex.Text = lastLetterIndex.ToString();
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

            if (dtpStudentSearchRecordFromDate.Checked || dtpStudentSearchRecordEndDate.Checked)
            {
                if (dtpStudentSearchRecordFromDate.Checked && dtpStudentSearchRecordEndDate.Checked)
                    isOK = true;
                else
                    MessageBox.Show("日期條件: 起始與結束日期都要選擇!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                isOK = true;

            return isOK;
        }

        private object SelectStudentSearchRecord(string select, string selectBy, string selectData)
        {
            facade = new FacadeLayer(SystemTypeForDB);
            string fromDate = null, endDate = null;
            string[] dateSelect = new string[3];
            //List<StudentDefinition> tempStudentSets = new List<StudentDefinition>();
            List<StudentPaymentDefinition> tempStudentPaymentSets = new List<StudentPaymentDefinition>();
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();
            object returnObject = null;
            panelStudentRecordDateInfo.Visible = false;

            if (CheckSearchDate())
            {
                if (dtpStudentSearchRecordFromDate.Checked && dtpStudentSearchRecordEndDate.Checked)
                {
                    fromDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpStudentSearchRecordFromDate.Value, null).ToString();
                    endDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpStudentSearchRecordEndDate.Value, null).ToString();

                    lblStudentSearchRecordShowFromDate.Text = fromDate;
                    lblStudentSearchRecordShowEndDate.Text = endDate;

                    lblStudentRecordShowDateInfo.Text = "選擇日期從 " + fromDate + " 到 " + endDate;
                    panelStudentRecordDateInfo.Visible = true;
                }

                if (selectData != "" || fromDate != null)
                {
                    lblRecordCheckerSelectWay.Text = cboStudentSearchRecord.SelectedItem.ToString();

                    if (lblCurrentPage.Text.IndexOf("04") > -1)
                    {
                        if (select == "FromClass")
                            returnObject = facade.FacadeFunctions("select", "studentpaymentbyclassid", (object)selectData, null);
                        else
                            returnObject = facade.FacadeFunctions("select", "studentpaymenttotalforrecord", (object)(select + selectBy), selectData);
                        //returnObject = (object)tempStudentPaymentSets;
                    }
                    else if (lblCurrentPage.Text.IndexOf("05") > -1)
                    {
                        panelStudentRecordDateInfo.Visible = false;

                        if (selectData == "")
                        {
                            if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("個人") > -1)
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

                                //returnObject = (object)tempClassRefundSets;
                            }
                        }

                        returnObject = (object)tempRecordSets;
                    }
                    else if (lblCurrentPage.Text.IndexOf("11") > -1)
                    {
                        string selectType = null;

                        if (selectData != "")
                        {
                            if (cboStudentSearchRecordSearchBy.SelectedItem.ToString().IndexOf("學生") > -1)
                                selectType = "WithStudentID";
                            else
                                selectType = "WithClassID";

                            if (select == "FromClass")
                                selectType = "FromClass";

                            dateSelect[0] = selectData;
                            dateSelect[1] = fromDate;
                            dateSelect[2] = endDate;

                            if (lblCurrentPage.Text.IndexOf("明細") > -1)
                                returnObject = facade.FacadeFunctions("reusefunction", "studentprepaidhistorytotal", (object)selectType, (object)dateSelect);
                            else
                                returnObject = facade.FacadeFunctions("reusefunction", "studentpaymentrecord", (object)selectType, (object)dateSelect);
                        }
                        else
                        {

                            if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("個人") > -1)
                                selectType = "WithStudentID";
                            else
                                selectType = "WithClassID";

                            dateSelect[0] = selectData;
                            dateSelect[1] = fromDate;
                            dateSelect[2] = endDate;

                            string dates = fromDate + "," + endDate;

                            if (lblCurrentPage.Text.IndexOf("明細") > -1)
                                returnObject = facade.FacadeFunctions("reusefunction", "studentprepaidhistorytotal", (object)selectType, (object)dateSelect);
                            else
                                returnObject = facade.FacadeFunctions("select", "studentpaymentrecordtotalbydate", (object)selectType, (object)dates);
                        }
                    }
                    else if (lblCurrentPage.Text.IndexOf("12") > -1)
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
                    MessageBox.Show("請輸入查詢條件!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return returnObject;
        }

        private void SearchStudentRecordShowRecordList()
        {
            if (dgvStudentRecordData.Columns.Count > 0)
                dgvStudentRecordData.Columns.Clear();

            ShowStudentSearchRecord();

            dgvStudentRecordData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvStudentRecordData.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvStudentRecordData.AllowUserToAddRows = false;

            //if (lblCurrentPage.Text.IndexOf("12") == -1)
            //{
            //    if (dgvStudentRecordData.Columns.Count == 0 || dgvStudentRecordData.Columns[0].Name != "Check")
            //    {
            //        // Initialize and add a check box column.
            //        DataGridViewColumn column = new DataGridViewCheckBoxColumn();
            //        column.Name = "Check";
            //        dgvStudentRecordData.Columns.Insert(0, column);
            //        column.HeaderCell.Value = string.Empty;
            //    }
            //    CreateComboBoxWithEnums();
            //}

            if (dgvStudentRecordData.Rows.Count > 0)
                dgvStudentRecordData.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvStudentRecordData.Rows.Count; i++)
                dgvStudentRecordData.Rows[i].Resizable = DataGridViewTriState.False;
            dgvStudentRecordData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            //if (lblCurrentPage.Text.IndexOf("12") == -1)
            //    dgvStudentRecordData.Columns[0].Width = 20;
            for (int i = 0; i < dgvStudentRecordData.Columns.Count; i++)
            {
                dgvStudentRecordData.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvStudentRecordData.Columns[i].Resizable = DataGridViewTriState.False;
                dgvStudentRecordData.ReadOnly = true;
            }

            btnStudentRecordUnselectAll.Enabled = false;
            btnShowStudentRecordData.Enabled = false;
            btnStudentRecordSelectAll.Enabled = true;
        }

        private void ShowStudentSearchRecord()
        {
            dgvStudentRecordData.DataSource = recordSets;

            btnStudentRecordUnselectAll.Visible = true;
            btnStudentRecordSelectAll.Visible = true;
            btnShowStudentRecordData.Visible = true;

            if (lblCurrentPage.Text.IndexOf("04") > -1)
            {
                //dgvStudentRecordData.DataSource = studentPaymentSets;

                //dgvStudentRecordData.Columns.Remove("RefundType");
                //dgvStudentRecordData.Columns.Remove("Receiver");
                ////dgvStudentRecordData.Columns.Remove("StaffName");
                //dgvStudentRecordData.Columns.Remove("StaffID");
                //dgvStudentRecordData.Columns.Remove("SubID");
                //dgvStudentRecordData.Columns.Remove("ID");

                //dgvStudentRecordData.Columns.Remove("Date");

                if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("個人") > -1)
                {
                    dgvStudentRecordData.Columns.Remove("Money2");
                    dgvStudentRecordData.Columns.Remove("Money1");
                    dgvStudentRecordData.Columns.Remove("Date2");
                    dgvStudentRecordData.Columns.Remove("Date1");
                    dgvStudentRecordData.Columns.Remove("Data2Name");
                    dgvStudentRecordData.Columns.Remove("Data2ID");

                    dgvStudentRecordData.Columns["Data1ID"].DisplayIndex = 0;
                    dgvStudentRecordData.Columns["Data1Name"].DisplayIndex = 1;
                    dgvStudentRecordData.Columns["Note1"].DisplayIndex = 2;
                    dgvStudentRecordData.Columns["Note2"].DisplayIndex = 3;
                    dgvStudentRecordData.Columns["Discount"].DisplayIndex = 4;

                    dgvStudentRecordData.Columns["Data1ID"].HeaderText = studentPaymentCountDataGridViewHeaderText[0];
                    dgvStudentRecordData.Columns["Data1Name"].HeaderText = studentPaymentCountDataGridViewHeaderText[1];
                    dgvStudentRecordData.Columns["Note1"].HeaderText = studentPaymentCountDataGridViewHeaderText[2];
                    dgvStudentRecordData.Columns["Note2"].HeaderText = studentPaymentCountDataGridViewHeaderText[3];
                    dgvStudentRecordData.Columns["Discount"].HeaderText = studentPaymentCountDataGridViewHeaderText[4];
                }
                else if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") > -1)
                {
                    dgvStudentRecordData.Columns.Remove("Date2");
                    dgvStudentRecordData.Columns.Remove("Date1");
                    dgvStudentRecordData.Columns.Remove("Data2Name");
                    dgvStudentRecordData.Columns.Remove("Data2ID");

                    dgvStudentRecordData.Columns["Data1ID"].DisplayIndex = 0;
                    dgvStudentRecordData.Columns["Data1Name"].DisplayIndex = 1;
                    dgvStudentRecordData.Columns["Money1"].DisplayIndex = 2;
                    dgvStudentRecordData.Columns["Money2"].DisplayIndex = 3;
                    dgvStudentRecordData.Columns["Note1"].DisplayIndex = 4;
                    dgvStudentRecordData.Columns["Note2"].DisplayIndex = 5;
                    dgvStudentRecordData.Columns["Discount"].DisplayIndex = 6;

                    dgvStudentRecordData.Columns["Data1ID"].HeaderText = classPaymentCountDataGridViewHeaderText[0];
                    dgvStudentRecordData.Columns["Data1Name"].HeaderText = classPaymentCountDataGridViewHeaderText[1];
                    dgvStudentRecordData.Columns["Money1"].HeaderText = classPaymentCountDataGridViewHeaderText[2];
                    dgvStudentRecordData.Columns["Money2"].HeaderText = classPaymentCountDataGridViewHeaderText[3];
                    dgvStudentRecordData.Columns["Note1"].HeaderText = classPaymentCountDataGridViewHeaderText[4];
                    dgvStudentRecordData.Columns["Note2"].HeaderText = classPaymentCountDataGridViewHeaderText[5];
                    dgvStudentRecordData.Columns["Discount"].HeaderText = classPaymentCountDataGridViewHeaderText[6];
                }
            }
            else if (lblCurrentPage.Text.IndexOf("05") > -1)
            {
                //dgvStudentRecordData.DataSource = classRefundSets;

                //dgvStudentRecordData.Columns.Remove("RefundType");
                //dgvStudentRecordData.Columns.Remove("Receiver");
                ////dgvStudentRecordData.Columns.Remove("StaffName");
                //dgvStudentRecordData.Columns.Remove("StaffID");
                //dgvStudentRecordData.Columns.Remove("SubID");
                //dgvStudentRecordData.Columns.Remove("ID");
                //dgvStudentRecordData.DataSource = recordSets;

                dgvStudentRecordData.Columns.Remove("Note2");
                dgvStudentRecordData.Columns.Remove("Note1");
                //dgvStudentRecordData.Columns.Remove("StaffName");
                //dgvStudentRecordData.Columns.Remove("StaffID");
                dgvStudentRecordData.Columns.Remove("Date2");
                dgvStudentRecordData.Columns.Remove("Money2");
                dgvStudentRecordData.Columns.Remove("Data1ID");

                //for (int i = 0; i < classRefundDataGridViewHeaderText.Length; i++)
                //    dgvStudentRecordData.Columns[i].HeaderText = classRefundDataGridViewHeaderText[i];

                dgvStudentRecordData.Columns["Data2ID"].DisplayIndex = 0;
                dgvStudentRecordData.Columns["Data2Name"].DisplayIndex = 1;
                dgvStudentRecordData.Columns["Date1"].DisplayIndex = 2;
                dgvStudentRecordData.Columns["Money1"].DisplayIndex = 3;
                dgvStudentRecordData.Columns["Discount"].DisplayIndex = 4;
                dgvStudentRecordData.Columns["Data1Name"].DisplayIndex = 5;

                dgvStudentRecordData.Columns["Data2ID"].HeaderText = classRefundDataGridViewHeaderText[0];
                dgvStudentRecordData.Columns["Data2Name"].HeaderText = classRefundDataGridViewHeaderText[1];
                dgvStudentRecordData.Columns["Date1"].HeaderText = classRefundDataGridViewHeaderText[2];
                dgvStudentRecordData.Columns["Money1"].HeaderText = classRefundDataGridViewHeaderText[3];
                dgvStudentRecordData.Columns["Discount"].HeaderText = classRefundDataGridViewHeaderText[4];
                dgvStudentRecordData.Columns["Data1Name"].HeaderText = classRefundDataGridViewHeaderText[5];
            }
            else if (lblCurrentPage.Text.IndexOf("11") > -1)
            {
                btnStudentRecordUnselectAll.Visible = false;
                btnStudentRecordSelectAll.Visible = false;

                if (lblCurrentPage.Text.IndexOf("明細") > -1)
                {
                    dgvStudentRecordData.Columns.Remove("Note2");
                    dgvStudentRecordData.Columns.Remove("Note1");
                    dgvStudentRecordData.Columns.Remove("Discount");
                    dgvStudentRecordData.Columns.Remove("Date2");
                    dgvStudentRecordData.Columns.Remove("Date1");
                    dgvStudentRecordData.Columns.Remove("Money2");
                    dgvStudentRecordData.Columns.Remove("Data1Name");
                    dgvStudentRecordData.Columns.Remove("Data1ID");

                    dgvStudentRecordData.Columns["Data2ID"].DisplayIndex = 0;
                    dgvStudentRecordData.Columns["Data2Name"].DisplayIndex = 1;
                    dgvStudentRecordData.Columns["Money1"].DisplayIndex = 2;

                    dgvStudentRecordData.Columns["Data2ID"].HeaderText = "學生編號";
                    dgvStudentRecordData.Columns["Data2Name"].HeaderText = "學生姓名";
                    dgvStudentRecordData.Columns["Money1"].HeaderText = "預收金額";
                }
                else
                {
                    dgvStudentRecordData.Columns.Remove("Discount");
                    dgvStudentRecordData.Columns.Remove("Date2");
                    dgvStudentRecordData.Columns.Remove("Date1");
                    dgvStudentRecordData.Columns.Remove("Money2");
                    dgvStudentRecordData.Columns.Remove("Money1");
                    dgvStudentRecordData.Columns.Remove("Data1Name");
                    dgvStudentRecordData.Columns.Remove("Data1ID");

                    //for (int i = 0; i < classRefundDataGridViewHeaderText.Length; i++)
                    //    dgvStudentRecordData.Columns[i].HeaderText = classRefundDataGridViewHeaderText[i];

                    dgvStudentRecordData.Columns["Data2ID"].DisplayIndex = 0;
                    dgvStudentRecordData.Columns["Data2Name"].DisplayIndex = 1;
                    dgvStudentRecordData.Columns["Note1"].DisplayIndex = 2;
                    dgvStudentRecordData.Columns["Note2"].DisplayIndex = 3;

                    if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("個人") > -1)
                    {
                        dgvStudentRecordData.Columns["Data2ID"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[0];
                        dgvStudentRecordData.Columns["Data2Name"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[1];
                        dgvStudentRecordData.Columns["Note1"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[2];
                        dgvStudentRecordData.Columns["Note2"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[3];
                    }
                    else
                    {
                        dgvStudentRecordData.Columns["Data2ID"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[0];
                        dgvStudentRecordData.Columns["Data2Name"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[1];
                        dgvStudentRecordData.Columns["Note1"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[2];
                        dgvStudentRecordData.Columns["Note2"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[3];
                    }
                }
            }
            else if (lblCurrentPage.Text.IndexOf("12") > -1)
            {
                btnStudentRecordUnselectAll.Visible = false;
                btnStudentRecordSelectAll.Visible = false;
                btnShowStudentRecordData.Visible = false;

                dgvStudentRecordData.Columns.Remove("Discount");
                dgvStudentRecordData.Columns.Remove("Date2");
                dgvStudentRecordData.Columns.Remove("Date1");
                dgvStudentRecordData.Columns.Remove("Money2");
                dgvStudentRecordData.Columns.Remove("Money1");
                dgvStudentRecordData.Columns.Remove("Data2Name");
                dgvStudentRecordData.Columns.Remove("Data2ID");

                //for (int i = 0; i < classRefundDataGridViewHeaderText.Length; i++)
                //    dgvStudentRecordData.Columns[i].HeaderText = classRefundDataGridViewHeaderText[i];

                dgvStudentRecordData.Columns["Data1ID"].DisplayIndex = 0;
                dgvStudentRecordData.Columns["Data1Name"].DisplayIndex = 1;
                dgvStudentRecordData.Columns["Note1"].DisplayIndex = 2;
                dgvStudentRecordData.Columns["Note2"].DisplayIndex = 3;

                dgvStudentRecordData.Columns["Data1ID"].HeaderText = studentInClassCountDataGridViewHeaderText[0];
                dgvStudentRecordData.Columns["Data1Name"].HeaderText = studentInClassCountDataGridViewHeaderText[1];
                dgvStudentRecordData.Columns["Note1"].HeaderText = studentInClassCountDataGridViewHeaderText[2];
                dgvStudentRecordData.Columns["Note2"].HeaderText = studentInClassCountDataGridViewHeaderText[3];
            }
        }

        private void dgvStudentRecordData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblCurrentPage.Text.IndexOf("12") == -1)
                dgvStudentRecordData_CellDoubleClick(sender, e);
        }

        private void dgvStudentRecordData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvStudentRecordData.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvStudentRecordData.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentRecordData.ReadOnly = false;
                dgvStudentRecordData.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            //int selectItem = 0;
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvStudentRecordData.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvStudentRecordData.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvStudentRecordData.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvStudentRecordData.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvStudentRecordData.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvStudentRecordData.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvStudentRecordData.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

            if (selectItem == 0)
            {
                btnStudentRecordUnselectAll.Enabled = false;
                btnShowStudentRecordData.Enabled = false;
                btnStudentRecordSelectAll.Enabled = true;
            }
            else if (selectItem > 0)
            {
                if (lblCurrentPage.Text.IndexOf("11") == -1)
                {
                    btnStudentRecordUnselectAll.Enabled = true;
                    btnShowStudentRecordData.Enabled = true;
                    btnStudentRecordSelectAll.Enabled = true;
                }
                else
                {
                    if (selectItem == 1)
                        btnShowStudentRecordData.Enabled = true;
                    else
                        btnShowStudentRecordData.Enabled = false;
                }
            }
        }

        private void btnShowStudentRecordData_Click(object sender, EventArgs e)
        {
            LoadRecordData();
        }

        private void LoadRecordData()
        {
            try
            {
                facade = new FacadeLayer(SystemTypeForDB);
                int selectIndex = 0;
                bool withDetail = false, isSelect = false;
                classRefundRecordSets = new List<ClassRefundDefinition>();
                recordShowListSets = new List<RecordDefinition>();
                string[] dateSelect = new string[3];

                lblRecordCheckerCheckID.Text = "學生編號:";
                lblRecordCheckerCheckName.Text = "學生姓名:";
                lblRecordCheckerWithDetailRecordList.Text = "記錄清單:";
                lblRecordCheckerWithoutDetailRecordList.Text = "記錄清單:";

                foreach (DataGridViewRow dgvRow in this.dgvStudentRecordData.Rows)
                {
                    //if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                    //{
                    if (dgvRow.Selected)
                    {
                        recordShowListSets.Add(recordSets.ElementAt(selectIndex));
                        isSelect = true;
                    }
                        //if (lblCurrentPage.Text.IndexOf("退費") > -1)
                        //{
                        //    classRefundRecordSets.Add(classRefundSets.ElementAt(selectIndex));
                        //    isSelect = true;
                        //}
                    //}

                    selectIndex++;
                }

                if (lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("05") > -1)
                {
                    //publicObject = (object)classRefundRecordSets;
                    withDetail = true;
                }
                else if (lblCurrentPage.Text.IndexOf("11") > -1)
                {
                    List<RecordDefinition> tempRecordListSets = null;

                    lblRecordCheckerShowCheckID.Text = recordShowListSets.ElementAt(0).Data2ID;
                    lblRecordCheckerShowCheckName.Text = recordShowListSets.ElementAt(0).Data2Name;

                    if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("個人") > -1)
                    {
                        if (lblCurrentPage.Text.IndexOf("明細") > -1)
                        {
                            tempRecordListSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentprepaidhistorylistbyid", (object)recordShowListSets.ElementAt(0).Data2ID, null);
                            lblRecordCheckerWithoutDetailRecordListNote.Text = "剩餘預收金額: " + recordShowListSets.ElementAt(0).Money1 + " 元";
                        }
                        else
                            tempRecordListSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentrecordlist", "StudentID", recordShowListSets.ElementAt(0).Data2ID);
                    }
                    else if (cboStudentSearchRecord.SelectedItem.ToString().IndexOf("全班") > -1)
                    {
                        lblRecordCheckerCheckID.Text = "班級編號:";
                        lblRecordCheckerCheckName.Text = "班級名稱:";
                        tempRecordListSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentrecordlist", "ClassID", recordShowListSets.ElementAt(0).Data2ID);
                    }

                    recordShowListSets = tempRecordListSets;

                    if (lblStudentSearchRecordShowFromDate.Text != "")
                    {
                        string fromDate = lblStudentSearchRecordShowFromDate.Text;
                        string endDate = lblStudentSearchRecordShowEndDate.Text;

                        dateSelect[0] = "ClassRefund";
                        dateSelect[1] = fromDate;
                        dateSelect[2] = endDate;

                        lblRecordCheckerWithoutDetailRecordList.Text = "指定日期: " + fromDate + " 到 " + endDate;

                        recordShowListSets = (List<RecordDefinition>)facade.FacadeFunctions("reusefunction", "dateselect", (object)tempRecordListSets, (object)dateSelect);
                    }

                    withDetail = false;
                }

                if (isSelect)
                    ShowRecordList(recordShowListSets, withDetail);
                //ShowRecordList(publicObject, withDetail);
            }
            catch
            {
            }
        }

        private void btnStudentRecordSelectAll_Click(object sender, EventArgs e)
        {
            int selectIndex = 0;
            foreach (DataGridViewRow dgvRow in this.dgvStudentRecordData.Rows)
            {
                dgvRow.Cells[0].Selected = true;
                selectIndex++;
            }

            btnShowStudentRecordData.Enabled = true;
            btnStudentRecordUnselectAll.Enabled = true;
        }

        private void btnStudentRecordUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in this.dgvStudentRecordData.Rows)
                dgvRow.Cells[0].Selected = false;

            btnStudentRecordSelectAll.Enabled = true;
            btnShowStudentRecordData.Enabled = false;
            btnStudentRecordUnselectAll.Enabled = false;
        }

        private void btnRecordCheckerReturnStudentSearch_Click(object sender, EventArgs e)
        {
            ReturnToStudentSearch();
        }

        #endregion

        #region Record Checker Panel

        private void SetRecordListDefault()
        {
            panelRecordCheckerWithoutDetail.Visible = false;
            panelRecordCheckerRecordWithDetail.Visible = false;
            panelRecordCheckerWithDetailList.Visible = false;
            panelRecordCheckerWithTwoDetailList.Visible = false;
            btnRecordCheckerSelectAll.Visible = false;

            if (dgvRecordCheckerWithDetailRecordList.Columns.Count > 0)
                dgvRecordCheckerWithDetailRecordList.Columns.Clear();

            if (dgvRecordCheckerWithDetailPanel.Columns.Count > 0)
                dgvRecordCheckerWithDetailPanel.Columns.Clear();

            if (dgvRecordCheckerWithoutDetailRecordList.Columns.Count > 0)
                dgvRecordCheckerWithoutDetailRecordList.Columns.Clear();
        }

        public void GetRecordListFromRecordSearch(List<RecordDefinition> recordList, string searchDate, string searchPersonalOrClass)
        {
            try
            {
                lblInvisibleFromDate.Text = "";
                lblInvisibleEndDate.Text = "";

                if (searchDate != "")
                {
                    string fromDate = searchDate.Substring(0, 10);
                    string endDate = searchDate.Substring(10);
                    lblInvisibleFromDate.Text = fromDate;
                    lblInvisibleEndDate.Text = endDate;

                    lblRecordCheckerWithDetailRecordList.Text = "日期指定: " + fromDate + " ~ " + endDate;
                }
                else
                {
                    lblRecordCheckerWithDetailRecordList.Text = "記錄清單:";

                    if (lblCurrentPage.Text.IndexOf("06") > -1)
                    {
                        if (searchPersonalOrClass.IndexOf("班級") > -1)
                        {
                            lblRecordCheckerWithDetailRecordList.Text = "班級指定: " + searchPersonalOrClass.Substring(searchPersonalOrClass.IndexOf(',') + 1);
                        }
                    }
                }

                if (lblCurrentPage.Text.IndexOf("06") > -1)
                {
                    var orderRecordList = from rls in recordList
                                          orderby int.Parse(rls.Data1ID)
                                          select rls;

                    recordList = orderRecordList.ToList();
                }

                lblRecordCheckerSelectWay.Text = searchPersonalOrClass;
                recordShowListSets = recordList;

                if (lblCurrentPage.Text.IndexOf("12") == -1)
                    ShowRecordList(recordList, true);
                else
                    ShowRecordList(recordList, false);
            }
            catch
            {
            }
        }

        //private void ShowRecordList(object recordList, bool withDetail)
        private void ShowRecordList(List<RecordDefinition> recordList, bool withDetail)
        {
            if (recordList != null && recordList.Count > 0)
            {
                DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                panelRecordChecker.Visible = true;
                panelMainMenuScreen.Visible = false;
                panelSubButtons.Visible = true;
                btnReturnSearchPage.Visible = true;
                recordShowListSets = recordList;

                int money = 0;

                if (withDetail)
                {
                    panelRecordCheckerRecordWithDetail.Visible = true;
                    lblRecordCheckerWithDetailRecordListNote.Visible = true;

                    if (dgvRecordCheckerWithDetailRecordList.Columns.Count > 0)
                        dgvRecordCheckerWithDetailRecordList.Columns.Clear();

                    dgvRecordCheckerWithDetailRecordList.DataSource = recordList;
                    btnRecordCheckerWithDetailDelete.Text = "刪除記錄";
                    btnRecordCheckerWithDetailDelete.Visible = false;

                    if (lblCurrentPage.Text.IndexOf("04") > -1)
                    {
                        foreach (var recordSingle in recordList)
                            money += recordSingle.Discount;

                        panelRecordCheckerRecordWithDetail.Visible = true;
                        lblRecordCheckerWithDetailRecordListNote.Text = "繳費總金額: " + money.ToString();

                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money1");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date1");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data2Name");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data2ID");

                        dgvRecordCheckerWithDetailRecordList.Columns["Data1ID"].DisplayIndex = 0;
                        dgvRecordCheckerWithDetailRecordList.Columns["Data1Name"].DisplayIndex = 1;
                        dgvRecordCheckerWithDetailRecordList.Columns["Note1"].DisplayIndex = 2;
                        dgvRecordCheckerWithDetailRecordList.Columns["Note2"].DisplayIndex = 3;
                        dgvRecordCheckerWithDetailRecordList.Columns["Discount"].DisplayIndex = 4;

                        dgvRecordCheckerWithDetailRecordList.Columns["Data1ID"].HeaderText = classPaymentCountDataGridViewHeaderText[0];
                        dgvRecordCheckerWithDetailRecordList.Columns["Data1Name"].HeaderText = classPaymentCountDataGridViewHeaderText[1];
                        dgvRecordCheckerWithDetailRecordList.Columns["Note1"].HeaderText = classPaymentCountDataGridViewHeaderText[2];
                        dgvRecordCheckerWithDetailRecordList.Columns["Note2"].HeaderText = classPaymentCountDataGridViewHeaderText[3];
                        dgvRecordCheckerWithDetailRecordList.Columns["Discount"].HeaderText = classPaymentCountDataGridViewHeaderText[4];
                    }
                    else if (lblCurrentPage.Text.IndexOf("05") > -1)
                    {

                        //foreach (var classRefundSingle in (List<ClassRefundDefinition>)recordList)
                        foreach (var classRefundSingle in recordList)
                            money += classRefundSingle.Money1;

                        panelRecordCheckerRecordWithDetail.Visible = true;
                        lblRecordCheckerWithDetailRecordListNote.Text = "退費總金額: " + money.ToString();

                        //dgvRecordCheckerWithDetailRecordList.DataSource = (List<ClassRefundDefinition>)recordList;
                        dgvRecordCheckerWithDetailRecordList.DataSource = recordList;

                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Note2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Note1");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data1ID");

                        dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].DisplayIndex = 0;
                        dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].DisplayIndex = 1;
                        dgvRecordCheckerWithDetailRecordList.Columns["Date1"].DisplayIndex = 2;
                        dgvRecordCheckerWithDetailRecordList.Columns["Money1"].DisplayIndex = 3;
                        dgvRecordCheckerWithDetailRecordList.Columns["Discount"].DisplayIndex = 4;
                        dgvRecordCheckerWithDetailRecordList.Columns["Data1Name"].DisplayIndex = 5;

                        dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].HeaderText = classRefundDataGridViewHeaderText[0];
                        dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].HeaderText = classRefundDataGridViewHeaderText[1];
                        dgvRecordCheckerWithDetailRecordList.Columns["Date1"].HeaderText = classRefundDataGridViewHeaderText[2];
                        dgvRecordCheckerWithDetailRecordList.Columns["Money1"].HeaderText = classRefundDataGridViewHeaderText[3];
                        dgvRecordCheckerWithDetailRecordList.Columns["Discount"].HeaderText = classRefundDataGridViewHeaderText[4];
                        dgvRecordCheckerWithDetailRecordList.Columns["Data1Name"].HeaderText = classRefundDataGridViewHeaderText[5];
                    }
                    else if (lblCurrentPage.Text.IndexOf("06") > -1)
                    {
                        foreach (var recordSingle in recordList)
                            money += recordSingle.Discount;

                        btnRecordCheckerSelectAll.Visible = true;
                        panelRecordCheckerRecordWithDetail.Visible = true;
                        lblRecordCheckerWithDetailRecordListNote.Text = "繳費總金額: " + money.ToString();

                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money1");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date2");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date1");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data2Name");
                        dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data2ID");

                        dgvRecordCheckerWithDetailRecordList.Columns["Data1ID"].DisplayIndex = 0;
                        dgvRecordCheckerWithDetailRecordList.Columns["Data1Name"].DisplayIndex = 1;
                        dgvRecordCheckerWithDetailRecordList.Columns["Note1"].DisplayIndex = 2;
                        dgvRecordCheckerWithDetailRecordList.Columns["Note2"].DisplayIndex = 3;
                        dgvRecordCheckerWithDetailRecordList.Columns["Discount"].DisplayIndex = 4;

                        dgvRecordCheckerWithDetailRecordList.Columns["Data1ID"].HeaderText = studentPaymentCountDataGridViewHeaderText[0];
                        dgvRecordCheckerWithDetailRecordList.Columns["Data1Name"].HeaderText = studentPaymentCountDataGridViewHeaderText[1];
                        dgvRecordCheckerWithDetailRecordList.Columns["Note1"].HeaderText = studentPaymentCountDataGridViewHeaderText[2];
                        dgvRecordCheckerWithDetailRecordList.Columns["Note2"].HeaderText = studentPaymentCountDataGridViewHeaderText[3];
                        dgvRecordCheckerWithDetailRecordList.Columns["Discount"].HeaderText = studentPaymentCountDataGridViewHeaderText[4];
                    }
                    else if (lblCurrentPage.Text.IndexOf("11") > -1)
                    {
                        panelRecordCheckerRecordWithDetail.Visible = true;

                        if (lblCurrentPage.Text.IndexOf("明細") > -1)
                        {
                            foreach (var recordSingle in recordList)
                                money += recordSingle.Money1;
                            
                            lblRecordCheckerWithDetailRecordListNote.Text = "剩餘預收金額: " + money.ToString();

                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Note2");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Note1");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Discount");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date2");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date1");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money2");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data1Name");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data1ID");

                            dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].DisplayIndex = 0;
                            dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].DisplayIndex = 1;
                            dgvRecordCheckerWithDetailRecordList.Columns["Money1"].DisplayIndex = 2;

                            dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].HeaderText = "學生編號";
                            dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].HeaderText = "學生姓名";
                            dgvRecordCheckerWithDetailRecordList.Columns["Money1"].HeaderText = "預收金額";
                        }
                        else
                        {
                            foreach (var recordSingle in recordList)
                                money += int.Parse(recordSingle.Note2);

                            btnRecordCheckerSelectAll.Visible = true;
                            lblRecordCheckerWithDetailRecordListNote.Text = "進班總金額: " + money.ToString();

                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Discount");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date2");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Date1");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money2");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Money1");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data1Name");
                            dgvRecordCheckerWithDetailRecordList.Columns.Remove("Data1ID");

                            dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].DisplayIndex = 0;
                            dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].DisplayIndex = 1;
                            dgvRecordCheckerWithDetailRecordList.Columns["Note1"].DisplayIndex = 2;
                            dgvRecordCheckerWithDetailRecordList.Columns["Note2"].DisplayIndex = 3;

                            if (lblRecordCheckerSelectWay.Text.IndexOf("全班") == -1)
                            {
                                dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[0];
                                dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[1];
                                dgvRecordCheckerWithDetailRecordList.Columns["Note1"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[2];
                                dgvRecordCheckerWithDetailRecordList.Columns["Note2"].HeaderText = studentPaymentRecordByStudentDataGridViewHeaderText[3];
                            }
                            else
                            {
                                dgvRecordCheckerWithDetailRecordList.Columns["Data2ID"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[0];
                                dgvRecordCheckerWithDetailRecordList.Columns["Data2Name"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[1];
                                dgvRecordCheckerWithDetailRecordList.Columns["Note1"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[2];
                                dgvRecordCheckerWithDetailRecordList.Columns["Note2"].HeaderText = studentPaymentRecordByClassDataGridViewHeaderText[3];
                            }
                        }
                    }

                    dgvRecordCheckerWithDetailRecordList.MultiSelect = false;
                    dgvRecordCheckerWithDetailRecordList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvRecordCheckerWithDetailRecordList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    dgvRecordCheckerWithDetailRecordList.AllowUserToAddRows = false;

                    //if (dgvRecordCheckerWithDetailRecordList.Columns.Count == 0 || dgvRecordCheckerWithDetailRecordList.Columns[0].Name != "Check")
                    //{
                    //    // Initialize and add a check box column.
                    //    DataGridViewColumn column = new DataGridViewCheckBoxColumn();
                    //    column.Name = "Check";
                    //    dgvRecordCheckerWithDetailRecordList.Columns.Insert(0, column);
                    //    column.HeaderCell.Value = string.Empty;
                    //}
                    //CreateComboBoxWithEnums();

                    if (dgvRecordCheckerWithDetailRecordList.Rows.Count > 0)
                        dgvRecordCheckerWithDetailRecordList.Rows[0].Selected = false;

                    //Disable Resizing
                    for (int i = 0; i < dgvRecordCheckerWithDetailRecordList.Rows.Count; i++)
                        dgvRecordCheckerWithDetailRecordList.Rows[i].Resizable = DataGridViewTriState.False;
                    dgvRecordCheckerWithDetailRecordList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //dgvRecordCheckerWithDetailRecordList.Columns[0].Width = 20;
                    for (int i = 0; i < dgvRecordCheckerWithDetailRecordList.Columns.Count; i++)
                    {
                        dgvRecordCheckerWithDetailRecordList.Columns[i].Resizable = DataGridViewTriState.False;
                        dgvRecordCheckerWithDetailRecordList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvRecordCheckerWithDetailRecordList.ReadOnly = true;
                    }

                    btnRecordCheckerUnselectAll.Enabled = false;
                    btnRecordCheckerShowDetail.Enabled = false;
                    btnRecordCheckerSelectAll.Enabled = true;
                }
                else
                {
                    panelRecordCheckerWithoutDetail.Visible = true;
                    lblRecordCheckerWithoutDetailRecordList.Visible = true;
                    lblRecordCheckerWithoutDetailRecordListNote.Visible = true;

                    if (dgvRecordCheckerWithoutDetailRecordList.Columns.Count > 0)
                        dgvRecordCheckerWithoutDetailRecordList.Columns.Clear();

                    dgvRecordCheckerWithoutDetailRecordList.DataSource = recordList;

                    if (lblCurrentPage.Text.IndexOf("12") > -1)
                    {
                        if (lblCurrentPage.Text.IndexOf("加退") > -1)
                        {
                            int addStudent = 0, dropStudent = 0;
                            foreach (var recordSingle in recordList)
                            {
                                addStudent += int.Parse(recordSingle.Note1);
                                dropStudent += int.Parse(recordSingle.Note2);
                            }
                            lblRecordCheckerWithoutDetailRecordListNote.Text = "總加選人數: " + addStudent.ToString() + ", 總退選人數:" + dropStudent.ToString();

                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Discount");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Date2");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Date1");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Money2");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Money1");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Data2Name");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Data2ID");

                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1ID"].DisplayIndex = 0;
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1Name"].DisplayIndex = 1;
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Note1"].DisplayIndex = 2;
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Note2"].DisplayIndex = 3;

                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1ID"].HeaderText = studentInClassCountDataGridViewHeaderText[0];
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1Name"].HeaderText = studentInClassCountDataGridViewHeaderText[1];
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Note1"].HeaderText = studentInClassCountDataGridViewHeaderText[2];
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Note2"].HeaderText = studentInClassCountDataGridViewHeaderText[3];
                        }
                        else
                        {
                            int addStudent = 0;
                            foreach (var recordSingle in recordList)
                            {
                                addStudent += int.Parse(recordSingle.Note1);
                            }
                            lblRecordCheckerWithoutDetailRecordListNote.Text = "學生總數: " + addStudent.ToString();

                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Note2");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Discount");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Date2");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Date1");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Money2");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Money1");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Data2Name");
                            dgvRecordCheckerWithoutDetailRecordList.Columns.Remove("Data2ID");

                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1ID"].DisplayIndex = 0;
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1Name"].DisplayIndex = 1;
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Note1"].DisplayIndex = 2;

                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1ID"].HeaderText = studentInClassCountDataGridViewHeaderText[0];
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Data1Name"].HeaderText = studentInClassCountDataGridViewHeaderText[1];
                            dgvRecordCheckerWithoutDetailRecordList.Columns["Note1"].HeaderText = studentInClassCountDataGridViewHeaderText[2];
                        }
                    }

                    dgvRecordCheckerWithoutDetailRecordList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    dgvRecordCheckerWithoutDetailRecordList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                    dgvRecordCheckerWithoutDetailRecordList.AllowUserToAddRows = false;

                    //if (dgvRecordCheckerWithoutDetailRecordList.Columns.Count == 0 || dgvRecordCheckerWithoutDetailRecordList.Columns[0].Name != "Check")
                    //{
                    //    // Initialize and add a check box column.
                    //    DataGridViewColumn column = new DataGridViewCheckBoxColumn();
                    //    column.Name = "Check";
                    //    dgvRecordCheckerWithoutDetailRecordList.Columns.Insert(0, column);
                    //    column.HeaderCell.Value = string.Empty;
                    //}
                    //CreateComboBoxWithEnums();

                    if (dgvRecordCheckerWithoutDetailRecordList.Rows.Count > 0)
                        dgvRecordCheckerWithoutDetailRecordList.Rows[0].Selected = false;

                    //Disable Resizing
                    for (int i = 0; i < dgvRecordCheckerWithoutDetailRecordList.Rows.Count; i++)
                        dgvRecordCheckerWithoutDetailRecordList.Rows[i].Resizable = DataGridViewTriState.False;
                    dgvRecordCheckerWithoutDetailRecordList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                    //dgvRecordCheckerWithoutDetailRecordList.Columns[0].Width = 20;
                    for (int i = 0; i < dgvRecordCheckerWithoutDetailRecordList.Columns.Count; i++)
                    {
                        dgvRecordCheckerWithoutDetailRecordList.Columns[i].Resizable = DataGridViewTriState.False;
                        dgvRecordCheckerWithoutDetailRecordList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                        dgvRecordCheckerWithoutDetailRecordList.ReadOnly = true;
                    }

                    btnRecordCheckerUnselectAll.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("查無相關記錄!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnToStudentSearch();
            }
        }

        private void dgvRecordCheckerWithDetailRecordList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRecordCheckerWithDetailRecordList_CellDoubleClick(sender, e);
        }

        private void dgvRecordCheckerWithDetailRecordList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvRecordCheckerWithDetailRecordList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvRecordCheckerWithDetailRecordList.ReadOnly = false;
                dgvRecordCheckerWithDetailRecordList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            //if (e.ColumnIndex > 0)
            //    dgvRecordCheckerWithDetailRecordList.ReadOnly = true;
            //else
            //{
            //    dgvRecordCheckerWithDetailRecordList.ReadOnly = false;
            //    dgvRecordCheckerWithDetailRecordList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //}

            //int selectItem = 0;
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvRecordCheckerWithDetailRecordList.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvRecordCheckerWithDetailRecordList.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRecordCheckerWithDetailRecordList.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvRecordCheckerWithDetailRecordList.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvRecordCheckerWithDetailRecordList.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvRecordCheckerWithDetailRecordList.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

            if (selectItem == 0)
            {
                btnRecordCheckerUnselectAll.Enabled = false;
                btnRecordCheckerShowDetail.Enabled = false;
                btnRecordCheckerSelectAll.Enabled = true;
            }
            else if (selectItem > 0)
            {
                btnRecordCheckerUnselectAll.Enabled = true;
                btnRecordCheckerShowDetail.Enabled = true;
                btnRecordCheckerSelectAll.Enabled = true;
            }
        }

        private void btnRecordCheckerShowDetail_Click(object sender, EventArgs e)
        {
            dgvRecordCheckerWithDetailRecordList.MultiSelect = false;
            lblInvisibleIsSelectAll.Text = "false";
            GetSelectRecordCheckerItems();
        }

        private void btnRecordCheckerSelectAll_Click(object sender, EventArgs e)
        {
            int selectCount = 0;
            dgvRecordCheckerWithDetailRecordList.MultiSelect = true;
            foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
            {
                dgvRow.Selected = true;
                selectCount++;
            }

            if (selectCount > 1)
                lblInvisibleIsSelectAll.Text = "true";
            else
                lblInvisibleIsSelectAll.Text = "false";
            GetSelectRecordCheckerItems();
        }

        private void GetSelectRecordCheckerItems()
        {
            try
            {
                List<RecordDefinition> tempRecord = new List<RecordDefinition>();
                int selectIndex = 0;
                bool isSelect = false;

                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
                {
                    //if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                    if (dgvRow.Selected)
                    {
                        tempRecord.Add(recordShowListSets.ElementAt(selectIndex));
                        isSelect = true;

                        //if (lblCurrentPage.Text.IndexOf("退費") > -1)
                        //{
                        //    tempRecord.Add(classRefundRecordSets.ElementAt(selectIndex));
                        //    isSelect = true;
                        //}
                    }

                    selectIndex++;
                }

                if (isSelect)
                    ShowRecordListDetail(tempRecord);   //ShowRecordListDetail((object)tempRecord);
            }
            catch
            {
            }
        }

        private void btnRecordCheckerUnselectAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
                dgvRow.Cells[0].Selected = false;

            btnRecordCheckerUnselectAll.Enabled = false;
            btnRecordCheckerShowDetail.Enabled = false;
            btnRecordCheckerSelectAll.Enabled = true;
        }

        private void ShowRecordListDetail(List<RecordDefinition> recordList)
        {
            int money = 0;
            panelRecordCheckerWithTwoDetailList.Visible = false;
            panelRecordCheckerWithDetailList.Visible = false;

            List<RecordDefinition> tempPrepaidListSets = new List<RecordDefinition>();  //用來記錄 繳費記錄中的預收明細
            List<ClassRefundDefinition> tempClassRefund = new List<ClassRefundDefinition>();
            List<ClassRefundDetailDefinition> tempClassRefundDetail = new List<ClassRefundDetailDefinition>();
            List<RecordDefinition> recordListSets = new List<RecordDefinition>();
            classRefundDetailSets = new List<ClassRefundDetailDefinition>();

            btnRecordCheckerWithDetailDelete.Text = "刪除記錄";
            btnRecordCheckerWithDetailPrintRecord.Visible = false;

            foreach (var recordSingle in recordList)
            {
                if (lblCurrentPage.Text.IndexOf("04") > -1)
                {
                    List<RecordDefinition> tempRecordSets = null;
                    btnRecordCheckerWithDetailPrintRecord.Visible = true;

                    //if (lblRecordCheckerSelectWay.Text.IndexOf("個人") > -1)
                    //    tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentforrecord", (object)"StudentID", (object)recordSingle.Data1ID);
                    //else
                    tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentforrecord", (object)"ClassID", (object)recordSingle.Data1ID);

                    recordListSets.AddRange(tempRecordSets);
                }
                else if (lblCurrentPage.Text.IndexOf("05") > -1)
                {
                    //money += recordSingle.Money1;
                    btnRecordCheckerWithDetailPrintRecord.Visible = false;
                    btnRecordCheckerWithDetailDelete.Visible = true;
                    btnRecordCheckerWithDetailDelete.Enabled = false;
                    btnRecordCheckerWithDetailDelete.Text = "退費課程";

                    List<ClassRefundDefinition> tempClassRefundSets = null;

                    if (lblRecordCheckerSelectWay.Text.IndexOf("全班") == -1)
                        tempClassRefundSets = (List<ClassRefundDefinition>)facade.FacadeFunctions("select", "studentrefundlist", (object)"StudentID", (object)recordSingle.Data2ID);
                    else
                        tempClassRefundSets = (List<ClassRefundDefinition>)facade.FacadeFunctions("select", "studentrefundlist", (object)"ClassID", (object)recordSingle.Data2ID);

                    if (lblInvisibleFromDate.Text != "")
                    {
                        var tempList = from crs in tempClassRefundSets
                                       where DateTime.Parse(crs.RefundDate) >= DateTime.Parse(lblInvisibleFromDate.Text)
                                           && DateTime.Parse(crs.RefundDate) <= DateTime.Parse(lblInvisibleEndDate.Text)
                                       select crs;

                        tempClassRefundSets = tempList.ToList();
                    }
                    else
                    {
                        var tempList = from crs in tempClassRefundSets
                                       where crs.ID.ToString() == recordSingle.Data1ID
                                       select crs;

                        tempClassRefundSets = tempList.ToList();
                    }

                    if (lblRecordCheckerSelectWay.Text.IndexOf("全班") == -1)
                        tempClassRefundDetail = (List<ClassRefundDetailDefinition>)facade.FacadeFunctions("select", "studentrefunddetail", (object)int.Parse(recordSingle.Data1ID), null);
                    else
                    {
                        List<ClassRefundDetailDefinition> tempRefundDetail = null;
                        foreach (var classRefundListSingle in tempClassRefundSets)
                        {
                            //if (lblRecordCheckerSelectWay.Text.IndexOf("全班") == -1)
                            //    tempRefundDetail = (List<ClassRefundDetailDefinition>)facade.FacadeFunctions("select", "studentrefunddetail", (object)classRefundListSingle.ID, null);
                            //else
                                tempRefundDetail = (List<ClassRefundDetailDefinition>)facade.FacadeFunctions("select", "studentrefunddetail", (object)classRefundListSingle.ID, null);

                            tempClassRefundDetail.AddRange(tempRefundDetail);
                        }
                    }

                    tempClassRefundSets.ElementAt(0).StaffName = "共 " + tempClassRefundDetail.Count + " 科";

                    tempClassRefund.AddRange(tempClassRefundSets);
                    classRefundDetailSets.AddRange(tempClassRefundDetail);
                }
                else if (lblCurrentPage.Text.IndexOf("06") > -1)
                {
                    List<RecordDefinition> tempRecordSets = null;
                    btnRecordCheckerWithDetailPrintRecord.Visible = true;

                    tempRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentforrecord", (object)"StudentID", (object)recordSingle.Data1ID);

                    if (bool.Parse(lblInvisibleIsSelectAll.Text))
                    {
                        foreach (var tempRecordSingle in tempRecordSets)
                            tempRecordSingle.Data2Name = recordSingle.Data1ID;
                    }

                    recordListSets.AddRange(tempRecordSets);

                    //var orderRecordList = from rls in recordListSets
                    //                                orderby int.Parse(rls.Data2Name)
                    //                                select rls;

                    //recordListSets = orderRecordList.ToList();
                }
                else if (lblCurrentPage.Text.IndexOf("11") > -1)
                {
                    List<RecordDefinition> tempRecordListSets = null;
                    btnRecordCheckerWithDetailPrintRecord.Visible = true;
                    btnRecordCheckerWithDetailDelete.Visible = true;
                    btnRecordCheckerWithDetailDelete.Enabled = false;

                    if (lblCurrentPage.Text.IndexOf("明細") > -1)
                    {
                        tempRecordListSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentprepaidhistorylistbyid", (object)recordSingle.Data2ID, null);
                        lblRecordCheckerWithDetailLabel.Text = "剩餘預收金額: " + recordSingle.Money1 + " 元";

                        if (lblInvisibleFromDate.Text != "")
                        {
                            var tempList = from rls in tempRecordListSets
                                                 where DateTime.Parse(rls.Date1) >= DateTime.Parse(lblInvisibleFromDate.Text) 
                                                     && DateTime.Parse(rls.Date1) <= DateTime.Parse(lblInvisibleEndDate.Text)
                                                 select rls;

                            tempRecordListSets = tempList.ToList();
                        }
                    }
                    else
                    {
                        if (lblRecordCheckerSelectWay.Text.IndexOf("全班") == -1)
                        {
                            tempRecordListSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentrecordlist", "StudentID", recordSingle.Data2ID);

                            ////*****************************************************
                            ////增加預收明細資料
                            List<RecordDefinition> tempPrepaidList = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentprepaidhistorylistgroupdatebyid", (object)recordSingle.Data2ID, null);
                            if (tempPrepaidList != null && tempPrepaidList.Count > 0)
                            {
                                foreach (var item in tempPrepaidList)
                                {
                                    var prepaidData = new RecordDefinition();
                                    prepaidData.Data1ID = recordSingle.Data2ID;
                                    prepaidData.Data1Name = recordSingle.Data2Name;
                                    prepaidData.Data2ID = recordSingle.Data2ID + " " + recordSingle.Data2Name;
                                    prepaidData.Data2Name = "預繳金額";
                                    prepaidData.Note1 = item.Date1;
                                    prepaidData.Money1 = item.Money1 - item.Money2;

                                    tempPrepaidListSets.Add(prepaidData);
                                }
                                ////Get Left Prepaid Money
                                //RecordDefinition PrepaidData = new RecordDefinition();
                                //PrepaidData.Data1ID = recordSingle.Data2ID;
                                //PrepaidData.Data1Name = recordSingle.Data2Name;
                                //PrepaidData.Data2ID = recordSingle.Data2ID + " " + recordSingle.Data2Name;
                                //PrepaidData.Data2Name = "預繳金額";

                                //int prepaidMoney = 0;
                                //foreach (var item in tempPrepaidList)
                                //{
                                //    if (lblInvisibleFromDate.Text != "")
                                //    {
                                //        if (DateTime.Parse(item.Date1) >= DateTime.Parse(lblInvisibleFromDate.Text) &&
                                //            DateTime.Parse(item.Date1) <= DateTime.Parse(lblInvisibleEndDate.Text))
                                //        {
                                //            prepaidMoney = prepaidMoney + item.Money1 - item.Money2;
                                //        }
                                //    }
                                //    else
                                //        prepaidMoney = prepaidMoney + item.Money1 - item.Money2;
                                //}

                                //PrepaidData.Money1 = prepaidMoney;
                                //tempPrepaidListSets.Add(PrepaidData);
                            }
                            ////*****************************************************

                            tempRecordListSets = tempRecordListSets.OrderBy(u => u.Date1).ToList();
                        }
                        else
                            tempRecordListSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentrecordlist", "ClassID", recordSingle.Data2ID);

                        if (lblInvisibleFromDate.Text != "")
                        {
                            var tempList = from rls in tempRecordListSets
                                           where DateTime.Parse(rls.Note1) >= DateTime.Parse(lblInvisibleFromDate.Text)
                                               && DateTime.Parse(rls.Note1) <= DateTime.Parse(lblInvisibleEndDate.Text)
                                           select rls;

                            tempRecordListSets = tempList.ToList();
                        }
                    }

                    recordListSets.AddRange(tempRecordListSets);
                }
            }
            if (tempPrepaidListSets != null && tempPrepaidListSets.Count > 0)
                recordListSets.InsertRange(0, tempPrepaidListSets.OrderBy(u => DateTime.Parse(u.Note1)));
            recordDetails = recordListSets;

            if (recordListSets != null && recordListSets.Count > 0)
            {
                if (lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("06") > -1)
                {
                    panelRecordCheckerWithDetailList.Visible = true;

                    foreach (var recordSingle in recordListSets)
                        money += int.Parse(recordSingle.Note2);

                    lblRecordCheckerWithDetailLabel.Text = "需繳清單: 總繳費金額 " + money + " 元";
                }
                else if (lblCurrentPage.Text.IndexOf("11") > -1)
                {
                    panelRecordCheckerWithDetailList.Visible = true;

                    if (lblCurrentPage.Text.IndexOf("預收") == -1)
                    {
                        foreach (var recordSingle in recordListSets)
                            money += recordSingle.Money1;

                        lblRecordCheckerWithDetailLabel.Text = "已繳清單: 總繳費金額 " + money + " 元";
                    }
                    else
                    {
                        int inMoney = 0, outMoney = 0;
                        foreach (var recordSingle in recordListSets)
                        {
                            inMoney += recordSingle.Money1;
                            outMoney += recordSingle.Money2;
                        }

                        lblRecordCheckerWithDetailLabel.Text = "預收記錄: 總預收金額 " + inMoney + " 元, 已花費金額 " + outMoney + " 元";
                    }
                }

                ShowRecordWithDetailList((object)recordListSets);
            }
            else if (tempClassRefund != null && tempClassRefund.Count > 0)
            {
                panelRecordCheckerWithDetailList.Visible = true;

                foreach (var classRefundSingle in tempClassRefund)
                    money += classRefundSingle.Refunded;

                lblRecordCheckerWithDetailLabel.Text = "退費清單: 退費總金額 " + money + " 元";

                ShowRecordWithDetailList((object)tempClassRefund);

                //lblRecordCheckerWithDetailFirstLabel.Text = "退費方式:";
                //lblRecordCheckerWithDetailSecondLabel.Text = "退費課程:";

                //ShowRecordFirstDetail((object)tempClassRefund);
                //ShowRecordSecondDetail((object)classRefundDetailSets);
            }
        }

        private void ShowRecordWithDetailList(object recordList)
        {
            if (dgvRecordCheckerWithDetailPanel.Columns.Count > 0)
                dgvRecordCheckerWithDetailPanel.Columns.Clear();

            if (lblCurrentPage.Text.IndexOf("05") == -1)
                dgvRecordCheckerWithDetailPanel.DataSource = (List<RecordDefinition>)recordList;

            if (lblCurrentPage.Text.IndexOf("04") > -1 || lblCurrentPage.Text.IndexOf("06") > -1)
            {
                //dgvRecordCheckerWithDetailPanel.DataSource = (List<RecordDefinition>)recordList;

                if (lblCurrentPage.Text.IndexOf("04") > -1 || (lblCurrentPage.Text.IndexOf("06") > -1 && !bool.Parse(lblInvisibleIsSelectAll.Text)))
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Data2Name");

                dgvRecordCheckerWithDetailPanel.Columns["Data1ID"].DisplayIndex = 0;
                dgvRecordCheckerWithDetailPanel.Columns["Data1Name"].DisplayIndex = 1;
                dgvRecordCheckerWithDetailPanel.Columns["Date1"].DisplayIndex = 2;
                dgvRecordCheckerWithDetailPanel.Columns["Date2"].DisplayIndex = 3;
                dgvRecordCheckerWithDetailPanel.Columns["Money1"].DisplayIndex = 4;
                dgvRecordCheckerWithDetailPanel.Columns["Money2"].DisplayIndex = 5;
                dgvRecordCheckerWithDetailPanel.Columns["Data2ID"].DisplayIndex = 6;
                dgvRecordCheckerWithDetailPanel.Columns["Discount"].DisplayIndex = 7;
                dgvRecordCheckerWithDetailPanel.Columns["Note1"].DisplayIndex = 8;
                dgvRecordCheckerWithDetailPanel.Columns["Note2"].DisplayIndex = 9;

                if (lblCurrentPage.Text.IndexOf("06") > -1 && bool.Parse(lblInvisibleIsSelectAll.Text))
                {
                    dgvRecordCheckerWithDetailPanel.Columns["Data2Name"].DisplayIndex = 0;
                    dgvRecordCheckerWithDetailPanel.Columns["Data2Name"].HeaderText = "學生編號";
                }

                if (lblCurrentPage.Text.IndexOf("04") > -1)
                {
                    dgvRecordCheckerWithDetailPanel.Columns["Data1ID"].HeaderText = "學生編號";
                    dgvRecordCheckerWithDetailPanel.Columns["Data1Name"].HeaderText = "學生姓名";
                }
                else if (lblCurrentPage.Text.IndexOf("06") > -1)
                {
                    dgvRecordCheckerWithDetailPanel.Columns["Data1ID"].HeaderText = "課程編號";
                    dgvRecordCheckerWithDetailPanel.Columns["Data1Name"].HeaderText = "課程名稱";
                }

                dgvRecordCheckerWithDetailPanel.Columns["Date1"].HeaderText = "上課日期";
                dgvRecordCheckerWithDetailPanel.Columns["Date2"].HeaderText = "結束日期";
                dgvRecordCheckerWithDetailPanel.Columns["Money1"].HeaderText = "課程價格";
                dgvRecordCheckerWithDetailPanel.Columns["Money2"].HeaderText = "教材費用";
                dgvRecordCheckerWithDetailPanel.Columns["Data2ID"].HeaderText = "報名費用";
                dgvRecordCheckerWithDetailPanel.Columns["Discount"].HeaderText = "折扣金額";
                dgvRecordCheckerWithDetailPanel.Columns["Note1"].HeaderText = "已繳費用";
                dgvRecordCheckerWithDetailPanel.Columns["Note2"].HeaderText = "應繳費用";
            }
            else if (lblCurrentPage.Text.IndexOf("05") > -1)
            {
                dgvRecordCheckerWithDetailPanel.DataSource = (List<ClassRefundDefinition>)recordList;

                dgvRecordCheckerWithDetailPanel.Columns.Remove("Discount");
                //dgvRecordCheckerWithDetailPanel.Columns.Remove("StaffName");
                dgvRecordCheckerWithDetailPanel.Columns.Remove("StaffID");
                dgvRecordCheckerWithDetailPanel.Columns.Remove("SubID");
                dgvRecordCheckerWithDetailPanel.Columns.Remove("ID");

                for (int i = 0; i < classRefundDataListGridViewHeaderText.Length; i++)
                    dgvRecordCheckerWithDetailPanel.Columns[i].HeaderText = classRefundDataListGridViewHeaderText[i];
            }
            else if (lblCurrentPage.Text.IndexOf("11") > -1)
            {
                if (lblCurrentPage.Text.IndexOf("明細") > -1)
                {
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Note2");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Discount");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Date2");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Data2Name");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Data2ID");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Data1Name");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Data1ID");

                    dgvRecordCheckerWithDetailPanel.Columns["Date1"].DisplayIndex = 0;
                    dgvRecordCheckerWithDetailPanel.Columns["Money1"].DisplayIndex = 1;
                    dgvRecordCheckerWithDetailPanel.Columns["Money2"].DisplayIndex = 2;
                    dgvRecordCheckerWithDetailPanel.Columns["Note1"].DisplayIndex = 3;

                    dgvRecordCheckerWithDetailPanel.Columns["Date1"].HeaderText = "異動日期";
                    dgvRecordCheckerWithDetailPanel.Columns["Money1"].HeaderText = "收取金額";
                    dgvRecordCheckerWithDetailPanel.Columns["Money2"].HeaderText = "使用金額";
                    dgvRecordCheckerWithDetailPanel.Columns["Note1"].HeaderText = "異動方式";
                }
                else
                {
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Discount");
                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Money2");

                    dgvRecordCheckerWithDetailPanel.Columns["Data1ID"].DisplayIndex = 0;
                    dgvRecordCheckerWithDetailPanel.Columns["Data1Name"].DisplayIndex = 1;
                    dgvRecordCheckerWithDetailPanel.Columns["Data2ID"].DisplayIndex = 2;
                    dgvRecordCheckerWithDetailPanel.Columns["Data2Name"].DisplayIndex = 3;
                    dgvRecordCheckerWithDetailPanel.Columns["Date1"].DisplayIndex = 4;
                    dgvRecordCheckerWithDetailPanel.Columns["Date2"].DisplayIndex = 5;
                    dgvRecordCheckerWithDetailPanel.Columns["Note1"].DisplayIndex = 6;
                    dgvRecordCheckerWithDetailPanel.Columns["Money1"].DisplayIndex = 7;
                    dgvRecordCheckerWithDetailPanel.Columns["Note2"].DisplayIndex = 8;

                    dgvRecordCheckerWithDetailPanel.Columns["Data1ID"].HeaderText = "學生編號";
                    dgvRecordCheckerWithDetailPanel.Columns["Data1Name"].HeaderText = "學生姓名";
                    dgvRecordCheckerWithDetailPanel.Columns["Data2ID"].HeaderText = "課程編號";
                    dgvRecordCheckerWithDetailPanel.Columns["Data2Name"].HeaderText = "課程名稱";
                    dgvRecordCheckerWithDetailPanel.Columns["Date1"].HeaderText = "上課日期";
                    dgvRecordCheckerWithDetailPanel.Columns["Date2"].HeaderText = "結束日期";
                    dgvRecordCheckerWithDetailPanel.Columns["Note1"].HeaderText = "繳費日期";
                    dgvRecordCheckerWithDetailPanel.Columns["Money1"].HeaderText = "繳費金額";
                    dgvRecordCheckerWithDetailPanel.Columns["Note2"].HeaderText = "繳費方式";

                    dgvRecordCheckerWithDetailPanel.Columns.Remove("Note2");

                    if (lblRecordCheckerSelectWay.Text.IndexOf("全班") == -1)
                    {
                        dgvRecordCheckerWithDetailPanel.Columns.Remove("Data1Name");
                        dgvRecordCheckerWithDetailPanel.Columns.Remove("Data1ID");
                    }
                    else
                    {
                        dgvRecordCheckerWithDetailPanel.Columns.Remove("Data2Name");
                        dgvRecordCheckerWithDetailPanel.Columns.Remove("Data2ID");
                    }
                }
            }

            dgvRecordCheckerWithDetailPanel.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecordCheckerWithDetailPanel.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvRecordCheckerWithDetailPanel.AllowUserToAddRows = false;

            if (dgvRecordCheckerWithDetailPanel.Rows.Count > 0)
                dgvRecordCheckerWithDetailPanel.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvRecordCheckerWithDetailPanel.Rows.Count; i++)
                dgvRecordCheckerWithDetailPanel.Rows[i].Resizable = DataGridViewTriState.False;
            dgvRecordCheckerWithDetailPanel.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvRecordCheckerWithDetailPanel.Columns.Count; i++)
            {
                dgvRecordCheckerWithDetailPanel.Columns[i].Resizable = DataGridViewTriState.False;
                dgvRecordCheckerWithDetailPanel.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRecordCheckerWithDetailPanel.ReadOnly = true;
            }

            //Change Colour
            if (lblCurrentPage.Text.IndexOf("06") > -1)
            {
                Color currentColor = Color.Transparent;
                string tempStudentIDOne = "";
                string tempStudentIDTwo = "";

                for (int i = 0; i < dgvRecordCheckerWithDetailPanel.Rows.Count; i++)
                {
                    tempStudentIDOne = dgvRecordCheckerWithDetailPanel.Rows[i].Cells["Data2Name"].Value.ToString();
                    tempStudentIDTwo = "";

                    if (i > 0)
                        tempStudentIDTwo = dgvRecordCheckerWithDetailPanel.Rows[i - 1].Cells["Data2Name"].Value.ToString();

                    if (tempStudentIDOne != tempStudentIDTwo)
                    {
                        if (currentColor == Color.FromArgb(255, 255, 128))
                            currentColor = Color.LightCoral;
                        else
                            currentColor = Color.FromArgb(255, 255, 128);
                    }

                    //dgvRecordCheckerWithDetailPanel.Rows[i].DefaultCellStyle.BackColor = currentColor;
                    dgvRecordCheckerWithDetailPanel.Rows[i].DefaultCellStyle.ForeColor = currentColor;
                }
            }
        }

        private void dgvRecordCheckerWithDetailPanel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRecordCheckerWithDetailPanel_CellDoubleClick(sender, e);
        }

        private void dgvRecordCheckerWithDetailPanel_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailPanel.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvRecordCheckerWithDetailPanel.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvRecordCheckerWithDetailPanel.ReadOnly = false;
                dgvRecordCheckerWithDetailPanel.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            if (selectItem == 0)
                btnRecordCheckerWithDetailDelete.Enabled = false;
            else if (selectItem > 0)
                btnRecordCheckerWithDetailDelete.Enabled = true;
        }

        private void btnRecordCheckerWithDetailDelete_Click(object sender, EventArgs e)
        {
            try
            {
                 List<RecordDefinition> tempRecord = new List<RecordDefinition>();
                int selectIndex = 0;
                bool isSelect = false;

                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailPanel.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        try
                        {
                            tempRecord.Add(recordDetails.ElementAt(selectIndex));
                        }
                        catch
                        {
                        }

                        isSelect = true;
                    }

                    selectIndex++;
                }

                if (isSelect)
                {
                    if (lblCurrentPage.Text.IndexOf("05") > -1)
                        CallfrmShowStudentRefundClass();
                    else if (lblCurrentPage.Text.IndexOf("11") > -1)
                    {
                        DialogResult result = MessageBox.Show("是否確定刪除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            facade = new FacadeLayer(SystemTypeForDB);

                            foreach (var recordSingle in tempRecord)
                            {
                                facade.FacadeFunctions("delete", "classpayment", recordSingle, null);
                                CreateSystemLogs("刪除 " + recordSingle.Data1Name + "(" + recordSingle.Data1ID + ") 的 " + recordSingle.Data2Name + "(" + recordSingle.Data2ID + ") 繳費記錄");
                            }

                            MessageBox.Show("刪除成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRecordData();
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private void btnRecordCheckerWithDetailPrintRecord_Click(object sender, EventArgs e)
        {
            facade = new FacadeLayer(SystemTypeForDB);
            if (lblCurrentPage.Text.IndexOf("04") > -1)
            {
                int dgvRowIndex = 0;
                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
                {
                    if (dgvRow.Selected)
                        facade.FacadeFunctions("reusefunction", "needtopaynoticebitmapbyclass", recordShowListSets.ElementAt(dgvRowIndex).Data1ID, recordShowListSets.ElementAt(dgvRowIndex).Data1Name);

                    dgvRowIndex += 1;
                }
            }
            else if (lblCurrentPage.Text.IndexOf("06") > -1)
                CallfrmPrintNeedToPayNotice();
            else if (lblCurrentPage.Text.IndexOf("11") > -1)
            {
                bool isSelectAll = false;

                try
                {
                    isSelectAll = bool.Parse(lblInvisibleIsSelectAll.Text);
                }
                catch { isSelectAll = false; }

                string[] havePaidInfo = new string[6];
                havePaidInfo[0] = lblCurrentPage.Text;
                havePaidInfo[2] = lblRecordCheckerSelectWay.Text;

                string startDate = "", endDate = "";

                if (lblRecordCheckerWithDetailRecordList.Text.IndexOf("日期") > -1)
                {
                    startDate = lblRecordCheckerWithDetailRecordList.Text.Substring(6, 10);
                    endDate = lblRecordCheckerWithDetailRecordList.Text.Substring(19);
                }

                havePaidInfo[3] = startDate;
                havePaidInfo[4] = endDate;
                havePaidInfo[5] = isSelectAll.ToString();

                if (isSelectAll)
                {
                    havePaidInfo[1] = "";
                    facade.FacadeFunctions("reusefunction", "havepaidbitmap", havePaidInfo, null);
                }
                else
                {
                    int dgvRowIndex = 0;
                    foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
                    {
                        if (dgvRow.Selected)
                        {
                            havePaidInfo[1] = recordShowListSets.ElementAt(dgvRowIndex).Data2ID;

                            facade.FacadeFunctions("reusefunction", "havepaidbitmap", havePaidInfo, null);
                        }
                        dgvRowIndex += 1;
                    }
                }
            }
        }

        public void GetNeedToPayNotice(string notice, bool isPrint)
        {
            if (isPrint)
            {
                lblPrintNeedToPayNotice.Text = notice;

                int dgvRowIndex = 0;
                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithDetailRecordList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        string[] needToPayInfo = new string[3];
                        needToPayInfo[0] = "StudentID";
                        needToPayInfo[1] = recordShowListSets.ElementAt(dgvRowIndex).Data1ID;
                        needToPayInfo[2] = lblPrintNeedToPayNotice.Text;

                        facade.FacadeFunctions("reusefunction", "needtopaynoticebitmap", needToPayInfo, null);
                    }
                    dgvRowIndex += 1;
                }
            }
        }

        private void dgvRecordCheckerWithoutDetailRecordList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvRecordCheckerWithoutDetailRecordList_CellDoubleClick(sender, e);
        }

        private void dgvRecordCheckerWithoutDetailRecordList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithoutDetailRecordList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvRecordCheckerWithoutDetailRecordList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvRecordCheckerWithoutDetailRecordList.ReadOnly = false;
                dgvRecordCheckerWithoutDetailRecordList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            //if (e.ColumnIndex > 0)
            //    dgvRecordCheckerWithoutDetailRecordList.ReadOnly = true;
            //else
            //{
            //    dgvRecordCheckerWithoutDetailRecordList.ReadOnly = false;
            //    dgvRecordCheckerWithoutDetailRecordList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //}

            //int selectItem = 0;
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithoutDetailRecordList.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvRecordCheckerWithoutDetailRecordList.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvRecordCheckerWithoutDetailRecordList.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRecordCheckerWithoutDetailRecordList.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvRecordCheckerWithoutDetailRecordList.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvRecordCheckerWithoutDetailRecordList.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvRecordCheckerWithoutDetailRecordList.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

            if (selectItem == 0)
                btnRecordCheckerWithDetailDelete.Enabled = false;
            else if (selectItem > 0)
                btnRecordCheckerWithDetailDelete.Enabled = true;
        }

        private void btnRecordCheckerWithoutDetailDelete_Click(object sender, EventArgs e)
        {
            try
            {
                List<RecordDefinition> tempRecord = new List<RecordDefinition>();
                int selectIndex = 0;
                bool isSelect = false;

                foreach (DataGridViewRow dgvRow in this.dgvRecordCheckerWithoutDetailRecordList.Rows)
                {
                    if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                    {
                        tempRecord.Add(recordShowListSets.ElementAt(selectIndex));
                        isSelect = true;
                    }

                    selectIndex++;
                }

                if (isSelect)
                {
                    DialogResult result = MessageBox.Show("是否確定刪除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        facade = new FacadeLayer(SystemTypeForDB);

                        foreach (var recordSingle in tempRecord)
                        {
                            facade.FacadeFunctions("delete", "classpayment", recordSingle, null);
                            CreateSystemLogs("刪除 " + recordSingle.Data1Name + "(" + recordSingle.Data1ID + ") 的 " + recordSingle.Data2Name + "(" + recordSingle.Data2ID + ") 繳費記錄");
                        }

                        MessageBox.Show("刪除成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRecordData();
                    }
                }
            }
            catch
            {
            }
        }

        private void ShowRecordFirstDetail(object recordList)
        {
            if (dgvRecordCheckerFirstList.Columns.Count > 0)
                dgvRecordCheckerFirstList.Columns.Clear();

            if (lblCurrentPage.Text.IndexOf("退費") > -1)
            {
                dgvRecordCheckerFirstList.DataSource = (List<ClassRefundDefinition>)recordList;

                //dgvRecordCheckerWithDetailRecordList.Columns.Remove("RefundType");
                dgvRecordCheckerFirstList.Columns.Remove("Discount");
                dgvRecordCheckerFirstList.Columns.Remove("StaffName");
                dgvRecordCheckerFirstList.Columns.Remove("StaffID");
                dgvRecordCheckerFirstList.Columns.Remove("SubID");
                dgvRecordCheckerFirstList.Columns.Remove("ID");

                for (int i = 0; i < classRefundDataListGridViewHeaderText.Length; i++)
                    dgvRecordCheckerFirstList.Columns[i].HeaderText = classRefundDataListGridViewHeaderText[i];
            }

            dgvRecordCheckerFirstList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecordCheckerFirstList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvRecordCheckerFirstList.AllowUserToAddRows = false;

            if (dgvRecordCheckerFirstList.Rows.Count > 0)
                dgvRecordCheckerFirstList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvRecordCheckerFirstList.Rows.Count; i++)
                dgvRecordCheckerFirstList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvRecordCheckerFirstList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvRecordCheckerFirstList.Columns.Count; i++)
            {
                dgvRecordCheckerFirstList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRecordCheckerFirstList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvRecordCheckerFirstList.ReadOnly = true;
            }
        }

        private void ShowRecordSecondDetail(object recordList)
        {
            if (dgvRecordCheckerSecondList.Columns.Count > 0)
                dgvRecordCheckerSecondList.Columns.Clear();

            if (lblCurrentPage.Text.IndexOf("退費") > -1)
            {
                dgvRecordCheckerSecondList.DataSource = (List<ClassRefundDetailDefinition>)recordList;

                dgvRecordCheckerSecondList.Columns.Remove("RefundID");
                dgvRecordCheckerSecondList.Columns.Remove("ID");

                for (int i = 0; i < classRefundDataDetailGridViewHeaderText.Length; i++)
                    dgvRecordCheckerSecondList.Columns[i].HeaderText = classRefundDataDetailGridViewHeaderText[i];
            }

            dgvRecordCheckerSecondList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRecordCheckerSecondList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            dgvRecordCheckerSecondList.AllowUserToAddRows = false;

            if (dgvRecordCheckerSecondList.Rows.Count > 0)
                dgvRecordCheckerSecondList.Rows[0].Selected = false;

            //Disable Resizing
            for (int i = 0; i < dgvRecordCheckerSecondList.Rows.Count; i++)
                dgvRecordCheckerSecondList.Rows[i].Resizable = DataGridViewTriState.False;
            dgvRecordCheckerSecondList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            for (int i = 0; i < dgvRecordCheckerSecondList.Columns.Count; i++)
            {
                dgvRecordCheckerSecondList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvRecordCheckerSecondList.Columns[i].Resizable = DataGridViewTriState.False;
                dgvRecordCheckerSecondList.ReadOnly = true;
            }
        }

        #endregion

        #region SystemLogs

        public void CreateSystemLogs(string insertEvent)
        {
            facade = new FacadeLayer(SystemTypeForDB);

            staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "getcurrentuser", null, null);

            if (staffAccountData != null && staffAccountData.Password != "")
                CallfrmEMSFromLogin(staffAccountData.StaffName, staffAccountData.Password, staffAccountData.MasterKey, staffAccountData.StaffRoleID.ToString(), staffAccountData.StaffRole);

            systemLogData = new SystemLogsDefinition(0, 0, lblInvisibleStaffEnglishName.Text, "", insertEvent);

            facade.FacadeFunctions("insert", "systemlog", (object)systemLogData, null);
        }

        #endregion

        #region Insert Panel

        private void SetInsertScreenDefault()
        {
            panelInsertScreen.Visible = false;
            //errorMsg.SetErrorMsgDefault();

            SetInsertStudentQuickDefault();
            SetInsertStudentDefault();
            SetInsertClassDefault();
            SetInsertStaffDefault();
        }

        private void SetInsertStudentQuickDefault()
        {
            SetNewStudentQuickErrorsDefault();

            panelInsertStudentQuick.Visible = false;

            txtInsertStudentQuickStudentID.Text = "";
            txtInsertStudentQuickStudentName.Text = "";
            cboInsertStudentQuickStudentSex.SelectedIndex = -1;
        }

        private void SetInsertStudentDefault()
        {
            //SetNewStudentErrorsDefault();

            panelInsertStudent.Visible = false;

            txtNewStudentID.Text = "";
            txtNewStudentName.Text = "";
            cboNewStudentSex.SelectedIndex = -1;
            cboNewStudentDOBYear.SelectedIndex = -1;
            cboNewStudentDOBMonth.SelectedIndex = -1;
            cboNewStudentDOBDay.SelectedIndex = -1;
            txtNewStudentSocialNumber.Text = "";
            dtpNewStudentStartDate.Value = DateTime.Now;
            txtNewStudentSchoolName.Text = "";
            cboNewStudentStudyYear.SelectedIndex = -1;
            txtNewStudentFatherName.Text = "";
            txtNewStudentFatherWork.Text = "";
            txtNewStudentMotherName.Text = "";
            txtNewStudentMotherWork.Text = "";
            txtNewStudentOldBrother.Text = "0";
            txtNewStudentOldSister.Text = "0";
            txtNewStudentYoungBrother.Text = "0";
            txtNewStudentYoungSister.Text = "0";
            cboNewStudentInChargePerson.SelectedItem = -1;
            txtNewStudentInChargePerson.Visible = false;
            txtNewStudentInChargePerson.Text = "";
            txtNewStudentInChargePersonHomePhone1.Text = "";
            txtNewStudentInChargePersonHomePhone2.Text = "";
            txtNewStudentInChargePersonHomePhone3.Text = "";
            txtNewStudentInChargePersonHomePhone4.Text = "";
            txtNewStudentInChargePersonMobile1.Text = "";
            txtNewStudentInChargePersonMobile2.Text = "";
            txtNewStudentInChargePersonMobile3.Text = "";
            txtNewStudentInChargePersonMobile4.Text = "";
            txtNewStudentEmergencyPerson.Text = "";
            txtNewStudentEmergencyPhone1.Text = "";
            txtNewStudentEmergencyPhone2.Text = "";
            txtNewStudentEmergencyPhone3.Text = "";
            txtNewStudentEmergencyPhone4.Text = "";
            cboNewStudentAddressCity.SelectedIndex = -1;
            txtNewStudentAddressLocalCity.Text = "";
            cboNewStudentAddressLocalCity.SelectedIndex = -1;
            txtNewStudentRoad.Text = "";
            cboNewStudentRoad.SelectedIndex = 0;
            txtNewStudentSection.Text = "";
            txtNewStudentLane.Text = "";
            txtNewStudentAlley.Text = "";
            txtNewStudentNumber.Text = "";
            txtNewStudentFloor.Text = "";
            txtNewStudentFloorS.Text = "";

            //txtNewStudentName.Enabled = false;
            cboNewStudentSex.Enabled = false;
            cboNewStudentDOBYear.Enabled = false;
            cboNewStudentDOBMonth.Enabled = false;
            cboNewStudentDOBDay.Enabled = false;
            txtNewStudentSocialNumber.Enabled = false;
            dtpNewStudentStartDate.Enabled = false;
            txtNewStudentSchoolName.Enabled = false;
            cboNewStudentStudyYear.Enabled = false;
            txtNewStudentFatherName.Enabled = false;
            txtNewStudentFatherWork.Enabled = false;
            txtNewStudentMotherName.Enabled = false;
            txtNewStudentMotherWork.Enabled = false;
            txtNewStudentOldBrother.Enabled = false;
            txtNewStudentOldSister.Enabled = false;
            txtNewStudentYoungBrother.Enabled = false;
            txtNewStudentYoungSister.Enabled = false;
            cboNewStudentInChargePerson.Enabled = false;
            txtNewStudentInChargePersonHomePhone1.Enabled = false;
            txtNewStudentInChargePersonHomePhone2.Enabled = false;
            txtNewStudentInChargePersonHomePhone3.Enabled = false;
            txtNewStudentInChargePersonHomePhone4.Enabled = false;
            txtNewStudentCompanyPhone1.Enabled = false;
            txtNewStudentCompanyPhone2.Enabled = false;
            txtNewStudentCompanyPhone3.Enabled = false;
            txtNewStudentCompanyPhone4.Enabled = false;
            txtNewStudentInChargePersonMobile1.Enabled = false;
            txtNewStudentInChargePersonMobile2.Enabled = false;
            txtNewStudentInChargePersonMobile3.Enabled = false;
            txtNewStudentInChargePersonMobile4.Enabled = false;
            txtNewStudentEmergencyPerson.Enabled = false;
            txtNewStudentEmergencyPhone1.Enabled = false;
            txtNewStudentEmergencyPhone2.Enabled = false;
            txtNewStudentEmergencyPhone3.Enabled = false;
            txtNewStudentEmergencyPhone4.Enabled = false;
            cboNewStudentAddressCity.Enabled = false;
            txtNewStudentAddressLocalCity.Enabled = false;
            cboNewStudentAddressLocalCity.Enabled = false;
            txtNewStudentRoad.Enabled = false;
            cboNewStudentRoad.Enabled = false;
            txtNewStudentSection.Enabled = false;
            txtNewStudentLane.Enabled = false;
            txtNewStudentAlley.Enabled = false;
            txtNewStudentNumber.Enabled = false;
            txtNewStudentFloor.Enabled = false;
            txtNewStudentFloorS.Enabled = false;
        }

        private void SetInsertClassDefault()
        {
            SetNewClassErrorsDefault();

            panelInsertClass.Visible = false;

            txtNewClassID.Enabled = true;
            txtNewClassID.Text = "";
            txtNewClassName.Text = "";
            cboNewClassCategory.SelectedIndex = -1;
            cbNewClassSunday.Checked = false;
            cbNewClassMonday.Checked = false;
            cbNewClassTuesday.Checked = false;
            cbNewClassWednesday.Checked = false;
            cbNewClassThursday.Checked = false;
            cbNewClassFriday.Checked = false;
            cbNewClassSaturday.Checked = false;
            txtNewClassPeriod.Text = "";
            txtNewClassTeacher.Text = "";
            txtNewClassSeat.Text = "";
            txtNewClassPrice.Text = "";
            txtNewClassMaterialFee.Text = "";
            txtNewClassApplyFee.Text = "";
            lbNewClassTime.Items.Clear();
            txtNewClassNote.Text = "";

            btnRemoveNewClassTime.Enabled = false;
            btnDeleteExistClass.Visible = false;

            txtNewClassName.Enabled = false;
            cboNewClassCategory.Enabled = false;
            cbNewClassSunday.Enabled = false;
            cbNewClassMonday.Enabled = false;
            cbNewClassTuesday.Enabled = false;
            cbNewClassWednesday.Enabled = false;
            cbNewClassThursday.Enabled = false;
            cbNewClassFriday.Enabled = false;
            cbNewClassSaturday.Enabled = false;
            dtpNewClassStartDate.Enabled = false;
            dtpNewClassEndDate.Enabled = false;
            txtNewClassPeriod.Enabled = false;
            txtNewClassTeacher.Enabled = false;
            txtNewClassSeat.Enabled = false;
            txtNewClassPrice.Enabled = false;
            txtNewClassMaterialFee.Enabled = false;
            txtNewClassApplyFee.Enabled = false;
            btnAddNewClassTime.Enabled = false;
            txtNewClassNote.Enabled = false;
        }

        private void SetInsertStaffDefault()
        {
            SetNewStaffErrorsDefault();

            panelInsertStaff.Visible = false;

            txtInsertStaffName.Text = "";
            txtInsertStaffEngName.Text = "";
            txtInsertStaffPassword.Text = "";
            txtInsertStaffConfirm.Text = "";
            cbInsertStaffMasterKey.Checked = false;
            txtInsertStaffMasterKey.Enabled = false;
            txtInsertStaffMasterKey.Text = "";
            dtpInsertStaffStartDate.Value = DateTime.Now;
            dtpInsertStaffEndDate.Value = DateTime.Now;
            dtpInsertStaffEndDate.Visible = false;
            txtInsertStaffLaborCover.Text = "0";
            txtInsertStaffHealthCover.Text = "0";
            txtInsertStaffGroupCover.Text = "0";
            txtInsertStaffNote.Text = "";

            lblInvisibleOldStaffEnglishName.Text = "";
            lblInvisibleStaffDataStatus.Text = "Insert";
            lblInvisibleOldStaffMasterKey.Text = "";
            lblInvisibleOldStaffPassword.Text = "";

            btnInsertStaffDelete.Visible = false;

            txtInsertStaffName.Enabled = false;
            cboInsertStaffRole.Enabled = false;
            txtInsertStaffPassword.Enabled = false;
            txtInsertStaffConfirm.Enabled = false;
            cbInsertStaffMasterKey.Enabled = false;
            txtInsertStaffMasterKey.Enabled = false;
            txtInsertStaffMasterKey.Enabled = false;
            dtpInsertStaffStartDate.Enabled = false;
            dtpInsertStaffEndDate.Enabled = false;
            dtpInsertStaffEndDate.Enabled = false;
            txtInsertStaffLaborCover.Enabled = false;
            txtInsertStaffHealthCover.Enabled = false;
            txtInsertStaffGroupCover.Enabled = false;
            txtInsertStaffNote.Enabled = false;

            if (cboInsertStaffRole.Items.Count > 0)
            {
                if (cboInsertStaffRole.Items.Count == 1)
                    cboInsertStaffRole.SelectedIndex = 0;
                else
                    cboInsertStaffRole.SelectedIndex = -1;
            }
        }


        /**********************************************************************************************
         *                                       Insert Student                                       *
         **********************************************************************************************/

        #region Insert Student Quick Panel

        private void btnCancelStudentQuick_Click(object sender, EventArgs e)
        {
            DefaultSetting();
        }

        private void btnInsertStudentQuick_Click(object sender, EventArgs e)
        {
            if (!CheckNewStudentQuickErrors())
            {
                facade = new FacadeLayer(SystemTypeForDB);
                int studentID = 0;

                //DialogResult result = MessageBox.Show("是否確定新增?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //if (result == DialogResult.Yes)
                //{
                if (txtInsertStudentQuickStudentID.Text.Trim() != "")
                    studentID = int.Parse(txtInsertStudentQuickStudentID.Text.Trim());

                studentData = new StudentDefinition(studentID.ToString(), txtInsertStudentQuickStudentName.Text, cboInsertStudentQuickStudentSex.SelectedItem.ToString(), "", "",
                                                    (string)facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null),
                                                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, '0');
                studentID = int.Parse(facade.FacadeFunctions("insert", "student", (object)studentData, null).ToString());

                //lblNewStudentAddClassShowStudentID.Text = studentID.ToString("00000000");
                //lblNewStudentAddClassShowStudentName.Text = txtInsertStudentQuickStudentName.Text;
                CreateSystemLogs("新增學生 " + txtInsertStudentQuickStudentName.Text + "(" + studentID.ToString("00000000") + ")");
                //MessageBox.Show("新增成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //DefaultSetting();
                //SetErrorMsgDefault();
                //SetNewStudentAddClassDefault();
                //ShowStudentAddClassStudentInClassAmount();

                ////panelTopScreen.Visible = true;

                //SettingCurrentPage("新生選課管理 > 新生加選");

                SetNewStudentAddNewClass(studentID.ToString("00000000"), txtInsertStudentQuickStudentName.Text);
                //}
            }
        }

        private void SetNewStudentQuickErrorsDefault()
        {
            lblInsertStudentQuickStudentID.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStudentQuickStudentName.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStudentQuickStudentSex.ForeColor = Color.FromArgb(255, 255, 128);
        }

        private bool CheckNewStudentQuickErrors()
        {
            if (bool.Parse(lblInsertErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetNewStudentQuickErrorsDefault();
            bool isError = false;
            facade = new FacadeLayer(SystemTypeForDB);

            if (txtInsertStudentQuickStudentID.Text.Trim() != "")
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtInsertStudentQuickStudentID.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStudentQuickStudentID.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("學生編號只能為數字!!");
                    isError = true;
                }
                else
                {
                    studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", "ID", txtInsertStudentQuickStudentID.Text.Trim());

                    if (studentData != null && studentData.ID != null)
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblInsertStudentQuickStudentID.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("此學生編號已存在, 請輸入新學生編號!!");
                        isError = true;
                    }
                }
            }
            if (txtInsertStudentQuickStudentName.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblInsertStudentQuickStudentName.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入學生姓名!!");
                isError = true;
            }
            if (cboInsertStudentQuickStudentSex.SelectedIndex < 0)
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblInsertStudentQuickStudentSex.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請選擇學生性別!!");
                isError = true;
            }

            return isError;
        }

        #endregion

        #region Insert Student Panel

        private void btnInsertNewStudent_Click(object sender, EventArgs e)
        {
            if (!CheckNewStudentErrors())
            {
                facade = new FacadeLayer(SystemTypeForDB);
                string studentID = "", studentDOB = "", studentAddress = "", studentStudyYear = "", homePhone = "", companyPhone = "",
                       mobile = "", emergencyPhone = "", inChargePerson = "";
                int studentPrepaid = 0;

                if (lblInvisibleStudentDataStatus.Text.IndexOf("Insert") > -1)
                {
                    DialogResult result = MessageBox.Show("是否確定新增?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        if (btnNewStudentCheckID.Text == "查 詢")
                            studentID = txtNewStudentID.Text.Trim();
                        else
                            studentID = "";
                        studentAddress = lblInvisibleStudentAddress.Text;
                        studentDOB = lblInvisibleStudentDOB.Text;
                        studentStudyYear = lblInvisibleStudentStudyYear.Text;
                        homePhone = lblInvisibleHomePhone.Text;
                        companyPhone = lblInvisibleCompanyPhone.Text;
                        mobile = lblInvisibleInChargePersonMobile.Text;
                        emergencyPhone = lblInvisibleEmergencyPhone.Text;

                        if (cboNewStudentInChargePerson.SelectedIndex > -1)
                            inChargePerson = StaticFunction.SetEncodingString(cboNewStudentInChargePerson.SelectedItem.ToString());

                        studentData = new StudentDefinition(studentID, StaticFunction.SetEncodingString(txtNewStudentName.Text.Trim()),
                                                            StaticFunction.SetEncodingString(cboNewStudentSex.SelectedItem.ToString()),
                                                            studentDOB, txtNewStudentSocialNumber.Text.Trim(),
                                                            (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewStudentStartDate.Value, null),
                                                            StaticFunction.SetEncodingString(txtNewStudentSchoolName.Text.Trim()),
                                                            StaticFunction.SetEncodingString(studentStudyYear),
                                                            StaticFunction.SetEncodingString(txtNewStudentFatherName.Text),
                                                            StaticFunction.SetEncodingString(txtNewStudentFatherWork.Text.Trim()),
                                                            StaticFunction.SetEncodingString(txtNewStudentMotherName.Text.Trim()),
                                                            StaticFunction.SetEncodingString(txtNewStudentMotherWork.Text.Trim()),
                                                            txtNewStudentOldBrother.Text.Trim(),
                                                            txtNewStudentOldSister.Text.Trim(),
                                                            txtNewStudentYoungBrother.Text.Trim(),
                                                            txtNewStudentYoungSister.Text.Trim(),
                                                            inChargePerson, homePhone, companyPhone, mobile,
                                                            StaticFunction.SetEncodingString(txtNewStudentEmergencyPerson.Text.Trim()), emergencyPhone,
                                                            StaticFunction.SetEncodingString(studentAddress), "", 0, '0');

                        studentID = int.Parse(facade.FacadeFunctions("insert", "student", (object)studentData, null).ToString()).ToString("00000000");

                        //lblNewStudentAddClassShowStudentID.Text = studentID;
                        //lblNewStudentAddClassShowStudentName.Text = txtNewStudentName.Text;
                        CreateSystemLogs("新增學生 " + txtNewStudentName.Text + "(" + studentID + ")" + " 資料");
                        MessageBox.Show("新增成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SetInsertStudentDefault();
                        panelInsertStudent.Visible = true;
                        //ReturnToStudentSearch();
                        //SetErrorMsgDefault();

                        //if (lblInvisibleStudentDataStatus.Text.IndexOf("Class") > -1)
                        //{
                        //    //panelTopScreen.Visible = true;

                        //    SettingCurrentPage("新生選課管理 > 新生加選");
                        //}
                    }
                }
                else if (lblInvisibleStudentDataStatus.Text == "Update")
                {
                    DialogResult result = MessageBox.Show("是否確定修改?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        studentID = studentData.ID;
                        studentAddress = lblInvisibleStudentAddress.Text;
                        studentDOB = lblInvisibleStudentDOB.Text;
                        studentStudyYear = lblInvisibleStudentStudyYear.Text;
                        homePhone = lblInvisibleHomePhone.Text;
                        companyPhone = lblInvisibleCompanyPhone.Text;
                        mobile = lblInvisibleInChargePersonMobile.Text;
                        emergencyPhone = lblInvisibleEmergencyPhone.Text;
                        studentPrepaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(studentID), null).ToString());

                        if (cboNewStudentInChargePerson.SelectedIndex > -1)
                            inChargePerson = StaticFunction.SetEncodingString(cboNewStudentInChargePerson.SelectedItem.ToString());

                        studentData = new StudentDefinition(studentID, StaticFunction.SetEncodingString(txtNewStudentName.Text.Trim()),
                                                            StaticFunction.SetEncodingString(cboNewStudentSex.SelectedItem.ToString()),
                                                            studentDOB, txtNewStudentSocialNumber.Text.Trim(),
                                                            (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewStudentStartDate.Value, null),
                                                            StaticFunction.SetEncodingString(txtNewStudentSchoolName.Text.Trim()),
                                                            StaticFunction.SetEncodingString(studentStudyYear),
                                                            StaticFunction.SetEncodingString(txtNewStudentFatherName.Text),
                                                            StaticFunction.SetEncodingString(txtNewStudentFatherWork.Text.Trim()),
                                                            StaticFunction.SetEncodingString(txtNewStudentMotherName.Text.Trim()),
                                                            StaticFunction.SetEncodingString(txtNewStudentMotherWork.Text.Trim()),
                                                            txtNewStudentOldBrother.Text.Trim(),
                                                            txtNewStudentOldSister.Text.Trim(),
                                                            txtNewStudentYoungBrother.Text.Trim(),
                                                            txtNewStudentYoungSister.Text.Trim(),
                                                            inChargePerson, homePhone, companyPhone, mobile,
                                                            StaticFunction.SetEncodingString(txtNewStudentEmergencyPerson.Text.Trim()), emergencyPhone,
                                                            StaticFunction.SetEncodingString(studentAddress), "", 0, '0');

                        facade.FacadeFunctions("update", "student", (object)studentData, null);

                        CreateSystemLogs("修改學生 " + txtNewStudentName.Text + "(" + studentID + ")" + " 資料");
                        MessageBox.Show("修改成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)"ID", (object)studentID);
                    }
                }
            }
            else
                MessageBox.Show("輸入資料有誤，請參考錯誤訊息，並修正!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnCancelNewStudent_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("取消後, 所有資料將會回復!! 是否確定取消??", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                UnlockStudentInfo();
                panelInsertStudent.Visible = true;

                if (lblInvisibleStudentDataStatus.Text.IndexOf("Insert") > -1)
                {
                    SetInsertStudentDefault();
                    panelInsertStudent.Visible = true;
                }
                else if (lblInvisibleStudentDataStatus.Text == "Update")
                    LoadStudentDataByUpdating();
            }
        }

        private void cboNewStudentInChargePerson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboNewStudentInChargePerson.SelectedIndex > -1)
            {
                if (cboNewStudentInChargePerson.SelectedItem.ToString() == "其他")
                    txtNewStudentInChargePerson.Visible = true;
                else
                    txtNewStudentInChargePerson.Visible = false;
            }
        }

        private void UnlockStudentInfo()
        {
            txtNewStudentID.Enabled = true;
            txtNewStudentName.Enabled = true;
            cboNewStudentSex.Enabled = true;
            cboNewStudentDOBYear.Enabled = true;
            cboNewStudentDOBMonth.Enabled = true;
            cboNewStudentDOBDay.Enabled = true;
            txtNewStudentSocialNumber.Enabled = true;
            dtpNewStudentStartDate.Enabled = true;
            txtNewStudentSchoolName.Enabled = true;
            cboNewStudentStudyYear.Enabled = true;
            txtNewStudentFatherName.Enabled = true;
            txtNewStudentFatherWork.Enabled = true;
            txtNewStudentMotherName.Enabled = true;
            txtNewStudentMotherWork.Enabled = true;
            txtNewStudentOldBrother.Enabled = true;
            txtNewStudentOldSister.Enabled = true;
            txtNewStudentYoungBrother.Enabled = true;
            txtNewStudentYoungSister.Enabled = true;
            cboNewStudentInChargePerson.Enabled = true;
            txtNewStudentInChargePersonHomePhone1.Enabled = true;
            txtNewStudentInChargePersonHomePhone2.Enabled = true;
            txtNewStudentInChargePersonHomePhone3.Enabled = true;
            txtNewStudentInChargePersonHomePhone4.Enabled = true;
            txtNewStudentCompanyPhone1.Enabled = true;
            txtNewStudentCompanyPhone2.Enabled = true;
            txtNewStudentCompanyPhone3.Enabled = true;
            txtNewStudentCompanyPhone4.Enabled = true;
            txtNewStudentInChargePersonMobile1.Enabled = true;
            txtNewStudentInChargePersonMobile2.Enabled = true;
            txtNewStudentInChargePersonMobile3.Enabled = true;
            txtNewStudentInChargePersonMobile4.Enabled = true;
            txtNewStudentEmergencyPerson.Enabled = true;
            txtNewStudentEmergencyPhone1.Enabled = true;
            txtNewStudentEmergencyPhone2.Enabled = true;
            txtNewStudentEmergencyPhone3.Enabled = true;
            txtNewStudentEmergencyPhone4.Enabled = true;
            cboNewStudentAddressCity.Enabled = true;
            txtNewStudentAddressLocalCity.Enabled = true;
            cboNewStudentAddressLocalCity.Enabled = true;
            txtNewStudentRoad.Enabled = true;
            cboNewStudentRoad.Enabled = true;
            txtNewStudentSection.Enabled = true;
            txtNewStudentLane.Enabled = true;
            txtNewStudentAlley.Enabled = true;
            txtNewStudentNumber.Enabled = true;
            txtNewStudentFloor.Enabled = true;
            txtNewStudentFloorS.Enabled = true;
        }

        private void SetNewStudentErrorsDefault()
        {
            lblNewStudentName.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentSex.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentDOB.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentSocialNumber.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentSchoolName.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentStudyYear.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentFatherName.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentFatherWork.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentMotherName.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentMotherWork.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentSibling.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentInChargeParent.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentInChargeParentPhone.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentCompanyPhone.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentInChargePersonMobile.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentEmergencyPerson.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentEmergencyPhone.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentAddress.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewStudentStartDate.ForeColor = Color.FromArgb(255, 255, 128);
        }

        private bool CheckNewStudentErrors()
        {
            if (bool.Parse(lblInsertErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetNewStudentErrorsDefault();
            bool isError = false;
            facade = new FacadeLayer(SystemTypeForDB);
            lblInvisibleStudentDOB.Text = "";
            lblInvisibleStudentAddress.Text = "";
            lblInvisibleStudentStudyYear.Text = "";

            if (btnNewStudentCheckID.Text == "查 詢" && lblInvisibleStudentDataStatus.Text.IndexOf("Insert") > -1)
            {
                if (txtNewStudentID.Text.Trim() == "")
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentID.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請輸入學生編號!!");
                    isError = true;
                }
                else
                {
                    if (!(bool)facade.FacadeFunctions("check", "number", txtNewStudentID.Text.Trim(), null))
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblNewStudentID.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("學生編號只能為數字!!");
                        isError = true;
                    }
                    else if (int.Parse(txtNewStudentID.Text) == 0)
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblNewStudentID.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("學生編號不得為零!!");
                        isError = true;
                    }
                    else if (int.Parse(txtNewStudentID.Text) < 0)
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblNewStudentID.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("學生編號不得小於零!!");
                        isError = true;
                    }
                    else if (int.Parse(txtNewStudentID.Text) > 99999999)
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblNewStudentID.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("學生編號超出預設最大值!!");
                        isError = true;
                    }
                    else
                    {
                        studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)"ID", (object)txtNewStudentID.Text.Trim());

                        if (studentData != null && studentData.ID != null && studentData.ID != "" && studentData.ID != "0")
                        {
                            CallfrmErrorMessage();
                            lblInsertErrorMsgIsShow.Text = "true";
                            lblNewStudentID.ForeColor = Color.Red;
                            errorMsg.ShowErrorMessage("此學生編號已被使用!!");
                            isError = true;
                        }
                    }
                }
            }
            if (txtNewStudentName.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewStudentName.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入學生姓名!!");
                isError = true;
            }
            if (cboNewStudentSex.SelectedIndex < 0)
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewStudentSex.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請選擇學生性別!!");
                isError = true;
            }
            if (cboNewStudentDOBYear.SelectedIndex > -1 || cboNewStudentDOBMonth.SelectedIndex > -1 || cboNewStudentDOBDay.SelectedIndex > -1) //DOB
            {
                if (cboNewStudentDOBYear.SelectedIndex == -1 || cboNewStudentDOBMonth.SelectedIndex == -1 || cboNewStudentDOBDay.SelectedIndex == -1)
                {
                    CallfrmErrorMessage();
                    lblNewStudentDOB.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請學生生日錯誤!!");
                    isError = true;
                }
                else
                {
                    facade = new FacadeLayer(SystemTypeForDB);
                    lblInvisibleStudentDOB.Text = facade.FacadeFunctions("format", "datebystring", cboNewStudentDOBYear.SelectedItem.ToString() + "-" +
                                                                                                   (cboNewStudentDOBMonth.SelectedIndex + 1).ToString("00") + "-" +
                                                                                                   (cboNewStudentDOBDay.SelectedIndex + 1).ToString("00"), null).ToString();

                    if (lblInvisibleStudentDOB.Text == "")
                    {
                        CallfrmErrorMessage();
                        lblNewStudentDOB.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("請學生生日錯誤!!");
                        isError = true;
                    }
                }
            }
            //if (txtNewStudentSchoolName.Text.Trim() == "")
            //{
            //    lblNewStudentSchoolName.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入學生學校!!");
            //    isError = true;
            //}

            if (cboNewStudentStudyYear.SelectedIndex > -1)
                lblInvisibleStudentStudyYear.Text = cboNewStudentStudyYear.SelectedItem.ToString();
            //if (cboNewStudentStudyYear.SelectedIndex < 0)
            //{
            //    lblNewStudentStudyYear.ForeColor = Color.Red;
            //    ShowErrorMessage("請選擇學生年級!!");
            //    isError = true;
            //}
            //if (txtNewStudentFatherName.Text.Trim() == "")
            //{
            //    lblNewStudentFatherName.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入父親姓名!!");
            //    isError = true;
            //}
            //if (txtNewStudentFatherWork.Text.Trim() == "")
            //{
            //    lblNewStudentFatherWork.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入父親工作!!");
            //    isError = true;
            //}
            //if (txtNewStudentMotherName.Text.Trim() == "")
            //{
            //    lblNewStudentMotherName.ForeColor = Color.Red;
            //    ShowErrorMessage("請選擇母親姓名!!");
            //    isError = true;
            //}
            //if (txtNewStudentMotherWork.Text.Trim() == "")
            //{
            //    lblNewStudentMotherWork.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入母親工作!!");
            //    isError = true;
            //}
            //if (txtNewStudentInChargePerson.Text.Trim() == "")
            //{
            //    lblNewStudentInChargeParent.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入負責家長!!");
            //    isError = true;
            //}

            //if (txtNewStudentInChargePersonPhone.Text.Trim() == "")
            //{
            //    lblNewStudentInChargeParentPhone.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入負責家長電話!!");
            //    isError = true;
            //}
            if (txtNewStudentOldBrother.Text.Trim() == "")
                txtNewStudentOldBrother.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtNewStudentOldBrother.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentOldBrother.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("哥哥狀況只能為數字!!");
                    isError = true;
                }
            }
            if (txtNewStudentYoungBrother.Text.Trim() == "")
                txtNewStudentYoungBrother.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtNewStudentYoungBrother.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentYoungBrother.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("弟弟狀況只能為數字!!");
                    isError = true;
                }
            }
            if (txtNewStudentOldSister.Text.Trim() == "")
                txtNewStudentOldSister.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtNewStudentOldSister.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentOldSister.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("姊姊狀況只能為數字!!");
                    isError = true;
                }
            }
            if (txtNewStudentYoungSister.Text.Trim() == "")
                txtNewStudentYoungSister.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtNewStudentYoungSister.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentYoungSister.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("妹妹狀況只能為數字!!");
                    isError = true;
                }
            }

            if (cboNewStudentInChargePerson.SelectedIndex == -1)
                lblInvisibleStudentInChargeParent.Text = "";
            else if (cboNewStudentInChargePerson.SelectedItem.ToString() == "其他")
                lblInvisibleStudentInChargeParent.Text = txtNewStudentInChargePerson.Text;
            else
                lblInvisibleStudentInChargeParent.Text = cboNewStudentInChargePerson.SelectedItem.ToString();

            lblInvisibleHomePhone.Text = "";
            if (txtNewStudentInChargePersonHomePhone1.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentInChargePersonHomePhone1.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargeParentPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("住宅電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleHomePhone.Text += "," + phoneNum;
            }
            if (txtNewStudentInChargePersonHomePhone2.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentInChargePersonHomePhone2.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargeParentPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("住宅電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleHomePhone.Text += "," + phoneNum;
            }
            if (txtNewStudentInChargePersonHomePhone3.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentInChargePersonHomePhone3.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargeParentPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("住宅電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleHomePhone.Text += "," + phoneNum;
            }
            if (txtNewStudentInChargePersonHomePhone4.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentInChargePersonHomePhone4.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargeParentPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("住宅電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleHomePhone.Text += "," + phoneNum;
            }
            if (lblInvisibleHomePhone.Text.Length > 0)
                lblInvisibleHomePhone.Text = lblInvisibleHomePhone.Text.Substring(1);

            lblInvisibleCompanyPhone.Text = "";
            if (txtNewStudentCompanyPhone1.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentCompanyPhone1.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentCompanyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("公司電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleCompanyPhone.Text += "," + phoneNum;
            }
            if (txtNewStudentCompanyPhone2.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentCompanyPhone2.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentCompanyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("公司電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleCompanyPhone.Text += "," + phoneNum;
            }
            if (txtNewStudentCompanyPhone3.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentCompanyPhone3.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentCompanyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("公司電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleCompanyPhone.Text += "," + phoneNum;
            }
            if (txtNewStudentCompanyPhone4.Text.Trim() != "")
            {
                string phoneNum = txtNewStudentCompanyPhone4.Text.Trim();

                if (phoneNum.IndexOf('-') > -1)
                    phoneNum = phoneNum.Substring(0, phoneNum.IndexOf('-') - 1) + phoneNum.Substring(phoneNum.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", phoneNum, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentCompanyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("公司電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleCompanyPhone.Text += "," + phoneNum;
            }
            if (lblInvisibleCompanyPhone.Text.Length > 0)
                lblInvisibleCompanyPhone.Text = lblInvisibleCompanyPhone.Text.Substring(1);

            lblInvisibleInChargePersonMobile.Text = "";
            if (txtNewStudentInChargePersonMobile1.Text.Trim() != "")
            {
                string mobile = txtNewStudentInChargePersonMobile1.Text.Trim();
                if (!(bool)facade.FacadeFunctions("check", "number", mobile, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargePersonMobile.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("家長手機只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleInChargePersonMobile.Text += "," + mobile;
            }
            if (txtNewStudentInChargePersonMobile2.Text.Trim() != "")
            {
                string mobile = txtNewStudentInChargePersonMobile2.Text.Trim();
                if (!(bool)facade.FacadeFunctions("check", "number", mobile, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargePersonMobile.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("家長手機只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleInChargePersonMobile.Text += "," + mobile;
            }
            if (txtNewStudentInChargePersonMobile3.Text.Trim() != "")
            {
                string mobile = txtNewStudentInChargePersonMobile3.Text.Trim();
                if (!(bool)facade.FacadeFunctions("check", "number", mobile, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargePersonMobile.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("家長手機只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleInChargePersonMobile.Text += "," + mobile;
            }
            if (txtNewStudentInChargePersonMobile4.Text.Trim() != "")
            {
                string mobile = txtNewStudentInChargePersonMobile4.Text.Trim();
                if (!(bool)facade.FacadeFunctions("check", "number", mobile, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentInChargePersonMobile.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("家長手機只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleInChargePersonMobile.Text += "," + mobile;
            }
            if (lblInvisibleInChargePersonMobile.Text.Length > 0)
                lblInvisibleInChargePersonMobile.Text = lblInvisibleInChargePersonMobile.Text.Substring(1);

            //if (txtNewStudentEmergencyPerson.Text.Trim() == "")
            //{
            //    lblNewStudentEmergencyPerson.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入緊急連絡人!!");
            //    isError = true;
            //}

            //if (txtNewStudentEmergencyPhone.Text.Trim() == "")
            //{
            //    lblNewStudentEmergencyPhone.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入緊急連絡電話!!");
            //    isError = true;
            //}
            lblInvisibleEmergencyPhone.Text = "";
            if (txtNewStudentEmergencyPhone1.Text.Trim() != "")
            {
                string emergencyPhone = txtNewStudentEmergencyPhone1.Text.Trim();

                if (emergencyPhone.IndexOf('-') > -1)
                    emergencyPhone = emergencyPhone.Substring(0, emergencyPhone.IndexOf('-') - 1) + emergencyPhone.Substring(emergencyPhone.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", emergencyPhone, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentEmergencyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("緊急電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleEmergencyPhone.Text += "," + emergencyPhone;
            }
            if (txtNewStudentEmergencyPhone2.Text.Trim() != "")
            {
                string emergencyPhone = txtNewStudentEmergencyPhone2.Text.Trim();

                if (emergencyPhone.IndexOf('-') > -1)
                    emergencyPhone = emergencyPhone.Substring(0, emergencyPhone.IndexOf('-') - 1) + emergencyPhone.Substring(emergencyPhone.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", emergencyPhone, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentEmergencyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("緊急電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleEmergencyPhone.Text += "," + emergencyPhone;
            }
            if (txtNewStudentEmergencyPhone3.Text.Trim() != "")
            {
                string emergencyPhone = txtNewStudentEmergencyPhone3.Text.Trim();

                if (emergencyPhone.IndexOf('-') > -1)
                    emergencyPhone = emergencyPhone.Substring(0, emergencyPhone.IndexOf('-')) + emergencyPhone.Substring(emergencyPhone.IndexOf('-') + 1);

                if (!(bool)facade.FacadeFunctions("check", "number", emergencyPhone, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentEmergencyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("緊急電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleEmergencyPhone.Text += "," + emergencyPhone;
            }
            if (txtNewStudentEmergencyPhone4.Text.Trim() != "")
            {
                string emergencyPhone = txtNewStudentEmergencyPhone4.Text.Trim();

                if (emergencyPhone.IndexOf('-') > -1)
                    emergencyPhone = emergencyPhone.Substring(0, emergencyPhone.IndexOf('-') - 1) + emergencyPhone.Substring(emergencyPhone.IndexOf('-'));

                if (!(bool)facade.FacadeFunctions("check", "number", emergencyPhone, null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentEmergencyPhone.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("緊急電話只能為數字!!");
                    isError = true;
                }
                else
                    lblInvisibleEmergencyPhone.Text += "," + emergencyPhone;
            }
            if (lblInvisibleEmergencyPhone.Text.Length > 0)
                lblInvisibleEmergencyPhone.Text = lblInvisibleEmergencyPhone.Text.Substring(1);

            if (cboNewStudentAddressCity.SelectedIndex > -1 || txtNewStudentRoad.Text.Trim() != "")
            {
                if (cboNewStudentAddressCity.SelectedIndex == -1)
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentAddress.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請選擇居住城市!!");
                    isError = true;
                }
                if (txtNewStudentAddressLocalCity.Text.Trim() != "")
                {
                    if (cboNewStudentAddressLocalCity.SelectedIndex == -1)
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblNewStudentAddress.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("請選擇居住鄉鎮市!!");
                        isError = true;
                    }
                }
                if (txtNewStudentRoad.Text.Trim() == "")
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewStudentAddress.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請輸入居住路名!!");
                    isError = true;
                }
                if (cboNewStudentAddressCity.SelectedIndex > -1 && txtNewStudentRoad.Text != "")
                {
                    lblInvisibleStudentAddress.Text = cboNewStudentAddressCity.SelectedItem.ToString() + " ";
                    lblInvisibleStudentAddress.Text += txtNewStudentAddressLocalCity.Text + cboNewStudentAddressLocalCity.SelectedItem.ToString();
                    lblInvisibleStudentAddress.Text += txtNewStudentRoad.Text;

                    lblInvisibleStudentAddress.Text += cboNewStudentRoad.SelectedItem.ToString();
                    if (txtNewStudentSection.Text != "")
                        lblInvisibleStudentAddress.Text += txtNewStudentSection.Text + "段";
                    if (txtNewStudentLane.Text != "")
                        lblInvisibleStudentAddress.Text += txtNewStudentLane.Text + "巷";
                    if (txtNewStudentAlley.Text != "")
                        lblInvisibleStudentAddress.Text += txtNewStudentAlley.Text + "弄";
                    if (txtNewStudentNumber.Text != "")
                        lblInvisibleStudentAddress.Text += txtNewStudentNumber.Text + "號";
                    if (txtNewStudentFloor.Text != "")
                        lblInvisibleStudentAddress.Text += txtNewStudentFloor.Text + "樓";
                    if (txtNewStudentFloorS.Text != "")
                        lblInvisibleStudentAddress.Text += "之" + txtNewStudentFloorS.Text;
                }
            }


            //if (txtNewStudentPostCode.Text.Trim() == "")
            //{
            //    lblNewStudentPostCode.ForeColor = Color.Red;
            //    ShowErrorMessage("請輸入郵遞區號!!");
            //    isError = true;
            //}
            //if (txtNewStudentPostCode.Text.Trim() != "")
            //{
            //    if (!(bool)facade.FacadeFunctions("check", "number", txtNewStudentPostCode.Text.Trim(), null))
            //    {
            //        CallfrmErrorMessage();
            //        lblInsertErrorMsgIsShow.Text = "true";
            //        lblNewStudentStartDate.ForeColor = Color.Red;
            //        errorMsg.ShowErrorMessage("郵遞區號只能為數字!!");
            //        isError = true;
            //    }
            //    else
            //    {
            //        lblInvisibleStudentAddress.Text = cboNewStudentAddressCity.SelectedItem.ToString() + " " + txtNewStudentRoad.Text;
            //        if (txtNewStudentLane.Text.Trim() != "")
            //            lblInvisibleStudentAddress.Text += " " + txtNewStudentLane.Text;
            //    }
            //}

            return isError;
        }

        private void txtNewStudent_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        #endregion


        /**********************************************************************************************
         *                                        Insert Class                                        *
         **********************************************************************************************/

        #region Insert Class Panel

        private void btnCancelNewClass_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("取消後, 所有資料將會回復!! 是否確定取消??", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (lblInvisibleClassDataStatus.Text == "Update")
                    ShowClassDataForUpdating();
                else
                {
                    SetInsertClassDefault();
                    panelInsertStaff.Visible = true;
                }
            }
        }

        private void btnInsertNewClass_Click(object sender, EventArgs e)
        {
            if (!CheckNewClassErrors())
            {
                facade = new FacadeLayer(SystemTypeForDB);

                if (lblInvisibleClassDataStatus.Text == "Insert")
                {
                    DialogResult result = MessageBox.Show("是否確定新增?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        //classData = new ClassDefinition(txtNewClassID.Text, cboNewClassCategory.SelectedItem.ToString(), txtNewClassName.Text,
                        classData = new ClassDefinition(txtNewClassID.Text, "None", StaticFunction.SetEncodingString(txtNewClassName.Text),
                                                        (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassStartDate.Value, null),
                                                        (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassEndDate.Value, null),
                                                        int.Parse(txtNewClassPeriod.Text), lblInvisibleClassDay.Text, 0, int.Parse(txtNewClassPrice.Text), "",
                                                        StaticFunction.SetEncodingString(txtNewClassTeacher.Text), int.Parse(txtNewClassMaterialFee.Text), int.Parse(txtNewClassApplyFee.Text),
                                                        StaticFunction.SetEncodingString(txtNewClassNote.Text), '0');
                        facade.FacadeFunctions("insert", "class", (object)classData, null);

                        if (lbNewClassTime.Visible)
                        {
                            classTimeSets = new List<ClassTimeDefinition>();
                            for (int i = 0; i < lbNewClassTime.Items.Count; i++)
                            {
                                classTimeData = new ClassTimeDefinition(0, txtNewClassID.Text, txtNewClassName.Text, lbNewClassTime.Items[i].ToString());
                                classTimeSets.Add(classTimeData);
                            }
                            facade.FacadeFunctions("insert", "classtime", (object)classTimeSets, null);
                        }

                        CreateSystemLogs("新增課程 " + txtNewClassName.Text + "(" + txtNewClassID.Text + ")" + " 資料");
                        MessageBox.Show("新增成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SetInsertClassDefault();
                        panelInsertClass.Visible = true;
                    }
                }
                else if (lblInvisibleClassDataStatus.Text == "Update")
                {
                    DialogResult result = MessageBox.Show("是否確定修改?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        string id = classData.ID;
                        string classStatus = classData.ClassStatus;
                        classData = new ClassDefinition(txtNewClassID.Text, "None", StaticFunction.SetEncodingString(txtNewClassName.Text),
                                                        (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassStartDate.Value, null),
                                                        (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassEndDate.Value, null),
                                                        int.Parse(txtNewClassPeriod.Text), lblInvisibleClassDay.Text, 0, int.Parse(txtNewClassPrice.Text), classStatus,
                                                        StaticFunction.SetEncodingString(txtNewClassTeacher.Text), int.Parse(txtNewClassMaterialFee.Text), int.Parse(txtNewClassApplyFee.Text),
                                                        StaticFunction.SetEncodingString(txtNewClassNote.Text), '0');
                        facade.FacadeFunctions("update", "class", (object)classData, lblInvisibleOldClassID.Text);

                        if (lbNewClassTime.Visible)
                        {
                            facade.FacadeFunctions("delete", "classtime", id, null);
                            classTimeSets = new List<ClassTimeDefinition>();
                            for (int i = 0; i < lbNewClassTime.Items.Count; i++)
                            {
                                classTimeData = new ClassTimeDefinition(0, id, txtNewClassName.Text, lbNewClassTime.Items[i].ToString());
                                classTimeSets.Add(classTimeData);
                            }
                            facade.FacadeFunctions("insert", "classtime", (object)classTimeSets, null);
                        }

                        CreateSystemLogs("修改課程 " + txtNewClassName.Text + "(" + txtNewClassID.Text + ")" + " 資料");
                        MessageBox.Show("修改成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
                MessageBox.Show("輸入課程資料有誤，請參考錯誤訊息，並修正!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UnlockClassInfo()
        {
            txtNewClassName.Enabled = true;
            cboNewClassCategory.Enabled = true;
            cbNewClassSunday.Enabled = true;
            cbNewClassMonday.Enabled = true;
            cbNewClassTuesday.Enabled = true;
            cbNewClassWednesday.Enabled = true;
            cbNewClassThursday.Enabled = true;
            cbNewClassFriday.Enabled = true;
            cbNewClassSaturday.Enabled = true;
            dtpNewClassStartDate.Enabled = true;
            dtpNewClassEndDate.Enabled = true;
            txtNewClassPeriod.Enabled = true;
            txtNewClassTeacher.Enabled = true;
            txtNewClassSeat.Enabled = true;
            txtNewClassPrice.Enabled = true;
            txtNewClassMaterialFee.Enabled = true;
            txtNewClassApplyFee.Enabled = true;
            btnAddNewClassTime.Enabled = true;
            txtNewClassNote.Enabled = true;
        }

        private void btnAddNewClassTime_Click(object sender, EventArgs e)
        {
            CallfrmNewClassTime();
            btnAddNewClassTime.Enabled = false;
        }

        //Get ClassTime from frmNewClassTime
        public void GetNewClassTime(string classDay, string startTime, string endTime)
        {
            //string classTime = classDay + " " + startTime + " 到 " + endTime;

            lbNewClassTime.Items.Add(classDay + " " + startTime + " 到 " + endTime);

            if (lbNewClassTime.Items.Count > 1)
            {
                string newDay = lbNewClassTime.Items[lbNewClassTime.Items.Count - 1].ToString().Substring(0, 3);
                DateTime classFromTime = DateTime.Parse(lbNewClassTime.Items[lbNewClassTime.Items.Count - 1].ToString().Substring(4, 5));

                for (int i = 0; i < lbNewClassTime.Items.Count - 1; i++)
                {
                    string weekDay = lbNewClassTime.Items[i].ToString().Substring(0, 3);

                    if (ReturnWeeklyNum(newDay) < ReturnWeeklyNum(weekDay))
                    {
                        lbNewClassTime.Items.Insert(i, classDay + " " + startTime + " 到 " + endTime);
                        lbNewClassTime.Items.RemoveAt(lbNewClassTime.Items.Count - 1);
                        break;
                    }
                    else if (ReturnWeeklyNum(classDay) == ReturnWeeklyNum(weekDay))
                    {
                        DateTime weekFromTime = DateTime.Parse(lbNewClassTime.Items[i].ToString().Substring(4, 5));

                        if (classFromTime < weekFromTime)
                        {
                            lbNewClassTime.Items.Insert(i, classDay + " " + startTime + " 到 " + endTime);
                            lbNewClassTime.Items.RemoveAt(lbNewClassTime.Items.Count - 1);
                            break;
                        }
                    }
                }
            }

            //btnRemoveNewClassTime.Enabled = true;
        }

        private int ReturnWeeklyNum(string weekDay)
        {
            string[] weekly = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
            int weekNum = -1;

            for (int i = 0; i < weekly.Length; i++)
            {
                if (weekly[i] == weekDay)
                    weekNum = i;
            }

            return weekNum;
        }

        private void lbNewClassTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbNewClassTime.SelectedIndex > -1)
                btnRemoveNewClassTime.Enabled = true;
            else
                btnRemoveNewClassTime.Enabled = false;
        }

        private void btnRemoveNewClassTime_Click(object sender, EventArgs e)
        {
            if (lbNewClassTime.SelectedIndex > -1)
                lbNewClassTime.Items.Remove(lbNewClassTime.SelectedItem);

            if (lbNewClassTime.Items.Count == 0)
                btnRemoveNewClassTime.Enabled = false;
        }

        private void cbNewClassClassDay_CheckedChanged(object sender, EventArgs e)
        {
            CountClassDays();
            if (dtpNewClassStartDate.Value <= dtpNewClassEndDate.Value)
                CountClassPeriod();
        }

        private void dtpNewClassStartDate_ValueChanged(object sender, EventArgs e)
        {
            lblTempNewClassPeriod.Text = txtNewClassPeriod.Text;
            GetClassEndDate();
        }

        private void dtpNewClassEndDate_ValueChanged(object sender, EventArgs e)
        {
            CountClassPeriod();
        }

        private void txtNewClassPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                lblTempNewClassPeriod.Text = txtNewClassPeriod.Text;
                GetClassEndDate();
            }
        }

        private int CountClassDays()
        {
            int countClassDay = 0;
            lblInvisibleNewClassCheckClassDay.Text = "false";
            if (cbNewClassSunday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            if (cbNewClassMonday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            if (cbNewClassTuesday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            if (cbNewClassWednesday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            if (cbNewClassThursday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            if (cbNewClassFriday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            if (cbNewClassSaturday.Checked)
            {
                lblInvisibleNewClassCheckClassDay.Text = "true";
                countClassDay++;
            }
            return countClassDay;
        }

        private string[] GetClassDays()
        {
            string[] classDay = new string[CountClassDays()];
            int countClassDay = 0;
            int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);
            if (CheckClassDaysIsStartDate(firstDay))
            {
                int tempDay = firstDay;
                while (tempDay <= 6)
                {
                    if (CheckClassDaysIsStartDate(tempDay))
                    {
                        classDay[countClassDay] = tempDay.ToString();
                        countClassDay++;
                    }
                    tempDay++;
                }

                for (int i = 0; i < firstDay; i++)
                {
                    if (CheckClassDaysIsStartDate(i))
                    {
                        classDay[countClassDay] = i.ToString();
                        countClassDay++;
                    }
                }
            }
            return classDay;
        }

        private bool CheckClassDaysIsStartDate(int weekDay)
        {
            bool checkDay = false;

            switch (weekDay)
            {
                case 0:
                    if (cbNewClassSunday.Checked)
                        checkDay = true;
                    break;
                case 1:
                    if (cbNewClassMonday.Checked)
                        checkDay = true;
                    break;
                case 2:
                    if (cbNewClassTuesday.Checked)
                        checkDay = true;
                    break;
                case 3:
                    if (cbNewClassWednesday.Checked)
                        checkDay = true;
                    break;
                case 4:
                    if (cbNewClassThursday.Checked)
                        checkDay = true;
                    break;
                case 5:
                    if (cbNewClassFriday.Checked)
                        checkDay = true;
                    break;
                case 6:
                    if (cbNewClassSaturday.Checked)
                        checkDay = true;
                    break;
                case 7:
                    if (cbNewClassSunday.Checked)
                        checkDay = true;
                    break;
                default:
                    checkDay = true;
                    break;
            }

            return checkDay;
        }

        private void CountClassPeriod()
        {
            if (lblInvisibleNewClassCheckClassDay.Text == "true")
            {
                if (dtpNewClassStartDate.Value < dtpNewClassEndDate.Value)
                {
                    try
                    {
                        int lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassEndDate.Value.DayOfWeek, null);
                        int tempLastDay = lastDay;

                        if (!CheckClassDaysIsStartDate(lastDay))
                        {
                            int needToSub = 0;

                            //while (!CheckClassDaysIsStartDate(lastDay))
                            while (!CheckClassDaysIsStartDate(lastDay) && lastDay > 0)
                            {
                                lastDay = tempLastDay;
                                needToSub++;
                                lastDay -= needToSub;
                            }

                            dtpNewClassEndDate.Value = dtpNewClassEndDate.Value.AddDays(-needToSub);
                            lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassEndDate.Value.DayOfWeek, null);
                        }

                        int classDays = CountClassDays();
                        System.TimeSpan timeSpan = dtpNewClassEndDate.Value - dtpNewClassStartDate.Value;
                        int dayPeriod = timeSpan.Days + 1;
                        //if (CheckClassDaysIsStartDate(lastDay))
                        //    dayPeriod += 1;

                        //int periodAndDayGap = 0;
                        //if (classDays >= int.Parse(txtNewClassPeriod.Text))
                        //    periodAndDayGap = classDays - int.Parse(txtNewClassPeriod.Text);
                        int modPeriod = dayPeriod % 7;
                        int dividePeriod = dayPeriod / 7;
                        int finalPeriod = dividePeriod * classDays;

                        DateTime tempDay = dtpNewClassStartDate.Value.AddDays(dividePeriod * 7 - 1);

                        for (int i = 1; i <= modPeriod; i++)
                        {
                            lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", tempDay.AddDays(i).DayOfWeek, null);
                            if (CheckClassDaysIsStartDate(lastDay))
                                finalPeriod++;
                        }

                        txtNewClassPeriod.Text = finalPeriod.ToString();

                        if (lblTempNewClassPeriod.Text != "" && txtNewClassPeriod.Text != lblTempNewClassPeriod.Text)
                            ReGetClassEndDate();

                        lblTempNewClassPeriod.Text = "";
                    }
                    catch
                    {
                        MessageBox.Show("CountClassPeriod", "Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void ReGetClassEndDate()
        {
            try
            {
                int enterDay = int.Parse(lblTempNewClassPeriod.Text);
                int countedDay = int.Parse(txtNewClassPeriod.Text);
                int gapDay = enterDay - countedDay;

                int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassEndDate.Value.DayOfWeek, null);

                if (gapDay != 0)
                {
                    firstDay += gapDay;


                    while (!CheckClassDaysIsStartDate(firstDay) && firstDay > -6)
                    {
                        if (gapDay > 0)
                            firstDay++;
                        else if (gapDay < 0)
                            firstDay--;
                    }
                }

                if (gapDay > 0)
                    firstDay = firstDay - gapDay;
                else if (gapDay < 0)
                    firstDay = firstDay + gapDay;

                dtpNewClassEndDate.Value = dtpNewClassEndDate.Value.AddDays(firstDay - gapDay);
            }
            catch { }
        }

        private void GetClassEndDate()
        {
            if (lblInvisibleNewClassCheckClassDay.Text == "true")
            {
                if (txtNewClassPeriod.Text.Trim() != "")
                {
                    try
                    {
                        if ((bool)facade.FacadeFunctions("check", "number", txtNewClassPeriod.Text.Trim(), null))
                        {
                            string[] classDays = GetClassDays();

                            if (classDays[0] != null && classDays[0] != "")
                            {
                                //int temp = int.Parse(txtNewClassPeriod.Text) % classDays.Length - 1;
                                int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);
                                //int lastDay = int.Parse(classDays[int.Parse(txtNewClassPeriod.Text) % classDays.Length - 1]);
                                //if (int.Parse(txtNewClassPeriod.Text) != classDays.Length)
                                //    lastDay = int.Parse(classDays[int.Parse(txtNewClassPeriod.Text) % classDays.Length - 1]);
                                //else
                                //    lastDay = int.Parse(classDays[int.Parse(txtNewClassPeriod.Text) % classDays.Length]);

                                if (CheckClassDaysIsStartDate(firstDay))
                                {
                                    DateTime endDate;

                                    int modPeriod = int.Parse(txtNewClassPeriod.Text) % classDays.Length;
                                    int spanPeriod = int.Parse(txtNewClassPeriod.Text) / classDays.Length;
                                    endDate = dtpNewClassStartDate.Value.AddDays(7 * spanPeriod - 1 + modPeriod);
                                    //if (lastDay != firstDay)
                                    //{
                                    //    if (firstDay > lastDay)
                                    //    {
                                    //        int tempDay = firstDay;
                                    //        firstDay = lastDay;
                                    //        lastDay = tempDay;
                                    //    }

                                    //    endDate = dtpNewClassStartDate.Value.AddDays(lastDay - firstDay);

                                    //}
                                    //else
                                    //    endDate = dtpNewClassStartDate.Value.AddDays(7 * (int.Parse(txtNewClassPeriod.Text) / classDays.Length));

                                    int lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", endDate.DayOfWeek, null);
                                    int tempLastDay = lastDay;
                                    if (!CheckClassDaysIsStartDate(lastDay))
                                    {
                                        int needToSub = 0;

                                        if (tempLastDay == 0)
                                            tempLastDay = 7;

                                        //while (!CheckClassDaysIsStartDate(lastDay))
                                        while (!CheckClassDaysIsStartDate(lastDay) && lastDay > -6)
                                        {
                                            lastDay = tempLastDay;
                                            needToSub++;
                                            lastDay -= needToSub;

                                            if (lastDay < 0)
                                                lastDay += 7;
                                        }

                                        endDate = endDate.AddDays(-needToSub);
                                    }

                                    dtpNewClassEndDate.Value = endDate;
                                }
                            }
                        }
                    }
                    catch
                    {
                        MessageBox.Show("GetClassEndDate", "Error", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void SetNewClassErrorsDefault()
        {
            lblNewClassID.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassName.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassType.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassStartDate.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassEndDate.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassPeriod.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassTeacher.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassSeat.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassPrice.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassMaterialFee.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassTime.ForeColor = Color.FromArgb(255, 255, 128);
        }

        private bool CheckNewClassErrors()
        {
            if (bool.Parse(lblInsertErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetNewClassErrorsDefault();
            bool isError = false;
            facade = new FacadeLayer(SystemTypeForDB);

            if (txtNewClassID.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassID.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程編號!!");
                isError = true;
            }
            if (txtNewClassName.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassName.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程名稱!!");
                isError = true;
            }
            //if (cboNewClassCategory.SelectedIndex < 0)
            //{
            //    lblNewClassType.ForeColor = Color.Red;
            //    ShowErrorMessage("請選擇課程類別!!");
            //    isError = true;
            //}
            if (dtpNewClassStartDate.Value > dtpNewClassEndDate.Value)
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassStartDate.ForeColor = Color.Red;
                lblNewClassEndDate.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("開課日期無法晚於結束日期!!");
                isError = true;
            }
            if (txtNewClassTeacher.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassTeacher.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程老師!!");
                isError = true;
            }

            //if (txtNewClassSeat.Text.Trim() == "")
            //{
            //    CallfrmErrorMessage();
            //    lblInsertErrorMsgIsShow.Text = "true";
            //    lblNewClassSeat.ForeColor = Color.Red;
            //    errorMsg.ShowErrorMessage("請輸入課程人數!!");
            //    isError = true;
            //}
            //else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassSeat.Text.Trim(), null))
            //{
            //    CallfrmErrorMessage();
            //    lblInsertErrorMsgIsShow.Text = "true";
            //    lblNewClassSeat.ForeColor = Color.Red;
            //    errorMsg.ShowErrorMessage("課程人數只能為數字!!");
            //    isError = true;
            //}

            if (txtNewClassPrice.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPrice.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程價格!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassPrice.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPrice.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("課程價格只能為數字!!");
                isError = true;
            }

            if (txtNewClassMaterialFee.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassMaterialFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入教材費用!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassMaterialFee.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassMaterialFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("教材費用只能為數字!!");
                isError = true;
            }

            if (txtNewClassApplyFee.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassApplyFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入報名費用!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassApplyFee.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassApplyFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("報名費用只能為數字!!");
                isError = true;
            }

            if (lbNewClassTime.Visible)
            {
                if (lbNewClassTime.Items.Count == 0)
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewClassTime.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請新增課程時間!!");
                    isError = true;
                }
            }
            else
            {
                CountClassDays();
                if (lblInvisibleNewClassCheckClassDay.Text == "false")
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblNewClassTime.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請勾選課程時間!!");
                    isError = true;
                }
                else
                {
                    string classDay = null;
                    if (cbNewClassSunday.Checked)
                        classDay = "1,";
                    else
                        classDay = "0,";
                    if (cbNewClassMonday.Checked)
                        classDay += "1,";
                    else
                        classDay += "0,";
                    if (cbNewClassTuesday.Checked)
                        classDay += "1,";
                    else
                        classDay += "0,";
                    if (cbNewClassWednesday.Checked)
                        classDay += "1,";
                    else
                        classDay += "0,";
                    if (cbNewClassThursday.Checked)
                        classDay += "1,";
                    else
                        classDay += "0,";
                    if (cbNewClassFriday.Checked)
                        classDay += "1,";
                    else
                        classDay += "0,";
                    if (cbNewClassSaturday.Checked)
                        classDay += "1";
                    else
                        classDay += "0";
                    lblInvisibleClassDay.Text = classDay;

                    CountClassPeriod();
                    //GetClassEndDate();

                    int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);
                    if (!CheckClassDaysIsStartDate(firstDay))
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblNewClassStartDate.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("開課日期未被勾選!!");
                        isError = true;
                    }
                }
            }

            if (txtNewClassPeriod.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPeriod.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程節數!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassPeriod.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPeriod.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("課程節數只能為數字!!");
                isError = true;
            }

            return isError;
        }

        #endregion

        /**********************************************************************************************
         *                                        Insert Staff                                        *
         **********************************************************************************************/

        #region Insert Staff Panel

        private void btnInsertStaff_Click(object sender, EventArgs e)
        {
            if (!CheckNewStaffErrors())
            {
                facade = new FacadeLayer(SystemTypeForDB);
                int staffID = 0;
                string studentDOB = "", studentAddress = "", studentStudyYear = "";

                if (lblInvisibleStaffDataStatus.Text.IndexOf("Insert") > -1)
                {
                    DialogResult result = MessageBox.Show("是否確定新增?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        staffData = new StaffDefinition("", cboInsertStaffRole.SelectedIndex + 1, "Employee", StaticFunction.SetEncodingString(txtInsertStaffName.Text),
                                                        txtInsertStaffEngName.Text,
                                                        (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpInsertStaffStartDate.Value, null),
                                                        "0000-00-00", int.Parse(txtInsertStaffLaborCover.Text), int.Parse(txtInsertStaffHealthCover.Text),
                                                        int.Parse(txtInsertStaffGroupCover.Text),
                                                        StaticFunction.SetEncodingString(txtInsertStaffNote.Text), '0');

                        staffID = int.Parse(facade.FacadeFunctions("insert", "staff", (object)staffData, null).ToString());

                        staffAccountData = new StaffAccountDefinition(0, staffID, "", txtInsertStaffEngName.Text,
                                                                      txtInsertStaffPassword.Text, txtInsertStaffMasterKey.Text, 0, "");

                        facade.FacadeFunctions("insert", "staffaccount", (object)staffAccountData, null);

                        CreateSystemLogs("新增員工 " + txtInsertStaffName.Text + "(" + staffID + "), 英文名字 " + txtInsertStaffEngName.Text + " 資料");
                        MessageBox.Show("新增成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        SetInsertStaffDefault();
                        panelInsertStaff.Visible = true;
                    }
                }
                else if (lblInvisibleStaffDataStatus.Text == "Update")
                {
                    DialogResult result = MessageBox.Show("是否確定修改?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        staffID = int.Parse(staffData.ID);
                        studentAddress = lblInvisibleStudentAddress.Text;
                        studentDOB = lblInvisibleStudentDOB.Text;
                        studentStudyYear = lblInvisibleStudentStudyYear.Text;

                        staffData = new StaffDefinition(staffID.ToString(), cboInsertStaffRole.SelectedIndex + 1, "Employee",
                                                        StaticFunction.SetEncodingString(txtInsertStaffName.Text),
                                                        txtInsertStaffEngName.Text,
                                                       (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpInsertStaffStartDate.Value, null),
                                                        "0000-00-00", int.Parse(txtInsertStaffLaborCover.Text), int.Parse(txtInsertStaffHealthCover.Text),
                                                        int.Parse(txtInsertStaffGroupCover.Text),
                                                        StaticFunction.SetEncodingString(txtInsertStaffNote.Text), '0');

                        facade.FacadeFunctions("update", "staff", (object)staffData, null);

                        if (lblInvisibleOldStaffEnglishName.Text != txtInsertStaffEngName.Text)
                        {
                            CreateSystemLogs("修改員工 " + txtInsertStaffName.Text + "(" + staffID + "), 英文名字 " + lblInvisibleOldStaffEnglishName.Text +
                                             " 為 " + txtInsertStaffEngName.Text);
                        }

                        CreateSystemLogs("修改員工 " + txtInsertStaffName.Text + "(" + staffID + "), 英文名字 " + txtInsertStaffEngName.Text + " 資料");


                        string newPassword = null, newMasterKey = "";
                        string msg = null;

                        if (txtInsertStaffPassword.Text.Trim() != "" && (txtInsertStaffPassword.Text.Trim() != lblInvisibleOldStaffPassword.Text.Trim()))
                        {
                            newPassword = txtInsertStaffPassword.Text.Trim();
                            msg = "員工密碼";
                        }
                        else
                            newPassword = lblInvisibleOldStaffPassword.Text.Trim();

                        if (txtInsertStaffMasterKey.Enabled)
                        {
                            if (txtInsertStaffMasterKey.Text.Trim() != "" && txtInsertStaffMasterKey.Text.Trim() != lblInvisibleOldStaffMasterKey.Text.Trim())
                            {
                                newMasterKey = txtInsertStaffMasterKey.Text.Trim();

                                if (lblInvisibleOldStaffMasterKey.Text.Trim() == "")
                                {
                                    if (msg == null)
                                        msg = "新增安全密碼";
                                    else
                                        msg = "及新增安全密碼";
                                }
                                else
                                {
                                    if (msg == null)
                                        msg = "安全密碼";
                                    else
                                        msg = "及安全密碼";
                                }
                            }
                            else
                                newMasterKey = staffAccountData.MasterKey;
                        }
                        else
                        {
                            newMasterKey = "";

                            if (msg == null)
                            {
                                if (lblInvisibleOldStaffMasterKey.Text.Trim() != "")
                                    msg = "解除安全密碼";
                            }
                            else
                            {
                                if (lblInvisibleOldStaffMasterKey.Text.Trim() != "")
                                    msg = "及解除安全密碼";
                            }
                        }

                        if (msg != null)
                        {
                            staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "staffaccountbyenglishname", staffData.EnglishName, null);

                            int staffAccountID = staffAccountData.ID;
                            staffAccountData = new StaffAccountDefinition(staffAccountID, staffID, "", StaticFunction.SetEncodingString(txtInsertStaffEngName.Text),
                                                                          newPassword, newMasterKey, 0, "");

                            facade.FacadeFunctions("update", "staffaccount", (object)staffAccountData, null);

                            CreateSystemLogs("修改員工 " + txtInsertStaffName.Text + "(" + staffID + "), 英文名字 " + txtInsertStaffEngName.Text + " " + msg);

                            if (lblInvisibleOldStaffEnglishName.Text == lblInvisibleStaffEnglishName.Text)
                            {
                                lblInvisibleStaffPassword.Text = newPassword;
                                lblInvisibleStaffMasterKey.Text = newMasterKey;
                            }
                        }

                        lblInvisibleOldStaffEnglishName.Text = txtInsertStaffEngName.Text;

                        MessageBox.Show("修改成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            else
                MessageBox.Show("輸入資料有誤，請參考錯誤訊息，並修正!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnInsertStaffCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("取消後, 所有資料將會回復!! 是否確定取消??", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (lblInvisibleStaffDataStatus.Text.IndexOf("Insert") > -1)
                {
                    SetInsertStaffDefault();
                    panelInsertStaff.Visible = true;
                }
                else if (lblInvisibleStaffDataStatus.Text == "Update")
                    ShowStaffDataForUpdating();
            }
        }

        private void UnlockStaffInfo()
        {
            txtInsertStaffName.Enabled = true;
            cboInsertStaffRole.Enabled = true;
            txtInsertStaffPassword.Enabled = true;
            txtInsertStaffConfirm.Enabled = true;
            //cbInsertStaffMasterKey.Enabled = true;
            //txtInsertStaffMasterKey.Enabled = true;
            txtInsertStaffMasterKey.Enabled = true;
            dtpInsertStaffStartDate.Enabled = true;
            dtpInsertStaffEndDate.Enabled = true;
            dtpInsertStaffEndDate.Enabled = true;
            txtInsertStaffLaborCover.Enabled = true;
            txtInsertStaffHealthCover.Enabled = true;
            txtInsertStaffGroupCover.Enabled = true;
            txtInsertStaffNote.Enabled = true;
        }

        private void cbInsertStaffMasterKey_CheckedChanged(object sender, EventArgs e)
        {
            if (panelInsertStaff.Visible)
            {
                if (lblInvisibleOldStaffMasterKey.Text != "")
                {
                    if (!cbInsertStaffMasterKey.Checked)
                    {
                        DialogResult result = MessageBox.Show("是否確定取消安全密碼??", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                            txtInsertStaffMasterKey.Enabled = false;
                        else
                            cbInsertStaffMasterKey.Checked = true;
                    }
                    else
                        txtInsertStaffMasterKey.Enabled = true;
                }
                else
                {
                    if (cbInsertStaffMasterKey.Checked)
                        txtInsertStaffMasterKey.Enabled = true;
                    else
                        txtInsertStaffMasterKey.Enabled = false;
                }
            }
        }

        private void cboInsertStaffRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblInsertStaffMasterKey.Enabled = false;
            txtInsertStaffMasterKey.Enabled = false;

            if (cboInsertStaffRole.SelectedIndex > -1)
            {
                if (cboInsertStaffRole.SelectedIndex != cboInsertStaffRole.Items.Count - 1)
                {
                    lblInsertStaffMasterKey.Enabled = true;
                    txtInsertStaffMasterKey.Enabled = true;
                }
            }
        }

        private void SetNewStaffErrorsDefault()
        {
            lblInsertStaffName.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffRole.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffEngName.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffPassword.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffMasterKey.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffLaborCover.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffHealthCover.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffGroupCover.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStaffNote.ForeColor = Color.FromArgb(255, 255, 128);
        }

        private bool CheckNewStaffErrors()
        {
            if (bool.Parse(lblInsertErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetNewStaffErrorsDefault();
            bool isError = false;
            facade = new FacadeLayer(SystemTypeForDB);

            if (txtInsertStaffName.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblInsertStaffName.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入員工姓名!!");
                isError = true;
            }

            if (txtInsertStaffEngName.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblInsertStaffEngName.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入員工英文名字!!");
                isError = true;
            }
            else
            {
                if (txtInsertStaffEngName.Text.Trim() != lblInvisibleOldStaffEnglishName.Text.Trim())
                {
                    if (!(bool)facade.FacadeFunctions("check", "staffenglishname", (object)txtInsertStaffEngName.Text.Trim(), null))
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblInsertStaffEngName.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("此英文名字已存在, 請更換另一個!!");
                        isError = true;
                    }
                }
            }

            if (cboInsertStaffRole.SelectedIndex == -1)
            {
                CallfrmErrorMessage();
                lblInsertErrorMsgIsShow.Text = "true";
                lblInsertStaffRole.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請選擇員工角色!!");
                isError = true;
            }

            if (lblInvisibleStaffDataStatus.Text == "Insert")
            {
                if (txtInsertStaffPassword.Text.Trim() == "")
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStaffPassword.ForeColor = Color.Red;
                    txtInsertStaffConfirm.Text = "";
                    errorMsg.ShowErrorMessage("請輸入員工密碼!!");
                    isError = true;
                }
            }
            else if (lblInvisibleStaffDataStatus.Text == "Update")
            {
                if (txtInsertStaffMasterKey.Text.Trim().Length > 0)
                {
                }
                else
                {
                    if (txtInsertStaffPassword.Text.Trim() == lblInvisibleOldStaffMasterKey.Text.Trim())
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblInsertStaffPassword.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("員工密碼不能與安全密碼相同!!");
                        isError = true;
                    }
                }
            }

            if (txtInsertStaffPassword.Text.Trim().Length > 0)
            {
                if (txtInsertStaffPassword.Text.Trim().Length < 6)
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStaffPassword.ForeColor = Color.Red;
                    txtInsertStaffConfirm.Text = "";
                    errorMsg.ShowErrorMessage("員工密碼請介於6~15個字元!!");
                    isError = true;
                }
                else if (txtInsertStaffPassword.Text.Trim() != txtInsertStaffConfirm.Text.Trim())
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStaffPassword.ForeColor = Color.Red;
                    txtInsertStaffConfirm.Text = "";
                    errorMsg.ShowErrorMessage("員工密碼與確認密碼不相同!!");
                    isError = true;
                }
            }

            if (txtInsertStaffMasterKey.Enabled)
            {
                if (lblInvisibleStaffDataStatus.Text == "Insert")
                {
                    if (txtInsertStaffMasterKey.Text.Trim().Length < 6)
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblInsertStaffMasterKey.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("安全密碼請介於6~15個字元!!");
                        isError = true;
                    }
                    else if (txtInsertStaffPassword.Text.Trim() == txtInsertStaffMasterKey.Text.Trim())
                    {
                        CallfrmErrorMessage();
                        lblInsertErrorMsgIsShow.Text = "true";
                        lblInsertStaffMasterKey.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("安全密碼不能與員工密碼相同!!");
                        isError = true;
                    }
                }
                else if (lblInvisibleStaffDataStatus.Text == "Update")
                {
                    if (txtInsertStaffMasterKey.Text.Trim().Length > 0)
                    {
                        if (txtInsertStaffMasterKey.Text.Trim().Length < 6)
                        {
                            CallfrmErrorMessage();
                            lblInsertErrorMsgIsShow.Text = "true";
                            lblInsertStaffMasterKey.ForeColor = Color.Red;
                            errorMsg.ShowErrorMessage("安全密碼請介於6~15個字元!!");
                            isError = true;
                        }
                        else if (txtInsertStaffPassword.Text.Trim() == txtInsertStaffMasterKey.Text.Trim())
                        {
                            CallfrmErrorMessage();
                            lblInsertErrorMsgIsShow.Text = "true";
                            lblInsertStaffMasterKey.ForeColor = Color.Red;
                            errorMsg.ShowErrorMessage("安全密碼不能與員工密碼相同!!");
                            isError = true;
                        }
                    }
                }
            }

            if (txtInsertStaffLaborCover.Text.Trim() == "")
                txtInsertStaffLaborCover.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtInsertStaffLaborCover.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStaffLaborCover.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("勞工保險只能為數字!!");
                    isError = true;
                }
            }

            if (txtInsertStaffHealthCover.Text.Trim() == "")
                txtInsertStaffHealthCover.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtInsertStaffHealthCover.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStaffHealthCover.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("健康保險只能為數字!!");
                    isError = true;
                }
            }

            if (txtInsertStaffGroupCover.Text.Trim() == "")
                txtInsertStaffGroupCover.Text = "0";
            else
            {
                if (!(bool)facade.FacadeFunctions("check", "number", txtInsertStaffGroupCover.Text.Trim(), null))
                {
                    CallfrmErrorMessage();
                    lblInsertErrorMsgIsShow.Text = "true";
                    lblInsertStaffGroupCover.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("團體保險只能為數字!!");
                    isError = true;
                }
            }

            return isError;
        }

        #endregion

        #endregion

        #region ErrorMsg Panel

        //private void SetErrorMsgDefault()
        //{
        //    panelErrorMsg.Visible = false;
        //    lblInvisibleErrorMsgCount.Text = "0";

        //    lblCreateNumTwo.Visible = false;
        //    lblCreateNumThree.Visible = false;
        //    lblCreateNumFour.Visible = false;
        //    lblCreateNumFive.Visible = false;
        //    lblCreateNumSix.Visible = false;
        //    lblCreateNumSeven.Visible = false;
        //    lblCreateNumEight.Visible = false;
        //    lblCreateNumNine.Visible = false;
        //    lblCreateNumTen.Visible = false;

        //    lblCreateMsgOne.Text = "";
        //    lblCreateMsgTwo.Text = "";
        //    lblCreateMsgThree.Text = "";
        //    lblCreateMsgFour.Text = "";
        //    lblCreateMsgFive.Text = "";
        //    lblCreateMsgSix.Text = "";
        //    lblCreateMsgSeven.Text = "";
        //    lblCreateMsgEight.Text = "";
        //    lblCreateMsgNine.Text = "";
        //    lblCreateMsgTen.Text = "";
        //}

        //private bool ShowErrorMessage(string errMsg)
        //{
        //    int errMsgCount = int.Parse(lblInvisibleErrorMsgCount.Text) + 1;
        //    bool msgIsFull = false;
        //    panelErrorMsg.Visible = true;

        //    switch (errMsgCount)
        //    {
        //        case 1:
        //            lblCreateMsgOne.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 2:
        //            lblCreateNumTwo.Visible = true;
        //            lblCreateMsgTwo.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 3:
        //            lblCreateNumThree.Visible = true;
        //            lblCreateMsgThree.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 4:
        //            lblCreateNumFour.Visible = true;
        //            lblCreateMsgFour.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 5:
        //            lblCreateNumFive.Visible = true;
        //            lblCreateMsgFive.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 6:
        //            lblCreateNumSix.Visible = true;
        //            lblCreateMsgSix.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 7:
        //            lblCreateNumSeven.Visible = true;
        //            lblCreateMsgSeven.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 8:
        //            lblCreateNumEight.Visible = true;
        //            lblCreateMsgEight.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 9:
        //            lblCreateNumNine.Visible = true;
        //            lblCreateMsgNine.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        case 10:
        //            lblCreateNumTen.Visible = true;
        //            lblCreateMsgTen.Text = errMsg;
        //            lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
        //            break;
        //        default:
        //            //lblInvisibleErrorMsgCount.Text = "0";
        //            msgIsFull = true;
        //            break;
        //    }

        //    return msgIsFull;
        //}

        #endregion

        #region Student Manage Class Panel

        //Manage By Person

        private void SetStudentManageClassDefault()
        {
            SetStudentManageClassSearchNewClassDefault();

            panelStudentManageClassByPersonShowClassInfo.Visible = false;
            panelStudentManageClassByClassShowClassInfo.Visible = false;
            panelStudentManageClassAddNewClassAddClass.Visible = false;

            btnStudentManageClassByPersonRemoveClass.Enabled = false;
            txtStudentManageClassSearchNewClass.Text = "";
            txtStudentManageClassSearchNewClass.Visible = false;
            btnStudentManageClassSearchNewClass.Enabled = false;
            btnStudentManageClassAddNewClass.Enabled = false;

            cboStudentManageClassSearchNewClassBy.SelectedIndex = -1;
        }

        private void SetStudentManageClassSearchNewClassDefault()
        {
            btnStudentManageClassSearchNewClass.Enabled = false;
            txtStudentManageClassSearchNewClass.Visible = false;
            txtStudentManageClassSearchNewClass.Text = "";

            if (dgvStudentManageClassJoinClass.Columns.Count > 0)
                dgvStudentManageClassJoinClass.Columns.Clear();

            if (dgvStudentManageClassNewClass.Columns.Count > 0)
                dgvStudentManageClassNewClass.Columns.Clear();

            if (dgvStudentManageClassByClassStudentList.Columns.Count > 0)
                dgvStudentManageClassByClassStudentList.Columns.Clear();
        }

        private void btnStudentAddNewClassSearchStudent_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text.IndexOf("全班") == -1)
                CallfrmSearchStudentData();
            else
                CallfrmSearchClassData();
        }

        public void ShowStudentInClassAmount()
        {
            if (dgvStudentManageClassJoinClass.Columns.Count > 0)
                dgvStudentManageClassJoinClass.Columns.Clear();

            facade = new FacadeLayer(SystemTypeForDB);

            if (tempStudentInClassSets == null)
            {
                if (Boolean.Parse(lblInvisibleDisplayEndClass.Text))
                    studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclasses", "StudentID", lblStudentManageClassShowStudentID.Text);
                else
                    studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclassesbyenddate", "StudentID", (object)int.Parse(lblStudentManageClassShowStudentID.Text));
            }
            else
                studentPaymentSets = tempStudentPaymentSets;

            lblStudentManageClassShowJoinClassNum.Text = "共 " + studentPaymentSets.Count + " 科目.";

            if (studentPaymentSets.Count > 0)
            {
                btnNewStudentClassPayment.Enabled = true;

                dgvStudentManageClassJoinClass.DataSource = studentPaymentSets;

                for (int i = 0; i < studentSelectClassDataGridViewHeaderText.Length; i++)
                    dgvStudentManageClassJoinClass.Columns[i].HeaderText = studentSelectClassDataGridViewHeaderText[i];

                dgvStudentManageClassJoinClass.Columns.Remove("StudentID");
                dgvStudentManageClassJoinClass.Columns.Remove("StudentName");

                dgvStudentManageClassJoinClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentManageClassJoinClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentManageClassJoinClass.AllowUserToAddRows = false;

                if (dgvStudentManageClassJoinClass.Rows.Count > 0)
                    dgvStudentManageClassJoinClass.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentManageClassJoinClass.Rows.Count; i++)
                    dgvStudentManageClassJoinClass.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentManageClassJoinClass.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvStudentManageClassJoinClass.Columns.Count; i++)
                {
                    dgvStudentManageClassJoinClass.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentManageClassJoinClass.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvStudentManageClassJoinClass.ReadOnly = true;
                }
            }
            else
            {
                if (lblCurrentPage.Text.IndexOf("05") > -1)
                {
                    ReturnToStudentSearch();
                    MessageBox.Show("此學生無需退費!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            StudentManageClassShowAllClass();
        }

        private void dgvStudentManageClassJoinClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblCurrentPage.Text.IndexOf("刪除") > -1)
                dgvStudentManageClassJoinClass_CellDoubleClick(sender, e);
        }

        private void dgvStudentManageClassJoinClass_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassJoinClass.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvStudentManageClassJoinClass.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentManageClassJoinClass.ReadOnly = false;
                dgvStudentManageClassJoinClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            //if (e.ColumnIndex > 0)
            //    dgvStudentManageClassJoinClass.ReadOnly = true;
            //else
            //{
            //    dgvStudentManageClassJoinClass.ReadOnly = false;
            //    dgvStudentManageClassJoinClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //}

            //int selectItem = 0;
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassJoinClass.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvStudentManageClassJoinClass.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvStudentManageClassJoinClass.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvStudentManageClassJoinClass.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvStudentManageClassJoinClass.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvStudentManageClassJoinClass.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvStudentManageClassJoinClass.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

            if (selectItem == 0)
                btnStudentManageClassByPersonRemoveClass.Enabled = false;
            else if (selectItem == 1)
                btnStudentManageClassByPersonRemoveClass.Enabled = true;
            else if (selectItem > 1)
                btnStudentManageClassByPersonRemoveClass.Enabled = false;
        }

        private void btnStudentManageClassRemoveClass_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("是否確定退選此課程?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    int countIndex = 0;

                    //List<StudentPaymentDefinition> tempStudentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclassesbyenddate", "StudentID", (object)int.Parse(lblStudentManageClassShowStudentID.Text));
                    List<StudentPaymentDefinition> tempStudentPaymentSets = new List<StudentPaymentDefinition>();
                    if (Boolean.Parse(lblInvisibleDisplayEndClass.Text))
                        tempStudentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclasses", "StudentID", lblStudentManageClassShowStudentID.Text);
                    else
                        tempStudentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclassesbyenddate", "StudentID", (object)int.Parse(lblStudentManageClassShowStudentID.Text));

                    foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassJoinClass.Rows)
                    {
                        //if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                        if (dgvRow.Selected)
                        {
                            facade.FacadeFunctions("updatedeleted", "studentinclass", (object)int.Parse(lblStudentManageClassShowStudentID.Text), (object)studentPaymentSets.ElementAt(countIndex).ClassID);

                            CreateSystemLogs(lblStudentManageClassShowStudentName.Text + "(" + lblStudentManageClassShowStudentID.Text + ")" + " 退選 " + dgvRow.Cells[2].Value.ToString() + "(" + studentPaymentSets.ElementAt(countIndex).ClassID + ")");
                            tempStudentPaymentSets.RemoveAt(countIndex);
                        }
                        countIndex++;
                    }

                    if (tempStudentPaymentSets != null)
                        studentPaymentSets = tempStudentPaymentSets;

                    ShowStudentInClassAmount();
                    btnStudentManageClassByPersonRemoveClass.Enabled = false;

                    MessageBox.Show("退選課程成功!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
            }
        }

        //Manage By Class

        public class FourValueStudent
        {
            public string StudentID { get; set; }
            public string StudentName { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
        }

        private void ShowStudentListByClass()
        {
            if (dgvStudentManageClassByClassStudentList.Columns.Count > 0)
                dgvStudentManageClassByClassStudentList.Columns.Clear();

            if (!bool.Parse(lblInvisibleDisplayEndClass.Text))
                studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclassesbyenddate", "ClassID", (object)lblStudentManageClassShowStudentID.Text);
            else
                studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclasses", "ClassID", (object)lblStudentManageClassShowStudentID.Text);

            if (studentPaymentSets != null && studentPaymentSets.Count > 0)
            {
                var newStudentSet = from s in studentPaymentSets
                                                select new FourValueStudent
                                                {
                                                    StudentID = s.StudentID,
                                                    StudentName = s.StudentName,
                                                    StartDate = s.StartDate,
                                                    EndDate = s.EndDate
                                                };
                dgvStudentManageClassByClassStudentList.DataSource = newStudentSet.ToList();
                dgvStudentManageClassByClassStudentList.Columns[0].HeaderText = "學生編號";
                dgvStudentManageClassByClassStudentList.Columns[1].HeaderText = "學生姓名";
                dgvStudentManageClassByClassStudentList.Columns[2].HeaderText = "上課日期";
                dgvStudentManageClassByClassStudentList.Columns[3].HeaderText = "結束日期";

                ////Add Student ID
                //DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
                //newColumn.HeaderText = "學生編號";
                //dgvStudentManageClassByClassStudentList.Columns.Add(newColumn);

                //newColumn = new DataGridViewTextBoxColumn();
                //newColumn.HeaderText = "學生姓名";
                //dgvStudentManageClassByClassStudentList.Columns.Add(newColumn);

                //newColumn = new DataGridViewTextBoxColumn();
                //newColumn.HeaderText = "上課日期";
                //dgvStudentManageClassByClassStudentList.Columns.Add(newColumn);

                //newColumn = new DataGridViewTextBoxColumn();
                //newColumn.HeaderText = "結束日期";
                //dgvStudentManageClassByClassStudentList.Columns.Add(newColumn);

                //foreach (var studentPaymentSingle in studentPaymentSets)
                //{
                //    DataGridViewRow newRow = new DataGridViewRow();
                //    DataGridViewCell newCell;

                //    newCell = new DataGridViewTextBoxCell();
                //    newCell.Value = studentPaymentSingle.StudentID;
                //    newRow.Cells.Add(newCell);

                //    newCell = new DataGridViewTextBoxCell();
                //    newCell.Value = studentPaymentSingle.StudentName;
                //    newRow.Cells.Add(newCell);

                //    newCell = new DataGridViewTextBoxCell();
                //    newCell.Value = studentPaymentSingle.StartDate;
                //    newRow.Cells.Add(newCell);

                //    newCell = new DataGridViewTextBoxCell();
                //    newCell.Value = studentPaymentSingle.EndDate;
                //    newRow.Cells.Add(newCell);

                //    dgvStudentManageClassByClassStudentList.Rows.Add(newRow);
                //}

                dgvStudentManageClassByClassStudentList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentManageClassByClassStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentManageClassByClassStudentList.AllowUserToAddRows = false;
                //CreateComboBoxWithEnums();

                if (dgvStudentManageClassByClassStudentList.Rows.Count > 0)
                    dgvStudentManageClassByClassStudentList.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentManageClassByClassStudentList.Rows.Count; i++)
                    dgvStudentManageClassByClassStudentList.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentManageClassByClassStudentList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvStudentManageClassByClassStudentList.Columns.Count; i++)
                {
                    dgvStudentManageClassByClassStudentList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentManageClassByClassStudentList.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvStudentManageClassByClassStudentList.ReadOnly = true;
                }

                StudentManageClassShowAllClass();
            }
            else
                MessageBox.Show("無學生選擇此課程!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvStudentManageClassByClassStudentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (lblCurrentPage.Text.IndexOf("刪除") == -1)
                dgvStudentManageClassByClassStudentList_CellDoubleClick(sender, e);
        }

        private void dgvStudentManageClassByClassStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassByClassStudentList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvStudentManageClassByClassStudentList.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentManageClassByClassStudentList.ReadOnly = false;
                dgvStudentManageClassByClassStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            //if (e.ColumnIndex > 0)
            //    dgvStudentManageClassByClassStudentList.ReadOnly = true;
            //else
            //{
            //    dgvStudentManageClassByClassStudentList.ReadOnly = false;
            //    dgvStudentManageClassByClassStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //}

            //int selectItem = 0;
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassByClassStudentList.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvStudentManageClassByClassStudentList.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvStudentManageClassByClassStudentList.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvStudentManageClassByClassStudentList.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvStudentManageClassByClassStudentList.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvStudentManageClassByClassStudentList.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvStudentManageClassByClassStudentList.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

            lblStudentManageClassShowSelectNumber.Text = "目前共選擇" + selectItem + "個學生";
        }

        private void btnStudentManageClassByClassRemoveClass_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("是否確定退選此課程?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    if (DateTime.Parse(classData.EndDate) >= DateTime.Now)
                    {
                        facade = new FacadeLayer(SystemTypeForDB);

                        facade.FacadeFunctions("updatedeleted", "studentinclassbyclassid", (object)lblStudentManageClassShowClassID.Text, null);
                        CreateSystemLogs("刪除 " + lblStudentManageClassShowClassName.Text + "(" + lblStudentManageClassShowClassID.Text + ")" + " 全班學生");

                        //facade.FacadeFunctions("updatedeleted", "class", (object)lblStudentManageClassShowClassID.Text, null);
                        //CreateSystemLogs("刪除 " + lblStudentManageClassShowClassName.Text + "(" + lblStudentManageClassShowClassID.Text + ")" + " 課程");

                        MessageBox.Show("全班刪除成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        studentData = null;
                        studentSets = null;
                        ReturnToStudentSearch();
                    }
                    else
                        MessageBox.Show("無法刪除此課程!! 因課程已結束!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
            }
        }

        //Search New Class

        private void cboStudentManageClassSearchNewClassBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            //SetStudentManageClassSearchNewClassDefault();

            if (cboStudentManageClassSearchNewClassBy.SelectedIndex > -1)
            {
                btnStudentManageClassSearchNewClass.Enabled = true;
                txtStudentManageClassSearchNewClass.Visible = true;
            }
        }

        private void btnStudentManageClassSearchNewClass_Click(object sender, EventArgs e)
        {
            if (cboStudentManageClassSearchNewClassBy.SelectedItem.ToString().IndexOf("課程") > -1)
            {
                ClassDefinition tempClassData = null;
                List<ClassDefinition> tempClassSets = null;

                facade = new FacadeLayer(SystemTypeForDB);
                string selectBy = null, selectData = null;

                if (cboStudentManageClassSearchNewClassBy.SelectedItem.ToString().IndexOf("全部") > -1)
                {
                    selectBy = "All";
                    selectData = "";
                }
                else if (cboStudentManageClassSearchNewClassBy.SelectedItem.ToString().IndexOf("編號") > -1)
                {
                    if (txtStudentManageClassSearchNewClass.Text.Trim().Length > 0)
                    {
                        if (txtStudentManageClassSearchNewClass.Text.Trim().Length <= 7)
                        {
                            selectBy = "ID";
                            selectData = txtStudentManageClassSearchNewClass.Text.Trim();
                        }
                        else
                            MessageBox.Show("課程編號格式錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("請輸入課程編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (txtStudentManageClassSearchNewClass.Text.Trim().Length > 0)
                    {
                        selectBy = "Name";
                        selectData = txtStudentManageClassSearchNewClass.Text.Trim();
                    }
                    else
                        MessageBox.Show("請輸入課程名稱!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (selectBy != null)
                {
                    string searchFunction = "";
                    if (!bool.Parse(lblInvisibleDisplayEndClass.Text))
                        searchFunction = "classbyenddate";
                    else
                        searchFunction = "classbyisdeletedbysearch";

                    if (selectBy == "ID")
                        tempClassData = (ClassDefinition)facade.FacadeFunctions("select", searchFunction, (object)selectBy, (object)selectData);
                    else
                        tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", searchFunction, (object)selectBy, (object)selectData);

                    if (tempClassData != null && tempClassData.ID != null)
                    {
                        tempClassSets = new List<ClassDefinition>();
                        tempClassSets.Add(tempClassData);

                        StudentManageClassShowClassList(tempClassSets);
                    }
                    else if (tempClassSets != null && tempClassSets.Count > 0)
                        StudentManageClassShowClassList(tempClassSets);
                    else
                        MessageBox.Show("查無此班級資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private List<ClassDefinition> GetStudentNewClassByStudent()
        {
            List<ClassDefinition> tempClassSets = null;
            if (!bool.Parse(lblInvisibleDisplayEndClass.Text))
                tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classbyenddate", "All", "");
            else
                tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classbyisdeleted", null, null);

            return tempClassSets;
        }

        public void StudentManageClassShowAllClass()
        {
            List<ClassDefinition> tempClassSets = null;
            tempClassSets = GetStudentNewClassByStudent();
            if (tempClassSets != null && tempClassSets.Count > 0)
                StudentManageClassShowClassList(tempClassSets);
        }

        public class TwoValueClass
        {
            public string classID { get; set; }
            public string className { get; set; }
        }

        private void StudentManageClassShowClassList(List<ClassDefinition> tempClassSets)
        {
            //if (dgvStudentManageClassNewClass != null)
            //    dgvStudentManageClassNewClass = new DataGridView();
            
            //if (dgvStudentManageClassNewClass.Rows.Count > 0)
            //    dgvStudentManageClassNewClass.Rows.Clear();

            //if (dgvStudentManageClassNewClass.Columns.Count > 0)
            //    dgvStudentManageClassNewClass.Columns.Clear();

            //dgvStudentManageClassNewClass.DataSource = null;
            //dgvStudentManageClassNewClass.BindingContext = null;
            //dgvStudentManageClassNewClass.Refresh();

            lblInvisibleChosenClassIDs.Text = "";

            bool isReady = false;
            bool removeAll = false;
            int countIndex = 0, selectIndex = -1;
            if (lblCurrentPage.Text.IndexOf("全班") == -1)
            {
                try
                {
                    int.Parse(lblStudentManageClassShowStudentID.Text);
                    isReady = true;
                }
                catch { isReady = false; }

                if (isReady)
                {
                    List<StudentPaymentDefinition> tempStudentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentselectclasses", "StudentID", int.Parse(lblStudentManageClassShowStudentID.Text));
                    if (tempStudentPaymentSets != null && tempStudentPaymentSets.Count > 0)
                    {
                        //string[] indexSet = new string[tempStudentPaymentSets.Count];
                        //foreach (var tempClassSingle in tempClassSets)
                        //{
                            foreach (var studentPaymentSingle in tempStudentPaymentSets)
                            {
                                //if (tempClassSingle.ID == studentPaymentSingle.ClassID)
                                //{
                                //    indexSet[++selectIndex] = countIndex.ToString();
                                //}
                                lblInvisibleChosenClassIDs.Text += studentPaymentSingle.ClassID + ";";
                            }
                            //countIndex++;
                        //}

                        //for (int i = indexSet.Length - 1; i >= 0; i--)
                        //{
                        //    if (indexSet[i] != null)
                        //    {
                        //        tempClassSets.RemoveAt(int.Parse(indexSet[i]));
                        //        removeAll = true;
                        //    }
                        //}
                    }
                }
            }
            else
            {
                isReady = true;
                //if (classData != null && classData.ID != null)
                //{
                //    string removeClassID = "";
                //    foreach (var tempClassSingle in tempClassSets)
                //    {
                //        if (tempClassSingle.ID == classData.ID)
                //            removeClassID += countIndex.ToString() + ",";
                //        else
                //        {
                //            facade = new FacadeLayer(SystemTypeForDB);
                //            if ((bool)facade.FacadeFunctions("check", "checkstudentinclassisrepeat", classData.ID, tempClassSingle.ID))
                //                removeClassID += countIndex.ToString() + ",";
                //        }

                //        countIndex++;
                //    }

                //    if (removeClassID != "")
                //    {
                //        string[] removeClassArray = removeClassID.Split(',');
                //        for (int i = removeClassArray.Count() - 2; i >= 0; i--)
                //            tempClassSets.RemoveAt(int.Parse(removeClassArray[i]));

                //        //if (selectIndex > -1)
                //        //    tempClassSets.RemoveAt(selectIndex);
                //    }
                //}
            }

            if (tempClassSets.Count > 0 && isReady)
            {
                //Add Student ID
                //DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
                //newColumn.HeaderText = "課程編號";
                //dgvStudentManageClassNewClass.Columns.Add(newColumn);

                //newColumn = new DataGridViewTextBoxColumn();
                //newColumn.HeaderText = "課程名稱";
                //dgvStudentManageClassNewClass.Columns.Add(newColumn);

                //foreach (var classSingle in tempClassSets)
                //{
                //    DataGridViewRow newRow = new DataGridViewRow();
                //    DataGridViewCell newCell;

                //    newCell = new DataGridViewTextBoxCell();
                //    newCell.Value = classSingle.ID;
                //    newRow.Cells.Add(newCell);

                //    newCell = new DataGridViewTextBoxCell();
                //    newCell.Value = classSingle.Name;
                //    newRow.Cells.Add(newCell);

                //    dgvStudentManageClassNewClass.Rows.Add(newRow);
                //}

                var newClassSet = from c in tempClassSets
                                              select new TwoValueClass
                                              {
                                                  classID = c.ID,
                                                  className = c.Name
                                              };
                dgvStudentManageClassNewClass.DataSource = newClassSet.ToList();
                dgvStudentManageClassNewClass.Columns[0].HeaderText = "課程編號";
                dgvStudentManageClassNewClass.Columns[1].HeaderText = "課程名稱";
                dgvStudentManageClassNewClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentManageClassNewClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentManageClassNewClass.AllowUserToAddRows = false;

                if (dgvStudentManageClassNewClass.Rows.Count > 0)
                    dgvStudentManageClassNewClass.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentManageClassNewClass.Rows.Count; i++)
                    dgvStudentManageClassNewClass.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentManageClassNewClass.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvStudentManageClassNewClass.Columns.Count; i++)
                {
                    dgvStudentManageClassNewClass.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentManageClassNewClass.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvStudentManageClassNewClass.ReadOnly = true;
                }
            }
            else
            {
                if (lblCurrentPage.Text.IndexOf("刪除") == -1 && isReady)
                {
                    if (removeAll)
                        MessageBox.Show("此學生已選此課程!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("查無此班級資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    tempClassSets = GetStudentNewClassByStudent();
                    StudentManageClassShowClassList(tempClassSets);
                }
            }
            //dgvStudentManageClassNewClass.Refresh();
            //panelStudentManageClassNewClassInfo.Controls.Add(dgvStudentManageClassNewClass);
        }

        private void dgvStudentManageClassNewClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvStudentManageClassNewClass_CellDoubleClick(sender, e);
        }

        private void dgvStudentManageClassNewClass_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            if (e.ColumnIndex >= 0)
            {
                foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassNewClass.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvStudentManageClassNewClass.ReadOnly = true;
                        selectItem++;
                    }
                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentManageClassNewClass.ReadOnly = false;
                dgvStudentManageClassNewClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            //if (e.ColumnIndex > 0)
            //    dgvStudentManageClassNewClass.ReadOnly = true;
            //else
            //{
            //    dgvStudentManageClassNewClass.ReadOnly = false;
            //    dgvStudentManageClassNewClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            //}

            //int selectItem = 0;
            //int dgvRowIndex = 0;
            //foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassNewClass.Rows)
            //{
            //    if (e.ColumnIndex == 0)
            //    {
            //        if (e.RowIndex == dgvRowIndex)
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvRow.Selected = false;
            //                dgvStudentManageClassNewClass.Rows[e.RowIndex].Selected = false;
            //            }
            //            else
            //            {
            //                dgvRow.Selected = true;
            //                dgvStudentManageClassNewClass.Rows[e.RowIndex].Selected = true;
            //                selectItem++;
            //            }
            //        else
            //            if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //            {
            //                dgvStudentManageClassNewClass.Rows[dgvRowIndex].Selected = true;
            //                selectItem++;
            //            }
            //            else
            //                dgvStudentManageClassNewClass.Rows[dgvRowIndex].Selected = false;
            //    }
            //    else
            //        if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //        {
            //            dgvStudentManageClassNewClass.Rows[dgvRowIndex].Selected = true;
            //            selectItem++;
            //        }
            //        else
            //            dgvStudentManageClassNewClass.Rows[dgvRowIndex].Selected = false;

            //    dgvRowIndex += 1;
            //}

            if (selectItem == 0)
                btnStudentManageClassAddNewClass.Enabled = false;
            else if (selectItem == 1)
                btnStudentManageClassAddNewClass.Enabled = true;
            else if (selectItem > 1)
                btnStudentManageClassAddNewClass.Enabled = false;
        }

        private void btnStudentManageClassAddNewClass_Click(object sender, EventArgs e)
        {
            try
            {

                int countIndex = 0;

                foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassNewClass.Rows)
                {
                    //if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
                    if (dgvRow.Selected)
                    {
                        //if (lblCurrentPage.Text.IndexOf("全班") == -1)
                        //{
                        if (lblInvisibleChosenClassIDs.Text.Contains(dgvRow.Cells[0].Value.ToString()))
                            MessageBox.Show("此學生已選此課程!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            CallfrmStudentAddNewClass(dgvRow.Cells[0].Value.ToString());
                        //studentInClassData = new StudentInClassDefinition(lblStudentManageClassShowStudentID.Text, dgvRow.Cells[1].Value.ToString(), 0);
                        //facade.FacadeFunctions("insert", "studentinclass", (object)studentInClassData, null);

                        //ShowStudentInClassAmount();

                        //CreateSystemLogs(lblStudentManageClassShowStudentName.Text + "(" + lblStudentManageClassShowStudentID.Text + ")" + " 加選 " + dgvRow.Cells[2].Value.ToString() + "(" + dgvRow.Cells[1].Value.ToString() + ")");
                        //}
                        //else
                        //{
                        //    DialogResult result = MessageBox.Show("是否確定加選此課程?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        //    if (result == DialogResult.Yes)
                        //    {
                        //        int dgvRowIndex = 0;
                        //        foreach (DataGridViewRow dgvRow1 in this.dgvStudentManageClassByClassStudentList.Rows)
                        //        {
                        //            if (!(bool)dgvRow1.Cells[0].Value)
                        //                studentSets.RemoveAt(dgvRowIndex);

                        //            dgvRowIndex++;
                        //        }

                        //        facade = new FacadeLayer(SystemTypeForDB);

                        //        facade.FacadeFunctions("insert", "studentinclassbyclass", (object)studentSets, (object)dgvRow.Cells[1].Value.ToString());

                        //        ReturnToStudentSearch();

                        //        CreateSystemLogs(lblStudentManageClassShowStudentName.Text + "(" + lblStudentManageClassShowStudentID.Text + ")" + " 全班加選 " + dgvRow.Cells[2].Value.ToString() + "(" + dgvRow.Cells[1].Value.ToString() + ")");

                        //        SetStudentManageClassSearchNewClassDefault();
                        //        cboStudentManageClassSearchNewClassBy.SelectedIndex = -1;

                        //        MessageBox.Show("加選課程成功!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    }
                        //}
                    }
                    countIndex++;
                }

            }
            catch
            {
            }
        }

        public void StudentManageClassAddNewClassByClass(ClassDefinition selectClassData)
        {
            int dgvRowIndex = 0;
            foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassByClassStudentList.Rows)
            {
                if (!dgvRow.Selected)
                    studentSets.RemoveAt(dgvRow.Index);

                dgvRowIndex++;
            }

            facade = new FacadeLayer(SystemTypeForDB);

            facade.FacadeFunctions("insert", "studentinclassbyclass", (object)studentSets, (object)selectClassData);

            CreateSystemLogs(lblStudentManageClassShowStudentName.Text + "(" + lblStudentManageClassShowStudentID.Text + ")" + " 全班加選 " + selectClassData.Name + "(" + selectClassData.ID + ")");
        }

        private void btnStudentManageClassReturnSearchPage_Click(object sender, EventArgs e)
        {
            ReturnToStudentSearch();
        }

        #endregion

        #region Student Payment Panel

        private void SetStudentPaymentDefault()
        {
            SetStudentPaymentButtonsDefault();

            lblStudentPaymentIsDoubleWorking.Text = "false";

            if (dgvStudentPaymentShowStudentUnpaidClass.Columns.Count > 0)
                dgvStudentPaymentShowStudentUnpaidClass.Columns.Clear();

            if (dgvStudentPaymentByClassShowStudentList.Columns.Count > 0)
                dgvStudentPaymentByClassShowStudentList.Columns.Clear();
        }

        private void SetStudentPaymentButtonsDefault()
        {
            btnStudentPaymentSinglePay.Enabled = false;
            btnStudentPaymentAllPay.Enabled = false;
            btnStudentPaymentPrepaid.Enabled = false;
        }

        //Payment By Person
        //Pay Money
        public void ShowsStudentNeedToPayAmount()
        {
            SetStudentPaymentButtonsDefault();

            if (dgvStudentPaymentShowStudentUnpaidClass.Columns.Count > 0)
                dgvStudentPaymentShowStudentUnpaidClass.Columns.Clear();

            facade = new FacadeLayer(SystemTypeForDB);
            studentInClassSets = null;

            string prePaid = facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null).ToString();
            lblStudentPaymentShowStudentUnpaidMoney.Text = "預收金額: " + prePaid + " 元, ";

            studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentpaymentlist", "StudentID", (object)lblStudentPaymentShowStudentID.Text);

            int totalPayment = 0;
            foreach (var studentPaymentSingle in studentPaymentSets)
                totalPayment += studentPaymentSingle.NeedToPay;

            lblStudentPaymentShowStudentUnpaidMoney.Text += "需繳總金額: " + totalPayment + " 元.";

            if (studentPaymentSets != null && studentPaymentSets.Count() > 0)
            {
                //if (dgvStudentPaymentShowStudentUnpaidClass.Columns.Count == 0 || dgvStudentPaymentShowStudentUnpaidClass.Columns[0].Name != "Check")
                //{
                //    // Initialize and add a check box column.
                //    DataGridViewColumn column = new DataGridViewCheckBoxColumn();
                //    column.Name = "Check";
                //    dgvStudentPaymentShowStudentUnpaidClass.Columns.Add(column);
                //    column.HeaderCell.Value = string.Empty;
                //}

                dgvStudentPaymentShowStudentUnpaidClass.DataSource = studentPaymentSets;

                //for (int i = 1; i <= studentPaymentDataGridViewHeaderText.Length; i++)
                //    dgvStudentPaymentShowStudentUnpaidClass.Columns[i].HeaderText = studentPaymentDataGridViewHeaderText[i - 1];

                //dgvStudentPaymentShowStudentUnpaidClass.Columns.Remove("EndDate");
                //dgvStudentPaymentShowStudentUnpaidClass.Columns.Remove("StartDate");

                for (int i = 0; i < studentPaymentDataGridViewHeaderText.Length; i++)
                    dgvStudentPaymentShowStudentUnpaidClass.Columns[i].HeaderText = studentPaymentDataGridViewHeaderText[i];

                if (lblCurrentPage.Text.IndexOf("全班") == -1)
                {
                    dgvStudentPaymentShowStudentUnpaidClass.Columns.Remove("StudentName");
                    dgvStudentPaymentShowStudentUnpaidClass.Columns.Remove("StudentID");
                }

                dgvStudentPaymentShowStudentUnpaidClass.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentPaymentShowStudentUnpaidClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentPaymentShowStudentUnpaidClass.AllowUserToAddRows = false;
                //CreateComboBoxWithEnums();

                if (dgvStudentPaymentShowStudentUnpaidClass.Rows.Count > 0)
                    dgvStudentPaymentShowStudentUnpaidClass.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentPaymentShowStudentUnpaidClass.Rows.Count; i++)
                    dgvStudentPaymentShowStudentUnpaidClass.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentPaymentShowStudentUnpaidClass.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                dgvStudentPaymentShowStudentUnpaidClass.Columns["HavePaid"].DefaultCellStyle.BackColor = Color.SkyBlue;
                dgvStudentPaymentShowStudentUnpaidClass.Columns["HavePaid"].DefaultCellStyle.ForeColor = Color.Black;

                //dgvStudentPaymentShowStudentUnpaidClass.Columns[0].Width = 20;
                for (int i = 0; i < dgvStudentPaymentShowStudentUnpaidClass.Columns.Count; i++)
                {
                    dgvStudentPaymentShowStudentUnpaidClass.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentPaymentShowStudentUnpaidClass.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvStudentPaymentShowStudentUnpaidClass.ReadOnly = true;
                }

                if (studentPaymentSets.Count() > 1)
                    btnStudentPaymentAllPay.Enabled = true;
                else
                    btnStudentPaymentAllPay.Enabled = false;
            }
            else
            {
                btnStudentPaymentPrepaid.Enabled = true;

                if (lblStudentPaymentFromPage.Text == "StudentRefund")
                {
                    MessageBox.Show("此學生無未付費課程!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefurnStudentRefundFromStudentPayment();
                }
            }
        }

        private void dgvStudentPaymentShowStudentUnpaidClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvStudentPaymentShowStudentUnpaidClass_CellDoubleClick(sender, e);
        }

        private void dgvStudentPaymentShowStudentUnpaidClass_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;
            
            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                dgvRowIndex = 0;
                foreach (DataGridViewRow dgvRow in this.dgvStudentPaymentShowStudentUnpaidClass.Rows)
                {
                    //lblStudentPaymentIsDoubleWorking.Text = "true";
                    if (dgvRow.Selected)
                    {
                        dgvRow.Selected = true;
                        dgvStudentPaymentShowStudentUnpaidClass.ReadOnly = true;
                        dgvStudentPaymentShowStudentUnpaidClass.Rows[e.RowIndex].Selected = true;
                        selectItem++;
                    }
                    else
                        dgvStudentPaymentShowStudentUnpaidClass.Rows[dgvRowIndex].Selected = false;

                    dgvRowIndex += 1;
                }

                if (e.ColumnIndex == 7)
                {
                    if (lblStudentPaymentIsDoubleWorking.Text == "false")
                    {
                        lblStudentPaymentIsDoubleWorking.Text = "true";
                        btnStudentPaymentSinglePay.Enabled = false;
                        btnStudentPaymentAllPay.Enabled = false;
                        dgvStudentPaymentShowStudentUnpaidClass.Enabled = false;
                        CallfrmPaymentDiscount(studentPaymentSets.ElementAt(e.RowIndex), "Student");
                    }
                }
                //else
                //{
                //    if (lblStudentPaymentIsDoubleWorking.Text == "false")
                //    {
                //        dgvRowIndex = 0;
                //        foreach (DataGridViewRow dgvRow in this.dgvStudentPaymentShowStudentUnpaidClass.Rows)
                //        {
                //            //lblStudentPaymentIsDoubleWorking.Text = "true";
                //            if (dgvRow.Selected)
                //            {
                //                dgvRow.Selected = true;
                //                dgvStudentPaymentShowStudentUnpaidClass.ReadOnly = true;
                //                dgvStudentPaymentShowStudentUnpaidClass.Rows[e.RowIndex].Selected = true;
                //                selectItem++;
                //            }
                //            else
                //                dgvStudentPaymentShowStudentUnpaidClass.Rows[dgvRowIndex].Selected = false;

                //            dgvRowIndex += 1;
                //        }

                //        //foreach (DataGridViewRow dgvRow in this.dgvStudentManageClassNewClass.Rows)
                //        //{
                //        //    if (dgvRow.Selected)
                //        //    {
                //        //        dgvStudentManageClassNewClass.ReadOnly = true;
                //        //        selectItem++;
                //        //    }
                //        //    dgvRowIndex += 1;
                //        //}
                //    }
                //}
            }
            else
            {
                dgvStudentPaymentShowStudentUnpaidClass.ReadOnly = false;
                dgvStudentPaymentShowStudentUnpaidClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            dgvRowIndex = 0;

            if (selectItem == 0)
                btnStudentPaymentSinglePay.Enabled = false;
            else if (selectItem >= 1)
                btnStudentPaymentSinglePay.Enabled = true;
            //else if (selectItem > 1)
            //    btnStudentPaymentSinglePay.Enabled = false;
        }

        private void btnStudentPaymentSinglePay_Click(object sender, EventArgs e)
        {
            StudentPaymentShowPayMoney();
        }

        private void btnStudentPaymentAllPay_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dgvRow in this.dgvStudentPaymentShowStudentUnpaidClass.Rows)
            {
                dgvRow.Selected = true;
                //dgvRow.Cells[0].Value = true;
                //selectIndex++;
            }

            //lblInvisibleStudentPaymentSelectNumber.Text = selectIndex.ToString();

            //btnStudentPaymentSinglePay.Enabled = true;
            //btnStudentPaymentCancelAll.Enabled = true;
            //btnStudentPaymentDiscount.Enabled = false;
            //txtStudentPaymentDiscountMoney.Enabled = false;
            btnStudentPaymentSinglePay.Enabled = false;
            btnStudentPaymentAllPay.Enabled = false;
            StudentPaymentShowPayMoney();
        }

        private void StudentPaymentShowPayMoney()
        {
            List<StudentPaymentDefinition> tempStudentPaymentSet = new List<StudentPaymentDefinition>();
            int countIndex = 0;
            foreach (DataGridViewRow dgvRow in this.dgvStudentPaymentShowStudentUnpaidClass.Rows)
            {
                if (dgvRow.Selected)
                    tempStudentPaymentSet.Add(studentPaymentSets.ElementAt(countIndex));

                countIndex++;
            }

            if (tempStudentPaymentSet.Count > 0)
                CallfrmStudentPayment(tempStudentPaymentSet, lblStudentPaymentFromPage.Text, lblStudentRefundIndex.Text, lblStudentRefundShowRefundMoney.Text);
            else
                MessageBox.Show("請選擇繳費課程!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //SetStudentPaymentPanelTextDefault();
            //panelStudentPaymentManagementPage.Visible = true;
            //panelStudentPaymentPayMoneyPage.Visible = true;

            //if (lblStudentPaymentFromPage.Text == "StudentRefund")
            //    panelStudentPaymentPayRefund.Visible = true;

            //int totalPrice = 0, totalDiscount = 0, totalHavePaid = 0, countIndex = 0;
            //int prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null).ToString());

            //foreach (DataGridViewRow dgvRow in this.dgvStudentPaymentShowStudentUnpaidClass.Rows)
            //{
            //    if (dgvRow.Cells[0].Value != null && bool.Parse(dgvRow.Cells[0].Value.ToString()))
            //    {
            //        totalPrice += studentPaymentSets.ElementAt(countIndex).ClassPrice + studentPaymentSets.ElementAt(countIndex).ClassMaterialFee;
            //        totalDiscount += studentPaymentSets.ElementAt(countIndex).Discount;
            //        totalHavePaid += studentPaymentSets.ElementAt(countIndex).HavePaid;

            //        lblStudentPaymentDiscountShowClassID.Text = studentPaymentSets.ElementAt(countIndex).ClassID;
            //        lblStudentPaymentDiscountShowClassName.Text = studentPaymentSets.ElementAt(countIndex).ClassName;
            //        lblStudentPaymentDiscountShowClassPrice.Text = studentPaymentSets.ElementAt(countIndex).ClassPrice.ToString();
            //        lblStudentPaymentDiscountShowClassMaterialFee.Text = studentPaymentSets.ElementAt(countIndex).ClassMaterialFee.ToString();
            //    }

            //    countIndex++;
            //}

            //lblStudentPaymentShowNeedToPayMoney.Text = totalPrice.ToString();
            //lblStudentPaymentShowHavePaidMoney.Text = totalHavePaid.ToString();
            //lblStudentPaymentShowPrepaidMoney.Text = prePaid.ToString();
            //lblStudentPaymentShowPaymentMoney.Text = (totalPrice - totalDiscount - totalHavePaid - prePaid).ToString();
            //lblStudentPaymentPayRefundShowPaymentMoney.Text = (totalPrice - totalDiscount - totalHavePaid - prePaid).ToString(); //From Student Refund
            //lblInvisibleStudentPaymentDiscount.Text = totalDiscount.ToString();
            //lblStudentPaymentDiscountShowOriginalDiscount.Text = totalDiscount.ToString();
            //txtStudentPaymentDiscountMoney.Text = totalDiscount.ToString();

            ////From Student Refund
            ////if (lblStudentPaymentFromPage.Text == "StudentRefund")
            ////{
            ////    txtStudentPaymentPayRefundInputPayMoney.Enabled = true;
            ////    cboStudentPaymentPayRefundPaymentType.Enabled = true;

            ////    if (int.Parse(lblStudentPaymentPayRefundShowPaymentMoney.Text) <= int.Parse(lblStudentPaymentPayRefundShowRefundMoney.Text))
            ////    {
            ////        txtStudentPaymentPayRefundInputPayMoney.Enabled = false;
            ////        cboStudentPaymentPayRefundPaymentType.Enabled = false;
            ////    }
            ////}
        }

        public void AfterStudentPayment()
        {
            SetStudentPaymentDefault();
            panelStudentPaymentManagementPage.Visible = true;

            if (lblStudentPaymentFromPage.Text == "StudentRefund")
                RefurnStudentRefundFromStudentPayment();
            else
                ShowsStudentNeedToPayAmount();
        }

        private void btnStudentPaymentHistory_Click(object sender, EventArgs e)
        {
            facade = new FacadeLayer(SystemTypeForDB);

            classPaymentSets = (List<ClassPaymentDefinition>)facade.FacadeFunctions("select", "studentpaymentrecordtopsix", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null);

            if (classPaymentSets != null && classPaymentSets.Count > 0)
            {
                btnStudentPaymentHistory.Enabled = false;
                CallfrmStudentPaymentHistory(classPaymentSets);
            }
            else
                MessageBox.Show("尚無相關繳費記錄!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Others
        //Prepaid

        private void btnStudentPaymentPrepaid_Click(object sender, EventArgs e)
        {
            //SetStudentPaymentPanelTextDefault();
            //panelStudentPaymentManagementPage.Visible = true;
            //panelStudentPaymentPrepaidPage.Visible = true;

            //int prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null).ToString());
            //lblStudentPaymentPrepaidShowCurrentPrepaid.Text = prePaid.ToString();
            CallfrmStudentPrepaid(lblStudentPaymentShowStudentID.Text, lblStudentPaymentShowStudentName.Text);
            //btnStudentPaymentPrepaid.Enabled = false;
        }

        //Payment By Class

        public void ShowsClassNeedToPayStudentList()
        {
            SetStudentPaymentButtonsDefault();

            facade = new FacadeLayer(SystemTypeForDB);

            studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentpaymentlist", "ClassID", (object)lblStudentPaymentShowStudentID.Text);

            if (studentPaymentSets.Count() > 0)
            {
                int totalPrice = 0, totalDiscount = 0, totalHavePaid = 0;

                foreach (var studentPaymentSingle in studentPaymentSets)
                {
                    totalPrice = totalPrice + studentPaymentSingle.ClassPrice + studentPaymentSingle.ClassMaterialFee + studentPaymentSingle.ClassApplyFee;
                    totalDiscount += studentPaymentSingle.Discount;
                    totalHavePaid += studentPaymentSingle.HavePaid;
                }

                lblStudentPaymentShowClassPrice.Text = totalPrice.ToString();
                lblStudentPaymentShowClassSeat.Text = studentPaymentSets.Count().ToString();
                lblStudentPaymentShowDiscount.Text = totalDiscount.ToString();
                lblStudentPaymentShowHavePaid.Text = totalHavePaid.ToString();
                lblStudentPaymentByClassShowPaymentMoney.Text = (totalPrice - totalDiscount - totalHavePaid).ToString();

                if (dgvStudentPaymentByClassShowStudentList.Columns.Count > 0)
                    dgvStudentPaymentByClassShowStudentList.Columns.Clear();

                dgvStudentPaymentByClassShowStudentList.DataSource = studentPaymentSets;

                for (int i = 0; i < studentPaymentDataGridViewHeaderText.Length; i++)
                    dgvStudentPaymentByClassShowStudentList.Columns[i].HeaderText = studentPaymentDataGridViewHeaderText[i];

                dgvStudentPaymentByClassShowStudentList.Columns.Remove("EndDate");
                dgvStudentPaymentByClassShowStudentList.Columns.Remove("StartDate");

                if (lblCurrentPage.Text.IndexOf("全班") >= -1)
                {
                    dgvStudentPaymentByClassShowStudentList.Columns.Remove("ClassName");
                    dgvStudentPaymentByClassShowStudentList.Columns.Remove("ClassID");
                }

                dgvStudentPaymentByClassShowStudentList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentPaymentByClassShowStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentPaymentByClassShowStudentList.AllowUserToAddRows = false;

                if (dgvStudentPaymentByClassShowStudentList.Rows.Count > 0)
                    dgvStudentPaymentByClassShowStudentList.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentPaymentByClassShowStudentList.Rows.Count; i++)
                    dgvStudentPaymentByClassShowStudentList.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentPaymentByClassShowStudentList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                //dgvStudentPaymentByClassShowStudentList.Columns[0].Width = 20;
                for (int i = 0; i < dgvStudentPaymentByClassShowStudentList.Columns.Count; i++)
                {
                    dgvStudentPaymentByClassShowStudentList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentPaymentByClassShowStudentList.Columns[i].Resizable = DataGridViewTriState.False;
                }

            }
            else
            {
                ReturnToStudentSearch();

                MessageBox.Show("本班級無學生需要繳費!!!", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvStudentPaymentByClassShowStudentList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvStudentPaymentByClassShowStudentList_CellDoubleClick(sender, e);
        }

        private void dgvStudentPaymentByClassShowStudentList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;

            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                dgvRowIndex = 0;
                foreach (DataGridViewRow dgvRow in this.dgvStudentPaymentByClassShowStudentList.Rows)
                {
                    if (dgvRow.Selected)
                    {
                        dgvRow.Selected = true;
                        dgvStudentPaymentByClassShowStudentList.ReadOnly = true;
                        dgvStudentPaymentByClassShowStudentList.Rows[e.RowIndex].Selected = true;
                        selectItem++;
                    }
                    else
                        dgvStudentPaymentByClassShowStudentList.Rows[dgvRowIndex].Selected = false;

                    dgvRowIndex += 1;
                }

                if (e.ColumnIndex == 5)
                {
                    if (lblStudentPaymentIsDoubleWorking.Text == "false")
                    {
                        lblStudentPaymentIsDoubleWorking.Text = "true";
                        dgvStudentPaymentShowStudentUnpaidClass.Enabled = false;
                        CallfrmPaymentDiscount(studentPaymentSets.ElementAt(e.RowIndex), "Class");
                    }
                }
            }
            else
            {
                dgvStudentPaymentByClassShowStudentList.ReadOnly = false;
                dgvStudentPaymentByClassShowStudentList.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }
        }

        private void btnStudentPaymentByClassPayMoney_Click(object sender, EventArgs e)
        {
            facade = new FacadeLayer(SystemTypeForDB);

            if (txtStudentPaymentByClassPayMoney.Text != "")
            {
                if ((bool)facade.FacadeFunctions("check", "number", (object)txtStudentPaymentByClassPayMoney.Text, null))
                {
                    if (int.Parse(txtStudentPaymentByClassPayMoney.Text) > 0)
                    {
                        if (int.Parse(txtStudentPaymentByClassPayMoney.Text) == int.Parse(lblStudentPaymentByClassShowPaymentMoney.Text))
                        {
                            DialogResult result = MessageBox.Show("是否確定繳費?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                                StudentClassPaymentPayMoneyByClass();
                        }
                        else
                            MessageBox.Show("繳費金額必需等於實繳金額!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("繳費金額必需大於零!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("繳費金額只能為數字!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("請輸入繳費金額!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StudentClassPaymentPayMoneyByClass()
        {
            facade = new FacadeLayer(SystemTypeForDB);
            studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studentpaymentlist", "ClassID", (object)lblStudentPaymentShowStudentID.Text);

            int classPrice = 0, discount = 0, havePaid = 0, payment = 0;

            foreach (var studentPaymentSingle in studentPaymentSets)
            {

                classPrice = studentPaymentSingle.ClassPrice + studentPaymentSingle.ClassMaterialFee;
                discount = studentPaymentSingle.Discount;
                havePaid = studentPaymentSingle.HavePaid;
                payment = classPrice - discount - havePaid;

                string studentInClassID = facade.FacadeFunctions("select", "studentinclassid", (object)studentPaymentSingle.StudentID, (object)lblStudentPaymentShowStudentID.Text).ToString();
                ClassPaymentDefinition classPayment = new ClassPaymentDefinition(0, studentPaymentSingle.StudentID, studentPaymentSingle.StudentName,
                                                                                 lblStudentPaymentShowStudentID.Text, lblStudentPaymentShowStudentName.Text, studentInClassID, 0,
                                                                                 lblInvisibleStaffEnglishName.Text, "", "", payment,
                                                                                 facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString(), "現金");

                facade.FacadeFunctions("insert", "studentclasspayment", (object)classPayment, true);

                CreateSystemLogs(studentPaymentSingle.StudentName + "(" + studentPaymentSingle.StudentID + ")" + " 繳交 " + studentPaymentSingle.ClassName + "(" + lblStudentPaymentShowStudentID.Text + ")" + " 費用, 共 " + payment.ToString() + " 元");
            }

            ReturnToStudentSearch();

            MessageBox.Show("學生付費完成!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStudentPaymentReturnStudentSearchPage_Click(object sender, EventArgs e)
        {
            if (lblCurrentPage.Text.IndexOf("01") > -1)
                SetNewStudentAddNewClass(lblStudentPaymentShowStudentID.Text, lblStudentPaymentShowStudentName.Text);
            else if (lblCurrentPage.Text.IndexOf("02") > -1)
                SetNewStudentAddNewClass(lblStudentPaymentShowStudentID.Text, lblStudentPaymentShowStudentName.Text);
            else if (lblStudentPaymentFromPage.Text == "StudentRefund")
                RefurnStudentRefundFromStudentPayment();
            else
                ReturnToStudentSearch();
        }

        #endregion

        #region Student Refund Panel

        private void SetStudentRefundDefault()
        {
            lblStudentRefundShowHavePaid.Text = "0";
            lblStudentRefundShowHaveRefunded.Text = "0";
            txtStudentRefundDiscount.Text = "";
            cboStudentRefundRefundType.SelectedIndex = -1;

            txtStudentRefundReceiver.Text = "";
            lblStudentRefundReceiver.Visible = false;
            txtStudentRefundReceiver.Visible = false;

            lblStudentRefundID.Text = "0";
            lblStudentRefundIndex.Text = "0";
            lblStudentRefundTempRefundMoney.Text = "0";
            lblStudentRefundShowHaveRefunded.Text = "0";

            if (dgvStudentRefundClassList.Columns.Count > 0)
                dgvStudentRefundClassList.Columns.Clear();
        }

        private bool SetStudentRefunddgvData()
        {
            int havePaid = 0;

            if (studentPaymentSets.Count > 0)
            {
                panelStudentRefund.Enabled = true;

                foreach (var studentPaymentSingle in studentPaymentSets)
                    havePaid += studentPaymentSingle.HavePaid;

                //Refund By Person
                lblStudentRefundShowHavePaid.Text = havePaid.ToString();
                txtStudentRefundDiscount.Text = "0";

                //Refund By Class
                lblStudentRefundShowHavePaidPeople.Text = studentPaymentSets.Count().ToString();
                lblStudentRefundShowHavePaidMoney.Text = havePaid.ToString();
                lblStudentRefundShowRefundByClass.Text = havePaid.ToString();

                dgvStudentRefundClassList.Visible = true;

                dgvStudentRefundClassList.DataSource = studentPaymentSets;

                for (int i = 0; i < studentPaymentDataGridViewHeaderText.Length; i++)
                    dgvStudentRefundClassList.Columns[i].HeaderText = studentPaymentDataGridViewHeaderText[i];

                dgvStudentRefundClassList.Columns.Remove("StartDate");
                dgvStudentRefundClassList.Columns.Remove("EndDate");

                dgvStudentRefundClassList.Columns.Remove("NeedToPay");
                if (lblCurrentPage.Text.IndexOf("全班") == -1)
                {
                    dgvStudentRefundClassList.Columns.Remove("StudentID");
                    dgvStudentRefundClassList.Columns.Remove("StudentName");
                    lblStudentRefundClassList.Text = "課程清單:";
                }
                else
                {
                    dgvStudentRefundClassList.Columns.Remove("ClassID");
                    dgvStudentRefundClassList.Columns.Remove("ClassName");
                    lblStudentRefundClassList.Text = "學生清單:";
                }

                dgvStudentRefundClassList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvStudentRefundClassList.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvStudentRefundClassList.AllowUserToAddRows = false;

                if (dgvStudentRefundClassList.Rows.Count > 0)
                    dgvStudentRefundClassList.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvStudentRefundClassList.Rows.Count; i++)
                    dgvStudentRefundClassList.Rows[i].Resizable = DataGridViewTriState.False;
                dgvStudentRefundClassList.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                //dgvStudentRefundClassList.Columns[0].Width = 20;
                for (int i = 0; i < dgvStudentRefundClassList.Columns.Count; i++)
                {
                    dgvStudentRefundClassList.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvStudentRefundClassList.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvStudentRefundClassList.ReadOnly = true;
                }

                return true;
            }
            else
            {
                DefaultSetting();
                panelSubButtons.Visible = true;
                panelStudentRefund.Visible = true;

                return false;
            }
        }

        //Refund By Person

        private void ShowsStudentRefundClassAmount()
        {
            facade = new FacadeLayer(SystemTypeForDB);

            studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundlist", (object)"StudentID", (object)int.Parse(lblStudentRefundShowStudentID.Text));

            if (!SetStudentRefunddgvData())
            {
                MessageBox.Show("此學生無需退費!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                SearchAgain();
            }
        }

        private void txtStudentRefundDiscount_TextChanged(object sender, EventArgs e)
        {
            bool ableToCount = false;
            if (txtStudentRefundDiscount.Text == "")
            {
                ableToCount = true;
                txtStudentRefundDiscount.Text = "0";
            }
            else
            {
                facade = new FacadeLayer(SystemTypeForDB);
                if ((bool)facade.FacadeFunctions("check", "number", (object)txtStudentRefundDiscount.Text, null))
                {
                    if (int.Parse(txtStudentRefundDiscount.Text) >= 0)
                        ableToCount = true;
                    else
                        MessageBox.Show("退費折扣不得小於零!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("退費折扣只能為數字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (ableToCount)
            {
                if (!(bool)facade.FacadeFunctions("check", "number", (object)lblStudentRefundShowHavePaid.Text, null))
                    lblStudentRefundShowHavePaid.Text = "0";
                if (!(bool)facade.FacadeFunctions("check", "number", (object)lblStudentRefundShowHaveRefunded.Text, null))
                    lblStudentRefundShowHaveRefunded.Text = "0";

                if (int.Parse(lblStudentRefundShowHavePaid.Text) - int.Parse(txtStudentRefundDiscount.Text) - int.Parse(lblStudentRefundShowHaveRefunded.Text) >= 0)
                    lblStudentRefundShowRefundMoney.Text = (int.Parse(lblStudentRefundShowHavePaid.Text) - int.Parse(lblStudentRefundShowHaveRefunded.Text) -
                                                            int.Parse(txtStudentRefundDiscount.Text)).ToString();
                else
                {
                    if (int.Parse(lblStudentRefundShowHavePaid.Text) > 0)
                        MessageBox.Show("退費折扣不得大於已繳費用!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {

                    }
                }
            }
        }

        private void cboStudentRefundRefundType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblStudentRefundReceiver.Visible = false;
            txtStudentRefundReceiver.Visible = false;

            if (cboStudentRefundRefundType.SelectedIndex > -1)
            {
                //lblStudentRefundEvent.Text = cboStudentRefundRefundType.SelectedItem.ToString();
                if (cboStudentRefundRefundType.SelectedItem.ToString().IndexOf("現金") > -1)
                {
                    lblStudentRefundReceiver.Visible = true;
                    txtStudentRefundReceiver.Visible = true;
                }
            }
        }

        private void btnStudentRefundByPerson_Click(object sender, EventArgs e)
        {
            if (cboStudentRefundRefundType.SelectedIndex > -1)
            {
                DialogResult result = MessageBox.Show("是否確定退費?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    string receiver = "";
                    facade = new FacadeLayer(SystemTypeForDB);

                    if (cboStudentRefundRefundType.SelectedItem.ToString().IndexOf("現金") > -1)
                    {
                        if (txtStudentRefundReceiver.Text != "")
                        {
                            receiver = txtStudentRefundReceiver.Text;
                            StudentRefundSetEvent(lblStudentRefundShowRefundMoney.Text, cboStudentRefundRefundType.SelectedItem.ToString(), receiver);
                        }
                        else
                            MessageBox.Show("請輸入收取退費者姓名!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (cboStudentRefundRefundType.SelectedItem.ToString().IndexOf("新課") > -1)
                    {
                        receiver = txtStudentRefundReceiver.Text;
                        StudentRefundPayClassPayment(lblStudentRefundShowStudentID.Text, lblStudentRefundShowStudentName.Text, lblStudentRefundShowRefundMoney.Text);
                        btnReturnSearchPage.Visible = true;
                        //btnStudentPaymentReturnStudentSearchPage.Visible = true;
                        SetRefundInfo();
                        //StudentRefundSetEvent(receiver);
                    }
                    else if (cboStudentRefundRefundType.SelectedItem.ToString().IndexOf("帳戶") > -1)
                    {
                        int prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentRefundShowStudentID.Text), null).ToString());
                        facade.FacadeFunctions("update", "prepaid", (object)lblStudentRefundShowStudentID.Text, (object)(prePaid + int.Parse(lblStudentRefundShowRefundMoney.Text)).ToString());

                        studentPrepaidData = new StudentPrepaidDefinition(lblStudentRefundShowStudentID.Text,
                                                                          facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString(),
                                                                          int.Parse(lblStudentRefundShowRefundMoney.Text), 0,
                                                                          StaticFunction.SetEncodingString("個人退費"));

                        facade.FacadeFunctions("insert", "studentprepaid", (object)studentPrepaidData, null);

                        StudentRefundSetEvent(lblStudentRefundShowRefundMoney.Text, cboStudentRefundRefundType.SelectedItem.ToString(), receiver);
                    }
                }
            }
            else
                MessageBox.Show("請選擇退費方式!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void SetRefundInfo()
        {
            string[] refundInfo = new string[6];
            refundInfo[0] = lblStudentRefundIndex.Text;
            refundInfo[1] = lblStudentRefundEvent.Text;
            refundInfo[2] = lblStudentRefundShowRefundMoney.Text;
            refundInfo[3] = lblStudentRefundTempRefundMoney.Text;
            refundInfo[4] = txtStudentRefundDiscount.Text;
            refundInfo[5] = lblStudentRefundShowStudentID.Text;

            facade = new FacadeLayer(SystemTypeForDB);
            facade.FacadeFunctions("reusefunction", "setstudentrefund", (object)refundInfo, null);
        }

        private void GetRefundInfo()
        {
            facade = new FacadeLayer(SystemTypeForDB);
            string[] refundInfo = (string[])facade.FacadeFunctions("reusefunction", "getstudentrefund", null, null);

            lblStudentRefundIndex.Text = refundInfo[0];
            lblStudentRefundEvent.Text = refundInfo[1];
            lblStudentRefundShowRefundMoney.Text = refundInfo[2];
            lblStudentRefundTempRefundMoney.Text = refundInfo[3];
            lblStudentRefundShowHaveRefunded.Text = lblStudentRefundTempRefundMoney.Text;
            txtStudentRefundDiscount.Text = refundInfo[4];
            lblStudentRefundShowStudentID.Text = refundInfo[5];
        }

        public void StudentRefundSetEvent(string refund, string refundType, string receiver)
        {
            if (!(bool)facade.FacadeFunctions("check", "number", (object)lblStudentRefundIndex.Text, null))
            {
                SetStudentRefundDefault();
                GetRefundInfo();
                ShowsStudentRefundClassAmount();
                cboStudentRefundRefundType.SelectedIndex = 2;
            }

            if (lblStudentRefundIndex.Text == "0")
            {
                lblStudentRefundEvent.Text = lblStudentRefundShowRefundMoney.Text;
                lblStudentRefundIndex.Text = "1";
            }

            if (lblStudentRefundIndex.Text != "0")
            {
                int refundIndex = int.Parse(lblStudentRefundIndex.Text);
                lblStudentRefundEvent.Text += ";" + refund + ", " + receiver + ", " + refundType;
                lblStudentRefundIndex.Text = (refundIndex++).ToString();
                lblStudentRefundTempRefundMoney.Text = (int.Parse(lblStudentRefundTempRefundMoney.Text) + int.Parse(refund)).ToString();
                lblStudentRefundShowHaveRefunded.Text = lblStudentRefundTempRefundMoney.Text;
            }

            if (lblStudentRefundTempRefundMoney.Text == (int.Parse(lblStudentRefundShowHavePaid.Text) - int.Parse(txtStudentRefundDiscount.Text)).ToString())
                StudentRefundPayRefund();
            else
                SetRefundInfo();
        }

        /*  Student Refund 
         * 
         *  When SubID = 0, It records how much money totally has been refunded
         *  When SubID > 0, It records how it been refunded and how much it refunded, such as when SubID = 1, it refund by Paying other classes
         *                                                                                    when SubID = 2, it refund by Cash
         *  After this, It will record which withdrew classes are refunded
         *  
         */
        private void StudentRefundPayRefund()
        {
            int discount = 0, refundID = 0;
            string today = facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString();

            string[] refundEvent = lblStudentRefundEvent.Text.Split(';');

            for (int i = 0; i < refundEvent.Length; i++)
            {
                string[] singleEvent = refundEvent[i].Split(',');
                string refundMoney = "", receiver = "", refundType = "";

                refundMoney = singleEvent[0];
                if (i == 0)
                    discount = int.Parse(txtStudentRefundDiscount.Text);
                else
                {
                    receiver = singleEvent[1];
                    refundType = singleEvent[2];
                }

                classRefund = new ClassRefundDefinition(int.Parse(lblStudentRefundID.Text), i, lblStudentRefundShowStudentID.Text,
                                                        StaticFunction.SetEncodingString(lblStudentRefundShowStudentName.Text),
                                                        0, lblInvisibleStaffEnglishName.Text, discount, int.Parse(refundMoney), today,
                                                        StaticFunction.SetEncodingString(receiver),
                                                        StaticFunction.SetEncodingString(refundType));

                refundID = (int)facade.FacadeFunctions("insert", "studentclassrefund", (object)classRefund, null);
                lblStudentRefundID.Text = refundID.ToString();

                //Print Receipt
                if ((i > 0) && (refundType.IndexOf("新課費用") == -1))
                {
                    string[] receiptInfo = new string[6];
                    receiptInfo[0] = lblStudentRefundShowStudentID.Text;
                    receiptInfo[1] = lblStudentRefundShowStudentName.Text;
                    receiptInfo[2] = refundType;
                    receiptInfo[3] = receiver;
                    receiptInfo[4] = refundMoney;
                    receiptInfo[5] = lblInvisibleStaffEnglishName.Text;

                    facade.FacadeFunctions("reusefunction", "receiptforrefund", (object)receiptInfo, null);
                }

                if (i == 0)
                    StudentRefundPayRefundDetail(refundID);

                string tempReceiver = "";
                if (singleEvent.Length > 1)
                {
                    if (singleEvent[1] != "")
                        tempReceiver = ", 收取人為 " + singleEvent[1];
                }

                if (i > 0)
                    CreateSystemLogs("學生 " + lblStudentRefundShowStudentName.Text + "(" + lblStudentRefundShowStudentID.Text + ")" + " 退費, 退費方式為 " + cboStudentRefundRefundType.SelectedItem.ToString() + tempReceiver);
            }

            MessageBox.Show("學生退費完成!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            DefaultSetting();
            panelMainMenuScreen.Visible = false;
            panelSubButtons.Visible = true;
            panelStudentRefund.Visible = false;
        }

        private void StudentRefundPayRefundDetail(int refundID)
        {
            studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundlist", (object)"StudentID", (object)int.Parse(lblStudentRefundShowStudentID.Text));

            foreach (var studentPaymentSingle in studentPaymentSets)
            {
                classRefundDetail = new ClassRefundDetailDefinition(0, refundID, "", "", studentPaymentSingle.ClassID,
                                                                    StaticFunction.SetEncodingString(studentPaymentSingle.ClassName),
                                                                    studentPaymentSingle.HavePaid);

                facade.FacadeFunctions("insert", "studentclassrefunddetail", (object)classRefundDetail, null);
                facade.FacadeFunctions("updatedeleted", "studentinclassrefunded", (object)studentPaymentSingle.StudentID, (object)studentPaymentSingle.ClassID);
            }
        }

        //Refund By Pay Class Payment

        private void StudentRefundPayClassPayment(string studentID, string studentName, string refund)
        {
            //DefaultSetting();

            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            panelStudentPayment.Visible = true;
            panelStudentRefund.Visible = false;
            //panelMainMenuScreen.Visible = false;
            //btnStudentManageClassReturnSearchPage.Visible = true;

            panelStudentPaymentManagementPage.Visible = true;
            panelStudentPaymentByClassPaymentPage.Visible = false;

            lblStudentPaymentStudentID.Text = "學生編號:";
            lblStudentPaymentStudentName.Text = "學生姓名:";
            lblStudentPaymentShowStudentID.Text = studentID;
            lblStudentPaymentShowStudentName.Text = studentName;

            lblStudentPaymentFromPage.Text = "StudentRefund";

            ShowsStudentNeedToPayAmount();
        }

        private void RefurnStudentRefundFromStudentPayment()
        {
            ////panelTopScreen.Visible = true;
            ////panelSideFunctions.Visible = true;
            panelStudentRefund.Visible = true;
            panelStudentPayment.Visible = false;
            //panelMainMenuScreen.Visible = false;

            //lblStudentPaymentPayRefundShowRefundMoney.Text = refund;

            lblStudentRefundShowRefundMoney.Text = (int.Parse(lblStudentRefundShowHavePaid.Text) - int.Parse(lblStudentRefundShowHaveRefunded.Text) -
                                                    int.Parse(txtStudentRefundDiscount.Text)).ToString();
        }

        //Refund By Class

        private void ShowsStudentRefundStudnetAmount()
        {
            facade = new FacadeLayer(SystemTypeForDB);

            studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundlist", (object)"ClassID", (object)lblStudentRefundShowStudentID.Text);

            if (!SetStudentRefunddgvData())
                MessageBox.Show("此班級無需退費!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnStudentRefundRefundByClass_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定退費?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string today = facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString();

                studentPaymentSets = (List<StudentPaymentDefinition>)facade.FacadeFunctions("select", "studenthavetorefundlist", (object)"ClassID", (object)lblStudentRefundShowStudentID.Text);

                //Create Class Record
                classRefund = new ClassRefundDefinition(0, 0, studentPaymentSets.ElementAt(0).StudentID, "", 0, lblInvisibleStaffEnglishName.Text, 0, int.Parse(lblStudentRefundShowRefundByClass.Text),
                                                        today, lblStudentRefundShowStudentID.Text, "ClassRefunded");

                int classRefundID = (int)facade.FacadeFunctions("insert", "studentclassrefund", (object)classRefund, null);

                CreateSystemLogs("班級 " + lblStudentRefundShowStudentName.Text + "(" + lblStudentRefundShowStudentID.Text + ")" + " 退費");

                //Create Student Record
                foreach (var studentPaymentSingle in studentPaymentSets)
                {
                    //Create Total Refund Money
                    classRefund = new ClassRefundDefinition(0, 0, studentPaymentSingle.StudentID, studentPaymentSingle.StudentName,
                                                            0, lblInvisibleStaffEnglishName.Text, 0, studentPaymentSingle.HavePaid, today, classRefundID.ToString(), "ClassRefunded");

                    //Create Single Refund Money
                    int refundID = (int)facade.FacadeFunctions("insert", "studentclassrefund", (object)classRefund, null);

                    classRefund = new ClassRefundDefinition(refundID, 1, studentPaymentSingle.StudentID, studentPaymentSingle.StudentName,
                                                            0, lblInvisibleStaffEnglishName.Text, 0, studentPaymentSingle.HavePaid, today, "", "轉入帳戶");

                    refundID = (int)facade.FacadeFunctions("insert", "studentclassrefund", (object)classRefund, null);

                    //Create Refund Detail
                    classRefundDetail = new ClassRefundDetailDefinition(0, refundID, "", "", lblStudentRefundShowStudentID.Text,
                                                                        lblStudentRefundShowStudentName.Text, studentPaymentSingle.HavePaid);

                    facade.FacadeFunctions("insert", "studentclassrefunddetail", (object)classRefundDetail, null);
                    facade.FacadeFunctions("updatedeleted", "studentinclassrefunded", (object)studentPaymentSingle.StudentID, (object)lblStudentRefundShowStudentID.Text);

                    //Prepaid Info
                    studentPrepaidData = new StudentPrepaidDefinition(studentPaymentSingle.StudentID, today,
                                                                      studentPaymentSingle.HavePaid, 0, "全班退費");

                    facade.FacadeFunctions("insert", "studentprepaid", (object)studentPrepaidData, null);

                    CreateSystemLogs("學生 " + studentPaymentSingle.StudentName + "(" + studentPaymentSingle.StudentID + ")" + " 退費, 退費方式為 " + "轉入帳戶");
                }

                MessageBox.Show("全班退費完成!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ReturnToStudentSearch();
            }
        }

        private void btnStudentRefundReturnStudentSearchPage_Click(object sender, EventArgs e)
        {
            ReturnToStudentSearch();
        }

        #endregion

        #region Student Manage Data

        private void txtNewStudentID_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtNewStudentID.Text.Trim().Length > 0)
            {
                btnNewStudentCheckID.Text = "查 詢";
                btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
                //txtNewStudentName.Enabled = false;
            }
            else
            {
                btnNewStudentCheckID.Text = "新 增";
                btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
            }
        }

        private void txtNewStudentName_MouseDown(object sender, MouseEventArgs e)
        {
            if (txtNewStudentName.Text.Trim().Length > 0)
            {
                btnNewStudentCheckID.Text = "查 詢";
                btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentName.Location.Y);
                //txtNewStudentName.Enabled = false;
            }
            else
            {
                btnNewStudentCheckID.Text = "新 增";
                btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentName.Location.Y);
            }
        }

        private void txtNewStudentID_TextChanged(object sender, EventArgs e)
        {
            //if (txtNewStudentID.Text.Trim().Length > 0)
            //{
            //    btnNewStudentCheckID.Text = "查 詢";
            //    btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
            //    //txtNewStudentName.Enabled = false;
            //}
            //else
            //{
            //    txtNewStudentName.Enabled = true;
            //    if (txtNewStudentName.Text == "")
            //    {
            //        btnNewStudentCheckID.Text = "新 增";
            //        btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
            //    }
            //    else
            //    {
            //        btnNewStudentCheckID.Text = "查 詢";
            //        btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentName.Location.Y);
            //    }
            //}
        }

        private void txtNewStudentName_TextChanged(object sender, EventArgs e)
        {
            //if (txtNewStudentName.Text.Trim().Length > 0)
            //{
            //    btnNewStudentCheckID.Text = "查 詢";
            //    btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentName.Location.Y);
            //    txtNewStudentID.Enabled = false;
            //}
            //else
            //{
            //    txtNewStudentID.Enabled = true;
            //    if (txtNewStudentID.Text == "")
            //    {
            //        btnNewStudentCheckID.Text = "新 增";
            //        btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
            //    }
            //    else
            //    {
            //        btnNewStudentCheckID.Text = "查 詢";
            //        btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
            //    }
            //}
        }

        private void btnNewStudentCheckID_Click(object sender, EventArgs e)
        {
            if (btnNewStudentCheckID.Location.Y == txtNewStudentID.Location.Y)
                CheckStudentByStudentID();
            else if (btnNewStudentCheckID.Location.Y == txtNewStudentName.Location.Y)
                CheckStudentByStudentName();
            else if (txtNewStudentName.Text.Trim().Length > 0)
                CheckStudentByStudentName();
            else
                CheckStudentByStudentID();
            //if (txtNewStudentID.Text.Trim().Length > 0)
            //    CheckStudentByStudentID();
            //else
            //{
            //    if (txtNewStudentName.Enabled)
            //        CheckStudentByStudentName();
            //}
        }

        private void CheckStudentByStudentID()
        {
            bool isCheck = false;
            studentData = null;
            if (txtNewStudentID.Text.Trim().Length > 0)
            {
                if (txtNewStudentID.Text.Trim() != "")
                {
                    if ((bool)facade.FacadeFunctions("check", "number", txtNewStudentID.Text.Trim(), null))
                    {
                        if (int.Parse(txtNewStudentID.Text.Trim()) != 0)
                        {
                            isCheck = true;
                            studentData = (StudentDefinition)facade.FacadeFunctions("select", "student", (object)"ID", (object)txtNewStudentID.Text.Trim());
                        }
                        else
                            MessageBox.Show("查無此學生資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        MessageBox.Show("學生編號只能為數字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("請輸入學生編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (isCheck)
                {
                    string tempStudentID = txtNewStudentID.Text.Trim();
                    SetInsertStudentDefault();
                    panelInsertStudent.Visible = true;

                    if (studentData != null && studentData.ID != null && studentData.ID != "" && studentData.ID != "0")
                    {
                        UnlockStudentInfo();
                        LoadStudentDataByUpdating();
                        txtNewStudentName.Enabled = true;
                        lblInvisibleStudentDataStatus.Text = "Update";
                        lblInsertStudentTitle.Text = "修改學生";
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("查無此學生編號, 是否新增此學生?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            UnlockStudentInfo();
                            lblInvisibleStudentDataStatus.Text = "Insert";
                            txtNewStudentID.Text = tempStudentID;
                            txtNewStudentName.Enabled = true;
                            lblInsertStudentTitle.Text = "新增學生";
                        }
                    }
                }
            }
            else
            {
                UnlockStudentInfo();
                lblInvisibleStudentDataStatus.Text = "Insert";
                lblInsertStudentTitle.Text = "新增學生";
            }
        }

        private void CheckStudentByStudentName()
        {
            CallfrmShowAllStudents(txtNewStudentName.Text.Trim());
        }

        public void LoadStudentDataByStudentName(StudentDefinition tempData)
        {
            studentData = tempData;
            UnlockStudentInfo();
            LoadStudentDataByUpdating();
            lblInvisibleStudentDataStatus.Text = "Update";
            lblInsertStudentTitle.Text = "修改學生";

            btnNewStudentCheckID.Location = new Point(btnNewStudentCheckID.Location.X, txtNewStudentID.Location.Y);
        }

        private void txtNewStudentID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                CheckStudentByStudentID();
        }

        private void txtNewStudentName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                CheckStudentByStudentName();
        }

        private void LoadStudentDataByUpdating()
        {
            SetNewStudentErrorsDefault();

            txtNewStudentID.Enabled = true;
            txtNewStudentName.Enabled = true;
            txtNewStudentID.Text = studentData.ID;
            txtNewStudentName.Text = studentData.Name;

            for (int i = 0; i < cboNewStudentSex.Items.Count; i++)
            {
                if (cboNewStudentSex.Items[i].ToString() == studentData.Sex)
                    cboNewStudentSex.SelectedIndex = i;
            }
            //lblStudentDataManagementShowStudentSex.Text = studentData.Sex;

            if (studentData.DateOfBirth != null && studentData.DateOfBirth != "")
            {
                string dOBYear, dOBDay, tempDOB;
                int dOBMonth;
                tempDOB = studentData.DateOfBirth;
                dOBYear = tempDOB.Substring(0, 4);
                dOBMonth = int.Parse(tempDOB.Substring(5, 2));
                dOBDay = tempDOB.Substring(8, 2);
                for (int i = 0; i < cboNewStudentDOBYear.Items.Count; i++)
                {
                    if (cboNewStudentDOBYear.Items[i].ToString() == dOBYear)
                        cboNewStudentDOBYear.SelectedIndex = i;
                }

                cboNewStudentDOBMonth.SelectedIndex = dOBMonth - 1;

                for (int i = 0; i < cboNewStudentDOBDay.Items.Count; i++)
                {
                    if (cboNewStudentDOBDay.Items[i].ToString() == dOBDay)
                        cboNewStudentDOBDay.SelectedIndex = i;
                }

                //lblStudentDataManagementShowStudentDOB.Text = studentData.DateOfBirth;
            }
            else
            {
                cboNewStudentDOBYear.SelectedIndex = -1;
                cboNewStudentDOBMonth.SelectedIndex = -1;
                cboNewStudentDOBDay.SelectedIndex = -1;
            }

            txtNewStudentSocialNumber.Text = studentData.SocialNumber;
            if (studentData.StartDate != null && studentData.StartDate != "") 
                dtpNewStudentStartDate.Value = DateTime.Parse(studentData.StartDate);
            txtNewStudentSchoolName.Text = studentData.School;

            if (studentData.StudyYear != null && studentData.StudyYear != "")
            {
                for (int i = 0; i < cboNewStudentStudyYear.Items.Count; i++)
                {
                    if (cboNewStudentStudyYear.Items[i].ToString() == studentData.StudyYear)
                        cboNewStudentStudyYear.SelectedIndex = i;
                }
                //lblStudentDataManagementShowStudentStudyYear.Text = studentData.StudyYear;
            }

            txtNewStudentFatherName.Text = studentData.FatherName;
            txtNewStudentFatherWork.Text = studentData.FatherWork;
            txtNewStudentMotherName.Text = studentData.MotherName;
            txtNewStudentMotherWork.Text = studentData.MotherWork;

            txtNewStudentOldBrother.Text = studentData.OldBrother;
            txtNewStudentOldSister.Text = studentData.OldSister;
            txtNewStudentYoungBrother.Text = studentData.YoungBrother;
            txtNewStudentYoungSister.Text = studentData.YoungSister;

            if (studentData.InChargePerson != null && studentData.InChargePerson != "")
            {
                bool isFound = false;

                for (int i = 0; i < cboNewStudentInChargePerson.Items.Count; i++)
                {
                    if (cboNewStudentInChargePerson.Items[i].ToString() == studentData.InChargePerson)
                    {
                        cboNewStudentInChargePerson.SelectedIndex = i;
                        isFound = true;
                    }
                }


                if (!isFound)
                {
                    cboNewStudentInChargePerson.SelectedIndex = cboNewStudentInChargePerson.Items.Count - 1;
                    txtNewStudentInChargePerson.Visible = true;
                    txtNewStudentInChargePerson.Text = studentData.InChargePerson;
                }
            }

            if (studentData.InChargePersonHomePhone != "")
            {
                string[] phoneNum = studentData.InChargePersonHomePhone.Split(',');

                txtNewStudentInChargePersonHomePhone1.Text = phoneNum[0];

                if (phoneNum.Count() > 1)
                    txtNewStudentInChargePersonHomePhone2.Text = phoneNum[1];
                if (phoneNum.Count() > 2)
                    txtNewStudentInChargePersonHomePhone3.Text = phoneNum[2];
                if (phoneNum.Count() > 3)
                    txtNewStudentInChargePersonHomePhone4.Text = phoneNum[3];
            }

            if (studentData.InChargePersonCompanyPhone != "")
            {
                string[] phoneNum = studentData.InChargePersonCompanyPhone.Split(',');

                txtNewStudentCompanyPhone1.Text = phoneNum[0];

                if (phoneNum.Count() > 1)
                    txtNewStudentCompanyPhone2.Text = phoneNum[1];
                if (phoneNum.Count() > 2)
                    txtNewStudentCompanyPhone3.Text = phoneNum[2];
                if (phoneNum.Count() > 3)
                    txtNewStudentCompanyPhone4.Text = phoneNum[3];
            }

            if (studentData.InChargePersonMobile != "")
            {
                string[] phoneNum = studentData.InChargePersonMobile.Split(',');

                txtNewStudentInChargePersonMobile1.Text = phoneNum[0];

                if (phoneNum.Count() > 1)
                    txtNewStudentInChargePersonMobile2.Text = phoneNum[1];
                if (phoneNum.Count() > 2)
                    txtNewStudentInChargePersonMobile3.Text = phoneNum[2];
                if (phoneNum.Count() > 3)
                    txtNewStudentInChargePersonMobile4.Text = phoneNum[3];
            }

            txtNewStudentEmergencyPerson.Text = studentData.EmergencyPerson;

            if (studentData.EmergencyPhone != "")
            {
                string[] phoneNum = studentData.EmergencyPhone.Split(',');

                txtNewStudentEmergencyPhone1.Text = phoneNum[0];

                if (phoneNum.Count() > 1)
                    txtNewStudentEmergencyPhone2.Text = phoneNum[1];
                if (phoneNum.Count() > 2)
                    txtNewStudentEmergencyPhone3.Text = phoneNum[2];
                if (phoneNum.Count() > 3)
                    txtNewStudentEmergencyPhone4.Text = phoneNum[3];
            }

            //txtNewStudentPostCode.Text = studentData.PostCode;

            if (studentData.Address != null && studentData.Address != "")
            {
                string studentAddress = null;
                for (int i = 0; i < cboNewStudentAddressCity.Items.Count; i++)
                {
                    if (cboNewStudentAddressCity.Items[i].ToString() == studentData.Address.Substring(0, 3))
                        cboNewStudentAddressCity.SelectedIndex = i;
                }

                studentAddress = studentData.Address.Substring(4);
                if (studentAddress.IndexOf("市") > -1)
                {
                    txtNewStudentAddressLocalCity.Text = studentAddress.Substring(0, studentAddress.IndexOf("市"));
                    cboNewStudentAddressLocalCity.SelectedIndex = 0;
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("市") + 1);
                }
                if (studentAddress.IndexOf("鄉") > -1)
                {
                    txtNewStudentAddressLocalCity.Text = studentAddress.Substring(0, studentAddress.IndexOf("鄉"));
                    cboNewStudentAddressLocalCity.SelectedIndex = 0;
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("鄉") + 1);
                }
                if (studentAddress.IndexOf("鎮") > -1)
                {
                    txtNewStudentAddressLocalCity.Text = studentAddress.Substring(0, studentAddress.IndexOf("鎮"));
                    cboNewStudentAddressLocalCity.SelectedIndex = 0;
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("鎮") + 1);
                }

                if (studentAddress.IndexOf("路") > -1)
                {
                    txtNewStudentRoad.Text = studentAddress.Substring(0, studentAddress.IndexOf("路"));
                    cboNewStudentRoad.SelectedIndex = 0;
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("路") + 1);
                }
                else if (studentAddress.IndexOf("街") > -1)
                {
                    txtNewStudentRoad.Text = studentAddress.Substring(0, studentAddress.IndexOf("街"));
                    cboNewStudentRoad.SelectedIndex = 1;
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("街") + 1);
                }

                if (studentAddress.IndexOf("段") > -1)
                {
                    txtNewStudentSection.Text = studentAddress.Substring(0, studentAddress.IndexOf("段"));
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("段") + 1);
                }
                if (studentAddress.IndexOf("巷") > -1)
                {
                    txtNewStudentLane.Text = studentAddress.Substring(0, studentAddress.IndexOf("巷"));
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("巷") + 1);
                }
                if (studentAddress.IndexOf("弄") > -1)
                {
                    txtNewStudentAlley.Text = studentAddress.Substring(0, studentAddress.IndexOf("弄"));
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("弄") + 1);
                }
                if (studentAddress.IndexOf("號") > -1)
                {
                    txtNewStudentNumber.Text = studentAddress.Substring(0, studentAddress.IndexOf("號"));
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("號") + 1);
                }
                if (studentAddress.IndexOf("樓") > -1)
                {
                    txtNewStudentFloor.Text = studentAddress.Substring(0, studentAddress.IndexOf("樓"));
                    studentAddress = studentAddress.Substring(studentAddress.IndexOf("樓") + 1);
                }
                if (studentAddress.IndexOf("之") > -1)
                    txtNewStudentFloorS.Text = studentAddress.Substring(studentAddress.IndexOf("之"));
            }
        }

        private void btnInsertStudentReturnStudentManagement_Click(object sender, EventArgs e)
        {
            ReturnToStudentSearch();
        }

        #endregion

        #region Class Manage Data

        private void btnNewClassCheckID_Click(object sender, EventArgs e)
        {
            if (txtNewClassID.Text.Trim() != "")
                CheckClassByClassID();
            else
                MessageBox.Show("請輸入課程編號!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void txtNewClassID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                CheckClassByClassID();
        }

        private void CheckClassByClassID()
        {
            classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)"ID", (object)txtNewClassID.Text.Trim());
            string tempClassID = txtNewClassID.Text.Trim();

            SetInsertClassDefault();
            panelInsertClass.Visible = true;
            if (classData != null && classData.ID != null)
            {
                UnlockClassInfo();
                ShowClassDataForUpdating();
                lblInsertClassTitle.Text = "修改課程";
                lblInvisibleClassDataStatus.Text = "Update";
                btnDeleteExistClass.Visible = true;
            }
            else
            {
                DialogResult result = MessageBox.Show("查無此課程編號, 是否新增此課程?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    UnlockClassInfo();
                    lblInsertClassTitle.Text = "新增課程";
                    txtNewClassID.Text = tempClassID;
                    lblInvisibleClassDataStatus.Text = "Insert";
                    btnDeleteExistClass.Visible = false;
                }
            }
        }

        private void ShowClassInfoAfterSearch()
        {
            panelStudentSearchStudentInfo.Visible = true;

            lblStudentSearchStudentID.Text = "課程編號";
            lblStudentSearchStudentName.Text = "課程名稱";
            lblStudentSearchStudentSex.Text = "起始日期";
            lblStudentSearchStudentDOB.Text = "結束日期";
            lblStudentSearchStudentSchool.Text = "目前人數";
            lblStudentSearchStudentStudyYear.Text = "課程價格";

            lblStudentSearchShowStudentID.Text = classData.ID;
            lblStudentSearchShowStudentName.Text = classData.Name;
            lblStudentSearchShowStudentSex.Text = classData.StartDate;
            lblStudentSearchShowStudentDOB.Text = classData.EndDate;
            lblStudentSearchShowStudentSchool.Text = classData.Seat.ToString();
            lblStudentSearchShowStudentStudyYear.Text = classData.Price.ToString();
        }

        private void ShowClassDataForUpdating()
        {
            //txtNewClassID.Enabled = false;
            txtNewClassID.Text = classData.ID;
            lblInvisibleOldClassID.Text = classData.ID;

            for (int i = 0; i < cboNewClassCategory.Items.Count; i++)
            {
                if (cboNewClassCategory.Items[i].ToString() == classData.ClassCategoryName)
                    cboNewClassCategory.SelectedIndex = i;
            }

            if (lbNewClassTime.Visible)
            {
                classTimeSets = (List<ClassTimeDefinition>)facade.FacadeFunctions("select", "classtime", classData.ID, null);

                foreach (var classTimeSingle in classTimeSets)
                    lbNewClassTime.Items.Add(classTimeSingle.Time);
            }
            else
            {
                if (classData.ClassDay != null && classData.ClassDay != "")
                {
                    cbNewClassSunday.Checked = false;
                    cbNewClassMonday.Checked = false;
                    cbNewClassTuesday.Checked = false;
                    cbNewClassWednesday.Checked = false;
                    cbNewClassThursday.Checked = false;
                    cbNewClassFriday.Checked = false;
                    cbNewClassSaturday.Checked = false;

                    string[] classDay = classData.ClassDay.Split(',');
                    if (classDay[0] == "1")
                        cbNewClassSunday.Checked = true;
                    if (classDay[1] == "1")
                        cbNewClassMonday.Checked = true;
                    if (classDay[2] == "1")
                        cbNewClassTuesday.Checked = true;
                    if (classDay[3] == "1")
                        cbNewClassWednesday.Checked = true;
                    if (classDay[4] == "1")
                        cbNewClassThursday.Checked = true;
                    if (classDay[5] == "1")
                        cbNewClassFriday.Checked = true;
                    if (classDay[6] == "1")
                        cbNewClassSaturday.Checked = true;
                }
            }

            txtNewClassName.Text = classData.Name;
            txtNewClassTeacher.Text = classData.Teacher;
            dtpNewClassStartDate.Value = DateTime.Parse(classData.StartDate);
            dtpNewClassEndDate.Value = DateTime.Parse(classData.EndDate);
            txtNewClassPeriod.Text = classData.ClassPeriod.ToString();
            txtNewClassSeat.Text = classData.Seat.ToString();
            txtNewClassPrice.Text = classData.Price.ToString();
            txtNewClassMaterialFee.Text = classData.MaterialFee.ToString();
            txtNewClassApplyFee.Text = classData.ApplyFee.ToString();
            txtNewClassNote.Text = classData.Note;
        }

        private void btnNewClassShowClasses_Click(object sender, EventArgs e)
        {
            CallfrmShowAllClasses();
        }

        public void GetClassIDFromShowClasses(string classID)
        {
            txtNewClassID.Text = classID;
            CheckClassByClassID();
        }

        private void btnDeleteExistClass_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定刪除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                facade = new FacadeLayer(SystemTypeForDB);

                studentSets = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentbyclass", (object)"ID", (object)classData.ID);

                foreach (var studentSingle in studentSets)
                {
                    facade.FacadeFunctions("updatedeleted", "studentinclass", (object)int.Parse(studentSingle.ID), (object)classData.ID);

                    CreateSystemLogs(studentSingle.Name + "(" + studentSingle.ID + ")" + " 退選 " + classData.Name + "(" + classData.ID + ")");
                }

                facade.FacadeFunctions("updatedeleted", "class", (object)classData.ID, null);
                CreateSystemLogs("刪除課程 " + classData.Name + "(" + classData.ID + ")");
                MessageBox.Show("刪除成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //DefaultSetting();

                //panelTopScreen.Visible = true;
                //panelSideFunctions.Visible = true;
                ////panelSearchStudentScreen.Visible = true;
                //panelMainMenuScreen.Visible = false;

                SetInsertClassDefault();
                panelInsertClass.Visible = true;
            }
        }

        private void btnInsertClassReturnClassManagement_Click(object sender, EventArgs e)
        {
            ReturnToStudentSearch();
        }

        #endregion

        #region Staff Manage Data

        private void txtInsertStaffEngName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                CheckStaffByStaffEnglishName();
        }

        private void btnInsertStaffCheckEngName_Click(object sender, EventArgs e)
        {
            CheckStaffByStaffEnglishName();
        }

        private void CheckStaffByStaffEnglishName()
        {
            if (txtInsertStaffEngName.Text.Trim() != "")
            {
                staffData = (StaffDefinition)facade.FacadeFunctions("select", "staffbyenglishname", (object)txtInsertStaffEngName.Text.Trim(), null);
                string tempStaffEngName = txtInsertStaffEngName.Text.Trim();
                dtpInsertStaffEndDate.Value = DateTime.Now;

                SetInsertStaffDefault();
                panelInsertStaff.Visible = true;
                if (staffData != null && staffData.ID != null)
                {
                    if (int.Parse(lblInvisibleStaffRoleID.Text) == 1 || staffData.StaffRole > int.Parse(lblInvisibleStaffRoleID.Text))
                    {
                        UnlockStaffInfo();
                        ShowStaffDataForUpdating();
                        lblInvisibleStaffDataStatus.Text = "Update";
                        btnInsertStaffDelete.Visible = true;
                        lblInsertStaff.Text = "修改員工";
                    }
                    else
                        MessageBox.Show("您無權限修改此員工資料!!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DialogResult result = MessageBox.Show("查無此員工英文名字, 是否新增此員工?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        UnlockStaffInfo();
                        lblInvisibleStaffDataStatus.Text = "Insert";
                        btnInsertStaffDelete.Visible = false;
                        txtInsertStaffEngName.Text = tempStaffEngName;
                        lblInsertStaff.Text = "新增員工";
                        dtpInsertStaffStartDate.Value = DateTime.Now;
                    }
                }
            }
            else
                MessageBox.Show("請輸入員工英文名字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowStaffInfoAfterSearch()
        {
            panelStudentSearchStudentInfo.Visible = true;

            lblStudentSearchStudentID.Text = "員工編號";
            lblStudentSearchStudentName.Text = "員工名字";
            lblStudentSearchStudentSex.Text = "英文名字";
            lblStudentSearchStudentDOB.Text = "起始日期";
            lblStudentSearchStudentSchool.Text = "";
            lblStudentSearchStudentStudyYear.Text = "";

            lblStudentSearchShowStudentID.Text = staffData.ID;
            lblStudentSearchShowStudentName.Text = staffData.Name;
            lblStudentSearchShowStudentSex.Text = staffData.EnglishName;
            lblStudentSearchShowStudentDOB.Text = staffData.StartDate;
            lblStudentSearchShowStudentSchool.Text = "";
            lblStudentSearchShowStudentStudyYear.Text = "";
        }

        private void ShowStaffDataForUpdating()
        {
            txtInsertStaffName.Text = staffData.Name;
            txtInsertStaffEngName.Text = staffData.EnglishName;
            dtpInsertStaffStartDate.Value = DateTime.Parse(staffData.StartDate);

            if (staffData.IsDeleted == '1')
            {
                dtpInsertStaffEndDate.Value = DateTime.Parse(staffData.EndDate);
                dtpInsertStaffEndDate.Visible = true;
                btnInsertStaffDelete.Visible = false;
            }
            else
            {
                dtpInsertStaffEndDate.Visible = false;
                btnInsertStaffDelete.Visible = true;
            }

            txtInsertStaffLaborCover.Text = staffData.LaborCover.ToString();
            txtInsertStaffHealthCover.Text = staffData.HealthCover.ToString();
            txtInsertStaffGroupCover.Text = staffData.GroupCover.ToString();
            txtInsertStaffNote.Text = staffData.Note;

            staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "staffaccountbyenglishname", staffData.EnglishName, null);

            if (int.Parse(lblInvisibleStaffRoleID.Text) == 1)
                cboInsertStaffRole.SelectedIndex = staffAccountData.StaffRoleID - 1;
            else
                cboInsertStaffRole.SelectedIndex = staffAccountData.StaffRoleID - 1 - int.Parse(lblInvisibleStaffRoleID.Text);

            if (int.Parse(lblInvisibleStaffRoleID.Text) != cboInsertStaffRole.Items.Count - 1)
                txtInsertStaffMasterKey.Enabled = true;

            lblInvisibleOldStaffPassword.Text = staffAccountData.Password;
            lblInvisibleOldStaffMasterKey.Text = staffAccountData.MasterKey;
            lblInvisibleOldStaffEnglishName.Text = staffData.EnglishName;
        }

        private void btnInsertStaffDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定刪除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                facade = new FacadeLayer(SystemTypeForDB);

                facade.FacadeFunctions("updatedeleted", "staff", (object)staffData.ID, null);
                dtpInsertStaffEndDate.Visible = true;
                staffData.IsDeleted = '1';
                btnInsertStaffDelete.Visible = false;
                CreateSystemLogs("刪除員工 " + staffData.Name + "(" + staffData.ID + ")");
                MessageBox.Show("刪除成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                SetInsertStaffDefault();
                panelInsertStaff.Visible = true;
            }
        }

        #endregion

        #region Expanse Manage Data

        private void SetDailyExpanseDefault()
        {
            SetDailyExpanseItemsDefault();

            dtpSearchExpanseStartDate.Value = DateTime.Now;
            dtpSearchExpanseEndDate.Value = DateTime.Now;

            btnDeleteExpanse.Visible = false;
            btnConfirmExpanse.Visible = true;
            btnConfirmExpanse.Text = "新 增";
        }

        private void SetDailyExpanseItemsDefault()
        {
            if (dgvDailyExpanse.Columns.Count > 0)
                dgvDailyExpanse.Columns.Clear();

            btnDeleteExpanse.Visible = false;
            txtExpanseEvent.Text = "";
            txtExpanseQuantity.Text = "0";
            txtExpanseUnitPrice.Text = "0";
            txtExpanseShop.Text = "";
            lblShowExpanseTotalMoney.Text = "0";
            cboExpanseCategory.SelectedIndex = 0;
        }

        private void ShowsDailyExpanseData(string startDate, string endDate)
        {
            SetDailyExpanseItemsDefault();

            facade = new FacadeLayer(SystemTypeForDB);
            expanseSets = (List<ExpanseDefinition>)facade.FacadeFunctions("select", "dailyexpansebydates", startDate, endDate);

            double totalSearchMoney = 0;

            if (expanseSets != null && expanseSets.Count() > 0)
            {
                List<ExpanseDefinition> tempExpanse = new List<ExpanseDefinition>();
                foreach (var expanseSingle in expanseSets)
                {
                    if (!panelSearchExpanse.Visible)
                    {
                        if (expanseSingle.InsertStaffName == lblInvisibleStaffEnglishName.Text.Trim())
                            tempExpanse.Add(expanseSingle);
                    }
                }

                if (!panelSearchExpanse.Visible)
                    expanseSets = tempExpanse;

                foreach (var expanseSingle in expanseSets)
                    totalSearchMoney += expanseSingle.TotalMoney;

                dgvDailyExpanse.DataSource = expanseSets;

                dgvDailyExpanse.Columns.Remove("IsDeleted");
                dgvDailyExpanse.Columns.Remove("ShopName");
                dgvDailyExpanse.Columns.Remove("UpdateStaffID");
                dgvDailyExpanse.Columns.Remove("InsertStaffID");
                dgvDailyExpanse.Columns.Remove("ExpanseCategoryName");
                dgvDailyExpanse.Columns.Remove("ID");

                dgvDailyExpanse.Columns["ItemName"].DisplayIndex = 0;
                dgvDailyExpanse.Columns["UnitPrice"].DisplayIndex = 1;
                dgvDailyExpanse.Columns["Quantity"].DisplayIndex = 2;
                dgvDailyExpanse.Columns["TotalMoney"].DisplayIndex = 3;
                dgvDailyExpanse.Columns["InsertStaffName"].DisplayIndex = 4;
                dgvDailyExpanse.Columns["InsertDate"].DisplayIndex = 5;
                dgvDailyExpanse.Columns["UpdateStaffName"].DisplayIndex = 6;
                dgvDailyExpanse.Columns["UpdateDate"].DisplayIndex = 7;

                dgvDailyExpanse.Columns["ItemName"].HeaderText = "支出項目";
                dgvDailyExpanse.Columns["UnitPrice"].HeaderText = "單項金額";
                dgvDailyExpanse.Columns["Quantity"].HeaderText = "項目數量";
                dgvDailyExpanse.Columns["TotalMoney"].HeaderText = "支出總額";
                dgvDailyExpanse.Columns["InsertStaffName"].HeaderText = "新增員工";
                dgvDailyExpanse.Columns["InsertDate"].HeaderText = "新增時間";
                dgvDailyExpanse.Columns["UpdateStaffName"].HeaderText = "修改員工";
                dgvDailyExpanse.Columns["UpdateDate"].HeaderText = "修改時間";

                if (!panelSearchExpanse.Visible)
                {
                    dgvDailyExpanse.Columns.Remove("UpdateDate");
                    dgvDailyExpanse.Columns.Remove("UpdateStaffName");
                }

                dgvDailyExpanse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvDailyExpanse.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvDailyExpanse.AllowUserToAddRows = false;

                if (dgvDailyExpanse.Rows.Count > 0)
                    dgvDailyExpanse.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvDailyExpanse.Rows.Count; i++)
                    dgvDailyExpanse.Rows[i].Resizable = DataGridViewTriState.False;
                dgvDailyExpanse.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvDailyExpanse.Columns.Count; i++)
                {
                    dgvDailyExpanse.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvDailyExpanse.Columns[i].Resizable = DataGridViewTriState.False;
                }
            }

            lblExpanseDate.Visible = true;
            dtpExpanseDate.Visible = true;
            dtpExpanseDate.Value = DateTime.Now;

            lblDailyExpanseSearchTotalMoney.Text = "查詢總金額: " + totalSearchMoney.ToString();
        }

        private void txtExpanseEvent_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                Control c = GetNextControl((Control)sender, true);
                if (c != null)
                    c.Focus();
            }
        }

        private void txtExpanseUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                Control c = GetNextControl((Control)sender, true);
                if (c != null)
                    c.Focus();
            }
        }

        private void txtExpanseQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            //InsertOrUpdateDailyExpanse();
        }

        private void txtExpanseUnitPrice_TextChanged(object sender, EventArgs e)
        {
            CountTotalExpanseMoney();
        }

        private void txtExpanseQuantity_TextChanged(object sender, EventArgs e)
        {
            CountTotalExpanseMoney();
        }

        private bool CountTotalExpanseMoney()
        {
            facade = new FacadeLayer(SystemTypeForDB);

            bool isTrue = false;
            if (txtExpanseQuantity.Text.Trim() != "")
            {
                if ((bool)facade.FacadeFunctions("check", "number", (object)txtExpanseQuantity.Text, null))
                {
                    if (int.Parse(txtExpanseQuantity.Text) >= 0)
                        isTrue = true;
                    else
                        MessageBox.Show("項目數量不能小於零!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("項目數量只能為數字!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("請輸入項目數量!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (isTrue)
            {
                isTrue = false;
                if (txtExpanseUnitPrice.Text.Trim() != "")
                {
                    if ((bool)facade.FacadeFunctions("check", "number", (object)txtExpanseUnitPrice.Text, null))
                    {
                        if (double.Parse(txtExpanseUnitPrice.Text) >= 0)
                            isTrue = true;
                        else
                            MessageBox.Show("單項金額不能小於零!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("單項金額只能為數字!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("請輸入單項金額!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (isTrue)
            {
                double unitPrice = double.Parse(txtExpanseUnitPrice.Text);
                int quantity = int.Parse(txtExpanseQuantity.Text);

                lblShowExpanseTotalMoney.Text = (unitPrice * quantity).ToString();
            }

            return isTrue;
        }

        private void InsertOrUpdateDailyExpanse()
        {
            if (CountTotalExpanseMoney())
            {
                if (dtpExpanseDate.Value.AddDays(1) >= DateTime.Now.AddDays(1))
                    MessageBox.Show("支出日期不能大於今天!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (txtExpanseEvent.Text.Trim() != "")
                {
                    facade = new FacadeLayer(SystemTypeForDB);

                    expanseData = new ExpanseDefinition();
                    expanseData.ID = lblOriginalExpanseID.Text.Trim();
                    expanseData.ItemName = StaticFunction.SetEncodingString(txtExpanseEvent.Text.Trim());
                    expanseData.ExpanseCategoryName = cboExpanseCategory.SelectedItem.ToString();
                    expanseData.ShopName = StaticFunction.SetEncodingString(txtExpanseShop.Text.Trim());
                    expanseData.InsertStaffName = lblInvisibleStaffEnglishName.Text.Trim();
                    expanseData.UpdateStaffName = lblInvisibleStaffEnglishName.Text.Trim();
                    expanseData.Quantity = int.Parse(txtExpanseQuantity.Text.Trim());
                    expanseData.UnitPrice = double.Parse(txtExpanseUnitPrice.Text.Trim());
                    expanseData.InsertDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpExpanseDate.Value, null).ToString();

                    if (btnConfirmExpanse.Text == "新 增")
                    {
                        facade.FacadeFunctions("insert", "dailyexpanse", expanseData, null);
                        CreateSystemLogs("新增支出 " + txtExpanseEvent.Text.Trim() + ", 總金額為 " + lblShowExpanseTotalMoney.Text + " 元");
                    }
                    else if (btnConfirmExpanse.Text == "修 改")
                    {
                        if (expanseData.ItemName != lblOriginalExpanseEvent.Text ||
                            expanseData.ExpanseCategoryName != lblOriginalExpanseCategory.Text ||
                            expanseData.Quantity != int.Parse(lblOriginalExpanseQuantity.Text) ||
                            expanseData.UnitPrice != double.Parse(lblOriginalExpanseUnitPrice.Text))
                        {
                            facade.FacadeFunctions("update", "dailyexpanse", expanseData, null);
                            CreateSystemLogs("修改支出 " + txtExpanseEvent.Text.Trim() + ", 總金額為 " + lblShowExpanseTotalMoney.Text + " 元");
                        }
                    }

                    ShowsDailyExpanseData(facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseStartDate.Value, null).ToString(),
                                                       facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseEndDate.Value.AddDays(1), null).ToString());
                }
                else
                    MessageBox.Show("請輸入支出項目!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowDailyExpanseDataForUpdating()
        {
            btnDeleteExpanse.Visible = true;
            btnConfirmExpanse.Text = "修 改";
            lblOriginalExpanseID.Text = expanseData.ID;
            txtExpanseEvent.Text = expanseData.ItemName;
            txtExpanseQuantity.Text = expanseData.Quantity.ToString();
            txtExpanseUnitPrice.Text = expanseData.UnitPrice.ToString();

            lblOriginalExpanseCategory.Text = expanseData.ExpanseCategoryName;
            lblOriginalExpanseEvent.Text = expanseData.ItemName;
            lblOriginalExpanseID.Text = expanseData.ID;
            lblOriginalExpanseQuantity.Text = expanseData.Quantity.ToString();
            lblOriginalExpanseUnitPrice.Text = expanseData.UnitPrice.ToString();
        }

        private void dgvDailyExpanse_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvDailyExpanse_CellDoubleClick(sender, e);
        }

        private void dgvDailyExpanse_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int dgvRowIndex = 0;
            int selectItem = 0;

            if (e.ColumnIndex > -1 && e.RowIndex > -1)
            {
                dgvRowIndex = 0;
                foreach (DataGridViewRow dgvRow in this.dgvDailyExpanse.Rows)
                {
                    //lblStudentPaymentIsDoubleWorking.Text = "true";
                    if (dgvRow.Selected)
                    {
                        dgvDailyExpanse.ReadOnly = true;
                        selectItem++;
                        expanseData = expanseSets.ElementAt(dgvRowIndex);
                    }

                    dgvRowIndex += 1;
                }
            }
            else
            {
                dgvStudentPaymentShowStudentUnpaidClass.ReadOnly = false;
                dgvStudentPaymentShowStudentUnpaidClass.EditMode = DataGridViewEditMode.EditOnKeystroke;
            }

            if (selectItem > 0)
                ShowDailyExpanseDataForUpdating();
        }

        private void btnConfirmExpanse_Click(object sender, EventArgs e)
        {
            InsertOrUpdateDailyExpanse();
        }

        private void btnDeleteExpanse_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("是否確定刪除?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                facade = new FacadeLayer(SystemTypeForDB);
                facade.FacadeFunctions("delete", "dailyexpanse", lblOriginalExpanseID.Text, null);
                CreateSystemLogs("刪除支出 " + txtExpanseEvent.Text.Trim() + ", 總金額為 " + lblShowExpanseTotalMoney.Text + " 元");
                ShowsDailyExpanseData(facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseStartDate.Value, null).ToString(),
                                                    facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseEndDate.Value.AddDays(1), null).ToString());
            }
        }

        private void btnSearchExpanse_Click(object sender, EventArgs e)
        {
            if (dtpSearchExpanseStartDate.Value <= dtpSearchExpanseEndDate.Value)
            {
                ShowsDailyExpanseData(facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseStartDate.Value, null).ToString(),
                                                            facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseEndDate.Value.AddDays(1), null).ToString());
            }
            else
                MessageBox.Show("查詢起始日期不得晚於結束日期!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnExpanseCancel_Click(object sender, EventArgs e)
        {
            btnConfirmExpanse.Text = "新 增";
            ShowsDailyExpanseData(facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseStartDate.Value, null).ToString(),
                                                facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseEndDate.Value.AddDays(1), null).ToString());
        }

        private void btnExpansePrint_Click(object sender, EventArgs e)
        {
            if (dtpSearchExpanseStartDate.Value <= dtpSearchExpanseEndDate.Value)
            {
                facade = new FacadeLayer(SystemTypeForDB);

                string startDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseStartDate.Value, null).ToString();
                string endDate = facade.FacadeFunctions("format", "datebydatetime", (object)dtpSearchExpanseEndDate.Value.AddDays(1), null).ToString();

                expanseSets = (List<ExpanseDefinition>)facade.FacadeFunctions("select", "dailyexpansebydates", startDate, endDate);

                if (expanseSets != null && expanseSets.Count() > 0)
                {
                    facade.FacadeFunctions("reusefunction", "expenselogbitmap", startDate, endDate);
                }
                else
                {
                    MessageBox.Show("無查詢結果!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("查詢起始日期不得晚於結束日期!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        #region Backup MySQL

        /// <summary>
        ///  Run Backup Via Solid Batch Backup File
        /// </summary>
        /// <returns></returns>
        protected void ExecuteBatchFile()
        {
            try
            {
                System.Diagnostics.Process processBackup = new System.Diagnostics.Process();
                processBackup.EnableRaisingEvents = false;
                processBackup.StartInfo.FileName = "MySql_Backup_v1.bat";
                processBackup.Start();
                processBackup.WaitForExit();

                facade = new FacadeLayer(SystemTypeForDB);
                string msg = "備份於硬碟成功!!\r\n";
                string usbDrive = facade.FacadeFunctions("reusefunction", "getusbdrive", Application.StartupPath, null).ToString();
                msg += usbDrive;

                MessageBox.Show(msg, "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////Create process start information
                //System.Diagnostics.ProcessStartInfo DBProcessStartInfo = new System.Diagnostics.ProcessStartInfo("MySql_Backup_v1.bat");

                ////Redirect the output to standard window
                //DBProcessStartInfo.RedirectStandardOutput = false;

                ////The output display window need not be falshed onto the front.
                //DBProcessStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                //DBProcessStartInfo.UseShellExecute = false;

                ////Create the process and run it
                //System.Diagnostics.Process dbProcess;
                //dbProcess = System.Diagnostics.Process.Start(DBProcessStartInfo);
            }
            catch
            {
            }
        }

        /// <summary>
        ///  Run Backup Via Variable Batch Backup File
        /// </summary>
        /// <param name="batchFileName">Name of the batch file</param>
        /// <param name="argumentsToBatchFile">Arguments to the batch file</param>
        /// <returns></returns>
        protected bool ExecuteBatchFile(string batchFileName, string[] argumentsToBatchFile)
        {
            string argumentsString = string.Empty;
            try
            {
                //Add up all arguments as string with space separator between the arguments
                if (argumentsToBatchFile != null)
                {
                    for (int count = 0; count < argumentsToBatchFile.Length; count++)
                    {
                        argumentsString += " ";
                        argumentsString += argumentsToBatchFile[count];
                        //argumentsString += "\"";
                    }
                }

                //Create process start information
                System.Diagnostics.ProcessStartInfo DBProcessStartInfo = new System.Diagnostics.ProcessStartInfo(batchFileName, argumentsString);

                //Redirect the output to standard window
                DBProcessStartInfo.RedirectStandardOutput = true;

                //The output display window need not be falshed onto the front.
                DBProcessStartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                DBProcessStartInfo.UseShellExecute = false;

                //Create the process and run it
                System.Diagnostics.Process dbProcess;
                dbProcess = System.Diagnostics.Process.Start(DBProcessStartInfo);

                //Catch the output text from the console so that if error happens, the output text can be logged.
                System.IO.StreamReader standardOutput = dbProcess.StandardOutput;

                /* Wait as long as the DB Backup or Restore or Repair is going on. 
                Ping once in every 2 seconds to check whether process is completed. */
                while (!dbProcess.HasExited)
                    dbProcess.WaitForExit(2000);

                if (dbProcess.HasExited)
                {
                    string consoleOutputText = standardOutput.ReadToEnd();
                    //TODO - log consoleOutputText to the log file.

                }

                return true;
            }
            // Catch the SQL exception and throw the customized exception made out of that
            catch (SqlException ex)
            {
                return false;
                //ExceptionManager.Publish(ex);
                //throw SQLExceptionClassHelper.GetCustomMsSqlException(ex.Number);
            }
            // Catch all general exceptions
            catch (Exception ex)
            {
                return false;
                //ExceptionManager.Publish(ex);
                //throw new CustomizedException(ARCExceptionManager.ErrorCodeConstants.generalError, ex.Message);
            }
        }

        //Run Backup Via StreamWriter
        StreamWriter outputstream;
        private void ExecuteBackup()
        {
            string dbname = "educatemanagement"; //dummy-name of the database --routines Even backups the stored procedures
            string backupOpt = "--default-character-set=utf8 --no-create-db --no-create-info --extended-insert=false ";
            string port = "localhost";
            string user = "root";
            string passwd = "123456";

            string filePath = Application.StartupPath + "\\backupfile.sql";

            facade = new FacadeLayer(SystemTypeForDB);
            facade.FacadeFunctions("delete", "file", filePath, null);
            facade.FacadeFunctions("create", "file", filePath, null);
            outputstream = new StreamWriter(filePath);

            try
            {
                string mysqldumpString = string.Format("--databases {0} --user={1} --password={2}", backupOpt + dbname, user, passwd);
                //ProcessStartInfo info = new ProcessStartInfo("mysqldump");
                //info.Arguments = mysqldumpstring;
                //info.UseShellExecute = false;
                //info.CreateNoWindow = true;
                //info.RedirectStandardError = true;
                //info.RedirectStandardOutput = true;

                //Process processBackup = new Process();
                //processBackup.StartInfo = info;
                //processBackup.OutputDataReceived += new DataReceivedEventHandler(OnDataReceived);
                //processBackup.Start();
                //processBackup.BeginOutputReadLine();
                //processBackup.WaitForExit();

                //System.Diagnostics.Process processBackup = new System.Diagnostics.Process();
                //processBackup.EnableRaisingEvents = false;
                //processBackup.StartInfo.FileName = "mysqldump";
                //processBackup.StartInfo.Arguments = mysqldumpString + " > " + filePath;
                //processBackup.Start();
                //processBackup.WaitForExit();

                StringBuilder sbcommand = new StringBuilder();
                sbcommand.AppendFormat("mysqldump {0} > {1}", mysqldumpString, filePath);

                String command = sbcommand.ToString();
                String appDirecroty = System.Windows.Forms.Application.StartupPath + "\\";
                StartCmd(appDirecroty, command);

                ProcessStartInfo processBackup = new ProcessStartInfo();
                processBackup.FileName = "mysqldump";
                processBackup.RedirectStandardInput = false;
                processBackup.RedirectStandardOutput = true;
                processBackup.Arguments = mysqldumpString;
                processBackup.UseShellExecute = false;
                Process process = Process.Start(processBackup);
                string res;
                res = process.StandardOutput.ReadToEnd();
                outputstream.WriteLine(res);
                process.WaitForExit();
                outputstream.Close();
            }
            catch { }
            finally
            {
                //outputstream.Flush();
                //outputstream.Close();
            }
        }

        public static void StartCmd(String workingDirectory, String command)
        {
            Process processBackup = new Process();
            processBackup.StartInfo.FileName = "cmd.exe";
            processBackup.StartInfo.WorkingDirectory = workingDirectory;
            processBackup.StartInfo.UseShellExecute = false;
            processBackup.StartInfo.RedirectStandardInput = true;
            processBackup.StartInfo.RedirectStandardOutput = true;
            processBackup.StartInfo.RedirectStandardError = true;
            processBackup.StartInfo.CreateNoWindow = true;
            processBackup.StartInfo.Arguments = command;
            processBackup.Start();
            //processBackup.StandardInput.WriteLine(command);
            //processBackup.StandardInput.WriteLine("exit");
        }  

        private void OnDataReceived(object Sender, DataReceivedEventArgs e)
        {
            string data = null;
            //if (e.Data != null)
            {
                data = e.Data;
                data += "\r\n";
                //MessageBox.Show(data);

            }
            //if(data!=null)
            try
            {
                outputstream.Write(data);
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        #endregion

        #region 新生選課管理

        public void SetNewStudentAddNewClass(string studentID, string studentName)
        {
            if (lblCurrentPage.Text.IndexOf("01") > -1)
            {
                if (lblCurrentPage.Text.IndexOf("新增") > -1)
                    lblCurrentPage.Text = lblCurrentPage.Text.Replace("新增學生", "新生加選");
                else if (lblCurrentPage.Text.IndexOf("付費") > -1)
                    lblCurrentPage.Text = lblCurrentPage.Text.Replace("新生付費", "新生加選");
            }
            else if (lblCurrentPage.Text.IndexOf("02") > -1)
            {
                if (lblCurrentPage.Text.IndexOf("收費") > -1)
                {
                    lblCurrentPage.Text = lblCurrentPage.Text.Replace("學生收費管理", "學生加選課程");
                    lblCurrentPage.Text = lblCurrentPage.Text.Replace("收費", "加選");
                }
            }

            btnNewStudentClassPayment.Enabled = false;
            AfterStudentSearch(studentID, studentName);
            //btnStudentManageClassReturnSearchPage.Visible = false;
            btnReturnSearchPage.Visible = false;

            if (lblCurrentPage.Text.IndexOf("加選") > -1)
                StudentManageClassShowAllClass();

            //{
            //    List<ClassDefinition> tempClassSets = (List<ClassDefinition>)facade.FacadeFunctions("select", "classbyenddate", "All", "");
            //    if (tempClassSets != null && tempClassSets.Count > 0)
            //        StudentManageClassShowClassList(tempClassSets);
            //}
        }

        private void btnNewStudentClassPayment_Click(object sender, EventArgs e)
        {
            SetNewStudentPayment(lblStudentManageClassShowStudentID.Text, lblStudentManageClassShowStudentName.Text);
        }

        private void SetNewStudentPayment(string studentID, string studentName)
        {
            if (lblCurrentPage.Text.IndexOf("01") > -1)
            {
                lblCurrentPage.Text = lblCurrentPage.Text.Replace("新生加選", "新生付費");
            }
            else if (lblCurrentPage.Text.IndexOf("02") > -1)
            {
                lblCurrentPage.Text = lblCurrentPage.Text.Replace("舊生加選課程", "舊生收費管理");
                lblCurrentPage.Text = lblCurrentPage.Text.Replace("加選", "收費");
            }

            btnStudentPaymentReturnStudentSearchPage.Visible = true;
            AfterStudentSearch(studentID, studentName);
            //btnStudentManageClassReturnSearchPage.Visible = false;
            btnReturnSearchPage.Visible = true;
            btnNewStudentClassPayment.Enabled = false;
        }

        #endregion

        #region 學生收費管理

        //Student Payment Search Page
        #region Student Payment Search Page

        private void SetStudentPaymentSearchDefault()
        {
            SetStudentPaymentSearchResultDefault();


        }

        private void SetStudentPaymentSearchResultDefault()
        {
            SetStudentPaymentPayMoneyPageDefault();


        }

        private void SetStudentPaymentPayMoneyPageDefault()
        {
            SetStudentPaymentPayMoneyButtonDefault();
            SetStudentPaymentPayMoneyByPersonPageDefault();



        }

        private void SetStudentPaymentPayMoneyByPersonPageDefault()
        {

        }

        private void SetStudentPaymentPayMoneyButtonDefault()
        {

        }

        //Show Select Type

        private void StudentPaymentShowStudentList()
        {

        }

        private void dgvStudentPaymentSearchShowStudentInClassList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ShowStudentNeedToPayClass()
        {

        }

        #endregion

        //Student Payment By Student PayMoney Page
        #region Student Payment By Student PayMoney Page


        private void StudentClassPayment()
        {

        }

        private void UpdateStudentClassPaymentDiscount(string studentID, string classID, string oldDiscount, string newDiscount, string needToPay)
        {

        }

        private void StudentClassPaymentPayMoney()
        {

        }

        #endregion

        //Student Payment By Class PayMoney Page
        #region Student Payment By Class PayMoney Page


        private void ShowStudentNeedToPayByClass()
        {

        }

        #endregion

        #endregion

        #region 應收費用查詢

        private void tcSearchPayment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region 退費紀錄查詢

        //Insert Refund Page
        #region Insert Refund Page

        private void tcSearchRefund_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void SetInsertRefundDefault()
        {
            SetInsertRefundSearchResultDefault();


        }

        private void SetInsertRefundSearchResultDefault()
        {

        }

        private void SetRefundRecodeDefault()
        {

        }

        //Refund Search Page
        #region Refund Search Search Page

        private void cboInsertRefundSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cboInsertRefundSearchByClassCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnInsertRefundSearchBy_Click(object sender, EventArgs e)
        {

        }

        private void StudentRefundShowStudentList()
        {

        }

        private void dgvInsertRefundShowStudentByClass_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvInsertRefundShowStudentByClass_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        #endregion

        //Refund PayRefund By Person Page
        #region Refund Search PayRefund Page


        private void btnInsertRefundByClass_Click(object sender, EventArgs e)
        {

        }

        private void btnInsertRefundByStudent_Click(object sender, EventArgs e)
        {

        }

        private void ShowStudentNeedToRefundClass()
        {

        }

        private void btnInsertRefundSingleRefund_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        //Search Refund Page
        #region Search Refund Page

        private void SetStudentRefundRecodeDefault()
        {

        }

        private void SetStudentRefundRecodeSearchDefault()
        {

        }

        private void SetStudentRefundRecodeSearchResultDefault()
        {

        }

        private void cboRefundRecodeSearchBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetStudentRefundRecodeSearchDefault();


        }

        private void cboRefundRecodeSearchByClassCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #region 系統資料管理

        private void SetManageSystemInfoDefault()
        {
            txtCompanyManager.Text = "";
            txtCompanyName.Text = "";
            lblShowStartTime.Visible = false;
            btnStartSystem.Visible = false;

            dtpSystemLogStartDate.Value = DateTime.Now;
            dtpSystemLogEndDate.Value = DateTime.Now;

            companyInfo = (CompanyInfoDefinition)facade.FacadeFunctions("select", "whole", "CompanyInfo", null);
            if (companyInfo.StartTime != "" && companyInfo.StartTime != null)
                dtpSystemLogStartDate.Value = DateTime.Parse(companyInfo.StartTime);

            if (dgvSystemLog.Columns.Count > 0)
                dgvSystemLog.Columns.Clear();
        }

        #region Company Info

        private void txtCompanyName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                Control c = GetNextControl((Control)sender, true);
                if (c != null)
                    c.Focus();
            }
        }

        private void txtCompanyManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                UpdateCompanyInfo();
        }

        private void btnStartSystem_Click(object sender, EventArgs e)
        {
            companyInfo = new CompanyInfoDefinition(txtCompanyName.Text, txtCompanyManager.Text, "");
            facade.FacadeFunctions("insert", "companyinfo", companyInfo, null);
            btnStartSystem.Visible = false;
            lblShowStartTime.Visible = true;
            lblShowStartTime.Text = DateTime.Now.ToString();
        }

        private void btnUpdateSystemInfo_Click(object sender, EventArgs e)
        {
            UpdateCompanyInfo();
        }

        private void UpdateCompanyInfo()
        {
            companyInfo = new CompanyInfoDefinition(txtCompanyName.Text, txtCompanyManager.Text, "");

            if (btnStartSystem.Visible)
            {
                facade.FacadeFunctions("insert", "companyinfo", companyInfo, null);
                btnStartSystem.Visible = false;
                lblShowStartTime.Visible = true;
                lblShowStartTime.Text = DateTime.Now.ToString();
                MessageBox.Show("新增公司資訊成功!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                facade.FacadeFunctions("update", "companyinfo", companyInfo, null);
                MessageBox.Show("修改公司資訊成功!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (txtCompanyName.Text != "")
            {
                lblSystemTitle.Text = txtCompanyName.Text;
                ReSize();
            }
        }

        #endregion

        #region Show System Log

        private void btnSystemLogSearchBy_Click(object sender, EventArgs e)
        {
            if (dgvSystemLog.Columns.Count > 0)
                dgvSystemLog.Columns.Clear();

            facade = new FacadeLayer(SystemTypeForDB);

            string[] systemLogInfo = new string[3];
            systemLogInfo[0] = (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpSystemLogStartDate.Value, null);
            systemLogInfo[1] = (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpSystemLogEndDate.Value.AddDays(1), null);
            systemLogInfo[2] = cboSystemLogSearchBy.SelectedItem.ToString();

            systemLogsSets = (List<SystemLogsDefinition>)facade.FacadeFunctions("select", "systemlogs", systemLogInfo, null);

            if (systemLogsSets != null && systemLogsSets.Count > 0)
            {
                DataGridViewColumn newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "員工姓名";
                dgvSystemLog.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "記錄時間";
                dgvSystemLog.Columns.Add(newColumn);

                newColumn = new DataGridViewTextBoxColumn();
                newColumn.HeaderText = "記錄事件";
                dgvSystemLog.Columns.Add(newColumn);

                foreach (var systemLogsSingle in systemLogsSets)
                {
                    DataGridViewRow newRow = new DataGridViewRow();
                    DataGridViewCell newCell;

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = systemLogsSingle.StaffName;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = systemLogsSingle.Date;
                    newRow.Cells.Add(newCell);

                    newCell = new DataGridViewTextBoxCell();
                    newCell.Value = systemLogsSingle.Action;
                    newRow.Cells.Add(newCell);

                    dgvSystemLog.Rows.Add(newRow);
                }

                dgvSystemLog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvSystemLog.EditMode = DataGridViewEditMode.EditOnKeystroke;
                dgvSystemLog.AllowUserToAddRows = false;

                if (dgvSystemLog.Rows.Count > 0)
                    dgvSystemLog.Rows[0].Selected = false;

                //Disable Resizing
                for (int i = 0; i < dgvSystemLog.Rows.Count; i++)
                    dgvSystemLog.Rows[i].Resizable = DataGridViewTriState.False;
                dgvSystemLog.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

                for (int i = 0; i < dgvSystemLog.Columns.Count; i++)
                {
                    dgvSystemLog.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                    //dgvSystemLog.Columns[i].Resizable = DataGridViewTriState.False;
                    dgvSystemLog.ReadOnly = true;
                }
            }
            else
                MessageBox.Show("查無此指定資料!!", "資訊", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #endregion

    }
}
