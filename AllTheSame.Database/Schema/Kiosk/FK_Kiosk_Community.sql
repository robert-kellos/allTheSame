ALTER TABLE [dbo].[Kiosk]
	ADD CONSTRAINT [FK_Kiosk_Community]
	FOREIGN KEY (CommunityId)
	REFERENCES [Community] (Id)
