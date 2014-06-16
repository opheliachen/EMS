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
//using Microsoft.Office.Interop.Word;
//using Word = Microsoft.Office.Interop.Word;

namespace EMSSystem
{
    public partial class frmStudentPayment : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;
        List<StudentInClassDefinition> studentInClassSets;
        List<StudentPaymentDefinition> studentPaymentSets;
        string[] moneyInfo = new string[13];

        public frmStudentPayment()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        private void SetStudentPaymentPanelTextDefault()
        {
            //From Student Refund
            panelStudentPaymentPayRefund.Visible = false;
            txtStudentPaymentInputPayMoney.Enabled = true;

            txtStudentPaymentInputPayMoney.Text = "";
            cboStudentPaymentType.SelectedIndex = 0;
            lblStudentPaymentShowClassID.Text = "";
            lblStudentRefundShowHaveRefunded.Text = "0";
        }

        public void GetStudentPaymentInfo(List<StudentPaymentDefinition> studentPaymentDatas, string fromPage, string refundID, string haveRefundMoney, string staffEngName)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            SetStudentPaymentPanelTextDefault();

            studentPaymentSets = studentPaymentDatas;
            lblStudentPaymentShowStudentID.Text = studentPaymentDatas.ElementAt(0).StudentID;
            lblStudentPaymentShowStudentName.Text = studentPaymentDatas.ElementAt(0).StudentName;
            lblInvisibleStudentPaymentSelectNumber.Text = studentPaymentDatas.Count.ToString();
            lblStudentPaymentPayRefundShowRefundMoney.Text = haveRefundMoney;
            lblInvisibleStaffEnglishName.Text = staffEngName;
            lblStudentPaymentFromPage.Text = fromPage;
            lblStudentRefundIndex.Text = refundID;

            if (fromPage == "StudentRefund")
                panelStudentPaymentPayRefund.Visible = true;

            int totalPrice = 0, totalDiscount = 0, totalHavePaid = 0;
            int prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null).ToString());

            foreach (var studentPaymentSingle in studentPaymentDatas)
            {
                totalPrice += studentPaymentSingle.ClassPrice + studentPaymentSingle.ClassMaterialFee + studentPaymentSingle.ClassApplyFee;
                totalDiscount += studentPaymentSingle.Discount;
                totalHavePaid += studentPaymentSingle.HavePaid;
            }

            lblStudentPaymentShowNeedToPayMoney.Text = totalPrice.ToString();
            lblStudentPaymentShowHavePaidMoney.Text = totalHavePaid.ToString();
            lblStudentPaymentShowPrepaidMoney.Text = prePaid.ToString();
            lblStudentPaymentShowPaymentMoney.Text = (totalPrice - totalDiscount - totalHavePaid - prePaid).ToString();

            if (int.Parse(lblStudentPaymentShowPaymentMoney.Text) <= 0)
            {
                lblStudentPaymentShowPaymentMoney.Text = "0";
                txtStudentPaymentInputPayMoney.Text = "0";
                txtStudentPaymentInputPayMoney.Enabled = false;
            }
            else
            {
                if (studentPaymentDatas.Count > 1)
                {
                    txtStudentPaymentInputPayMoney.Text = lblStudentPaymentShowPaymentMoney.Text;
                    txtStudentPaymentInputPayMoney.Enabled = false;
                }
            }
            

            //lblStudentPaymentPayRefundShowRefundMoney.Text = (totalPrice - totalDiscount - totalHavePaid - prePaid).ToString(); //From Student Refund
            lblStudentPaymentShowDiscountMoney.Text = totalDiscount.ToString();
        }

        private void btnStudentPaymentInputPayMoney_Click(object sender, EventArgs e)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (txtStudentPaymentInputPayMoney.Enabled)
            {
                if ((bool)facade.FacadeFunctions("check", "number", (object)txtStudentPaymentInputPayMoney.Text, null))
                {
                    if (int.Parse(txtStudentPaymentInputPayMoney.Text) != 0)
                    {
                        if (int.Parse(txtStudentPaymentInputPayMoney.Text) >= 0)
                        {
                            if (cboStudentPaymentType.SelectedIndex > -1)
                            {
                                if (txtStudentPaymentInputPayMoney.Text != lblStudentPaymentShowPaymentMoney.Text)
                                {
                                    if (int.Parse(lblInvisibleStudentPaymentSelectNumber.Text) == 1)
                                    {
                                        if (int.Parse(txtStudentPaymentInputPayMoney.Text) <= int.Parse(lblStudentPaymentShowPaymentMoney.Text))
                                            StudentPaymentPayMoney();
                                        else
                                            MessageBox.Show("繳費金額不得超過實繳金額!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                    else
                                        MessageBox.Show("繳費金額需等同於實繳金額!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                else
                                    StudentPaymentPayMoney();
                            }
                            else
                                MessageBox.Show("請選擇繳費方式!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("繳費金額不得小於零!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("繳費金額不得小於零!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("繳費金額只能為數字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                StudentPaymentPayMoney();
        }

        private void PrintReceipt()
        {
            string[] receiptInfo = new string[6];
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);

            if (studentPaymentSets.Count > 1)
            {
            }
            else
            {
                receiptInfo[0] = lblStudentPaymentShowStudentID.Text;
                receiptInfo[1] = lblStudentPaymentShowStudentName.Text;
                receiptInfo[2] = studentPaymentSets.ElementAt(0).ClassID;
                receiptInfo[3] = studentPaymentSets.ElementAt(0).ClassName;
                receiptInfo[4] = txtStudentPaymentInputPayMoney.Text.Trim();
                receiptInfo[5] = lblInvisibleStaffEnglishName.Text;

                pbReceipt.Image = (Bitmap)facade.FacadeFunctions("reusefunction", "receiptbitmap", receiptInfo, null);
                pbReceipt.Image.Save((System.Windows.Forms.Application.StartupPath + ("\\temp.jpeg")));
            }
        }

        private void txtStudentPaymentInputPayMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
                StudentPaymentPayMoney();
        }

        private void btnStudentPaymentPayRefundInputPayMoney_Click(object sender, EventArgs e)
        {
            StudentPaymentPayMoney();
        }

        private void StudentPaymentPayMoney()
        {
            DialogResult result = MessageBox.Show("是否確定繳費?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                string msg = null;
                bool needReceipt;

                if (lblStudentPaymentFromPage.Text != "StudentRefund")
                {
                    //DialogResult printResult = MessageBox.Show("是否列印收據?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    //if (printResult == DialogResult.Yes)
                    //    needReceipt = true;
                    //else
                    //    needReceipt = false;
                    ShowConfirmPrint();
                }
                else
                {
                    needReceipt = true;
                    msg = StudentPayment(needReceipt);
                    MessageBox.Show("學生付費完成!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    emsSystem.AfterStudentPayment();
                    CloseStudentPayment();
                }


                
                //CloseStudentPayment();
                //emsSystem = (frmEMS)this.Owner;

                //DialogResult printResult = MessageBox.Show("是否列印收據?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                //if (printResult == DialogResult.Yes)
                //{
                //    PrintReceipt();
                //}

                //emsSystem.EnableButton();
                //this.Close();
            }
        }

        private void ShowConfirmPrint()
        {
            this.Enabled = false;
            frmConfirmMessage confirmPrint = new frmConfirmMessage("frmStudentPayment", "是否列印收據?");
            confirmPrint.Owner = this;
            confirmPrint.Show();
        }

        public void StudentPaymentAfterConfirmPrint(bool needReceipt)
        {
            this.Enabled = true;
            StudentPayment(needReceipt);
            MessageBox.Show("學生付費完成!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
            emsSystem.AfterStudentPayment();
            CloseStudentPayment();
        }

        private string StudentPayment(bool needReceipt)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            string msg = null;

            //Set Current Staff English Name
            facade.FacadeFunctions("reusefunction", "setcurrentuser", lblInvisibleStaffEnglishName.Text.Trim(), null);

            moneyInfo[0] = lblStudentPaymentShowStudentID.Text;
            moneyInfo[2] = lblInvisibleStaffEnglishName.Text;
            moneyInfo[3] = txtStudentPaymentInputPayMoney.Text;
            moneyInfo[6] = cboStudentPaymentType.SelectedItem.ToString();
            moneyInfo[7] = needReceipt.ToString();
            moneyInfo[8] = lblStudentPaymentShowStudentName.Text;

            if (lblStudentPaymentFromPage.Text == "StudentRefund")
            {
                moneyInfo[10] = "true";
                if (int.Parse(lblStudentPaymentPayRefundShowRefundMoney.Text) >= int.Parse(lblStudentPaymentShowPaymentMoney.Text))
                {
                    moneyInfo[3] = lblStudentPaymentShowPaymentMoney.Text;
                    lblStudentRefundShowHaveRefunded.Text = (int.Parse(lblStudentRefundShowHaveRefunded.Text) + int.Parse(lblStudentPaymentShowPaymentMoney.Text)).ToString();
                }
                else
                {
                    moneyInfo[3] = lblStudentPaymentPayRefundShowRefundMoney.Text;
                    lblStudentRefundShowHaveRefunded.Text = (int.Parse(lblStudentRefundShowHaveRefunded.Text) + int.Parse(lblStudentPaymentPayRefundShowRefundMoney.Text)).ToString();
                }
            }

            if (studentPaymentSets.Count > 1)
            {
                emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
                //studentInClassSets = (List<StudentInClassDefinition>)facade.FacadeFunctions("select", "studentneedtopayclass", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null);

                //List<StudentInClassDefinition> tempStudentInClass = new List<StudentInClassDefinition>();

                //foreach (var studentPaymentSingle in studentPaymentSets)
                //{
                //    foreach (var studentInClassSingle in studentInClassSets)
                //    {
                //        if (studentPaymentSingle.ClassID == studentInClassSingle.ClassID)
                //            tempStudentInClass.Add(studentInClassSingle);
                //    }
                //}

                //studentInClassSets = tempStudentInClass;

                int dgvRowIndex = 0, classPrice = 0, discount = 0, prePaid = 0, havePaid = 0;

                prePaid = int.Parse(lblStudentPaymentShowPrepaidMoney.Text);
                foreach (var studentPaymentSingle in studentPaymentSets)
                {
                    //var studentInClassSingle = studentInClassSets.ElementAt(dgvRowIndex);
                    //ClassDefinition tempClassData = (ClassDefinition)facade.FacadeFunctions("select", "class", "ID", (object)studentPaymentSingle.ClassID);
                    classPrice = int.Parse(facade.FacadeFunctions("select", "studentneedtopaymoney", (object)int.Parse(lblStudentPaymentShowStudentID.Text), (object)studentPaymentSingle.ClassID).ToString());
                    classPrice = classPrice + studentPaymentSingle.ClassMaterialFee + studentPaymentSingle.ClassApplyFee;
                    discount = studentPaymentSingle.Discount;
                    havePaid = int.Parse(facade.FacadeFunctions("select", "studentclasshavepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), (object)studentPaymentSingle.ClassID).ToString());
                    prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null).ToString());

                    moneyInfo[1] = studentPaymentSingle.ClassID;
                    moneyInfo[4] = (classPrice - havePaid - discount).ToString();
                    moneyInfo[5] = prePaid.ToString();
                    moneyInfo[9] = studentPaymentSingle.ClassName;

                    moneyInfo = (string[])facade.FacadeFunctions("reusefunction", "payment", (object)moneyInfo, null);

                    emsSystem.CreateSystemLogs(lblStudentPaymentShowStudentName.Text + "(" + lblStudentPaymentShowStudentID.Text + ")" + " 繳交 " + studentPaymentSingle.ClassName + "(" + studentPaymentSingle.ClassID + ")" + " 費用, 共 " + (classPrice - havePaid - discount).ToString() + " 元");

                    if (lblStudentPaymentFromPage.Text == "StudentRefund")
                        emsSystem.StudentRefundSetEvent(moneyInfo[4], "新課費用", studentPaymentSingle.ClassName + "(" + studentPaymentSingle.ClassID + ")");

                    dgvRowIndex += 1;

                    //string temp = (int.Parse(txtStudentPaymentInputPayMoney.Text) + prePaid - (classPrice - havePaid - discount)).ToString();
                    //prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)int.Parse(lblStudentPaymentShowStudentID.Text), null).ToString());
                    //moneyInfo[3] = (int.Parse(txtStudentPaymentInputPayMoney.Text) - (classPrice - havePaid - discount)).ToString();
                }
            }
            else
            {
                moneyInfo[1] = studentPaymentSets.ElementAt(0).ClassID;
                moneyInfo[4] = (int.Parse(lblStudentPaymentShowNeedToPayMoney.Text) - int.Parse(lblStudentPaymentShowHavePaidMoney.Text) - int.Parse(lblStudentPaymentShowDiscountMoney.Text)).ToString();
                moneyInfo[5] = lblStudentPaymentShowPrepaidMoney.Text;
                moneyInfo[9] = studentPaymentSets.ElementAt(0).ClassName;

                facade.FacadeFunctions("reusefunction", "payment", (object)moneyInfo, null);

                emsSystem.CreateSystemLogs(lblStudentPaymentShowStudentName.Text + "(" + lblStudentPaymentShowStudentID.Text + ")" + " 繳交 " + studentPaymentSets.ElementAt(0).ClassName + "(" + studentPaymentSets.ElementAt(0).ClassID + ")" + " 費用, 共 " + moneyInfo[3] + " 元");

                if (lblStudentPaymentFromPage.Text == "StudentRefund")
                    emsSystem.StudentRefundSetEvent(moneyInfo[3], "新課費用", studentPaymentSets.ElementAt(0).ClassName + "(" + studentPaymentSets.ElementAt(0).ClassID + ")");
            }

            return msg;
        }

        private void CloseStudentPayment()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.EnablefrmEMS();
            emsSystem.EnableButton();
            emsSystem.ShowsStudentNeedToPayAmount();
            this.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseStudentPayment();
        }
    }
}
