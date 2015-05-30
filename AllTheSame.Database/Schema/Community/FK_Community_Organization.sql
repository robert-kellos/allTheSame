ALTER TABLE [dbo].[Community]
	ADD CONSTRAINT [FK_Community_Organization]
	FOREIGN KEY (OrgId)
	REFERENCES [Organization] (Id)
