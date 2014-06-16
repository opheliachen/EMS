using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class StaffDefinition
    {
        #region Variable

        private string _ID;
        private int _StaffRole;
        private string _StaffTypeName;
        private string _Name;
        private string _EngName;
        private string _StartDate;
        private string _EndDate;
        private int _LaborCover;
        private int _HealthCover;
        private int _GroupCover;
        private string _Note;
        private char _IsDeleted;

        #endregion

        #region Constructors

        public StaffDefinition()
        {
        }

        public StaffDefinition(string id, int staffRole, string staffTypeName, string name, string englishName, string startDate, string endDate,
                               int laborCover, int healthCover, int groupCover, string note, char isDeleted)
        {
            _ID = id;
            _StaffRole = staffRole;
            _StaffTypeName = staffTypeName;
            _Name = name;
            _EngName = englishName;
            _StartDate = startDate;
            _EndDate = endDate;
            _LaborCover = laborCover;
            _HealthCover = healthCover;
            _GroupCover = groupCover;
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

        public int StaffRole
        {
            get { return _StaffRole; }
            set { _StaffRole = value; }
        }

        public string StaffTypeName
        {
            get { return _StaffTypeName; }
            set { _StaffTypeName = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string EnglishName
        {
            get { return _EngName; }
            set { _EngName = value; }
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

        public int LaborCover
        {
            get { return _LaborCover; }
            set { _LaborCover = value; }
        }

        public int HealthCover
        {
            get { return _HealthCover; }
            set { _HealthCover = value; }
        }

        public int GroupCover
        {
            get { return _GroupCover; }
            set { _GroupCover = value; }
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
