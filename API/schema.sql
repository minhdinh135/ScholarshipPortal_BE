CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

START TRANSACTION;

CREATE TABLE `Categories` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Certificates` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `Type` longtext NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Countries` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Code` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Majors` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `ParentMajorId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Majors_Majors_ParentMajorId` FOREIGN KEY (`ParentMajorId`) REFERENCES `Majors` (`Id`)
);

CREATE TABLE `Roles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Skills` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `Type` longtext NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Universities` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `City` longtext NULL,
    `CountryId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Universities_Countries_CountryId` FOREIGN KEY (`CountryId`) REFERENCES `Countries` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Accounts` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Username` longtext NULL,
    `Email` longtext NULL,
    `PhoneNumber` longtext NULL,
    `HashedPassword` longtext NULL,
    `Address` longtext NULL,
    `AvatarUrl` longtext NULL,
    `LoginWithGoogle` tinyint(1) NULL,
    `Status` longtext NULL,
    `RoleId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Accounts_Roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `Roles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `applicant_profiles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FirstName` longtext NULL,
    `LastName` longtext NULL,
    `BirthDate` datetime(6) NULL,
    `Gender` longtext NULL,
    `Nationality` longtext NULL,
    `Ethnicity` longtext NULL,
    `ApplicantId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_applicant_profiles_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Chats` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Message` longtext NULL,
    `SentDate` datetime(6) NULL,
    `SenderId` int NULL,
    `IsRead` tinyint(1) NULL,
    `ReceiverId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Chats_Accounts_ReceiverId` FOREIGN KEY (`ReceiverId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Chats_Accounts_SenderId` FOREIGN KEY (`SenderId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `funder_profiles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `OrganizationName` longtext NULL,
    `ContactPersonName` longtext NULL,
    `FunderId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_funder_profiles_Accounts_FunderId` FOREIGN KEY (`FunderId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Notifications` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Message` longtext NULL,
    `IsRead` tinyint(1) NULL,
    `SentDate` datetime(6) NULL,
    `ReceiverId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Notifications_Accounts_ReceiverId` FOREIGN KEY (`ReceiverId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `provider_profiles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `OrganizationName` longtext NULL,
    `ContactPersonName` longtext NULL,
    `ProviderId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_provider_profiles_Accounts_ProviderId` FOREIGN KEY (`ProviderId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Requests` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext NULL,
    `RequestDate` datetime(6) NULL,
    `Status` longtext NULL,
    `ApplicantId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Requests_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `Accounts` (`Id`)
);

CREATE TABLE `scholarship_programs` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `ImageUrl` longtext NULL,
    `Description` longtext NULL,
    `ScholarshipAmount` decimal(18,2) NULL,
    `NumberOfScholarships` int NULL,
    `Deadline` datetime(6) NULL,
    `Status` longtext NULL,
    `FunderId` int NULL,
    `CategoryId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_scholarship_programs_Accounts_FunderId` FOREIGN KEY (`FunderId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_scholarship_programs_Categories_CategoryId` FOREIGN KEY (`CategoryId`) REFERENCES `Categories` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Services` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `Type` longtext NULL,
    `Price` decimal(18,2) NULL,
    `Status` longtext NULL,
    `Duration` datetime(6) NULL,
    `ProviderId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Services_Accounts_ProviderId` FOREIGN KEY (`ProviderId`) REFERENCES `Accounts` (`Id`)
);

CREATE TABLE `Wallets` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `BankAccountName` longtext NULL,
    `BankAccountNumber` longtext NULL,
    `Balance` decimal(18,2) NULL,
    `StripeCustomerId` longtext NULL,
    `AccountId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Wallets_Accounts_AccountId` FOREIGN KEY (`AccountId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Achievements` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `AchievedDate` datetime(6) NULL,
    `ApplicantProfileId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Achievements_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `applicant_certificates` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `Type` longtext NULL,
    `ImageUrl` longtext NULL,
    `ApplicantProfileId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_applicant_certificates_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `applicant_skills` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Type` longtext NULL,
    `Description` longtext NULL,
    `ApplicantProfileId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_applicant_skills_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Experiences` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `ApplicantProfileId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Experiences_applicant_profiles_ApplicantProfileId` FOREIGN KEY (`ApplicantProfileId`) REFERENCES `applicant_profiles` (`Id`)
);

CREATE TABLE `funder_documents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Type` longtext NULL,
    `FileUrl` longtext NULL,
    `FunderProfileId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_funder_documents_funder_profiles_FunderProfileId` FOREIGN KEY (`FunderProfileId`) REFERENCES `funder_profiles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `provider_documents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Type` longtext NULL,
    `FileUrl` longtext NULL,
    `ProviderProfileId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_provider_documents_provider_profiles_ProviderProfileId` FOREIGN KEY (`ProviderProfileId`) REFERENCES `provider_profiles` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Applications` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `AppliedDate` datetime(6) NULL,
    `Status` longtext NULL,
    `ApplicantId` int NULL,
    `ScholarshipProgramId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Applications_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Applications_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `award_milestones` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `FromDate` datetime(6) NULL,
    `ToDate` datetime(6) NULL,
    `Amount` decimal(18,2) NULL,
    `ScholarshipProgramId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_award_milestones_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Criteria` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Description` longtext NULL,
    `ScholarshipProgramId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Criteria_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `major_skills` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `MajorId` int NULL,
    `SkillId` int NULL,
    `ScholarshipProgramId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_major_skills_Majors_MajorId` FOREIGN KEY (`MajorId`) REFERENCES `Majors` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_major_skills_Skills_SkillId` FOREIGN KEY (`SkillId`) REFERENCES `Skills` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_major_skills_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `review_milestones` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext NULL,
    `FromDate` datetime(6) NULL,
    `ToDate` datetime(6) NULL,
    `ScholarshipProgramId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_review_milestones_scholarship_programs_ScholarshipProgramId` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `scholarship_program_certificates` (
    `ScholarshipProgramId` int NOT NULL,
    `CertificateId` int NOT NULL,
    PRIMARY KEY (`ScholarshipProgramId`, `CertificateId`),
    CONSTRAINT `FK_scholarship_program_certificates_Certificates_CertificateId` FOREIGN KEY (`CertificateId`) REFERENCES `Certificates` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_scholarship_program_certificates_scholarship_programs_Schola~` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `scholarship_program_universities` (
    `ScholarshipProgramId` int NOT NULL,
    `UniversityId` int NOT NULL,
    PRIMARY KEY (`ScholarshipProgramId`, `UniversityId`),
    CONSTRAINT `FK_scholarship_program_universities_Universities_UniversityId` FOREIGN KEY (`UniversityId`) REFERENCES `Universities` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_scholarship_program_universities_scholarship_programs_Schola~` FOREIGN KEY (`ScholarshipProgramId`) REFERENCES `scholarship_programs` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Feedbacks` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Content` longtext NULL,
    `Rating` double NULL,
    `FeedbackDate` datetime(6) NULL,
    `ApplicantId` int NULL,
    `ServiceId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Feedbacks_Accounts_ApplicantId` FOREIGN KEY (`ApplicantId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Feedbacks_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `Services` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `request_details` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `ExpectedCompletionTime` datetime(6) NULL,
    `ApplicationNotes` longtext NULL,
    `ScholarshipType` longtext NULL,
    `ApplicationFileUrl` longtext NULL,
    `RequestId` int NULL,
    `ServiceId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_request_details_Requests_RequestId` FOREIGN KEY (`RequestId`) REFERENCES `Requests` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_request_details_Services_ServiceId` FOREIGN KEY (`ServiceId`) REFERENCES `Services` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Transactions` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Amount` decimal(18,2) NULL,
    `PaymentMethod` longtext NULL,
    `Description` longtext NULL,
    `TransactionId` longtext NULL,
    `TransactionDate` datetime(6) NULL,
    `Status` longtext NULL,
    `WalletSenderId` int NULL,
    `WalletReceiverId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Transactions_Wallets_WalletReceiverId` FOREIGN KEY (`WalletReceiverId`) REFERENCES `Wallets` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_Transactions_Wallets_WalletSenderId` FOREIGN KEY (`WalletSenderId`) REFERENCES `Wallets` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `application_documents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Type` longtext NULL,
    `FileUrl` longtext NULL,
    `ApplicationId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_application_documents_Applications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `Applications` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `application_reviews` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Description` longtext NULL,
    `Comment` longtext NULL,
    `ReviewDate` datetime(6) NULL,
    `Status` longtext NULL,
    `ExpertId` int NULL,
    `ApplicationId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_application_reviews_Accounts_ExpertId` FOREIGN KEY (`ExpertId`) REFERENCES `Accounts` (`Id`) ON DELETE RESTRICT,
    CONSTRAINT `FK_application_reviews_Applications_ApplicationId` FOREIGN KEY (`ApplicationId`) REFERENCES `Applications` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `award_milestone_documents` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` longtext NULL,
    `Type` longtext NULL,
    `FileUrl` longtext NULL,
    `AwardMilestoneId` int NULL,
    `CreatedAt` datetime(6) NULL,
    `UpdatedAt` datetime(6) NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_award_milestone_documents_award_milestones_AwardMilestoneId` FOREIGN KEY (`AwardMilestoneId`) REFERENCES `award_milestones` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Accounts_RoleId` ON `Accounts` (`RoleId`);

CREATE INDEX `IX_Achievements_ApplicantProfileId` ON `Achievements` (`ApplicantProfileId`);

CREATE INDEX `IX_applicant_certificates_ApplicantProfileId` ON `applicant_certificates` (`ApplicantProfileId`);

CREATE UNIQUE INDEX `IX_applicant_profiles_ApplicantId` ON `applicant_profiles` (`ApplicantId`);

CREATE INDEX `IX_applicant_skills_ApplicantProfileId` ON `applicant_skills` (`ApplicantProfileId`);

CREATE INDEX `IX_application_documents_ApplicationId` ON `application_documents` (`ApplicationId`);

CREATE INDEX `IX_application_reviews_ApplicationId` ON `application_reviews` (`ApplicationId`);

CREATE INDEX `IX_application_reviews_ExpertId` ON `application_reviews` (`ExpertId`);

CREATE INDEX `IX_Applications_ApplicantId` ON `Applications` (`ApplicantId`);

CREATE INDEX `IX_Applications_ScholarshipProgramId` ON `Applications` (`ScholarshipProgramId`);

CREATE INDEX `IX_award_milestone_documents_AwardMilestoneId` ON `award_milestone_documents` (`AwardMilestoneId`);

CREATE INDEX `IX_award_milestones_ScholarshipProgramId` ON `award_milestones` (`ScholarshipProgramId`);

CREATE INDEX `IX_Chats_ReceiverId` ON `Chats` (`ReceiverId`);

CREATE INDEX `IX_Chats_SenderId` ON `Chats` (`SenderId`);

CREATE INDEX `IX_Criteria_ScholarshipProgramId` ON `Criteria` (`ScholarshipProgramId`);

CREATE INDEX `IX_Experiences_ApplicantProfileId` ON `Experiences` (`ApplicantProfileId`);

CREATE INDEX `IX_Feedbacks_ApplicantId` ON `Feedbacks` (`ApplicantId`);

CREATE INDEX `IX_Feedbacks_ServiceId` ON `Feedbacks` (`ServiceId`);

CREATE INDEX `IX_funder_documents_FunderProfileId` ON `funder_documents` (`FunderProfileId`);

CREATE UNIQUE INDEX `IX_funder_profiles_FunderId` ON `funder_profiles` (`FunderId`);

CREATE INDEX `IX_major_skills_MajorId` ON `major_skills` (`MajorId`);

CREATE INDEX `IX_major_skills_ScholarshipProgramId` ON `major_skills` (`ScholarshipProgramId`);

CREATE INDEX `IX_major_skills_SkillId` ON `major_skills` (`SkillId`);

CREATE INDEX `IX_Majors_ParentMajorId` ON `Majors` (`ParentMajorId`);

CREATE INDEX `IX_Notifications_ReceiverId` ON `Notifications` (`ReceiverId`);

CREATE INDEX `IX_provider_documents_ProviderProfileId` ON `provider_documents` (`ProviderProfileId`);

CREATE UNIQUE INDEX `IX_provider_profiles_ProviderId` ON `provider_profiles` (`ProviderId`);

CREATE INDEX `IX_request_details_RequestId` ON `request_details` (`RequestId`);

CREATE INDEX `IX_request_details_ServiceId` ON `request_details` (`ServiceId`);

CREATE INDEX `IX_Requests_ApplicantId` ON `Requests` (`ApplicantId`);

CREATE INDEX `IX_review_milestones_ScholarshipProgramId` ON `review_milestones` (`ScholarshipProgramId`);

CREATE INDEX `IX_scholarship_program_certificates_CertificateId` ON `scholarship_program_certificates` (`CertificateId`);

CREATE INDEX `IX_scholarship_program_universities_UniversityId` ON `scholarship_program_universities` (`UniversityId`);

CREATE INDEX `IX_scholarship_programs_CategoryId` ON `scholarship_programs` (`CategoryId`);

CREATE INDEX `IX_scholarship_programs_FunderId` ON `scholarship_programs` (`FunderId`);

CREATE INDEX `IX_Services_ProviderId` ON `Services` (`ProviderId`);

CREATE INDEX `IX_Transactions_WalletReceiverId` ON `Transactions` (`WalletReceiverId`);

CREATE INDEX `IX_Transactions_WalletSenderId` ON `Transactions` (`WalletSenderId`);

CREATE INDEX `IX_Universities_CountryId` ON `Universities` (`CountryId`);

CREATE UNIQUE INDEX `IX_Wallets_AccountId` ON `Wallets` (`AccountId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20241110105819_InitV4', '8.0.0');

COMMIT;

