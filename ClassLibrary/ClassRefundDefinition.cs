using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ClassRefundDefinition
    {
        #region Variable

        private int _ID;
        private int _SubID;
        private string _StudentID;
        private string _StudentName;
        private int _StaffID;
        private string _StaffName;
        private int _Refunded;
        private string _RefundDate;
        private int _Discount;
        private string _Receiver;
        private string _RefundType;

        #endregion

        #region Constructors

        public ClassRefundDefinition()
        {
        }

        public ClassRefundDefinition(int id, int subID, string studentID, string studentName, int staffID, string staffName, int discount, int refunded, 
                                     string refundDate, string receiver, string refundType)
        {
            _ID = id;
            _SubID = subID;
            _StudentID = studentID;
            _StudentName = studentName;
            _StaffID = staffID;
            _StaffName = staffName;
            _Discount = discount;
            _Refunded = refunded;
            _RefundDate = refundDate;
            _Receiver = receiver;
            _RefundType = refundType;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int SubID
        {
            get { return _SubID; }
            set { _SubID = value; }
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

        public int Refunded
        {
            get { return _Refunded; }
            set { _Refunded = value; }
        }

        public string RefundDate
        {
            get { return _RefundDate; }
            set { _RefundDate = value; }
        }

        public int Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public string Receiver
        {
            get { return _Receiver; }
            set { _Receiver = value; }
        }

        public string RefundType
        {
            get { return _RefundType; }
            set { _RefundType = value; }
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

        #endregion
    }
}
