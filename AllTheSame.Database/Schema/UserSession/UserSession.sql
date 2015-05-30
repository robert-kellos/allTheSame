﻿CREATE TABLE [dbo].[UserSession]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [SessionId] UNIQUEIDENTIFIER NOT NULL, 
    [Created] DATETIME NOT NULL DEFAULT SYSUTCDATETIME (), 
    [Expiration] DATETIME NULL, 
    [IsValid] BIT NOT NULL DEFAULT 0, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 			
)
