CREATE TABLE [dbo].[VendorAdmin]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [PersonId] INT NOT NULL, 
    [VendorId] INT NOT NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL
)
