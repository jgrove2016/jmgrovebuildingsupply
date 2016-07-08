USE [jgrove_JGP]
GO
/****** Object:  StoredProcedure [dbo].[usp_SearchUserTasks]    Script Date: 6/30/2016 8:05:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yogesh Keraliya
-- Create date: 06/29/2016
-- Description:	This will search task based on search parameters.
-- =============================================
CREATE PROCEDURE [dbo].[usp_SearchUserTasks]
(
@UserID INT = NULL,
@Title  VARCHAR(250) = NULL,
@Designation VARCHAR(50) = NULL,
@Status TINYINT = NULL,
@CreatedOn DATETIME =  NULL,
@Start INT = 0, -- pagenumber
@PageLimit  INT = 5 -- pagesize
)	
AS
BEGIN

SET @Start = @Start + 1

;WITH Tasklist
AS
(	
SELECT        Tasks.TaskId, Tasks.Title, UsersMaster.Designation, UsersMaster.InstallId, UsersMaster.FristName, Tasks.[Status], Tasks.DueDate,
			  Row_number() OVER(ORDER BY CONVERT(varchar,Tasks.[DueDate],101)) AS rownum

FROM          tblTask AS Tasks INNER JOIN
              tblTaskUser AS TaskUsers ON Tasks.TaskId = TaskUsers.TaskId INNER JOIN
              tblInstallUsers AS UsersMaster ON TaskUsers.UserId = UsersMaster.Id

WHERE

TaskUsers.[UserId] = ISNULL(@UserID , TaskUsers.[UserId]) AND
Tasks.[Title] LIKE '%' + ISNULL(@Title,Tasks.[Title]) + '%' AND
UsersMaster.[Designation] = ISNULL(@Designation , UsersMaster.[Designation]) AND
Tasks.[Status] = ISNULL(@Status,Tasks.[Status]) AND
CONVERT(varchar,Tasks.[CreatedOn],101) =  ISNULL(CONVERT(varchar,@CreatedOn,101), CONVERT(varchar,Tasks.[CreatedOn],101)) 

)

SELECT * 
FROM   Tasklist 
WHERE  rownum BETWEEN ( @Start - 1 ) * @PageLimit + 1 AND @Start * @PageLimit


-- get total number of records for virtual count

SELECT        COUNT(Tasks.TaskId) AS VirtualCount
FROM          tblTask AS Tasks INNER JOIN
              tblTaskUser AS TaskUsers ON Tasks.TaskId = TaskUsers.TaskId INNER JOIN
              tblInstallUsers AS UsersMaster ON TaskUsers.UserId = UsersMaster.Id

WHERE

TaskUsers.[UserId] = ISNULL(@UserID , TaskUsers.[UserId]) AND
Tasks.[Title] LIKE '%' + ISNULL(@Title,Tasks.[Title]) + '%' AND
UsersMaster.[Designation] = ISNULL(@Designation , UsersMaster.[Designation]) AND
Tasks.[Status] = ISNULL(@Status,Tasks.[Status]) AND
CONVERT(varchar,Tasks.[CreatedOn],101) =  ISNULL(CONVERT(varchar,@CreatedOn,101), CONVERT(varchar,Tasks.[CreatedOn],101))


END


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Yogesh Keraliya
-- Create date: 096/30/2016
-- Description:	Fetch all sales and install users for whom tasks are available in system
-- =============================================
CREATE PROCEDURE usp_GetUsersNDesignationForTaskFilter 
AS
BEGIN

SET NOCOUNT ON;

--- get all username for whom tasks are assgined
	SELECT        Users.Id, Users.FristName AS FirstName
	FROM            tblInstallUsers AS Users INNER JOIN
                         tblTaskUser AS Tasks ON Users.Id = Tasks.UserId
WHERE        (Users.UserType = 'SalesUser') OR
                         (Users.UserType = 'sales') OR
                         (Users.UserType = 'installer')
GROUP BY Users.Id, Users.FristName
ORDER BY Users.FristName

-- get all user's designations for whom tasks are assigned.
SELECT        Users.Id, Users.Designation
FROM            tblInstallUsers AS Users INNER JOIN
                         tblTaskUser AS Tasks ON Users.Id = Tasks.UserId
WHERE        (Users.UserType = 'SalesUser') OR
                         (Users.UserType = 'sales') OR
                         (Users.UserType = 'installer')
GROUP BY Users.Id, Users.Designation
ORDER BY Users.Designation


END
GO

----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
-- Date :  04/07/2016

/*
   Monday, July 4, 20164:42:23 PM
   User: sa
   Server: KERLAP01\DBS001
   Database: jgrove_JGP
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.tblTaskUser SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_tblTaskUserFiles
	(
	Id int NOT NULL IDENTITY (1, 1),
	TaskUpdateID bigint NOT NULL,
	TaskId bigint NOT NULL,
	UserId int NOT NULL,
	Attachment varchar(MAX) NOT NULL,
	IsDeleted bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_tblTaskUserFiles SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_tblTaskUserFiles ON
GO
IF EXISTS(SELECT * FROM dbo.tblTaskUserFiles)
	 EXEC('INSERT INTO dbo.Tmp_tblTaskUserFiles (Id, TaskId, UserId, Attachment, IsDeleted)
		SELECT Id, TaskId, UserId, Attachment, IsDeleted FROM dbo.tblTaskUserFiles WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_tblTaskUserFiles OFF
GO
DROP TABLE dbo.tblTaskUserFiles
GO
EXECUTE sp_rename N'dbo.Tmp_tblTaskUserFiles', N'tblTaskUserFiles', 'OBJECT' 
GO
ALTER TABLE dbo.tblTaskUserFiles ADD CONSTRAINT
	PK_tblTaskUserFiles PRIMARY KEY CLUSTERED 
	(
	Id
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
ALTER TABLE dbo.tblTaskUserFiles ADD CONSTRAINT
	FK_tblTaskUserFiles_tblTaskUser FOREIGN KEY
	(
	TaskUpdateID
	) REFERENCES dbo.tblTaskUser
	(
	Id
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
DECLARE @v sql_variant 
SET @v = N'Relation to user''s task update id and attachment id.'
EXECUTE sp_addextendedproperty N'MS_Description', @v, N'SCHEMA', N'dbo', N'TABLE', N'tblTaskUserFiles', N'CONSTRAINT', N'FK_tblTaskUserFiles_tblTaskUser'
GO
COMMIT

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- =============================================  
-- Author:  MUNEER MK  
-- Create Date: 23-JUNE-2016  
-- Update By: Yogesh Keraliya
-- Description: TaskUpdate id inserted along with attachment.
-- Update Date: 07/04/2016
-- Description: SP_SaveOrDeleteTaskUserFiles  
-- =============================================  

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveOrDeleteTaskUserFiles]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_SaveOrDeleteTaskUserFiles]
GO

CREATE PROCEDURE [dbo].[SP_SaveOrDeleteTaskUserFiles]  
   
  @Mode tinyint, -- 0:Insert, 1: Update 2: Delete  
  @TaskUpDateId bigint=0,  
  @TaskId bigint,  
  @UserId int,  
  @Attachment varchar(MAX)  
  
AS  
BEGIN  
  
IF @Mode=0  
  
BEGIN  
  
INSERT INTO tblTaskUserFiles (TaskId,UserId,Attachment,TaskUpdateID,IsDeleted)   
VALUES(@TaskId,@UserId,@Attachment,@TaskUpDateId,0)  
  
END  
  
ELSE IF @Mode=1  
  
BEGIN  
  
UPDATE tblTaskUserFiles  
  
SET Attachment=@Attachment  WHERE TaskUpdateID = @TaskUpDateId
  
END  
  
ELSE IF @Mode=2 --DELETE  
  
BEGIN  
  
UPDATE tblTaskUserFiles  
  
SET IsDeleted =1  WHERE TaskUpdateID = @TaskUpDateId 
  
END  
  
END  
  
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[usp_GetTaskDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[usp_GetTaskDetails]
GO
-- =============================================
-- Author:		Yogesh Keraliya
-- Create date: 04/07/2016
-- Description:	Load all details of task for edit.
-- =============================================
-- usp_GetTaskDetails 1
CREATE PROCEDURE usp_GetTaskDetails 
(
	@TaskId int 
)	  
AS
BEGIN
	
	SET NOCOUNT ON;

-- task's main details
SELECT        Title, [Description], [Status], DueDate,Tasks.[Hours], (SELECT TOP (1) tblInstallUsers.Designation FROM  tblTaskUser AS ttu INNER JOIN
                         tblInstallUsers ON ttu.UserId = tblInstallUsers.Id  WHERE ttu.TaskId = @TaskId) AS Designation
FROM            tblTask AS Tasks
WHERE Tasks.TaskId = @TaskId


-- task's user details
SELECT        TaskUsers.Id,TaskUsers.UserId, TaskUsers.UserType, TaskUsers.Notes, TaskUsers.UserAcceptance, TaskUsers.UpdatedOn, TaskUsers.[Status], TaskUsers.TaskId, tblInstallUsers.FristName, tblInstallUsers.Designation,
             (SELECT COUNT(ttuf.[Id]) FROM dbo.tblTaskUserFiles ttuf WHERE ttuf.[TaskUpdateID] = TaskUsers.Id) AS AttachmentCount
FROM            tblTaskUser AS TaskUsers LEFT OUTER JOIN
                         tblInstallUsers ON TaskUsers.UserId = tblInstallUsers.Id
WHERE        (TaskUsers.TaskId = @TaskId)

    
END
GO


----------------------------------------------------------------------------------------------------------------------------------------------------------------------


-- Author:  MUNEER MK  
-- Create date: 23-JUNE-2016 
-- Update By: Yogesh Keraliya
-- Description: TaskUpdate id returned after insert.
-- Updated date : 29-JUNE-2016 
-- Description: SP_SaveOrDeleteTaskUser  
-- =============================================  
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveOrDeleteTaskUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_SaveOrDeleteTaskUser]
GO
CREATE PROCEDURE [dbo].[SP_SaveOrDeleteTaskUser]  
  
  @Mode tinyint, -- 0:Insert, 1: Update by Admin, 2: Update by User 3: Delete  
  @Id bigint=0,  
  @TaskId bigint=0,  
  @UserId int=0,  
  @UserType bit=false, -- To link tblUser and tblInstallUser tables (Not Usertype defined in both tables)  
  @Status tinyint=0,  
  @Notes varchar(MAX)='',  
  @UserAcceptance bit=false, 
  @TaskUpdateId bigint OUTPUT     
  
AS  
BEGIN  
   
IF @Mode=0  
  
BEGIN  
  
INSERT INTO tblTaskUser (TaskId,UserId,UserType,Status,Notes,UserAcceptance,IsDeleted)   
VALUES(@TaskId,@UserId,@UserType,@Status,@Notes,@UserAcceptance,0)  
  
SELECT @TaskUpdateId = SCOPE_IDENTITY()

END  
  
ELSE IF @Mode=1  
  
BEGIN  
  
UPDATE tblTaskUser  
  
SET  
  
TaskId=@TaskId,  
UserId=@UserId,  
UserType=@UserType  
  
WHERE Id=@Id  
  
END  
  
ELSE IF @Mode=2  
  
BEGIN  
  
UPDATE tblTaskUser  
  
SET  
  
Status=@Status,  
Notes=@Notes,  
UserAcceptance=@UserAcceptance  
  
WHERE TaskId=@TaskId AND UserId=@UserId  
  
END  
  
ELSE IF @Mode=3  
  
BEGIN  
  
UPDATE tblTaskUser  
  
SET IsDeleted=1  
  
WHERE Id=@Id   
  
END  
  
END  

GO
-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetInstallUserDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetInstallUserDetails]
GO
CREATE PROCEDURE SP_GetInstallUserDetails      
@Id int   
AS      
BEGIN    
  
 SELECT * , 0 AS AttachmentCount, GETDATE() AS UpdatedOn from tblinstallUsers where  Id =  @Id  

END
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

/****** Object:  StoredProcedure [dbo].[SP_GetInstallUsers]    Script Date: 7/6/2016 6:59:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
  -- =============================================    
-- Author: ALI SHAHABAS  
-- Create date: 26-JUNE-2016  
-- Updated By: Yogesh Keraliya
-- Updated date: 06-July-2016  
-- Description: SP_GetInstallUsers    
-- =============================================    
ALTER PROCEDURE [dbo].[SP_GetInstallUsers]    
@Key int,  
@Designation varchar(50)  
AS    
BEGIN    

IF @Key=1  
 SELECT  DISTINCT(Designation)AS Designation FROM tblinstallUsers WHERE Designation IS NOT NULL     
ELSE IF @Key = 2  
 SELECT DISTINCT (FristName), Id FROM tblinstallUsers WHERE  FristName IS NOT NULL AND Designation = @Designation  

END



