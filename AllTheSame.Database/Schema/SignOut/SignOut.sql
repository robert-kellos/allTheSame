CREATE TABLE [dbo].[SignOut]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResidentId] INT NOT NULL, 
    [VisitorId] INT NOT NULL, 
    [VendorWorkerId] INT NOT NULL, 
    [TimeOut] DATETIME NOT NULL, 
    [TimeBack] DATETIME NULL, 
    [SignOutType] VARCHAR(50) NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
