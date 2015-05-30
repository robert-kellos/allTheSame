CREATE TABLE [dbo].[Kiosk]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CommunityId] INT NULL, 
    [KioskStatusId] INT NOT NULL, 
    [Name] NVARCHAR(100) NULL, 
    [Identifier] NVARCHAR(50) NULL, 
	[IdentifierType] VARCHAR(50) NULL,
    [OnSiteLocationDesc] NVARCHAR(200) NULL, 
    [BadgesRemaining] INT NOT NULL DEFAULT 0, 
    [BadgeAlertCount] INT NOT NULL DEFAULT 0, 
    [RestartTime] DATETIME NULL, 
    [SessionMaxHours] INT NULL, 
    [LastUpdate] DATETIME NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
