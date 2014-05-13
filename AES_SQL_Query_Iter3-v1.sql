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
                STORE MANAGER RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PK_StoreManagerID') 
  ALTER TABLE StoreManager DROP CONSTRAINT [PK_StoreManagerID];
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'StoreManager') 
  DROP TABLE StoreManager;
CREATE TABLE StoreManager (
  StoreManager_ID   int IDENTITY(1,1),
  FirstName         varchar(50) NOT NULL,
  MiddleName        varchar(50) NULL,
  LastName      	varchar(50) NOT NULL,
  Phone             varchar(50) NOT NULL,
  CONSTRAINT [PK_StoreManagerID] PRIMARY KEY (StoreManager_ID ASC),
  CONSTRAINT [CHK_Name] CHECK (DATALENGTH(FirstName) > 0 AND DATALENGTH(LastName) > 0)
);
CREATE UNIQUE INDEX IDX_Person ON StoreManager (FirstName, MiddleName, LastName ASC);

INSERT INTO StoreManager (FirstName, MiddleName, LastName, Phone) VALUES ('John', 'W.', 'Scott', '555-143-7437');
INSERT INTO StoreManager (FirstName, MiddleName, LastName, Phone) VALUES ('Mary', 'E.', 'Smith', '505-145-7437');
INSERT INTO StoreManager (FirstName, MiddleName, LastName, Phone) VALUES ('Ashley', 'L.', 'Abott', '503-899-1258');
INSERT INTO StoreManager (FirstName, MiddleName, LastName, Phone) VALUES ('Katie', 'C.', 'Johnson', '971-543-7454');
INSERT INTO StoreManager (FirstName, MiddleName, LastName, Phone) VALUES ('Miranda', 'P.', 'Mason', '541-255-9803');

/********************************************************************************
                APPLICANT RELATION 
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
INSERT INTO Applicant(FirstName, MiddleName, LastName, SSN, Gender, ApplicantAddress, Phone, NameAlias) VALUES ('Khanh', 'W', 'Nguyen', 1992, 'M', '123 Oak Grove Ct, Sunnyville, AT 10001', '123-456-7890', 'KW');
INSERT INTO Applicant(FirstName, MiddleName, LastName, SSN, Gender, ApplicantAddress, Phone, NameAlias) VALUES ('Khanh', 'X', 'Nguyen', 1986, 'M', '234 Mountain View Rd, Sunnyville, AT 10001', '123-123-4567', 'KX');
INSERT INTO Applicant(FirstName, MiddleName, LastName, SSN, Gender, ApplicantAddress, Phone, NameAlias) VALUES ('Andy', 'Y','Summers', 1985, 'M', '345 Willow St, Sunnyville, AT 10001', '123-456-3254', 'AY');
INSERT INTO Applicant(FirstName, MiddleName, LastName, SSN, Gender, ApplicantAddress, Phone, NameAlias) VALUES ('Smahane', 'Z', 'Douyeb', 1987, 'F', '456 Valley View Ct, Sunnyville, AT 10001', '123-123-3145', 'SD');

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

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKExpertiseID')
  ALTER TABLE [Expertise] DROP CONSTRAINT [PKExpertiseID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKExpertiseApplicantID')
  ALTER TABLE [Expertise] DROP CONSTRAINT [FKExpertiseApplicantID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKExpertiseSkillID')
  ALTER TABLE [Expertise] DROP CONSTRAINT [FKExpertiseSkillID];
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'Expertise') 
  DROP TABLE Expertise;
CREATE TABLE Expertise (
  Expertise_ID  int NOT NULL IDENTITY(1,1),
  Applicant_ID  int NOT NULL,
  Skill_ID      int NOT NULL,
  CONSTRAINT [PKExpertiseID] PRIMARY KEY (Expertise_ID ASC),
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
INSERT INTO Application(ApplicationStatus, SalaryExpectation, FullTime, AvailableForDays, AvailableForEvenings, AvailableForWeekends, MondayFrom, TuesdayFrom, WednesdayFrom, ThursdayFrom, FridayFrom, SaturdayFrom, SundayFrom, MondayTo, TuesdayTo, WednesdayTo, ThursdayTo, FridayTo, SaturdayTo, SundayTo) VALUES ('Submitted', '12345', 1, 1, 1, 1, '01:00:00', '02:00:00', '03:00:00', '04:00:00', '05:00:00', '06:00:00', '07:00:00', '08:00:00', '09:00:00', '10:00:00', '11:00:00', '12:00:00', '13:00:00', '14:00:00')
INSERT INTO Application(ApplicationStatus, SalaryExpectation, FullTime, AvailableForDays, AvailableForEvenings, AvailableForWeekends, MondayFrom, TuesdayFrom, WednesdayFrom, ThursdayFrom, FridayFrom, SaturdayFrom, SundayFrom, MondayTo, TuesdayTo, WednesdayTo, ThursdayTo, FridayTo, SaturdayTo, SundayTo) VALUES ('Submitted', '23456', 1, 0, 0, 1, '02:00:00', '03:00:00', '04:00:00', '05:00:00', '06:00:00', '07:00:00', '08:00:00', '09:00:00', '10:00:00', '11:00:00', '12:00:00', '13:00:00', '14:00:00', '15:00:00')
INSERT INTO Application(ApplicationStatus, SalaryExpectation, FullTime, AvailableForDays, AvailableForEvenings, AvailableForWeekends, MondayFrom, TuesdayFrom, WednesdayFrom, ThursdayFrom, FridayFrom, SaturdayFrom, SundayFrom, MondayTo, TuesdayTo, WednesdayTo, ThursdayTo, FridayTo, SaturdayTo, SundayTo) VALUES ('Rejected', '34567', 1, 0, 1, 1, '03:00:00', '04:00:00', '05:00:00', '06:00:00', '07:00:00', '08:00:00', '09:00:00', '10:00:00', '11:00:00', '12:00:00', '13:00:00', '14:00:00', '15:00:00', '16:00:00')
INSERT INTO Application(ApplicationStatus, SalaryExpectation, FullTime, AvailableForDays, AvailableForEvenings, AvailableForWeekends, MondayFrom, TuesdayFrom, WednesdayFrom, ThursdayFrom, FridayFrom, SaturdayFrom, SundayFrom, MondayTo, TuesdayTo, WednesdayTo, ThursdayTo, FridayTo, SaturdayTo, SundayTo) VALUES ('Reviewed', '45678', 1, 1, 0, 1, '04:00:00', '05:00:00', '06:00:00', '07:00:00', '08:00:00', '09:00:00', '10:00:00', '11:00:00', '12:00:00', '13:00:00', '14:00:00', '15:00:00', '16:00:00', '17:00:00')

/********************************************************************************
                STORE RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PK_StoreID') 
  ALTER TABLE Store DROP CONSTRAINT [PK_StoreID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKStoreManagerID')
  ALTER TABLE [Store] DROP CONSTRAINT [FKStoreManagerID];
/*IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKJobOpeningID2')
  ALTER TABLE [Store] DROP CONSTRAINT [FKJobOpeningID];*/
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'Store') 
  DROP TABLE Store;
CREATE TABLE Store (
  Store_ID          int IDENTITY(1,1),
  Location          varchar(50) NULL UNIQUE,
  Manager_ID		int NOT NULL,
  /*JobOpening_ID     int NOT NULL,*/
  CONSTRAINT [PK_StoreID] PRIMARY KEY (Store_ID ASC),
  CONSTRAINT [FKStoreManagerID] FOREIGN KEY (Manager_ID) REFERENCES StoreManager(StoreManager_ID) 
    ON DELETE CASCADE ON UPDATE CASCADE,
  /*CONSTRAINT [FKJobOpeningID2] FOREIGN KEY (JobOpening_ID) REFERENCES JobOpening (JobOpening_ID),*/
  CONSTRAINT [CHK_Store] CHECK (DATALENGTH(Location) > 0)
);

INSERT INTO Store (Location, Manager_ID) VALUES ('Lake Oswego', 1);
INSERT INTO Store (Location, Manager_ID) VALUES ('Hillsboro', 2);
INSERT INTO Store (Location, Manager_ID) VALUES ('Wilsonville', 3);
INSERT INTO Store (Location, Manager_ID) VALUES ('Hawaii', 5);
INSERT INTO Store (Location, Manager_ID) VALUES ('New York', 4);


/********************************************************************************
                JOB RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKJobID')
  ALTER TABLE Job DROP CONSTRAINT [PKJobID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKJobReqID')
  ALTER TABLE [Job] DROP CONSTRAINT [FKJobReqID];
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
INSERT INTO Job(Title) VALUES ('Tester');
INSERT INTO Job(Title) VALUES ('Manager');

/********************************************************************************
                JOB OPENING RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKJobOpeningID')
  ALTER TABLE [JobOpening] DROP CONSTRAINT [PKJobOpeningID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKJobOpeningJobID')
  ALTER TABLE [JobOpening] DROP CONSTRAINT [FKJobOpeningJobID];
  IF EXISTS (SELECT * FROM sys.default_constraints WHERE Name = N'FKStoreID')
  ALTER TABLE [JobOpening] DROP CONSTRAINT [FKStoreID];
IF EXISTS (SELECT * FROM information_schema.tables WHERE table_name = N'JobOpening') 
  DROP TABLE JobOpening;
CREATE TABLE JobOpening (
  JobOpening_ID     int IDENTITY (1,1),
  OpenDate          date NOT NULL,
  Job_ID            int  NOT NULL,
  Approved          tinyint NULL,
  Description       text NULL,
  Store_ID          int  NOT NULL,
  CONSTRAINT [PKJobOpeningID] PRIMARY KEY (JobOpening_ID ASC),
  CONSTRAINT [FKJobOpeningJobID] FOREIGN KEY (Job_ID) REFERENCES Job (Job_ID),
  CONSTRAINT [FKStoreID] FOREIGN KEY (Store_ID) REFERENCES Store (Store_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);    

INSERT INTO JobOpening (OpenDate, Job_ID, Approved, Description, Store_ID) VALUES ('04-08-2014', 1, 1, 'Description here...', 1);
INSERT INTO JobOpening (OpenDate, Job_ID, Approved, Description, Store_ID) VALUES ('09-13-2013', 4, 1, 'Description here...', 1);
INSERT INTO JobOpening (OpenDate, Job_ID, Approved, Description, Store_ID) VALUES ('03-18-2014', 3, 0, 'Description here...', 1);
INSERT INTO JobOpening (OpenDate, Job_ID, Approved, Description, Store_ID) VALUES ('11-28-2013', 2, 0, 'Description here...', 1);


/********************************************************************************
              JOB REQUIREMENT RELATION
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKJobReqID')
  ALTER TABLE JobRequirement DROP CONSTRAINT [PKJobReqID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKJobOpeningID')
  ALTER TABLE JobRequirement DROP CONSTRAINT [FKJobOpeningID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSkillReqID')
  ALTER TABLE JobRequirement DROP CONSTRAINT [FKSkillReqID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'JobRequirement')
  DROP TABLE JobRequirement;
CREATE TABLE JobRequirement (
  JobRequirement_ID int     NOT NULL IDENTITY(1, 1),
  JobOpening_ID     int     NOT NULL,
  Skill_ID          int     NOT NULL,
  Notes             varchar(20) NULL,
  CONSTRAINT [PKJobReqID] PRIMARY KEY (JobRequirement_ID ASC),
  CONSTRAINT [FKJobOpeningID] FOREIGN KEY (JobOpening_ID) REFERENCES JobOpening (JobOpening_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKSkillReqID] FOREIGN KEY (Skill_ID) REFERENCES Skill (Skill_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR JOB_REQUIREMENTS RELATION */
INSERT INTO JobRequirement(JobOpening_ID, Skill_ID, Notes) VALUES (1, 2, '');
INSERT INTO JobRequirement(JobOpening_ID, Skill_ID, Notes) VALUES (1, 3, '');
INSERT INTO JobRequirement(JobOpening_ID, Skill_ID, Notes) VALUES (1, 5, 'Preferred');
INSERT INTO JobRequirement(JobOpening_ID, Skill_ID, Notes) VALUES (2, 4, '');
INSERT INTO JobRequirement(JobOpening_ID, Skill_ID, Notes) VALUES (2, 1, 'Not Required');



/********************************************************************************
                APPLIED RELATION  
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKAppliedID')
  ALTER TABLE Applied DROP CONSTRAINT [PKAppliedID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantID')
  ALTER TABLE Applied DROP CONSTRAINT [FKApplicantID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantionID')
  ALTER TABLE Applied DROP CONSTRAINT [FKApplicationID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKJobOpeningIDForApplied')
  ALTER TABLE Applied DROP CONSTRAINT [FKJobOpeningIDForApplied];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Applied')
  DROP TABLE Applied;
CREATE TABLE Applied (
  Applied_ID      int IDENTITY(1,1) NOT NULL,
  Applicant_ID    int NOT NULL,
  Application_ID  int NOT NULL UNIQUE,
  JobOpening_ID   int NOT NULL,
  DateApplied     date NOT NULL,
  CONSTRAINT [PKAppliedID] PRIMARY KEY (Applied_ID ASC),
  CONSTRAINT [FKApplicantID] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKApplicationID] FOREIGN KEY (Application_ID) REFERENCES Application (Application_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKJobOpeningIDForApplied] FOREIGN KEY (JobOpening_ID) REFERENCES JobOpening (JobOpening_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR APPLIED RELATION */
INSERT INTO Applied(Applicant_ID, Application_ID, JobOpening_ID, DateApplied) VALUES (1, 1, 1, GETDATE());
INSERT INTO Applied(Applicant_ID, Application_ID, JobOpening_ID, DateApplied) VALUES (2, 2, 2, GETDATE());
INSERT INTO Applied(Applicant_ID, Application_ID, JobOpening_ID, DateApplied) VALUES (3, 3, 3, '12-30-2013');
INSERT INTO Applied(Applicant_ID, Application_ID, JobOpening_ID, DateApplied) VALUES (4, 4, 4, '12-30-2013');

/********************************************************************************
                SCHOOL RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKSchoolID')
  ALTER TABLE School DROP CONSTRAINT [PKSchoolID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'School')
  DROP TABLE School;
CREATE TABLE School (
  School_ID     int IDENTITY(1,1) NOT NULL,
  School_Name   varchar(255) NOT NULL,
  School_Address  varchar(255) NULL,
  CONSTRAINT [PKSchoolID] PRIMARY KEY (School_ID ASC),
  CONSTRAINT  [CHK_SchoolName] CHECK (DATALENGTH(School_Name) > 0 AND ISNUMERIC(School_Name) = 0)
);

/* DATA FOR SCHOOL RELATION */
INSERT INTO School(School_Name, School_Address) VALUES ('PCC', '123 N Sky St, Sunnyville');
INSERT INTO School(School_Name, School_Address) VALUES ('OIT', '234 E Wind St, Sunnyville');
INSERT INTO School(School_Name, School_Address) VALUES ('CCC', '345 W Tower Ct, Sunnyville');
INSERT INTO School(School_Name, School_Address) VALUES ('WSU', '456 S Cloud Ct, Sunnyville');

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
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (1, 1, '2002-09-01', '2006-06-01', 1, 'BS Computer Science');
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (2, 2, '2000-09-01', '2004-06-01', 1, 'BA Public Speaking');
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (2, 3, '2005-09-01', '2006-06-01', 1, 'Master''s in Public Speaking');
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (3, 3, '2002-09-01', '2006-06-01', 0, 'Economics');
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (4, 4, '2002-09-01', '2006-06-01', 1, 'BA Psychology');
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (4, 3, '2006-09-01', '2007-06-01', 1, 'Master''s in Psychology');
INSERT INTO Education(Applicant_ID, School_ID, YearsAttendedFrom, YearsAttendedTo, Graduated, DegreeAndMajor) VALUES (4, 2, '2008-09-01', '2009-06-01', 1, 'Master''s in Teaching');

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
INSERT INTO Employer(Name, EmployerAddress, PhoneNumber) VALUES ('Intely', '123 NW Way, Sunnyville', '123-412-2345');
INSERT INTO Employer(Name, EmployerAddress, PhoneNumber) VALUES ('Googles', '234 SW Way, Sunnyville', '123-414-1234');
INSERT INTO Employer(Name, EmployerAddress, PhoneNumber) VALUES ('Microsoftor', '345 SE Ct, Sunnyville', '123-122-4334');
INSERT INTO Employer(Name, EmployerAddress, PhoneNumber) VALUES ('Applet', '456 NE Ct, Sunnyville', '123-111-1111');

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
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (1, 1, 1, '2002-09-01', '2006-06-01', 'Jay', 'First Year Worker', '24000', '26000', 'Another job opportunity opened up...', 'Taking care of first year worker duties.');
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (2, 1, 0, '2000-09-01', '2004-06-01', 'Quan', 'First Year Worker', '24000', '26000', 'Another job opportunity opened up...', 'Taking care of first year worker duties.');
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (2, 2, 1, '2005-09-01', '2006-06-01', 'Sherry', 'Second Year Worker', '30000', '1', 'Felt like moving on...', 'Taking care of second year worker duties.');
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (3, 1, 0, '2002-09-01', '2006-06-01', 'Rich', 'First Year Worker', '24000', '26000', 'Another job opportunity opened up...', 'Taking care of first year worker duties.');
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (4, 1, 0, '2002-09-01', '2006-06-01', 'Rich', 'First Year Worker', '24000', '26000', 'Another job opportunity opened up...', 'Taking care of first year worker duties.');
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (4, 2, 0, '2002-09-01', '2006-06-01', 'Jay', 'Second Year Worker', '26000', '30000', 'Wanted to remain upwardly mobile.', 'Taking care of second year worker duties.');
INSERT INTO Employment(Applicant_ID, Employer_ID, MayWeContactCurrentEmployer, EmployedFrom, EmployedTo, Supervisor, Position, StartingSalary, EndingSalary, ReasonForLeaving, Responsibilities) VALUES (4, 3, 0, '2002-09-01', '2006-06-01', 'Buffalo', 'Third Year Worker', '35000', '50000', 'Wanted to face new challenges.', 'Taking care of third year worker duties.');

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

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKAssociateID')
  ALTER TABLE Associate DROP CONSTRAINT [PKAssociateID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantIDForAssociate')
  ALTER TABLE Associate DROP CONSTRAINT [FKApplicantIDForAssociate];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKReferenceIDForAssociate')
  ALTER TABLE Reference DROP CONSTRAINT [FKAssociateIDForReference];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Associate')
  DROP TABLE Associate;
CREATE TABLE Associate (
  Associate_ID    int NOT NULL IDENTITY(1, 1),
  Applicant_ID    int NOT NULL,
  Reference_ID	  int NOT NULL,
  Name            varchar(255) NULL,
  Phone           varchar(50) NULL,
  Title           varchar(50) NULL,
  CONSTRAINT [PKAssociateID] PRIMARY KEY (Associate_ID ASC),
  CONSTRAINT [FKApplicantIDForAssociate] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKReferenceIDForAssociate] FOREIGN KEY (Reference_ID) REFERENCES Reference (Reference_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
);

/* DATA FOR EMPLOYMENT_REFERENCE RELATION */
INSERT INTO Associate(Applicant_ID, Reference_ID, Name, Phone, Title) VALUES (1,1, 'Bill Gates', '911', 'CEO');

/********************************************************************************
                MULTIPLE CHOICE OPTIONS RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKMCOptionID')
  ALTER TABLE MCOptions DROP CONSTRAINT [PKMCOptionID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'MCOptions')
  DROP TABLE MCOptions;
CREATE TABLE MCOptions (
  MCOption_ID			int IDENTITY(1,1) NOT NULL,
  MCOptionDescription	varchar(255) NOT NULL,
  CONSTRAINT [PKMCOptionID] PRIMARY KEY (MCOption_ID ASC)
)

/* DATA FOR MULTIPLE CHOICE OPTIONS RELATION */
INSERT INTO MCOptions(MCOptionDescription) VALUES ('Yes');
INSERT INTO MCOptions(MCOptionDescription) VALUES ('No');
INSERT INTO MCOptions(MCOptionDescription) VALUES ('Don''t Know');
INSERT INTO MCOptions(MCOptionDescription) VALUES ('Maybe');
INSERT INTO MCOptions(MCOptionDescription) VALUES ('Not Sure');

/********************************************************************************
                MULTIPLE CHOICE QUESTIONS RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKMCQuestionID')
  ALTER TABLE MCQuestions DROP CONSTRAINT [PKMCQUestionID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'MCQuestions')
  DROP TABLE MCQuestions;
CREATE TABLE MCQuestions (
  MCQuestion_ID				int IDENTITY(1,1) NOT NULL,
  MCQuestionDescription		varchar(255) NOT NULL,
  CONSTRAINT [PKMCQuestionID]	PRIMARY KEY (MCQuestion_ID ASC)
)

/* DATA FOR MULTIPLE CHOICE QUESTIONS RELATION */
INSERT INTO MCQuestions(MCQuestionDescription) VALUES ('Can you lift 50 lbs?');
INSERT INTO MCQuestions(MCQuestionDescription) VALUES ('Have you ever been convicted for any crime?');

/********************************************************************************
                QUESTION BANK RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKMCQuestionBankID')
  ALTER TABLE QuestionBank DROP CONSTRAINT [PKMCQuestionBankID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKMCQuestionIDForQuestionBank')
  ALTER TABLE QuestionBank DROP CONSTRAINT [FKMCQuestionIDForQuestionBank];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKMCOptionIDForQuestionBank')
  ALTER TABLE QuestionBank DROP CONSTRAINT [FKMCOptionIDForQuestionBank];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'QuestionBank')
  DROP TABLE QuestionBank;
CREATE TABLE QuestionBank (
  QuestionBank_ID			int IDENTITY(1,1) NOT NULL,
  MCQuestion_ID				int NOT NULL,
  MCOption_ID				int NOT NULL,
  CONSTRAINT [PKMCQuestionBankID]	PRIMARY KEY (QuestionBank_ID ASC),
  CONSTRAINT [FKMCQuestionIDForQuestionBank] FOREIGN KEY (MCQuestion_ID) REFERENCES MCQuestions (MCQuestion_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKMCOptionIDForQuestionBank] FOREIGN KEY (MCOption_ID) REFERENCES MCOptions (MCOption_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
)

/* DATA FOR QUESTION BANK RELATION */
INSERT INTO QuestionBank(MCQuestion_ID, MCOption_ID) VALUES (1, 1);
INSERT INTO QuestionBank(MCQuestion_ID, MCOption_ID) VALUES (1, 2);
INSERT INTO QuestionBank(MCQuestion_ID, MCOption_ID) VALUES (2, 1);
INSERT INTO QuestionBank(MCQuestion_ID, MCOption_ID) VALUES (2, 2);

/********************************************************************************
                ASSESSMENT RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKAssessmentID')
  ALTER TABLE Assessment DROP CONSTRAINT [PKAssessmentID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantIDForAssessment')
  ALTER TABLE Assessment DROP CONSTRAINT [FKApplicantIDForAssessment];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKQuestionBankIDForAssessment')
  ALTER TABLE Assessment DROP CONSTRAINT [FKQuestionBankIDForAssessment];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Assessment')
  DROP TABLE Assessment;
CREATE TABLE Assessment (
  Assessment_ID		  int IDENTITY(1,1) NOT NULL,
  Applicant_ID		  int NOT NULL,
  QuestionBank_ID	  int NOT NULL,
  CONSTRAINT [PKAssessmentID] PRIMARY KEY (Assessment_ID ASC),
  CONSTRAINT [FKApplicantIDForAssessment] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKQuestionBankIDForAssessment] FOREIGN KEY (QuestionBank_ID) REFERENCES QuestionBank (QuestionBank_ID)
    ON DELETE CASCADE ON UPDATE CASCADE
)

/* DATA FOR ASSESSMENT RELATION */
INSERT INTO Assessment(Applicant_ID, QuestionBank_ID) VALUES (1, 1);
INSERT INTO Assessment(Applicant_ID, QuestionBank_ID) VALUES (2, 2);
INSERT INTO Assessment(Applicant_ID, QuestionBank_ID) VALUES (3, 2);

/********************************************************************************
                SHORT ANSWER QUESTIONS RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKSAQuestionID')
  ALTER TABLE SAQuestions DROP CONSTRAINT [PKSAQuestionID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'SAQuestions')
  DROP TABLE SAQuestions;
CREATE TABLE SAQuestions (
  SAQuestion_ID			int IDENTITY(1,1) NOT NULL,
  SAQuestionDescription	varchar(255) NOT NULL,
  CONSTRAINT [PKSAQuestionID] PRIMARY KEY (SAQuestion_ID ASC)
)

/* DATA FOR SHORT ANSWER QUESTION RELATION */
INSERT INTO SAQuestions(SAQuestionDescription) VALUES ('What are your weaknesses?');
INSERT INTO SAQuestions(SAQuestionDescription) VALUES ('What are your strengths?');
INSERT INTO SAQuestions(SAQuestionDescription) VALUES ('What would you do to resolve a conflict?');

/********************************************************************************
                SHORT ANSWER RESPONSES RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKSAResponseID')
  ALTER TABLE SAResponses DROP CONSTRAINT [PKSAResponseID];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'SAResponses')
  DROP TABLE SAResponses;
CREATE TABLE SAResponses (
  SAResponse_ID				int IDENTITY(1,1) NOT NULL,
  SAResponseDescription		varchar(255) NOT NULL,
  CONSTRAINT [PKSAResponseID]	PRIMARY KEY (SAResponse_ID ASC)
)

/* DATA FOR MULTIPLE CHOICE QUESTIONS RELATION */
INSERT INTO SAResponses(SAResponseDescription) VALUES ('I don''t have any weaknesses.');
INSERT INTO SAResponses(SAResponseDescription) VALUES ('I try not to be in any sort of conflicts.');

/********************************************************************************
                AUTHORIZATION
*********************************************************************************/

GO
SET ANSI_PADDING ON
GO
/****** Object:  Table UserProfile    Script Date: 4/13/2014 9:56:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'UserProfile')
  DROP TABLE UserProfile;
CREATE TABLE UserProfile(
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table webpages_Membership    Script Date: 4/13/2014 9:56:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'webpages_Membership')
  DROP TABLE webpages_Membership;
CREATE TABLE webpages_Membership(
	[UserId] [int] NOT NULL,
	[CreateDate] [datetime] NULL,
	[ConfirmationToken] [nvarchar](128) NULL,
	[IsConfirmed] [bit] NULL,
	[LastPasswordFailureDate] [datetime] NULL,
	[PasswordFailuresSinceLastSuccess] [int] NOT NULL,
	[Password] [nvarchar](128) NOT NULL,
	[PasswordChangedDate] [datetime] NULL,
	[PasswordSalt] [nvarchar](128) NOT NULL,
	[PasswordVerificationToken] [nvarchar](128) NULL,
	[PasswordVerificationTokenExpirationDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table webpages_OAuthMembership    Script Date: 4/13/2014 9:56:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'webpages_OAuthMembership')
  DROP TABLE webpages_OAuthMembership;
CREATE TABLE webpages_OAuthMembership(
	[Provider] [nvarchar](30) NOT NULL,
	[ProviderUserId] [nvarchar](100) NOT NULL,
	[UserId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Provider] ASC,
	[ProviderUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table webpages_Roles    Script Date: 4/13/2014 9:56:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'webpages_Roles')
  DROP TABLE webpages_Roles;
CREATE TABLE webpages_Roles(
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](256) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table webpages_UsersInRoles    Script Date: 4/13/2014 9:56:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'webpages_UsersInRoles')
  DROP TABLE webpages_UsersInRoles;
CREATE TABLE webpages_UsersInRoles(
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]


SET IDENTITY_INSERT UserProfile ON 
INSERT UserProfile ([UserId], [UserName]) VALUES (1, N'firstuser')
INSERT UserProfile ([UserId], [UserName]) VALUES (2, N'seconduser')
INSERT UserProfile ([UserId], [UserName]) VALUES (3, N'hiringmanager')
INSERT UserProfile ([UserId], [UserName]) VALUES (4, N'storemanager')
SET IDENTITY_INSERT UserProfile OFF

INSERT webpages_Membership ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (1, CAST(0x0000A30A014888CB AS DateTime), NULL, 1, NULL, 0, N'AK6QsQuK6SsoW8qMDh2BBGT84QRJu4GrHh8lW9CkxN3828bGC91oq5vl+Syjr7hErw==', CAST(0x0000A30A014888CB AS DateTime), N'', NULL, NULL)
INSERT webpages_Membership ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (2, CAST(0x0000A30A0167AC10 AS DateTime), NULL, 1, NULL, 0, N'ABvcOO2m6JZMXxccn+Rp5VGa1Ut7GuPqxEEfs8StdcShWCgM8S6HpH/RebrNOypkoQ==', CAST(0x0000A30A0167AC10 AS DateTime), N'', NULL, NULL)
INSERT webpages_Membership ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (3, CAST(0x0000A311010C5048 AS DateTime), NULL, 1, NULL, 0, N'AHx7OaRY0CIik+s8XuBottp6y8ADLThImZirjDHu0RcGoVFJ+XQZM+2b7pE9itbjOQ==', CAST(0x0000A311010C5048 AS DateTime), N'', NULL, NULL)
INSERT webpages_Membership ([UserId], [CreateDate], [ConfirmationToken], [IsConfirmed], [LastPasswordFailureDate], [PasswordFailuresSinceLastSuccess], [Password], [PasswordChangedDate], [PasswordSalt], [PasswordVerificationToken], [PasswordVerificationTokenExpirationDate]) VALUES (4, CAST(0x0000A3110115FE10 AS DateTime), NULL, 1, NULL, 0, N'ABYunDqQHj9w5GYYvnDbEtfXzszJJ5sjuZJWr/DJMH7EzOp2dU/DY8WB6l68l1bOcw==', CAST(0x0000A3110115FE10 AS DateTime), N'', NULL, NULL)
SET IDENTITY_INSERT webpages_Roles ON 

INSERT webpages_Roles ([RoleId], [RoleName]) VALUES (1, N'Administrator')
INSERT webpages_Roles ([RoleId], [RoleName]) VALUES (2, N'Applicant')
INSERT webpages_Roles ([RoleId], [RoleName]) VALUES (3, N'HiringManager')
INSERT webpages_Roles ([RoleId], [RoleName]) VALUES (4, N'StoreManager')
SET IDENTITY_INSERT webpages_Roles OFF
INSERT webpages_UsersInRoles ([UserId], [RoleId]) VALUES (1, 1)
INSERT webpages_UsersInRoles ([UserId], [RoleId]) VALUES (2, 2)
INSERT webpages_UsersInRoles ([UserId], [RoleId]) VALUES (3, 3)
INSERT webpages_UsersInRoles ([UserId], [RoleId]) VALUES (4, 4)
SET ANSI_PADDING ON

GO
/****** Object:  Index [UQ__webpages__8A2B616042D5758F]    Script Date: 4/13/2014 9:56:43 AM ******/
ALTER TABLE webpages_Roles ADD UNIQUE NONCLUSTERED 
(
	[RoleName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE webpages_Membership ADD  DEFAULT ((0)) FOR [IsConfirmed]
GO
ALTER TABLE webpages_Membership ADD  DEFAULT ((0)) FOR [PasswordFailuresSinceLastSuccess]
GO
ALTER TABLE webpages_UsersInRoles  WITH CHECK ADD  CONSTRAINT [fk_RoleId] FOREIGN KEY([RoleId])
REFERENCES webpages_Roles ([RoleId])
GO
ALTER TABLE webpages_UsersInRoles CHECK CONSTRAINT [fk_RoleId]
GO
ALTER TABLE webpages_UsersInRoles  WITH CHECK ADD  CONSTRAINT [fk_UserId] FOREIGN KEY([UserId])
REFERENCES UserProfile ([UserId])
GO
ALTER TABLE webpages_UsersInRoles CHECK CONSTRAINT [fk_UserId]
GO
USE [master]
GO
ALTER DATABASE [AESDatabase] SET  READ_WRITE 
GO
USE [AESDatabase]
GO

/********************************************************************************
                INTERVIEW RELATION 
*********************************************************************************/

IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'PKInterviewID')
  ALTER TABLE Interview DROP CONSTRAINT [PKInterviewID];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKApplicantIDForInterview')
  ALTER TABLE Interview DROP CONSTRAINT [FKApplicantIDForInterview];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSAQuestionIDForInterview')
  ALTER TABLE Interview DROP CONSTRAINT [FKSAQuestionIDForInterview];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKSAResponseIDForInterview')
  ALTER TABLE Interview DROP CONSTRAINT [FKSAResponseIDForInterview];
IF EXISTS (SELECT * FROM sys.default_constraints WHERE name = N'FKUserIDForInterview')
  ALTER TABLE Interview DROP CONSTRAINT [FKUserIDForInterview];
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = N'Interview')
  DROP TABLE Interview;
CREATE TABLE Interview (
  Interview_ID				int IDENTITY(1,1) NOT NULL,
  Applicant_ID				int NOT NULL,
  SAQuestion_ID				int NOT NULL,
  SAResponse_ID				int NOT NULL,
  UserID					int NOT NULL,
  CONSTRAINT [PKInterviewID]	PRIMARY KEY (Interview_ID ASC),
  CONSTRAINT [FKApplicantIDForInterview] FOREIGN KEY (Applicant_ID) REFERENCES Applicant (Applicant_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKSAQuestionIDForInterview] FOREIGN KEY (SAQuestion_ID) REFERENCES SAQuestions (SAQuestion_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKSAResponseIDForInterview] FOREIGN KEY (SAResponse_ID) REFERENCES SAResponses (SAResponse_ID)
    ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT [FKUserIDForInterview] FOREIGN KEY (UserID) REFERENCES UserProfile (UserID)
    ON DELETE CASCADE ON UPDATE CASCADE
)

/* DATA FOR INTERVIEW RELATION */
INSERT INTO Interview(Applicant_ID, SAQuestion_ID, SAResponse_ID, UserID) VALUES (1, 1, 1, 3);
INSERT INTO Interview(Applicant_ID, SAQuestion_ID, SAResponse_ID, UserID) VALUES (2, 2, 2, 4);
INSERT INTO Interview(Applicant_ID, SAQuestion_ID, SAResponse_ID, UserID) VALUES (3, 3, 1, 4);
INSERT INTO Interview(Applicant_ID, SAQuestion_ID, SAResponse_ID, UserID) VALUES (4, 2, 1, 3);
