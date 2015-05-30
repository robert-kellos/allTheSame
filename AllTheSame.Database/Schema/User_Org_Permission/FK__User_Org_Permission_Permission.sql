ALTER TABLE [dbo].[User_Org_Permission]
	ADD CONSTRAINT [FK__User_Org_Permission__Permission]
	FOREIGN KEY (PermissionId)
	REFERENCES [Permission] (Id)