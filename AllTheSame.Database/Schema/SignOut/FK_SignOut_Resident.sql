ALTER TABLE [dbo].[SignOut]
	ADD CONSTRAINT [FK_SignOut_Resident]
	FOREIGN KEY (ResidentId)
	REFERENCES [Resident] (Id)
