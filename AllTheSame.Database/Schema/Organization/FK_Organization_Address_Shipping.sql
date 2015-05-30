ALTER TABLE [dbo].[Organization]
	ADD CONSTRAINT [FK_Organization_Address_Shipping]
	FOREIGN KEY (ShippingAddressId)
	REFERENCES [Address] (Id)
