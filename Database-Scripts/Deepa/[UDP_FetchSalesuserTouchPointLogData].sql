USE [jgrove_JGP]
GO

/****** Object:  StoredProcedure [dbo].[UDP_FetchSalesUserTouchPointLogData]    Script Date: 6/15/2016 12:52:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/**********************************************************************
*     Name : [UDP_FetchSalesUserTouchPointLogData]
*
*	 CREATED By : Deepa
*	
*	 Purpose : Stores touch point data from Sales users.
*
*
*
***********************************************************************/





CREATE PROCEDURE [dbo].[UDP_FetchSalesUserTouchPointLogData]  (@Customerid int ,@userId int )
        
AS            
BEGIN            
             
 SET NOCOUNT ON;            
             
SELECT 'R' + RIGHT('000'+ CONVERT(VARCHAR,row_number() OVER (ORDER BY sf.CustomerId desc)),3) as [ReferenceNumber],         
              
   sf.Meetingstatus as [Status] , sf.CreatedOn as [Date]   ,u.Email            
           
FROM [dbo].[tblsalesuser_followup] sf            
          
INNER JOIN tblUsers u on sf.UserId=u.Id           
            
  WHERE (sf.CustomerId= @Customerid and sf.MeetingStatus is not null AND sf.MeetingStatus <>'')      
 or       
    
  sf.userid =@userid and sf.CustomerId is null AND (sf.MeetingStatus is not null AND sf.MeetingStatus <>'')  
  AND sf.createdon >= dateadd(minute, -30, getdate())  
 
 order by sf.CustomerId desc         
        
         
END


GO


