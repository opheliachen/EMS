using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ClassPaymentDefinition
    {
        #region Variable

        private int _ID;
        private string _StudentID;
        private string _StudentName;
        private string _ClassID;
        private string _ClassName;
        private string _StudentInClassID;
        private int _StaffID;
        private string _StaffName;
        private string _AddDate;
        private string _EndDate;
        private int _Paid;
        private string _PayDate;
        private string _PaymentType;

        #endregion

        #region Constructors

        public ClassPaymentDefinition()
        {
        }

        public ClassPaymentDefinition(int id, string studentID, string studentName, string classID, string className, string studentInClassID, int staffID, string staffName, 
                                      string addDate, string endDate, int paid, string payDate, string paymentType)
        {
            _ID = id;
            _StudentID = studentID;
            _StudentName = studentName;
            _ClassID = classID;
            _ClassName = className;
            _StudentInClassID = studentInClassID;
            _StaffID = staffID;
            _StaffName = staffName;
            _AddDate = addDate;
            _EndDate = endDate;
            _Paid = paid;
            _PayDate = payDate;
            _PaymentType = paymentType;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
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

        public string StudentInClassID
        {
            get { return _StudentInClassID; }
            set { _StudentInClassID = value; }
        }

        public int StaffID
        {
            get { return _StaffID; }
            set { _StaffID = value; }
        }

        public string StaffName
        {
            get { return _StaffName; }
            set { _StaffName = value; }
        }

        public string AddDate
        {
            get { return _AddDate; }
            set { _AddDate = value; }
        }

        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public int Paid
        {
            get { return _Paid; }
            set { _Paid = value; }
        }

        public string PayDate
        {
            get { return _PayDate; }
            set { _PayDate = value; }
        }

        public string PaymentType
        {
            get { return _PaymentType; }
            set { _PaymentType = value; }
        }

        #endregion
    }
}
