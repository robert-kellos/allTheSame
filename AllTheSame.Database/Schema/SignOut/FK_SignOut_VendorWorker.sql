ALTER TABLE [dbo].[SignOut]
	ADD CONSTRAINT [FK_SignOut_VendorWorker]
	FOREIGN KEY (VendorWorkerId)
	REFERENCES [VendorWorker] (Id)
