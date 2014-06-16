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
    public partial class frmPrintNeedToPayNotice : Form
    {
        frmEMS emsSystem = new frmEMS();

        public frmPrintNeedToPayNotice()
        {
            InitializeComponent();

            foreach (Control control in this.Controls)
            {
                control.Font = new Font("MingLiU", 12F, System.Drawing.FontStyle.Bold);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //DialogResult result = MessageBox.Show("是否確定?", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            //if (result == DialogResult.Yes)
            //{

            CloseNeedToPayNotice(true);

            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseNeedToPayNotice(false);
        }

        private void CloseNeedToPayNotice(bool isPrint)
        {
            emsSystem = new frmEMS();
            emsSystem = (frmEMS)this.Owner;
            emsSystem.GetNeedToPayNotice(txtNotice.Text.Trim(), isPrint);
            emsSystem.EnablefrmEMS();
            this.Close();
        }
    }
}
