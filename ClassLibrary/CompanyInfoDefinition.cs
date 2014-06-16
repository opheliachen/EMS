using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class CompanyInfoDefinition
    {
        #region Variable

        private string _CompanyName;
        private string _CompanyManager;
        private string _StartTime;

        #endregion

        #region Constructors

        public CompanyInfoDefinition()
        {
        }

        public CompanyInfoDefinition(string companyName, string companyManager, string startTime)
        {
            _CompanyName = companyName;
            _CompanyManager = companyManager;
            _StartTime = startTime;
        }

        #endregion

        #region Properties

        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }

        public string CompanyManager
        {
            get { return _CompanyManager; }
            set { _CompanyManager = value; }
        }

        public string StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        #endregion
    }
}
