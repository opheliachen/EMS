using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class SystemLogsDefinition
    {
        #region Variable

        private int _ID;
        private int _StaffID;
        private string _StaffName;
        private string _Date;
        private string _Action;

        #endregion

        #region Constructors

        public SystemLogsDefinition()
        {
        }

        public SystemLogsDefinition(int id, int staffID, string staffName, string date, string action)
        {
            _ID = id;
            _StaffID = staffID;
            _StaffName = staffName;
            _Date = date;
            _Action = action;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
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

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        #endregion
    }
}
