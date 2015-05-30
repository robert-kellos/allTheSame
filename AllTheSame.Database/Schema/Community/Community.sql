CREATE TABLE [dbo].[Community]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [OrgId] INT NOT NULL, 
	[CommunityTypeId] INT NULL,
	[IndustryId] INT NULL,
    [Name] NVARCHAR(100) NOT NULL, 
    [Description] NVARCHAR(500) NULL,    
    [Raiting] INT NULL, 
    [NumBeds] INT NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
