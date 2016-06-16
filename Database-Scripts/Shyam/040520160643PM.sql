IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'State' AND Object_ID = Object_ID(N'tblVendorAddress'))
BEGIN
   Alter table tblVendorAddress drop column State
END
Alter table tblVendorAddress add State nvarchar(50) 
go
IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Title' AND Object_ID = Object_ID(N'tblVendorEmail'))
BEGIN
   Alter table tblVendorEmail drop column Title
END
Alter table tblVendorEmail add Title nvarchar(50) 
go

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorEmail]
GO
IF EXISTS (SELECT *
           FROM   [sys].[table_types]
           WHERE  user_type_id = Type_id(N'[dbo].[VendorEmail]'))
  BEGIN
     drop TYPE VendorEmail
  END

GO

CREATE TYPE [dbo].[VendorEmail] AS TABLE(
	[VendorId] [int] NULL,
	[EmailType] [nvarchar](50) NULL,
	[SeqNo] [int] NULL,
	[Email] [nvarchar](150) NULL,
	[FName] [nvarchar](50) NULL,
	[LName] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Contact] [nvarchar](max) NULL,
	[AddressID] [int] NULL,
	[TempID] [nvarchar](150) NULL
)
GO




CREATE proc [dbo].[sp_VendorEmail]  
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
	 
	 insert into tblVendorEmail(VendorId,EmailType,SeqNo,Email,FName,LName,Title,Contact,AddressID,TempID) 
	 select VendorId,EmailType,SeqNo,Email,FName,LName,Title,Contact,@AddressID,@TempID
	 from @tblVendorEmail 
		 
 End  
else if(@action=2)  
 Begin  
	select * from tblVendorEmail where VendorId=@VendorId  
 End
 
 else if(@action=3)  
 Begin  
 if(@TempID<>'')
 begin
	select * from tblVendorEmail where VendorId=@VendorId  and AddressID=@AddressID and TempID=@TempID
	end
	else
	begin
	select * from tblVendorEmail where VendorId=@VendorId  and AddressID=@AddressID
	end
	
 End
 
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorAddress]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorAddress]
GO
IF EXISTS (SELECT *
           FROM   [sys].[table_types]
           WHERE  user_type_id = Type_id(N'[dbo].[VendorAddress]'))
  BEGIN
     drop TYPE VendorAddress
  END

GO


CREATE TYPE [dbo].[VendorAddress] AS TABLE(
	[VendorId] [int] NULL,
	[AddressType] [nvarchar](50) NULL,
	[Address] [nvarchar](500) NULL,
	[City] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Zip] [nvarchar](10) NULL,
	[Country] [nvarchar](500) NULL,
	[AddressID] [int] NULL,
	[TempID] [nvarchar](500) NULL,
	[Latitude] [nvarchar](50) NULL,
	[Longitude] [nvarchar](50) NULL
)
GO


CREATE proc [dbo].[sp_VendorAddress]  
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
	insert into tblVendorAddress(VendorId,AddressType,Address,City,Zip,Country,TempID,Latitude,Longitude,State) 
	output inserted.Id into @temptable
	select VendorId,AddressType,Address,City,Zip,Country,TempID,Latitude,Longitude,State from @tblVendorAddress 
	
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
	Longitude=t.Longitude,
	State=t.State
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
   set @BaseQuery= 'select distinct tv.VendorName as VendorName,tv.VendorId as VendorId,tv.VendorStatus,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.State,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude '  
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
   set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tv.VendorStatus,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.State,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid inner join tblVendorAddress TVAdd on tv.VendorId=TVAdd.VendorId'
      +' where tvVcat.VendorCatId=@vendorCatId and tv.ManufacturerType=@manufacturer'  
  end  
  else if(LEN(@productId)>0)  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tv.VendorStatus,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.State,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid inner join tblVendorAddress TVAdd on tv.VendorId=TVAdd.VendorId'
      +' where tvVcat.VendorCatId in  (select VendorCategoryId from tblProductVendorCat where ProductCategoryId=@productId)'
      +' and tv.ManufacturerType=@manufacturer'  
  end  
  else 
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tv.VendorStatus,tv.Address as FullAddress,TVAdd.AddressType,TVAdd.Address,TVAdd.City,TVAdd.State,TVAdd.Zip,TVAdd.Country,TVAdd.Latitude,TVAdd.Longitude from tblvendors tv '  
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
   set @BaseQuery= 'select distinct v.VendorName as VendorName,v.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip '  
          +'from tbl_Vendor_VendorSubCat V_Vsc '  
          +'inner join tblVendors v '  
          +'on V_Vsc.VendorId = v.VendorId '  
          +'inner join tbl_VendorCat_VendorSubCat Vc_Vsc '  
          +'on Vc_Vsc.VendorSubCategoryId=V_Vsc.VendorSubCatId '
		  +' left outer join tblVendorAddress tvAdd on v.VendorId=tvAdd.VendorId '
		  +' where V_Vsc.VendorSubCatId=@FilterParams'  
  end  
  else if(@FilterBy='VendorCategory')  
  begin  
   set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid '
      +' left outer join tblVendorAddress tvAdd on tv.VendorId=tvAdd.VendorId '
      +' where tvVcat.VendorCatId=@FilterParams'  
   --set @WhereClause= @WhereClause +' AND Vc_Vsc.VendorCategoryId=@FilterParams'  
  end  
  else if(@FilterBy='ProductCategory')  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid '
      +' left outer join tblVendorAddress tvAdd on tv.VendorId=tvAdd.VendorId '
      +' where tvVcat.VendorCatId in (select * from dbo.split(@FilterParams,'',''))'  
   --set @WhereClause= @WhereClause +' And Vc_Vsc.VendorCategoryId in (select * from dbo.split(@FilterParams,'',''))';  
  end  
  else if(@FilterBy='ProductCategoryAll')  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid '
      +' left outer join tblVendorAddress tvAdd on tv.VendorId=tvAdd.VendorId '      
  end    
 --else if(@FilterBy='ManufacturerType')  
 -- begin  
 --  set @WhereClause= @WhereClause +' And (ManufacturerType=''Retail'' OR ManufacturerType=''Wholesale'')';  
 -- end  
    
 print @BaseQuery  
 EXECUTE sp_executesql @BaseQuery,N'@FilterParams nvarchar(max),@FilterBy nvarchar(50),@ManufacturerType nvarchar(20),@VendorCategoryId nvarchar(10)',  
       @FilterParams,@FilterBy,@ManufacturerType,@VendorCategoryId  
END  
  
  
  
  go
  
--exec [USP_GetVendorList] @FilterBy='ProductCategory',@FilterParams='80,84,118'  
  
--select * from tblvendors where ManufacturerType='Retail' OR ManufacturerType='Wholesale'  

IF (EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'tbl_VendorNotes'))
BEGIN
    drop table tbl_VendorNotes
END

CREATE TABLE [dbo].[tbl_VendorNotes](
	[NotesId] [int] IDENTITY(1,1) NOT NULL,
	[VendorId] [nvarchar](50) NULL,
	[Notes] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[TempId] [nvarchar](50) NULL
 CONSTRAINT [PK_tbl_VendorNotes] PRIMARY KEY CLUSTERED 
(
	[NotesId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]


 go
 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_vendorNotes]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_vendorNotes]
GO

create proc sp_vendorNotes
@action int=null,
@NotesId int= null,
@VendorId nvarchar(50)=null,
@Notes nvarchar(max)=null,
@TempId nvarchar(250)=null
as
begin
if(@action=1)
begin
declare @CreatedOn datetime=null;
set @CreatedOn=GETDATE();
insert into tbl_VendorNotes(VendorId,Notes,CreatedOn,TempId)
values (@VendorId,@Notes,@CreatedOn,@TempId)
end
if(@action=2)
begin
if(@TempId<>'')
 begin
	select NotesId,Notes,CreatedOn from tbl_VendorNotes where VendorId=@VendorId and TempId=@TempId
	end
	else
	begin
	select NotesId,Notes,CreatedOn from tbl_VendorNotes where VendorId=@VendorId and VendorId>0
	end

end
end



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
@TempID nvarchar(500)=null, 
@NotesTempID nvarchar(250)=null 
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
		
		update tbl_VendorNotes set VendorId=@vendor_id where TempId=@NotesTempID
		
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
