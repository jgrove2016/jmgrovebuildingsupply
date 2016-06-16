/****** Object:  StoredProcedure [dbo].[UDP_IsValidInstallerUser]    Script Date: 03/09/2016 01:29:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UDP_IsValidInstallerUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UDP_IsValidInstallerUser]
GO

/****** Object:  StoredProcedure [dbo].[UDP_IsValidInstallerUser]    Script Date: 03/09/2016 01:29:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE ProcEDURE [dbo].[UDP_IsValidInstallerUser]
@userid varchar(50),
@password varchar(50),
@result int output
AS
BEGIN
	if exists(select Id from tblInstallUsers where Email=@userid and Password=@password and (Status='Active' OR Status='OfferMade') )
	begin
		Set @result ='1'
	end
	else
	begin
		set @result='0'
	end


	return @result

END

GO


