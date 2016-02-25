USE [jgrove_JGP]
GO

/****** Object:  Table [dbo].[tblProductVendor]    Script Date: 02/25/2016 10:46:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblProductVendor](
	[ProductCategoryId] [int] NOT NULL,
	[ProductCategoryName] [nvarchar](500) NULL,
	[VendorCategoryId] [int] NOT NULL,
	[VendorcategoryName] [nvarchar](500) NULL
) ON [PRIMARY]

GO

