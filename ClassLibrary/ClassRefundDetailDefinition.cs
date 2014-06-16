using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ClassRefundDetailDefinition
    {
        #region Variable

        private int _ID;
        private int _RefundID;
        private string _StudentID;
        private string _StudentName;
        private string _ClassID;
        private string _ClassName;
        private int _HavePaid;

        #endregion

        #region Constructors

        public ClassRefundDetailDefinition()
        {
        }

        public ClassRefundDetailDefinition(int id, int refundID, string studentID, string studentName, string classID, string className, int havePaid)
        {
            _ID = id;
            _RefundID = refundID;
            _StudentID = studentID;
            _StudentName = studentName;
            _ClassID = classID;
            _ClassName = className;
            _HavePaid = havePaid;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int RefundID
        {
            get { return _RefundID; }
            set { _RefundID = value; }
        }

        public string StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }

        public string StudentName
        {
            get { return _StudentName; }
            set { _StudentName = value; }
        }

        public string ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }

        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        public int HavePaid
        {
            get { return _HavePaid; }
            set { _HavePaid = value; }
        }

        #endregion
    }
}
