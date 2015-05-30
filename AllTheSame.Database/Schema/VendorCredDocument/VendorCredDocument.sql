﻿CREATE TABLE [dbo].[VendorCredDocument]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [VendorCredId] INT NOT NULL, 
    [Title] NVARCHAR(100) NULL, 
    [URL] VARCHAR(200) NULL, 
    [Text] NVARCHAR(MAX) NULL, 
    [DocType] VARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL
)
