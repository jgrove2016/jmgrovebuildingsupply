
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GETInvetoryCatogriesList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GETInvetoryCatogriesList]
GO
create PROC [dbo].[GETInvetoryCatogriesList]
@ManufacturerType nvarchar(30)=null
as
declare @strQuery nvarchar(max)=null, @strProductCategory nvarchar(max)=null,
 @strVendorCategory nvarchar(max)=null, @strVendorSubCategory nvarchar(max)=null;
Begin
 set @strProductCategory = 'SELECT TPM.ProductId,TPM.ProductName FROM tblProductMaster TPM'
								+' LEFT OUTER JOIN tblProductVendorCat TPVC'
								+' ON TPM.ProductId=TPVC.ProductCategoryId'
								+' where LEN(TPM.ProductName)>0'
								+' GROUP BY TPM.ProductId,TPM.ProductName'
								+' ORDER BY TPM.ProductName'

set @strVendorCategory = 'SELECT TPVC.ProductCategoryId, TVC.VendorCategpryId,TVC.VendorCategoryNm,TVC.IsRetail_Wholesale,TVC.IsManufacturer  from tblVendorCategory TVC'
								+' LEFT OUTER JOIN tbl_VendorCat_VendorSubCat TVCVS'
								+' ON TVC.VendorCategpryId=TVCVS.VendorCategoryId'
								+' INNER JOIN tblProductVendorCat  TPVC'
								+' ON TVC.VendorCategpryId=TPVC.VendorCategoryId'
							
set @strVendorSubCategory='SELECT TVCVS.VendorCategoryId,TVSC.VendorSubCategoryId,TVSC.VendorSubCategoryName,TVSC.IsRetail_Wholesale,TVSC.IsManufacturer FROM tbl_VendorCat_VendorSubCat TVCVS'
								+' LEFT OUTER JOIN tblVendorSubCategory TVSC'
								+' ON TVSC.VendorSubCategoryId=TVCVS.VendorSubCategoryId'
								
	if(@ManufacturerType='Retail/Wholesale')
		Begin
			set @strVendorCategory= @strVendorCategory + ' where TVC.IsRetail_Wholesale=''true'' Group By TVC.VendorCategpryId,TVC.VendorCategoryNm,TPVC.ProductCategoryId,TVC.IsRetail_Wholesale,TVC.IsManufacturer ORDER BY TVC.VendorCategoryNm'
			set @strVendorSubCategory = @strVendorSubCategory + ' where TVSC.IsRetail_Wholesale=''true'' GROUP BY TVCVS.VendorCategoryId,TVSC.VendorSubCategoryId,TVSC.VendorSubCategoryName,TVSC.IsRetail_Wholesale,TVSC.IsManufacturer'
		End
	else if(@ManufacturerType='Manufacturer')
		Begin
			set @strVendorCategory= @strVendorCategory + ' where TVC.IsManufacturer=''true'' Group By TVC.VendorCategpryId,TVC.VendorCategoryNm,TPVC.ProductCategoryId,TVC.IsRetail_Wholesale,TVC.IsManufacturer ORDER BY TVC.VendorCategoryNm'
			set @strVendorSubCategory = @strVendorSubCategory + ' where TVSC.IsManufacturer=''true'' GROUP BY TVCVS.VendorCategoryId,TVSC.VendorSubCategoryId,TVSC.VendorSubCategoryName,TVSC.IsRetail_Wholesale,TVSC.IsManufacturer'
		End
	set @strQuery = @strProductCategory +' '+ @strVendorCategory +' '+@strVendorSubCategory
 print @strQuery  
 EXECUTE sp_executesql @strQuery,N'@ManufacturerType nvarchar(30)',@ManufacturerType
End



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorCategory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorCategory]
GO


CREATE proc [dbo].[sp_VendorCategory]
@VendorCategoryName nvarchar(500)=null,
@VendorCategoryId nvarchar(5)=null,
@VendorProductId nvarchar(5)=null,
@IsRetail_Wholesale bit=false,
@IsManufacturer bit=false,
@action int=null
as
begin 
if(@action=1)
 begin
	update tblVendorCategory set VendorCategoryNm=@VendorCategoryName,IsRetail_Wholesale=@IsRetail_Wholesale,
								IsManufacturer=@IsManufacturer where VendorCategpryId=@VendorCategoryId
 end	

end

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
		select * from tblVendorSubCategory order by VendorSubCategoryName
	end
	
if(@action=4)
 begin
	update tblVendorSubCategory set VendorSubCategoryName=@VendorSubCategoryName,IsRetail_Wholesale=@IsRetail_Wholesale,
								IsManufacturer=@IsManufacturer where VendorSubCategoryId=@VendorSubCategoryId
 end	

end