using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ExpanseCategoryDefinition
    {
        #region Variable

        private int _ID;
        private string _Name;

        #endregion

        #region Constructors

        public ExpanseCategoryDefinition()
        {
        }

        public ExpanseCategoryDefinition(int id, string name)
        {
            _ID = id;
            _Name = name;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        #endregion
    }
}
