CREATE TABLE [dbo].[ImportErrorLog]
(
	[Id] [int] PRIMARY KEY IDENTITY NOT NULL,
	[ImportId] [uniqueidentifier] NOT NULL,
	[Flat File Source Error Output Column] [varchar](max) NULL,
	[ErrorCode] [int] NULL,
	[ErrorColumn] [varchar](150) NULL,
	[ErrorDescription] [varchar](max) NULL,
)
