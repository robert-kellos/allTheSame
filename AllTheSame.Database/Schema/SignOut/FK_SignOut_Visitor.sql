ALTER TABLE [dbo].[SignOut]
	ADD CONSTRAINT [FK_SignOut_Visitor]
	FOREIGN KEY (VisitorId)
	REFERENCES [Visitor] (Id)
