ALTER TABLE [dbo].[FamilyMember]
	ADD CONSTRAINT [FK_FamilyMember_Resident]
	FOREIGN KEY (ResidentId)
	REFERENCES [Resident] (Id)
