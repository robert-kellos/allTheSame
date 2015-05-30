CREATE UNIQUE INDEX [UI__CommunityWorkerAlert__CommunityWorkerId__AlertId]
	ON [dbo].[CommunityWorker_Alert]
	(CommunityWorkerId, AlertId)
