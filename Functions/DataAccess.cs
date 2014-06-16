using System;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Odbc;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
//using System.Data.DataSetExtensions;
using System.Xml.Linq;
using EMSSystem.ClassLibrary;
using EMSSystem.StaticFunctions;
using System.Text;


namespace EMSSystem.Functions
{
    public class DataAccess
    {
        private DBConnectionString dbConnString;
        private OdbcConnection conn;
        private NormalFunctions func;
        private string connectString;
        private string sSQL;

        private string _SystemType;
        public DataAccess(string systemType)
        {
            _SystemType = systemType;
        }


        private string GetNewID() 
        {
            Thread.Sleep(20);
            return DateTime.Now.ToString("yyyyMMddHHmmsssss");
        }

        #region Connection

        public void SetConnectionString()
        {
            dbConnString = new DBConnectionString(_SystemType);
            connectString = dbConnString.ConnectString;
        }

        public void OpenConnection()
        {
            SetConnectionString();
            conn = new OdbcConnection(connectString);
            conn.Open();
        }

        public void CloseConnection()
        {
            conn.Close();
        }

        #endregion


        /**********************************************************************************************
         *                                       Count Related                                        *
         * ********************************************************************************************/

        #region Count Related

        public object CountClassByClassCategoryName(string classCategoryName)
        {
            OdbcDataAdapter adapterClassCategory;
            int classCateID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassCategory = new OdbcDataAdapter();

                    sSQL = "select qryListClassCategoryIDByName('" + classCategoryName + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    classCateID = int.Parse(adapterClassCategory.SelectCommand.ExecuteScalar().ToString());

                    sSQL = "Call qryCountClassByClassCategoryID('" + classCateID + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    return (object)int.Parse(adapterClassCategory.SelectCommand.ExecuteScalar().ToString());
                }
            }
            catch
            {
                return null;
            }
        }

        public object CountStudentNumberByClassID(string classID)
        {
            OdbcDataAdapter adapterClass;
            int studentNumber = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "select qryListClassCurrentStudentNumber('" + classID + "')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    studentNumber = int.Parse(adapterClass.SelectCommand.ExecuteScalar().ToString());
                }
            }
            catch
            {
                studentNumber = 0;
            }

            return (object)studentNumber;
        }

        #endregion



        /**********************************************************************************************
         *                                       Select Related                                       *
         * ********************************************************************************************/

        #region Select Related

        public object SelectWholeData(string tableName)
        {
            OdbcDataAdapter adapterWholeTable;
            DataSet dsWholeData = new DataSet("WholeTables");
            List<ClassDefinition> classSets = new List<ClassDefinition>();
            List<ClassCategoryDefinition> classCateSets = new List<ClassCategoryDefinition>();
            List<StudentInClassDefinition> studentInClassCateSets = new List<StudentInClassDefinition>();
            List<SideFunctionsDefinition> sideFunctionsSets = new List<SideFunctionsDefinition>();
            CompanyInfoDefinition companyInfo = new CompanyInfoDefinition();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterWholeTable = new OdbcDataAdapter();

                    sSQL = "Select * From " + tableName + ";";

                    adapterWholeTable.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterWholeTable.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterWholeTable.TableMappings.Add("Table", "ShowWholeTables");
                    adapterWholeTable.Fill(dsWholeData);

                    //var classCateData = from c in dsCategory.Tables[0].AsEnumerable()
                    //                    select new ClassCategoryDefinition 
                    //                    { 
                    //                        ID = c.Field<int>("ID"),
                    //                        Name = c.Field<string>("Name")
                    //                    };

                    func = new NormalFunctions();

                    foreach (DataRow row in dsWholeData.Tables[0].Rows)
                    {
                        if (tableName == "Class")
                        {
                            //if (char.Parse(row["IsDeleted"].ToString()) == '0')
                            //{
                            int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                            classSets.Add(new ClassDefinition(row["ID"].ToString(), row["ClassCategoryID"].ToString(),
                                                              StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                              func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                              func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                              int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                              int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                              StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                              int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                              StaticFunction.GetEncodingString(row["Note"].ToString()),
                                                              char.Parse(row["IsDeleted"].ToString())));
                            //}
                        }
                        else if (tableName == "ClassCategory")
                            classCateSets.Add(new ClassCategoryDefinition(int.Parse(row["ID"].ToString()), StaticFunction.GetEncodingString(row["Name"].ToString())));
                        else if (tableName == "StudentInClass")
                            studentInClassCateSets.Add(new StudentInClassDefinition(int.Parse(row["StudentID"].ToString()).ToString("00000000"), row["ClassID"].ToString(),
                                                                                    func.ChangeDateFormatForMySql(row["AddDate"].ToString()),
                                                                                    func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                                                    int.Parse(row["ClassPeriod"].ToString()),
                                                                                    int.Parse(row["ClassPrice"].ToString()),
                                                                                    int.Parse(row["Discount"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                                                    int.Parse(row["MaterialFee"].ToString())));
                        else if (tableName == "SideFunctions")
                            sideFunctionsSets.Add(new SideFunctionsDefinition(int.Parse(row["ID"].ToString()), row["FunctionName"].ToString(),
                                                                              int.Parse(row["MainFunctionID"].ToString())));
                        else if (tableName == "CompanyInfo")
                            companyInfo = new CompanyInfoDefinition(StaticFunction.GetEncodingString(row["CompanyName"].ToString()),
                                                                    StaticFunction.GetEncodingString(row["CompanyManager"].ToString()),
                                                                    row["StartTime"].ToString());
                    }

                    if (tableName == "Class")
                        return (object)classSets;
                    else if (tableName == "ClassCategory")
                        return (object)classCateSets;
                    else if (tableName == "StudentInClass")
                        return (object)studentInClassCateSets;
                    else if (tableName == "SideFunctions")
                        return (object)sideFunctionsSets;
                    else if (tableName == "CompanyInfo")
                        return (object)companyInfo;
                    else
                        return "";
                }
            }
            catch
            {
                return null;
            }
        }

        //Student Data
        public StudentDefinition SelectStudentDataByID(string studentID, bool showAliveOnly = false)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Student");
            StudentDefinition studentData = new StudentDefinition();
            bool sendThisStudent = true;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentByID(" + studentID + ")";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowStudent");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        if (showAliveOnly)
                        {
                            if (char.Parse(row["IsDeleted"].ToString()) == '1')
                                sendThisStudent = false;
                            else
                                sendThisStudent = true;
                        }

                        if (sendThisStudent)
                        {
                            string[] sibling = new string[4];
                            sibling[0] = "0";
                            sibling[1] = "0";
                            sibling[2] = "0";
                            sibling[3] = "0";
                            if (row["Sibling"].ToString() != "")
                                sibling = row["Sibling"].ToString().Split(',');

                            studentData = new StudentDefinition(int.Parse(row["ID"].ToString()).ToString("00000000"), StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                                StaticFunction.GetEncodingString(row["Sex"].ToString()),
                                                                row["DateOfBirth"].ToString(), row["SocialNumber"].ToString(), row["StartDate"].ToString(),
                                                                StaticFunction.GetEncodingString(row["School"].ToString()),
                                                                StaticFunction.GetEncodingString(row["StudyYear"].ToString()),
                                                                StaticFunction.GetEncodingString(row["FatherName"].ToString()), StaticFunction.GetEncodingString(row["FatherWork"].ToString()),
                                                                StaticFunction.GetEncodingString(row["MotherName"].ToString()), StaticFunction.GetEncodingString(row["MotherWork"].ToString()),
                                                                sibling[0], sibling[1], sibling[2], sibling[3],
                                                                StaticFunction.GetEncodingString(row["InChargePerson"].ToString()),
                                                                row["InChargePersonHomePhone"].ToString(), row["CompanyPhone"].ToString(),
                                                                row["InChargePersonMobile"].ToString(),
                                                                StaticFunction.GetEncodingString(row["EmergencyPerson"].ToString()), row["EmergencyPhone"].ToString(),
                                                                StaticFunction.GetEncodingString(row["Address"].ToString()), row["PostCode"].ToString(), int.Parse(row["PrePaid"].ToString()), 
                                                                char.Parse(row["IsDeleted"].ToString()));
                        }
                    }

                    return studentData;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<StudentDefinition> SelectStudentDataByName(string studentName, bool showAliveOnly = false)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Student");
            List<StudentDefinition> studentSets = new List<StudentDefinition>();
            StudentDefinition studentData = new StudentDefinition();
            bool sendThisStudent = true;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentByName('%" + studentName + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowStudent");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        //string[] sibling = new string[4];
                        //sibling[0] = "0";
                        //sibling[1] = "0";
                        //sibling[2] = "0";
                        //sibling[3] = "0";
                        //if (row["Sibling"].ToString() != "")
                        //    sibling = row["Sibling"].ToString().Split(',');

                        //studentData = new StudentDefinition(int.Parse(row["ID"].ToString()).ToString("00000000"), StaticFunction.GetEncodingString(row["Name"].ToString()),
                        //                                    StaticFunction.GetEncodingString(row["Sex"].ToString()),
                        //                                    row["DateOfBirth"].ToString(), row["SocialNumber"].ToString(), row["StartDate"].ToString(),
                        //                                    StaticFunction.GetEncodingString(row["School"].ToString()),
                        //                                    StaticFunction.GetEncodingString(row["StudyYear"].ToString()),
                        //                                    StaticFunction.GetEncodingString(row["FatherName"].ToString()), StaticFunction.GetEncodingString(row["FatherWork"].ToString()),
                        //                                    StaticFunction.GetEncodingString(row["MotherName"].ToString()), StaticFunction.GetEncodingString(row["MotherWork"].ToString()),
                        //                                    sibling[0], sibling[1], sibling[2], sibling[3],
                        //                                    StaticFunction.GetEncodingString(row["InChargePerson"].ToString()),
                        //                                    row["InChargePersonHomePhone"].ToString(), row["CompanyPhone"].ToString(),
                        //                                    row["InChargePersonMobile"].ToString(),
                        //                                    StaticFunction.GetEncodingString(row["EmergencyPerson"].ToString()), row["EmergencyPhone"].ToString(),
                        //                                    StaticFunction.GetEncodingString(row["Address"].ToString()), row["PostCode"].ToString(), int.Parse(row["PrePaid"].ToString()), '0');

                        if (showAliveOnly)
                        {
                            if (char.Parse(row["IsDeleted"].ToString()) == '1')
                                sendThisStudent = false;
                            else
                                sendThisStudent = true;
                        }

                        if (sendThisStudent)
                        {
                            string[] sibling = new string[4];
                            sibling[0] = "0";
                            sibling[1] = "0";
                            sibling[2] = "0";
                            sibling[3] = "0";
                            if (row["Sibling"].ToString() != "")
                                sibling = row["Sibling"].ToString().Split(',');

                            studentData = new StudentDefinition(int.Parse(row["ID"].ToString()).ToString("00000000"), StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                                StaticFunction.GetEncodingString(row["Sex"].ToString()),
                                                                row["DateOfBirth"].ToString(), row["SocialNumber"].ToString(), row["StartDate"].ToString(),
                                                                StaticFunction.GetEncodingString(row["School"].ToString()),
                                                                StaticFunction.GetEncodingString(row["StudyYear"].ToString()),
                                                                StaticFunction.GetEncodingString(row["FatherName"].ToString()), StaticFunction.GetEncodingString(row["FatherWork"].ToString()),
                                                                StaticFunction.GetEncodingString(row["MotherName"].ToString()), StaticFunction.GetEncodingString(row["MotherWork"].ToString()),
                                                                sibling[0], sibling[1], sibling[2], sibling[3],
                                                                StaticFunction.GetEncodingString(row["InChargePerson"].ToString()),
                                                                row["InChargePersonHomePhone"].ToString(), row["CompanyPhone"].ToString(),
                                                                row["InChargePersonMobile"].ToString(),
                                                                StaticFunction.GetEncodingString(row["EmergencyPerson"].ToString()), row["EmergencyPhone"].ToString(),
                                                                StaticFunction.GetEncodingString(row["Address"].ToString()), row["PostCode"].ToString(), int.Parse(row["PrePaid"].ToString()),
                                                                char.Parse(row["IsDeleted"].ToString()));
                        }

                        studentSets.Add(studentData);
                    }

                    return studentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<StudentDefinition> SelectStudentData()
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Student");
            List<StudentDefinition> studentSets = new List<StudentDefinition>();
            StudentDefinition studentData = new StudentDefinition();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Select * From Student";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowStudent");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        string[] sibling = new string[4];
                        sibling[0] = "0";
                        sibling[1] = "0";
                        sibling[2] = "0";
                        sibling[3] = "0";
                        if (row["Sibling"].ToString() != "")
                            sibling = row["Sibling"].ToString().Split(',');

                        studentData = new StudentDefinition(int.Parse(row["ID"].ToString()).ToString("00000000"), StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                            StaticFunction.GetEncodingString(row["Sex"].ToString()),
                                                            row["DateOfBirth"].ToString(), row["SocialNumber"].ToString(), row["StartDate"].ToString(),
                                                            StaticFunction.GetEncodingString(row["School"].ToString()),
                                                            StaticFunction.GetEncodingString(row["StudyYear"].ToString()),
                                                            StaticFunction.GetEncodingString(row["FatherName"].ToString()), StaticFunction.GetEncodingString(row["FatherWork"].ToString()),
                                                            StaticFunction.GetEncodingString(row["MotherName"].ToString()), StaticFunction.GetEncodingString(row["MotherWork"].ToString()),
                                                            sibling[0], sibling[1], sibling[2], sibling[3],
                                                            StaticFunction.GetEncodingString(row["InChargePerson"].ToString()),
                                                            row["InChargePersonHomePhone"].ToString(), row["CompanyPhone"].ToString(),
                                                            row["InChargePersonMobile"].ToString(),
                                                            StaticFunction.GetEncodingString(row["EmergencyPerson"].ToString()), row["EmergencyPhone"].ToString(),
                                                            StaticFunction.GetEncodingString(row["Address"].ToString()), row["PostCode"].ToString(), int.Parse(row["PrePaid"].ToString()), '0');

                        studentSets.Add(studentData);
                    }

                    return studentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<StudentDefinition> SelectStudentDataByClassIDorName(string classItemName, string classItemValue)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Student");
            List<StudentDefinition> studentSets = new List<StudentDefinition>();
            StudentDefinition studentData = new StudentDefinition();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    if (classItemName == "ID")
                        sSQL = "Call qryListStudentByClass" + classItemName + "('" + classItemValue + "')";
                    else
                        sSQL = "Call qryListStudentByClass" + classItemName + "('%" + classItemValue + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowStudent");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        string[] sibling = new string[4];
                        sibling[0] = "0";
                        sibling[1] = "0";
                        sibling[2] = "0";
                        sibling[3] = "0";
                        if (row["Sibling"].ToString() != "")
                            sibling = row["Sibling"].ToString().Split(',');

                        studentData = new StudentDefinition(int.Parse(row["ID"].ToString()).ToString("00000000"), StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                            StaticFunction.GetEncodingString(row["Sex"].ToString()),
                                                            row["DateOfBirth"].ToString(), row["SocialNumber"].ToString(), row["StartDate"].ToString(),
                                                            StaticFunction.GetEncodingString(row["School"].ToString()),
                                                            StaticFunction.GetEncodingString(row["StudyYear"].ToString()),
                                                            StaticFunction.GetEncodingString(row["FatherName"].ToString()), StaticFunction.GetEncodingString(row["FatherWork"].ToString()),
                                                            StaticFunction.GetEncodingString(row["MotherName"].ToString()), StaticFunction.GetEncodingString(row["MotherWork"].ToString()),
                                                            sibling[0], sibling[1], sibling[2], sibling[3],
                                                            StaticFunction.GetEncodingString(row["InChargePerson"].ToString()),
                                                            row["InChargePersonHomePhone"].ToString(), row["CompanyPhone"].ToString(),
                                                            row["InChargePersonMobile"].ToString(),
                                                            StaticFunction.GetEncodingString(row["EmergencyPerson"].ToString()), row["EmergencyPhone"].ToString(),
                                                            StaticFunction.GetEncodingString(row["Address"].ToString()), row["PostCode"].ToString(), int.Parse(row["PrePaid"].ToString()), '0');

                        studentSets.Add(studentData);
                    }

                    return studentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Class Data
        public ClassDefinition SelectClassDataByIDorName(string classItemName, string classItemValue)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();
            //List<ClassDefinition> classSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    if (classItemName == "ID")
                        sSQL = "Call qryListClassBy" + classItemName + "('" + classItemValue + "')";
                    else
                        sSQL = "Call qryListClassBy" + classItemName + "('%" + classItemValue + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        //classSets.Add(classData);
                    }

                    if (classItemName != "ID" && string.IsNullOrEmpty(classData.ID))
                    {
                        sSQL = "Call qryListClassBy" + classItemName + "('%" + StaticFunction.SetEncodingString(classItemValue) + "%')";

                        adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                        adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapterClass.TableMappings.Add("Table", "ShowClass");
                        adapterClass.Fill(dsClassData);

                        func = new NormalFunctions();

                        foreach (DataRow row in dsClassData.Tables[0].Rows)
                        {
                            int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                            classData = new ClassDefinition(row["ID"].ToString(),
                                                            StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                            StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                            func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                            func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                            int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                            int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                            StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                            int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                            StaticFunction.GetEncodingString(row["Note"].ToString()), '0');
                        }
                    }

                    return classData;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassDefinition> SelectClassDataByName(string className)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();
            List<ClassDefinition> classSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListClassByName('%" + className + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        classSets.Add(classData);
                    }

                    sSQL = "Call qryListClassByName('%" + StaticFunction.SetEncodingString(className) + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        classSets.Add(classData);
                    }

                    return classSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public ClassDefinition SelectClassDataByIDAndEndDate(string classID)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();
            //List<ClassDefinition> classSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListClassByIDAndEndDate('" + classID + "')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        //classSets.Add(classData);
                    }

                    return classData;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassDefinition> SelectClassDataByNameAndEndDate(string className)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();
            List<ClassDefinition> classSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListClassByNameAndEndDate('%" + className + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        classSets.Add(classData);
                    }

                    sSQL = "Call qryListClassByNameAndEndDate('%" + StaticFunction.SetEncodingString(className) + "%')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        classSets.Add(classData);
                    }

                    return classSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassDefinition> SelectClassDataByEndDate()
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();
            List<ClassDefinition> classSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListClassByEndDate()";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        int currentStudentNumber = int.Parse(CountStudentNumberByClassID(row["ID"].ToString()).ToString());
                        classData = new ClassDefinition(row["ID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["ClassCategoryName"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), currentStudentNumber,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');

                        classSets.Add(classData);
                    }

                    return classSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassDefinition> SelectClassDataByClassCategory(string classCategoryItemName, string classCategoryItemValue)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();
            List<ClassDefinition> classDataSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListClassNameByClassCategory" + classCategoryItemName + "('" + classCategoryItemValue + "')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        classData = new ClassDefinition();
                        classData.Name = row["Name"].ToString();
                        classDataSets.Add(classData);
                    }

                    return classDataSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassTimeDefinition> SelectClassTimeByClassID(string classID)
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("ClassTime");
            List<ClassTimeDefinition> classTimeData = new List<ClassTimeDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryListClassTimeByClassID('" + classID + "')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowClassTime");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        classTimeData.Add(new ClassTimeDefinition(int.Parse(row["ID"].ToString()), row["ClassID"].ToString(), row["ClassName"].ToString(),
                                                                  row["Time"].ToString()));
                    }

                    return classTimeData;
                }
            }
            catch
            {
                return null;
            }
        }

        //Staff Data
        public StaffDefinition SelectStaffByID(int staffID)
        {
            OdbcDataAdapter adapterStaff;
            DataSet dsStaffData = new DataSet("StaffData");
            StaffDefinition staffData = null;
            string startDate = null, endDate = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryListStaffByID(" + staffID + ")";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.TableMappings.Add("Table", "ShowClass");
                    adapterStaff.Fill(dsStaffData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStaffData.Tables[0].Rows)
                    {
                        startDate = func.ChangeDateFormatForMySql(row["StartDate"].ToString());

                        if (char.Parse(row["IsDeleted"].ToString()) == '1')
                            endDate = func.ChangeDateFormatForMySql(row["endDate"].ToString());
                        else
                            endDate = "";

                        staffData = new StaffDefinition(row["ID"].ToString(), int.Parse(row["StaffRole"].ToString()), row["StaffTypeID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()), row["EnglishName"].ToString(),
                                                        startDate, endDate, int.Parse(row["LaborCover"].ToString()), int.Parse(row["HealthCover"].ToString()),
                                                        int.Parse(row["GroupCover"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), char.Parse(row["IsDeleted"].ToString()));
                    }

                    return staffData;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<StaffDefinition> SelectStaffByName(string staffName)
        {
            OdbcDataAdapter adapterStaff;
            DataSet dsStaffData = new DataSet("StaffData");
            StaffDefinition staffData = null;
            List<StaffDefinition> staffSets = new List<StaffDefinition>();
            string startDate = null, endDate = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryListStaffByName('%" + staffName + "%')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.TableMappings.Add("Table", "ShowClass");
                    adapterStaff.Fill(dsStaffData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStaffData.Tables[0].Rows)
                    {
                        startDate = func.ChangeDateFormatForMySql(row["StartDate"].ToString());

                        if (char.Parse(row["IsDeleted"].ToString()) == '1')
                            endDate = func.ChangeDateFormatForMySql(row["endDate"].ToString());
                        else
                            endDate = "";

                        staffData = new StaffDefinition(row["ID"].ToString(), int.Parse(row["StaffRole"].ToString()), row["StaffTypeID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()), row["EnglishName"].ToString(),
                                                        startDate, endDate, int.Parse(row["LaborCover"].ToString()), int.Parse(row["HealthCover"].ToString()),
                                                        int.Parse(row["GroupCover"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), char.Parse(row["IsDeleted"].ToString()));

                        staffSets.Add(staffData);
                    }

                    sSQL = "Call qryListStaffByName('%" + StaticFunction.SetEncodingString(staffName) + "%')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.TableMappings.Add("Table", "ShowClass");
                    adapterStaff.Fill(dsStaffData);

                    foreach (DataRow row in dsStaffData.Tables[0].Rows)
                    {
                        startDate = func.ChangeDateFormatForMySql(row["StartDate"].ToString());

                        if (char.Parse(row["IsDeleted"].ToString()) == '1')
                            endDate = func.ChangeDateFormatForMySql(row["endDate"].ToString());
                        else
                            endDate = "";

                        staffData = new StaffDefinition(row["ID"].ToString(), int.Parse(row["StaffRole"].ToString()), row["StaffTypeID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()), row["EnglishName"].ToString(),
                                                        startDate, endDate, int.Parse(row["LaborCover"].ToString()), int.Parse(row["HealthCover"].ToString()),
                                                        int.Parse(row["GroupCover"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), char.Parse(row["IsDeleted"].ToString()));

                        staffSets.Add(staffData);
                    }

                    return staffSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public StaffDefinition SelectStaffByEnglishName(string engName)
        {
            OdbcDataAdapter adapterStaff;
            DataSet dsStaffData = new DataSet("StaffData");
            StaffDefinition staffData = null;
            string startDate = null, endDate = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryListStaffByEnglishName('" + engName + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.TableMappings.Add("Table", "ShowClass");
                    adapterStaff.Fill(dsStaffData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStaffData.Tables[0].Rows)
                    {
                        startDate = func.ChangeDateFormatForMySql(row["StartDate"].ToString());

                        if (char.Parse(row["IsDeleted"].ToString()) == '1')
                            endDate = func.ChangeDateFormatForMySql(row["endDate"].ToString());
                        else
                            endDate = "";

                        staffData = new StaffDefinition(row["ID"].ToString(), int.Parse(row["StaffRole"].ToString()), row["StaffTypeID"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()), row["EnglishName"].ToString(),
                                                        startDate, endDate, int.Parse(row["LaborCover"].ToString()), int.Parse(row["HealthCover"].ToString()),
                                                        int.Parse(row["GroupCover"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), char.Parse(row["IsDeleted"].ToString()));
                    }

                    return staffData;
                }
            }
            catch
            {
                return null;
            }
        }

        public StaffAccountDefinition SelectStaffAccountByEnglishName(string engName)
        {
            OdbcDataAdapter adapterStaff;
            DataSet dsStaffData = new DataSet("StaffData");
            StaffAccountDefinition staffAccountData = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryListStaffAccountByEnglishName('" + engName + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.TableMappings.Add("Table", "ShowClass");
                    adapterStaff.Fill(dsStaffData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStaffData.Tables[0].Rows)
                    {
                        staffAccountData = new StaffAccountDefinition(int.Parse(row["ID"].ToString()), int.Parse(row["StaffID"].ToString()), "", engName,
                                                                      row["Password"].ToString(), row["MasterKey"].ToString(), int.Parse(row["StaffRoleID"].ToString()), row["Role"].ToString());
                    }

                    return staffAccountData;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<string> SelectStaffRoleMatchName(int staffRoleID)
        {
            OdbcDataAdapter adapterStaff;
            DataSet dsStaffData = new DataSet("StaffData");
            List<string> staffRoleList = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Select * From qryliststaffrole";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.TableMappings.Add("Table", "ShowClass");
                    adapterStaff.Fill(dsStaffData);

                    func = new NormalFunctions();

                    if (staffRoleID == 1)
                    {
                        var roleList = from r in dsStaffData.Tables[0].AsEnumerable()
                                       select r["MatchName"].ToString();

                        staffRoleList = roleList.ToList();
                    }
                    else
                    {
                        var roleList = from r in dsStaffData.Tables[0].AsEnumerable()
                                       where int.Parse(r["ID"].ToString()) > staffRoleID
                                       select r["MatchName"].ToString();

                        staffRoleList = roleList.ToList();
                    }

                    return staffRoleList;
                }
            }
            catch
            {
                return null;
            }
        }

        public bool CheckStaffEnglishNameAvailable(string engName)
        {
            OdbcDataAdapter adapterStaff;
            bool isAvaiable = false;
            object staffID = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Select qryListStaffIDByEnglishName('" + engName + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    staffID = adapterStaff.SelectCommand.ExecuteScalar().ToString();

                    if (staffID == null || staffID == "")
                        isAvaiable = true;
                }
            }
            catch
            {
            }

            return isAvaiable;
        }

        //Student In Class Data
        public List<StudentInClassDefinition> SelectStudentInClassByStudentID(int studentID)
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();
            StudentInClassDefinition studentInClassData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentInClassByStudentID(" + studentID + ")";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.TableMappings.Add("Table", "ShowStudentInClass");
                    adapterStudentInClass.Fill(dsStudentInClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentInClassData.Tables[0].Rows)
                    {
                        studentInClassData = new StudentInClassDefinition();
                        studentInClassData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentInClassData.ClassID = row["ClassID"].ToString();
                        studentInClassData.Discount = int.Parse(row["Discount"].ToString());
                        studentInClassData.ApplyFee = int.Parse(row["ApplyFee"].ToString());
                        studentInClassData.AddDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        studentInClassData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        studentInClassData.ClassPeriod = int.Parse(row["ClassPeriod"].ToString());
                        studentInClassSets.Add(studentInClassData);
                    }

                    return studentInClassSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<StudentInClassDefinition> SelectStudentInClassByClassID(string classID)
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();
            StudentInClassDefinition studentInClassData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Select * From StudentInClass Where ClassID = '" + classID + "'";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.TableMappings.Add("Table", "ShowStudentInClass");
                    adapterStudentInClass.Fill(dsStudentInClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentInClassData.Tables[0].Rows)
                    {
                        studentInClassData = new StudentInClassDefinition();
                        studentInClassData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentInClassData.ClassID = row["ClassID"].ToString();
                        studentInClassData.Discount = int.Parse(row["Discount"].ToString());
                        studentInClassData.ApplyFee = int.Parse(row["ApplyFee"].ToString());
                        studentInClassData.AddDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        studentInClassData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        studentInClassData.ClassPeriod = int.Parse(row["ClassPeriod"].ToString());
                        studentInClassSets.Add(studentInClassData);
                    }

                    return studentInClassSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public void InsertStudentInClassWithStudentIDList(string studentIDList, ClassDefinition classData)
        {
            //OdbcDataAdapter adapterStudentInClass;

            //try
            //{
            //    SetConnectionString();
            //    using (conn = new OdbcConnection(connectString))
            //    {
            //        conn.Open();

            //        adapterStudentInClass = new OdbcDataAdapter();

            //        sSQL = "Call qryInsertNewClassAndCheckIsRepeated('" + studentIDList + "', '" +
            //                                                                                           classData.ID + "', '" +
            //                                                                                           classData.StartDate + "', '" +
            //                                                                                           classData.EndDate + "', " +
            //                                                                                           classData.ClassPeriod + ", " +
            //                                                                                           classData.Price + ", " +
            //                                                                                           classData.ApplyFee + ", " +
            //                                                                                           classData.MaterialFee + ", " +
            //                                                                                           0 + ", " + 8 + ")";

            //        adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
            //        adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
            //        adapterStudentInClass.SelectCommand.ExecuteNonQuery();
            //    }
            //}
            //catch
            //{

            //}
        }

        public string SelectStudentInClassIDByIDs(int studentID, string classID)
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentInClassIDByIDs(" + studentID + ", '" + classID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    return adapterStudentInClass.SelectCommand.ExecuteScalar().ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        public string SelectStudentInClassIDByIDsForStudentPayment(int studentID, string classID)
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Select Id From StudentInClass Where StudentId = " + studentID + " And ClassId = '" + classID + "' And IsDeleted = '0'";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    return adapterStudentInClass.SelectCommand.ExecuteScalar().ToString();
                }
            }
            catch
            {
                return "";
            }
        }

        //Student Select Classes
        public List<StudentPaymentDefinition> SelectStudentSelectClassByStudentIDOrClassID(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentSelectClass;
            DataSet dsStudentSelectClassData = new DataSet("StudentSelectClass");
            List<StudentPaymentDefinition> studentSelectClassSets = new List<StudentPaymentDefinition>();
            StudentPaymentDefinition studentSelectClassData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentSelectClass = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentSelectClassByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentSelectClassByClassID('" + studentOrClassID + "')";

                    adapterStudentSelectClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentSelectClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentSelectClass.TableMappings.Add("Table", "ShowStudentSelectClass");
                    adapterStudentSelectClass.Fill(dsStudentSelectClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentSelectClassData.Tables[0].Rows)
                    {
                        studentSelectClassData = new StudentPaymentDefinition();
                        studentSelectClassData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentSelectClassData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        studentSelectClassData.ClassID = row["ClassID"].ToString();
                        studentSelectClassData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        studentSelectClassData.StartDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        studentSelectClassData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        studentSelectClassData.ClassPrice = int.Parse(row["ClassPrice"].ToString());
                        studentSelectClassData.ClassMaterialFee = int.Parse(row["ClassMaterialFee"].ToString());
                        studentSelectClassData.ClassApplyFee = int.Parse(row["ClassApplyFee"].ToString());
                        studentSelectClassData.Discount = int.Parse(row["Discount"].ToString());
                        studentSelectClassData.HavePaid = int.Parse(row["HavePaid"].ToString());
                        studentSelectClassData.NeedToPay = (studentSelectClassData.ClassPrice + studentSelectClassData.ClassMaterialFee + studentSelectClassData.ClassApplyFee) -
                                                        studentSelectClassData.Discount - studentSelectClassData.HavePaid;
                        studentSelectClassSets.Add(studentSelectClassData);
                    }

                    return studentSelectClassSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Student Have To Pay Data
        public List<StudentInClassDefinition> SelectStudentNeedToPayClassByStudentID(int studentID)
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();
            StudentInClassDefinition studentInClassData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentNeedToPayByStudentID(" + studentID + ")";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.TableMappings.Add("Table", "ShowStudentInClass");
                    adapterStudentInClass.Fill(dsStudentInClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentInClassData.Tables[0].Rows)
                    {
                        studentInClassData = new StudentInClassDefinition();
                        studentInClassData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentInClassData.ClassID = row["ClassID"].ToString();
                        studentInClassData.Discount = int.Parse(row["Discount"].ToString());
                        studentInClassSets.Add(studentInClassData);
                    }

                    return studentInClassSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public int SelectStudentNeedToPayMoneyByIDs(int studentID, string classID)
        {
            OdbcDataAdapter adapterStudentNeedToPay;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentNeedToPay = new OdbcDataAdapter();

                    sSQL = "Select qryListNeedToPayMoneyByIDs(" + studentID + ", '" + classID + "')";

                    adapterStudentNeedToPay.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentNeedToPay.SelectCommand.CommandType = CommandType.StoredProcedure;
                    return int.Parse(adapterStudentNeedToPay.SelectCommand.ExecuteScalar().ToString());
                }
            }
            catch
            {
                return 0;
            }
        }

        //Student Have Paid Data
        public int SelectStudentClassHavePaidPayment(int studentID, string classID)
        {
            OdbcDataAdapter adapterStudentInClass;
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Select qryListStudentClassHavePaidPayment(" + studentID + ", '" + classID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    return (int)adapterStudentInClass.SelectCommand.ExecuteScalar();
                }
            }
            catch
            {
                return 0;
            }
        }

        public List<StudentPaymentDefinition> SelectStudentPaymentListByStudentIDOrClassID(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<StudentPaymentDefinition> studentPaymentSets = new List<StudentPaymentDefinition>();
            StudentPaymentDefinition studentPaymentData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentNeedToPayByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentNeedToPayByClassID('" + studentOrClassID + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        studentPaymentData = new StudentPaymentDefinition();
                        studentPaymentData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentPaymentData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        studentPaymentData.ClassID = row["ClassID"].ToString();
                        studentPaymentData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        studentPaymentData.StartDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        studentPaymentData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        studentPaymentData.ClassPrice = int.Parse(row["ClassPrice"].ToString());
                        studentPaymentData.ClassMaterialFee = int.Parse(row["ClassMaterialFee"].ToString());
                        studentPaymentData.ClassApplyFee = int.Parse(row["ClassApplyFee"].ToString());
                        studentPaymentData.Discount = int.Parse(row["Discount"].ToString());
                        studentPaymentData.HavePaid = int.Parse(row["HavePaid"].ToString());
                        studentPaymentData.NeedToPay = (studentPaymentData.ClassPrice + studentPaymentData.ClassMaterialFee + studentPaymentData.ClassApplyFee) -
                                                        studentPaymentData.Discount - studentPaymentData.HavePaid;
                        studentPaymentSets.Add(studentPaymentData);
                    }

                    return studentPaymentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Student Payment Data
        public List<ClassPaymentDefinition> SelectStudentPaymentRecordByStudentID(int studentID, string[] classIDSet)
        {
            OdbcDataAdapter adapterClassPayment;
            DataSet dsClassPaymentData = new DataSet("ClassPayment");
            List<ClassPaymentDefinition> classPaymentSets = new List<ClassPaymentDefinition>();
            ClassPaymentDefinition classPaymentData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentPaymentRecordByStudentID(" + studentID + ")";

                    adapterClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassPayment.TableMappings.Add("Table", "ShowClassPayment");
                    adapterClassPayment.Fill(dsClassPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassPaymentData.Tables[0].Rows)
                    {
                        bool isTrue = false;
                        for (int i = 0; i < classIDSet.Count(); i++)
                        {
                            if (classIDSet[i] == row["ClassID"].ToString())
                                isTrue = true;
                        }

                        if (isTrue)
                        {
                            classPaymentData = new ClassPaymentDefinition();
                            classPaymentData.StudentID = studentID.ToString("00000000");
                            classPaymentData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                            classPaymentData.ClassID = row["ClassID"].ToString();
                            classPaymentData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                            classPaymentData.AddDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                            classPaymentData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                            classPaymentData.PayDate = func.ChangeDateFormatForMySql(row["PayDate"].ToString());
                            classPaymentData.Paid = int.Parse(row["Paid"].ToString());
                            classPaymentData.PaymentType = StaticFunction.GetEncodingString(row["PayType"].ToString());
                            classPaymentSets.Add(classPaymentData);
                        }
                    }

                    return classPaymentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassPaymentDefinition> SelectStudentPaymentRecordTopSixByStudentID(int studentID)
        {
            OdbcDataAdapter adapterClassPayment;
            DataSet dsClassPaymentData;
            List<ClassPaymentDefinition> classPaymentSets = new List<ClassPaymentDefinition>();
            ClassPaymentDefinition classPaymentData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassPayment = new OdbcDataAdapter();
                    dsClassPaymentData = new DataSet("ClassPayment");

                    sSQL = "Call qryListStudentPaymentRecordByStudentID(" + studentID + ")";

                    adapterClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassPayment.TableMappings.Add("Table", "ShowClassPayment");
                    adapterClassPayment.Fill(dsClassPaymentData);

                    func = new NormalFunctions();

                    List<ClassDefinition> classSet = SelectClassDataByEndDate();
                    ClassDefinition classData = new ClassDefinition();
                    foreach (DataRow row in dsClassPaymentData.Tables[0].Rows)
                    {
                        //classData = classSet.Where(c => c.ID == row["ClassID"].ToString()).SingleOrDefault();

                        //if (classData != null && !string.IsNullOrEmpty(classData.ID))
                        //{
                        classPaymentData = new ClassPaymentDefinition();
                        classPaymentData.StudentID = studentID.ToString("00000000");
                        classPaymentData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        classPaymentData.ClassID = row["ClassID"].ToString();
                        classPaymentData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        classPaymentData.AddDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        classPaymentData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        classPaymentData.PayDate = func.ChangeDateFormatForMySql(row["PayDate"].ToString());
                        classPaymentData.Paid = int.Parse(row["Paid"].ToString());
                        classPaymentData.PaymentType = StaticFunction.GetEncodingString(row["PayType"].ToString());
                        classPaymentSets.Add(classPaymentData);
                        //}
                    }
                }



                //**************************************************************************************************
                //加入預收金額明細
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {

                    conn.Open();
                    adapterClassPayment = new OdbcDataAdapter();
                    dsClassPaymentData = new DataSet("StudentPrepaid");

                    sSQL = "Call qryListPrepaidHistoryByStudentID(" + studentID + ")";

                    adapterClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassPayment.Fill(dsClassPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassPaymentData.Tables[0].Rows)
                    {
                        if (int.Parse(row["In"].ToString()) > 0)
                        {
                            classPaymentData = new ClassPaymentDefinition();
                            classPaymentData.StudentID = studentID.ToString("00000000");
                            classPaymentData.ClassName = "預收金額";
                            classPaymentData.PayDate = func.ChangeDateFormatForMySql(row["Date"].ToString());
                            classPaymentData.Paid = int.Parse(row["In"].ToString());
                            classPaymentData.PaymentType = StaticFunction.GetEncodingString(row["Event"].ToString());
                            classPaymentSets.Add(classPaymentData);
                        }
                    }

                }
                //**************************************************************************************************



                var tempClassPayment = from c in classPaymentSets
                                       orderby DateTime.Parse(c.PayDate) descending, c.ClassID
                                       select c;

                //classPaymentSets = tempClassPayment.Take(6).ToList();
                classPaymentSets = tempClassPayment.Take(10).ToList();

                return classPaymentSets;
            }
            catch
            {
                return null;
            }
        }

        public List<ClassPaymentDefinition> SelectStudentPaymentRecordByStudentIDAndClassID(int studentID, string classID)
        {
            OdbcDataAdapter adapterClassPayment;
            DataSet dsClassPaymentData = new DataSet("ClassPayment");
            List<ClassPaymentDefinition> classPaymentSets = new List<ClassPaymentDefinition>();
            ClassPaymentDefinition classPaymentData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentPaymentRecordByIDs(" + studentID + ", '" + classID + "')";

                    adapterClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassPayment.TableMappings.Add("Table", "ShowClassPayment");
                    adapterClassPayment.Fill(dsClassPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsClassPaymentData.Tables[0].Rows)
                    {
                        classPaymentData = new ClassPaymentDefinition();
                        classPaymentData.ID = int.Parse(row["ID"].ToString());
                        classPaymentData.StudentID = studentID.ToString("00000000");
                        classPaymentData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        classPaymentData.ClassID = row["ClassID"].ToString();
                        classPaymentData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        classPaymentData.AddDate = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        classPaymentData.EndDate = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        classPaymentData.PayDate = func.ChangeDateFormatForMySql(row["PayDate"].ToString());
                        classPaymentData.Paid = int.Parse(row["Paid"].ToString());
                        classPaymentData.PaymentType = StaticFunction.GetEncodingString(row["PayType"].ToString());
                        classPaymentSets.Add(classPaymentData);
                    }

                    return classPaymentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Student Have To Refund Data
        public List<StudentDefinition> SelectStudentHaveToRefundListByStudentIDOrClassIDInStudentList(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<StudentDefinition> studentSets = new List<StudentDefinition>();
            StudentDefinition studentData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentHaveToRefundByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentHaveToRefundByClassID('" + studentOrClassID + "')";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        studentData = new StudentDefinition();
                        studentData.ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentData.Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        studentSets.Add(studentData);
                    }

                    return studentSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public ClassDefinition SelectStudentHaveToRefundListByClassID(string classID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            ClassDefinition classData = null;
            List<ClassDefinition> classSets = new List<ClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentHaveToRefundClassByClassID('" + classID + "')";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        classData = new ClassDefinition(row["ID"].ToString(), "",
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), 0,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');
                        //classSets.Add(classData);
                    }

                    return classData;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassDefinition> SelectStudentHaveToRefundListByClassName(string className)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<ClassDefinition> classSets = new List<ClassDefinition>();
            ClassDefinition classData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentHaveToRefundClassByClassName('%" + className + "%')";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        classData = new ClassDefinition(row["ID"].ToString(), "",
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), 0,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');
                        classSets.Add(classData);
                    }

                    sSQL = "Call qryListStudentHaveToRefundClassByClassName('%" + StaticFunction.SetEncodingString(className) + "%')";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        classData = new ClassDefinition(row["ID"].ToString(), "",
                                                        StaticFunction.GetEncodingString(row["Name"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["StartDate"].ToString()),
                                                        func.ChangeDateFormatForMySql(row["EndDate"].ToString()),
                                                        int.Parse(row["ClassPeriod"].ToString()), row["ClassDay"].ToString(), 0,
                                                        int.Parse(row["Price"].ToString()), row["ClassStatus"].ToString(),
                                                        StaticFunction.GetEncodingString(row["Teacher"].ToString()),
                                                        int.Parse(row["MaterialFee"].ToString()), int.Parse(row["ApplyFee"].ToString()),
                                                        StaticFunction.GetEncodingString(row["Note"].ToString()), '0');
                        classSets.Add(classData);
                    }

                    return classSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<StudentPaymentDefinition> SelectStudentHaveToRefundListByStudentIDOrClassID(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<StudentPaymentDefinition> studentRefundSets = new List<StudentPaymentDefinition>();
            StudentPaymentDefinition studentRefundData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentHaveToRefundByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentHaveToRefundByClassID('%" + studentOrClassID + "%')";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        studentRefundData = new StudentPaymentDefinition();
                        studentRefundData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentRefundData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        studentRefundData.ClassID = row["ClassID"].ToString();
                        studentRefundData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        studentRefundData.ClassPrice = int.Parse(row["ClassPrice"].ToString());
                        studentRefundData.ClassMaterialFee = int.Parse(row["MaterialFee"].ToString());
                        studentRefundData.Discount = int.Parse(row["Discount"].ToString());
                        studentRefundData.HavePaid = int.Parse(row["HavePaid"].ToString());
                        studentRefundSets.Add(studentRefundData);
                    }

                    return studentRefundSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Student Refund Data
        public List<ClassRefundDefinition> SelectStudentRefundRecordByIDOrStudentIDOrClassID(string inputType, string idOrStudentOrClassID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<ClassRefundDefinition> studentRefundSets = new List<ClassRefundDefinition>();
            ClassRefundDefinition studentRefundData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentRefundByStudentID(" + idOrStudentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentRefundByClassID('" + idOrStudentOrClassID + "')";
                    else if (inputType == "WithStudentID")
                        sSQL = "Call qryListStudentRefundWithStudentID()";
                    else if (inputType == "WithClassID")
                        sSQL = "Call qryListStudentRefundWithClassID()";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    if (sSQL.IndexOf("Class") > -1)
                    {
                        foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                        {
                            studentRefundData = new ClassRefundDefinition();
                            studentRefundData.ID = int.Parse(row["ID"].ToString());
                            studentRefundData.StudentID = row["ClassID"].ToString();
                            studentRefundData.StudentName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                            studentRefundData.RefundDate = func.ChangeDateFormatForMySql(row["RefundDate"].ToString());
                            studentRefundData.Refunded = int.Parse(row["Refund"].ToString());
                            studentRefundData.Discount = int.Parse(row["Discount"].ToString());
                            studentRefundData.StaffName = StaticFunction.GetEncodingString(row["StaffName"].ToString());
                            studentRefundSets.Add(studentRefundData);
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                        {
                            studentRefundData = new ClassRefundDefinition();
                            studentRefundData.ID = int.Parse(row["ID"].ToString());
                            studentRefundData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                            studentRefundData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                            studentRefundData.RefundDate = func.ChangeDateFormatForMySql(row["RefundDate"].ToString());
                            studentRefundData.Refunded = int.Parse(row["Refund"].ToString());
                            studentRefundData.Discount = int.Parse(row["Discount"].ToString());
                            studentRefundData.StaffName = row["StaffName"].ToString();
                            studentRefundSets.Add(studentRefundData);
                        }
                    }

                    //if (inputType == "")
                    //{
                    //    sSQL = "Call qryListStudentRefundWithClassID()";

                    //    dsStudentRefundData = new DataSet("StudentRefund");
                    //    adapterStudentRefund = new OdbcDataAdapter();
                    //    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    //    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    //    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    //    adapterStudentRefund.Fill(dsStudentRefundData);

                    //    func = new NormalFunctions();

                    //    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    //    {
                    //        studentRefundData = new ClassRefundDefinition();
                    //        studentRefundData.ID = int.Parse(row["ID"].ToString());
                    //        studentRefundData.StudentID = row["ClassID"].ToString();
                    //        studentRefundData.StudentName = row["ClassName"].ToString();
                    //        studentRefundData.RefundDate = func.ChangeDateFormatForMySql(row["RefundDate"].ToString());
                    //        studentRefundData.Refunded = int.Parse(row["Refund"].ToString());
                    //        studentRefundData.Discount = int.Parse(row["Discount"].ToString());
                    //        studentRefundSets.Add(studentRefundData);
                    //    }

                    //    var tempStudentRefundSets = from sr in studentRefundSets
                    //                                orderby DateTime.Parse(sr.RefundDate)
                    //                                select new ClassRefundDefinition
                    //                                {
                    //                                    ID = sr.ID,
                    //                                    StudentID = sr.StudentID,
                    //                                    StudentName = sr.StudentName,
                    //                                    RefundDate = sr.RefundDate,
                    //                                    Refunded = sr.Refunded,
                    //                                    Discount = sr.Discount
                    //                                };

                    //    return tempStudentRefundSets.ToList();
                    //}
                    //else
                    return studentRefundSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassRefundDefinition> SelectStudentRefundRecordListByRefundIDOrStudentIDOrClassID(string inputType, string idOrStudentOrClassID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<ClassRefundDefinition> studentRefundSets = new List<ClassRefundDefinition>();
            ClassRefundDefinition studentRefundData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentRefundListByStudentID(" + idOrStudentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentRefundListByClassID('" + idOrStudentOrClassID + "')";
                    else if (inputType == "RefundID")
                        sSQL = "Call qryListStudentRefundListByRefundID(" + idOrStudentOrClassID + ")";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        studentRefundData = new ClassRefundDefinition();
                        studentRefundData.ID = int.Parse(row["ID"].ToString());
                        studentRefundData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentRefundData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        studentRefundData.RefundDate = func.ChangeDateFormatForMySql(row["RefundDate"].ToString());
                        studentRefundData.Refunded = int.Parse(row["Refund"].ToString());
                        studentRefundData.Receiver = StaticFunction.GetEncodingString(row["Receiver"].ToString());
                        studentRefundData.RefundType = StaticFunction.GetEncodingString(row["RefundType"].ToString());
                        studentRefundSets.Add(studentRefundData);
                    }

                    return studentRefundSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<ClassRefundDetailDefinition> SelectStudentRefundRecordDetailByRefundID(int refundID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<ClassRefundDetailDefinition> studentRefundDetailSets = new List<ClassRefundDetailDefinition>();
            ClassRefundDetailDefinition studentRefundDetailData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    //if (inputType == "StudentID")
                    //    sSQL = "Call qryListStudentRefundListByStudentID(" + idOrStudentOrClassID + ")";
                    //else if (inputType == "ClassID")
                    //    sSQL = "Call qryListStudentRefundListByClassID('" + idOrStudentOrClassID + "')";
                    //else if (inputType == "RefundID")
                    sSQL = "Call qryListStudentRefundDetailByRefundID(" + refundID + ")";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        studentRefundDetailData = new ClassRefundDetailDefinition();
                        studentRefundDetailData.StudentID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        studentRefundDetailData.StudentName = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        studentRefundDetailData.ClassID = row["ClassID"].ToString();
                        studentRefundDetailData.ClassName = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        studentRefundDetailData.HavePaid = int.Parse(row["HavePaid"].ToString());
                        studentRefundDetailSets.Add(studentRefundDetailData);
                    }

                    return studentRefundDetailSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Student Prepaid Data
        public int SelectStudentPrePaid(int studentID)
        {
            OdbcDataAdapter adapterStudent;
            List<StudentInClassDefinition> studentInClassSets = new List<StudentInClassDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudent = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentPrePaidByID(" + studentID + ")";

                    adapterStudent.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudent.SelectCommand.CommandType = CommandType.StoredProcedure;
                    return int.Parse(adapterStudent.SelectCommand.ExecuteScalar().ToString());
                }
            }
            catch
            {
                return 0;
            }
        }

        //Daily Expanse
        public List<ExpanseDefinition> SelectDailyExpanseByDates(string startDate, string endDate)
        {
            OdbcDataAdapter adapterDailyExpanse;
            DataSet dsDailyExpanseData = new DataSet("DailyExpanse");
            List<ExpanseDefinition> dailyExpanseDetailSets = new List<ExpanseDefinition>();
            ExpanseDefinition dailyExpanseData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterDailyExpanse = new OdbcDataAdapter();

                    sSQL = "Call qryListExpanseByDates('" + startDate + "', '" + endDate + "')";

                    adapterDailyExpanse.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterDailyExpanse.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterDailyExpanse.TableMappings.Add("Table", "ShowDailyExpanse");
                    adapterDailyExpanse.Fill(dsDailyExpanseData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsDailyExpanseData.Tables[0].Rows)
                    {
                        dailyExpanseData = new ExpanseDefinition();
                        dailyExpanseData.ID = row["ID"].ToString();
                        dailyExpanseData.ExpanseCategoryName = StaticFunction.GetEncodingString(row["ExpanseCategoryName"].ToString());
                        dailyExpanseData.ShopName = StaticFunction.GetEncodingString(row["ShopName"].ToString());
                        dailyExpanseData.ItemName = StaticFunction.GetEncodingString(row["ItemName"].ToString());
                        dailyExpanseData.UnitPrice = double.Parse(row["UnitPrice"].ToString());
                        dailyExpanseData.Quantity = int.Parse(row["Quantity"].ToString());
                        dailyExpanseData.InsertStaffID = int.Parse(row["InsertStaffID"].ToString());
                        dailyExpanseData.InsertStaffName = StaticFunction.GetEncodingString(row["InsertStaffName"].ToString());
                        dailyExpanseData.InsertDate = row["InsertDate"].ToString();

                        string today = func.ChangeDateFormatForMySql(DateTime.Now.ToShortDateString());
                        string tomorrow = func.ChangeDateFormatForMySql(DateTime.Now.AddDays(1).ToShortDateString());

                        if (startDate == today && endDate == tomorrow)
                            dailyExpanseData.InsertDate = DateTime.Parse(dailyExpanseData.InsertDate).ToLongTimeString();
                        else
                            dailyExpanseData.InsertDate = func.ChangeDateFormatForMySql(dailyExpanseData.InsertDate);

                        if (row["UpdateStaffID"] != null && row["UpdateStaffID"].ToString() != "" && int.Parse(row["UpdateStaffID"].ToString()) != 0)
                        {
                            dailyExpanseData.UpdateStaffID = row["UpdateStaffID"].ToString();
                            dailyExpanseData.UpdateStaffName = StaticFunction.GetEncodingString(row["UpdateStaffName"].ToString());
                            dailyExpanseData.UpdateDate = row["UpdateDate"].ToString();

                            if (startDate == today && endDate == tomorrow)
                                dailyExpanseData.UpdateDate = DateTime.Parse(dailyExpanseData.UpdateDate).ToLongTimeString();
                            else
                                dailyExpanseData.UpdateDate = func.ChangeDateFormatForMySql(dailyExpanseData.UpdateDate);
                        }
                        dailyExpanseData.TotalMoney = dailyExpanseData.UnitPrice * dailyExpanseData.Quantity;
                        dailyExpanseDetailSets.Add(dailyExpanseData);
                    }

                    return dailyExpanseDetailSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //System Logs
        public List<SystemLogsDefinition> SelectSystemLogs(string[] systemLogsInfo)
        {
            OdbcDataAdapter adapterSystemLogs;
            DataSet dsSystemLogsData = new DataSet("SystemLogs");
            List<SystemLogsDefinition> systemLogsSets = new List<SystemLogsDefinition>();
            SystemLogsDefinition systemLogsData;
            string startDate = null, endDate = null, searchBy = null;

            try
            {
                startDate = systemLogsInfo[0];
                endDate = systemLogsInfo[1];
                searchBy = systemLogsInfo[2];

                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterSystemLogs = new OdbcDataAdapter();

                    sSQL = "Call qryListSystemLogs('" + startDate + "', '" + endDate + "')";

                    adapterSystemLogs.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterSystemLogs.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterSystemLogs.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterSystemLogs.Fill(dsSystemLogsData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsSystemLogsData.Tables[0].Rows)
                    {
                        systemLogsData = new SystemLogsDefinition();
                        systemLogsData.Date = row["Date"].ToString();
                        systemLogsData.StaffID = int.Parse(row["StaffID"].ToString());
                        systemLogsData.StaffName = StaticFunction.GetEncodingString(row["StaffName"].ToString());
                        systemLogsData.Action = StaticFunction.GetEncodingString(row["Action"].ToString());
                        systemLogsSets.Add(systemLogsData);
                    }

                    if (searchBy != "全部")
                    {
                        var systemLog = from s in systemLogsSets
                                        where s.Action.IndexOf(searchBy) > -1
                                        select s;

                        systemLogsSets = systemLog.ToList();
                    }

                    return systemLogsSets;
                }
            }
            catch
            {
                return null;
            }
        }

        //Record
        public List<RecordDefinition> SelectStudentPaymentCountByForRecord(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentNeedToPayTotalByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentNeedToPayByClassID('" + studentOrClassID + "')";
                    else
                    {
                        sSQL = "SELECT * FROM qrylistneedtopaytotalstudent";
                        inputType = "StudentID";
                    }
                    //sSQL = "Call qryListStudentNeedToPayTotalByClassID('" + studentOrClassID + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    int havePaid = 0, discount = 0, needToPay = 0, price = 0, classSeat = 0;
                    string classID = null, className = null;
                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        if (inputType == "StudentID")
                        {
                            if (row["StudentID"] != null)
                            {
                                recordData.Data1ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                                recordData.Data1Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                                recordData.Note1 = StaticFunction.GetEncodingString(row["ChosenClass"].ToString());
                                recordData.Discount = int.Parse(row["NeedToPay"].ToString());
                                recordData.Note2 = row["HavePaid"].ToString();
                                recordSets.Add(recordData);
                            }
                        }
                        else if (inputType == "ClassID")
                        {
                            havePaid += int.Parse(row["HavePaid"].ToString());
                            discount += int.Parse(row["Discount"].ToString());
                            price += int.Parse(row["ClassPrice"].ToString()) + int.Parse(row["ClassMaterialFee"].ToString()) + int.Parse(row["ClassApplyFee"].ToString());
                            classID = row["ClassID"].ToString();
                            className = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                            classSeat++;
                            //recordData.Data1ID = row["ClassID"].ToString();
                            //recordData.Data1Name = row["ClassName"].ToString();
                            //recordData.Money1 = int.Parse(row["ClassPrice"].ToString());
                            //recordData.Money2 = int.Parse(row["ClassMaterialFee"].ToString());
                            //recordData.Note1 = row["ClassSeat"].ToString();
                            //recordData.Discount = int.Parse(row["NeedToPay"].ToString());
                            //recordData.Note2 = row["HavePaid"].ToString();
                            //recordSets.Add(recordData);
                        }
                    }

                    if (inputType == "ClassID" && classID != null)
                    {
                        needToPay = price - havePaid - discount;

                        recordData = new RecordDefinition();
                        recordData.Data1ID = classID;
                        recordData.Data1Name = className;
                        recordData.Note1 = classSeat.ToString();
                        recordData.Discount = needToPay;
                        recordData.Note2 = havePaid.ToString();
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentCountByClassID(string classID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();
            RecordDefinition recordData = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentNeedToPayByClassID('" + classID + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        tempRecordSets = SelectStudentPaymentCountByForRecord("StudentID", row["StudentID"].ToString());

                        //foreach (var recordSingle in tempRecordSets)
                        //{
                        //    recordData = new RecordDefinition();
                        //    recordData.Data1ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        //    recordData.Data1Name = row["StudentName"].ToString();
                        //    recordData.Note1 = row["ChosenClass"].ToString();
                        //    recordData.Note1 = row["HavePaid"].ToString();
                        //}
                        recordSets.AddRange(tempRecordSets);
                    }

                    if (recordSets.Count > 0)
                    {
                        var orderStudentSets = (from s in recordSets
                                                orderby s.Data1ID
                                                select new RecordDefinition
                                                {
                                                    Data1ID = s.Data1ID,
                                                    Data1Name = s.Data1Name,
                                                    Note1 = s.Note1,
                                                    Note2 = s.Note2,
                                                    Discount = s.Discount
                                                }).Distinct();

                        recordSets = orderStudentSets.ToList();
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentAllRecord()
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentPaymentRecord()";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    var result = from payment in dsStudentPaymentData.Tables[0].AsEnumerable()
                                 select new RecordDefinition
                                 {
                                     Data1ID = int.Parse(payment["StudentID"].ToString()).ToString("00000000"),
                                     Data1Name = StaticFunction.GetEncodingString(payment["StudentName"].ToString()),
                                     Data2ID = payment["ClassID"].ToString(),
                                     Data2Name = StaticFunction.GetEncodingString(payment["ClassName"].ToString()),
                                     Date1 = func.ChangeDateFormatForMySql(payment["AddDate"].ToString()),
                                     Date2 = func.ChangeDateFormatForMySql(payment["EndDate"].ToString()),
                                     Note1 = func.ChangeDateFormatForMySql(payment["PayDate"].ToString()),
                                     Money1 = int.Parse(payment["Paid"].ToString()),
                                     Note2 = StaticFunction.GetEncodingString(payment["PayType"].ToString())
                                 };

                    if (result != null && result.Count() > 0)
                        recordSets = result.ToList();
                    //foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    //{
                    //    recordData = new RecordDefinition();

                    //    recordData.Data1ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                    //    recordData.Data1Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                    //    recordData.Data2ID = row["ClassID"].ToString();
                    //    recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                    //    recordData.Date1 = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                    //    recordData.Date2 = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                    //    recordData.Note1 = func.ChangeDateFormatForMySql(row["PayDate"].ToString());
                    //    recordData.Money1 = int.Parse(row["Paid"].ToString());
                    //    recordData.Note2 = StaticFunction.GetEncodingString(row["PayType"].ToString());
                    //    recordSets.Add(recordData);
                    //}

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentWithSearchDate(string fromDate, string endDate)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentPaymentRecordWithSearchDate('" + fromDate + "', '" + endDate + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    var result = from payment in dsStudentPaymentData.Tables[0].AsEnumerable()
                                 select new RecordDefinition
                                 {
                                     Data1ID = int.Parse(payment["StudentID"].ToString()).ToString("00000000"),
                                     Data1Name = StaticFunction.GetEncodingString(payment["StudentName"].ToString()),
                                     Data2ID = payment["ClassID"].ToString(),
                                     Data2Name = StaticFunction.GetEncodingString(payment["ClassName"].ToString()),
                                     Date1 = func.ChangeDateFormatForMySql(payment["AddDate"].ToString()),
                                     Date2 = func.ChangeDateFormatForMySql(payment["EndDate"].ToString()),
                                     Note1 = func.ChangeDateFormatForMySql(payment["PayDate"].ToString()),
                                     Money1 = int.Parse(payment["Paid"].ToString()),
                                     Note2 = StaticFunction.GetEncodingString(payment["PayType"].ToString())
                                 };

                    if (result != null && result.Count() > 0)
                        recordSets = result.ToList();
                    //foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    //{
                    //    recordData = new RecordDefinition();

                    //    recordData.Data1ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                    //    recordData.Data1Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                    //    recordData.Data2ID = row["ClassID"].ToString();
                    //    recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                    //    recordData.Date1 = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                    //    recordData.Date2 = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                    //    recordData.Note1 = func.ChangeDateFormatForMySql(row["PayDate"].ToString());
                    //    recordData.Money1 = int.Parse(row["Paid"].ToString());
                    //    recordData.Note2 = StaticFunction.GetEncodingString(row["PayType"].ToString());
                    //    recordSets.Add(recordData);
                    //}

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentListByForRecord(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;
            int needToPay = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentNeedToPayByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentSelectClassByClassID('" + studentOrClassID + "')";
                    //sSQL = "Call qryListStudentNeedToPayByClassID('" + studentOrClassID + "')";
                    else
                        sSQL = "SELECT * FROM educatemanagement.qryListNeedToPayStudent e;";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        if (inputType == "StudentID")
                        {
                            recordData.Data1ID = row["ClassID"].ToString();
                            recordData.Data1Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        }
                        else if (inputType == "ClassID")
                        {
                            recordData.Data1ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                            recordData.Data1Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        }
                        else if (inputType == "")
                        {
                            recordData.Data2Name = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                            recordData.Data1ID = row["ClassID"].ToString();
                            recordData.Data1Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        }

                        recordData.Date1 = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        recordData.Date2 = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        recordData.Money1 = int.Parse(row["ClassPrice"].ToString());
                        recordData.Money2 = int.Parse(row["ClassMaterialFee"].ToString());
                        recordData.Data2ID = row["ClassApplyFee"].ToString();
                        recordData.Discount = int.Parse(row["Discount"].ToString());
                        recordData.Note1 = row["HavePaid"].ToString();
                        needToPay = (recordData.Money1 + recordData.Money2 + int.Parse(recordData.Data2ID)) - recordData.Discount - int.Parse(recordData.Note1);
                        recordData.Note2 = needToPay.ToString();
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentRecordTotal(string inputType)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentPaymentTotalRecordWithStudentID()";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentPaymentTotalRecordWithClassID()";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        if (inputType == "StudentID")
                        {
                            recordData.Data2ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                            recordData.Data2Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                            recordData.Note1 = StaticFunction.GetEncodingString(row["ChosenClass"].ToString());
                        }
                        else if (inputType == "ClassID")
                        {
                            recordData.Data2ID = row["ClassID"].ToString();
                            recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                            recordData.Note1 = row["ClassSeat"].ToString();
                        }

                        recordData.Note2 = row["HavePaid"].ToString();
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentRecordTotalByIDs(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentPaymentTotalRecordByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentPaymentTotalRecordByClassID('" + studentOrClassID + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();
                    IEnumerable<RecordDefinition> results = null;
                    if (inputType == "StudentID")
                    {
                        results = from row in dsStudentPaymentData.Tables[0].AsEnumerable()
                                  select new RecordDefinition
                                  {
                                      Data2ID = row.Field<Int64>("StudentID").ToString("00000000"),
                                      Data2Name = StaticFunction.GetEncodingString(row.Field<string>("StudentName")),
                                      Note1 = StaticFunction.GetEncodingString(row["ChosenClass"].ToString()),
                                      Note2 = row["HavePaid"].ToString()
                                  };

                    }
                    else if (inputType == "ClassID")
                    {
                        results = from row in dsStudentPaymentData.Tables[0].AsEnumerable()
                                  select new RecordDefinition
                                  {
                                      Data2ID = row.Field<string>("ClassID"),
                                      Data2Name = StaticFunction.GetEncodingString(row.Field<string>("ClassName")),
                                      Note1 = row["ClassSeat"].ToString(),
                                      Note2 = row["HavePaid"].ToString()
                                  };
                    }

                    if (results != null && results.Count() > 0)
                        recordSets = results.ToList();
                    //foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    //{
                    //    recordData = new RecordDefinition();

                    //    if (inputType == "StudentID")
                    //    {
                    //        recordData.Data2ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                    //        recordData.Data2Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                    //        recordData.Note1 = StaticFunction.GetEncodingString(row["ChosenClass"].ToString());
                    //    }
                    //    else if (inputType == "ClassID")
                    //    {
                    //        recordData.Data2ID = row["ClassID"].ToString();
                    //        recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                    //        recordData.Note1 = row["ClassSeat"].ToString();
                    //    }

                    //    //recordData.Discount = int.Parse(row["NeedToPay"].ToString());
                    //    recordData.Note2 = row["HavePaid"].ToString();
                    //    recordSets.Add(recordData);
                    //}

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentRecordTotalByDate(string inputType, string dates)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;
            string fromDate = null, endDate = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    fromDate = dates.Substring(0, 10);
                    endDate = dates.Substring(11);

                    if (inputType == "WithStudentID")
                        sSQL = "Call qryListStudentPaymentTotalRecordWithStudentIDByDate('" + fromDate + "', '" + endDate + "')";
                    else if (inputType == "WithClassID")
                        sSQL = "Call qryListStudentPaymentTotalRecordWithClassIDByDate('" + fromDate + "', '" + endDate + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        if (inputType == "WithStudentID")
                        {
                            recordData.Data2ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                            recordData.Data2Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                            recordData.Note1 = StaticFunction.GetEncodingString(row["ChosenClass"].ToString());
                        }
                        else if (inputType == "WithClassID")
                        {
                            recordData.Data2ID = row["ClassID"].ToString();
                            recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                            recordData.Note1 = row["ClassSeat"].ToString();
                        }

                        recordData.Note2 = row["HavePaid"].ToString();
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPaymentRecordListByIDs(string inputType, string studentOrClassID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPayment");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentPaymentRecordByStudentID(" + studentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentPaymentRecordByClassID('" + studentOrClassID + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPayment");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        recordData.Data1ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        recordData.Data1Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                        recordData.Data2ID = row["ClassID"].ToString();
                        recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        recordData.Date1 = func.ChangeDateFormatForMySql(row["AddDate"].ToString());
                        recordData.Date2 = func.ChangeDateFormatForMySql(row["EndDate"].ToString());
                        recordData.Note1 = func.ChangeDateFormatForMySql(row["PayDate"].ToString());
                        recordData.Money1 = int.Parse(row["Paid"].ToString());
                        recordData.Note2 = StaticFunction.GetEncodingString(row["PayType"].ToString());
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPrepaidTotalByStudentID(string studentID, string dates)
        {
            DataSet dsStudentPaymentData = new DataSet("StudentPrepaidHistory");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets;
            RecordDefinition recordData;
            StudentDefinition studentData;
            int prepaid = 0;
            string fromDate = null, endDate = null;

            try
            {
                if (studentID != "")
                {
                    recordSets = SelectStudentPrepaidHistoryByStudentID(studentID);

                    if (dates != "")
                    {
                        fromDate = dates.Substring(0, 10);
                        endDate = dates.Substring(11);

                        tempRecordSets = new List<RecordDefinition>();

                        foreach (var recordSingle in recordSets)
                        {
                            if (DateTime.Parse(recordSingle.Date1) >= DateTime.Parse(fromDate) && DateTime.Parse(recordSingle.Date1) <= DateTime.Parse(endDate))
                                tempRecordSets.Add(recordSingle);
                        }

                        recordSets = tempRecordSets;
                    }
                }
                else if (dates != null)
                {
                    fromDate = dates.Substring(0, 10);
                    endDate = dates.Substring(11);

                    recordSets = SelectStudentPrepaidHistoryByDate(fromDate, endDate);
                }

                tempRecordSets = recordSets;
                recordSets = new List<RecordDefinition>();

                foreach (var recordSingle in tempRecordSets)
                {
                    string tempStudentID = recordSingle.Data2ID;

                    var tempRecord = from r in recordSets
                                     where r.Data2ID.Equals(tempStudentID)
                                     select r;

                    if (tempRecord.Count() == 0)
                    {
                        prepaid = SelectStudentPrePaid(int.Parse(recordSingle.Data2ID));
                        studentData = SelectStudentDataByID(recordSingle.Data2ID);

                        recordData = new RecordDefinition();
                        recordData.Data2ID = studentData.ID;
                        recordData.Data2Name = studentData.Name;
                        recordData.Money1 = prepaid;
                        recordSets.Add(recordData);
                    }
                }

                return recordSets;
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPrepaidTotalByStudentIDForStudentPayment(string studentID, string dates)
        {
            var dsStudentPaymentData = new DataSet("StudentPrepaidHistory");
            var recordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets;
            RecordDefinition recordData;
            StudentDefinition studentData;
            int prepaid = 0;
            string fromDate = null, endDate = null;

            try
            {
                if (studentID != "")
                {
                    recordSets = SelectStudentPrepaidHistoryByStudentID(studentID);

                    if (dates != "")
                    {
                        fromDate = dates.Substring(0, 10);
                        endDate = dates.Substring(11);

                        tempRecordSets = new List<RecordDefinition>();

                        foreach (var recordSingle in recordSets)
                        {
                            if (DateTime.Parse(recordSingle.Date1) >= DateTime.Parse(fromDate) && DateTime.Parse(recordSingle.Date1) <= DateTime.Parse(endDate))
                                tempRecordSets.Add(recordSingle);
                        }

                        recordSets = tempRecordSets;
                    }
                }
                else if (dates != null)
                {
                    fromDate = dates.Substring(0, 10);
                    endDate = dates.Substring(11);

                    recordSets = SelectStudentPrepaidHistoryByDate(fromDate, endDate);
                }

                tempRecordSets = recordSets;
                var studentIds = recordSets.Select(u => u.Data2ID).Distinct();
                var returnSets = new List<RecordDefinition>();

                foreach (var recordSingle in studentIds)
                {
                    var tempRecord = from r in recordSets
                                     where r.Data2ID.Equals(recordSingle)
                                     select r;

                    prepaid = tempRecord.Sum(u => u.Money1);// SelectStudentPrePaid(int.Parse(recordSingle.Data2ID));
                    studentData = SelectStudentDataByID(recordSingle);

                    recordData = new RecordDefinition();
                    recordData.Data2ID = studentData.ID;
                    recordData.Data2Name = studentData.Name;
                    recordData.Money1 = prepaid;
                    returnSets.Add(recordData);
                }

                return returnSets;
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPrepaidHistoryByStudentID(string studentID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPrepaidHistory");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListPrepaidHistoryByStudentID(" + studentID + ")";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPrepaidHistory");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        recordData.Data1ID = row["ID"].ToString();
                        recordData.Data2ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        recordData.Date1 = func.ChangeDateFormatForMySql(row["Date"].ToString());
                        recordData.Money1 = int.Parse(row["In"].ToString());
                        recordData.Money2 = int.Parse(row["Out"].ToString());
                        recordData.Note1 = StaticFunction.GetEncodingString(row["Event"].ToString());
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPrepaidHistoryGroupDateByStudentID(string studentID)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPrepaidHistory");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListPrepaidHistoryByStudentID(" + studentID + ")";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPrepaidHistory");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    var dataList = from r in dsStudentPaymentData.Tables[0].AsEnumerable()
                                   group r by r["Date"].ToString() into g
                                   select new RecordDefinition
                                   {
                                       Data1ID = "",
                                       Data2ID = int.Parse(g.First()["StudentID"].ToString()).ToString("00000000"),
                                       Date1 = func.ChangeDateFormatForMySql(g.Key),
                                       Money1 = g.Select(row => int.Parse(row["In"].ToString())).Sum(),
                                       Money2 = g.Select(row => int.Parse(row["Out"].ToString())).Sum()
                                   };
                    if (dataList != null && dataList.Count() > 0)
                        recordSets.AddRange(dataList);

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentPrepaidHistoryByDate(string fromDate, string endDate)
        {
            OdbcDataAdapter adapterStudentPayment;
            DataSet dsStudentPaymentData = new DataSet("StudentPrepaidHistory");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentPayment = new OdbcDataAdapter();

                    sSQL = "Call qryListPrepaidHistoryByDate('" + fromDate + "', '" + endDate + "')";

                    adapterStudentPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPayment.TableMappings.Add("Table", "ShowStudentPrepaidHistory");
                    adapterStudentPayment.Fill(dsStudentPaymentData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentPaymentData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();

                        recordData.Data1ID = row["ID"].ToString();
                        recordData.Data2ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                        recordData.Date1 = func.ChangeDateFormatForMySql(row["Date"].ToString());
                        recordData.Money1 = int.Parse(row["In"].ToString());
                        recordData.Money2 = int.Parse(row["Out"].ToString());
                        recordData.Note1 = StaticFunction.GetEncodingString(row["Event"].ToString());
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentRefundRecordForRecord(string inputType, string idOrStudentOrClassID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    if (inputType == "StudentID")
                        sSQL = "Call qryListStudentRefundByStudentID(" + idOrStudentOrClassID + ")";
                    else if (inputType == "ClassID")
                        sSQL = "Call qryListStudentRefundByClassID('" + idOrStudentOrClassID + "')";
                    else if (inputType == "WithStudentID")
                        sSQL = "Call qryListStudentRefundWithStudentID()";
                    else if (inputType == "WithClassID")
                        sSQL = "Call qryListStudentRefundWithClassID()";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    if (sSQL.IndexOf("Class") > -1)
                    {
                        foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                        {
                            recordData = new RecordDefinition();
                            recordData.Data1ID = row["ID"].ToString();
                            recordData.Data2ID = row["ClassID"].ToString();
                            recordData.Data2Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                            recordData.Date1 = func.ChangeDateFormatForMySql(row["RefundDate"].ToString());
                            recordData.Money1 = int.Parse(row["Refund"].ToString());
                            recordData.Discount = int.Parse(row["Discount"].ToString());
                            recordData.Data1Name = StaticFunction.GetEncodingString(row["StaffName"].ToString());
                            recordSets.Add(recordData);
                        }
                    }
                    else
                    {
                        foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                        {
                            recordData = new RecordDefinition();
                            recordData.Data1ID = row["ID"].ToString();
                            recordData.Data2ID = int.Parse(row["StudentID"].ToString()).ToString("00000000");
                            recordData.Data2Name = StaticFunction.GetEncodingString(row["StudentName"].ToString());
                            recordData.Date1 = func.ChangeDateFormatForMySql(row["RefundDate"].ToString());
                            recordData.Money1 = int.Parse(row["Refund"].ToString());
                            recordData.Discount = int.Parse(row["Discount"].ToString());
                            recordData.Data1Name = StaticFunction.GetEncodingString(row["StaffName"].ToString());
                            recordSets.Add(recordData);
                        }
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentRefundRecordListByClassID(string classID)
        {
            OdbcDataAdapter adapterStudentRefund;
            DataSet dsStudentRefundData = new DataSet("StudentRefund");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentRefund = new OdbcDataAdapter();

                    sSQL = "Call qryListStudentRefundListByClassID('" + classID + "')";

                    adapterStudentRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentRefund.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentRefund.Fill(dsStudentRefundData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentRefundData.Tables[0].Rows)
                    {
                        List<RecordDefinition> tempRecordSets = SelectStudentRefundRecordForRecord("StudentID", row["StudentID"].ToString());

                        recordSets.AddRange(tempRecordSets);
                    }

                    //if (studentRefundSets.Count > 0)
                    //{
                    //    foreach (var studentSingle in studentRefundSets)
                    //        studentSets.Add(SelectStudentDataByID(studentSingle.StudentID));

                    //    studentSets.AddRange(SelectStudentDataByClassIDorName("ID", classID));
                    //}

                    if (recordSets.Count > 0)
                    {
                        //var distinctStudentSets = studentSets.Distinct();

                        var orderRecordSets = (from s in recordSets
                                               orderby int.Parse(s.Data2ID)
                                               select new RecordDefinition
                                               {
                                                   Data1ID = s.Data1ID,
                                                   Data2ID = s.Data2ID,
                                                   Data2Name = s.Data2Name,
                                                   Date1 = s.Date1,
                                                   Money1 = s.Money1,
                                                   Discount = s.Discount,
                                                   Data1Name = s.Data1Name
                                               }).Distinct();

                        //studentSets = distinctStudentSets.OrderBy(ID => ID);
                        recordSets = orderRecordSets.ToList();
                        //studentSets = finalStudentSets.ToList();
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentInClassCount(string fromDate, string endDate)
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    if (fromDate != "")
                        sSQL = "Call qryCountStudentInClassByDate('" + fromDate + "', '" + endDate + "')";
                    else
                        sSQL = "Call qryCountStudentInClass()";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentInClass.Fill(dsStudentInClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentInClassData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();
                        recordData.Data1ID = row["ClassID"].ToString();
                        recordData.Data1Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        recordData.Note1 = row["AddCount"].ToString();
                        recordData.Note2 = row["WithdrawCount"].ToString();
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public List<RecordDefinition> SelectStudentInClassTotalNumber()
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = @"SELECT ClassID, 
                                      (Select Name From Class Where ID = sc.ClassID) As ClassName,
                                      (Select Count(*) From StudentInClass Where IsDeleted = '0' And ClassID = sc.ClassID ) As StudentCount
                                    FROM   StudentInClass sc
                                    Inner Join Class cs
                                    On sc.ClassID = cs.ID
                                    Where cs.EndDate >= '{0}'
                                    And cs.IsDeleted = '0'
                                    Group  By sc.ClassID";

                    string today = DateTime.Now.ToString("yyyy/MM/dd");
                    sSQL = string.Format(sSQL, today);

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.TableMappings.Add("Table", "ShowStudentRefund");
                    adapterStudentInClass.Fill(dsStudentInClassData);

                    func = new NormalFunctions();

                    foreach (DataRow row in dsStudentInClassData.Tables[0].Rows)
                    {
                        recordData = new RecordDefinition();
                        recordData.Data1ID = row["ClassID"].ToString();
                        recordData.Data1Name = StaticFunction.GetEncodingString(row["ClassName"].ToString());
                        recordData.Note1 = row["StudentCount"].ToString();
                        recordSets.Add(recordData);
                    }

                    return recordSets;
                }
            }
            catch
            {
                return null;
            }
        }

        public string SelectStudentTotalNumber()
        {
            OdbcDataAdapter adapterStudentInClass;
            DataSet dsStudentInClassData = new DataSet("StudentInClass");
            string studentCount = "0";

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = @"Select Count(*) From Student
                                    Where ID In (Select StudentID From StudentInClass Where IsDeleted = '0' And EndDate >= '{0}')";

                    string today = DateTime.Now.ToString("yyyy/MM/dd");
                    sSQL = string.Format(sSQL, today);

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    studentCount = adapterStudentInClass.SelectCommand.ExecuteScalar().ToString();
                }
            }
            catch
            {
            }

            return studentCount;
        }

        #endregion



        /**********************************************************************************************
         *                                       Insert Related                                       *
         * ********************************************************************************************/

        #region Insert Related

        public void InsertCompanyInfo(CompanyInfoDefinition companyInfo)
        {
            OdbcDataAdapter adapterCompanyInfo;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterCompanyInfo = new OdbcDataAdapter();

                    sSQL = "Call qryInsertCompanyInfo('" + companyInfo.CompanyName + "', '" + companyInfo.CompanyManager + "')";

                    adapterCompanyInfo.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterCompanyInfo.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterCompanyInfo.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public int InsertStudentWithID(StudentDefinition studentData)
        {
            OdbcDataAdapter adapterStudent;
            int studentID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudent = new OdbcDataAdapter();

                    string sibling = studentData.OldBrother + "," + studentData.OldSister + "," + studentData.YoungBrother + "," + studentData.YoungSister;
                    sSQL = "Select qryInsertStudentWithID(" + int.Parse(studentData.ID) + ", '" + studentData.Name + "', '" + studentData.Sex + "', '" + studentData.DateOfBirth + "', '" +
                                                              studentData.SocialNumber + "', '" + studentData.StartDate + "', '" + studentData.School + "','" + studentData.StudyYear + "', '" + studentData.FatherName + "', '" +
                                                              studentData.FatherWork + "','" + studentData.MotherName + "', '" + studentData.MotherWork + "', '" + sibling + "', '" +
                                                              studentData.InChargePerson + "','" + studentData.InChargePersonHomePhone + "','" + studentData.InChargePersonCompanyPhone + "', '" +
                                                              studentData.InChargePersonMobile + "', '" + studentData.EmergencyPerson + "', '" + studentData.EmergencyPhone + "','" +
                                                              studentData.Address + "', '" + studentData.PostCode + "', " + studentData.PrePaid + ")";

                    adapterStudent.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudent.SelectCommand.CommandType = CommandType.StoredProcedure;
                    studentID = (int)adapterStudent.SelectCommand.ExecuteScalar();
                }
            }
            catch
            {
            }

            return studentID;
        }

        public int InsertStudentWithoutID(StudentDefinition studentData)
        {
            OdbcDataAdapter adapterStudent;
            int studentID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudent = new OdbcDataAdapter();

                    string sibling = studentData.OldBrother + "," + studentData.OldSister + "," + studentData.YoungBrother + "," + studentData.YoungSister;
                    sSQL = "Select qryInsertStudent('" + studentData.Name + "', '" + studentData.Sex + "', '" + studentData.DateOfBirth + "', '" +
                                                    studentData.SocialNumber + "', '" + studentData.StartDate + "', '" + studentData.School + "','" + studentData.StudyYear + "', '" + studentData.FatherName + "', '" +
                                                    studentData.FatherWork + "','" + studentData.MotherName + "', '" + studentData.MotherWork + "', '" + sibling + "', '" +
                                                    studentData.InChargePerson + "','" + studentData.InChargePersonHomePhone + "','" + studentData.InChargePersonCompanyPhone + "', '" +
                                                    studentData.InChargePersonMobile + "', '" + studentData.EmergencyPerson + "', '" + studentData.EmergencyPhone + "','" +
                                                    studentData.Address + "', '" + studentData.PostCode + "', " + studentData.PrePaid + ")";

                    adapterStudent.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudent.SelectCommand.CommandType = CommandType.StoredProcedure;
                    studentID = (int)adapterStudent.SelectCommand.ExecuteScalar();
                }
            }
            catch
            {
            }

            return studentID;
        }

        public void InsertClassWithID(ClassDefinition classData)
        {
            OdbcDataAdapter adapterClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryInsertClass('" + classData.ID + "', '" + classData.ClassCategoryName + "', '" + classData.Name + "','" +
                                                     classData.StartDate + "', '" + classData.EndDate + "', " + classData.ClassPeriod + ", '" +
                                                     classData.ClassDay + "', " + classData.Seat + ", " + classData.Price + ", '" +
                                                     classData.ClassStatus + "', '" + classData.Teacher + "', " + classData.MaterialFee + ", " +
                                                     classData.ApplyFee + ", '" + classData.Note + "')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void InsertClassTimeWithoutID(List<ClassTimeDefinition> classTimeDataSets)
        {
            OdbcDataAdapter adapterClassTime;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    foreach (var classTimeData in classTimeDataSets)
                    {
                        adapterClassTime = new OdbcDataAdapter();

                        sSQL = "Call qryInsertClassTime('" + classTimeData.ClassID + "', '" + classTimeData.Time + "')";

                        adapterClassTime.SelectCommand = new OdbcCommand(sSQL, conn);
                        adapterClassTime.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapterClassTime.SelectCommand.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }

        public void InsertClassCategoryWithoutID(string classCategoryName)
        {
            OdbcDataAdapter adapterClassCategory;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassCategory = new OdbcDataAdapter();

                    sSQL = "Call qryInsertClassCategory('" + classCategoryName + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassCategory.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public int InsertStaffWithoutID(StaffDefinition staffData)
        {
            OdbcDataAdapter adapterStaff;
            int staffID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Select qryInsertStaff(" + staffData.StaffRole + ", '" + staffData.StaffTypeName + "', '" + staffData.Name + "', '" + staffData.EnglishName + "', '" +
                                                       staffData.StartDate + "','" + staffData.EndDate + "', " + staffData.LaborCover + ", " +
                                                       staffData.HealthCover + ", " + staffData.GroupCover + ", '" + staffData.Note + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    staffID = (int)adapterStaff.SelectCommand.ExecuteScalar();
                }
            }
            catch
            {
            }

            return staffID;
        }

        public void InsertStaffAccountWithoutID(StaffAccountDefinition staffAccountData)
        {
            OdbcDataAdapter adapterStaffAccount;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaffAccount = new OdbcDataAdapter();

                    sSQL = "Call qryInsertStaffAccount('" + staffAccountData.UserName + "', '" + staffAccountData.Password + "', '" + staffAccountData.MasterKey + "')";

                    adapterStaffAccount.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaffAccount.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaffAccount.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void InsertStudentInClass(StudentInClassDefinition studentInClassData)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryInsertStudentInClass(" + GetNewID() + ", " +
                                                             int.Parse(studentInClassData.StudentID) + ", '" +
                                                             studentInClassData.ClassID + "', '" +
                                                             studentInClassData.AddDate + "', '" +
                                                             studentInClassData.EndDate + "', " +
                                                             studentInClassData.ClassPeriod + ", " +
                                                             studentInClassData.ClassPrice + ", " +
                                                             studentInClassData.ApplyFee + ", " +
                                                             studentInClassData.MaterialFee + ", " +
                                                             studentInClassData.Discount + ")";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStudentInClassNotIsDeletedByIDs(" + int.Parse(studentInClassData.StudentID) + ", '" + studentInClassData.ClassID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
        }

        public void InsertNewClassAndCheckIsRepeat(StudentInClassDefinition studentInClassData)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryInsertNewClassAndCheckIsRepeated(" + int.Parse(studentInClassData.StudentID) + ", '" +
                                                                                                     studentInClassData.ClassID + "', '" +
                                                                                                     studentInClassData.AddDate + "', '" +
                                                                                                     studentInClassData.EndDate + "', " +
                                                                                                     studentInClassData.ClassPeriod + ", " +
                                                                                                     studentInClassData.ClassPrice + ", " +
                                                                                                     studentInClassData.ApplyFee + ", " +
                                                                                                     studentInClassData.MaterialFee + ", " +
                                                                                                     studentInClassData.Discount + ")";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStudentInClassNotIsDeletedByIDs(" + int.Parse(studentInClassData.StudentID) + ", '" + studentInClassData.ClassID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
        }

        public void InsertStudentPrepaidHistory(StudentPrepaidDefinition studentPrepaidData)
        {
            OdbcDataAdapter adapterStudentPrepaidHistory;
            func = new NormalFunctions();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentPrepaidHistory = new OdbcDataAdapter();

                    sSQL = "Call qryInsertPrepaidHistory(" + int.Parse(studentPrepaidData.StudentID) + ", '" + func.ChangeDateFormatForMySql(DateTime.Now) + "', " + studentPrepaidData.InMoney +
                                                             ", " + studentPrepaidData.OutMoney + ", '" + StaticFunction.SetEncodingString(studentPrepaidData.Events) + "')";

                    adapterStudentPrepaidHistory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentPrepaidHistory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentPrepaidHistory.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void InsertStudentClassPayment(ClassPaymentDefinition classPaymentData)
        {
            OdbcDataAdapter adapterStudentClassPayment;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentClassPayment = new OdbcDataAdapter();

                    sSQL = "Call qryInsertClassPayment(" + classPaymentData.StudentID + ", '" + classPaymentData.ClassID + "', " + classPaymentData.StudentInClassID +
                                                           ", '" + classPaymentData.StaffName + "', " + classPaymentData.Paid + ", '" + classPaymentData.PayDate + "', '" +
                                                           classPaymentData.PaymentType + "')";

                    adapterStudentClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentClassPayment.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        //Transaction Refund Action
        public void InsertStudentRefundWithTransaction(string[] refundInfo, List<object> refundList)
        {
            FacadeLayer facade = new FacadeLayer(_SystemType);
            SystemLogsDefinition systemLogData = null;
            int refundID = 0;
            int studentID = int.Parse(refundList.ElementAt(0).ToString());
            string studentName = refundList.ElementAt(1).ToString();
            string staffEnglishName = refundList.ElementAt(2).ToString();
            string systemLogEvent = null;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    conn.BeginTransaction();

                    try
                    {

                        for (int i = 2; i < refundInfo.Count(); i++)
                        {
                            if (refundInfo[i] == "ClassPayment")
                            {
                                string classID, className, payment;
                                string[] moneyInfo = (string[])refundList.ElementAt(i);
                                facade.FacadeFunctions("reusefunction", "payment", (object)moneyInfo, null);

                                classID = moneyInfo[1];
                                className = moneyInfo[9];
                                payment = moneyInfo[4];
                                systemLogEvent = studentName + "(" + studentID.ToString("00000000") + ")" + " 繳交 " + className + "(" + classID + ")" + " 費用, 共 " + payment.ToString() + " 元";
                                systemLogData = new SystemLogsDefinition(0, 0, staffEnglishName, "", systemLogEvent);
                                facade.FacadeFunctions("insert", "systemlog", (object)systemLogData, null);

                                systemLogEvent = "學生 " + studentName + "(" + studentID.ToString("00000000") + ")" + " 退費, 退費方式為 新課費用, 收取人為 " + className + "(" + classID + ")";
                            }
                            else
                            {
                                if (refundInfo[i].IndexOf("RefundList") > -1)
                                {
                                    ClassRefundDefinition classRefund = (ClassRefundDefinition)refundList.ElementAt(i);
                                    if (refundInfo[i].IndexOf("Main") > -1)
                                        refundID = (int)facade.FacadeFunctions("insert", "studentclassrefund", (object)classRefund, null);
                                    else if (refundInfo[i].IndexOf("Sub") > -1)
                                    {
                                        string refundType = null, receiver = "";

                                        classRefund.ID = refundID;
                                        facade.FacadeFunctions("insert", "studentclassrefund", (object)classRefund, null);

                                        refundType = refundInfo[i].Substring(refundInfo[i].IndexOf(',') + 1);


                                        if (refundType.IndexOf("帳戶") > -1)
                                        {
                                            int prePaid = int.Parse(facade.FacadeFunctions("select", "studentprepaid", (object)studentID, null).ToString());
                                            facade.FacadeFunctions("update", "prepaid", (object)studentID, (object)(prePaid + classRefund.Refunded).ToString());

                                            StudentPrepaidDefinition studentPrepaidData = new StudentPrepaidDefinition(studentID.ToString("00000000"),
                                                                                              facade.FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString(),
                                                                                              classRefund.Refunded, 0, "個人退費");

                                            facade.FacadeFunctions("insert", "studentprepaid", (object)studentPrepaidData, null);
                                        }
                                        else
                                        {
                                            receiver = ", 收取人為 " + refundType.Substring(refundInfo[i].IndexOf(',') + 1);
                                            refundType = refundType.Substring(0, refundInfo[i].IndexOf(','));
                                        }

                                        systemLogEvent = "學生 " + studentName + "(" + studentID.ToString("00000000") + ")" + " 退費, 退費方式為 " + refundType + receiver;
                                    }
                                }
                                else
                                {
                                    ClassRefundDetailDefinition classRefundDetail = (ClassRefundDetailDefinition)refundList.ElementAt(i);

                                    facade.FacadeFunctions("insert", "studentclassrefunddetail", (object)classRefundDetail, null);
                                    facade.FacadeFunctions("updatedeleted", "studentinclassrefunded", (object)studentID, (object)classRefundDetail.ClassID);
                                }
                            }

                            systemLogData = new SystemLogsDefinition(0, 0, staffEnglishName, "", systemLogEvent);
                            facade.FacadeFunctions("insert", "systemlog", (object)systemLogData, null);
                        }

                        conn.BeginTransaction().Commit();
                    }
                    catch
                    {
                        conn.BeginTransaction().Rollback();
                    }
                }
            }
            catch
            {
            }
        }

        public int InsertStudentClassRefund(ClassRefundDefinition classRefundData)
        {
            OdbcDataAdapter adapterStudentClassRefund;
            int refundID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentClassRefund = new OdbcDataAdapter();

                    if (classRefundData.ID > 0)
                    {
                        refundID = classRefundData.ID;
                        sSQL = "Call qryInsertClassRefunded(" + classRefundData.ID + ", " + classRefundData.SubID + ", " + classRefundData.StudentID + ", '" +
                                                                classRefundData.StaffName + "', " + classRefundData.Discount + ", " + classRefundData.Refunded + ", '" +
                                                                classRefundData.RefundDate + "', '" + classRefundData.Receiver + "', '" + classRefundData.RefundType + "')";
                    }
                    else
                    {
                        sSQL = "Select qryInsertClassRefundedWithoutID(" + classRefundData.SubID + ", " + classRefundData.StudentID + ", '" + classRefundData.StaffName + "', " +
                                                                           classRefundData.Discount + ", " + classRefundData.Refunded + ", '" +
                                                                           classRefundData.RefundDate + "', '" + classRefundData.Receiver + "', '" +
                                                                           classRefundData.RefundType + "')";
                    }

                    adapterStudentClassRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentClassRefund.SelectCommand.CommandType = CommandType.StoredProcedure;

                    if (classRefundData.ID > 0)
                        adapterStudentClassRefund.SelectCommand.ExecuteNonQuery();
                    else
                        refundID = (int)adapterStudentClassRefund.SelectCommand.ExecuteScalar();
                }
            }
            catch
            {
                //if (classRefundData.StudentID == "")
                //{
                //    StudentDefinition tempStudentData = new StudentDefinition("0", "WholeClass", "", "", "", "", "", "", "", "", "", "", "", "", "", "", 0, '0');
                //    InsertStudentWithID(tempStudentData);

                //    using (conn = new OdbcConnection(connectString))
                //    {
                //        conn.Open();

                //        adapterStudentClassRefund = new OdbcDataAdapter();
                //        sSQL = "Select qryInsertClassRefundedWithoutID(" + classRefundData.SubID + ", " + classRefundData.StudentID + ", '" + classRefundData.StaffName + "', " +
                //                                                           classRefundData.Discount + ", " + classRefundData.Refunded + ", '" +
                //                                                           classRefundData.RefundDate + "', '" + classRefundData.Receiver + "', '" +
                //                                                           classRefundData.RefundType + "')";

                //        adapterStudentClassRefund.SelectCommand = new OdbcCommand(sSQL, conn);
                //        adapterStudentClassRefund.SelectCommand.CommandType = CommandType.StoredProcedure;
                //        refundID = (int)adapterStudentClassRefund.SelectCommand.ExecuteScalar();
                //    }
                //}
            }

            return refundID;
        }

        public void InsertStudentClassRefundDetail(ClassRefundDetailDefinition classRefundDetailData)
        {
            OdbcDataAdapter adapterStudentClassRefundDetail;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();

                    adapterStudentClassRefundDetail = new OdbcDataAdapter();

                    sSQL = "Call qryInsertClassRefundDetail(" + classRefundDetailData.RefundID + ", '" + classRefundDetailData.ClassID + "', " +
                                                                classRefundDetailData.HavePaid + ")";

                    adapterStudentClassRefundDetail.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentClassRefundDetail.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentClassRefundDetail.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void InsertDailyExpanse(ExpanseDefinition dailyExpanseData)
        {
            OdbcDataAdapter adapterDailyExpanse;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterDailyExpanse = new OdbcDataAdapter();

                    sSQL = "Call qryInsertExpanseWithInsertDate('" + dailyExpanseData.ExpanseCategoryName + "', '" + dailyExpanseData.InsertStaffName + "', '" + dailyExpanseData.ItemName + "', '" +
                                                                     dailyExpanseData.ShopName + "', " + dailyExpanseData.UnitPrice + ", " + dailyExpanseData.Quantity + ", '" + dailyExpanseData.InsertDate + "')";

                    adapterDailyExpanse.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterDailyExpanse.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterDailyExpanse.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void InsertSystemLog(SystemLogsDefinition systemLogs)
        {
            OdbcDataAdapter adapterSystemLogs;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterSystemLogs = new OdbcDataAdapter();

                    sSQL = "Call qryInsertSystemlogs('" + systemLogs.StaffName + "', '" + systemLogs.Date + "', '" + StaticFunction.SetEncodingString(systemLogs.Action) + "')";

                    adapterSystemLogs.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterSystemLogs.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterSystemLogs.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        #endregion



        /**********************************************************************************************
         *                                       Update Related                                       *
         * ********************************************************************************************/

        #region Format Data

        public void FormatData()
        {
            OdbcDataAdapter adapterClass;
            DataSet dsClassData = new DataSet("Class");
            ClassDefinition classData = new ClassDefinition();

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Select ID, Name From Student";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("Table", "ShowStudent");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    string itemID = "";
                    string itemName = "";
                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        itemID = row["ID"].ToString();
                        itemName = StaticFunction.GetEncodingString(row["Name"].ToString());

                        sSQL = "Update Student Set Name = '" + StaticFunction.SetEncodingString(itemName) + "' Where ID = '" + itemID + "'";

                        adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                        adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapterClass.SelectCommand.ExecuteNonQuery();
                    }

                    sSQL = "Select ID, Name From Class";

                    dsClassData = new DataSet("Class");
                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.TableMappings.Add("ClassTable", "ShowClass");
                    adapterClass.Fill(dsClassData);

                    func = new NormalFunctions();

                    itemID = "";
                    itemName = "";
                    foreach (DataRow row in dsClassData.Tables[0].Rows)
                    {
                        itemID = row["ID"].ToString();
                        itemName = StaticFunction.GetEncodingString(row["Name"].ToString());

                        sSQL = "Update Class Set Name = '" + StaticFunction.SetEncodingString(itemName) + "' Where ID = '" + itemID + "'";

                        adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                        adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapterClass.SelectCommand.ExecuteNonQuery();
                    }
                }
            }
            catch
            {

            }
        }

        #endregion

        #region Update Related

        public void UpdateCompanyInfo(CompanyInfoDefinition companyInfo)
        {
            OdbcDataAdapter adapterCompanyInfo;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterCompanyInfo = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateCompanyInfo('" + companyInfo.CompanyName + "', '" + companyInfo.CompanyManager + "')";

                    adapterCompanyInfo.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterCompanyInfo.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterCompanyInfo.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudent(StudentDefinition studentData)
        {
            OdbcDataAdapter adapterStudent;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudent = new OdbcDataAdapter();

                    string sibling = studentData.OldBrother + "," + studentData.OldSister + "," + studentData.YoungBrother + "," + studentData.YoungSister;
                    sSQL = "Call qryUpdateStudentByID(" + int.Parse(studentData.ID) + ", '" + studentData.Name + "', '" + studentData.Sex + "', '" + studentData.DateOfBirth + "', '" +
                                                          studentData.SocialNumber + "', '" + studentData.StartDate + "', '" + studentData.School + "','" + studentData.StudyYear + "', '" +
                                                          studentData.FatherName + "', '" + studentData.FatherWork + "','" + studentData.MotherName + "', '" +
                                                          studentData.MotherWork + "', '" + sibling + "', '" + studentData.InChargePerson + "','" + studentData.InChargePersonHomePhone + "', '" +
                                                          studentData.InChargePersonCompanyPhone + "', '" + studentData.InChargePersonMobile + "', '" + studentData.EmergencyPerson + "', '" + studentData.EmergencyPhone + "','" +
                                                          studentData.Address + "', '" + studentData.PostCode + "')";

                    adapterStudent.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudent.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudent.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudentStatus(string studentID, string status)
        {
            OdbcDataAdapter adapterStudent;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudent = new OdbcDataAdapter();

                    sSQL = "Update student Set IsDeleted = '" + status + "' Where ID='" + studentID + "'";

                    adapterStudent.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudent.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudent.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudentPrePaid(int studentID, int prePaid)
        {
            OdbcDataAdapter adapterStudent;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudent = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStudentPrePaidByID(" + studentID + ", " + prePaid + ")";

                    adapterStudent.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudent.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudent.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateClass(ClassDefinition classData, string oldClassID)
        {
            OdbcDataAdapter adapterClassCategory;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassCategory = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateClassByID('" + oldClassID + "', '" + classData.ID + "', '" + classData.ClassCategoryName + "', '" + classData.Name + "','" +
                                                                         classData.StartDate + "', '" + classData.EndDate + "', " + classData.ClassPeriod + ", '" +
                                                                         classData.ClassDay + "', " + classData.Seat + ", " + classData.Price + ", '" +
                                                                         classData.Teacher + "', " + classData.MaterialFee + ", " + classData.ApplyFee + ", '" + classData.Note + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassCategory.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateClassCategory(string oldClassCategoryName, string newClassCategoryName)
        {
            OdbcDataAdapter adapterClassCategory;
            int classCateID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassCategory = new OdbcDataAdapter();

                    sSQL = "select qryListClassCategoryIDByName('" + oldClassCategoryName + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    classCateID = int.Parse(adapterClassCategory.SelectCommand.ExecuteScalar().ToString());

                    sSQL = "Call qryUpdateClassCategoryByID(" + classCateID + ", '" + newClassCategoryName + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassCategory.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudentClassPaymentDiscount(StudentInClassDefinition studentInClassData)
        {
            OdbcDataAdapter adapterPaymentDiscount;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterPaymentDiscount = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStudentInClassDiscountByIDs(" + int.Parse(studentInClassData.StudentID) + ", '" + studentInClassData.ClassID + "', " +
                                                                          studentInClassData.Discount + ")";

                    adapterPaymentDiscount.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterPaymentDiscount.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterPaymentDiscount.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStaff(StaffDefinition staffData)
        {
            OdbcDataAdapter adapterStaff;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStaffByID(" + staffData.ID + ", " + staffData.StaffRole + ", '" + staffData.StaffTypeName + "', '" + staffData.Name + "', '" + staffData.EnglishName + "', '" +
                                                        staffData.StartDate + "','" + staffData.EndDate + "', " + staffData.LaborCover + ", " +
                                                        staffData.HealthCover + ", " + staffData.GroupCover + ", '" + staffData.Note + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStaffAccount(StaffAccountDefinition staffAccountData)
        {
            OdbcDataAdapter adapterStaff;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStaffAccountByID(" + staffAccountData.ID + ", '" + staffAccountData.Password + "', '" + staffAccountData.MasterKey + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        private void UpdateStaffEndDateByID(int staffID)
        {
            OdbcDataAdapter adapterStaff;

            try
            {
                func = new NormalFunctions();
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStaffEndDateByID(" + staffID + ", '" + func.ChangeDateFormatForMySql(DateTime.Now) + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateDailyExpanse(ExpanseDefinition dailyExpanseData)
        {
            OdbcDataAdapter adapterDailyExpanse;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterDailyExpanse = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateExpanseWithInsertDate(" + dailyExpanseData.ID + ", '" + dailyExpanseData.ExpanseCategoryName + "', '" + dailyExpanseData.UpdateStaffName + "', '" +
                                                                              dailyExpanseData.ItemName + "', '" + dailyExpanseData.ShopName + "', " + dailyExpanseData.UnitPrice + ", " +
                                                                              dailyExpanseData.Quantity + ", '" + dailyExpanseData.InsertDate + "')";

                    adapterDailyExpanse.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterDailyExpanse.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterDailyExpanse.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        #endregion




        /**********************************************************************************************
         *                                    Update Deleted Related                                  *
         * ********************************************************************************************/

        #region Update Deleted Related

        public void UpdateClassToDelete(string classID)
        {
            OdbcDataAdapter adapterClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClass = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateClassIsDeletedByID('" + classID + "')";

                    adapterClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClass.SelectCommand.ExecuteNonQuery();

                    DeleteClassTime(classID);
                }
            }
            catch
            {
            }
        }

        public void UpdateStaffToDelete(int staffID)
        {
            OdbcDataAdapter adapterStaff;

            try
            {
                UpdateStaffEndDateByID(staffID);

                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStaff = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStaffIsDeletedByID('" + staffID + "')";

                    adapterStaff.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStaff.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStaff.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudentInClassToDelete(int studentID, string classID)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    //sSQL = "Select * From ClassPayment Where StudentId = " + studentID + " And ClassId = '" + classID + "'";
                    sSQL = "Select IsRefund From studentinclass Where StudentId = " + studentID + " And ClassId = '" + classID + "'";
                    DataSet dsData = new DataSet("NoTables");
                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.TableMappings.Add("Table", "ShowStudent");
                    adapterStudentInClass.Fill(dsData);

                    int count = 0;
                    foreach (DataRow row in dsData.Tables[0].Rows)
                    {
                        if (char.Parse(row["IsRefund"].ToString()) == '0')
                            count++;
                    }

                    if (count > 0)
                        sSQL = "Call qryUpdateStudentInClassIsDeletedByID(" + studentID + ", '" + classID + "')";
                    else
                        sSQL = "Update StudentInClass Set IsDeleted = '1', IsRefund = '1' Where StudentId = " + studentID + " And ClassId = '" + classID + "'";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudentInClassToRefund(int studentID, string classID)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStudentInClassIsRefundByID(" + studentID + ", '" + classID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void UpdateStudentInClassToDelete(string classID)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryUpdateStudentInClassIsDeletedByClassID('" + classID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        #endregion



        /**********************************************************************************************
         *                                       Delete Related                                       *
         * ********************************************************************************************/

        #region Delete Related

        public void DeleteClassCategory(string classCategoryName)
        {
            OdbcDataAdapter adapterClassCategory;
            int classCateID = 0;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassCategory = new OdbcDataAdapter();

                    sSQL = "Call qryListClassCategoryIDByName('" + classCategoryName + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    classCateID = int.Parse(adapterClassCategory.SelectCommand.ExecuteScalar().ToString());

                    sSQL = "Call qryDeleteClassCategoryByID('" + classCateID + "')";

                    adapterClassCategory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassCategory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassCategory.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void DeleteClassTime(string classID)
        {
            OdbcDataAdapter adapterClassTime;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassTime = new OdbcDataAdapter();

                    sSQL = "Call qryDeleteClassTimeByClassID('" + classID + "')";

                    adapterClassTime.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassTime.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassTime.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void DeleteClassPaymentByIDs(int studentID, string classID)
        {
            OdbcDataAdapter adapterClassPayment;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterClassPayment = new OdbcDataAdapter();

                    sSQL = "Call qryDeleteClassPaymentByID(" + studentID + ", '" + classID + "')";

                    adapterClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterClassPayment.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void DeleteClassPaymentByRecordDefi(RecordDefinition recordData)
        {
            OdbcDataAdapter adapterClassPayment;
            List<ClassPaymentDefinition> classPaymentSets = new List<ClassPaymentDefinition>();
            List<ClassPaymentDefinition> tempClassPaymentSets = new List<ClassPaymentDefinition>();

            try
            {
                tempClassPaymentSets = SelectStudentPaymentRecordByStudentIDAndClassID(int.Parse(recordData.Data1ID), recordData.Data2ID);

                foreach (var classPaymentSingle in tempClassPaymentSets)
                {
                    if (classPaymentSingle.Paid == recordData.Money1)
                    {
                        if (classPaymentSingle.PayDate == recordData.Note1)
                        {
                            if (classPaymentSingle.PaymentType == recordData.Note2)
                                classPaymentSets.Add(classPaymentSingle);
                        }

                    }
                }

                if (classPaymentSets.Count > 0)
                {
                    SetConnectionString();
                    using (conn = new OdbcConnection(connectString))
                    {
                        conn.Open();
                        adapterClassPayment = new OdbcDataAdapter();

                        sSQL = "Call qryDeleteClassPaymentByID(" + classPaymentSets.ElementAt(0).ID + ")";

                        adapterClassPayment.SelectCommand = new OdbcCommand(sSQL, conn);
                        adapterClassPayment.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapterClassPayment.SelectCommand.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
            }
        }

        public void DeleteStudentPrepaidHistoryByID(int iD)
        {
            OdbcDataAdapter adapterPrepaidHistory;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterPrepaidHistory = new OdbcDataAdapter();

                    sSQL = "Call qryDeletePrepaidHistoryByID(" + iD + ")";

                    adapterPrepaidHistory.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterPrepaidHistory.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterPrepaidHistory.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void DeleteStudentInClass(StudentInClassDefinition studentInClassData)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryDeleteStudentInClassByIDs(" + int.Parse(studentInClassData.StudentID) + ", '" + studentInClassData.ClassID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void DeleteStudentInClassByClassID(string classID)
        {
            OdbcDataAdapter adapterStudentInClass;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterStudentInClass = new OdbcDataAdapter();

                    sSQL = "Call qryDeleteStudentInClassByClassID('" + classID + "')";

                    adapterStudentInClass.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterStudentInClass.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterStudentInClass.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        public void DeleteDailyExpanse(string dailyExpanseID)
        {
            OdbcDataAdapter adapterDailyExpanse;

            try
            {
                SetConnectionString();
                using (conn = new OdbcConnection(connectString))
                {
                    conn.Open();
                    adapterDailyExpanse = new OdbcDataAdapter();

                    sSQL = "Call qryDeleteExpanseByID(" + dailyExpanseID + ")";

                    adapterDailyExpanse.SelectCommand = new OdbcCommand(sSQL, conn);
                    adapterDailyExpanse.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapterDailyExpanse.SelectCommand.ExecuteNonQuery();
                }
            }
            catch
            {
            }
        }

        #endregion

    }
}
