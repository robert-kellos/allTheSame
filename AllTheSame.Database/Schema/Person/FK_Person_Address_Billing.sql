ALTER TABLE [dbo].[Person]
	ADD CONSTRAINT [FK_Address_User_Billing]
	FOREIGN KEY (BillingAddressId)
	REFERENCES [Address] (Id)
