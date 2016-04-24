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
-- Table structure for table `ai_game_table`
--

DROP TABLE IF EXISTS `ai_game_table`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_game_table` (
  `GameId` int(11) NOT NULL AUTO_INCREMENT,
  `IsReady` bit(1) NOT NULL,
  `DateGamePlayed_CDT` datetime DEFAULT NULL,
  PRIMARY KEY (`GameId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_game_table`
--

LOCK TABLES `ai_game_table` WRITE;
/*!40000 ALTER TABLE `ai_game_table` DISABLE KEYS */;
INSERT INTO `ai_game_table` VALUES (1,'','2016-04-11 17:01:35'),(2,'','2016-04-11 17:01:35'),(3,'','2016-04-11 17:01:35'),(4,'','2016-04-11 17:01:35'),(5,'','2016-04-11 17:01:35'),(6,'','2016-04-11 17:01:35'),(7,'','2016-04-11 17:01:35'),(8,'','2016-04-11 17:01:35'),(9,'','2016-04-11 17:01:35'),(10,'','2016-04-11 17:01:35'),(11,'','2016-04-11 17:01:35'),(12,'','2016-04-11 17:01:35'),(13,'','2016-04-11 17:49:36');
/*!40000 ALTER TABLE `ai_game_table` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-04-21 12:13:31
