ALTER TABLE [dbo].[Organization]
	ADD CONSTRAINT [FK__Organization_Industry]
	FOREIGN KEY (IndustryId)
	REFERENCES [Industry] (Id)
