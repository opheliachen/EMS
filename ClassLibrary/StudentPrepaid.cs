using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class StudentPrepaidDefinition
    {
        #region Variable

        private string _StudentID;
        private string _Date;
        private int _InMoney;
        private int _OutMoney;
        private string _Events;

        #endregion

        #region Constructors

        public StudentPrepaidDefinition()
        {
        }

        public StudentPrepaidDefinition(string studentID, string date, int inMoney, int outMoney, string events)
        {
            _StudentID = studentID;
            _Date = date;
            _InMoney = inMoney;
            _OutMoney = outMoney;
            _Events = events;
        }

        #endregion

        #region Properties

        public string StudentID
        {
            get { return _StudentID; }
            set { _StudentID = value; }
        }

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public int InMoney
        {
            get { return _InMoney; }
            set { _InMoney = value; }
        }

        public int OutMoney
        {
            get { return _OutMoney; }
            set { _OutMoney = value; }
        }

        public string Events
        {
            get { return _Events; }
            set { _Events = value; }
        }

        #endregion
    }
}
