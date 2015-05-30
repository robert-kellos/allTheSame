CREATE TABLE [dbo].[VendorCredential]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [RequirementId] INT NOT NULL, 
    [VendorWorkerId] INT NOT NULL, 
    [IsAttested] BIT NOT NULL DEFAULT 0, 
    [IsConfirmed] BIT NOT NULL DEFAULT 0, 
    [ConfirmedOn] DATETIME NULL, 
    [ConfirmedByUserId] INT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL
)
