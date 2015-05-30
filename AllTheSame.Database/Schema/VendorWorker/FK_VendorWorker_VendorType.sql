ALTER TABLE [dbo].[VendorWorker]
	ADD CONSTRAINT [FK_VendorWorker_VendorType]
	FOREIGN KEY (VendorTypeId)
	REFERENCES [VendorType] (Id)
