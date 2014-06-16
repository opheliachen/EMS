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
    public partial class frmLogin : Form
    {
        frmEMS emsSystem = new frmEMS();
        StaffAccountDefinition staffAccountData;
        StaffDefinition staffData;

        public frmLogin()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        private void frmLogin_SizeChanged(object sender, EventArgs e)
        {
            ReSize();
        }

        private void ReSize()
        {
            int mainWidth = panelLogin.Width;
            int mainHeight = panelLogin.Height;
            int mainGeneralScreenX, mainGeneralScreenY;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;

            //Set MainScreen's Location
            if (this.Width < screenWidth)
                this.Location = new Point((screenWidth - this.Width) / 2, (screenHeight - this.Height) / 2);

            //Set MainScreen Buttons' Locations
            mainGeneralScreenX = (screenWidth - panelLogin.Width) / 2;
            mainGeneralScreenY = (screenHeight - panelLogin.Height) / 2;
            panelLogin.Location = new Point(mainGeneralScreenX, mainGeneralScreenY);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginSystem();
        }

        private void LoginSystem()
        {
            if (txtUsername.Text.Trim() != "" && txtPassword.Text.Trim() != "")
            {
                string systemType = "x86";

                FacadeLayer facade = new FacadeLayer(systemType);
                staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "staffaccountbyenglishname", txtUsername.Text.Trim(), null);
                staffData = (StaffDefinition)facade.FacadeFunctions("select", "staffbyenglishname", txtUsername.Text.Trim(), null);

                if (staffAccountData == null || staffAccountData.ID == 0)
                {
                    systemType = "x64";
                    facade = new FacadeLayer(systemType);
                    staffAccountData = (StaffAccountDefinition)facade.FacadeFunctions("select", "staffaccountbyenglishname", txtUsername.Text.Trim(), null);
                    staffData = (StaffDefinition)facade.FacadeFunctions("select", "staffbyenglishname", txtUsername.Text.Trim(), null);
                }

                if (staffAccountData != null && staffAccountData.ID > 0)
                {
                    if (staffData.IsDeleted != '1')
                    {
                        if (txtPassword.Text.Trim() == staffAccountData.Password)
                        {
                            //emsSystem = new frmEMS();
                            //emsSystem = (frmEMS)this.Owner;
                            //emsSystem.CallfrmEMSFromLogin(txtUsername.Text.Trim(), txtPassword.Text.Trim(), staffAccountData.MasterKey);

                            //Format Data
                            //facade.FacadeFunctions("format", "dbdata", "", "");

                            facade.FacadeFunctions("systemsetting", "getsetting", "SystemTypeForDB", systemType);

                            SystemLogsDefinition systemLogData = new SystemLogsDefinition(0, 0, staffData.EnglishName, "", "登入系統");
                            facade.FacadeFunctions("insert", "systemlog", (object)systemLogData, null);

                            facade.FacadeFunctions("reusefunction", "setcurrentuser", staffData.EnglishName, null);
                            this.Close();
                            System.Threading.Thread newForm = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                            newForm.Start();
                        }
                        else
                        {
                            txtPassword.Text = "";
                            MessageBox.Show("帳號或密碼錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        txtPassword.Text = "";
                        MessageBox.Show("帳號或密碼錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    txtPassword.Text = "";
                    MessageBox.Show("帳號或密碼錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                txtPassword.Text = "";
                MessageBox.Show("請輸入帳號跟密碼!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void ThreadProc()
        {
            Application.Run(new frmEMS());
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                e.Handled = true;
                Control c = GetNextControl((Control)sender, true);
                if (c != null)
                    c.Focus();
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                LoginSystem();
        }

    }
}
