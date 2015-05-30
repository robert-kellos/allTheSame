CREATE TABLE [dbo].[CommunityWorker_Alert]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CommunityWorkerId] INT NOT NULL, 
    [AlertId] INT NOT NULL, 
    [IsRead] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
