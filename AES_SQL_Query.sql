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
<<<<<<< Updated upstream
CREATE UNIQUE INDEX IDX_Person ON Applicant (FirstName, LastName, SSN);
=======
CREATE UNIQUE INDEX IDX_Person ON Applicant (FirstName, LastName, SSN ASC);

/* DATAS FOR APPLICANT RELATION*/ 
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Khanh', 'Nguyen', 1992, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Khanh', 'Nguyen', 1986, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Andy', 'Summers', 1983, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Smahane', 'Douyeb', 1986, 'F');

/********************************************************************************
								SKILLS RELATION
*********************************************************************************/
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
=======

/* DATA FOR APPLIED RELATION */
INSERT INTO Applied(Applicant_ID, Application_ID, Job_ID, DateApplied) VALUES (1, 1, 1, GETDATE());
INSERT INTO Applied(Applicant_ID, Application_ID, Job_ID, DateApplied) VALUES (3, 2, 1, GETDATE());
INSERT INTO Applied(Applicant_ID, Application_ID, Job_ID, DateApplied) VALUES (4, 3, 2, '12-30-2013');

/********************************************************************************
								SCHOOL RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKSchoolID')
	ALTER TABLE School DROP CONSTRAINT [PKSchoolID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'School')
	DROP TABLE School;
CREATE TABLE School (
	School_ID		int IDENTITY(1,1) NOT NULL,
	School_Name		varchar(20)		  NULL,
	CONSTRAINT [PKSchoolID] PRIMARY KEY (School_ID ASC)
);

/* DATA FOR SCHOOL RELATION */
INSERT INTO School(School_Name) VALUES ('PCC');
INSERT INTO School(School_Name) VALUES ('OIT');
INSERT INTO School(School_Name) VALUES ('CCC');
INSERT INTO School(School_Name) VALUES ('WSU');
INSERT INTO School(School_Name) VALUES ('MIT');

/********************************************************************************
								EDUCATION RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantEucationID')
	ALTER TABLE Education DROP CONSTRAINT [FKApplicantEducationID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSchoolID')
	ALTER TABLE Education DROP CONSTRAINT [FKSchoolID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Education')
	DROP TABLE Education;
CREATE TABLE Education (
	Applicant_ID		int NOT NULL UNIQUE,
	School_ID			int NOT NULL,
	CONSTRAINT [FKApplicantEducationID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKSchoolID] FOREIGN KEY (School_ID) REFERENCES School (School_ID)
		ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR EDUCATION RELATION */
INSERT INTO Education(Applicant_ID, School_ID) VALUES (1, 2);
INSERT INTO Education(Applicant_ID, School_ID) VALUES (2, 1);
INSERT INTO Education(Applicant_ID, School_ID) VALUES (3, 4);
INSERT INTO Education(Applicant_ID, School_ID) VALUES (4, 3);

/********************************************************************************
								Employer RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKEmployerID')
	ALTER TABLE Employer DROP CONSTRAINT [PKEmployerID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Employer')
	DROP TABLE Employer;
CREATE TABLE Employer (
	Employer_ID		int IDENTITY(1,1) NOT NULL,
	Employer_Name	varchar(20)		  NULL,
	CONSTRAINT [PKEmployerID] PRIMARY KEY (Employer_ID ASC)
);

/* DATA FOR EMPLOYER RELATION */
INSERT INTO Employer(Employer_Name) VALUES ('Intel');
INSERT INTO Employer(Employer_Name) VALUES ('Google');
INSERT INTO Employer(Employer_Name) VALUES ('Microsoft');

/********************************************************************************
								WORK RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantWorkID')
	ALTER TABLE Work DROP CONSTRAINT [FKApplicantWorkID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKEmployerID')
	ALTER TABLE Work DROP CONSTRAINT [FKEmployerID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Work')
	DROP TABLE Work;
CREATE TABLE Work (
	Applicant_ID		int NOT NULL UNIQUE,
	Employer_ID			int NOT NULL,
	CONSTRAINT [FKApplicantWorkID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKEmployerID] FOREIGN KEY (Employer_ID) REFERENCES Employer (Employer_ID)
		ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR WORK RELATION */
INSERT INTO Work(Applicant_ID, Employer_ID) VALUES (1, 2);
INSERT INTO Work(Applicant_ID, Employer_ID) VALUES (2, 1);
INSERT INTO Work(Applicant_ID, Employer_ID) VALUES (3, 3);
>>>>>>> Stashed changes
