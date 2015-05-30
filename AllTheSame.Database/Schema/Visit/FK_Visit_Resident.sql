ALTER TABLE [dbo].[Visit]
	ADD CONSTRAINT [FK_Visit_Resident]
	FOREIGN KEY (ResidentId)
	REFERENCES [Resident] (Id)
