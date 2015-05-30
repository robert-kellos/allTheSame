ALTER TABLE [dbo].[User_Org_Permission]
	ADD CONSTRAINT [UK__User_Org_Permission]
	UNIQUE (UserId, OrgId, PermissionId)
