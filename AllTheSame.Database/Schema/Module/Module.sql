﻿CREATE TABLE [dbo].[Module]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
