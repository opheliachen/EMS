using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class StudentPaymentDefinition
    {
        #region Variable

        private string _StudentID;
        private string _StudentName;
        private string _ClassID;
        private string _ClassName;
        private string _StartDate;
        private string _EndDate;
        private int _ClassPrice;
        private int _ClassMaterialFee;
        private int _ClassApplyFee;
        private int _Discount;
        private int _HavePaid;
        private int _NeedToPay;

        #endregion

        #region Constructors

        public StudentPaymentDefinition()
        {
        }

        public StudentPaymentDefinition(string studentID, string studentName, string classID, string className, string startDate, string endDate,
                                        int classPrice, int classMaterialFee, int classApplyFee, int discount, int havePaid, int needToPay)
        {
            _StudentID = studentID;
            _StudentName = studentName;
            _ClassID = classID;
            _ClassName = className;
            _StartDate = startDate;
            _EndDate = endDate;
            _ClassPrice = classPrice;
            _ClassMaterialFee = classMaterialFee;
            _ClassApplyFee = classApplyFee;
            _Discount = discount;
            _HavePaid = havePaid;
            _NeedToPay = needToPay;
        }

        #endregion

        #region Properties

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

        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public string EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public int ClassPrice
        {
            get { return _ClassPrice; }
            set { _ClassPrice = value; }
        }

        public int ClassMaterialFee
        {
            get { return _ClassMaterialFee; }
            set { _ClassMaterialFee = value; }
        }

        public int ClassApplyFee
        {
            get { return _ClassApplyFee; }
            set { _ClassApplyFee = value; }
        }

        public int Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public int HavePaid
        {
            get { return _HavePaid; }
            set { _HavePaid = value; }
        }

        public int NeedToPay
        {
            get { return _NeedToPay; }
            set { _NeedToPay = value; }
        }

        #endregion
    }
}
