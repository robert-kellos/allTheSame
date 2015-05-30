ALTER TABLE [dbo].[Visitor]
	ADD CONSTRAINT [FK_Visitor_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
