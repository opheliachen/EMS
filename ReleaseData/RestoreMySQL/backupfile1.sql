-- MySQL dump 10.13  Distrib 5.1.32, for Win32 (ia32)
--
-- Host: localhost    Database: educatemanagement
-- ------------------------------------------------------
-- Server version	5.1.32-community

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES big5 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Current Database: `educatemanagement`
--

USE `educatemanagement`;

--
-- Dumping data for table `class`
--

LOCK TABLES `class` WRITE;
/*!40000 ALTER TABLE `class` DISABLE KEYS */;
/*!40000 ALTER TABLE `class` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `classcategory`
--

LOCK TABLES `classcategory` WRITE;
/*!40000 ALTER TABLE `classcategory` DISABLE KEYS */;
INSERT INTO `classcategory` VALUES (1,'Math');
/*!40000 ALTER TABLE `classcategory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `classpayment`
--

LOCK TABLES `classpayment` WRITE;
/*!40000 ALTER TABLE `classpayment` DISABLE KEYS */;
/*!40000 ALTER TABLE `classpayment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `classrefunddetail`
--

LOCK TABLES `classrefunddetail` WRITE;
/*!40000 ALTER TABLE `classrefunddetail` DISABLE KEYS */;
/*!40000 ALTER TABLE `classrefunddetail` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `classrefunded`
--

LOCK TABLES `classrefunded` WRITE;
/*!40000 ALTER TABLE `classrefunded` DISABLE KEYS */;
/*!40000 ALTER TABLE `classrefunded` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `classtime`
--

LOCK TABLES `classtime` WRITE;
/*!40000 ALTER TABLE `classtime` DISABLE KEYS */;
/*!40000 ALTER TABLE `classtime` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `companyinfo`
--

LOCK TABLES `companyinfo` WRITE;
/*!40000 ALTER TABLE `companyinfo` DISABLE KEYS */;
/*!40000 ALTER TABLE `companyinfo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `expanse`
--

LOCK TABLES `expanse` WRITE;
/*!40000 ALTER TABLE `expanse` DISABLE KEYS */;
/*!40000 ALTER TABLE `expanse` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `expansecategory`
--

LOCK TABLES `expansecategory` WRITE;
/*!40000 ALTER TABLE `expansecategory` DISABLE KEYS */;
INSERT INTO `expansecategory` VALUES (1,'Food');
/*!40000 ALTER TABLE `expansecategory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `prepaidhistory`
--

LOCK TABLES `prepaidhistory` WRITE;
/*!40000 ALTER TABLE `prepaidhistory` DISABLE KEYS */;
/*!40000 ALTER TABLE `prepaidhistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `qrylistneedtopaystudent`
--

LOCK TABLES `qrylistneedtopaystudent` WRITE;
/*!40000 ALTER TABLE `qrylistneedtopaystudent` DISABLE KEYS */;
/*!40000 ALTER TABLE `qrylistneedtopaystudent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `room`
--

LOCK TABLES `room` WRITE;
/*!40000 ALTER TABLE `room` DISABLE KEYS */;
/*!40000 ALTER TABLE `room` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `sidefunctions`
--

LOCK TABLES `sidefunctions` WRITE;
/*!40000 ALTER TABLE `sidefunctions` DISABLE KEYS */;
/*!40000 ALTER TABLE `sidefunctions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `staff`
--

LOCK TABLES `staff` WRITE;
/*!40000 ALTER TABLE `staff` DISABLE KEYS */;
/*!40000 ALTER TABLE `staff` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `staffaccount`
--

LOCK TABLES `staffaccount` WRITE;
/*!40000 ALTER TABLE `staffaccount` DISABLE KEYS */;
/*!40000 ALTER TABLE `staffaccount` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `staffrole`
--

LOCK TABLES `staffrole` WRITE;
/*!40000 ALTER TABLE `staffrole` DISABLE KEYS */;
/*!40000 ALTER TABLE `staffrole` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `stafftype`
--

LOCK TABLES `stafftype` WRITE;
/*!40000 ALTER TABLE `stafftype` DISABLE KEYS */;
/*!40000 ALTER TABLE `stafftype` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `studentinclass`
--

LOCK TABLES `studentinclass` WRITE;
/*!40000 ALTER TABLE `studentinclass` DISABLE KEYS */;
/*!40000 ALTER TABLE `studentinclass` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping data for table `systemlogs`
--

LOCK TABLES `systemlogs` WRITE;
/*!40000 ALTER TABLE `systemlogs` DISABLE KEYS */;
/*!40000 ALTER TABLE `systemlogs` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2010-08-14 14:20:51
