USE [jgrove_JGP]
GO

/****** Object:  StoredProcedure [dbo].[sp_VendorSubCat]    Script Date: 02/25/2016 10:48:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_VendorSubCat]
@VendorSubCategoryName nvarchar(500)=null,
@VendorCategoryId nvarchar(5)=null,
@VendorSubCategoryId nvarchar(5)=null,
@action int=null
as
begin 
if(@action=1)
 begin
	insert into tblVendorSubCategory (VendorSubCategoryName,VendorCategoryId) values(@VendorSubCategoryName,@VendorCategoryId)
 end	
if(@action=2)
 begin
	delete from tblVendorSubCategory where VendorSubCategoryId=@VendorSubCategoryId
 end
if(@action=3)
	begin
		select * from tblVendorSubCategory
	end
end
GO

