CREATE TABLE [dbo].[Visit]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResidentId] INT NOT NULL, 
    [VendorWorkerId] INT NULL, 
    [VisitorId] INT NULL, 
    [TimeIn] DATETIME NOT NULL, 
    [TimeOut] DATETIME NULL, 
    [VisitType] VARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL
)
