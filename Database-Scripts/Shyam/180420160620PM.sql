IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Country' AND Object_ID = Object_ID(N'tblVendorAddress'))
BEGIN
   Alter table tblVendorAddress drop column Country
END
alter table tblVendorAddress add Country nvarchar(500) null
go
IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'TempID' AND Object_ID = Object_ID(N'tblVendorAddress'))
BEGIN
   Alter table tblVendorAddress drop column TempID
END
alter table tblVendorAddress add TempID nvarchar(500) null
go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorAddress]
GO

Drop TYPE dbo.VendorAddress

go


CREATE TYPE [dbo].[VendorAddress] AS TABLE(
	[VendorId] [int] NULL,
	[AddressType] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](50) NULL,
	[Zip] [nvarchar](10) NULL,
	[Country][nvarchar](500) NULL,
	[AddressID][int] NULL,
	[TempID][nvarchar](500) NULL
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
	insert into tblVendorAddress(VendorId,AddressType,Address,City,Zip,Country,TempID) 
	output inserted.Id into @temptable
	select VendorId,AddressType,Address,City,Zip,Country,TempID from @tblVendorAddress 
	
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
	Country=t.Country
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
go

alter table tblVendors add Vendrosource nvarchar(500) null
go
alter table tblVendors add AddressID int null
go
alter table tblVendors add PaymentTerms nvarchar(500) null
go
alter table tblVendors add PaymentMethod nvarchar(500) null
go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPP_savevendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPP_savevendor]
GO

create Procedure [dbo].[UPP_savevendor]  
@vendor_id int,  
@vendor_name varchar(100),  
@vendor_category int,  
@contact_person varchar(100),  
@contact_number varchar(20),  
@fax varchar(20),  
@email varchar(50),  
@address varchar(100),  
@notes varchar(500),  
@ManufacturerType varchar(70) = '',  
@BillingAddress varchar(MAX) = '',  
@TaxId varchar(50) = '',  
@ExpenseCategory varchar(50) = '',  
@AutoTruckInsurance varchar(50) = '',  
@VendorSubCategoryId int,  
@VendorStatus nvarchar(50)='',  
@Website nvarchar(100)='',  
@ContactExten nvarchar(6)='',
@Vendrosource nvarchar(500)=null,
@AddressID int=null,
@PaymentTerms nvarchar(500)=null,
@PaymentMethod nvarchar(500)=null,
@TempID nvarchar(500)=null 
as  
if exists(select 1 from tblVendors where VendorId=@vendor_id)  
begin  
		update tblVendors  
		set  
		VendorName=@vendor_name,  
		VendorCategoryId=@vendor_category,  
		ContactPerson=@contact_person,  
		ContactNumber= @contact_number,  
		Fax=@fax,  
		Email=@email,  
		[Address]=@address,  
		Notes=@notes,  
		ManufacturerType = @ManufacturerType,  
		BillingAddress = @BillingAddress,  
		TaxId = @TaxId,  
		ExpenseCategory = @ExpenseCategory,  
		AutoTruckInsurance = @AutoTruckInsurance,  
		VendorSubCategoryId=@VendorSubCategoryId,  
		VendorStatus=@VendorStatus,  
		Website=@Website,  
		ContactExten = @ContactExten,
		Vendrosource = @Vendrosource,
		AddressID = @AddressID,
		PaymentTerms = @PaymentTerms,
		PaymentMethod = @PaymentMethod
		where VendorId = @vendor_id  
end  
else  
begin
		
		declare @temptbl table
		(
			ID int
		)
		
		insert into tblVendors(
		VendorName,VendorCategoryId,
		ContactPerson,ContactNumber,
		Fax,Email,[Address],Notes,
		ManufacturerType,BillingAddress,
		TaxId,ExpenseCategory,
		AutoTruckInsurance,VendorSubCategoryId,
		VendorStatus,Website,
		ContactExten,Vendrosource,
		AddressID,PaymentTerms,PaymentMethod)   
		output inserted.VendorId into @temptbl
		values(
		@vendor_name,@vendor_category,
		@contact_person,@contact_number,
		@fax,@email,
		@address,@notes,
		@ManufacturerType,@BillingAddress,
		@TaxId,@ExpenseCategory,
		@AutoTruckInsurance,@VendorSubCategoryId,
		@VendorStatus,@Website,
		@ContactExten,@Vendrosource,
		@AddressID,@PaymentTerms,@PaymentMethod) 
		
		select @vendor_id=ID from @temptbl
		
		update tblVendorEmail set 
		VendorID=@vendor_id
		where TempID=@TempID
		
		update tblVendorAddress set
		VendorId=@vendor_id
		where TempID=@TempID
		
		if(@VendorSubCategoryId > 0)
		begin
		insert into tbl_Vendor_VendorSubCat(VendorId,VendorSubCatId)
		values (@vendor_id,@VendorSubCategoryId)
		end
		
		if(@vendor_category > 0)
		begin
		insert into tbl_Vendor_VendorCat(VendorId,VendorCatId)
		values (@vendor_id,@vendor_category)
		end
end

go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorEmail]
GO

drop TYPE VendorEmail

go


CREATE TYPE [dbo].[VendorEmail] AS TABLE(
	[VendorId] [int] NULL,
	[EmailType] [nvarchar](50) NULL,
	[SeqNo] [int] NULL,
	[Email] [nvarchar](150) NULL,
	[FName] [nvarchar](50) NULL,
	[LName] [nvarchar](50) NULL,
	[Contact] [nvarchar](max) NULL,
	[AddressID] [int] NULL,
	[TempID][nvarchar](150) NULL
)
GO

alter table tblVendorEmail add AddressID int NULL

go
alter table tblVendorEmail add TempID nvarchar(500) null
go

create proc [dbo].[sp_VendorEmail]  
@VendorId int=null,  
@action int=null,  
@AddressID int=null,
@TempID nvarchar(500)=null,
@tblVendorEmail [dbo].[VendorEmail] READONLY  
as  
if(@action=1)  
 Begin  
 SET NOCOUNT ON;  
 
	 select @VendorId=VendorID,@TempID=TempID from @tblVendorEmail
	 
	 if(exists(select *from tblVendorEmail 
	 where VendorId=@VendorId and AddressID=@AddressID))
	 begin
		 delete from tblVendorEmail
		 where VendorId=@VendorId and AddressID=@AddressID
	 END
	 ELSE if(exists(select *from tblVendorEmail 
	 where TempID=@TempID and AddressID=@AddressID))
	 begin
		 delete from tblVendorEmail
		 where TempID=@TempID and AddressID=@AddressID
	 END
	 
	 insert into tblVendorEmail(VendorId,EmailType,SeqNo,Email,FName,LName,Contact,AddressID,TempID) 
	 select VendorId,EmailType,SeqNo,Email,FName,LName,Contact,@AddressID,@TempID
	 from @tblVendorEmail 
		 
 End  
else if(@action=2)  
 Begin  
	select * from tblVendorEmail where VendorId=@VendorId  
 End
 
 go
 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_GetVendorList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_GetVendorList]
GO

 
create proc [dbo].[USP_GetVendorList]  
@FilterParams nvarchar(Max)=null,  
@FilterBy nvarchar(50)=null,  
@ManufacturerType nvarchar(20)=null,  
@VendorCategoryId nvarchar(10)=null  
as  
BEGIN  
 Declare @BaseQuery nvarchar(max)=null  
  if(@FilterBy='VendorSubCategory')  
  begin  
   set @BaseQuery= 'select distinct v.VendorName as VendorName,v.VendorId as VendorId '  
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
  
  
   