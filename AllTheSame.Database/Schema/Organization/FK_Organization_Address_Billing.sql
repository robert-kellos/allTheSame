ALTER TABLE [dbo].Organization
	ADD CONSTRAINT [FK_Organization_Address_Billing]
	FOREIGN KEY (BillingAddressId)
	REFERENCES [Address] (Id)
