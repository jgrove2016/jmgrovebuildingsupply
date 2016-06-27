USE [jgrove_JGP]
GO

/****** Object:  Table [dbo].[tblsalesuser_followup]    Script Date: 6/15/2016 12:48:01 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblsalesuser_followup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CustomerId] [int] NULL,
	[MeetingDate] [datetime] NULL,
	[MeetingStatus] [nvarchar](1000) NULL,
	[UserId] [int] NULL,
	[CreatedOn] [datetime] NULL CONSTRAINT [DF_tblsalesuser_followup_CreatedOn]  DEFAULT (getdate()),
 CONSTRAINT [PK_tblsalesuser_followup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


