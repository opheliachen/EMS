using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class RoomDefinition
    {
        #region Variable

        private int _ID;
        private string _ClassRoom;
        private int _SeatSpace;

        #endregion

        #region Constructors

        public RoomDefinition()
        {
        }

        public RoomDefinition(int id, string classRoom, int seatSpace)
        {
            _ID = id;
            _ClassRoom = classRoom;
            _SeatSpace = seatSpace;
        }

        #endregion

        #region Properties

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ClassRoom
        {
            get { return _ClassRoom; }
            set { _ClassRoom = value; }
        }

        public int SeatSpace
        {
            get { return _SeatSpace; }
            set { _SeatSpace = value; }
        }

        #endregion
    }
}
