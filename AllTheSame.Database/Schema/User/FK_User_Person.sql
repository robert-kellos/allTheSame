ALTER TABLE [dbo].[User]
	ADD CONSTRAINT [FK_User_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
