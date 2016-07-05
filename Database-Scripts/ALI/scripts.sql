  -- =============================================  
-- Author:  ALI SHAHABAS  
-- Create date: 25-JUNE-2016  
-- Description: SP_GetTaskUserDetailsByTaskId  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_GetTaskUserDetailsByTaskId]
	@TaskId int
AS
BEGIN

	SET NOCOUNT ON;

	SELECT U.Username,TU.Status,TU.Notes,UpdatedOn,
				(SELECT COUNT(*) FROM tblTaskUserFiles TUF WHERE TUF.TaskId=TU.TaskId) AS NoOfFiles 
	FROM [tblTaskUser]   TU LEFT OUTER JOIN 
				tblUsers U ON U.Id=TU.UserId 
	WHERE TU.TaskId=@TaskId AND TU.IsDeleted=0 
					

END


  
  GO
----------------------------------------------------------------------------------------------------------------------------------------------------------------------
  -- =============================================    
-- Author: ALI SHAHABAS  
-- Create date: 26-JUNE-2016  
-- Updated date: 29-JUNE-2016  
-- Description: SP_GetInstallUsers    
-- =============================================    
CREATE PROCEDURE SP_GetInstallUsers    
@Key int,  
@Designation varchar(100)  
AS    
BEGIN    
IF @Key=1  
 SELECT  DISTINCT(Designation)AS Designation FROM tblinstallUsers WHERE Designation is not null  AND (UserType = 'SalesUser' OR UserType = 'sales' OR UserType = 'installer')   
ELSE IF @Key = 2  
 select DISTINCT(FristName),Id from tblinstallUsers where  (UserType = 'SalesUser' OR UserType = 'sales' OR UserType = 'installer') AND FristName is not null AND Designation=@Designation  
END



go

----------------------------------------------------------------------------------------------------------------------------------------------------------------------

-- Author:  MUNEER MK  
-- Create date: 23-JUNE-2016 
-- Alter: ALI
-- Updated date : 29-JUNE-2016 
-- Description: SP_SaveOrDeleteTaskUser  
-- =============================================  
CREATE PROCEDURE [dbo].[SP_SaveOrDeleteTaskUser]  
  
  @Mode tinyint, -- 0:Insert, 1: Update by Admin, 2: Update by User 3: Delete  
  @Id bigint=0,  
  @TaskId bigint=0,  
  @UserId int=0,  
  @UserType bit=false, -- To link tblUser and tblInstallUser tables (Not Usertype defined in both tables)  
  @Status tinyint=0,  
  @Notes varchar(MAX)='',  
  @UserAcceptance bit=false  
   
    
  
AS  
BEGIN  
   
IF @Mode=0  
  
BEGIN  
  
INSERT INTO tblTaskUser (TaskId,UserId,UserType,Status,Notes,UserAcceptance,IsDeleted)   
VALUES(@TaskId,@UserId,@UserType,@Status,@Notes,@UserAcceptance,0)  
  
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
----------------------------------------------------------------------------------------------------------------------------------------------------------------------

---------------------------------------------------------------------------------------------------------------------------------------------------------------------

CREATE PROCEDURE [dbo].[SP_SaveOrDeleteTask]  
   
 @Mode tinyint, -- 0:Insert, 1: Update, 2: Delete  
 @TaskId bigint,  
 @Title varchar(250),  
 @Description varchar(MAX),  
 @Status tinyint,  
 @DueDate varchar(25),  
 @Hours int,  
 @Notes varchar(MAX),  
 @Attachment varchar(MAX),  
 @CreatedBy int,  
 @Result int output  
   
  
AS  
BEGIN  
  
IF @Mode=0  
  
BEGIN  
  
  
INSERT INTO tblTask (Title,[Description],[Status],DueDate,[Hours],Notes,Attachment,CreatedBy,CreatedOn,IsDeleted)   
VALUES(@Title,@Description,@Status,@DueDate,@Hours,@Notes,@Attachment,@CreatedBy,GETDATE(),0)  
  
SET @Result=SCOPE_IDENTITY ()  
  
RETURN @Result  
  
END  
  
ELSE IF @Mode=1 -- Update  
  
BEGIN  
  
UPDATE tblTask  
  
SET  
  
Title=@Title,  
[Description]=@Description,  
[Status]=@Status,  
DueDate=@DueDate,  
[Hours]=@Hours,  
Notes=@Notes,  
Attachment=@Attachment  
  
WHERE TaskId=@TaskId  
  
END  
  
ELSE IF @Mode=2 --Delete  
  
BEGIN  
  
UPDATE tblTask  
  
SET  
  
IsDeleted=1  
  
WHERE TaskId=@TaskId  
  
  
END  
  
END  

go  

