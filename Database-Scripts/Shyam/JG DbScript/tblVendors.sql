USE [jgrove_JGP]
GO

/****** Object:  Table [dbo].[tblVendors]    Script Date: 02/25/2016 10:51:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblVendors](
	[VendorId] [int] IDENTITY(1,1) NOT NULL,
	[VendorName] [varchar](100) NULL,
	[VendorCategoryId] [int] NULL,
	[VendorSubCategoryId] [int] NULL,
	[ContactPerson] [varchar](100) NULL,
	[ContactNumber] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Address] [varchar](100) NULL,
	[Notes] [varchar](500) NULL,
	[ManufacturerType] [varchar](70) NULL,
	[BillingAddress] [varchar](max) NULL,
	[TaxId] [varchar](50) NULL,
	[ExpenseCategory] [varchar](50) NULL,
	[AutoTruckInsurance] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[tblVendors]  WITH CHECK ADD  CONSTRAINT [FK_tblVendors_tblVendorCategory] FOREIGN KEY([VendorCategoryId])
REFERENCES [dbo].[tblVendorCategory] ([VendorCategpryId])
GO

ALTER TABLE [dbo].[tblVendors] CHECK CONSTRAINT [FK_tblVendors_tblVendorCategory]
GO

