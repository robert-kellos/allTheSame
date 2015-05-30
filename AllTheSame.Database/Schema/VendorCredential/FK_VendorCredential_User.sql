ALTER TABLE [dbo].[VendorCredential]
	ADD CONSTRAINT [FK_VendorCredential_User]
	FOREIGN KEY (ConfirmedByUserId)
	REFERENCES [User] (Id)
