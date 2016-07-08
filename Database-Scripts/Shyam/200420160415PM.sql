
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProductCategoryByVendorCatID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetProductCategoryByVendorCatID]
GO
create proc [dbo].[GetProductCategoryByVendorCatID]
@VendorCategoryId int=null
as
begin
select * from tblProductVendorCat where VendorCategoryId=@VendorCategoryId
end