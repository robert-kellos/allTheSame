ALTER TABLE [dbo].[User_Org_Permission]
	ADD CONSTRAINT [FK__User_Org_Permission__User]
	FOREIGN KEY (UserId)
	REFERENCES [User] (Id)