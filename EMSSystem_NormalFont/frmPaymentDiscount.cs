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
    public partial class frmPaymentDiscount : Form
    {
        frmEMS emsSystem = new frmEMS();
        FacadeLayer facade;

        public frmPaymentDiscount()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        public void GetStudentDiscountInfo(StudentPaymentDefinition studentPaymentData, string staffEngName, string fromStudentOrClass)
        {
            txtStudentPaymentDiscountClassNewDiscount.Text = "";

            lblStudentPaymentDiscountShowStudentID.Text = studentPaymentData.StudentID;
            lblStudentPaymentDiscountShowStudentName.Text = studentPaymentData.StudentName;
            lblStudentPaymentDiscountShowClassID.Text = studentPaymentData.ClassID;
            lblStudentPaymentDiscountShowClassName.Text = studentPaymentData.ClassName;
            lblStudentPaymentDiscountShowClassPrice.Text = studentPaymentData.ClassPrice.ToString();
            lblStudentPaymentDiscountShowClassMaterialFee.Text = studentPaymentData.ClassMaterialFee.ToString();
            lblStudentPaymentDiscountShowOriginalDiscount.Text = studentPaymentData.Discount.ToString();
            lblStudentPaymentShowPaymentMoney.Text = (studentPaymentData.ClassPrice + studentPaymentData.ClassMaterialFee).ToString();
            lblStudentPaymentFromStudentOrClass.Text = fromStudentOrClass;
            lblInvisibleStaffEnglishName.Text = staffEngName;
        }

        private bool CheckStudentClassPaymentDiscount(string oldDiscount, string newDiscount, string needToPay)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            bool checkResult = false;

            if (newDiscount != "")
            {
                if ((bool)facade.FacadeFunctions("check", "number", (object)newDiscount, null))
                {
                    if (int.Parse(lblStudentPaymentDiscountShowOriginalDiscount.Text) >= 0)
                    {
                        if (int.Parse(newDiscount) <= int.Parse(needToPay) + int.Parse(oldDiscount))
                        {
                            checkResult = true;
                        }
                        else
                            MessageBox.Show("課程折扣不能大於實繳金額!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                        MessageBox.Show("課程折扣不得小於零!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("課程折扣只能是數字!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("請輸入課程折扣!!!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return checkResult;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            ClosePaymentDiscount();
        }

        private void ClosePaymentDiscount()
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;

            if (lblStudentPaymentFromStudentOrClass.Text == "Student")
                emsSystem.ShowsStudentNeedToPayAmount();
            else
                emsSystem.ShowsClassNeedToPayStudentList();

            emsSystem.EnablefrmEMS();
            emsSystem.EnableButton();
            this.Close();
        }

        private void btnStudentPaymentDiscountClassDiscount_Click(object sender, EventArgs e)
        {
            if (CheckStudentClassPaymentDiscount(lblStudentPaymentDiscountShowOriginalDiscount.Text, txtStudentPaymentDiscountClassNewDiscount.Text,
                                                 lblStudentPaymentShowPaymentMoney.Text))
            {
                DialogResult result = MessageBox.Show("是否確定更改課程折扣?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    UpdateStudentPaymentClassDiscount(lblStudentPaymentDiscountShowStudentID.Text, lblStudentPaymentDiscountShowClassID.Text,
                                                      txtStudentPaymentDiscountClassNewDiscount.Text);
                    //MessageBox.Show("更改折扣金額成功!!!", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClosePaymentDiscount();
                    //emsSystem.ShowsStudentNeedToPayAmount();
                    //emsSystem.EnableButton();
                    this.Close();
                }
            }
        }

        private void UpdateStudentPaymentClassDiscount(string studentID, string classID, string discount)
        {
            emsSystem = (frmEMS)this.Owner; facade = new FacadeLayer(emsSystem.SystemTypeForDB);
            StudentInClassDefinition studentInClassData = new StudentInClassDefinition(studentID, classID, "", "", 0, 0, int.Parse(discount), 0, 0);
            facade.FacadeFunctions("update", "paymentdiscount", studentInClassData, null);

            //Set Current Staff English Name
            facade.FacadeFunctions("reusefunction", "setcurrentuser", lblInvisibleStaffEnglishName.Text.Trim(), null);

            emsSystem = new frmEMS();
            emsSystem.CreateSystemLogs("修改 " + lblStudentPaymentDiscountShowStudentName.Text + "(" + studentID + ")" + " " + lblStudentPaymentDiscountShowClassName.Text + "(" + classID + ")" + " 折扣金額");
        }
    }
}
