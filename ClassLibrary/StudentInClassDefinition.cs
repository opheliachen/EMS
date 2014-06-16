using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class StudentInClassDefinition
    {
        #region Variable

        private string _StudentID;
        private string _ClassID;
        private string _AddDate;
        private string _EndDate;
        private int _ClassPeriod;
        private int _ClassPrice;
        private int _Discount;
        private int _ApplyFee;
        private int _MaterialFee;

        #endregion

        #region Constructors

        public StudentInClassDefinition()
        {
        }

        public StudentInClassDefinition(string studentID, string classID, string addDate, string endDate, int classPeriod, int classPrice, int discount, int applyFee, int materialFee)
        {
            _StudentID = studentID;
            _ClassID = classID;
            _AddDate = addDate;
            _EndDate = endDate;
            _ClassPeriod = classPeriod;
            _ClassPrice = classPrice;
            _Discount = discount;
            _ApplyFee = applyFee;
            _MaterialFee = materialFee;
        }

        #endregion

        #region Properties

        public string StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }

        public string ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
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

        public int ClassPeriod
        {
            get { return _ClassPeriod; }
            set { _ClassPeriod = value; }
        }

        public int ClassPrice
        {
            get { return _ClassPrice; }
            set { _ClassPrice = value; }
        }

        public int Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public int ApplyFee
        {
            get { return _ApplyFee; }
            set { _ApplyFee = value; }
        }

        public int MaterialFee
        {
            get { return _MaterialFee; }
            set { _MaterialFee = value; }
        }

        #endregion
    }
}
