alter proc [dbo].[USP_GetVendorList]  
@FilterParams nvarchar(Max)=null,  
@FilterBy nvarchar(50)=null,  
@ManufacturerType nvarchar(20)=null,  
@VendorCategoryId nvarchar(10)=null  
as  
BEGIN  
 Declare @BaseQuery nvarchar(max)=null  
  if(@FilterBy='VendorSubCategory')  
  begin  
   set @BaseQuery= 'select v.VendorName as VendorName,v.VendorId as VendorId'  
          +'from tbl_Vendor_VendorSubCat V_Vsc '  
          +'inner join tblVendors v '  
          +'on V_Vsc.VendorId = v.VendorId '  
          +'inner join tbl_VendorCat_VendorSubCat Vc_Vsc '  
          +'on Vc_Vsc.VendorSubCategoryId=V_Vsc.VendorSubCatId'  
          +' where V_Vsc.VendorSubCatId=@FilterParams'  
  end  
  else if(@FilterBy='VendorCategory')  
  begin  
   set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid where tvVcat.VendorCatId=@FilterParams'  
   --set @WhereClause= @WhereClause +' AND Vc_Vsc.VendorCategoryId=@FilterParams'  
  end  
  else if(@FilterBy='ProductCategory')  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid where tvVcat.VendorCatId in (select * from dbo.split(@FilterParams,'',''))'  
   --set @WhereClause= @WhereClause +' And Vc_Vsc.VendorCategoryId in (select * from dbo.split(@FilterParams,'',''))';  
  end  
  else if(@FilterBy='ProductCategoryAll')  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid'
  end    
 --else if(@FilterBy='ManufacturerType')  
 -- begin  
 --  set @WhereClause= @WhereClause +' And (ManufacturerType=''Retail'' OR ManufacturerType=''Wholesale'')';  
 -- end  
    
 print @BaseQuery  
 EXECUTE sp_executesql @BaseQuery,N'@FilterParams nvarchar(max),@FilterBy nvarchar(50),@ManufacturerType nvarchar(20),@VendorCategoryId nvarchar(10)',  
       @FilterParams,@FilterBy,@ManufacturerType,@VendorCategoryId  
END  
  
  
  
  
  
--exec [USP_GetVendorList] @FilterBy='ProductCategory',@FilterParams='80,84,118'  
  
--select * from tblvendors where ManufacturerType='Retail' OR ManufacturerType='Wholesale'  