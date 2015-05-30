ALTER TABLE [dbo].[VendorWorker]
	ADD CONSTRAINT [FK_Vendor_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
