ALTER TABLE [dbo].[Role_Permission]
	ADD CONSTRAINT [UK__Role_Permission]
	UNIQUE (RoleId, PermissionId)
