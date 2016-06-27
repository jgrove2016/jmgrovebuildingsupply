USE [jgrove_JGP]
GO
/****** Object:  StoredProcedure [dbo].[GetAllAnnualEvent]    Script Date: 6/12/2016 10:17:10 AM ******/
DROP PROCEDURE [dbo].[GetAllAnnualEvent]
GO
/****** Object:  StoredProcedure [dbo].[GetAllAnnualEvent]    Script Date: 6/12/2016 10:17:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllAnnualEvent] 
	-- Add the parameters for the stored procedure here
	@Year varchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	--select a.ID,a.EventName,a.EventDate,a.EventAddedBy,a.ApplicantId,u.Username,i.FristName,i.LastName,i.Phone
	--from tbl_AnnualEvents a INNER JOIN  tblUsers u ON u.Id = a.EventAddedBy LEFT JOIN tblInstallUsers i ON i.Id = a.ApplicantId
	--where DATEPART(yyyy,EventDate)=@Year

	select a.ID,a.EventName,a.EventDate,a.EventAddedBy,a.ApplicantId,u.Username,i.FristName,i.LastName,i.Phone,i.Id,
	i.Status,
	i.Designation
	from tbl_AnnualEvents a INNER JOIN  tblUsers u ON u.Id = a.EventAddedBy LEFT JOIN tblInstallUsers i ON i.Id = a.ApplicantId 
	WHERE a.EventName IS NOT NULL AND a.EventName !='InterViewDetails' AND DATEPART(yyyy,EventDate)=@Year
	Union 

	select a.ID,( a.EventName + '  '+u.Username + ' '+i.Designation+  '  ' + i.Phone +'   '+i.FristName) as EventName,
	EventDate = CONVERT(datetime,EventDate) + CONVERT(datetime,InterviewTime),a.EventAddedBy,a.ApplicantId,u.Username,i.FristName,i.LastName,i.Phone,i.Id,
	i.Status,
	i.Designation
	from tbl_AnnualEvents a INNER JOIN  tblUsers u ON u.Id = a.EventAddedBy 
	LEFT JOIN tblInstallUsers i ON i.Id = a.ApplicantId
	WHERE a.EventName='InterViewDetails' AND DATEPART(yyyy,EventDate)=@Year





END

GO
