ALTER TABLE [dbo].[FamilyMember]
	ADD CONSTRAINT [FK_FamilyMember_Person]
	FOREIGN KEY (PersonId)
	REFERENCES [Person] (Id)
