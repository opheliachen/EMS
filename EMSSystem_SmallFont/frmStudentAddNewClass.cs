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
    public partial class frmStudentAddNewClass : Form
    {
        frmEMS emsSystem = new frmEMS();
        frmErrorMessage errorMsg;
        FacadeLayer facade;
        ClassDefinition classData;
        StudentInClassDefinition studentInClassData;

        public frmStudentAddNewClass()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 10F, System.Drawing.FontStyle.Bold);
            }
        }

        private void frmStudentAddNewClass_Load(object sender, EventArgs e)
        {
            //SetNewClassErrorsDefault();
            lblInsertErrorMsgIsShow.Text = "false";
        }

        public void GetClassInfo(string studentID, string studentName, string classID)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)"ID", (object)studentID);

            if (classData == null || classData.ID == null)
                lblFromStudentOrClass.Text = "Student";
            else
                lblFromStudentOrClass.Text = "Class";

            lblStudentManageClassShowStudentID.Text = studentID;
            lblStudentManageClassShowStudentName.Text = studentName;

            classData = (ClassDefinition)facade.FacadeFunctions("select", "class", (object)"ID", (object)classID);

            double tempClassPrice = classData.Price / classData.ClassPeriod;
            lblEachClassPrice.Text = tempClassPrice.ToString();

            lblStudentAddClassShowClassID.Text = classData.ID;
            lblStudentAddClassShowClassName.Text = classData.Name;
            lblInvisibleClassPrice.Text = classData.Price.ToString();
            txtNewClassPrice.Text = classData.Price.ToString();
            txtNewClassMaterialFee.Text = classData.MaterialFee.ToString();
            txtNewClassApplyFee.Text = classData.ApplyFee.ToString();

            if (classData.ClassDay != null && classData.ClassDay != "")
            {
                string[] classDay = classData.ClassDay.Split(',');
                if (classDay[0] == "1")
                {
                    cbNewClassSunday.Checked = true;
                    cbNewClassSunday.Enabled = true;
                }
                if (classDay[1] == "1")
                {
                    cbNewClassMonday.Checked = true;
                    cbNewClassMonday.Enabled = true;
                }
                if (classDay[2] == "1")
                {
                    cbNewClassTuesday.Checked = true;
                    cbNewClassTuesday.Enabled = true;
                }
                if (classDay[3] == "1")
                {
                    cbNewClassWednesday.Checked = true;
                    cbNewClassWednesday.Enabled = true;
                }
                if (classDay[4] == "1")
                {
                    cbNewClassThursday.Checked = true;
                    cbNewClassThursday.Enabled = true;
                }
                if (classDay[5] == "1")
                {
                    cbNewClassFriday.Checked = true;
                    cbNewClassFriday.Enabled = true;
                }
                if (classDay[6] == "1")
                {
                    cbNewClassSaturday.Checked = true;
                    cbNewClassSaturday.Enabled = true;
                }
            }

            dtpNewClassStartDate.Value = DateTime.Parse(classData.StartDate);
            txtNewClassPeriod.Text = classData.ClassPeriod.ToString();
            dtpNewClassEndDate.Value = DateTime.Parse(classData.EndDate);
        }

        private void cbNewClassDays_CheckedChanged(object sender, EventArgs e)
        {
            if (cbNewClassSunday.Enabled)
                cbNewClassSunday.Checked = true;
            if (cbNewClassMonday.Enabled)
                cbNewClassMonday.Checked = true;
            if (cbNewClassTuesday.Enabled)
                cbNewClassTuesday.Checked = true;
            if (cbNewClassWednesday.Enabled)
                cbNewClassWednesday.Checked = true;
            if (cbNewClassThursday.Enabled)
                cbNewClassThursday.Checked = true;
            if (cbNewClassFriday.Enabled)
                cbNewClassFriday.Checked = true;
            if (cbNewClassSaturday.Enabled)
                cbNewClassSaturday.Checked = true;
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

        private void CountClassPeriodByFirstDate()
        {
            if (dtpNewClassStartDate.Value < dtpNewClassEndDate.Value)
            {
                int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);
                int tempFirstDay = firstDay;

                if (!CheckClassDaysIsStartDate(firstDay))
                {
                    int needToSub = 0;
                    while (!CheckClassDaysIsStartDate(firstDay))
                    {
                        firstDay = tempFirstDay;
                        needToSub++;
                        firstDay += needToSub;
                    }

                    dtpNewClassEndDate.Value = dtpNewClassEndDate.Value.AddDays(needToSub);
                    firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);
                }

                int classDays = CountClassDays();
                System.TimeSpan timeSpan = dtpNewClassEndDate.Value - dtpNewClassStartDate.Value;
                int dayPeriod = timeSpan.Days + 1;

                int modPeriod = dayPeriod % 7;
                int dividePeriod = dayPeriod / 7;
                int finalPeriod = dividePeriod * classDays;

                DateTime tempDay = dtpNewClassStartDate.Value.AddDays(dividePeriod * 7 - 1);

                for (int i = 1; i <= modPeriod; i++)
                {
                    int lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", tempDay.AddDays(i).DayOfWeek, null);
                    if (CheckClassDaysIsStartDate(lastDay))
                        finalPeriod++;
                }

                txtNewClassPeriod.Text = finalPeriod.ToString();
            }
        }

        private void CountClassPeriodByEndDate()
        {
            if (dtpNewClassStartDate.Value < dtpNewClassEndDate.Value)
            {
                int lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassEndDate.Value.DayOfWeek, null);
                int tempLastDay = lastDay;

                if (!CheckClassDaysIsStartDate(lastDay))
                {
                    int needToSub = 0;
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
                GetClassPriceByClassPeriod();
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

        private int CountClassDays()
        {
            int countClassDay = 0;
            if (cbNewClassSunday.Checked)
            {
                countClassDay++;
            }
            if (cbNewClassMonday.Checked)
            {
                countClassDay++;
            }
            if (cbNewClassTuesday.Checked)
            {
                countClassDay++;
            }
            if (cbNewClassWednesday.Checked)
            {
                countClassDay++;
            }
            if (cbNewClassThursday.Checked)
            {
                countClassDay++;
            }
            if (cbNewClassFriday.Checked)
            {
                countClassDay++;
            }
            if (cbNewClassSaturday.Checked)
            {
                countClassDay++;
            }
            return countClassDay;
        }

        private string[] GetClassDays()
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

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

        private void GetClassEndDate()
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (txtNewClassPeriod.Text.Trim() != "")
            {
                if ((bool)facade.FacadeFunctions("check", "number", txtNewClassPeriod.Text.Trim(), null))
                {
                    string[] classDays = GetClassDays();

                    if (classDays[0] != null && classDays[0] != "")
                    {
                        int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);

                        if (CheckClassDaysIsStartDate(firstDay))
                        {
                            DateTime endDate;

                            int modPeriod = int.Parse(txtNewClassPeriod.Text) % classDays.Length;
                            int spanPeriod = int.Parse(txtNewClassPeriod.Text) / classDays.Length;
                            endDate = dtpNewClassStartDate.Value.AddDays(7 * spanPeriod - 1 + modPeriod);

                            int lastDay = (int)facade.FacadeFunctions("count", "weekdaynumber", endDate.DayOfWeek, null);
                            int tempLastDay = lastDay;
                            if (!CheckClassDaysIsStartDate(lastDay))
                            {
                                int needToSub = 0;

                                if (tempLastDay == 0)
                                    tempLastDay = 7;

                                while (!CheckClassDaysIsStartDate(lastDay))
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
                            GetClassPriceByClassPeriod();
                        }
                    }
                }
            }
        }

        private void GetClassPriceByClassPeriod()
        {
            if (classData.ClassPeriod != int.Parse(txtNewClassPeriod.Text))
            {
                double tempClassPrice = int.Parse(lblEachClassPrice.Text) * int.Parse(txtNewClassPeriod.Text);
                txtNewClassPrice.Text = tempClassPrice.ToString();
            }
        }

        private void txtNewClassPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                lblTempNewClassPeriod.Text = txtNewClassPeriod.Text;
                GetClassEndDate();
            }
        }

        private void dtpNewClassStartDate_ValueChanged(object sender, EventArgs e)
        {
            //CountClassPeriodByFirstDate();
            lblTempNewClassPeriod.Text = txtNewClassPeriod.Text;
            GetClassEndDate();
        }

        private void dtpNewClassEndDate_ValueChanged(object sender, EventArgs e)
        {
            CountClassPeriodByEndDate();
        }

        public void CloseErrorMessage()
        {
            lblInsertErrorMsgIsShow.Text = "false";
        }

        private void CallfrmErrorMessage()
        {
            if (!bool.Parse(lblInsertErrorMsgIsShow.Text))
            {
                errorMsg = new frmErrorMessage("AddNewClass");
                errorMsg.Owner = this;
                errorMsg.Show();
            }
        }

        private void SetNewClassErrorsDefault()
        {
            lblNewClassStartDate.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassEndDate.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassPeriod.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassPrice.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassMaterialFee.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewClassTime.ForeColor = Color.FromArgb(255, 255, 128);
        }

        private bool ErrorChecker()
        {
            if (bool.Parse(lblInsertErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetNewClassErrorsDefault();
            bool isError = false;
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (dtpNewClassStartDate.Value > dtpNewClassEndDate.Value)
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassStartDate.ForeColor = Color.Red;
                lblNewClassEndDate.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("開課日期無法晚於結束日期!!");
                isError = true;
            }

            if (txtNewClassPrice.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPrice.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程價格!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassPrice.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPrice.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("課程價格只能為數字!!");
                isError = true;
            }

            if (txtNewClassMaterialFee.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassMaterialFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入教材費用!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassMaterialFee.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassMaterialFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("教材費用只能為數字!!");
                isError = true;
            }

            if (txtNewClassApplyFee.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassApplyFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入報名費用!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassApplyFee.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassApplyFee.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("報名費用只能為數字!!");
                isError = true;
            }

            int firstDay = (int)facade.FacadeFunctions("count", "weekdaynumber", dtpNewClassStartDate.Value.DayOfWeek, null);
            if (!CheckClassDaysIsStartDate(firstDay))
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassStartDate.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("開課日期未被勾選!!");
                isError = true;
            }

            if (txtNewClassPeriod.Text.Trim() == "")
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPeriod.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("請輸入課程節數!!");
                isError = true;
            }
            else if (!(bool)facade.FacadeFunctions("check", "number", txtNewClassPeriod.Text.Trim(), null))
            {
                CallfrmErrorMessage();
                //lblInsertErrorMsgIsShow.Text = "true";
                lblNewClassPeriod.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("課程節數只能為數字!!");
                isError = true;
            }

            return isError;
        }

        private void btnStudentAddClass_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ErrorChecker())
                {
                    DialogResult result = MessageBox.Show("是否確定加選此課程?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
                        emsSystem = new frmEMS();
                        emsSystem = (frmEMS)this.Owner;

                        if (lblFromStudentOrClass.Text == "Student")
                        {
                            studentInClassData = new StudentInClassDefinition(lblStudentManageClassShowStudentID.Text, lblStudentAddClassShowClassID.Text,
                                                                              (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassStartDate.Value, null),
                                                                              (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassEndDate.Value, null),
                                                                              int.Parse(txtNewClassPeriod.Text), int.Parse(txtNewClassPrice.Text), 0, int.Parse(txtNewClassApplyFee.Text), int.Parse(txtNewClassMaterialFee.Text));
                            facade.FacadeFunctions("insert", "studentinclass", (object)studentInClassData, null);

                            emsSystem.CreateSystemLogs(lblStudentManageClassShowStudentName.Text + "(" + lblStudentManageClassShowStudentID.Text + ")" + " 加選 " + lblStudentAddClassShowClassName.Text + "(" + lblStudentAddClassShowClassID.Text + ")");

                            emsSystem.ShowStudentInClassAmount();
                        }
                        else
                        {
                            ClassDefinition selectClassData = classData;
                            selectClassData.StartDate = (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassStartDate.Value, null);
                            selectClassData.EndDate = (string)facade.FacadeFunctions("format", "datebydatetime", (object)dtpNewClassEndDate.Value, null);
                            selectClassData.ClassPeriod = int.Parse(txtNewClassPeriod.Text);
                            selectClassData.ApplyFee = int.Parse(txtNewClassApplyFee.Text);
                            selectClassData.MaterialFee = int.Parse(txtNewClassMaterialFee.Text);
                            emsSystem.StudentManageClassAddNewClassByClass(selectClassData);
                            emsSystem.StudentManageClassShowAllClass();
                        }

                        CloseStudentAddNewClass();

                        //MessageBox.Show("加選課程成功!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseStudentAddNewClass();
        }

        private void CloseStudentAddNewClass()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnableButton();
            this.Close();
        }
    }
}
