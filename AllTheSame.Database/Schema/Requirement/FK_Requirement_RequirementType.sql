ALTER TABLE [dbo].[Requirement]
	ADD CONSTRAINT [FK_Requirement_RequirementType]
	FOREIGN KEY (RequirementTypeId)
	REFERENCES [RequirementType] (Id)
