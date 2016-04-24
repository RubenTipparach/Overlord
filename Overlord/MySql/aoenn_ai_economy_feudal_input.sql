CREATE DATABASE  IF NOT EXISTS `aoenn` /*!40100 DEFAULT CHARACTER SET latin1 */;
USE `aoenn`;
-- MySQL dump 10.13  Distrib 5.7.9, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: aoenn
-- ------------------------------------------------------
-- Server version	5.7.9

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `ai_economy_feudal_input`
--

DROP TABLE IF EXISTS `ai_economy_feudal_input`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_economy_feudal_input` (
  `AIIndex` int(11) DEFAULT NULL,
  `Wood` double DEFAULT NULL,
  `Food` double DEFAULT NULL,
  `Gold` double DEFAULT NULL,
  `Stone` double DEFAULT NULL,
  `Builders` double DEFAULT NULL,
  `GameId` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_economy_feudal_input`
--

LOCK TABLES `ai_economy_feudal_input` WRITE;
/*!40000 ALTER TABLE `ai_economy_feudal_input` DISABLE KEYS */;
INSERT INTO `ai_economy_feudal_input` VALUES (2,0.05,0.05,0.05,0.05,0.05,1),(2,0.1,0.1,0.1,0.1,0.1,2),(2,0.2,0.2,0.2,0.2,0.2,3),(2,0.2,0.2,0.2,0.2,0.2,4),(2,0.3,0.3,0.3,0.3,0.3,5),(2,0.2,0.1,0.1,0.1,0.1,6),(2,0.3,0.1,0.1,0.1,0.1,7),(2,0.5,0.1,0.1,0.1,0.1,8),(2,0.5,0.2,0.1,0.1,0.1,9),(2,0.5,0.3,0.1,0.1,0.1,10),(2,0.5,0.3,0.1,0.05,0.05,11),(2,0.3,0.5,0.1,0.5,0.5,12),(2,0.47,0.41,0.12,0.05,0.1,13),(2,0.47,0.41,0.12,0.05,0.1,14),(2,0.47,0.41,0.12,0.05,0.1,15),(2,0.47,0.41,0.12,0.05,0.1,16),(1,0.47,0.41,0.12,0.05,0.1,1),(1,0.47,0.41,0.12,0.05,0.1,2),(1,0.47,0.41,0.12,0.05,0.1,3),(1,0.47,0.41,0.12,0.05,0.1,4),(1,0.47,0.41,0.12,0.05,0.1,5),(1,0.47,0.41,0.12,0.05,0.1,6),(1,0.47,0.41,0.12,0.05,0.1,7),(1,0.47,0.41,0.12,0.05,0.1,8),(1,0.47,0.41,0.12,0.05,0.1,9),(1,0.47,0.41,0.12,0.05,0.1,10),(1,0.47,0.41,0.12,0.05,0.1,11),(1,0.47,0.41,0.12,0.05,0.1,12),(1,0.47,0.41,0.12,0.05,0.1,13),(1,0.47,0.41,0.12,0.05,0.1,14),(1,0.47,0.41,0.12,0.05,0.1,15),(1,0.47,0.41,0.12,0.05,0.1,16);
/*!40000 ALTER TABLE `ai_economy_feudal_input` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-04-21 12:13:30
