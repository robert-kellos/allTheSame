CREATE TABLE [dbo].[Organization]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(100) NULL,      
    [NodeId] [sys].[hierarchyid] NOT NULL ,
	[ParentNodeId] AS NodeId.GetAncestor(1) PERSISTED,
	[Level] AS NodeId.GetLevel() PERSISTED, 
    [OrgTypeId] INT NULL,	
    [BillingAddressId] INT NULL, 
    [ShippingAddressId] INT NULL, 
	[IndustryId] INT NULL,
	[Facebook] VARCHAR(50) NULL, 
    [Twitter] VARCHAR(50) NULL, 
    [GooglePlus] VARCHAR(50) NULL, 
    [AnnualRevenue] INT NULL, 
    [NumEmployees] INT NULL, 
    [WebURL] VARCHAR(100) NULL, 
    [OfficePhone] VARCHAR(50) NULL, 
	[AltPhone] VARCHAR(50) NULL, 
    [TickerSymbol] VARCHAR(10) NULL, 
    [Version] ROWVERSION NOT NULL, 
    [CreatedOn] DATETIME NULL DEFAULT GETUTCDATE(), 
    [UpdatedOn] DATETIME NULL 
)
