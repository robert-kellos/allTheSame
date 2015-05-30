ALTER TABLE [dbo].[Policy]
	ADD CONSTRAINT [FK_Policy_Community]
	FOREIGN KEY (CommunityId)
	REFERENCES [Community] (Id)
