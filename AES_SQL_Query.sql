SET NOCOUNT ON;
GO

USE master;
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name = N'AESDatabase') DROP DATABASE AESDATABASE;
IF NOT EXISTS (SELECT * FROM sysdatabases WHERE name = N'AESDatabase') CREATE DATABASE [AESDatabase];
GO

USE AESDatabase;
GO

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'IDX_Person')
	DROP INDEX IDX_Person ON Applicant;
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PK_UID') 
	ALTER TABLE [Applicant] DROP CONSTRAINT [PK_UID];
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'Applicant') 
	DROP TABLE Applicant;
CREATE TABLE Applicant (
	Applicant_ID	int IDENTITY(1,1),
	FirstName		varchar(20) NOT NULL,
	LastName		varchar(20) NOT NULL,
	SSN				varchar(10) NOT NULL,
	Gender			nvarchar(1),
	CONSTRAINT	[PK_UID] PRIMARY KEY (Applicant_ID)
);
CREATE UNIQUE INDEX IDX_Person ON Applicant (FirstName, LastName, SSN);

IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'PKSkillID')
	ALTER TABLE Skills DROP CONSTRAINT [PKSkillID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Skills')
	DROP TABLE Skills;
CREATE TABLE Skills (
	Skill_ID	int,
	SkillName	varchar(20)
	CONSTRAINT [PKSkillID] PRIMARY KEY (Skill_ID)
);

IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'PKExpertiseUID')
	ALTER TABLE [Expertise] DROP CONSTRAINT [PKExpertiseUID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKExpertiseApplicantID')
	ALTER TABLE [Expertise] DROP CONSTRAINT [FKExpertiseApplicantID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKExpertiseSkillID')
	ALTER TABLE [Expertise] DROP CONSTRAINT [FKExpertiseSkillID];
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'Expertise') 
	DROP TABLE Expertise;
CREATE TABLE Expertise (
	Applicant_ID	int,
	Skill_ID		int,
	CONSTRAINT [FKExpertiseApplicantID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant(Applicant_ID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKExpertiseSkillID] FOREIGN KEY (Skill_ID) REFERENCES Skills (Skill_ID) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [PKExpertiseUID] PRIMARY KEY (Applicant_ID)
);
