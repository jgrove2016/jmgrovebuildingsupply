

/****** Object:  StoredProcedure [dbo].[USP_GetVendorList]    Script Date: 05/20/2016 18:49:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[USP_GetVendorList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[USP_GetVendorList]
GO

/****** Object:  StoredProcedure [dbo].[USP_GetVendorList]    Script Date: 05/20/2016 18:49:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[USP_GetVendorList]  
@FilterParams nvarchar(Max)=null,  
@FilterBy nvarchar(50)=null,  
@ManufacturerType nvarchar(20)=null,  
@VendorCategoryId nvarchar(10)=null,
@VendorStatus nvarchar(20)=null  
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
		  +' where V_Vsc.VendorSubCatId=@FilterParams And vendorStatus=@VendorStatus order by VendorName'  
  end  
  else if(@FilterBy='VendorCategory')  
  begin  
   set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid '
      +' left outer join tblVendorAddress tvAdd on tv.VendorId=tvAdd.VendorId '
      +' where tvVcat.VendorCatId=@FilterParams And vendorStatus=@VendorStatus order by VendorName'  
   --set @WhereClause= @WhereClause +' AND Vc_Vsc.VendorCategoryId=@FilterParams'  
  end  
  else if(@FilterBy='ProductCategory')  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid '
      +' left outer join tblVendorAddress tvAdd on tv.VendorId=tvAdd.VendorId '
      +' where tvVcat.VendorCatId in (select * from dbo.split(@FilterParams,'','')) And vendorStatus=@VendorStatus order by VendorName'  
   --set @WhereClause= @WhereClause +' And Vc_Vsc.VendorCategoryId in (select * from dbo.split(@FilterParams,'',''))';  
  end  
  else if(@FilterBy='ProductCategoryAll')  
  begin  
  set @BaseQuery='select tv.VendorName as VendorName,tv.VendorId as VendorId,tvAdd.Id as AddressId,tvAdd.Zip from tblvendors tv '  
      +'inner join tbl_Vendor_VendorCat tvVcat '  
      +'on tv.VendorId=tvVcat.Vendorid '
      +' left outer join tblVendorAddress tvAdd on tv.VendorId=tvAdd.VendorId where tv.vendorStatus=@VendorStatus order by VendorName'      
  end    
 --else if(@FilterBy='ManufacturerType')  
 -- begin  
 --  set @WhereClause= @WhereClause +' And (ManufacturerType=''Retail'' OR ManufacturerType=''Wholesale'')';  
 -- end  
    
 print @BaseQuery  
 EXECUTE sp_executesql @BaseQuery,N'@FilterParams nvarchar(max),@FilterBy nvarchar(50),@ManufacturerType nvarchar(20),@VendorCategoryId nvarchar(10),@VendorStatus nvarchar(20)',
       @FilterParams,@FilterBy,@ManufacturerType,@VendorCategoryId,@VendorStatus 
END  
  
GO

IF EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Fax' AND Object_ID = Object_ID(N'tblVendorEmail'))
BEGIN
    ALTER TABLE dbo.tblVendorEmail DROP COLUMN Fax ;
END
ALTER TABLE dbo.tblVendorEmail ADD Fax VARCHAR(20) NULL
go

/****** Object:  StoredProcedure [dbo].[sp_VendorEmail]    Script Date: 05/20/2016 18:49:15 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_VendorEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_VendorEmail]
GO

/****** Object:  UserDefinedTableType [dbo].[VendorEmail]    Script Date: 05/20/2016 18:53:53 ******/
IF  EXISTS (SELECT * FROM sys.types st JOIN sys.schemas ss ON st.schema_id = ss.schema_id WHERE st.name = N'VendorEmail' AND ss.name = N'dbo')
DROP TYPE [dbo].[VendorEmail]
GO

/****** Object:  UserDefinedTableType [dbo].[VendorEmail]    Script Date: 05/20/2016 18:53:53 ******/
CREATE TYPE [dbo].[VendorEmail] AS TABLE(
	[VendorId] [int] NULL,
	[EmailType] [nvarchar](50) NULL,
	[SeqNo] [int] NULL,
	[Email] [nvarchar](150) NULL,
	[FName] [nvarchar](50) NULL,
	[LName] [nvarchar](50) NULL,
	[Title] [nvarchar](50) NULL,
	[Contact] [nvarchar](max) NULL,
	[Fax] [nvarchar](20) NULL,
	[AddressID] [int] NULL,
	[TempID] [nvarchar](150) NULL
)
GO




/****** Object:  StoredProcedure [dbo].[sp_VendorEmail]    Script Date: 05/20/2016 18:54:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



Create proc [dbo].[sp_VendorEmail]  
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
	 
	 insert into tblVendorEmail(VendorId,EmailType,SeqNo,Email,FName,LName,Title,Contact,Fax,AddressID,TempID) 
	 select VendorId,EmailType,SeqNo,Email,FName,LName,Title,Contact,Fax,@AddressID,@TempID
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

GO


/****** Object:  StoredProcedure [dbo].[sp_GetCategoryList]    Script Date: 05/24/2016 20:40:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_GetCategoryList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_GetCategoryList]
GO

/****** Object:  StoredProcedure [dbo].[sp_GetCategoryList]    Script Date: 05/24/2016 20:40:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Proc [dbo].[sp_GetCategoryList]
@ProductCategory nvarchar(max)=null,
@VendorCategory nvarchar(max)=null,
@action nvarchar(20)=null
as
if(@action='1')
	Begin
		select * from [tblProductMaster]
	End
if(@action='2')
	Begin
		select pvm.VendorCategoryId,vc.VendorCategoryNm from [tblProductVendorCat] pvm
		 inner join [tblVendorCategory] as vc on pvm.VendorCategoryId=vc.VendorCategpryId  
		 where ProductCategoryId in (select * from dbo.split(@ProductCategory,',')) Group By pvm.VendorCategoryId,vc.VendorCategoryNm
	End
if(@action='3')
	Begin
		select vsc.VendorSubCategoryId,vsc.VendorSubCategoryName from tbl_VendorCat_VendorSubCat as vvsm inner join tblVendorSubCategory as vsc 
		on vvsm.VendorSubCategoryId=vsc.VendorSubCategoryId where vvsm.VendorCategoryId in (select * from dbo.split(@VendorCategory,',')) Group By vsc.VendorSubCategoryId,vsc.VendorSubCategoryName
	End
	
	
--select * from [tblVendorCategory]
--select * from [tblProductVendorCat]

--select * from tblVendorSubCategory
--select * from tbl_VendorCat_VendorSubCat




GO





/****** Object:  StoredProcedure [dbo].[UPP_savevendor]    Script Date: 05/24/2016 20:41:40 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UPP_savevendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UPP_savevendor]
GO


/****** Object:  StoredProcedure [dbo].[UPP_savevendor]    Script Date: 05/24/2016 20:41:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[UPP_savevendor]  
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
@NotesTempID nvarchar(250)=null ,
@VendorCategories nvarchar(max)=null,
@VendorSubCategories nvarchar(max)=null
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
		
		if(@VendorCategories != '')
		Begin
			delete from tbl_Vendor_VendorCat where VendorId = @vendor_id
			insert into tbl_Vendor_VendorCat(VendorId,VendorCatId)
			SELECT @vendor_id,Item FROM dbo.SplitString(@VendorCategories, ',')
		End
		
		if(@VendorSubCategories != '')
		Begin
			delete from tbl_Vendor_VendorSubCat where VendorId = @vendor_id
			insert into tbl_Vendor_VendorSubCat(VendorId,VendorSubCatId)
			SELECT @vendor_id,Item FROM dbo.SplitString(@VendorSubCategories, ',')
		End
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
		
		--if(@VendorSubCategoryId > 0)
		--begin
		--insert into tbl_Vendor_VendorSubCat(VendorId,VendorSubCatId)
		--values (@vendor_id,@VendorSubCategoryId)
		--end
		
		--if(@vendor_category > 0)
		--begin
		--insert into tbl_Vendor_VendorCat(VendorId,VendorCatId)
		--values (@vendor_id,@vendor_category)
		--end
		
		if(@VendorCategories != '')
		Begin
			insert into tbl_Vendor_VendorCat(VendorId,VendorCatId)
			SELECT @vendor_id,Item FROM dbo.SplitString(@VendorCategories, ',')
		End
		
		if(@VendorSubCategories != '')
		Begin
			insert into tbl_Vendor_VendorSubCat(VendorId,VendorSubCatId)
			SELECT @vendor_id,Item FROM dbo.SplitString(@VendorSubCategories, ',')
		End
		
end
GO



/****** Object:  StoredProcedure [dbo].[sp_FetchCategories]    Script Date: 05/24/2016 20:44:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_FetchCategories]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_FetchCategories]
GO


/****** Object:  StoredProcedure [dbo].[sp_FetchCategories]    Script Date: 05/24/2016 20:44:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_FetchCategories]
@VendorId nvarchar(20)=null
as 
select pvcm.ProductCategoryId from tbl_Vendor_VendorCat as vvc inner join tblProductVendorCat as pvcm 
on vvc.VendorCatId=pvcm.VendorCategoryId where VendorId=@VendorId Group By  pvcm.ProductCategoryId

select * from tbl_Vendor_VendorCat where VendorId=@VendorId

select * from tbl_Vendor_VendorSubCat where VendorId=@VendorId



--select vvscm.VendorCategoryId  from tbl_Vendor_VendorSubCat as vvsc inner join tbl_VendorCat_VendorSubCat vvscm
--on vvsc.VendorSubCatId=vvscm.VendorSubCategoryId  where VendorId=@VendorId  Group By vvscm.VendorCategoryId
GO








