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

-- Dump completed on 2016-04-29 15:04:01
