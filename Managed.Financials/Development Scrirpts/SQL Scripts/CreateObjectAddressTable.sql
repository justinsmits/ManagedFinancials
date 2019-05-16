USE [SQL2008DataService]
GO

/****** Object:  Table [DAL].[ObjectAddress]    Script Date: 09/23/2011 22:40:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [DAL].[ObjectAddress](
	[objectId] [int] NOT NULL,
	[addressLine1] [varchar](200) NULL,
	[addressLine2] [varchar](200) NULL,
	[city] [varchar](100) NULL,
	[state] [varchar](50) NULL,
	[zip] [varchar](50) NULL,
	[country] [varchar](50) NULL,
	[addressType] [varchar](50) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [DAL].[ObjectAddress]  WITH CHECK ADD  CONSTRAINT [FK_ObjectAddress_Object] FOREIGN KEY([objectId])
REFERENCES [DAL].[Object] ([objectId])
GO

ALTER TABLE [DAL].[ObjectAddress] CHECK CONSTRAINT [FK_ObjectAddress_Object]
GO

