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
    public partial class frmNewClassTime : Form
    {
        frmEMS emsSystem = new frmEMS();

        public frmNewClassTime()
        {
            InitializeComponent();
        }

        private void frmNewClassTime_Load(object sender, EventArgs e)
        {
            cboNewClassFromHour.Items.Clear();
            cboNewClassToHour.Items.Clear();

            LoadFromHours();
            SetErrorMsgDefault();
        }

        private void SetErrorMsgDefault()
        {
            lblNewClassTimeClassDayErrorMsg.Visible = false;
            lblNewClassTimeClassFromTimeErrorMsg.Visible = false;
            lblNewClassTimeClassToTimeErrorMsg.Visible = false;
        }

        private void cboNewClassFromHour_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboNewClassToHour.Enabled = true;
            cboNewClassToMiunte.Enabled = true;
            btnAdd.Enabled = true;

            LoadToHours();
        }

        private void LoadFromHours()
        {
            for (int i = 0; i <= 23; i++)
            {
                if (i < 10)
                    cboNewClassFromHour.Items.Add("0" + i);
                else
                    cboNewClassFromHour.Items.Add(i);
            }
        }

        private void LoadToHours()
        {
            for (int i = cboNewClassFromHour.SelectedIndex; i <= 23; i++)
            {
                if (i < 10)
                    cboNewClassToHour.Items.Add("0" + i);
                else
                    cboNewClassToHour.Items.Add(i);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnableButton();
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SetErrorMsgDefault();
            bool isError = false;
            string msg = "請填選所有欄位!!";

            if (cboNewClassDay.SelectedIndex == -1)
            {
                isError = true;
                lblNewClassTimeClassDayErrorMsg.Visible = true;
            }
            if (cboNewClassFromHour.SelectedIndex == -1 || cboNewClassFromMiunte.SelectedIndex == -1)
            {
                isError = true;
                lblNewClassTimeClassFromTimeErrorMsg.Visible = true;
            }
            if (cboNewClassToHour.SelectedIndex == -1 || cboNewClassToMiunte.SelectedIndex == -1)
            {
                isError = true;
                lblNewClassTimeClassToTimeErrorMsg.Visible = true;
            }
            if (cboNewClassFromHour.SelectedIndex > -1 && cboNewClassFromMiunte.SelectedIndex > -1 &&
                cboNewClassToHour.SelectedIndex > -1 && cboNewClassToMiunte.SelectedIndex > -1)
            {
                if (DateTime.Parse(cboNewClassFromHour.SelectedItem.ToString() + ":" + cboNewClassFromMiunte.SelectedItem.ToString()) >
                    DateTime.Parse(cboNewClassToHour.SelectedItem.ToString() + ":" + cboNewClassToMiunte.SelectedItem.ToString()))
                {
                    isError = true;
                    lblNewClassTimeClassFromTimeErrorMsg.Visible = true;
                    lblNewClassTimeClassToTimeErrorMsg.Visible = true;
                    msg = "開始時間不得晚於結束時間!!";
                }
            }

            if (!isError)
            {
                emsSystem = new frmEMS();
                emsSystem = (frmEMS)this.Owner;
                emsSystem.GetNewClassTime(cboNewClassDay.SelectedItem.ToString(),
                                          cboNewClassFromHour.SelectedItem.ToString() + ":" + cboNewClassFromMiunte.SelectedItem.ToString(),
                                          cboNewClassToHour.SelectedItem.ToString() + ":" + cboNewClassToMiunte.SelectedItem.ToString());

                emsSystem.EnableButton();
                this.Close();
            }
            else
                MessageBox.Show(msg, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
