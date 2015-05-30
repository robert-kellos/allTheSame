ALTER TABLE [dbo].[VendorCredential]
	ADD CONSTRAINT [FK_VendorCredential_VendorWorker]
	FOREIGN KEY (VendorWorkerId)
	REFERENCES [VendorWorker] (Id)
