USE [SQL2008DataService]
GO

/****** Object:  Table [DAL].[Object]    Script Date: 09/23/2011 22:36:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [DAL].[Object](
	[objectId] [int] IDENTITY(1,1) NOT NULL,
	[firstName] [varchar](50) NULL,
	[middleName] [varchar](50) NULL,
	[lastName] [varchar](75) NULL,
	[createdOn] [datetime] NULL,
	[objectTypeId] [int] NULL,
	[lastEdited] [datetime] NULL,
 CONSTRAINT [PK_Object] PRIMARY KEY CLUSTERED 
(
	[objectId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [DAL].[Object]  WITH CHECK ADD  CONSTRAINT [FK_Object_ObjectType] FOREIGN KEY([objectTypeId])
REFERENCES [DAL].[ObjectType] ([objectTypeId])
GO

ALTER TABLE [DAL].[Object] CHECK CONSTRAINT [FK_Object_ObjectType]
GO

