ALTER TABLE [dbo].[VendorCredential]
	ADD CONSTRAINT [FK_VenderCredential_Requirement]
	FOREIGN KEY (RequirementId)
	REFERENCES [Requirement] (Id)
