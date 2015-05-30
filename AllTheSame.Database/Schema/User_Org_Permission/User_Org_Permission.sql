CREATE TABLE [dbo].[User_Org_Permission]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [OrgId] INT NOT NULL, 
    [PermissionId] INT NOT NULL, 
    [IsAllowed] BIT NOT NULL DEFAULT 0, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
