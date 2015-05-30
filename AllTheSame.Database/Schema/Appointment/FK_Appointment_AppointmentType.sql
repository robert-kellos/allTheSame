ALTER TABLE [dbo].[Appointment]
	ADD CONSTRAINT [FK_Appointment_AppointmentType]
	FOREIGN KEY (AppointmentTypeId)
	REFERENCES [AppointmentType] (Id)
