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
    public partial class frmStudentPrepaid : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        
        public frmStudentPrepaid()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        public void GetStudentPrepaidInfo(string studentID, string studentName, string staffEngName)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            int prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(studentID), null).ToString());
            lblStudentPaymentShowStudentID.Text = studentID;
            lblStudentPaymentShowStudentName.Text = studentName;
            lblStudentPaymentPrepaidShowCurrentPrepaid.Text = prePaid.ToString();
            lblInvisibleStaffEnglishName.Text = staffEngName;
        }

        private void btnStudentPaymentPrepaidInputPrepaid_Click(object sender, EventArgs e)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (txtStudentPaymentPrepaidInputPrepaid.Text != "")
            {
                if ((bool)facade.FacadeFunctions("check", "number", (object)txtStudentPaymentPrepaidInputPrepaid.Text, null))
                {
                    if (int.Parse(txtStudentPaymentPrepaidInputPrepaid.Text) > 0)
                    {
                        DialogResult result = MessageBox.Show("是否確定預繳?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            //bool needReceipt = false;
                            //result = MessageBox.Show("是否列印收據?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            //if (result == DialogResult.Yes)
                            //    needReceipt = true;

                            //StudentPrepaid(needReceipt);

                            //MessageBox.Show("預繳金額成功!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            //CloseStudentPrepaid();

                            ShowConfirmPrint();
                        }
                    }
                    else
                        MessageBox.Show("預繳金額必需大於零!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("預繳金額只能為數字!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("請輸入預繳金額!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void StudentPrepaid(bool needReceipt)
        {
            int prepaid = int.Parse(lblStudentPaymentPrepaidShowCurrentPrepaid.Text) + int.Parse(txtStudentPaymentPrepaidInputPrepaid.Text);
            facade.FacadeFunctions("update", "prepaid", (object)lblStudentPaymentShowStudentID.Text, (object)prepaid.ToString());

            string events = "現金";
            if (txtStudentPaymentPrepaidInputNote.Text != "")
                events = txtStudentPaymentPrepaidInputNote.Text;

            StudentPrepaidDefinition studentPrepaidData = new StudentPrepaidDefinition(lblStudentPaymentShowStudentID.Text, "",
                                                                                       int.Parse(txtStudentPaymentPrepaidInputPrepaid.Text), 0,
                                                                                       StaticFunction.SetEncodingString(events));
            facade.FacadeFunctions("insert", "studentprepaid", (object)studentPrepaidData, null);

            if (needReceipt)
            {
                string[] receiptInfo = new string[4];
                receiptInfo[0] = lblStudentPaymentShowStudentID.Text;
                receiptInfo[1] = lblStudentPaymentShowStudentName.Text;
                receiptInfo[2] = txtStudentPaymentPrepaidInputPrepaid.Text;
                receiptInfo[3] = lblInvisibleStaffEnglishName.Text;

                facade.FacadeFunctions("reusefunction", "receiptforprepaidbitmap", (object)receiptInfo, null);
            }

            lblStudentPaymentPrepaidShowCurrentPrepaid.Text = prepaid.ToString();
            txtStudentPaymentPrepaidInputPrepaid.Text = "";

            //Set Current Staff English Name
            facade.FacadeFunctions("reusefunction", "setcurrentuser", lblInvisibleStaffEnglishName.Text.Trim(), null);

            //emsSystem = new frmEMS();
            emsSystem.CreateSystemLogs(lblStudentPaymentShowStudentName.Text + "(" + lblStudentPaymentShowStudentID.Text + ")" + " 新增預繳 " + " " + txtStudentPaymentPrepaidInputPrepaid.Text + " 元");
        }

        private void ShowConfirmPrint()
        {
            this.Enabled = false;
            frmConfirmMessage confirmPrint = new frmConfirmMessage("frmStudentPrepaid", "是否列印收據?");
            confirmPrint.Owner = this;
            confirmPrint.Show();
        }

        public void StudentPrepaidAfterConfirmPrint(bool needReceipt)
        {
            this.Enabled = true;
            StudentPrepaid(needReceipt);
            MessageBox.Show("預繳金額成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            emsSystem.AfterStudentPayment();
            CloseStudentPrepaid();
        }

        private void CloseStudentPrepaid()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMS();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseStudentPrepaid();
        }
    }
}
