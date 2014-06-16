using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ClassDefinition
    {
        #region Variable

        private string _ID;
        private string _ClassCategoryName;
        private string _Name;
        private string _StartDate;
        private string _EndDate;
        private int _ClassPeriod;
        private string _ClassDay;
        private int _Seat;
        private int _Price;
        private int _MaterialFee;
        private int _ApplyFee;
        private string _ClassStatus;
        private string _Teacher;
        private string _Note;
        private char _IsDeleted;

        #endregion

        #region Constructors

        public ClassDefinition()
        {
        }

        public ClassDefinition(string id, string classCategoryName, string name, string startDate, string endDate, int classPeriod, string classDay,
                               int seat, int price, string classStatus, string teacher, int materialFee, int applyFee, string note, char isDeleted)
        {
            _ID = id;
            _ClassCategoryName = classCategoryName;
            _Name = name;
            _StartDate = startDate;
            _EndDate = endDate;
            _ClassPeriod = classPeriod;
            _ClassDay = classDay;
            _Seat = seat;
            _Price = price;
            _ClassStatus = classStatus;
            _Teacher = teacher;
            _MaterialFee = materialFee;
            _ApplyFee = applyFee;
            _Note = note;
            _IsDeleted = isDeleted;
        }

        #endregion

        #region Properties

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ClassCategoryName
        {
            get { return _ClassCategoryName; }
            set { _ClassCategoryName = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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

        public int ClassPeriod
        {
            get { return _ClassPeriod; }
            set { _ClassPeriod = value; }
        }

        public string ClassDay
        {
            get { return _ClassDay; }
            set { _ClassDay = value; }
        }

        public int Seat
        {
            get { return _Seat; }
            set { _Seat = value; }
        }

        public int Price
        {
            get { return _Price; }
            set { _Price = value; }
        }

        public int MaterialFee
        {
            get { return _MaterialFee; }
            set { _MaterialFee = value; }
        }

        public int ApplyFee
        {
            get { return _ApplyFee; }
            set { _ApplyFee = value; }
        }

        public string ClassStatus
        {
            get { return _ClassStatus; }
            set { _ClassStatus = value; }
        }

        public string Teacher
        {
            get { return _Teacher; }
            set { _Teacher = value; }
        }

        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }

        public char IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        #endregion
    }
}
