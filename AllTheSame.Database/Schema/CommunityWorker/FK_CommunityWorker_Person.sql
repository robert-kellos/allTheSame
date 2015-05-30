ALTER TABLE [dbo].[CommunityWorker]
	ADD CONSTRAINT [FK_CommunityWorker_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
