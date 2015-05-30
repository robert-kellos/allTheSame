ALTER TABLE [dbo].[VendorWorker_Alert]
	ADD CONSTRAINT [FK__VendorWorker_Alert__VendorWorker]
	FOREIGN KEY (VendorWorkerId)
	REFERENCES [VendorWorker] (Id)
