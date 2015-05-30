CREATE TABLE [dbo].[Requirement]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CommunityId] INT NOT NULL, 
    [RequirementTypeId] INT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
