ALTER TABLE [dbo].[Organization]
	ADD CONSTRAINT [FK_Organization_OrgType]
	FOREIGN KEY (OrgTypeId)
	REFERENCES [OrgType] (Id)
