ALTER TABLE [dbo].[Appointment]
	ADD CONSTRAINT [FK_Appointment_VendorWorker]
	FOREIGN KEY (VendorWorkerId)
	REFERENCES [VendorWorker] (Id)
