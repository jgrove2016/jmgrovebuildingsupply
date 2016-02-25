USE [jgrove_JGP]
GO

/****** Object:  StoredProcedure [dbo].[UDP_deletevendorcategory]    Script Date: 02/25/2016 10:49:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[UDP_deletevendorcategory]
@vendorcategory_id int 
as
delete from tblVendorCategory where VendorCategpryId=@vendorcategory_id
delete from tblVendorSubCategory where VendorCategoryId=@vendorcategory_id
delete from tblProductVendor where VendorCategoryId=@vendorcategory_id

GO

