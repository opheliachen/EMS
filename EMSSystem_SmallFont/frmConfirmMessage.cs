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
    public partial class frmConfirmMessage : Form
    {
        frmStudentPayment studentPayment;
        frmStudentPrepaid studentPrepaid;

        public frmConfirmMessage()
        {
            InitializeComponent();
        }

        public frmConfirmMessage(string frmFrom, string msg)
        {
            InitializeComponent();
            lblFromFrm.Text = frmFrom;
            lblMsg.Text = msg;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            CloseForm();

            if (lblFromFrm.Text == "frmStudentPayment")
                studentPayment.StudentPaymentAfterConfirmPrint(false);
            else if (lblFromFrm.Text == "frmStudentPrepaid")
                studentPrepaid.StudentPrepaidAfterConfirmPrint(false);
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            CloseForm();
            if (lblFromFrm.Text == "frmStudentPayment")
                studentPayment.StudentPaymentAfterConfirmPrint(true);
            else if (lblFromFrm.Text == "frmStudentPrepaid")
                studentPrepaid.StudentPrepaidAfterConfirmPrint(true);
        }

        private void CloseForm()
        {
            if (lblFromFrm.Text == "frmStudentPayment")
            {
                studentPayment = new frmStudentPayment();
                studentPayment = (frmStudentPayment)this.Owner;
            }
            else if (lblFromFrm.Text == "frmStudentPrepaid")
            {
                studentPrepaid = new frmStudentPrepaid();
                studentPrepaid = (frmStudentPrepaid)this.Owner;
            }
            this.Close();
        }
    }
}
