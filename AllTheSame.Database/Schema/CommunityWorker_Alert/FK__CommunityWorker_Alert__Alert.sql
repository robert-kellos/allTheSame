ALTER TABLE [dbo].[CommunityWorker_Alert]
	ADD CONSTRAINT [FK__CommunityWorker_Alert__Alert]
	FOREIGN KEY (AlertId)
	REFERENCES [Alert] (Id)
