using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class RecordDefinition
    {
        #region Variable

        private string _Data1ID;
        private string _Data1Name;
        private string _Data2ID;
        private string _Data2Name;
        private int _Money1;
        private int _Money2;
        private int _Discount;
        private string _Date1;
        private string _Date2;
        private string _Note1;
        private string _Note2;

        #endregion

        #region Constructors

        public RecordDefinition()
        {
        }

        public RecordDefinition(string data1ID, string data1Name, string data2ID, string data2Name, int money1, int money2, int discount,
                                string date1, string date2, string note1, string note2)
        {
            _Data1ID = data1ID;
            _Data1Name = data1Name;
            _Data2ID = data2ID;
            _Data2Name = data2Name;
            _Money1 = money1;
            _Money2 = money2;
            _Discount = discount;
            _Date1 = date1;
            _Date2 = date2;
            _Note1 = note1;
            _Note2 = note2;
        }

        #endregion

        #region Properties

        public string Data1ID
        {
            get { return _Data1ID; }
            set { _Data1ID = value; }
        }

        public string Data1Name
        {
            get { return _Data1Name; }
            set { _Data1Name = value; }
        }

        public string Data2ID
        {
            get { return _Data2ID; }
            set { _Data2ID = value; }
        }

        public string Data2Name
        {
            get { return _Data2Name; }
            set { _Data2Name = value; }
        }

        public string Date1
        {
            get { return _Date1; }
            set { _Date1 = value; }
        }

        public string Date2
        {
            get { return _Date2; }
            set { _Date2 = value; }
        }

        public int Money1
        {
            get { return _Money1; }
            set { _Money1 = value; }
        }

        public int Money2
        {
            get { return _Money2; }
            set { _Money2 = value; }
        }

        public int Discount
        {
            get { return _Discount; }
            set { _Discount = value; }
        }

        public string Note1
        {
            get { return _Note1; }
            set { _Note1 = value; }
        }

        public string Note2
        {
            get { return _Note2; }
            set { _Note2 = value; }
        }

        #endregion
    }
}
