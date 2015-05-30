ALTER TABLE [dbo].[Alert]		
	ADD CONSTRAINT [FK_Alert_Appointment]
	FOREIGN KEY (AppointmentId)
	REFERENCES [Appointment] (Id)
