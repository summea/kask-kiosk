/********************************************************************************
				GENERAL PURPOSE SCRIPTS	- SETTING UP ENVIRONMENT
*********************************************************************************/

SET NOCOUNT ON;
GO

USE master;
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE name = N'AESDatabase') 
	DROP DATABASE AESDATABASE;
IF NOT EXISTS (SELECT * FROM sysdatabases WHERE name = N'AESDatabase') 
	CREATE DATABASE [AESDatabase];
GO

USE AESDatabase;
GO

/********************************************************************************
								APPLICANT RELATIONS	
*********************************************************************************/

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
	SSN				varchar(10) UNIQUE NOT NULL,
	Gender			nvarchar(1),
	CONSTRAINT	[PK_UID] PRIMARY KEY (Applicant_ID ASC),
	CONSTRAINT	[CHK_Person] CHECK (DATALENGTH(FirstName) > 0 AND DATALENGTH(LastName) > 0 AND DATALENGTH(SSN) > 0)
);
CREATE UNIQUE INDEX IDX_Person ON Applicant (FirstName, LastName, SSN ASC);

/* DATAS FOR APPLICANT RELATION*/ 
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Khanh', 'Nguyen', 1992, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Khanh', 'Nguyen', 1986, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Andy', 'Summers', 1984, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Andy', 'Summers', 1983, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Smahane', 'Douyeb', 1987, 'F');

/********************************************************************************
								SKILLS RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'PKSkillID')
	ALTER TABLE Skills DROP CONSTRAINT [PKSkillID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Skills')
	DROP TABLE Skills;
CREATE TABLE Skills (
	Skill_ID	int IDENTITY(1,1)	NOT NULL,
	SkillName	varchar(20) UNIQUE	NULL,
	CONSTRAINT [PKSkillID] PRIMARY KEY (Skill_ID ASC)
);

/* DATA FOR SKILLS RELATION */
INSERT INTO Skills (SkillName) VALUES ('Typing');
INSERT INTO Skills (SkillName) VALUES ('Basic Math');
INSERT INTO Skills (SkillName) VALUES ('Social');
INSERT INTO Skills (SkillName) VALUES ('Computer');
INSERT INTO Skills (SkillName) VALUES ('Speaking');
INSERT INTO Skills (SkillName) VALUES ('Assembly');

/********************************************************************************
								EXPERTISE RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKExpertiseApplicantID')
	ALTER TABLE [Expertise] DROP CONSTRAINT [FKExpertiseApplicantID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKExpertiseSkillID')
	ALTER TABLE [Expertise] DROP CONSTRAINT [FKExpertiseSkillID];
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'Expertise') 
	DROP TABLE Expertise;
CREATE TABLE Expertise (
	Applicant_ID	int	NOT NULL,
	Skill_ID		int	NOT NULL,
	CONSTRAINT [FKExpertiseApplicantID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant(Applicant_ID) 
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKExpertiseSkillID] FOREIGN KEY (Skill_ID) REFERENCES Skills (Skill_ID) 
		ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR EXPERTISE RELATION */
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (1, 2);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (1, 4);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (1, 6);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (2, 5);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (3, 1);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (3, 2);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (4, 1);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (4, 2);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (4, 4);
INSERT INTO Expertise(Applicant_ID, Skill_ID) VALUES (4, 6);

/********************************************************************************
								APPLICATIONS RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKApplicationID')
	ALTER TABLE Applications DROP CONSTRAINT [PKApplicationID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Applications')
	DROP TABLE Applications;
CREATE TABLE Applications (
	Application_ID		int IDENTITY(1,1) NOT NULL,
	ApplicationStatus	varchar(10) NULL,
	CONSTRAINT [PKApplicationID] PRIMARY KEY (Application_ID ASC)
);

/* DATA FOR APPLICATIONS RELATION */
INSERT INTO Applications(ApplicationStatus) VALUES ('Submitted');
INSERT INTO Applications(ApplicationStatus) VALUES ('Rejected');
INSERT INTO Applications(ApplicationStatus) VALUES ('Reviewed');

/********************************************************************************
								JOBS RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKJobID')
	ALTER TABLE Jobs DROP CONSTRAINT [PKJobID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Jobs')
	DROP TABLE Jobs;
CREATE TABLE Jobs (
	Job_ID				int IDENTITY(1,1)	NOT NULL,
	Job_Title			varchar(20) UNIQUE	NULL,
	CONSTRAINT [PKJobID] PRIMARY KEY (Job_ID ASC)
);

/* DATA FOR JOBS RELATION */
INSERT INTO Jobs(Job_Title) VALUES ('Factory Worker');
INSERT INTO Jobs(Job_Title) VALUES ('Receptionist');

/********************************************************************************
							JOB_REQUIREMENTS RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKJobReqID')
	ALTER TABLE Job_Requirements DROP CONSTRAINT [FKJobReqID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSkillReqID')
	ALTER TABLE Job_Requirements DROP CONSTRAINT [FKSkillReqID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Job_Requirements')
	DROP TABLE Job_Requirements;
CREATE TABLE Job_Requirements (
	Job_ID		int			NOT NULL,
	Skill_ID	int			NOT NULL	UNIQUE,
	Notes		varchar(20) NULL,
	CONSTRAINT [FKJobReqID] FOREIGN KEY (Job_ID) REFERENCES Jobs (Job_ID)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKSkillReqID] FOREIGN KEY (Skill_ID) REFERENCES Skills (Skill_ID)
		ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR JOB_REQUIREMENTS RELATION */
INSERT INTO Job_Requirements(Job_ID, Skill_ID, Notes) VALUES (1, 2, '');
INSERT INTO Job_Requirements(Job_ID, Skill_ID, Notes) VALUES (1, 3, '');
INSERT INTO Job_Requirements(Job_ID, Skill_ID, Notes) VALUES (1, 5, 'Preferred');
INSERT INTO Job_Requirements(Job_ID, Skill_ID, Notes) VALUES (2, 4, '');
INSERT INTO Job_Requirements(Job_ID, Skill_ID, Notes) VALUES (2, 1, 'Not Required');

/********************************************************************************
								APPLIED RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantID')
	ALTER TABLE Applicant DROP CONSTRAINT [FKApplicantID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantionID')
	ALTER TABLE Applicant DROP CONSTRAINT [FKApplicationID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKJobID')
	ALTER TABLE Applicant DROP CONSTRAINT [FKJobID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Applied')
	DROP TABLE Applied;
CREATE TABLE Applied (
	Applicant_ID	int	NOT NULL UNIQUE,
	Application_ID	int NOT NULL UNIQUE,
	Job_ID			int NOT NULL,
	DateApplied		date NOT NULL,
	CONSTRAINT [FKApplicantID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKApplicationID] FOREIGN KEY (Application_ID) REFERENCES Applications (Application_ID)
		ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FKJobID] FOREIGN KEY (Job_ID) REFERENCES Jobs (Job_ID)
		ON DELETE CASCADE ON UPDATE CASCADE
);

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
	School_Name		varchar(20)	UNIQUE	  NULL,
	CONSTRAINT [PKSchoolID] PRIMARY KEY (School_ID ASC),
	CONSTRAINT	[CHK_SchoolName] CHECK (DATALENGTH(School_Name) > 0 AND ISNUMERIC(School_Name) = 0)
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
	Applicant_ID		int NOT NULL,
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
								EMPLOYER RELATION	
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKEmployerID')
	ALTER TABLE Employer DROP CONSTRAINT [PKEmployerID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Employer')
	DROP TABLE Employer;
CREATE TABLE Employer (
	Employer_ID		int IDENTITY(1,1) NOT NULL,
	Employer_Name	varchar(20)	UNIQUE	  NULL,
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