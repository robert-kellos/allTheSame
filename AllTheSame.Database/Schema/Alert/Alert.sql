CREATE TABLE [dbo].[Alert]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [AlertTypeId] INT NOT NULL, 
    [AppointmentId] INT NULL, 
    [KioskId] INT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
