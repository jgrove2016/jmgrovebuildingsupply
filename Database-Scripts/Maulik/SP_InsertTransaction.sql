/****** Object:  StoredProcedure [dbo].[InsertTransaction]    Script Date: 12-03-2016 PM 07:23:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[InsertTransaction] 
	-- Add the parameters for the stored procedure here
	@ccNumber varchar(max),
	@ccSecurityCode varchar(max),
	@ccFirstName varchar(100),
	@ccLastName varchar(100),
	@ExpirationDate int,
	@ccPriceValue money,
	@ccStatus bit,
	@ccMessage varchar(50),
	@ccResponse varchar(max),
	@ccRequest varchar(max),
	@CustomerId INT,
	@ProductId INT,
	@AuthorizationCode VARCHAR(max),
	@PaylineTransectionId VARCHAR(max)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	insert into PaylineTransaction values(@ccNumber,@ccSecurityCode,@ccFirstName,@ccLastName,
	@ExpirationDate,@ccPriceValue,@ccStatus,@ccMessage,@ccResponse,@ccRequest,@CustomerId,
	@ProductId,GETDATE(),@AuthorizationCode,@PaylineTransectionId)
END



GO


