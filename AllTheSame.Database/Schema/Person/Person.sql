CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,	
    [LookUpId] UNIQUEIDENTIFIER NULL,
    [FirstName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(100) NULL, 
    [BillingAddressId] INT NULL, 
    [ShippingAddressId] INT NULL, 
    [Salutation] NVARCHAR(50) NULL, 
    [Title] NVARCHAR(50) NULL, 
    [Facebook] NVARCHAR(50) NULL, 
    [Twitter] NVARCHAR(50) NULL, 
    [HomePhone] VARCHAR(30) NULL, 
    [MobilePhone] VARCHAR(30) NULL, 
    [WorkPhone] VARCHAR(30) NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
