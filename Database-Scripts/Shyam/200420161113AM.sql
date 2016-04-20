
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorSubCat]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorSubCat]
GO
create proc [dbo].[sp_VendorSubCat]
@VendorSubCategoryName nvarchar(500)=null,
@VendorCategoryId nvarchar(5)=null,
@VendorSubCategoryId nvarchar(5)=null,
@IsRetail_Wholesale bit=false,
@IsManufacturer bit=false,
@action int=null
as
begin 
if(@action=1)
 begin
	declare @temptable table
 (
	ID int
 )
	insert into tblVendorSubCategory (VendorSubCategoryName,IsRetail_Wholesale,IsManufacturer)
	output inserted.VendorSubCategoryId into @temptable
	 values(@VendorSubCategoryName,@IsRetail_Wholesale,@IsManufacturer)
	insert into tbl_VendorCat_VendorSubCat(VendorCategoryId,VendorSubCategoryId) values(@VendorCategoryId,(select ID from @temptable))
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
