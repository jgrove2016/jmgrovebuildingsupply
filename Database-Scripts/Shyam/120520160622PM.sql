-- table for attendance history
CREATE TABLE [dbo].[tbl_AttendanceHistory](
	[AttendanceHistoryId] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [int] NULL,
	[Date] [datetime] NULL,
	[Time_in] [datetime] NULL,
	[Time_out] [datetime] NULL,
 CONSTRAINT [PK_tbl_AttendanceHistory] PRIMARY KEY CLUSTERED 
(
	[AttendanceHistoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


-- table for miss punch report
CREATE TABLE [dbo].[tbl_MisspunchReport](
	[ReportMissPunchId] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [int] NULL,
	[Date] [datetime] NULL,
	[Reason] [nvarchar](500) NULL,
	[Status] [nvarchar](50) NULL,
 CONSTRAINT [PK_tbl_MisspunchReport] PRIMARY KEY CLUSTERED 
(
	[ReportMissPunchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[getAttendanceHistoryByEmployeeId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[getAttendanceHistoryByEmployeeId]

GO
-- sp for get attendance by employeeid
CREATE PROCEDURE [dbo].[getAttendanceHistoryByEmployeeId] 
(
@EmployeeID int
)
AS
BEGIN
select Date,LTRIM(RIGHT(CONVERT(VARCHAR(20), Time_in, 100), 7)) as CheckInTime,
LTRIM(RIGHT(CONVERT(VARCHAR(20),Time_out, 100), 7)) as CheckOutTime,
Cast(((Cast(DateDiff(minute, Time_in, Time_out) as decimal(10,2)))/60) as decimal(10,2)) 
as TotalHours 
from tbl_AttendanceHistory 
where Employee_id=@EmployeeID order by Date
END
go

GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[postMissPunchReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[postMissPunchReport]
go
-- sp for post miss punch reports
CREATE PROCEDURE [dbo].[postMissPunchReport]
(
@EmployeeID int,
@Date datetime,
@Reason nvarchar(500)
)
AS
BEGIN
insert into tbl_MisspunchReport(Employee_id,Date,Reason,Status)
values(@EmployeeID,@Date,@Reason,'Pending')
END
go
--sp for get misspunchreport by employeeid
Create PROCEDURE getMissPunchReportsByEmployeeId
(
@EmployeeID int
)
AS
BEGIN
select Date,Reason,Status from tbl_MisspunchReport where Employee_id=@EmployeeID
END
go