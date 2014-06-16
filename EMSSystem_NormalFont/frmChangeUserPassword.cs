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
    public partial class frmChangeUserPassword : Form
    {
        frmEMS emsSystem = new frmEMS();
        frmErrorMessage errorMsg;
        FacadeLayer facade;
        StaffAccountDefinition staffAccountData = null;

        public frmChangeUserPassword()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        public void GetChangePasswordInfo(string userName, string currentPassword, string masterKey)
        {
            lblErrorMsgIsShow.Text = "false";
            lblInvisibleUserName.Text = userName;
            lblInvisibleCurrentPassword.Text = currentPassword;
            lblInvisibleCurrentMasterKey.Text = masterKey;

            if (masterKey == "")
            {
                lblNewMasterKey.Visible = false;
                txtNewMasterKey.Visible = false;
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!CheckChangePassword())
            {
                string msg = "";
                emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
                staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "staffaccountbyenglishname", lblInvisibleUserName.Text, null);
                
                if (staffAccountData != null) 
                {
                    msg = "員工 " + lblInvisibleUserName.Text + "(" + staffAccountData.StaffID + ") 變更密碼";
                    staffAccountData.Password = txtNewPassword.Text;

                    if (txtNewMasterKey.Visible) 
                    {
                        if (txtNewMasterKey.Text.Trim() != "")
                        {
                            staffAccountData.MasterKey = txtNewMasterKey.Text;
                            msg += "及安全密碼";
                        }
                    }

                    facade.FacadeFunctions("update", "staffaccount", staffAccountData, null);
                    emsSystem = new frmEMS();
                    emsSystem.CreateSystemLogs(msg);
                    MessageBox.Show("變更密碼成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CloseChangePassword();
                }
            }
        }

        private bool CheckChangePassword()
        {
            if (bool.Parse(lblErrorMsgIsShow.Text))
                errorMsg.SetErrorMsgDefault();

            SetChangePasswordErrorsDefault();
            bool isError = false;
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (txtCurrentPassword.Text.Trim() != lblInvisibleCurrentPassword.Text)
            {
                CallfrmErrorMessage();
                lblErrorMsgIsShow.Text = "true";
                lblCurrentPassword.ForeColor = Color.Red;
                errorMsg.ShowErrorMessage("目前密碼錯誤!!");
                isError = true;
            }
            else
            {
                if (txtNewPassword.Text.Trim() == "")
                {
                    CallfrmErrorMessage();
                    lblErrorMsgIsShow.Text = "true";
                    lblNewPassword.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("請輸入新密碼!!");
                    isError = true;
                }
                else if (txtNewPassword.Text.Trim() == txtCurrentPassword.Text.Trim())
                {
                    CallfrmErrorMessage();
                    lblErrorMsgIsShow.Text = "true";
                    lblNewPassword.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("新密碼不得與目前密碼相同!!");
                    isError = true;
                }
                else
                {
                    if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
                    {
                        CallfrmErrorMessage();
                        lblErrorMsgIsShow.Text = "true";
                        lblConfirmPassword.ForeColor = Color.Red;
                        errorMsg.ShowErrorMessage("確認密碼必須與新密碼相同!!");
                        isError = true;
                    }
                }
            }
            if (txtNewMasterKey.Text.Trim() != "")
            {
                if (txtNewMasterKey.Text.Trim() == txtCurrentPassword.Text.Trim() && txtNewMasterKey.Text.Trim() == lblInvisibleCurrentPassword.Text)
                {
                    CallfrmErrorMessage();
                    lblErrorMsgIsShow.Text = "true";
                    lblNewMasterKey.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("安全密碼不得與目前密碼及新密碼相同!!");
                    isError = true;
                }
                else if (txtNewMasterKey.Text.Trim() == lblInvisibleCurrentMasterKey.Text.Trim())
                {
                    CallfrmErrorMessage();
                    lblErrorMsgIsShow.Text = "true";
                    lblNewMasterKey.ForeColor = Color.Red;
                    errorMsg.ShowErrorMessage("安全密碼不得與目前安全密碼相同!!");
                    isError = true;
                }
            }

            return isError;
        }

        private void SetChangePasswordErrorsDefault()
        {
            lblCurrentPassword.ForeColor = Color.FromArgb(255, 255, 128);
            lblConfirmPassword.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewPassword.ForeColor = Color.FromArgb(255, 255, 128);
            lblNewMasterKey.ForeColor = Color.FromArgb(255, 255, 128);
        }

        public void CloseErrorMessage()
        {
            lblErrorMsgIsShow.Text = "false";
        }

        private void CallfrmErrorMessage()
        {
            if (!bool.Parse(lblErrorMsgIsShow.Text))
            {
                errorMsg = new frmErrorMessage("ChangePassword");
                errorMsg.Owner = this;
                errorMsg.Show();
            }
        }

        private void CloseChangePassword()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMS();
            this.Close();
        }

        private void btnCancelChangePassword_Click(object sender, EventArgs e)
        {
            CloseChangePassword();
        }
    }
}
