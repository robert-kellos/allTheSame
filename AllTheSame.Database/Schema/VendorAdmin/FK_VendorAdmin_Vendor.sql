ALTER TABLE [dbo].[VendorAdmin]
	ADD CONSTRAINT [FK_VendorAdmin_Vendor]
	FOREIGN KEY (VendorId)
	REFERENCES [Vendor] (Id)
