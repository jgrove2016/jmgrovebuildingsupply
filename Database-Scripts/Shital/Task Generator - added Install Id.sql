
ALTER Table tblTask ADD InstallId varchar(15) NULL

USE [jgrove_JGP]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveOrDeleteTask]    Script Date: 07/08/2016 19:00:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_SaveOrDeleteTask]  
   
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
 @InstallId varchar(15)='',
 @Result int output  
   
  
AS  
BEGIN  
  
IF @Mode=0 -- Insert
	BEGIN  
			INSERT INTO tblTask (Title,[Description],[Status],DueDate,[Hours],Notes,Attachment,CreatedBy, InstallId,CreatedOn,IsDeleted)   
			VALUES(@Title,@Description,@Status,@DueDate,@Hours,@Notes,@Attachment,@CreatedBy,@InstallId,GETDATE(),0)  
			  
			SET @Result=SCOPE_IDENTITY ()  
			
			UPDATE tblTask
				SET InstallId = InstallId + Right('0000' + CONVERT(NVARCHAR, @Result), 4)
			WHERE TaskId=@Result
		
			  
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
