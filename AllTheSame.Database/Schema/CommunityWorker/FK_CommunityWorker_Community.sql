ALTER TABLE [dbo].[CommunityWorker]
	ADD CONSTRAINT [FK_CommunityWorker_Community]
	FOREIGN KEY (CommunityId)
	REFERENCES [Community] (Id)
