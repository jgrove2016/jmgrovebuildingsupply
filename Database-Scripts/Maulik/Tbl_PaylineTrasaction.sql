
/****** Object:  Table [dbo].[PaylineTransaction]    Script Date: 12-03-2016 PM 07:21:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PaylineTransaction](
	[TransactionID] [int] IDENTITY(1,1) NOT NULL,
	[CardNumber] [varchar](max) NULL,
	[SecurityCode] [varchar](max) NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[ExpirationDate] [int] NULL,
	[Amount] [money] NULL,
	[Status] [bit] NULL,
	[Message] [varchar](50) NULL,
	[Response] [varchar](max) NULL,
	[Request] [varchar](max) NULL,
	[CustomerId] [int] NULL,
	[ProductId] [int] NULL,
	[CreatedOn] [datetime] NULL,
	[AuthorizationCode] [varchar](max) NULL,
	[PaylineTransectionId] [varchar](max) NULL,
 CONSTRAINT [PK_PaylineTransaction] PRIMARY KEY CLUSTERED 
(
	[TransactionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


