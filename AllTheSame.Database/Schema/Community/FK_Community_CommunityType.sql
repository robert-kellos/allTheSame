ALTER TABLE [dbo].[Community]
	ADD CONSTRAINT [FK_Community_CommunityType]
	FOREIGN KEY (CommunityTypeId)
	REFERENCES [CommunityType] (Id)
