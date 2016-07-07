
IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Latitude' AND Object_ID = Object_ID(N'tblVendorAddress'))
BEGIN
   Alter table tblVendorAddress drop column Latitude
END
go
Alter Table tblVendorAddress Add Latitude nvarchar(50) null
go

IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Longitude' AND Object_ID = Object_ID(N'tblVendorAddress'))
BEGIN
   Alter table tblVendorAddress drop column Longitude
END
alter table tblVendorAddress add Longitude nvarchar(50) null
go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorAddress]
GO

IF EXISTS (SELECT *
           FROM   [sys].[table_types]
           WHERE  user_type_id = Type_id(N'[dbo].[VendorAddress]'))
  BEGIN
     drop TYPE VendorAddress
  END

go


CREATE TYPE [dbo].[VendorAddress] AS TABLE(
	[VendorId] [int] NULL,
	[AddressType] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](50) NULL,
	[Zip] [nvarchar](10) NULL,
	[Country][nvarchar](500) NULL,
	[AddressID][int] NULL,
	[TempID][nvarchar](500) NULL,
	[Latitude][nvarchar](50) NULL,
	[Longitude][nvarchar](50) NULL
)


go

Create proc [dbo].[sp_VendorAddress]  
@action int=null,  
@VendorId int = null,
@AddressID int=null,
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
go
