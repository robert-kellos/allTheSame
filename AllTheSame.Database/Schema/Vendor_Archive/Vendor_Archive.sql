CREATE TABLE [dbo].[Vendor_Archive]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(100) NULL, 
    [OrgId] INT NULL, 
    [ArchivedVersion] binary(8) NULL,
	[ArchivedDate] datetime NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL
)
