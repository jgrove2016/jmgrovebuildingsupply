
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorAddress]
go
create proc [dbo].[sp_VendorAddress]  
@action int=null,  
@VendorId int = null,
@AddressID int=null,
@TempID nvarchar(250)=null,
@manufacturer nvarchar(250)=null,
@productId nvarchar(250)=null,
@vendorCatId nvarchar(250)=null,
@vendorSubCatId nvarchar(250)=null,
@tblVendorAddress VendorAddress READONLY  
  
AS  
 if(@action=1)  
 Begin  
 
 select @AddressID=AddressID from @tblVendorAddress
 
 declare @temptable table
 (
	ID int
 )
 
 if(@AddressID is null or @AddressID=0)
 begin
	--insert code
	insert into tblVendorAddress(VendorId,AddressType,Address,City,Zip,Country,TempID,Latitude,Longitude) 
	output inserted.Id into @temptable
	select VendorId,AddressType,Address,City,Zip,Country,TempID,Latitude,Longitude from @tblVendorAddress 
	
	select ID from @temptable
	 
 end
 else
 begin
	--update code
	update tblVendorAddress
	set AddressType=t.AddressType,
	City=t.City,
	Zip=t.Zip,
	Address=t.Address,
	Country=t.Country,
	Latitude=t.Latitude,
	Longitude=t.Longitude
	from @tblVendorAddress t
	where tblVendorAddress.Id=t.AddressId
	
	select t.AddressID from @tblVendorAddress t
 end  
 End  
if(@action=2)  
 Begin  
	 select * from tblVendorAddress 
	 where VendorId=@VendorId  
 End  
 if(@action=3)  
 Begin  
	 --select * from tblVendorAddress 
	 
	 Declare @BaseQuery nvarchar(max)=null  

if(LEN(@vendorSubCatId) > 0)  
  begin  
   set @BaseQuery= 'select distinct tv.VendorName as VendorName,tv.VendorId as VendorId,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude '  
          +'from tbl_Vendor_VendorSubCat V_Vsc '  
          +'inner join tblVendors tv '  
          +'on V_Vsc.VendorId = tv.VendorId '  
          +'inner join tbl_VendorCat_VendorSubCat Vc_Vsc '  
          +'on Vc_Vsc.VendorSubCategoryId=V_Vsc.VendorSubCatId '  
          +'inner join tblVendorAddress TVAdd on tv.VendorId=TVAdd.VendorId'
          +' where V_Vsc.VendorSubCatId=@vendorSubCatId and tv.ManufacturerType=@manufacturer'  
  end  
  
  else if(LEN(@vendorCatId)>0)  
  begin  
   set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid inner join tblVendorAddress TVAdd on tv.VendorId=TVAdd.VendorId'
      +' where tvVcat.VendorCatId=@vendorCatId and tv.ManufacturerType=@manufacturer'  
  end  
  else if(LEN(@productId)>0)  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid inner join tblVendorAddress TVAdd on tv.VendorId=TVAdd.VendorId'
      +' where tvVcat.VendorCatId in  (select VendorCategoryId from tblProductVendorCat where ProductCategoryId=@productId)'
      +' and tv.ManufacturerType=@manufacturer'  
  end  
  else 
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid inner join tblVendorAddress TVAdd on tv.VendorId=TVAdd.VendorId where tv.ManufacturerType=@manufacturer'
  end   
 print @BaseQuery  
 
  EXECUTE sp_executesql @BaseQuery,N'@manufacturer nvarchar(250),@productId nvarchar(250),@vendorCatId nvarchar(250),@vendorSubCatId nvarchar(250)',  
       @manufacturer,@productId,@vendorCatId,@vendorSubCatId  
 End  
 if(@action=4)  
 Begin  
	 select * from tblVendorAddress 
	 where VendorId=@VendorId  and TempID=@TempID
 End  
