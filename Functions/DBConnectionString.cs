using System.Data.Odbc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace EMSSystem.Functions
{
    public class DBConnectionString
    {
        private string x86ConnectString = ConfigurationManager.AppSettings["x86ConnectString"];
        private string x64ConnectString = ConfigurationManager.AppSettings["x64ConnectString"];

        private string _SystemType;
        public DBConnectionString(string systemType)
        {
            _SystemType = systemType;
        }

        public string ConnectString
        {
            get
            {
                string connectString = x86ConnectString;

                if (!string.IsNullOrEmpty(_SystemType) && _SystemType.ToLower() == "x64")
                    connectString = x64ConnectString;

                return connectString;
            }
        }
    }
}
