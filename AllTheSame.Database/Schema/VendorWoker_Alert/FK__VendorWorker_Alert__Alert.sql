ALTER TABLE [dbo].[VendorWorker_Alert]
	ADD CONSTRAINT [FK__VendorWorker_Alert__Alert]
	FOREIGN KEY (AlertId)
	REFERENCES [Alert] (Id)
