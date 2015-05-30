ALTER TABLE [dbo].[Alert]
	ADD CONSTRAINT [FK_Alert_Kiosk]
	FOREIGN KEY (KioskId)
	REFERENCES [Kiosk] (Id)
