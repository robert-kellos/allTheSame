ALTER TABLE [dbo].[Appointment]
	ADD CONSTRAINT [FK_Appointment_Resident]
	FOREIGN KEY (ResidentId)
	REFERENCES [Resident] (Id)
