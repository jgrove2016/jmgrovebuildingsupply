
GO

/****** Object:  Table [dbo].[tbl_sku]    Script Date: 05/31/2016 17:40:04 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbl_sku]') AND type in (N'U'))
DROP TABLE [dbo].[tbl_sku]
GO

/****** Object:  Table [dbo].[tbl_sku]    Script Date: 05/31/2016 17:40:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tbl_sku](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[skuName] [nvarchar](max) NULL
) ON [PRIMARY]

GO



GO

/****** Object:  StoredProcedure [dbo].[sp_Sku]    Script Date: 05/31/2016 17:41:18 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_Sku]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_Sku]
GO



/****** Object:  StoredProcedure [dbo].[sp_Sku]    Script Date: 05/31/2016 17:41:18 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[sp_Sku]
@Id int=null,
@skuName nvarchar(max)=null,
@action nvarchar(10)=null
as
if(@action='1')
Begin
 insert into tbl_sku values (@skuName)
End
if(@action='2')
Begin
	select * from tbl_sku
End
if(@action='3')
Begin
	update tbl_sku set skuName=@skuName where Id=@Id
End
if(@action='4')
Begin
	delete  from tbl_sku where Id=@Id
End
GO


