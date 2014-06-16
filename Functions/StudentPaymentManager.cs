using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMSSystem.ClassLibrary;

namespace EMSSystem.Functions
{
    public class StudentPaymentManager
    {
        public object GetStudentPaymentWithPrepaid(FacadeLayer facade, string selectType, string[] dateSelect, string dates)
        {
            List<RecordDefinition> tempRecordSets = new List<RecordDefinition>();
            object returnObject = null;

            int totalMoney = 0;
            if (selectType == "WithStudentID")
            {
                var prepaidRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("reusefunction", "studentprepaidhistoryforstudentpayment", (object)selectType, (object)dateSelect);
                var paymentRecordSets = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentpaymentrecordtotalbydate", (object)selectType, (object)dates);

                if (paymentRecordSets != null && paymentRecordSets.Count > 0 &&
                    prepaidRecordSets != null && prepaidRecordSets.Count > 0)
                {
                    totalMoney = totalMoney + int.Parse(paymentRecordSets.ElementAt(0).Note2);
                    paymentRecordSets.ElementAt(0).Note2 = totalMoney.ToString();
                    foreach (var item in paymentRecordSets.Where(u => prepaidRecordSets.Select(p => p.Data2ID).Contains(u.Data2ID)))
                    {
                        var prepaid = prepaidRecordSets.First(u => u.Data2ID == item.Data2ID);
                        item.Note1 = (int.Parse(item.Note1) + 1).ToString();
                        item.Note2 = (int.Parse(paymentRecordSets.ElementAt(0).Note2) + prepaid.Money1).ToString();
                    }
                }

                if (prepaidRecordSets != null && prepaidRecordSets.Count > 0)
                {
                    if (paymentRecordSets != null && paymentRecordSets.Count > 0)
                    {
                        var newPrepaidRecord = prepaidRecordSets.Where(u => !paymentRecordSets.Select(p => p.Data2ID).Contains(u.Data2ID));
                        if (newPrepaidRecord != null && newPrepaidRecord.Count() > 0)
                            tempRecordSets = newPrepaidRecord.Select(u => new RecordDefinition
                                                                            {
                                                                                Data2ID = u.Data2ID,
                                                                                Data2Name = u.Data2Name,
                                                                                Note1 = "1",
                                                                                Note2 = u.Money1.ToString()
                                                                            }).ToList();
                        tempRecordSets.AddRange(paymentRecordSets);
                    }
                    else
                        tempRecordSets = prepaidRecordSets;
                }
                else if (paymentRecordSets != null && paymentRecordSets.Count > 0)
                    tempRecordSets = paymentRecordSets;

                returnObject = (object)tempRecordSets;
            }
            else
                returnObject = facade.FacadeFunctions("select", "studentpaymentrecordtotalbydate", (object)selectType, (object)dates);

            return returnObject;
        }

        //public List<RecordDefinition> GetPrepaidRecord(FacadeLayer facade, string studentId, DateTime searchFromDate, DateTime searchEndDate)
        //{
        //    List<RecordDefinition> tempPrepaidList = (List<RecordDefinition>)facade.FacadeFunctions("select", "studentprepaidhistorylistbyid", (object)studentId, null);
        //    if (tempPrepaidList != null && tempPrepaidList.Count > 0)
        //    {
        //        foreach (var item in tempPrepaidList)
        //        {
        //            //Get Left Prepaid Money
        //            RecordDefinition PrepaidData = new RecordDefinition();
        //            PrepaidData.Data1ID = recordSingle.Data2ID;
        //            PrepaidData.Data1Name = recordSingle.Data2Name;
        //            PrepaidData.Data2ID = recordSingle.Data2ID + " " + recordSingle.Data2Name;
        //            PrepaidData.Data2Name = "預繳金額";

        //            int prepaidMoney = 0;

        //            if (lblInvisibleFromDate.Text != "")
        //            {
        //                if (DateTime.Parse(item.Date1) >= DateTime.Parse(lblInvisibleFromDate.Text) &&
        //                    DateTime.Parse(item.Date1) <= DateTime.Parse(lblInvisibleEndDate.Text))
        //                {
        //                    prepaidMoney = prepaidMoney + item.Money1 - item.Money2;
        //                }
        //            }
        //            else
        //                prepaidMoney = prepaidMoney + item.Money1 - item.Money2;

        //            PrepaidData.Money1 = prepaidMoney;
        //            tempPrepaidListSets.Add(PrepaidData);
        //        }

                
        //    }
        //}
    }
}
