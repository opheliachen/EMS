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
    public partial class frmSaftyChecker : Form
    {
        frmEMS emsSystem = new frmEMS();

        public frmSaftyChecker()
        {
            InitializeComponent();
        }

        public void GetSaftyCheckerInfo(string currentPage, string password, string masterKey, bool needMasterKey)
        {
            lblCurrentPage.Text = currentPage;
            lblInvisibleStaffPassword.Text = password;
            lblInvisibleStaffMasterKey.Text = masterKey;

            if (needMasterKey)
                txtSaftyCheckerMasterKey.Visible = true;
            else
                txtSaftyCheckerMasterKey.Visible = false;
        }

        private void frmSaftyChecker_Load(object sender, EventArgs e)
        {
            txtSaftyCheckerPassword.Text = "";
            txtSaftyCheckerMasterKey.Text = "";

            Control c = GetNextControl((Control)panelContainSaftyChecker, true);
            if (c != null)
                c.Focus();
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

        private void btnSaftyChecker_Click(object sender, EventArgs e)
        {
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
            {
                emsSystem = new frmEMS();
                emsSystem = (frmEMS)this.Owner;
                emsSystem.LoadSubButtons(lblCurrentPage.Text);
                CloseSaftyChecker();
            }
            else
                MessageBox.Show("密碼錯誤!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CloseSaftyChecker()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnableAllMainButtons();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseSaftyChecker();
        }
    }
}
