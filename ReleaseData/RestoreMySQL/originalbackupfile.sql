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

INSERT INTO `classcategory` VALUES (1,'Math');
INSERT INTO `expansecategory` VALUES (1,'Food');
INSERT INTO `staffrole` VALUES (1,'Boss','老闆');
INSERT INTO `staffrole` VALUES (2,'Manager','主任');
INSERT INTO `staffrole` VALUES (3,'Employee','老師');
INSERT INTO `stafftype` VALUES (1,'Employee');
INSERT INTO `staff` VALUES (1,'Owen','Owen','2010-07-01','0000-00-00',0,0,0,'None',1,'0', 1);
INSERT INTO `staff` VALUES (2,'Boss','Boss','2010-07-01','0000-00-00',0,0,0,'None',1,'0', 1);
INSERT INTO `staffaccount` VALUES (1,1,'123111','111111');
INSERT INTO `staffaccount` VALUES (2,2,'123456','111111');