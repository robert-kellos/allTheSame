ALTER TABLE [dbo].[Person]
	ADD CONSTRAINT [FK_User_Address_Shipping]
	FOREIGN KEY (ShippingAddressId)
	REFERENCES [Address] (Id)
