using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class StaffAccountDefinition
    {
        #region Variable

        private int _ID;
        private int _StaffID;
        private string _StaffName;
        private string _UserName;
        private string _Password;
        private string _MasterKey;
        private int _StaffRoleID;
        private string _StaffRole;

        #endregion

        #region Constructors

        public StaffAccountDefinition()
        {
        }

        public StaffAccountDefinition(int id, int staffID, string staffName, string userName, string password, string masterKey, int staffRoleID, string staffRole)
        {
            _ID = id;
            _StaffID = staffID;
            _StaffName = staffName;
            _UserName = userName;
            _Password = password;
            _MasterKey = masterKey;
            _StaffRoleID = staffRoleID;
            _StaffRole = staffRole;
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

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public string MasterKey
        {
            get { return _MasterKey; }
            set { _MasterKey = value; }
        }

        public int StaffRoleID
        {
            get { return _StaffRoleID; }
            set { _StaffRoleID = value; }
        }

        public string StaffRole
        {
            get { return _StaffRole; }
            set { _StaffRole = value; }
        }

        #endregion
    }
}
