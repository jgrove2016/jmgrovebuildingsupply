
GO

/****** Object:  Table [dbo].[tblTask]    Script Date: 06/24/2016 6:05:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblTask](
	[TaskId] [bigint] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](250) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Status] [tinyint] NOT NULL,
	[DueDate] [varchar](25) NULL,
	[Hours] [int] NULL,
	[Notes] [varchar](max) NULL,
	[Attachment] [varchar](max) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedOn] [varchar](25) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_tblTask] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[tblTaskUser]    Script Date: 06/24/2016 6:05:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblTaskUser](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[TaskId] [bigint] NOT NULL,
	[UserId] [int] NOT NULL,
	[UserType] [bit] NOT NULL,
	[Status] [tinyint] NULL,
	[Notes] [varchar](max) NULL,
	[UserAcceptance] [bit] NULL,
	[UpdatedOn] [varchar](25) NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_tblTaskUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[tblTaskUserFiles]    Script Date: 06/24/2016 6:05:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[tblTaskUserFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [bigint] NOT NULL,
	[UserId] [int] NOT NULL,
	[Attachment] [varchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_tblTaskUserFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


