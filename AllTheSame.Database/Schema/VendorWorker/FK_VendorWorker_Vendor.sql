ALTER TABLE [dbo].[VendorWorker]
	ADD CONSTRAINT [FK_VendorWorker_Vendor]
	FOREIGN KEY (VendorId)
	REFERENCES [Vendor] (Id)
