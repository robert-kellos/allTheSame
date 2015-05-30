CREATE TABLE [dbo].[Resident]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CommunityId] INT NOT NULL, 
    [PersonId] INT NOT NULL, 
    [AssistantPhone] VARCHAR(50) NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
