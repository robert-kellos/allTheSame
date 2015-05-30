ALTER TABLE [dbo].[VendorAdmin]
	ADD CONSTRAINT [FK_VendorAdmin_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
