ALTER TABLE [dbo].[VendorCredDocument]	
	ADD CONSTRAINT [FK_VendorCredDocuement_VendorCredential]
	FOREIGN KEY (VendorCredId)
	REFERENCES [VendorCredential] (Id)
