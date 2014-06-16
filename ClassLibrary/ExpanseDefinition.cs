using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class ExpanseDefinition
    {
        #region Variable

        private string _ID;
        private string _ExpanseCategoryName;
        private string _ItemName;
        private int _InsertStaffID;
        private string _InsertStaffName;
        private string _InsertDate;
        private string _UpdateStaffID;
        private string _UpdateStaffName;
        private string _UpdateDate;
        private string _ShopName;
        private double _UnitPrice;
        private int _Quantity;
        private double _TotalMoney;
        private char _IsDeleted;

        #endregion

        #region Constructors

        public ExpanseDefinition()
        {
        }

        public ExpanseDefinition(string id, string expanseCategoryName, string shopName, string itemName, int insertStaffID, string insertStaffName,
                                 string insertDate, string updateStaffID, string updateStaffName, string updateDate, double unitPrice, int quantity, double totalMoney, char isDeleted)
        {
            _ID = id;
            _ExpanseCategoryName = expanseCategoryName;
            _ShopName = shopName;
            _ItemName = itemName;
            _InsertStaffID = insertStaffID;
            _InsertStaffName = insertStaffName;
            _InsertDate = insertDate;
            _UpdateStaffID = updateStaffID;
            _UpdateStaffName = updateStaffName;
            _UpdateDate = updateDate;
            _UnitPrice = unitPrice;
            _Quantity = quantity;
            _TotalMoney = totalMoney;
            _IsDeleted = isDeleted;
        }

        #endregion

        #region Properties

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ExpanseCategoryName
        {
            get { return _ExpanseCategoryName; }
            set { _ExpanseCategoryName = value; }
        }

        public string ItemName
        {
            get { return _ItemName; }
            set { _ItemName = value; }
        }

        public int InsertStaffID
        {
            get { return _InsertStaffID; }
            set { _InsertStaffID = value; }
        }

        public string InsertStaffName
        {
            get { return _InsertStaffName; }
            set { _InsertStaffName = value; }
        }

        public string InsertDate
        {
            get { return _InsertDate; }
            set { _InsertDate = value; }
        }

        public string UpdateStaffID
        {
            get { return _UpdateStaffID; }
            set { _UpdateStaffID = value; }
        }

        public string UpdateStaffName
        {
            get { return _UpdateStaffName; }
            set { _UpdateStaffName = value; }
        }

        public string UpdateDate
        {
            get { return _UpdateDate; }
            set { _UpdateDate = value; }
        }

        public string ShopName
        {
            get { return _ShopName; }
            set { _ShopName = value; }
        }

        public double UnitPrice
        {
            get { return _UnitPrice; }
            set { _UnitPrice = value; }
        }

        public int Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public double TotalMoney
        {
            get { return _TotalMoney; }
            set { _TotalMoney = value; }
        }

        public char IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        #endregion
    }
}
