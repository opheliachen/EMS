using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EMSSystem.ClassLibrary
{
    public class StudentDefinition
    {
        #region Variable

        private string _ID;
        private string _Name;
        private string _Sex;
        private string _DateOfBirth;
        private string _SocialNumber;
        private string _StartDate;
        private string _School;
        private string _StudyYear;
        private string _FatherName;
        private string _FatherWork;
        private string _MotherName;
        private string _MotherWork;
        private string _OldBrother;
        private string _OldSister;
        private string _YoungBrother;
        private string _YoungSister;
        private string _InChargePerson;
        private string _InChargePersonHomePhone;
        private string _InChargePersonCompanyPhone;
        private string _InChargePersonMobile;
        private string _EmergencyPerson;
        private string _EmergencyPhone;
        private string _Address;
        private string _PostCode;
        private int _PrePaid;
        private char _IsDeleted;

        #endregion

        #region Constructors

        public StudentDefinition()
        {
        }

        public StudentDefinition(string id, string name, string sex, string dateOfBirth, string socialNumber, string startDate, string school, string studyYear,
                                 string fatherName, string fatherWork, string motherName, string motherWork, string oldBrother, string oldSister, string youngBrother,
                                 string youngSister, string inChargePerson, string inChargePersonHomePhone, string inChargePersonCompanyPhone, string inChargePersonMobile, 
                                 string emergencyPerson, string emergencyPhone, string address, string postCode, int prePaid, char isDeleted)
        {
            _ID = id;
            _Name = name;
            _Sex = sex;
            _DateOfBirth = dateOfBirth;
            _SocialNumber = socialNumber;
            _StartDate = startDate;
            _School = school;
            _StudyYear = studyYear;
            _FatherName = fatherName;
            _FatherWork = fatherWork;
            _MotherName = motherName;
            _MotherWork = motherWork;
            _OldBrother = oldBrother;
            _OldSister = oldSister;
            _YoungBrother = youngBrother;
            _YoungSister = youngSister;
            _InChargePerson = inChargePerson;
            _InChargePersonHomePhone = inChargePersonHomePhone;
            _InChargePersonCompanyPhone = inChargePersonCompanyPhone;
            _InChargePersonMobile = inChargePersonMobile;
            _EmergencyPerson = emergencyPerson;
            _EmergencyPhone = emergencyPhone;
            _Address = address;
            _PostCode = postCode;
            _PrePaid = prePaid;
            _IsDeleted = isDeleted;
        }

        #endregion

        #region Properties

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Sex
        {
            get { return _Sex; }
            set { _Sex = value; }
        }

        public string DateOfBirth
        {
            get { return _DateOfBirth; }
            set { _DateOfBirth = value; }
        }

        public string SocialNumber
        {
            get { return _SocialNumber; }
            set { _SocialNumber = value; }
        }

        public string StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public string School
        {
            get { return _School; }
            set { _School = value; }
        }

        public string StudyYear
        {
            get { return _StudyYear; }
            set { _StudyYear = value; }
        }

        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }

        public string FatherWork
        {
            get { return _FatherWork; }
            set { _FatherWork = value; }
        }

        public string MotherName
        {
            get { return _MotherName; }
            set { _MotherName = value; }
        }

        public string MotherWork
        {
            get { return _MotherWork; }
            set { _MotherWork = value; }
        }

        public string OldBrother
        {
            get { return _OldBrother; }
            set { _OldBrother = value; }
        }

        public string OldSister
        {
            get { return _OldSister; }
            set { _OldSister = value; }
        }

        public string YoungBrother
        {
            get { return _YoungBrother; }
            set { _YoungBrother = value; }
        }

        public string YoungSister
        {
            get { return _YoungSister; }
            set { _YoungSister = value; }
        }

        public string InChargePerson
        {
            get { return _InChargePerson; }
            set { _InChargePerson = value; }

        }

        public string InChargePersonHomePhone
        {
            get { return _InChargePersonHomePhone; }
            set { _InChargePersonHomePhone = value; }
        }

        public string InChargePersonCompanyPhone
        {
            get { return _InChargePersonCompanyPhone; }
            set { _InChargePersonCompanyPhone = value; }
        }

        public string InChargePersonMobile
        {
            get { return _InChargePersonMobile; }
            set { _InChargePersonMobile = value; }
        }

        public string EmergencyPerson
        {
            get { return _EmergencyPerson; }
            set { _EmergencyPerson = value; }

        }

        public string EmergencyPhone
        {
            get { return _EmergencyPhone; }
            set { _EmergencyPhone = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }

        }

        public string PostCode
        {
            get { return _PostCode; }
            set { _PostCode = value; }

        }

        public int PrePaid
        {
            get { return _PrePaid; }
            set { _PrePaid = value; }
        }

        public char IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        #endregion
    }
}
