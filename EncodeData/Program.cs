using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EMSSystem.Functions;
using System.Configuration;
using EMSSystem.ClassLibrary;
using EMSSystem.StaticFunctions;

namespace EncryptData
{
    class Program
    {
        static void Main(string[] args)
        {
            var salted = new SaltedHashManager();
            var facade = new FacadeLayer(ConfigurationManager.AppSettings["SystemType"]);
            var studentList = (List<StudentDefinition>)facade.FacadeFunctions("select", "studentall", null, null);
            var encodeList = new List<StudentDefinition>();

            if (studentList != null && studentList.Count > 0)
            {
                StudentDefinition student;
                foreach (var item in studentList)
                {
                    //student = new StudentDefinition
                    //{
                    //    ID = item.ID,
                    //    Name = salted.EncryptData(item.Name),
                    //    Address = salted.EncryptData(item.Address),
                    //    DateOfBirth = salted.EncryptData(item.DateOfBirth),
                    //    EmergencyPerson = salted.EncryptData(item.EmergencyPerson),
                    //    EmergencyPhone = salted.EncryptData(item.EmergencyPhone),
                    //    FatherName = salted.EncryptData(item.FatherName),
                    //    FatherWork = StaticFunction.SetEncodingString(item.FatherWork),
                    //    InChargePerson = item.InChargePerson,
                    //    InChargePersonCompanyPhone = salted.EncryptData(item.InChargePersonCompanyPhone),
                    //    InChargePersonHomePhone = salted.EncryptData(item.InChargePersonHomePhone),
                    //    InChargePersonMobile = salted.EncryptData(item.InChargePersonMobile),
                    //    IsDeleted = item.IsDeleted,
                    //    MotherName = salted.EncryptData(item.MotherName),
                    //    MotherWork = StaticFunction.SetEncodingString(item.MotherWork),
                    //    PostCode = item.PostCode,
                    //    PrePaid = item.PrePaid,
                    //    School = salted.EncryptData(item.School),
                    //    Sex = salted.EncryptData(item.Sex),
                    //    SocialNumber = salted.EncryptData(item.SocialNumber),
                    //    StartDate = item.StartDate,
                    //    StudyYear = StaticFunction.SetEncodingString(item.StudyYear),
                    //    OldBrother = item.OldBrother,
                    //    OldSister = item.OldSister,
                    //    YoungBrother = item.YoungBrother,
                    //    YoungSister = item.YoungSister
                    //};
                    facade.FacadeFunctions("update", "student", (object)item, null);
                }
            }
        }
    }
}
