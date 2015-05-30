ALTER TABLE [dbo].[Alert]
	ADD CONSTRAINT [FK_Alert_AlertType]
	FOREIGN KEY (AlertTypeId)
	REFERENCES [AlertType] (Id)
