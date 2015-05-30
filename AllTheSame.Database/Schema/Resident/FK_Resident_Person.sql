ALTER TABLE [dbo].[Resident]
	ADD CONSTRAINT [FK_Resident_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
