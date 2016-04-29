
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorAddress]
go
create proc [dbo].[sp_VendorAddress]  
@action int=null,  
@VendorId int = null,
@AddressID int=null,
@TempID nvarchar(250)=null,
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
	 select * from tblVendorAddress 
 End  
 if(@action=4)  
 Begin  
	 select * from tblVendorAddress 
	 where VendorId=@VendorId  and TempID=@TempID
 End  
 
 
 go
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_deletevendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_deletevendor]
go
 
 
 CREATE procedure [dbo].[sp_deletevendor]
@VendorId int
as
if exists(select 1 from tblVendors where VendorId=@VendorId)
begin
delete tblVendorQuotes where VendorId=@VendorId
delete tblVendorEmail where VendorId=@VendorId
delete tblVendorAddress where VendorId=@VendorId
delete tbl_Vendor_VendorSubCat where VendorId=@VendorId
delete tbl_Vendor_VendorCat where VendorId=@VendorId
delete tblVendors where VendorId=@VendorId
end

