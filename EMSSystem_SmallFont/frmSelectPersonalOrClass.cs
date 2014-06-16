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
    public partial class frmSelectPersonalOrClass : Form
    {
        frmSearchRecordData searchRecordData;

        public frmSelectPersonalOrClass()
        {
            InitializeComponent();
        }

        private void btnSelectByPerson_Click(object sender, EventArgs e)
        {
            ReturnfrmSearchRecord("個別");
        }

        private void btnSelectByClass_Click(object sender, EventArgs e)
        {
            ReturnfrmSearchRecord("全班");
        }

        private void ReturnfrmSearchRecord(string selectBy)
        {
            searchRecordData = new frmSearchRecordData();
            searchRecordData = (frmSearchRecordData)this.Owner;
            searchRecordData.SearchByPersonOrClass(selectBy);
            this.Close();
        }
    }
}
