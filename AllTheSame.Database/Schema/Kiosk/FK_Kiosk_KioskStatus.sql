ALTER TABLE [dbo].[Kiosk]
	ADD CONSTRAINT [FK_Kiosk_KioskStatus]
	FOREIGN KEY (KioskStatusId)
	REFERENCES [KioskStatus] (Id)
