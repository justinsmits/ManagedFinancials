USE [SQL2008DataService]
GO

/****** Object:  Table [DAL].[ObjectEmail]    Script Date: 09/23/2011 23:07:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [DAL].[ObjectEmail](
	[objectId] [int] NOT NULL,
	[email] [varchar](100) NULL,
	[emailType] [varchar](50) NULL,
	[primaryEmail] [char](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [DAL].[ObjectEmail]  WITH CHECK ADD  CONSTRAINT [FK_ObjectEmail_Object] FOREIGN KEY([objectId])
REFERENCES [DAL].[Object] ([objectId])
GO

ALTER TABLE [DAL].[ObjectEmail] CHECK CONSTRAINT [FK_ObjectEmail_Object]
GO

