using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class SideFunctionsDefinition
    {
        #region Variable

        private int _ID;
        private string _Function;
        private int _MainID;

        #endregion

        #region Constructors

        public SideFunctionsDefinition()
        {
        }

        public SideFunctionsDefinition(int id, string function, int mainID)
        {
            _ID = id;
            _Function = function;
            _MainID = mainID;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Function
        {
            get { return _Function; }
            set { _Function = value; }
        }

        public int MainID
        {
            get { return _MainID; }
            set { _MainID = value; }
        }

        #endregion
    }
}
