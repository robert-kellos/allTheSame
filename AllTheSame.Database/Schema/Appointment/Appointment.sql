CREATE TABLE [dbo].[Appointment]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ResidentId] INT NOT NULL, 
    [VendorWorkerId] INT NOT NULL, 
    [AppointmentTypeId] INT NOT NULL, 
    [StartTime] DATETIME NOT NULL, 
    [EndTime] DATETIME NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [RemindVendor] BIT NOT NULL DEFAULT 0, 
    [AlertOnVendorSignIn] BIT NOT NULL DEFAULT 0, 
    [AlertOnVendorSignOut] BIT NOT NULL DEFAULT 0, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
