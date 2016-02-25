USE [jgrove_JGP]
GO

/****** Object:  Table [dbo].[tblVendorSubCategory]    Script Date: 02/25/2016 10:45:42 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblVendorSubCategory](
	[VendorSubCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[VendorSubCategoryName] [nvarchar](500) NULL,
	[VendorCategoryId] [nvarchar](5) NULL,
 CONSTRAINT [PK_tblVendorSubCategory] PRIMARY KEY CLUSTERED 
(
	[VendorSubCategoryId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

