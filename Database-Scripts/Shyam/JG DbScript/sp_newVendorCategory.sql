USE [jgrove_JGP]
GO

/****** Object:  StoredProcedure [dbo].[sp_newVendorCategory]    Script Date: 02/25/2016 10:47:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_newVendorCategory]
@vendorCatId nvarchar(5)=null,
@vendorCatName nvarchar(max)=null,
@productCatName nvarchar(max)=null,
@productCatId nvarchar(5)=null,
@action int=null
as

Begin
if(@action='1')
	Begin
		insert into tblVendorCategory(VendorCategoryNm) output inserted.VendorCategpryId values(@vendorCatName)
	End
if(@action='2')
	Begin
		insert into tblProductVendor (ProductCategoryId,ProductCategoryName,VendorCategoryId,VendorcategoryName) values (@productCatId,@productCatName,@vendorCatId,@vendorCatName)
	End
End
GO

