CREATE TABLE [dbo].[Employee](
	[ID]   [INT] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [NVARCHAR](255) NOT NULL,
	[Salary] [int] NOT NULL,
	[Designation] [NVARCHAR](100) NOT NULL,
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
	[CreateDate] [DATETIME]  NOT NULL ,
	[CreatedBy] [NVARCHAR] (255)  NOT NULL,
	[ModifiedDate] [DATETIME]  NOT NULL ,
	[ModifiedBy] [NVARCHAR] (255)  NOT NULL ,
);


CREATE TABLE [dbo].[Leave](
	[Type] [INT] NOT NULL,
	[NumberOfLeaves] [INT] NOT NULL,
	[LeaveStatus] [INT] NOT NULL,
	[EmployeeID] [INT] FOREIGN KEY REFERENCES Employee(ID) NOT NULL,
	[CreateDate] [DATETIME]  NOT NULL ,
	[CreatedBy] [NVARCHAR] (255)  NOT NULL,
	[ModifiedDate] [DATETIME]  NOT NULL ,
	[ModifiedBy] [NVARCHAR] (255)  NOT NULL ,
);
