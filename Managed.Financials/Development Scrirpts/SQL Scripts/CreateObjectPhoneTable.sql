USE [SQL2008DataService]
GO

/****** Object:  Table [DAL].[ObjectPhone]    Script Date: 09/23/2011 23:07:49 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [DAL].[ObjectPhone](
	[objectId] [int] NOT NULL,
	[phone] [varchar](50) NULL,
	[phoneType] [varchar](50) NULL,
	[phonePrimary] [char](10) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [DAL].[ObjectPhone]  WITH CHECK ADD  CONSTRAINT [FK_ObjectPhone_Object] FOREIGN KEY([objectId])
REFERENCES [DAL].[Object] ([objectId])
GO

ALTER TABLE [DAL].[ObjectPhone] CHECK CONSTRAINT [FK_ObjectPhone_Object]
GO

