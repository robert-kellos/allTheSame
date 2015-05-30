ALTER TABLE [dbo].[DataSync]
	ADD CONSTRAINT [FK_DataSync_Kiosk]
	FOREIGN KEY (KioskId)
	REFERENCES [Kiosk] (Id)
