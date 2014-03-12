/********************************************************************************
        GENERAL PURPOSE SCRIPTS - SETTING UP ENVIRONMENT
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
  Applicant_ID      int IDENTITY(1,1),
  FirstName         varchar(50) NOT NULL,
  MiddleName        varchar(50) NULL,
  LastName          varchar(50) NOT NULL,
  SSN               varchar(10) UNIQUE NOT NULL,
  Gender            varchar(1),
  ApplicantAddress  varchar(255) NULL,
  Phone             varchar(50) NULL,
  NameAlias         varchar(255) NULL,
  CONSTRAINT [PK_UID] PRIMARY KEY (Applicant_ID ASC),
  CONSTRAINT [CHK_Person] CHECK (DATALENGTH(FirstName) > 0 AND DATALENGTH(LastName) > 0 AND DATALENGTH(SSN) > 0)
);
CREATE UNIQUE INDEX IDX_Person ON Applicant (FirstName, LastName, SSN ASC);

/* DATAS FOR APPLICANT RELATION*/ 
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Khanh', 'Nguyen', 1992, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Khanh', 'Nguyen', 1986, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Andy', 'Summers', 1985, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Andy', 'Summers', 1983, 'M');
INSERT INTO Applicant(FirstName, LastName, SSN, Gender) VALUES ('Smahane', 'Douyeb', 1987, 'F');

/********************************************************************************
                SKILLS RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'PKSkillID')
  ALTER TABLE Skill DROP CONSTRAINT [PKSkillID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Skill')
  DROP TABLE Skill;
CREATE TABLE Skill (
  Skill_ID    int IDENTITY(1,1) NOT NULL,
  SkillName   varchar(20) UNIQUE  NOT NULL,
  CONSTRAINT  [PKSkillID] PRIMARY KEY (Skill_ID ASC),
  CONSTRAINT  [CHK_SKILLVALID] CHECK (DATALENGTH(SkillName) > 0 AND ISNUMERIC(SkillName) = 0)
);

/* DATA FOR SKILLS RELATION */
INSERT INTO Skill (SkillName) VALUES ('Typing');
INSERT INTO Skill (SkillName) VALUES ('Basic Math');
INSERT INTO Skill (SkillName) VALUES ('Social');
INSERT INTO Skill (SkillName) VALUES ('Computer');
INSERT INTO Skill (SkillName) VALUES ('Speaking');
INSERT INTO Skill (SkillName) VALUES ('Assembly');

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
  Applicant_ID  int NOT NULL,
  Skill_ID      int NOT NULL,
  CONSTRAINT [FKExpertiseApplicantID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant(Applicant_ID) 
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKExpertiseSkillID] FOREIGN KEY (Skill_ID) REFERENCES Skill (Skill_ID) 
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
                APPLICATION RELATION  
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKApplicationID')
  ALTER TABLE Application DROP CONSTRAINT [PKApplicationID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Application')
  DROP TABLE Application;
CREATE TABLE Application (
  Application_ID        int IDENTITY(1,1) NOT NULL,
  ApplicationStatus     varchar(10) NULL,
  SalaryExpectation     varchar(50) NULL,
  FullTime              tinyint NULL,
  AvailableForDays      tinyint NULL,
  AvailableForEvenings  tinyint NULL,
  AvailableForWeekends  tinyint NULL,
  MondayFrom            time NULL,
  TuesdayFrom           time NULL,
  WednesdayFrom         time NULL,
  ThursdayFrom          time NULL,
  FridayFrom            time NULL,
  SaturdayFrom          time NULL,
  SundayFrom            time NULL,
  MondayTo              time NULL,
  TuesdayTo             time NULL,
  WednesdayTo           time NULL,
  ThursdayTo            time NULL,
  FridayTo              time NULL,
  SaturdayTo            time NULL,
  SundayTo              time NULL,
  CONSTRAINT [PKApplicationID] PRIMARY KEY (Application_ID ASC)
);

/* DATA FOR APPLICATIONS RELATION */
INSERT INTO Application(ApplicationStatus) VALUES ('Submitted');
INSERT INTO Application(ApplicationStatus) VALUES ('Rejected');
INSERT INTO Application(ApplicationStatus) VALUES ('Reviewed');

/********************************************************************************
                JOB RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKJobID')
  ALTER TABLE Job DROP CONSTRAINT [PKJobID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Job')
  DROP TABLE Job;
CREATE TABLE Job (
  Job_ID      int IDENTITY(1,1) NOT NULL,
  Title       varchar(20) UNIQUE  NOT NULL,
  CONSTRAINT [PKJobID] PRIMARY KEY (Job_ID ASC),
  CONSTRAINT [CHK_EMPJOB] CHECK (DATALENGTH(Title) > 0 AND ISNUMERIC(Title) = 0)
);

/* DATA FOR JOBS RELATION */
INSERT INTO Job(Title) VALUES ('Factory Worker');
INSERT INTO Job(Title) VALUES ('Receptionist');

/********************************************************************************
              JOB_REQUIREMENT RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKJobReqID')
  ALTER TABLE JobRequirement DROP CONSTRAINT [FKJobReqID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSkillReqID')
  ALTER TABLE JobRequirement DROP CONSTRAINT [FKSkillReqID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'JobRequirement')
  DROP TABLE JobRequirement;
CREATE TABLE JobRequirement (
  Job_ID    int     NOT NULL,
  Skill_ID  int     NOT NULL  UNIQUE,
  Notes     varchar(20) NULL,
  CONSTRAINT [FKJobReqID] FOREIGN KEY (Job_ID) REFERENCES Job (Job_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKSkillReqID] FOREIGN KEY (Skill_ID) REFERENCES Skill (Skill_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR JOB_REQUIREMENTS RELATION */
INSERT INTO JobRequirement(Job_ID, Skill_ID, Notes) VALUES (1, 2, '');
INSERT INTO JobRequirement(Job_ID, Skill_ID, Notes) VALUES (1, 3, '');
INSERT INTO JobRequirement(Job_ID, Skill_ID, Notes) VALUES (1, 5, 'Preferred');
INSERT INTO JobRequirement(Job_ID, Skill_ID, Notes) VALUES (2, 4, '');
INSERT INTO JobRequirement(Job_ID, Skill_ID, Notes) VALUES (2, 1, 'Not Required');

/********************************************************************************
                APPLIED RELATION  
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKAppliedID')
  ALTER TABLE Applied DROP CONSTRAINT [PKAppliedID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantID')
  ALTER TABLE Applied DROP CONSTRAINT [FKApplicantID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantionID')
  ALTER TABLE Applied DROP CONSTRAINT [FKApplicationID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKJobID')
  ALTER TABLE Applied DROP CONSTRAINT [FKJobID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Applied')
  DROP TABLE Applied;
CREATE TABLE Applied (
  Applied_ID      int IDENTITY(1,1) NOT NULL,
  Applicant_ID    int NOT NULL,
  Application_ID  int NOT NULL UNIQUE,
  Job_ID          int NOT NULL,
  DateApplied     date NOT NULL,
  CONSTRAINT [PKAppliedID] PRIMARY KEY (Applied_ID ASC),
  CONSTRAINT [FKApplicantID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKApplicationID] FOREIGN KEY (Application_ID) REFERENCES Application (Application_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKJobID] FOREIGN KEY (Job_ID) REFERENCES Job (Job_ID)
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
  School_ID     int IDENTITY(1,1) NOT NULL,
  School_Name   varchar(255) UNIQUE NOT NULL,
  School_Address  varchar(255) NULL,
  CONSTRAINT [PKSchoolID] PRIMARY KEY (School_ID ASC),
  CONSTRAINT  [CHK_SchoolName] CHECK (DATALENGTH(School_Name) > 0 AND ISNUMERIC(School_Name) = 0)
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

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKEducationID')
  ALTER TABLE Education DROP CONSTRAINT [PKEducationID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantEucationID')
  ALTER TABLE Education DROP CONSTRAINT [FKApplicantEducationID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSchoolID')
  ALTER TABLE Education DROP CONSTRAINT [FKSchoolID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Education')
  DROP TABLE Education;
CREATE TABLE Education (
  Education_ID      int IDENTITY(1,1) NOT NULL,
  Applicant_ID      int NOT NULL,
  School_ID         int NOT NULL,
  YearsAttendedFrom datetime NULL,
  YearsAttendedTo   datetime NULL,
  Graduated         tinyint NULL,
  DegreeAndMajor    varchar(255) NULL,
  CONSTRAINT [PKEducationID] PRIMARY KEY (Education_ID ASC),
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
  Employer_ID   int IDENTITY(1,1) NOT NULL,
  Name        varchar(20) UNIQUE  NOT NULL,
  EmployerAddress     varchar(255) NULL,
  PhoneNumber varchar(50) NULL,
  CONSTRAINT [PKEmployerID] PRIMARY KEY (Employer_ID ASC),
  CONSTRAINT [CHK_EMPEMPLOYERNAME] CHECK (DATALENGTH(Name) > 0 AND ISNUMERIC(Name) = 0)
);

/* DATA FOR EMPLOYER RELATION */
INSERT INTO Employer(Name) VALUES ('Intel');
INSERT INTO Employer(Name) VALUES ('Google');
INSERT INTO Employer(Name) VALUES ('Microsoft');

/********************************************************************************
                EMPLOYMENT RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKEmploymentID')
  ALTER TABLE Employment DROP CONSTRAINT [PKEmploymentID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantWorkID')
  ALTER TABLE Employment DROP CONSTRAINT [FKApplicantWorkID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKEmployerID')
  ALTER TABLE Employment DROP CONSTRAINT [FKEmployerID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Employment')
  DROP TABLE Employment;
CREATE TABLE Employment (
  Employment_ID               int IDENTITY(1,1),
  Applicant_ID                int NOT NULL,
  Employer_ID                 int NOT NULL,
  MayWeContactCurrentEmployer tinyint NULL,
  EmployedFrom                date NULL,
  EmployedTo                  date NULL,
  Supervisor                  varchar(255) NULL,
  Position                    varchar(255) NULL,
  StartingSalary              varchar(255) NULL,
  EndingSalary                varchar(255) NULL,
  ReasonForLeaving            text NULL,
  Responsibilities            text NULL,
  CONSTRAINT [PKEmploymentID] PRIMARY KEY (Employment_ID ASC),
  CONSTRAINT [FKApplicantWorkID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKEmployerID] FOREIGN KEY (Employer_ID) REFERENCES Employer (Employer_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR EMPLOYMENT RELATION */
INSERT INTO Employment(Applicant_ID, Employer_ID) VALUES (1, 2);
INSERT INTO Employment(Applicant_ID, Employer_ID) VALUES (2, 1);
INSERT INTO Employment(Applicant_ID, Employer_ID) VALUES (3, 3);

/********************************************************************************
                REFERENCE RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKReferenceID')
  ALTER TABLE Reference DROP CONSTRAINT [PKReferenceID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Reference')
  DROP TABLE Reference;
CREATE TABLE Reference (
  Reference_ID    int IDENTITY(1,1) NOT NULL,
  YearsKnown	  int NULL,
  CONSTRAINT [PKReferenceID] PRIMARY KEY (Reference_ID ASC)
)

/* DATA FOR EMPLOYMENT_REFERENCE RELATION */
INSERT INTO Reference(YearsKnown) VALUES (10);

/********************************************************************************
                ASSOCIATE RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantIDForAssociate')
  ALTER TABLE Associate DROP CONSTRAINT [FKApplicantIDForAssociate];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKReferenceIDForAssociate')
  ALTER TABLE Reference DROP CONSTRAINT [FKAssociateIDForReference];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Associate')
  DROP TABLE Associate;
CREATE TABLE Associate (
  Applicant_ID    int NOT NULL,
  Reference_ID	  int NOT NULL,
  Name            varchar(255) NULL,
  Phone           varchar(50) NULL,
  Title           varchar(50) NULL,
  CONSTRAINT [FKApplicantIDForAssociate] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKReferenceIDForAssociate] FOREIGN KEY (Reference_ID) REFERENCES Reference (Reference_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR EMPLOYMENT_REFERENCE RELATION */
INSERT INTO Associate(Applicant_ID, Reference_ID, Name, Phone, Title) VALUES (1,1, 'Bill Gates', '911', 'CEO');