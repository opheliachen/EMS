-- MySQL Administrator dump 1.4
--
-- ------------------------------------------------------
-- Server version	5.1.32-community


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;


--
-- Create schema educatemanagement
--

CREATE DATABASE IF NOT EXISTS educatemanagement;
USE educatemanagement;

--
-- Temporary table structure for view `qrylistneedtopaystudent`
--
DROP TABLE IF EXISTS `qrylistneedtopaystudent`;
DROP VIEW IF EXISTS `qrylistneedtopaystudent`;
CREATE TABLE `qrylistneedtopaystudent` (
  `StudentID` int(10) unsigned,
  `StudentName` varchar(10),
  `ClassID` varchar(10),
  `ClassName` Text,
  `AddDate` datetime,
  `EndDate` datetime,
  `ClassPrice` bigint(11),
  `ClassMaterialFee` bigint(11) unsigned,
  `ClassApplyFee` bigint(11) unsigned,
  `Discount` int(10) unsigned,
  `HavePaid` decimal(33,0)
);

--
-- Definition of table `class`
--

DROP TABLE IF EXISTS `class`;
CREATE TABLE `class` (
  `ID` varchar(10) NOT NULL,
  `Name` Text NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `Price` int(10) unsigned NOT NULL,
  `ClassStatus` varchar(10) NOT NULL,
  `Teacher` Text NOT NULL,
  `MaterialFee` int(10) unsigned NOT NULL,
  `Note` text NOT NULL,
  `IsDeleted` char(1) NOT NULL,
  `ClassCategoryID` int(10) unsigned NOT NULL,
  `Seat` int(10) unsigned NOT NULL,
  `ClassPeriod` int(10) unsigned NOT NULL,
  `ClassDay` varchar(13) NOT NULL,
  `ApplyFee` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_class_ClassCategory` (`ClassCategoryID`),
  CONSTRAINT `FK_class_ClassCategory` FOREIGN KEY (`ClassCategoryID`) REFERENCES `classcategory` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `classcategory`
--

DROP TABLE IF EXISTS `classcategory`;
CREATE TABLE `classcategory` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` varchar(30) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `classcategory`
--

--
-- Definition of table `classpayment`
--

DROP TABLE IF EXISTS `classpayment`;
CREATE TABLE `classpayment` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `StudentID` int(10) unsigned NOT NULL,
  `ClassID` varchar(10) NOT NULL,
  `Paid` int(10) unsigned NOT NULL,
  `PayDate` date NOT NULL,
  `StaffID` int(10) unsigned NOT NULL,
  `PayType` Text NOT NULL,
  `StudentInClassID` varchar(20) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_classpayment_Student` (`StudentID`),
  KEY `FK_classpayment_Class` (`ClassID`),
  KEY `FK_classpayment_Staff` (`StaffID`),
  CONSTRAINT `FK_classpayment_Class` FOREIGN KEY (`ClassID`) REFERENCES `class` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_classpayment_Staff` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_classpayment_Student` FOREIGN KEY (`StudentID`) REFERENCES `student` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;

--
-- Definition of table `classrefunddetail`
--

DROP TABLE IF EXISTS `classrefunddetail`;
CREATE TABLE `classrefunddetail` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ClassID` varchar(10) NOT NULL,
  `HavePaid` int(10) unsigned NOT NULL,
  `RefundID` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_classrefunddetail_RefundID` (`RefundID`),
  KEY `FK_classrefunddetail_ClassID` (`ClassID`),
  CONSTRAINT `FK_classrefunddetail_ClassID` FOREIGN KEY (`ClassID`) REFERENCES `class` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_classrefunddetail_RefundID` FOREIGN KEY (`RefundID`) REFERENCES `classrefunded` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `classrefunddetail`
--

--
-- Definition of table `classrefunded`
--

DROP TABLE IF EXISTS `classrefunded`;
CREATE TABLE `classrefunded` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `SubID` int(10) unsigned NOT NULL,
  `Refund` int(10) unsigned NOT NULL,
  `RefundDate` datetime NOT NULL,
  `Discount` int(10) unsigned NOT NULL,
  `RefundType` Text NOT NULL,
  `Receiver` Text NOT NULL,
  `StaffID` int(10) unsigned NOT NULL,
  `StudentID` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`,`SubID`),
  KEY `FK_classrefunded_StaffID` (`StaffID`),
  KEY `FK_classrefunded_StudentID` (`StudentID`),
  CONSTRAINT `FK_classrefunded_StaffID` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_classrefunded_StudentID` FOREIGN KEY (`StudentID`) REFERENCES `student` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `classrefunded`
--

--
-- Definition of table `classtime`
--

DROP TABLE IF EXISTS `classtime`;
CREATE TABLE `classtime` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ClassID` varchar(10) NOT NULL,
  `Time` varchar(30) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `classtime`
--

--
-- Definition of table `companyinfo`
--

DROP TABLE IF EXISTS `companyinfo`;
CREATE TABLE `companyinfo` (
  `CompanyName` varchar(50) NOT NULL,
  `CompanyManager` varchar(10) NOT NULL,
  `StartTime` datetime NOT NULL,
  PRIMARY KEY (`StartTime`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `companyinfo`
--

--
-- Definition of table `expanse`
--

DROP TABLE IF EXISTS `expanse`;
CREATE TABLE `expanse` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `ExpanseCategoryID` int(10) unsigned NOT NULL,
  `ItemName` Text NOT NULL,
  `InsertStaffID` int(10) unsigned NOT NULL,
  `ShopName` Text NOT NULL,
  `InsertDate` datetime NOT NULL,
  `UnitPrice` DOUBLE unsigned NOT NULL,
  `Quantity` int(10) unsigned NOT NULL,
  `IsDeleted` char(1) NOT NULL,
  `UpdateStaffID` int(10) unsigned NOT NULL DEFAULT '0',
  `UpdateDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_expanse_ExpanseCategory` (`ExpanseCategoryID`),
  KEY `FK_Expanse_Staff` (`InsertStaffID`) USING BTREE,
  CONSTRAINT `FK_expanse_ExpanseCategory` FOREIGN KEY (`ExpanseCategoryID`) REFERENCES `expansecategory` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_Expanse_Staff_InsertStaff` FOREIGN KEY (`InsertStaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;

--
-- Definition of table `expansecategory`
--

DROP TABLE IF EXISTS `expansecategory`;
CREATE TABLE `expansecategory` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` Text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `expansecategory`
--

--
-- Definition of table `prepaidhistory`
--

DROP TABLE IF EXISTS `prepaidhistory`;
CREATE TABLE `prepaidhistory` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `StudentID` int(10) unsigned NOT NULL,
  `Date` varchar(10) NOT NULL,
  `In` int(10) unsigned NOT NULL,
  `Out` int(10) unsigned NOT NULL,
  `Event` text NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_prepaid_StudentID` (`StudentID`),
  CONSTRAINT `FK_prepaid_StudentID` FOREIGN KEY (`StudentID`) REFERENCES `student` (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;

--
-- Definition of table `room`
--

DROP TABLE IF EXISTS `room`;
CREATE TABLE `room` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Classroom` varchar(30) NOT NULL,
  `SeatSpace` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `room`
--

--
-- Definition of table `sidefunctions`
--

DROP TABLE IF EXISTS `sidefunctions`;
CREATE TABLE `sidefunctions` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `FunctionName` Text NOT NULL,
  `MainFunctionID` int(10) unsigned NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_sidefunctions_MainFunctionID` (`MainFunctionID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Dumping data for table `sidefunctions`
--


DROP TABLE IF EXISTS `staffrole`;
CREATE TABLE  `staffrole` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Role` Text NOT NULL,
  `MatchName` Text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;


--
-- Definition of table `staff`
--

DROP TABLE IF EXISTS `staff`;
CREATE TABLE `staff` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` Text NOT NULL,
  `EnglishName` Text NOT NULL,
  `StartDate` varchar(10) NOT NULL,
  `EndDate` varchar(10) NOT NULL,
  `LaborCover` int(10) unsigned NOT NULL,
  `HealthCover` int(10) unsigned NOT NULL,
  `GroupCover` int(10) unsigned NOT NULL,
  `Note` text NOT NULL,
  `StaffTypeID` int(10) unsigned NOT NULL,
  `IsDeleted` char(1) NOT NULL,
  `StaffRole` int(10) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_staff_StaffType` (`StaffTypeID`),
  CONSTRAINT `FK_staff_StaffType` FOREIGN KEY (`StaffTypeID`) REFERENCES `stafftype` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `staff`
--

--
-- Definition of table `staffaccount`
--

DROP TABLE IF EXISTS `staffaccount`;
CREATE TABLE `staffaccount` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `StaffID` int(10) unsigned NOT NULL,
  `Password` varchar(15) NOT NULL,
  `MasterKey` varchar(15) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_StaffAccount_Staff` (`StaffID`),
  CONSTRAINT `FK_StaffAccount_Staff` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `staffaccount`
--

--
-- Definition of table `stafftype`
--

DROP TABLE IF EXISTS `stafftype`;
CREATE TABLE `stafftype` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` Text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `stafftype`
--

--
-- Definition of table `student`
--

DROP TABLE IF EXISTS `student`;
CREATE TABLE `student` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Name` text NOT NULL,
  `DateOfBirth` varchar(10) NOT NULL,
  `InChargePerson` Text NOT NULL,
  `InChargePersonHomePhone` Text NOT NULL,
  `School` Text NOT NULL,
  `Address` text NOT NULL,
  `IsDeleted` char(1) NOT NULL,
  `StudyYear` Text NOT NULL,
  `FatherName` text NOT NULL,
  `FatherWork` Text NOT NULL,
  `MotherName` text NOT NULL,
  `MotherWork` Text NOT NULL,
  `EmergencyPerson` Text NOT NULL,
  `EmergencyPhone` Text NOT NULL,
  `PostCode` varchar(5) NOT NULL,
  `PrePaid` int(10) unsigned NOT NULL,
  `Sex` varchar(15) NOT NULL,
  `InChargePersonMobile` Text NOT NULL,
  `StartDate` varchar(10) NOT NULL,
  `Sibling` Text NOT NULL,
  `SocialNumber` varchar(10) NOT NULL,
  `CompanyPhone` Text NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB AUTO_INCREMENT=5018 DEFAULT CHARSET=utf8;

--
-- Definition of table `studentinclass`
--

DROP TABLE IF EXISTS `studentinclass`;
CREATE TABLE  `studentinclass` (
  `StudentID` int(10) unsigned NOT NULL,
  `ClassID` varchar(10) NOT NULL,
  `Discount` int(10) unsigned NOT NULL,
  `IsDeleted` char(1) NOT NULL,
  `ID` varchar(20) NOT NULL,
  `IsRefund` char(1) NOT NULL,
  `AddDate` datetime NOT NULL,
  `WithdrawDate` datetime NOT NULL,
  `EndDate` datetime NOT NULL,
  `ClassPeriod` int(10) unsigned NOT NULL,
  `ApplyFee` int(10) unsigned NOT NULL,
  `MaterialFee` int(10) unsigned NOT NULL,
  `ClassPrice` int(10) unsigned NOT NULL,
  PRIMARY KEY (`StudentID`,`ID`,`ClassID`) USING BTREE,
  KEY `FK_studentinclass_Class` (`ClassID`),
  CONSTRAINT `FK_studentinclass_Class` FOREIGN KEY (`ClassID`) REFERENCES `class` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `FK_studentinclass_Student` FOREIGN KEY (`StudentID`) REFERENCES `student` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Definition of table `systemlogs`
--

DROP TABLE IF EXISTS `systemlogs`;
CREATE TABLE `systemlogs` (
  `ID` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `Date` datetime NOT NULL,
  `StaffID` int(10) unsigned NOT NULL,
  `Action` text NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `FK_Logs_Staff` (`StaffID`),
  CONSTRAINT `FK_Logs_Staff` FOREIGN KEY (`StaffID`) REFERENCES `staff` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=144 DEFAULT CHARSET=utf8;

--
-- Dumping data for table `systemlogs`
--

--
-- Definition of function `qryInsertClassRefundedWithoutID`
--

DROP FUNCTION IF EXISTS `qryInsertClassRefundedWithoutID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryInsertClassRefundedWithoutID`(classRefund_SubID int,
                                                                             classRefund_StudentID int,
                                                                             classRefund_StaffEngName Text,
                                                                             classRefund_Discount int,
                                                                             classRefund_Refunded int,
                                                                             classRefund_RefundDate Date,
                                                                             classRefund_Receiver Text,
                                                                             classRefund_RefundType Text) RETURNS int(11)
BEGIN
  Declare newID int;
  Declare staffID int;

  Set newID = (Select qryListAvailableClassRefundID());
  Set staffID = (Select qryListStaffIDByEnglishName(classRefund_StaffEngName));

  Insert into ClassRefunded values (newID, classRefund_SubID, classRefund_Refunded, classRefund_RefundDate,
                                    classRefund_Discount, classRefund_RefundType, classRefund_Receiver,
                                    staffID, classRefund_StudentID);

  Return newID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryInsertStaff`
--

DROP FUNCTION IF EXISTS `qryInsertStaff`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryInsertStaff`(staff_StaffRole int,
                                                            staff_TypeName Text,
                                                            staff_Name Text,
                                                            staff_EngName Text,
                                                            staff_StartDate varchar(10),
                                                            staff_EndDate varchar(10),
                                                            staff_LaborCover int,
                                                            staff_HealthCover int,
                                                            staff_GroupCover int,
                                                            staff_Note Text) RETURNS int(11)
BEGIN
  Declare newID int;
  Declare staff_TypeID int;

  Set newID = (Select qryListAvailableStaffID());
  Set staff_TypeID = (Select qryListStaffTypeIDByName(staff_TypeName));

  Insert into Staff values (newID, staff_Name, staff_EngName, staff_StartDate,
                            staff_EndDate, staff_LaborCover, staff_HealthCover, staff_GroupCover,
                            staff_Note, staff_TypeID, '0', staff_StaffRole);
  Return newID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryInsertStudent`
--

DROP FUNCTION IF EXISTS `qryInsertStudent`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryInsertStudent`(student_Name Text,
                                                              student_Sex varchar(15),
                                                              student_DateOfBirth varchar(10),
                                                              student_SocialNumber varchar(10),
                                                              student_StartDate varchar(10),
                                                              student_School Text,
                                                              student_StudyYear Text,
                                                              student_FatherName Text,
                                                              student_FatherWork Text,
                                                              student_MotherName Text,
                                                              student_MotherWork Text,
                                                              student_Sibling Text,
                                                              student_InChargePerson Text,
                                                              student_InChargePersonHomePhone Text,
                                                              student_InChargePersonCompanyPhone Text,
                                                              student_InChargePersonMobile Text,
                                                              student_EmergencyPerson Text,
                                                              student_EmergencyPhone Text,
                                                              student_Address Text,
                                                              student_PostCode varchar(10),
                                                              student_PrePaid int) RETURNS int(11)
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailableStudentID());
  Insert into Student values (newID, student_Name, student_DateOfBirth, student_InChargePerson,
                              student_InChargePersonHomePhone, student_School, student_Address, '0',
                              student_StudyYear, student_FatherName, student_FatherWork, student_MotherName,
                              student_MotherWork, student_EmergencyPerson, student_EmergencyPhone, student_PostCode,
                              student_PrePaid, student_Sex, student_InChargePersonMobile, student_StartDate,
                              student_Sibling, student_SocialNumber, student_InChargePersonCompanyPhone);

  Return newID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryInsertStudentWithID`
--

DROP FUNCTION IF EXISTS `qryInsertStudentWithID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryInsertStudentWithID`(student_ID int,
                                                                    student_Name Text,
                                                                    student_Sex varchar(15),
                                                                    student_DateOfBirth varchar(10),
                                                                    student_SocialNumber varchar(10),
                                                                    student_StartDate varchar(10),
                                                                    student_School Text,
                                                                    student_StudyYear Text,
                                                                    student_FatherName varchar(10),
                                                                    student_FatherWork Text,
                                                                    student_MotherName varchar(10),
                                                                    student_MotherWork Text,
                                                                    student_Sibling Text,
                                                                    student_InChargePerson Text,
                                                                    student_InChargePersonHomePhone Text,
                                                                    student_InChargePersonCompanyPhone Text,
                                                                    student_InChargePersonMobile Text,
                                                                    student_EmergencyPerson Text,
                                                                    student_EmergencyPhone Text,
                                                                    student_Address Text,
                                                                    student_PostCode varchar(10),
                                                                    student_PrePaid int) RETURNS int(11)
BEGIN
  Insert into Student values (student_ID, student_Name, student_DateOfBirth, student_InChargePerson,
                              student_InChargePersonHomePhone, student_School, student_Address, '0',
                              student_StudyYear, student_FatherName, student_FatherWork, student_MotherName,
                              student_MotherWork, student_EmergencyPerson, student_EmergencyPhone, student_PostCode,
                              student_PrePaid, student_Sex, student_InChargePersonMobile, student_StartDate,
                              student_Sibling, student_SocialNumber, student_InChargePersonCompanyPhone);

  Return student_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableClassCategoryID`
--

DROP FUNCTION IF EXISTS `qryListAvailableClassCategoryID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableClassCategoryID`() RETURNS int(11)
BEGIN
  Declare newClassCategoryID int;
  Declare classCategoryCount int;
  Set newClassCategoryID = 0;

  Repeat
    Set newClassCategoryID = newClassCategoryID + 1;
    Set classCategoryCount = (Select Count(*) As ClassCategoryCount From ClassCategory Where ID = newClassCategoryID);
    Until classCategoryCount = 0
  End Repeat;

  Return newClassCategoryID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableClassPaymentID`
--

DROP FUNCTION IF EXISTS `qryListAvailableClassPaymentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableClassPaymentID`() RETURNS int(11)
BEGIN
  Declare newClassPaymentID int;
  Declare classPaymentCount int;
  Set newClassPaymentID = 0;

  Repeat
    Set newClassPaymentID = newClassPaymentID + 1;
    Set classPaymentCount = (Select Count(*) As ClassPaymentCount From ClassPayment Where ID = newClassPaymentID);
    Until classPaymentCount = 0
  End Repeat;

  Return newClassPaymentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableClassRefundDetailID`
--

DROP FUNCTION IF EXISTS `qryListAvailableClassRefundDetailID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableClassRefundDetailID`() RETURNS int(11)
BEGIN
  Declare newClassRefundDetailID int;
  Declare classRefundDetailCount int;
  Set newClassRefundDetailID = 0;

  Repeat
    Set newClassRefundDetailID = newClassRefundDetailID + 1;
    Set classRefundDetailCount = (Select Count(*) As ClassRefundCount From ClassRefundDetail Where ID = newClassRefundDetailID);
    Until classRefundDetailCount = 0
  End Repeat;

  Return newClassRefundDetailID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableClassRefundID`
--

DROP FUNCTION IF EXISTS `qryListAvailableClassRefundID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableClassRefundID`() RETURNS int(11)
BEGIN
  Declare newClassRefundID int;
  Declare classRefundCount int;
  Set newClassRefundID = 0;

  Repeat
    Set newClassRefundID = newClassRefundID + 1;
    Set classRefundCount = (Select Count(*) As ClassRefundCount From ClassRefunded Where ID = newClassRefundID);
    Until classRefundCount = 0
  End Repeat;

  Return newClassRefundID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableClassTimeID`
--

DROP FUNCTION IF EXISTS `qryListAvailableClassTimeID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableClassTimeID`() RETURNS int(11)
BEGIN
  Declare newClassTimeID int;
  Declare classTimeCount int;
  Set newClassTimeID = 0;

  Repeat
    Set newClassTimeID = newClassTimeID + 1;
    Set classTimeCount = (Select Count(*) As ClassTimeCount From ClassTime Where ID = newClassTimeID);
    Until classTimeCount = 0
  End Repeat;

  Return newClassTimeID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableExpanseCategoryID`
--

DROP FUNCTION IF EXISTS `qryListAvailableExpanseCategoryID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableExpanseCategoryID`() RETURNS int(11)
BEGIN
  Declare newExpanseCategoryID int;
  Declare expanseCategoryCount int;
  Set newExpanseCategoryID = 0;

  Repeat
    Set newExpanseCategoryID = newExpanseCategoryID + 1;
    Set expanseCategoryCount = (Select Count(*) As ExpanseCategoryCount From ExpanseCategory Where ID = newExpanseCategoryID);
    Until expanseCategoryCount = 0
  End Repeat;

  Return newExpanseCategoryID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableExpanseID`
--

DROP FUNCTION IF EXISTS `qryListAvailableExpanseID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableExpanseID`() RETURNS int(11)
BEGIN
  Declare newExpanseID int;
  Declare expanseCount int;
  Set newExpanseID = 0;

  Repeat
    Set newExpanseID = newExpanseID + 1;
    Set expanseCount = (Select Count(*) As ExpanseCount From Expanse Where ID = newExpanseID);
    Until expanseCount = 0
  End Repeat;

  Return newExpanseID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailablePrepaidHistoryID`
--

DROP FUNCTION IF EXISTS `qryListAvailablePrepaidHistoryID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailablePrepaidHistoryID`() RETURNS int(11)
BEGIN
  Declare newPrepaidID int;
  Declare prepaidCount int;
  Set newPrepaidID = 0;

  Repeat
    Set newPrepaidID = newPrepaidID + 1;
    Set prepaidCount = (Select Count(*) As PrepaidCount From PrepaidHistory Where ID = newPrepaidID);
    Until prepaidCount = 0
  End Repeat;

  Return newPrepaidID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableRoomID`
--

DROP FUNCTION IF EXISTS `qryListAvailableRoomID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableRoomID`() RETURNS int(11)
BEGIN
  Declare newRoomID int;
  Declare roomCount int;
  Set newRoomID = 0;

  Repeat
    Set newRoomID = newRoomID + 1;
    Set roomCount = (Select Count(*) As RoomCount From Room Where ID = newRoomID);
    Until roomCount = 0
  End Repeat;

  Return newRoomID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableStaffAccountID`
--

DROP FUNCTION IF EXISTS `qryListAvailableStaffAccountID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableStaffAccountID`() RETURNS int(11)
BEGIN
  Declare newStaffAccountID int;
  Declare staffAccountCount int;
  Set newStaffAccountID = 0;

  Repeat
    Set newStaffAccountID = newStaffAccountID + 1;
    Set staffAccountCount = (Select Count(*) As StaffAccountCount From StaffAccount Where ID = newStaffAccountID);
    Until staffAccountCount = 0
  End Repeat;

  Return newStaffAccountID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableStaffID`
--

DROP FUNCTION IF EXISTS `qryListAvailableStaffID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableStaffID`() RETURNS int(11)
BEGIN
  Declare newStaffID int;
  Declare staffCount int;
  Set newStaffID = 0;

  Repeat
    Set newStaffID = newStaffID + 1;
    Set staffCount = (Select Count(*) As StaffCount From Staff Where ID = newStaffID);
    Until staffCount = 0
  End Repeat;

  Return newStaffID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableStaffTypeID`
--

DROP FUNCTION IF EXISTS `qryListAvailableStaffTypeID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableStaffTypeID`() RETURNS int(11)
BEGIN
  Declare newStaffTypeID int;
  Declare staffTypeCount int;
  Set newStaffTypeID = 0;

  Repeat
    Set newStaffTypeID = newStaffTypeID + 1;
    Set staffTypeCount = (Select Count(*) As StaffTypeCount From StaffType Where ID = newStaffTypeID);
    Until staffTypeCount = 0
  End Repeat;

  Return newStaffTypeID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableStudentID`
--

DROP FUNCTION IF EXISTS `qryListAvailableStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableStudentID`() RETURNS int(11)
BEGIN
  Declare newStudentID int;
  Declare studentCount int;
  Set newStudentID = 0;

  Repeat
    Set newStudentID = newStudentID + 1;
    Set studentCount = (Select Count(*) As StudentCount From Student Where ID = newStudentID);
    Until studentCount = 0
  End Repeat;

  Return newStudentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableStudentInClassID`
--

DROP FUNCTION IF EXISTS `qryListAvailableStudentInClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableStudentInClassID`(newStudentInClassID varchar(20)) RETURNS varchar(20)
BEGIN
  Declare studentInClassCount int;
  Declare newID bigint;
  Set newID = Cast(newStudentInClassID AS UNSIGNED);
  Repeat
    Set newID = newID + 1;
    Set studentInClassCount = (Select Count(*) As StudentInClassCount From StudentInClass Where ID = newID);
    Until studentInClassCount = 0
  End Repeat;

  Return newID + '';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListAvailableSystemLogsID`
--

DROP FUNCTION IF EXISTS `qryListAvailableSystemLogsID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListAvailableSystemLogsID`() RETURNS int(11)
BEGIN
  Declare newSystemLogsID int;
  Declare systemlogsCount int;
  Set newSystemLogsID = 0;

  Repeat
    Set newSystemLogsID = newSystemLogsID + 1;
    Set systemlogsCount = (Select Count(*) As SystemLogsCount From SystemLogs Where ID = newSystemLogsID);
    Until systemlogsCount = 0
  End Repeat;

  Return newSystemLogsID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListClassCategoryIDByName`
--

DROP FUNCTION IF EXISTS `qryListClassCategoryIDByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListClassCategoryIDByName`(classCate_Name varchar(30)) RETURNS int(11)
BEGIN
  Declare classCate_ID int;

  Set classCate_ID = (SELECT ID
                      FROM   ClassCategory
                      WHERE  Name = classCate_Name);

  Return classCate_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListClassCurrentStudentNumber`
--

DROP FUNCTION IF EXISTS `qryListClassCurrentStudentNumber`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListClassCurrentStudentNumber`(class_ID varchar(10)) RETURNS int(11)
BEGIN
  Declare studentCount int;

  Set studentCount = (SELECT Count(*)
                      FROM   StudentInClass
                      WHERE  ClassID = class_ID
                        And  IsDeleted = '0');

  Return studentCount;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListExpanseCategoryIDByName`
--

DROP FUNCTION IF EXISTS `qryListExpanseCategoryIDByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListExpanseCategoryIDByName`(expanseCate_Name Text) RETURNS int(11)
BEGIN
  Declare expanseCategoryID int;
  Set expanseCategoryID = 0;

  Set expanseCategoryID = (SELECT ID
                           FROM ExpanseCategory
                           WHERE Name Like (expanseCate_Name));

  Return expanseCategoryID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListNeedToPayMoneyByIDs`
--

DROP FUNCTION IF EXISTS `qryListNeedToPayMoneyByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListNeedToPayMoneyByIDs`(student_ID int,
                                                                        class_ID varchar(10)) RETURNS int(11)
BEGIN
  Declare studentClassPrice int;
  Set studentClassPrice = 0;

  Set studentClassPrice = (Select ClassPrice
                            From  StudentInClass sc
                           Where  StudentID = student_ID
                             And  ClassID = class_ID
                             And  IsDeleted = '0');

  Return studentClassPrice;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListNeedToPayMoneyByStudentID`
--

DROP FUNCTION IF EXISTS `qryListNeedToPayMoneyByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListNeedToPayMoneyByStudentID`(student_ID int) RETURNS int(11)
BEGIN
  Declare studentClassPrice int;
  Set studentClassPrice = 0;

  Set studentClassPrice = (Select ClassPrice
                            From  StudentInClass sc
                           Where  StudentID = student_ID);

  Set studentClassPrice = studentClassPrice + studentClassPrice;

  Return studentClassPrice;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListStaffIDByEnglishName`
--

DROP FUNCTION IF EXISTS `qryListStaffIDByEnglishName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListStaffIDByEnglishName`(staff_EngName Text) RETURNS int(11)
BEGIN
  Declare staff_ID int;

  Set staff_ID = (SELECT ID
                  FROM Staff
                  WHERE EnglishName = (staff_EngName));

  Return staff_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListStaffTypeIDByName`
--

DROP FUNCTION IF EXISTS `qryListStaffTypeIDByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListStaffTypeIDByName`(staffType_Name Text) RETURNS int(11)
BEGIN
  Declare staffType_ID int;

  Set staffType_ID = (SELECT ID
                      FROM StaffType
                      WHERE Name Like (staffType_Name));

  Return staffType_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of function `qryListStudentClassHavePaidPayment`
--

DROP FUNCTION IF EXISTS `qryListStudentClassHavePaidPayment`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` FUNCTION `qryListStudentClassHavePaidPayment`(student_ID int,
                                                                                class_ID varchar(10)) RETURNS int(11)
BEGIN
  Declare havePaid int;
  Declare classPrice int;
  Declare discount int;
  Declare moneyNeedToPay int;

  Set havePaid = (Select Sum(Paid) From ClassPayment Where StudentID = student_ID And ClassID = class_ID);
  Set classPrice = (Select Price From Class Where ID = class_ID);
  Set discount = (Select Discount From StudentInClass Where StudentID = student_ID And ClassID = class_ID);
  Set moneyNeedToPay = classPrice - discount - havePaid;

  Return havePaid;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryCountClassByClassCategoryID`
--

DROP PROCEDURE IF EXISTS `qryCountClassByClassCategoryID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryCountClassByClassCategoryID`(class_ClassCategoryID int)
BEGIN
  SELECT Count(*)
  FROM Class
  WHERE ClassCategoryID = (class_ClassCategoryID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryCountClassByClassCategoryName`
--

DROP PROCEDURE IF EXISTS `qryCountClassByClassCategoryName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryCountClassByClassCategoryName`(class_ClassCategoryName varchar(30))
BEGIN
  Declare class_ClassCategoryID int;
  Set class_ClassCategoryID = (Select qryListClassCategoryIDByName(class_ClassCategoryName));

  SELECT Count(*)
  FROM Class
  WHERE ClassCategoryID = (class_ClassCategoryID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryCountStudentInClass`
--

DROP PROCEDURE IF EXISTS `qryCountStudentInClass`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryCountStudentInClass`()
BEGIN
  SELECT ClassID,
         (Select Name From Class Where ID = sc.ClassID) As ClassName,
         (Select Count(*) From StudentInClass Where IsDeleted = '0' And ClassID = sc.ClassID) As AddCount,
         (Select Count(*) From StudentInClass Where IsDeleted = '1' And ClassID = sc.ClassID) As WithdrawCount
  FROM StudentInClass sc
  Group By ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryCountStudentInClassByDate`
--

DROP PROCEDURE IF EXISTS `qryCountStudentInClassByDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryCountStudentInClassByDate`(fromDate DateTime,
                                                                           endDate  DateTime)
BEGIN
  SELECT ClassID,
         (Select Name From Class Where ID = sc.ClassID) As ClassName,
         (Select Count(*) From StudentInClass Where IsDeleted = '0'
                                                And ClassID = sc.ClassID
                                                And AddDate >= fromDate
                                                And AddDate <= endDate) As AddCount,
         (Select Count(*) From StudentInClass Where IsDeleted = '1'
                                                And ClassID = sc.ClassID
                                                And WithdrawDate >= fromDate
                                                And WithdrawDate <= endDate) As WithdrawCount
  FROM   StudentInClass sc
  Group  By ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryCountStudentNeedToPayByClassID`
--

DROP PROCEDURE IF EXISTS `qryCountStudentNeedToPayByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryCountStudentNeedToPayByClassID`(class_ID varchar(10))
BEGIN
  Select ClassID,
         (Select Name From Class Where ID = sc.ClassID) As ClassName,
         Count(StudentID) As StudentCount
  From   StudentInClass sc
  Where  ClassID = class_ID
    And  Discount < (Select (Price + MaterialFee) - IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = sc.StudentID And ClassID = sc.ClassID), 0)
                     From Class
                     Where ID = sc.ClassID)
  Group  By ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryCountStudentNeedToPayByStudentID`
--

DROP PROCEDURE IF EXISTS `qryCountStudentNeedToPayByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryCountStudentNeedToPayByStudentID`(student_ID int)
BEGIN
  Select StudentID,
         (Select Name From Student Where ID = sc.StudentID) As StudentName,
         Count(ClassID) As ClassCount
  From   StudentInClass sc
  Where  StudentID = student_ID
    And  Discount < (Select (Price + MaterialFee) - IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = student_ID And ClassID = sc.ClassID), 0)
                     From Class
                     Where ID = sc.ClassID)
  Group  By StudentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassByID`(class_ID int)
BEGIN
  Delete From Class Where ID = class_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassCategoryByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassCategoryByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassCategoryByID`(classCate_ID int)
BEGIN
  Delete From ClassCategory Where ID = classCate_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassPaymentByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassPaymentByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassPaymentByID`(classPayment_ID int)
BEGIN
  Delete From ClassPayment Where ID = classPayment_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassPaymentByIDs`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassPaymentByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassPaymentByIDs`(classPayment_StudentID int,
                                                                         classPayment_ClassID varchar(10))
BEGIN
  Delete From ClassPayment Where StudentID = classPayment_StudentID
                             And ClassID = classPayment_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassRefundByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassRefundByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassRefundByID`(classPayment_StudentID int,
                                                                  classPayment_ClassID varchar(10))
BEGIN
  Delete From ClassRefund Where StudentID = classPayment_StudentID
                           And ClassID = classPayment_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassTimeByClassID`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassTimeByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassTimeByClassID`(classTime_ClassID varchar(10))
BEGIN
  Delete From ClassTime Where ClassID = classTime_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteClassTimeByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteClassTimeByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteClassTimeByID`(classTime_ID int)
BEGIN
  Delete From ClassTime Where ID = classTime_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteExpanseByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteExpanseByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteExpanseByID`(expanse_ID int)
BEGIN
  Delete From Expanse Where ID = expanse_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteExpanseCategoryByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteExpanseCategoryByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteExpanseCategoryByID`(expanseCate_ID int)
BEGIN
  Delete From ExpanseCategory Where ID = expanseCate_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeletePrepaidHistoryByID`
--

DROP PROCEDURE IF EXISTS `qryDeletePrepaidHistoryByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeletePrepaidHistoryByID`(prepaid_ID int)
BEGIN
  Delete From PrepaidHistory Where ID = prepaid_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteRoomByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteRoomByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteRoomByID`(room_ID int)
BEGIN
  Delete From Room Where ID = room_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteStaffAccountByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteStaffAccountByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteStaffAccountByID`(staffAccount_ID int)
BEGIN
  Delete From StaffAccount Where ID = staffAccount_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteStaffByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteStaffByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteStaffByID`(staff_ID int)
BEGIN
  Delete From Staff Where ID = staff_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteStaffTypeByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteStaffTypeByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteStaffTypeByID`(staffType_ID int)
BEGIN
  Delete From StaffType Where ID = staffType_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteStudentByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteStudentByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteStudentByID`(student_ID int)
BEGIN
  Delete From Student Where ID = student_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteStudentInClassByClassID`
--

DROP PROCEDURE IF EXISTS `qryDeleteStudentInClassByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteStudentInClassByClassID`(studentInClass_ClassID varchar(10))
BEGIN
  Delete From StudentInClass
  Where ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteStudentInClassByIDs`
--

DROP PROCEDURE IF EXISTS `qryDeleteStudentInClassByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteStudentInClassByIDs`(studentInClass_StudentID int,
                                                                           studentInClass_ClassID varchar(10))
BEGIN
  Delete From StudentInClass
  Where StudentID = studentInClass_StudentID
    And ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryDeleteSystemLogsByID`
--

DROP PROCEDURE IF EXISTS `qryDeleteSystemLogsByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryDeleteSystemLogsByID`(systemLogs_ID int)
BEGIN
  Delete From SystemLogs Where ID = systemLogs_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertClass`
--

DROP PROCEDURE IF EXISTS `qryInsertClass`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertClass`(class_ID varchar(10),
                                                             class_ClassCategoryName varchar(30),
                                                             class_Name Text,
                                                             class_StartDate DateTime,
                                                             class_EndDate DateTime,
                                                             class_ClassPeriod int,
                                                             class_ClassDay varchar(13),
                                                             class_Seat int,
                                                             class_Price int,
                                                             class_Status varchar(10),
                                                             class_Teacher Text,
                                                             class_MaterialFee int,
                                                             class_ApplyFee int,
                                                             class_Note Text)
BEGIN
  Declare class_ClassCategoryID int;
  Set class_ClassCategoryID = (Select qryListClassCategoryIDByName(class_ClassCategoryName));

  Insert into Class values (class_ID, class_Name, class_StartDate,
                            class_EndDate, class_Price, class_Status, class_Teacher,
                            class_MaterialFee, class_Note, '0', 1, class_Seat,
                            class_ClassPeriod, class_ClassDay, class_ApplyFee);

END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertClassCategory`
--

DROP PROCEDURE IF EXISTS `qryInsertClassCategory`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertClassCategory`(classCate_Name varchar(30))
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailableClassCategoryID());
  Insert into ClassCategory values (newID, classCate_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertClassPayment`
--

DROP PROCEDURE IF EXISTS `qryInsertClassPayment`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertClassPayment`(classPayment_StudentID int,
                                                                    classPayment_ClassID varchar(10),
                                                                    classPayment_StudentInClassID varchar(20),
                                                                    classPayment_StaffEngName Text,
                                                                    classPayment_Paid int,
                                                                    classPayment_PayDate Date,
                                                                    classPayment_PaymentType Text)
BEGIN
  Declare newID int;
  Declare staffID int;

  Set newID = (Select qryListAvailableClassPaymentID());
  Set staffID = (Select qryListStaffIDByEnglishName(classPayment_StaffEngName));

  Insert into ClassPayment values (newID, classPayment_StudentID, classPayment_ClassID, classPayment_Paid,
                                   classPayment_PayDate, staffID, classPayment_PaymentType,
                                   classPayment_StudentInClassID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertClassRefundDetail`
--

DROP PROCEDURE IF EXISTS `qryInsertClassRefundDetail`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertClassRefundDetail`(classRefund_ClassRefundedID int,
                                                                         classRefund_ClassID varchar(10),
                                                                         classRefund_HavePaid int)
BEGIN
  Declare newID int;
  Declare staffID int;

  Set newID = (Select qryListAvailableClassRefundDetailID());

  Insert into ClassRefundDetail values (newID, classRefund_ClassID,
                                        classRefund_HavePaid, classRefund_ClassRefundedID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertClassRefunded`
--

DROP PROCEDURE IF EXISTS `qryInsertClassRefunded`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertClassRefunded`(classRefund_ID int,
                                                                     classRefund_SubID int,
                                                                     classRefund_StudentID int,
                                                                     classRefund_StaffEngName Text,
                                                                     classRefund_Discount int,
                                                                     classRefund_Refunded int,
                                                                     classRefund_RefundDate Date,
                                                                     classRefund_Receiver Text,
                                                                     classRefund_RefundType Text)
BEGIN
  Declare staffID int;

  Set staffID = (Select qryListStaffIDByEnglishName(classRefund_StaffEngName));

  Insert into ClassRefunded values (classRefund_ID, classRefund_SubID, classRefund_Refunded,
                                    classRefund_RefundDate, classRefund_Discount, classRefund_RefundType,
                                    classRefund_Receiver, staffID, classRefund_StudentID);

END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertClassTime`
--

DROP PROCEDURE IF EXISTS `qryInsertClassTime`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertClassTime`(classTime_ClassID varchar(10),
                                                                 classTime_Time varchar(30))
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailableClassTimeID());

  Insert into ClassTime values (newID, classTime_ClassID, classTime_Time);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertCompanyInfo`
--

DROP PROCEDURE IF EXISTS `qryInsertCompanyInfo`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertCompanyInfo`(company_Name Varchar(50),
                                                                   company_Manager Varchar(10))
BEGIN
  Insert into CompanyInfo values (company_Name, company_Manager, Now());

END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertCompanyStartTime`
--

DROP PROCEDURE IF EXISTS `qryInsertCompanyStartTime`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertCompanyStartTime`()
BEGIN
  Insert into CompanyInfo(StartTime) values (Now());

END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertExpanse`
--

DROP PROCEDURE IF EXISTS `qryInsertExpanse`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertExpanse`(expanse_ExpanseCategoryName Text,
                                                               expanse_StaffEngName Text,
                                                               expanse_ItemName Text,
                                                               expanse_ShopName Text,
                                                               expanse_UnitPrice Double,
                                                               expanse_Quantity int)
BEGIN
  Declare newID int;
  Declare expanse_expanseCateID int;
  Declare expanse_staffID int;

  Set newID = (Select qryListAvailableExpanseID());
  Set expanse_expanseCateID = (Select qryListExpanseCategoryIDByName(expanse_ExpanseCategoryName));
  Set expanse_staffID = (Select qryListStaffIDByEnglishName(expanse_StaffEngName));

  Insert into Expanse values (newID, expanse_expanseCateID, expanse_ItemName, expanse_staffID,
                              expanse_ShopName, Now(), expanse_UnitPrice, expanse_Quantity,
                              '0', 0, Now());
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertExpanseWithInsertDate`
--

DROP PROCEDURE IF EXISTS `qryInsertExpanseWithInsertDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertExpanseWithInsertDate`(expanse_ExpanseCategoryName varchar(45),
																			 expanse_StaffEngName varchar(20),
																			 expanse_ItemName varchar(45),
																			 expanse_ShopName varchar(45),
																			 expanse_UnitPrice int,
																			 expanse_Quantity int,
																			 expanse_InsertDate DateTime)
BEGIN
  Declare newID int;
  Declare expanse_expanseCateID int;
  Declare expanse_staffID int;

  Set newID = (Select qryListAvailableExpanseID());
  Set expanse_expanseCateID = (Select qryListExpanseCategoryIDByName(expanse_ExpanseCategoryName));
  Set expanse_staffID = (Select qryListStaffIDByEnglishName(expanse_StaffEngName));

  Insert into Expanse values (newID, expanse_expanseCateID, expanse_ItemName, expanse_staffID,
                              expanse_ShopName, expanse_InsertDate, expanse_UnitPrice, expanse_Quantity,
                              '0', 0, Now());
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertExpanseCategory`
--

DROP PROCEDURE IF EXISTS `qryInsertExpanseCategory`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertExpanseCategory`(expanseCate_Name Text)
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailableExpanseCategoryID());
  Insert into ExpanseCategory values (newID, expanseCate_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertPrepaidHistory`
--

DROP PROCEDURE IF EXISTS `qryInsertPrepaidHistory`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertPrepaidHistory`(prepaid_StudentID int,
                                                                      prepaid_Date varchar(10),
                                                                      prepaid_In int,
                                                                      prepaid_Out int,
                                                                      prepaid_Event Text)
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailablePrepaidHistoryID());
  Insert into PrepaidHistory values (newID, prepaid_StudentID, prepaid_Date,
                                     prepaid_In, prepaid_Out, prepaid_Event);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertRoom`
--

DROP PROCEDURE IF EXISTS `qryInsertRoom`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertRoom`(room_ClassRoom Text,
                                                            room_SeatSpace int)
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailableRoomID());
  Insert into Room values (newID, room_ClassRoom, room_SeatSpace);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertStaffAccount`
--

DROP PROCEDURE IF EXISTS `qryInsertStaffAccount`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertStaffAccount`(staffAccount_StaffEngName Text,
                                                                    staffAccount_Password varchar(15),
                                                                    staffAccount_MasterKey varchar(15))
BEGIN
  Declare newID int;
  Declare staffAccount_staffID int;

  Set newID = (Select qryListAvailableStaffAccountID());
  Set staffAccount_staffID = (Select qryListStaffIDByEnglishName(staffAccount_StaffEngName));

  Insert into StaffAccount values (newID, staffAccount_staffID, staffAccount_Password,
                                   staffAccount_MasterKey);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertStaffType`
--

DROP PROCEDURE IF EXISTS `qryInsertStaffType`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertStaffType`(staffType_Name Text)
BEGIN
  Declare newID int;

  Set newID = (Select qryListAvailableStaffTypeID());
  Insert into StaffType values (newID, staffType_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertStudentInClass`
--

DROP PROCEDURE IF EXISTS `qryInsertStudentInClass`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertStudentInClass`(studentInClass_ID varchar(20),
																	  studentInClass_StudentID int,
                                                                      studentInClass_ClassID varchar(10),
                                                                      studentInClass_AddDate DateTime,
                                                                      studentInClass_EndDate DateTime,
                                                                      studentInClass_ClassPeriod int,
                                                                      studentInClass_ClassPrice int,
                                                                      studentInClass_ApplyFee int,
                                                                      studentInClass_MaterialFee int,
                                                                      studentInClass_Discount int)
BEGIN
  Declare newID varchar(20);

  Set newID = (Select qryListAvailableStudentInClassID(studentInClass_ID));
  Insert into StudentInClass values (studentInClass_StudentID, studentInClass_ClassID,
                                     studentInClass_Discount, '0', newID, '0',
                                     studentInClass_AddDate, Now(), studentInClass_EndDate,
                                     studentInClass_ClassPeriod, studentInClass_ApplyFee,
                                     studentInClass_MaterialFee, studentInClass_ClassPrice);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryInsertSystemlogs`
--

DROP PROCEDURE IF EXISTS `qryInsertSystemlogs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryInsertSystemlogs`(systemlogs_StaffEngName Text,
                                                                  systemlogs_Date DateTime,
                                                                  systemlogs_Action Text)
BEGIN
  Declare newID int;
  Declare systemlogs_StaffID int;

  Set newID = (Select qryListAvailableSystemlogsID());
  Set systemlogs_StaffID = (Select qryListStaffIDByEnglishName(systemlogs_StaffEngName));

  Insert into Systemlogs values (newID, systemlogs_Date, systemlogs_StaffID, systemlogs_Action);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassByEndDate`
--

DROP PROCEDURE IF EXISTS `qryListClassByEndDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassByEndDate`()
BEGIN
  SELECT ID,
         Name,
         (Select Name From ClassCategory Where ID = ClassCategoryID) As ClassCategoryName,
         StartDate,
         EndDate,
         ClassPeriod,
         ClassDay,
         Price,
         ClassStatus,
         Teacher,
         ApplyFee,
         MaterialFee,
         Note,
         Seat
  FROM Class
  WHERE  CURDATE() <= EndDate
    And  IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassByID`
--

DROP PROCEDURE IF EXISTS `qryListClassByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassByID`(class_ID varchar(10))
BEGIN
  SELECT ID,
         Name,
         (Select Name From ClassCategory Where ID = ClassCategoryID) As ClassCategoryName,
         StartDate,
         EndDate,
         ClassPeriod,
         ClassDay,
         Price,
         ClassStatus,
         Teacher,
         ApplyFee,
         MaterialFee,
         Note,
         Seat
  FROM Class
  WHERE ID = (class_ID)
  And   IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassByIDAndEndDate`
--

DROP PROCEDURE IF EXISTS `qryListClassByIDAndEndDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassByIDAndEndDate`(class_ID varchar(10))
BEGIN
  SELECT ID,
         Name,
         (Select Name From ClassCategory Where ID = ClassCategoryID) As ClassCategoryName,
         StartDate,
         EndDate,
         ClassPeriod,
         ClassDay,
         Price,
         ClassStatus,
         Teacher,
         ApplyFee,
         MaterialFee,
         Note,
         Seat
  FROM Class
  WHERE ID = (class_ID)
    And CURDATE() <= EndDate
    And   IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassByName`
--

DROP PROCEDURE IF EXISTS `qryListClassByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassByName`(class_Name text)
BEGIN
  SELECT ID,
         Name,
         (Select Name From ClassCategory Where ID = ClassCategoryID) As ClassCategoryName,
         StartDate,
         EndDate,
         ClassPeriod,
         ClassDay,
         Price,
         ClassStatus,
         Teacher,
         ApplyFee,
         MaterialFee,
         Note,
         Seat
  FROM Class
  WHERE Name Like (class_Name)
  And   IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassByNameAndEndDate`
--

DROP PROCEDURE IF EXISTS `qryListClassByNameAndEndDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassByNameAndEndDate`(class_Name text)
BEGIN
  SELECT ID,
         Name,
         (Select Name From ClassCategory Where ID = ClassCategoryID) As ClassCategoryName,
         StartDate,
         EndDate,
         ClassPeriod,
         ClassDay,
         Price,
         ClassStatus,
         Teacher,
         ApplyFee,
         MaterialFee,
         Note,
         Seat
  FROM Class
  WHERE Name Like (class_Name)
    And CURDATE() <= EndDate
    And   IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassIDByName`
--

DROP PROCEDURE IF EXISTS `qryListClassIDByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassIDByName`(class_Name text)
BEGIN
  SELECT ID
  FROM Class
  WHERE Name Like (class_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassNameByClassCategoryID`
--

DROP PROCEDURE IF EXISTS `qryListClassNameByClassCategoryID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassNameByClassCategoryID`(class_ClassCategoryID int)
BEGIN
  SELECT Name
  FROM Class
  WHERE ClassCategoryID In (class_ClassCategoryID)
  And  IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassNameByClassCategoryName`
--

DROP PROCEDURE IF EXISTS `qryListClassNameByClassCategoryName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassNameByClassCategoryName`(class_ClassCategoryName Text)
BEGIN
  SELECT Name
  FROM Class
  WHERE ClassCategoryID In (Select ID From ClassCategory Where Name = (class_ClassCategoryName))
  And  IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassPaymentByIDs`
--

DROP PROCEDURE IF EXISTS `qryListClassPaymentByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassPaymentByIDs`(classPayment_StudentID int,
                                                                       classPayment_ClassID varchar(10))
BEGIN
  Select * From ClassPayment Where StudentID = classPayment_StudentID
                               And ClassID = classPayment_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassTimeByClassID`
--

DROP PROCEDURE IF EXISTS `qryListClassTimeByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassTimeByClassID`(classTime_ClassID varchar(10))
BEGIN
  SELECT ID, ClassID, (Select Name From Class Where ID = classTime_ClassID) As ClassName, Time
  FROM ClassTime
  WHERE ClassID = (classTime_ClassID)
  Order By ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListClassTimeIDByTime`
--

DROP PROCEDURE IF EXISTS `qryListClassTimeIDByTime`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListClassTimeIDByTime`(classTime_Time varchar(30))
BEGIN
  SELECT ID
  FROM ClassTime
  WHERE Time = (classTime_Time);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListDiscount`
--

DROP PROCEDURE IF EXISTS `qryListDiscount`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListDiscount`(StudentInClass_StudentID int,
                                                              StudentInClass_ClassID varchar(10))
BEGIN
  SELECT Discount
  FROM StudentInClass
  WHERE StudentID = (StudentInClass_StudentID)
    And ClassID = (StudentInClass_ClassID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListExpanseByDates`
--

DROP PROCEDURE IF EXISTS `qryListExpanseByDates`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListExpanseByDates`(start_Date DateTime,
                                                                    end_Date DateTime)
BEGIN
  SELECT *, (Select Name From ExpanseCategory Where ID = e.ExpanseCategoryID) As ExpanseCategoryName,
            (Select EnglishName From Staff Where ID = e.InsertStaffID) As InsertStaffName,
            (Select EnglishName From Staff Where ID = e.UpdateStaffID) As UpdateStaffName
  FROM Expanse e
  WHERE InsertDate >= start_Date
    And InsertDate <= end_Date
  Order By InsertDate, UpdateDate;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListExpanseIDByItemName`
--

DROP PROCEDURE IF EXISTS `qryListExpanseIDByItemName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListExpanseIDByItemName`(expanse_ItemName Text)
BEGIN
  SELECT ID
  FROM Expanse
  WHERE ItemName Like (expanse_ItemName);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListPrepaidHistoryByDate`
--

DROP PROCEDURE IF EXISTS `qryListPrepaidHistoryByDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListPrepaidHistoryByDate`(fromDate DateTime,
                                                                          endDate  DateTime)
BEGIN
  SELECT *
  FROM PrepaidHistory
  WHERE Date >= (fromDate)
    AND Date <= (endDate);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListPrepaidHistoryByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListPrepaidHistoryByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListPrepaidHistoryByStudentID`(prepaid_StudentID int)
BEGIN
  SELECT *
  FROM PrepaidHistory
  WHERE StudentID = (prepaid_StudentID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListRoomIDByClassRoom`
--

DROP PROCEDURE IF EXISTS `qryListRoomIDByClassRoom`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListRoomIDByClassRoom`(room_ClassRoom Text)
BEGIN
  SELECT ID
  FROM Room
  WHERE ClassRoom = (room_ClassRoom);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffAccountByEnglishName`
--

DROP PROCEDURE IF EXISTS `qryListStaffAccountByEnglishName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffAccountByEnglishName`(staff_EngName Text)
BEGIN
  Declare staffAccount_staffID int;
  Set staffAccount_staffID = (Select qryListStaffIDByEnglishName(staff_EngName));

  SELECT sa.*, StaffRole As StaffRoleID, (Select Role From StaffRole Where ID = StaffRole) As Role
  FROM StaffAccount sa
  Inner Join Staff s
  On sa.StaffID = s.ID
  WHERE StaffID = (staffAccount_staffID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffAccountIDByStaffEnglishName`
--

DROP PROCEDURE IF EXISTS `qryListStaffAccountIDByStaffEnglishName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffAccountIDByStaffEnglishName`(staffAccount_StaffEngName Text)
BEGIN
  Declare staffAccount_staffID int;
  Set staffAccount_staffID = (Select qryListStaffIDByEnglishName(staffAccount_StaffEngName));

  SELECT ID
  FROM StaffAccount
  WHERE StaffID = (staffAccount_staffID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffAccountIDByStaffID`
--

DROP PROCEDURE IF EXISTS `qryListStaffAccountIDByStaffID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffAccountIDByStaffID`(staffAccount_StaffID int)
BEGIN
  SELECT ID
  FROM StaffAccount
  WHERE StaffID = (staffAccount_StaffID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffByEnglishName`
--

DROP PROCEDURE IF EXISTS `qryListStaffByEnglishName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffByEnglishName`(staff_EngName Text)
BEGIN
  SELECT *, (Select Role From StaffRole Where ID = Staff.StaffRole) As Role
  FROM Staff
  WHERE EnglishName = (staff_EngName);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffByID`
--

DROP PROCEDURE IF EXISTS `qryListStaffByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffByID`(staff_ID int)
BEGIN
  SELECT *, (Select Role From StaffRole Where ID = Staff.StaffRole) As Role
  FROM Staff
  WHERE ID = (staff_ID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffByName`
--

DROP PROCEDURE IF EXISTS `qryListStaffByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffByName`(staff_Name text)
BEGIN
  SELECT *, (Select Role From StaffRole Where ID = Staff.StaffRole) As Role
  FROM Staff
  WHERE Name Like (staff_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStaffIDByName`
--

DROP PROCEDURE IF EXISTS `qryListStaffIDByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStaffIDByName`(staff_Name text)
BEGIN
  SELECT ID
  FROM Staff
  WHERE Name Like (staff_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentByClassID`(class_ID varchar(10))
BEGIN
  SELECT *
  FROM Student
  WHERE ID In (Select Distinct StudentID From StudentInClass
               Where  ClassID = class_ID
                 And  IsDeleted = '0');
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentByClassName`
--

DROP PROCEDURE IF EXISTS `qryListStudentByClassName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentByClassName`(class_Name Text)
BEGIN
  SELECT *
  FROM Student
  WHERE ID In (Select Distinct StudentID From StudentInClass
               Where  ClassID = (Select ClassID From Class
                                 Where  Name = (class_Name))
                 And  IsDeleted = '0') ;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentByID`
--

DROP PROCEDURE IF EXISTS `qryListStudentByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentByID`(Student_ID int)
BEGIN
  SELECT *
  FROM Student
  WHERE ID = (Student_ID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentByName`
--

DROP PROCEDURE IF EXISTS `qryListStudentByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentByName`(Student_Name text)
BEGIN
  SELECT *
  FROM Student
  WHERE Name Like (Student_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentHaveToRefundByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentHaveToRefundByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentHaveToRefundByClassID`(class_ID varchar(10))
BEGIN
  Select Distinct StudentID,
         (Select Name From Student Where ID = cp.StudentID) AS StudentName,
         ClassID,
         (Select Name From Class Where ID = class_ID) AS ClassName,
         (Select Price From Class Where ID = class_ID) AS ClassPrice,
         (Select MaterialFee From Class Where ID = class_ID) AS ClassMaterialFee,
         IFNULL((Select Discount From StudentInClass Where StudentID = cp.StudentID
                                                       And ClassID = class_ID
                                                       And IsDeleted = '1'
                                                       And IsRefund = '0'), 0) AS Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = cp.StudentID And ClassID = class_ID), 0) AS HavePaid
   From ClassPayment cp
  Where ClassID Like (class_ID)
    And '1' = (Select IsDeleted From Class Where ID = class_ID)
    And StudentID Not In (Select StudentID From ClassRefunded Where ID In (Select RefundID
                                                                             From ClassRefundDetail
                                                                            Where ClassID = class_ID))
    And StudentID In (Select StudentID From StudentInClass Where ClassID = class_ID
                                                             And IsDeleted = '1'
                                                             And IsRefund = '0')
  Order By StudentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentHaveToRefundByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentHaveToRefundByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentHaveToRefundByStudentID`(student_ID int)
BEGIN
  Select Distinct sc.StudentID,
         (Select Name From Student Where ID = sc.StudentID) AS StudentName,
         sc.ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         ClassPrice,
         MaterialFee,
         (Select Sum(Discount) From StudentInClass Where StudentID = sc.StudentID
                                                            And ClassID = sc.ClassID
                                                            And IsDeleted = '1'
                                                            And IsRefund = '0') AS Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) AS HavePaid
   From ClassPayment cp Right Join StudentInClass sc
     On cp.StudentID = sc.StudentID
  Where sc.StudentID = student_ID
    And IsDeleted = '1'
    And IsRefund = '0'
    And 0 < IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID And StudentID = sc.StudentID And ClassID = sc.ClassID), 0)
    Group By sc.ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentHaveToRefundClassByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentHaveToRefundClassByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentHaveToRefundClassByClassID`(class_ID varchar(10))
BEGIN
  Select Distinct *
   From Class
  Where ID Like (class_ID)
    And IsDeleted = '1';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentHaveToRefundClassByClassName`
--

DROP PROCEDURE IF EXISTS `qryListStudentHaveToRefundClassByClassName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentHaveToRefundClassByClassName`(class_Name Text)
BEGIN
  Select Distinct *
   From Class
  Where ID = (Select ID From Class Where Name Like (class_Name)
                                     And IsDeleted = '1');
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentIDByName`
--

DROP PROCEDURE IF EXISTS `qryListStudentIDByName`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentIDByName`(student_Name text)
BEGIN
  SELECT ID
  FROM Student
  WHERE Name Like (student_Name);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentInClassByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentInClassByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentInClassByStudentID`(StudentInClass_StudentID int)
BEGIN
  SELECT *
  FROM StudentInClass sc
  WHERE StudentID = (StudentInClass_StudentID)
        /*And CURDATE() <= (Select EndDate From Class Where ID = sc.ClassID)*/
    And IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentInClassIDByIDs`
--

DROP PROCEDURE IF EXISTS `qryListStudentInClassIDByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentInClassIDByIDs`(studentInClass_StudentID int,
                                                                           studentInClass_ClassID varchar(10))
BEGIN
  SELECT ID
  FROM StudentInClass sc
  WHERE StudentID = (studentInClass_StudentID)
    And ClassID = (studentInClass_ClassID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentNeedToPayByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentNeedToPayByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentNeedToPayByClassID`(class_ID varchar(10))
BEGIN
  Select StudentID,
         (Select Name From Student Where ID = sc.StudentID) AS StudentName,
         ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         AddDate,
         EndDate,
         (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)) AS ClassPrice,
         (Select MaterialFee From StudentInClass Where StudentID = sc.StudentID
                                                   And ClassID = sc.ClassID
                                                   And IsDeleted = '0') AS ClassMaterialFee,
         (Select ApplyFee From StudentInClass Where StudentID = sc.StudentID
                                                And ClassID = sc.ClassID
                                                And IsDeleted = '0') AS ClassApplyFee,
         Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) AS HavePaid
  From StudentInClass sc
  Where ClassID = class_ID
    And Discount < (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)+ sc.MaterialFee + sc.ApplyFee) - IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = sc.StudentID And ClassID = sc.ClassID), 0)
    And IsDeleted = '0'
  Order By StudentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentNeedToPayByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentNeedToPayByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentNeedToPayByStudentID`(student_ID int)
BEGIN
  Select StudentID,
         (Select Name From Student Where ID = student_ID) AS StudentName,
         ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         AddDate,
         EndDate,
         (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)) AS ClassPrice,
         (Select MaterialFee From StudentInClass Where StudentID = sc.StudentID
                                                   And ClassID = sc.ClassID
                                                   And IsDeleted = '0') AS ClassMaterialFee,
         (Select ApplyFee From StudentInClass Where StudentID = sc.StudentID
                                                And ClassID = sc.ClassID
                                                And IsDeleted = '0') AS ClassApplyFee,
         Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) AS HavePaid
  From StudentInClass sc
  Where StudentID = student_ID
    /*And IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = student_ID And ClassID = sc.ClassID), 0) > 0*/
    And  (Select (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)+ sc.MaterialFee + ApplyFee) - Discount
                  From Class
                  Where ID = sc.ClassID) > IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                                                        And ClassID = sc.ClassID
                                                                                        And StudentID = sc.StudentID), 0)
    And IsDeleted = '0';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentNeedToPayTotalByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentNeedToPayTotalByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentNeedToPayTotalByClassID`(class_ID varchar(10))
BEGIN
  Select Distinct ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         (Select Price From Class Where ID = sc.ClassID) AS ClassPrice,
         (Select MaterialFee From Class Where ID = sc.ClassID) AS ClassMaterialFee,
         (Select ApplyFee From Class Where ID = sc.ClassID) AS ClassApplyFee,
         Count(StudentID) As ClassSeat,
         IFNULL((Select Sum(Paid) From ClassPayment
                  Where ClassID = sc.ClassID
                    And StudentID Not In (Select StudentID From StudentInClass
                                           Where IsDeleted = '0'
                                             And 0 < (Select (Select qryListNeedToPayMoneyByIDs(sc.StudentID, ClassID) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																																				 And ClassID = sc.ClassID
																																																				 And StudentID = sc.StudentID), 0)
                                                                                                                 From Class
                                                                                                                 Where ID = sc.ClassID))), 0) As HavePaid,
         Sum((Select qryListNeedToPayMoneyByIDs(StudentID, ClassID) + sc.MaterialFee + sc.ApplyFee)) - Sum(Discount) - Sum(IFNULL((Select Sum(Paid) From ClassPayment
                                                                            Where ClassID = sc.ClassID
                                                                              And StudentID In (Select StudentID From StudentInClass
                                                                                                     Where IsDeleted = '0'
                                                                                                       And 0 < (Select (Select qryListNeedToPayMoneyByIDs(sc.StudentID, ClassID) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																																																		   And ClassID = sc.ClassID
																																																																		   And StudentID = sc.StudentID), 0)
                                                                                                                 From Class
                                                                                                                 Where ID = sc.ClassID))), 0)) As NeedToPay
  From StudentInClass sc Join Class c
    On sc.ClassID = c.ID
  Where ClassID = class_ID
    And 0 < (Select (Select qryListNeedToPayMoneyByIDs(sc.StudentID, ClassID) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = sc.StudentID And ClassID = sc.ClassID), 0)
             From Class
             Where ID = sc.ClassID)
    And sc.IsDeleted = '0'
  Group By StudentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentNeedToPayTotalByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentNeedToPayTotalByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentNeedToPayTotalByStudentID`(student_ID int)
BEGIN
  Select Distinct StudentID,
         (Select Name From Student Where ID = sc.StudentID) AS StudentName,
         Count(ClassID) As ChosenClass,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) As HavePaid,
         Sum((Select IFNULL(qryListNeedToPayMoneyByIDs(sc.StudentID, sc.ClassID), 0) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																								  			   And ClassID = sc.ClassID
																																											   And StudentID = sc.StudentID), 0)) As NeedToPay
  From StudentInClass sc Join Class c
    On sc.ClassID = c.ID
  Where sc.StudentID = student_ID
    And 0 < (Select (Select IFNULL(qryListNeedToPayMoneyByIDs(sc.StudentID, sc.ClassID), 0) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																													  And ClassID = sc.ClassID
																																													  And StudentID = sc.StudentID), 0)
             From Class
             Where ID = sc.ClassID)
    And sc.IsDeleted = '0'
  Order By ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentRecord`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentRecord`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentRecord`()
BEGIN
  SELECT cp.StudentID,
         (Select Name From Student Where ID = cp.StudentID) As StudentName,
         cp.ClassID,
         (Select Name From Class Where ID = cp.ClassID) As ClassName,
         sc.AddDate,
         sc.EndDate,
         PayDate,
         Paid,
         PayType
  FROM   ClassPayment cp Join StudentInClass sc
    On   cp.StudentInClassID = sc.ID
  Order  By cp.PayDate, cp.StudentID, cp.ClassID, cp.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;


--
-- Definition of procedure `qryListStudentPaymentRecordWithSearchDate`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentRecordWithSearchDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentRecordWithSearchDate`(fromDate DateTime, 
																						endDate DateTime)
BEGIN
  SELECT cp.StudentID,
         (Select Name From Student Where ID = cp.StudentID) As StudentName,
         cp.ClassID,
         (Select Name From Class Where ID = cp.ClassID) As ClassName,
         sc.AddDate,
         sc.EndDate,
         PayDate,
         Paid,
         PayType
  FROM   ClassPayment cp Join StudentInClass sc
    On   cp.StudentInClassID = sc.ID
 Where   PayDate >= fromDate
    And  PayDate <= endDate
  Order  By cp.PayDate, cp.StudentID, cp.ClassID, cp.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;


--
-- Definition of procedure `qryListStudentPaymentRecordByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentRecordByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentRecordByClassID`(class_ID varchar(10))
BEGIN
  SELECT cp.StudentID,
         (Select Name From Student Where ID = cp.StudentID) As StudentName,
         cp.ClassID,
         (Select Name From Class Where ID = cp.ClassID) As ClassName,
         sc.AddDate,
         sc.EndDate,
         PayDate,
         Paid,
         PayType
  FROM   ClassPayment cp Join StudentInClass sc
    On   cp.StudentInClassID = sc.ID
  WHERE  cp.ClassID = (class_ID)
    And  IsDeleted = '0'
  Order By cp.StudentID, cp.PayDate, cp.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentRecordByIDs`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentRecordByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentRecordByIDs`(student_ID int,
                                                                               class_ID varchar(10))
BEGIN
  SELECT cp.ID,
         cp.StudentID,
         (Select Name From Student Where ID = cp.StudentID) As StudentName,
         cp.ClassID,
         (Select Name From Class Where ID = cp.ClassID) As ClassName,
         sc.AddDate,
         sc.EndDate,
         PayDate,
         Paid,
         PayType
  FROM   ClassPayment cp Join StudentInClass sc
    On   cp.StudentInClassID = sc.ID
  WHERE  cp.ClassID = (class_ID)
  And    cp.StudentID = (student_ID)
  Order  By cp.StudentID, cp.ClassID, cp.PayDate, cp.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentRecordByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentRecordByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentRecordByStudentID`(student_ID int)
BEGIN
  SELECT cp.StudentID,
         (Select Name From Student Where ID = cp.StudentID) As StudentName,
         cp.ClassID,
         (Select Name From Class Where ID = cp.ClassID) As ClassName,
         sc.AddDate,
         sc.EndDate,
         cp.PayDate,
         cp.Paid,
         cp.PayType
  FROM  ClassPayment cp Join StudentInClass sc
    On  cp.StudentInClassID = sc.ID
  WHERE cp.StudentID = (student_ID)
    And sc.IsDeleted = '0'
  Order By cp.PayDate, cp.ClassID, cp.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentTotalRecordByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentTotalRecordByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentTotalRecordByClassID`(class_ID varchar(10))
BEGIN
  Select Distinct ClassID,
         (Select Name From Class Where ID = cpa.ClassID) As ClassName,
         (Select Count(*) From StudentInClass Where ClassID = cpa.ClassID
                                                And IsDeleted = '0') As ClassSeat,
         IFNULL((Select Sum(Paid) From  ClassPayment cp Join StudentInClass sc
                                    On  cp.StudentInClassID = sc.ID
                                  Where cp.ClassID = cpa.ClassID
                                    And IsDeleted = '0'), 0) As HavePaid
  From   ClassPayment cpa
  Where  ClassID = (class_ID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentTotalRecordByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentTotalRecordByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentTotalRecordByStudentID`(student_ID int)
BEGIN
  Select Distinct StudentID,
         (Select Name From Student Where ID = cpa.StudentID) As StudentName,
         (Select Count(*) From StudentInClass Where StudentID = cpa.StudentID
                                                And IsDeleted = '0') As ChosenClass,
         IFNULL((Select Sum(Paid) From  ClassPayment cp Join StudentInClass sc
                                    On  cp.StudentInClassID = sc.ID
                                  Where cp.StudentID = cpa.StudentID
                                    And IsDeleted = '0'), 0) As HavePaid
  From   ClassPayment cpa
  Where  StudentID = student_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentTotalRecordWithClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentTotalRecordWithClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentTotalRecordWithClassID`()
BEGIN
  Select Distinct ClassID,
         (Select Name From Class Where ID = cpa.ClassID) As ClassName,
         (Select Count(*) From StudentInClass Where ClassID = cpa.ClassID
                                                And IsDeleted = '0') As ClassSeat,
         IFNULL((Select Sum(Paid) From  ClassPayment cp Join StudentInClass sc
                                    On  cp.StudentInClassID = sc.ID
                                  Where cp.ClassID = cpa.ClassID
                                    And IsDeleted = '0'), 0) As HavePaid
  From   ClassPayment cpa
  Order  by cpa.ClassID, cpa.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentTotalRecordWithClassIDByDate`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentTotalRecordWithClassIDByDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentTotalRecordWithClassIDByDate`(fromDate DateTime,
                                                                                                endDate  DateTime)
BEGIN
  Select Distinct ClassID,
         (Select Name From Class Where ID = cpa.ClassID) As ClassName,
         (Select Count(*) From StudentInClass Where ClassID = cpa.ClassID
                                                And IsDeleted = '0') As ClassSeat,
         IFNULL((Select Sum(Paid) From  ClassPayment cp Join StudentInClass sc
                                    On  cp.StudentInClassID = sc.ID
                                  Where cp.ClassID = cpa.ClassID
                                    And IsDeleted = '0'), 0) As HavePaid
   From  ClassPayment cpa
  Where  PayDate >= fromDate
    And  PayDate <= endDate
  Order  by cpa.ClassID, cpa.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentTotalRecordWithStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentTotalRecordWithStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentTotalRecordWithStudentID`()
BEGIN
  Select Distinct StudentID,
         (Select Name From Student Where ID = cpa.StudentID) As StudentName,
         (Select Count(*) From StudentInClass Where StudentID = cpa.StudentID
                                                And IsDeleted = '0') As ChosenClass,
         IFNULL((Select Sum(Paid) From  ClassPayment cp Join StudentInClass sc
                                    On  cp.StudentInClassID = sc.ID
                                  Where cp.StudentID = cpa.StudentID
                                    And IsDeleted = '0'), 0) As HavePaid
   From  ClassPayment cpa
   Order by cpa.StudentID, cpa.ClassID, cpa.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPaymentTotalRecordWithStudentIDByDate`
--

DROP PROCEDURE IF EXISTS `qryListStudentPaymentTotalRecordWithStudentIDByDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPaymentTotalRecordWithStudentIDByDate`(fromDate DateTime,
                                                                                                  endDate  DateTime)
BEGIN
  Select Distinct StudentID,
         (Select Name From Student Where ID = cpa.StudentID) As StudentName,
         (Select Count(*) From StudentInClass Where StudentID = cpa.StudentID
                                                And IsDeleted = '0') As ChosenClass,
         IFNULL((Select Sum(Paid) From  ClassPayment cp Join StudentInClass sc
                                    On  cp.StudentInClassID = sc.ID
                                  Where cp.StudentID = cpa.StudentID
                                    And IsDeleted = '0'), 0) As HavePaid
   From  ClassPayment cpa
  Where  PayDate >= fromDate
    And  PayDate <= endDate
  Order  by cpa.StudentID, cpa.ClassID, cpa.ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentPrePaidByID`
--

DROP PROCEDURE IF EXISTS `qryListStudentPrePaidByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentPrePaidByID`(Student_ID int)
BEGIN
  SELECT PrePaid
  FROM Student
  WHERE ID = (Student_ID);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundByClassID`(class_ID varchar(10))
BEGIN
  Select ID,
         Receiver As ClassID,
         (Select Name From Class Where ID = cr.Receiver) AS ClassName,
         RefundDate,
         Refund,
         Discount,
         (Select Name From Staff Where ID = cr.StaffID) As StaffName
   From  ClassRefunded cr
   Where Receiver = (class_ID)
     And SubID = 0
   Order By RefundDate, ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundByStudentID`(student_ID int)
BEGIN
  Select ID,
         StudentID,
         (Select Name From Student Where ID = cr.StudentID) AS StudentName,
         RefundDate,
         Refund,
         Discount,
         (Select Name From Staff Where ID = cr.StaffID) As StaffName
   From  ClassRefunded cr
   Where StudentID = student_ID
     And SubID = 0
     And Receiver Not In (Select ID From Class)
   Order By RefundDate, ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundDetailByRefundID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundDetailByRefundID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundDetailByRefundID`(refund_ID int)
BEGIN
  Select (Select Distinct StudentID From ClassRefunded Where ID = crd.RefundID) As StudentID,
         (Select Name From Student Where ID = (Select Distinct StudentID
                                                 From ClassRefunded
                                                Where ID = crd.RefundID)) As StudentName,
         ClassID,
         (Select Name From Class Where ID = crd.ClassID) As ClassName,
         HavePaid
   From  ClassRefundDetail crd
   Where RefundID = refund_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundListByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundListByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundListByClassID`(class_ID varchar(10))
BEGIN
  Select ID,
         StudentID,
         (Select Name From Student Where ID = cr.StudentID) AS StudentName,
         RefundDate,
         Refund,
         Receiver,
         RefundType
   From  ClassRefunded cr
   Where ID In (Select ID From ClassRefunded Where Receiver = (Select ID
                                                                 From ClassRefunded
                                                                Where Receiver = class_ID
                                                                  And RefundType = 'ClassRefunded'))
     And SubID <> 0
   Order By RefundDate, StudentID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundListByRefundID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundListByRefundID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundListByRefundID`(refund_ID int)
BEGIN
  Select ID,
         StudentID,
         (Select Name From Student Where ID = cr.StudentID) AS StudentName,
         RefundDate,
         Refund,
         Receiver,
         RefundType
   From  ClassRefunded cr
   Where ID = refund_ID
     And SubID <> 0
   Order By StudentID, RefundDate, ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundListByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundListByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundListByStudentID`(student_ID int)
BEGIN
  Select ID,
         StudentID,
         (Select Name From Student Where ID = cr.StudentID) AS StudentName,
         RefundDate,
         Refund,
         Receiver,
         RefundType
   From  ClassRefunded cr
   Where StudentID = student_ID
     And SubID <> 0
   Order By RefundDate, ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundWithClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundWithClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundWithClassID`()
BEGIN
  Select ID,
         Receiver As ClassID,
         (Select Name From Class Where ID = cr.Receiver) AS ClassName,
         RefundDate,
         Refund,
         Discount,
         (Select Name From Staff Where ID = cr.StaffID) As StaffName
   From  ClassRefunded cr
   Where SubID = 0
     And Receiver In (Select ID From Class)
   Order By RefundDate, ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentRefundWithStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentRefundWithStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentRefundWithStudentID`()
BEGIN
  Select ID,
         StudentID,
         (Select Name From Student Where ID = cr.StudentID) AS StudentName,
         RefundDate,
         Refund,
         Discount,
         (Select Name From Staff Where ID = cr.StaffID) As StaffName
   From  ClassRefunded cr
   Where SubID = 0
     And Receiver Not In (Select ID From Class)
   Order By StudentID, RefundDate, ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentSelectClassByClassID`
--

DROP PROCEDURE IF EXISTS `qryListStudentSelectClassByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentSelectClassByClassID`(class_ID varchar(10))
BEGIN
  Select StudentID,
         (Select Name From Student Where ID = sc.StudentID) AS StudentName,
         ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         AddDate,
         EndDate,
         (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)) AS ClassPrice,
         (Select MaterialFee From StudentInClass Where StudentID = sc.StudentID
                                                   And ClassID = sc.ClassID
                                                   And IsDeleted = '0') AS ClassMaterialFee,
         (Select ApplyFee From StudentInClass Where StudentID = sc.StudentID
                                                And ClassID = sc.ClassID
                                                And IsDeleted = '0') AS ClassApplyFee,
         Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) AS HavePaid
  From StudentInClass sc
  Where ClassID = class_ID
    /*And Now() <= EndDate*/
    And IsDeleted = '0'
  Order By ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentSelectClassByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentSelectClassByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentSelectClassByStudentID`(student_ID int)
BEGIN
  Select StudentID,
         (Select Name From Student Where ID = student_ID) AS StudentName,
         ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         AddDate,
         EndDate,
         (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)) AS ClassPrice,
         MaterialFee As ClassMaterialFee,
         ApplyFee As ClassApplyFee,
         Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) AS HavePaid
  From StudentInClass sc
  Where StudentID = student_ID
    /*And Now() <= EndDate*/
    And IsDeleted = '0'
  Order By ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListStudentWithDrawClassByStudentID`
--

DROP PROCEDURE IF EXISTS `qryListStudentWithDrawClassByStudentID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListStudentWithDrawClassByStudentID`(StudentInClass_StudentID int)
BEGIN
  SELECT *
  FROM StudentInClass
  WHERE StudentID = (StudentInClass_StudentID)
    And IsDeleted = '1';
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryListSystemLogs`
--

DROP PROCEDURE IF EXISTS `qryListSystemLogs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryListSystemLogs`(start_Date DateTime, end_Date DateTime)
BEGIN
  SELECT *, (Select EnglishName From Staff Where ID = s.StaffID) As StaffName
  FROM  SystemLogs s
  WHERE Date >= (start_Date)
    And Date <= (end_Date);
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateClassByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateClassByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateClassByID`(class_OldID varchar(10),
                                                                 class_NewID varchar(10),
                                                                 class_ClassCategoryName Text,
                                                                 class_Name Text,
                                                                 class_StartDate DateTime,
                                                                 class_EndDate DateTime,
                                                                 class_ClassPeriod int,
                                                                 class_ClassDay varchar(13),
                                                                 class_Seat int,
                                                                 class_Price int,
                                                                 class_Teacher Text,
                                                                 class_MaterialFee int,
                                                                 class_ApplyFee int,
                                                                 class_Note Text)
BEGIN
  Declare class_ClassCategoryID int;
  Set class_ClassCategoryID = (Select qryListClassCategoryIDByName(class_ClassCategoryName));

  Update Class Set ID = class_NewID,
                   Name = class_Name,
                   StartDate = class_StartDate,
                   EndDate = class_EndDate,
                   ClassPeriod = class_ClassPeriod,
                   ClassDay = class_ClassDay,
                   Seat = class_Seat,
                   Price = class_Price,
                   Teacher = class_Teacher,
                   MaterialFee = class_MaterialFee,
                   ApplyFee = class_ApplyFee,
                   Note = class_Note
  Where ID = class_OldID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateClassCategoryByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateClassCategoryByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateClassCategoryByID`(classCate_ID int,
                                                                         class_ClassCategoryName Text)
BEGIN
  Update ClassCategory Set Name = class_ClassCategoryName
  Where ID = classCate_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateClassIsDeletedByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateClassIsDeletedByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateClassIsDeletedByID`(class_ID varchar(10))
BEGIN
  Update Class Set IsDeleted = '1'
  Where ID = class_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateClassStatusByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateClassStatusByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateClassStatusByID`(class_ID varchar(10),
                                                                       class_Status varchar(10))
BEGIN
  Update Class Set ClassStatus = class_Status
  Where ID = class_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateClassTimeByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateClassTimeByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateClassTimeByID`(classTime_ID int,
                                                                     classTime_ClassName Text,
                                                                     classTime_Time varchar(30))
BEGIN
  Declare classID int;

  Set classID = (Select qryListClassIDByName());

  Update Class Set ClassID = classID,
                   Time = classTime_Time
  Where ID = classTime_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateCompanyInfo`
--

DROP PROCEDURE IF EXISTS `qryUpdateCompanyInfo`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateCompanyInfo`(company_Name Varchar(50),
                                                                   company_Manager Varchar(10))
BEGIN
  Update CompanyInfo Set CompanyName = company_Name,
                         CompanyManager= company_Manager;

END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateExpanseByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateExpanseByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateExpanseByID`(expanse_ID int,
                                                                   expanse_ExpanseCategoryName Text,
                                                                   expanse_StaffName Text,
                                                                   expanse_ItemName Text,
                                                                   expanse_ShopName Text,
                                                                   expanse_UnitPrice Double,
                                                                   expanse_Quantity int)
BEGIN
  Declare expanse_expanseCateID int;
  Declare expanse_staffID int;

  Set expanse_expanseCateID = (Select qryListExpanseCategoryIDByName(expanse_ExpanseCategoryName));
  Set expanse_staffID = (Select qryListStaffIDByEnglishName(expanse_StaffName));

  Update Expanse Set ExpanseCategoryID = expanse_expanseCateID,
                     UpdateStaffID = expanse_staffID,
                     ItemName = expanse_ItemName,
                     ShopName = expanse_ShopName,
                     UpdateDate = Now(),
                     UnitPrice = expanse_UnitPrice,
                     Quantity = expanse_Quantity
  Where ID = expanse_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateExpanseWithInsertDate`
--

DROP PROCEDURE IF EXISTS `qryUpdateExpanseWithInsertDate`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateExpanseWithInsertDate`(expanse_ID int,
																			 expanse_ExpanseCategoryName varchar(45),
																			 expanse_StaffName varchar(20),
																			 expanse_ItemName varchar(45),
																			 expanse_ShopName varchar(45),
																			 expanse_UnitPrice int,
																			 expanse_Quantity int,
																			 expanse_InsertDate DateTime)
BEGIN
  Declare expanse_expanseCateID int;
  Declare expanse_staffID int;

  Set expanse_expanseCateID = (Select qryListExpanseCategoryIDByName(expanse_ExpanseCategoryName));
  Set expanse_staffID = (Select qryListStaffIDByEnglishName(expanse_StaffName));

  Update Expanse Set ExpanseCategoryID = expanse_expanseCateID,
                     UpdateStaffID = expanse_staffID,
                     ItemName = expanse_ItemName,
                     ShopName = expanse_ShopName,
					 InsertDate = expanse_InsertDate,
                     UpdateDate = Now(),
                     UnitPrice = expanse_UnitPrice,
                     Quantity = expanse_Quantity
  Where ID = expanse_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateExpanseCategoryByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateExpanseCategoryByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateExpanseCategoryByID`(expanseCate_ID int,
                                                                           expanseCate_Name Text)
BEGIN
  Update ExpanseCategory Set Name = expanseCate_Name
  Where ID = expanseCate_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateExpanseIsDeletedByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateExpanseIsDeletedByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateExpanseIsDeletedByID`(expanse_ID int)
BEGIN
  Update Expanse Set IsDeleted = '1'
  Where ID = expanse_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateRoomByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateRoomByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateRoomByID`(room_ID int,
                                                                room_ClassRoom Text,
                                                                room_SeatSpace int)
BEGIN
  Update Room Set ClassRoom = room_ClassRoom,
                  SeatSpace = room_SeatSpace
  Where ID = room_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStaffAccountByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStaffAccountByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStaffAccountByID`(staffAccount_ID int,
                                                                        staffAccount_Password varchar(15),
                                                                        staffAccount_MasterKey varchar(15))
BEGIN
  Update StaffAccount Set Password = staffAccount_Password,
                          MasterKey = staffAccount_MasterKey
  Where ID = staffAccount_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStaffAccountMasterKeyByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStaffAccountMasterKeyByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStaffAccountMasterKeyByID`(staffAccount_ID int,
                                                                                 staffAccount_MasterKey varchar(15))
BEGIN
  Update StaffAccount Set MasterKey = staffAccount_MasterKey
  Where ID = staffAccount_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStaffByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStaffByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStaffByID`(staff_ID int,
                                                                 staff_StaffRole int,
                                                                 staff_TypeName Text,
                                                                 staff_Name Text,
                                                                 staff_EngName Text,
                                                                 staff_StartDate varchar(10),
                                                                 staff_EndDate varchar(10),
                                                                 staff_LaborCover int,
                                                                 staff_HealthCover int,
                                                                 staff_GroupCover int,
                                                                 staff_Note Text)
BEGIN
  Declare staff_TypeID int;

  Set staff_TypeID = (Select qryListStaffTypeIDByName(staff_TypeName));

  Update Staff Set StaffRole = staff_StaffRole,
                   StaffTypeID = staff_TypeID,
                   Name = staff_Name,
                   EnglishName = staff_EngName,
                   StartDate = staff_StartDate,
                   EndDate = staff_EndDate,
                   LaborCover = staff_LaborCover,
                   HealthCover = staff_HealthCover,
                   GroupCover = staff_GroupCover,
                   Note = staff_Note
  Where ID = staff_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStaffEndDateByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStaffEndDateByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStaffEndDateByID`(staff_ID int,
                                                                        staff_EndDate varchar(10))
BEGIN
  Update Staff Set EndDate = staff_EndDate
  Where ID = staff_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStaffIsDeletedByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStaffIsDeletedByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStaffIsDeletedByID`(staff_ID int)
BEGIN
  Update Staff Set IsDeleted = '1'
  Where ID = staff_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStaffTypeByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStaffTypeByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStaffTypeByID`(staffType_ID int,
                                                                     staffType_Name Text)
BEGIN
  Update StaffType Set Name = staffType_Name
  Where ID = staffType_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentByID`(student_ID int,
                                                                   student_Name Text,
                                                                   student_Sex varchar(15),
                                                                   student_DateOfBirth varchar(10),
                                                                   student_SocialNumber varchar(10),
                                                                   student_StartDate varchar(10),
                                                                   student_School Text,
                                                                   student_StudyYear Text,
                                                                   student_FatherName Text,
                                                                   student_FatherWork Text,
                                                                   student_MotherName Text,
                                                                   student_MotherWork Text,
                                                                   student_Sibling Text,
                                                                   student_InChargePerson Text,
                                                                   student_InChargePersonHomePhone Text,
                                                                   student_InChargePersonCompanyPhone Text,
                                                                   student_InChargePersonMobile Text,
                                                                   student_EmergencyPerson Text,
                                                                   student_EmergencyPhone Text,
                                                                   student_Address Text,
                                                                   student_PostCode varchar(5))
BEGIN
  Update Student Set Name = student_Name,
                     Sex = student_Sex,
                     DateOfBirth = student_DateOfBirth,
                     SocialNumber = student_SocialNumber,
                     StartDate = student_StartDate,
                     School = student_School,
                     StudyYear = student_StudyYear,
                     FatherName = student_FatherName,
                     FatherWork = student_FatherWork,
                     MotherName = student_MotherName,
                     MotherWork = student_MotherWork,
                     Sibling = student_Sibling,
                     InChargePerson = student_InChargePerson,
                     InChargePersonHomePhone = student_InChargePersonHomePhone,
                     CompanyPhone = student_InChargePersonCompanyPhone,
                     InChargePersonMobile = student_InChargePersonMobile,
                     EmergencyPerson = student_EmergencyPerson,
                     EmergencyPhone = student_EmergencyPhone,
                     Address = student_Address,
                     PostCode = student_PostCode
  Where ID = student_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentInClassByIDs`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentInClassByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentInClassByIDs`(studentInClass_StudentID int,
                                                                           studentInClass_OldClassID varchar(10),
                                                                           studentInClass_NewClassID varchar(10))
BEGIN
  Update StudentInClass Set ClassID = studentInClass_NewClassID
  Where StudentID = studentInClass_StudentID
    And ClassID = studentInClass_OldClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentInClassDiscountByIDs`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentInClassDiscountByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentInClassDiscountByIDs`(studentInClass_StudentID int,
                                                                                   studentInClass_ClassID varchar(10),
                                                                                   studentInClass_Discount int)
BEGIN
  Update StudentInClass Set Discount = studentInClass_Discount
  Where StudentID = studentInClass_StudentID
    And ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentInClassIsDeletedByClassID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentInClassIsDeletedByClassID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentInClassIsDeletedByClassID`(studentInClass_ClassID varchar(10))
BEGIN
  Update StudentInClass Set IsDeleted = '1',
                            WithdrawDate = Now()
  Where ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentInClassIsDeletedByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentInClassIsDeletedByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentInClassIsDeletedByID`(studentInClass_StudentID int,
                                                                                   studentInClass_ClassID varchar(10))
BEGIN
  Update StudentInClass Set IsDeleted = '1'
  Where StudentID = studentInClass_StudentID
    And ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentInClassIsRefundByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentInClassIsRefundByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentInClassIsRefundByID`(studentInClass_StudentID int,
                                                                                  studentInClass_ClassID varchar(10))
BEGIN
  Update StudentInClass Set IsRefund = '1'
  Where StudentID = studentInClass_StudentID
    And ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentInClassNotIsDeletedByIDs`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentInClassNotIsDeletedByIDs`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentInClassNotIsDeletedByIDs`(studentInClass_StudentID int,
                                                                                       studentInClass_ClassID varchar(10))
BEGIN
  Update StudentInClass Set IsDeleted = '0'
  Where StudentID = studentInClass_StudentID
    And ClassID = studentInClass_ClassID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentIsDeletedByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentIsDeletedByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentIsDeletedByID`(student_ID int)
BEGIN
  Update Student Set IsDeleted = '1'
  Where ID = student_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of procedure `qryUpdateStudentPrePaidByID`
--

DROP PROCEDURE IF EXISTS `qryUpdateStudentPrePaidByID`;

DELIMITER $$

/*!50003 SET @TEMP_SQL_MODE=@@SQL_MODE, SQL_MODE='STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ $$
CREATE DEFINER=`root`@`localhost` PROCEDURE `qryUpdateStudentPrePaidByID`(student_ID int,
                                                                          student_PrePaid int)
BEGIN
  Update Student Set PrePaid = (student_PrePaid)
  Where ID = student_ID;
END $$
/*!50003 SET SESSION SQL_MODE=@TEMP_SQL_MODE */  $$

DELIMITER ;

--
-- Definition of view `qrylistneedtopaystudent`
--

Drop Table IF EXISTS `qrylistneedtopaystudent`;
CREATE OR REPLACE VIEW `qrylistneedtopaystudent` AS
  Select StudentID,
         (Select Name From Student Where ID = sc.StudentID) AS StudentName,
         ClassID,
         (Select Name From Class Where ID = sc.ClassID) AS ClassName,
         AddDate,
         EndDate,
         (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)) AS ClassPrice,
         (Select MaterialFee From StudentInClass Where StudentID = sc.StudentID
                                                   And ClassID = sc.ClassID
                                                   And IsDeleted = '0') AS ClassMaterialFee,
         (Select ApplyFee From StudentInClass Where StudentID = sc.StudentID
                                                And ClassID = sc.ClassID
                                                And IsDeleted = '0') AS ClassApplyFee,
         Discount,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
                                                      And ClassID = sc.ClassID
                                                      And StudentID = sc.StudentID), 0) AS HavePaid
  From StudentInClass sc
  Where Discount < (Select (Select qryListNeedToPayMoneyByIDs(StudentID, ClassID)+ sc.MaterialFee + ApplyFee) - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																							 And ClassID = sc.ClassID
																																							 And StudentID = sc.StudentID), 0)
                    From Class
                    Where ID = sc.ClassID)
    And IsDeleted = '0';

Drop Table IF EXISTS `qrylistneedtopaytotalstudent`;
CREATE OR REPLACE VIEW `qrylistneedtopaytotalstudent` AS
  Select Distinct StudentID,
         (Select Name From Student Where ID = sc.StudentID) AS StudentName,
         Count(ClassID) As ChosenClass,
         IFNULL((Select Sum(Paid) From ClassPayment Where StudentID = sc.StudentID And ClassID = sc.ClassID), 0) As HavePaid,
         Sum((Select IFNULL(qryListNeedToPayMoneyByIDs(sc.StudentID, sc.ClassID), 0) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																										       And ClassID = sc.ClassID
																																											   And StudentID = sc.StudentID), 0)) As NeedToPay
  From StudentInClass sc Join Class c
    On sc.ClassID = c.ID
  Where 0 < (Select (Select IFNULL(qryListNeedToPayMoneyByIDs(sc.StudentID, sc.ClassID), 0) + sc.MaterialFee + sc.ApplyFee) - Discount - IFNULL((Select Sum(Paid) From ClassPayment Where StudentInClassID = sc.ID
																																													  And ClassID = sc.ClassID
																																													  And StudentID = sc.StudentID), 0)
             From Class
             Where ID = sc.ClassID)
    And sc.IsDeleted = '0'
  Group By StudentID
  Order By ClassID;

Drop Table IF EXISTS `qryliststaffrole`;
CREATE OR REPLACE VIEW `qryliststaffrole` AS
  Select ID, MatchName
  From   StaffRole;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
