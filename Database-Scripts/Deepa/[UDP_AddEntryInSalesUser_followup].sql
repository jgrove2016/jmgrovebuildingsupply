USE [jgrove_JGP]
GO

/****** Object:  StoredProcedure [dbo].[UDP_AddEntryInSalesUser_followup]    Script Date: 6/15/2016 12:53:17 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE Proc [dbo].[UDP_AddEntryInSalesUser_followup]
@custId int,
@userid  int,
@meetingDate datetime,
@meetingstatus varchar(150)

As Begin
DECLARE @EstId as int = 0
if not Exists(select * from [dbo].[tblsalesuser_followup] where MeetingDate = @MeetingDate and meetingstatus = @meetingstatus and CustomerId=@custId  )
begin



INSERT INTO [dbo].[tblsalesuser_followup]
           (CustomerId
           ,[MeetingDate]
           ,meetingstatus
            ,userId      
           
		   )
          
     VALUES
           (@custId
           ,@MeetingDate
           ,@meetingstatus
		   ,@userId
           )
 END
 
 
end



GO


