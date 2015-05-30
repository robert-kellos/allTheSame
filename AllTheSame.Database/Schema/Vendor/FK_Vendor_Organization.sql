ALTER TABLE [dbo].[Vendor]
	ADD CONSTRAINT [FK_Vendor_Organization]
	FOREIGN KEY (OrgId)
	REFERENCES [Organization] (Id)
