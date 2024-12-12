-- MySQL dump 10.13  Distrib 8.0.38, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: ssap
-- ------------------------------------------------------
-- Server version	8.0.39

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `accounts`
--

DROP TABLE IF EXISTS `accounts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `accounts` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Username` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `PhoneNumber` varchar(100) NOT NULL,
  `HashedPassword` varchar(100) NOT NULL,
  `Address` varchar(100) DEFAULT NULL,
  `AvatarUrl` varchar(1024) DEFAULT NULL,
  `LoginWithGoogle` tinyint(1) NOT NULL,
  `SubscriptionEndDate` datetime(6) DEFAULT NULL,
  `Status` varchar(100) NOT NULL,
  `SubscriptionId` int DEFAULT NULL,
  `RoleId` int NOT NULL,
  `FunderId` int DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Accounts_FunderId` (`FunderId`),
  KEY `IX_Accounts_RoleId` (`RoleId`),
  KEY `IX_Accounts_SubscriptionId` (`SubscriptionId`),
  CONSTRAINT `FK_Accounts_Accounts_FunderId` FOREIGN KEY (`FunderId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Accounts_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Accounts_Subscriptions_SubscriptionId` FOREIGN KEY (`SubscriptionId`) REFERENCES `subscriptions` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `accounts`
--

LOCK TABLES `accounts` WRITE;
/*!40000 ALTER TABLE `accounts` DISABLE KEYS */;
INSERT INTO `accounts` VALUES (1,'system','admin@gmail.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,1,NULL,'2024-12-09 18:43:16.815887','2024-12-09 18:43:16.815887'),(2,'ethan','ethan@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,2,NULL,'2024-12-09 18:43:16.815898','2024-12-09 18:43:16.815898'),(3,'mia','mia@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,2,NULL,'2024-12-09 18:43:16.815907','2024-12-09 18:43:16.815907'),(4,'leo','leo@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,2,NULL,'2024-12-09 18:43:16.815912','2024-12-09 18:43:16.815912'),(5,'zoe','zoe@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,3,2,'2024-12-09 18:43:16.815918','2024-12-09 18:43:16.815918'),(6,'noah','noah@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,3,2,'2024-12-09 18:43:16.815925','2024-12-09 18:43:16.815925'),(7,'ava','ava@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,3,3,'2024-12-09 18:43:16.815931','2024-12-09 18:43:16.815931'),(8,'lucas','lucas@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1733762330/pqz0zt99kupqv8lgj5s6.jpg',0,'2025-02-10 13:37:49.886000','Active',2,4,NULL,NULL,NULL),(9,'lily','lily@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,'2025-01-01 00:00:00.000000','Active',1,4,NULL,'2024-12-09 18:43:16.815973','2024-12-09 18:43:16.815973'),(10,'mason','mason@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,'2025-01-01 00:00:00.000000','Active',1,4,NULL,'2024-12-09 18:43:16.815979','2024-12-09 18:43:16.815979'),(11,'chloe','chloe@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,5,NULL,'2024-12-09 18:43:16.815990','2024-12-09 18:43:16.815990'),(12,'oliver','oliver@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,5,NULL,'2024-12-09 18:43:16.815999','2024-12-09 18:43:16.815999'),(13,'emma','emma@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,5,NULL,'2024-12-09 18:43:16.816006','2024-12-09 18:43:16.816006'),(14,'grace','grace@mailinator.com','0123456789','$2a$12$p6dADeZmY.mViBo4/PNzFedqnfWThJKGponbYf/DVDA6pMbi18nXa','Ho Chi Minh, Vietnam','https://res.cloudinary.com/djiztef3a/image/upload/v1731735243/srsbudawgg7wqbvsswhw',0,NULL,'Active',NULL,5,NULL,'2024-12-09 18:43:16.816012','2024-12-09 18:43:16.816012'),(17,'lamnhse','lamnhse172286@fpt.edu.vn','0902691222','$2a$11$Iwpk36UsFqFZp3bOAjfOaeamJpwXgHnKjj1Fvjd9XQ6zWT3mTtkYy','430/83A Điện Biên Phủ phường 17 quận Bình Thạnh','https://res.cloudinary.com/djiztef3a/image/upload/v1733922697/qkt6cazwc5apfxtbnvql.jpg',0,NULL,'Active',NULL,5,NULL,'2024-12-11 20:11:39.217820','2024-12-11 20:11:39.217819'),(18,'lamhoangnguyen','nguyenhoanglam18112003@gmail.com','0902691233','$2a$11$2kIJTt9kfrd6yQK5r3T84OT90o2gRMN9JNNFs4/zcPUYzD6dgNjFS','430/83A Điện Biên Phủ phường 17 quận Bình Thạnh','https://res.cloudinary.com/djiztef3a/image/upload/v1733922900/dyrhdbxrqnihcwb2qgk8.jpg',0,NULL,'Active',NULL,5,NULL,'2024-12-11 20:15:02.028193','2024-12-11 20:15:02.028192'),(19,'trido','tridmse173029@fpt.edu.vn','0902691244','$2a$11$qtZopqinV.DB0PPTKzL5s.Jf1DDOncCZetzN845JS8b9aO4E40r7.','430/83A Điện Biên Phủ phường 17 quận Bình Thạnh','https://res.cloudinary.com/djiztef3a/image/upload/v1733923106/h0y4mkyothudmneruhx4.jpg',0,'2025-02-11 17:36:17.868000','Active',2,4,NULL,NULL,NULL);
/*!40000 ALTER TABLE `accounts` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `achievements`
--

DROP TABLE IF EXISTS `achievements`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `achievements` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `AchievedDate` datetime(6) DEFAULT NULL,
  `ApplicantProfileId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Achievements_ApplicantProfileId` (`ApplicantProfileId`),
  CONSTRAINT `FK_Achievements_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `achievements`
--

LOCK TABLES `achievements` WRITE;
/*!40000 ALTER TABLE `achievements` DISABLE KEYS */;
/*!40000 ALTER TABLE `achievements` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `aggregatedcounter`
--

DROP TABLE IF EXISTS `aggregatedcounter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `aggregatedcounter` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` int NOT NULL,
  `ExpireAt` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_CounterAggregated_Key` (`Key`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `aggregatedcounter`
--

LOCK TABLES `aggregatedcounter` WRITE;
/*!40000 ALTER TABLE `aggregatedcounter` DISABLE KEYS */;
INSERT INTO `aggregatedcounter` VALUES (1,'stats:succeeded:2024-12-11',3,'2025-01-11 11:57:50'),(2,'stats:succeeded:2024-12-11-11',3,'2024-12-12 11:57:50'),(3,'stats:succeeded',3,NULL);
/*!40000 ALTER TABLE `aggregatedcounter` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `applicant_certificates`
--

DROP TABLE IF EXISTS `applicant_certificates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applicant_certificates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Type` varchar(100) NOT NULL,
  `ImageUrl` varchar(1024) DEFAULT NULL,
  `ApplicantProfileId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_applicant_certificates_ApplicantProfileId` (`ApplicantProfileId`),
  CONSTRAINT `FK_applicant_certificates_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `applicant_certificates`
--

LOCK TABLES `applicant_certificates` WRITE;
/*!40000 ALTER TABLE `applicant_certificates` DISABLE KEYS */;
/*!40000 ALTER TABLE `applicant_certificates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `applicant_profiles`
--

DROP TABLE IF EXISTS `applicant_profiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applicant_profiles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(100) NOT NULL,
  `LastName` varchar(100) NOT NULL,
  `BirthDate` datetime(6) DEFAULT NULL,
  `Gender` varchar(100) DEFAULT NULL,
  `Nationality` varchar(100) DEFAULT NULL,
  `Ethnicity` varchar(100) DEFAULT NULL,
  `Major` varchar(100) DEFAULT NULL,
  `Gpa` double DEFAULT NULL,
  `School` varchar(100) DEFAULT NULL,
  `ApplicantId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_applicant_profiles_ApplicantId` (`ApplicantId`),
  CONSTRAINT `FK_applicant_profiles_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `applicant_profiles`
--

LOCK TABLES `applicant_profiles` WRITE;
/*!40000 ALTER TABLE `applicant_profiles` DISABLE KEYS */;
INSERT INTO `applicant_profiles` VALUES (1,'Chloe','Williams',NULL,'Female',NULL,NULL,NULL,NULL,NULL,11,'2024-12-09 18:43:16.815995','2024-12-09 18:43:16.815995'),(2,'Oliver','Johnson',NULL,'Male',NULL,NULL,NULL,NULL,NULL,12,'2024-12-09 18:43:16.816002','2024-12-09 18:43:16.816002'),(3,'Emma','Doe',NULL,'Female',NULL,NULL,NULL,NULL,NULL,13,'2024-12-09 18:43:16.816008','2024-12-09 18:43:16.816008'),(4,'Grace','Halland',NULL,'Female',NULL,NULL,NULL,NULL,NULL,14,'2024-12-09 18:43:16.816014','2024-12-09 18:43:16.816014'),(5,'Lâm','Hoàng','2003-11-18 00:00:00.000000','Male','Việt Nam','Kinh','SE',4,'FPT',17,'2024-12-11 20:11:47.174523','2024-12-11 20:11:47.174522'),(6,'Lâm','Hoàng','2003-11-18 00:00:00.000000','Male','','','',0,'',18,'2024-12-11 20:15:10.557902','2024-12-11 20:15:10.557902');
/*!40000 ALTER TABLE `applicant_profiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `applicant_skills`
--

DROP TABLE IF EXISTS `applicant_skills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applicant_skills` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `ApplicantProfileId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_applicant_skills_ApplicantProfileId` (`ApplicantProfileId`),
  CONSTRAINT `FK_applicant_skills_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `applicant_skills`
--

LOCK TABLES `applicant_skills` WRITE;
/*!40000 ALTER TABLE `applicant_skills` DISABLE KEYS */;
/*!40000 ALTER TABLE `applicant_skills` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `application_documents`
--

DROP TABLE IF EXISTS `application_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `application_documents` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `FileUrl` varchar(1024) NOT NULL,
  `ApplicationId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_application_documents_ApplicationId` (`ApplicationId`),
  CONSTRAINT `FK_application_documents_Applications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `applications` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `application_documents`
--

LOCK TABLES `application_documents` WRITE;
/*!40000 ALTER TABLE `application_documents` DISABLE KEYS */;
INSERT INTO `application_documents` VALUES (1,'CV','Research Proposal','https://res.cloudinary.com/djiztef3a/raw/upload/v1732444673/rxfpb53czhsxiygsuvwx.pdf',1,'2024-12-09 18:43:16.816526','2024-12-09 18:43:16.816526'),(2,'Schôl','Portfolio','https://res.cloudinary.com/djiztef3a/raw/upload/v1732444721/lg4gpjrejwanbcxiqhww.pdf',2,'2024-12-09 18:43:16.816529','2024-12-09 18:43:16.816529'),(3,'CV','Personal Statement','https://res.cloudinary.com/djiztef3a/raw/upload/v1732444851/wgc2pcyhgctynhjpysww.pdf',3,'2024-12-09 18:43:16.816531','2024-12-09 18:43:16.816531'),(4,'emma','Academic Transcript','https://res.cloudinary.com/djiztef3a/raw/upload/v1732447467/kakxnfdvvelolgnmuv8i.pdf',4,'2024-12-09 18:43:16.816534','2024-12-09 18:43:16.816534'),(5,'grace','CV/Resume','https://res.cloudinary.com/djiztef3a/raw/upload/v1732445775/iple0mwdsoakdzlqoq93.pdf',5,'2024-12-09 18:43:16.816537','2024-12-09 18:43:16.816537'),(6,'oliver','CV/Resume','https://res.cloudinary.com/djiztef3a/raw/upload/v1732445810/aqxoszew6c5nvyuad7r3.pdf',6,'2024-12-09 18:43:16.816544','2024-12-09 18:43:16.816544');
/*!40000 ALTER TABLE `application_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `application_reviews`
--

DROP TABLE IF EXISTS `application_reviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `application_reviews` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Description` varchar(200) DEFAULT NULL,
  `Score` int DEFAULT NULL,
  `Comment` varchar(200) DEFAULT NULL,
  `ReviewDate` datetime(6) NOT NULL,
  `Status` varchar(100) NOT NULL,
  `ExpertId` int NOT NULL,
  `ApplicationId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_application_reviews_ApplicationId` (`ApplicationId`),
  KEY `IX_application_reviews_ExpertId` (`ExpertId`),
  CONSTRAINT `FK_application_reviews_Accounts_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_application_reviews_Applications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `applications` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `application_reviews`
--

LOCK TABLES `application_reviews` WRITE;
/*!40000 ALTER TABLE `application_reviews` DISABLE KEYS */;
/*!40000 ALTER TABLE `application_reviews` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `applications`
--

DROP TABLE IF EXISTS `applications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `applications` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `AppliedDate` datetime(6) NOT NULL,
  `Status` varchar(100) NOT NULL,
  `ApplicantId` int NOT NULL,
  `ScholarshipProgramId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Applications_ApplicantId` (`ApplicantId`),
  KEY `IX_Applications_ScholarshipProgramId` (`ScholarshipProgramId`),
  CONSTRAINT `FK_Applications_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Applications_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `applications`
--

LOCK TABLES `applications` WRITE;
/*!40000 ALTER TABLE `applications` DISABLE KEYS */;
INSERT INTO `applications` VALUES (1,'2024-11-24 17:37:54.000000','Approved',14,5,'2024-12-09 18:43:16.816523','2024-12-09 18:43:16.816523'),(2,'2024-11-24 17:38:42.000000','Approved',13,5,'2024-12-09 18:43:16.816527','2024-12-09 18:43:16.816527'),(3,'2024-11-24 17:40:52.000000','Approved',12,5,'2024-12-09 18:43:16.816530','2024-12-09 18:43:16.816530'),(4,'2024-11-27 17:55:25.000000','NeedExtend',13,6,'2024-12-09 18:43:16.816533','2024-12-09 18:43:16.816533'),(5,'2024-11-27 17:56:16.000000','NeedExtend',14,6,'2024-12-09 18:43:16.816536','2024-12-09 18:43:16.816536'),(6,'2024-11-27 17:56:51.000000','NeedExtend',12,6,'2024-12-09 18:43:16.816542','2024-12-09 18:43:16.816542');
/*!40000 ALTER TABLE `applications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `award_milestone_documents`
--

DROP TABLE IF EXISTS `award_milestone_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `award_milestone_documents` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(100) NOT NULL,
  `AwardMilestoneId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_award_milestone_documents_AwardMilestoneId` (`AwardMilestoneId`),
  CONSTRAINT `FK_award_milestone_documents_award_milestones_AwardMilestoneId` FOREIGN KEY (`AwardMilestoneId`) REFERENCES `award_milestones` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `award_milestone_documents`
--

LOCK TABLES `award_milestone_documents` WRITE;
/*!40000 ALTER TABLE `award_milestone_documents` DISABLE KEYS */;
/*!40000 ALTER TABLE `award_milestone_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `award_milestones`
--

DROP TABLE IF EXISTS `award_milestones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `award_milestones` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FromDate` datetime(6) NOT NULL,
  `ToDate` datetime(6) NOT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `Note` varchar(200) DEFAULT NULL,
  `ScholarshipProgramId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_award_milestones_ScholarshipProgramId` (`ScholarshipProgramId`),
  CONSTRAINT `FK_award_milestones_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `award_milestones`
--

LOCK TABLES `award_milestones` WRITE;
/*!40000 ALTER TABLE `award_milestones` DISABLE KEYS */;
INSERT INTO `award_milestones` VALUES (1,'2024-12-27 13:53:00.000000','2024-12-29 13:54:00.000000',5000.00,NULL,1,'2024-12-09 18:43:16.816477','2024-12-09 18:43:16.816477'),(2,'2025-01-02 13:54:00.000000','2025-01-25 13:54:00.000000',5000.00,NULL,1,'2024-12-09 18:43:16.816479','2024-12-09 18:43:16.816479'),(3,'2025-01-25 13:55:00.000000','2025-01-28 13:56:00.000000',10000.00,NULL,2,'2024-12-09 18:43:16.816488','2024-12-09 18:43:16.816488'),(4,'2025-01-30 13:56:00.000000','2025-02-08 13:56:00.000000',5000.00,NULL,2,'2024-12-09 18:43:16.816490','2024-12-09 18:43:16.816490'),(5,'2024-12-01 13:56:00.000000','2024-12-10 13:57:00.000000',10000.00,NULL,3,'2024-12-09 18:43:16.816494','2024-12-09 18:43:16.816494'),(6,'2024-12-11 13:57:00.000000','2024-12-20 13:57:00.000000',2000.00,NULL,3,'2024-12-09 18:43:16.816496','2024-12-09 18:43:16.816496'),(7,'2024-11-11 17:35:00.000000','2025-11-30 17:35:00.000000',20000.00,NULL,5,'2024-12-09 18:43:16.816506','2024-12-09 18:43:16.816506'),(8,'2025-12-01 17:36:00.000000','2026-12-03 17:36:00.000000',2500.00,NULL,5,'2024-12-09 18:43:16.816507','2024-12-09 18:43:16.816507'),(9,'2026-12-08 17:36:00.000000','2027-12-11 17:36:00.000000',2500.00,NULL,5,'2024-12-09 18:43:16.816509','2024-12-09 18:43:16.816509'),(10,'2023-11-27 17:52:00.000000','2024-11-22 17:52:00.000000',3000.00,NULL,6,'2024-12-09 18:43:16.816517','2024-12-09 18:43:16.816517'),(11,'2024-11-23 19:07:00.000000','2025-12-03 17:52:00.000000',3000.00,NULL,6,'2024-12-09 18:43:16.816518','2024-12-09 18:43:16.816518'),(12,'2025-12-08 17:52:00.000000','2026-12-10 17:52:00.000000',1000.00,NULL,6,'2024-12-09 18:43:16.816520','2024-12-09 18:43:16.816520'),(13,'2025-01-01 13:58:00.000000','2025-01-10 13:58:00.000000',5000.00,NULL,4,'2024-12-09 18:43:16.816500','2024-12-09 18:43:16.816500'),(14,'2025-01-18 13:58:00.000000','2025-01-25 13:58:00.000000',5000.00,NULL,4,'2024-12-09 18:43:16.816502','2024-12-09 18:43:16.816502');
/*!40000 ALTER TABLE `award_milestones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categories`
--

DROP TABLE IF EXISTS `categories`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categories` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categories`
--

LOCK TABLES `categories` WRITE;
/*!40000 ALTER TABLE `categories` DISABLE KEYS */;
INSERT INTO `categories` VALUES (1,'Merit-based','For excellent achievements','2024-12-09 18:43:16.816032','2024-12-09 18:43:16.816032'),(2,'Regional','For people at certain areas','2024-12-09 18:43:16.816033','2024-12-09 18:43:16.816033'),(3,'Minority and Ethnic','For people with certain ethnicity','2024-12-09 18:43:16.816035','2024-12-09 18:43:16.816035');
/*!40000 ALTER TABLE `categories` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `certificates`
--

DROP TABLE IF EXISTS `certificates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `certificates` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Type` varchar(100) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `certificates`
--

LOCK TABLES `certificates` WRITE;
/*!40000 ALTER TABLE `certificates` DISABLE KEYS */;
INSERT INTO `certificates` VALUES (1,'Project Management Professional','Certification for professionals in project management.','Professional','2024-12-09 18:43:16.816463','2024-12-09 18:43:16.816463'),(2,'Certified Information Systems Security Professional','Cybersecurity certification to demonstrate expertise in security management.','Security','2024-12-09 18:43:16.816464','2024-12-09 18:43:16.816464'),(3,'Certified Data Scientist','Certification for individuals proficient in data science and analytics.','Data Science','2024-12-09 18:43:16.816466','2024-12-09 18:43:16.816466'),(4,'Advanced Software Engineering','Certification for expertise in advanced software engineering concepts.','Engineering','2024-12-09 18:43:16.816467','2024-12-09 18:43:16.816467'),(5,'Financial Risk Manager','Certification for finance professionals specializing in risk management.','Finance','2024-12-09 18:43:16.816469','2024-12-09 18:43:16.816469');
/*!40000 ALTER TABLE `certificates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `chats`
--

DROP TABLE IF EXISTS `chats`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `chats` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Message` longtext NOT NULL,
  `SentDate` datetime(6) NOT NULL,
  `SenderId` int NOT NULL,
  `IsRead` tinyint(1) NOT NULL,
  `ReceiverId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Chats_ReceiverId` (`ReceiverId`),
  KEY `IX_Chats_SenderId` (`SenderId`),
  CONSTRAINT `FK_Chats_Accounts_ReceiverId` FOREIGN KEY (`ReceiverId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Chats_Accounts_SenderId` FOREIGN KEY (`SenderId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `chats`
--

LOCK TABLES `chats` WRITE;
/*!40000 ALTER TABLE `chats` DISABLE KEYS */;
INSERT INTO `chats` VALUES (1,'hello provider','2024-12-11 23:25:13.192113',14,1,8,'2024-12-11 23:25:13.205629','2024-12-11 23:25:13.257305'),(2,'hello what\'s wrong?','2024-12-11 23:25:34.285645',8,1,14,'2024-12-11 23:25:34.286291','2024-12-11 23:25:34.329530'),(3,'hi you free? can I talk with you?','2024-12-11 23:52:02.625571',14,1,9,'2024-12-11 23:52:02.626512','2024-12-11 23:52:02.642606');
/*!40000 ALTER TABLE `chats` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `counter`
--

DROP TABLE IF EXISTS `counter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `counter` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` int NOT NULL,
  `ExpireAt` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Counter_Key` (`Key`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `counter`
--

LOCK TABLES `counter` WRITE;
/*!40000 ALTER TABLE `counter` DISABLE KEYS */;
/*!40000 ALTER TABLE `counter` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `countries`
--

DROP TABLE IF EXISTS `countries`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `countries` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Code` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=241 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `countries`
--

LOCK TABLES `countries` WRITE;
/*!40000 ALTER TABLE `countries` DISABLE KEYS */;
INSERT INTO `countries` VALUES (1,'Afghanistan',4,'2024-12-09 18:43:16.816037','2024-12-09 18:43:16.816037'),(2,'Albania',8,'2024-12-09 18:43:16.816038','2024-12-09 18:43:16.816038'),(3,'Algeria',12,'2024-12-09 18:43:16.816040','2024-12-09 18:43:16.816039'),(4,'American Samoa',16,'2024-12-09 18:43:16.816041','2024-12-09 18:43:16.816041'),(5,'Andorra',20,'2024-12-09 18:43:16.816042','2024-12-09 18:43:16.816042'),(6,'Angola',24,'2024-12-09 18:43:16.816043','2024-12-09 18:43:16.816043'),(7,'Anguilla',660,'2024-12-09 18:43:16.816044','2024-12-09 18:43:16.816044'),(8,'Antarctica',10,'2024-12-09 18:43:16.816045','2024-12-09 18:43:16.816045'),(9,'Antigua and Barbuda',28,'2024-12-09 18:43:16.816047','2024-12-09 18:43:16.816047'),(10,'Argentina',32,'2024-12-09 18:43:16.816048','2024-12-09 18:43:16.816048'),(11,'Armenia',51,'2024-12-09 18:43:16.816049','2024-12-09 18:43:16.816049'),(12,'Aruba',533,'2024-12-09 18:43:16.816050','2024-12-09 18:43:16.816050'),(13,'Australia',36,'2024-12-09 18:43:16.816051','2024-12-09 18:43:16.816051'),(14,'Austria',40,'2024-12-09 18:43:16.816052','2024-12-09 18:43:16.816052'),(15,'Azerbaijan',31,'2024-12-09 18:43:16.816057','2024-12-09 18:43:16.816057'),(16,'Bahamas',44,'2024-12-09 18:43:16.816059','2024-12-09 18:43:16.816059'),(17,'Bahrain',48,'2024-12-09 18:43:16.816060','2024-12-09 18:43:16.816060'),(18,'Bangladesh',50,'2024-12-09 18:43:16.816061','2024-12-09 18:43:16.816061'),(19,'Barbados',52,'2024-12-09 18:43:16.816063','2024-12-09 18:43:16.816062'),(20,'Belarus',112,'2024-12-09 18:43:16.816064','2024-12-09 18:43:16.816064'),(21,'Belgium',56,'2024-12-09 18:43:16.816065','2024-12-09 18:43:16.816065'),(22,'Belize',84,'2024-12-09 18:43:16.816066','2024-12-09 18:43:16.816066'),(23,'Benin',204,'2024-12-09 18:43:16.816067','2024-12-09 18:43:16.816067'),(24,'Bermuda',60,'2024-12-09 18:43:16.816068','2024-12-09 18:43:16.816068'),(25,'Bhutan',64,'2024-12-09 18:43:16.816069','2024-12-09 18:43:16.816069'),(26,'Bolivia',68,'2024-12-09 18:43:16.816070','2024-12-09 18:43:16.816070'),(27,'Bosnia and Herzegovina',70,'2024-12-09 18:43:16.816072','2024-12-09 18:43:16.816072'),(28,'Botswana',72,'2024-12-09 18:43:16.816073','2024-12-09 18:43:16.816073'),(29,'Brazil',76,'2024-12-09 18:43:16.816074','2024-12-09 18:43:16.816074'),(30,'British Indian Ocean Territory',86,'2024-12-09 18:43:16.816075','2024-12-09 18:43:16.816075'),(31,'British Virgin Islands',92,'2024-12-09 18:43:16.816076','2024-12-09 18:43:16.816076'),(32,'Brunei',96,'2024-12-09 18:43:16.816078','2024-12-09 18:43:16.816078'),(33,'Bulgaria',100,'2024-12-09 18:43:16.816082','2024-12-09 18:43:16.816082'),(34,'Burkina Faso',854,'2024-12-09 18:43:16.816084','2024-12-09 18:43:16.816084'),(35,'Burundi',108,'2024-12-09 18:43:16.816085','2024-12-09 18:43:16.816085'),(36,'Cambodia',116,'2024-12-09 18:43:16.816086','2024-12-09 18:43:16.816086'),(37,'Cameroon',120,'2024-12-09 18:43:16.816087','2024-12-09 18:43:16.816087'),(38,'Canada',124,'2024-12-09 18:43:16.816089','2024-12-09 18:43:16.816088'),(39,'Cape Verde',132,'2024-12-09 18:43:16.816090','2024-12-09 18:43:16.816090'),(40,'Cayman Islands',136,'2024-12-09 18:43:16.816091','2024-12-09 18:43:16.816091'),(41,'Central African Republic',140,'2024-12-09 18:43:16.816092','2024-12-09 18:43:16.816092'),(42,'Chad',148,'2024-12-09 18:43:16.816093','2024-12-09 18:43:16.816093'),(43,'Chile',152,'2024-12-09 18:43:16.816095','2024-12-09 18:43:16.816094'),(44,'China',156,'2024-12-09 18:43:16.816096','2024-12-09 18:43:16.816096'),(45,'Christmas Island',162,'2024-12-09 18:43:16.816097','2024-12-09 18:43:16.816097'),(46,'Cocos Islands',166,'2024-12-09 18:43:16.816098','2024-12-09 18:43:16.816098'),(47,'Colombia',170,'2024-12-09 18:43:16.816100','2024-12-09 18:43:16.816100'),(48,'Comoros',174,'2024-12-09 18:43:16.816101','2024-12-09 18:43:16.816101'),(49,'Cook Islands',184,'2024-12-09 18:43:16.816103','2024-12-09 18:43:16.816102'),(50,'Costa Rica',188,'2024-12-09 18:43:16.816105','2024-12-09 18:43:16.816105'),(51,'Croatia',191,'2024-12-09 18:43:16.816110','2024-12-09 18:43:16.816110'),(52,'Cuba',192,'2024-12-09 18:43:16.816111','2024-12-09 18:43:16.816111'),(53,'Curacao',531,'2024-12-09 18:43:16.816112','2024-12-09 18:43:16.816112'),(54,'Cyprus',196,'2024-12-09 18:43:16.816114','2024-12-09 18:43:16.816114'),(55,'Czech Republic',203,'2024-12-09 18:43:16.816115','2024-12-09 18:43:16.816115'),(56,'Democratic Republic of the Congo',180,'2024-12-09 18:43:16.816116','2024-12-09 18:43:16.816116'),(57,'Denmark',208,'2024-12-09 18:43:16.816117','2024-12-09 18:43:16.816117'),(58,'Djibouti',262,'2024-12-09 18:43:16.816118','2024-12-09 18:43:16.816118'),(59,'Dominica',212,'2024-12-09 18:43:16.816120','2024-12-09 18:43:16.816120'),(60,'Dominican Republic',214,'2024-12-09 18:43:16.816121','2024-12-09 18:43:16.816121'),(61,'East Timor',626,'2024-12-09 18:43:16.816122','2024-12-09 18:43:16.816122'),(62,'Ecuador',218,'2024-12-09 18:43:16.816123','2024-12-09 18:43:16.816123'),(63,'Egypt',818,'2024-12-09 18:43:16.816124','2024-12-09 18:43:16.816124'),(64,'El Salvador',222,'2024-12-09 18:43:16.816126','2024-12-09 18:43:16.816125'),(65,'Equatorial Guinea',226,'2024-12-09 18:43:16.816127','2024-12-09 18:43:16.816127'),(66,'Eritrea',232,'2024-12-09 18:43:16.816128','2024-12-09 18:43:16.816128'),(67,'Estonia',233,'2024-12-09 18:43:16.816129','2024-12-09 18:43:16.816129'),(68,'Ethiopia',231,'2024-12-09 18:43:16.816130','2024-12-09 18:43:16.816130'),(69,'Falkland Islands',238,'2024-12-09 18:43:16.816135','2024-12-09 18:43:16.816135'),(70,'Faroe Islands',234,'2024-12-09 18:43:16.816137','2024-12-09 18:43:16.816137'),(71,'Fiji',242,'2024-12-09 18:43:16.816138','2024-12-09 18:43:16.816138'),(72,'Finland',246,'2024-12-09 18:43:16.816139','2024-12-09 18:43:16.816139'),(73,'France',250,'2024-12-09 18:43:16.816140','2024-12-09 18:43:16.816140'),(74,'French Polynesia',258,'2024-12-09 18:43:16.816141','2024-12-09 18:43:16.816141'),(75,'Gabon',266,'2024-12-09 18:43:16.816142','2024-12-09 18:43:16.816142'),(76,'Gambia',270,'2024-12-09 18:43:16.816144','2024-12-09 18:43:16.816144'),(77,'Georgia',268,'2024-12-09 18:43:16.816145','2024-12-09 18:43:16.816145'),(78,'Germany',276,'2024-12-09 18:43:16.816146','2024-12-09 18:43:16.816146'),(79,'Ghana',288,'2024-12-09 18:43:16.816147','2024-12-09 18:43:16.816147'),(80,'Gibraltar',292,'2024-12-09 18:43:16.816148','2024-12-09 18:43:16.816148'),(81,'Greece',300,'2024-12-09 18:43:16.816149','2024-12-09 18:43:16.816149'),(82,'Greenland',304,'2024-12-09 18:43:16.816150','2024-12-09 18:43:16.816150'),(83,'Grenada',308,'2024-12-09 18:43:16.816152','2024-12-09 18:43:16.816152'),(84,'Guam',316,'2024-12-09 18:43:16.816153','2024-12-09 18:43:16.816153'),(85,'Guatemala',320,'2024-12-09 18:43:16.816154','2024-12-09 18:43:16.816154'),(86,'Guernsey',831,'2024-12-09 18:43:16.816155','2024-12-09 18:43:16.816155'),(87,'Guinea',324,'2024-12-09 18:43:16.816160','2024-12-09 18:43:16.816160'),(88,'Guinea-Bissau',624,'2024-12-09 18:43:16.816161','2024-12-09 18:43:16.816161'),(89,'Guyana',328,'2024-12-09 18:43:16.816162','2024-12-09 18:43:16.816162'),(90,'Haiti',332,'2024-12-09 18:43:16.816163','2024-12-09 18:43:16.816163'),(91,'Honduras',340,'2024-12-09 18:43:16.816164','2024-12-09 18:43:16.816164'),(92,'Hong Kong',344,'2024-12-09 18:43:16.816165','2024-12-09 18:43:16.816165'),(93,'Hungary',348,'2024-12-09 18:43:16.816167','2024-12-09 18:43:16.816167'),(94,'Iceland',352,'2024-12-09 18:43:16.816168','2024-12-09 18:43:16.816168'),(95,'India',356,'2024-12-09 18:43:16.816169','2024-12-09 18:43:16.816169'),(96,'Indonesia',360,'2024-12-09 18:43:16.816170','2024-12-09 18:43:16.816170'),(97,'Iran',364,'2024-12-09 18:43:16.816171','2024-12-09 18:43:16.816171'),(98,'Iraq',368,'2024-12-09 18:43:16.816172','2024-12-09 18:43:16.816172'),(99,'Ireland',372,'2024-12-09 18:43:16.816174','2024-12-09 18:43:16.816173'),(100,'Isle of Man',833,'2024-12-09 18:43:16.816175','2024-12-09 18:43:16.816175'),(101,'Israel',376,'2024-12-09 18:43:16.816176','2024-12-09 18:43:16.816176'),(102,'Italy',380,'2024-12-09 18:43:16.816177','2024-12-09 18:43:16.816177'),(103,'Ivory Coast',384,'2024-12-09 18:43:16.816178','2024-12-09 18:43:16.816178'),(104,'Jamaica',388,'2024-12-09 18:43:16.816180','2024-12-09 18:43:16.816180'),(105,'Japan',392,'2024-12-09 18:43:16.816183','2024-12-09 18:43:16.816183'),(106,'Jersey',832,'2024-12-09 18:43:16.816185','2024-12-09 18:43:16.816184'),(107,'Jordan',400,'2024-12-09 18:43:16.816186','2024-12-09 18:43:16.816186'),(108,'Kazakhstan',398,'2024-12-09 18:43:16.816187','2024-12-09 18:43:16.816187'),(109,'Kenya',404,'2024-12-09 18:43:16.816188','2024-12-09 18:43:16.816188'),(110,'Kiribati',296,'2024-12-09 18:43:16.816189','2024-12-09 18:43:16.816189'),(111,'Kosovo',0,'2024-12-09 18:43:16.816190','2024-12-09 18:43:16.816190'),(112,'Kuwait',414,'2024-12-09 18:43:16.816191','2024-12-09 18:43:16.816191'),(113,'Kyrgyzstan',417,'2024-12-09 18:43:16.816192','2024-12-09 18:43:16.816192'),(114,'Laos',418,'2024-12-09 18:43:16.816194','2024-12-09 18:43:16.816194'),(115,'Latvia',428,'2024-12-09 18:43:16.816195','2024-12-09 18:43:16.816195'),(116,'Lebanon',422,'2024-12-09 18:43:16.816197','2024-12-09 18:43:16.816196'),(117,'Lesotho',426,'2024-12-09 18:43:16.816198','2024-12-09 18:43:16.816198'),(118,'Liberia',430,'2024-12-09 18:43:16.816199','2024-12-09 18:43:16.816199'),(119,'Libya',434,'2024-12-09 18:43:16.816200','2024-12-09 18:43:16.816200'),(120,'Liechtenstein',438,'2024-12-09 18:43:16.816201','2024-12-09 18:43:16.816201'),(121,'Lithuania',440,'2024-12-09 18:43:16.816203','2024-12-09 18:43:16.816203'),(122,'Luxembourg',442,'2024-12-09 18:43:16.816204','2024-12-09 18:43:16.816204'),(123,'Macau',446,'2024-12-09 18:43:16.816211','2024-12-09 18:43:16.816211'),(124,'Macedonia',807,'2024-12-09 18:43:16.816212','2024-12-09 18:43:16.816212'),(125,'Madagascar',450,'2024-12-09 18:43:16.816213','2024-12-09 18:43:16.816213'),(126,'Malawi',454,'2024-12-09 18:43:16.816214','2024-12-09 18:43:16.816214'),(127,'Malaysia',458,'2024-12-09 18:43:16.816216','2024-12-09 18:43:16.816216'),(128,'Maldives',462,'2024-12-09 18:43:16.816217','2024-12-09 18:43:16.816217'),(129,'Mali',466,'2024-12-09 18:43:16.816218','2024-12-09 18:43:16.816218'),(130,'Malta',470,'2024-12-09 18:43:16.816219','2024-12-09 18:43:16.816219'),(131,'Marshall Islands',584,'2024-12-09 18:43:16.816221','2024-12-09 18:43:16.816221'),(132,'Mauritania',478,'2024-12-09 18:43:16.816222','2024-12-09 18:43:16.816222'),(133,'Mauritius',480,'2024-12-09 18:43:16.816223','2024-12-09 18:43:16.816223'),(134,'Mayotte',175,'2024-12-09 18:43:16.816224','2024-12-09 18:43:16.816224'),(135,'Mexico',484,'2024-12-09 18:43:16.816225','2024-12-09 18:43:16.816225'),(136,'Micronesia',583,'2024-12-09 18:43:16.816227','2024-12-09 18:43:16.816227'),(137,'Moldova',498,'2024-12-09 18:43:16.816228','2024-12-09 18:43:16.816228'),(138,'Monaco',492,'2024-12-09 18:43:16.816229','2024-12-09 18:43:16.816229'),(139,'Mongolia',496,'2024-12-09 18:43:16.816230','2024-12-09 18:43:16.816230'),(140,'Montenegro',499,'2024-12-09 18:43:16.816231','2024-12-09 18:43:16.816231'),(141,'Montserrat',500,'2024-12-09 18:43:16.816238','2024-12-09 18:43:16.816238'),(142,'Morocco',504,'2024-12-09 18:43:16.816239','2024-12-09 18:43:16.816239'),(143,'Mozambique',508,'2024-12-09 18:43:16.816240','2024-12-09 18:43:16.816240'),(144,'Myanmar',104,'2024-12-09 18:43:16.816241','2024-12-09 18:43:16.816241'),(145,'Namibia',516,'2024-12-09 18:43:16.816243','2024-12-09 18:43:16.816243'),(146,'Nauru',520,'2024-12-09 18:43:16.816244','2024-12-09 18:43:16.816244'),(147,'Nepal',524,'2024-12-09 18:43:16.816245','2024-12-09 18:43:16.816245'),(148,'Netherlands',528,'2024-12-09 18:43:16.816246','2024-12-09 18:43:16.816246'),(149,'Netherlands Antilles',530,'2024-12-09 18:43:16.816248','2024-12-09 18:43:16.816248'),(150,'New Caledonia',540,'2024-12-09 18:43:16.816249','2024-12-09 18:43:16.816249'),(151,'New Zealand',554,'2024-12-09 18:43:16.816250','2024-12-09 18:43:16.816250'),(152,'Nicaragua',558,'2024-12-09 18:43:16.816251','2024-12-09 18:43:16.816251'),(153,'Niger',562,'2024-12-09 18:43:16.816252','2024-12-09 18:43:16.816252'),(154,'Nigeria',566,'2024-12-09 18:43:16.816253','2024-12-09 18:43:16.816253'),(155,'Niue',570,'2024-12-09 18:43:16.816255','2024-12-09 18:43:16.816255'),(156,'North Korea',408,'2024-12-09 18:43:16.816256','2024-12-09 18:43:16.816256'),(157,'Northern Mariana Islands',580,'2024-12-09 18:43:16.816257','2024-12-09 18:43:16.816257'),(158,'Norway',578,'2024-12-09 18:43:16.816258','2024-12-09 18:43:16.816258'),(159,'Oman',512,'2024-12-09 18:43:16.816268','2024-12-09 18:43:16.816268'),(160,'Pakistan',586,'2024-12-09 18:43:16.816269','2024-12-09 18:43:16.816269'),(161,'Palau',585,'2024-12-09 18:43:16.816271','2024-12-09 18:43:16.816271'),(162,'Palestine',275,'2024-12-09 18:43:16.816272','2024-12-09 18:43:16.816272'),(163,'Panama',591,'2024-12-09 18:43:16.816273','2024-12-09 18:43:16.816273'),(164,'Papua New Guinea',598,'2024-12-09 18:43:16.816274','2024-12-09 18:43:16.816274'),(165,'Paraguay',600,'2024-12-09 18:43:16.816276','2024-12-09 18:43:16.816276'),(166,'Peru',604,'2024-12-09 18:43:16.816277','2024-12-09 18:43:16.816277'),(167,'Philippines',608,'2024-12-09 18:43:16.816278','2024-12-09 18:43:16.816278'),(168,'Pitcairn',612,'2024-12-09 18:43:16.816279','2024-12-09 18:43:16.816279'),(169,'Poland',616,'2024-12-09 18:43:16.816281','2024-12-09 18:43:16.816280'),(170,'Portugal',620,'2024-12-09 18:43:16.816282','2024-12-09 18:43:16.816282'),(171,'Puerto Rico',630,'2024-12-09 18:43:16.816283','2024-12-09 18:43:16.816283'),(172,'Qatar',634,'2024-12-09 18:43:16.816284','2024-12-09 18:43:16.816284'),(173,'Republic of the Congo',178,'2024-12-09 18:43:16.816285','2024-12-09 18:43:16.816285'),(174,'Reunion',638,'2024-12-09 18:43:16.816286','2024-12-09 18:43:16.816286'),(175,'Romania',642,'2024-12-09 18:43:16.816287','2024-12-09 18:43:16.816287'),(176,'Russia',643,'2024-12-09 18:43:16.816289','2024-12-09 18:43:16.816288'),(177,'Rwanda',646,'2024-12-09 18:43:16.816294','2024-12-09 18:43:16.816294'),(178,'Saint Barthelemy',652,'2024-12-09 18:43:16.816295','2024-12-09 18:43:16.816295'),(179,'Saint Helena',654,'2024-12-09 18:43:16.816296','2024-12-09 18:43:16.816296'),(180,'Saint Kitts and Nevis',659,'2024-12-09 18:43:16.816297','2024-12-09 18:43:16.816297'),(181,'Saint Lucia',662,'2024-12-09 18:43:16.816298','2024-12-09 18:43:16.816298'),(182,'Saint Martin',663,'2024-12-09 18:43:16.816300','2024-12-09 18:43:16.816300'),(183,'Saint Pierre and Miquelon',666,'2024-12-09 18:43:16.816301','2024-12-09 18:43:16.816301'),(184,'Saint Vincent and the Grenadines',670,'2024-12-09 18:43:16.816302','2024-12-09 18:43:16.816302'),(185,'Samoa',882,'2024-12-09 18:43:16.816303','2024-12-09 18:43:16.816303'),(186,'San Marino',674,'2024-12-09 18:43:16.816304','2024-12-09 18:43:16.816304'),(187,'Sao Tome and Principe',678,'2024-12-09 18:43:16.816306','2024-12-09 18:43:16.816306'),(188,'Saudi Arabia',682,'2024-12-09 18:43:16.816307','2024-12-09 18:43:16.816307'),(189,'Senegal',686,'2024-12-09 18:43:16.816308','2024-12-09 18:43:16.816308'),(190,'Serbia',688,'2024-12-09 18:43:16.816309','2024-12-09 18:43:16.816309'),(191,'Seychelles',690,'2024-12-09 18:43:16.816310','2024-12-09 18:43:16.816310'),(192,'Sierra Leone',694,'2024-12-09 18:43:16.816312','2024-12-09 18:43:16.816312'),(193,'Singapore',702,'2024-12-09 18:43:16.816313','2024-12-09 18:43:16.816313'),(194,'Sint Maarten',534,'2024-12-09 18:43:16.816314','2024-12-09 18:43:16.816314'),(195,'Slovakia',703,'2024-12-09 18:43:16.816319','2024-12-09 18:43:16.816319'),(196,'Slovenia',705,'2024-12-09 18:43:16.816320','2024-12-09 18:43:16.816320'),(197,'Solomon Islands',90,'2024-12-09 18:43:16.816321','2024-12-09 18:43:16.816321'),(198,'Somalia',706,'2024-12-09 18:43:16.816323','2024-12-09 18:43:16.816323'),(199,'South Africa',710,'2024-12-09 18:43:16.816324','2024-12-09 18:43:16.816324'),(200,'South Korea',410,'2024-12-09 18:43:16.816325','2024-12-09 18:43:16.816325'),(201,'South Sudan',728,'2024-12-09 18:43:16.816326','2024-12-09 18:43:16.816326'),(202,'Spain',724,'2024-12-09 18:43:16.816328','2024-12-09 18:43:16.816328'),(203,'Sri Lanka',144,'2024-12-09 18:43:16.816329','2024-12-09 18:43:16.816329'),(204,'Sudan',729,'2024-12-09 18:43:16.816330','2024-12-09 18:43:16.816330'),(205,'Suriname',740,'2024-12-09 18:43:16.816331','2024-12-09 18:43:16.816331'),(206,'Svalbard and Jan Mayen',744,'2024-12-09 18:43:16.816332','2024-12-09 18:43:16.816332'),(207,'Swaziland',748,'2024-12-09 18:43:16.816333','2024-12-09 18:43:16.816333'),(208,'Sweden',752,'2024-12-09 18:43:16.816334','2024-12-09 18:43:16.816334'),(209,'Switzerland',756,'2024-12-09 18:43:16.816336','2024-12-09 18:43:16.816336'),(210,'Syria',760,'2024-12-09 18:43:16.816337','2024-12-09 18:43:16.816337'),(211,'Taiwan',158,'2024-12-09 18:43:16.816338','2024-12-09 18:43:16.816338'),(212,'Tajikistan',762,'2024-12-09 18:43:16.816339','2024-12-09 18:43:16.816339'),(213,'Tanzania',834,'2024-12-09 18:43:16.816344','2024-12-09 18:43:16.816344'),(214,'Thailand',764,'2024-12-09 18:43:16.816345','2024-12-09 18:43:16.816345'),(215,'Togo',768,'2024-12-09 18:43:16.816346','2024-12-09 18:43:16.816346'),(216,'Tokelau',772,'2024-12-09 18:43:16.816347','2024-12-09 18:43:16.816347'),(217,'Tonga',776,'2024-12-09 18:43:16.816349','2024-12-09 18:43:16.816349'),(218,'Trinidad and Tobago',780,'2024-12-09 18:43:16.816350','2024-12-09 18:43:16.816350'),(219,'Tunisia',788,'2024-12-09 18:43:16.816351','2024-12-09 18:43:16.816351'),(220,'Turkey',792,'2024-12-09 18:43:16.816352','2024-12-09 18:43:16.816352'),(221,'Turkmenistan',795,'2024-12-09 18:43:16.816353','2024-12-09 18:43:16.816353'),(222,'Turks and Caicos Islands',796,'2024-12-09 18:43:16.816354','2024-12-09 18:43:16.816354'),(223,'Tuvalu',798,'2024-12-09 18:43:16.816356','2024-12-09 18:43:16.816356'),(224,'U.S. Virgin Islands',850,'2024-12-09 18:43:16.816357','2024-12-09 18:43:16.816357'),(225,'Uganda',800,'2024-12-09 18:43:16.816358','2024-12-09 18:43:16.816358'),(226,'Ukraine',804,'2024-12-09 18:43:16.816360','2024-12-09 18:43:16.816360'),(227,'United Arab Emirates',784,'2024-12-09 18:43:16.816361','2024-12-09 18:43:16.816361'),(228,'United Kingdom',826,'2024-12-09 18:43:16.816362','2024-12-09 18:43:16.816362'),(229,'United States',840,'2024-12-09 18:43:16.816363','2024-12-09 18:43:16.816363'),(230,'Uruguay',858,'2024-12-09 18:43:16.816364','2024-12-09 18:43:16.816364'),(231,'Uzbekistan',860,'2024-12-09 18:43:16.816369','2024-12-09 18:43:16.816369'),(232,'Vanuatu',548,'2024-12-09 18:43:16.816370','2024-12-09 18:43:16.816370'),(233,'Vatican',336,'2024-12-09 18:43:16.816371','2024-12-09 18:43:16.816371'),(234,'Venezuela',862,'2024-12-09 18:43:16.816372','2024-12-09 18:43:16.816372'),(235,'Vietnam',704,'2024-12-09 18:43:16.816373','2024-12-09 18:43:16.816373'),(236,'Wallis and Futuna',876,'2024-12-09 18:43:16.816374','2024-12-09 18:43:16.816374'),(237,'Western Sahara',732,'2024-12-09 18:43:16.816376','2024-12-09 18:43:16.816375'),(238,'Yemen',887,'2024-12-09 18:43:16.816377','2024-12-09 18:43:16.816377'),(239,'Zambia',894,'2024-12-09 18:43:16.816378','2024-12-09 18:43:16.816378'),(240,'Zimbabwe',716,'2024-12-09 18:43:16.816379','2024-12-09 18:43:16.816379');
/*!40000 ALTER TABLE `countries` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `criteria`
--

DROP TABLE IF EXISTS `criteria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `criteria` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `ScholarshipProgramId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Criteria_ScholarshipProgramId` (`ScholarshipProgramId`),
  CONSTRAINT `FK_Criteria_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `criteria`
--

LOCK TABLES `criteria` WRITE;
/*!40000 ALTER TABLE `criteria` DISABLE KEYS */;
/*!40000 ALTER TABLE `criteria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `distributedlock`
--

DROP TABLE IF EXISTS `distributedlock`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `distributedlock` (
  `Resource` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CreatedAt` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `distributedlock`
--

LOCK TABLES `distributedlock` WRITE;
/*!40000 ALTER TABLE `distributedlock` DISABLE KEYS */;
/*!40000 ALTER TABLE `distributedlock` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `experiences`
--

DROP TABLE IF EXISTS `experiences`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `experiences` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `ApplicantProfileId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Experiences_ApplicantProfileId` (`ApplicantProfileId`),
  CONSTRAINT `FK_Experiences_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `experiences`
--

LOCK TABLES `experiences` WRITE;
/*!40000 ALTER TABLE `experiences` DISABLE KEYS */;
/*!40000 ALTER TABLE `experiences` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `expert_profiles`
--

DROP TABLE IF EXISTS `expert_profiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `expert_profiles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Major` varchar(100) NOT NULL,
  `ExpertId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_expert_profiles_ExpertId` (`ExpertId`),
  CONSTRAINT `FK_expert_profiles_Accounts_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `expert_profiles`
--

LOCK TABLES `expert_profiles` WRITE;
/*!40000 ALTER TABLE `expert_profiles` DISABLE KEYS */;
INSERT INTO `expert_profiles` VALUES (1,'Zoe','Expert','STEM (Science, Technology, Engineering, and Mathematics)',5,'2024-12-09 18:43:16.815921','2024-12-09 18:43:16.815921'),(2,'Zoe','Expert','STEM (Science, Technology, Engineering, and Mathematics)',6,'2024-12-09 18:43:16.815927','2024-12-09 18:43:16.815927'),(3,'Zoe','Expert','Business & Economics',7,'2024-12-09 18:43:16.815933','2024-12-09 18:43:16.815933');
/*!40000 ALTER TABLE `expert_profiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `feedbacks`
--

DROP TABLE IF EXISTS `feedbacks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `feedbacks` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Content` varchar(200) NOT NULL,
  `Rating` double NOT NULL,
  `FeedbackDate` datetime(6) NOT NULL,
  `ApplicantId` int NOT NULL,
  `ServiceId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Feedbacks_ApplicantId` (`ApplicantId`),
  KEY `IX_Feedbacks_ServiceId` (`ServiceId`),
  CONSTRAINT `FK_Feedbacks_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Feedbacks_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feedbacks`
--

LOCK TABLES `feedbacks` WRITE;
/*!40000 ALTER TABLE `feedbacks` DISABLE KEYS */;
INSERT INTO `feedbacks` VALUES (1,'This service is very perfect',5,'2024-12-11 16:26:18.541000',14,2,'2024-12-11 23:26:18.614545','2024-12-11 23:26:18.614544'),(2,'This service really bad',1,'2024-12-11 16:42:43.250000',14,12,'2024-12-11 23:42:43.271253','2024-12-11 23:42:43.271252'),(3,'Perfect ',4,'2024-12-11 17:01:30.849000',14,33,'2024-12-12 00:01:30.866401','2024-12-12 00:01:30.866400');
/*!40000 ALTER TABLE `feedbacks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `funder_documents`
--

DROP TABLE IF EXISTS `funder_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `funder_documents` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `FileUrl` varchar(1024) NOT NULL,
  `FunderProfileId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_funder_documents_FunderProfileId` (`FunderProfileId`),
  CONSTRAINT `FK_funder_documents_funder_profiles_FunderProfileId` FOREIGN KEY (`FunderProfileId`) REFERENCES `funder_profiles` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `funder_documents`
--

LOCK TABLES `funder_documents` WRITE;
/*!40000 ALTER TABLE `funder_documents` DISABLE KEYS */;
/*!40000 ALTER TABLE `funder_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `funder_profiles`
--

DROP TABLE IF EXISTS `funder_profiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `funder_profiles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrganizationName` varchar(100) NOT NULL,
  `ContactPersonName` varchar(100) DEFAULT NULL,
  `FunderId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_funder_profiles_FunderId` (`FunderId`),
  CONSTRAINT `FK_funder_profiles_Accounts_FunderId` FOREIGN KEY (`FunderId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `funder_profiles`
--

LOCK TABLES `funder_profiles` WRITE;
/*!40000 ALTER TABLE `funder_profiles` DISABLE KEYS */;
INSERT INTO `funder_profiles` VALUES (1,'Coca Cola','Ethan',2,'2024-12-09 18:43:16.815901','2024-12-09 18:43:16.815901'),(2,'Pepsi','Mia',3,'2024-12-09 18:43:16.815908','2024-12-09 18:43:16.815908'),(3,'Full Bright Org','Leo',4,'2024-12-09 18:43:16.815914','2024-12-09 18:43:16.815914');
/*!40000 ALTER TABLE `funder_profiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `hash`
--

DROP TABLE IF EXISTS `hash`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `hash` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Field` varchar(40) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` longtext,
  `ExpireAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Hash_Key_Field` (`Key`,`Field`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `hash`
--

LOCK TABLES `hash` WRITE;
/*!40000 ALTER TABLE `hash` DISABLE KEYS */;
INSERT INTO `hash` VALUES (1,'recurring-job:ScheduleScholarshipsAfterDeadline','Queue','default',NULL),(2,'recurring-job:ScheduleScholarshipsAfterDeadline','Cron','0 0 * * *',NULL),(3,'recurring-job:ScheduleScholarshipsAfterDeadline','TimeZoneId','UTC',NULL),(4,'recurring-job:ScheduleScholarshipsAfterDeadline','Job','{\"t\":\"Infrastructure.ExternalServices.Background.BackgroundService, Infrastructure\",\"m\":\"ScheduleScholarshipsAfterDeadline\"}',NULL),(5,'recurring-job:ScheduleScholarshipsAfterDeadline','CreatedAt','1733832539984',NULL),(6,'recurring-job:ScheduleScholarshipsAfterDeadline','NextExecution','1733961600000',NULL),(7,'recurring-job:ScheduleScholarshipsAfterDeadline','V','2',NULL),(8,'recurring-job:ScheduleApplicationsNeedExtend','Queue','default',NULL),(9,'recurring-job:ScheduleApplicationsNeedExtend','Cron','0 0 * * *',NULL),(10,'recurring-job:ScheduleApplicationsNeedExtend','TimeZoneId','UTC',NULL),(11,'recurring-job:ScheduleApplicationsNeedExtend','Job','{\"t\":\"Infrastructure.ExternalServices.Background.BackgroundService, Infrastructure\",\"m\":\"ScheduleApplicationsNeedExtend\"}',NULL),(12,'recurring-job:ScheduleApplicationsNeedExtend','CreatedAt','1733832540095',NULL),(13,'recurring-job:ScheduleApplicationsNeedExtend','NextExecution','1733961600000',NULL),(14,'recurring-job:ScheduleApplicationsNeedExtend','V','2',NULL),(15,'recurring-job:ScheduleApplicationsNeedExtend','LastExecution','1733918239644',NULL),(17,'recurring-job:ScheduleApplicationsNeedExtend','LastJobId','1',NULL),(18,'recurring-job:ScheduleScholarshipsAfterDeadline','LastExecution','1733918239644',NULL),(20,'recurring-job:ScheduleScholarshipsAfterDeadline','LastJobId','2',NULL);
/*!40000 ALTER TABLE `hash` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `job`
--

DROP TABLE IF EXISTS `job`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `job` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `StateId` int DEFAULT NULL,
  `StateName` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `InvocationData` longtext NOT NULL,
  `Arguments` longtext NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `ExpireAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Job_StateName` (`StateName`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `job`
--

LOCK TABLES `job` WRITE;
/*!40000 ALTER TABLE `job` DISABLE KEYS */;
INSERT INTO `job` VALUES (1,6,'Succeeded','{\"Type\":\"Infrastructure.ExternalServices.Background.BackgroundService, Infrastructure\",\"Method\":\"ScheduleApplicationsNeedExtend\",\"ParameterTypes\":\"[]\",\"Arguments\":\"[]\"}','[]','2024-12-11 11:57:19.689190','2024-12-12 11:57:34.867538'),(2,10,'Succeeded','{\"Type\":\"Infrastructure.ExternalServices.Background.BackgroundService, Infrastructure\",\"Method\":\"ScheduleScholarshipsAfterDeadline\",\"ParameterTypes\":\"[]\",\"Arguments\":\"[]\"}','[]','2024-12-11 11:57:19.806215','2024-12-12 11:57:35.003096'),(3,5,'Scheduled','{\"Type\":\"Infrastructure.Repositories.ScholarshipProgramRepository, Infrastructure\",\"Method\":\"Update\",\"ParameterTypes\":\"[\\\"Domain.Entities.ScholarshipProgram, Domain\\\"]\",\"Arguments\":\"[\\\"{\\\\\\\"Name\\\\\\\":\\\\\\\"Global Excellence Scholarship\\\\\\\",\\\\\\\"ImageUrl\\\\\\\":\\\\\\\"https://example.com/images/global_excellence.jpg\\\\\\\",\\\\\\\"Description\\\\\\\":\\\\\\\"Awarded to outstanding students demonstrating academic excellence and leadership potential.\\\\\\\",\\\\\\\"ScholarshipAmount\\\\\\\":10000.00,\\\\\\\"NumberOfAwardMilestones\\\\\\\":3,\\\\\\\"NumberOfScholarships\\\\\\\":5,\\\\\\\"Deadline\\\\\\\":\\\\\\\"2024-12-15T23:59:59\\\\\\\",\\\\\\\"Status\\\\\\\":\\\\\\\"Reviewing\\\\\\\",\\\\\\\"FunderId\\\\\\\":1,\\\\\\\"CategoryId\\\\\\\":2,\\\\\\\"UniversityId\\\\\\\":3,\\\\\\\"MajorId\\\\\\\":5,\\\\\\\"Id\\\\\\\":1,\\\\\\\"CreatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816474\\\\\\\",\\\\\\\"UpdatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816474\\\\\\\"}\\\"]\"}','[\"{\\\"Name\\\":\\\"Global Excellence Scholarship\\\",\\\"ImageUrl\\\":\\\"https://example.com/images/global_excellence.jpg\\\",\\\"Description\\\":\\\"Awarded to outstanding students demonstrating academic excellence and leadership potential.\\\",\\\"ScholarshipAmount\\\":10000.00,\\\"NumberOfAwardMilestones\\\":3,\\\"NumberOfScholarships\\\":5,\\\"Deadline\\\":\\\"2024-12-15T23:59:59\\\",\\\"Status\\\":\\\"Reviewing\\\",\\\"FunderId\\\":1,\\\"CategoryId\\\":2,\\\"UniversityId\\\":3,\\\"MajorId\\\":5,\\\"Id\\\":1,\\\"CreatedAt\\\":\\\"2024-12-09T18:43:16.816474\\\",\\\"UpdatedAt\\\":\\\"2024-12-09T18:43:16.816474\\\"}\"]','2024-12-11 11:57:34.822127',NULL),(4,7,'Scheduled','{\"Type\":\"Infrastructure.Repositories.ScholarshipProgramRepository, Infrastructure\",\"Method\":\"Update\",\"ParameterTypes\":\"[\\\"Domain.Entities.ScholarshipProgram, Domain\\\"]\",\"Arguments\":\"[\\\"{\\\\\\\"Name\\\\\\\":\\\\\\\"Future Innovators Fellowship\\\\\\\",\\\\\\\"ImageUrl\\\\\\\":\\\\\\\"https://example.com/images/future_innovators.jpg\\\\\\\",\\\\\\\"Description\\\\\\\":\\\\\\\"Supporting aspiring scientists and engineers working on innovative projects.\\\\\\\",\\\\\\\"ScholarshipAmount\\\\\\\":15000.00,\\\\\\\"NumberOfAwardMilestones\\\\\\\":3,\\\\\\\"NumberOfScholarships\\\\\\\":3,\\\\\\\"Deadline\\\\\\\":\\\\\\\"2025-01-20T23:59:59\\\\\\\",\\\\\\\"Status\\\\\\\":\\\\\\\"Reviewing\\\\\\\",\\\\\\\"FunderId\\\\\\\":2,\\\\\\\"CategoryId\\\\\\\":1,\\\\\\\"UniversityId\\\\\\\":4,\\\\\\\"MajorId\\\\\\\":2,\\\\\\\"Id\\\\\\\":2,\\\\\\\"CreatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816487\\\\\\\",\\\\\\\"UpdatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816487\\\\\\\"}\\\"]\"}','[\"{\\\"Name\\\":\\\"Future Innovators Fellowship\\\",\\\"ImageUrl\\\":\\\"https://example.com/images/future_innovators.jpg\\\",\\\"Description\\\":\\\"Supporting aspiring scientists and engineers working on innovative projects.\\\",\\\"ScholarshipAmount\\\":15000.00,\\\"NumberOfAwardMilestones\\\":3,\\\"NumberOfScholarships\\\":3,\\\"Deadline\\\":\\\"2025-01-20T23:59:59\\\",\\\"Status\\\":\\\"Reviewing\\\",\\\"FunderId\\\":2,\\\"CategoryId\\\":1,\\\"UniversityId\\\":4,\\\"MajorId\\\":2,\\\"Id\\\":2,\\\"CreatedAt\\\":\\\"2024-12-09T18:43:16.816487\\\",\\\"UpdatedAt\\\":\\\"2024-12-09T18:43:16.816487\\\"}\"]','2024-12-11 11:57:34.878687',NULL),(5,13,'Succeeded','{\"Type\":\"Infrastructure.Repositories.ScholarshipProgramRepository, Infrastructure\",\"Method\":\"Update\",\"ParameterTypes\":\"[\\\"Domain.Entities.ScholarshipProgram, Domain\\\"]\",\"Arguments\":\"[\\\"{\\\\\\\"Name\\\\\\\":\\\\\\\"Global Women in STEM Award\\\\\\\",\\\\\\\"ImageUrl\\\\\\\":\\\\\\\"https://example.com/images/women_in_stem.jpg\\\\\\\",\\\\\\\"Description\\\\\\\":\\\\\\\"Encouraging women to pursue careers in STEM fields through financial aid and mentorship.\\\\\\\",\\\\\\\"ScholarshipAmount\\\\\\\":12000.00,\\\\\\\"NumberOfAwardMilestones\\\\\\\":3,\\\\\\\"NumberOfScholarships\\\\\\\":10,\\\\\\\"Deadline\\\\\\\":\\\\\\\"2024-11-30T23:59:59\\\\\\\",\\\\\\\"Status\\\\\\\":\\\\\\\"Reviewing\\\\\\\",\\\\\\\"FunderId\\\\\\\":3,\\\\\\\"CategoryId\\\\\\\":3,\\\\\\\"UniversityId\\\\\\\":2,\\\\\\\"MajorId\\\\\\\":7,\\\\\\\"Id\\\\\\\":3,\\\\\\\"CreatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816492\\\\\\\",\\\\\\\"UpdatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816492\\\\\\\"}\\\"]\"}','[\"{\\\"Name\\\":\\\"Global Women in STEM Award\\\",\\\"ImageUrl\\\":\\\"https://example.com/images/women_in_stem.jpg\\\",\\\"Description\\\":\\\"Encouraging women to pursue careers in STEM fields through financial aid and mentorship.\\\",\\\"ScholarshipAmount\\\":12000.00,\\\"NumberOfAwardMilestones\\\":3,\\\"NumberOfScholarships\\\":10,\\\"Deadline\\\":\\\"2024-11-30T23:59:59\\\",\\\"Status\\\":\\\"Reviewing\\\",\\\"FunderId\\\":3,\\\"CategoryId\\\":3,\\\"UniversityId\\\":2,\\\"MajorId\\\":7,\\\"Id\\\":3,\\\"CreatedAt\\\":\\\"2024-12-09T18:43:16.816492\\\",\\\"UpdatedAt\\\":\\\"2024-12-09T18:43:16.816492\\\"}\"]','2024-12-11 11:57:34.925822','2024-12-12 11:57:50.166890'),(6,9,'Scheduled','{\"Type\":\"Infrastructure.Repositories.ScholarshipProgramRepository, Infrastructure\",\"Method\":\"Update\",\"ParameterTypes\":\"[\\\"Domain.Entities.ScholarshipProgram, Domain\\\"]\",\"Arguments\":\"[\\\"{\\\\\\\"Name\\\\\\\":\\\\\\\"Global Change-Maker Scholarship\\\\\\\",\\\\\\\"ImageUrl\\\\\\\":\\\\\\\"https://example.com/images/change_maker.jpg\\\\\\\",\\\\\\\"Description\\\\\\\":\\\\\\\"Recognizing students with initiatives addressing global challenges.\\\\\\\",\\\\\\\"ScholarshipAmount\\\\\\\":10000.00,\\\\\\\"NumberOfAwardMilestones\\\\\\\":3,\\\\\\\"NumberOfScholarships\\\\\\\":8,\\\\\\\"Deadline\\\\\\\":\\\\\\\"2024-12-25T23:59:59\\\\\\\",\\\\\\\"Status\\\\\\\":\\\\\\\"Reviewing\\\\\\\",\\\\\\\"FunderId\\\\\\\":6,\\\\\\\"CategoryId\\\\\\\":2,\\\\\\\"UniversityId\\\\\\\":5,\\\\\\\"MajorId\\\\\\\":8,\\\\\\\"Id\\\\\\\":4,\\\\\\\"CreatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816499\\\\\\\",\\\\\\\"UpdatedAt\\\\\\\":\\\\\\\"2024-12-09T18:43:16.816499\\\\\\\"}\\\"]\"}','[\"{\\\"Name\\\":\\\"Global Change-Maker Scholarship\\\",\\\"ImageUrl\\\":\\\"https://example.com/images/change_maker.jpg\\\",\\\"Description\\\":\\\"Recognizing students with initiatives addressing global challenges.\\\",\\\"ScholarshipAmount\\\":10000.00,\\\"NumberOfAwardMilestones\\\":3,\\\"NumberOfScholarships\\\":8,\\\"Deadline\\\":\\\"2024-12-25T23:59:59\\\",\\\"Status\\\":\\\"Reviewing\\\",\\\"FunderId\\\":6,\\\"CategoryId\\\":2,\\\"UniversityId\\\":5,\\\"MajorId\\\":8,\\\"Id\\\":4,\\\"CreatedAt\\\":\\\"2024-12-09T18:43:16.816499\\\",\\\"UpdatedAt\\\":\\\"2024-12-09T18:43:16.816499\\\"}\"]','2024-12-11 11:57:34.957520',NULL);
/*!40000 ALTER TABLE `job` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jobparameter`
--

DROP TABLE IF EXISTS `jobparameter`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jobparameter` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `JobId` int NOT NULL,
  `Name` varchar(40) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` longtext,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_JobParameter_JobId_Name` (`JobId`,`Name`),
  KEY `FK_JobParameter_Job` (`JobId`),
  CONSTRAINT `FK_JobParameter_Job` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jobparameter`
--

LOCK TABLES `jobparameter` WRITE;
/*!40000 ALTER TABLE `jobparameter` DISABLE KEYS */;
INSERT INTO `jobparameter` VALUES (1,1,'RecurringJobId','\"ScheduleApplicationsNeedExtend\"'),(2,1,'Time','1733918239'),(3,1,'CurrentCulture','\"en-US\"'),(4,2,'RecurringJobId','\"ScheduleScholarshipsAfterDeadline\"'),(5,2,'Time','1733918239'),(6,2,'CurrentCulture','\"en-US\"'),(7,3,'CurrentCulture','\"en-US\"'),(8,4,'CurrentCulture','\"en-US\"'),(9,5,'CurrentCulture','\"en-US\"'),(10,6,'CurrentCulture','\"en-US\"');
/*!40000 ALTER TABLE `jobparameter` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jobqueue`
--

DROP TABLE IF EXISTS `jobqueue`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jobqueue` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `JobId` int NOT NULL,
  `FetchedAt` datetime(6) DEFAULT NULL,
  `Queue` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `FetchToken` varchar(36) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_JobQueue_QueueAndFetchedAt` (`Queue`,`FetchedAt`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jobqueue`
--

LOCK TABLES `jobqueue` WRITE;
/*!40000 ALTER TABLE `jobqueue` DISABLE KEYS */;
/*!40000 ALTER TABLE `jobqueue` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `jobstate`
--

DROP TABLE IF EXISTS `jobstate`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `jobstate` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `JobId` int NOT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Reason` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Data` longtext,
  PRIMARY KEY (`Id`),
  KEY `FK_JobState_Job` (`JobId`),
  CONSTRAINT `FK_JobState_Job` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `jobstate`
--

LOCK TABLES `jobstate` WRITE;
/*!40000 ALTER TABLE `jobstate` DISABLE KEYS */;
/*!40000 ALTER TABLE `jobstate` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `list`
--

DROP TABLE IF EXISTS `list`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `list` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` longtext,
  `ExpireAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `list`
--

LOCK TABLES `list` WRITE;
/*!40000 ALTER TABLE `list` DISABLE KEYS */;
/*!40000 ALTER TABLE `list` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `major_skills`
--

DROP TABLE IF EXISTS `major_skills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `major_skills` (
  `MajorId` int NOT NULL,
  `SkillId` int NOT NULL,
  PRIMARY KEY (`MajorId`,`SkillId`),
  KEY `IX_major_skills_SkillId` (`SkillId`),
  CONSTRAINT `FK_major_skills_Majors_MajorId` FOREIGN KEY (`MajorId`) REFERENCES `majors` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_major_skills_Skills_SkillId` FOREIGN KEY (`SkillId`) REFERENCES `skills` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `major_skills`
--

LOCK TABLES `major_skills` WRITE;
/*!40000 ALTER TABLE `major_skills` DISABLE KEYS */;
INSERT INTO `major_skills` VALUES (1,1),(2,1),(3,1),(4,1),(5,1),(1,2),(2,2),(3,2),(4,2),(5,2);
/*!40000 ALTER TABLE `major_skills` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `majors`
--

DROP TABLE IF EXISTS `majors`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `majors` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `ParentMajorId` int DEFAULT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Majors_ParentMajorId` (`ParentMajorId`),
  CONSTRAINT `FK_Majors_Majors_ParentMajorId` FOREIGN KEY (`ParentMajorId`) REFERENCES `majors` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `majors`
--

LOCK TABLES `majors` WRITE;
/*!40000 ALTER TABLE `majors` DISABLE KEYS */;
INSERT INTO `majors` VALUES (1,'STEM (Science, Technology, Engineering, and Mathematics)','Majors related to scientific, technological, engineering, and mathematical fields.',NULL,'2024-12-09 18:43:16.816435','2024-12-09 18:43:16.816435'),(2,'Business & Economics','Majors focused on the study of business, finance, economics, and management.',NULL,'2024-12-09 18:43:16.816437','2024-12-09 18:43:16.816437'),(3,'Health & Medicine','Majors related to the study of health, medicine, and wellness fields.',NULL,'2024-12-09 18:43:16.816438','2024-12-09 18:43:16.816438'),(4,'Social Sciences & Humanities','Majors in disciplines that study human society, behavior, and culture.',NULL,'2024-12-09 18:43:16.816440','2024-12-09 18:43:16.816440'),(5,'Arts & Media','Majors focused on creative and performing arts, as well as media, journalism, and digital communication.',NULL,'2024-12-09 18:43:16.816441','2024-12-09 18:43:16.816441'),(6,'Engineering','Majors related to the study of various types of engineering.',1,'2024-12-09 18:43:16.816443','2024-12-09 18:43:16.816443'),(7,'Computer Science','Majors related to computer systems, software, and algorithms.',1,'2024-12-09 18:43:16.816444','2024-12-09 18:43:16.816444'),(8,'Business Administration','Study of managing and overseeing business operations.',2,'2024-12-09 18:43:16.816451','2024-12-09 18:43:16.816451'),(9,'Economics','Focuses on the production, consumption, and distribution of goods and services.',2,'2024-12-09 18:43:16.816452','2024-12-09 18:43:16.816452'),(10,'Nursing','Focuses on providing healthcare to individuals and communities.',3,'2024-12-09 18:43:16.816454','2024-12-09 18:43:16.816454'),(11,'Medical Sciences','Deals with the scientific study of medicine and healthcare.',3,'2024-12-09 18:43:16.816455','2024-12-09 18:43:16.816455'),(12,'Psychology','Study of the mind and behavior.',4,'2024-12-09 18:43:16.816456','2024-12-09 18:43:16.816456'),(13,'Sociology','Study of society, social behavior, and institutions.',4,'2024-12-09 18:43:16.816457','2024-12-09 18:43:16.816457'),(14,'Fine Arts','Study of visual and performing arts including painting, sculpture, and design.',5,'2024-12-09 18:43:16.816459','2024-12-09 18:43:16.816459'),(15,'Media Studies','Focuses on communication, journalism, and digital media.',5,'2024-12-09 18:43:16.816460','2024-12-09 18:43:16.816460');
/*!40000 ALTER TABLE `majors` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notifications`
--

DROP TABLE IF EXISTS `notifications`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `notifications` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Message` longtext NOT NULL,
  `IsRead` tinyint(1) NOT NULL,
  `SentDate` datetime(6) NOT NULL,
  `ReceiverId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Notifications_ReceiverId` (`ReceiverId`),
  CONSTRAINT `FK_Notifications_Accounts_ReceiverId` FOREIGN KEY (`ReceiverId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notifications`
--

LOCK TABLES `notifications` WRITE;
/*!40000 ALTER TABLE `notifications` DISABLE KEYS */;
INSERT INTO `notifications` VALUES (1,'You have successfully purchased the subscription package \'Intermediate\'.',0,'2024-12-10 20:37:50.614749',8,'2024-12-10 20:37:50.632157','2024-12-10 20:37:50.632156'),(4,'hoanglamne has registered.',0,'2024-12-11 00:57:55.554444',1,'2024-12-11 00:57:55.554673','2024-12-11 00:57:55.554672'),(8,'Update your profile now.',0,'2024-12-11 20:11:39.975781',17,'2024-12-11 20:11:39.976071','2024-12-11 20:11:39.976070'),(9,'Check out our scholarship program.',0,'2024-12-11 20:11:40.476940',17,'2024-12-11 20:11:40.477292','2024-12-11 20:11:40.477291'),(10,'lamnhse has registered.',0,'2024-12-11 20:11:44.030014',1,'2024-12-11 20:11:44.030213','2024-12-11 20:11:44.030213'),(11,'Update your profile now.',0,'2024-12-11 20:15:02.814552',18,'2024-12-11 20:15:02.814771','2024-12-11 20:15:02.814770'),(12,'Check out our scholarship program.',0,'2024-12-11 20:15:03.427986',18,'2024-12-11 20:15:03.428270','2024-12-11 20:15:03.428269'),(13,'lamhoangnguyen has registered.',0,'2024-12-11 20:15:07.000873',1,'2024-12-11 20:15:07.001271','2024-12-11 20:15:07.001269'),(14,'Update your profile now.',0,'2024-12-11 20:18:30.220476',19,'2024-12-11 20:18:30.221286','2024-12-11 20:18:30.221286'),(15,'Check out our scholarship program.',0,'2024-12-11 20:18:30.766275',19,'2024-12-11 20:18:30.766592','2024-12-11 20:18:30.766591'),(16,'trido has registered.',0,'2024-12-11 20:18:34.919432',1,'2024-12-11 20:18:34.919728','2024-12-11 20:18:34.919727'),(17,'Provider has commented to your Service\'s request Scholarship Document Translation.',1,'2024-12-11 23:24:37.770940',14,'2024-12-11 23:24:37.817606','2024-12-12 00:02:38.457161'),(18,'grace has requested to Scholarship Document Translation.',0,'2024-12-11 23:36:00.914173',8,'2024-12-11 23:36:00.928964','2024-12-11 23:36:00.928962'),(19,'You have requested to service Scholarship Document Translation.',1,'2024-12-11 23:36:03.972752',14,'2024-12-11 23:36:03.978885','2024-12-12 00:02:38.454703'),(20,'grace has requested to Scholarship Application Critique.',0,'2024-12-11 23:37:57.643735',10,'2024-12-11 23:37:57.644399','2024-12-11 23:37:57.644398'),(21,'You have requested to service Scholarship Application Critique.',1,'2024-12-11 23:38:00.791698',14,'2024-12-11 23:38:00.792705','2024-12-12 00:02:38.446496'),(22,'Provider has commented to your Service\'s request Scholarship Application Critique.',1,'2024-12-11 23:39:50.652659',14,'2024-12-11 23:39:50.653019','2024-12-12 00:02:38.433806'),(23,'grace has requested to CV Tailoring for Scholarships.',0,'2024-12-11 23:44:06.287586',10,'2024-12-11 23:44:06.287894','2024-12-11 23:44:06.287893'),(24,'You have requested to service CV Tailoring for Scholarships.',1,'2024-12-11 23:44:09.289180',14,'2024-12-11 23:44:09.289439','2024-12-12 00:02:38.434850'),(25,'grace has requested to Scholarship Success Workshop.',0,'2024-12-11 23:50:23.205960',9,'2024-12-11 23:50:23.206318','2024-12-11 23:50:23.206317'),(26,'You have requested to service Scholarship Success Workshop.',1,'2024-12-11 23:50:26.087809',14,'2024-12-11 23:50:26.088166','2024-12-12 00:02:38.436208'),(27,'Provider has commented to your Service\'s request Scholarship Success Workshop.',1,'2024-12-11 23:51:02.099027',14,'2024-12-11 23:51:02.099355','2024-12-12 00:02:38.435260'),(28,'Provider has commented to your Service\'s request Scholarship Essay Feedback.',1,'2024-12-11 23:53:46.667593',14,'2024-12-11 23:53:46.667929','2024-12-12 00:02:38.432748'),(29,'grace has requested to Scholarship Readiness Assessment.',0,'2024-12-11 23:55:56.223665',9,'2024-12-11 23:55:56.223942','2024-12-11 23:55:56.223941'),(30,'You have requested to service Scholarship Readiness Assessment.',1,'2024-12-11 23:55:59.917080',14,'2024-12-11 23:55:59.917349','2024-12-12 00:02:38.404807'),(31,'Provider has commented to your Service\'s request CV Tailoring for Scholarships.',1,'2024-12-12 00:00:45.305476',14,'2024-12-12 00:00:45.305894','2024-12-12 00:00:56.531823'),(32,'grace has requested to Scholarship Readiness Assessment.',0,'2024-12-12 00:04:37.209105',9,'2024-12-12 00:04:37.209902','2024-12-12 00:04:37.209900'),(33,'You have requested to service Scholarship Readiness Assessment.',0,'2024-12-12 00:04:40.416484',14,'2024-12-12 00:04:40.416756','2024-12-12 00:04:40.416756'),(34,'Provider has commented to your Service\'s request Scholarship Readiness Assessment.',0,'2024-12-12 00:05:17.789214',14,'2024-12-12 00:05:17.789548','2024-12-12 00:05:17.789547'),(35,'Provider has commented to your Service\'s request Scholarship Readiness Assessment.',0,'2024-12-12 00:05:41.879029',14,'2024-12-12 00:05:41.879241','2024-12-12 00:05:41.879240'),(37,'Your account has been approved, please log in again to use more features. Thank you.',0,'2024-12-12 00:34:11.588974',19,'2024-12-12 00:34:11.590155','2024-12-12 00:34:11.590154'),(38,'You have successfully purchased the subscription package \'Intermediate\'.',0,'2024-12-12 00:36:18.600221',19,'2024-12-12 00:36:18.601296','2024-12-12 00:36:18.601295'),(39,'lamnhse has requested to Impactful CV Design Service.',0,'2024-12-12 00:40:16.988488',19,'2024-12-12 00:40:16.990463','2024-12-12 00:40:16.990463'),(40,'You have requested to service Impactful CV Design Service.',0,'2024-12-12 00:40:20.020592',17,'2024-12-12 00:40:20.021611','2024-12-12 00:40:20.021610');
/*!40000 ALTER TABLE `notifications` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `provider_documents`
--

DROP TABLE IF EXISTS `provider_documents`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `provider_documents` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Type` varchar(100) NOT NULL,
  `FileUrl` varchar(1024) NOT NULL,
  `ProviderProfileId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_provider_documents_ProviderProfileId` (`ProviderProfileId`),
  CONSTRAINT `FK_provider_documents_provider_profiles_ProviderProfileId` FOREIGN KEY (`ProviderProfileId`) REFERENCES `provider_profiles` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `provider_documents`
--

LOCK TABLES `provider_documents` WRITE;
/*!40000 ALTER TABLE `provider_documents` DISABLE KEYS */;
INSERT INTO `provider_documents` VALUES (1,'Provider\'s certificate','Curriculum Vitae (CV) of Lead Instructor','https://res.cloudinary.com/djiztef3a/raw/upload/v1733923108/dgxt8bsksxltn5kpy6y1.avif',4,'2024-12-11 20:18:29.531448','2024-12-11 20:18:29.531448');
/*!40000 ALTER TABLE `provider_documents` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `provider_profiles`
--

DROP TABLE IF EXISTS `provider_profiles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `provider_profiles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `OrganizationName` varchar(100) NOT NULL,
  `ContactPersonName` varchar(100) DEFAULT NULL,
  `ProviderId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_provider_profiles_ProviderId` (`ProviderId`),
  CONSTRAINT `FK_provider_profiles_Accounts_ProviderId` FOREIGN KEY (`ProviderId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `provider_profiles`
--

LOCK TABLES `provider_profiles` WRITE;
/*!40000 ALTER TABLE `provider_profiles` DISABLE KEYS */;
INSERT INTO `provider_profiles` VALUES (1,'IDP','Lucas',8,'2024-12-09 18:43:16.815941','2024-12-09 18:43:16.815941'),(2,'Consulting Org','Lily',9,'2024-12-09 18:43:16.815974','2024-12-09 18:43:16.815974'),(3,'America Study Abroad Comp','Mason',10,'2024-12-09 18:43:16.815980','2024-12-09 18:43:16.815980'),(4,'FPT Software','Trí Chill',19,'2024-12-11 20:18:29.531446','2024-12-11 20:18:29.531445');
/*!40000 ALTER TABLE `provider_profiles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_detail_files`
--

DROP TABLE IF EXISTS `request_detail_files`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `request_detail_files` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `FileUrl` varchar(1024) NOT NULL,
  `UploadedBy` varchar(100) NOT NULL,
  `UploadDate` datetime(6) NOT NULL,
  `RequestDetailId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_request_detail_files_RequestDetailId` (`RequestDetailId`),
  CONSTRAINT `FK_request_detail_files_request_details_RequestDetailId` FOREIGN KEY (`RequestDetailId`) REFERENCES `request_details` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_detail_files`
--

LOCK TABLES `request_detail_files` WRITE;
/*!40000 ALTER TABLE `request_detail_files` DISABLE KEYS */;
INSERT INTO `request_detail_files` VALUES (1,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733745243/stwyrmcail0j9ltmbmkx.avif','Applicant','2024-12-09 18:54:03.669396',1,'2024-12-09 18:54:03.703825','2024-12-09 18:54:03.703824'),(2,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733745404/pndsmmeenmsyqoilbjf4.jpg','Applicant','2024-12-09 18:56:44.766381',2,'2024-12-09 18:56:44.766729','2024-12-09 18:56:44.766729'),(3,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733745486/qs6diy4lld8sy3q8cmpn.jpg','Applicant','2024-12-09 18:58:07.259746',3,'2024-12-09 18:58:07.259975','2024-12-09 18:58:07.259975'),(4,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733934959/ych9fwvcuxqmeofcdzyw.jpg','Applicant','2024-12-11 23:35:59.924185',4,'2024-12-11 23:35:59.933950','2024-12-11 23:35:59.933950'),(5,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733935076/iz4geiahqibntav8snml.pdf','Applicant','2024-12-11 23:37:56.730266',5,'2024-12-11 23:37:56.731396','2024-12-11 23:37:56.731395'),(6,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733935189/hmh8hf7pi61ryaaxi3ni.jpg','Provider','2024-12-11 23:39:49.848699',5,'2024-12-11 23:39:49.866815','2024-12-11 23:39:49.866814'),(7,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733935445/gbrr8zt3yzpuhgv07ylf.jpg','Applicant','2024-12-11 23:44:05.549761',6,'2024-12-11 23:44:05.550112','2024-12-11 23:44:05.550112'),(8,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733935822/rkq9sto9qwhftlsk8wwe.pdf','Applicant','2024-12-11 23:50:22.464643',7,'2024-12-11 23:50:22.465430','2024-12-11 23:50:22.465430'),(9,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733935861/x4qnzqxdumkw2ptd6n9t.pdf','Provider','2024-12-11 23:51:01.390271',7,'2024-12-11 23:51:01.390877','2024-12-11 23:51:01.390876'),(10,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733936025/pqtpw9c58ydpy6mf2k10.jpg','Provider','2024-12-11 23:53:45.966725',3,'2024-12-11 23:53:45.966969','2024-12-11 23:53:45.966968'),(11,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733936155/i49udr3wj4ufgsdw8pff.jpg','Applicant','2024-12-11 23:55:55.435267',8,'2024-12-11 23:55:55.435664','2024-12-11 23:55:55.435664'),(12,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733936676/jkihw9niqu9vorjcrqbs.jpg','Applicant','2024-12-12 00:04:36.450331',9,'2024-12-12 00:04:36.450687','2024-12-12 00:04:36.450687'),(13,'https://res.cloudinary.com/djiztef3a/raw/upload/v1733938815/nuc23u1xwrtelxxqqxbd.jpg','Applicant','2024-12-12 00:40:16.108397',10,'2024-12-12 00:40:16.126644','2024-12-12 00:40:16.126644');
/*!40000 ALTER TABLE `request_detail_files` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `request_details`
--

DROP TABLE IF EXISTS `request_details`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `request_details` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Comment` varchar(200) DEFAULT NULL,
  `RequestId` int NOT NULL,
  `ServiceId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_request_details_RequestId` (`RequestId`),
  KEY `IX_request_details_ServiceId` (`ServiceId`),
  CONSTRAINT `FK_request_details_Requests_RequestId` FOREIGN KEY (`RequestId`) REFERENCES `requests` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_request_details_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `services` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `request_details`
--

LOCK TABLES `request_details` WRITE;
/*!40000 ALTER TABLE `request_details` DISABLE KEYS */;
INSERT INTO `request_details` VALUES (1,'Ok your document perfect, now you can apply scholarship',1,2,'2024-12-09 18:54:03.703823','2024-12-11 23:24:36.884982'),(2,NULL,2,1,'2024-12-09 18:56:44.766728','2024-12-09 18:56:44.766728'),(3,'Detailed financial aid options for your program are now available.',3,3,'2024-12-09 18:58:07.259974','2024-12-11 23:53:45.973867'),(4,NULL,4,2,'2024-12-11 23:35:59.933947','2024-12-11 23:35:59.933947'),(5,'Your profile need to fix',5,12,'2024-12-11 23:37:56.731392','2024-12-11 23:39:49.881742'),(6,'OK good CV <3',6,9,'2024-12-11 23:44:05.550110','2024-12-12 00:00:44.630028'),(7,'You have to fix your CV like this',7,33,'2024-12-11 23:50:22.465429','2024-12-11 23:51:01.396778'),(8,'Your essay now has a strong introduction and compelling conclusion.',8,31,'2024-12-11 23:55:55.435662','2024-12-12 00:05:41.208025'),(9,'Translation into Spanish is complete. Please verify the document.',9,31,'2024-12-12 00:04:36.450685','2024-12-12 00:05:17.195212'),(10,NULL,10,35,'2024-12-12 00:40:16.126642','2024-12-12 00:40:16.126642');
/*!40000 ALTER TABLE `request_details` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `requests`
--

DROP TABLE IF EXISTS `requests`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `requests` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Description` varchar(200) NOT NULL,
  `RequestDate` datetime(6) NOT NULL,
  `Status` varchar(100) NOT NULL,
  `ApplicantId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Requests_ApplicantId` (`ApplicantId`),
  CONSTRAINT `FK_Requests_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `accounts` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `requests`
--

LOCK TABLES `requests` WRITE;
/*!40000 ALTER TABLE `requests` DISABLE KEYS */;
INSERT INTO `requests` VALUES (1,'Do me a favor','2024-12-09 18:54:03.669601','Finished',14,'2024-12-09 18:54:03.703821','2024-12-11 23:26:00.008543'),(2,'Proofread my essay for grammar and flow improvement.','2024-12-09 18:56:44.766385','Pending',14,'2024-12-09 18:56:44.766724','2024-12-09 18:56:44.766723'),(3,'I am looking for scholarships in Europe. Please help.','2024-12-09 18:58:07.259748','Finished',14,'2024-12-09 18:58:07.259972','2024-12-11 23:53:53.562210'),(4,'Please search for scholarships that fit my profile.','2024-12-11 23:35:59.924537','Pending',14,'2024-12-11 23:35:59.933943','2024-12-11 23:35:59.933941'),(5,'I need assistance drafting a recommendation letter for my professor.','2024-12-11 23:37:56.730269','Finished',14,'2024-12-11 23:37:56.731388','2024-12-11 23:42:35.150031'),(6,'Please review my entire scholarship application for errors and suggestions.','2024-12-11 23:44:05.549763','Pending',14,'2024-12-11 23:44:05.550107','2024-12-12 00:00:44.628160'),(7,'I need detailed feedback on my CV for applying to scholarships in Asia.','2024-12-11 23:50:22.464645','Finished',14,'2024-12-11 23:50:22.465427','2024-12-12 00:01:18.565856'),(8,'Please review my entire scholarship application for errors and suggestions.','2024-12-11 23:55:55.435270','Pending',14,'2024-12-11 23:55:55.435660','2024-12-12 00:05:41.203666'),(9,'Can you help me develop a personalized strategy for winning scholarships?','2024-12-12 00:04:36.450334','Pending',14,'2024-12-12 00:04:36.450684','2024-12-12 00:05:17.190681'),(10,'My CV needs to be tailored for a specific scholarship. Please assist.','2024-12-12 00:40:16.108576','Pending',17,'2024-12-12 00:40:16.126641','2024-12-12 00:40:16.126640');
/*!40000 ALTER TABLE `requests` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `review_milestones`
--

DROP TABLE IF EXISTS `review_milestones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `review_milestones` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Description` varchar(200) NOT NULL,
  `FromDate` datetime(6) NOT NULL,
  `ToDate` datetime(6) NOT NULL,
  `ScholarshipProgramId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_review_milestones_ScholarshipProgramId` (`ScholarshipProgramId`),
  CONSTRAINT `FK_review_milestones_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `review_milestones`
--

LOCK TABLES `review_milestones` WRITE;
/*!40000 ALTER TABLE `review_milestones` DISABLE KEYS */;
/*!40000 ALTER TABLE `review_milestones` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `roles`
--

LOCK TABLES `roles` WRITE;
/*!40000 ALTER TABLE `roles` DISABLE KEYS */;
INSERT INTO `roles` VALUES (1,'Admin','2024-12-09 18:43:16.815870','2024-12-09 18:43:16.815868'),(2,'Funder','2024-12-09 18:43:16.815874','2024-12-09 18:43:16.815874'),(3,'Expert','2024-12-09 18:43:16.815875','2024-12-09 18:43:16.815875'),(4,'Provider','2024-12-09 18:43:16.815876','2024-12-09 18:43:16.815876'),(5,'Applicant','2024-12-09 18:43:16.815877','2024-12-09 18:43:16.815877');
/*!40000 ALTER TABLE `roles` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `scholarship_program_certificates`
--

DROP TABLE IF EXISTS `scholarship_program_certificates`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `scholarship_program_certificates` (
  `ScholarshipProgramId` int NOT NULL,
  `CertificateId` int NOT NULL,
  PRIMARY KEY (`ScholarshipProgramId`,`CertificateId`),
  KEY `IX_scholarship_program_certificates_CertificateId` (`CertificateId`),
  CONSTRAINT `FK_scholarship_program_certificates_Certificates_CertificateId` FOREIGN KEY (`CertificateId`) REFERENCES `certificates` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_scholarship_program_certificates_scholarship_programs_Schola~` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scholarship_program_certificates`
--

LOCK TABLES `scholarship_program_certificates` WRITE;
/*!40000 ALTER TABLE `scholarship_program_certificates` DISABLE KEYS */;
INSERT INTO `scholarship_program_certificates` VALUES (5,1),(6,1),(1,2),(4,2),(2,3),(3,3),(3,4);
/*!40000 ALTER TABLE `scholarship_program_certificates` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `scholarship_programs`
--

DROP TABLE IF EXISTS `scholarship_programs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `scholarship_programs` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `ImageUrl` varchar(1024) DEFAULT NULL,
  `Description` varchar(200) NOT NULL,
  `ScholarshipAmount` decimal(18,2) NOT NULL,
  `NumberOfAwardMilestones` int NOT NULL,
  `NumberOfScholarships` int NOT NULL,
  `Deadline` datetime(6) NOT NULL,
  `Status` varchar(100) NOT NULL,
  `FunderId` int NOT NULL,
  `CategoryId` int NOT NULL,
  `UniversityId` int NOT NULL,
  `MajorId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_scholarship_programs_CategoryId` (`CategoryId`),
  KEY `IX_scholarship_programs_FunderId` (`FunderId`),
  KEY `IX_scholarship_programs_MajorId` (`MajorId`),
  KEY `IX_scholarship_programs_UniversityId` (`UniversityId`),
  CONSTRAINT `FK_scholarship_programs_Accounts_FunderId` FOREIGN KEY (`FunderId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_scholarship_programs_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `categories` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_scholarship_programs_Majors_MajorId` FOREIGN KEY (`MajorId`) REFERENCES `majors` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_scholarship_programs_Universities_UniversityId` FOREIGN KEY (`UniversityId`) REFERENCES `universities` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `scholarship_programs`
--

LOCK TABLES `scholarship_programs` WRITE;
/*!40000 ALTER TABLE `scholarship_programs` DISABLE KEYS */;
INSERT INTO `scholarship_programs` VALUES (1,'Global Excellence Scholarship','https://example.com/images/global_excellence.jpg','Awarded to outstanding students demonstrating academic excellence and leadership potential.',10000.00,3,5,'2024-12-15 23:59:59.000000','Open',1,2,3,5,'2024-12-09 18:43:16.816474','2024-12-09 18:43:16.816474'),(2,'Future Innovators Fellowship','https://example.com/images/future_innovators.jpg','Supporting aspiring scientists and engineers working on innovative projects.',15000.00,3,3,'2025-01-20 23:59:59.000000','Open',2,1,4,2,'2024-12-09 18:43:16.816487','2024-12-09 18:43:16.816487'),(3,'Global Women in STEM Award','https://example.com/images/women_in_stem.jpg','Encouraging women to pursue careers in STEM fields through financial aid and mentorship.',12000.00,3,10,'2024-11-30 23:59:59.000000','Reviewing',3,3,2,7,'2024-12-09 18:43:16.816492','2024-12-11 18:57:50.058913'),(4,'Global Change-Maker Scholarship','https://example.com/images/change_maker.jpg','Recognizing students with initiatives addressing global challenges.',10000.00,3,8,'2024-12-25 23:59:59.000000','Open',6,2,5,8,'2024-12-09 18:43:16.816499','2024-12-09 18:43:16.816499'),(5,'PhD Research Excellence Award','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQmkvlHDU2Ut2U_rlwukwpHtnQCF0f2b_1bw&s','Supporting PhD students with outstanding research proposals.',25000.00,3,3,'2024-11-09 00:00:00.000000','FINISHED',2,1,1,1,'2024-12-09 18:43:16.816504','2024-12-09 18:43:16.816504'),(6,'Community Leadership Scholarship','https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTQmkvlHDU2Ut2U_rlwukwpHtnQCF0f2b_1bw&s','Awarded to students showing significant community impact.',7000.00,3,3,'2024-11-09 00:00:00.000000','FINISHED',2,1,1,1,'2024-12-09 18:43:16.816515','2024-12-09 18:43:16.816515');
/*!40000 ALTER TABLE `scholarship_programs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `server`
--

DROP TABLE IF EXISTS `server`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `server` (
  `Id` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Data` longtext NOT NULL,
  `LastHeartbeat` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `server`
--

LOCK TABLES `server` WRITE;
/*!40000 ALTER TABLE `server` DISABLE KEYS */;
INSERT INTO `server` VALUES ('desktop-s51e4bq:316444:f4b14d19-9cb8-4f03-a283-b4abaf5498e6','{\"WorkerCount\":20,\"Queues\":[\"default\"],\"StartedAt\":\"2024-12-11T17:30:24.3387096Z\"}','2024-12-11 17:54:55.342593');
/*!40000 ALTER TABLE `server` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `services`
--

DROP TABLE IF EXISTS `services`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `services` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Type` varchar(100) NOT NULL,
  `Price` decimal(18,2) DEFAULT NULL,
  `Status` varchar(100) NOT NULL,
  `ProviderId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Services_ProviderId` (`ProviderId`),
  CONSTRAINT `FK_Services_Accounts_ProviderId` FOREIGN KEY (`ProviderId`) REFERENCES `accounts` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `services`
--

LOCK TABLES `services` WRITE;
/*!40000 ALTER TABLE `services` DISABLE KEYS */;
INSERT INTO `services` VALUES (1,'CV Polish for Competitive Scholarships','Ensure your CV meets the highest standards for competitive scholarship programs.','CV_REVIEW',65.00,'Active',8,'2024-12-09 18:43:16.816548','2024-12-09 18:43:16.816547'),(2,'Scholarship Document Translation','Professional translation for scholarship-related documents.','TRANSLATION',85.00,'Active',8,'2024-12-09 18:43:16.816549','2024-12-09 18:43:16.816549'),(3,'Scholarship Essay Feedback','Detailed critique and enhancement of your scholarship essay drafts.','ESSAY_WRITING',85.00,'Active',8,'2024-12-09 18:43:16.816551','2024-12-09 18:43:16.816551'),(4,'Comprehensive Application Review','Full evaluation of your scholarship application with actionable improvements.','APPLICATION_REVIEW',0.00,'Inactive',8,'2024-12-09 18:43:16.816553','2024-12-10 19:18:48.451559'),(5,'Virtual Interview Preparation','Practice and tips for acing virtual scholarship interviews.','INTERVIEW_COACHING',85.00,'Inactive',8,'2024-12-09 18:43:16.816555','2024-12-10 00:02:51.860977'),(6,'Custom Recommendation Letter Draft','Assistance in drafting personalized recommendation letters.','RECOMMENDATION_LETTER',85.00,'Active',8,'2024-12-09 18:43:16.816556','2024-12-09 21:08:46.646369'),(7,'Scholarship Finder Tool Access','Access to a curated database of scholarships tailored to your profile.','SCHOLARSHIP_SEARCH',45.00,'Active',10,'2024-12-09 18:43:16.816558','2024-12-09 18:43:16.816558'),(8,'Scholarship Finder Tool Access','Access to a curated database of scholarships tailored to your profile.','SCHOLARSHIP_SEARCH',45.00,'Active',10,'2024-12-09 18:43:16.816560','2024-12-09 18:43:16.816560'),(9,'CV Tailoring for Scholarships','Personalized CV review and enhancement to highlight scholarship-specific achievements.','CV_REVIEW',45.00,'Active',10,'2024-12-09 18:43:16.816561','2024-12-09 18:43:16.816561'),(10,'Academic Translation Services','Translation of academic documents to the required language for scholarship applications.','TRANSLATION',45.00,'Active',10,'2024-12-09 18:43:16.816563','2024-12-09 18:43:16.816563'),(11,'Winning Scholarship Essay Guide','Professional assistance in crafting compelling scholarship essays.','ESSAY_WRITING',45.00,'Active',10,'2024-12-09 18:43:16.816565','2024-12-09 18:43:16.816565'),(12,'Scholarship Application Critique','Detailed feedback on your entire scholarship application to ensure success.','APPLICATION_REVIEW',45.00,'Active',10,'2024-12-09 18:43:16.816566','2024-12-09 18:43:16.816566'),(31,'Scholarship Readiness Assessment','Comprehensive evaluation to determine your readiness and fit for scholarships.','APPLICATION_REVIEW',60.00,'Active',9,'2024-12-11 23:45:42.763281','2024-12-11 23:45:42.763280'),(32,'Multilingual Scholarship Document','Translation and adaptation of documents in multiple languages.','TRANSLATION',95.00,'Active',9,'2024-12-11 23:46:21.663680','2024-12-11 23:46:21.663679'),(33,'Scholarship Success Workshop','Participate in interactive sessions to learn scholarship-winning strategies.','PERSONALIZED_STRATEGY',58.00,'Active',9,'2024-12-11 23:48:39.126133','2024-12-11 23:48:39.126132'),(34,'Video Application Script Writing','Craft persuasive and engaging scripts for video-based scholarship applications.','DOCUMENT_PROOFREADING',20.00,'Active',19,'2024-12-12 00:37:10.729007','2024-12-12 00:37:10.729005'),(35,'Impactful CV Design Service','Transform your CV with visually appealing and professional layouts.','CV_REVIEW',20.00,'Active',19,'2024-12-12 00:37:39.390091','2024-12-12 00:37:39.390090');
/*!40000 ALTER TABLE `services` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `set`
--

DROP TABLE IF EXISTS `set`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `set` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Key` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Value` varchar(256) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Score` float NOT NULL,
  `ExpireAt` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Set_Key_Value` (`Key`,`Value`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `set`
--

LOCK TABLES `set` WRITE;
/*!40000 ALTER TABLE `set` DISABLE KEYS */;
INSERT INTO `set` VALUES (1,'recurring-jobs','ScheduleScholarshipsAfterDeadline',1733960000,NULL),(2,'recurring-jobs','ScheduleApplicationsNeedExtend',1733960000,NULL),(5,'schedule','3',1734280000,NULL),(6,'schedule','4',1737390000,NULL),(8,'schedule','6',1735150000,NULL);
/*!40000 ALTER TABLE `set` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `skills`
--

DROP TABLE IF EXISTS `skills`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `skills` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Type` varchar(100) NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `skills`
--

LOCK TABLES `skills` WRITE;
/*!40000 ALTER TABLE `skills` DISABLE KEYS */;
INSERT INTO `skills` VALUES (1,'Leadership','Ability to guide, inspire, and manage a team towards achieving objectives.','Soft Skill','2024-12-09 18:43:16.816404','2024-12-09 18:43:16.816404'),(2,'Project Management','Skills in planning, organizing, and managing resources to achieve project goals.','Soft Skill','2024-12-09 18:43:16.816405','2024-12-09 18:43:16.816405'),(3,'Data Analysis','Ability to analyze data and interpret patterns, trends, and insights.','Technical Skill','2024-12-09 18:43:16.816407','2024-12-09 18:43:16.816407'),(4,'Financial Modeling','Ability to create financial representations of real-world scenarios for decision-making.','Technical Skill','2024-12-09 18:43:16.816408','2024-12-09 18:43:16.816408'),(5,'Financial Analysis','Ability to evaluate financial data and make informed financial decisions.','Technical Skill','2024-12-09 18:43:16.816409','2024-12-09 18:43:16.816409'),(6,'Public Speaking','Skill in presenting information effectively to an audience.','Soft Skill','2024-12-09 18:43:16.816411','2024-12-09 18:43:16.816411'),(7,'Digital Marketing','Proficiency in using online platforms to promote products and services.','Technical Skill','2024-12-09 18:43:16.816412','2024-12-09 18:43:16.816412'),(8,'Market Research','Ability to gather and analyze data about market trends and consumer behavior.','Technical Skill','2024-12-09 18:43:16.816413','2024-12-09 18:43:16.816413'),(9,'Patient Care','Skills in providing direct healthcare and support to patients.','Technical Skill','2024-12-09 18:43:16.816415','2024-12-09 18:43:16.816415'),(10,'Communication','Ability to effectively communicate with patients and healthcare teams.','Soft Skill','2024-12-09 18:43:16.816416','2024-12-09 18:43:16.816416'),(11,'Research','Experience in conducting systematic investigation to establish facts and conclusions.','Technical Skill','2024-12-09 18:43:16.816417','2024-12-09 18:43:16.816417'),(12,'Problem Solving','Ability to identify issues and implement effective solutions.','Soft Skill','2024-12-09 18:43:16.816419','2024-12-09 18:43:16.816419'),(13,'Health Policy','Understanding of policies that impact public health and wellness.','Technical Skill','2024-12-09 18:43:16.816422','2024-12-09 18:43:16.816422'),(14,'Community Outreach','Ability to engage and educate communities on health issues.','Soft Skill','2024-12-09 18:43:16.816424','2024-12-09 18:43:16.816424'),(15,'Pharmacology','Understanding of the effects of drugs on the human body.','Technical Skill','2024-12-09 18:43:16.816425','2024-12-09 18:43:16.816425'),(16,'Attention to Detail','Ability to carefully analyze prescriptions and drug interactions.','Soft Skill','2024-12-09 18:43:16.816426','2024-12-09 18:43:16.816426'),(17,'Psychological Assessment','Ability to assess and diagnose mental health conditions.','Technical Skill','2024-12-09 18:43:16.816428','2024-12-09 18:43:16.816428'),(18,'Counseling','Skills in providing support and guidance to individuals facing mental health challenges.','Soft Skill','2024-12-09 18:43:16.816429','2024-12-09 18:43:16.816429'),(19,'Social Research','Ability to design and conduct research related to social behaviors and societal trends.','Technical Skill','2024-12-09 18:43:16.816430','2024-12-09 18:43:16.816430'),(20,'Critical Thinking','Ability to analyze societal issues and propose solutions based on evidence.','Soft Skill','2024-12-09 18:43:16.816432','2024-12-09 18:43:16.816432');
/*!40000 ALTER TABLE `skills` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `state`
--

DROP TABLE IF EXISTS `state`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `state` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `JobId` int NOT NULL,
  `Name` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Reason` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `CreatedAt` datetime(6) NOT NULL,
  `Data` longtext,
  PRIMARY KEY (`Id`),
  KEY `FK_HangFire_State_Job` (`JobId`),
  CONSTRAINT `FK_HangFire_State_Job` FOREIGN KEY (`JobId`) REFERENCES `job` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb3;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `state`
--

LOCK TABLES `state` WRITE;
/*!40000 ALTER TABLE `state` DISABLE KEYS */;
INSERT INTO `state` VALUES (1,1,'Enqueued','Triggered by recurring job scheduler','2024-12-11 11:57:19.755124','{\"EnqueuedAt\":\"1733918239739\",\"Queue\":\"default\"}'),(2,2,'Enqueued','Triggered by recurring job scheduler','2024-12-11 11:57:19.833423','{\"EnqueuedAt\":\"1733918239831\",\"Queue\":\"default\"}'),(3,1,'Processing',NULL,'2024-12-11 11:57:34.690657','{\"StartedAt\":\"1733918254667\",\"ServerId\":\"desktop-s51e4bq:7612:801426ba-91e4-4a66-915a-9620bf15a9d8\",\"WorkerId\":\"2b688d1f-41cf-416d-9e5d-5086069cff41\"}'),(4,2,'Processing',NULL,'2024-12-11 11:57:34.690662','{\"StartedAt\":\"1733918254669\",\"ServerId\":\"desktop-s51e4bq:7612:801426ba-91e4-4a66-915a-9620bf15a9d8\",\"WorkerId\":\"3731e20a-23ca-4af6-b3ed-c93997dad251\"}'),(5,3,'Scheduled',NULL,'2024-12-11 11:57:34.850971','{\"EnqueueAt\":\"1734281999000\",\"ScheduledAt\":\"1733918254820\"}'),(6,1,'Succeeded',NULL,'2024-12-11 11:57:34.865226','{\"SucceededAt\":\"1733918254845\",\"PerformanceDuration\":\"113\",\"Latency\":\"15042\"}'),(7,4,'Scheduled',NULL,'2024-12-11 11:57:34.896259','{\"EnqueueAt\":\"1737392399000\",\"ScheduledAt\":\"1733918254878\"}'),(8,5,'Scheduled',NULL,'2024-12-11 11:57:34.938530','{\"EnqueueAt\":\"1732985999000\",\"ScheduledAt\":\"1733918254925\"}'),(9,6,'Scheduled',NULL,'2024-12-11 11:57:34.976579','{\"EnqueueAt\":\"1735145999000\",\"ScheduledAt\":\"1733918254957\"}'),(10,2,'Succeeded',NULL,'2024-12-11 11:57:35.002209','{\"SucceededAt\":\"1733918254992\",\"PerformanceDuration\":\"252\",\"Latency\":\"14934\"}'),(11,5,'Enqueued','Triggered by DelayedJobScheduler','2024-12-11 11:57:49.735675','{\"EnqueuedAt\":\"1733918269706\",\"Queue\":\"default\"}'),(12,5,'Processing',NULL,'2024-12-11 11:57:49.957665','{\"StartedAt\":\"1733918269947\",\"ServerId\":\"desktop-s51e4bq:7612:801426ba-91e4-4a66-915a-9620bf15a9d8\",\"WorkerId\":\"2b688d1f-41cf-416d-9e5d-5086069cff41\"}'),(13,5,'Succeeded',NULL,'2024-12-11 11:57:50.162756','{\"SucceededAt\":\"1733918270158\",\"PerformanceDuration\":\"180\",\"Latency\":\"15051\",\"Result\":\"{\\\"$type\\\":\\\"Domain.Entities.ScholarshipProgram, Domain\\\",\\\"Name\\\":\\\"Global Women in STEM Award\\\",\\\"ImageUrl\\\":\\\"https://example.com/images/women_in_stem.jpg\\\",\\\"Description\\\":\\\"Encouraging women to pursue careers in STEM fields through financial aid and mentorship.\\\",\\\"ScholarshipAmount\\\":12000.00,\\\"NumberOfAwardMilestones\\\":3,\\\"NumberOfScholarships\\\":10,\\\"Deadline\\\":\\\"2024-11-30T23:59:59\\\",\\\"Status\\\":\\\"Reviewing\\\",\\\"FunderId\\\":3,\\\"CategoryId\\\":3,\\\"UniversityId\\\":2,\\\"MajorId\\\":7,\\\"Id\\\":3,\\\"CreatedAt\\\":\\\"2024-12-09T18:43:16.816492\\\",\\\"UpdatedAt\\\":\\\"2024-12-11T18:57:50.0589135+07:00\\\"}\"}');
/*!40000 ALTER TABLE `state` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `subscriptions`
--

DROP TABLE IF EXISTS `subscriptions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `subscriptions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `Amount` decimal(18,2) NOT NULL,
  `NumberOfServices` int NOT NULL,
  `ValidMonths` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `subscriptions`
--

LOCK TABLES `subscriptions` WRITE;
/*!40000 ALTER TABLE `subscriptions` DISABLE KEYS */;
INSERT INTO `subscriptions` VALUES (1,'Beginner','Perfect for individuals starting out with basic needs.',50.00,10,1,'2024-12-09 18:43:16.815881','2024-12-09 18:43:16.815881'),(2,'Intermediate','Ideal for users who require more services and longer usage.',100.00,20,2,'2024-12-10 19:30:44.048764','2024-12-10 19:30:44.048762'),(3,'Professional','Great for professionals looking to maximize their potential.',190.00,40,4,'2024-12-10 19:31:13.889208','2024-12-10 19:31:13.889207'),(4,'Enterprise','Designed for large-scale needs with extensive features.',390.00,80,12,'2024-12-10 19:31:47.588311','2024-12-10 19:31:47.588310');
/*!40000 ALTER TABLE `subscriptions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transactions`
--

DROP TABLE IF EXISTS `transactions`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transactions` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Amount` decimal(18,2) NOT NULL,
  `PaymentMethod` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `TransactionId` varchar(100) NOT NULL,
  `TransactionDate` datetime(6) NOT NULL,
  `Status` varchar(100) NOT NULL,
  `WalletSenderId` int NOT NULL,
  `WalletReceiverId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Transactions_WalletReceiverId` (`WalletReceiverId`),
  KEY `IX_Transactions_WalletSenderId` (`WalletSenderId`),
  CONSTRAINT `FK_Transactions_Wallets_WalletReceiverId` FOREIGN KEY (`WalletReceiverId`) REFERENCES `wallets` (`Id`) ON DELETE RESTRICT,
  CONSTRAINT `FK_Transactions_Wallets_WalletSenderId` FOREIGN KEY (`WalletSenderId`) REFERENCES `wallets` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transactions`
--

LOCK TABLES `transactions` WRITE;
/*!40000 ALTER TABLE `transactions` DISABLE KEYS */;
INSERT INTO `transactions` VALUES (1,85.00,'Cash','Pay for service','1db061d48e9a45ca884d2542d82a1230','2024-12-09 11:54:03.577576','Successful',11,5,'2024-12-09 18:54:03.597137','2024-12-09 18:54:03.597136'),(2,65.00,'Cash','Pay for service','5166cac6592b46bf9b902e2ea42462c4','2024-12-09 11:56:44.736970','Successful',11,5,'2024-12-09 18:56:44.737516','2024-12-09 18:56:44.737514'),(3,85.00,'Cash','Pay for service','bc1d32c4af104b6390556863f1919c28','2024-12-09 11:58:07.240167','Successful',11,5,'2024-12-09 18:58:07.240340','2024-12-09 18:58:07.240340'),(4,50.00,'Cash','Pay for subscription upgrade','794ae68aef7346efbdb34426c27223ff','2024-12-10 13:37:49.847729','Successful',5,1,'2024-12-10 20:37:49.869843','2024-12-10 20:37:49.869842'),(5,85.00,'Cash','Pay for service','7321327893e846419a8636035374f363','2024-12-11 16:35:59.832970','Successful',11,5,'2024-12-11 23:35:59.847480','2024-12-11 23:35:59.847478'),(6,45.00,'Cash','Pay for service','04dceaf5359e4227b0b86a7964c4a2b4','2024-12-11 16:37:56.701187','Successful',11,7,'2024-12-11 23:37:56.701559','2024-12-11 23:37:56.701558'),(7,45.00,'Cash','Pay for service','6bf16bc3a20942b4bff2acc53eb4f104','2024-12-11 16:44:05.529763','Successful',11,7,'2024-12-11 23:44:05.529971','2024-12-11 23:44:05.529970'),(8,58.00,'Cash','Pay for service','9d943c2fd470472abf4c4cd100761556','2024-12-11 16:50:22.430563','Successful',11,6,'2024-12-11 23:50:22.445465','2024-12-11 23:50:22.445464'),(9,60.00,'Cash','Pay for service','27d77836fd07488eade049eb1d86cea9','2024-12-11 16:55:55.413863','Successful',11,6,'2024-12-11 23:55:55.414197','2024-12-11 23:55:55.414196'),(10,60.00,'Cash','Pay for service','5d846ed712c74371883e8b30db013f01','2024-12-11 17:04:36.429578','Successful',11,6,'2024-12-12 00:04:36.429943','2024-12-12 00:04:36.429942'),(11,100.00,'Cash','Pay for subscription','239b9b8f51894513bed0867f5047b9b4','2024-12-11 17:36:17.832194','Successful',15,1,'2024-12-12 00:36:17.850248','2024-12-12 00:36:17.850247'),(12,20.00,'Cash','Pay for service','831e6260811b463c8126140a7f2316ff','2024-12-11 17:40:16.081739','Successful',16,15,'2024-12-12 00:40:16.081942','2024-12-12 00:40:16.081942');
/*!40000 ALTER TABLE `transactions` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `universities`
--

DROP TABLE IF EXISTS `universities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `universities` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `Description` varchar(200) DEFAULT NULL,
  `City` varchar(100) NOT NULL,
  `CountryId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_Universities_CountryId` (`CountryId`),
  CONSTRAINT `FK_Universities_Countries_CountryId` FOREIGN KEY (`CountryId`) REFERENCES `countries` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `universities`
--

LOCK TABLES `universities` WRITE;
/*!40000 ALTER TABLE `universities` DISABLE KEYS */;
INSERT INTO `universities` VALUES (1,'Harvard University','A prestigious Ivy League university known for its strong programs in law, business, and medicine.','Cambridge',2,'2024-12-09 18:43:16.816383','2024-12-09 18:43:16.816383'),(2,'Stanford University','Renowned for its entrepreneurial spirit and leading programs in technology and engineering.','Stanford',2,'2024-12-09 18:43:16.816384','2024-12-09 18:43:16.816384'),(3,'University of Oxford','One of the oldest universities in the world, known for its strong research and liberal arts programs.','Oxford',1,'2024-12-09 18:43:16.816385','2024-12-09 18:43:16.816385'),(4,'University of Cambridge','A historic institution known for its academic excellence in sciences and humanities.','Cambridge',1,'2024-12-09 18:43:16.816387','2024-12-09 18:43:16.816387'),(5,'Massachusetts Institute of Technology (MIT)','A world leader in scientific and technological education and research.','Cambridge',2,'2024-12-09 18:43:16.816388','2024-12-09 18:43:16.816388'),(6,'California Institute of Technology (Caltech)','A small but highly focused institution known for its cutting-edge research in science and engineering.','Pasadena',2,'2024-12-09 18:43:16.816390','2024-12-09 18:43:16.816390'),(7,'University of Tokyo','Japan’s leading university, excelling in research and education across various fields.','Tokyo',3,'2024-12-09 18:43:16.816395','2024-12-09 18:43:16.816395'),(8,'Peking University','One of China\'s top universities, recognized for its strong liberal arts and research programs.','Beijing',4,'2024-12-09 18:43:16.816397','2024-12-09 18:43:16.816397'),(9,'ETH Zurich','A leading science and technology university in Switzerland, known for engineering and natural sciences.','Zurich',5,'2024-12-09 18:43:16.816398','2024-12-09 18:43:16.816398'),(10,'University of Melbourne','Australia\'s top university, with strong research programs in medicine, engineering, and social sciences.','Melbourne',6,'2024-12-09 18:43:16.816400','2024-12-09 18:43:16.816400'),(11,'FPT University','Vietnam\'s top university, with strong research programs in medicine, engineering, and social sciences.','Ho Chi Minh City',21,'2024-12-09 18:43:16.816401','2024-12-09 18:43:16.816401');
/*!40000 ALTER TABLE `universities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wallets`
--

DROP TABLE IF EXISTS `wallets`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wallets` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `BankAccountName` varchar(100) NOT NULL,
  `BankAccountNumber` varchar(100) NOT NULL,
  `Balance` decimal(18,2) NOT NULL,
  `AccountId` int NOT NULL,
  `CreatedAt` datetime(6) DEFAULT NULL,
  `UpdatedAt` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `IX_Wallets_AccountId` (`AccountId`),
  CONSTRAINT `FK_Wallets_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `accounts` (`Id`) ON DELETE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wallets`
--

LOCK TABLES `wallets` WRITE;
/*!40000 ALTER TABLE `wallets` DISABLE KEYS */;
INSERT INTO `wallets` VALUES (1,'Scholarship Portal','42424242424242',0.00,1,'2024-12-09 18:43:16.815890','2024-12-09 18:43:16.815890'),(2,'Ethan Ho','424242424242',0.00,2,'2024-12-09 18:43:16.815903','2024-12-09 18:43:16.815903'),(3,'Mia Nguyen','424242424242',0.00,3,'2024-12-09 18:43:16.815910','2024-12-09 18:43:16.815910'),(4,'Leo Panther','4242424242',0.00,4,'2024-12-09 18:43:16.815915','2024-12-09 18:43:16.815915'),(5,'Lucas Vo','424242424242',0.00,8,'2024-12-09 18:43:16.815969','2024-12-09 18:43:16.815969'),(6,'Lily Anna','424242424242',0.00,9,'2024-12-09 18:43:16.815976','2024-12-09 18:43:16.815976'),(7,'Mason Ho','4242424242',0.00,10,'2024-12-09 18:43:16.815987','2024-12-09 18:43:16.815987'),(8,'Chloe Han','4242424242',0.00,11,'2024-12-09 18:43:16.815997','2024-12-09 18:43:16.815997'),(9,'Oliver Giant','4242424242',0.00,12,'2024-12-09 18:43:16.816003','2024-12-09 18:43:16.816003'),(10,'Emma','42424242424242',0.00,13,'2024-12-09 18:43:16.816010','2024-12-09 18:43:16.816010'),(11,'Grace Nguyen','424242424242',0.00,14,'2024-12-09 18:43:16.816030','2024-12-09 18:43:16.816029'),(12,'Zoe','4242424242',0.00,5,'2024-12-09 18:43:16.815923','2024-12-09 18:43:16.815923'),(13,'Noah Christ','4242424242',0.00,6,'2024-12-09 18:43:16.815929','2024-12-09 18:43:16.815929'),(14,'Ava John','42424242424242',0.00,7,'2024-12-09 18:43:16.815935','2024-12-09 18:43:16.815935'),(15,'Tri Do','079203006661',0.00,19,'2024-12-12 00:36:10.880291','2024-12-12 00:36:10.880290'),(16,'Lam Hoang','0701238741',0.00,17,'2024-12-12 00:39:12.821966','2024-12-12 00:39:12.821965');
/*!40000 ALTER TABLE `wallets` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-12-12  0:55:11
