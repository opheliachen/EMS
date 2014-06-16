using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EMSSystem
{
    public partial class frmErrorMessage : Form
    {
        frmEMS emsSystem = new frmEMS();
        frmAddNewStudentInfo addNewStudentInfo;
        frmStudentAddNewClass studentAddNewClass;
        frmChangeUserPassword changeUserPassword;

        public frmErrorMessage()
        {
            InitializeComponent();
            SetErrorMsgDefault();
        }

        public frmErrorMessage(string frmFrom)
        {
            InitializeComponent();
            lblFromfrm.Text = frmFrom;
            SetErrorMsgDefault();
        }

        public void SetErrorMsgDefault()
        {
            panelErrorMsg.Visible = false;
            lblInvisibleErrorMsgCount.Text = "0";

            lblCreateNumTwo.Visible = false;
            lblCreateNumThree.Visible = false;
            lblCreateNumFour.Visible = false;
            lblCreateNumFive.Visible = false;
            lblCreateNumSix.Visible = false;
            lblCreateNumSeven.Visible = false;
            lblCreateNumEight.Visible = false;
            lblCreateNumNine.Visible = false;
            lblCreateNumTen.Visible = false;

            lblCreateMsgOne.Text = "";
            lblCreateMsgTwo.Text = "";
            lblCreateMsgThree.Text = "";
            lblCreateMsgFour.Text = "";
            lblCreateMsgFive.Text = "";
            lblCreateMsgSix.Text = "";
            lblCreateMsgSeven.Text = "";
            lblCreateMsgEight.Text = "";
            lblCreateMsgNine.Text = "";
            lblCreateMsgTen.Text = "";
        }

        public bool ShowErrorMessage(string errMsg)
        {
            int errMsgCount = int.Parse(lblInvisibleErrorMsgCount.Text) + 1;
            bool msgIsFull = false;
            panelErrorMsg.Visible = true;

            switch (errMsgCount)
            {
                case 1:
                    lblCreateMsgOne.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 2:
                    lblCreateNumTwo.Visible = true;
                    lblCreateMsgTwo.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 3:
                    lblCreateNumThree.Visible = true;
                    lblCreateMsgThree.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 4:
                    lblCreateNumFour.Visible = true;
                    lblCreateMsgFour.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 5:
                    lblCreateNumFive.Visible = true;
                    lblCreateMsgFive.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 6:
                    lblCreateNumSix.Visible = true;
                    lblCreateMsgSix.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 7:
                    lblCreateNumSeven.Visible = true;
                    lblCreateMsgSeven.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 8:
                    lblCreateNumEight.Visible = true;
                    lblCreateMsgEight.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 9:
                    lblCreateNumNine.Visible = true;
                    lblCreateMsgNine.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                case 10:
                    lblCreateNumTen.Visible = true;
                    lblCreateMsgTen.Text = errMsg;
                    lblInvisibleErrorMsgCount.Text = errMsgCount.ToString();
                    break;
                default:
                    //lblInvisibleErrorMsgCount.Text = "0";
                    msgIsFull = true;
                    break;
            }

            return msgIsFull;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (lblFromfrm.Text == "frmFrom")
            {
                emsSystem = new frmEMS();
                emsSystem = (frmEMS)this.Owner;
                emsSystem.CloseErrorMessage();
            }
            else if (lblFromfrm.Text == "AddNewClass")
            {
                studentAddNewClass = new frmStudentAddNewClass();
                studentAddNewClass = (frmStudentAddNewClass)this.Owner;
                studentAddNewClass.CloseErrorMessage();
            }
            else if (lblFromfrm.Text == "AddNewStudent")
            {
                addNewStudentInfo = new frmAddNewStudentInfo();
                addNewStudentInfo = (frmAddNewStudentInfo)this.Owner;
                addNewStudentInfo.CloseErrorMessage();
            }
            else if (lblFromfrm.Text == "ChangePassword")
            {
                changeUserPassword = new frmChangeUserPassword();
                changeUserPassword = (frmChangeUserPassword)this.Owner;
                changeUserPassword.CloseErrorMessage();
            }

            this.Close();
        }
    }
}
