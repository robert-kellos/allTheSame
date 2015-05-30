CREATE TABLE [dbo].[Policy]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CommunityId] INT NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [DocumentURL] VARCHAR(200) NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
