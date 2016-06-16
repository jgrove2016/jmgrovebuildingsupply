/****** Object:  StoredProcedure [dbo].[UDP_GetInstallerUserDetailsByLoginId]    Script Date: 03/09/2016 01:30:55 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UDP_GetInstallerUserDetailsByLoginId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UDP_GetInstallerUserDetailsByLoginId]
GO

/****** Object:  StoredProcedure [dbo].[UDP_GetInstallerUserDetailsByLoginId]    Script Date: 03/09/2016 01:30:55 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE ProcEDURE [dbo].[UDP_GetInstallerUserDetailsByLoginId]
@loginId varchar(50) 
AS
BEGIN
	
	SELECT Id,FristName,Lastname,Email,Address,Designation,[Status],
		[Password],[Address],Phone,Picture,Attachements,usertype 
	from tblInstallUsers 
	where (Email = @loginId and Status='Active')  OR 
	(Email = @loginId AND (Designation = 'SubContractor' OR Designation='Installer') AND 
	(Status='OfferMade' OR Status='Offer Made' OR Status='Active'))
END

GO


