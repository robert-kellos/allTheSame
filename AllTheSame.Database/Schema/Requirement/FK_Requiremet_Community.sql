ALTER TABLE [dbo].[Requirement]
	ADD CONSTRAINT [FK_Requiremet_Community]
	FOREIGN KEY (CommunityId)
	REFERENCES [Community] (Id)
