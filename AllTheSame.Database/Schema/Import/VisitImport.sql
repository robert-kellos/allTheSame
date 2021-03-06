﻿CREATE TABLE [dbo].[VisitImport]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[VisitId] int NULL,
    [ImportId] uniqueidentifier,	
	[TimeIn] datetime,
    [TimeOut] datetime,
    [VisitType] varchar(100),
    [CommunityName] varchar(100),
    [CommunityPhone] varchar(50),
    [ResidentFirstName] varchar(50),
    [ResidentLastName] varchar(50),
    [ResidentEmail] varchar(100),
    [ResidentPhone] varchar(50),
    [VendorType] varchar(50),
	[VendorCompanyName] varchar(50),
	[VendorCompanyPhone] varchar(50),
    [VendorWorkerFirstName] varchar(50),
    [VendorWorkerLastName] varchar(50),
    [VendorWorkerEmail] varchar(50),
    [VendorWorkerPhone] varchar(50),
    [VisitorFirstName] varchar(50),
    [VisitorLastName] varchar(50),
    [VisitorEmail] varchar(50),
    [VisitorPhone] varchar(50),
	[ProcessedOn] datetime NULL,
	)
