CREATE TABLE [dbo].[Address]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Line1] NVARCHAR(100) NULL, 
    [Line2] NVARCHAR(100) NULL, 
    [City] NVARCHAR(50) NULL, 
    [State] NVARCHAR(50) NULL, 
    [Country] NVARCHAR(50) NULL, 
    [PostalCode] NVARCHAR(50) NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
