
GO

/****** Object:  StoredProcedure [dbo].[sp_GetHrData]    Script Date: 06/23/2016 12:31:23 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetHrData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetHrData]
GO

GO

/****** Object:  StoredProcedure [dbo].[sp_GetHrData]    Script Date: 06/23/2016 12:31:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_GetHrData]
@UserId int,
@FromDate date,
@ToDate date

as
BEGIN
	
	SET NOCOUNT ON;
IF(@UserId<>0)
	BEGIN
		select t.status,count(*)cnt FROM tblInstallUsers t 
		LEFT OUTER JOIN tblUsers U ON U.Id = t.SourceUser
		LEFT OUTER JOIN tblUsers ru on t.RejectedUserId=ru.Id	  
		WHERE (t.UserType = 'SalesUser' OR t.UserType = 'sales') and
		 U.Id=@UserId 
					and  CAST(t.CreatedDateTime as date) >= CAST( @FromDate  as date) 
					and CAST (t.CreatedDateTime  as date) <= CAST( @ToDate  as date)
		group by t.status
	END
ELSE 
	BEGIN
		select t.status,count(*)cnt FROM tblInstallUsers t 
		LEFT OUTER JOIN tblUsers U ON U.Id = t.SourceUser
		LEFT OUTER JOIN tblUsers ru on t.RejectedUserId=ru.Id	  
		WHERE (t.UserType = 'SalesUser' OR t.UserType = 'sales')
					and  CAST(t.CreatedDateTime as date) >= CAST( @FromDate  as date) 
					and CAST (t.CreatedDateTime  as date) <= CAST( @ToDate  as date)
		group by t.status
	END
	
	
IF(@UserId<>0)
  Begin
	SELECT t.Id,t.FristName,t.LastName,t.Phone,t.Zip,t.Designation,t.Status,t.HireDate,t.InstallId,t.picture, t.CreatedDateTime, Isnull(Source,'') AS Source,
	SourceUser, ISNULL(U.Username,'')  AS AddedBy,
	InterviewDetail = case when (t.Status='InterviewDate' or t.Status='Interview Date') then coalesce(RejectionDate,'') + ' ' + coalesce(InterviewTime,'') else '' end,
	RejectDetail = case when (t.Status='Rejected' ) then coalesce(RejectionDate,'') + ' ' + coalesce(RejectionTime,'') + ' ' + '-' + coalesce(ru.LastName,'') else '' end

	FROM tblInstallUsers t 
	LEFT OUTER JOIN tblUsers U ON U.Id = t.SourceUser
	LEFT OUTER JOIN tblUsers ru on t.RejectedUserId=ru.Id	  
	WHERE (t.UserType = 'SalesUser' OR t.UserType = 'sales') AND t.Status <> 'Deactive' 
		AND U.Id=@UserId 
				and  CAST(t.CreatedDateTime as date) >= CAST( @FromDate  as date) 
				and CAST (t.CreatedDateTime  as date) <= CAST( @ToDate  as date)
	ORDER BY Id DESC
 End
Else
	Begin
	SELECT t.Id,t.FristName,t.LastName,t.Phone,t.Zip,t.Designation,t.Status,t.HireDate,t.InstallId,t.picture, t.CreatedDateTime, Isnull(Source,'') AS Source,
	SourceUser, ISNULL(U.Username,'')  AS AddedBy,
	InterviewDetail = case when (t.Status='InterviewDate' or t.Status='Interview Date') then coalesce(RejectionDate,'') + ' ' + coalesce(InterviewTime,'') else '' end,
	RejectDetail = case when (t.Status='Rejected' ) then coalesce(RejectionDate,'') + ' ' + coalesce(RejectionTime,'') + ' ' + '-' + coalesce(ru.LastName,'') else '' end

	FROM tblInstallUsers t 
	LEFT OUTER JOIN tblUsers U ON U.Id = t.SourceUser
	LEFT OUTER JOIN tblUsers ru on t.RejectedUserId=ru.Id	  
	WHERE (t.UserType = 'SalesUser' OR t.UserType = 'sales') AND t.Status <> 'Deactive' 
				and  CAST(t.CreatedDateTime as date) >= CAST( @FromDate  as date) 
				and CAST (t.CreatedDateTime  as date) <= CAST( @ToDate  as date)
	ORDER BY Id DESC
	End
 
END

GO


