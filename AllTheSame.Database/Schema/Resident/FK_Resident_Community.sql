ALTER TABLE [dbo].[Resident]
	ADD CONSTRAINT [FK_Resident_Community]
	FOREIGN KEY (CommunityId)
	REFERENCES [Community] (Id)
