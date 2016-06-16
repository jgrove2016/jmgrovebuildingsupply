
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetVendorMaterialList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetVendorMaterialList]
GO
create proc [dbo].[sp_GetVendorMaterialList]
@ManufacturerType nvarchar(250)=null,
@VendorId nvarchar(250)=null,
@ProductCatId nvarchar(250)=null,
@VendorCatId nvarchar(250)=null,
@VendorSubCatId nvarchar(250)=null,
@PeriodStart nvarchar(250)=null,
@PeriodEnd nvarchar(250)=null,
@PayPeriod nvarchar(250)=null
AS
BEGIN

 Declare @BaseQuery nvarchar(max)=null  

set @BaseQuery='SELECT 
	cm.Id
	,cm.CreatedOn as Date
	,cm.Amount as TotalAmount
	,cm.MaterialList as Description
	,'''' as PaymentMethod 
	,'''' as Transations
	,cm.EmailStatus as Status
	,'''' as InvoiceAttach
	,cm.VendorId  
	,cd.DocName
	,cd.TempName
	,ISNULL(cm.IsForemanPermission,'''') as IsForemanPermission
	,ISNULL(cm.IsSrSalemanPermissionF,'''') as IsSrSalemanPermissionF
	,ISNULL(cm.IsAdminPermission,'''') as IsAdminPermission
	,ISNULL(cm.IsSrSalemanPermissionA,'''') as IsSrSalemanPermissionA
	,ISNULL(cm.EmailStatus,'''') as EmailStatus
	FROM tblCustom_MaterialList cm ';

if(LEN(@VendorId) > 0)  
  begin  
   set @BaseQuery=@BaseQuery+ ' LEFT OUTER JOIN tblVendors v on v.VendorId =cm.VendorId 
	LEFT JOIN tblVendorQuotes cd on cd.SoldJobId=cm.SoldJobId and cd.VendorId=cm.VendorId 
	where v.VendorId=@VendorId'  
  end  
  
  else if(LEN(@VendorSubCatId) > 0)  
  begin  
   set @BaseQuery=@BaseQuery+ '  LEFT OUTER JOIN tblVendors v on v.VendorId =cm.VendorId 
   inner join tbl_Vendor_VendorSubCat tvvs
   on tvvs.VendorId=cm.VendorId   
   inner join tblVendorSubCategory tvsc on tvsc.VendorSubCategoryId=tvvs.VendorSubCatId
   LEFT JOIN tblVendorQuotes cd on cd.SoldJobId=cm.SoldJobId and cd.VendorId=cm.VendorId
    where tvvs.VendorSubCatId=@VendorSubCatId '  

if(@ManufacturerType='Retail/Wholesale')
		Begin
		set @BaseQuery=@BaseQuery+ ' and tvsc.IsRetail_Wholesale=''true'''
		End
	else if(@ManufacturerType='Manufacturer')
		Begin
		set @BaseQuery=@BaseQuery+ ' and tvsc.IsManufacturer=''true'''
		End
    
  end  
  
  else if(LEN(@VendorCatId)>0)  
  begin  
   set @BaseQuery=@BaseQuery+ ' LEFT OUTER JOIN tblVendors v on v.VendorId =cm.VendorId 
    inner join tbl_Vendor_VendorCat tvvc on tvvc.VendorId=cm.VendorId
    inner join tblVendorCategory tvc on tvc.VendorCategpryId =tvvc.VendorCatId
   LEFT JOIN tblVendorQuotes cd on cd.SoldJobId=cm.SoldJobId and cd.VendorId=cm.VendorId
    where tvvc.VendorCatId=@VendorCatId '  
      
      if(@ManufacturerType='Retail/Wholesale')
		Begin
		set @BaseQuery=@BaseQuery+ ' and tvc.IsRetail_Wholesale=''true'''
		End
	else if(@ManufacturerType='Manufacturer')
		Begin
		set @BaseQuery=@BaseQuery+ ' and tvc.IsManufacturer=''true'''
		End
      
  end  
  else if(LEN(@ProductCatId)>0)  
  begin  
  set @BaseQuery=@BaseQuery+ '  LEFT OUTER JOIN tblVendors v on v.VendorId =cm.VendorId
   inner join tbl_Vendor_VendorCat tvvc on tvvc.VendorId=cm.VendorId
  LEFT JOIN tblVendorQuotes cd on cd.SoldJobId=cm.SoldJobId and cd.VendorId=cm.VendorId
    where tvvc.VendorCatId in  (select VendorCategoryId from tblProductVendorCat where ProductCategoryId=@ProductCatId)'  
  end  
  else
  begin
  set @BaseQuery=@BaseQuery+' LEFT JOIN tblVendorQuotes cd on cd.SoldJobId=cm.SoldJobId and cd.VendorId=cm.VendorId where 1=1';
  end
 
 set @BaseQuery=@BaseQuery+' and cm.CreatedOn >= convert(datetime, '''+@PeriodStart+''', 101) and cm.CreatedOn <= convert(datetime, '''+@PeriodEnd+''', 101) '
  set @BaseQuery=@BaseQuery+' ORDER BY cm.Id'
  
 print @BaseQuery  
 
  EXECUTE sp_executesql @BaseQuery,N'@ManufacturerType nvarchar(250),@VendorId nvarchar(250),@ProductCatId nvarchar(250),@VendorCatId nvarchar(250),@VendorSubCatId nvarchar(250)
  ,@PeriodStart nvarchar(250),@PeriodEnd nvarchar(250),@PayPeriod nvarchar(250)',  
       @ManufacturerType,@VendorId,@ProductCatId,@VendorCatId,@VendorSubCatId,@PeriodStart,@PeriodEnd,@PayPeriod

END