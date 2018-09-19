CREATE DATABASE Organization;
DROP TABLE Employee;
DROP TABLE Task;
DROP TABLE Leave;
DROP TABLE Organization;

CREATE TABLE [dbo].[Organization](
	[ID]   [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [NVARCHAR](255) NOT NULL,
	[Location] [NVARCHAR] (255) NOT NULL,
	[TotalSickLeaves] [INT] NOT NULL,
	[TotalCausalLeaves] [INT] NOT NULL,
	[TotalSpecialLeaves] [INT] NOT NULL,
	[CreateDate] [DATETIME]  NOT NULL ,
	[CreatedBy] [NVARCHAR] (255)  NOT NULL,
	[ModifiedDate] [DATETIME]  NOT NULL ,
	[ModifiedBy] [NVARCHAR] (255)  NOT NULL ,
	);

CREATE TABLE [dbo].[Employee](
	[ID]   [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [NVARCHAR](255) NOT NULL,
	[Salary] [int] NOT NULL,
	[Designation] [INT] NOT NULL,
	[ReportingManagerID] [INT] ,
	[CreateDate] [DATETIME]  NOT NULL ,
	[CreatedBy] [NVARCHAR] (255)  NOT NULL,
	[ModifiedDate] [DATETIME]  NOT NULL ,
	[ModifiedBy] [NVARCHAR] (255)  NOT NULL ,	
);

CREATE TABLE [dbo].[Task](
	[ID]   [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[EmployeeID] [INT]  FOREIGN KEY REFERENCES Employee(ID) NOT NULL,
	[LeaveType] [INT] NOT NULL,
	[NumberOfLeaves] [INT] NOT NULL,
	[Description] [NVARCHAR](255) NOT NULL,
	[CreateDate] [DATETIME]  NOT NULL ,
	[CreatedBy] [NVARCHAR] (255)  NOT NULL,
	[ModifiedDate] [DATETIME]  NOT NULL ,
	[ModifiedBy] [NVARCHAR] (255)  NOT NULL ,
);

CREATE TABLE [dbo].[Leave](
	[ID]   [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY ,
	[Type] [INT] NOT NULL,
	[NumberOfLeaves] [INT] NOT NULL,
	[EmployeeID] [INT] FOREIGN KEY REFERENCES Employee(ID) NOT NULL,
	[CreateDate] [DATETIME]  NOT NULL ,
	[CreatedBy] [NVARCHAR] (255)  NOT NULL,
	[ModifiedDate] [DATETIME]  NOT NULL ,
	[ModifiedBy] [NVARCHAR] (255)  NOT NULL ,
);

select * from Organization;
select * from Employee;
select * from Leave;
select * from Task;

