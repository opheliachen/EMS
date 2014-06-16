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
    public partial class frmAddNewStudentInfo : Form
    {
        frmEMS emsSystem = new frmEMS();
        frmErrorMessage errorMsg;
        FacadeLayer facade;
        StudentDefinition studentData;

        public frmAddNewStudentInfo()
        {
            InitializeComponent();
        }

        private void frmAddNewStudentInfo_Load(object sender, EventArgs e)
        {
            SetNewStudentQuickErrorsDefault();
            lblInsertErrorMsgIsShow.Text = "false";
        }

        private void btnCancelStudentQuick_Click(object sender, EventArgs e)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.DefaultSetting();
            CloseAddNewStudentInfo();
        }

        private void btnInsertStudentQuick_Click(object sender, EventArgs e)
        {
            if (!CheckNewStudentQuickErrors())
            {
                emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
                int studentID = 0;

                if (txtInsertStudentQuickStudentID.Text.Trim() != "")
                    studentID = int.Parse(txtInsertStudentQuickStudentID.Text.Trim());

                studentData = new StudentDefinition(studentID.ToString(),
                                    StaticFunction.SetEncodingString(txtInsertStudentQuickStudentName.Text),
                                    StaticFunction.SetEncodingString(cboInsertStudentQuickStudentSex.SelectedItem.ToString()), "", "",
                                    (string)facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null),
                                    "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, '0');

                studentID = int.Parse(facade.FacadeFunctions("insert", "student", (object)studentData, null).ToString());

                emsSystem = new frmEMS();
                emsSystem = (frmEMS)this.Owner;

                emsSystem.CreateSystemLogs("新增學生 " + txtInsertStudentQuickStudentName.Text + "(" + studentID.ToString("00000000") + ")");

                emsSystem.SetNewStudentAddNewClass(studentID.ToString("00000000"), txtInsertStudentQuickStudentName.Text);
                CloseAddNewStudentInfo();
            }
        }

        private void SetNewStudentQuickErrorsDefault()
        {
            lblInsertStudentQuickStudentID.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStudentQuickStudentName.ForeColor = Color.FromArgb(255, 255, 128);
            lblInsertStudentQuickStudentSex.ForeColor = Color.FromArgb(255, 255, 128);
        }

        public void CloseErrorMessage()
        {
            lblInsertErrorMsgIsShow.Text = "false";
        }

        private void CallfrmErrorMessage()
        {
            if (!bool.Parse(lblInsertErrorMsgIsShow.Text))
            {
                errorMsg = new frmErrorMessage("AddNewStudent");
                errorMsg.Owner = this;
                errorMsg.Show();
            }
        }

        private bool CheckNewStudentQuickErrors()
        {
            if (bool.Parse(lblInsertErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetNewStudentQuickErrorsDefault();
            bool isError = false;
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

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

        private void SetInsertStudentQuickDefault()
        {
            SetNewStudentQuickErrorsDefault();

            panelInsertStudentQuick.Visible = false;

            txtInsertStudentQuickStudentID.Text = "";
            txtInsertStudentQuickStudentName.Text = "";
            cboInsertStudentQuickStudentSex.SelectedIndex = -1;
        }

        private void CloseAddNewStudentInfo()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMS();
            this.Close();
        }
    }
}
