ALTER TABLE [dbo].[CommunityWorker_Alert]
	ADD CONSTRAINT [FK__CommunityWorker_Alert__VendorWorker]
	FOREIGN KEY (CommunityWorkerId)
	REFERENCES [CommunityWorker] (Id)
