ALTER TABLE [dbo].[Permission]
	ADD CONSTRAINT [FK_Permission_Module]
	FOREIGN KEY (ModuleId)
	REFERENCES [Module] (Id)
