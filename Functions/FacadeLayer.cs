using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using EMSSystem.ClassLibrary;
using EMSSystem.StaticFunctions;

namespace EMSSystem.Functions
{
    public class FacadeLayer
    {
        private string _SystemType;
        private SaltedHashManager salted;

        public FacadeLayer(string systemType)
        {
            _SystemType = systemType;
            salted = new SaltedHashManager();
        }

        private DataAccess dbAccess;
        private FileOperator fOper;
        private NormalFunctions func;

        public object FacadeFunctions(string functions, string objName, object objObjectOne, object objObjectTwo)
        {
            func = new NormalFunctions();

            if (functions.ToLower().IndexOf("count") > -1)
            {
                if (objName.ToLower().IndexOf("class") > -1 && objName.ToLower().IndexOf("classcategory") > -1)
                    return CountClassByClassCategoryName(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentnumber") > -1)
                    return CountStudentNumberByClassID(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                    return CountStudentInClass(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("weekdaynumber") > -1)
                    return (object)func.GetWeekDayNumber(objObjectOne.ToString());
            }
            else if (functions.ToLower().IndexOf("create") > -1)
            {
                if (objName.ToLower().IndexOf("file") > -1)
                {
                    fOper = new FileOperator();
                    fOper.CreateFiles(objObjectOne.ToString());
                    return null;
                }
            }
            else if (functions.ToLower().IndexOf("move") > -1)
            {
                if (objName.ToLower().IndexOf("file") > -1)
                {
                    fOper = new FileOperator();
                    fOper.CreateFiles(objObjectOne.ToString());
                    return null;
                }
            }
            else if (functions.ToLower().IndexOf("select") > -1)
            {
                if (objName.ToLower().IndexOf("getcurrentuser") > -1)
                    return GetCurrentUser();
                else if (objName.ToLower().IndexOf("whole") > -1)
                    return SelectWholeInfo(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentinclasstotalnumber") > -1)
                    return SelectStudentInClassTotalNumber();
                else if (objName.ToLower().IndexOf("studenttotalnumber") > -1)
                    return SelectStudentTotalNumber();
                else if (objName.ToLower().IndexOf("studentinclassid") > -1)
                    return SelectStudentInClassID(int.Parse(objObjectOne.ToString()), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                    return SelectStudentInClassInfo(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentprepaidhistorylistbydate") > -1)
                    return SelectStudentPrepaidHistoryByDate(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentprepaidhistorylistgroupdatebyid") > -1)
                    return SelectStudentPrepaidHistoryGroupDateByStudentID(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentprepaidhistorylistbyid") > -1)
                    return SelectStudentPrepaidHistoryByStudentID(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentprepaidhistorytotalforstudentpayment") > -1)
                    return SelectStudentPrepaidTotalByStudentIDForStudentPayment(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentprepaidhistorytotal") > -1)
                    return SelectStudentPrepaidTotalByStudentID(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentprepaid") > -1)
                    return SelectStudentPrePaid(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentpaymenttotalforrecord") > -1)
                    return SelectStudentPaymentCountForRecord(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentbyclassid") > -1)
                    return SelectStudentPaymentCountByClassID(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentforrecord") > -1)
                    return SelectStudentPaymentForRecord(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentlist") > -1)
                    return SelectStudentPaymentList(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentrecordlist") > -1)
                    return SelectStudentPaymentRecordListByIDs(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentrecordtotalbydate") > -1)
                    return SelectStudentPaymentRecordTotalByDate(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentrecordtotalbyids") > -1)
                    return SelectStudentPaymentRecordTotalByIDs(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentrecordtotal") > -1)
                    return SelectStudentPaymentRecordTotal(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentrecordtopsix") > -1)
                    return SelectClassPaymentRecordTopSix((int)objObjectOne);
                else if (objName.ToLower().IndexOf("studenthavetorefundinstudentlist") > -1)
                    return SelectStudentHaveToRefundListInStudentList(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studenthavetorefundbyclassidorname") > -1)
                    return SelectStudentHaveToRefundByClassIDOrName(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studenthavetorefundlist") > -1)
                    return SelectStudentHaveToRefundList(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentneedtopaymoney") > -1)
                    return SelectStudentNeedToPayMoney(int.Parse(objObjectOne.ToString()), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentneedtopaylist") > -1)
                    return SelectNeedToPayList(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentneedtopayclass") > -1)
                    return SelectStudentNeedToPayClass(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentselectclassesinrecordistformat") > -1)
                    return SelectStudentSelectClassListInRecordListFormat(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentselectclassesbyenddate") > -1)
                    return SelectStudentSelectClassListByEndDate(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentselectclasses") > -1)
                    return SelectStudentSelectClassList(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentclasshavepaid") > -1)
                    return SelectStudentClassHavePaidPayment(int.Parse(objObjectOne.ToString()), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentbyclass") > -1)
                    return SelectStudentInfoByClass(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentrefunddetail") > -1)
                    return SelectStudentRefundDetail(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentrefundstudentlistbyclassid") > -1)
                    return SelectStudentRefundStudentListForRecord(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentrefundlist") > -1)
                    return SelectStudentRefundRecordList(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentrefundforrecord") > -1)
                    return SelectStudentRefundForRecord(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentrefund") > -1)
                    return SelectStudentRefundRecord(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("classbyclasscategory") > -1)
                    return SelectClassNameByClassCategory(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("classtime") > -1)
                    return SelectClassTimeInfo(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("classpaymentrecordbyclassid") > -1)
                    return SelectClassPaymentRecordByClassID(int.Parse(objObjectOne.ToString()), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("classpaymentrecord") > -1)
                    return SelectClassPaymentRecord(int.Parse(objObjectOne.ToString()), (string[])objObjectTwo);
                else if (objName.ToLower().IndexOf("classbyenddate") > -1)
                    return SelectClassInfoByEndDate(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("classbyisdeletedbysearch") > -1)
                    return SelectClassByIsDeleted(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("classbyisdeleted") > -1)
                    return SelectClassByIsDeleted();
                else if (objName.ToLower().IndexOf("sidesubfunctions") > -1)
                    return GetSideSubFunctions(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("sidefunctions") > -1)
                    return GetSideFunctions();
                else if (objName.ToLower().IndexOf("staffrolematchnamelist") > -1)
                    return SelectStaffRoleList(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("staffaccountbyenglishname") > -1)
                    return SelectStaffAccountInfoByEnglishName(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("staffbyenglishname") > -1)
                    return SelectStaffInfoByEnglishName(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("staffbyname") > -1)
                    return SelectStaffInfoByName(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("staffbyid") > -1)
                    return SelectStaffInfoByID(int.Parse(objObjectOne.ToString()));
                else if (objName.ToLower().IndexOf("studentbydeleted") > -1)
                    return SelectStudentInfoByDeleted(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentall") > -1)
                    return SelectStudentAll();
                else if (objName.ToLower().IndexOf("student") > -1)
                    return SelectStudentInfo(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("classall") > -1)
                    return SelectClassWholeInfo(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("class") > -1)
                    return SelectClassInfo(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("dailyexpansebydates") > -1)
                    return SelectDailyExpanseByDates(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("systemlogs") > -1)
                    return SelectSystemLogs((string[])objObjectOne);
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("insert") > -1)
            {
                if (objName.ToLower().IndexOf("companyinfo") > -1)
                {
                    InsertCompanyInfo((CompanyInfoDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentinclassbyclass") > -1)
                {
                    InsertStudentInClassInfoByClass((List<StudentDefinition>)objObjectOne, (ClassDefinition)objObjectTwo);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentprepaid") > -1)
                {
                    InsertStudentPrepaidInfo((StudentPrepaidDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                {
                    InsertStudentInClassInfo((StudentInClassDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentclasspayment") > -1)
                {
                    bool paymentByClass = false;
                    if (objObjectTwo != null)
                        paymentByClass = true;
                    InsertStudentClassPaymentInfo((ClassPaymentDefinition)objObjectOne, paymentByClass);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentclassrefunddetail") > -1)
                {
                    InsertStudentClassRefundDetailInfo((ClassRefundDetailDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentclassrefund") > -1)
                    return (object)InsertStudentClassRefundInfo((ClassRefundDefinition)objObjectOne);
                else if (objName.ToLower().IndexOf("staffaccount") > -1)
                {
                    InsertStaffAccountInfo((StaffAccountDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("staff") > -1)
                    return (object)InsertStaffInfo((StaffDefinition)objObjectOne);
                else if (objName.ToLower().IndexOf("studentwithid") > -1)
                    return (object)InsertStudentInfoWithID((StudentDefinition)objObjectOne);
                else if (objName.ToLower().IndexOf("student") > -1)
                    return (object)InsertStudentInfo((StudentDefinition)objObjectOne);
                else if (objName.ToLower().IndexOf("classcategory") > -1)
                {
                    InsertClassCategoryInfo(objObjectOne.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("classtime") > -1)
                {
                    InsertClassTimeInfo((List<ClassTimeDefinition>)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("class") > -1)
                {
                    InsertClassInfo((ClassDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("dailyexpanse") > -1)
                {
                    InsertDailyExpanse((ExpanseDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("systemlog") > -1)
                {
                    InsertSystemLogs((SystemLogsDefinition)objObjectOne);
                    return null;
                }
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("updatedeleted") > -1)
            {
                func = new NormalFunctions();

                if (objName.ToLower().IndexOf("studentinclassbyclassid") > -1)
                {
                    UpdateStudentInClassDeleted(objObjectOne.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentinclassrefunded") > -1)
                {
                    UpdateStudentInClassRefunded(int.Parse(objObjectOne.ToString()), objObjectTwo.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                {
                    UpdateStudentInClassDeleted((int)objObjectOne, objObjectTwo.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("staff") > -1)
                {
                    UpdateStaffDeleted(int.Parse(objObjectOne.ToString()));
                    return null;
                }
                else if (objName.ToLower().IndexOf("class") > -1)
                {
                    UpdateClassDeleted(objObjectOne.ToString());
                    return null;
                }
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("update") > -1)
            {
                func = new NormalFunctions();

                if (objName.ToLower().IndexOf("companyinfo") > -1)
                {
                    UpdateCompanyInfo((CompanyInfoDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("classcategory") > -1)
                {
                    UpdateClassCategoryInfo(objObjectOne.ToString(), objObjectTwo.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("staffaccount") > -1)
                {
                    UpdateStaffAccountInfo((StaffAccountDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("staff") > -1)
                {
                    UpdateStaffInfo((StaffDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentstatus") > -1)
                {
                    UpdateStudentStatus(objObjectOne.ToString(), objObjectTwo.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("student") > -1)
                {
                    UpdateStudentInfo((StudentDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("class") > -1)
                {
                    UpdateClassInfo((ClassDefinition)objObjectOne, objObjectTwo.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("paymentdiscount") > -1)
                {
                    UpdateStudentClassPaymentDiscount((StudentInClassDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("prepaid") > -1)
                {
                    UpdateStudentPrePaid(int.Parse(objObjectOne.ToString()), int.Parse(objObjectTwo.ToString()));
                    return null;
                }
                else if (objName.ToLower().IndexOf("dailyexpanse") > -1)
                {
                    UpdateDailyExpanse((ExpanseDefinition)objObjectOne);
                    return null;
                }
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("delete") > -1)
            {
                func = new NormalFunctions();

                if (objName.ToLower().IndexOf("classcategory") > -1)
                {
                    DeleteClassCategoryInfo(objObjectOne.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("classtime") > -1)
                {
                    DeleteClassTimeInfo(objObjectOne.ToString());
                    return null;
                }
                if (objName.ToLower().IndexOf("classpayment") > -1)
                {
                    DeleteClassPaymentInfo((RecordDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentinclassbyclass") > -1)
                {
                    DeleteStudentInClassInfoByClass(objObjectOne.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                {
                    DeleteStudentInClassInfo((StudentInClassDefinition)objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("dailyexpanse") > -1)
                {
                    DeleteDailyExpanse(objObjectOne.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("file") > -1)
                {
                    fOper = new FileOperator();
                    fOper.DeleteFile(objObjectOne.ToString());
                    return null;
                }
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("reusefunction") > -1)
            {
                func = new NormalFunctions();

                if (objName.ToLower().IndexOf("setcurrentuser") > -1)
                {
                    SetCurrentUser(objObjectOne.ToString());
                    return null;
                }
                else if (objName.ToLower().IndexOf("getusbdrive") > -1)
                    return (object)GetLegalUSBDrive(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("studentpaymentrecord") > -1)
                    return (object)SelectStudentPaymentRecordByID(objObjectOne.ToString(), (string[])objObjectTwo);
                else if (objName.ToLower().IndexOf("setstudentrefund") > -1)
                {
                    SetStudentRefundInfo((string[])objObjectOne);
                    return null;
                }
                else if (objName.ToLower().IndexOf("getstudentrefund") > -1)
                    return (object)GetStudentRefundInfo();
                else if (objName.ToLower().IndexOf("studentprepaidhistoryforstudentpayment") > -1)
                    return (object)SelectStudentPrepaidHistoryByIDsForStudentPayment(objObjectOne.ToString(), (string[])objObjectTwo);
                else if (objName.ToLower().IndexOf("payment") > -1)
                    return (object)StudentClassPayment((string[])objObjectOne);
                else if (objName.ToLower().IndexOf("dateselect") > -1)
                    return (object)SelectDataByDate(objObjectOne, (string[])objObjectTwo);
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                    return (object)SelectStudentInClassCountByID((string[])objObjectOne);
                else if (objName.ToLower().IndexOf("studentprepaidhistory") > -1)
                    return (object)SelectStudentPrepaidHistoryByIDs(objObjectOne.ToString(), (string[])objObjectTwo);
                else if (objName.ToLower().IndexOf("receiptforrefund") > -1)
                    return ((object)SettleDrawingReceiptPicFoRefund((string[])objObjectOne));
                else if (objName.ToLower().IndexOf("receiptforprepaidbitmap") > -1)
                    return (object)SettleDrawingReceiptPicForPrepaid((string[])objObjectOne);
                else if (objName.ToLower().IndexOf("receiptforpaymentbitmap") > -1)
                    return (object)SettleDrawingReceiptPicForPayment((string[])objObjectOne);
                else if (objName.ToLower().IndexOf("needtopaynoticebitmapbyclass") > -1)
                    return (object)DrawingNeedToPayA4(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("needtopaynoticebitmap") > -1)
                    return (object)SettleDrawingNeedToPayNotice((string[])objObjectOne);
                else if (objName.ToLower().IndexOf("havepaidbitmap") > -1)
                    return (object)DrawingHavePaidList((string[])objObjectOne);
                else if (objName.ToLower().IndexOf("classlistbitmap") > -1)
                    return (object)DrawingClassList();
                else if (objName.ToLower().IndexOf("expenselogbitmap") > -1)
                    return (object)DrawingExpenseLog(objObjectOne.ToString(), objObjectTwo.ToString());
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("check") > -1)
            {
                func = new NormalFunctions();

                if (objName.ToLower().IndexOf("checksystemislegal") > -1)
                    return (object)CheckSystemLegal(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("string") > -1)
                    return (object)func.CheckInputDataIsRightOfFormat(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("number") > -1)
                    return (object)func.IsNumberic(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("checkstudentinclassisrepeat") > -1)
                    return (object)CheckStudentInClassIsRepeat(objObjectOne.ToString(), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("studentinclass") > -1)
                    return (object)CheckClassIsSelectedByStudent(int.Parse(objObjectOne.ToString()), objObjectTwo.ToString());
                else if (objName.ToLower().IndexOf("staffenglishname") > -1)
                    return (object)CheckStaffEnglishNameAvailable(objObjectOne.ToString());
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("format") > -1)
            {
                func = new NormalFunctions();

                if (objName.ToLower().IndexOf("datebydatetime") > -1)
                    return (object)func.ChangeDateFormatForMySql((DateTime)objObjectOne);
                else if (objName.ToLower().IndexOf("datebystring") > -1)
                    return (object)func.ChangeDateFormatForMySql(objObjectOne.ToString());
                else if (objName.ToLower().IndexOf("dbdata") > -1)
                {
                    dbAccess = new DataAccess(_SystemType);
                    dbAccess.FormatData();
                }
                else
                    return null;
            }
            else if (functions.ToLower().IndexOf("systemsetting") > -1)
            {
                if (objName.ToLower().IndexOf("getsetting") > -1)
                    return (object)GetSystemSetting(objObjectOne.ToString(), objObjectTwo.ToString());
                else
                    return null;
            }

            return null;
        }





        #region System Setting

        private object GetSystemSetting(string whichSetting, string settingValue)
        {
            IQueryable<string> settingItem = GetSettingItems().AsQueryable();

            if (!string.IsNullOrEmpty(whichSetting))
            {
                if (settingItem.Contains(whichSetting))
                {
                    string[] values = GetSettingValue().ToString().Split(';');

                    int count = 0;
                    foreach (var item in settingItem)
                    {
                        if (item == whichSetting)
                            values[count] = settingValue;
                        count++;
                    }

                    SavingSetting(values);
                }

                return null;
            }
            else
                return GetSettingValue();
        }

        private List<string> GetSettingItems()
        {
            List<string> settingItem = new List<string>();
            settingItem.Add("SystemTypeForDB");
            settingItem.Add("SearchClassByEndDate");

            return settingItem;
        }

        private string GetSettingValue()
        {
            fOper = new FileOperator();
            string systemSetting = "";
            bool needNewSetting = false;

            if (fOper.CheckFileExist("SystemSetting.txt"))
            {
                systemSetting = fOper.ReadData("SystemSetting.txt");

                if (systemSetting.Trim() == "")
                    needNewSetting = true;
                else if (systemSetting.IndexOf(';') == -1)
                    needNewSetting = true;
                else if (systemSetting.IndexOf(';') == 0)
                    needNewSetting = true;
                else if (systemSetting.Split(';').Count() - 1 != GetSettingItems().Count)
                    needNewSetting = true;
                else
                {
                    string[] checkValue = systemSetting.Split(';');
                    for (int i = 0; i < checkValue.Count() - 1; i++)
                    {
                        if (string.IsNullOrEmpty(checkValue[i]))
                            needNewSetting = true;
                    }
                }
            }
            else
                needNewSetting = true;

            if (needNewSetting)
            {
                systemSetting = "false;";

                fOper.DeleteFile("SystemSetting.txt");
                fOper.CreateFiles("SystemSetting.txt");
                fOper.WriteData("SystemSetting.txt", systemSetting);
            }

            return systemSetting;
        }

        private void SavingSetting(string[] values)
        {
            string value = "";
            foreach (var item in values)
            {
                if (!string.IsNullOrEmpty(item))
                    value += item + ";";
            }

            fOper = new FileOperator();
            fOper.WriteData("SystemSetting.txt", value);
        }

        #endregion

        #region Check

        private object CheckClassIsSelectedByStudent(int studentID, string classID)
        {
            List<StudentInClassDefinition> studentInClassSets = (List<StudentInClassDefinition>)SelectStudentInClassInfo(studentID);

            if (studentInClassSets != null)
            {
                foreach (var studentInClassData in studentInClassSets)
                {
                    if (classID == studentInClassData.ClassID)
                        return true;
                }
            }

            return false;
        }

        private object CheckStaffEnglishNameAvailable(string engName)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.CheckStaffEnglishNameAvailable(engName);
        }

        private object CheckStudentInClassIsRepeat(string fromClassID, string addClassID)
        {
            List<StudentPaymentDefinition> studentData = (List<StudentPaymentDefinition>)FacadeFunctions("select", "studentselectclasses", "ClassID", fromClassID);
            List<StudentPaymentDefinition> checkStudentData = (List<StudentPaymentDefinition>)FacadeFunctions("select", "studentselectclasses", "ClassID", addClassID);
            int count = 0;
            bool allSelected = false;

            //if (addClassID == "BB0999")
            //{
            //    string temp = "";
            //}
            if (studentData != null && checkStudentData != null)
            {
                //foreach (var studentSingle in studentData)
                //{
                    //if (!(bool)FacadeFunctions("check", "studentinclass", (object)studentSingle.StudentID, addClassID))
                    //    allSelected = false;

                //}
                foreach (var studentSingle in checkStudentData)
                {
                    count = studentData.Where(s => s.StudentID.Contains(studentSingle.StudentID)).Count();
                    if (count > 0)
                    {
                        allSelected = true;
                        break;
                    }

                }
            }

            return allSelected;
        }

        #endregion

        #region CountData

        private object CountClassByClassCategoryName(string classCategoryName)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.CountClassByClassCategoryName(classCategoryName);
        }

        private object CountStudentNumberByClassID(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.CountStudentNumberByClassID(classID);
        }

        private object CountStudentInClass(string fromDate, string endDate)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.SelectStudentInClassCount(fromDate, endDate);
        }

        #endregion

        #region Reuse Function

        private string CheckSystemLegal(string applicationPath)
        {
#if !DEBUG
            fOper = new FileOperator();
            string msg = "啟動失敗: 此系統為不法系統!!";
            string checkPassword = "";
            string checkOrder = "";
            string pathOfEMSSystemOne = "C:\\Documents and Settings\\All Users";
            string pathOfEMSSystemTwo = "C:\\Program Files\\EMSSystem";
            string pathOfEMSSystemThree = applicationPath;
            string pathOfPasswordOrder = "C:\\WINDOWS\\system";

            string fileNameInDrives = "\\SystemPass.ems";
            string fileNameInApplication = "\\PasswordCheck.txt";
            string fileNameOfPasswordOrder = "\\PasswordOrder.ems";

            checkOrder = fOper.ReadData(pathOfPasswordOrder + fileNameOfPasswordOrder);

            if (checkOrder != "File is not Exist!!")
            {
                checkPassword += fOper.ReadData(pathOfEMSSystemOne + fileNameInDrives) + ",";
                checkPassword += fOper.ReadData(pathOfEMSSystemTwo + fileNameInDrives) + ",";
                checkPassword += fOper.ReadData(pathOfEMSSystemThree + fileNameInApplication);

                if (checkPassword.IndexOf("File is not Exist!!") == -1)
                {
                    string[] orderNum = checkOrder.Split(',');
                    string[] passwordSet = checkPassword.Split(',');

                    string getPassword = "";
                    for (int i = 0; i < orderNum.Length; i++)
                    {
                        for (int j = 0; j < orderNum.Length; j++)
                        {
                            if (i == int.Parse(orderNum[j]))
                                getPassword += passwordSet[j] + "-";
                        }
                    }

                    getPassword = getPassword.Remove(getPassword.Length - 1);

                    try
                    {
                        Guid finalCheck = new Guid(getPassword);
                        msg = "";
                    }
                    catch
                    {
                    }
                }
            }
            
            return msg;
#else
            return "";
#endif
        }

        private string GetLegalUSBDrive(string applicationPath)
        {
            fOper = new FileOperator();
            string msg = "備份失敗: 指定之外接硬碟不存在!!";
            string saveUSBDrive = "";
            string usbGuidInApplication, usbGuidInCDrive, usbGuidInUSBDrive;
            string pathOfCDrive = "C:\\WINDOWS\\system";
            string pathOfApplication = applicationPath;
            string pathOfUSBDrive = "EMSSystem\\Password";

            string fileNameInDrives = "\\USBPass.ems";
            string fileNameInApplication = "\\PasswordMatch.txt";

            usbGuidInApplication = fOper.ReadData(pathOfApplication + fileNameInApplication);

            if (usbGuidInApplication != "File is not Exist!!")
            {
                usbGuidInCDrive = fOper.ReadData(pathOfCDrive + fileNameInDrives);

                if (usbGuidInCDrive != "File is not Exist!!")
                {
                    usbGuidInUSBDrive = fOper.FindFileInDriveByAbsolutePath(pathOfUSBDrive + fileNameInDrives, out saveUSBDrive);

                    if (usbGuidInUSBDrive != "File is not Exist!!" && usbGuidInUSBDrive != "")
                    {
                        try
                        {
                            Guid checkGuid = new Guid(usbGuidInCDrive + usbGuidInApplication);

                            if (usbGuidInCDrive + "-" + usbGuidInApplication == usbGuidInUSBDrive + "-" + usbGuidInApplication)
                            {
                                saveUSBDrive += "EMSSystem\\Backup";
                                fOper.DeleteDirectory(saveUSBDrive);
                                fOper.CreateDirectory(saveUSBDrive);

                                //Move Exe Files
                                //File.Copy("mysql.exe", saveUSBDrive + "\\" + "mysql.exe");
                                //File.Copy("mysqldump.exe", saveUSBDrive + "\\" + "mysqldump.exe");
                                //File.Copy("mysqlimport.exe", saveUSBDrive + "\\" + "mysqlimport.exe");

                                ////Move Batch Files
                                //File.Copy("MySql_Backup.bat", saveUSBDrive + "\\" + "MySql_Backup_v1.bat");
                                //File.Copy("MySql_OriginalRestore.BAT", saveUSBDrive + "\\" + "MySql_OriginalRestore.BAT");
                                //File.Copy("MySql_Restore.BAT", saveUSBDrive + "\\" + "MySql_Restore.BAT");

                                ////Move Sql Files
                                File.Copy("backupfile.sql", saveUSBDrive + "\\" + "usbbackupfile.sql");
                                //File.Copy("originalbackupfile.sql", saveUSBDrive + "\\" + "originalbackupfile.sql");
                                //File.Copy("EducateManagement_BasicCreate.sql", saveUSBDrive + "\\" + "EducateManagement_BasicCreate.sql");
                                msg = "備份指定之外接硬碟成功!!";
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            return msg;
        }

        private void SetCurrentUser(string username)
        {
            List<StaffAccountDefinition> staffAccountData = null;

            fOper = new FileOperator();
            fOper.CreateFiles("User.txt");
            fOper.WriteData("User.txt", username + ";");
        }

        private object StudentClassPayment(string[] moneyInfo)
        {
            dbAccess = new DataAccess(_SystemType);
            ClassPaymentDefinition classPayment;
            StudentPrepaidDefinition studentPrepaid;
            string studentID, classID, className, staffEnglishName, payType, needReceipt, studentName, studentInClassID;
            int inputMoneyAfterPay = 0, inputMoney, needToPayMoney, prePaidMoney, payment = 0, usePrepaid = 0;

            studentID = moneyInfo[0];
            classID = moneyInfo[1];
            staffEnglishName = moneyInfo[2];
            inputMoney = int.Parse(moneyInfo[3]);
            needToPayMoney = int.Parse(moneyInfo[4]);
            prePaidMoney = int.Parse(moneyInfo[5]);
            payType = moneyInfo[6];
            needReceipt = moneyInfo[7];
            studentName = moneyInfo[8];
            className = moneyInfo[9];

            //int needToPay = 0;
            //int moneyCheck = int.Parse(inputMoney) - int.Parse(needToPayMoney);

            //if (moneyCheck >= 0)
            //    needToPay = int.Parse(needToPayMoney) + int.Parse(prePaidMoney);
            //else if (moneyCheck < 0)
            //    needToPay = int.Parse(inputMoney) + int.Parse(prePaidMoney);
            if (prePaidMoney > 0)
            {
                if ((prePaidMoney + inputMoney) > needToPayMoney)
                {
                    int tempNeedToPay = needToPayMoney - inputMoney;

                    if (tempNeedToPay > 0)
                        usePrepaid = needToPayMoney - inputMoney;
                }
                else
                    usePrepaid = prePaidMoney;

                if (usePrepaid > 0)
                {
                    string events = "課程繳費: " + className + "(" + classID + ")";
                    studentPrepaid = new StudentPrepaidDefinition(studentID, FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString(),
                                                                  0, usePrepaid, StaticFunction.SetEncodingString(events));

                    FacadeFunctions("insert", "studentprepaid", (object)studentPrepaid, null);
                }
            }

            if ((prePaidMoney + inputMoney) > needToPayMoney)
            {
                if (inputMoney > 0)
                {
                    payment = needToPayMoney;

                    //No Matter NeedToPayMoney >, =, or < inputMoney
                    //PrepaidMoney All + the value after inputMoney - needToPayMoney
                    //prePaidMoney = prePaidMoney + inputMoney - needToPayMoney;

                    inputMoney = inputMoney - needToPayMoney;

                    if (inputMoney < 0)
                        prePaidMoney = prePaidMoney + inputMoney;
                }
                else if (inputMoney == 0)
                {
                    payment = needToPayMoney;
                    prePaidMoney = prePaidMoney - needToPayMoney;
                }
            }
            else //((prePaidMoney + inputMoney) <= needToPayMoney)
            {
                payment = inputMoney + prePaidMoney;
                prePaidMoney = 0;
            }


            studentInClassID = dbAccess.SelectStudentInClassIDByIDsForStudentPayment(int.Parse(studentID), classID);
            classPayment = new ClassPaymentDefinition(0, studentID, "", classID,
                                                      "", studentInClassID, 0, staffEnglishName, "", "", payment,
                                                      FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString(),
                                                      StaticFunction.SetEncodingString(payType));

            FacadeFunctions("insert", "studentclasspayment", (object)classPayment, null);

            if (bool.Parse(needReceipt))
            {
                string printClassName = "";
                string[] receiptInfo = new string[6];
                receiptInfo[0] = studentID;
                receiptInfo[1] = studentName;
                receiptInfo[2] = classID;
                receiptInfo[4] = payment.ToString();
                receiptInfo[5] = staffEnglishName;

                ClassDefinition classData = (ClassDefinition)FacadeFunctions("select", "class", "ID", classID);
                printClassName = classData.Name;

                if (moneyInfo.Length > 10)
                {
                    if (!string.IsNullOrEmpty(moneyInfo[10]))
                    {
                        if (bool.Parse(moneyInfo[10]))
                            printClassName += " (轉)";
                    }
                }

                receiptInfo[3] = printClassName;

                SettleDrawingReceiptPicForPayment(receiptInfo);
            }

            //if (moneyCheck > 0)
            //    FacadeFunctions("update", "prepaid", (object)int.Parse(studentID), (object)moneyCheck);
            //else
            //    FacadeFunctions("update", "prepaid", (object)int.Parse(studentID), (object)0);

            FacadeFunctions("update", "prepaid", (object)int.Parse(studentID), (object)prePaidMoney);
            moneyInfo[3] = inputMoney.ToString();
            //return (object)"學生付費完成!!!";
            return (object)moneyInfo;
        }

        private void SetStudentRefundInfo(string[] refundInfo)
        {
            XMLFileOperator xFOper = new XMLFileOperator();
            XDocument loadDoc;

            string filePath = "RefundInfo.xml";

            if (!xFOper.CheckXMLExist(filePath))
                xFOper.CreateXMLFiles(filePath, "Record Refund Info", "Refunds");
            else
            {
                loadDoc = XDocument.Load(filePath);

                var refundData = from a in loadDoc.Descendants("Refund")
                                 select a;

                XElement newElement = refundData.First();
                newElement.Remove();

                loadDoc.Save(filePath);
            }

            loadDoc = XDocument.Load(filePath);

            loadDoc.Element("Refunds").Add(new XElement("Refund",
                                           new XElement("ID", refundInfo[0]),
                                           new XElement("Event", refundInfo[1]),
                                           new XElement("RefundMoney", refundInfo[2]),
                                           new XElement("TempRefundMoney", refundInfo[3]),
                                           new XElement("RefundDiscount", refundInfo[4]),
                                           new XElement("StudentID", refundInfo[5])));

            loadDoc.Save(filePath);
        }

        private object GetStudentRefundInfo()
        {
            XMLFileOperator xFOper = new XMLFileOperator();
            XDocument loadDoc;
            string filePath = "RefundInfo.xml";

            string[] refundInfo = new string[6];
            loadDoc = XDocument.Load(filePath);

            var refundInfos = from r in loadDoc.Descendants("Refund")
                              select r;

            foreach (var refundInfoSingle in refundInfos)
            {
                refundInfo[0] = refundInfoSingle.Element("ID").Value;
                refundInfo[1] = refundInfoSingle.Element("Event").Value;
                refundInfo[2] = refundInfoSingle.Element("RefundMoney").Value;
                refundInfo[3] = refundInfoSingle.Element("TempRefundMoney").Value;
                refundInfo[4] = refundInfoSingle.Element("RefundDiscount").Value;
                refundInfo[5] = refundInfoSingle.Element("StudentID").Value;
            }

            return refundInfo;
        }

        private object SelectDataByDate(object dataSets, string[] selectInfo)
        {
            object afterDate = null;

            try
            {
                DateTime fromDate = DateTime.Parse(selectInfo[1]);
                DateTime endDate = DateTime.Parse(selectInfo[2]);

                //if (selectInfo[0] == "ClassRefund")
                //{
                List<RecordDefinition> recordSets = (List<RecordDefinition>)dataSets;
                List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();

                foreach (var recordSingle in recordSets)
                {
                    if (DateTime.Parse(recordSingle.Date1) >= fromDate && DateTime.Parse(recordSingle.Date1) <= endDate)
                        tempRecordSets.Add(recordSingle);
                }

                afterDate = (object)tempRecordSets;
                //}
            }
            catch
            {
            }

            return afterDate;
        }

        private object SelectNeedToPayList(string selectBy, string data)
        {
            if (selectBy.IndexOf("ID") > -1)
                return FacadeFunctions("select", "studentpaymentforrecord", (object)selectBy, (object)data);
            else // if (selectBy.IndexOf("Name") > -1)
            {
                List<RecordDefinition> studentPaymentSets = new List<RecordDefinition>();

                if (selectBy.IndexOf("Student") > -1)
                {
                    List<StudentDefinition> studentSets = (List<StudentDefinition>)FacadeFunctions("select", "student", (object)"Name", (object)data);

                    if (studentSets != null && studentSets.Count > 0)
                    {
                        foreach (var studentSingle in studentSets)
                        {
                            List<RecordDefinition> tempStudentPaymentSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentforrecord", (object)"StudentID", (object)studentSingle.ID);

                            if (tempStudentPaymentSets != null && tempStudentPaymentSets.Count > 0)
                                studentPaymentSets.AddRange(tempStudentPaymentSets);
                        }
                    }
                }
                else
                {
                    List<ClassDefinition> classSets = (List<ClassDefinition>)FacadeFunctions("select", "class", (object)"Name", (object)data);

                    if (classSets != null && classSets.Count > 0)
                    {
                        foreach (var classSingle in classSets)
                        {
                            List<RecordDefinition> tempStudentPaymentSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentforrecord", (object)"ClassID", (object)classSingle.ID);

                            if (tempStudentPaymentSets != null && tempStudentPaymentSets.Count > 0)
                                studentPaymentSets.AddRange(tempStudentPaymentSets);
                        }
                    }
                }

                return (object)studentPaymentSets;
            }
        }

        private object SelectStudentInClassCountByID(string[] selectInfo)
        {
            List<RecordDefinition> recordSets = new List<RecordDefinition>();

            try
            {
                string classID = selectInfo[0];
                string fromDate = "";
                string endDate = "";

                if (selectInfo[1] != null)
                {
                    fromDate = selectInfo[1];
                    endDate = selectInfo[2];
                }

                List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();

                tempRecordSets = (List<RecordDefinition>)FacadeFunctions("count", "studentinclass", fromDate, endDate);

                foreach (var recordSingle in tempRecordSets)
                {
                    if (recordSingle.Data1ID == classID)
                        recordSets.Add(recordSingle);
                }
            }
            catch
            {
            }

            return (object)recordSets;
        }

        private object SelectStudentPaymentRecordByID(string selectType, string[] selectInfo)
        {
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();

            try
            {
                string selectIDs = selectInfo[0];
                string fromDate = "";
                string endDate = "";

                if (selectType == "FromClass")
                {
                    string dates = null;

                    if (selectInfo[1] != null)
                    {
                        fromDate = selectInfo[1];
                        endDate = selectInfo[2];
                        dates = fromDate + "," + endDate;
                        tempRecordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordtotalbydate", "WithStudentID", dates);
                    }
                    else
                        tempRecordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordtotal", "StudentID", null);

                    List<RecordDefinition> tempRecordListSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordlist", "ClassID", selectIDs);

                    foreach (var recordSingle in tempRecordSets)
                    {
                        foreach (var recordListSingle in tempRecordListSets)
                        {
                            if (recordSingle.Data2ID == recordListSingle.Data1ID)
                                recordSets.Add(recordSingle);
                        }
                    }

                }
                else if (selectInfo[1] != null)
                {
                    fromDate = selectInfo[1];
                    endDate = selectInfo[2];
                    string dates = fromDate + "," + endDate;

                    tempRecordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordtotalbydate", selectType, dates);

                    foreach (var recordSingle in tempRecordSets)
                    {
                        if (recordSingle.Data2ID.IndexOf(selectIDs) > -1)
                            recordSets.Add(recordSingle);
                    }
                }
                else
                {
                    if (selectType.IndexOf("Student") > -1)
                        selectType = "StudentID";
                    else
                        selectType = "ClassID";

                    recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordtotalbyids", selectType, selectIDs);
                }

                if (recordSets != null && recordSets.Count > 0)
                    recordSets = recordSets.Distinct().ToList();
            }
            catch
            {
            }

            return (object)recordSets;
        }

        private object SelectStudentPrepaidHistoryByIDs(string selectType, string[] selectInfo)
        {
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();
            List<StudentDefinition> studentSets = new List<StudentDefinition>();

            try
            {
                string selectIDs = selectInfo[0];
                string fromDate = "";
                string endDate = "";
                string dates = "";

                if (selectInfo[1] != null)
                {
                    fromDate = selectInfo[1];
                    endDate = selectInfo[2];
                    dates = fromDate + "," + endDate;
                }

                if (selectType.IndexOf("Student") > -1)
                    recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentprepaidhistorytotal", selectIDs, dates);
                else
                {
                    selectType = "ClassID";

                    studentSets = (List<StudentDefinition>)FacadeFunctions("select", "studentbyclass", "ID", selectIDs);

                    foreach (var studentSingle in studentSets)
                    {
                        tempRecordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentprepaidhistorytotal", (object)studentSingle.ID, (object)dates);
                        recordSets.AddRange(tempRecordSets);
                    }
                }

                if (recordSets != null && recordSets.Count > 0)
                {
                    //var tempRecordSet = (from r in recordSets
                    //                     select new RecordDefinition
                    //                     {
                    //                         Data2ID = r.Data2ID,
                    //                         Data2Name = r.Data2Name,
                    //                         Money1 = r.Money1
                    //                     }).Distinct();
                    recordSets = recordSets.Distinct().ToList();
                }
            }
            catch
            {
            }

            return (object)recordSets;
        }

        private object SelectStudentPrepaidHistoryByIDsForStudentPayment(string selectType, string[] selectInfo)
        {
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();
            List<StudentDefinition> studentSets = new List<StudentDefinition>();

            try
            {
                string selectIDs = selectInfo[0];
                string fromDate = "";
                string endDate = "";
                string dates = "";

                if (selectInfo[1] != null)
                {
                    fromDate = selectInfo[1];
                    endDate = selectInfo[2];
                    dates = fromDate + "," + endDate;
                }

                if (selectType.IndexOf("Student") > -1)
                    recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentprepaidhistorytotalforstudentpayment", selectIDs, dates);
                else
                {
                    SelectStudentPrepaidHistoryByIDs(selectType, selectInfo);
                }

                if (recordSets != null && recordSets.Count > 0)
                    recordSets = recordSets.Distinct().ToList();
            }
            catch
            {
            }

            return (object)recordSets;
        }

        #endregion

        #region Print Function

        private object SettleDrawingReceiptPicFoRefund(string[] receiptInfo)
        {
            string studentID = receiptInfo[0];
            string studentName = receiptInfo[1];
            string refundType = receiptInfo[2];
            string receiver = receiptInfo[3];
            string refundMoney = receiptInfo[4];
            string staffEnglishName = receiptInfo[5];

            if (!string.IsNullOrEmpty(receiptInfo[3]))
                receiver = "收取人: " + receiptInfo[3];

            return DrawingReceiptPic(studentID, studentName, refundType, refundMoney, receiver, "", staffEnglishName);
        }

        private object SettleDrawingReceiptPicForPayment(string[] receiptInfo)
        {
            string studentID = receiptInfo[0];
            string studentName = receiptInfo[1];
            string classID = receiptInfo[2];
            string className = receiptInfo[3];
            string payment = receiptInfo[4];
            string staffEnglishName = receiptInfo[5];
            string classStartDate = "";
            string classEndDate = "";
            string classPeriod = "";

            List<StudentInClassDefinition> studentInClassSets = (List<StudentInClassDefinition>)FacadeFunctions("select", "studentinclass", int.Parse(studentID), null);

            foreach (var studentInClassSingle in studentInClassSets)
            {
                if (studentInClassSingle.ClassID == classID)
                {
                    classStartDate = studentInClassSingle.AddDate;
                    classEndDate = studentInClassSingle.EndDate;
                    classPeriod = studentInClassSingle.ClassPeriod.ToString();
                }
            }

            return DrawingReceiptPic(studentID, studentName, className, payment, classStartDate + " ~ " + classEndDate, classPeriod, staffEnglishName);
        }

        private object SettleDrawingReceiptPicForPrepaid(string[] receiptInfo)
        {
            string studentID = receiptInfo[0];
            string studentName = receiptInfo[1];
            string prepaid = receiptInfo[2];
            string staffEnglishName = receiptInfo[3];

            return DrawingReceiptPic(studentID, studentName, "預繳金額", prepaid, "", "", staffEnglishName);
        }

        private object DrawingReceiptPic(string studentID, string studentName, string className, string payment, string classDate, string classPeriod, string staffEnglishName)
        {
            string today = (string)FacadeFunctions("format", "datebydatetime", DateTime.Now, null);
            string supervisor = "Manager";

            CompanyInfoDefinition companyInfo = (CompanyInfoDefinition)FacadeFunctions("select", "whole", "CompanyInfo", null);
            if (companyInfo != null && companyInfo.CompanyManager != null && companyInfo.CompanyManager != "")
                supervisor = companyInfo.CompanyManager;

            Bitmap img = null;
            Graphics graphic;
            LinearGradientBrush blackBrush;
            Pen blackPen;
            Font titleFont;
            int drawWidth, drawingLocationY;

            //Image Width
            drawWidth = 700;

            //Set Picture as Bitmap
            img = new Bitmap(drawWidth, 480); //Image Height
            graphic = Graphics.FromImage(img);

            //Set Pens
            blackPen = Pens.Black;

            //Set Fonts
            titleFont = new Font("新細明體", Configuration.PrintFont, FontStyle.Regular);

            //Set Blush
            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

            //Set Background Colour and Height
            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

            drawingLocationY = 95;
            //Payment Date
            graphic.DrawString(today, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(today, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 35;
            //Class ID
            graphic.DrawString(studentID, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(studentID, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 35;
            //Class Name
            graphic.DrawString(className, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(className, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 32;
            //Student Name
            graphic.DrawString(studentName, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(studentName, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 35;
            //Payment
            graphic.DrawString(payment, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(payment, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 35;
            //Class Dates
            graphic.DrawString(classDate, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(classDate, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 35;
            //Class Period
            graphic.DrawString(classPeriod, titleFont, blackBrush, 120, drawingLocationY);
            graphic.DrawString(classPeriod, titleFont, blackBrush, 470, drawingLocationY);

            drawingLocationY += 160;
            //Supervisor
            graphic.DrawString(supervisor, titleFont, blackBrush, 60, drawingLocationY);
            graphic.DrawString(supervisor, titleFont, blackBrush, 400, drawingLocationY);

            //Staff English Name
            graphic.DrawString(staffEnglishName, titleFont, blackBrush, 230, drawingLocationY);
            graphic.DrawString(staffEnglishName, titleFont, blackBrush, 630, drawingLocationY);

            PrintDialog printReceipt = new PrintDialog();
            printReceipt.PrintBitmap(img, "");
            return (object)img;
        }

        private object SettleDrawingNeedToPayNotice(string[] needToPayInfo)
        {
            List<StudentPaymentDefinition> studentPaymentSets = null;
            List<StudentPaymentDefinition> tempStudentPaymentSets = null;

            Bitmap img = null;
            string needToPayByWho = needToPayInfo[0];
            string studentID = "", studentName = "";
            string notice = needToPayInfo[2], tempNotice = "";
            int needToPay = 0, prePaid = 0, finalPayment = 0, totalPages = 0;

            if (needToPayByWho == "StudentID")
            {
                studentID = needToPayInfo[1];
                studentPaymentSets = (List<StudentPaymentDefinition>)FacadeFunctions("select", "studentpaymentlist", "StudentID", (object)int.Parse(studentID));

                if (studentPaymentSets != null)
                {
                    try
                    {
                        studentPaymentSets = studentPaymentSets.OrderBy(u => u.StartDate).ToList();
                    }
                    catch
                    {
                    }

                    studentID = studentPaymentSets.ElementAt(0).StudentID;
                    studentName = studentPaymentSets.ElementAt(0).StudentName;
                    prePaid = int.Parse(FacadeFunctions("select", "studentprepaid", (object)int.Parse(studentID), null).ToString());

                    foreach (var studentPaymentSingle in studentPaymentSets)
                        needToPay += studentPaymentSingle.NeedToPay;

                    finalPayment = needToPay - prePaid;

                    totalPages = studentPaymentSets.Count / 6;

                    if ((studentPaymentSets.Count % 6) != 0)
                        totalPages++;

                    for (int i = 1; i <= totalPages; i++)
                    {
                        tempNotice = notice;
                        if (i != totalPages)
                        {
                            tempStudentPaymentSets = studentPaymentSets.GetRange((i - 1) * 6, 6);
                            tempNotice = "";
                        }
                        else
                        {
                            int finalPageItemCount = studentPaymentSets.Count % 6;

                            if (finalPageItemCount == 0)
                                finalPageItemCount = 6;

                            tempStudentPaymentSets = studentPaymentSets.GetRange((i - 1) * 6, finalPageItemCount);
                        }

                        img = (Bitmap)DrawingNeedToPayNotice(studentID, studentName, needToPay.ToString(), prePaid.ToString(), finalPayment.ToString(),
                                                             tempStudentPaymentSets, "叮嚀語: ", tempNotice, i.ToString(), totalPages.ToString());
                    }
                }
            }

            return img;
        }

        private object DrawingNeedToPayNotice(string studentID, string studentName, string needToPay, string prePaid, string finalPayment,
                                                                  List<StudentPaymentDefinition> studentPaymentSets, string noticeTitle, string notice, string currentPage, string totalPages)
        {
            Bitmap img = null;
            Graphics graphic;
            LinearGradientBrush blackBrush;
            Pen blackPen;
            Font titleFont, normalFont, extraLargeFont;
            int drawWidth, firstLineX, itemHeight, classNameWidth = 0;

            string today = (string)FacadeFunctions("format", "datebydatetime", DateTime.Now, null);

            //Image Width
            drawWidth = 900;

            //Set Picture as Bitmap
            img = new Bitmap(drawWidth, 480); //Image Height
            graphic = Graphics.FromImage(img);

            //Set Pens
            blackPen = Pens.Black;

            //Set Fonts
            extraLargeFont = new Font("新細明體", Configuration.PrintExtraFont, FontStyle.Bold);
            titleFont = new Font("新細明體", Configuration.PrintTitleFont, Configuration.PrintFontStyle);
            normalFont = new Font("新細明體", Configuration.PrintFont, Configuration.PrintFontStyle);

            //Set Blush
            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

            //Set Background Colour and Height
            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

            //Print Date & Pages
            graphic.DrawString(DateTime.Now.ToString("yyyy年 MM月 dd日"), normalFont, blackBrush, 10, 50);
            graphic.DrawString("第 " + currentPage + " 頁, 共 " + totalPages + " 頁", normalFont, blackBrush, 780, 50);

            firstLineX = 10;
            //Need To Pay Title
            graphic.DrawString("繳費通知", titleFont, blackBrush, firstLineX, 85);
            graphic.DrawString("繳費通知", titleFont, blackBrush, firstLineX, 85);
            //graphic.DrawString("學生:", titleFont, blackBrush, firstLineX + 100, 85);
            graphic.DrawString(int.Parse(studentID).ToString("000000"), extraLargeFont, blackBrush, firstLineX + 100, 70);
            //graphic.DrawString("姓名:", titleFont, blackBrush, firstLineX + 260, 85);
            graphic.DrawString(studentName, extraLargeFont, blackBrush, firstLineX + 260, 70);
            graphic.DrawString("應繳:", titleFont, blackBrush, firstLineX + 420, 85);
            graphic.DrawString(needToPay, titleFont, blackBrush, firstLineX + 500, 85);
            graphic.DrawString("預收:", titleFont, blackBrush, firstLineX + 580, 85);
            graphic.DrawString(prePaid, titleFont, blackBrush, firstLineX + 660, 85);
            graphic.DrawString("需繳:", titleFont, blackBrush, firstLineX + 740, 85);
            graphic.DrawString(finalPayment, titleFont, blackBrush, firstLineX + 820, 85);

            //Class Name
            itemHeight = 150;
            if (studentPaymentSets != null)
            {
                foreach (var studentPaymentSingle in studentPaymentSets)
                {
                    if (classNameWidth < studentPaymentSingle.ClassName.Length)
                        classNameWidth = studentPaymentSingle.ClassName.Length;

                    graphic.DrawString(studentPaymentSingle.ClassName, normalFont, blackBrush, 10, itemHeight);
                    itemHeight += 30;
                }
            }

            classNameWidth *= 20;
            if (classNameWidth < 85)
                classNameWidth = 85;
            classNameWidth += 10;

            if (classNameWidth + 715 > 900)
                classNameWidth = classNameWidth - 50;

            //Item Header
            graphic.DrawString("課程名稱", normalFont, blackBrush, 10, 120);
            graphic.DrawString("上課日期", normalFont, blackBrush, classNameWidth, 120);
            graphic.DrawString("結束日期", normalFont, blackBrush, classNameWidth + 100, 120);
            graphic.DrawString("課程價格", normalFont, blackBrush, classNameWidth + 180, 120);
            graphic.DrawString("教材費用", normalFont, blackBrush, classNameWidth + 280, 120);
            graphic.DrawString("報名費用", normalFont, blackBrush, classNameWidth + 380, 120);
            graphic.DrawString("折扣金額", normalFont, blackBrush, classNameWidth + 480, 120);
            graphic.DrawString("已繳金額", normalFont, blackBrush, classNameWidth + 580, 120);
            graphic.DrawString("應繳金額", normalFont, blackBrush, classNameWidth + 680, 120);

            //Print Line
            graphic.DrawLine(blackPen, 10, 140, 900, 140);

            //Other Items
            itemHeight = 150;
            float moneyPlace = 0.0F;
            if (studentPaymentSets != null)
            {
                foreach (var studentPaymentSingle in studentPaymentSets)
                {
                    if (studentPaymentSingle.NeedToPay > 0)
                    {
                        graphic.DrawString(studentPaymentSingle.StartDate, normalFont, blackBrush, classNameWidth, itemHeight);
                        graphic.DrawString(studentPaymentSingle.EndDate, normalFont, blackBrush, classNameWidth + 100, itemHeight);

                        moneyPlace = PrintMoneyPlace(studentPaymentSingle.ClassPrice, classNameWidth + 170);
                        graphic.DrawString(studentPaymentSingle.ClassPrice.ToString(), normalFont, blackBrush, moneyPlace, itemHeight);

                        moneyPlace = PrintMoneyPlace(studentPaymentSingle.ClassMaterialFee, classNameWidth + 270);
                        graphic.DrawString(studentPaymentSingle.ClassMaterialFee.ToString(), normalFont, blackBrush, moneyPlace, itemHeight);

                        moneyPlace = PrintMoneyPlace(studentPaymentSingle.ClassApplyFee, classNameWidth + 370);
                        graphic.DrawString(studentPaymentSingle.ClassApplyFee.ToString(), normalFont, blackBrush, moneyPlace, itemHeight);

                        moneyPlace = PrintMoneyPlace(studentPaymentSingle.Discount, classNameWidth + 470);
                        graphic.DrawString(studentPaymentSingle.Discount.ToString(), normalFont, blackBrush, moneyPlace, itemHeight);

                        moneyPlace = PrintMoneyPlace(studentPaymentSingle.HavePaid, classNameWidth + 570);
                        graphic.DrawString(studentPaymentSingle.HavePaid.ToString(), normalFont, blackBrush, moneyPlace, itemHeight);

                        moneyPlace = PrintMoneyPlace(studentPaymentSingle.NeedToPay, classNameWidth + 670);
                        graphic.DrawString(studentPaymentSingle.NeedToPay.ToString(), normalFont, blackBrush, moneyPlace, itemHeight);

                        //needToPay += studentPaymentSingle.NeedToPay;
                        itemHeight += 30;
                    }
                }
            }

            //Print Sub Total Amount Line
            graphic.DrawLine(blackPen, 10, itemHeight - 12, 900, itemHeight - 12);

            ////Student Prepaid Money
            //graphic.DrawString("應繳總額:", titleFont, blackBrush, 700, itemHeight + 30);

            //moneyPlace = PrintMoneyPlace(needToPay, 800);
            //graphic.DrawString(needToPay.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 30);

            //int prePaid = int.Parse(FacadeFunctions("select", "studentprepaid", (object)int.Parse(studentID), null).ToString());
            //graphic.DrawString("- 預收金額:", titleFont, blackBrush, 700, itemHeight + 60);

            //moneyPlace = PrintMoneyPlace(prePaid, 800);
            //graphic.DrawString(prePaid.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 60);

            ////Print Total Amount Line
            //graphic.DrawLine(blackPen, 700, itemHeight + 80, 900, itemHeight + 80);

            ////Student Prepaid Money
            //needToPay = needToPay - prePaid;
            //graphic.DrawString("需繳總額:", titleFont, blackBrush, 700, itemHeight + 90);

            //moneyPlace = PrintMoneyPlace(needToPay, 800);
            //graphic.DrawString(needToPay.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 90);

            if (notice != "")
            {
                //string notice1 = "";
                graphic.DrawString(noticeTitle, normalFont, blackBrush, 100, 335);

                //if (notice.Length > 30)
                //{
                //    notice1 = notice.Substring(30);
                //    notice = notice.Substring(0, 30);
                //}

                graphic.DrawString(notice, normalFont, blackBrush, 170, 335);

                //if (notice1 != "")
                //    graphic.DrawString(notice1, titleFont, blackBrush, 140, itemHeight + 150);
            }

            PrintDialog printReceipt = new PrintDialog();
            printReceipt.PrintBitmap(img, "notice");
            return (object)img;
        }

        private object DrawingNeedToPayA4(string classID, string className)
        {
            Bitmap img = null;
            List<RecordDefinition> recordSets = null;
            List<RecordDefinition> tempRecordSets = null;
            recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentforrecord", "ClassID", (object)classID);

            const int FirstPage_Item = 28, Page_Item = 30;
            int totalPages = 1, currentPage = 1;
            int totalItems = recordSets.Count - FirstPage_Item, currentItem = 0;

            if (totalItems > 0)
            {
                totalPages += totalItems / Page_Item + (totalItems % Page_Item > 0 ? 1 : 0);
                currentItem = FirstPage_Item;
            }
            else
            {
                totalItems = recordSets.Count;
                currentItem = totalItems;
            }

            tempRecordSets = recordSets.GetRange(0, currentItem);
            string moneySets = DrawingNeedToPayByClass(tempRecordSets, classID, true, totalPages, 1, 0, 0, 0);
            recordSets.RemoveRange(0, currentItem);

            while (recordSets.Count > 0)
            {
                if (totalItems > Page_Item)
                    currentItem = Page_Item;
                else
                    currentItem = recordSets.Count;

                currentPage++;
                string[] moneys = moneySets.Split(',');
                tempRecordSets = recordSets.GetRange(0, currentItem);
                moneySets = DrawingNeedToPayByClass(tempRecordSets, classID, false, totalPages, currentPage,
                                                    int.Parse(moneys[0]), int.Parse(moneys[1]), int.Parse(moneys[2]));

                recordSets.RemoveRange(0, currentItem);
                totalItems -= Page_Item;
            }
            
            return (object)img;
        }

        private string DrawingNeedToPayByClass(List<RecordDefinition> recordSets, string classID, bool firstPage, int totalPages, int currentPage,
                                               int needToPay, int totalMoney, int havePaid)
        {
            //List<RecordDefinition> recordSets = null;
            ClassDefinition classData = null;
            Bitmap img = null;
            Graphics graphic;
            LinearGradientBrush blackBrush;
            Pen blackPen;
            Font titleFont;
            int drawWidth;
            int itemHeight;

            //Image Width
            drawWidth = 850;

            //Set Picture as Bitmap
            img = new Bitmap(drawWidth, 1100); //Image Height
            graphic = Graphics.FromImage(img);

            //Set Pens
            blackPen = Pens.Black;

            //Set Fonts
            titleFont = new Font("新細明體", Configuration.PrintFont, FontStyle.Regular);

            //Set Blush
            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

            //Set Background Colour and Height
            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

            if (firstPage)
            {
                string companyName = "EMS System";
                CompanyInfoDefinition companyInfo = (CompanyInfoDefinition)FacadeFunctions("select", "whole", "CompanyInfo", null);
                if (companyInfo != null && companyInfo.CompanyName != null && companyInfo.CompanyName != "")
                    companyName = companyInfo.CompanyName;

                string today = (string)FacadeFunctions("format", "datebydatetime", DateTime.Now, null);

                classData = (ClassDefinition)FacadeFunctions("select", "class", (object)"ID", (object)classID);

                //Title
                graphic.DrawString(companyName, titleFont, blackBrush, 400, 50);
                graphic.DrawString("應收費用查詢", titleFont, blackBrush, 400, 80);

                //Need To Pay Title
                graphic.DrawString("課程編號:", titleFont, blackBrush, 200, 110);
                graphic.DrawString(classID, titleFont, blackBrush, 300, 110);

                graphic.DrawString("課程名稱:", titleFont, blackBrush, 400, 110);
                graphic.DrawString(classData.Name, titleFont, blackBrush, 500, 110);

                //Class Time
                graphic.DrawString("課程時間:", titleFont, blackBrush, 200, 140);
                graphic.DrawString(classData.StartDate + " 至 " + classData.EndDate + ", 共 " + classData.ClassPeriod.ToString() + " 節", titleFont, blackBrush, 300, 140);

                //Pages
                graphic.DrawString("第 " + currentPage.ToString() + " 頁, 共 " + totalPages.ToString() + " 頁", titleFont, blackBrush, 700, 140);

                itemHeight = 160;
            }
            else
            {
                graphic.DrawString("第 " + currentPage.ToString() + " 頁, 共 " + totalPages.ToString() + " 頁", titleFont, blackBrush, 700, 50);
                itemHeight = 70;
            }

            //Print Line
            graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

            itemHeight += 10;
            //Item Header
            graphic.DrawString("學生編號", titleFont, blackBrush, 0, itemHeight);
            graphic.DrawString("學生姓名", titleFont, blackBrush, 90, itemHeight);
            //graphic.DrawString("上課日期", titleFont, blackBrush, 180, itemHeight);
            //graphic.DrawString("結束日期", titleFont, blackBrush, 270, itemHeight);
            graphic.DrawString("課程價格", titleFont, blackBrush, 180, itemHeight);
            graphic.DrawString("教材費用", titleFont, blackBrush, 270, itemHeight);
            graphic.DrawString("報名費用", titleFont, blackBrush, 360, itemHeight);
            graphic.DrawString("折扣金額", titleFont, blackBrush, 450, itemHeight);
            graphic.DrawString("應繳金額", titleFont, blackBrush, 540, itemHeight);
            graphic.DrawString("已繳金額", titleFont, blackBrush, 630, itemHeight);
            graphic.DrawString("應繳餘額", titleFont, blackBrush, 720, itemHeight);

            itemHeight += 20;
            //Print Line
            graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

            //Other Items
            itemHeight += 10;
            float moneyPlace = 0.0F;
            if (recordSets != null)
            {
                foreach (var recordSingle in recordSets)
                {
                    graphic.DrawString(recordSingle.Data1ID, titleFont, blackBrush, 0, itemHeight);
                    graphic.DrawString(recordSingle.Data1Name, titleFont, blackBrush, 90, itemHeight);
                    //graphic.DrawString(recordSingle.Date1, titleFont, blackBrush, 180, itemHeight);
                    //graphic.DrawString(recordSingle.Date2.ToString(), titleFont, blackBrush, 270, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.Money1, 180);
                    graphic.DrawString(recordSingle.Money1.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.Money2, 270);
                    graphic.DrawString(recordSingle.Money2.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(int.Parse(recordSingle.Data2ID), 360);
                    graphic.DrawString(recordSingle.Data2ID, titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.Discount, 450);
                    graphic.DrawString(recordSingle.Discount.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.Money1 + recordSingle.Money2 + int.Parse(recordSingle.Data2ID) - recordSingle.Discount, 540);
                    graphic.DrawString((recordSingle.Money1 + recordSingle.Money2 + int.Parse(recordSingle.Data2ID) - recordSingle.Discount).ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(int.Parse(recordSingle.Note1), 630);
                    graphic.DrawString(recordSingle.Note1, titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(int.Parse(recordSingle.Note2), 720);
                    graphic.DrawString(recordSingle.Note2, titleFont, blackBrush, moneyPlace, itemHeight);

                    totalMoney += recordSingle.Money1 + recordSingle.Money2 + int.Parse(recordSingle.Data2ID) - recordSingle.Discount;
                    havePaid += int.Parse(recordSingle.Note1);
                    needToPay += int.Parse(recordSingle.Note2);
                    itemHeight += 30;
                }
            }

            if (totalPages == currentPage)
            {
                //Print Line
                graphic.DrawLine(blackPen, 10, itemHeight + 10, drawWidth, itemHeight + 10);

                //Item Header
                graphic.DrawString("應收金額", titleFont, blackBrush, 540, itemHeight + 20);
                graphic.DrawString("已收金額", titleFont, blackBrush, 630, itemHeight + 20);
                graphic.DrawString("應收餘額", titleFont, blackBrush, 720, itemHeight + 20);

                //Total Need To Pay Money
                graphic.DrawString("應收總額合計:", titleFont, blackBrush, 0, itemHeight + 50);

                moneyPlace = PrintMoneyPlace(totalMoney, 540);
                graphic.DrawString(totalMoney.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 50);

                moneyPlace = PrintMoneyPlace(havePaid, 630);
                graphic.DrawString(havePaid.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 50);

                moneyPlace = PrintMoneyPlace(needToPay, 720);
                graphic.DrawString(needToPay.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 50);
            }

            PrintDialog printReceipt = new PrintDialog();
            printReceipt.PrintBitmap(img, "A4");
            return needToPay + "," + totalMoney + "," + havePaid;
        }

        private object DrawingHavePaidList(string[] havePaidInfo)
        {
            Bitmap img = null;
            List<RecordDefinition> recordSets = null;
            List<RecordDefinition> newRecordSets = new List<RecordDefinition>();
            List<RecordDefinition> tempRecordSets = null;
            ClassDefinition classData = null;
            StudentDefinition studentData = null;

            string needToPrint = havePaidInfo[0];
            string searchID = havePaidInfo[1];
            string searchName = null;
            string searchBy = havePaidInfo[2];
            string searchStartDate = havePaidInfo[3];
            string searchEndDate = havePaidInfo[4];
            bool isSelectAll = false;
            string moneyDate = "";

            try
            {
                isSelectAll = bool.Parse(havePaidInfo[5]);
            }
            catch { isSelectAll = false; }

            if (needToPrint.IndexOf("明細") > -1)
            {
                recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentprepaidhistorylistbyid", (object)searchID, null);
                studentData = (StudentDefinition)FacadeFunctions("select", "student", (object)"ID", (object)searchID);
                searchName = studentData.Name;
            }
            else
            {
                if (isSelectAll)
                {
                    dbAccess = new DataAccess(_SystemType);

                    if (!string.IsNullOrEmpty(searchStartDate) && !string.IsNullOrEmpty(searchEndDate))
                        recordSets = (List<RecordDefinition>)dbAccess.SelectStudentPaymentWithSearchDate(searchStartDate, searchEndDate);
                    else
                        recordSets = (List<RecordDefinition>)dbAccess.SelectStudentPaymentAllRecord();

                    if (searchBy.IndexOf("全班") == -1)
                        recordSets = recordSets.OrderBy(r => r.Data2ID).OrderBy(r => int.Parse(r.Data1ID)).ToList();
                    else
                        recordSets = recordSets.OrderBy(r => int.Parse(r.Data1ID)).OrderBy(r => r.Data2ID).ToList();
                }
                else
                {
                    if (searchBy.IndexOf("全班") == -1)
                    {
                        recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordlist", "StudentID", searchID);
                        var prepaidRecordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentprepaidhistorylistbyid", (object)searchID, null);
                        if (prepaidRecordSets != null && prepaidRecordSets.Count > 0)
                            recordSets.AddRange(prepaidRecordSets);
                        studentData = (StudentDefinition)FacadeFunctions("select", "student", (object)"ID", (object)searchID);
                        searchName = studentData.Name;
                    }
                    else
                    {
                        recordSets = (List<RecordDefinition>)FacadeFunctions("select", "studentpaymentrecordlist", "ClassID", searchID);
                        classData = (ClassDefinition)FacadeFunctions("select", "class", (object)"ID", (object)searchID);
                        searchName = classData.Name;
                    }
                }
            }

            if (searchStartDate != "" && searchEndDate != "")
            {
                foreach (var recordSingle in recordSets)
                {
                    if (needToPrint.IndexOf("明細") > -1)
                        moneyDate = recordSingle.Date1.ToString();
                    else
                        moneyDate = recordSingle.Note1;

                    if (DateTime.Parse(moneyDate) >= DateTime.Parse(searchStartDate) && DateTime.Parse(moneyDate) <= DateTime.Parse(searchEndDate))
                        newRecordSets.Add(recordSingle);
                }

                recordSets = newRecordSets;
            }

            if (recordSets != null)
            {
                const int FirstPage_Item = 28, Page_Item = 30;
                int totalPages = 1, currentPage = 1;
                int totalItems = recordSets.Count - FirstPage_Item, currentItem = 0;

                if (totalItems > 0)
                {
                    totalPages += totalItems / Page_Item + (totalItems % Page_Item > 0 ? 1 : 0);
                    currentItem = FirstPage_Item;
                }
                else
                {
                    totalItems = recordSets.Count;
                    currentItem = totalItems;
                }

                tempRecordSets = recordSets.GetRange(0, currentItem);
                string totalMoney = DrawingHavePaid(tempRecordSets, searchBy, searchID, searchName, searchStartDate, searchEndDate, true, totalPages, 1, 0, needToPrint, isSelectAll);
                recordSets.RemoveRange(0, currentItem);

                while (recordSets.Count > 0)
                {
                    if (totalItems > Page_Item)
                        currentItem = Page_Item;
                    else
                        currentItem = recordSets.Count;

                    currentPage++;
                    tempRecordSets = recordSets.GetRange(0, currentItem);
                    totalMoney = DrawingHavePaid(tempRecordSets, searchBy, searchID, searchName, searchStartDate, searchEndDate, false, totalPages, currentPage,
                                                int.Parse(totalMoney), needToPrint, isSelectAll);

                    recordSets.RemoveRange(0, currentItem);
                    totalItems -= Page_Item;
                }
            }

            return (object)img;
        }

        private string DrawingHavePaid(List<RecordDefinition> recordSets, string searchBy, string itemID, string itemName, string startDate, string endDate,
                                       bool firstPage, int totalPages, int currentPage, int totalMoney, string needToPrint, bool isSelectAll)
        {
            //List<RecordDefinition> recordSets = null;
            Bitmap img = null;
            Graphics graphic;
            LinearGradientBrush blackBrush;
            Pen blackPen;
            Font titleFont;
            int drawWidth;
            int itemWidth, itemHeight, itemWidthGap;
            string printTitle = null, printItemID = null, printItemName = null, printExtraItemID = null, printExtraItemName = null;

            //Image Width
            drawWidth = 850;

            //Set Picture as Bitmap
            img = new Bitmap(drawWidth, 1100); //Image Height
            graphic = Graphics.FromImage(img);

            //Set Pens
            blackPen = Pens.Black;

            //Set Fonts
            titleFont = new Font("新細明體", Configuration.PrintFont, Configuration.PrintFontStyle);

            //Set Blush
            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

            //Set Background Colour and Height
            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

            if (firstPage)
            {
                string companyName = "EMS System";
                CompanyInfoDefinition companyInfo = (CompanyInfoDefinition)FacadeFunctions("select", "whole", "CompanyInfo", null);
                if (companyInfo != null && companyInfo.CompanyName != null && companyInfo.CompanyName != "")
                    companyName = companyInfo.CompanyName;

                string today = (string)FacadeFunctions("format", "datebydatetime", DateTime.Now, null);

                //Title
                graphic.DrawString(companyName, titleFont, blackBrush, 400, 50);

                if (needToPrint.IndexOf("明細") > -1)
                    printTitle = "預收";
                else
                    printTitle = "繳費";

                int printDataY = 0;
                if (isSelectAll)
                {
                    printDataY = 110;
                }
                else
                {
                    if (searchBy.IndexOf("全班") == -1)
                    {
                        printItemID = "學生編號:";
                        printItemName = "學生姓名:";
                    }
                    else
                    {
                        printItemID = "課程編號:";
                        printItemName = "課程名稱:";
                    }

                    printDataY = 140;
                }

                graphic.DrawString(printTitle + "明細查詢", titleFont, blackBrush, 400, 80);

                //Need To Pay Title
                if (!isSelectAll)
                {
                    graphic.DrawString(printItemID, titleFont, blackBrush, 200, 110);
                    graphic.DrawString(itemID, titleFont, blackBrush, 300, 110);

                    graphic.DrawString(printItemName, titleFont, blackBrush, 400, 110);
                    graphic.DrawString(itemName, titleFont, blackBrush, 500, 110);
                }

                //Search Date
                if (startDate != "" && endDate != "")
                {
                    graphic.DrawString("查詢日期:", titleFont, blackBrush, 200, printDataY);
                    graphic.DrawString(startDate + " 至 " + endDate, titleFont, blackBrush, 300, printDataY);
                }

                //Pages
                graphic.DrawString("第 " + currentPage.ToString() + " 頁, 共 " + totalPages.ToString() + " 頁", titleFont, blackBrush, 700, printDataY);

                itemHeight = printDataY + 20;
            }
            else
            {
                graphic.DrawString("第 " + currentPage.ToString() + " 頁, 共 " + totalPages.ToString() + " 頁", titleFont, blackBrush, 700, 50);
                itemHeight = 70;
            }

            //Print Line
            graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

            if (needToPrint.IndexOf("明細") == -1)
            {
                if (isSelectAll)
                {
                    if (searchBy.IndexOf("全班") == -1)
                    {
                        printItemID = "學生編號:";
                        printItemName = "學生姓名:";
                        printExtraItemID = "課程編號:";
                        printExtraItemName = "課程名稱:";
                    }
                    else
                    {
                        printItemID = "課程編號:";
                        printItemName = "課程名稱:";
                        printExtraItemID = "學生編號:";
                        printExtraItemName = "學生姓名:";
                    }
                }
                else
                {
                    if (searchBy.IndexOf("全班") == -1)
                    {
                        printItemID = "課程編號:";
                        printItemName = "課程名稱:";
                    }
                    else
                    {
                        printItemID = "學生編號:";
                        printItemName = "學生姓名:";
                    }
                }

                if (isSelectAll)
                {
                    itemWidth = 30;
                    itemWidthGap = 100;
                }
                else
                {
                    itemWidth = 100;
                    itemWidthGap = 120;
                }
                itemHeight += 10;

                //Item Header
                graphic.DrawString(printItemID, titleFont, blackBrush, itemWidth, itemHeight);
                itemWidth += itemWidthGap;
                graphic.DrawString(printItemName, titleFont, blackBrush, itemWidth, itemHeight);

                if (isSelectAll)
                {
                    if (searchBy.IndexOf("全班") > -1)
                        itemWidth += 20;

                    itemWidth += itemWidthGap;
                    graphic.DrawString(printExtraItemID, titleFont, blackBrush, itemWidth, itemHeight);
                    itemWidth += itemWidthGap;
                    graphic.DrawString(printExtraItemName, titleFont, blackBrush, itemWidth, itemHeight);

                    if (searchBy.IndexOf("全班") == -1)
                        itemWidth += 20;
                }

                itemWidth += itemWidthGap;
                graphic.DrawString("上課日期", titleFont, blackBrush, itemWidth, itemHeight);
                itemWidth += itemWidthGap;
                graphic.DrawString("結束日期", titleFont, blackBrush, itemWidth, itemHeight);
                itemWidth += itemWidthGap;
                graphic.DrawString("繳費日期", titleFont, blackBrush, itemWidth, itemHeight);
                itemWidth += itemWidthGap;
                graphic.DrawString("繳費金額", titleFont, blackBrush, itemWidth, itemHeight);

                itemHeight += 20;
                //Print Line
                graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

                //Other Items
                itemHeight += 10;
                float moneyPlace = 0.0F;
                if (recordSets != null)
                {
                    foreach (var recordSingle in recordSets)
                    {
                        if (isSelectAll)
                        {
                            itemWidth = 30;

                            if (searchBy.IndexOf("全班") == -1)
                            {
                                graphic.DrawString(recordSingle.Data1ID, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += itemWidthGap;
                                graphic.DrawString(recordSingle.Data1Name, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += itemWidthGap;
                                graphic.DrawString(recordSingle.Data2ID, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += itemWidthGap;
                                graphic.DrawString(recordSingle.Data2Name, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += 20;
                            }
                            else
                            {
                                graphic.DrawString(recordSingle.Data2ID, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += itemWidthGap;
                                graphic.DrawString(recordSingle.Data2Name, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += itemWidthGap + 20;
                                graphic.DrawString(recordSingle.Data1ID, titleFont, blackBrush, itemWidth, itemHeight);
                                itemWidth += itemWidthGap;
                                graphic.DrawString(recordSingle.Data1Name, titleFont, blackBrush, itemWidth, itemHeight);
                            }
                        }
                        else
                        {
                            itemWidth = 100;
                            if (searchBy.IndexOf("全班") == -1)
                            {
                                graphic.DrawString(recordSingle.Data2ID, titleFont, blackBrush, itemWidth, itemHeight);
                                graphic.DrawString(recordSingle.Data2Name, titleFont, blackBrush, itemWidth + itemWidthGap, itemHeight);
                            }
                            else
                            {
                                graphic.DrawString(recordSingle.Data1ID, titleFont, blackBrush, itemWidth, itemHeight);
                                graphic.DrawString(recordSingle.Data1Name, titleFont, blackBrush, itemWidth + itemWidthGap, itemHeight);
                            }
                            itemWidth += itemWidthGap;
                        }

                        itemWidth += itemWidthGap;
                        graphic.DrawString(recordSingle.Date1, titleFont, blackBrush, itemWidth, itemHeight);
                        itemWidth += itemWidthGap;
                        graphic.DrawString(recordSingle.Date2, titleFont, blackBrush, itemWidth, itemHeight);
                        itemWidth += itemWidthGap;
                        graphic.DrawString(recordSingle.Note1, titleFont, blackBrush, itemWidth, itemHeight);

                        itemWidth += itemWidthGap;
                        moneyPlace = PrintMoneyPlace(recordSingle.Money1, itemWidth);
                        graphic.DrawString(recordSingle.Money1.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                        totalMoney += recordSingle.Money1;
                        itemHeight += 30;
                    }
                }

                if (totalPages == currentPage)
                {
                    //Print Line
                    graphic.DrawLine(blackPen, 10, itemHeight + 10, drawWidth, itemHeight + 10);

                    //Total Need To Pay Money
                    graphic.DrawString("應收總額合計:", titleFont, blackBrush, 100, itemHeight + 20);

                    moneyPlace = PrintMoneyPlace(totalMoney, 700);
                    graphic.DrawString(totalMoney.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 20);
                }
            }
            else //if(needToPrint.IndexOf("明細") >= -1)
            {
                itemHeight += 10;
                //Item Header
                graphic.DrawString("異動日期", titleFont, blackBrush, 150, itemHeight);
                graphic.DrawString("收取金額", titleFont, blackBrush, 300, itemHeight);
                graphic.DrawString("使用金額", titleFont, blackBrush, 450, itemHeight);
                graphic.DrawString("異動方式", titleFont, blackBrush, 600, itemHeight);

                itemHeight += 20;
                //Print Line
                graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

                //Other Items
                itemHeight += 10;
                float moneyPlace = 0.0F;
                if (recordSets != null)
                {
                    foreach (var recordSingle in recordSets)
                    {
                        graphic.DrawString(recordSingle.Date1, titleFont, blackBrush, 150, itemHeight);

                        moneyPlace = PrintMoneyPlace(recordSingle.Money1, 300);
                        graphic.DrawString(recordSingle.Money1.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                        moneyPlace = PrintMoneyPlace(recordSingle.Money2, 450);
                        graphic.DrawString(recordSingle.Money2.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                        graphic.DrawString(recordSingle.Note1, titleFont, blackBrush, 600, itemHeight);

                        totalMoney += recordSingle.Money1 - recordSingle.Money2;
                        itemHeight += 30;
                    }
                }

                if (totalPages == currentPage)
                {
                    //Print Line
                    graphic.DrawLine(blackPen, 10, itemHeight + 10, drawWidth, itemHeight + 10);

                    //Total Need To Pay Money
                    graphic.DrawString("現有預收金額:", titleFont, blackBrush, 100, itemHeight + 20);

                    moneyPlace = PrintMoneyPlace(totalMoney, 700);
                    graphic.DrawString(totalMoney.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 20);
                }
            }

            PrintDialog printReceipt = new PrintDialog();
            printReceipt.PrintBitmap(img, "A4");
            return totalMoney.ToString();
        }

        private float PrintMoneyPlace(int money, int startInt)
        {
            int unitInterval = 10;

            int printMoneyPlace = startInt + (80 - unitInterval * money.ToString().Length);

            if (money.ToString().Length > 5)
            {
                printMoneyPlace = startInt + (85 - unitInterval * money.ToString().Length);
            }
            else if (money.ToString().Length > 4)
            {
                printMoneyPlace = startInt + (83 - unitInterval * money.ToString().Length);
            }

            return printMoneyPlace;
        }

        private float PrintMoneyPlace(double money, int startInt)
        {
            int unitInterval = 10;

            int printMoneyPlace = startInt + (80 - unitInterval * money.ToString().Length);

            if (money.ToString().Length > 5)
            {
                printMoneyPlace = startInt + (85 - unitInterval * money.ToString().Length);
            }
            else if (money.ToString().Length > 4)
            {
                printMoneyPlace = startInt + (83 - unitInterval * money.ToString().Length);
            }

            return printMoneyPlace;
        }

        private object DrawingClassList()
        {
            Bitmap img = null;
            Graphics graphic;
            LinearGradientBrush blackBrush;
            Pen blackPen;
            Font titleFont;
            int drawWidth;

            List<ClassDefinition> classSets = (List<ClassDefinition>)FacadeFunctions("select", "whole", (object)"Class", null);

            //Image Width
            drawWidth = 700;

            //Set Picture as Bitmap
            img = new Bitmap(drawWidth, 1100); //Image Height
            graphic = Graphics.FromImage(img);

            //Set Pens
            blackPen = Pens.Black;

            //Set Fonts
            titleFont = new Font("新細明體", Configuration.PrintFont, FontStyle.Regular);

            //Set Blush
            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

            //Set Background Colour and Height
            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

            //Class Title
            int classIDX = 60, classNameX = 210;
            graphic.DrawString("課程編號", titleFont, blackBrush, classIDX, 50);
            graphic.DrawString("課程名稱", titleFont, blackBrush, classNameX, 50);

            int currentHeight = 80;
            int topLength = 0;
            int printList = 0;
            foreach (var classSingle in classSets)
            {
                if (classSingle.IsDeleted == '0')
                {
                    if (currentHeight == 1070)
                    {
                        printList++;

                        if (printList == 1)
                        {
                            classIDX = 400 + topLength;
                            classNameX = classIDX + 150;

                            graphic.DrawLine(blackPen, classIDX - 50, 80, classIDX - 50, 1100);
                        }
                        else if (printList >= 2)
                        {
                            //Draw Line
                            graphic.DrawLine(blackPen, 40, 80, 700, 80);

                            PrintDialog printClassList = new PrintDialog();
                            printClassList.PrintBitmap(img, "A4");

                            printList = 0;
                            img = new Bitmap(drawWidth, 1100);
                            graphic = Graphics.FromImage(img);

                            //Set Blush
                            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

                            //Set Background Colour and Height
                            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

                            classIDX = 60;
                            classNameX = 210;
                        }

                        currentHeight = 80;

                        graphic.DrawString("課程編號", titleFont, blackBrush, classIDX, 50);
                        graphic.DrawString("課程名稱", titleFont, blackBrush, classNameX, 50);
                    }

                    if (topLength < classSingle.Name.Length)
                        topLength = classSingle.Name.Length;

                    currentHeight += 30;
                    graphic.DrawString(classSingle.ID, titleFont, blackBrush, classIDX, currentHeight);
                    graphic.DrawString(classSingle.Name, titleFont, blackBrush, classNameX, currentHeight);
                }
            }

            if (printList < 2)
            {
                //Draw Line
                graphic.DrawLine(blackPen, 40, 80, 700, 80);

                PrintDialog printClassList = new PrintDialog();
                printClassList.PrintBitmap(img, "A4");
            }

            return (object)img;
        }

        private object DrawingExpenseLog(string startDate, string endDate)
        {
            Bitmap img = null;
            List<ExpanseDefinition> tempExpanseSets = null;
            List<ExpanseDefinition> expanseSets = (List<ExpanseDefinition>)FacadeFunctions("select", "dailyexpansebydates", startDate, endDate);

            const int FirstPage_Item = 34, Page_Item = 36;
            int totalPages = 1, currentPage = 1;
            int totalItems = expanseSets.Count - FirstPage_Item, currentItem = 0;

            if (totalItems > 0)
            {
                totalPages += totalItems / Page_Item + (totalItems % Page_Item > 0 ? 1 : 0);
                currentItem = FirstPage_Item;
            }
            else
            {
                totalItems = expanseSets.Count;
                currentItem = totalItems;
            }

            tempExpanseSets = expanseSets.GetRange(0, currentItem);
            double totalMoney = DrawingExpenseLogList(tempExpanseSets, true, totalPages, 1, 0);
            expanseSets.RemoveRange(0, currentItem);

            while (expanseSets.Count > 0)
            {
                if (totalItems > Page_Item)
                    currentItem = Page_Item;
                else
                    currentItem = expanseSets.Count;

                currentPage++;
                tempExpanseSets = expanseSets.GetRange(0, currentItem);
                totalMoney = DrawingExpenseLogList(tempExpanseSets, false, totalPages, currentPage, totalMoney);

                expanseSets.RemoveRange(0, currentItem);
                totalItems -= Page_Item;
            }
            
            return (object)img;
        }

        private double DrawingExpenseLogList(List<ExpanseDefinition> expanseSets, bool firstPage, int totalPages, int currentPage, double totalMoney)
        {
            Bitmap img = null;
            Graphics graphic;
            LinearGradientBrush blackBrush;
            Pen blackPen;
            Font titleFont;
            int drawWidth;
            int itemHeight;

            //Image Width
            drawWidth = 850;

            //Set Picture as Bitmap
            img = new Bitmap(drawWidth, 1100); //Image Height
            graphic = Graphics.FromImage(img);

            //Set Pens
            blackPen = Pens.Black;

            //Set Fonts
            titleFont = new Font("新細明體", Configuration.PrintFont, FontStyle.Regular);

            //Set Blush
            blackBrush = new LinearGradientBrush(new Rectangle(0, 0, img.Width, img.Height), Color.Black, Color.Black, 1.2F, true);

            //Set Background Colour and Height
            graphic.DrawRectangle(new Pen(Color.White, 1100), 0, 0, img.Width, img.Height);

            if (firstPage)
            {
                string companyName = "EMS System";
                CompanyInfoDefinition companyInfo = (CompanyInfoDefinition)FacadeFunctions("select", "whole", "CompanyInfo", null);
                if (companyInfo != null && companyInfo.CompanyName != null && companyInfo.CompanyName != "")
                    companyName = companyInfo.CompanyName;

                string today = (string)FacadeFunctions("format", "datebydatetime", DateTime.Now, null);

                //Title
                graphic.DrawString(companyName, titleFont, blackBrush, 300, 50);
                graphic.DrawString("每日支出記錄", titleFont, blackBrush, 400, 80);

                //Pages
                graphic.DrawString("第 " + currentPage.ToString() + " 頁, 共 " + totalPages.ToString() + " 頁", titleFont, blackBrush, 700, 80);

                itemHeight = 100;
            }
            else
            {
                graphic.DrawString("第 " + currentPage.ToString() + " 頁, 共 " + totalPages.ToString() + " 頁", titleFont, blackBrush, 700, 50);
                itemHeight = 70;
            }

            //Print Line
            graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

            itemHeight += 10;
            //Item Header
            graphic.DrawString("支出項目", titleFont, blackBrush, 0, itemHeight);
            graphic.DrawString("單項金額", titleFont, blackBrush, 200, itemHeight);
            graphic.DrawString("項目數量", titleFont, blackBrush, 290, itemHeight);
            graphic.DrawString("支出總額", titleFont, blackBrush, 380, itemHeight);
            graphic.DrawString("新增員工", titleFont, blackBrush, 470, itemHeight);
            graphic.DrawString("新增時間", titleFont, blackBrush, 560, itemHeight);
            graphic.DrawString("修改員工", titleFont, blackBrush, 650, itemHeight);
            graphic.DrawString("修改時間", titleFont, blackBrush, 740, itemHeight);

            itemHeight += 20;
            //Print Line
            graphic.DrawLine(blackPen, 0, itemHeight, drawWidth, itemHeight);

            //Other Items
            itemHeight += 10;
            float moneyPlace = 0.0F;
            if (expanseSets != null)
            {
                foreach (var recordSingle in expanseSets)
                {
                    graphic.DrawString(recordSingle.ItemName, titleFont, blackBrush, 0, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.UnitPrice, 180);
                    graphic.DrawString(recordSingle.UnitPrice.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.Quantity, 270);
                    graphic.DrawString(recordSingle.Quantity.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);

                    moneyPlace = PrintMoneyPlace(recordSingle.TotalMoney, 360);
                    graphic.DrawString(recordSingle.TotalMoney.ToString(), titleFont, blackBrush, moneyPlace, itemHeight);
                    graphic.DrawString(recordSingle.InsertStaffName, titleFont, blackBrush, 470, itemHeight);
                    graphic.DrawString(recordSingle.InsertDate, titleFont, blackBrush, 560, itemHeight);
                    graphic.DrawString(recordSingle.UpdateStaffName, titleFont, blackBrush, 650, itemHeight);
                    graphic.DrawString(recordSingle.UpdateDate, titleFont, blackBrush, 740, itemHeight);

                    totalMoney += recordSingle.TotalMoney;
                    itemHeight += 30;
                }
            }

            if (totalPages == currentPage)
            {
                //Total Need To Pay Money
                graphic.DrawString("總金額:", titleFont, blackBrush, 600, itemHeight + 30);

                moneyPlace = PrintMoneyPlace(totalMoney, 720);
                graphic.DrawString(totalMoney.ToString(), titleFont, blackBrush, moneyPlace, itemHeight + 50);
            }

            PrintDialog printReceipt = new PrintDialog();
            printReceipt.PrintBitmap(img, "A4");
            return totalMoney;
        }

        #endregion

        #region SelectData

        private object GetCurrentUser()
        {
            StaffAccountDefinition staffAccountData = null;

            fOper = new FileOperator();

            if (fOper.CheckFileExist("User.txt"))
            {
                string data = fOper.ReadData("User.txt");
                string[] dataSet = data.Split(';');

                if (dataSet.Count() > 0)
                {
                    staffAccountData = (StaffAccountDefinition)FacadeFunctions("select", "staffaccountbyenglishname", dataSet[0].Trim(), null);
                    staffAccountData.StaffName = dataSet[0].Trim();
                }

                fOper.DeleteFile("User.txt");
            }

            return (object)staffAccountData;
        }

        private object GetSideFunctions()
        {
            List<SideFunctionsDefinition> sideFunctionsSet = null;

            fOper = new FileOperator();
            string data = fOper.ReadData("SideFunctions.txt");
            string[] dataSet = data.Split(';');

            if (dataSet.Count() > 0)
            {
                sideFunctionsSet = new List<SideFunctionsDefinition>();

                for (int i = 0; i < dataSet.Count() - 1; i++)
                {
                    string[] item = dataSet[i].Split(',');
                    sideFunctionsSet.Add(new SideFunctionsDefinition(int.Parse(item[0]), item[1].Trim(), int.Parse(item[2])));
                }
            }

            return (object)sideFunctionsSet;
        }

        private object GetSideSubFunctions(int mainID)
        {
            List<SideFunctionsDefinition> sideFunctionsSet = null;

            fOper = new FileOperator();
            string data = fOper.ReadData("SideFunctions.txt");
            string[] dataSet = data.Split(';');

            if (dataSet.Count() > 0)
            {
                sideFunctionsSet = new List<SideFunctionsDefinition>();

                for (int i = 0; i < dataSet.Count() - 1; i++)
                {
                    string[] item = dataSet[i].Split(',');

                    if (int.Parse(item[2]) == mainID)
                        sideFunctionsSet.Add(new SideFunctionsDefinition(int.Parse(item[0]), item[1].Trim(), int.Parse(item[2])));
                }
            }

            return (object)sideFunctionsSet;
        }

        private object SelectWholeInfo(string tableName)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.SelectWholeData(tableName);
        }

        //Student Data
        private object SelectStudentInfo(string studentItemName, string studentItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            if (studentItemName == "ID")
                return (object)DecodeStudent(dbAccess.SelectStudentDataByID(studentItemValue));
            else if (studentItemName == "Name")
                return (object)DecodeStudent(dbAccess.SelectStudentDataByName(studentItemValue));
            else
                return null;
        }

        private object SelectStudentAll()
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.SelectStudentData();
        }

        private object SelectStudentInfoByDeleted(string studentItemName, string studentItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            if (studentItemName == "ID")
                return (object)DecodeStudent(dbAccess.SelectStudentDataByID(studentItemValue, true));
            else if (studentItemName == "Name")
                return (object)DecodeStudent(dbAccess.SelectStudentDataByName(studentItemValue, true));
            else
                return null;
        }

        private object SelectStudentInfoByClass(string classItemName, string classItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)DecodeStudent(dbAccess.SelectStudentDataByClassIDorName(classItemName, classItemValue));
        }

        //Class Data
        private object SelectClassWholeInfo(string classItemName, string classItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            //if (classItemName == "ID")
            //    return (object)dbAccess.SelectClassDataByIDorName(classItemName, classItemValue);
            //else if (classItemName == "Name")
            //    return (object)dbAccess.SelectClassDataByName(classItemValue);
            //else
            //    return null;
            List<ClassDefinition> tempClassSets = (List<ClassDefinition>)dbAccess.SelectWholeData("Class");
            List<ClassDefinition> classSets = new List<ClassDefinition>();

            if (tempClassSets.Count > 0)
            {
                foreach (var classSingle in tempClassSets)
                {
                    if (classItemName.IndexOf("ID") > -1)
                    {
                        if (classSingle.ID == classItemValue)
                            classSets.Add(classSingle);
                    }
                    else if (classItemName.IndexOf("Name") > -1)
                    {
                        if (classSingle.Name.IndexOf(classItemValue) > -1)
                            classSets.Add(classSingle);
                    }
                }
            }
            else
                classSets = tempClassSets;

            return (object)classSets;
        }

        private object SelectClassByIsDeleted()
        {
            dbAccess = new DataAccess(_SystemType);
            List<ClassDefinition> tempClassSets = (List<ClassDefinition>)dbAccess.SelectWholeData("Class");
            List<ClassDefinition> classSets = new List<ClassDefinition>();

            if (tempClassSets.Count > 0)
            {
                foreach (var classSingle in tempClassSets)
                {
                    if (classSingle.IsDeleted == '0')
                        classSets.Add(classSingle);
                }
            }
            else
                classSets = tempClassSets;

            return (object)classSets;
        }

        private object SelectClassByIsDeleted(string classItemName, string classItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            List<ClassDefinition> tempClassSets = (List<ClassDefinition>)dbAccess.SelectWholeData("Class");
            List<ClassDefinition> classSets = new List<ClassDefinition>();
            ClassDefinition classSelected = new ClassDefinition();

            if (tempClassSets.Count > 0)
            {
                foreach (var classSingle in tempClassSets)
                {
                    if (classSingle.IsDeleted == '0')
                        classSets.Add(classSingle);
                }
            }
            else
                classSets = tempClassSets;

            if (classItemName == "ID")
            {
                return (object)classSets.Where(c => c.ID.ToLower() == classItemValue.ToLower()).SingleOrDefault();
            }
            else if (classItemName == "Name")
            {
                var newClassSet = classSets.Where(c => c.Name.Contains(classItemValue));
                return (object)newClassSet.ToList();
            }
            else if (classItemName == "All")
                return (object)classSets;
            else
                return null;
        }

        private object SelectClassInfo(string classItemName, string classItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            if (classItemName == "ID")
                return (object)dbAccess.SelectClassDataByIDorName(classItemName, classItemValue);
            else if (classItemName == "Name")
                return (object)dbAccess.SelectClassDataByName(classItemValue);
            else
                return null;
        }

        private object SelectClassInfoByEndDate(string classItemName, string classItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            if (classItemName == "ID")
                return (object)dbAccess.SelectClassDataByIDAndEndDate(classItemValue);
            else if (classItemName == "Name")
                return (object)dbAccess.SelectClassDataByNameAndEndDate(classItemValue);
            else if (classItemName == "All")
                return (object)dbAccess.SelectClassDataByEndDate();
            else
                return null;
        }

        private object SelectClassNameByClassCategory(string classCategoryItemName, string classCategoryItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectClassDataByClassCategory(classCategoryItemName, classCategoryItemValue);
        }

        private object SelectClassTimeInfo(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectClassTimeByClassID(classID);
        }

        //Staff Data
        private object SelectStaffInfoByID(int staffID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStaffByID(staffID);
        }

        private object SelectStaffInfoByName(string name)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStaffByName(name);
        }

        private object SelectStaffInfoByEnglishName(string engName)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStaffByEnglishName(engName);
        }

        private object SelectStaffAccountInfoByEnglishName(string engName)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStaffAccountByEnglishName(engName);
        }

        private object SelectStaffRoleList(int staffRoleID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStaffRoleMatchName(staffRoleID);
        }

        //Student Number
        private object SelectStudentInClassTotalNumber()
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentInClassTotalNumber();
        }

        private object SelectStudentTotalNumber()
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentTotalNumber();
        }

        //Student In Class Data
        private object SelectStudentInClassInfo(int studentID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentInClassByStudentID(studentID);
        }

        private object SelectStudentInClassID(int studentID, string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentInClassIDByIDs(studentID, classID);
        }

        //Student Select Classes
        private object SelectStudentSelectClassListByEndDate(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            List<StudentPaymentDefinition> studentClassList = (List<StudentPaymentDefinition>)SelectStudentSelectClassList(inputType, studentOrClassID);

            return studentClassList.Where(sc => DateTime.Parse(sc.EndDate) >= DateTime.Now).ToList();
        }

        private object SelectStudentSelectClassList(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentSelectClassByStudentIDOrClassID(inputType, studentOrClassID);
        }

        private object SelectStudentSelectClassListInRecordListFormat(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            List<StudentPaymentDefinition> studentPaymentSet = (List<StudentPaymentDefinition>)dbAccess.SelectStudentSelectClassByStudentIDOrClassID(inputType, studentOrClassID);
            List<RecordDefinition> recordSets = new List<RecordDefinition>();
            RecordDefinition recordData = null;

            int havePaid = 0, discount = 0, needToPay = 0, price = 0, classSeat = 0;
            string classID = null, className = null;
            foreach (var studentSelectClassData in studentPaymentSet)
            {
                recordData = new RecordDefinition();

                if (inputType == "StudentID")
                {
                    if (studentSelectClassData.StudentID != null)
                    {
                        recordData.Data1ID = studentSelectClassData.StudentID;
                        recordData.Data1Name = studentSelectClassData.StudentName;
                        recordData.Note1 = studentPaymentSet.Count.ToString();
                        recordData.Discount = studentSelectClassData.NeedToPay;
                        recordData.Note2 = studentSelectClassData.HavePaid.ToString();
                        recordSets.Add(recordData);
                    }
                }
                else if (inputType == "ClassID")
                {
                    havePaid += studentSelectClassData.HavePaid;
                    discount += studentSelectClassData.Discount;
                    price += studentSelectClassData.ClassPrice + studentSelectClassData.ClassMaterialFee + studentSelectClassData.ClassApplyFee;
                    classID = studentSelectClassData.ClassID;
                    className = studentSelectClassData.ClassName;
                    classSeat++;
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
            }

            return (object)recordSets;
        }

        //Student Payment Data
        private object SelectClassPaymentRecord(int studentID, string[] classIDSet)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordByStudentID(studentID, classIDSet);
        }

        private object SelectClassPaymentRecordTopSix(int studentID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordTopSixByStudentID(studentID);
        }

        private object SelectClassPaymentRecordByClassID(int studentID, string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordByStudentIDAndClassID(studentID, classID);
        }

        private object SelectStudentPaymentList(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentListByStudentIDOrClassID(inputType, studentOrClassID);
        }

        //Student Have Paid Data
        private object SelectStudentClassHavePaidPayment(int studentID, string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentClassHavePaidPayment(studentID, classID);
        }

        //Student Prepaid Data
        private object SelectStudentPrePaid(int studentID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPrePaid(studentID);
        }

        //Student Have To Pay Data
        private object SelectStudentNeedToPayClass(int studentID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentNeedToPayClassByStudentID(studentID);
        }

        private object SelectStudentNeedToPayMoney(int studentID, string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentNeedToPayMoneyByIDs(studentID, classID);
        }

        //Student Have To Refund Data
        private object SelectStudentHaveToRefundList(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentHaveToRefundListByStudentIDOrClassID(inputType, studentOrClassID);
        }

        private object SelectStudentHaveToRefundListInStudentList(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentHaveToRefundListByStudentIDOrClassIDInStudentList(inputType, studentOrClassID);
        }

        private object SelectStudentHaveToRefundByClassIDOrName(string classItemName, string classItemValue)
        {
            dbAccess = new DataAccess(_SystemType);
            if (classItemName == "ID")
                return (object)dbAccess.SelectStudentHaveToRefundListByClassID(classItemValue);
            else if (classItemName == "Name")
                return (object)dbAccess.SelectStudentHaveToRefundListByClassName(classItemValue);
            else
                return null;
        }

        //Student Refund Data
        private object SelectStudentRefundRecord(string inputType, string idOrStudentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentRefundRecordByIDOrStudentIDOrClassID(inputType, idOrStudentOrClassID);
        }

        private object SelectStudentRefundRecordList(string inputType, string idOrStudentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentRefundRecordListByRefundIDOrStudentIDOrClassID(inputType, idOrStudentOrClassID);
        }

        private object SelectStudentRefundDetail(int refundID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentRefundRecordDetailByRefundID(refundID);
        }

        //Record
        private object SelectStudentPaymentCountForRecord(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentCountByForRecord(inputType, studentOrClassID);
        }

        private object SelectStudentPaymentCountByClassID(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentCountByClassID(classID);
        }

        private object SelectStudentPaymentForRecord(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentListByForRecord(inputType, studentOrClassID);
        }

        private object SelectStudentPaymentRecordTotal(string inputType)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordTotal(inputType);
        }

        private object SelectStudentPaymentRecordTotalByIDs(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordTotalByIDs(inputType, studentOrClassID);
        }

        private object SelectStudentPaymentRecordTotalByDate(string inputType, string dates)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordTotalByDate(inputType, dates);
        }

        private object SelectStudentPaymentRecordListByIDs(string inputType, string studentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPaymentRecordListByIDs(inputType, studentOrClassID);
        }

        private object SelectStudentPrepaidTotalByStudentID(string student, string dates)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPrepaidTotalByStudentID(student, dates);
        }

        private object SelectStudentPrepaidTotalByStudentIDForStudentPayment(string student, string dates)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPrepaidTotalByStudentIDForStudentPayment(student, dates);
        }

        private object SelectStudentPrepaidHistoryByStudentID(int student)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPrepaidHistoryByStudentID(student.ToString());
        }

        private object SelectStudentPrepaidHistoryGroupDateByStudentID(int student)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPrepaidHistoryGroupDateByStudentID(student.ToString());
        }

        private object SelectStudentPrepaidHistoryByDate(string fromDate, string endDate)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentPrepaidHistoryByDate(fromDate, endDate);
        }

        private object SelectStudentRefundForRecord(string inputType, string idOrStudentOrClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentRefundRecordForRecord(inputType, idOrStudentOrClassID);
        }

        private object SelectStudentRefundStudentListForRecord(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectStudentRefundRecordListByClassID(classID);
        }

        private object SelectDailyExpanseByDates(string startDate, string endDate)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectDailyExpanseByDates(startDate, endDate);
        }

        private object SelectSystemLogs(string[] systemLogsInfo)
        {
            dbAccess = new DataAccess(_SystemType);
            return (object)dbAccess.SelectSystemLogs(systemLogsInfo);
        }

        #endregion

        #region InsertData

        private void InsertCompanyInfo(CompanyInfoDefinition companyInfo)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertCompanyInfo(companyInfo);
        }

        private int InsertStudentInfo(StudentDefinition studentData)
        {
            dbAccess = new DataAccess(_SystemType);

            if (studentData.ID != "" && studentData.ID != "0")
                return dbAccess.InsertStudentWithID(EncodeStudent(studentData));
            else
                return dbAccess.InsertStudentWithoutID(EncodeStudent(studentData));
        }

        private int InsertStudentInfoWithID(StudentDefinition studentData)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.InsertStudentWithID(EncodeStudent(studentData));
        }

        private void InsertClassInfo(ClassDefinition classData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertClassWithID(classData);
        }

        private int InsertStaffInfo(StaffDefinition staffData)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.InsertStaffWithoutID(staffData);
        }

        private void InsertStaffAccountInfo(StaffAccountDefinition staffAccountData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertStaffAccountWithoutID(staffAccountData);
        }

        private void InsertClassTimeInfo(List<ClassTimeDefinition> classTimeData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertClassTimeWithoutID(classTimeData);
        }

        private void InsertClassCategoryInfo(string classCategoryName)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertClassCategoryWithoutID(classCategoryName);
        }

        private void InsertStudentInClassInfo(StudentInClassDefinition studentInClassData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertStudentInClass(studentInClassData);
        }

        private void InsertStudentPrepaidInfo(StudentPrepaidDefinition studentPrepaidData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertStudentPrepaidHistory(studentPrepaidData);
        }

        private void InsertStudentInClassInfoByClass(List<StudentDefinition> studentData, ClassDefinition classData)
        {
            //List<StudentPaymentDefinition> originalStudentData = (List<StudentPaymentDefinition>)FacadeFunctions("select", "studentselectclasses", "ClassID", classData);
            //studentData = from s in studentData
            //                  where originalStudentData.Where(o => o.StudentID != s.ID)
            //                  select s;
            //dbAccess = new DataAccess(_SystemType);
            foreach (var studentSingle in studentData)
            {
                if (!(bool)FacadeFunctions("check", "studentinclass", (object)studentSingle.ID, (object)classData.ID))
                {
                //    //ClassDefinition classData = (ClassDefinition)FacadeFunctions("select", "class", (object)"ID", classID);
                    StudentInClassDefinition studentInClassData = new StudentInClassDefinition(studentSingle.ID, classData.ID, classData.StartDate, classData.EndDate,
                                                                                           classData.ClassPeriod, classData.Price, 0, classData.ApplyFee, classData.MaterialFee);

                    FacadeFunctions("insert", "studentinclass", (object)studentInClassData, null);
                }

                //dbAccess.InsertNewClassAndCheckIsRepeat(studentInClassData);
            }
        }

        private void InsertStudentClassPaymentInfo(ClassPaymentDefinition classPaymentData, bool needReceipt)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertStudentClassPayment(classPaymentData);

            if (needReceipt)
            {
                string[] receiptInfo = new string[6];
                receiptInfo[0] = classPaymentData.StudentID;
                receiptInfo[1] = classPaymentData.StudentName;
                receiptInfo[2] = classPaymentData.ClassID;
                receiptInfo[3] = classPaymentData.ClassName;
                receiptInfo[4] = classPaymentData.Paid.ToString();
                receiptInfo[5] = classPaymentData.StaffName;

                SettleDrawingReceiptPicForPayment(receiptInfo);
            }
        }

        private int InsertStudentClassRefundInfo(ClassRefundDefinition classRefundData)
        {
            dbAccess = new DataAccess(_SystemType);
            return dbAccess.InsertStudentClassRefund(classRefundData);
        }

        private void InsertStudentClassRefundDetailInfo(ClassRefundDetailDefinition classRefundDetailData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.InsertStudentClassRefundDetail(classRefundDetailData);
        }

        private void InsertDailyExpanse(ExpanseDefinition dailyExpanseData)
        {
            dbAccess = new DataAccess(_SystemType);

            dbAccess.InsertDailyExpanse(dailyExpanseData);
        }

        private void InsertSystemLogs(SystemLogsDefinition systemLogData)
        {
            dbAccess = new DataAccess(_SystemType);

            string currentDate = FacadeFunctions("format", "datebydatetime", (object)DateTime.Now, null).ToString();
            currentDate += " " + DateTime.Now.Hour.ToString("00") + ":" + DateTime.Now.Minute.ToString("00") + ":" + DateTime.Now.Second.ToString("00");

            systemLogData.Date = currentDate;

            dbAccess.InsertSystemLog(systemLogData);
        }

        #endregion

        #region UpdateData

        private void UpdateCompanyInfo(CompanyInfoDefinition companyInfo)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateCompanyInfo(companyInfo);
        }

        private void UpdateStudentInfo(StudentDefinition studentData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudent(EncodeStudent(studentData));
        }

        private void UpdateStudentStatus(string studentId, string status)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudentStatus(studentId, status);
        }

        private void UpdateClassInfo(ClassDefinition classData, string oldClassID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateClass(classData, oldClassID);
        }

        private void UpdateClassCategoryInfo(string oldClassCategoryName, string newClassCategoryName)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateClassCategory(oldClassCategoryName, newClassCategoryName);
        }

        private void UpdateStudentClassPaymentDiscount(StudentInClassDefinition studentInClassData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudentClassPaymentDiscount(studentInClassData);
        }

        private void UpdateStudentPrePaid(int studentID, int prePaid)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudentPrePaid(studentID, prePaid);
        }

        private void UpdateStaffInfo(StaffDefinition staffData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStaff(staffData);
        }

        private void UpdateDailyExpanse(ExpanseDefinition dailyExpanseData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateDailyExpanse(dailyExpanseData);
        }

        private void UpdateStaffAccountInfo(StaffAccountDefinition staffAccountData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStaffAccount(staffAccountData);
        }

        //private void UpdateStaffEndDateByID(int staffID)
        //{
        //    dbAccess = new DataAccess(_SystemType);
        //    dbAccess.UpdateStaffEndDateByID(staffID);
        //}

        #endregion

        #region UpdateDeleted

        private void UpdateClassDeleted(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateClassToDelete(classID);
        }

        private void UpdateStaffDeleted(int staffID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStaffToDelete(staffID);
        }

        private void UpdateStudentInClassDeleted(int studentID, string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudentInClassToDelete(studentID, classID);
        }

        private void UpdateStudentInClassRefunded(int studentID, string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudentInClassToRefund(studentID, classID);
        }

        private void UpdateStudentInClassDeleted(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.UpdateStudentInClassToDelete(classID);
        }

        #endregion

        #region DeleteData

        private void DeleteClassCategoryInfo(string classCategoryName)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.DeleteClassCategory(classCategoryName);
        }

        private void DeleteClassTimeInfo(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.DeleteClassTime(classID);
        }

        private void DeleteClassPaymentInfo(RecordDefinition recordData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.DeleteClassPaymentByRecordDefi(recordData);
        }

        private void DeleteStudentInClassInfo(StudentInClassDefinition studentInClassData)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.DeleteStudentInClass(studentInClassData);
        }

        private void DeleteStudentInClassInfoByClass(string classID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.DeleteStudentInClassByClassID(classID);
        }

        private void DeleteDailyExpanse(string dailyExpanseID)
        {
            dbAccess = new DataAccess(_SystemType);
            dbAccess.DeleteDailyExpanse(dailyExpanseID);
        }

        #endregion

        #region Encrypt Data
        private StudentDefinition EncodeStudent(StudentDefinition student)
        {
            return student;
            //return salted.EncordSingleData<StudentDefinition>(student, StudentEncryptColumns());
        }

        private List<StudentDefinition> EncodeStudent(List<StudentDefinition> students)
        {
            return students;
            //return salted.EncordMultipleData<StudentDefinition>(students, StudentEncryptColumns());
        }
        #endregion

        #region Decrypt Data
        private StudentDefinition DecodeStudent(StudentDefinition student)
        {
            return student;
            //return salted.DecordSingleData<StudentDefinition>(student, StudentEncryptColumns());
        }

        private IEnumerable<StudentDefinition> DecodeStudent(List<StudentDefinition> students)
        {
            return students;
            //return salted.DecordMultipleData<StudentDefinition>(students, StudentEncryptColumns());
        }
        #endregion

        #region Encrypt Columns
        private List<string> StudentEncryptColumns()
        {
            var columns = new string[] { "Name", "Address", "DateOfBirth", "EmergencyPerson", "EmergencyPhone",
                                         "FatherName", "InChargePersonCompanyPhone", "InChargePersonHomePhone", 
                                         "InChargePersonMobile", "MotherName", "School", "Sex", "SocialNumber" };
            
            return columns.ToList();
        }
        #endregion

    }
}
