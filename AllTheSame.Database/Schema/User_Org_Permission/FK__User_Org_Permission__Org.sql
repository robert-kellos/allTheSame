ALTER TABLE [dbo].[User_Org_Permission]
	ADD CONSTRAINT [FK__User_Org_Permission__Org]
	FOREIGN KEY (OrgId)
	REFERENCES [Organization] (Id)