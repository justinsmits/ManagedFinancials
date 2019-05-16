USE [SQL2008DataService]
GO

/****** Object:  Table [DAL].[Following]    Script Date: 09/23/2011 23:10:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [DAL].[Following](
	[objectId] [int] NOT NULL,
	[followingObjectId] [int] NOT NULL
) ON [PRIMARY]

GO

ALTER TABLE [DAL].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_Object] FOREIGN KEY([objectId])
REFERENCES [DAL].[Object] ([objectId])
GO

ALTER TABLE [DAL].[Following] CHECK CONSTRAINT [FK_Following_Object]
GO

ALTER TABLE [DAL].[Following]  WITH CHECK ADD  CONSTRAINT [FK_Following_Object1] FOREIGN KEY([followingObjectId])
REFERENCES [DAL].[Object] ([objectId])
GO

ALTER TABLE [DAL].[Following] CHECK CONSTRAINT [FK_Following_Object1]
GO

