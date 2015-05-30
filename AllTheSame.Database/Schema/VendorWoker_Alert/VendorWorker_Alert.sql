CREATE TABLE [dbo].[VendorWorker_Alert]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AlertId] INT NOT NULL, 
    [VendorWorkerId] INT NOT NULL, 
    [IsRead] BIT NOT NULL DEFAULT 0, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL
)
