ALTER TABLE [dbo].[UserSession]
	ADD CONSTRAINT [FK_UserSession_User]
	FOREIGN KEY (UserId)
	REFERENCES [User] (Id)
