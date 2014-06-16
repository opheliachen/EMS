using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ClassTimeDefinition
    {
        #region Variable

        private int _ID;
        private string _ClassID;
        private string _ClassName;
        private string _Time;
        //private string _Room;

        #endregion

        #region Constructors

        public ClassTimeDefinition()
        {
        }

        public ClassTimeDefinition(int id, string classID, string className, string time)
        {
            _ID = id;
            _ClassID = classID;
            _ClassName = className;
            _Time = time;
            //_Room = room;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ClassID
        {
            get { return _ClassID; }
            set { _ClassID = value; }
        }

        public string ClassName
        {
            get { return _ClassName; }
            set { _ClassName = value; }
        }

        public string Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

        //public string Room
        //{
        //    get { return _Room; }
        //    set { _Room = value; }
        //}

        #endregion
    }
}
