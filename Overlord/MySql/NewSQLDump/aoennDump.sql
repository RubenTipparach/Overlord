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
-- Table structure for table `ai_definition`
--

DROP TABLE IF EXISTS `ai_definition`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_definition` (
  `AIIndex` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(45) NOT NULL,
  `Author` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`AIIndex`),
  KEY `index2` (`AIIndex`,`Name`)
) ENGINE=MyISAM AUTO_INCREMENT=3 DEFAULT CHARSET=latin1 COMMENT='This table represents the AI, their names, and a clustered index to link them to the ai input table.';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_definition`
--

LOCK TABLES `ai_definition` WRITE;
/*!40000 ALTER TABLE `ai_definition` DISABLE KEYS */;
INSERT INTO `ai_definition` VALUES (1,'G4-Coastal-Raiders','AI Scripters'),(2,'G4-Coastal-Raiders_Learnt','Ruben Tipparach');
/*!40000 ALTER TABLE `ai_definition` ENABLE KEYS */;
UNLOCK TABLES;

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
INSERT INTO `ai_economy_feudal_input` VALUES (1,0.2,0.2,0.2,0.2,0.2,1),(2,0.2,0.2,0.2,0.2,0.2,1),(1,0.2,0.2,0.2,0.2,0.2,27),(2,0.24,0.19,0.19,0.19,0.19,27),(1,0.2,0.2,0.2,0.2,0.2,28),(2,0.28,0.18,0.18,0.18,0.18,28),(1,0.2,0.2,0.2,0.2,0.2,29),(2,0.27,0.22,0.17,0.17,0.17,29),(1,0.2,0.2,0.2,0.2,0.2,30),(2,0.26,0.26,0.16,0.16,0.16,30),(1,0.2,0.2,0.2,0.2,0.2,31),(2,0.25,0.3,0.15,0.15,0.15,31),(1,0.2,0.2,0.2,0.2,0.2,32),(2,0.24,0.29,0.14,0.19,0.14,32),(1,0.2,0.2,0.2,0.2,0.2,33),(2,0.23,0.28,0.18,0.18,0.13,33),(1,0.2,0.2,0.2,0.2,0.2,34),(2,0.22,0.32,0.17,0.17,0.12,34),(1,0.2,0.2,0.2,0.2,0.2,35),(2,0.26,0.31,0.16,0.16,0.11,35),(1,0.2,0.2,0.2,0.2,0.2,36),(2,0.25,0.3,0.2,0.15,0.1,36),(1,0.2,0.2,0.2,0.2,0.2,37),(2,0.24,0.29,0.19,0.14,0.14,37),(1,0.2,0.2,0.2,0.2,0.2,38),(2,0.28,0.28,0.18,0.13,0.13,38),(1,0.2,0.2,0.2,0.2,0.2,39),(2,0.27,0.27,0.17,0.12,0.17,39),(1,0.2,0.2,0.2,0.2,0.2,40),(2,0.31,0.26,0.16,0.11,0.16,40),(1,0.2,0.2,0.2,0.2,0.2,41),(2,0.3,0.25,0.2,0.0999999999999999,0.15,41),(1,0.2,0.2,0.2,0.2,0.2,42),(2,0.29,0.24,0.19,0.14,0.14,42),(1,0.2,0.2,0.2,0.2,0.2,43),(2,0.28,0.28,0.18,0.13,0.13,43),(1,0.2,0.2,0.2,0.2,0.2,44),(2,0.27,0.27,0.17,0.12,0.17,44),(1,0.2,0.2,0.2,0.2,0.2,45),(2,0.31,0.26,0.16,0.11,0.16,45),(1,0.2,0.2,0.2,0.2,0.2,46),(2,0.3,0.25,0.15,0.15,0.15,46),(1,0.2,0.2,0.2,0.2,0.2,47),(2,0.29,0.24,0.14,0.14,0.19,47),(1,0.2,0.2,0.2,0.2,0.2,48),(2,0.33,0.23,0.13,0.13,0.18,48),(1,0.2,0.2,0.2,0.2,0.2,49),(2,0.32,0.22,0.12,0.12,0.22,49),(1,0.2,0.2,0.2,0.2,0.2,50),(2,0.36,0.21,0.11,0.11,0.21,50),(1,0.2,0.2,0.2,0.2,0.2,51),(2,0.35,0.2,0.1,0.15,0.2,51),(1,0.2,0.2,0.2,0.2,0.2,52),(2,0.34,0.24,0.09,0.14,0.19,52),(1,0.2,0.2,0.2,0.2,0.2,53),(2,0.33,0.23,0.08,0.13,0.23,53),(1,0.2,0.2,0.2,0.2,0.2,54),(2,0.37,0.22,0.07,0.12,0.22,54),(1,0.2,0.2,0.2,0.2,0.2,55),(2,0.41,0.21,0.06,0.11,0.21,55),(1,0.4,0.33,0.12,0.05,0.1,56),(2,0.4,0.2,0.05,0.15,0.2,56),(1,0.4,0.33,0.12,0.05,0.1,57),(2,0.39,0.19,0.09,0.14,0.19,57),(1,0.4,0.33,0.12,0.05,0.1,58),(2,0.43,0.18,0.08,0.13,0.18,58),(1,0.4,0.33,0.12,0.05,0.1,59),(2,0.42,0.17,0.07,0.17,0.17,59),(1,0.4,0.33,0.12,0.05,0.1,60),(2,0.46,0.16,0.06,0.16,0.16,60),(1,0.4,0.33,0.12,0.05,0.1,61),(2,0.5,0.15,0.05,0.15,0.15,61),(1,0.4,0.33,0.12,0.05,0.1,62),(2,0.49,0.14,0.04,0.14,0.19,62),(1,0.4,0.33,0.12,0.05,0.1,63),(2,0.48,0.13,0.03,0.18,0.18,63),(1,0.4,0.33,0.12,0.05,0.1,64),(2,0.47,0.17,0.02,0.17,0.17,64),(1,0.4,0.33,0.12,0.05,0.1,65),(2,0.46,0.16,0.06,0.16,0.16,65),(1,0.4,0.33,0.12,0.05,0.1,66),(2,0.45,0.15,0.05,0.2,0.15,66),(1,0.4,0.33,0.12,0.05,0.1,67),(2,0.44,0.14,0.09,0.19,0.14,67),(1,0.4,0.33,0.12,0.05,0.1,68),(2,0.43,0.13,0.08,0.18,0.18,68),(1,0.4,0.33,0.12,0.05,0.1,69),(2,0.42,0.12,0.07,0.17,0.22,69),(1,0.4,0.33,0.12,0.05,0.1,70),(2,0.41,0.11,0.06,0.21,0.21,70),(1,0.4,0.33,0.12,0.05,0.1,71),(2,0.4,0.15,0.05,0.2,0.2,71),(1,0.4,0.33,0.12,0.05,0.1,72),(2,0.39,0.14,0.04,0.19,0.24,72),(1,0.4,0.33,0.12,0.05,0.1,73),(2,0.38,0.18,0.03,0.18,0.23,73),(1,0.4,0.33,0.12,0.05,0.1,74),(2,0.37,0.17,0.02,0.17,0.27,74),(1,0.4,0.33,0.12,0.05,0.1,75),(2,0.36,0.16,0.01,0.21,0.26,75);
/*!40000 ALTER TABLE `ai_economy_feudal_input` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ai_economy_feudal_output_raw`
--

DROP TABLE IF EXISTS `ai_economy_feudal_output_raw`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_economy_feudal_output_raw` (
  `AIIndex` int(11) DEFAULT NULL,
  `Food` int(11) DEFAULT NULL,
  `Wood` int(11) DEFAULT NULL,
  `Stone` int(11) DEFAULT NULL,
  `Gold` int(11) DEFAULT NULL,
  `GameId` int(11) DEFAULT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_economy_feudal_output_raw`
--

LOCK TABLES `ai_economy_feudal_output_raw` WRITE;
/*!40000 ALTER TABLE `ai_economy_feudal_output_raw` DISABLE KEYS */;
INSERT INTO `ai_economy_feudal_output_raw` VALUES (1,6094,5596,3252,4008,1),(2,6512,6716,2947,6453,1),(1,7288,6044,3150,5638,27),(2,7043,6074,3150,6559,27),(1,6697,6380,3576,5938,28),(2,6920,4739,3150,6960,28),(1,6385,6122,3511,5682,29),(2,8906,6246,2842,6189,29),(1,7471,6772,3150,5143,30),(2,8570,7760,3318,5357,30),(1,9620,8336,3150,5383,31),(2,7845,8175,2823,4896,31),(1,8914,7361,3150,5597,32),(2,7833,7539,2521,6292,32),(1,9471,7884,3150,5028,33),(2,7067,7579,3070,5683,33),(1,9097,7935,3150,5593,34),(2,6018,7634,3148,4933,34),(1,8837,7319,3150,5901,35),(2,8026,8646,2497,4936,35),(1,7355,7146,3326,5600,36),(2,7066,8640,2677,4350,36),(1,9232,7629,3150,5601,37),(2,8277,8669,3072,4575,37),(1,8406,7035,3150,5704,38),(2,8400,7413,2784,4349,38),(1,7691,7336,3757,5521,39),(2,8503,7865,3178,4651,39),(1,9322,7720,3150,5634,40),(2,10460,8406,3467,4181,40),(1,9109,7563,3150,5595,41),(2,8458,7499,2910,3703,41),(1,8959,7550,3150,5416,42),(2,9827,7049,3319,5229,42),(1,8959,7550,3150,5416,43),(2,9827,7049,3319,5229,43),(1,7678,6770,3458,5807,44),(2,9618,8883,3405,4420,44),(2,9252,8180,3549,4143,45),(1,8800,7467,3150,5379,45),(1,5936,3176,3240,4618,46),(2,8841,7898,2975,4951,46),(1,8906,7144,3150,5612,47),(2,9226,7099,3371,5536,47),(1,6650,5865,3564,5515,48),(2,10067,7049,3205,5029,48),(1,8960,7598,3149,5339,49),(2,11024,8049,3150,4802,49),(1,10026,7698,3150,5123,50),(2,10397,7289,3265,5060,50),(1,5509,5889,3200,5817,51),(2,11523,7565,3382,4865,51),(1,6118,5840,3240,6385,52),(2,11545,8627,3225,4450,52),(1,6579,6234,3590,5923,53),(2,11798,8415,2864,4428,53),(1,7412,6615,3495,6883,54),(2,13354,7923,2291,4309,54),(1,6721,6650,3159,6139,55),(2,13350,7904,2356,3681,55),(1,12759,10130,1763,4149,56),(2,13357,7805,1779,4703,56),(1,11271,10186,1800,4262,57),(2,12657,7120,3150,4573,57),(1,12935,10529,1853,4334,58),(2,13323,6045,2201,4022,58),(1,11647,10021,1766,4586,59),(2,12200,6247,1974,3927,59),(1,11325,10016,1773,4231,60),(2,12903,5703,1568,4779,60),(1,12703,10286,1730,4104,61),(2,13745,4967,1657,4552,61),(1,11898,9426,1750,4098,62),(2,12103,4025,854,2810,62),(1,11814,10371,1739,4214,63),(2,12697,4945,630,5107,63),(1,11641,9800,1667,3754,64),(2,13965,6519,1009,5515,64),(1,12433,10234,1746,4300,65),(2,13346,5078,1353,4379,65),(1,11183,10107,1730,4326,66),(2,13328,5828,1436,4206,66),(1,11133,9652,1750,3942,67),(2,12456,5203,2218,5201,67),(1,12774,10467,1799,4268,68),(2,12158,4888,2579,5221,68),(1,12688,9892,1776,4303,69),(2,11361,4322,2269,4106,69),(1,12049,9922,1762,4204,70),(2,9550,2688,1136,3214,70),(1,12079,9807,1827,4167,71),(2,10962,3929,1788,5882,71),(1,11179,9430,1741,4028,72),(2,12334,5470,1691,5880,72),(1,11278,9943,1750,4115,73),(2,11955,6405,1056,5682,73),(1,11662,9780,1776,4237,74),(2,11960,6417,882,5612,74),(1,11932,9472,1750,4083,75),(2,11662,5986,0,5869,75);
/*!40000 ALTER TABLE `ai_economy_feudal_output_raw` ENABLE KEYS */;
UNLOCK TABLES;

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
) ENGINE=InnoDB AUTO_INCREMENT=78 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_game_table`
--

LOCK TABLES `ai_game_table` WRITE;
/*!40000 ALTER TABLE `ai_game_table` DISABLE KEYS */;
INSERT INTO `ai_game_table` VALUES (1,'','2016-04-11 17:01:35'),(27,'','2016-04-30 13:45:10'),(28,'','2016-04-30 13:50:21'),(29,'','2016-04-30 13:53:49'),(30,'','2016-04-30 13:56:20'),(31,'','2016-04-30 13:59:23'),(32,'','2016-04-30 14:02:31'),(33,'','2016-04-30 14:05:37'),(34,'','2016-04-30 14:08:11'),(35,'','2016-04-30 14:10:42'),(36,'','2016-04-30 14:13:42'),(37,'','2016-04-30 14:21:46'),(38,'','2016-04-30 14:28:40'),(39,'','2016-04-30 14:32:57'),(40,'','2016-04-30 14:35:26'),(41,'','2016-04-30 14:39:05'),(42,'','2016-04-30 14:43:27'),(43,'','2016-04-30 14:53:33'),(44,'','2016-04-30 14:59:19'),(45,'','2016-04-30 15:41:16'),(46,'','2016-04-30 15:44:06'),(47,'','2016-04-30 15:47:05'),(48,'','2016-04-30 15:50:12'),(49,'','2016-04-30 15:53:43'),(50,'','2016-04-30 15:58:42'),(51,'','2016-04-30 16:01:31'),(52,'','2016-04-30 16:08:38'),(53,'','2016-04-30 16:12:40'),(54,'','2016-04-30 16:16:23'),(55,'','2016-04-30 16:22:25'),(56,'','2016-04-30 16:50:24'),(57,'','2016-04-30 16:59:41'),(58,'','2016-04-30 17:03:07'),(59,'','2016-04-30 17:08:31'),(60,'','2016-04-30 17:15:53'),(61,'','2016-04-30 17:38:48'),(62,'','2016-04-30 17:44:03'),(63,'','2016-04-30 17:48:47'),(64,'','2016-04-30 17:52:44'),(65,'','2016-04-30 17:55:50'),(66,'','2016-04-30 18:00:33'),(67,'','2016-04-30 18:03:18'),(68,'','2016-04-30 18:09:12'),(69,'','2016-04-30 18:11:53'),(70,'','2016-04-30 18:15:08'),(71,'','2016-04-30 18:18:35'),(72,'','2016-04-30 18:21:09'),(73,'','2016-04-30 18:26:04'),(74,'','2016-04-30 18:28:59'),(75,'','2016-04-30 18:31:42');
/*!40000 ALTER TABLE `ai_game_table` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary view structure for view `ai_neural_network_feed`
--

DROP TABLE IF EXISTS `ai_neural_network_feed`;
/*!50001 DROP VIEW IF EXISTS `ai_neural_network_feed`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `ai_neural_network_feed` AS SELECT 
 1 AS `GameId`,
 1 AS `p1Index`,
 1 AS `p1Wood`,
 1 AS `p1Food`,
 1 AS `p1Gold`,
 1 AS `p1Stone`,
 1 AS `p1Builders`,
 1 AS `p1WoodHighest`,
 1 AS `p1FoodHighest`,
 1 AS `p1GoldHighest`,
 1 AS `p1StoneHighest`,
 1 AS `p2Index`,
 1 AS `p2Wood`,
 1 AS `p2Food`,
 1 AS `p2Gold`,
 1 AS `p2Stone`,
 1 AS `p2Builders`,
 1 AS `p2WoodHighest`,
 1 AS `p2FoodHighest`,
 1 AS `p2GoldHighest`,
 1 AS `p2StoneHighest`*/;
SET character_set_client = @saved_cs_client;

--
-- Temporary view structure for view `ai_performance_data`
--

DROP TABLE IF EXISTS `ai_performance_data`;
/*!50001 DROP VIEW IF EXISTS `ai_performance_data`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE VIEW `ai_performance_data` AS SELECT 
 1 AS `AIIndex`,
 1 AS `GameId`,
 1 AS `Food_Input`,
 1 AS `Wood_Input`,
 1 AS `Gold_Input`,
 1 AS `Stone_Input`,
 1 AS `Builders_Input`,
 1 AS `Food_Score`,
 1 AS `Wood_Score`,
 1 AS `Gold_Score`,
 1 AS `Stone_Score`*/;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `ai_plotable_data`
--

DROP TABLE IF EXISTS `ai_plotable_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_plotable_data` (
  `DataId` int(11) NOT NULL,
  `X` double NOT NULL,
  `Y` double NOT NULL,
  `Z` double NOT NULL,
  `OrdinalId` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_plotable_data`
--

LOCK TABLES `ai_plotable_data` WRITE;
/*!40000 ALTER TABLE `ai_plotable_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `ai_plotable_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ai_plotable_unnormalized_data`
--

DROP TABLE IF EXISTS `ai_plotable_unnormalized_data`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_plotable_unnormalized_data` (
  `DataId` int(11) NOT NULL,
  `X` double NOT NULL,
  `Y` double NOT NULL,
  `Z1` double NOT NULL,
  `Z2` double NOT NULL,
  `Z3` double NOT NULL,
  `Z4` double NOT NULL,
  `OrdinalId` int(11) NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_plotable_unnormalized_data`
--

LOCK TABLES `ai_plotable_unnormalized_data` WRITE;
/*!40000 ALTER TABLE `ai_plotable_unnormalized_data` DISABLE KEYS */;
/*!40000 ALTER TABLE `ai_plotable_unnormalized_data` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ai_plotset`
--

DROP TABLE IF EXISTS `ai_plotset`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `ai_plotset` (
  `DataId` int(11) NOT NULL,
  `ToleranceLevel` double NOT NULL,
  `AxisX` int(11) NOT NULL,
  `AxisY` int(11) NOT NULL,
  `OrdinalId` int(11) NOT NULL,
  PRIMARY KEY (`DataId`,`OrdinalId`)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ai_plotset`
--

LOCK TABLES `ai_plotset` WRITE;
/*!40000 ALTER TABLE `ai_plotset` DISABLE KEYS */;
/*!40000 ALTER TABLE `ai_plotset` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'aoenn'
--

--
-- Final view structure for view `ai_neural_network_feed`
--

/*!50001 DROP VIEW IF EXISTS `ai_neural_network_feed`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `ai_neural_network_feed` AS select `player1`.`GameId` AS `GameId`,`player1`.`AIIndex` AS `p1Index`,`player1`.`Wood_Input` AS `p1Wood`,`player1`.`Food_Input` AS `p1Food`,`player1`.`Gold_Input` AS `p1Gold`,`player1`.`Stone_Input` AS `p1Stone`,`player1`.`Builders_Input` AS `p1Builders`,(case when (`player1`.`Wood_Score` > `player2`.`Wood_Score`) then 1 else 0 end) AS `p1WoodHighest`,(case when (`player1`.`Food_Score` > `player2`.`Food_Score`) then 1 else 0 end) AS `p1FoodHighest`,(case when (`player1`.`Gold_Score` > `player2`.`Gold_Score`) then 1 else 0 end) AS `p1GoldHighest`,(case when (`player1`.`Stone_Score` > `player2`.`Stone_Score`) then 1 else 0 end) AS `p1StoneHighest`,`player2`.`AIIndex` AS `p2Index`,`player2`.`Wood_Input` AS `p2Wood`,`player2`.`Food_Input` AS `p2Food`,`player2`.`Gold_Input` AS `p2Gold`,`player2`.`Stone_Input` AS `p2Stone`,`player2`.`Builders_Input` AS `p2Builders`,(case when (`player2`.`Wood_Score` > `player1`.`Wood_Score`) then 1 else 0 end) AS `p2WoodHighest`,(case when (`player2`.`Food_Score` > `player1`.`Food_Score`) then 1 else 0 end) AS `p2FoodHighest`,(case when (`player2`.`Gold_Score` > `player1`.`Gold_Score`) then 1 else 0 end) AS `p2GoldHighest`,(case when (`player2`.`Stone_Score` > `player1`.`Stone_Score`) then 1 else 0 end) AS `p2StoneHighest` from (((select `ai_performance_data`.`AIIndex` AS `AIIndex`,`ai_performance_data`.`GameId` AS `GameId`,`ai_performance_data`.`Food_Input` AS `Food_Input`,`ai_performance_data`.`Wood_Input` AS `Wood_Input`,`ai_performance_data`.`Gold_Input` AS `Gold_Input`,`ai_performance_data`.`Stone_Input` AS `Stone_Input`,`ai_performance_data`.`Builders_Input` AS `Builders_Input`,`ai_performance_data`.`Food_Score` AS `Food_Score`,`ai_performance_data`.`Wood_Score` AS `Wood_Score`,`ai_performance_data`.`Gold_Score` AS `Gold_Score`,`ai_performance_data`.`Stone_Score` AS `Stone_Score` from `aoenn`.`ai_performance_data` where (`ai_performance_data`.`AIIndex` = 1))) `player1` join (select `ai_performance_data`.`AIIndex` AS `AIIndex`,`ai_performance_data`.`GameId` AS `GameId`,`ai_performance_data`.`Food_Input` AS `Food_Input`,`ai_performance_data`.`Wood_Input` AS `Wood_Input`,`ai_performance_data`.`Gold_Input` AS `Gold_Input`,`ai_performance_data`.`Stone_Input` AS `Stone_Input`,`ai_performance_data`.`Builders_Input` AS `Builders_Input`,`ai_performance_data`.`Food_Score` AS `Food_Score`,`ai_performance_data`.`Wood_Score` AS `Wood_Score`,`ai_performance_data`.`Gold_Score` AS `Gold_Score`,`ai_performance_data`.`Stone_Score` AS `Stone_Score` from `aoenn`.`ai_performance_data` where (`ai_performance_data`.`AIIndex` = 2)) `player2` on((`player1`.`GameId` = `player2`.`GameId`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `ai_performance_data`
--

/*!50001 DROP VIEW IF EXISTS `ai_performance_data`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `ai_performance_data` AS select `ad`.`AIIndex` AS `AIIndex`,`afin`.`GameId` AS `GameId`,`afin`.`Food` AS `Food_Input`,`afin`.`Wood` AS `Wood_Input`,`afin`.`Gold` AS `Gold_Input`,`afin`.`Stone` AS `Stone_Input`,`afin`.`Builders` AS `Builders_Input`,`afou`.`Food` AS `Food_Score`,`afou`.`Wood` AS `Wood_Score`,`afou`.`Gold` AS `Gold_Score`,`afou`.`Stone` AS `Stone_Score` from ((`ai_definition` `ad` left join `ai_economy_feudal_input` `afin` on((`ad`.`AIIndex` = `afin`.`AIIndex`))) join `ai_economy_feudal_output_raw` `afou` on(((`ad`.`AIIndex` = `afou`.`AIIndex`) and (`afin`.`GameId` = `afou`.`GameId`)))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-05-01 19:53:52
